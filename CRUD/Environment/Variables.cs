using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Data;

namespace Environment
{
    public class Variables : System.Collections.Generic.Dictionary<string,Variable>
    {
        private string mConnection;
        public string Connection
        {
            get { return mConnection; }
        }

        public Variables(string argConnection)
        {
            mConnection = argConnection;
            System.String sql = "	SELECT DISTINCT * FROM (	 ";
            sql += "	SELECT 	 ";
            sql += "	[Property],	 ";
            sql += "	(SELECT [Value] FROM Environment env1 	 ";
            sql += "	WHERE env1.Property = Environment.[Property] AND 	 ";
            sql += "	env1.DtFrom = 	 ";
            sql += "	(SELECT MAX(env.DtFrom) FROM Environment env 	 ";
            sql += "	WHERE env.[Property] = env1.Property	 ";
            sql += "	AND env.DtFrom <= GetDate())	 ";
            sql += "	) AS [Value],	 ";
            sql += "	(SELECT MAX(env.DtFrom) FROM Environment env 	 ";
            sql += "	WHERE env.[Property] = Environment.[Property]	 ";
            sql += "	AND env.DtFrom <= GetDate()) AS [DtFrom]	 ";
            sql += "	FROM Environment) [Environment] ORDER BY Property ";

            System.Data.SqlClient.SqlDataReader dr =
                SqlHelper.ExecuteReader(Connection, CommandType.Text, sql);
            while (dr.Read())
            {
                this.Add(dr["Property"].ToString(), new Variable());
                this[dr["Property"].ToString()].Property = dr["Property"].ToString();
                this[dr["Property"].ToString()].Value = dr["Value"].ToString();
                try
                {
                    this[dr["Property"].ToString()].DtFrom = (System.DateTime)dr["DtFrom"];
                }
                catch (Exception )
                {
                    this[dr["Property"].ToString()].DtFrom = DateTime.Today;
                }
                
            }
            dr.Close();
        }
        public Variables(string argConnection, System.String property)
        {
            mConnection = argConnection;
            string sql = "SELECT * FROM Environment WHERE [Property] = '" + property + "' ORDER BY DtFrom DESC";
            System.Data.SqlClient.SqlDataReader dr =
                SqlHelper.ExecuteReader(Connection, CommandType.Text, sql);
            while (dr.Read())
            {
                this.Add(dr["Property"].ToString(), new Variable());
                this[dr["Property"].ToString()].Property = dr["Property"].ToString();
                this[dr["Property"].ToString()].Value = dr["Value"].ToString();
                this[dr["Property"].ToString()].DtFrom = (System.DateTime)dr["DtFrom"];
            }
            dr.Close();
        }
        public new Variable this[string argKey]
        {
            get
            {
                return base[argKey];
            }
        }
        public System.String Update()
        {
            try
            {
                foreach (Environment.Variable  var in this.Values)
                {
                    System.String sql = "UPDATE Environment SET [Value]='"
                        + var.Value + "', DtFrom=" +
                        System.String.Format("'{0:yyyyMMdd}'", var.DtFrom) + " WHERE [Property]='" + var.Property + "' ";
                    System.Int32 rec= Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(
                        mConnection, CommandType.Text, sql);
                    if (rec<=0)
                    {
                        System.String sql1 = "INSERT INTO Environment ([Property], [Value], DtFrom) " +
                        System.String.Format("'{0}', '{1}', '{2:yyyyMMdd}'", var.Property, var.Value, var.DtFrom);
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(
                            mConnection, CommandType.Text, sql1);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                //OSN.Generic.EventLogHelper appLog = new OSN.Generic.EventLogHelper();
                //appLog.Source = "Communique";
                //StringBuilder sb = new StringBuilder();
                //sb.Append("Variables.public void Update()");
                //sb.Append("Exception=" + ex.Message);
                //appLog.WriteEntry(sb.ToString());
                return "Transaction failed";
            }
        }
    
    }
}
