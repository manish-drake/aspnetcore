 using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;

namespace OSN.Generic
{
    public class Identity
    {
        private string mstrTable;
        private string mstrColumn;
        private string mstrConnection;

        private int mUniqueId;
        public int UniqueId
        {
            get { return mUniqueId; }
        }

        public string Connection
        {
            get { return mstrConnection; }
        }
        public Identity(string Connection, string Table, string Column)
        {
            mstrConnection = Connection;
            mstrTable = Table;
            mstrColumn = Column;
        }
        public Identity(string Connection, string Table, string Column, int UniqueId)
        {
            mstrConnection = Connection;
            mstrTable = Table;
            mstrColumn = Column;
            mUniqueId = UniqueId;
        }
        public string Column
        {
            get { return mstrColumn; }
            set { mstrColumn = value; }
        }
	

        public string Table
        {
            get { return mstrTable; }
            set { mstrTable = value; }
        }


        public Int32 New()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ISNULL(MAX(" + this.Column + "),0) + 1 ");
            sql.Append("FROM " + this.Table);
            Int32 id = (Int32)SqlHelper.ExecuteScalar(this.Connection,
                System.Data.CommandType.Text, sql.ToString());
            return id;

        }

        public Int32 GetIdentity(params Object[] argKey)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT " + this.Column + " FROM " + this.Table + " ");
            sql.Append("WHERE ");

            foreach (Object key in argKey)
            {
                switch (key.GetType().ToString())
                {
                    case "OSN.ColumnValuePair`1[System.String]":
                        {
                            ColumnValuePair<string> k = (ColumnValuePair<string>)key;
                            sql.Append(k.Column + " = ");
                            sql.Append("'" + k.Value + "'");
                            break;
                        }
                    case "OSN.ColumnValuePair`1[System.Int32]":
                        {
                            ColumnValuePair<Int32> k = (ColumnValuePair<Int32>)key;
                            sql.Append(k.Column + " = ");
                            sql.Append(k.Value);
                            break;
                        }
                    case "OSN.ColumnValuePair`1[System.Float]":
                        {
                            ColumnValuePair<float> k = (ColumnValuePair<float>)key;
                            sql.Append(k.Column + " = ");
                            sql.Append(k.Value);
                            break;
                        }
                    case "OSN.ColumnValuePair`1[System.Double]":
                        {
                            ColumnValuePair<double> k = (ColumnValuePair<double>)key;
                            sql.Append(k.Column + " = ");
                            sql.Append(k.Value);
                            break;
                        }
                    case "OSN.ColumnValuePair`1[System.DateTime]":
                        {
                            ColumnValuePair<DateTime> k = (ColumnValuePair<DateTime>)key;
                            sql.Append(k.Column + " = ");
                            sql.Append("'" + k.Value.Year.ToString() +
                                k.Value.Month.ToString() + k.Value.Day.ToString() + "'");
                            break;
                        }
                }
                sql.Append(" AND ");
            }
            if (argKey.Length > 0)
            {
                sql.Remove(sql.Length - 5, 5);
            }
            Int32 id = (Int32)SqlHelper.ExecuteScalar(this.Connection, System.Data.CommandType.Text, sql.ToString());
            return id;
        }	


    }
}
