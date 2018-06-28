using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class Assessment : IDBObject
    {
        public enum enumAssessmentState
        {
            Active = 0x01,
            Cleared = 0x02,
            Closed = 0x04,
        }
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mAssessmentId;
        private System.String mAssessmentKey;
        private Office mOffice;
        private AssessmentType mAssessmentType;
        private Workshop mWorkshop;
        private System.String mPolicyNo;
        private System.String mInsured;
        private System.String mHPA;
        private System.Int16 mState;
        private Vehicle mVehicle;
        private Driver mDriver;
        private Accident mAccident;
        private LoadChallanDetail mLoadChallanDetail;
        private FIR mFIR;
        private ThirdPartyLoss mThirdPartyLoss;
        private Injuries mInjuries;
        private CauseNature mCauseNature;
        private Particulars mParticulars;
        private System.String mPaymentTo;
        private Assemblies mAssemblies;
        private System.Decimal mSalvage;
        private System.Double mExcess;
        private System.Double mNetAssessedLoss;
        private System.DateTime mDtPolicyFrom;
        private System.DateTime mDtPolicyTill;
        private Enclosure mEnclosure;
        private bool mIsServiceTaxApcbl;
        private System.DateTime mDtAssessment;
        private string mEndorsement;
        private System.String mSubject;
        private System.Double mVATRate;
        private System.String mInsuredPhone;
        private Reinspection mReinspection;
        private System.Boolean mIsBifurcatedAssessment;
        private Bill mBill;
        private System.Double mTotalClaimed;
        private AssessmentChecklists mAssessmentChecklists;
        private System.Double mTowingCharges;
        private System.String mRemarks;
        private System.String mRemarksVAT;
        private System.String mDeputingOffice;
        private System.Int32 mBillingOfficeId;
        private System.Int32 mUnderWritingOfficeId;
        private System.Int32 mDeputingOfficeId;
        private System.Int32 mBankAccountId;
        private BankAccount mBankAccount;
        private System.String mGSTIn;


        public System.String DeputingOffice
        {
            get { return mDeputingOffice; }
            set { mDeputingOffice = value; }
        }

        public System.String GSTIn
        {
            get { return mGSTIn; }
            set { mGSTIn = value; }
        }

        private System.String mDeputingOfficeAddress;

        public System.String DeputingOfficeAddress
        {
            get { return mDeputingOfficeAddress; }
            set { mDeputingOfficeAddress = value; }
        }

        private LabourEstimates mLabourEstimates;
                
        public LabourEstimates LabourEstimates
        {
            get
            {
                if (mLabourEstimates == null)
                {
                    mLabourEstimates = new LabourEstimates();
                    LabourEstimate pLabourEstimate = null;

                    string sql = "";
                    sql += "SELECT AssessmentLabourId, EstimateId, WorkshopId ";
                    sql += "FROM AssessmentLabour ";
                    sql += "WHERE AssessmentId = " + this.mAssessmentId + " ";
                    sql += "ORDER BY WorkshopId, EstimateId, AssessmentLabour.ClaimId, AssessmentLabour.AssessmentLabourId ";
                    SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, sql);
                    while (dr.Read())
                    {
                        if (pLabourEstimate == null)
                        {
                            pLabourEstimate = new LabourEstimate
                                (new Estimate(Connection, dr.GetInt32(dr.GetOrdinal("EstimateId")))
                                , new Workshop(Connection, dr.GetInt32(dr.GetOrdinal("WorkshopId")))
                                , new AssessmentLabours());
                            AssessmentLabour fss = new AssessmentLabour(Connection, (int)dr["AssessmentLabourId"]);
                            pLabourEstimate.AssessmentLabours.Add(fss, dr["AssessmentLabourId"].ToString());
                        }
                        else
                        {
                            if (pLabourEstimate.Estimate.EstimateId != dr.GetInt32(dr.GetOrdinal("EstimateId")))
                            {
                                if (pLabourEstimate.AssessmentLabours.Count > 0)
                                {
                                    mLabourEstimates.Add(pLabourEstimate);
                                }
                                pLabourEstimate = new LabourEstimate
                               (new Estimate(Connection, dr.GetInt32(dr.GetOrdinal("EstimateId")))
                                , new Workshop(Connection, dr.GetInt32(dr.GetOrdinal("WorkshopId")))
                                , new AssessmentLabours());
                                AssessmentLabour fss = new AssessmentLabour(Connection, (int)dr["AssessmentLabourId"]);
                                pLabourEstimate.AssessmentLabours.Add(fss, dr["AssessmentLabourId"].ToString());
                            }
                            else
                            {
                                AssessmentLabour fss = new AssessmentLabour(Connection, (int)dr["AssessmentLabourId"]);
                                pLabourEstimate.AssessmentLabours.Add(fss, dr["AssessmentLabourId"].ToString());
                            }
                        }
                    }
                    if (pLabourEstimate != null)
                    {
                        if (pLabourEstimate.AssessmentLabours.Count > 0)
                        {
                            mLabourEstimates.Add(pLabourEstimate);
                        }
                    }

                    dr.Close();
                }
                return mLabourEstimates;
            }
            set { mLabourEstimates = value; }
        }

        private PartEstimates mPartEstimates;

        public PartEstimates PartEstimates
        {
            get
            {
                if (mPartEstimates == null)
                {
                    mPartEstimates = new PartEstimates();
                    PartEstimate pPartEstimate = null;

                    string sql = "";
                    sql += "SELECT AssessmentPartId, EstimateId, WorkshopId ";
                    sql += "FROM AssessmentPart ";
                    sql += "WHERE AssessmentId = " + this.mAssessmentId + " ";
                    sql += "ORDER BY WorkshopId, EstimateId, Convert(int, AssessmentPart.SrNo)";
                    SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, sql);
                    while (dr.Read())
                    {
                        if (pPartEstimate == null)
                        {
                            pPartEstimate = new PartEstimate
                                (new Estimate(Connection, dr.GetInt32(dr.GetOrdinal("EstimateId")))
                                , new Workshop(Connection, dr.GetInt32(dr.GetOrdinal("WorkshopId")))
                                , new AssessmentParts());
                            AssessmentPart fss = new AssessmentPart(Connection, (int)dr["AssessmentPartId"]);
                            pPartEstimate.AssessmentParts.Add(fss, dr["AssessmentPartId"].ToString());
                        }
                        else
                        {
                            if (pPartEstimate.Estimate.EstimateId != dr.GetInt32(dr.GetOrdinal("EstimateId")))
                            {
                                if (pPartEstimate.AssessmentParts.Count > 0)
                                {
                                    mPartEstimates.Add(pPartEstimate);
                                }
                                pPartEstimate = new PartEstimate
                               (new Estimate(Connection, dr.GetInt32(dr.GetOrdinal("EstimateId")))
                                , new Workshop(Connection, dr.GetInt32(dr.GetOrdinal("WorkshopId")))
                                , new AssessmentParts());
                                AssessmentPart fss = new AssessmentPart(Connection, (int)dr["AssessmentPartId"]);
                                pPartEstimate.AssessmentParts.Add(fss, dr["AssessmentPartId"].ToString());
                            }
                            else
                            {
                                AssessmentPart fss = new AssessmentPart(Connection, (int)dr["AssessmentPartId"]);
                                pPartEstimate.AssessmentParts.Add(fss, dr["AssessmentPartId"].ToString());
                            }
                        }
                    }
                    if (pPartEstimate != null)
                    {
                        if (pPartEstimate.AssessmentParts.Count > 0)
                        {
                            mPartEstimates.Add(pPartEstimate);
                        }
                    }
                    dr.Close();
                }
                return mPartEstimates;
            }
            set { mPartEstimates = value; }
        }

        public System.String RemarksVAT
        {
            get { return mRemarksVAT; }
            set { mRemarksVAT = value; }
        }
        private System.String mRemarksDepreciation;

        public System.String RemarksDepreciation
        {
            get { return mRemarksDepreciation; }
            set { mRemarksDepreciation = value; }
        }

        public System.String Remarks
        {
            get { return mRemarks; }
            set { mRemarks = value; }
        }

        public System.Double TowingCharges
        {
            get { return mTowingCharges; }
            set { mTowingCharges = value; }
        }
        private System.String mInsuredAddress1;

        public System.String InsuredAddress1
        {
            get { return mInsuredAddress1; }
            set { mInsuredAddress1 = value; }
        }
        private System.String mInsuredAddress2;

        public System.String InsuredAddress2
        {
            get { return mInsuredAddress2; }
            set { mInsuredAddress2 = value; }
        }

        public AssessmentChecklists AssessmentChecklists
        {
            get
            {
                if (mAssessmentChecklists == null)
                {
                    mAssessmentChecklists = new AssessmentChecklists();
                    System.String sql = System.String.Format(
                        "SELECT * FROM AssessmentChecklist WHERE AssessmentChecklist.AssessmentId = {0}"
                        , this.AssessmentId);
                    SqlDataReader sdr =
                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(this.Connection, CommandType.Text, sql);
                    while (sdr.Read())
                    {
                        AssessmentChecklist acl =
                            new AssessmentChecklist(this.Connection,
                            sdr.GetInt32(sdr.GetOrdinal("AssessmentChecklistId")));
                        mAssessmentChecklists.Add(acl);
                    }
                    sdr.Close();
                }
                return mAssessmentChecklists;
            }
            set
            {
                mAssessmentChecklists = value;
            }
        }

        public System.Double TotalClaimed
        {
            get
            {
                System.Double dblLbr =
                    this.LabourEstimates.NetClaimed;
                System.Double dblPrt =
                    this.PartEstimates.NetClaimed;
                System.Double pSrvTax = dblLbr * ServiceTaxRate / 100;
                mTotalClaimed = Math.Round(dblLbr + pSrvTax + dblPrt - this.Excess, 2);
                return mTotalClaimed;
            }
            set { mTotalClaimed = value; }
        }

        public Bill Bill
        {
            get
            {
                if (mBill == null)
                {
                    System.String sql = "SELECT BillId FROM Bill WHERE Bill.AssessmentId = " + this.AssessmentId;
                    SqlDataReader dr = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Connection, CommandType.Text, sql);
                    if (dr.Read())
                    {
                        System.Int32 billId = dr.GetInt32(dr.GetOrdinal("BillId"));
                        mBill = new Bill(Connection, billId);
                    }
                    dr.Close();
                }
                return mBill;
            }
        }

        public System.Boolean IsBifurcatedAssessment
        {
            get { return mIsBifurcatedAssessment; }
            set { mIsBifurcatedAssessment = value; }
        }

        private System.Int32 mReinspectionId;
        public Reinspection Reinspection
        {
            get
            {
                if (mReinspection == null)
                {
                    if (mReinspectionId > 0)
                    {
                        mReinspection = new Reinspection(Connection, mReinspectionId);
                    }
                    else
                    {
                        mReinspection = new Reinspection(Connection);
                    }
                }
                return mReinspection;
            }
            set { mReinspection = value; }
        }

        public System.String InsuredPhone
        {
            get { return mInsuredPhone; }
            set { mInsuredPhone = value; }
        }

        public System.Double VATRate
        {
            get { return mVATRate; }
            set { mVATRate = value; }
        }
        private System.Double mVAT;

        public System.Double VAT
        {
            get
            {
                float pVAT = 0;
                foreach (PartEstimate estimate in this.PartEstimates.Values)
                {
                    foreach (AssessmentPart assessment in estimate.AssessmentParts.Values)
                    {
                        pVAT = (pVAT + assessment.VAT);
                    }
                }
                mVAT = pVAT;
                return mVAT;
            }
            set { mVAT = value; }
        }
        private System.Double mDepreciation;

        public System.Double Depreciation
        {
            get
            {
                System.Double pDepreciation = 0;
                foreach (PartEstimate estimate in this.PartEstimates.Values)
                {
                    foreach (AssessmentPart assessment in estimate.AssessmentParts.Values)
                    {
                        pDepreciation += assessment.Depreciation;
                    }
                }
                mDepreciation = pDepreciation;
                return mDepreciation;
            }
            set { mDepreciation = value; }
        }
        private System.Double mServiceTaxRate;

        public System.Double ServiceTaxRate
        {
            get { return mServiceTaxRate; }
            set { mServiceTaxRate = value; }
        }
        private System.Double mServiceTax;

        public System.Double ServiceTax
        {
            get
            {
                System.Double pServiceTax = 0;
                foreach (LabourEstimate estimate in this.LabourEstimates.Values)
                {
                    foreach (AssessmentLabour assessment in estimate.AssessmentLabours.Values)
                    {
                        pServiceTax += assessment.NetAssessed * ServiceTaxRate / 100;
                    }
                }
                mServiceTax = pServiceTax;
                return mServiceTax;
            }
            set { mServiceTax = value; }
        }

        public System.String Subject
        {
            get { return mSubject; }
            set { mSubject = value; }
        }


        public string Endorsement
        {
            get { return mEndorsement; }
            set { mEndorsement = value; }
        }


        public System.DateTime DtAssessment
        {
            get
            {
                if (mDtAssessment == DateTime.MinValue)
                {
                    Environment.Variables vars = new Environment.Variables(Connection);
                    mDtAssessment = DateTime.Parse(vars["MinimumDate"].Value);
                }
                return mDtAssessment;
            }
            set { mDtAssessment = value; }
        }

        public bool IsServiceTaxApcbl
        {
            get { return mIsServiceTaxApcbl; }
            set { mIsServiceTaxApcbl = value; }
        }

        private LogicalValidations mLogicalValidations;

        public LogicalValidations LogicalValidations
        {
            get
            {
                if (mLogicalValidations == null)
                {
                    mLogicalValidations = new LogicalValidations();
                }
                return mLogicalValidations;
            }
            set { mLogicalValidations = value; }
        }
        private System.String mClaimNo;

        public System.String ClaimNo
        {
            get { return mClaimNo; }
            set { mClaimNo = value; }
        }

        private System.Int32 mEnclosureId;
        public Enclosure Enclosure
        {
            get
            {
                if (mEnclosure == null)
                {
                    if (mEnclosureId > 0)
                    {
                        mEnclosure =
                            new Enclosure(Connection, mEnclosureId);
                    }
                    else
                    {
                        mEnclosure = new Enclosure(Connection);
                    }
                }
                return mEnclosure;
            }
            set { mEnclosure = value; }
        }

        public Assemblies Assemblies
        {
            get
            {
                if (mAssemblies == null)
                {
                    mAssemblies = new Assemblies();
                    string sql = "";
                    sql += "SELECT AssemblyId ";
                    sql += "FROM Assembly ";
                    sql += "WHERE AssessmentId = " + this.mAssessmentId + " ";
                    SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, sql);
                    while (dr.Read())
                    {
                        Assembly fss = new Assembly(Connection, (int)dr["AssemblyId"]);
                        mAssemblies.Add(fss, dr["AssemblyId"].ToString());
                    }
                    dr.Close();
                }
                return mAssemblies;
            }
            set { mAssemblies = value; }
        }

        private AssessmentParts mAssessmentParts;
        public AssessmentParts AssessmentParts
        {
            get
            {
                if (mAssessmentParts == null)
                {
                    mAssessmentParts = new AssessmentParts();
                    string sql = "";
                    sql += "SELECT AssessmentPartId ";
                    sql += "FROM AssessmentPart ";
                    sql += "WHERE AssessmentId = " + this.mAssessmentId + " ";
                    sql += "ORDER BY Convert(int, AssessmentPart.SrNo)";
                    SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, sql);
                    while (dr.Read())
                    {
                        AssessmentPart fss = new AssessmentPart(Connection, (int)dr["AssessmentPartId"]);
                        mAssessmentParts.Add(fss, dr["AssessmentPartId"].ToString());
                    }
                    dr.Close();
                }

                return mAssessmentParts;

            }
            set { mAssessmentParts = value; }
        }
        private AssessmentLabours mAssessmentLabours;

        public AssessmentLabours AssessmentLabours
        {
            get
            {
                if (mAssessmentLabours == null)
                {
                    mAssessmentLabours = new AssessmentLabours();
                    string sql = "";
                    sql += "SELECT AssessmentLabourId ";
                    sql += "FROM AssessmentLabour ";
                    sql += "WHERE AssessmentId = " + this.mAssessmentId + " ";
                    sql += "ORDER BY AssessmentLabour.ClaimId, AssessmentLabour.AssessmentLabourId ";
                    SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, sql);
                    while (dr.Read())
                    {
                        AssessmentLabour fss = new AssessmentLabour(Connection, (int)dr["AssessmentLabourId"]);
                        mAssessmentLabours.Add(fss, dr["AssessmentLabourId"].ToString());
                    }
                    dr.Close();
                }
                return mAssessmentLabours;
            }
            set { mAssessmentLabours = value; }
        }


        public Assessment(String argConnection)
        {
            mConnection = argConnection;
            this.LogicalValidations.Add(
                new LogicalValidation("Liscence was invalid as on date of loss.", "licenseValidation"));
            this.LogicalValidations.Add(
                new LogicalValidation("Liscence type not valid for the kind of vehicle selected.", "licenseVehicleValidation"));

        }
        public Assessment(String argConnection, Int32 argAssessmentId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM Assessment WHERE AssessmentId=" + argAssessmentId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mAssessmentId = (System.Int32)dr["AssessmentId"];
                mAssessmentKey = (System.String)dr["AssessmentKey"];
                mOfficeId = (System.Int32)dr["DeputingOfficeId"];
                mAssessmentTypeId = (System.Int32)dr["AssessmentTypeId"];
                mWorkshopId = (System.Int32)dr["WorkshopId"];
                mPolicyNo = (System.String)dr["PolicyNo"];
                mInsured = (System.String)dr["Insured"];
                mInsuredAddress1 = (System.String)dr["InsuredAddress1"];
                mInsuredAddress2 = (System.String)dr["InsuredAddress2"];
                mHPA = (System.String)dr["HPA"];
                mState = (System.Int16)dr["State"];
                mVehicleId = (System.Int32)dr["VehicleId"];
                mDriverId = (System.Int32)dr["DriverId"];
                mAccidentId = (System.Int32)dr["AccidentId"];
                mLoadChallanDetailId = (System.Int32)dr["LoadChallanDetailId"];
                mFIRId = (System.Int32)dr["FIRId"];
                mThirdPartyLossId = (System.Int32)dr["ThirdPartyLossId"];
                mInjuriesId = (System.Int32)dr["InjuriesId"];
                mCauseNatureId = (System.Int32)dr["CauseNatureId"];
                mParticularsId = (System.Int32)dr["ParticularsId"];
                mPaymentTo = (System.String)dr["PaymentTo"];
                mTowingCharges = (System.Double)dr["TowingCharges"];
                mSalvage = (System.Decimal)dr["Salvage"];
                mExcess = (System.Double)dr["Excess"];
                mNetAssessedLoss = (System.Double)dr["NetAssessedLoss"];
                mDtPolicyFrom = (System.DateTime)dr["DtPolicyFrom"];
                mDtPolicyTill = (System.DateTime)dr["DtPolicyTill"];
                mEndorsement = (System.String)dr["Endorsement"];
                mEnclosureId = (System.Int32)dr["EnclosureId"];
                mClaimNo = (System.String)dr["ClaimNo"];
                mIsServiceTaxApcbl = (System.Boolean)dr["IsServiceTaxApcbl"];
                mDtAssessment = (System.DateTime)dr["DtAssessment"];
                mSubject = dr["Subject"].ToString();
                mVATRate = (System.Double)dr["VATRate"];
                mVAT = (System.Double)dr["VAT"];
                mDepreciation = (System.Double)dr["Depreciation"];
                mServiceTaxRate = (System.Double)dr["ServiceTaxRate"];
                mServiceTax = (System.Double)dr["ServiceTax"];
                mUnderWritingOfficeId = (System.Int32)dr[("UnderWritingOfficeId")];
                mBillingOfficeId = (System.Int32)dr[("BillingOfficeId")];
                mGSTIn = (System.String)dr["GSTIn"];
                mBankAccount = new BankAccount(Connection, (int)dr["BankAccountId"]);
                mInsuredPhone = dr["InsuredPhone"].ToString();
                mReinspectionId = (System.Int32)dr["ReinspectionId"];
                mIsBifurcatedAssessment = dr.GetBoolean(dr.GetOrdinal("IsBifurcatedAssessment"));
                mTotalClaimed = dr.GetDouble(dr.GetOrdinal("TotalClaimed"));
                mRemarks = dr.GetString(dr.GetOrdinal("Remarks"));
                mRemarksVAT = dr.GetString(dr.GetOrdinal("RemarksVAT"));
                mRemarksDepreciation = dr.GetString(dr.GetOrdinal("RemarksDepreciation"));
                mDeputingOffice = dr.GetString(dr.GetOrdinal("DeputingOffice"));
                mDeputingOfficeAddress = dr.GetString(dr.GetOrdinal("DeputingOfficeAddress"));

                this.LogicalValidations.Add(
                    new LogicalValidation("Liscence was invalid as on date of loss.", "licenseValidation"));
                this.LogicalValidations.Add(
                    new LogicalValidation("Liscence type not valid for the kind of vehicle selected.", "licenseVehicleValidation"));
            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "Assessment.Assessment()";
                throw ex;
            }
            dr.Close();
        }

        public Assessment(String argConnection, System.String argAssessmentKey)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM Assessment WHERE AssessmentKey='" + argAssessmentKey + "'";
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mAssessmentId = (System.Int32)dr["AssessmentId"];
                mAssessmentKey = (System.String)dr["AssessmentKey"];
                mOfficeId = (System.Int32)dr["DeputingOfficeId"];
                mAssessmentTypeId = (System.Int32)dr["AssessmentTypeId"];
                mWorkshopId = (System.Int32)dr["WorkshopId"];
                mPolicyNo = (System.String)dr["PolicyNo"];
                mInsured = (System.String)dr["Insured"];
                mInsuredAddress1 = (System.String)dr["InsuredAddress1"];
                mInsuredAddress2 = (System.String)dr["InsuredAddress2"];
                mHPA = (System.String)dr["HPA"];
                mState = (System.Int16)dr["State"];
                mVehicleId = (System.Int32)dr["VehicleId"];
                mDriverId = (System.Int32)dr["DriverId"];
                mAccidentId = (System.Int32)dr["AccidentId"];
                mLoadChallanDetailId = (System.Int32)dr["LoadChallanDetailId"];
                mFIRId = (System.Int32)dr["FIRId"];
                mThirdPartyLossId = (System.Int32)dr["ThirdPartyLossId"];
                mInjuriesId = (System.Int32)dr["InjuriesId"];
                mCauseNatureId = (System.Int32)dr["CauseNatureId"];
                mParticularsId = (System.Int32)dr["ParticularsId"];
                mPaymentTo = (System.String)dr["PaymentTo"];
                mTowingCharges = (System.Double)dr["TowingCharges"];
                mSalvage = (System.Decimal)dr["Salvage"];
                mExcess = (System.Double)dr["Excess"];
                mNetAssessedLoss = (System.Double)dr["NetAssessedLoss"];
                mDtPolicyFrom = (System.DateTime)dr["DtPolicyFrom"];
                mDtPolicyTill = (System.DateTime)dr["DtPolicyTill"];
                mEnclosureId = (System.Int32)dr["EnclosureId"];
                mClaimNo = (System.String)dr["ClaimNo"];
                mIsServiceTaxApcbl = (System.Boolean)dr["IsServiceTaxApcbl"];
                mDtAssessment = (System.DateTime)dr["DtAssessment"];
                mSubject = dr["Subject"].ToString();
                mVATRate = (System.Double)dr["VATRate"];
                mVAT = (System.Double)dr["VAT"];
                mDepreciation = (System.Double)dr["Depreciation"];
                mServiceTaxRate = (System.Double)dr["ServiceTaxRate"];
                mServiceTax = (System.Double)dr["ServiceTax"];
                mUnderWritingOfficeId = (System.Int32)dr[("UnderWritingOfficeId")];
                mBillingOfficeId = (System.Int32)dr[("BillingOfficeId")];
                mBankAccountId = (System.Int32)dr[("BankAccountId")];
                mGSTIn = (System.String)dr["GSTIn"];
                mEndorsement = dr["Endorsement"].GetType() == typeof(System.DBNull) ? "" : (System.String)dr["Endorsement"];
                mInsuredPhone = dr["InsuredPhone"].ToString();
                mReinspectionId = (System.Int32)dr["ReinspectionId"];
                mIsBifurcatedAssessment = dr.GetBoolean(dr.GetOrdinal("IsBifurcatedAssessment"));
                mTotalClaimed = dr.GetDouble(dr.GetOrdinal("TotalClaimed"));
                mRemarks = dr.GetString(dr.GetOrdinal("Remarks"));
                mRemarksVAT = dr.GetString(dr.GetOrdinal("RemarksVAT"));
                mRemarksDepreciation = dr.GetString(dr.GetOrdinal("RemarksDepreciation"));
                mDeputingOffice = dr.GetString(dr.GetOrdinal("DeputingOffice"));
                mDeputingOfficeAddress = dr.GetString(dr.GetOrdinal("DeputingOfficeAddress"));

                this.LogicalValidations.Add(
                    new LogicalValidation("License was invalid as on date of loss.", "licenseValidation"));
                this.LogicalValidations.Add(
                    new LogicalValidation("License type not valid for the kind of vehicle selected.", "licenseVehicleValidation"));

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "Assessment.Assessment()";
                throw ex;
            }
            dr.Close();
        }

        public System.Int32 AssessmentId
        {
            get
            {
                return mAssessmentId;
            }
            set
            {
                mAssessmentId = value;
            }
        }
        public System.Int32 UnderWritingOfficeId
        {
            get
            {
                return mUnderWritingOfficeId;
            }
            set
            {
                mUnderWritingOfficeId = value;
            }
        }

        public System.Int32 DeputingOfficeId
        {
            get
            {
                return mDeputingOfficeId;
            }
            set
            {
                mDeputingOfficeId = value;
            }
        }

        public System.Int32 BankAccountId
        {
            get
            {
                return mBankAccountId;
            }
            set
            {
                mBankAccountId = value;
            }
        }


        public System.Int32 BillingOfficeId
        {
            get
            {
                return mBillingOfficeId;
            }
            set
            {
                mBillingOfficeId = value;
            }
        }
        public System.String AssessmentKey
        {
            get
            {
                return mAssessmentKey;
            }
            set
            {
                mAssessmentKey = value;
            }
        }
        private System.Int32 mOfficeId;
        public Office Office
        {
            get
            {
                if (mOffice == null)
                {
                    if (mOfficeId > 0)
                    {
                        mOffice =
                            new Office(Connection, mOfficeId);
                    }
                    else
                    {
                        mOffice = new Office(Connection);
                    }
                }
                return mOffice;
            }
            set
            {
                mOffice = value;
            }
        }

        public BankAccount BankAccount
        {
            get
            {
                if (mBankAccount == null)
                {
                    mBankAccount = new BankAccount(Connection);
                }
                return mBankAccount;
            }
            set
            {
                mBankAccount = value;
            }
        }

        private System.Int32 mAssessmentTypeId;
        public AssessmentType AssessmentType
        {
            get
            {
                if (mAssessmentType == null)
                {
                    if (mAssessmentTypeId > 0)
                    {
                        mAssessmentType =
                            new AssessmentType(Connection, mAssessmentTypeId);
                    }
                    else
                    {
                        mAssessmentType =
                            new AssessmentType(Connection);
                    }
                }
                return mAssessmentType;
            }
            set
            {
                mAssessmentType = value;
            }
        }

        private System.Int32 mWorkshopId;
        public Workshop Workshop
        {
            get
            {
                if (mWorkshop == null)
                {
                    if (mWorkshopId > 0)
                    {
                        mWorkshop = new Workshop(Connection, mWorkshopId);
                    }
                    else
                    {
                        mWorkshop = new Workshop(Connection);
                    }
                }
                return mWorkshop;
            }
            set
            {
                mWorkshop = value;
            }
        }
        public System.String PolicyNo
        {
            get
            {
                return mPolicyNo;
            }
            set
            {
                mPolicyNo = value;
            }
        }
        public System.String Insured
        {
            get
            {
                return mInsured;
            }
            set
            {
                mInsured = value;
            }
        }
        public System.String HPA
        {
            get
            {
                return mHPA;
            }
            set
            {
                mHPA = value;
            }
        }
        public System.Int16 State
        {
            get
            {
                return mState;
            }
            set
            {
                mState = value;
            }
        }

        private System.Int32 mVehicleId;
        public Vehicle Vehicle
        {
            get
            {
                if (mVehicle == null)
                {
                    if (mVehicleId > 0)
                    {
                        mVehicle = new Vehicle(Connection, mVehicleId);
                    }
                    else
                    {
                        mVehicle = new Vehicle(Connection);
                    }
                }
                return mVehicle;
            }
            set
            {
                mVehicle = value;
            }
        }

        private System.Int32 mDriverId;
        public Driver Driver
        {
            get
            {
                if (mDriver == null)
                {
                    if (mDriverId > 0)
                    {
                        mDriver = new Driver(Connection, mDriverId);
                    }
                    else
                    {
                        mDriver = new Driver(Connection);
                    }
                }
                return mDriver;
            }
            set
            {
                mDriver = value;
            }
        }

        private System.Int32 mAccidentId;
        public Accident Accident
        {
            get
            {
                if (mAccident == null)
                {
                    if (mAccidentId > 0)
                    {
                        mAccident = new Accident(Connection, mAccidentId);
                    }
                    else
                    {
                        mAccident = new Accident(Connection);
                    }
                }
                return mAccident;
            }
            set
            {
                mAccident = value;
            }
        }

        private System.Int32 mLoadChallanDetailId;
        public LoadChallanDetail LoadChallanDetail
        {
            get
            {
                if (mLoadChallanDetail == null)
                {
                    if (mLoadChallanDetailId > 0)
                    {
                        mLoadChallanDetail =
                            new LoadChallanDetail(Connection, mLoadChallanDetailId);
                    }
                    else
                    {
                        mLoadChallanDetail =
                            new LoadChallanDetail(Connection);
                    }
                }
                return mLoadChallanDetail;
            }
            set
            {
                mLoadChallanDetail = value;
            }
        }

        private System.Int32 mFIRId;
        public FIR FIR
        {
            get
            {
                if (mFIR == null)
                {
                    if (mFIRId > 0)
                    {
                        mFIR = new FIR(Connection, mFIRId);
                    }
                    else
                    {
                        mFIR = new FIR(Connection);
                    }
                }
                return mFIR;
            }
            set
            {
                mFIR = value;
            }
        }

        private System.Int32 mThirdPartyLossId;
        public ThirdPartyLoss ThirdPartyLoss
        {
            get
            {
                if (mThirdPartyLoss == null)
                {
                    if (mThirdPartyLossId > 0)
                    {
                        mThirdPartyLoss =
                            new ThirdPartyLoss(Connection, mThirdPartyLossId);
                    }
                    else
                    {
                        mThirdPartyLoss =
                            new ThirdPartyLoss(Connection);
                    }
                }
                return mThirdPartyLoss;
            }
            set
            {
                mThirdPartyLoss = value;
            }
        }

        private System.Int32 mInjuriesId;
        public Injuries Injuries
        {
            get
            {
                if (mInjuries == null)
                {
                    if (mInjuriesId > 0)
                    {
                        mInjuries = new Injuries(Connection, mInjuriesId);
                    }
                    else
                    {
                        mInjuries = new Injuries(Connection);
                    }
                }
                return mInjuries;
            }
            set
            {
                mInjuries = value;
            }
        }

        private System.Int32 mCauseNatureId;
        public CauseNature CauseNature
        {
            get
            {
                if (mCauseNature == null)
                {
                    if (mCauseNatureId > 0)
                    {
                        mCauseNature = new CauseNature(Connection, mCauseNatureId);
                    }
                    else
                    {
                        mCauseNature = new CauseNature(Connection);
                    }
                }
                return mCauseNature;
            }
            set
            {
                mCauseNature = value;
            }
        }

        private System.Int32 mParticularsId;
        public Particulars Particulars
        {
            get
            {
                if (mParticulars == null)
                {
                    if (mParticularsId > 0)
                    {
                        mParticulars = new Particulars(Connection, mParticularsId);
                    }
                    else
                    {
                        mParticulars = new Particulars(Connection);
                    }
                }
                return mParticulars;
            }
            set
            {
                mParticulars = value;
            }
        }
        public System.String PaymentTo
        {
            get
            {
                return mPaymentTo;
            }
            set
            {
                mPaymentTo = value;
            }
        }

        public System.Decimal Salvage
        {
            get
            {
                return mSalvage;
            }
            set
            {
                mSalvage = value;
            }
        }
        public System.Double Excess
        {
            get
            {
                return mExcess;
            }
            set
            {
                mExcess = value;
            }
        }
        public System.Double NetAssessedLoss
        {
            get
            {
                System.Double dblLbr =
                    this.LabourEstimates.Netassessed;
                System.Double dblPrt =
                    this.PartEstimates.Netassessed;
                mNetAssessedLoss = Math.Round(dblLbr + this.ServiceTax + dblPrt - this.Excess, 2);
                return mNetAssessedLoss;
            }
            set
            {
                mNetAssessedLoss = value;
            }
        }
        private System.Double mSurveyorFee;

        public System.Double SurveyorFee
        {
            get
            {
                System.Double loss = NetAssessedLoss;
                System.Double claim = TotalClaimed;
                FeeSchedule fs = new FeeSchedule(this.Connection, this.AssessmentType, this.Office, this.DtAssessment);
                mSurveyorFee = fs.GetSurveyorFee(
                    claim,
                    loss);
                return mSurveyorFee;
            }
        }
        private System.Double mSurveyorReinspectionFee;

        public System.Double SurveyorReinspectionFee
        {
            get
            {
                System.Double loss = NetAssessedLoss;
                System.Double claim = TotalClaimed;
                FeeSchedule fs = new FeeSchedule(this.Connection, this.AssessmentType, this.Office, this.DtAssessment);
                mSurveyorReinspectionFee = fs.GetSurveyorReFee(
                    claim,
                    loss);
                return mSurveyorReinspectionFee;
            }
        }

        public System.DateTime DtPolicyFrom
        {
            get
            {
                if (mDtPolicyFrom == DateTime.MinValue)
                {
                    Environment.Variables vars = new Environment.Variables(Connection);
                    mDtPolicyFrom = DateTime.Parse(vars["MinimumDate"].Value);
                }
                return mDtPolicyFrom;
            }
            set
            {
                mDtPolicyFrom = value;
            }
        }
        public System.DateTime DtPolicyTill
        {
            get
            {
                if (mDtPolicyTill == DateTime.MinValue)
                {
                    Environment.Variables vars = new Environment.Variables(Connection);
                    mDtPolicyTill = DateTime.Parse(vars["MinimumDate"].Value);
                }
                return mDtPolicyTill;
            }
            set
            {
                mDtPolicyTill = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "Assessment", "AssessmentId");
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
                    mAssessmentId = this.Identity.New();
                }
                System.String err = ValidateObject();
                if (err.Length > 0)
                {
                    return err;
                }
                LogicalValidation lv;
                lv = this.LogicalValidations["licenseValidation"];
                lv.IsValid = (this.Driver.DtValidLicense > this.Accident.DtAccident);
                lv = this.LogicalValidations["licenseVehicleValidation"];
                lv.IsValid =
                    (this.Driver.IsValidForVehicleCategory(this.Vehicle.ModelVersion.Model.VehicleCategory.VehicleCategoryId));

                this.Accident.Transaction(argTransactionType);
                this.Vehicle.Transaction(argTransactionType);
                this.Driver.Transaction(argTransactionType);
                this.LoadChallanDetail.Transaction(argTransactionType);
                this.FIR.Transaction(argTransactionType);
                this.Particulars.Transaction(argTransactionType);
                this.ThirdPartyLoss.Transaction(argTransactionType);
                this.CauseNature.Transaction(argTransactionType);
                this.Enclosure.Transaction(argTransactionType);
                this.Injuries.Transaction(argTransactionType);
                this.Reinspection.Transaction(argTransactionType);

                SqlParameter[] parrSP = new SqlParameter[53];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@AssessmentId", AssessmentId);
                parrSP[2] = new SqlParameter("@AssessmentKey", AssessmentKey);

                if (DeputingOfficeId != 0)
                    parrSP[3] = new SqlParameter("@OfficeId", DeputingOfficeId);
                parrSP[4] = new SqlParameter("@AssessmentTypeId", AssessmentType.AssessmentTypeId);
                parrSP[5] = new SqlParameter("@WorkshopId", Workshop.WorkshopId);
                parrSP[6] = new SqlParameter("@PolicyNo", PolicyNo);
                parrSP[7] = new SqlParameter("@Insured", Insured);
                parrSP[8] = new SqlParameter("@HPA", HPA);
                parrSP[9] = new SqlParameter("@State", State);
                parrSP[10] = new SqlParameter("@VehicleId", Vehicle.VehicleId);
                parrSP[11] = new SqlParameter("@DriverId", Driver.DriverId);
                parrSP[12] = new SqlParameter("@AccidentId", Accident.AccidentId);
                parrSP[13] = new SqlParameter("@LoadChallanDetailId", LoadChallanDetail.LoadChallanDetailId);
                parrSP[14] = new SqlParameter("@FIRId", FIR.FIRId);
                parrSP[15] = new SqlParameter("@ThirdPartyLossId", ThirdPartyLoss.ThirdPartyLossId);
                parrSP[16] = new SqlParameter("@InjuriesId", Injuries.InjuriesId);
                parrSP[17] = new SqlParameter("@CauseNatureId", CauseNature.CauseNatureId);
                parrSP[18] = new SqlParameter("@ParticularsId", Particulars.ParticularsId);
                parrSP[19] = new SqlParameter("@PaymentTo", PaymentTo);
                parrSP[20] = new SqlParameter("@AssemblyId", 0);
                parrSP[21] = new SqlParameter("@Salvage", Salvage);
                parrSP[22] = new SqlParameter("@Excess", Excess);
                parrSP[23] = new SqlParameter("@NetAssessedLoss", NetAssessedLoss);
                parrSP[24] = new SqlParameter("@DtPolicyFrom", DtPolicyFrom);
                parrSP[25] = new SqlParameter("@DtPolicyTill", DtPolicyTill);
                parrSP[26] = new SqlParameter("@EnclosureId", Enclosure.EnclosureId);
                parrSP[27] = new SqlParameter("@ClaimNo", ClaimNo);
                parrSP[28] = new SqlParameter("@IsServiceTaxApcbl", IsServiceTaxApcbl);
                parrSP[29] = new SqlParameter("@DtAssessment", DtAssessment);
                parrSP[30] = new SqlParameter("@Endorsement", Endorsement);
                parrSP[31] = new SqlParameter("@Subject", Subject);
                parrSP[32] = new SqlParameter("@VATRate", VATRate);
                parrSP[33] = new SqlParameter("@VAT", VAT);
                parrSP[34] = new SqlParameter("@Depreciation", Depreciation);
                parrSP[35] = new SqlParameter("@ServiceTaxRate", ServiceTaxRate);
                parrSP[36] = new SqlParameter("@ServiceTax", ServiceTax);
                parrSP[37] = new SqlParameter("@InsuredPhone", mInsuredPhone);
                parrSP[38] = new SqlParameter("@ReinspectionId", mReinspection.ReinspectionId);
                parrSP[39] = new SqlParameter("@IsBifurcatedAssessment", IsBifurcatedAssessment);
                parrSP[40] = new SqlParameter("@TotalClaimed", TotalClaimed);
                parrSP[41] = new SqlParameter("@TowingCharges", TowingCharges);
                parrSP[42] = new SqlParameter("@InsuredAddress1", InsuredAddress1);
                parrSP[43] = new SqlParameter("@InsuredAddress2", InsuredAddress2);
                parrSP[44] = new SqlParameter("@Remarks", Remarks);
                parrSP[45] = new SqlParameter("@RemarksVAT", RemarksVAT);
                parrSP[46] = new SqlParameter("@RemarksDepreciation", RemarksDepreciation);
                parrSP[47] = new SqlParameter("@DeputingOffice", DeputingOffice);
                parrSP[48] = new SqlParameter("@DeputingOfficeAddress", DeputingOfficeAddress);
                parrSP[49] = new SqlParameter("@UnderWritingOfficeId", UnderWritingOfficeId);
                parrSP[50] = new SqlParameter("@BillingOfficeId", BillingOfficeId);
                parrSP[51] = new SqlParameter("@BankAccountId", BankAccountId);
                parrSP[52] = new SqlParameter("@GSTIn", GSTIn);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspAssessment", parrSP);

                this.PartEstimates.Delete(this.Connection, this.AssessmentId);
                foreach (PartEstimate item in this.PartEstimates.Values)
                {
                    foreach (AssessmentPart aPart in item.AssessmentParts.Values)
                    {
                        aPart.Assessment = this;
                        aPart.Transaction(aPart.TransactionType);
                    }
                }
                this.LabourEstimates.Delete(this.Connection, this.AssessmentId);
                foreach (LabourEstimate item in this.LabourEstimates.Values)
                {
                    foreach (AssessmentLabour aLabour in item.AssessmentLabours.Values)
                    {
                        aLabour.Assessment = this;
                        aLabour.Transaction(pTransactionType);
                    }
                }
                //if (this.AssessmentLabours.IsDirty)
                //{
                //    foreach (AssessmentLabour aLabour in this.AssessmentLabours.Values)
                //    {
                //        aLabour.Assessment = this;
                //        aLabour.Transaction();
                //    }
                //}

                if (this.Assemblies.IsDirty)
                {
                    foreach (Assembly aAssembly in this.Assemblies.Values)
                    {
                        aAssembly.Assessment = this;
                        aAssembly.Transaction(argTransactionType);
                    }
                }
                this.AssessmentChecklists.Save(this);
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
        public System.String ValidateObject()
        {
            StringBuilder sb = new StringBuilder();
            if (this.AssessmentKey.Trim().Length <= 0)
            {
                sb.AppendLine("Empty assessment ref. cannot be saved!");
            }
            return sb.ToString();
        }
        public string NewAssessmentKey()
        {
            string sql = "SELECT MAX(AssessmentKey) AS MaxKey FROM Assessment";
            String str = "";
            System.Data.SqlClient.SqlDataReader sdr = SqlHelper.ExecuteReader(this.Connection, CommandType.Text, sql);
            while (sdr.Read())
            {
                try
                {
                    if (sdr.GetValue(0) != DBNull.Value)
                    {
                        System.Int32 num = System.Int32.Parse(sdr["MaxKey"].ToString());
                        num++;
                        str = num.ToString();
                        str = new string('0', 5 - str.Length) + str;
                    }
                    else
                    {
                        str = "00001";
                    }
                }
                catch (Exception)
                {

                }
            }
            sdr.Close();
            return str;
        }
        public string Transaction(enumDBTransaction argTransactionType, System.Data.SqlClient.SqlTransaction argTransaction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public System.Data.DataTable Items()
        {
            try
            {
                SqlParameter[] parrSP = new SqlParameter[53];
                enumDBTransaction pTransactionType = enumDBTransaction.spSelect;
                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@AssessmentId", AssessmentId);
                parrSP[2] = new SqlParameter("@AssessmentKey", AssessmentKey);
                parrSP[3] = new SqlParameter("@DeputingOfficeId", Office.OfficeId);
                parrSP[4] = new SqlParameter("@AssessmentTypeId", AssessmentType.AssessmentTypeId);
                parrSP[5] = new SqlParameter("@WorkshopId", Workshop.WorkshopId);
                parrSP[6] = new SqlParameter("@PolicyNo", PolicyNo);
                parrSP[7] = new SqlParameter("@Insured", Insured);
                parrSP[8] = new SqlParameter("@HPA", HPA);
                parrSP[9] = new SqlParameter("@State", State);
                parrSP[10] = new SqlParameter("@VehicleId", Vehicle.VehicleId);
                parrSP[11] = new SqlParameter("@DriverId", Driver.DriverId);
                parrSP[12] = new SqlParameter("@AccidentId", Accident.AccidentId);
                parrSP[13] = new SqlParameter("@LoadChallanDetailId", LoadChallanDetail.LoadChallanDetailId);
                parrSP[14] = new SqlParameter("@FIRId", FIR.FIRId);
                parrSP[15] = new SqlParameter("@ThirdPartyLossId", ThirdPartyLoss.ThirdPartyLossId);
                parrSP[16] = new SqlParameter("@InjuriesId", Injuries.InjuriesId);
                parrSP[17] = new SqlParameter("@CauseNatureId", CauseNature.CauseNatureId);
                parrSP[18] = new SqlParameter("@ParticularsId", Particulars.ParticularsId);
                parrSP[19] = new SqlParameter("@PaymentTo", PaymentTo);
                parrSP[20] = new SqlParameter("@AssemblyId", 0);
                parrSP[21] = new SqlParameter("@Salvage", Salvage);
                parrSP[22] = new SqlParameter("@Excess", Excess);
                parrSP[23] = new SqlParameter("@NetAssessedLoss", NetAssessedLoss);
                parrSP[24] = new SqlParameter("@DtPolicyFrom", DtPolicyFrom);
                parrSP[25] = new SqlParameter("@DtPolicyTill", DtPolicyTill);
                parrSP[26] = new SqlParameter("@EnclosureId", Enclosure.EnclosureId);
                parrSP[27] = new SqlParameter("@ClaimNo", ClaimNo);
                parrSP[28] = new SqlParameter("@IsServiceTaxApcbl", IsServiceTaxApcbl);
                parrSP[29] = new SqlParameter("@DtAssessment", DtAssessment);
                parrSP[30] = new SqlParameter("@Endorsement", Endorsement);
                parrSP[31] = new SqlParameter("@Subject", Subject);
                parrSP[32] = new SqlParameter("@VATRate", VATRate);
                parrSP[33] = new SqlParameter("@VAT", VAT);
                parrSP[34] = new SqlParameter("@Depreciation", Depreciation);
                parrSP[35] = new SqlParameter("@ServiceTaxRate", ServiceTaxRate);
                parrSP[36] = new SqlParameter("@ServiceTax", ServiceTax);
                parrSP[37] = new SqlParameter("@InsuredPhone", mInsuredPhone);
                parrSP[38] = new SqlParameter("@ReinspectionId", Reinspection.ReinspectionId);
                parrSP[39] = new SqlParameter("@IsBifurcatedAssessment", IsBifurcatedAssessment);
                parrSP[40] = new SqlParameter("@TotalClaimed", TotalClaimed);
                parrSP[41] = new SqlParameter("@TowingCharges", TowingCharges);
                parrSP[42] = new SqlParameter("@InsuredAddress1", InsuredAddress1);
                parrSP[43] = new SqlParameter("@InsuredAddress2", InsuredAddress2);
                parrSP[44] = new SqlParameter("@Remarks", Remarks);
                parrSP[45] = new SqlParameter("@RemarksVAT", RemarksVAT);
                parrSP[46] = new SqlParameter("@RemarksDepreciation", RemarksDepreciation);
                parrSP[47] = new SqlParameter("@DeputingOffice", DeputingOffice);
                parrSP[48] = new SqlParameter("@DeputingOfficeAddress", DeputingOfficeAddress);
                parrSP[49] = new SqlParameter("@UnderWritingOfficeId", UnderWritingOfficeId);
                parrSP[50] = new SqlParameter("@BillingOfficeId", BillingOfficeId);
                parrSP[51] = new SqlParameter("@BankAccountId", BankAccountId);
                parrSP[52] = new SqlParameter("@GSTIn", GSTIn);

                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspAssessment", parrSP);
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
        public System.Data.DataTable Items(Assessment.enumAssessmentState argState)
        {
            string sql =
                "SELECT Assessment.*, Office.Office, Workshop.Workshop, Office.OfficeCode "
                + "FROM (Assessment INNER JOIN Workshop ON Assessment.WorkshopId = Workshop.WorkshopId) "
                + "INNER JOIN Office ON Assessment.DeputingOfficeId = Office.OfficeId ";
            switch (argState)
            {
                case enumAssessmentState.Active:
                    sql += "WHERE NOT EXISTS (SELECT BillId FROM Bill WHERE Bill.AssessmentId = Assessment.AssessmentId) ";
                    sql += "AND NOT EXISTS (SELECT ReceiptId FROM Receipt WHERE Receipt.AssessmentId = Assessment.AssessmentId) ";
                    break;
                case enumAssessmentState.Cleared:
                    sql += "WHERE EXISTS (SELECT BillId FROM Bill WHERE Bill.AssessmentId = Assessment.AssessmentId) ";
                    sql += "AND NOT EXISTS (SELECT ReceiptId FROM Receipt WHERE Receipt.AssessmentId = Assessment.AssessmentId) ";
                    break;
                case enumAssessmentState.Closed:
                    sql += "WHERE EXISTS (SELECT BillId FROM Bill WHERE Bill.AssessmentId = Assessment.AssessmentId) ";
                    sql += "AND EXISTS (SELECT ReceiptId FROM Receipt WHERE Receipt.AssessmentId = Assessment.AssessmentId) ";
                    break;
                default:
                    break;
            }
            DataSet ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Connection, CommandType.Text, sql);
            return ds.Tables[0];
        }
        public System.Data.DataTable Items(DateTime DtFrom, DateTime DtTill)
        {
            string sql =
                "SELECT Assessment.*, Office.Office, Workshop.Workshop "
                + "FROM (Assessment INNER JOIN Workshop ON Assessment.WorkshopId = Workshop.WorkshopId) "
                + "INNER JOIN Office ON Assessment.DeputingOfficeId = Office.OfficeId "
                + "WHERE DtAssessment BETWEEN '" + ANSI(DtFrom) + "' "
                + "AND '" + ANSI(DtTill) + "'";
            DataSet ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Connection, CommandType.Text, sql);
            return ds.Tables[0];
        }
        public System.Data.DataTable Items(DateTime DtFrom, DateTime DtTill, Workshop wrk)
        {
            string sql =
                "SELECT Assessment.*, Office.Office, Workshop.Workshop "
                + "FROM (Assessment INNER JOIN Workshop ON Assessment.WorkshopId = Workshop.WorkshopId) "
                + "INNER JOIN Office ON Assessment.DeputingOfficeId = Office.OfficeId "
                + "WHERE DtAssessment BETWEEN '" + ANSI(DtFrom) + "' "
                + "AND '" + ANSI(DtTill) + "' AND "
                + "Assessment.WorkshopId = " + wrk.WorkshopId;
            DataSet ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Connection, CommandType.Text, sql);
            return ds.Tables[0];
        }
        public System.Data.DataTable Items(DateTime DtFrom, DateTime DtTill, Office off)
        {
            string sql =
                "SELECT Assessment.*, Office.Office, Workshop.Workshop "
                + "FROM (Assessment INNER JOIN Workshop ON Assessment.WorkshopId = Workshop.WorkshopId) "
                + "INNER JOIN Office ON Assessment.DeputingOfficeId = Office.OfficeId "
                + "WHERE DtAssessment BETWEEN '" + ANSI(DtFrom) + "' "
                + "AND '" + ANSI(DtTill) + "' AND "
                + "Assessment.DeputingOfficeId = " + off.OfficeId;
            DataSet ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Connection, CommandType.Text, sql);
            return ds.Tables[0];
        }
        public System.Data.DataTable Items(DateTime DtFrom, DateTime DtTill, Workshop wrk,
            Office off)
        {
            string sql =
                "SELECT Assessment.*, Office.Office, Workshop.Workshop "
                + "FROM (Assessment INNER JOIN Workshop ON Assessment.WorkshopId = Workshop.WorkshopId) "
                + "INNER JOIN Office ON Assessment.DeputingOfficeId = Office.OfficeId "
                + "WHERE DtAssessment BETWEEN '" + System.String.Format("{0:yyyyMMdd HH:mm}", DtFrom) + "' "
                + "AND '" + System.String.Format("{0:yyyyMMdd HH:mm}", DtTill) + "' AND "
                + "Assessment.DeputingOfficeId = " + off.OfficeId + " AND "
                + "Assessment.WorkshopId = " + wrk.WorkshopId;
            DataSet ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Connection, CommandType.Text, sql);
            return ds.Tables[0];
        }
        public System.Data.DataTable Items(DateTime DtFrom, DateTime DtTill, Workshop wrk,
            Office off, System.Int32 argState, Model argModel,
            AssessmentType argAType)
        {
            string sql =
                "SELECT Assessment.*, Accident.DtInstructions, Office.OfficeCode,Office.Office,Workshop.Workshop, "
                + "Vehicle.RegistrationNo, Bill.BillNo, Bill.DtBill, Receipt.ChequeAmount, ROUND(Bill.GrandTotal,0) AS BillAmount,"
                + "Case WHEN Assessment.BillingOfficeId = Assessment.DeputingOfficeId THEN Office.OfficeCode ELSE '' END AS DeputingCode,"
                + "Case WHEN Assessment.BillingOfficeId = Assessment.UnderWritingOfficeId THEN Office.OfficeCode ELSE '' END AS URCode "
                + " FROM ((((Assessment INNER JOIN Accident ON Assessment.AccidentId = Accident.AccidentId) LEFT JOIN (Bill LEFT JOIN Receipt ON Bill.BillId = Receipt.BillId) "
                + "ON Assessment.AssessmentId = Bill.AssessmentId) INNER JOIN (Vehicle "
                + "INNER JOIN (ModelVersion INNER JOIN Model "
                + "ON ModelVersion.ModelId = Model.ModelId) "
                + "ON Vehicle.ModelVersionId=ModelVersion.ModelVersionId) "
                + "ON Assessment.VehicleId = Vehicle.VehicleId) "
                + "INNER JOIN Workshop ON Assessment.WorkshopId = Workshop.WorkshopId) "
                + "INNER JOIN Office ON Assessment.DeputingOfficeId = Office.OfficeId "
                + "WHERE Accident.DtInstructions BETWEEN '" + System.String.Format("{0:yyyyMMdd HH:mm}", DtFrom) + "' "
                + "AND '" + System.String.Format("{0:yyyyMMdd HH:mm}", DtTill) + "' ";
            if (off != null)
            {
                sql += "AND Assessment.DeputingOfficeId = " + off.OfficeId + " ";
            }
            if (wrk != null)
            {
                sql += "AND Assessment.WorkshopId = " + wrk.WorkshopId + " ";
            }
            if (argModel != null)
            {
                sql += "AND Model.ModelId = " + argModel.ModelId + " ";
            }
            if (argAType != null)
            {
                sql += "AND Assessment.AssessmentTypeId = " + argAType.AssessmentTypeId + " ";
            }
            switch (argState)
            {
                case 1:
                    sql += "AND EXISTS(SELECT BillId FROM Bill WHERE Bill.AssessmentId = Assessment.AssessmentId) AND NOT EXISTS(SELECT ReceiptId FROM Receipt WHERE Receipt.AssessmentId = Assessment.AssessmentId)";
                    break;
                case 2:
                    sql += "AND EXISTS(SELECT ReceiptId FROM Receipt WHERE Receipt.AssessmentId = Assessment.AssessmentId)";
                    break;
                case 0:
                    sql += "AND NOT EXISTS(SELECT BillId FROM Bill WHERE Bill.AssessmentId = Assessment.AssessmentId) ";
                    break;
                default:
                    break;
            }
            sql += "ORDER BY Accident.DtInstructions ";

            DataSet ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(Connection, CommandType.Text, sql);
            return ds.Tables[0];
        }
        public override string ToString()
        {
            return this.AssessmentId.ToString();
        }
        private string ANSI(DateTime dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(dt.Year.ToString());
            if (dt.Month < 10)
            {
                sb.Append("0" + dt.Month.ToString());
            }
            else
            {
                sb.Append(dt.Month.ToString());
            }
            if (dt.Day < 10)
            {
                sb.Append("0" + dt.Day.ToString());
            }
            else
            {
                sb.Append(dt.Day.ToString());
            }

            return sb.ToString();
        }
    }
}
