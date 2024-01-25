using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SW.SW_Common;

/// <summary>
/// Summary description for GLCashRecVoucher_DAL
/// </summary>
public class GLCashRecVoucher_DAL
{
	public GLCashRecVoucher_DAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
    public virtual DataTable GetSubAccNameCashRecievedVoucherLike(string Match)
    {
        DataTable dt = new DataTable();
        SqlParameter[] param = { new SqlParameter("@Match", Match) };
        return dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SPGetSubCodeCashRecievedVoucherLike", param).Tables[0];
    }
    public virtual DataSet InsertUpdateTransaction(GLCashRecVoucher_BAL BO, SCGL_Session SBO, DataTable TransTable)
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
                    SqlParameter[] _DebitParam = {new SqlParameter("@TransactionID",BO.TransactionID)
                                                   ,new SqlParameter("@Sno",null)
                                                   ,new SqlParameter("@VoucherTypeID",BO.VoucherTypeID) 
                                                   ,new SqlParameter("@VoucherTypeName",BO.VoucherTypeName) 
                                                   ,new SqlParameter("@VoucherNumber",BO.VoucherNumber) 
                                                   ,new SqlParameter("@ReferenceNo",BO.ReferenceNo)
                                                   ,new SqlParameter("@Narration",BO.Narration)
                                                   ,new SqlParameter("@VoucharDate",BO.VoucharDate)
                                                   ,new SqlParameter("@Dimension",BO.Dimension)
                                                   ,new SqlParameter("@Code",BO.Code) 
                                                   ,new SqlParameter("@Debit",BO.Debit) 
                                                   ,new SqlParameter("@Credit",BO.Credit) 
                                                   ,new SqlParameter("@CostCenterID",BO.CostCenterID) 
                                                   ,new SqlParameter("@Remarks",BO.Narration) 
                                                   //,new SqlParameter("@Remarks",BO.Remarks) 
                                                   ,new SqlParameter("@ActivityBy",SBO.UserID)
                                                   ,new SqlParameter("@ActivityDate",DateTime.UtcNow.ToString())
                                                   ,new SqlParameter("@SiteID",SBO.SiteID)
                                                   ,new SqlParameter("@IP",SBO.UserIP)
                                                   ,new SqlParameter("@IsActive",BO.IsActive) 
                                                   ,new SqlParameter("@IsPosted",BO.IsPosted)
                                                   ,new SqlParameter("@FinYearID",BO.FinYearID)
                                                ,new SqlParameter("@JobID",BO.JobID)};
                    // SqlHelper.ExecuteNonQuery(trans, "vt_SCGL_SpInsertGeneralVoucherTransaction", DebitParam);
                    dset = SqlHelper.ExecuteDataset(trans, "vt_SCGL_SpInsertGeneralVoucherTransaction", _DebitParam);
                    foreach (DataRow Row in TransTable.Rows)
                    {
                        SqlParameter[] _prams = {new SqlParameter("@TransactionID",Row["TransactionID"])
                                                   ,new SqlParameter("@Sno",Row["Sno"])
                                                   ,new SqlParameter("@VoucherTypeID",BO.VoucherTypeID) 
                                                   ,new SqlParameter("@VoucherTypeName",BO.VoucherTypeName) 
                                                   ,new SqlParameter("@VoucherNumber",BO.VoucherNumber) 
                                                   ,new SqlParameter("@ReferenceNo",BO.ReferenceNo)
                                                   ,new SqlParameter("@Narration",BO.Narration)
                                                   ,new SqlParameter("@VoucharDate",BO.VoucharDate)
                                                   ,new SqlParameter("@Dimension",BO.Dimension)
                                                   //,new SqlParameter("@MainCode",Row["MainCode"]) // BO.MainCode
                                                   //,new SqlParameter("@ControlCode",Row["ControlCode"]) //BO.ControlCode
                                                   //,new SqlParameter("@SubsidiaryCode",Row["SubCode"])//BO.SubsidiaryCode
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
                        SqlHelper.ExecuteNonQuery(trans, "vt_SCGL_SpInsertGeneralVoucherTransaction", _prams);
                    }
                    trans.Commit();
                    SqlParameter[] pram = { new SqlParameter("@VoucherNumber", BO.VoucherNumber) };
                    ds = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SPGetTransactionCashRecieptVoucher", pram);
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
                if (dset.Tables.Count > 0)
                {
                    ds.Merge(dset.Tables[0]);
                }
                else
                {
                    return ds;
                }
            }
        }
        return ds;
    }

    public virtual DataSet GetCashRecVoucherRecord(string VoucherNumber)
    {
        SqlParameter[] pram = { new SqlParameter("@VoucherNumber", VoucherNumber) };
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SPGetTransactionCashRecieptVoucher", pram);
    }
}
