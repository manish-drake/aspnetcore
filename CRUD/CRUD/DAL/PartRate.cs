using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class PartRate : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mPartRateId;
        private Part mPart;
        private System.Single mRate;
        private System.DateTime mDtWEF;

        public PartRate(String argConnection)
        {
            mConnection = argConnection;
        }

        public PartRate(String argConnection, Int32 argPartRateId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM PartRate WHERE PartRateId=" + argPartRateId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mPartRateId = (System.Int32)dr["PartRateId"];
                mPart = new Part(Connection, (int)dr["PartId"]);
                mRate = (System.Single)dr["Rate"];
                mDtWEF = (System.DateTime)dr["DtWEF"];
            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "PartRate.PartRate()";
                throw ex;
            }
            dr.Close();
        }

        public System.Int32 PartRateId
        {
            get
            {
                return mPartRateId;
            }
            set
            {
                mPartRateId = value;
            }
        }
        public Part Part
        {
            get
            {
                if (mPart == null)
                {
                    mPart = new Part(Connection);
                }
                return mPart;
            }
            set
            {
                mPart = value;
            }
        }
        public System.Single Rate
        {
            get
            {
                return mRate;
            }
            set
            {
                mRate = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "PartRate", "PartRateId");
                }
                return mIdentity;
            }
        }

        public string Transaction(enumDBTransaction argTransactionType)
        {
            try
            {
                enumDBTransaction pTransactionType = argTransactionType;
                if (mPartRateId <= 0)
                {
                    mPartRateId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[5];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@PartRateId", PartRateId);
                parrSP[2] = new SqlParameter("@PartId", Part.PartId);
                parrSP[3] = new SqlParameter("@Rate", Rate);
                parrSP[4] = new SqlParameter("@DtWEF", DtWEF);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspPartRate", parrSP);
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
                parrSP[1] = new SqlParameter("@PartRateId", PartRateId);
                parrSP[2] = new SqlParameter("@PartId", Part.PartId);
                parrSP[3] = new SqlParameter("@Rate", Rate);
                parrSP[4] = new SqlParameter("@DtWEF", DtWEF);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspPartRate", parrSP);
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
            return this.PartRateId.ToString();
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "PartRateId":
        //            return this.PartRateId.ToString();
        //            break;
        //        case "PartId":
        //            return this.Part.PartId.ToString();
        //            break;
        //        case "Rate":
        //            return this.Rate.ToString();
        //            break;
        //        case "DtWEF":
        //            return this.DtWEF.ToString();
        //            break;
        //        default:
        //            return null;
        //            break;
        //    }
        //}
    }
}
