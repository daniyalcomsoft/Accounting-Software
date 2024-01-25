using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class InvoiceDetail_BAL:InvoiceDetail_DAL
{
    public int InvoiceDetailID { get; set; }
    public int InvoiceID { get; set; }
    public int ProductServiceID { get; set; }
    public string Description { get; set; }
    public decimal Quantity { get; set; }
    public decimal Rate { get; set; }
    public decimal Amount { get; set; }
	
    public InvoiceDetail_BAL()
	{
	}
    public override System.Data.DataTable CreateModifyInvoiceDetailForm(InvoiceDetail_BAL InvoiceDetailBAL)
    {
        try { return base.CreateModifyInvoiceDetailForm(InvoiceDetailBAL); }
        catch (Exception ex) { throw ex; }
    }
    public override System.Data.DataTable getInvoiceDetailByInvoiceID(int invoiceDetailID)
    {
        try { return base.getInvoiceDetailByInvoiceID(invoiceDetailID); }
        catch (Exception ex) { throw ex; }
    }
    public override System.Data.DataTable getProformaInvoiceDetailByInvoiceID1(int ID)
    {
        return base.getProformaInvoiceDetailByInvoiceID1(ID);
    }
    public override System.Data.DataTable getProformaInvoiceDetailByInvoiceID3(int ID)
    {
        return base.getProformaInvoiceDetailByInvoiceID3(ID);
    }
    public override System.Data.DataTable getProformaInvoiceDetailByInvoiceID4(int ID)
    {
        return base.getProformaInvoiceDetailByInvoiceID4(ID);
    } 
    public override System.Data.DataTable getProformaInvoiceDetailByInvoiceID(int invoiceDetailID)
    {
        return base.getProformaInvoiceDetailByInvoiceID(invoiceDetailID);
    }
    public override int DeleteInvoiceDetail(int InvoiceID)
    {
        try { return base.DeleteInvoiceDetail(InvoiceID); }
        catch (Exception ex) { throw ex; }
    }
    public override System.Data.DataTable getInvoiceDetailByInvoiceID1(int ID)
    {
        return base.getInvoiceDetailByInvoiceID1(ID);
    }

    //Temp Area Code
    public override System.Data.DataTable getInvoiceDetailByInvoiceID3(int ID)
    {
        return base.getInvoiceDetailByInvoiceID3(ID);
    }
    public override System.Data.DataTable getInvoiceDetailByInvoiceID4(int ID)
    {
        return base.getInvoiceDetailByInvoiceID4(ID);
    }
    public override System.Data.DataTable getInvoiceDetailByInvoiceID5(int ID)
    {
        return base.getInvoiceDetailByInvoiceID5(ID);
    }
    public override System.Data.DataTable getInvoiceDetailByInvoiceID6(int ID)
    {
        return base.getInvoiceDetailByInvoiceID6(ID);
    }
    public override System.Data.DataTable getInvoiceDetailByInvoiceID7(int ID)
    {
        return base.getInvoiceDetailByInvoiceID7(ID);
    }
    public override System.Data.DataTable getInvoiceDetailByInvoiceID8(int ID)
    {
        return base.getInvoiceDetailByInvoiceID8(ID);
    }
    public override System.Data.DataTable getInvoiceDetailByInvoiceID9(int ID)
    {
        return base.getInvoiceDetailByInvoiceID9(ID);
    }
    public override System.Data.DataTable getInvoiceDetailByInvoiceID10(int ID)
    {
        return base.getInvoiceDetailByInvoiceID10(ID);
    }

    public override System.Data.DataTable getInvoiceDetailByInvoiceID11(int ID)
    {
        return base.getInvoiceDetailByInvoiceID11(ID);
    }
    public override System.Data.DataTable getInvoiceDetailByInvoiceID12(int ID)
    {
        return base.getInvoiceDetailByInvoiceID12(ID);
    }
    public override System.Data.DataTable getInvoiceDetailByInvoiceID13(int ID)
    {
        return base.getInvoiceDetailByInvoiceID13(ID);
    }
    public override System.Data.DataTable getInvoiceDetailByInvoiceID14(int ID)
    {
        return base.getInvoiceDetailByInvoiceID14(ID);
    }
    public override System.Data.DataTable getInvoiceDetailByInvoiceID15(int ID)
    {
        return base.getInvoiceDetailByInvoiceID15(ID);
    }
}
