using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class ThirdPartyLoss : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mThirdPartyLossId;
        private System.String mDefault;

        public ThirdPartyLoss(String argConnection)
        {
            mConnection = argConnection;
        }
        public ThirdPartyLoss(String argConnection, Int32 argThirdPartyLossId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM ThirdPartyLoss WHERE ThirdPartyLossId=" + argThirdPartyLossId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mThirdPartyLossId = (System.Int32)dr["ThirdPartyLossId"];
                mDefault = (System.String)dr["ThirdPartyLoss"];
                //mDefault = mDefault.Replace("%", "\\%");

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "ThirdPartyLoss.ThirdPartyLoss()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 ThirdPartyLossId
        {
            get
            {
                return mThirdPartyLossId;
            }
            set
            {
                mThirdPartyLossId = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "ThirdPartyLoss", "ThirdPartyLossId");
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
                    mThirdPartyLossId = this.Identity.New();
                }
                try
                {
                    CallContentManager();
                }
                catch (Exception) { }
                SqlParameter[] parrSP = new SqlParameter[3];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@ThirdPartyLossId", ThirdPartyLossId);
                parrSP[2] = new SqlParameter("@ThirdPartyLoss", Default);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspThirdPartyLoss", parrSP);

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
            cm.Context = "ThirdPartyLoss";
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
                parrSP[1] = new SqlParameter("@ThirdPartyLossId", ThirdPartyLossId);
                parrSP[2] = new SqlParameter("@ThirdPartyLoss", Default);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspThirdPartyLoss", parrSP);
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
            return this.ThirdPartyLossId.ToString();
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "ThirdPartyLossId":
        //            return this.ThirdPartyLossId.ToString();
        //            break;
        //        case "ThirdPartyLoss":
        //            return this.Default.ToString();
        //            break;

        //        default:
        //            return null;
        //            break;
        //    }
        //}

    }
}
