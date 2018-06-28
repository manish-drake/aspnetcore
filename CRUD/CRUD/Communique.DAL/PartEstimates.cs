using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class PartEstimates : System.Collections.Generic.Dictionary<System.String,PartEstimate>
    {
       
        private int mTempKey = 0;
        private bool mIsDirty;

        public bool IsDirty
        {
            get
            {
                return mIsDirty;
            }
            set
            {
                mIsDirty = value;
            }
        }
        public void Add(PartEstimate fs)
        {
            string key = fs.Estimate.Default;
            this.Add(key, fs);
        }
        public void Add(PartEstimate fs, System.String key)
        {
            this.Add(key, fs);
        }


        internal void Delete(System.String connection, System.Int32 assessmentId)
        {
            System.String strIds = "";
            foreach (PartEstimate item in this.Values)
            {
                foreach (AssessmentPart aPart in item.AssessmentParts.Values)
                {
                    if (aPart.AssessmentPartId > 0)
                    {
                        strIds += aPart.AssessmentPartId.ToString() + ",";
                    }
                }
            }
            if (strIds.Length > 0)
            {
                strIds = strIds.Substring(0, strIds.Length - 1);
                System.String sql = System.String.Format("DELETE FROM AssessmentPart WHERE AssessmentId={0} AND AssessmentPartId NOT IN({1})", assessmentId, strIds);
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(connection, System.Data.CommandType.Text, sql);
            }
            else
            {
                System.String sql = System.String.Format("DELETE FROM AssessmentPart WHERE AssessmentId={0} ", assessmentId);
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(connection, System.Data.CommandType.Text, sql);
            }
        }

        private System.Double mNetassessed;
        public System.Double Netassessed
        {
            get
            {
                mNetassessed = 0;
                foreach (PartEstimate estimate in this.Values)
                {
                    foreach (AssessmentPart var in estimate.AssessmentParts.Values)
                    {
                        mNetassessed += var.NetAssessed;
                    }
                }
                return mNetassessed;
            }
        }
        private System.Double mNetClaimed;
        public System.Double NetClaimed
        {
            get
            {
                mNetClaimed = 0;
                foreach (PartEstimate estimate in this.Values)
                {
                    foreach (AssessmentPart var in estimate.AssessmentParts.Values)
                    {
                        System.Double pVAT = (float)Math.Round(var.Claimed * var.VATRate / 100, 2);
                        System.Double pDprc = (float)Math.Round(((var.Claimed + pVAT) * var.DepreciationRate / 100), 2);
                        mNetClaimed += var.Claimed + pVAT - pDprc;
                    }
                }
                return mNetClaimed;
            }
        }
    }
}
