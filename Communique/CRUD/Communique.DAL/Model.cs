using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class Model : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mModelId;
        private VehicleMake mVehicleMake;
        private System.String mDefault;
        private VehicleCategory mVehicleCategory;
        private System.String mTypeOfBody;
        private System.Int32 mModelType;

        public System.Int32 ModelType
        {
            get { return mModelType; }
            set { mModelType = value; }
        }

        public System.String TypeOfBody
        {
            get { return mTypeOfBody; }
            set { mTypeOfBody = value; }
        }

        public VehicleCategory VehicleCategory
        {
            get
            {
                if (mVehicleCategory == null)
                {
                    mVehicleCategory = new VehicleCategory(Connection);
                }
                return mVehicleCategory;
            }
            set { mVehicleCategory = value; }
        }

        private System.Single mExcess;

        public System.Single Excess
        {
            get { return mExcess; }
            set { mExcess = value; }
        }

        public Model(String argConnection)
        {
            mConnection = argConnection;
        }
        public Model(String argConnection, Int32 argModelId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM Model WHERE ModelId=" + argModelId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mModelId = (System.Int32)dr["ModelId"];
                mVehicleMake = new VehicleMake(Connection, (int)dr["VehicleMakeId"]);
                mVehicleCategory = new VehicleCategory(Connection, (int)dr["VehicleCategoryId"]);
                mDefault = (System.String)dr["Model"];
                mExcess = (System.Single)dr["Excess"];
                mTypeOfBody = dr["TypeOfBody"].ToString();
                mModelType = (System.Int32)dr["ModelType"];
                mVehicleCategory = new VehicleCategory(Connection, (System.Int32)dr["VehicleCategoryId"]);

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "Model.Model()";
                throw ex;
            }
            dr.Close();
        }
        public Model(String argConnection, System.String argModel)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM Model WHERE Model='" + argModel + "'";
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mModelId = (System.Int32)dr["ModelId"];
                mVehicleMake = new VehicleMake(Connection, (int)dr["VehicleMakeId"]);
                mVehicleCategory = new VehicleCategory(Connection, (int)dr["VehicleCategoryId"]);
                mDefault = (System.String)dr["Model"];
                mExcess = (System.Single)dr["Excess"];
                mTypeOfBody = dr["TypeOfBody"].ToString();
                mModelType = (System.Int32)dr["ModelType"];
                mVehicleCategory = new VehicleCategory(Connection, (System.Int32)dr["VehicleCategoryId"]);

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "Model.Model()";
                throw ex;
            }
            dr.Close();
        }

        public System.Int32 ModelId
        {
            get
            {
                return mModelId;
            }
            set
            {
                mModelId = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "Model", "ModelId", mModelId);
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
                    mModelId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[8];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@ModelId", ModelId);
                parrSP[2] = new SqlParameter("@VehicleMakeId", VehicleMake.VehicleMakeId);
                parrSP[3] = new SqlParameter("@Model", Default);
                parrSP[4] = new SqlParameter("@Excess", Excess);
                parrSP[5] = new SqlParameter("@VehicleCategoryId", VehicleCategory.VehicleCategoryId);
                parrSP[6] = new SqlParameter("@TypeOfBody", TypeOfBody);
                parrSP[7] = new SqlParameter("@ModelType", ModelType);

                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspModel", parrSP);
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
        public System.Data.DataTable Items(params System.String[] argJoinTables)
        {
            System.String sql = "SELECT DISTINCT Model.*, VehicleMake.VehicleMake ";
            sql += "FROM ((Model INNER JOIN (ModelVersion INNER JOIN (Vehicle INNER JOIN Assessment ON Vehicle.VehicleId = Assessment.VehicleId) ON ModelVersion.ModelVersionId = Vehicle.ModelVersionId) ON Model.ModelId = ModelVersion.ModelId ) INNER JOIN VehicleMake ";
            sql += "ON Model.VehicleMakeId = VehicleMake.VehicleMakeId) ";

            DataSet ds = SqlHelper.ExecuteDataset(Connection, CommandType.Text, sql);
            ds.Tables[0].TableName = "Model";
            return ds.Tables[0];
        }
        public System.Data.DataTable Items()
        {
            try
            {
                SqlParameter[] parrSP = new SqlParameter[8];
                enumDBTransaction pTransactionType = enumDBTransaction.spSelect;
                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@ModelId", ModelId);
                parrSP[2] = new SqlParameter("@VehicleMakeId", VehicleMake.VehicleMakeId);
                parrSP[3] = new SqlParameter("@Model", Default);
                parrSP[4] = new SqlParameter("@Excess", Excess);
                parrSP[5] = new SqlParameter("@VehicleCategoryId", VehicleCategory.VehicleCategoryId);
                parrSP[6] = new SqlParameter("@TypeOfBody", TypeOfBody);
                parrSP[7] = new SqlParameter("@ModelType", ModelType);

                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspModel", parrSP);
                ds.Tables[0].TableName = "Model";
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
        public System.Data.DataTable Items(System.String argVehicleMake)
        {
            try
            {
                string sql = "SELECT DISTINCT Model.*, VehicleMake.VehicleMake ";
                sql += "FROM (Model INNER JOIN VehicleMake ";
                sql += "ON Model.VehicleMakeId = VehicleMake.VehicleMakeId) ";
                // sql += "WHERE VehicleMake.VehicleMake = '" + argVehicleMake + "'";

                DataSet ds = SqlHelper.ExecuteDataset(Connection, CommandType.Text, sql);
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
            return this.Default;
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "ModelId":
        //            return this.ModelId.ToString();
        //            break;
        //        case "VehicleMakeId":
        //            return this.VehicleMake.VehicleMakeId.ToString();
        //            break;
        //        case "Model":
        //            return this.Default.ToString();
        //            break;
        //        case "Excess":
        //            return this.Excess.ToString();
        //            break;
        //        case "VehicleCategory":
        //            return this.VehicleCategory.ToString();
        //            break;
        //        default:
        //            return null;
        //            break;
        //    }
        //}

    }
}
