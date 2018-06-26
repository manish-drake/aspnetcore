using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using CRUD.DAL;

namespace CRUD.DAL
{
    public class LabourSchdDB : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mLabourSchdDBId;
        private Part mPart;
        private System.Double mRate;

        public LabourSchdDB(String argConnection)
        {
            mConnection = argConnection;
        }
        public LabourSchdDB(String argConnection, Int32 argLabourSchdDBId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM LabourSchdDB WHERE LabourSchdDBId=" + argLabourSchdDBId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mLabourSchdDBId = (System.Int32)dr["LabourSchdDBId"];
                mPart = new Part(Connection, (int)dr["PartId"]);
                mRate = (System.Double)dr["Rate"];

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "LabourSchdDB.LabourSchdDB()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 LabourSchdDBId
        {
            get
            {
                return mLabourSchdDBId;
            }
            set
            {
                mLabourSchdDBId = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "LabourSchdDB", "LabourSchdDBId");
                }
                return mIdentity;
            }
        }

        public string Transaction(enumDBTransaction argTransactionType)
        {
            try
            {
                enumDBTransaction pTransactionType = argTransactionType;
                if (mLabourSchdDBId <= 0)
                {
                    mLabourSchdDBId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[4];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@LabourSchdDBId", LabourSchdDBId);
                parrSP[2] = new SqlParameter("@PartId", Part.PartId);
                parrSP[3] = new SqlParameter("@Rate", Rate);

                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspLabourSchdDB", parrSP);
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
                parrSP[1] = new SqlParameter("@LabourSchdDBId", LabourSchdDBId);
                parrSP[2] = new SqlParameter("@PartId", Part.PartId);
                parrSP[3] = new SqlParameter("@Rate", Rate);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspLabourSchdDB", parrSP);
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
            return this.LabourSchdDBId.ToString();
        }
        public string Attributes(string argName)
        {
            switch (argName)
            {
                case "LabourSchdDBId":
                    return this.LabourSchdDBId.ToString();
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