using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class Checklist : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mChecklistId;
        private System.String mDefault;
        private System.Boolean mIsWorkshopSpecific;
        private System.Boolean mIsInsuredSpecific;
        private System.Boolean mIsInsurerSpecific;

        public Checklist(String argConnection)
        {
            mConnection = argConnection;
        }
        public Checklist(String argConnection, Int32 argChecklistId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM Checklist WHERE ChecklistId=" + argChecklistId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mChecklistId = (System.Int32)dr["ChecklistId"];
                mDefault = (System.String)dr["Checklist"];
                mIsWorkshopSpecific = (System.Boolean)dr["IsWorkshopSpecific"];
                mIsInsuredSpecific = (System.Boolean)dr["IsInsuredSpecific"];
                mIsInsurerSpecific = (System.Boolean)dr["IsInsurerSpecific"];

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "Checklist.Checklist()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 ChecklistId
        {
            get
            {
                return mChecklistId;
            }
            set
            {
                mChecklistId = value;
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
        public System.Boolean IsWorkshopSpecific
        {
            get
            {
                return mIsWorkshopSpecific;
            }
            set
            {
                mIsWorkshopSpecific = value;
            }
        }
        public System.Boolean IsInsuredSpecific
        {
            get
            {
                return mIsInsuredSpecific;
            }
            set
            {
                mIsInsuredSpecific = value;
            }
        }
        public System.Boolean IsInsurerSpecific
        {
            get
            {
                return mIsInsurerSpecific;
            }
            set
            {
                mIsInsurerSpecific = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "Checklist", "ChecklistId");
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
                    mChecklistId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[6];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@ChecklistId", ChecklistId);
                parrSP[2] = new SqlParameter("@Checklist", Default);
                parrSP[3] = new SqlParameter("@IsWorkshopSpecific", IsWorkshopSpecific);
                parrSP[4] = new SqlParameter("@IsInsuredSpecific", IsInsuredSpecific);
                parrSP[5] = new SqlParameter("@IsInsurerSpecific", IsInsurerSpecific);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspChecklist", parrSP);
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
                parrSP[1] = new SqlParameter("@ChecklistId", ChecklistId);
                parrSP[2] = new SqlParameter("@Checklist", Default);
                parrSP[3] = new SqlParameter("@IsWorkshopSpecific", IsWorkshopSpecific);
                parrSP[4] = new SqlParameter("@IsInsuredSpecific", IsInsuredSpecific);
                parrSP[5] = new SqlParameter("@IsInsurerSpecific", IsInsurerSpecific);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspChecklist", parrSP);
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
                case "ChecklistId":
                    return this.ChecklistId.ToString();
                    break;
                case "Checklist":
                    return this.Default.ToString();
                    break;
                case "IsWorkshopSpecific":
                    return this.IsWorkshopSpecific.ToString();
                    break;
                case "IsInsuredSpecific":
                    return this.IsInsuredSpecific.ToString();
                    break;
                case "IsInsurerSpecific":
                    return this.IsInsurerSpecific.ToString();
                    break;

                default:
                    return null;
                    break;
            }
        }

    }
}
