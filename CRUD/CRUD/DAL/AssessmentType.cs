using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationBlocks.Data;


namespace CRUD.DAL
{
    public class AssessmentType
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mAssessmentTypeId;
        private System.String mDefault;

        public AssessmentType(String argConnection)
        {
            mConnection = argConnection;
        }
        public AssessmentType(String argConnection, Int32 argAssessmentTypeId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM AssessmentType WHERE AssessmentTypeId=" + argAssessmentTypeId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mAssessmentTypeId = (System.Int32)dr["AssessmentTypeId"];
                mDefault = (System.String)dr["AssessmentType"];
            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "AssessmentType.AssessmentType()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 AssessmentTypeId
        {
            get
            {
                return mAssessmentTypeId;
            }
            set
            {
                mAssessmentTypeId = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "AssessmentType", "AssessmentTypeId");
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
                    mAssessmentTypeId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[3];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@AssessmentTypeId", AssessmentTypeId);
                parrSP[2] = new SqlParameter("@AssessmentType", Default);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspAssessmentType", parrSP);
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
                parrSP[1] = new SqlParameter("@AssessmentTypeId", AssessmentTypeId);
                parrSP[2] = new SqlParameter("@AssessmentType", Default);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspAssessmentType", parrSP);
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
            return this.Default;
        }
        public string Attributes(string argName)
        {
            switch (argName)
            {
                case "AssessmentTypeId":
                    return this.AssessmentTypeId.ToString();
                    break;
                case "AssessmentType":
                    return this.Default.ToString();
                    break;

                default:
                    return null;
                    break;
            }
        }
    }
}
