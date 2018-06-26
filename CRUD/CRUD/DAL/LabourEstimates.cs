using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class LabourEstimates : System.Collections.Generic.Dictionary<System.String, LabourEstimate>
    {
        private bool mIsDirty;
        private int mTempKey = 0;

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
        public void Add(LabourEstimate fs)
        {
            string key = fs.Estimate.Default;
            this.Add(key, fs);
        }
        public void Add(LabourEstimate fs, System.String key)
        {
            this.Add(key, fs);
        }


        internal void Delete(System.String connection, System.Int32 assessmentId)
        {
            System.String strIds = "";
            foreach (LabourEstimate item in this.Values)
            {
                foreach (AssessmentLabour aLabour in item.AssessmentLabours.Values)
                {
                    if (aLabour.AssessmentLabourId > 0)
                    {
                        strIds += aLabour.AssessmentLabourId.ToString() + ",";
                    }
                }
            }
            if (strIds.Length > 0)
            {
                strIds = strIds.Substring(0, strIds.Length - 1);
                System.String sql = System.String.Format("DELETE FROM AssessmentLabour WHERE AssessmentId={0} AND AssessmentLabourId NOT IN({1})", assessmentId, strIds);
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(connection, System.Data.CommandType.Text, sql);
            }
            else
            {
                System.String sql = System.String.Format("DELETE FROM AssessmentLabour WHERE AssessmentId={0}", assessmentId);
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(connection, System.Data.CommandType.Text, sql);
            }
        }
        private System.Double mNetClaimed;
        public System.Double NetClaimed
        {
            get
            {
                mNetClaimed = 0;
                foreach (LabourEstimate estimate in this.Values)
                {
                    foreach (AssessmentLabour var in estimate.AssessmentLabours.Values)
                    {
                        mNetClaimed += var.Claimed;
                    }
                }
                return mNetClaimed;
            }
        }

        private System.Double mNetassessed;
        public System.Double Netassessed
        {
            get
            {
                mNetassessed = 0;
                foreach (LabourEstimate estimate in this.Values)
                {
                    foreach (AssessmentLabour var in estimate.AssessmentLabours.Values)
                    {
                        mNetassessed += var.NetAssessed;
                    }
                }
                return mNetassessed;
            }
        }
    }
}
