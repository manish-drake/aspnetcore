using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class LabourCategory : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mLabourCategoryId;
        private System.String mDefault;



        public LabourCategory(String argConnection)
        {
            mConnection = argConnection;
        }
        public LabourCategory(String argConnection, Int32 argLabourCategoryId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM LabourCategory WHERE LabourCategoryId=" + argLabourCategoryId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mLabourCategoryId = (System.Int32)dr["LabourCategoryId"];
                mDefault = (System.String)dr["LabourCategory"];

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "LabourCategory.LabourCategory()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 LabourCategoryId
        {
            get
            {
                return mLabourCategoryId;
            }
            set
            {
                mLabourCategoryId = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "LabourCategory", "LabourCategoryId");
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
                    mLabourCategoryId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[3];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@LabourCategoryId", LabourCategoryId);
                parrSP[2] = new SqlParameter("@LabourCategory", Default);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspLabourCategory", parrSP);
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
                parrSP[1] = new SqlParameter("@LabourCategoryId", LabourCategoryId);
                parrSP[2] = new SqlParameter("@LabourCategory", Default);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspLabourCategory", parrSP);
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
            return this.LabourCategoryId.ToString();
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "LabourCategoryId":
        //            return this.LabourCategoryId.ToString();
        //            break;
        //        case "LabourCategory":
        //            return this.Default.ToString();
        //            break;

        //        default:
        //            return null;
        //            break;
        //    }
        //}

    }
}
