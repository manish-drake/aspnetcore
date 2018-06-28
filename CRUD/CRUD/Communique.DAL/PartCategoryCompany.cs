using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class PartCategoryCompany : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mPartCategoryCompanyId;
        private Part mPart;
        private InsuranceCompany mInsuranceCompany;
        private PartCategory mPartCategory;

        public PartCategoryCompany(String argConnection)
        {
            mConnection = argConnection;
        }
        public PartCategoryCompany(String argConnection, Int32 argPartCategoryCompanyId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM PartCategoryCompany WHERE PartCategoryCompanyId=" + argPartCategoryCompanyId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mPartCategoryCompanyId = (System.Int32)dr["PartCategoryCompanyId"];
                mPart = new Part(Connection, (int)dr["PartId"]);
                mInsuranceCompany = new InsuranceCompany(Connection, (int)dr["InsuranceCompanyId"]);
                mPartCategory = new PartCategory(Connection, (int)dr["PartCategoryId"]);

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "PartCategoryCompany.PartCategoryCompany()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 PartCategoryCompanyId
        {
            get
            {
                return mPartCategoryCompanyId;
            }
            set
            {
                mPartCategoryCompanyId = value;
            }
        }
        public Part Part
        {
            get
            {
                if (mPart == null)
                {
                    mPart = new Part(Connection);
                }
                return mPart;
            }
            set
            {
                mPart = value;
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
        public PartCategory PartCategory
        {
            get
            {
                if (mPartCategory == null)
                {
                    mPartCategory = new PartCategory(Connection);
                }
                return mPartCategory;
            }
            set
            {
                mPartCategory = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "PartCategoryCompany", "PartCategoryCompanyId");
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
                    mPartCategoryCompanyId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[5];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@PartCategoryCompanyId", PartCategoryCompanyId);
                parrSP[2] = new SqlParameter("@PartId", Part.PartId);
                parrSP[3] = new SqlParameter("@InsuranceCompanyId", InsuranceCompany.InsuranceCompanyId);
                parrSP[4] = new SqlParameter("@PartCategoryId", PartCategory.PartCategoryId);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspPartCategoryCompany", parrSP);
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
                SqlParameter[] parrSP = new SqlParameter[5];
                enumDBTransaction pTransactionType = enumDBTransaction.spSelect;
                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@PartCategoryCompanyId", PartCategoryCompanyId);
                parrSP[2] = new SqlParameter("@PartId", Part.PartId);
                parrSP[3] = new SqlParameter("@InsuranceCompanyId", InsuranceCompany.InsuranceCompanyId);
                parrSP[4] = new SqlParameter("@PartCategoryId", PartCategory.PartCategoryId);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspPartCategoryCompany", parrSP);
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
            return this.PartCategoryCompanyId.ToString();
        }
        public string Attributes(string argName)
        {
            switch (argName)
            {
                case "PartCategoryCompanyId":
                    return this.PartCategoryCompanyId.ToString();
                    break;
                case "PartId":
                    return this.Part.PartId.ToString();
                    break;
                case "InsuranceCompanyId":
                    return this.InsuranceCompany.InsuranceCompanyId.ToString();
                    break;
                case "PartCategoryId":
                    return this.PartCategory.PartCategoryId.ToString();
                    break;

                default:
                    return null;
                    break;
            }
        }

    }
}
