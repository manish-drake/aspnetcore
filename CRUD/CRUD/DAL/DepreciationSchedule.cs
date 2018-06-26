using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class DepreciationSchedule : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mDepreciationScheduleId;
        private System.Single mMonthAgeFrom;
        private System.Single mMonthAgeTill;
        private System.Single mDepreciationRate;
        private PartCategory mPartCategory;

        private bool mIsDirty;

        public bool IsDirty
        {
            get { return mIsDirty; }
        }

        public DepreciationSchedule(String argConnection)
        {
            mConnection = argConnection;
        }
        public DepreciationSchedule(String argConnection, Int32 argDepreciationScheduleId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM DepreciationSchedule WHERE DepreciationScheduleId=" + argDepreciationScheduleId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mDepreciationScheduleId = (System.Int32)dr["DepreciationScheduleId"];
                mMonthAgeFrom = (System.Single)dr["MonthAgeFrom"];
                mMonthAgeTill = (System.Single)dr["MonthAgeTill"];
                mDepreciationRate = (System.Single)dr["DepreciationRate"];
                mPartCategory = new PartCategory(Connection, (int)dr["PartCategoryId"]);
            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "DepreciationSchedule.DepreciationSchedule()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 DepreciationScheduleId
        {
            get
            {
                return mDepreciationScheduleId;
            }
            set
            {
                mIsDirty = true;
                mDepreciationScheduleId = value;
            }
        }
        public System.Single MonthAgeFrom
        {
            get
            {
                return mMonthAgeFrom;
            }
            set
            {
                mIsDirty = true;
                mMonthAgeFrom = value;
            }
        }
        public System.Single MonthAgeTill
        {
            get
            {
                return mMonthAgeTill;
            }
            set
            {
                mIsDirty = true;
                mMonthAgeTill = value;
            }
        }
        public System.Single DepreciationRate
        {
            get
            {
                return mDepreciationRate;
            }
            set
            {
                mIsDirty = true;
                mDepreciationRate = value;
            }
        }
        public PartCategory PartCategory
        {
            get
            {
                if (mPartCategory == null)
                {
                    mPartCategory = new PartCategory(Connection);
                }
                return mPartCategory;
            }
            set
            {
                mPartCategory = value;
            }
        }



        #region IDBObject Members

        public string Connection
        {
            get { return mConnection; }
        }

        public OSN.Generic.Identity Identity
        {
            get
            {
                if (mIdentity == null)
                {
                    mIdentity = new OSN.Generic.Identity(Connection, "DepreciationSchedule", "DepreciationScheduleId");
                }
                return mIdentity;
            }
        }

        public string Transaction(enumDBTransaction argTransactionType)
        {
            try
            {
                enumDBTransaction pTransactionType = argTransactionType;
                if (pTransactionType == enumDBTransaction.spAdd)
                {
                    mDepreciationScheduleId = this.Identity.New();
                }
                if (pTransactionType == enumDBTransaction.spEdit)
                {
                    if (mDepreciationScheduleId <= 0)
                    {
                        pTransactionType = enumDBTransaction.spAdd;
                        mDepreciationScheduleId = this.Identity.New();
                    }
                    else
                    {
                        if (!IsDirty)
                        {
                            pTransactionType = enumDBTransaction.spDelete;
                        }
                    }
                }
                SqlParameter[] parrSP = new SqlParameter[6];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@DepreciationScheduleId", DepreciationScheduleId);
                parrSP[2] = new SqlParameter("@MonthAgeFrom", MonthAgeFrom);
                parrSP[3] = new SqlParameter("@MonthAgeTill", MonthAgeTill);
                parrSP[4] = new SqlParameter("@DepreciationRate", DepreciationRate);
                parrSP[5] = new SqlParameter("@PartCategoryId", PartCategory.PartCategoryId);



                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspDepreciationSchedule", parrSP);
                return null;
            }
            catch (Exception ex)
            {
                //OSN.Generic.EventLogHelper appLog = new OSN.Generic.EventLogHelper();
                //appLog.Source = "Communique";
                //StringBuilder sb = new StringBuilder();
                //sb.Append("public bool Transaction(enumDBTransaction argTransactionType)");
                //sb.Append("Exception=" + ex.Message);
                //appLog.WriteEntry(sb.ToString());
                return "Transaction failed";
            }
        }

        public string Transaction(enumDBTransaction argTransactionType, System.Data.SqlClient.SqlTransaction argTransaction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public System.Data.DataTable Items()
        {
            try
            {
                SqlParameter[] parrSP = new SqlParameter[6];
                enumDBTransaction pTransactionType = enumDBTransaction.spSelect;
                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@DepreciationScheduleId", DepreciationScheduleId);
                parrSP[2] = new SqlParameter("@MonthAgeFrom", MonthAgeFrom);
                parrSP[3] = new SqlParameter("@MonthAgeTill", MonthAgeTill);
                parrSP[4] = new SqlParameter("@DepreciationRate", DepreciationRate);
                parrSP[5] = new SqlParameter("@PartCategoryId", PartCategory.PartCategoryId);



                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspDepreciationSchedule", parrSP);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                //OSN.Generic.EventLogHelper appLog = new OSN.Generic.EventLogHelper();
                //appLog.Source = "Communique";
                //StringBuilder sb = new StringBuilder();
                //sb.Append("public System.Data.DataTable Items()");
                //sb.Append("Exception=" + ex.Message);
                //appLog.WriteEntry(sb.ToString());
                return new DataTable();
            }
        }
        #endregion
        public override string ToString()
        {
            return this.DepreciationScheduleId.ToString();
        }

    }
}
