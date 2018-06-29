using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class AssessmentChecklist : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mAssessmentChecklistId;
        private Checklist mChecklist;
        private Assessment mAssessment;
        private System.Boolean mIsChecked;

        private enumDBTransaction mTransactionType;

        public enumDBTransaction TransactionType
        {
            get { return mTransactionType; }
            set { mTransactionType = value; }
        }

        public AssessmentChecklist(String argConnection)
        {
            mConnection = argConnection;
            mTransactionType = enumDBTransaction.spNull;
        }
        public AssessmentChecklist(String argConnection, Int32 argAssessmentChecklistId)
        {

            mTransactionType = enumDBTransaction.spNull;
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM AssessmentChecklist WHERE AssessmentChecklistId=" + argAssessmentChecklistId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mAssessmentChecklistId = (System.Int32)dr["AssessmentChecklistId"];
                mChecklist = new Checklist(Connection, (int)dr["ChecklistId"]);
                mAssessment = new Assessment(Connection, (int)dr["AssessmentId"]);
                mIsChecked = (System.Boolean)dr["IsChecked"];

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "AssessmentChecklist.AssessmentChecklist()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 AssessmentChecklistId
        {
            get
            {
                return mAssessmentChecklistId;
            }
            set
            {
                mAssessmentChecklistId = value;
            }
        }
        public Checklist Checklist
        {
            get
            {
                if (mChecklist == null)
                {
                    mChecklist = new Checklist(Connection);
                }
                return mChecklist;
            }
            set
            {
                mChecklist = value;
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
        public System.Boolean IsChecked
        {
            get
            {
                return mIsChecked;
            }
            set
            {
                mIsChecked = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "AssessmentChecklist", "AssessmentChecklistId");
                }
                return mIdentity;
            }
        }

        public string Transaction(enumDBTransaction argTransactionType)
        {
            try
            {
                enumDBTransaction pTransactionType = enumDBTransaction.spNull;
                if (mTransactionType == enumDBTransaction.spNull)
                {
                    pTransactionType = argTransactionType;
                }
                else
                {
                    pTransactionType = mTransactionType; ;
                }

                if (pTransactionType == enumDBTransaction.spAdd)
                {
                    mAssessmentChecklistId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[5];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@AssessmentChecklistId", AssessmentChecklistId);
                parrSP[2] = new SqlParameter("@ChecklistId", Checklist.ChecklistId);
                parrSP[3] = new SqlParameter("@AssessmentId", Assessment.AssessmentId);
                parrSP[4] = new SqlParameter("@IsChecked", IsChecked);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspAssessmentChecklist", parrSP);
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
                parrSP[1] = new SqlParameter("@AssessmentChecklistId", AssessmentChecklistId);
                parrSP[2] = new SqlParameter("@ChecklistId", Checklist.ChecklistId);
                parrSP[3] = new SqlParameter("@AssessmentId", Assessment.AssessmentId);
                parrSP[4] = new SqlParameter("@IsChecked", IsChecked);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspAssessmentChecklist", parrSP);
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
            return this.Checklist.Default;
        }
        public string Attributes(string argName)
        {
            switch (argName)
            {
                case "AssessmentChecklistId":
                    return this.AssessmentChecklistId.ToString();
                    break;
                case "ChecklistId":
                    return this.Checklist.ChecklistId.ToString();
                    break;
                case "AssessmentId":
                    return this.Assessment.AssessmentId.ToString();
                    break;
                case "IsChecked":
                    return this.IsChecked.ToString();
                    break;

                default:
                    return null;
                    break;
            }
        }

    }
}
