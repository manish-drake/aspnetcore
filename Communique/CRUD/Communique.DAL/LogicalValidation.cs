using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class LogicalValidation
    {
        private string mKey;

        public string Key
        {
            get { return mKey; }
            set { mKey = value; }
        }

        private string mValidationText;

        public string ValidationText
        {
            get { return mValidationText; }
            set { mValidationText = value; }
        }
        private bool mIsValid;

        public bool IsValid
        {
            get { return mIsValid; }
            set { mIsValid = value; }
        }

        public LogicalValidation(string argValidationText, string argKey)
        {
            mValidationText = argValidationText;
            mKey = argKey;
        }
    }
}
