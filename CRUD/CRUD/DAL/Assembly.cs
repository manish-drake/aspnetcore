using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class Assembly : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mAssemblyId;
        private System.String mDefault;
        private System.String mRemarks;
        private Assessment mAssessment;

        private enumDBTransaction mTransactionType;

        public enumDBTransaction TransactionType
        {
            get { return mTransactionType; }
            set { mTransactionType = value; }
        }
        public Assembly()
        {
            TransactionType = enumDBTransaction.spNull;
        }
        public Assembly(String argConnection)
        {
            mConnection = argConnection;
            TransactionType = enumDBTransaction.spNull;
        }
        public Assembly(String argConnection, Int32 argAssemblyId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM Assembly WHERE AssemblyId=" + argAssemblyId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mAssemblyId = (System.Int32)dr["AssemblyId"];
                mDefault = (System.String)dr["Assembly"];
                mRemarks = (System.String)dr["Remarks"];
                mAssessment = new Assessment(Connection, (int)dr["AssessmentId"]);
                TransactionType = enumDBTransaction.spNull;
            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "Assembly.Assembly()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 AssemblyId
        {
            get
            {
                return mAssemblyId;
            }
            set
            {
                mAssemblyId = value;
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
        public System.String Remarks
        {
            get
            {
                return mRemarks;
            }
            set
            {
                mRemarks = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "Assembly", "AssemblyId");
                }
                return mIdentity;
            }
        }

        public string Transaction(enumDBTransaction argTransactionType)
        {
            try
            {
                enumDBTransaction pTransactionType = enumDBTransaction.spNull;
                if (TransactionType != enumDBTransaction.spNull)
                {
                    pTransactionType = TransactionType;
                    if (pTransactionType == enumDBTransaction.spAdd)
                    {
                        mAssemblyId = this.Identity.New();
                    }
                }
                else
                {
                    pTransactionType = argTransactionType;
                    if (pTransactionType == enumDBTransaction.spAdd)
                    {
                        mAssemblyId = this.Identity.New();
                    }
                    else if ((pTransactionType == enumDBTransaction.spEdit) && (mAssemblyId <= 0))
                    {
                        pTransactionType = enumDBTransaction.spAdd;
                        mAssemblyId = this.Identity.New();
                    }
                }

                SqlParameter[] parrSP = new SqlParameter[5];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@AssemblyId", AssemblyId);
                parrSP[2] = new SqlParameter("@Assembly", Default);
                parrSP[3] = new SqlParameter("@Remarks", Remarks);
                parrSP[4] = new SqlParameter("@AssessmentId", Assessment.AssessmentId);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspAssembly", parrSP);
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
                parrSP[1] = new SqlParameter("@AssemblyId", AssemblyId);
                parrSP[2] = new SqlParameter("@Assembly", Default);
                parrSP[3] = new SqlParameter("@Remarks", Remarks);
                parrSP[4] = new SqlParameter("@AssessmentId", Assessment.AssessmentId);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspAssembly", parrSP);
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
            return this.AssemblyId.ToString();
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "AssemblyId":
        //            return this.AssemblyId.ToString();
        //            break;
        //        case "Assembly":
        //            return this.Default.ToString();
        //            break;
        //        case "Remarks":
        //            return this.Remarks.ToString();
        //            break;
        //        case "AssessmentId":
        //            return this.Assessment.AssessmentId.ToString();
        //            break;

        //        default:
        //            return null;
        //            break;
        //    }
        //}

    }
}
