using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for SalesTax_BAL
/// </summary>
public class SalesTax_BAL : SalesTax_DAL
{
    public int SalesTaxID { get; set; }
    public int SalesTaxInvoiceID { get; set; }
    public string SalesTax { get; set; }
    public string YearFrom { get; set; }
    public string YearTo { get; set; }

    //Sales Tax Invoice
    public DateTime Date { get; set; }
    public int JobID { get; set; }
    public string Packages { get; set; }
    public string RefNo { get; set; }
    public decimal ServiceCharges { get; set; }
    public decimal OUE { get; set; }
    public decimal AmtAT { get; set; }

	public SalesTax_BAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public override DataTable getSalesTax()
    {
        return base.getSalesTax();
    }
    public override SalesTax_BAL GetSalesTaxByID(int SalesTaxID)
    {
        return base.GetSalesTaxByID(SalesTaxID);
    }
    public override string DeleteSalesTax(int SalesTaxID, string StartDate, string EndDate)
    {
        return base.DeleteSalesTax(SalesTaxID, StartDate, EndDate);
    }
    public override int CreateModifySalesTax(SalesTax_BAL FY, SCGL_Session SBO)
    {
        return base.CreateModifySalesTax(FY, SBO);
    }
    public override int CountSalesTaxOverlapPeriods(int SalesTaxID, DateTime StartDate, DateTime EndDate)
    {
        return base.CountSalesTaxOverlapPeriods(SalesTaxID, StartDate, EndDate);
    }
    public override int CreateModifySalesTaxInvoice(SalesTax_BAL BALSalesTax, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.CreateModifySalesTaxInvoice(BALSalesTax, Trans);
    }
    public override bool CreateSalesTaxInvoiceGLTrans(SalesTax_BAL BALSalesTax, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.CreateSalesTaxInvoiceGLTrans(BALSalesTax, Trans);
    }
    public override int DeleteTransaction_SalesTaxInvoice(int SalesTaxInvoiceID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.DeleteTransaction_SalesTaxInvoice(SalesTaxInvoiceID, Trans);
    }
    public override DataTable getSalesTaxInvoiceByID(int SalesTaxID)
    {
        return base.getSalesTaxInvoiceByID(SalesTaxID);
    }
    public override DataTable getallSalesTaxInvoices()
    {
        return base.getallSalesTaxInvoices();
    }
    public override bool DeleteSalesTaxInvoice(int SalesTaxID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.DeleteSalesTaxInvoice(SalesTaxID, Trans);
    }
    public override int CountAlreadyExistsSTI(SalesTax_BAL BALSalesTax)
    {
        return base.CountAlreadyExistsSTI(BALSalesTax);
    }
}
