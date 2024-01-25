using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SW.SW_Common;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for GeneralJournalVoucher_DAL
/// </summary>
public class GeneralJournalVoucher_DAL
{
	public GeneralJournalVoucher_DAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public virtual DataTable GetVoucherType()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.Text, "SELECT VoucherTypeID,VoucherTypeName FROM vt_SCGL_VoucherType").Tables[0];
    }
    //public virtual int CreateNewVoucherNumber(string VoucherTypeID)
    //{
    //    SqlParameter[] param = { new SqlParameter("@VoucherTypeID",VoucherTypeID) };
    //    return Convert.ToInt32(SqlHelper.ExecuteScalar(Config.ConnectionString, "vt_CB_GL_SPGetNewVoucherNumber", param));
    //}
    public virtual DataSet InsertIntoTransaction(GeneralJournalVoucher_BAL BO, SCGL_Session SBO, DataTable GeneralEntries)
    {
        DataSet ds = new DataSet();
        DataSet dset = new DataSet();
        string VoucherNumber = string.Empty;
        using (SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString))
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            using (SqlTransaction trans = con.BeginTransaction())
            {
                try
                {
                    if (BO.VoucherNumber == "")
                    {
                        SqlParameter[] param = { new SqlParameter("@VoucherTypeID", BO.VoucherTypeID) };
                        VoucherNumber = SqlHelper.ExecuteScalar(trans, "vt_SCGL_SPGetNewVoucherNumber", param).ToString();
                        BO.VoucherNumber = VoucherNumber;
                    }
                    foreach (DataRow Row in GeneralEntries.Rows)
                    {
                        SqlParameter[] sQLprams = {new SqlParameter("@TransactionID",Row["TransactionID"])
                                               ,new SqlParameter("@Sno",Row["Sno"])
                                               ,new SqlParameter("@VoucherTypeID",BO.VoucherTypeID) 
                                               ,new SqlParameter("@VoucherTypeName",BO.VoucherTypeName) 
                                               ,new SqlParameter("@VoucherNumber",BO.VoucherNumber) 
                                               ,new SqlParameter("@ReferenceNo",BO.ReferenceNo)
                                               ,new SqlParameter("@Narration",BO.Narration)
                                               ,new SqlParameter("@VoucharDate",BO.VoucharDate)
                                               ,new SqlParameter("@Dimension",BO.Dimension)
                                               ,new SqlParameter("@Code",Row["Code"]) //BO.Code
                                               ,new SqlParameter("@Debit",Row["Debit"].Equals("")?null:Row["Debit"]) //BO.Debit
                                               ,new SqlParameter("@Credit",Row["Credit"].Equals("")?null:Row["Credit"]) //BO.Credit
                                               ,new SqlParameter("@CostCenterID",Row["CostCenterID"]) //BO.CostCenterID
                                               ,new SqlParameter("@Remarks",Row["Remarks"]) //BO.Remarks
                                               ,new SqlParameter("@ActivityBy",SBO.UserID)
                                               ,new SqlParameter("@ActivityDate",DateTime.UtcNow.ToString())
                                               ,new SqlParameter("@SiteID",SBO.SiteID)
                                               ,new SqlParameter("@IP",SBO.UserIP)
                                               ,new SqlParameter("@IsActive",BO.IsActive) 
                                               ,new SqlParameter("@IsPosted",BO.IsPosted)
                                               ,new SqlParameter("@FinYearID",BO.FinYearID)
                                               ,new SqlParameter("@JobID",BO.JobID)};
                        dset = SqlHelper.ExecuteDataset(trans, "vt_SCGL_SpInsertGeneralVoucherTransaction", sQLprams);
                    }
                    trans.Commit();
                    SqlParameter[] pram = { new SqlParameter("@VoucherNumber", BO.VoucherNumber) };
                    ds = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SPGetTransactionByVoucherNumber", pram);
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }
        if (dset.Tables.Count > 0)
        {
            ds.Merge(dset.Tables[0]);
        }
        else
        {
            return ds;
        }
        return ds;
    }

    public virtual DataSet GetRecordByVoucherNumber(string VoucherNumber)
    {
        SqlParameter[] _pram = { new SqlParameter("@VoucherNumber", VoucherNumber) };
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SPGetTransactionByVoucherNumber", _pram);
    }
    public virtual int DeleteTransaction(List<string> list)
    {
        int Status = 0;
        using (SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString))
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            using (SqlTransaction trans = con.BeginTransaction())
            {
                try
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        SqlParameter[] param = { new SqlParameter("@TrasactionID", list[i]) };
                        SqlHelper.ExecuteNonQuery(trans, "vt_SCGL_SpDeleteTransaction", param);
                    }
                    trans.Commit();
                    Status = 1;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
                finally
                {
                    trans.Dispose();
                    con.Dispose();
                }
            }
        }
        return Status;
    }
    public virtual DataTable GetAccountName(string AccountCode)
    {
        SqlParameter[] param = { new SqlParameter("@Match", AccountCode) };
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SPGetSubCodeTitleLikeAll", param).Tables[0];
    }
}
