using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class Office : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mOfficeId;
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
        private InsuranceCompany mInsuranceCompany;
        private OfficeType mOfficeType;
        private System.String mOfficeCode;
        private System.Int32 mOfficeGSTID;
        private System.Int32 mInsuranceCompanyId;
        private OfficeGST mOfficeGST;
        private System.String mGSTNo;
        private System.String mCompanyCode;

        public Office(String argConnection)
        {
            mConnection = argConnection;
        }

        public Office(String argConnection, Int32 argOfficeId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM Office WHERE OfficeId=" + argOfficeId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mOfficeId = (System.Int32)dr["OfficeId"];
                mDefault = (System.String)dr["Office"];
                mAddress = (System.String)dr["Address"];
                mMilestone = (System.String)dr["Milestone"];
                mCity = (System.String)dr["City"];
                mPhone1 = (System.String)dr["Phone1"];
                mPhone2 = (System.String)dr["Phone2"];
                mContact = (System.String)dr["Contact"];
                mContactRank = (System.String)dr["ContactRank"];
                mContactEmail = (System.String)dr["ContactEmail"];
                mContactMobile = (System.String)dr["ContactMobile"];
                mGSTNo = (System.String)dr["GSTNo"];
                mCompanyCode = (System.String)dr["CompanyCode"];
                mInsuranceCompany = new InsuranceCompany(Connection, (int)dr["InsuranceCompanyId"]);
                mOfficeType = new OfficeType(Connection, (int)dr["OfficeTypeId"]);
                mOfficeGST = new OfficeGST(Connection, (int)dr["OfficeGSTID"]);
                mOfficeCode = (System.String)dr["OfficeCode"];
            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "Office.Office()";
                throw ex;
            }
            dr.Close();
        }

        public System.String CompanyCode
        {
            get
            {
                return mCompanyCode;
            }
            set
            {
                mCompanyCode = value;
            }
        }


        public System.Int32 OfficeId
        {
            get
            {
                return mOfficeId;
            }
            set
            {
                mOfficeId = value;
            }
        }

        public System.Int32 InsuranceCompanyId
        {
            get
            {
                return mInsuranceCompanyId;
            }
            set
            {
                mInsuranceCompanyId = value;
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
        public InsuranceCompany InsuranceCompany
        {
            get
            {
                if (mInsuranceCompany == null)
                {
                    mInsuranceCompany = new InsuranceCompany(Connection);
                }
                return mInsuranceCompany;
            }
            set
            {
                mInsuranceCompany = value;
            }
        }
        public OfficeType OfficeType
        {
            get
            {
                if (mOfficeType == null)
                {
                    mOfficeType = new OfficeType(Connection);
                }
                return mOfficeType;
            }
            set
            {
                mOfficeType = value;
            }
        }
        public System.String OfficeCode
        {
            get
            {
                return mOfficeCode;
            }
            set
            {
                mOfficeCode = value;
            }
        }

        public OfficeGST OfficeGST
        {
            get
            {
                if (mOfficeGST == null)
                {
                    mOfficeGST = new OfficeGST(Connection);
                }
                return mOfficeGST;
            }
            set
            {
                mOfficeGST = value;
            }
        }

        public System.Int32 OfficeGSTID
        {
            get
            {
                return mOfficeGSTID;
            }
            set
            {
                mOfficeGSTID = value;
            }
        }

        public System.String GSTNo
        {
            get
            {
                return mGSTNo;
            }
            set
            {
                mGSTNo = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "Office", "OfficeId");
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
                    mOfficeId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[18];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@OfficeId", OfficeId);
                parrSP[2] = new SqlParameter("@Office", Default);
                parrSP[3] = new SqlParameter("@Address", Address);
                parrSP[4] = new SqlParameter("@Milestone", Milestone);
                parrSP[5] = new SqlParameter("@City", City);
                parrSP[6] = new SqlParameter("@Phone1", Phone1);
                parrSP[7] = new SqlParameter("@Phone2", Phone2);
                parrSP[8] = new SqlParameter("@Contact", Contact);
                parrSP[9] = new SqlParameter("@ContactRank", ContactRank);
                parrSP[10] = new SqlParameter("@ContactEmail", ContactEmail);
                parrSP[11] = new SqlParameter("@ContactMobile", ContactMobile);
                parrSP[12] = new SqlParameter("@InsuranceCompanyId", InsuranceCompany.InsuranceCompanyId);
                parrSP[13] = new SqlParameter("@OfficeTypeId", OfficeType.OfficeTypeId);
                parrSP[14] = new SqlParameter("@OfficeCode", OfficeCode);
                //parrSP[15] = new SqlParameter("@StateCode", OfficeGST.StateCode);
                //parrSP[16] = new SqlParameter("@SGSTRate", OfficeGST.SGSTRate);
                //parrSP[17] = new SqlParameter("@CGSTRate", OfficeGST.CGSTRate);
                //parrSP[18] = new SqlParameter("@UGSTRate", OfficeGST.UGSTRate);
                //parrSP[19] = new SqlParameter("@IGSTRate", OfficeGST.IGSTRate);
                parrSP[15] = new SqlParameter("@OfficeGSTID", OfficeGSTID);
                parrSP[16] = new SqlParameter("@GSTNo", GSTNo);
                parrSP[17] = new SqlParameter("@CompanyCode", CompanyCode);

                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspOffice", parrSP);
                return null;
            }
            catch (Exception ex)
            {
                //OSN.Generic.EventLogHelper appLog = new OSN.Generic.EventLogHelper();
                //appLog.Source = "";
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
            System.String sql = "SELECT DISTINCT Office.*, InsuranceCompany.InsuranceCompany ";
            sql += System.String.Format("FROM ((Office INNER JOIN {0} ON {0}.DeputingOfficeId = Office.OfficeId) INNER JOIN InsuranceCompany ", argJoinTable);
            sql += "ON Office.InsuranceCompanyId = InsuranceCompany.InsuranceCompanyId) ORDER BY Office.OfficeCode ";

            DataSet ds = SqlHelper.ExecuteDataset(Connection, CommandType.Text, sql);
            return ds.Tables[0];
        }
        public System.Data.DataTable Items()
        {
            try
            {
                SqlParameter[] parrSP = new SqlParameter[18];
                enumDBTransaction pTransactionType = enumDBTransaction.spSelect;
                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@OfficeId", OfficeId);
                parrSP[2] = new SqlParameter("@Office", Default);
                parrSP[3] = new SqlParameter("@Address", Address);
                parrSP[4] = new SqlParameter("@Milestone", Milestone);
                parrSP[5] = new SqlParameter("@City", City);
                parrSP[6] = new SqlParameter("@Phone1", Phone1);
                parrSP[7] = new SqlParameter("@Phone2", Phone2);
                parrSP[8] = new SqlParameter("@Contact", Contact);
                parrSP[9] = new SqlParameter("@ContactRank", ContactRank);
                parrSP[10] = new SqlParameter("@ContactEmail", ContactEmail);
                parrSP[11] = new SqlParameter("@ContactMobile", ContactMobile);
                parrSP[12] = new SqlParameter("@InsuranceCompanyId", InsuranceCompany.InsuranceCompanyId);
                parrSP[13] = new SqlParameter("@OfficeTypeId", OfficeType.OfficeTypeId);
                parrSP[14] = new SqlParameter("@OfficeCode", OfficeCode);
                //parrSP[15] = new SqlParameter("@StateCode", OfficeGST.StateCode);
                //parrSP[16] = new SqlParameter("@SGSTRate", OfficeGST.SGSTRate);
                //parrSP[17] = new SqlParameter("@CGSTRate", OfficeGST.CGSTRate);
                //parrSP[18] = new SqlParameter("@UGSTRate", OfficeGST.UGSTRate);
                //parrSP[19] = new SqlParameter("@IGSTRate", OfficeGST.IGSTRate);
                parrSP[15] = new SqlParameter("@OfficeGSTID", OfficeGSTID);
                parrSP[16] = new SqlParameter("@GSTNo", GSTNo);
                parrSP[17] = new SqlParameter("@CompanyCode", CompanyCode);

                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspOffice", parrSP);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                //OSN.Generic.EventLogHelper appLog = new OSN.Generic.EventLogHelper();
                //appLog.Source = "";
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
            return this.OfficeCode;
        }


        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "OfficeId":
        //            return this.OfficeId.ToString();
        //            break;
        //        case "Office":
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
        //        case "InsuranceCompanyId":
        //            return this.InsuranceCompany.InsuranceCompanyId.ToString();
        //            break;
        //        case "OfficeTypeId":
        //            return this.OfficeType.OfficeTypeId.ToString();
        //            break;
        //        case "OfficeCode":
        //            return this.OfficeCode.ToString();
        //            break;
        //        default:
        //            return null;
        //            break;
        //    }
        //}

    }
}
