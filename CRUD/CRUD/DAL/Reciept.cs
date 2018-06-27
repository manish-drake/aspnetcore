using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class Receipt : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mReceiptId;
        private System.DateTime mDtCheque;
        private System.String mBank;
        private System.String mSlipNo;
        private System.DateTime mDtSlip;
        private System.String mChequeNo;
        private System.Single mNetReceipt;
        private System.Single mServiceTax;
        private System.Single mEducationTax;
        private System.Single mChequeAmount;
        private Bill mBill;
        private System.Single mTotalFeePaid;
        private System.Decimal mSGST;
        private System.Single mSGSTRate;
        private System.Decimal mCGST;
        private System.Single mCGSTRate;
        private System.Decimal mUGST;
        private System.Single mUGSTRate;
        private System.Decimal mIGST;
        private System.Single mIGSTRate;

        public System.Single TotalFeePaid
        {
            get { return mTotalFeePaid; }
            set { mTotalFeePaid = value; }
        }

        private System.Single mServiceTaxRate;

        public System.Single ServiceTaxRate
        {
            get { return mServiceTaxRate; }
            set { mServiceTaxRate = value; }
        }
        private System.Single mEduCessRate;

        public System.Single EduCessRate
        {
            get { return mEduCessRate; }
            set { mEduCessRate = value; }
        }
        private System.Single mTDSRate;

        public System.Single TDSRate
        {
            get { return mTDSRate; }
            set { mTDSRate = value; }
        }

        private System.Single mTDS;

        public System.Single TDS
        {
            get { return mTDS; }
            set { mTDS = value; }
        }

        private Assessment mAssessment;

        public Assessment Assessment
        {
            get
            {
                if (mAssessment == null)
                {
                    mAssessment = new Assessment(Connection);
                }
                return mAssessment;
            }
            set { mAssessment = value; }
        }

        public Receipt(String argConnection)
        {
            mConnection = argConnection;
        }
        public Receipt(String argConnection, Int32 argReceiptId)
        {
            mConnection = argConnection;
            if (argReceiptId > 0)
            {
                string pstrSql = "SELECT * FROM Receipt WHERE ReceiptId=" + argReceiptId;
                SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
                if (dr.Read())
                {
                    mReceiptId = (System.Int32)dr["ReceiptId"];
                    mDtCheque = (System.DateTime)dr["DtCheque"];
                    mBank = (System.String)dr["Bank"];
                    mSlipNo = (System.String)dr["SlipNo"];
                    mDtSlip = (System.DateTime)dr["DtSlip"];
                    mChequeNo = (System.String)dr["ChequeNo"];
                    mNetReceipt = (System.Single)dr["NetReceipt"];
                    mServiceTax = (System.Single)dr["ServiceTax"];
                    mEducationTax = (System.Single)dr["EducationTax"];
                    mSGST = (System.Decimal)dr["SGST"];
                    mSGSTRate = (System.Single)dr["SGSTRate"];
                    mCGST = (System.Decimal)dr["CGST"];
                    mCGSTRate = (System.Single)dr["CGSTRate"];
                    mUGST = (System.Decimal)dr["UGST"];
                    mUGSTRate = (System.Single)dr["UGSTRate"];
                    mIGST = (System.Decimal)dr["IGST"];
                    mIGSTRate = (System.Single)dr["IGSTRate"];
                    mChequeAmount = (System.Single)dr["ChequeAmount"];
                    mBill = new Bill(Connection, (int)dr["BillId"]);
                    mAssessment = new Assessment(Connection, dr.GetInt32(dr.GetOrdinal("AssessmentId")));
                    mTDS = dr.GetFloat(dr.GetOrdinal("TDS"));
                    mTDSRate = dr.GetFloat(dr.GetOrdinal("TDSRate"));
                    mServiceTaxRate = dr.GetFloat(dr.GetOrdinal("ServiceTaxRate"));
                    mEduCessRate = dr.GetFloat(dr.GetOrdinal("EduCessRate"));
                    mTotalFeePaid = dr.GetFloat(dr.GetOrdinal("TotalFeePaid"));
                }
                else
                {
                    Exception ex = new Exception("Identifier does not exist.");
                    ex.Source = "Receipt.Receipt()";
                    throw ex;
                }
                dr.Close();
            }
        }


        public System.Int32 ReceiptId
        {
            get
            {
                return mReceiptId;
            }
            set
            {
                mReceiptId = value;
            }
        }
        public System.DateTime DtCheque
        {
            get
            {
                if (mDtCheque == DateTime.MinValue)
                {
                    Environment.Variables vars = new Environment.Variables(Connection);
                    mDtCheque = DateTime.Parse(vars["MinimumDate"].Value);
                }
                return mDtCheque;
            }
            set
            {
                mDtCheque = value;
            }
        }
        public System.String Bank
        {
            get
            {
                return mBank;
            }
            set
            {
                mBank = value;
            }
        }
        public System.String SlipNo
        {
            get
            {
                return mSlipNo;
            }
            set
            {
                mSlipNo = value;
            }
        }
        public System.DateTime DtSlip
        {
            get
            {
                if (mDtSlip == DateTime.MinValue)
                {
                    Environment.Variables vars = new Environment.Variables(Connection);
                    mDtSlip = DateTime.Parse(vars["MinimumDate"].Value);
                }
                return mDtSlip;
            }
            set
            {
                mDtSlip = value;
            }
        }
        public System.String ChequeNo
        {
            get
            {
                return mChequeNo;
            }
            set
            {
                mChequeNo = value;
            }
        }
        public System.Single NetReceipt
        {
            get
            {
                return mNetReceipt;
            }
            set
            {
                mNetReceipt = value;
            }
        }
        public System.Single ServiceTax
        {
            get
            {
                return mServiceTax;
            }
            set
            {
                mServiceTax = value;
            }
        }
        public System.Single EducationTax
        {
            get
            {
                return mEducationTax;
            }
            set
            {
                mEducationTax = value;
            }
        }
        public System.Decimal SGST
        {
            get
            {
                return mSGST;
            }
            set
            {
                mSGST = value;
            }
        }

        public System.Single SGSTRate
        {
            get
            {
                return mSGSTRate;
            }
            set
            {
                mSGSTRate = value;
            }
        }

        public System.Decimal CGST
        {
            get
            {
                return mCGST;
            }
            set
            {
                mCGST = value;
            }
        }

        public System.Single CGSTRate
        {
            get
            {
                return mCGSTRate;
            }
            set
            {
                mCGSTRate = value;
            }
        }

        public System.Decimal UGST
        {
            get
            {
                return mUGST;
            }
            set
            {
                mUGST = value;
            }
        }

        public System.Single UGSTRate
        {
            get
            {
                return mUGSTRate;
            }
            set
            {
                mUGSTRate = value;
            }
        }

        public System.Decimal IGST
        {
            get
            {
                return mIGST;
            }
            set
            {
                mIGST = value;
            }
        }

        public System.Single IGSTRate
        {
            get
            {
                return mIGSTRate;
            }
            set
            {
                mIGSTRate = value;
            }
        }

        public System.Single ChequeAmount
        {
            get
            {
                return mChequeAmount;
            }
            set
            {
                mChequeAmount = value;
            }
        }
        public Bill Bill
        {
            get
            {
                if (mBill == null)
                {
                    mBill = new Bill(Connection);
                }
                return mBill;
            }
            set
            {
                mBill = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "Receipt", "ReceiptId");
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
                    mReceiptId = this.Identity.New();
                }
                SqlParameter[] parrSP = GetParameters(argTransactionType);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspReceipt", parrSP);
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
                SqlParameter[] parrSP = GetParameters(enumDBTransaction.spSelect);

                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspReceipt", parrSP);
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
        public System.Data.DataTable Items(DateTime DtFrom, DateTime DtTill, Workshop wrk,
            Office off, AssessmentType argAssessmentType,
            Model argModel)
        {
            try
            {
                SqlParameter[] parrSP = GetParameters(4, DtFrom, DtTill, wrk,
                    off, argAssessmentType, argModel);

                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspReceipt", parrSP);
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
        SqlParameter[] GetParameters(enumDBTransaction argTransactionType)
        {
            return GetParameters(argTransactionType.GetHashCode(), new DateTime(2007, 04, 1),
                DateTime.Today, null, null, null, null);
        }
        SqlParameter[] GetParameters(System.Int32 argTransactionType, DateTime DtFrom,
            DateTime DtTill, Workshop wrk, Office off,
            AssessmentType argAssessmentType, Model argModel)
        {
            SqlParameter[] parrSP = new SqlParameter[32];
            parrSP[0] = new SqlParameter("@Action", argTransactionType);
            parrSP[1] = new SqlParameter("@ReceiptId", ReceiptId);
            parrSP[2] = new SqlParameter("@DtCheque", DtCheque);
            parrSP[3] = new SqlParameter("@Bank", Bank);
            parrSP[4] = new SqlParameter("@SlipNo", SlipNo);
            parrSP[5] = new SqlParameter("@DtSlip", DtSlip);
            parrSP[6] = new SqlParameter("@ChequeNo", ChequeNo);
            parrSP[7] = new SqlParameter("@TotalReceipt", NetReceipt);
            parrSP[8] = new SqlParameter("@ServiceTax", ServiceTax);
            parrSP[9] = new SqlParameter("@EducationTax", EducationTax);
            parrSP[10] = new SqlParameter("@ChequeAmount", ChequeAmount);
            parrSP[11] = new SqlParameter("@BillId", Bill.BillId);
            parrSP[12] = new SqlParameter("@AssessmentId", Assessment.AssessmentId);
            parrSP[13] = new SqlParameter("@TDS", TDS);
            parrSP[14] = new SqlParameter("@WorkshopId", wrk == null ? 0 : wrk.WorkshopId);
            parrSP[15] = new SqlParameter("@OfficeId", off == null ? 0 : off.OfficeId);
            parrSP[16] = new SqlParameter("@ModelId", argModel == null ? 0 : argModel.ModelId);
            parrSP[17] = new SqlParameter("@AssessmentTypeId", argAssessmentType == null ? 0 : argAssessmentType.AssessmentTypeId);
            parrSP[18] = new SqlParameter("@DtFrom", DtFrom);
            parrSP[19] = new SqlParameter("@DtTill", DtTill);
            parrSP[20] = new SqlParameter("@EduCessRate", mEduCessRate);
            parrSP[21] = new SqlParameter("@ServiceTaxRate", mServiceTaxRate);
            parrSP[22] = new SqlParameter("@TDSRate", mTDSRate);
            parrSP[23] = new SqlParameter("@TotalFeePaid", mTotalFeePaid);
            parrSP[24] = new SqlParameter("@CGST", mCGST);
            parrSP[25] = new SqlParameter("@CGSTRate", mCGSTRate);
            parrSP[27] = new SqlParameter("@SGST", mSGST);
            parrSP[26] = new SqlParameter("@SGSTRate", mSGSTRate);
            parrSP[28] = new SqlParameter("@IGST", mIGST);
            parrSP[29] = new SqlParameter("@IGSTRate", mIGSTRate);
            parrSP[30] = new SqlParameter("@UGST", mUGST);
            parrSP[31] = new SqlParameter("@UGSTRate", mUGSTRate);

            return parrSP;
        }
        #endregion
        public override string ToString()
        {
            return this.ReceiptId.ToString();
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "ReceiptId":
        //            return this.ReceiptId.ToString();
        //            break;
        //        case "DtCheque":
        //            return this.DtCheque.ToString();
        //            break;
        //        case "Bank":
        //            return this.Bank.ToString();
        //            break;
        //        case "SlipNo":
        //            return this.SlipNo.ToString();
        //            break;
        //        case "DtSlip":
        //            return this.DtSlip.ToString();
        //            break;
        //        case "ChequeNo":
        //            return this.ChequeNo.ToString();
        //            break;
        //        case "NetReceipt":
        //            return this.NetReceipt.ToString();
        //            break;
        //        case "ServiceTax":
        //            return this.ServiceTax.ToString();
        //            break;
        //        case "EducationTax":
        //            return this.EducationTax.ToString();
        //            break;
        //        case "ChequeAmount":
        //            return this.ChequeAmount.ToString();
        //            break;
        //        case "BillId":
        //            return this.Bill.BillId.ToString();
        //            break;

        //        default:
        //            return null;
        //            break;
        //    }
        //}

    }
}
