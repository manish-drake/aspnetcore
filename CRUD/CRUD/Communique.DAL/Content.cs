using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class ContentManager
    {
        private System.String mContext;

        public System.String Context
        {
            get { return mContext; }
            set { mContext = value; }
        }

        public ContentManager(string argConnection)
        {
            this.Connection = argConnection;
        }
        private string mConnection;

        public string Connection
        {
            get { return mConnection; }
            set { mConnection = value; }
        }

        private string mContent;

        public string Content
        {
            get { return mContent; }
            set { mContent = value; }
        }
        private string[] mPhrases;

        public string[] Phrases
        {
            get
            {
                if (mContent.Replace("\\%", "^^Percentage^^;").IndexOf("%") == -1)
                {
                    return null;
                }
                mPhrases = mContent.Replace("\\%", "^^Percentage^^;").Split(new String[] { "%" }, StringSplitOptions.None);
                return mPhrases;
            }
        }

        public void Add()
        {
            for (int i = 1; i < this.Phrases.Length; i = i + 2)
            {
                try
                {
                    if (this.Phrases[i].Replace("^^Percentage^^;", "%").Length > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("INSERT INTO ContentManager ([Content], Context) ");
                        sb.Append("VALUES('" + this.Phrases[i].Replace("^^Percentage^^;", "%") + "', '" + this.Context + "')");
                        SqlHelper.ExecuteNonQuery(this.Connection, CommandType.Text, sb.ToString());
                    }
                }
                catch (Exception ex)
                {
                    //OSN.Generic.EventLogHelper appLog = new OSN.Generic.EventLogHelper();
                    //appLog.Source = "Communique";
                    //StringBuilder sb = new StringBuilder();
                    //sb.Append("ContentManager: public void Add()");
                    //sb.Append("Exception=" + ex.Message);
                    //appLog.WriteEntry(sb.ToString());
                }
            }
        }
        public void Delete(System.Int32 argContentId)
        {
            try
            {
                System.String sql = "DELETE FROM ContentManager WHERE ContentId = " + argContentId;
                SqlHelper.ExecuteNonQuery(this.Connection, CommandType.Text, sql);
            }
            catch (Exception ex)
            {
                //OSN.Generic.EventLogHelper appLog = new OSN.Generic.EventLogHelper();
                //appLog.Source = "Communique";
                //StringBuilder sb = new StringBuilder();
                //sb.Append("ContentManager: public void Delete(System.Int32 argContentId)");
                //sb.Append("Exception=" + ex.Message);
                //appLog.WriteEntry(sb.ToString());
            }
        }
        public DataTable Items()
        {
            string strSql = "SELECT * FROM [ContentManager] ORDER BY [Content]";
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(this.Connection, CommandType.Text, strSql);
            return ds.Tables[0];
        }
        public DataTable Items(System.String argContext)
        {
            string strSql = "SELECT * FROM [ContentManager] WHERE Context = '" + argContext + "' ORDER BY [Content]";
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(this.Connection, CommandType.Text, strSql);
            return ds.Tables[0];
        }
    }
}
