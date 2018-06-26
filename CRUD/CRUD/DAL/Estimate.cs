using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class Estimate : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mEstimateId;
        private System.String mDefault;



        public Estimate(String argConnection)
        {
            mConnection = argConnection;
        }
        public Estimate(String argConnection, Int32 argEstimateId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM Estimate WHERE EstimateId=" + argEstimateId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mEstimateId = (System.Int32)dr["EstimateId"];
                mDefault = (System.String)dr["Estimate"];

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "Estimate.Estimate()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 EstimateId
        {
            get
            {
                return mEstimateId;
            }
            set
            {
                mEstimateId = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "Estimate", "EstimateId");
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
                    mEstimateId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[3];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@EstimateId", EstimateId);
                parrSP[2] = new SqlParameter("@Estimate", Default);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspEstimate", parrSP);
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
                SqlParameter[] parrSP = new SqlParameter[3];
                enumDBTransaction pTransactionType = enumDBTransaction.spSelect;
                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@EstimateId", EstimateId);
                parrSP[2] = new SqlParameter("@Estimate", Default);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspEstimate", parrSP);
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
        public string Attributes(string argName)
        {
            switch (argName)
            {
                case "EstimateId":
                    return this.EstimateId.ToString();
                    break;
                case "Estimate":
                    return this.Default.ToString();
                    break;

                default:
                    return null;
                    break;
            }
        }

    }
}
