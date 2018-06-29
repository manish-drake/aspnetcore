using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.DAL
{
    public class BillDetail : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mBillDetailId;
        private System.String mItem;
        private System.String mHead;
        private System.Single mFee;
        private System.Single mReFee;
        private System.String mDescription;
        private System.Single mAmount;
        private System.Single mSAC;

        private Bill mBill;

        private bool mIsDirty;

        public bool IsDirty
        {
            get { return mIsDirty; }
        }
        public BillDetail(String argConnection)
        {
            mConnection = argConnection;
        }
        public BillDetail(String argConnection, Int32 argBillDetailId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM BillDetail WHERE BillDetailId=" + argBillDetailId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mBillDetailId = (System.Int32)dr["BillDetailId"];
                mItem = (System.String)dr["Item"];
                mFee = (System.Single)dr["Fee"];
                mFeeUnit = (System.Single)dr["FeeUnit"];
                mReFee = (System.Single)dr["ReFee"];
                mReInspectionFeeUnit = (System.Single)dr["ReFeeUnit"];
                mDescription = (System.String)dr["Description"];
                mBill = new Bill(Connection, (int)dr["BillId"]);
                mHead = (System.String)dr["Head"];
                mAmount = (System.Single)dr["Amount"];
                mSAC = (System.Int32)dr["SAC"];
            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "BillDetail.BillDetail()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 BillDetailId
        {
            get
            {
                return mBillDetailId;
            }
            set
            {
                mIsDirty = true;
                mBillDetailId = value;
            }
        }
        public System.String Item
        {
            get
            {
                return mItem;
            }
            set
            {
                mIsDirty = true;
                mItem = value;
            }
        }

        public System.String Head
        {
            get
            {
                return mHead;
            }
            set
            {
                mIsDirty = true;
                mHead = value;
            }
        }

        public System.Single Fee
        {
            get
            {
                return mFee;
            }
            set
            {
                mIsDirty = true;
                mFee = value;
            }
        }

        public System.Single Amount
        {
            get
            {
                return mAmount;
            }
            set
            {
                mIsDirty = true;
                mAmount = value;
            }
        }

        public System.Single SAC
        {
            get
            {
                return mSAC;
            }
            set
            {
                mIsDirty = true;
                mSAC = value;
            }
        }

        private System.Single mFeeUnit;

        public System.Single FeeUnit
        {
            get { return mFeeUnit; }
            set
            {
                mIsDirty = true;
                mFeeUnit = value;
            }
        }
        private System.Single mReInspectionFeeUnit;

        public System.Single ReInspectionFeeUnit
        {
            get { return mReInspectionFeeUnit; }
            set
            {
                mIsDirty = true;
                mReInspectionFeeUnit = value;
            }
        }

        public System.Single ReFee
        {
            get
            {
                return mReFee;
            }
            set
            {
                mIsDirty = true;
                mReFee = value;
            }
        }
        public System.String Description
        {
            get
            {
                return mDescription;
            }
            set
            {
                mIsDirty = true;
                mDescription = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "BillDetail", "BillDetailId");
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
                    mBillDetailId = this.Identity.New();
                }
                if (pTransactionType == enumDBTransaction.spEdit)
                {
                    if (mBillDetailId <= 0)
                    {
                        pTransactionType = enumDBTransaction.spAdd;
                        mBillDetailId = this.Identity.New();
                    }
                    else
                    {
                        if (!IsDirty)
                        {
                            pTransactionType = enumDBTransaction.spDelete;
                        }
                    }
                }
                SqlParameter[] parrSP = new SqlParameter[12];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@BillDetailId", BillDetailId);
                parrSP[2] = new SqlParameter("@Item", Item);
                parrSP[3] = new SqlParameter("@Fee", Fee);
                parrSP[4] = new SqlParameter("@FeeUnit", FeeUnit);
                parrSP[5] = new SqlParameter("@ReFee", ReFee);
                parrSP[6] = new SqlParameter("@ReFeeUnit", ReInspectionFeeUnit);
                parrSP[7] = new SqlParameter("@Description", Description);
                parrSP[8] = new SqlParameter("@BillId", Bill.BillId);
                parrSP[9] = new SqlParameter("@SAC", SAC);
                parrSP[10] = new SqlParameter("@Amount", Amount);
                parrSP[11] = new SqlParameter("@Head", Head);

                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspBillDetail", parrSP);
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
                SqlParameter[] parrSP = new SqlParameter[12];
                enumDBTransaction pTransactionType = enumDBTransaction.spSelect;
                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@BillDetailId", BillDetailId);
                parrSP[2] = new SqlParameter("@Item", Item);
                parrSP[3] = new SqlParameter("@Fee", Fee);
                parrSP[4] = new SqlParameter("@FeeUnit", FeeUnit);
                parrSP[5] = new SqlParameter("@ReFee", ReFee);
                parrSP[6] = new SqlParameter("@ReFeeUnit", ReInspectionFeeUnit);
                parrSP[7] = new SqlParameter("@Description", Description);
                parrSP[8] = new SqlParameter("@BillId", Bill.BillId);
                parrSP[9] = new SqlParameter("@SAC", SAC);
                parrSP[10] = new SqlParameter("@Amount", Amount);
                parrSP[11] = new SqlParameter("@Head", Head);

                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspBillDetail", parrSP);
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
            return this.BillDetailId.ToString();
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "BillDetailId":
        //            return this.BillDetailId.ToString();
        //            break;
        //        case "Item":
        //            return this.Item.ToString();
        //            break;
        //        case "Fee":
        //            return this.Fee.ToString();
        //            break;
        //        case "ReFee":
        //            return this.ReFee.ToString();
        //            break;
        //        case "Description":
        //            return this.Description.ToString();
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
