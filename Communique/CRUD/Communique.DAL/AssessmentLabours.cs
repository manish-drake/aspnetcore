using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class AssessmentLabours : Dictionary<System.String, AssessmentLabour>
    {
        private bool mIsDirty;
        private int mTempKey = 0;
        public bool IsDirty
        {
            get { return mIsDirty; }
            set { mIsDirty = value; }
        }

        public void Add(AssessmentLabour fs)
        {
            string key = "temp" + (++mTempKey);
            this.Add(key, fs);
        }
        public void Add(AssessmentLabour fs, string key)
        {
            this.Add(key, fs);
        }


    }
}
