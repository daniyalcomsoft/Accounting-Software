using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SW.SW_Common;
using System.Data;

/// <summary>
/// Summary description for Invoice_DAL_Temp
/// </summary>
public class PurchaseInvoice_DAL_Temp
{
	public PurchaseInvoice_DAL_Temp()
	{
		
	}
    public virtual int CreateModifyInvoice(PurchaseInvoice_BAL_Temp BALInvoice, SqlTransaction Trans)
    {
        SqlParameter[] param2 = {
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
        
        int i = Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, CommandType.StoredProcedure, "vt_SCGL_SpCreateModifyPurchaseInvoice", param2));
        return i;
    }

   
    public virtual bool CreateModifyInvoiceDetail(PurchaseInvoice_BAL_Temp BALInvoice, SqlTransaction Trans)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@pInvoiceID",BALInvoice.InvoiceNo)
                                   ,new SqlParameter("@ProductServiceID",BALInvoice.ProductServiceID)
                                   ,new SqlParameter("@Description",BALInvoice.Description)
                                   ,new SqlParameter("@Quantity",BALInvoice.Quantity)
                                   ,new SqlParameter("@Rate",BALInvoice.Rate)
                                   ,new SqlParameter("@Amount",BALInvoice.Amount)
                                   ,new SqlParameter("@GridName",BALInvoice.GridName)
                                   ,new SqlParameter("@InventoryID",BALInvoice.InventoryID)
                                   ,new SqlParameter("@FishID",BALInvoice.FishID)
                                   ,new SqlParameter("@FishGradeID",BALInvoice.FishGradeID)
                                   ,new SqlParameter("@FishSizeID",BALInvoice.FishSizeID)
                                    ,new SqlParameter("@CostCenterID",BALInvoice.CostCenterID)
                               };
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_CreateModifypInvoiceDetail", param);
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
    public virtual bool DeleteTransaction_pInvoice(int pInvoiceID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@pInvoiceID", pInvoiceID);
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_SCGL_SPDeleteTransaction_pInvoice", param);
        return i > 0;
    }

    public virtual bool Delete_Transaction(int pInvoiceID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@pInvoiceID", pInvoiceID);
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_SCGL_SPDeleteTransaction_purchaseInvoice", param);
        return i > 0;
    }

    public virtual DataTable getallVendorInvoice(int pInvoiceID, int FinYearID)
    {
        SqlParameter[] param = {new SqlParameter("@InvoiceID", pInvoiceID),
                                   new SqlParameter("@FinYearID", FinYearID)
                         
                              };

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetallVendorInvoice", param).Tables[0];
        return dt;
    }


    public virtual DataTable getInvoiceByID(int pinvoiceID, int FinYearID)
    {
        SqlParameter[] param = {new SqlParameter("@pInvoiceID", pinvoiceID),
                               new SqlParameter("@FinYearID", FinYearID)};

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpSearchInvoice_BYVENDORID", param).Tables[0];
        return dt;
    }

    public virtual DataTable getInvntoryByID(int FishID, int FishGradeID, int FishSizeID)
    {
        SqlParameter[] param = {new SqlParameter("@FishId", FishID),
                             new SqlParameter("@GradeId", FishGradeID),
                              new SqlParameter("@SizeId", FishSizeID) };

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SPGetInventoryId_ByFishId_ByGrade_IdBySizeId", param).Tables[0];
        return dt;
    }
    public virtual DataTable getInvoiceByVendorID(int VendorID, int FinYearID)
    {
        SqlParameter[] param = {new SqlParameter("@VendorID", VendorID),
                                new SqlParameter("@FinYearID", FinYearID)};

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetpInvoiceByVendorID", param).Tables[0];
        return dt;
    }

    public virtual DataTable getInvoiceByVendor(string VendorName, int FinYearID)
    {
        SqlParameter[] param = {new SqlParameter("@VendorName", VendorName),
                                   new SqlParameter("@FinYearID", FinYearID)
                         
                              };

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpSearchInvoice_BYVendor", param).Tables[0];
        return dt;
    }

    public virtual int DeleteInvoice(int pInvoiceID)
    {
        SqlParameter[] param = { new SqlParameter("@pInvoiceID", pInvoiceID) };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SPDeletepInvoice", param));
    }
    // overload method for deleting invoice
    public virtual int DeleteInvoice(int pInvoiceID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@pInvoiceID", pInvoiceID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, "vt_SCGL_SPDeletepInvoice", param));
    }


    public virtual SqlDataReader Get_Rows_InvoiceDetail_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetpInvoiceDetailRowNumber_byID", param);
    }
    public virtual SqlDataReader Get_Rows_InvoiceDetail2_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetpInvoiceDetailRowNumber2_byID", param);
    }

    public virtual SqlDataReader Get_Rows_InvoiceDetail3_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetpInvoiceDetailRowNumber3_byID", param);
    }

    public virtual SqlDataReader Get_Rows_InvoiceDetail4_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetpInvoiceDetailRowNumber4_byID", param);
    }
    public virtual SqlDataReader Get_Rows_InvoiceDetail5_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetpInvoiceDetailRowNumber5_byID", param);
    }
    public virtual SqlDataReader Get_Rows_InvoiceDetail6_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetpInvoiceDetailRowNumber6_byID", param);
    }
    public virtual SqlDataReader Get_Rows_InvoiceDetail7_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetpInvoiceDetailRowNumber7_byID", param);
    }
    public virtual SqlDataReader Get_Rows_InvoiceDetail8_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetpInvoiceDetailRowNumber8_byID", param);
    }
    public virtual SqlDataReader Get_Rows_InvoiceDetail9_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetpInvoiceDetailRowNumber9_byID", param);
    }
    public virtual SqlDataReader Get_Rows_InvoiceDetail10_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetpInvoiceDetailRowNumber10_byID", param);
    }
    public virtual SqlDataReader Get_Rows_InvoiceDetail11_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetpInvoiceDetailRowNumber11_byID", param);
    }
    public virtual SqlDataReader Get_Rows_InvoiceDetail12_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetpInvoiceDetailRowNumber12_byID", param);
    }
    public virtual SqlDataReader Get_Rows_InvoiceDetail13_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetpInvoiceDetailRowNumber13_byID", param);
    }
    public virtual SqlDataReader Get_Rows_InvoiceDetail14_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetpInvoiceDetailRowNumber14_byID", param);
    }
    public virtual SqlDataReader Get_Rows_InvoiceDetail15_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetpInvoiceDetailRowNumber15_byID", param);
    }
    public virtual DataTable GetMaxSalesDate(int FinYearID)
    {
        SqlParameter[] param = { new SqlParameter("@FinYearID", FinYearID) };
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SPGetMaxSalesDate", param).Tables[0];
        return dt;
    }

    public virtual DataTable GetMaxExcessShortDate(int FinYearID)
    {
        SqlParameter[] param = { new SqlParameter("@FinYearID", FinYearID) };
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SPGetMaxExcessShortDate", param).Tables[0];
        return dt;
    }

    public virtual DataTable getPurchaseInvoiceDetail(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_sp_GetPurchaseInvoiceDetail", param).Tables[0];
        return dt;
    }
    

}
