using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class BankAccount : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mID;
        private System.String mTitle;
        private System.String mAccountName;
        private System.String mBank;
        private System.String mIFSC;
        private System.String mMICR;
        private System.Int32 mAccountType;
        private System.String mAccountNumber;
        private System.String mBillNo;

        public enum enumBankAccount
        {
            Active = 0x01,
            Cleared = 0x02,
            Closed = 0x04,
        }

        public System.String Title
        {
            get
            {
                return mTitle;
            }
            set
            {
                mTitle = value;
            }
        }

        public System.String AccountName
        {
            get
            {
                return mAccountName;
            }
            set
            {
                mAccountName = value;
            }
        }

        public System.String Bank
        {
            get
            {
                return mBank;
            }
            set
            {
                mBank = value;
            }
        }
        public System.String IFSC
        {
            get
            {
                return mIFSC;
            }
            set
            {
                mIFSC = value;
            }
        }
        public System.String MICR
        {
            get
            {
                return mMICR;
            }
            set
            {
                mMICR = value;
            }
        }

        public System.Int32 ID
        {
            get
            {
                return mID;
            }
            set
            {
                mID = value;
            }
        }
        public System.Int32 AccountType
        {
            get
            {
                return mAccountType;
            }
            set
            {
                mAccountType = value;
            }
        }
        public System.String AccountNumber
        {
            get
            {
                return mAccountNumber;
            }
            set
            {
                mAccountNumber = value;
            }
        }

        public System.String BillNo
        {
            get
            {
                return mBillNo;
            }
            set
            {
                mBillNo = value;
            }
        }


        public BankAccount(String argConnection)
        {
            mConnection = argConnection;
        }



        public BankAccount(String argConnection, Int32 argId)
        {
            try
            {
                mConnection = argConnection;
                string pstrSql = "SELECT * FROM BankAccount WHERE ID=" + argId;
                SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);

                if (dr.Read())
                {
                    mID = (System.Int32)dr["ID"];
                    mTitle = (System.String)dr["Title"];
                    mAccountName = (System.String)dr["AccountName"];
                    mBank = (System.String)dr["Bank"];
                    mIFSC = (System.String)dr["IFSC"];
                    mMICR = (System.String)dr["MICR"];
                    mAccountType = (System.Int32)dr["AccountType"];
                    mAccountNumber = (System.String)dr["AccountNumber"];
                }
                else
                {
                    Exception ex = new Exception("Identifier does not exist.");
                    ex.Source = "BankAccount.BankAccount()";
                    throw ex;
                }
                dr.Close();
            }
            catch { }
        }

        public System.Data.DataTable Items(System.Boolean pending)
        {
            try
            {
                SqlParameter[] parrSP = new SqlParameter[9];
                enumDBTransaction pTransactionType = enumDBTransaction.spSelect;
                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@ID", ID);
                parrSP[2] = new SqlParameter("@Title", Title);
                parrSP[3] = new SqlParameter("@AccountName", AccountName);
                parrSP[4] = new SqlParameter("@MICR", MICR);
                parrSP[5] = new SqlParameter("@Bank", Bank);
                parrSP[6] = new SqlParameter("@IFSC", IFSC);
                parrSP[7] = new SqlParameter("mAccountType", AccountType);
                parrSP[8] = new SqlParameter("mAccountNumber", AccountNumber);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspBankAccount", parrSP);
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
        public System.Data.DataTable Items(DateTime DtFrom, DateTime DtTill, Workshop wrk,
            Office off, AssessmentType argAssessmentType,
            Model argModel)
        {
            try
            {
                SqlParameter[] parrSP = GetParameters(4, DtFrom, DtTill, wrk,
                    off, argAssessmentType, argModel);
                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspBankAccount", parrSP);
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

        SqlParameter[] GetParameters(System.Int32 argTransactionType, DateTime DtFrom,
          DateTime DtTill, Workshop wrk, Office off,
          AssessmentType argAssessmentType,Model argModel)
        {
            SqlParameter[] parrSP = new SqlParameter[26];
            parrSP[0] = new SqlParameter("@Action", argTransactionType);
            return parrSP;
        }

        public System.Data.DataTable Items()
        {
            try
            {
                SqlParameter[] parrSP = new SqlParameter[9];
                enumDBTransaction pTransactionType = enumDBTransaction.spSelect;
                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@ID", ID);
                parrSP[2] = new SqlParameter("@Title", Title);
                parrSP[3] = new SqlParameter("@AccountName", AccountName);
                parrSP[4] = new SqlParameter("@MICR", MICR);
                parrSP[5] = new SqlParameter("@Bank", Bank);
                parrSP[6] = new SqlParameter("@IFSC", IFSC);
                parrSP[7] = new SqlParameter("mAccountType", AccountType);
                parrSP[8] = new SqlParameter("mAccountNumber", AccountNumber);

                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspBankAccount", parrSP);
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



        public string Transaction(enumDBTransaction argTransactionType)
        {
            try
            {
                SqlParameter[] parrSP = new SqlParameter[9];

                if (argTransactionType == enumDBTransaction.spAdd)
                {
                    mID = this.Identity.New();
                }
                parrSP[0] = new SqlParameter("@Action", argTransactionType);
                parrSP[1] = new SqlParameter("@ID", ID);
                parrSP[2] = new SqlParameter("@Title", Title);
                parrSP[3] = new SqlParameter("@AccountName", AccountName);
                parrSP[4] = new SqlParameter("@MICR", MICR);
                parrSP[5] = new SqlParameter("@Bank", Bank);
                parrSP[6] = new SqlParameter("@IFSC", IFSC);
                parrSP[7] = new SqlParameter("@AccountType", AccountType);
                parrSP[8] = new SqlParameter("@AccountNumber", AccountNumber);

                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspBankAccount", parrSP);
                return null;
                //enumDBTransaction pTransactionType = argTransactionType;
                //if (pTransactionType == enumDBTransaction.spAdd)
                //{
                //    mBillId = this.Identity.New();
                //}
                //SqlParameter[] parrSP = GetParameters(pTransactionType);

                //SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspBill", parrSP);
                //if (BillDetails.IsDirty)
                //{
                //    foreach (BillDetail fss in this.BillDetails.Values)
                //    {
                //        fss.Bill = this;
                //        fss.Transaction(pTransactionType);
                //    }
                //}
                //  return null;
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

        public OSN.Generic.Identity Identity
        {
            get
            {
                if (mIdentity == null)
                {
                    mIdentity = new OSN.Generic.Identity(Connection, "BankAccount", "ID");
                }
                return mIdentity;
            }
        }

        public string Connection
        {
            get { return mConnection; }
        }

        public string Transaction(enumDBTransaction argTransactionType, System.Data.SqlClient.SqlTransaction argTransaction)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
