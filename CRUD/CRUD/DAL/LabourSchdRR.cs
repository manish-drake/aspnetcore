using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class LabourSchdRR : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mLabourSchdRRId;
        private Part mPart;
        private System.Double mRate;

        public LabourSchdRR(String argConnection)
        {
            mConnection = argConnection;
        }
        public LabourSchdRR(String argConnection, Int32 argLabourSchdRRId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM LabourSchdRR WHERE LabourSchdRRId=" + argLabourSchdRRId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mLabourSchdRRId = (System.Int32)dr["LabourSchdRRId"];
                mPart = new Part(Connection, (int)dr["PartId"]);
                mRate = (System.Double)dr["Rate"];

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "LabourSchdRR.LabourSchdRR()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 LabourSchdRRId
        {
            get
            {
                return mLabourSchdRRId;
            }
            set
            {
                mLabourSchdRRId = value;
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
        public System.Double Rate
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
                    mIdentity = new OSN.Generic.Identity(Connection, "LabourSchdRR", "LabourSchdRRId");
                }
                return mIdentity;
            }
        }

        public string Transaction(enumDBTransaction argTransactionType)
        {
            try
            {
                enumDBTransaction pTransactionType = argTransactionType;
                if (mLabourSchdRRId <= 0)
                {
                    mLabourSchdRRId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[4];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@LabourSchdRRId", LabourSchdRRId);
                parrSP[2] = new SqlParameter("@PartId", Part.PartId);
                parrSP[3] = new SqlParameter("@Rate", Rate);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspLabourSchdRR", parrSP);
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
                SqlParameter[] parrSP = new SqlParameter[4];
                enumDBTransaction pTransactionType = enumDBTransaction.spSelect;
                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@LabourSchdRRId", LabourSchdRRId);
                parrSP[2] = new SqlParameter("@PartId", Part.PartId);
                parrSP[3] = new SqlParameter("@Rate", Rate);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspLabourSchdRR", parrSP);
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
            return this.LabourSchdRRId.ToString();
        }
        public string Attributes(string argName)
        {
            switch (argName)
            {
                case "LabourSchdRRId":
                    return this.LabourSchdRRId.ToString();
                    break;
                case "PartId":
                    return this.Part.PartId.ToString();
                    break;
                case "Rate":
                    return this.Rate.ToString();
                    break;

                default:
                    return null;
                    break;
            }
        }

    }
}
