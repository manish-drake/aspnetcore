using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class AssessmentPart : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mAssessmentPartId;
        private System.String mSrNo;
        private Part mPart;
        private System.String mDescription;
        private System.Single mClaimed;
        private System.Single mAssessed;
        private System.Single mVATRate;
        private float mVAT;
        private System.Single mDepreciationRate;
        private System.Single mDepreciation;
        private System.Single mNetAssessed;
        private Assessment mAssessment;
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


        private bool mIsDirty;

        public bool IsDirty
        {
            get { return mIsDirty; }
        }
        public AssessmentPart(String argConnection)
        {
            mConnection = argConnection;
            TransactionType = enumDBTransaction.spNull;
        }
        public AssessmentPart(String argConnection, Int32 argAssessmentPartId)
        {
            TransactionType = enumDBTransaction.spNull;
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM AssessmentPart WHERE AssessmentPartId=" + argAssessmentPartId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mAssessmentPartId = (System.Int32)dr["AssessmentPartId"];
                mSrNo = (System.String)dr["SrNo"];
                mPart = new Part(Connection, (int)dr["PartId"]);
                mDescription = (System.String)dr["Description"];
                mClaimed = (System.Single)dr["Claimed"];
                mAssessed = (System.Single)dr["Assessed"];
                mVATRate = (System.Single)dr["VATRate"];
                mVAT = float.Parse(dr["VAT"].ToString());
                mDepreciationRate = (System.Single)dr["DepreciationRate"];
                mDepreciation = (System.Single)dr["Depreciation"];
                mNetAssessed = (System.Single)dr["NetAssessed"];
                mWorkshop = new Workshop(Connection, dr.GetInt32(dr.GetOrdinal("WorkshopId")));
                mEstimate = new Estimate(Connection, dr.GetInt32(dr.GetOrdinal("EstimateId")));
            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "AssessmentPart.AssessmentPart()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 AssessmentPartId
        {
            get
            {
                return mAssessmentPartId;
            }
            set
            {
                mIsDirty = true;
                mAssessmentPartId = value;
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
                mIsDirty = true;
                mSrNo = value;
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
                mIsDirty = true;
                mPart = value;
            }
        }
        public System.String Description
        {
            get
            {
                return mDescription;
            }
            set
            {
                mIsDirty = true;
                mDescription = value;
            }
        }
        public System.Single Claimed
        {
            get
            {
                return mClaimed;
            }
            set
            {
                mIsDirty = true;
                mClaimed = value;
            }
        }
        public System.Single Assessed
        {
            get
            {
                return mAssessed;
            }
            set
            {
                mIsDirty = true;
                mAssessed = value;
            }
        }
        public System.Single VATRate
        {
            get
            {
                return mVATRate;
            }
            set
            {
                mIsDirty = true;
                mVATRate = value;
            }
        }
        public float VAT
        {
            get
            {
                return mVAT;
            }
            set
            {
                mIsDirty = true;
                mVAT = value;
            }
        }
        public System.Single DepreciationRate
        {
            get
            {
                return mDepreciationRate;
            }
            set
            {
                mIsDirty = true;
                mDepreciationRate = value;
            }
        }
        public System.Single Depreciation
        {
            get
            {
                return mDepreciation;
            }
            set
            {
                mIsDirty = true;
                mDepreciation = value;
            }
        }
        public System.Single NetAssessed
        {
            get
            {
                return mNetAssessed;
            }
            set
            {
                mIsDirty = true;
                mNetAssessed = value;
            }
        }
        public Assessment Assessment
        {
            get
            {
                if (mAssessment == null)
                {
                    System.String sql = "SELECT AssessmentId FROM AssessmentPart WHERE AssessmentPartId = " + this.AssessmentPartId;
                    System.Data.SqlClient.SqlDataReader sdr =
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(this.Connection, CommandType.Text, sql);
                    if (sdr.Read())
                    {
                        mAssessment = new Assessment(Connection, sdr.GetInt32(sdr.GetOrdinal("AssessmentId")));
                    }
                    else
                    {
                        mAssessment = new Assessment(Connection);
                    }
                    sdr.Close();
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
                    mIdentity = new OSN.Generic.Identity(Connection, "AssessmentPart", "AssessmentPartId");
                }
                return mIdentity;
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
                    mAssessmentPartId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[15];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@AssessmentPartId", AssessmentPartId);
                parrSP[2] = new SqlParameter("@SrNo", SrNo);
                parrSP[3] = new SqlParameter("@PartId", Part.PartId);
                parrSP[4] = new SqlParameter("@Description", Description);
                parrSP[5] = new SqlParameter("@Claimed", Claimed);
                parrSP[6] = new SqlParameter("@Assessed", Assessed);
                parrSP[7] = new SqlParameter("@VATRate", VATRate);
                parrSP[8] = new SqlParameter("@VAT", VAT);
                parrSP[9] = new SqlParameter("@DepreciationRate", DepreciationRate);
                parrSP[10] = new SqlParameter("@Depreciation", Depreciation);
                parrSP[11] = new SqlParameter("@NetAssessed", NetAssessed);
                parrSP[12] = new SqlParameter("@AssessmentId", Assessment.AssessmentId);
                parrSP[13] = new SqlParameter("@WorkshopId", this.Workshop.WorkshopId);
                parrSP[14] = new SqlParameter("@EstimateId", this.Estimate.EstimateId);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspAssessmentPart", parrSP);
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
                SqlParameter[] parrSP = new SqlParameter[15];
                enumDBTransaction pTransactionType = enumDBTransaction.spSelect;
                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@AssessmentPartId", AssessmentPartId);
                parrSP[2] = new SqlParameter("@SrNo", SrNo);
                parrSP[3] = new SqlParameter("@PartId", Part.PartId);
                parrSP[4] = new SqlParameter("@Description", Description);
                parrSP[5] = new SqlParameter("@Claimed", Claimed);
                parrSP[6] = new SqlParameter("@Assessed", Assessed);
                parrSP[7] = new SqlParameter("@VATRate", VATRate);
                parrSP[8] = new SqlParameter("@VAT", VAT);
                parrSP[9] = new SqlParameter("@DepreciationRate", DepreciationRate);
                parrSP[10] = new SqlParameter("@Depreciation", Depreciation);
                parrSP[11] = new SqlParameter("@NetAssessed", NetAssessed);
                parrSP[12] = new SqlParameter("@AssessmentId", Assessment.AssessmentId);
                parrSP[13] = new SqlParameter("@WorkshopId", this.Workshop.WorkshopId);
                parrSP[14] = new SqlParameter("@EstimateId", this.Estimate.EstimateId);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspAssessmentPart", parrSP);
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
            return this.AssessmentPartId.ToString();
        }

    }
}
