using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class PartModel : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mPartModelId;
        private Part mPart;
        private Model mModel;

        public PartModel(String argConnection)
        {
            mConnection = argConnection;
        }
        public PartModel(String argConnection, Int32 argPartModelId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM PartModel WHERE PartModelId=" + argPartModelId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mPartModelId = (System.Int32)dr["PartModelId"];
                mPart = new Part(Connection, (int)dr["PartId"]);
                mModel = new Model(Connection, (int)dr["ModelId"]);

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "PartModel.PartModel()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 PartModelId
        {
            get
            {
                return mPartModelId;
            }
            set
            {
                mPartModelId = value;
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
        public Model Model
        {
            get
            {
                if (mModel == null)
                {
                    mModel = new Model(Connection);
                }
                return mModel;
            }
            set
            {
                mModel = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "PartModel", "PartModelId");
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
                    mPartModelId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[4];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@PartModelId", PartModelId);
                parrSP[2] = new SqlParameter("@PartId", Part.PartId);
                parrSP[3] = new SqlParameter("@ModelId", Model.ModelId);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspPartModel", parrSP);
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
                parrSP[1] = new SqlParameter("@PartModelId", PartModelId);
                parrSP[2] = new SqlParameter("@PartId", Part.PartId);
                parrSP[3] = new SqlParameter("@ModelId", Model.ModelId);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspPartModel", parrSP);
                ds.Tables[0].TableName = "PartModel";
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
            return this.PartModelId.ToString();
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "PartModelId":
        //            return this.PartModelId.ToString();
        //            break;
        //        case "PartId":
        //            return this.Part.PartId.ToString();
        //            break;
        //        case "ModelId":
        //            return this.Model.ModelId.ToString();
        //            break;

        //        default:
        //            return null;
        //            break;
        //    }
        //}

    }
}
