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
    public class Part : IDBObject
    {

        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mPartId;
        private System.String mDefault;
        private PartCategory mPartCategory;
        private System.Single mVatRate;
        private System.String mCode;
        private System.Boolean mIsIMT23Applicable;
        private PartCategoryCompanies mPartCategoryCompanies;

        public PartCategoryCompanies PartCategoryCompanies
        {
            get
            {
                if (mPartCategoryCompanies == null)
                {
                    mPartCategoryCompanies = new PartCategoryCompanies();
                    System.String sql = System.String.Format(
                        "SELECT * FROM PartCategoryCompany WHERE PartCategoryCompany.PartId = {0}"
                        , this.PartId);
                    SqlDataReader sdr =
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(this.Connection, CommandType.Text, sql);
                    while (sdr.Read())
                    {
                        PartCategoryCompany pcc =
                            new PartCategoryCompany(this.Connection,
                            sdr.GetInt32(sdr.GetOrdinal("PartCategoryCompanyId")));
                        mPartCategoryCompanies.Add(pcc);
                    }
                    sdr.Close();
                }
                return mPartCategoryCompanies;
            }
            set
            {
                mPartCategoryCompanies = value;
            }
        }
        public System.Boolean IsIMT23Applicable
        {
            get { return mIsIMT23Applicable; }
            set { mIsIMT23Applicable = value; }
        }

        public System.String Code
        {
            get { return mCode; }
            set { mCode = value; }
        }

        public System.Single VatRate
        {
            get { return mVatRate; }
            set { mVatRate = value; }
        }
        private LabourSchdDB mLabourSchdDB;

        public LabourSchdDB LabourSchdDB
        {
            get
            {
                if (mLabourSchdDB == null)
                {
                    if (this.PartId <= 0)
                    {
                        mLabourSchdDB = new LabourSchdDB(Connection);
                    }
                    else
                    {
                        String sql =
                            String.Format("SELECT * FROM LabourSchdDB WHERE PartId={0}", this.PartId);
                        SqlDataReader sdr = SqlHelper.ExecuteReader(Connection, CommandType.Text, sql);
                        if (sdr.Read())
                        {
                            mLabourSchdDB = new LabourSchdDB(Connection, (int)sdr["LabourSchdDBId"]);
                        }
                        else
                        {
                            mLabourSchdDB = new LabourSchdDB(Connection);
                        }
                        sdr.Close();
                    }
                }
                return mLabourSchdDB;
            }
            set { mLabourSchdDB = value; }
        }
        private LabourSchdRR mLabourSchdRR;

        public LabourSchdRR LabourSchdRR
        {
            get
            {
                if (mLabourSchdRR == null)
                {
                    if (this.PartId <= 0)
                    {
                        mLabourSchdRR = new LabourSchdRR(Connection);
                    }
                    else
                    {
                        String sql =
                            String.Format("SELECT * FROM LabourSchdRR WHERE PartId={0}", this.PartId);
                        SqlDataReader sdr = SqlHelper.ExecuteReader(Connection, CommandType.Text, sql);
                        if (sdr.Read())
                        {
                            mLabourSchdRR = new LabourSchdRR(Connection, (int)sdr["LabourSchdRRId"]);
                        }
                        else
                        {
                            mLabourSchdRR = new LabourSchdRR(Connection);
                        }
                        sdr.Close();
                    }
                }
                return mLabourSchdRR;
            }
            set { mLabourSchdRR = value; }
        }
        private LabourSchdPTs mLabourSchdPTs;
        public LabourSchdPTs LabourSchdPTs
        {
            get
            {
                if (mLabourSchdPTs == null)
                {
                    if (this.PartId <= 0)
                    {
                        mLabourSchdPTs = new LabourSchdPTs();
                    }
                    else
                    {
                        mLabourSchdPTs = new LabourSchdPTs();
                        String sql =
                            String.Format("SELECT * FROM LabourSchdPT WHERE PartId={0}", this.PartId);
                        SqlDataReader sdr = SqlHelper.ExecuteReader(Connection, CommandType.Text, sql);
                        while (sdr.Read())
                        {
                            LabourSchdPT pLabourSchdPT =
                                 new LabourSchdPT(Connection, (int)sdr["LabourSchdPTId"]);
                            mLabourSchdPTs.Add(pLabourSchdPT, pLabourSchdPT.PaintType.Default);
                        }
                        sdr.Close();
                    }
                }
                return mLabourSchdPTs;
            }
            set { mLabourSchdPTs = value; }
        }
        private PartRate mPartRate;
        public PartRate PartRate
        {
            get
            {
                if (mPartRate == null)
                {
                    string sql = "SELECT Top 1 PartRateId FROM PartRate WHERE PartId=" + this.PartId + " ORDER BY DtWEF DESC";
                    SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, sql);
                    if (dr.Read())
                    {
                        mPartRate =
                            new PartRate(Connection, (System.Int32)dr["PartRateId"]);
                    }
                    else
                    {
                        mPartRate = new PartRate(Connection);
                        mPartRate.Part = this;
                    }
                    dr.Close();
                }
                return mPartRate;
            }
        }

        private PartVehicleMake mPartVehicleMake;
        public PartVehicleMake PartVehicleMake
        {
            get
            {
                if (mPartVehicleMake == null)
                {
                    string sql = "SELECT PartVehicleMakeId FROM PartVehicleMake WHERE PartId=" + this.PartId;
                    SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, sql);
                    if (dr.Read())
                    {
                        mPartVehicleMake =
                            new PartVehicleMake(Connection, (System.Int32)dr["PartVehicleMakeId"]);
                    }
                    else
                    {
                        mPartVehicleMake = new PartVehicleMake(Connection);
                    }
                    dr.Close();
                }
                return mPartVehicleMake;
            }
        }
        private PartModel mPartModel;
        public PartModel PartModel
        {
            get
            {
                if (mPartModel == null)
                {
                    string sql = "SELECT PartModelId FROM PartModel WHERE PartId=" + this.PartId;
                    SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, sql);
                    if (dr.Read())
                    {
                        mPartModel =
                            new PartModel(Connection, (System.Int32)dr["PartModelId"]);
                    }
                    else
                    {
                        mPartModel = new PartModel(Connection);
                    }
                    dr.Close();
                }
                return mPartModel;
            }
        }
        private PartModelVersion mPartModelVerison;
        public PartModelVersion PartModelVersion
        {
            get
            {
                if (mPartModelVerison == null)
                {
                    string sql = "SELECT PartModelVersionId FROM PartModelVersion WHERE PartId=" + this.PartId;
                    SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, sql);
                    if (dr.Read())
                    {
                        mPartModelVerison =
                            new PartModelVersion(Connection, (System.Int32)dr["PartModelVersionId"]);
                    }
                    else
                    {
                        mPartModelVerison = new PartModelVersion(Connection);
                    }
                    dr.Close();
                }
                return mPartModelVerison;
            }
        }
        private String mAffiliationType;
        public String AffiliationType
        {
            get
            {
                if (mAffiliationType == null)
                {
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.Append("SELECT 'PartVehicleMake' AS AffiliationType FROM PartVehicleMake WHERE PartId = " + this.PartId);
                    sbSql.Append(" UNION ");

                    sbSql.Append("SELECT 'PartModel' AS AffiliationType FROM PartModel WHERE PartId = " + this.PartId);
                    sbSql.Append(" UNION ");

                    sbSql.Append("SELECT 'PartModelVersion' AS AffiliationType FROM PartModelVersion WHERE PartId = " + this.PartId);

                    System.Data.SqlClient.SqlDataReader dr =
                        SqlHelper.ExecuteReader(this.Connection, CommandType.Text, sbSql.ToString());
                    if (dr.Read())
                    {
                        mAffiliationType = dr["AffiliationType"].ToString();
                    }
                    else
                    {
                        mAffiliationType = "";
                    }
                    dr.Close();
                }
                return mAffiliationType;
            }
        }

        public Object Affiliation
        {
            get
            {
                switch (this.AffiliationType)
                {
                    case "PartVehicleMake":
                        {
                            return this.PartVehicleMake;
                        }
                    case "PartModel":
                        {
                            return this.PartModel;
                        }
                    case "PartModelVersion":
                        {
                            return this.PartModelVersion;
                        }
                    default:
                        {
                            return new object();
                        }
                }
            }
        }

        public Part(String argConnection)
        {
            mConnection = argConnection;
        }
        public Part(String argConnection, Int32 argPartId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM Part WHERE PartId=" + argPartId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mPartId = (System.Int32)dr["PartId"];
                mDefault = (System.String)dr["Part"];
                mPartCategory = new PartCategory(Connection, (int)dr["PartCategoryId"]);
                mVatRate = (System.Single)dr["VatRate"];
                mCode = dr["Code"].ToString();
                mIsIMT23Applicable = dr.GetBoolean(dr.GetOrdinal("IsIMT23Applicable"));
            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "Part.Part()";
                //throw ex;
            }
            dr.Close();
        }


        public System.Int32 PartId
        {
            get
            {
                return mPartId;
            }
            set
            {
                mPartId = value;
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
        public PartCategory PartCategory
        {
            get
            {
                if (mPartCategory == null)
                {
                    mPartCategory = new PartCategory(Connection);
                }
                return mPartCategory;
            }
            set
            {
                mPartCategory = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "Part", "PartId");
                }
                return mIdentity;
            }
        }
        SqlConnection conTransaction = null;
        SqlTransaction trsTransaction = null;
        public string Transaction(enumDBTransaction argTransactionType)
        {
            try
            {
                conTransaction = new SqlConnection(Connection);
                conTransaction.Open();
                trsTransaction = conTransaction.BeginTransaction();
                enumDBTransaction pTransactionType = argTransactionType;
                if (pTransactionType == enumDBTransaction.spAdd)
                {
                    mPartId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[7];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@PartId", PartId);
                parrSP[2] = new SqlParameter("@Part", Default);
                parrSP[3] = new SqlParameter("@PartCategoryId", PartCategory.PartCategoryId);
                parrSP[4] = new SqlParameter("@VatRate", VatRate);
                parrSP[5] = new SqlParameter("@Code", Code);
                parrSP[6] = new SqlParameter("@IsIMT23Applicable", IsIMT23Applicable);

                SqlHelper.ExecuteNonQuery(trsTransaction, CommandType.StoredProcedure, "uspPart", parrSP);
                //foreach (Insurance.Surveyor.LabourSchdPT var in this.LabourSchdPTs.Values)
                //{
                //    var.Transaction(enumDBTransaction.spAdd);
                //}
                this.PartCategoryCompanies.Save(this);
                trsTransaction.Commit();
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
                //trsTransaction.Rollback();
                return "Transaction failed";
            }
            finally
            {
                conTransaction.Close();
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
                parrSP[1] = new SqlParameter("@PartId", PartId);
                parrSP[2] = new SqlParameter("@Part", Default);
                parrSP[3] = new SqlParameter("@PartCategoryId", PartCategory.PartCategoryId);
                parrSP[4] = new SqlParameter("@VatRate", VatRate);
                parrSP[5] = new SqlParameter("@Code", Code);
                parrSP[6] = new SqlParameter("@IsIMT23Applicable", IsIMT23Applicable);

                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspPart", parrSP);

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
        public System.Data.DataTable Items(ModelVersion argMv)
        {
            try
            {

                //string sql = "SELECT Part.PartId, Part.Part, Part.PartCategoryId, Part.VatRate, ";
                //sql += "Part.MCode, PartCategory.PartCategory, Model.Model, ModelVersion.ModelVersion, ";
                //sql += "VehicleMake.VehicleMake, Part.Code ";
                //sql += "FROM VehicleMake LEFT OUTER JOIN ";
                //sql += "PartVehicleMake ON VehicleMake.VehicleMakeId = PartVehicleMake.VehicleMakeId ";
                //sql += "RIGHT OUTER JOIN ";
                //sql += "Part INNER JOIN ";
                //sql += "PartCategory ON Part.PartCategoryId = PartCategory.PartCategoryId LEFT OUTER JOIN ";
                //sql += "Model RIGHT OUTER JOIN ";
                //sql += "PartModel ON Model.ModelId = PartModel.ModelId ON Part.PartId = PartModel.PartId ";
                //sql += "ON PartVehicleMake.PartId = Part.PartId LEFT OUTER JOIN ";
                //sql += "PartModelVersion LEFT OUTER JOIN ";
                //sql += "ModelVersion ON PartModelVersion.ModelVersionId = ModelVersion.ModelVersionId ";
                //sql += "ON Part.PartId = PartModelVersion.PartId ";
                //sql += "WHERE ModelVersion.ModelVersionId = " + argMv.ModelVersionId;
                String sql = null;
                sql = "SELECT CompundSelect.* FROM ( ";
                sql += "	SELECT 			";
                sql += "		Part.PartId, Part.Part, Part.PartCategoryId, Code,		";
                sql += "		Part.VatRate, Part.MCode, PartCategory.PartCategory, 		";
                sql += "		Model.Model, ModelVersion.ModelVersion, VehicleMake.VehicleMake 		";
                sql += "	FROM 			";
                sql += "		(ModelVersion INNER JOIN (Model INNER JOIN VehicleMake 		";
                sql += "			ON Model.VehicleMakeId = VehicleMake.VehicleMakeId) 	";
                sql += "			ON ModelVersion.ModelId = Model.ModelId)	";
                sql += "			INNER JOIN (PartModelVersion INNER JOIN (Part INNER JOIN PartCategory 	";
                sql += "			ON Part.PartCategoryId = PartCategory.PartCategoryId) 	";
                sql += "			ON PartModelVersion.PartId = Part.PartId)	";
                sql += "			ON PartModelVersion.ModelVersionId = ModelVersion.ModelVersionId	";
                sql += "      WHERE ModelVersion.ModelVersionId = " + argMv.ModelVersionId + " ";
                sql += "	UNION 			";
                sql += "	SELECT 			";
                sql += "		Part.PartId, Part.Part, Part.PartCategoryId, Code,		";
                sql += "		Part.VatRate, Part.MCode, PartCategory.PartCategory, 		";
                sql += "		Model.Model, '<All>', VehicleMake.VehicleMake 		";
                sql += "	FROM 			";
                sql += "		(Model INNER JOIN VehicleMake 		";
                sql += "			ON Model.VehicleMakeId = VehicleMake.VehicleMakeId)	";
                sql += "			INNER JOIN (PartModel INNER JOIN (Part INNER JOIN PartCategory 	";
                sql += "			ON Part.PartCategoryId = PartCategory.PartCategoryId) 	";
                sql += "			ON PartModel.PartId = Part.PartId)	";
                sql += "			ON Model.ModelId = PartModel.ModelId 	";
                sql += "      WHERE Model.ModelId = " + argMv.Model.ModelId + " ";
                sql += "	UNION 			";
                sql += "	SELECT 			";
                sql += "		Part.PartId, Part.Part, Part.PartCategoryId, Code,		";
                sql += "		Part.VatRate, Part.MCode, PartCategory.PartCategory, 		";
                sql += "		'<All>', '<All>', VehicleMake.VehicleMake 		";
                sql += "	FROM 			";
                sql += "		VehicleMake		";
                sql += "		INNER JOIN (PartVehicleMake INNER JOIN (Part INNER JOIN PartCategory 		";
                sql += "			ON Part.PartCategoryId = PartCategory.PartCategoryId) 	";
                sql += "			ON PartVehicleMake.PartId = Part.PartId) 	";
                sql += "			ON VehicleMake.VehicleMakeId = PartVehicleMake.VehicleMakeId 	";
                sql += "      WHERE VehicleMake.VehicleMakeId = " + argMv.Model.VehicleMake.VehicleMakeId + " ";
                sql += ") CompundSelect ORDER BY CompundSelect.Part";

                DataSet ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Connection, CommandType.Text, sql);
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
        private String SQLSelectParts()
        {
            String sql = null;
            sql += "	SELECT 			";
            sql += "		Part.PartId, Part.Part, Part.PartCategoryId, Code,		";
            sql += "		Part.VatRate, Part.MCode, PartCategory.PartCategory, 		";
            sql += "		Model.Model, ModelVersion.ModelVersion, VehicleMake.VehicleMake, Rate 		";
            sql += "	FROM 			";
            sql += "		(ModelVersion INNER JOIN (Model INNER JOIN VehicleMake 		";
            sql += "			ON Model.VehicleMakeId = VehicleMake.VehicleMakeId) 	";
            sql += "			ON ModelVersion.ModelId = Model.ModelId)	";
            sql += "			INNER JOIN (PartModelVersion INNER JOIN ((Part INNER JOIN PartRate ON Part.PartId = PartRate.PartId) INNER JOIN PartCategory 	";
            sql += "			ON Part.PartCategoryId = PartCategory.PartCategoryId) 	";
            sql += "			ON PartModelVersion.PartId = Part.PartId)	";
            sql += "			ON PartModelVersion.ModelVersionId = ModelVersion.ModelVersionId	";
            sql += "	UNION ALL			";
            sql += "	SELECT 			";
            sql += "		Part.PartId, Part.Part, Part.PartCategoryId, Code,		";
            sql += "		Part.VatRate, Part.MCode, PartCategory.PartCategory, 		";
            sql += "		Model.Model, '<All>', VehicleMake.VehicleMake, Rate 		";
            sql += "	FROM 			";
            sql += "		(Model INNER JOIN VehicleMake 		";
            sql += "			ON Model.VehicleMakeId = VehicleMake.VehicleMakeId)	";
            sql += "			INNER JOIN (PartModel INNER JOIN ((Part INNER JOIN PartRate ON Part.PartId = PartRate.PartId) INNER JOIN PartCategory 	";
            sql += "			ON Part.PartCategoryId = PartCategory.PartCategoryId) 	";
            sql += "			ON PartModel.PartId = Part.PartId)	";
            sql += "			ON Model.ModelId = PartModel.ModelId 	";
            sql += "	UNION ALL			";
            sql += "	SELECT 			";
            sql += "		Part.PartId, Part.Part, Part.PartCategoryId, Code,		";
            sql += "		Part.VatRate, Part.MCode, PartCategory.PartCategory, 		";
            sql += "		'<All>', '<All>', VehicleMake.VehicleMake, Rate 		";
            sql += "	FROM 			";
            sql += "		VehicleMake		";
            sql += "		INNER JOIN (PartVehicleMake INNER JOIN ((Part INNER JOIN PartRate ON Part.PartId = PartRate.PartId) INNER JOIN PartCategory 		";
            sql += "			ON Part.PartCategoryId = PartCategory.PartCategoryId) 	";
            sql += "			ON PartVehicleMake.PartId = Part.PartId) 	";
            sql += "			ON VehicleMake.VehicleMakeId = PartVehicleMake.VehicleMakeId 	";
            return sql;
        }
        public System.Data.DataTable Items(String argVehicleMake)
        {
            try
            {
                String sql = "SELECT * FROM (";

                sql += SQLSelectParts();
                sql += ") Alias_1 WHERE Alias_1.VehicleMake = '" + argVehicleMake + "' ";

                DataSet ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Connection, CommandType.Text, sql);
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
                String sql = "SELECT ";
                sql += "		Alias_1.PartId, Alias_1.Part, Alias_1.PartCategoryId, Alias_1.Code,		";
                sql += "		Alias_1.VatRate, Alias_1.MCode, Alias_1.PartCategory, 		";
                sql += "		Alias_1.Model, Alias_1.ModelVersion, Alias_1.VehicleMake, MAX(Alias_1.Rate) AS Rate	";
                sql += "FROM (";
                sql += SQLSelectParts();
                sql += ") Alias_1 WHERE Alias_1.VehicleMake = '" + argVehicleMake + "' ";
                sql += "AND Alias_1.Model = '" + argModel + "' ";
                sql += "GROUP BY Alias_1.PartId, Alias_1.Part, Alias_1.PartCategoryId, Alias_1.Code,		";
                sql += "		Alias_1.VatRate, Alias_1.MCode, Alias_1.PartCategory, 		";
                sql += "		Alias_1.Model, Alias_1.ModelVersion, Alias_1.VehicleMake 		";


                DataSet ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Connection, CommandType.Text, sql);
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
        public System.Data.DataTable Items(String argVehicleMake, String argModel, String argModelVersion)
        {
            try
            {
                String sql = "SELECT * FROM (";

                sql += SQLSelectParts();
                sql += ") Alias_1 WHERE Alias_1.VehicleMake = '" + argVehicleMake + "' ";
                sql += "AND Alias_1.Model = '" + argModel + "' ";
                sql += "AND Alias_1.ModelVersion = '" + argModelVersion + "' ";


                DataSet ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Connection, CommandType.Text, sql);
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
        //        case "PartId":
        //            return this.PartId.ToString();
        //            break;
        //        case "Part":
        //            return this.Default.ToString();
        //            break;
        //        case "PartCategoryId":
        //            return this.PartCategory.PartCategoryId.ToString();
        //            break;
        //        case "VatRate":
        //            return this.mVatRate.ToString();
        //            break;

        //        default:
        //            return null;
        //            break;
        //    }
        //}
        public void Disaffiliate()
        {
            try
            {
                string sql = "";
                sql = "DELETE FROM PartModelVersion WHERE PartId = " + this.PartId;
                SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, sql);
                sql = "DELETE FROM PartModel WHERE PartId = " + this.PartId;
                SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, sql);
                sql = "DELETE FROM PartVehicleMake WHERE PartId = " + this.PartId;
                SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, sql);
            }
            catch (Exception ex)
            {
                //OSN.Generic.EventLogHelper appLog = new OSN.Generic.EventLogHelper();
                //appLog.Source = "Communique";
                //StringBuilder sb = new StringBuilder();
                //sb.AppendLine("public void Disaffiliate()");
                //sb.AppendLine("Exception=" + ex.Message);
                //appLog.WriteEntry(sb.ToString());
            }
        }
        public PartCategory GetPartCategory(InsuranceCompany argCompany)
        {
            foreach (PartCategoryCompany var in this.PartCategoryCompanies.Values)
            {
                if (argCompany.InsuranceCompanyId == var.InsuranceCompany.InsuranceCompanyId)
                {
                    return var.PartCategory;
                }
            }
            return this.PartCategory;
        }

    }
}
