using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class OfficeGST : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mOfficeGSTID;
        private System.String mStateCode;
        private System.Single mSGSTRate;
        private System.Single mCGSTRate;
        private System.Single mUGSTRate;
        private System.Single mIGSTRate;

        public OfficeGST(String argConnection)
        {
            mConnection = argConnection;
        }

        public OfficeGST(String argConnection, Int32 argOfficeGSTID)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM OfficeGST WHERE OfficeGSTID=" + argOfficeGSTID;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mOfficeGSTID = (System.Int32)dr["OfficeGSTID"];
                mSGSTRate = (System.Single)dr["SGSTRate"];
                mCGSTRate = (System.Single)dr["CGSTRate"];
                mUGSTRate = (System.Single)dr["UGSTRate"];
                mIGSTRate = (System.Single)dr["IGSTRate"];
                mStateCode = (System.String)dr["StateCode"];
            }
            dr.Close();
        }

        public string Transaction(enumDBTransaction argTransactionType)
        {
            try
            {
                enumDBTransaction pTransactionType = argTransactionType;
                if (pTransactionType == enumDBTransaction.spAdd)
                {
                    mOfficeGSTID = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[7];
                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@StateCode", StateCode);
                parrSP[2] = new SqlParameter("@SGSTRate", SGSTRate);
                parrSP[3] = new SqlParameter("@CGSTRate", CGSTRate);
                parrSP[4] = new SqlParameter("@UGSTRate", UGSTRate);
                parrSP[5] = new SqlParameter("@IGSTRate", IGSTRate);
                parrSP[6] = new SqlParameter("@OfficeGSTID", OfficeGSTID);

                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspOfficeGST", parrSP);
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
            }
            return "Transaction failed";

        }

        public string Transaction(enumDBTransaction argTransactionType, System.Data.SqlClient.SqlTransaction argTransaction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        SqlParameter[] GetParameters(enumDBTransaction argTransactionType)
        {
            return GetParameters(argTransactionType.GetHashCode(), new DateTime(2007, 04, 1),
                DateTime.Today, null, null, null, null);
        }
        SqlParameter[] GetParameters(System.Int32 argTransactionType, DateTime DtFrom,
            DateTime DtTill, Workshop wrk, Office off,
            AssessmentType argAssessmentType, Model argModel)
        {
            SqlParameter[] parrSP = new SqlParameter[26];
            parrSP[0] = new SqlParameter("@Action", argTransactionType);
            return parrSP;
        }


        public System.Data.DataTable Items()
        {
            return null;
        }

        public OSN.Generic.Identity Identity
        {
            get
            {
                if (mIdentity == null)
                {
                    mIdentity = new OSN.Generic.Identity(Connection, "OfficeGST", "OfficeGSTID");
                }
                return mIdentity;
            }
        }

        public System.Single SGSTRate
        {
            get
            {
                return mSGSTRate;
            }
            set
            {
                mSGSTRate = value;
            }
        }

        public System.Single CGSTRate
        {
            get
            {
                return mCGSTRate;
            }
            set
            {
                mCGSTRate = value;
            }
        }

        public System.Single IGSTRate
        {
            get
            {
                return mIGSTRate;
            }
            set
            {
                mIGSTRate = value;
            }
        }

        public System.Single UGSTRate
        {
            get
            {
                return mUGSTRate;
            }
            set
            {
                mUGSTRate = value;
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

        public System.String StateCode
        {
            get
            {
                return mStateCode;
            }
            set
            {
                mStateCode = value;
            }
        }

        public string Connection
        {
            get { return mConnection; }
        }
    }
}
