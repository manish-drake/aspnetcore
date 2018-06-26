using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class PartCategory : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mPartCategoryId;
        private System.String mDefault;
        private System.Single mDepreciation;
        private System.DateTime mDtWEF;

        private DepreciationSchedules mDepreciationSchedules;

        public DepreciationSchedules DepreciationSchedules
        {
            get
            {
                if (mDepreciationSchedules == null)
                {
                    mDepreciationSchedules = new DepreciationSchedules();
                    string sql = "";
                    sql += "SELECT DepreciationScheduleId ";
                    sql += "FROM DepreciationSchedule ";
                    sql += "WHERE PartCategoryId = " + this.mPartCategoryId;
                    SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, sql);
                    while (dr.Read())
                    {
                        DepreciationSchedule fss = new DepreciationSchedule(Connection, (int)dr["DepreciationScheduleId"]);
                        mDepreciationSchedules.Add(fss, dr["DepreciationScheduleId"].ToString());
                    }
                    dr.Close();
                }
                return mDepreciationSchedules;
            }
            set { mDepreciationSchedules = value; }
        }




        public PartCategory(String argConnection)
        {
            mConnection = argConnection;

        }
        public PartCategory(String argConnection, Int32 argPartCategoryId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM PartCategory WHERE PartCategoryId=" + argPartCategoryId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mPartCategoryId = (System.Int32)dr["PartCategoryId"];
                mDefault = (System.String)dr["PartCategory"];
                mDepreciation = (System.Single)dr["Depreciation"];
                mDtWEF = (System.DateTime)dr["DtWEF"];


            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "PartCategory.PartCategory()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 PartCategoryId
        {
            get
            {
                return mPartCategoryId;
            }
            set
            {
                mPartCategoryId = value;
            }
        }
        public System.String Default
        {
            get
            {
                return mDefault;
            }
            set
            {
                mDefault = value;
            }
        }
        public System.Single Depreciation
        {
            get
            {
                return mDepreciation;
            }
            set
            {
                mDepreciation = value;
            }
        }
        public System.DateTime DtWEF
        {
            get
            {
                if (mDtWEF == DateTime.MinValue)
                {
                    Environment.Variables vars = new Environment.Variables(Connection);
                    mDtWEF = DateTime.Parse(vars["MinimumDate"].Value);
                }
                return mDtWEF;
            }
            set
            {
                mDtWEF = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "PartCategory", "PartCategoryId");
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
                    mPartCategoryId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[5];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@PartCategoryId", PartCategoryId);
                parrSP[2] = new SqlParameter("@PartCategory", Default);
                parrSP[3] = new SqlParameter("@Depreciation", Depreciation);
                parrSP[4] = new SqlParameter("@DtWEF", DtWEF);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspPartCategory", parrSP);

                if (DepreciationSchedules.IsDirty)
                {
                    foreach (DepreciationSchedule fss in this.DepreciationSchedules.Values)
                    {
                        fss.PartCategory = this;
                        fss.Transaction(pTransactionType);
                    }
                }
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
                SqlParameter[] parrSP = new SqlParameter[5];
                enumDBTransaction pTransactionType = enumDBTransaction.spSelect;
                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@PartCategoryId", PartCategoryId);
                parrSP[2] = new SqlParameter("@PartCategory", Default);
                parrSP[3] = new SqlParameter("@Depreciation", Depreciation);
                parrSP[4] = new SqlParameter("@DtWEF", DateTime.Now.Date);



                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspPartCategory", parrSP);
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
            return this.Default;
        }
        public System.Double GetDepreciation(System.DateTime argForPurchase, System.DateTime argDateOfLoss)
        {

            if (this.DepreciationSchedules.Count <= 0)
            {
                return this.Depreciation;
            }
            else
            {
                foreach (DepreciationSchedule var in this.DepreciationSchedules.Values)
                {
                    Period prd = new Period(argForPurchase, var.MonthAgeFrom, var.MonthAgeTill);
                    if (prd.BelongsTo(argDateOfLoss))
                    {
                        return var.DepreciationRate;
                    }
                }
            }
            return this.mDepreciation;
        }
        class Period
        {
            private System.DateTime mFrom;

            public System.DateTime From
            {
                get { return mFrom; }
                set { mFrom = value; }
            }
            private System.DateTime mTill;

            public System.DateTime Till
            {
                get { return mTill; }
                set { mTill = value; }
            }
            public System.Boolean BelongsTo(System.DateTime date)
            {
                System.Boolean belongs =
                    (date.CompareTo(From) >= 0) && (date.CompareTo(Till) < 0);
                return belongs;
            }
            public Period(System.DateTime date, System.Double monthFrom, System.Double monthTo)
            {
                From = date.AddMonths((int)monthFrom);
                Till = date.AddMonths((int)monthTo);
            }
        }

    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class DepreciationSchedules : System.Collections.SortedList
    {
        private bool mIsDirty;
        private int mTempKey = 0;
        public bool IsDirty
        {
            get { return mIsDirty; }
            set { mIsDirty = value; }
        }

        // Return the item at the specified index.
        public DepreciationSchedule this[int Index]
        {
            get
            {
                return (DepreciationSchedule)this[Index];
            }
        }
        public DepreciationSchedule this[string key]
        {
            get
            {
                return (DepreciationSchedule)this[key];

            }
        }
        public void Add(DepreciationSchedule fs)
        {
            string key = "temp" + (++mTempKey);
            base.Add(key, fs);
        }
        public void Add(DepreciationSchedule fs, string key)
        {
            base.Add(key, fs);
        }
    }

}
