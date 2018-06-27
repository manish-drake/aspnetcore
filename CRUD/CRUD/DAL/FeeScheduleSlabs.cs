using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class FeeScheduleSlabs : System.Collections.SortedList
    {
        private bool mIsDirty;
        private int mTempKey = 0;
        public bool IsDirty
        {
            get { return mIsDirty; }
            set { mIsDirty = value; }
        }

        // Return the item at the specified index.
        public FeeScheduleSlab this[int Index]
        {
            get
            {
                return (FeeScheduleSlab)this[Index];
            }
        }
        public FeeScheduleSlab this[string key]
        {
            get
            {
                return (FeeScheduleSlab)this[key];

            }
        }
        public void Add(FeeScheduleSlab fs)
        {
            string key = "temp" + (++mTempKey);
            base.Add(key, fs);
        }
        public void Add(FeeScheduleSlab fs, string key)
        {
            base.Add(key, fs);
        }
    }
}
