using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class BillDetails : System.Collections.SortedList
    {
        private bool mIsDirty;
        private int mTempKey = 0;
        public bool IsDirty
        {
            get { return mIsDirty; }
            set { mIsDirty = value; }
        }

        // Return the item at the specified index.
        public BillDetail this[int Index]
        {
            get
            {
                return (BillDetail)this[Index];
            }
        }
        public BillDetail this[string key]
        {
            get
            {
                return (BillDetail)this[key];

            }
        }
        public void Add(BillDetail fs)
        {
            string key = "temp" + (++mTempKey);
            base.Add(key, fs);
        }
        public void Add(BillDetail fs, string key)
        {
            base.Add(key, fs);
        }
    }
}
