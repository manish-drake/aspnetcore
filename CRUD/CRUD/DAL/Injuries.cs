﻿using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class Injuries : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mInjuriesId;
        private System.String mDefault;



        public Injuries(String argConnection)
        {
            mConnection = argConnection;
        }
        public Injuries(String argConnection, Int32 argInjuriesId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM Injuries WHERE InjuriesId=" + argInjuriesId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mInjuriesId = (System.Int32)dr["InjuriesId"];
                mDefault = (System.String)dr["Injuries"];
                //mDefault = mDefault.Replace("%", "\\%");
            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "Injuries.Injuries()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 InjuriesId
        {
            get
            {
                return mInjuriesId;
            }
            set
            {
                mInjuriesId = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "Injuries", "InjuriesId");
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
                    mInjuriesId = this.Identity.New();
                }
                try
                {
                    CallContentManager();
                }
                catch (Exception) { }
                SqlParameter[] parrSP = new SqlParameter[3];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@InjuriesId", InjuriesId);
                parrSP[2] = new SqlParameter("@Injuries", Default);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspInjuries", parrSP);

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
            cm.Context = "Injuries";
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
                parrSP[1] = new SqlParameter("@InjuriesId", InjuriesId);
                parrSP[2] = new SqlParameter("@Injuries", Default);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspInjuries", parrSP);
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
            return this.InjuriesId.ToString();
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "InjuriesId":
        //            return this.InjuriesId.ToString();
        //            break;
        //        case "Injuries":
        //            return this.Default.ToString();
        //            break;

        //        default:
        //            return null;
        //            break;
        //    }
        //}

    }
}
