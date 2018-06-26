using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class LogicalValidations : System.Collections.SortedList
    {
        private bool mIsDirty;
        private int mTempKey = 0;
        public bool IsDirty
        {
            get { return mIsDirty; }
            set { mIsDirty = value; }
        }

        // Return the item at the specified index.
        public LogicalValidation this[int Index]
        {
            get
            {
                return (LogicalValidation)this[Index];
            }
        }
        public LogicalValidation this[string key]
        {
            get
            {
                return (LogicalValidation)base[key];

            }
        }
        public void Add(LogicalValidation fs)
        {
            if (fs.Key.Length <= 0)
            {
                string key = "temp" + (++mTempKey);
                base.Add(key, fs);
            }
            else
            {
                string key = fs.Key;
                base.Add(key, fs);
            }
        }
        public void Add(LogicalValidation fs, string key)
        {
            base.Add(key, fs);
        }
    }
}
