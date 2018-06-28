using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class LabourSchdPTs : System.Collections.SortedList
    {
        private bool mIsDirty;
        private int mTempKey = 0;
        public bool IsDirty
        {
            get { return mIsDirty; }
            set { mIsDirty = value; }
        }

        // Return the item at the specified index.
        public LabourSchdPT this[int Index]
        {
            get
            {
                return (LabourSchdPT)this[Index];
            }
        }
        public LabourSchdPT this[string key]
        {
            get
            {
                return (LabourSchdPT)base[key];

            }
        }
        public void Add(LabourSchdPT fs)
        {
            string key = "temp" + (++mTempKey);
            base.Add(key, fs);
        }
        public void Add(LabourSchdPT fs, string key)
        {
            base.Add(key, fs);
        }
    }
}
