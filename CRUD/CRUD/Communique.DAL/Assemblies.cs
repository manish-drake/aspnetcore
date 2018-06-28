using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class Assemblies : System.Collections.SortedList
    {
        private bool mIsDirty;
        private int mTempKey = 0;
        public bool IsDirty
        {
            get { return mIsDirty; }
            set { mIsDirty = value; }
        }

        // Return the item at the specified index.
        public Assembly this[int Index]
        {
            get
            {
                return (Assembly)this[Index];
            }
        }
        public Assembly this[string key]
        {
            get
            {
                return (Assembly)this[key];

            }
        }
        public void Add(Assembly fs)
        {
            string key = "temp" + (++mTempKey);
            base.Add(key, fs);
        }
        public void Add(Assembly fs, string key)
        {
            base.Add(key, fs);
        }
    }
}
