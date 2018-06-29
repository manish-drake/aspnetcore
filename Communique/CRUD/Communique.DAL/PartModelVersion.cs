using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class PartModelVersion : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mPartModelVersionId;
        private ModelVersion mModelVersion;
        private Part mPart;



        public PartModelVersion(String argConnection)
        {
            mConnection = argConnection;
        }
        public PartModelVersion(String argConnection, Int32 argPartModelVersionId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM PartModelVersion WHERE PartModelVersionId=" + argPartModelVersionId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mPartModelVersionId = (System.Int32)dr["PartModelVersionId"];
                mModelVersion = new ModelVersion(Connection, (int)dr["ModelVersionId"]);
                mPart = new Part(Connection, (int)dr["PartId"]);

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "PartModelVersion.PartModelVersion()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 PartModelVersionId
        {
            get
            {
                return mPartModelVersionId;
            }
            set
            {
                mPartModelVersionId = value;
            }
        }
        public ModelVersion ModelVersion
        {
            get
            {
                if (mModelVersion == null)
                {
                    mModelVersion = new ModelVersion(Connection);
                }
                return mModelVersion;
            }
            set
            {
                mModelVersion = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "PartModelVersion", "PartModelVersionId");
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
                    mPartModelVersionId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[4];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@PartModelVersionId", PartModelVersionId);
                parrSP[2] = new SqlParameter("@ModelVersionId", ModelVersion.ModelVersionId);
                parrSP[3] = new SqlParameter("@PartId", Part.PartId);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspPartModelVersion", parrSP);
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
                parrSP[1] = new SqlParameter("@PartModelVersionId", PartModelVersionId);
                parrSP[2] = new SqlParameter("@ModelVersionId", ModelVersion.ModelVersionId);
                parrSP[3] = new SqlParameter("@PartId", Part.PartId);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspPartModelVersion", parrSP);
                ds.Tables[0].TableName = "PartModelVersion";
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
            return this.PartModelVersionId.ToString();
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "PartModelVersionId":
        //            return this.PartModelVersionId.ToString();
        //            break;
        //        case "ModelVersionId":
        //            return this.ModelVersion.ModelVersionId.ToString();
        //            break;
        //        case "PartId":
        //            return this.Part.PartId.ToString();
        //            break;

        //        default:
        //            return null;
        //            break;
        //    }
        //}

    }
}
