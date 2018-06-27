using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class LicenseType : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mLicenseTypeId;
        private System.String mDefault;



        public LicenseType(String argConnection)
        {
            mConnection = argConnection;
        }
        public LicenseType(String argConnection, Int32 argLicenseTypeId)
        {
            mConnection = argConnection;
            if (argLicenseTypeId < 0)
            {
                System.String sql = "SELECT MAX(LicenseTypeId) AS LicenseTypeId FROM LicenseType";
                SqlDataReader ltDR =
                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Connection, CommandType.Text, sql);
                if (ltDR.Read())
                {
                    argLicenseTypeId = ltDR.GetInt32(ltDR.GetOrdinal("LicenseTypeId"));
                }
                ltDR.Close();
            }
            string pstrSql = "SELECT * FROM LicenseType WHERE LicenseTypeId=" + argLicenseTypeId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mLicenseTypeId = (System.Int32)dr["LicenseTypeId"];
                mDefault = (System.String)dr["LicenseType"];

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "LicenseType.LicenseType()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 LicenseTypeId
        {
            get
            {
                return mLicenseTypeId;
            }
            set
            {
                mLicenseTypeId = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "LicenseType", "LicenseTypeId");
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
                    mLicenseTypeId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[3];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@LicenseTypeId", LicenseTypeId);
                parrSP[2] = new SqlParameter("@LicenseType", Default);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspLicenseType", parrSP);
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
                parrSP[1] = new SqlParameter("@LicenseTypeId", LicenseTypeId);
                parrSP[2] = new SqlParameter("@LicenseType", Default);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspLicenseType", parrSP);
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
            return this.LicenseTypeId.ToString();
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "LicenseTypeId":
        //            return this.LicenseTypeId.ToString();
        //            break;
        //        case "LicenseType":
        //            return this.Default.ToString();
        //            break;

        //        default:
        //            return null;
        //            break;
        //    }
        //}

    }
}
