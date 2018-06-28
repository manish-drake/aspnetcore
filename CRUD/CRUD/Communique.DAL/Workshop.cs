using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class Workshop : IDBObject
    {

        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mWorkshopId;
        private System.String mDefault;
        private System.String mAddress;
        private System.String mMilestone;
        private System.String mCity;
        private System.String mPhone1;
        private System.String mPhone2;
        private System.String mContact;
        private System.String mContactRank;
        private System.String mContactEmail;
        private System.String mContactMobile;

        public Workshop(String argConnection)
        {
            mConnection = argConnection;
        }
        public Workshop(String argConnection, Int32 argWorkshopId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM Workshop WHERE WorkshopId=" + argWorkshopId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mWorkshopId = (System.Int32)dr["WorkshopId"];
                mDefault = (System.String)dr["Workshop"];
                mAddress = (System.String)dr["Address"];
                mMilestone = (System.String)dr["Milestone"];
                mCity = (System.String)dr["City"];
                mPhone1 = (System.String)dr["Phone1"];
                mPhone2 = (System.String)dr["Phone2"];
                mContact = (System.String)dr["Contact"];
                mContactRank = (System.String)dr["ContactRank"];
                mContactEmail = (System.String)dr["ContactEmail"];
                mContactMobile = (System.String)dr["ContactMobile"];

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "Workshop.Workshop()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 WorkshopId
        {
            get
            {
                return mWorkshopId;
            }
            set
            {
                mWorkshopId = value;
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
        public System.String Address
        {
            get
            {
                return mAddress;
            }
            set
            {
                mAddress = value;
            }
        }
        public System.String Milestone
        {
            get
            {
                return mMilestone;
            }
            set
            {
                mMilestone = value;
            }
        }
        public System.String City
        {
            get
            {
                return mCity;
            }
            set
            {
                mCity = value;
            }
        }
        public System.String Phone1
        {
            get
            {
                return mPhone1;
            }
            set
            {
                mPhone1 = value;
            }
        }
        public System.String Phone2
        {
            get
            {
                return mPhone2;
            }
            set
            {
                mPhone2 = value;
            }
        }
        public System.String Contact
        {
            get
            {
                return mContact;
            }
            set
            {
                mContact = value;
            }
        }
        public System.String ContactRank
        {
            get
            {
                return mContactRank;
            }
            set
            {
                mContactRank = value;
            }
        }
        public System.String ContactEmail
        {
            get
            {
                return mContactEmail;
            }
            set
            {
                mContactEmail = value;
            }
        }
        public System.String ContactMobile
        {
            get
            {
                return mContactMobile;
            }
            set
            {
                mContactMobile = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "Workshop", "WorkshopId");
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
                    mWorkshopId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[12];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@WorkshopId", WorkshopId);
                parrSP[2] = new SqlParameter("@Workshop", Default);
                parrSP[3] = new SqlParameter("@Address", Address);
                parrSP[4] = new SqlParameter("@Milestone", Milestone);
                parrSP[5] = new SqlParameter("@City", City);
                parrSP[6] = new SqlParameter("@Phone1", Phone1);
                parrSP[7] = new SqlParameter("@Phone2", Phone2);
                parrSP[8] = new SqlParameter("@Contact", Contact);
                parrSP[9] = new SqlParameter("@ContactRank", ContactRank);
                parrSP[10] = new SqlParameter("@ContactEmail", ContactEmail);
                parrSP[11] = new SqlParameter("@ContactMobile", ContactMobile);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspWorkshop", parrSP);
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
        public System.Data.DataTable Items(System.String argJoinTable)
        {
            System.String sql = System.String.Format("SELECT DISTINCT Workshop.* FROM Workshop INNER JOIN {0} ON Workshop.WorkshopId = {0}.WorkshopId ORDER BY Workshop.Workshop", argJoinTable);
            DataSet ds = SqlHelper.ExecuteDataset(Connection, CommandType.Text, sql);
            return ds.Tables[0];
        }
        public System.Data.DataTable Items()
        {
            try
            {
                SqlParameter[] parrSP = new SqlParameter[12];
                enumDBTransaction pTransactionType = enumDBTransaction.spSelect;
                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@WorkshopId", WorkshopId);
                parrSP[2] = new SqlParameter("@Workshop", Default);
                parrSP[3] = new SqlParameter("@Address", Address);
                parrSP[4] = new SqlParameter("@Milestone", Milestone);
                parrSP[5] = new SqlParameter("@City", City);
                parrSP[6] = new SqlParameter("@Phone1", Phone1);
                parrSP[7] = new SqlParameter("@Phone2", Phone2);
                parrSP[8] = new SqlParameter("@Contact", Contact);
                parrSP[9] = new SqlParameter("@ContactRank", ContactRank);
                parrSP[10] = new SqlParameter("@ContactEmail", ContactEmail);
                parrSP[11] = new SqlParameter("@ContactMobile", ContactMobile);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspWorkshop", parrSP);
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
            return this.Default.ToString();
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "WorkshopId":
        //            return this.WorkshopId.ToString();
        //            break;
        //        case "Workshop":
        //            return this.Default.ToString();
        //            break;
        //        case "Address":
        //            return this.Address.ToString();
        //            break;
        //        case "Milestone":
        //            return this.Milestone.ToString();
        //            break;
        //        case "City":
        //            return this.City.ToString();
        //            break;
        //        case "Phone1":
        //            return this.Phone1.ToString();
        //            break;
        //        case "Phone2":
        //            return this.Phone2.ToString();
        //            break;
        //        case "Contact":
        //            return this.Contact.ToString();
        //            break;
        //        case "ContactRank":
        //            return this.ContactRank.ToString();
        //            break;
        //        case "ContactEmail":
        //            return this.ContactEmail.ToString();
        //            break;
        //        case "ContactMobile":
        //            return this.ContactMobile.ToString();
        //            break;

        //        default:
        //            return null;
        //            break;
        //    }
        //}

    }
}
