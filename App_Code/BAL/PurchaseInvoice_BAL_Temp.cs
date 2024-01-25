using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class PurchaseInvoice_BAL_Temp : PurchaseInvoice_DAL_Temp
{
    public int pInvoiceID { get; set; }
    public int InvoiceNo { get; set; }
    public int VendorID { get; set; }
    public string Email { get; set; }
    public string BillingAddress { get; set; }
    public int TermID { get; set; }
    public DateTime InvoiceDate { get; set; }
    public DateTime DueDate { get; set; }
    public string PrintMessage { get; set; }
    public string StatementMemo { get; set; }
    public int LoginID { get; set; }
    public decimal Total { get; set; }
    public string Invoice_No { get; set; }
    public int FinYearID { get; set; }
    // for vt_SCGL_InvoiceDetail table
    public int ProductServiceID { get; set; }
    public string Description { get; set; }
    public decimal Quantity { get; set; }
    public decimal Rate { get; set; }
    public decimal Amount { get; set; }
    public decimal PKRAmount { get; set; }
    public decimal Discount { get; set; }
    public int Currency { get; set; }
    public decimal ConversionRate { get; set; }
    public decimal PKRTotal { get; set; }
    public decimal Description2 { get; set; }
    public string GridName { get; set; }
    public int InventoryID { get; set; }
    public int FishID { get; set; }
    public int FishGradeID { get; set; }
    public int FishSizeID { get; set; }
    public int CostCenterID { get; set; }
   
    public PurchaseInvoice_BAL_Temp()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public override int CreateModifyInvoice(PurchaseInvoice_BAL_Temp BALInvoice, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.CreateModifyInvoice(BALInvoice, Trans);
    }

    public override bool CreateModifyInvoiceDetail(PurchaseInvoice_BAL_Temp BALInvoice, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.CreateModifyInvoiceDetail(BALInvoice, Trans);
    }

    public override bool Delete_InvoiceDetail(int pInvoiceID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.Delete_InvoiceDetail(pInvoiceID, Trans);
    }

    public override bool DeleteTransaction_pInvoice(int pInvoiceID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.DeleteTransaction_pInvoice(pInvoiceID, Trans);
    }

    public override bool Delete_Transaction(int pInvoiceID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.Delete_Transaction(pInvoiceID, Trans);
    }

    public override int DeleteInvoice(int InvoiceID)
    {
        return base.DeleteInvoice(InvoiceID);
    }

    public override int DeleteInvoice(int InvoiceID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.DeleteInvoice(InvoiceID, Trans);
    }

    public override System.Data.DataTable getInvntoryByID(int FishID, int FishGradeID, int FishSizeID)
    {
        return base.getInvntoryByID(FishID, FishGradeID, FishSizeID);
    }

    public override System.Data.DataTable getInvoiceByVendorID(int VendorID, int FinYearID)
    {
        return base.getInvoiceByVendorID(VendorID, FinYearID);
    }

    public override System.Data.DataTable getInvoiceByID(int pinvoiceID, int FinYearID)
    {
        return base.getInvoiceByID(pinvoiceID, FinYearID);
    }

    public override System.Data.DataTable GetMaxInvoiceId()
    {
        return base.GetMaxInvoiceId();
    }

    public override System.Data.SqlClient.SqlDataReader Get_Rows_InvoiceDetail_byID(int InvoiceID)
    {
        return base.Get_Rows_InvoiceDetail_byID(InvoiceID);
    }

    public override System.Data.SqlClient.SqlDataReader Get_Rows_InvoiceDetail2_byID(int InvoiceID)
    {
        return base.Get_Rows_InvoiceDetail2_byID(InvoiceID);
    }

    public override System.Data.SqlClient.SqlDataReader Get_Rows_InvoiceDetail3_byID(int InvoiceID)
    {
        return base.Get_Rows_InvoiceDetail3_byID(InvoiceID);
    }

    public override System.Data.SqlClient.SqlDataReader Get_Rows_InvoiceDetail4_byID(int InvoiceID)
    {
        return base.Get_Rows_InvoiceDetail4_byID(InvoiceID);
    }

    public override System.Data.SqlClient.SqlDataReader Get_Rows_InvoiceDetail5_byID(int InvoiceID)
    {
        return base.Get_Rows_InvoiceDetail5_byID(InvoiceID);
    }

    public override System.Data.SqlClient.SqlDataReader Get_Rows_InvoiceDetail6_byID(int InvoiceID)
    {
        return base.Get_Rows_InvoiceDetail6_byID(InvoiceID);
    }

    public override System.Data.SqlClient.SqlDataReader Get_Rows_InvoiceDetail7_byID(int InvoiceID)
    {
        return base.Get_Rows_InvoiceDetail7_byID(InvoiceID);
    }

    public override System.Data.SqlClient.SqlDataReader Get_Rows_InvoiceDetail8_byID(int InvoiceID)
    {
        return base.Get_Rows_InvoiceDetail8_byID(InvoiceID);
    }

    public override System.Data.SqlClient.SqlDataReader Get_Rows_InvoiceDetail9_byID(int InvoiceID)
    {
        return base.Get_Rows_InvoiceDetail9_byID(InvoiceID);
    }

    public override System.Data.SqlClient.SqlDataReader Get_Rows_InvoiceDetail10_byID(int InvoiceID)
    {
        return base.Get_Rows_InvoiceDetail10_byID(InvoiceID);
    }

    public override System.Data.SqlClient.SqlDataReader Get_Rows_InvoiceDetail11_byID(int InvoiceID)
    {
        return base.Get_Rows_InvoiceDetail11_byID(InvoiceID);
    }

    public override System.Data.SqlClient.SqlDataReader Get_Rows_InvoiceDetail12_byID(int InvoiceID)
    {
        return base.Get_Rows_InvoiceDetail12_byID(InvoiceID);
    }

    public override System.Data.SqlClient.SqlDataReader Get_Rows_InvoiceDetail13_byID(int InvoiceID)
    {
        return base.Get_Rows_InvoiceDetail13_byID(InvoiceID);
    }

    public override System.Data.SqlClient.SqlDataReader Get_Rows_InvoiceDetail14_byID(int InvoiceID)
    {
        return base.Get_Rows_InvoiceDetail14_byID(InvoiceID);
    }

    public override System.Data.SqlClient.SqlDataReader Get_Rows_InvoiceDetail15_byID(int InvoiceID)
    {
        return base.Get_Rows_InvoiceDetail15_byID(InvoiceID);
    }

    public override System.Data.DataTable GetMaxSalesDate(int FinYearID)
    {
        return base.GetMaxSalesDate(FinYearID);
    }

    public override System.Data.DataTable GetMaxExcessShortDate(int FinYearID)
    {
        return base.GetMaxExcessShortDate(FinYearID);
    }

    public override System.Data.DataTable getPurchaseInvoiceDetail(int InvoiceID)
    {
        return base.getPurchaseInvoiceDetail(InvoiceID);
    }
}

