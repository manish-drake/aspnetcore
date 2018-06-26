using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class AssessmentParts : Dictionary<System.String, AssessmentPart>//System.Collections.SortedList
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

        public void Add(AssessmentPart fs)
        {
            string key = "temp" + (++mTempKey);
            this.Add(key, fs);
        }
        public void Add(AssessmentPart fs, string key)
        {
            this.Add(key, fs);
        }
    }
}
