using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class FIR : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mFIRId;
        private System.Boolean mHasBeenReported;
        private System.String mPoliceStation;
        private System.String mStationDiaryNo;
        private System.DateTime mDtStationDiary;

        public FIR(String argConnection)
        {
            mConnection = argConnection;
        }
        public FIR(String argConnection, Int32 argFIRId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM FIR WHERE FIRId=" + argFIRId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mFIRId = (System.Int32)dr["FIRId"];
                mHasBeenReported = (System.Boolean)dr["HasBeenReported"];
                mPoliceStation = (System.String)dr["PoliceStation"];
                mStationDiaryNo = (System.String)dr["StationDiaryNo"];
                mDtStationDiary = (System.DateTime)dr["DtStationDiary"];

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "FIR.FIR()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 FIRId
        {
            get
            {
                return mFIRId;
            }
            set
            {
                mFIRId = value;
            }
        }
        public System.Boolean HasBeenReported
        {
            get
            {
                return mHasBeenReported;
            }
            set
            {
                mHasBeenReported = value;
            }
        }
        public System.String PoliceStation
        {
            get
            {
                return mPoliceStation;
            }
            set
            {
                mPoliceStation = value;
            }
        }
        public System.String StationDiaryNo
        {
            get
            {
                return mStationDiaryNo;
            }
            set
            {
                mStationDiaryNo = value;
            }
        }
        public System.DateTime DtStationDiary
        {
            get
            {
                if (mDtStationDiary == DateTime.MinValue)
                {
                    Environment.Variables vars = new Environment.Variables(Connection);
                    mDtStationDiary = DateTime.Parse(vars["MinimumDate"].Value);
                }
                return mDtStationDiary;
            }
            set
            {
                mDtStationDiary = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "FIR", "FIRId");
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
                    mFIRId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[6];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@FIRId", FIRId);
                parrSP[2] = new SqlParameter("@HasBeenReported", HasBeenReported);
                parrSP[3] = new SqlParameter("@PoliceStation", PoliceStation);
                parrSP[4] = new SqlParameter("@StationDiaryNo", StationDiaryNo);
                parrSP[5] = new SqlParameter("@DtStationDiary", DtStationDiary);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspFIR", parrSP);
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
                SqlParameter[] parrSP = new SqlParameter[6];
                enumDBTransaction pTransactionType = enumDBTransaction.spSelect;
                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@FIRId", FIRId);
                parrSP[2] = new SqlParameter("@HasBeenReported", HasBeenReported);
                parrSP[3] = new SqlParameter("@PoliceStation", PoliceStation);
                parrSP[4] = new SqlParameter("@StationDiaryNo", StationDiaryNo);
                parrSP[5] = new SqlParameter("@DtStationDiary", DtStationDiary);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspFIR", parrSP);
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
            return this.FIRId.ToString();
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "FIRId":
        //            return this.FIRId.ToString();
        //            break;
        //        case "HasBeenReported":
        //            return this.HasBeenReported.ToString();
        //            break;
        //        case "PoliceStation":
        //            return this.PoliceStation.ToString();
        //            break;
        //        case "StationDiaryNo":
        //            return this.StationDiaryNo.ToString();
        //            break;
        //        case "DtStationDiary":
        //            return this.DtStationDiary.ToString();
        //            break;

        //        default:
        //            return null;
        //            break;
        //    }
        //}

    }
}
