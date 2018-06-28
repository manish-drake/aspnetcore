using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class Enclosure : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mEnclosureId;
        private System.String mDefault;

        public Enclosure(String argConnection)
        {
            mConnection = argConnection;
        }
        public Enclosure(String argConnection, Int32 argEnclosureId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM Enclosure WHERE EnclosureId=" + argEnclosureId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mEnclosureId = (System.Int32)dr["EnclosureId"];
                mDefault = (System.String)dr["Enclosure"];

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "Enclosure.Enclosure()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 EnclosureId
        {
            get
            {
                return mEnclosureId;
            }
            set
            {
                mEnclosureId = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "Enclosure", "EnclosureId");
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
                    mEnclosureId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[3];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@EnclosureId", EnclosureId);
                parrSP[2] = new SqlParameter("@Enclosure", Default);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspEnclosure", parrSP);
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
                parrSP[1] = new SqlParameter("@EnclosureId", EnclosureId);
                parrSP[2] = new SqlParameter("@Enclosure", Default);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspEnclosure", parrSP);
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
            return this.EnclosureId.ToString();
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "EnclosureId":
        //            return this.EnclosureId.ToString();
        //            break;
        //        case "Enclosure":
        //            return this.Default.ToString();
        //            break;

        //        default:
        //            return null;
        //            break;
        //    }
        //}

    }
}
