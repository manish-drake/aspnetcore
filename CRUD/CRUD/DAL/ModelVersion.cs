using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class ModelVersion : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mModelVersionId;
        private Model mModel;
        private System.String mDefault;
        private System.Single mEngineCapacity;
        private System.String mFuelCategory;

        private System.Double mExcess;

        public System.Double Excess
        {
            get { return mExcess; }
            set { mExcess = value; }
        }

        public ModelVersion(String argConnection)
        {
            mConnection = argConnection;
        }
        public ModelVersion(String argConnection, Int32 argModelVersionId)
        {
            mConnection = argConnection;
            //StringBuilder sb = new StringBuilder();

            string pstrSql = System.String.Format("SELECT {0}, {1}, {2}, {3}, {4}, {5} FROM ModelVersion WHERE ModelVersionId={6}",
                "ModelVersionId",       //0
                "ModelId",              //1
                "ModelVersion",         //2
                "EngineCapacity",       //3
                "FuelCategory",         //4
                "Excess",               //5
                argModelVersionId);
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mModelVersionId = dr.GetInt32(0);
                mModel = new Model(Connection, dr.GetInt32(1));
                mDefault = dr.GetString(2);
                mEngineCapacity = dr.GetFloat(3);
                mFuelCategory = dr.GetString(4);
                mExcess = dr.GetFloat(5);
            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "ModelVersion.ModelVersion()";
                throw ex;
            }
            dr.Close();
        }
        public ModelVersion(String argConnection, System.String argModelVersion, System.String argModel)
        {
            string pstrSql = "";
            mConnection = argConnection;
            StringBuilder sb = new StringBuilder();


            pstrSql = System.String.Format("SELECT {0}, {1}, {2}, {3}, {4}, {5} FROM ModelVersion INNER JOIN Model ON ModelVersion.ModelId = Model.ModelId WHERE ModelVersion.ModelVersion='{6}' AND Model.Model = '{7}'",
             "ModelVersion.ModelVersionId",       //0
             "ModelVersion.ModelId",              //1
             "ModelVersion.ModelVersion",         //2
             "ModelVersion.EngineCapacity",       //3
             "ModelVersion.FuelCategory",         //4
             "ModelVersion.Excess",               //5
             argModelVersion,                     //6
             argModel);


            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mModelVersionId = dr.GetInt32(0);
                mModel = new Model(Connection, dr.GetInt32(1));
                mDefault = dr.GetString(2);
                mEngineCapacity = dr.GetFloat(3);
                mFuelCategory = dr.GetString(4);
                mExcess = dr.GetFloat(5);
            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "ModelVersion.ModelVersion()";
                throw ex;
            }
            dr.Close();
        }

        public System.Int32 ModelVersionId
        {
            get
            {
                return mModelVersionId;
            }
            set
            {
                mModelVersionId = value;
            }
        }
        public Model Model
        {
            get
            {
                if (mModel == null)
                {
                    mModel = new Model(Connection);
                }
                return mModel;
            }
            set
            {
                mModel = value;
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
        public System.Single EngineCapacity
        {
            get
            {
                return mEngineCapacity;
            }
            set
            {
                mEngineCapacity = value;
            }
        }
        public System.String FuelCategory
        {
            get
            {
                return mFuelCategory;
            }
            set
            {
                mFuelCategory = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "ModelVersion", "ModelVersionId");
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
                    mModelVersionId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[7];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@ModelVersionId", ModelVersionId);
                parrSP[2] = new SqlParameter("@ModelId", Model.ModelId);
                parrSP[3] = new SqlParameter("@ModelVersion", Default);
                parrSP[4] = new SqlParameter("@EngineCapacity", EngineCapacity);
                parrSP[5] = new SqlParameter("@FuelCategory", FuelCategory);
                parrSP[6] = new SqlParameter("@Excess", Excess);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspModelVersion", parrSP);
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
                parrSP[1] = new SqlParameter("@ModelVersionId", ModelVersionId);
                parrSP[2] = new SqlParameter("@ModelId", Model.ModelId);
                parrSP[3] = new SqlParameter("@ModelVersion", Default);
                parrSP[4] = new SqlParameter("@EngineCapacity", EngineCapacity);
                parrSP[5] = new SqlParameter("@FuelCategory", FuelCategory);
                parrSP[6] = new SqlParameter("@Excess", Excess);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspModelVersion", parrSP);
                ds.Tables[0].TableName = "ModelVersion";
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
        public System.Data.DataTable Items(String argVehicleMake)
        {
            try
            {
                String sql = "SELECT ModelVersion.*, Model.Model, ";
                sql += "Model.VehicleMakeId, VehicleMake.VehicleMake ";
                sql += "FROM (ModelVersion INNER JOIN (Model ";
                sql += "INNER JOIN VehicleMake ";
                sql += "ON Model.VehicleMakeId = VehicleMake.VehicleMakeId) ";
                sql += "ON ModelVersion.ModelId=Model.ModelId) ";
                sql += "WHERE VehicleMake.VehicleMake='" + argVehicleMake + "' ";


                DataSet ds = SqlHelper.ExecuteDataset(Connection, CommandType.Text, sql);
                ds.Tables[0].TableName = "ModelVersion";
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
        public System.Data.DataTable Items(String argVehicleMake, String argModel)
        {
            try
            {
                String sql = "";

                if (!string.IsNullOrEmpty(argVehicleMake) || !string.IsNullOrEmpty(argModel))
                {
                    sql = "SELECT ModelVersion.*, Model.Model, ";
                    sql += "Model.VehicleMakeId, VehicleMake.VehicleMake ";
                    sql += "FROM (ModelVersion INNER JOIN (Model ";
                    sql += "INNER JOIN VehicleMake ";
                    sql += "ON Model.VehicleMakeId = VehicleMake.VehicleMakeId) ";
                    sql += "ON ModelVersion.ModelId=Model.ModelId) ";
                    sql += "WHERE VehicleMake.VehicleMake='" + argVehicleMake + "' ";
                    sql += "AND Model.Model = '" + argModel + "'";
                }
                else
                {
                    sql = "SELECT ModelVersion.*, Model.Model, ";
                    sql += "Model.VehicleMakeId, VehicleMake.VehicleMake ";
                    sql += "FROM (ModelVersion INNER JOIN (Model ";
                    sql += "INNER JOIN VehicleMake ";
                    sql += "ON Model.VehicleMakeId = VehicleMake.VehicleMakeId) ";
                    sql += "ON ModelVersion.ModelId=Model.ModelId) ";
                }

                DataSet ds = SqlHelper.ExecuteDataset(Connection, CommandType.Text, sql);
                ds.Tables[0].TableName = "ModelVersion";
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
            return this.ModelVersionId.ToString();
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "ModelVersionId":
        //            return this.ModelVersionId.ToString();
        //            break;
        //        case "ModelId":
        //            return this.Model.ModelId.ToString();
        //            break;
        //        case "ModelVersion":
        //            return this.Default.ToString();
        //            break;
        //        case "EngineCapacity":
        //            return this.EngineCapacity.ToString();
        //            break;
        //        case "FuelCategory":
        //            return this.FuelCategory.ToString();
        //            break;

        //        default:
        //            return null;
        //            break;
        //    }
        //}

    }
}
