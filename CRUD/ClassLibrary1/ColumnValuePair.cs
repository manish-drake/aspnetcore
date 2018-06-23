using System;
using System.Collections.Generic;
using System.Text;

namespace OSN
{
    public class ColumnValuePair<TKey>
    {
        private string mstrColumn;
        private TKey mValue;
        public TKey Value
        {
            get { return mValue; }
        } 

	
        public string Column
        {
            get { return mstrColumn; }
        }

	
        public ColumnValuePair(string Column, TKey Value)
        {
            mstrColumn = Column;
            mValue = Value;
        }
    }
}
