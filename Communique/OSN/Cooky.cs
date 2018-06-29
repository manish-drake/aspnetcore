using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace OSN
{
    public class Cooky
    {
        private System.String[] mCookies;
        public System.String Cookies
        {
            get { return mCookies; }
        }
        private System.String mName;
        public System.String Name
        {
            get { return mName; }
        }


        public Cooky(System.String argCooky)
        {
            mName = argCooky;
            if (File.Exists(AppPath(argCooky)))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(AppPath(argCooky));
                if (ds.Tables.Contains("Cookies"))
                {
                    DataTable dt = ds.Tables["Cookies"];
                    
                    foreach (System.Data.DataRow var in dt.Rows)
                    {
                        this.Add(var["Item"].ToString());
                    }
                }
            }
        }
        public void Save()
        {
            try
            {
                DataSet ds = new DataSet(AppPath(Name));
                DataTable dt = ds.Tables["Cookies"];
                dt.Columns.Add("Item");
                foreach (System.String var in this.Cookies)
                {
                    DataRow dr = dt.NewRow();
                    dr["Item"] = var;
                    dt.Rows.Add(dr);
                }
                ds.WriteXml(AppPath(Name));
            }
            catch (Exception)
            {

            }
        }
        public void Add(string argItem)
        {
            Array.Resize<string>(ref mCookies, Cookies.Length + 1);
            if (Array.IndexOf(mCookies, argItem) >= 0)
            {
                throw new Exception("Value already exists in the cookies.");
            }
            mCookies[mCookies.Length] = argItem;
        }
        private System.String AppPath(System.String argFile)
        {
            System.String AppDataRoot = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Communique");
            if (!System.IO.Directory.Exists(AppDataRoot))
            {
                System.IO.Directory.CreateDirectory(AppDataRoot);
            }
            return System.IO.Path.Combine(AppDataRoot, argFile);
            return argFile;
        }

    }
}
