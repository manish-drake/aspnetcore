using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class InsuranceCompany : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private Int32 mInsuranceCompanyId;
        private String mInsuranceCompany;
        private string mAddress;
        private string mCity;
        private string mContact;
        private string mContactRank;
        private string mContactEmail;
        private Int32 mBankAccountId;
        private String mSurveyCode;
        private BankAccount mBankAccount;

        public InsuranceCompany(String argConnection)
        {
            mConnection = argConnection;
        }

        public InsuranceCompany(String argConnection, Int32 argInsuranceCompanyId)
        {
            try
            {
                mConnection = argConnection;
                int i = 0;
                i++;
                string pstrSql = "SELECT * FROM InsuranceCompany WHERE InsuranceCompanyId=" + argInsuranceCompanyId;
                SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
                if (dr.Read())
                {
                    mInsuranceCompanyId = (int)dr["InsuranceCompanyId"];
                    this.Default = dr["InsuranceCompany"].ToString();
                    mSurveyCode = dr["SurveyCode"].ToString();
                    mBankAccount = new BankAccount(Connection, (int)dr["BankAccountId"]);
                    mBankAccountId = (int)dr["BankAccountId"];
                    //this.Address = dr["Address"].ToString();
                    //this.City = dr["City"].ToString();
                    //this.Contact = dr["Contact"].ToString();
                    //this.ContactEmail = dr["ContactEmail"].ToString();
                    //this.ContactRank = dr["ContactRank"].ToString();
                }
                else
                {
                    Exception ex = new Exception("Identifier does not exist.");
                    ex.Source = "InsuranceCompany.InsuranceCompany()";
                    throw ex;
                }
                dr.Close();
            }
            catch { }
        }

        public Int32 InsuranceCompanyId { get { return mInsuranceCompanyId; } }

        public string Default
        {
            get
            {
                return mInsuranceCompany;
            }
            set
            {
                if (value == "")
                {
                    Exception ex = new Exception("Invalid value for name of an Insurance Company.");
                    throw ex;
                }
                mInsuranceCompany = value;
            }
        }

        public string Address
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

        public BankAccount BankAccount
        {
            get
            {
                if (mBankAccount == null)
                {
                    mBankAccount = new BankAccount(Connection);
                }
                return mBankAccount;
            }
            set
            {
                mBankAccount = value;
            }
        }

        public string City { get { return mCity; } set { mCity = value; } }

        public string Contact { get { return mContact; } set { mContact = value; } }

        public string ContactRank { get { return mContactRank; } set { mContactRank = value; } }

        public string ContactEmail { get { return mContactEmail; } set { mContactEmail = value; } }

        public Int32 BankAccountId { get { return mBankAccountId; } set { mBankAccountId = value; } }

        public string SurveyCode { get { return mSurveyCode; } set { mSurveyCode = value; } }

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
                    mIdentity = new OSN.Generic.Identity(Connection, "InsuranceCompany", "InsuranceCompanyId");
                }
                return mIdentity;
            }
        }

        public string Transaction(enumDBTransaction argTransactionType)
        {
            try
            {
                SqlParameter[] parrSP = new SqlParameter[5];

                if (argTransactionType == enumDBTransaction.spAdd)
                {
                    mInsuranceCompanyId = this.Identity.New();
                }
                parrSP[0] = new SqlParameter("@InsuranceCompanyId", InsuranceCompanyId);
                parrSP[1] = new SqlParameter("@InsuranceCompany", Default);
                parrSP[2] = new SqlParameter("@Action", argTransactionType);

                if (BankAccount.ID != 0)
                { parrSP[3] = new SqlParameter("@BankAccountId", BankAccount.ID); }
                else
                { parrSP[3] = new SqlParameter("@BankAccountId", BankAccountId); }
                parrSP[4] = new SqlParameter("@SurveyCode", SurveyCode);
                //parrSP[2] = new SqlParameter("@Address", Address);
                //parrSP[3] = new SqlParameter("@City", City);
                //parrSP[4] = new SqlParameter("@Contact", Contact);
                //parrSP[5] = new SqlParameter("@ContactEmail", ContactEmail);
                //parrSP[6] = new SqlParameter("@ContactRank", ContactRank);

                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspInsuranceCompany", parrSP);
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
            SqlParameter[] spParam = new SqlParameter[5];
            spParam[0] = new SqlParameter("@InsuranceCompanyId", InsuranceCompanyId);
            spParam[1] = new SqlParameter("@InsuranceCompany", Default);
            spParam[2] = new SqlParameter("@Action", enumDBTransaction.spSelect);
            spParam[3] = new SqlParameter("@BankAccountId", BankAccount.ID);
            spParam[4] = new SqlParameter("@SurveyCode", SurveyCode);
            //spParam[2] = new SqlParameter("@Address", Address);
            //spParam[3] = new SqlParameter("@City", City);
            //spParam[4] = new SqlParameter("@Contact", Contact);
            //spParam[5] = new SqlParameter("@ContactEmail", ContactEmail);
            //spParam[6] = new SqlParameter("@ContactRank", ContactRank);

            DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspInsuranceCompany", spParam);
            return ds.Tables[0];
        }

        #endregion
        public override string ToString()
        {
            return this.Default;
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "InsuranceCompany":
        //            return this.Default;
        //            break;
        //        case "InsuranceCompanyId":
        //            return this.InsuranceCompanyId.ToString();
        //            break;
        //        case "Address":
        //            return this.Address;
        //            break;
        //        case "City":
        //            return this.City;
        //            break;
        //        case "Contact":
        //            return this.Contact;
        //            break;
        //        case "ContactEmail":
        //            return this.ContactEmail;
        //            break;
        //        case "ContactRank":
        //            return this.ContactRank;
        //            break;
        //        default:
        //            return null;
        //            break;
        //    }
        //}

    }
}
