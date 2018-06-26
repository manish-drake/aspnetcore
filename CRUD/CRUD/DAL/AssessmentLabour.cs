using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class AssessmentLabour : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mAssessmentLabourId;
        private Assessment mAssessment;
        private System.String mSrNo;
        private System.String mDescClaimed;
        private System.Double mClaimed;
        private Part mPart;
        private System.String mDescAssessed;
        private System.Double mAssessedRR;
        private System.Double mAssessedDB;
        private PaintType mPaintType;
        private System.Double mAssessedPT;
        private Workshop mWorkshop;

        private Estimate mEstimate;
        private enumDBTransaction mTransactionType;

        public enumDBTransaction TransactionType
        {
            get { return mTransactionType; }
            set { mTransactionType = value; }
        }

        public Estimate Estimate
        {
            get { return mEstimate; }
            set { mEstimate = value; }
        }

        public Workshop Workshop
        {
            get { return mWorkshop; }
            set { mWorkshop = value; }
        }


        private System.Int32 mClaimId;

        public System.Int32 ClaimId
        {
            get { return mClaimId; }
            set { mClaimId = value; }
        }
        private System.Double mNetAssessed;

        public System.Double NetAssessed
        {
            get { return mNetAssessed; }
            set { mNetAssessed = value; }
        }

        public AssessmentLabour(String argConnection)
        {
            mConnection = argConnection;
        }
        public AssessmentLabour(String argConnection, Int32 argAssessmentLabourId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM AssessmentLabour WHERE AssessmentLabourId=" + argAssessmentLabourId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mAssessmentLabourId = (System.Int32)dr["AssessmentLabourId"];
                mAssessment = new Assessment(Connection, (int)dr["AssessmentId"]);
                mSrNo = (System.String)dr["SrNo"];
                mClaimId = (System.Int32)dr["ClaimId"];
                mDescClaimed = dr["DescClaimed"].GetType() == typeof(System.DBNull) ? "" : (System.String)dr["DescClaimed"];
                mClaimed = (System.Double)dr["Claimed"];
                mPart = new Part(Connection, (int)dr["PartId"]);
                mDescAssessed = dr["DescAssessed"].GetType() == typeof(System.DBNull) ? "" : (System.String)dr["DescAssessed"];
                mAssessedRR = (System.Double)dr["AssessedRR"];
                mAssessedDB = (System.Double)dr["AssessedDB"];
                mPaintType = new PaintType(Connection, (int)dr["PaintTypeId"]);
                mAssessedPT = (System.Double)dr["AssessedPT"];
                mNetAssessed = System.Double.Parse(dr["NetAssessed"].ToString());
                mWorkshop = new Workshop(Connection, dr.GetInt32(dr.GetOrdinal("WorkshopId")));
                mEstimate = new Estimate(Connection, dr.GetInt32(dr.GetOrdinal("EstimateId")));
            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "AssessmentLabour.AssessmentLabour()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 AssessmentLabourId
        {
            get
            {
                return mAssessmentLabourId;
            }
            set
            {
                mAssessmentLabourId = value;
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
        public System.String SrNo
        {
            get
            {
                return mSrNo;
            }
            set
            {
                mSrNo = value;
            }
        }
        public System.String DescClaimed
        {
            get
            {
                return mDescClaimed;
            }
            set
            {
                mDescClaimed = value;
            }
        }
        public System.Double Claimed
        {
            get
            {
                return mClaimed;
            }
            set
            {
                mClaimed = value;
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
        public System.String DescAssessed
        {
            get
            {
                return mDescAssessed;
            }
            set
            {
                mDescAssessed = value;
            }
        }
        public System.Double AssessedRR
        {
            get
            {
                return mAssessedRR;
            }
            set
            {
                mAssessedRR = value;
            }
        }
        public System.Double AssessedDB
        {
            get
            {
                return mAssessedDB;
            }
            set
            {
                mAssessedDB = value;
            }
        }
        public PaintType PaintType
        {
            get
            {
                if (mPaintType == null)
                {
                    mPaintType = new PaintType(Connection);
                }
                return mPaintType;
            }
            set
            {
                mPaintType = value;
            }
        }
        public System.Double AssessedPT
        {
            get
            {
                return mAssessedPT;
            }
            set
            {
                mAssessedPT = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "AssessmentLabour", "AssessmentLabourId");
                }
                return mIdentity;
            }
        }
        public string Transaction()
        {
            if (TransactionType == enumDBTransaction.spNull)
            {
                return "No transaction to commit!";
            }
            else
            {
                return Transaction(TransactionType);
            }
        }
        public string Transaction(enumDBTransaction argTransactionType)
        {
            enumDBTransaction pTransactionType;
            try
            {
                pTransactionType =
                    (TransactionType == enumDBTransaction.spNull) ? argTransactionType : TransactionType;
                if (pTransactionType == enumDBTransaction.spAdd)
                {
                    mAssessmentLabourId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[16];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@AssessmentLabourId", AssessmentLabourId);
                parrSP[2] = new SqlParameter("@AssessmentId", Assessment.AssessmentId);
                parrSP[3] = new SqlParameter("@SrNo", SrNo);
                parrSP[4] = new SqlParameter("@DescClaimed", DescClaimed);
                parrSP[5] = new SqlParameter("@Claimed", Claimed);
                parrSP[6] = new SqlParameter("@PartId", Part.PartId);
                parrSP[7] = new SqlParameter("@DescAssessed", DescAssessed);
                parrSP[8] = new SqlParameter("@AssessedRR", AssessedRR);
                parrSP[9] = new SqlParameter("@AssessedDB", AssessedDB);
                parrSP[10] = new SqlParameter("@PaintTypeId", PaintType.PaintTypeId);
                parrSP[11] = new SqlParameter("@AssessedPT", AssessedPT);
                parrSP[12] = new SqlParameter("@ClaimId", ClaimId);
                parrSP[13] = new SqlParameter("@NetAssessed", NetAssessed);
                parrSP[14] = new SqlParameter("@WorkshopId", Workshop.WorkshopId);
                parrSP[15] = new SqlParameter("@EstimateId", this.Estimate.EstimateId);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspAssessmentLabour", parrSP);
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
                SqlParameter[] parrSP = new SqlParameter[16];
                enumDBTransaction pTransactionType = enumDBTransaction.spSelect;
                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@AssessmentLabourId", AssessmentLabourId);
                parrSP[2] = new SqlParameter("@AssessmentId", Assessment.AssessmentId);
                parrSP[3] = new SqlParameter("@SrNo", SrNo);
                parrSP[4] = new SqlParameter("@DescClaimed", DescClaimed);
                parrSP[5] = new SqlParameter("@Claimed", Claimed);
                parrSP[6] = new SqlParameter("@PartId", Part.PartId);
                parrSP[7] = new SqlParameter("@DescAssessed", DescAssessed);
                parrSP[8] = new SqlParameter("@AssessedRR", AssessedRR);
                parrSP[9] = new SqlParameter("@AssessedDB", AssessedDB);
                parrSP[10] = new SqlParameter("@PaintTypeId", PaintType.PaintTypeId);
                parrSP[11] = new SqlParameter("@AssessedPT", AssessedPT);
                parrSP[12] = new SqlParameter("@ClaimId", ClaimId);
                parrSP[13] = new SqlParameter("@NetAssessed", NetAssessed);
                parrSP[14] = new SqlParameter("@WorkshopId", Workshop.WorkshopId);
                parrSP[15] = new SqlParameter("@EstimateId", this.Estimate.EstimateId);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspAssessmentLabour", parrSP);
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
            return this.AssessmentLabourId.ToString();
        }
        public string Attributes(string argName)
        {
            switch (argName)
            {
                case "AssessmentLabourId":
                    return this.AssessmentLabourId.ToString();
                    break;
                case "AssessmentId":
                    return this.Assessment.AssessmentId.ToString();
                    break;
                case "SrNo":
                    return this.SrNo.ToString();
                    break;
                case "DescClaimed":
                    return this.DescClaimed.ToString();
                    break;
                case "Claimed":
                    return this.Claimed.ToString();
                    break;
                case "PartId":
                    return this.Part.PartId.ToString();
                    break;
                case "DescAssessed":
                    return this.DescAssessed.ToString();
                    break;
                case "AssessedRR":
                    return this.AssessedRR.ToString();
                    break;
                case "AssessedDB":
                    return this.AssessedDB.ToString();
                    break;
                case "PaintTypeId":
                    return this.PaintType.PaintTypeId.ToString();
                    break;
                case "AssessedPT":
                    return this.AssessedPT.ToString();
                    break;

                default:
                    return null;
                    break;
            }
        }

    }
}
