using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class OfficeType : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mOfficeTypeId;
        private System.String mDefault;

        public OfficeType(String argConnection)
        {
            mConnection = argConnection;
        }
        public OfficeType(String argConnection, Int32 argOfficeTypeId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM OfficeType WHERE OfficeTypeId=" + argOfficeTypeId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mOfficeTypeId = (System.Int32)dr["OfficeTypeId"];
                mDefault = (System.String)dr["OfficeType"];
            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "OfficeType.OfficeType()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 OfficeTypeId
        {
            get
            {
                return mOfficeTypeId;
            }
            set
            {
                mOfficeTypeId = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "OfficeType", "OfficeTypeId");
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
                    mOfficeTypeId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[3];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@OfficeTypeId", OfficeTypeId);
                parrSP[2] = new SqlParameter("@OfficeType", Default);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspOfficeType", parrSP);
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
                parrSP[1] = new SqlParameter("@OfficeTypeId", OfficeTypeId);
                parrSP[2] = new SqlParameter("@OfficeType", Default);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspOfficeType", parrSP);
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
            return this.OfficeTypeId.ToString();
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "OfficeTypeId":
        //            return this.OfficeTypeId.ToString();
        //            break;
        //        case "OfficeType":
        //            return this.Default.ToString();
        //            break;

        //        default:
        //            return null;
        //            break;
        //    }
        //}

    }
}
