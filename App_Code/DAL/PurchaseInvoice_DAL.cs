using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SW.SW_Common;
using System.Data;

public class PurchaseInvoice_DAL
{
	public PurchaseInvoice_DAL()
	{
    }

    public virtual bool CreateModifyInvoice(PurchaseInvoice_BAL BALInvoice, SqlTransaction Trans)
    {
        SqlParameter[] param = {
                                   new SqlParameter("@pInvoiceID", BALInvoice.pInvoiceID)
                                   ,new SqlParameter("@VendorID",BALInvoice.VendorID)
                                   ,new SqlParameter("@Email",BALInvoice.Email)
                                   ,new SqlParameter("@BillingAddress",BALInvoice.BillingAddress)
                                   ,new SqlParameter("@TermID",BALInvoice.TermID)
                                   ,new SqlParameter("@InvoiceDate",BALInvoice.InvoiceDate)
                                   ,new SqlParameter("@DueDate",BALInvoice.DueDate)
                                   ,new SqlParameter("@Discount",BALInvoice.Discount)
                                   ,new SqlParameter("@PrintMessage",BALInvoice.PrintMessage)
                                   ,new SqlParameter("@StatementMemo",BALInvoice.StatementMemo)
                                   ,new SqlParameter("@LoginID",BALInvoice.LoginID)
                                   ,new SqlParameter("@Total",BALInvoice.Total)
                                   ,new SqlParameter("@InvoiceNo",BALInvoice.Invoice_No)
                                   ,new SqlParameter("@FinYearID",BALInvoice.FinYearID)
                                   
                               };
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_SCGL_SpCreateModifyPurchaseInvoice", param);
        return i > 0;
    }
    
    public virtual bool CreateModifyInvoiceDetail(PurchaseInvoice_BAL BALInvoice, SqlTransaction Trans)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@pInvoiceID",BALInvoice.InvoiceNo)
                                   ,new SqlParameter("@ProductServiceID",BALInvoice.ProductServiceID)
                                   //,new SqlParameter("@Description",BALInvoice.Description)
                                   ,new SqlParameter("@Quantity",BALInvoice.Quantity)
                                   ,new SqlParameter("@Rate",BALInvoice.Rate)
                                   ,new SqlParameter("@Amount",BALInvoice.Amount)
                                   ,new SqlParameter("@CostCenterID",BALInvoice.CostCenterID)
                               };
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_CreateModifyPurchaseInvoiceDetail", param);
        return i > 0;
    }

    public virtual DataTable GetMaxInvoiceId()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_sp_getmax_pInvoiceID_I").Tables[0];
    }



    public virtual bool Delete_InvoiceDetail(int pInvoiceID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@pInvoiceID", pInvoiceID);
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_Delete_pInvoiceDetail", param);
        return i > 0;
    }



    public virtual DataTable getInvoiceByID(int pinvoiceID,int FinYearID)
    {
        SqlParameter[] param = {new SqlParameter("@pInvoiceID", pinvoiceID),
                               new SqlParameter("@FinYearID", FinYearID)};

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpSearchInvoice_BYVENDORID", param).Tables[0];
        return dt;
    }

    public virtual DataTable getallVendorInvoice(int pInvoiceID,int FinYearID)
    {
        SqlParameter[] param = {new SqlParameter("@InvoiceID", pInvoiceID),
                                   new SqlParameter("@FinYearID", FinYearID)};

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetallVendorInvoice", param).Tables[0];
        return dt;
    }

    public virtual DataTable getInvoiceByVendorID(int VendorID,int FinYearID)
    {
        SqlParameter[] param = {new SqlParameter("@VendorID", VendorID),
                                new SqlParameter("@FinYearID", FinYearID)};

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetpInvoiceByVendorID", param).Tables[0];
        return dt;
    }
    public virtual int DeleteInvoice(int pInvoiceID)
    {
        SqlParameter param = new SqlParameter("@pInvoiceID", pInvoiceID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SPDeletepInvoice", param));
    }

    public virtual DataTable getInvoiceByVendor(string VendorName,int FinYearID)
    {
        SqlParameter[] param = {new SqlParameter("@VendorName", VendorName),
                                   new SqlParameter("@FinYearID", FinYearID)
                         
                              };

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpSearchInvoice_BYVendor", param).Tables[0];
        return dt;
    }

    // overload method for delete in transaction
    public virtual int DeleteInvoice(int pInvoiceID, SqlTransaction trans)
    {
        SqlParameter param = new SqlParameter("@pInvoiceID", pInvoiceID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(trans, "vt_SCGL_SPDeletepInvoice", param));
    }
    public virtual DataTable getpInvoiceDetailByInvoiceID(int pinvoiceDetailID)
    {
        SqlParameter param = new SqlParameter("@pInvoiceID", pinvoiceDetailID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetpInvoiceDetail", param).Tables[0];
        return dt;
    }
    public virtual int DeleteTransaction_pInvoice(int pInvoiceID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@pInvoiceID", pInvoiceID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, "vt_SCGL_SPDeleteTransaction_pInvoice", param));
    }
}
