using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class AssessmentChecklists : Dictionary<System.String, AssessmentChecklist>
    {
        private bool mIsDirty;
        private int mTempKey = 0;
        public bool IsDirty
        {
            get { return mIsDirty; }
            set { mIsDirty = value; }
        }
        public void Add(AssessmentChecklist fs)
        {
            string key = "temp" + (++mTempKey);
            this.Add(key, fs);
        }
        public void Add(AssessmentChecklist fs, string key)
        {
            this.Add(key, fs);
        }
        public void Save(Assessment argAssessment)
        {
            foreach (AssessmentChecklist var in this.Values)
            {
                if (var.AssessmentChecklistId > 0)
                {
                    var.Assessment = argAssessment;
                    var.Transaction(enumDBTransaction.spEdit);
                }
                else
                {
                    var.Assessment = argAssessment;
                    var.Transaction(enumDBTransaction.spAdd);
                }
            }
        }
    }
}
