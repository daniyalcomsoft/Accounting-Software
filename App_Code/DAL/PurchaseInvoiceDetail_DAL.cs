using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SW.SW_Common;

/// <summary>
/// Summary description for PurchaseInvoiceDetail_DAL
/// </summary>
public class PurchaseInvoiceDetail_DAL
{
	public PurchaseInvoiceDetail_DAL()
	{
		
	}
    public virtual DataTable CreateModifyInvoiceDetailForm(PurchaseInvoiceDetail_BAL PurchaseInvoiceDetailBAL)
    {
        SqlParameter[] param = {    
                                   new SqlParameter("@DetailID", PurchaseInvoiceDetailBAL.pInvoiceDetailID)
                                   ,new SqlParameter("@ProductServiceID",PurchaseInvoiceDetailBAL.ProductServiceID)
                                   ,new SqlParameter("@Description",PurchaseInvoiceDetailBAL.Description)
                                   ,new SqlParameter("@Quantity",PurchaseInvoiceDetailBAL.Quantity)
                                   ,new SqlParameter("@Rate",PurchaseInvoiceDetailBAL.Rate)
                                   ,new SqlParameter("@Amount",PurchaseInvoiceDetailBAL.Amount)
                                   ,new SqlParameter("@pInvoiceID",PurchaseInvoiceDetailBAL.pInvoiceID)
                               };
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpCreateModify_pInvoiceDetail", param).Tables[0];
        return dt;
    }

    public virtual DataTable getInvoiceDetailByInvoiceID(int pinvoiceDetailID)
    {
        SqlParameter param = new SqlParameter("@pInvoiceID", pinvoiceDetailID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetPurchaseInvoiceDetail", param).Tables[0];
        return dt;
    }

    public virtual DataTable getInvoiceDetailByInvoiceID2(int pinvoiceDetailID)
    {
        SqlParameter param = new SqlParameter("@pInvoiceID", pinvoiceDetailID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetPurchaseInvoiceDetail2", param).Tables[0];
        return dt;
    }

    public virtual DataTable getInvoiceDetailByInvoiceID3(int pinvoiceDetailID)
    {
        SqlParameter param = new SqlParameter("@pInvoiceID", pinvoiceDetailID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetPurchaseInvoiceDetail3", param).Tables[0];
        return dt;
    }

    public virtual DataTable getInvoiceDetailByInvoiceID4(int pinvoiceDetailID)
    {
        SqlParameter param = new SqlParameter("@pInvoiceID", pinvoiceDetailID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetPurchaseInvoiceDetail4", param).Tables[0];
        return dt;
    }

    public virtual DataTable getInvoiceDetailByInvoiceID5(int pinvoiceDetailID)
    {
        SqlParameter param = new SqlParameter("@pInvoiceID", pinvoiceDetailID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetPurchaseInvoiceDetail5", param).Tables[0];
        return dt;
    }

    public virtual DataTable getInvoiceDetailByInvoiceID6(int pinvoiceDetailID)
    {
        SqlParameter param = new SqlParameter("@pInvoiceID", pinvoiceDetailID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetPurchaseInvoiceDetail6", param).Tables[0];
        return dt;
    }

    public virtual DataTable getInvoiceDetailByInvoiceID7(int pinvoiceDetailID)
    {
        SqlParameter param = new SqlParameter("@pInvoiceID", pinvoiceDetailID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetPurchaseInvoiceDetail7", param).Tables[0];
        return dt;
    }

    public virtual DataTable getInvoiceDetailByInvoiceID8(int pinvoiceDetailID)
    {
        SqlParameter param = new SqlParameter("@pInvoiceID", pinvoiceDetailID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetPurchaseInvoiceDetail8", param).Tables[0];
        return dt;
    }

    public virtual DataTable getInvoiceDetailByInvoiceID9(int pinvoiceDetailID)
    {
        SqlParameter param = new SqlParameter("@pInvoiceID", pinvoiceDetailID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetPurchaseInvoiceDetail9", param).Tables[0];
        return dt;
    }

    public virtual DataTable getInvoiceDetailByInvoiceID10(int pinvoiceDetailID)
    {
        SqlParameter param = new SqlParameter("@pInvoiceID", pinvoiceDetailID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetPurchaseInvoiceDetail10", param).Tables[0];
        return dt;
    }

    public virtual DataTable getInvoiceDetailByInvoiceID11(int pinvoiceDetailID)
    {
        SqlParameter param = new SqlParameter("@pInvoiceID", pinvoiceDetailID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetPurchaseInvoiceDetail11", param).Tables[0];
        return dt;
    }

    public virtual DataTable getInvoiceDetailByInvoiceID12(int pinvoiceDetailID)
    {
        SqlParameter param = new SqlParameter("@pInvoiceID", pinvoiceDetailID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetPurchaseInvoiceDetail12", param).Tables[0];
        return dt;
    }

    public virtual DataTable getInvoiceDetailByInvoiceID13(int pinvoiceDetailID)
    {
        SqlParameter param = new SqlParameter("@pInvoiceID", pinvoiceDetailID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetPurchaseInvoiceDetail13", param).Tables[0];
        return dt;
    }

    public virtual DataTable getInvoiceDetailByInvoiceID14(int pinvoiceDetailID)
    {
        SqlParameter param = new SqlParameter("@pInvoiceID", pinvoiceDetailID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetPurchaseInvoiceDetail14", param).Tables[0];
        return dt;
    }

    public virtual DataTable getInvoiceDetailByInvoiceID15(int pinvoiceDetailID)
    {
        SqlParameter param = new SqlParameter("@pInvoiceID", pinvoiceDetailID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetPurchaseInvoiceDetail15", param).Tables[0];
        return dt;
    }

    public virtual int DeleteInvoiceDetail(int pInvoiceID)
    {
        SqlParameter[] param = { new SqlParameter("@pInvoiceID", pInvoiceID) };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SPDeleteInvoiceDetail", param));
    }
}
