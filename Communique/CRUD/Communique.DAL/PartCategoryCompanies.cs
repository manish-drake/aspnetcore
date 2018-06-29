using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class PartCategoryCompanies : Dictionary<System.String, PartCategoryCompany>
    {
        private bool mIsDirty;
        private int mTempKey = 0;
        public bool IsDirty
        {
            get { return mIsDirty; }
            set { mIsDirty = value; }
        }
        public void Add(PartCategoryCompany fs)
        {
            string key = "temp" + (++mTempKey);
            this.Add(key, fs);
        }
        public void Add(PartCategoryCompany fs, string key)
        {
            this.Add(key, fs);
        }
        public void Save(Part argPart)
        {
            foreach (PartCategoryCompany var in this.Values)
            {
                if (var.PartCategoryCompanyId > 0)
                {
                    var.Part = argPart;
                    var.Transaction(enumDBTransaction.spEdit);
                }
                else
                {
                    var.Part = argPart;
                    var.Transaction(enumDBTransaction.spAdd);
                }
            }
        }
    }
}
