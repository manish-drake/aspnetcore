using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class FeeSchedule : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mFeeScheduleId;
        private InsuranceCompany mInsuranceCompany;
        private AssessmentType mAssessmentType;
        private System.DateTime mDtWEF;
        private System.Boolean mIsEstimateBased;
        private FeeScheduleSlabs mFeeScheduleSlabs;
        private float mDefault;

        public float Default
        {
            get { return mDefault; }
            set { mDefault = value; }
        }

        public FeeScheduleSlabs FeeScheduleSlabs
        {
            get
            {
                if (mFeeScheduleSlabs == null)
                {
                    mFeeScheduleSlabs = new FeeScheduleSlabs();
                    string sql = "";
                    sql += "SELECT FeeScheduleSlabId ";
                    sql += "FROM FeeScheduleSlab ";
                    sql += "WHERE FeeScheduleId = " + this.mFeeScheduleId;
                    SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, sql);
                    while (dr.Read())
                    {
                        FeeScheduleSlab fss = new FeeScheduleSlab(Connection, (int)dr["FeeScheduleSlabId"]);
                        mFeeScheduleSlabs.Add(fss, dr["FeeScheduleSlabId"].ToString());
                    }
                    dr.Close();
                }
                return mFeeScheduleSlabs;
            }
            set { mFeeScheduleSlabs = value; }
        }




        public FeeSchedule(String argConnection)
        {
            mConnection = argConnection;

        }
        public FeeSchedule(String argConnection, Int32 argFeeScheduleId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM FeeSchedule WHERE FeeScheduleId=" + argFeeScheduleId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mFeeScheduleId = (System.Int32)dr["FeeScheduleId"];
                mInsuranceCompany = new InsuranceCompany(Connection, (int)dr["InsuranceCompanyId"]);
                mAssessmentType = new AssessmentType(Connection, (int)dr["AssessmentTypeId"]);
                mDtWEF = (System.DateTime)dr["DtWEF"];
                mIsEstimateBased = (System.Boolean)dr["IsEstimateBased"];

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "FeeSchedule.FeeSchedule()";
                throw ex;
            }
            dr.Close();
        }
        public FeeSchedule(String argConnection, AssessmentType assessmentType, Office office)
        {
            mConnection = argConnection;
            string pstrSql = System.String.Format("SELECT * FROM FeeSchedule WHERE AssessmentTypeId={0} AND InsuranceCompanyId={1} ORDER BY DtWEF DESC", assessmentType.AssessmentTypeId, office.InsuranceCompany.InsuranceCompanyId);

            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mFeeScheduleId = (System.Int32)dr["FeeScheduleId"];
                mInsuranceCompany = new InsuranceCompany(Connection, (int)dr["InsuranceCompanyId"]);
                mAssessmentType = new AssessmentType(Connection, (int)dr["AssessmentTypeId"]);
                mDtWEF = (System.DateTime)dr["DtWEF"];
                mIsEstimateBased = (System.Boolean)dr["IsEstimateBased"];
            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "FeeSchedule.FeeSchedule()";
                throw ex;
            }
            dr.Close();
        }
        public FeeSchedule(String argConnection, AssessmentType assessmentType, Office office, DateTime dtAsOn)
        {
            mConnection = argConnection;
            string pstrSql = System.String.Format("SELECT * FROM FeeSchedule WHERE AssessmentTypeId={0} AND InsuranceCompanyId={1} AND DtWEF <= '{2:yyyyMMdd}' ORDER BY DtWEF DESC", assessmentType.AssessmentTypeId, office.InsuranceCompany.InsuranceCompanyId, dtAsOn);

            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mFeeScheduleId = (System.Int32)dr["FeeScheduleId"];
                mInsuranceCompany = new InsuranceCompany(Connection, (int)dr["InsuranceCompanyId"]);
                mAssessmentType = new AssessmentType(Connection, (int)dr["AssessmentTypeId"]);
                mDtWEF = (System.DateTime)dr["DtWEF"];
                mIsEstimateBased = (System.Boolean)dr["IsEstimateBased"];
            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "FeeSchedule.FeeSchedule()";
                throw ex;
            }
            dr.Close();
        }
        public System.Int32 FeeScheduleId
        {
            get
            {
                return mFeeScheduleId;
            }
            set
            {
                mFeeScheduleId = value;
            }
        }
        public InsuranceCompany InsuranceCompany
        {
            get
            {
                if (mInsuranceCompany == null)
                {
                    mInsuranceCompany = new InsuranceCompany(Connection);
                }
                return mInsuranceCompany;
            }
            set
            {
                mInsuranceCompany = value;
            }
        }
        public AssessmentType AssessmentType
        {
            get
            {
                if (mAssessmentType == null)
                {
                    mAssessmentType = new AssessmentType(Connection);
                }
                return mAssessmentType;
            }
            set
            {
                mAssessmentType = value;
            }
        }
        public System.DateTime DtWEF
        {
            get
            {
                if (mDtWEF == DateTime.MinValue)
                {
                    Environment.Variables vars = new Environment.Variables(Connection);
                    mDtWEF = DateTime.Parse(vars["MinimumDate"].Value);
                }
                return mDtWEF;
            }
            set
            {
                mDtWEF = value;
            }
        }
        public System.Boolean IsEstimateBased
        {
            get
            {
                return mIsEstimateBased;
            }
            set
            {
                mIsEstimateBased = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "FeeSchedule", "FeeScheduleId");
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
                    mFeeScheduleId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[6];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@FeeScheduleId", FeeScheduleId);
                parrSP[2] = new SqlParameter("@InsuranceCompanyId", InsuranceCompany.InsuranceCompanyId);
                parrSP[3] = new SqlParameter("@AssessmentTypeId", AssessmentType.AssessmentTypeId);
                parrSP[4] = new SqlParameter("@DtWEF", DtWEF);
                parrSP[5] = new SqlParameter("@IsEstimateBased", IsEstimateBased);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspFeeSchedule", parrSP);

                if (FeeScheduleSlabs.IsDirty)
                {
                    foreach (FeeScheduleSlab fss in this.FeeScheduleSlabs.Values)
                    {
                        fss.FeeSchedule = this;
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
                SqlParameter[] parrSP = new SqlParameter[6];
                enumDBTransaction pTransactionType = enumDBTransaction.spSelect;
                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@FeeScheduleId", FeeScheduleId);
                parrSP[2] = new SqlParameter("@InsuranceCompanyId", InsuranceCompany.InsuranceCompanyId);
                parrSP[3] = new SqlParameter("@AssessmentTypeId", AssessmentType.AssessmentTypeId);
                parrSP[4] = new SqlParameter("@DtWEF", DtWEF);
                parrSP[5] = new SqlParameter("@IsEstimateBased", IsEstimateBased);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspFeeSchedule", parrSP);
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
            return this.FeeScheduleId.ToString();
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "FeeScheduleId":
        //            return this.FeeScheduleId.ToString();
        //            break;
        //        case "InsuranceCompanyId":
        //            return this.InsuranceCompany.InsuranceCompanyId.ToString();
        //            break;
        //        case "AssessmentTypeId":
        //            return this.AssessmentType.AssessmentTypeId.ToString();
        //            break;
        //        case "DtWEF":
        //            return this.DtWEF.ToString();
        //            break;
        //        case "IsEstimateBased":
        //            return this.IsEstimateBased.ToString();
        //            break;

        //        default:
        //            return null;
        //            break;
        //    }
        //}


        internal double GetSurveyorFee(double claim, double loss)
        {
            foreach (FeeScheduleSlab slab in this.FeeScheduleSlabs.Values)
            {
                if (this.IsEstimateBased)
                {
                    if ((slab.SlabFrom <= claim) || (slab.SlabUpto >= claim))
                    {
                        return slab.Fee;
                    }
                }
                else
                {
                    if ((slab.SlabFrom <= loss) || (slab.SlabUpto >= loss))
                    {
                        return slab.Fee;
                    }
                }
            }
            return 0;

        }
        internal double GetSurveyorReFee(double claim, double loss)
        {
            foreach (FeeScheduleSlab slab in this.FeeScheduleSlabs.Values)
            {
                if (this.IsEstimateBased)
                {
                    if ((slab.SlabFrom <= claim) || (slab.SlabUpto >= claim))
                    {
                        return slab.ReInspectionFee;
                    }
                }
                else
                {
                    if ((slab.SlabFrom <= loss) || (slab.SlabUpto >= loss))
                    {
                        return slab.ReInspectionFee;
                    }
                }
            }
            return 0;
        }
    }
}
