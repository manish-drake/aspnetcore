using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class LabourEstimate
    {
        public LabourEstimate(Estimate estimate, Workshop workshop, AssessmentLabours assessmentParts)
        {
            this.Estimate = estimate;
            this.Workshop = workshop;
            this.AssessmentLabours = assessmentParts;
        }
        public LabourEstimate(AssessmentLabours assessmentParts)
        {
            this.AssessmentLabours = assessmentParts;
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

        private AssessmentLabours mAssessmentLabours;

        public AssessmentLabours AssessmentLabours
        {
            get
            {
                if (mAssessmentLabours == null)
                {
                    mAssessmentLabours = new AssessmentLabours();
                }
                return mAssessmentLabours;
            }
            set { mAssessmentLabours = value; }
        }

        public override string ToString()
        {
            return this.Estimate.Default;
        }
    }
}
