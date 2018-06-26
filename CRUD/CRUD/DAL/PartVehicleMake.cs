using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class PartVehicleMake : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mPartVehicleMakeId;
        private VehicleMake mVehicleMake;
        private Part mPart;



        public PartVehicleMake(String argConnection)
        {
            mConnection = argConnection;
        }
        public PartVehicleMake(String argConnection, Int32 argPartVehicleMakeId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM PartVehicleMake WHERE PartVehicleMakeId=" + argPartVehicleMakeId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mPartVehicleMakeId = (System.Int32)dr["PartVehicleMakeId"];
                mVehicleMake = new VehicleMake(Connection, (int)dr["VehicleMakeId"]);
                mPart = new Part(Connection, (int)dr["PartId"]);

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "PartVehicleMake.PartVehicleMake()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 PartVehicleMakeId
        {
            get
            {
                return mPartVehicleMakeId;
            }
            set
            {
                mPartVehicleMakeId = value;
            }
        }
        public VehicleMake VehicleMake
        {
            get
            {
                if (mVehicleMake == null)
                {
                    mVehicleMake = new VehicleMake(Connection);
                }
                return mVehicleMake;
            }
            set
            {
                mVehicleMake = value;
            }
        }
        public Part Part
        {
            get
            {
                if (mPart == null)
                {
                    mPart = new Part(Connection);
                }
                return mPart;
            }
            set
            {
                mPart = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "PartVehicleMake", "PartVehicleMakeId");
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
                    mPartVehicleMakeId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[4];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@PartVehicleMakeId", PartVehicleMakeId);
                parrSP[2] = new SqlParameter("@VehicleMakeId", VehicleMake.VehicleMakeId);
                parrSP[3] = new SqlParameter("@PartId", Part.PartId);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspPartVehicleMake", parrSP);
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
                parrSP[1] = new SqlParameter("@PartVehicleMakeId", PartVehicleMakeId);
                parrSP[2] = new SqlParameter("@VehicleMakeId", VehicleMake.VehicleMakeId);
                parrSP[3] = new SqlParameter("@PartId", Part.PartId);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspPartVehicleMake", parrSP);
                ds.Tables[0].TableName = "PartVehicleMake";
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
            return this.PartVehicleMakeId.ToString();
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "PartVehicleMakeId":
        //            return this.PartVehicleMakeId.ToString();
        //            break;
        //        case "VehicleMakeId":
        //            return this.VehicleMake.VehicleMakeId.ToString();
        //            break;
        //        case "PartId":
        //            return this.Part.PartId.ToString();
        //            break;

        //        default:
        //            return null;
        //            break;
        //    }
        //}

    }
}
