using System;
using System.Collections.Generic;
using System.Text;

namespace Environment
{
    public class Variable
    {
        private string mProperty;

        public string Property
        {
            get { return mProperty; }
            set { mProperty = value; }
        }
        private string mValue;

        public string Value
        {
            get { return mValue; }
            set { mValue = value; }
        }
        private System.DateTime mDtFrom;

        public System.DateTime DtFrom
        {
            get { return mDtFrom; }
            set { mDtFrom = value; }
        }

    }
}
