using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class Bill : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mBillId;
        private System.String mBillNo;
        private System.DateTime mDtBill;
        private System.Single mTotal;
        private System.Single mServiceTaxRate;
        private System.Decimal mServiceTax;
        private System.Single mEducationCessRate;
        private System.Decimal mEducationCess;
        private System.Single mGrandTotal;
        private System.String mStateCode;
        private System.Decimal mSGST;
        private System.Single mSGSTRate;
        private System.Decimal mCGST;
        private System.Single mCGSTRate;
        private System.Decimal mUGST;
        private System.Single mUGSTRate;
        private System.Decimal mIGST;
        private System.Single mIGSTRate;
        private BillDetails mBillDetails;

        public BillDetails BillDetails
        {
            get
            {
                if (mBillDetails == null)
                {
                    mBillDetails = new BillDetails();
                    string sql = "";
                    sql += "SELECT BillDetailId ";
                    sql += "FROM BillDetail ";
                    sql += "WHERE BillId = " + this.mBillId;
                    SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, sql);
                    while (dr.Read())
                    {
                        BillDetail fss = new BillDetail(Connection, (int)dr["BillDetailId"]);
                        mBillDetails.Add(fss, dr["BillDetailId"].ToString());
                    }
                    dr.Close();
                }
                return mBillDetails;
            }
            set
            {
                mBillDetails = value;
            }
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

        public Bill(String argConnection)
        {
            mConnection = argConnection;
        }
        public Bill(String argConnection, Int32 argBillId)
        {
            try
            {
                mConnection = argConnection;
                string pstrSql = "SELECT * FROM Bill WHERE BillId=" + argBillId;
                SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
                if (dr.Read())
                {
                    mBillId = (System.Int32)dr["BillId"];
                    mBillNo = (System.String)dr["BillNo"];
                    mDtBill = (System.DateTime)dr["DtBill"];
                    mTotal = (System.Single)dr["Total"];
                    mServiceTaxRate = (System.Single)dr["ServiceTaxRate"];
                    mServiceTax = (System.Decimal)dr["ServiceTax"];
                    mSGST = (System.Decimal)dr["SGST"];
                    mSGSTRate = (System.Single)dr["SGSTRate"];
                    mCGST = (System.Decimal)dr["CGST"];
                    mCGSTRate = (System.Single)dr["CGSTRate"];
                    mUGST = (System.Decimal)dr["UGST"];
                    mUGSTRate = (System.Single)dr["UGSTRate"];
                    mIGST = (System.Decimal)dr["IGST"];
                    mIGSTRate = (System.Single)dr["IGSTRate"];
                    mStateCode = (System.String)dr["StateCode"];
                    mEducationCessRate = (System.Single)dr["EducationCessRate"];
                    mEducationCess = (System.Decimal)dr["EducationCess"];
                    mGrandTotal = (System.Single)dr["GrandTotal"];
                    mAssessment = new Assessment(Connection, dr.GetInt32(dr.GetOrdinal("AssessmentId")));
                }
                else
                {
                    Exception ex = new Exception("Identifier does not exist.");
                    ex.Source = "Bill.Bill()";
                    throw ex;
                }
                dr.Close();
            }
            catch { }
        }

        public string NewBillNo()
        {
            string sql = "SELECT MAX(BillNo) AS MaxKey FROM Bill";
            String str = "";
            System.Data.SqlClient.SqlDataReader sdr = SqlHelper.ExecuteReader(this.Connection, CommandType.Text, sql);
            while (sdr.Read())
            {
                if (sdr.GetValue(0) != DBNull.Value)
                {
                    try
                    {
                        System.Int32 num = System.Int32.Parse(sdr["MaxKey"].ToString());
                        num++;
                        str = System.String.Format("{0:00000}", num);
                    }
                    catch { }
                }
                else
                {
                    str = "00001";
                }
            }
            sdr.Close();
            return str;
        }

        public System.Int32 BillId
        {
            get
            {
                return mBillId;
            }
            set
            {
                mBillId = value;
            }
        }
        public System.String BillNo
        {
            get
            {
                return mBillNo;
            }
            set
            {
                mBillNo = value;
            }
        }
        public System.DateTime DtBill
        {
            get
            {
                if (mDtBill == DateTime.MinValue)
                {
                    Environment.Variables vars = new Environment.Variables(Connection);
                    mDtBill = DateTime.Parse(vars["MinimumDate"].Value);
                }
                return mDtBill;
            }
            set
            {
                mDtBill = value;
            }
        }
        public System.Single Total
        {
            get
            {
                return mTotal;
            }
            set
            {
                mTotal = value;
            }
        }

        public System.Single ServiceTaxRate
        {
            get
            {
                return mServiceTaxRate;
            }
            set
            {
                mServiceTaxRate = value;
            }
        }
        public System.Decimal ServiceTax
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

        public System.String StateCode
        {
            get
            {
                return mStateCode;
            }
            set
            {
                mStateCode = value;
            }
        }


        public System.Single EducationCessRate
        {
            get
            {
                return mEducationCessRate;
            }
            set
            {
                mEducationCessRate = value;
            }
        }
        public System.Decimal EducationCess
        {
            get
            {
                return mEducationCess;
            }
            set
            {
                mEducationCess = value;
            }
        }
        public System.Single GrandTotal
        {
            get
            {
                return mGrandTotal;
            }
            set
            {
                mGrandTotal = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "Bill", "BillId");
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
                    mBillId = this.Identity.New();
                }
                SqlParameter[] parrSP = GetParameters(pTransactionType);

                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspBill", parrSP);
                if (BillDetails.IsDirty)
                {
                    foreach (BillDetail fss in this.BillDetails.Values)
                    {
                        fss.Bill = this;
                        fss.Transaction(pTransactionType);
                    }
                }
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
                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspBill", parrSP);
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
        public System.Data.DataTable Items(System.Boolean pending)
        {
            try
            {
                System.String sql = "SELECT Bill.* FROM Bill WHERE NOT EXISTS(SELECT ReceiptId FROM Receipt WHERE Receipt.BillId = Bill.BillId)";

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
        public System.Data.DataTable Items(DateTime DtFrom, DateTime DtTill, Workshop wrk,
            Office off, AssessmentType argAssessmentType,
           Model argModel)
        {
            try
            {
                SqlParameter[] parrSP = GetParameters(4, DtFrom, DtTill, wrk,
                    off, argAssessmentType, argModel);
                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspBill", parrSP);
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
            //@Action int = 0 OUTPUT, 
            //@BillId int = 0, 
            //@BillNo varchar (10) = '', 
            //@DtBill datetime = '20070101', 
            //@Total real = 0, 
            //@ServiceTaxRate real = 0, 
            //@ServiceTax decimal = 0, 
            //@EducationCessRate real = 0, 
            //@EducationCess decimal = 0, 
            //@GrandTotal real = 0, 
            //@AssessmentId int = 0,
            //@WorkshopId int = 0,
            //@OfficeId int = 0,
            //@ModelId int = 0,
            //@AssessmentTypeId int = 0,
            //@DtFrom datetime ='20070101 00:00',
            //@DtTill datetime ='20080101 00:00'
            SqlParameter[] parrSP = new SqlParameter[26];
            parrSP[0] = new SqlParameter("@Action", argTransactionType);
            parrSP[1] = new SqlParameter("@BillId", BillId);
            parrSP[2] = new SqlParameter("@BillNo", BillNo);
            parrSP[3] = new SqlParameter("@DtBill", DtBill);
            parrSP[4] = new SqlParameter("@Total", Total);
            parrSP[5] = new SqlParameter("@ServiceTaxRate", ServiceTaxRate);
            parrSP[6] = new SqlParameter("@ServiceTax", ServiceTax);
            parrSP[7] = new SqlParameter("@EducationCessRate", EducationCessRate);
            parrSP[8] = new SqlParameter("@EducationCess", EducationCess);
            parrSP[9] = new SqlParameter("@GrandTotal", GrandTotal);
            parrSP[10] = new SqlParameter("@AssessmentId", Assessment.AssessmentId);
            parrSP[11] = new SqlParameter("@WorkshopId", wrk == null ? 0 : wrk.WorkshopId);
            parrSP[12] = new SqlParameter("@OfficeId", off == null ? 0 : off.OfficeId);
            parrSP[13] = new SqlParameter("@ModelId", argModel == null ? 0 : argModel.ModelId);
            parrSP[14] = new SqlParameter("@AssessmentTypeId", argAssessmentType == null ? 0 : argAssessmentType.AssessmentTypeId);
            parrSP[15] = new SqlParameter("@DtFrom", DtFrom);
            parrSP[16] = new SqlParameter("@DtTill", DtTill);
            parrSP[17] = new SqlParameter("@StateCode", StateCode == null ? "0" : StateCode);
            parrSP[18] = new SqlParameter("@SGST", SGST);
            parrSP[19] = new SqlParameter("@SGSTRate", SGSTRate);
            parrSP[20] = new SqlParameter("@CGST", CGST);
            parrSP[21] = new SqlParameter("@CGSTRate", CGSTRate);
            parrSP[22] = new SqlParameter("@UGST", UGST);
            parrSP[23] = new SqlParameter("@UGSTRate", UGSTRate);
            parrSP[24] = new SqlParameter("@IGST", IGST);
            parrSP[25] = new SqlParameter("@IGSTRate", IGSTRate);
            return parrSP;
        }

        #endregion
        public override string ToString()
        {
            return this.BillId.ToString();
        }
    }
}
