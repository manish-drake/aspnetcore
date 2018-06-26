using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class PaintType : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mPaintTypeId;
        private System.String mDefault;
        private System.String mPaintType;

        public PaintType(String argConnection)
        {
            mConnection = argConnection;
        }
        public PaintType(String argConnection, Int32 argPaintTypeId)
        {
            mConnection = argConnection;
            if (argPaintTypeId < 0)
            {
                System.String sql = "SELECT MAX(PaintTypeId) AS PaintTypeId FROM PaintType";
                SqlDataReader ltDR =
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Connection, CommandType.Text, sql);
                if (ltDR.Read())
                {
                    argPaintTypeId = ltDR.GetInt32(ltDR.GetOrdinal("PaintTypeId"));
                }
                ltDR.Close();
            }
            string pstrSql = "SELECT * FROM PaintType WHERE PaintTypeId=" + argPaintTypeId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mPaintTypeId = (System.Int32)dr["PaintTypeId"];

                mPaintType = (System.String)dr["PaintType"];

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "PaintType.PaintType()";
                //throw ex;
            }
            dr.Close();
        }


        public System.Int32 PaintTypeId
        {
            get
            {
                return mPaintTypeId;
            }
            set
            {
                mPaintTypeId = value;
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
        public System.String PaintTypeName
        {
            get
            {
                return mPaintType;
            }
            set
            {
                mPaintType = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "PaintType", "PaintTypeId");
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
                    mPaintTypeId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[3];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@PaintTypeId", PaintTypeId);
                parrSP[2] = new SqlParameter("@PaintType", PaintTypeName);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspPaintType", parrSP);
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
                parrSP[1] = new SqlParameter("@PaintTypeId", PaintTypeId);
                parrSP[2] = new SqlParameter("@PaintType", PaintTypeName);

                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspPaintType", parrSP);
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
                case "PaintTypeId":
                    return this.PaintTypeId.ToString();
                    break;
                case "PaintType":
                    return this.Default.ToString();
                    break;

                default:
                    return null;
                    break;
            }
        }
    }
}
