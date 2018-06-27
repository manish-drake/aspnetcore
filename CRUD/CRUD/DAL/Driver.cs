using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class Driver : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mDriverId;
        private System.String mDefault;
        private System.DateTime mDtLicenseIssue;
        private System.String mLicenseNo;
        private System.DateTime mDtValidLicense;
        private System.String mIssuingAuthority;

        private System.String mBadgeNo;
        private System.String mStatusDL;

        public System.String StatusDL
        {
            get { return mStatusDL; }
            set { mStatusDL = value; }
        }

        public Driver(String argConnection)
        {
            mConnection = argConnection;
        }
        public Driver(String argConnection, Int32 argDriverId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM Driver WHERE DriverId=" + argDriverId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mDriverId = (System.Int32)dr["DriverId"];
                mDefault = (System.String)dr["Driver"];
                mDtLicenseIssue = (System.DateTime)dr["DtLicenseIssue"];
                mLicenseNo = (System.String)dr["LicenseNo"];
                mDtValidLicense = (System.DateTime)dr["DtValidLicense"];
                mIssuingAuthority = (System.String)dr["IssuingAuthority"];
                mLicenseType = new LicenseType(Connection, (int)dr["LicenseTypeId"]);
                mBadgeNo = (System.String)dr["BadgeNo"];
                mStatusDL = (System.String)dr["StatusDL"];

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "Driver.Driver()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 DriverId
        {
            get
            {
                return mDriverId;
            }
            set
            {
                mDriverId = value;
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
        public System.DateTime DtLicenseIssue
        {
            get
            {
                if (mDtLicenseIssue == DateTime.MinValue)
                {
                    Environment.Variables vars = new Environment.Variables(Connection);
                    mDtLicenseIssue = DateTime.Parse(vars["MinimumDate"].Value);
                }
                return mDtLicenseIssue;
            }
            set
            {
                mDtLicenseIssue = value;
            }
        }
        public System.String LicenseNo
        {
            get
            {
                return mLicenseNo;
            }
            set
            {
                mLicenseNo = value;
            }
        }
        public System.DateTime DtValidLicense
        {
            get
            {
                if (mDtValidLicense == DateTime.MinValue)
                {
                    Environment.Variables vars = new Environment.Variables(Connection);
                    mDtValidLicense = DateTime.Parse(vars["MinimumDate"].Value);
                }
                return mDtValidLicense;
            }
            set
            {
                mDtValidLicense = value;
            }
        }
        public System.String IssuingAuthority
        {
            get
            {
                return mIssuingAuthority;
            }
            set
            {
                mIssuingAuthority = value;
            }
        }
        private LicenseType mLicenseType;

        public LicenseType LicenseType
        {
            get
            {
                if (mLicenseType == null)
                {
                    mLicenseType = new LicenseType(Connection);
                }
                return mLicenseType;
            }
            set { mLicenseType = value; }
        }

        public System.String BadgeNo
        {
            get
            {
                return mBadgeNo;
            }
            set
            {
                mBadgeNo = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "Driver", "DriverId");
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
                    mDriverId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[10];

                if (LicenseType.LicenseTypeId == 0)
                {
                    LicenseType = new LicenseType(Connection, -1);
                }

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@DriverId", DriverId);
                parrSP[2] = new SqlParameter("@Driver", Default);
                parrSP[3] = new SqlParameter("@DtLicenseIssue", DtLicenseIssue);
                parrSP[4] = new SqlParameter("@LicenseNo", LicenseNo);
                parrSP[5] = new SqlParameter("@DtValidLicense", DtValidLicense);
                parrSP[6] = new SqlParameter("@IssuingAuthority", IssuingAuthority);
                parrSP[7] = new SqlParameter("@LicenseTypeId", this.LicenseType.LicenseTypeId);
                parrSP[8] = new SqlParameter("@BadgeNo", BadgeNo);
                parrSP[9] = new SqlParameter("@StatusDL", StatusDL);

                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspDriver", parrSP);
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
                SqlParameter[] parrSP = new SqlParameter[10];
                enumDBTransaction pTransactionType = enumDBTransaction.spSelect;
                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@DriverId", DriverId);
                parrSP[2] = new SqlParameter("@Driver", Default);
                parrSP[3] = new SqlParameter("@DtLicenseIssue", DtLicenseIssue);
                parrSP[4] = new SqlParameter("@LicenseNo", LicenseNo);
                parrSP[5] = new SqlParameter("@DtValidLicense", DtValidLicense);
                parrSP[6] = new SqlParameter("@IssuingAuthority", IssuingAuthority);
                parrSP[7] = new SqlParameter("@LicenseTypeId", LicenseType.LicenseTypeId);
                parrSP[8] = new SqlParameter("@BadgeNo", BadgeNo);
                parrSP[9] = new SqlParameter("@StatusDL", StatusDL);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspDriver", parrSP);
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
            return this.DriverId.ToString();
        }
        //public string Attributes(string argName)
        //{
        //    switch (argName)
        //    {
        //        case "DriverId":
        //            return this.DriverId.ToString();
        //            break;
        //        case "Driver":
        //            return this.Default.ToString();
        //            break;
        //        case "DtLicenseIssue":
        //            return this.DtLicenseIssue.ToString();
        //            break;
        //        case "LicenseNo":
        //            return this.LicenseNo.ToString();
        //            break;
        //        case "DtValidLicense":
        //            return this.DtValidLicense.ToString();
        //            break;
        //        case "IssuingAuthority":
        //            return this.IssuingAuthority.ToString();
        //            break;
        //        case "LicenseType":
        //            return this.LicenseType.ToString();
        //            break;
        //        case "BadgeNo":
        //            return this.BadgeNo.ToString();
        //            break;
        //        case "StatusDL":
        //            return this.StatusDL.ToString();
        //            break;

        //        default:
        //            return null;
        //            break;
        //    }
        //}
        public System.Boolean IsValidForVehicleCategory(System.Int32 argVCategoryId)
        {
            System.Boolean flag = false;
            string sql = "SELECT * FROM VldtLtVc WHERE LicenseTypeId = ";
            sql += this.LicenseType.LicenseTypeId + " AND ";
            sql += "VehicleCategoryId = " + argVCategoryId;
            System.Data.SqlClient.SqlDataReader dr =
                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Connection, CommandType.Text, sql);
            if (dr.Read())
            {
                flag = true;
            }
            dr.Close();
            return flag;
        }

    }
}
