using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class Particulars : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mParticularsId;
        private System.String mDefault;



        public Particulars(String argConnection)
        {
            mConnection = argConnection;
        }
        public Particulars(String argConnection, Int32 argParticularsId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM Particulars WHERE ParticularsId=" + argParticularsId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mParticularsId = (System.Int32)dr["ParticularsId"];
                mDefault = (System.String)dr["Particulars"];

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "Particulars.Particulars()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 ParticularsId
        {
            get
            {
                return mParticularsId;
            }
            set
            {
                mParticularsId = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "Particulars", "ParticularsId");
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
                    mParticularsId = this.Identity.New();
                }
                try
                {
                    CallContentManager();
                }
                catch (Exception) { }
                SqlParameter[] parrSP = new SqlParameter[3];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@ParticularsId", ParticularsId);
                parrSP[2] = new SqlParameter("@Particulars", Default);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspParticulars", parrSP);
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

        private void CallContentManager()
        {
            ContentManager cm = new ContentManager(this.Connection);
            cm.Content = this.Default;
            cm.Context = "Particulars";
            cm.Add();
            RemoveMarkup();
        }
        private void RemoveMarkup()
        {
            //this.Default = this.Default.Replace("\\%", "^^Percentage^^;");
            this.Default = this.Default.Replace("%", "");
            this.Default.Replace("^^Percentage^^;", "%");
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
                parrSP[1] = new SqlParameter("@ParticularsId", ParticularsId);
                parrSP[2] = new SqlParameter("@Particulars", Default);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspParticulars", parrSP);
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
            return this.ParticularsId.ToString();
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "ParticularsId":
        //            return this.ParticularsId.ToString();
        //            break;
        //        case "Particulars":
        //            return this.Default.ToString();
        //            break;

        //        default:
        //            return null;
        //            break;
        //    }
        //}

    }
}
