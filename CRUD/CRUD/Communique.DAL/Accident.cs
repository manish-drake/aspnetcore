using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class Accident : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mAccidentId;
        private System.DateTime mDtAccident;
        private System.String mPlaceOfAccident;
        private System.DateTime mDtInstructions;
        private System.DateTime mDtSurvey;
        private System.DateTime mDtReceiptSSR;

        public Accident(String argConnection)
        {
            mConnection = argConnection;
        }
        public Accident(String argConnection, Int32 argAccidentId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM Accident WHERE AccidentId=" + argAccidentId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mAccidentId = (System.Int32)dr["AccidentId"];
                mDtAccident = (System.DateTime)dr["DtAccident"];
                mPlaceOfAccident = (System.String)dr["PlaceOfAccident"];
                mDtInstructions = (System.DateTime)dr["DtInstructions"];
                mDtSurvey = (System.DateTime)dr["DtSurvey"];
                mDtReceiptSSR = (System.DateTime)dr["DtReceiptSSR"];
            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "Accident.Accident()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 AccidentId
        {
            get
            {
                return mAccidentId;
            }
            set
            {
                mAccidentId = value;
            }
        }
        public System.DateTime DtAccident
        {
            get
            {
                if (mDtAccident == DateTime.MinValue)
                {
                    Environment.Variables vars = new Environment.Variables(Connection);
                    mDtAccident = DateTime.Parse(vars["MinimumDate"].Value);
                }
                return mDtAccident;
            }
            set
            {
                mDtAccident = value;
            }
        }
        public System.String PlaceOfAccident
        {
            get
            {
                return mPlaceOfAccident;
            }
            set
            {
                mPlaceOfAccident = value;
            }
        }
        public System.DateTime DtInstructions
        {
            get
            {
                if (mDtInstructions == DateTime.MinValue)
                {
                    Environment.Variables vars = new Environment.Variables(Connection);
                    mDtInstructions = DateTime.Parse(vars["MinimumDate"].Value);
                }
                return mDtInstructions;
            }
            set
            {
                mDtInstructions = value;
            }
        }
        public System.DateTime DtSurvey
        {
            get
            {
                if (mDtSurvey == DateTime.MinValue)
                {
                    Environment.Variables vars = new Environment.Variables(Connection);
                    mDtSurvey = DateTime.Parse(vars["MinimumDate"].Value);
                }
                return mDtSurvey;
            }
            set
            {
                mDtSurvey = value;
            }
        }
        public System.DateTime DtReceiptSSR
        {
            get
            {
                if (mDtReceiptSSR == DateTime.MinValue)
                {
                    Environment.Variables vars = new Environment.Variables(Connection);
                    mDtReceiptSSR = DateTime.Parse(vars["MinimumDate"].Value);
                }
                return mDtReceiptSSR;
            }
            set
            {
                mDtReceiptSSR = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "Accident", "AccidentId");
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
                    mAccidentId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[7];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@AccidentId", AccidentId);
                parrSP[2] = new SqlParameter("@DtAccident", DtAccident);
                parrSP[3] = new SqlParameter("@PlaceOfAccident", PlaceOfAccident);
                parrSP[4] = new SqlParameter("@DtInstructions", DtInstructions);
                parrSP[5] = new SqlParameter("@DtSurvey", DtSurvey);
                parrSP[6] = new SqlParameter("@DtReceiptSSR", DtReceiptSSR);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspAccident", parrSP);
                return null;
            }
            catch (Exception ex)
            {
                //OSN.Generic.EventLogHelper appLog = new OSN.Generic.EventLogHelper();
                //appLog.Source = "Communique";
                //StringBuilder sb = new StringBuilder();
                //sb.Append("public bool Transaction(enumDBTransaction argTransactionType)");
                //sb.Append("Exception=" + ex.Message);
                //appLog.WriteEntry(sb.ToString());
                return "Transaction failed";
            }
        }

        public string Transaction(enumDBTransaction argTransactionType, System.Data.SqlClient.SqlTransaction argTransaction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public System.Data.DataTable Items()
        {
            try
            {
                SqlParameter[] parrSP = new SqlParameter[7];
                enumDBTransaction pTransactionType = enumDBTransaction.spSelect;
                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@AccidentId", AccidentId);
                parrSP[2] = new SqlParameter("@DtAccident", DtAccident);
                parrSP[3] = new SqlParameter("@PlaceOfAccident", PlaceOfAccident);
                parrSP[4] = new SqlParameter("@DtInstructions", DtInstructions);
                parrSP[5] = new SqlParameter("@DtSurvey", DtSurvey);
                parrSP[6] = new SqlParameter("@DtReceiptSSR", DtReceiptSSR);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspAccident", parrSP);
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
            return this.AccidentId.ToString();
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "AccidentId":
        //            return this.AccidentId.ToString();
        //            break;
        //        case "DtAccident":
        //            return this.DtAccident.ToString();
        //            break;
        //        case "PlaceOfAccident":
        //            return this.PlaceOfAccident.ToString();
        //            break;
        //        case "DtInstructions":
        //            return this.DtInstructions.ToString();
        //            break;
        //        case "DtSurvey":
        //            return this.DtSurvey.ToString();
        //            break;
        //        case "DtReceiptSSR":
        //            return this.DtReceiptSSR.ToString();
        //            break;
        //        default:
        //            return null;
        //            break;
        //    }
        //}

    }
}
