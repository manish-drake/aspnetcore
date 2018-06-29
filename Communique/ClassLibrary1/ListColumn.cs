using System;
using System.Collections.Generic;
using System.Text;

namespace OSN
{
    public class ListColumn
    {
        private string mColumnName;

        public string ColumnName
        {
            get { return mColumnName; }
            set { mColumnName = value; }
        }
        private string mColumnField;

        public string ColumnField
        {
            get { return mColumnField; }
            set { mColumnField = value; }
        }
        private bool mIsVisible;

        public bool IsVisible
        {
            get { return mIsVisible; }
            set { mIsVisible = value; }
        }
        private int mOrder;

        public int Order
        {
            get { return mOrder; }
            set { mOrder = value; }
        }
        private System.Double mColumnWidth;

        public System.Double ColumnWidth
        {
            get { return mColumnWidth; }
            set { mColumnWidth = value; }
        }
        private Boolean mIsKeyColumn;

        public Boolean IsKeyColumn
        {
            get { return mIsKeyColumn; }
            set { mIsKeyColumn = value; }
        }

    }
}
