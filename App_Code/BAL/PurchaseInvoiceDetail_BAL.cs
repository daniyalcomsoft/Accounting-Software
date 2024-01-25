using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PurchaseInvoiceDetail_BAL
/// </summary>
public class PurchaseInvoiceDetail_BAL:PurchaseInvoiceDetail_DAL
{
    public int pInvoiceDetailID { get; set; }
    public int pInvoiceID { get; set; }
    public int ProductServiceID { get; set; }
    public string Description { get; set; }
    public decimal Quantity { get; set; }
    public decimal Rate { get; set; }
    public decimal Amount { get; set; }
	public PurchaseInvoiceDetail_BAL()
	{	
	}
    public override System.Data.DataTable CreateModifyInvoiceDetailForm(PurchaseInvoiceDetail_BAL pInvoiceDetailBAL)
    {
        return base.CreateModifyInvoiceDetailForm(pInvoiceDetailBAL);
    }
    public override System.Data.DataTable getInvoiceDetailByInvoiceID(int pinvoiceDetailID)
    {
        return base.getInvoiceDetailByInvoiceID(pinvoiceDetailID);
    }

    public override System.Data.DataTable getInvoiceDetailByInvoiceID2(int pinvoiceDetailID)
    {
        return base.getInvoiceDetailByInvoiceID2(pinvoiceDetailID);
    }

    public override System.Data.DataTable getInvoiceDetailByInvoiceID3(int pinvoiceDetailID)
    {
        return base.getInvoiceDetailByInvoiceID3(pinvoiceDetailID);
    }

    public override System.Data.DataTable getInvoiceDetailByInvoiceID4(int pinvoiceDetailID)
    {
        return base.getInvoiceDetailByInvoiceID4(pinvoiceDetailID);
    }

    public override System.Data.DataTable getInvoiceDetailByInvoiceID5(int pinvoiceDetailID)
    {
        return base.getInvoiceDetailByInvoiceID5(pinvoiceDetailID);
    }

    public override System.Data.DataTable getInvoiceDetailByInvoiceID6(int pinvoiceDetailID)
    {
        return base.getInvoiceDetailByInvoiceID6(pinvoiceDetailID);
    }

    public override System.Data.DataTable getInvoiceDetailByInvoiceID7(int pinvoiceDetailID)
    {
        return base.getInvoiceDetailByInvoiceID7(pinvoiceDetailID);
    }

    public override System.Data.DataTable getInvoiceDetailByInvoiceID8(int pinvoiceDetailID)
    {
        return base.getInvoiceDetailByInvoiceID8(pinvoiceDetailID);
    }

    public override System.Data.DataTable getInvoiceDetailByInvoiceID9(int pinvoiceDetailID)
    {
        return base.getInvoiceDetailByInvoiceID9(pinvoiceDetailID);
    }

    public override System.Data.DataTable getInvoiceDetailByInvoiceID10(int pinvoiceDetailID)
    {
        return base.getInvoiceDetailByInvoiceID10(pinvoiceDetailID);
    }

    public override System.Data.DataTable getInvoiceDetailByInvoiceID11(int pinvoiceDetailID)
    {
        return base.getInvoiceDetailByInvoiceID11(pinvoiceDetailID);
    }

    public override System.Data.DataTable getInvoiceDetailByInvoiceID12(int pinvoiceDetailID)
    {
        return base.getInvoiceDetailByInvoiceID12(pinvoiceDetailID);
    }

    public override System.Data.DataTable getInvoiceDetailByInvoiceID13(int pinvoiceDetailID)
    {
        return base.getInvoiceDetailByInvoiceID13(pinvoiceDetailID);
    }

    public override System.Data.DataTable getInvoiceDetailByInvoiceID14(int pinvoiceDetailID)
    {
        return base.getInvoiceDetailByInvoiceID14(pinvoiceDetailID);
    }

    public override System.Data.DataTable getInvoiceDetailByInvoiceID15(int pinvoiceDetailID)
    {
        return base.getInvoiceDetailByInvoiceID15(pinvoiceDetailID);
    }

    public override int DeleteInvoiceDetail(int pInvoiceID)
    {
        return base.DeleteInvoiceDetail(pInvoiceID);
    }
}
