using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Invoice_BAL:Invoice_DAL
{
    public string OldProformaNo { get; set; }
    public int InvoiceID { get; set; }
    public int ShowInvoiceID { get; set; }
    public int InvoiceNo { get; set; }
    public int CustomerID { get; set; }
    public string Email { get; set; }
    public string BillingAddress { get; set; }
    public string TermID { get; set; }
    public DateTime InvoiceDate { get; set; }
    public DateTime DueDate { get; set; }
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
    public string From { get; set; }
    public string To { get; set; }
    public string Vessel { get; set; }
    public string FormENo { get; set; }
    public string Freight{ get; set; }
    public decimal NetWeight { get; set; }
    public decimal GrossWeight { get; set; }
    public string ContainerNo { get; set; }
    public string ProformaNo { get; set; }
    public string Insurance { get; set; }
    public string GridName { get; set; }
    public int InventoryID { get; set; }
    public int FishID { get; set; }
    public int FishGradeID { get; set; }
    public int FishSizeID { get; set; }

    public string Origin_Country { get; set; }
    public string Destination_Country { get; set; }
    public string Exporter { get; set; }
    public string Consignee { get; set; }
    public string Buyer { get; set; }
    public string ExportersRef { get; set; }
    public string Note { get; set; }



    public override int DeleteTransaction_Invoice(int InvoiceID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.DeleteTransaction_Invoice(InvoiceID, Trans);
    }
    public Invoice_BAL()
	{		
	}
    public override System.Data.DataTable getallInvoice(int InvoiceID, int FinYearID)
    {
        return base.getallInvoice(InvoiceID, FinYearID);
    }
    public override System.Data.DataTable GetGraph(string StartDate, string EndDate)
    {
        return base.GetGraph(StartDate, EndDate);
    }
    public override System.Data.DataTable GetGraph_ByDate(string StartDate, string EndDate)
    {
        return base.GetGraph_ByDate(StartDate, EndDate);
    }
    public override System.Data.DataTable GetAllBanks(int FinID, string YearFrom, string YearTo)
    {
        return base.GetAllBanks(FinID, YearFrom, YearTo);
    }
    public override System.Data.DataTable GetLastDaysReceipts()
    {
        return base.GetLastDaysReceipts();
    }
    public override System.Data.DataTable GetGraph_ByDate_2(string StartDate, string EndDate)
    {
        return base.GetGraph_ByDate_2(StartDate, EndDate);
    }
    public override System.Data.DataTable GetTotalSales_Graph(string StartDate, string EndDate)
    {
        return base.GetTotalSales_Graph(StartDate, EndDate);
    }
    public override System.Data.DataTable GetTotalEquity_Graph(string StartDate, string EndDate)
    {
        return base.GetTotalEquity_Graph(StartDate, EndDate);
    }
    public override System.Data.DataTable GetTotalMargin_Graph(string StartDate, string EndDate)
    {
        return base.GetTotalMargin_Graph(StartDate, EndDate);
    }
    public override System.Data.DataTable GetTotalIncome_Graph(string StartDate, string EndDate)
    {
        return base.GetTotalIncome_Graph(StartDate, EndDate);
    }
    public override System.Data.DataTable GetTotalBalance_Graph()
    {
        return base.GetTotalBalance_Graph();
    }
    public override System.Data.DataTable GetTotalSales_Temp(string StartDate, string EndDate)
    {
        return base.GetTotalSales_Temp(StartDate, EndDate);
    }
    public override System.Data.DataTable GetTotalExpense_Graph(string StartDate, string EndDate)
    {
        return base.GetTotalExpense_Graph(StartDate, EndDate);
    }
    public override System.Data.DataTable GetPasniProduction()
    {
        return base.GetPasniProduction();
    }
    public override System.Data.DataTable GetGwadarGraph_ByDate()
    {
        return base.GetGwadarGraph_ByDate();
    }
    public override System.Data.DataTable GetKarachiGraph_ByDate()
    {
        return base.GetKarachiGraph_ByDate();
    }
    public override System.Data.DataTable GetPasniGraph_ByDate()
    {
        return base.GetPasniGraph_ByDate();
    }
    public override System.Data.DataTable GetCurrentFisYear(int FinYearID)
    {
        return base.GetCurrentFisYear(FinYearID);
    }
    public override System.Data.DataTable getallProformaInvoice(int InvoiceID, int FinYearID)
    {
        return base.getallProformaInvoice(InvoiceID, FinYearID);
    }
    public override System.Data.DataTable getInvntoryByID(int FishID, int FishGradeID, int FishSizeID)
    {
        return base.getInvntoryByID(FishID, FishGradeID, FishSizeID);
    }

    public override bool CreateModifyInvoice(Invoice_BAL BALInvoice, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.CreateModifyInvoice(BALInvoice, Trans);
    }
    public override System.Data.DataTable getInvoiceByCustomer(string CustomerName, int FinYearID)
    {
        return base.getInvoiceByCustomer(CustomerName, FinYearID);
    }
    public override System.Data.DataTable getProformaInvoiceByCustomer(string CustomerName, int FinYearID)
    {
        return base.getProformaInvoiceByCustomer(CustomerName, FinYearID);
    }
    public override System.Data.DataTable getInvoiceByID(int InvoiceID, int FinYearID)
    {
        return base.getInvoiceByID(InvoiceID, FinYearID);
    }

    public override System.Data.DataTable searchInvoice2()
    {
        return base.searchInvoice2();
    }
    public override System.Data.DataTable getProformaInvoiceByID(int InvoiceID, int FinYearID)
    {
        return base.getProformaInvoiceByID(InvoiceID, FinYearID);
    }

    public override int DeleteInvoice(int InvoiceID)
    {
        try { return base.DeleteInvoice(InvoiceID); }
        catch (Exception ex) { throw ex; }
    }
    // overload method for deleting invoice
    public override int DeleteInvoice(int InvoiceID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.DeleteInvoice(InvoiceID, Trans);
    }
    public override System.Data.DataTable GetMaxInvoiceId()
    {
        return base.GetMaxInvoiceId();
    }
    public override bool Delete_InvoiceDetail(int InvoiceID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.Delete_InvoiceDetail(InvoiceID, Trans);
    }
    public override System.Data.DataTable getInvoiceByCustomerID(int CustomerID, int FinYearID)
    {
        return base.getInvoiceByCustomerID(CustomerID, FinYearID);
    }
    public override bool CreateModifyInvoiceDetail(Invoice_BAL BALInvoice, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.CreateModifyInvoiceDetail(BALInvoice, Trans);
    }

    public override System.Data.SqlClient.SqlDataReader Get_Rows_InvoiceDetail_byID(int InvoiceID)
    {
        return base.Get_Rows_InvoiceDetail_byID(InvoiceID);
    }

    public override System.Data.SqlClient.SqlDataReader Get_Rows_InvoiceDetail2_byID(int InvoiceID)
    {
        return base.Get_Rows_InvoiceDetail2_byID(InvoiceID);
    }
    public override System.Data.DataTable GetGwadarProduction()
    {
        return base.GetGwadarProduction();
    }
    public override System.Data.DataTable GetKarachiProduction()
    {
        return base.GetKarachiProduction();
    }
    public override System.Data.DataTable GetPasniPurchasesInKg()
    {
        return base.GetPasniPurchasesInKg();
    }
    public override System.Data.DataTable GetGwadarPurchasesInKg()
    {
        return base.GetGwadarPurchasesInKg();
    }
    public override System.Data.DataTable GetKarachiPurchasesInKg()
    {
        return base.GetKarachiPurchasesInKg();
    }
}
