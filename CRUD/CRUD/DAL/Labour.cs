using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class Labour : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mLabourId;
        private System.String mDefault;
        private LabourCategory mLabourCategory;



        public Labour(String argConnection)
        {
            mConnection = argConnection;
        }
        public Labour(String argConnection, Int32 argLabourId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM Labour WHERE LabourId=" + argLabourId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mLabourId = (System.Int32)dr["LabourId"];
                mDefault = (System.String)dr["Labour"];
                mLabourCategory = new LabourCategory(Connection, (int)dr["LabourCategoryId"]);

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "Labour.Labour()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 LabourId
        {
            get
            {
                return mLabourId;
            }
            set
            {
                mLabourId = value;
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
        public LabourCategory LabourCategory
        {
            get
            {
                if (mLabourCategory == null)
                {
                    mLabourCategory = new LabourCategory(Connection);
                }
                return mLabourCategory;
            }
            set
            {
                mLabourCategory = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "Labour", "LabourId");
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
                    mLabourId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[4];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@LabourId", LabourId);
                parrSP[2] = new SqlParameter("@Labour", Default);
                parrSP[3] = new SqlParameter("@LabourCategoryId", LabourCategory.LabourCategoryId);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspLabour", parrSP);
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
                parrSP[1] = new SqlParameter("@LabourId", LabourId);
                parrSP[2] = new SqlParameter("@Labour", Default);
                parrSP[3] = new SqlParameter("@LabourCategoryId", LabourCategory.LabourCategoryId);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspLabour", parrSP);
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
            return this.Default; ;
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "LabourId":
        //            return this.LabourId.ToString();
        //            break;
        //        case "Labour":
        //            return this.Default.ToString();
        //            break;
        //        case "LabourCategoryId":
        //            return this.LabourCategory.LabourCategoryId.ToString();
        //            break;

        //        default:
        //            return null;
        //            break;
        //    }
        //}

    }
}
