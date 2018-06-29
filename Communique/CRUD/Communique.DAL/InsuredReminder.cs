using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class InsuredReminder : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mInsuredReminderId;
        private System.String mFirstReminder;
        private System.String mSecondReminder;
        private System.String mThirdReminder;
        private Assessment mAssessment;

        public InsuredReminder(String argConnection)
        {
            mConnection = argConnection;
        }
        public InsuredReminder(String argConnection, Int32 argAssessmentId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM InsuredReminder WHERE AssessmentId=" + argAssessmentId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mInsuredReminderId = (System.Int32)dr["InsuredReminderId"];
                mFirstReminder = (System.String)dr["FirstReminder"];
                mSecondReminder = dr["SecondReminder"].GetType().Name == "DBNull" ? null : (System.String)dr["SecondReminder"];
                mThirdReminder = dr["ThirdReminder"].GetType().Name == "DBNull" ? null : (System.String)dr["ThirdReminder"];
                mAssessment = new Assessment(Connection, (int)dr["AssessmentId"]);

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");

                ex.Source = "InsuredReminder.InsuredReminder()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 InsuredReminderId
        {
            get
            {
                return mInsuredReminderId;
            }
            set
            {
                mInsuredReminderId = value;
            }
        }
        public System.String FirstReminder
        {
            get
            {
                return mFirstReminder;
            }
            set
            {
                mFirstReminder = value;
            }
        }
        public System.String SecondReminder
        {
            get
            {
                return mSecondReminder;
            }
            set
            {
                mSecondReminder = value;
            }
        }
        public System.String ThirdReminder
        {
            get
            {
                return mThirdReminder;
            }
            set
            {
                mThirdReminder = value;
            }
        }
        public Assessment Assessment
        {
            get
            {
                if (mAssessment == null)
                {
                    mAssessment = new Assessment(Connection);
                }
                return mAssessment;
            }
            set
            {
                mAssessment = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "InsuredReminder", "InsuredReminderId");
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
                    mInsuredReminderId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[6];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@InsuredReminderId", InsuredReminderId);
                parrSP[2] = new SqlParameter("@FirstReminder", FirstReminder);
                parrSP[3] = new SqlParameter("@SecondReminder", SecondReminder);
                parrSP[4] = new SqlParameter("@ThirdReminder", ThirdReminder);
                parrSP[5] = new SqlParameter("@AssessmentId", Assessment.AssessmentId);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspInsuredReminder", parrSP);
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
                SqlParameter[] parrSP = new SqlParameter[6];
                enumDBTransaction pTransactionType = enumDBTransaction.spSelect;
                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@InsuredReminderId", InsuredReminderId);
                parrSP[2] = new SqlParameter("@FirstReminder", FirstReminder);
                parrSP[3] = new SqlParameter("@SecondReminder", SecondReminder);
                parrSP[4] = new SqlParameter("@ThirdReminder", ThirdReminder);
                parrSP[5] = new SqlParameter("@AssessmentId", Assessment.AssessmentId);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspInsuredReminder", parrSP);
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
            return this.InsuredReminderId.ToString();
        }
        public string Attributes(string argName)
        {
            switch (argName)
            {
                case "InsuredReminderId":
                    return this.InsuredReminderId.ToString();
                    break;
                case "FirstReminder":
                    return this.FirstReminder.ToString();
                    break;
                case "SecondReminder":
                    return this.SecondReminder.ToString();
                    break;
                case "ThirdReminder":
                    return this.ThirdReminder.ToString();
                    break;
                case "AssessmentId":
                    return this.Assessment.AssessmentId.ToString();
                    break;

                default:
                    return null;
                    break;
            }
        }

    }
}
