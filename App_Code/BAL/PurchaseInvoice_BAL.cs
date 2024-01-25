using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class PurchaseInvoice_BAL:PurchaseInvoice_DAL
{
    public int pInvoiceID { get; set; }
    public int InvoiceNo { get; set; }
    public int VendorID { get; set; }
    public string Email { get; set; }
    public string BillingAddress { get; set; }
    public int TermID { get; set; }
    public DateTime InvoiceDate { get; set; }
    public DateTime DueDate { get; set; }
    public int DiscountID { get; set; }
    public decimal Discount { get; set; }
    public string PrintMessage { get; set; }
    public string StatementMemo { get; set; }
    public int LoginID { get; set; }
    public decimal Total { get; set; }
    public string Invoice_No { get; set; }
    public int FinYearID { get; set; }
    // for vt_SCGL_pInvoiceDetail table
    public int ProductServiceID { get; set; }
    public string Description { get; set; }
    public decimal Quantity { get; set; }
    public decimal Rate { get; set; }
    public decimal Amount { get; set; }
    public int CostCenterID { get; set; }
    

	public PurchaseInvoice_BAL()
	{
	}
    public override bool CreateModifyInvoice(PurchaseInvoice_BAL BALInvoice, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.CreateModifyInvoice(BALInvoice, Trans);
    }
    public override bool CreateModifyInvoiceDetail(PurchaseInvoice_BAL BALInvoice, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.CreateModifyInvoiceDetail(BALInvoice, Trans);
    }
    public override bool Delete_InvoiceDetail(int pInvoiceID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.Delete_InvoiceDetail(pInvoiceID, Trans);
    }
    public override int DeleteInvoice(int pInvoiceID)
    {
        return base.DeleteInvoice(pInvoiceID);
    }
    // overload method for delete in transaction
    public override int DeleteInvoice(int pInvoiceID, System.Data.SqlClient.SqlTransaction trans)
    {
        return base.DeleteInvoice(pInvoiceID, trans);
    }
    public override System.Data.DataTable getInvoiceByID(int pinvoiceID, int FinYearID)
    {
        return base.getInvoiceByID(pinvoiceID, FinYearID);
    }

    public override System.Data.DataTable getallVendorInvoice(int pInvoiceID, int FinYearID)
    {
        return base.getallVendorInvoice(pInvoiceID, FinYearID);
    }

    public override System.Data.DataTable getInvoiceByVendor(string VendorName, int FinYearID)
    {
        return base.getInvoiceByVendor(VendorName, FinYearID);
    }
    public override System.Data.DataTable getInvoiceByVendorID(int VendorID, int FinYearID)
    {
        return base.getInvoiceByVendorID(VendorID, FinYearID);
    }
    public override System.Data.DataTable GetMaxInvoiceId()
    {
        return base.GetMaxInvoiceId();
    }
    public override int DeleteTransaction_pInvoice(int pInvoiceID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.DeleteTransaction_pInvoice(pInvoiceID, Trans);
    }
}
