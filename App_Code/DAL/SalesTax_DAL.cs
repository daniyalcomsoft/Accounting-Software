using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SW.SW_Common;
using System.Data.SqlClient;

/// <summary>
/// Summary description for SalesTax_DAL
/// </summary>
public class SalesTax_DAL
{
	public SalesTax_DAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public virtual DataTable getSalesTax()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetSalesTaxData").Tables[0];

    }


    //public virtual DataTable getalloverFinancialYear()
    //{
    //    return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetallFinYear").Tables[0];

    //}

    public virtual SalesTax_BAL GetSalesTaxByID(int SalesTaxID)
    {
        SalesTax_BAL FBLL = new SalesTax_BAL();
        SqlParameter[] param = { new SqlParameter("@STID", SalesTaxID) };
        using (SqlDataReader dr = SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_SCGL_Sp_GetSalesTaxByID", param))
        {
            if (dr.Read())
            {
                FBLL.SalesTaxID = SCGL_Common.Convert_ToInt(dr["SalesTaxID"]);
                FBLL.SalesTax = dr["SalesTax"].ToString();
                //FBLL.YearFrom = Convert.ToDateTime(dr["YearFrom"]);
                FBLL.YearFrom = dr["YearFrom"].ToString();
                //FBLL.YearTo = Convert.ToDateTime(dr["YearTo"]);
                FBLL.YearTo = dr["YearTo"].ToString();
            }
        }
        return FBLL;
    }

    public virtual string DeleteSalesTax(int SalesTaxID, string StartDate, string EndDate)
    {
        SqlParameter[] param = { new SqlParameter("@STID", SalesTaxID)                               
                               ,new SqlParameter("@StartDate", StartDate)
                               ,new SqlParameter("@EndDate", EndDate)};
        return SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_Sp_DeleteSalesTax", param).ToString();
    }

    public virtual int CreateModifySalesTax(SalesTax_BAL FY, SCGL_Session SBO)
    {
        SqlParameter[] param = {new SqlParameter("@STID",FY.SalesTaxID)
                                   ,new SqlParameter("@SalesTax",FY.SalesTax)
                                   ,new SqlParameter("@YearFrom",FY.YearFrom)
                                   ,new SqlParameter("@YearTo",FY.YearTo)};
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_Sp_CreateModifySalesTax", param));
    }


    //public virtual int SetDefaultSalesTaxYear(int SalesTaxID)
    //{
    //    SqlParameter[] param = { new SqlParameter("@SalesTaxID", SalesTaxID) };
    //    return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SetDefaultYear", param));
    //}

    public virtual int CountSalesTaxOverlapPeriods(int SalesTaxID, DateTime StartDate, DateTime EndDate)
    {
        SqlParameter[] param = {new SqlParameter("@SalesTaxID", SalesTaxID)
                                ,new SqlParameter("@StartDate", StartDate)
                                ,new SqlParameter("@EndDate", EndDate)};
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_CountSalesTaxOverlapPeriod", param));
    }

    public virtual int CreateModifySalesTaxInvoice(SalesTax_BAL BALSalesTax, SqlTransaction Trans)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@SalesTaxID", BALSalesTax.SalesTaxID)
                                   ,new SqlParameter("@SalesTaxInvoiceID", BALSalesTax.SalesTaxInvoiceID)
                                   ,new SqlParameter("@Date", BALSalesTax.Date)
                                   ,new SqlParameter("@JobID",BALSalesTax.JobID)
                                   ,new SqlParameter("@Packages",BALSalesTax.Packages)
                                   ,new SqlParameter("@RefNo",BALSalesTax.RefNo)
                                   ,new SqlParameter("@ServiceCharges",BALSalesTax.ServiceCharges)
                                   ,new SqlParameter("@OUE",BALSalesTax.OUE)
                                   ,new SqlParameter("@AmtAT",BALSalesTax.AmtAT)
                                 
                               };
        int i = Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, CommandType.StoredProcedure, "vt_SCGL_SpCreateModifySalesTaxInvoice", param));
        return i;
    }

    public virtual bool CreateSalesTaxInvoiceGLTrans(SalesTax_BAL BALSalesTax, SqlTransaction Trans)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@SalesTaxID", BALSalesTax.SalesTaxID)
                                   ,new SqlParameter("@Date", BALSalesTax.Date)
                                   ,new SqlParameter("@JobID",BALSalesTax.JobID)
                                   ,new SqlParameter("@ServiceCharges",BALSalesTax.ServiceCharges)
                                   ,new SqlParameter("@AmtAT",BALSalesTax.AmtAT)
                                 
                               };
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_SCGL_SpCreateSalesTaxInvoiceGLTrans", param);
        return i > 0;
    }

    public virtual int DeleteTransaction_SalesTaxInvoice(int SalesTaxInvoiceID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@SalesTaxInvoiceID", SalesTaxInvoiceID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, "vt_SCGL_SPDeleteTransaction_SalesTaxInvoice", param));
    }

    public virtual DataTable getSalesTaxInvoiceByID(int SalesTaxID)
    {
        SqlParameter param = new SqlParameter("@SalesTaxID", SalesTaxID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetSalesTaxInvoice", param).Tables[0];
        return dt;
    }

    public virtual DataTable getallSalesTaxInvoices()
    {
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetallSalesTaxInvoices").Tables[0];
        return dt;
    }

    public virtual bool DeleteSalesTaxInvoice(int SalesTaxID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@SalesTaxID", SalesTaxID);
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_SCGL_SPDeleteSalesTaxInvoice", param);
        return i > 0;
    }
    public virtual int CountAlreadyExistsSTI(SalesTax_BAL BALSalesTax)
    {
        SqlParameter[] param = {new SqlParameter("@SalesTaxID", BALSalesTax.SalesTaxID)
                                ,new SqlParameter("@SalesTaxInvoiceID", BALSalesTax.SalesTaxInvoiceID)
                               };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_CountAlreadyExistsSTI", param));
    }

}
