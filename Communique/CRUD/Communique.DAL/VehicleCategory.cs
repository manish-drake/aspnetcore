using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class VehicleCategory : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mVehicleCategoryId;
        private System.String mDefault;



        public VehicleCategory(String argConnection)
        {
            mConnection = argConnection;
        }
        public VehicleCategory(String argConnection, Int32 argVehicleCategoryId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM VehicleCategory WHERE VehicleCategoryId=" + argVehicleCategoryId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mVehicleCategoryId = (System.Int32)dr["VehicleCategoryId"];
                mDefault = (System.String)dr["VehicleCategory"];

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "VehicleCategory.VehicleCategory()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 VehicleCategoryId
        {
            get
            {
                return mVehicleCategoryId;
            }
            set
            {
                mVehicleCategoryId = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "VehicleCategory", "VehicleCategoryId");
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
                    mVehicleCategoryId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[3];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@VehicleCategoryId", VehicleCategoryId);
                parrSP[2] = new SqlParameter("@VehicleCategory", Default);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspVehicleCategory", parrSP);
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
                SqlParameter[] parrSP = new SqlParameter[3];
                enumDBTransaction pTransactionType = enumDBTransaction.spSelect;
                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@VehicleCategoryId", VehicleCategoryId);
                parrSP[2] = new SqlParameter("@VehicleCategory", Default);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspVehicleCategory", parrSP);
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
            return this.VehicleCategoryId.ToString();
        }
        public System.Boolean IsValid(LicenseType argLicenseType)
        {
            System.String sql = "SELECT * FROM VldtLtVc WHERE VehicleCategoryId = " + this.VehicleCategoryId + " AND LicenseTypeId = " + argLicenseType.LicenseTypeId;
            System.Data.SqlClient.SqlDataReader dr =
                SqlHelper.ExecuteReader(Connection, CommandType.Text, sql);
            System.Boolean pIsValid = dr.Read();
            dr.Close();
            return pIsValid;
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "VehicleCategoryId":
        //            return this.VehicleCategoryId.ToString();
        //            break;
        //        case "VehicleCategory":
        //            return this.Default.ToString();
        //            break;

        //        default:
        //            return null;
        //            break;
        //    }
        //}

    }
}
