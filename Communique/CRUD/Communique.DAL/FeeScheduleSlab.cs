using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class FeeScheduleSlab : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mFeeScheduleSlabId;
        private FeeSchedule mFeeSchedule;
        private System.Single mSlabFrom;
        private System.Single mSlabUpto;
        private System.Single mFee;
        private System.Single mReInspectionFee;

        private bool mIsDirty;

        public bool IsDirty
        {
            get { return mIsDirty; }
        }

        public FeeScheduleSlab(String argConnection)
        {
            mConnection = argConnection;
        }
        public FeeScheduleSlab(String argConnection, Int32 argFeeScheduleSlabId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM FeeScheduleSlab WHERE FeeScheduleSlabId=" + argFeeScheduleSlabId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mFeeScheduleSlabId = (System.Int32)dr["FeeScheduleSlabId"];
                mFeeSchedule = new FeeSchedule(Connection, (int)dr["FeeScheduleId"]);
                mSlabFrom = (System.Single)dr["SlabFrom"];
                mSlabUpto = (System.Single)dr["SlabUpto"];
                mFee = (System.Single)dr["Fee"];
                mReInspectionFee = (System.Single)dr["ReInspectionFee"];
            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "FeeScheduleSlab.FeeScheduleSlab()";
                throw ex;
            }
            dr.Close();
        }

        public System.Int32 FeeScheduleSlabId
        {
            get
            {
                return mFeeScheduleSlabId;
            }
            set
            {
                mIsDirty = true;
                mFeeScheduleSlabId = value;
            }
        }
        public FeeSchedule FeeSchedule
        {
            get
            {
                if (mFeeSchedule == null)
                {
                    mFeeSchedule = new FeeSchedule(Connection);
                }
                return mFeeSchedule;
            }
            set
            {
                mFeeSchedule = value;
            }
        }
        public System.Single SlabFrom
        {
            get
            {
                return mSlabFrom;
            }
            set
            {
                mIsDirty = true;
                mSlabFrom = value;
            }
        }
        public System.Single SlabUpto
        {
            get
            {
                return mSlabUpto;
            }
            set
            {
                mIsDirty = true;
                mSlabUpto = value;
            }
        }
        public System.Single Fee
        {
            get
            {
                return mFee;
            }
            set
            {
                mIsDirty = true;
                mFee = value;
            }
        }
        public System.Single ReInspectionFee
        {
            get
            {
                return mReInspectionFee;
            }
            set
            {
                mIsDirty = true;
                mReInspectionFee = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "FeeScheduleSlab", "FeeScheduleSlabId");
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
                    mFeeScheduleSlabId = this.Identity.New();
                }
                if (pTransactionType == enumDBTransaction.spEdit)
                {
                    if (mFeeScheduleSlabId <= 0)
                    {
                        pTransactionType = enumDBTransaction.spAdd;
                        mFeeScheduleSlabId = this.Identity.New();
                    }
                    else
                    {
                        if (!IsDirty)
                        {
                            pTransactionType = enumDBTransaction.spDelete;
                        }
                    }
                }
                SqlParameter[] parrSP = new SqlParameter[7];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@FeeScheduleSlabId", FeeScheduleSlabId);
                parrSP[2] = new SqlParameter("@FeeScheduleId", FeeSchedule.FeeScheduleId);
                parrSP[3] = new SqlParameter("@SlabFrom", SlabFrom);
                parrSP[4] = new SqlParameter("@SlabUpto", SlabUpto);
                parrSP[5] = new SqlParameter("@Fee", Fee);
                parrSP[6] = new SqlParameter("@ReInspectionFee", ReInspectionFee);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspFeeScheduleSlab", parrSP);
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
                SqlParameter[] parrSP = new SqlParameter[7];
                enumDBTransaction pTransactionType = enumDBTransaction.spSelect;
                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@FeeScheduleSlabId", FeeScheduleSlabId);
                parrSP[2] = new SqlParameter("@FeeScheduleId", FeeSchedule.FeeScheduleId);
                parrSP[3] = new SqlParameter("@SlabFrom", SlabFrom);
                parrSP[4] = new SqlParameter("@SlabUpto", SlabUpto);
                parrSP[5] = new SqlParameter("@Fee", Fee);
                parrSP[6] = new SqlParameter("@ReInspectionFee", ReInspectionFee);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspFeeScheduleSlab", parrSP);
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
            return this.FeeScheduleSlabId.ToString();
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "FeeScheduleSlabId":
        //            return this.FeeScheduleSlabId.ToString();
        //            break;
        //        case "FeeScheduleId":
        //            return this.FeeSchedule.FeeScheduleId.ToString();
        //            break;
        //        case "SlabFrom":
        //            return this.SlabFrom.ToString();
        //            break;
        //        case "SlabUpto":
        //            return this.SlabUpto.ToString();
        //            break;
        //        case "Fee":
        //            return this.Fee.ToString();
        //            break;
        //        case "ReInspectionFee":
        //            return this.ReInspectionFee.ToString();
        //            break;

        //        default:
        //            return null;
        //            break;
        //    }
        //}

    }
}
