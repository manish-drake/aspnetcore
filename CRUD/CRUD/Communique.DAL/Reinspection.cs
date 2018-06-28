using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class Reinspection : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mReinspectionId;
        private System.DateTime mDtReinspection;
        private System.String mPlaceOfReinspection;
        private System.String mReinspectionDoneBy;
        private System.String mRemarks;

        public Reinspection(String argConnection)
        {
            mConnection = argConnection;
        }
        public Reinspection(String argConnection, Int32 argReinspectionId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM Reinspection WHERE ReinspectionId=" + argReinspectionId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mReinspectionId = (System.Int32)dr["ReinspectionId"];
                mDtReinspection = (System.DateTime)dr["DtReinspection"];
                mPlaceOfReinspection = (System.String)dr["PlaceOfReinspection"];
                mReinspectionDoneBy = (System.String)dr["ReinspectionDoneBy"];
                mRemarks = (System.String)dr["Remarks"];
                //mRemarks = mRemarks.Replace("%", "\\%");
            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "Reinspection.Reinspection()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 ReinspectionId
        {
            get
            {
                return mReinspectionId;
            }
            set
            {
                mReinspectionId = value;
            }
        }
        public System.DateTime DtReinspection
        {
            get
            {
                return mDtReinspection;
            }
            set
            {
                mDtReinspection = value;
            }
        }
        public System.String PlaceOfReinspection
        {
            get
            {
                return mPlaceOfReinspection;
            }
            set
            {
                mPlaceOfReinspection = value;
            }
        }
        public System.String ReinspectionDoneBy
        {
            get
            {
                return mReinspectionDoneBy;
            }
            set
            {
                mReinspectionDoneBy = value;
            }
        }
        public System.String Remarks
        {
            get
            {
                return mRemarks;
            }
            set
            {
                mRemarks = value;
            }
        }


        #region IDBObject Members

        public string Connection
        {
            get { return mConnection; }
        }

        public OSN.Generic.Identity Identity
        {
            get
            {
                if (mIdentity == null)
                {
                    mIdentity = new OSN.Generic.Identity(Connection, "Reinspection", "ReinspectionId");
                }
                return mIdentity;
            }
        }

        public string Transaction(enumDBTransaction argTransactionType)
        {
            try
            {
                enumDBTransaction pTransactionType = argTransactionType;
                if (pTransactionType == enumDBTransaction.spAdd)
                {
                    mReinspectionId = this.Identity.New();
                }
                try
                {
                    CallContentManager();
                }
                catch (Exception) { }
                SqlParameter[] parrSP = new SqlParameter[6];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@ReinspectionId", ReinspectionId);
                parrSP[2] = new SqlParameter("@DtReinspection", DtReinspection);
                parrSP[3] = new SqlParameter("@PlaceOfReinspection", PlaceOfReinspection);
                parrSP[4] = new SqlParameter("@ReinspectionDoneBy", ReinspectionDoneBy);
                parrSP[5] = new SqlParameter("@Remarks", Remarks);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspReinspection", parrSP);

                return null;
            }
            catch (Exception ex)
            {
                OSN.Generic.EventLogHelper appLog = new OSN.Generic.EventLogHelper();
                //appLog.Source = "Communique";
                //StringBuilder sb = new StringBuilder();
                //sb.Append("public bool Transaction(enumDBTransaction argTransactionType)");
                //sb.Append("Exception=" + ex.Message);
                //appLog.WriteEntry(sb.ToString());
                return "Transaction failed";
            }
        }
        private void CallContentManager()
        {
            ContentManager cm = new ContentManager(this.Connection);
            cm.Content = this.Remarks;
            cm.Context = "Reinspection";
            cm.Add();
            RemoveMarkup();
        }
        private void RemoveMarkup()
        {
            //this.Remarks = this.Remarks.Replace("\\%", "^^Percentage^^;");
            this.Remarks = this.Remarks.Replace("%", "");
            this.Remarks.Replace("^^Percentage^^;", "%");
        }

        public string Transaction(enumDBTransaction argTransactionType, System.Data.SqlClient.SqlTransaction argTransaction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public System.Data.DataTable Items()
        {
            try
            {
                SqlParameter[] parrSP = new SqlParameter[6];
                enumDBTransaction pTransactionType = enumDBTransaction.spSelect;
                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@ReinspectionId", ReinspectionId);
                parrSP[2] = new SqlParameter("@DtReinspection", DtReinspection);
                parrSP[3] = new SqlParameter("@PlaceOfReinspection", PlaceOfReinspection);
                parrSP[4] = new SqlParameter("@ReinspectionDoneBy", ReinspectionDoneBy);
                parrSP[5] = new SqlParameter("@Remarks", Remarks);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspReinspection", parrSP);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                //OSN.Generic.EventLogHelper appLog = new OSN.Generic.EventLogHelper();
                //appLog.Source = "Communique";
                //StringBuilder sb = new StringBuilder();
                //sb.Append("public System.Data.DataTable Items()");
                //sb.Append("Exception=" + ex.Message);
                //appLog.WriteEntry(sb.ToString());
                return new DataTable();
            }
        }
        #endregion
        public override string ToString()
        {
            return this.ReinspectionId.ToString();
        }
        public string Attributes(string argName)
        {
            switch (argName)
            {
                case "ReinspectionId":
                    return this.ReinspectionId.ToString();
                    break;
                case "DtReinspection":
                    return this.DtReinspection.ToString();
                    break;
                case "PlaceOfReinspection":
                    return this.PlaceOfReinspection.ToString();
                    break;
                case "ReinspectionDoneBy":
                    return this.ReinspectionDoneBy.ToString();
                    break;
                case "Remarks":
                    return this.Remarks.ToString();
                    break;

                default:
                    return null;
                    break;
            }
        }

    }
}
