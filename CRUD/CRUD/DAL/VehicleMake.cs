using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class VehicleMake : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mVehicleMakeId;
        private System.String mDefault;

        private System.Boolean mIsVATInclusive;

        public System.Boolean IsVATInclusive
        {
            get { return mIsVATInclusive; }
            set { mIsVATInclusive = value; }
        }

        public VehicleMake(String argConnection)
        {
            mConnection = argConnection;
        }
        public VehicleMake(String argConnection, Int32 argVehicleMakeId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM VehicleMake WHERE VehicleMakeId=" + argVehicleMakeId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mVehicleMakeId = (System.Int32)dr["VehicleMakeId"];
                mDefault = (System.String)dr["VehicleMake"];
                mIsVATInclusive = (System.Boolean)dr["IsVATInclusive"];
            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "VehicleMake.VehicleMake()";
                throw ex;
            }
            dr.Close();
        }
        public VehicleMake(String argConnection, System.String argVehicleMake)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM VehicleMake WHERE VehicleMake='" + argVehicleMake + "'";
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mVehicleMakeId = (System.Int32)dr["VehicleMakeId"];
                mDefault = (System.String)dr["VehicleMake"];
                mIsVATInclusive = (System.Boolean)dr["IsVATInclusive"];
            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "VehicleMake.VehicleMake()";
                throw ex;
            }
            dr.Close();
        }

        public System.Int32 VehicleMakeId
        {
            get
            {
                return mVehicleMakeId;
            }
            set
            {
                mVehicleMakeId = value;
            }
        }
        public System.String Default
        {
            get
            {
                return mDefault;
            }
            set
            {
                mDefault = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "VehicleMake", "VehicleMakeId");
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
                    mVehicleMakeId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[4];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@VehicleMakeId", VehicleMakeId);
                parrSP[2] = new SqlParameter("@VehicleMake", Default);
                parrSP[3] = new SqlParameter("@IsVATInclusive", IsVATInclusive);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspVehicleMake", parrSP);
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
                SqlParameter[] parrSP = new SqlParameter[4];
                enumDBTransaction pTransactionType = enumDBTransaction.spSelect;
                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@VehicleMakeId", VehicleMakeId);
                parrSP[2] = new SqlParameter("@VehicleMake", Default);
                parrSP[3] = new SqlParameter("@IsVATInclusive", IsVATInclusive);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspVehicleMake", parrSP);
                ds.Tables[0].TableName = "VehicleMake";
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
            return this.VehicleMakeId.ToString();
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "VehicleMakeId":
        //            return this.VehicleMakeId.ToString();
        //            break;
        //        case "VehicleMake":
        //            return this.Default.ToString();
        //            break;

        //        default:
        //            return null;
        //            break;
        //    }
        //}
    }
}
