using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class Vehicle : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mVehicleId;
        private System.String mRegistrationNo;
        private System.DateTime mDtRegistration;
        private System.DateTime mDtPurchase;
        private System.String mRegisteredOwner;
        private ModelVersion mModelVersion;
        private System.String mTypeOfBody;
        private System.String mClassOfVehice;
        private System.String mPreAccidentCondition;
        private System.Single mRegisteredLadenWeight;
        private System.Single mUnladenWeight;
        private System.String mFitnessCertificateNo;
        private System.DateTime mDtValidFitnessCertificate;
        private System.DateTime mDtTaxPaidUpto;
        private System.String mRoutePermitNo;
        private System.DateTime mDtValidRoutePermit;
        private System.String mTypeOfPermit;
        private System.String mRouteArea;
        private System.String mStatusRC;
        private PaintType mPaintType;
        private System.String mChassisNo;
        private System.String mIDV;
        private System.String mDtRegistrationRemarks;
        private System.Boolean mDoPrintDOP;

        public System.Boolean DoPrintDOP
        {
            get { return mDoPrintDOP; }
            set { mDoPrintDOP = value; }
        }

        public System.String DtRegistrationRemarks
        {
            get { return mDtRegistrationRemarks; }
            set { mDtRegistrationRemarks = value; }
        }

        public System.String IDV
        {
            get { return mIDV; }
            set { mIDV = value; }
        }

        public System.String ChassisNo
        {
            get { return mChassisNo; }
            set { mChassisNo = value; }
        }
        private System.String mEngineNo;

        public System.String EngineNo
        {
            get { return mEngineNo; }
            set { mEngineNo = value; }
        }

        public PaintType PaintType
        {
            get
            {
                if (mPaintType == null)
                {
                    mPaintType = new PaintType(Connection);
                }
                return mPaintType;
            }
            set { mPaintType = value; }
        }

        public System.String StatusRC
        {
            get { return mStatusRC; }
            set { mStatusRC = value; }
        }

        public Vehicle(String argConnection)
        {
            mConnection = argConnection;
        }
        public Vehicle(String argConnection, Int32 argVehicleId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM Vehicle WHERE VehicleId=" + argVehicleId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mVehicleId = (System.Int32)dr["VehicleId"];
                mRegistrationNo = (System.String)dr["RegistrationNo"];
                mDtRegistration = (System.DateTime)dr["DtRegistration"];
                mDtPurchase = (System.DateTime)dr["DtPurchase"];
                mRegisteredOwner = (System.String)dr["RegisteredOwner"];
                mModelVersion = new ModelVersion(Connection, (int)dr["ModelVersionId"]);
                mTypeOfBody = (System.String)dr["TypeOfBody"];
                mClassOfVehice = (System.String)dr["ClassOfVehice"];
                mPreAccidentCondition = (System.String)dr["PreAccidentCondition"];
                mRegisteredLadenWeight = (System.Single)dr["RegisteredLadenWeight"];
                mUnladenWeight = (System.Single)dr["UnladenWeight"];
                mFitnessCertificateNo = (System.String)dr["FitnessCertificateNo"];
                mDtValidFitnessCertificate = (System.DateTime)dr["DtValidFitnessCertificate"];
                mDtTaxPaidUpto = (System.DateTime)dr["DtTaxPaidUpto"];
                mRoutePermitNo = (System.String)dr["RoutePermitNo"];
                mDtValidRoutePermit = (System.DateTime)dr["DtValidRoutePermit"];
                mTypeOfPermit = (System.String)dr["TypeOfPermit"];
                mRouteArea = (System.String)dr["RouteArea"];
                mStatusRC = (System.String)dr["StatusRC"];
                mPaintType = new PaintType(Connection, (System.Int32)dr["PaintTypeId"]);
                mChassisNo = dr["ChassisNo"].ToString();
                mEngineNo = dr["EngineNo"].ToString();
                mIDV = dr["IDV"].ToString();
                mDtRegistrationRemarks = dr["DtRegistrationRemarks"].ToString();
                mDoPrintDOP = dr.GetBoolean(dr.GetOrdinal("DoPrintDOP"));

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "Vehicle.Vehicle()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 VehicleId
        {
            get
            {
                return mVehicleId;
            }
            set
            {
                mVehicleId = value;
            }
        }
        public System.String RegistrationNo
        {
            get
            {
                return mRegistrationNo;
            }
            set
            {
                mRegistrationNo = value;
            }
        }
        public System.DateTime DtRegistration
        {
            get
            {
                if (mDtRegistration == DateTime.MinValue)
                {
                    Environment.Variables vars = new Environment.Variables(Connection);
                    mDtRegistration = DateTime.Parse(vars["MinimumDate"].Value);
                }
                return mDtRegistration;
            }
            set
            {
                mDtRegistration = value;
            }
        }
        public System.DateTime DtPurchase
        {
            get
            {
                if (mDtPurchase == DateTime.MinValue)
                {
                    Environment.Variables vars = new Environment.Variables(Connection);
                    mDtPurchase = DateTime.Parse(vars["MinimumDate"].Value);
                }
                return mDtPurchase;
            }
            set
            {
                mDtPurchase = value;
            }
        }
        public System.String RegisteredOwner
        {
            get
            {
                return mRegisteredOwner;
            }
            set
            {
                mRegisteredOwner = value;
            }
        }
        public ModelVersion ModelVersion
        {
            get
            {
                if (mModelVersion == null)
                {
                    mModelVersion = new ModelVersion(Connection);
                }
                return mModelVersion;
            }
            set
            {
                mModelVersion = value;
            }
        }
        public System.String TypeOfBody
        {
            get
            {
                return mTypeOfBody;
            }
            set
            {
                mTypeOfBody = value;
            }
        }
        public System.String ClassOfVehice
        {
            get
            {
                return mClassOfVehice;
            }
            set
            {
                mClassOfVehice = value;
            }
        }
        public System.String PreAccidentCondition
        {
            get
            {
                return mPreAccidentCondition;
            }
            set
            {
                mPreAccidentCondition = value;
            }
        }
        public System.Single RegisteredLadenWeight
        {
            get
            {
                return mRegisteredLadenWeight;
            }
            set
            {
                mRegisteredLadenWeight = value;
            }
        }
        public System.Single UnladenWeight
        {
            get
            {
                return mUnladenWeight;
            }
            set
            {
                mUnladenWeight = value;
            }
        }
        public System.String FitnessCertificateNo
        {
            get
            {
                return mFitnessCertificateNo;
            }
            set
            {
                mFitnessCertificateNo = value;
            }
        }
        public System.DateTime DtValidFitnessCertificate
        {
            get
            {
                if (mDtValidFitnessCertificate == DateTime.MinValue)
                {
                    Environment.Variables vars = new Environment.Variables(Connection);
                    mDtValidFitnessCertificate = DateTime.Parse(vars["MinimumDate"].Value);
                }
                return mDtValidFitnessCertificate;
            }
            set
            {
                mDtValidFitnessCertificate = value;
            }
        }
        public System.DateTime DtTaxPaidUpto
        {
            get
            {
                if (mDtTaxPaidUpto == DateTime.MinValue)
                {
                    Environment.Variables vars = new Environment.Variables(Connection);
                    mDtTaxPaidUpto = DateTime.Parse(vars["MinimumDate"].Value);
                }
                return mDtTaxPaidUpto;
            }
            set
            {
                mDtTaxPaidUpto = value;
            }
        }
        public System.String RoutePermitNo
        {
            get
            {
                return mRoutePermitNo;
            }
            set
            {
                mRoutePermitNo = value;
            }
        }
        public System.DateTime DtValidRoutePermit
        {
            get
            {
                if (mDtValidRoutePermit == DateTime.MinValue)
                {
                    Environment.Variables vars = new Environment.Variables(Connection);
                    mDtValidRoutePermit = DateTime.Parse(vars["MinimumDate"].Value);
                }
                return mDtValidRoutePermit;
            }
            set
            {
                mDtValidRoutePermit = value;
            }
        }
        public System.String TypeOfPermit
        {
            get
            {
                return mTypeOfPermit;
            }
            set
            {
                mTypeOfPermit = value;
            }
        }
        public System.String RouteArea
        {
            get
            {
                return mRouteArea;
            }
            set
            {
                mRouteArea = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "Vehicle", "VehicleId");
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
                    mVehicleId = this.Identity.New();
                }

                if (PaintType.PaintTypeId == 0)
                {
                    PaintType = new PaintType(Connection, -1);
                }
                SqlParameter[] parrSP = new SqlParameter[26];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@VehicleId", VehicleId);
                parrSP[2] = new SqlParameter("@RegistrationNo", RegistrationNo);
                parrSP[3] = new SqlParameter("@DtRegistration", DtRegistration);
                parrSP[4] = new SqlParameter("@DtPurchase", DtPurchase);
                parrSP[5] = new SqlParameter("@RegisteredOwner", RegisteredOwner);
                parrSP[6] = new SqlParameter("@ModelVersionId", ModelVersion.ModelVersionId);
                parrSP[7] = new SqlParameter("@TypeOfBody", TypeOfBody);
                parrSP[8] = new SqlParameter("@ClassOfVehice", ClassOfVehice);
                parrSP[9] = new SqlParameter("@PreAccidentCondition", PreAccidentCondition);
                parrSP[10] = new SqlParameter("@RegisteredLadenWeight", RegisteredLadenWeight);
                parrSP[11] = new SqlParameter("@UnladenWeight", UnladenWeight);
                parrSP[12] = new SqlParameter("@FitnessCertificateNo", FitnessCertificateNo);
                parrSP[13] = new SqlParameter("@DtValidFitnessCertificate", DtValidFitnessCertificate);
                parrSP[14] = new SqlParameter("@DtTaxPaidUpto", DtTaxPaidUpto);
                parrSP[15] = new SqlParameter("@RoutePermitNo", RoutePermitNo);
                parrSP[16] = new SqlParameter("@DtValidRoutePermit", DtValidRoutePermit);
                parrSP[17] = new SqlParameter("@TypeOfPermit", TypeOfPermit);
                parrSP[18] = new SqlParameter("@RouteArea", RouteArea);
                parrSP[19] = new SqlParameter("@StatusRC", StatusRC);
                parrSP[20] = new SqlParameter("@PaintTypeId", PaintType.PaintTypeId);
                parrSP[21] = new SqlParameter("@ChassisNo", ChassisNo);
                parrSP[22] = new SqlParameter("@EngineNo", EngineNo);
                parrSP[23] = new SqlParameter("@IDV", IDV);
                parrSP[24] = new SqlParameter("@DtRegistrationRemarks", DtRegistrationRemarks);
                parrSP[25] = new SqlParameter("@DoPrintDOP", DoPrintDOP);

                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspVehicle", parrSP);
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
                return ex.ToString();
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
                SqlParameter[] parrSP = new SqlParameter[26];
                enumDBTransaction pTransactionType = enumDBTransaction.spSelect;
                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@VehicleId", VehicleId);
                parrSP[2] = new SqlParameter("@RegistrationNo", RegistrationNo);
                parrSP[3] = new SqlParameter("@DtRegistration", DtRegistration);
                parrSP[4] = new SqlParameter("@DtPurchase", DtPurchase);
                parrSP[5] = new SqlParameter("@RegisteredOwner", RegisteredOwner);
                parrSP[6] = new SqlParameter("@ModelVersionId", ModelVersion.ModelVersionId);
                parrSP[7] = new SqlParameter("@TypeOfBody", TypeOfBody);
                parrSP[8] = new SqlParameter("@ClassOfVehice", ClassOfVehice);
                parrSP[9] = new SqlParameter("@PreAccidentCondition", PreAccidentCondition);
                parrSP[10] = new SqlParameter("@RegisteredLadenWeight", RegisteredLadenWeight);
                parrSP[11] = new SqlParameter("@UnladenWeight", UnladenWeight);
                parrSP[12] = new SqlParameter("@FitnessCertificateNo", FitnessCertificateNo);
                parrSP[13] = new SqlParameter("@DtValidFitnessCertificate", DtValidFitnessCertificate);
                parrSP[14] = new SqlParameter("@DtTaxPaidUpto", DtTaxPaidUpto);
                parrSP[15] = new SqlParameter("@RoutePermitNo", RoutePermitNo);
                parrSP[16] = new SqlParameter("@DtValidRoutePermit", DtValidRoutePermit);
                parrSP[17] = new SqlParameter("@TypeOfPermit", TypeOfPermit);
                parrSP[18] = new SqlParameter("@RouteArea", RouteArea);
                parrSP[19] = new SqlParameter("@StatusRC", StatusRC);
                parrSP[20] = new SqlParameter("@PaintTypeId", PaintType.PaintTypeId);
                parrSP[21] = new SqlParameter("@ChassisNo", ChassisNo);
                parrSP[22] = new SqlParameter("@EngineNo", EngineNo);
                parrSP[23] = new SqlParameter("@IDV", IDV);
                parrSP[24] = new SqlParameter("@DtRegistrationRemarks", DtRegistrationRemarks);
                parrSP[25] = new SqlParameter("@DoPrintDOP", DoPrintDOP);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspVehicle", parrSP);
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
            return this.VehicleId.ToString();
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "VehicleId":
        //            return this.VehicleId.ToString();
        //            break;
        //        case "RegistrationNo":
        //            return this.RegistrationNo.ToString();
        //            break;
        //        case "DtRegistration":
        //            return this.DtRegistration.ToString();
        //            break;
        //        case "DtPurchase":
        //            return this.DtPurchase.ToString();
        //            break;
        //        case "RegisteredOwner":
        //            return this.RegisteredOwner.ToString();
        //            break;
        //        case "ModelVersionId":
        //            return this.ModelVersion.ModelVersionId.ToString();
        //            break;
        //        case "TypeOfBody":
        //            return this.TypeOfBody.ToString();
        //            break;
        //        case "ClassOfVehice":
        //            return this.ClassOfVehice.ToString();
        //            break;
        //        case "PreAccidentCondition":
        //            return this.PreAccidentCondition.ToString();
        //            break;
        //        case "RegisteredLadenWeight":
        //            return this.RegisteredLadenWeight.ToString();
        //            break;
        //        case "UnladenWeight":
        //            return this.UnladenWeight.ToString();
        //            break;
        //        case "FitnessCertificateNo":
        //            return this.FitnessCertificateNo.ToString();
        //            break;
        //        case "DtValidFitnessCertificate":
        //            return this.DtValidFitnessCertificate.ToString();
        //            break;
        //        case "DtTaxPaidUpto":
        //            return this.DtTaxPaidUpto.ToString();
        //            break;
        //        case "RoutePermitNo":
        //            return this.RoutePermitNo.ToString();
        //            break;
        //        case "DtValidRoutePermit":
        //            return this.DtValidRoutePermit.ToString();
        //            break;
        //        case "TypeOfPermit":
        //            return this.TypeOfPermit.ToString();
        //            break;
        //        case "RouteArea":
        //            return this.RouteArea.ToString();
        //            break;
        //        case "StatusRC":
        //            return this.StatusRC.ToString();
        //            break;

        //        default:
        //            return null;
        //            break;
        //    }
        //}

    }
}
