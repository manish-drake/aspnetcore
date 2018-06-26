using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.DAL
{
    public class LabourSchdPT : IDBObject
    {
        OSN.Generic.Identity mIdentity;
        private String mConnection;
        private System.Int32 mLabourSchdPTId;
        private Part mPart;
        private PaintType mPaintType;
        private System.Double mRate;


        private enumDBTransaction mTransactionType;

        public enumDBTransaction TransactionType
        {
            get { return mTransactionType; }
            set { mTransactionType = value; }
        }

        public LabourSchdPT(String argConnection)
        {
            mConnection = argConnection;
        }
        public LabourSchdPT(String argConnection, Int32 argLabourSchdPTId)
        {
            mConnection = argConnection;
            string pstrSql = "SELECT * FROM LabourSchdPT WHERE LabourSchdPTId=" + argLabourSchdPTId;
            SqlDataReader dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, pstrSql);
            if (dr.Read())
            {
                mLabourSchdPTId = (System.Int32)dr["LabourSchdPTId"];
                mPart = new Part(Connection, (int)dr["PartId"]);
                mPaintType = new PaintType(Connection, (int)dr["PaintTypeId"]);
                mRate = (System.Double)dr["Rate"];

            }
            else
            {
                Exception ex = new Exception("Identifier does not exist.");
                ex.Source = "LabourSchdPT.LabourSchdPT()";
                throw ex;
            }
            dr.Close();
        }


        public System.Int32 LabourSchdPTId
        {
            get
            {
                return mLabourSchdPTId;
            }
            set
            {
                mLabourSchdPTId = value;
            }
        }
        public Part Part
        {
            get
            {
                if (mPart == null)
                {
                    mPart = new Part(Connection);
                }
                return mPart;
            }
            set
            {
                mPart = value;
            }
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
            set
            {
                mPaintType = value;
            }
        }
        public System.Double Rate
        {
            get
            {
                return mRate;
            }
            set
            {
                mRate = value;
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
                    mIdentity = new OSN.Generic.Identity(Connection, "LabourSchdPT", "LabourSchdPTId");
                }
                return mIdentity;
            }
        }
        public string Transaction()
        {
            if (TransactionType == enumDBTransaction.spNull)
            {
                return "No transaction to commit!";
            }
            else
            {
                return Transaction(TransactionType);
            }
        }
        public string Transaction(enumDBTransaction argTransactionType)
        {
            try
            {
                enumDBTransaction pTransactionType = argTransactionType;
                if (mLabourSchdPTId <= 0)
                {
                    mLabourSchdPTId = this.Identity.New();
                }
                SqlParameter[] parrSP = new SqlParameter[5];

                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@LabourSchdPTId", LabourSchdPTId);
                parrSP[2] = new SqlParameter("@PartId", Part.PartId);
                parrSP[3] = new SqlParameter("@PaintTypeId", PaintType.PaintTypeId);
                parrSP[4] = new SqlParameter("@Rate", Rate);


                SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "uspLabourSchdPT", parrSP);
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
                SqlParameter[] parrSP = new SqlParameter[5];
                enumDBTransaction pTransactionType = enumDBTransaction.spSelect;
                parrSP[0] = new SqlParameter("@Action", pTransactionType);
                parrSP[1] = new SqlParameter("@LabourSchdPTId", LabourSchdPTId);
                parrSP[2] = new SqlParameter("@PartId", Part.PartId);
                parrSP[3] = new SqlParameter("@PaintTypeId", PaintType.PaintTypeId);
                parrSP[4] = new SqlParameter("@Rate", Rate);


                DataSet ds = SqlHelper.ExecuteDataset(Connection, "uspLabourSchdPT", parrSP);
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
            return this.LabourSchdPTId.ToString();
        }
        public string Attributes(string argName)
        {
            switch (argName)
            {
                case "LabourSchdPTId":
                    return this.LabourSchdPTId.ToString();
                    break;
                case "PartId":
                    return this.Part.PartId.ToString();
                    break;
                case "PaintTypeId":
                    return this.PaintType.PaintTypeId.ToString();
                    break;
                case "Rate":
                    return this.Rate.ToString();
                    break;

                default:
                    return null;
                    break;
            }
        }

    }
}
