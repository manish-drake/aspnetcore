using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class PartEstimate
    {
        public PartEstimate(Estimate estimate, Workshop workshop, AssessmentParts assessmentParts)
        {
            this.Estimate = estimate;
            this.Workshop = workshop;
            this.AssessmentParts = assessmentParts;
        }
        public PartEstimate(AssessmentParts assessmentParts)
        {
            this.AssessmentParts = assessmentParts;
        }
        private Estimate mEstimate;

        public Estimate Estimate
        {
            get
            {
                if (mEstimate == null)
                {
                    mEstimate = new Estimate("");
                }
                return mEstimate;
            }
            set { mEstimate = value; }
        }
        private Workshop mWorkshop;

        public Workshop Workshop
        {
            get
            {
                if (mWorkshop == null)
                {
                    mWorkshop = new Workshop("");
                }
                return mWorkshop;
            }
            set { mWorkshop = value; }
        }

        private AssessmentParts mAssessmentParts;

        public AssessmentParts AssessmentParts
        {
            get
            {
                if (mAssessmentParts == null)
                {
                    mAssessmentParts = new AssessmentParts();
                }
                return mAssessmentParts;
            }
            set { mAssessmentParts = value; }
        }

        public override string ToString()
        {
            return this.Estimate.Default;
        }
    }
}
