using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace OSN
{
    public class ListColumns : System.Collections.SortedList
    {
        private String mKeyColumn;
        private System.String mXMLFile;
        public System.String XMLFile
        {
            get 
            {
                System.String appFolder =
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                appFolder = System.IO.Path.Combine(appFolder, "Communique");
                if (!System.IO.Directory.Exists(appFolder))
                {
                    System.IO.Directory.CreateDirectory(appFolder);
                }
                mXMLFile = System.IO.Path.Combine(appFolder, this.ViewName + ".xml");                               
                return mXMLFile; 
            }
        }

        public String KeyColumn
        {
            get { return mKeyColumn; }
        }
        public override bool ContainsValue(object value)
        {
            foreach (ListColumn var in this.Values)
            {
                if (var.ColumnField == value.ToString())
                {
                    return true;
                }
            }
            return false;
        }
        public ListColumn GetByValue(string value)
        {
            foreach (ListColumn var in this.Values)
            {
                if (var.ColumnField == value)
                {
                    return var;
                }
            }
            return new ListColumn();
        }

        public ListColumns(string argViewName)
        {
            this.mViewName = argViewName;

            if (File.Exists(XMLFile))
            {
                try
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(XMLFile, XmlReadMode.Auto);
                    DataTable dt = ds.Tables["Format"];
                    foreach (DataRow var in dt.Rows)
                    {
                        ListColumn lc = this.Add(new ListColumn(), int.Parse(var["Order"].ToString()));
                        lc.ColumnField = (System.String)var["ColumnField"];
                        lc.ColumnName = (System.String)var["ColumnName"];
                        lc.ColumnWidth = System.Double.Parse(var["ColumnWidth"].ToString());
                        lc.IsVisible = System.Boolean.Parse(var["IsVisible"].ToString());
                        lc.Order = System.Int32.Parse(var["Order"].ToString());
                        lc.IsKeyColumn = System.Boolean.Parse(var["IsKeyColumn"].ToString());
                        if (lc.IsKeyColumn)
                        {
                            mKeyColumn = lc.ColumnField;
                        }
                    }                    
                }
                catch(Exception ex)
                {
                    //System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
        }
        private string mViewName;

        public string ViewName
        {
            get { return mViewName; }
            set { mViewName = value; }
        }
        public ListColumn Add(ListColumn fs, int key)
        {
            try
            {
                base.Add(key, fs);
            }
            catch (Exception)
            {
                key = (int)base.GetKey(base.Count - 1) + 1;
                base.Add(key, fs);                
            }            
            return (ListColumn)base[key];
        }
        public void Synchronize(DataTable argDT, DataColumn argKeyColumn)
        {
            Boolean IsDirty = false;
            if (File.Exists(XMLFile))
            {
                int Counter = this.Count;
                foreach (DataColumn dc in argDT.Columns)
                {
                    if (!this.ContainsValue(dc.ColumnName))
                    {
                        Counter += 1;
                        ListColumn lc = this.Add(new ListColumn(), Counter);
                        lc.ColumnField = dc.ColumnName;
                        lc.ColumnName = dc.ColumnName;
                        lc.ColumnWidth = 100;
                        lc.IsVisible = true;
                        lc.Order = Counter;
                        lc.IsKeyColumn = (dc.ColumnName == argKeyColumn.ColumnName);
                        if (lc.IsKeyColumn)
                        {
                            mKeyColumn = dc.ColumnName;
                        }
                        IsDirty = true;
                    }
                }
                foreach (ListColumn lc in this.Values)
                {
                    if (!argDT.Columns.Contains(lc.ColumnField))
                    {
                        try
                        {
                            this.Remove(lc.ColumnField);
                        }
                        catch { }
                        
                        IsDirty = true;
                    }
                }
                if (this.Count > 0)
                {
                    if (IsDirty) this.SaveFormat();
                }
            }
            else
            {
                try
                {
                    this.Clear();
                    int Counter = 0;
                    foreach (System.Data.DataColumn dc in argDT.Columns)
	                {
                        ListColumn lc = this.Add(new ListColumn(), ++Counter);
                        lc.ColumnField = dc.ColumnName;
                        lc.ColumnName = dc.ColumnName;
                        lc.ColumnWidth = 100;
                        lc.IsKeyColumn = (dc.ColumnName == argKeyColumn.ColumnName);
                        lc.IsVisible = true;
                        lc.Order = Counter;
                        if (lc.IsKeyColumn)
                        {
                            mKeyColumn = lc.ColumnField;
                            lc.ColumnWidth = 5;
                        }
	                }
                    this.SaveFormat();
                }
                catch
                { }
            }
        }
        public void SaveFormat()
        {
            DataSet ds = new DataSet();
            DataTable dt = ds.Tables.Add("Format");
            dt.Columns.Add("ColumnField", typeof(System.String));
            dt.Columns.Add("ColumnName", typeof(System.String));
            dt.Columns.Add("ColumnWidth", typeof(System.Double));
            dt.Columns.Add("IsVisible", typeof(System.Boolean));
            dt.Columns.Add("IsKeyColumn", typeof(System.Boolean));
            dt.Columns.Add("Order", typeof(System.Int32));
            int Counter = 0;
            foreach (ListColumn lc in this.Values)
            {
                Counter++;
                DataRow dr = dt.NewRow();
                dr["ColumnField"] = lc.ColumnField;
                dr["ColumnName"] = lc.ColumnName;
                dr["ColumnWidth"] = lc.ColumnWidth;
                dr["IsVisible"] = lc.IsVisible;
                dr["Order"] = lc.Order;
                dr["IsKeyColumn"] = lc.IsKeyColumn;
                dt.Rows.Add(dr);
            }
            if (dt.Rows.Count > 0)
            {
                ds.WriteXml(XMLFile);
            }
        }
    }
}
