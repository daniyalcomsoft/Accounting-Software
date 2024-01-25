using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SW.SW_Common;


public class Invoice_BAL_Temp : Invoice_DAL_Temp
{
    //public string OldProformaNo { get; set; }
    public int InvoiceID { get; set; }
    //public int InvoiceNo { get; set; }
    //public int CustomerID { get; set; }
    public int InvoiceDetailID { get; set; }
    //public string Email { get; set; }
    //public string BillingAddress { get; set; }
    public string TermID { get; set; }
    public DateTime InvoiceDate { get; set; }
    public string ReferenceNo { get; set; }
    public int LoginID { get; set; }
    public decimal TotalByParty { get; set; }
    //public int Currency { get; set; }
    //public decimal ConversionRate { get; set; }
    public decimal TotalByUs { get; set; }
    //public string Invoice_No { get; set; }
    public int FinYearID { get; set; }
    public int JobID { get; set; }
    // for vt_SCGL_InvoiceDetail table
    public int InvoiceDescID { get; set; }
    public string Number { get; set; }
    public string Date { get; set; }
    public decimal ByParty { get; set; }
    public decimal ByUS { get; set; }
    public string BillNumber { get; set; }
    public string CustInvoiceNo { get; set; }
    public bool IsAbbott { get; set; }
    public bool NoAdvance { get; set; }
    //public decimal CustomDuty { get; set; }
   // public decimal SalesTax { get; set; }
    //public decimal IncomeTax { get; set; }
    public string CustomPONo { get; set; }
    public DateTime CustomPODate { get; set; }
    public decimal CustomByParty { get; set; }
    public decimal CustomByUs { get; set; }

    public string SalesTaxPONo { get; set; }
    public decimal SalesTaxFine { get; set; }
    public decimal SalesTaxByParty { get; set; }
    public decimal SalesTaxByUs { get; set; }

    public string IncomeTaxPONo { get; set; }
    public decimal IncomeTaxAddition { get; set; }
    public decimal IncomeTaxByParty { get; set; }
    public decimal IncomeTaxByUs { get; set; }

    public string CEDPercent { get; set; }
    public decimal CEDByParty { get; set; }
    public decimal CEDByUs { get; set; }

    public string EOCPercent { get; set; }
    public decimal EOCByParty { get; set; }
    public decimal EOCByUs { get; set; }

    public string FEDPercent { get; set; }
    public decimal FEDByParty { get; set; }
    public decimal FEDByUs { get; set; }

    public string OthersPercent { get; set; }
    public decimal OthersByParty { get; set; }
    public decimal OthersByUs { get; set; }

    public string ExcessShortDutyPONo { get; set; }
    public decimal ExcessShortDutyByParty { get; set; }
    public decimal ExcessShortDutyByUs { get; set; }

    public int Status { get; set; }
    public string ChequeNo { get; set; }
    public DateTime ReceivedDate { get; set; }

    public string FormENo { get; set; }
    public string Freight { get; set; }
    public decimal NetWeight { get; set; }
    public decimal GrossWeight { get; set; }
    //public string ContainerNo { get; set; }
    public string ProformaNo { get; set; }
    public string Insurance { get; set; }
    //public string GridName { get; set; }
    //public int InventoryID { get; set; }
    //public int FishID { get; set; }
    //public int FishGradeID { get; set; }
    //public int FishSizeID { get; set; }
    //public int CostCenterID { get; set; }
    //public string Origin_Country { get; set; }
    //public string Destination_Country { get; set; }
    public string Exporter { get; set; }
    public string Consignee { get; set; }
    public string Buyer { get; set; }
    public string ExportersRef { get; set; }
    //public string BankDetails { get; set; }
    //public string Quantities { get; set; }
    //public string ShipmentDate { get; set; }
    public string Note { get; set; }

    public bool DeliveryChallan { get; set; }
    public bool LCContract { get; set; }
    public bool Certificates { get; set; }
    public bool PackingList { get; set; }
    public bool Invoice { get; set; }
    public bool WebocGD { get; set; }
    public bool PaccsCoupon { get; set; }
    public bool CashPaymentReceipt { get; set; }
    public bool ExciseDutyChallan { get; set; }
    public bool ExciseTaxChallan { get; set; }
    public bool DeliveryOrderReceipt { get; set; }
    public bool PICTLInvoice { get; set; }
    public bool TrasnportationBill { get; set; }
    public bool GSTInvoice { get; set; }

    public bool ITChallan { get; set; }
    public bool BEImporter { get; set; }
    public bool KPTWharfage { get; set; }
    public bool KPTStorage { get; set; }
    public bool MTOLift { get; set; }
    public bool YardCharges { get; set; }
    public bool EForm { get; set; }
    public bool BLCopy { get; set; }
    public bool AirwaysBL { get; set; }
    public bool InsuranceDoc { get; set; }
    public bool BondPapers { get; set; }
    public bool BEExchange { get; set; }
    public bool Original { get; set; }
    public bool Duplicate { get; set; }
    public bool EndorsmentReceipt { get; set; }
    public bool OtherDocs1 { get; set; }
    public bool OtherDocs2 { get; set; }
    public bool OtherDocs3 { get; set; }
    public bool OtherDocs4 { get; set; }
    public bool OtherDocs5 { get; set; }
    public bool OtherDocs6 { get; set; }
    public bool OtherDocs7 { get; set; }
    public bool OtherDocs8 { get; set; }
    public bool OtherDocs9 { get; set; }
    public bool OtherDocs10 { get; set; }
    public bool OtherDocs11 { get; set; }

    public string OtherDocs1_name { get; set; }
    public string OtherDocs2_name { get; set; }
    public string OtherDocs3_name { get; set; }
    public string OtherDocs4_name { get; set; }
    public string OtherDocs5_name { get; set; }
    public string OtherDocs6_name { get; set; }
    public string OtherDocs7_name { get; set; }
    public string OtherDocs8_name { get; set; }
    public string OtherDocs9_name { get; set; }
    public string OtherDocs10_name { get; set; }
    public string OtherDocs11_name { get; set; }
 
    

	public Invoice_BAL_Temp()
	{       

		//
		// TODO: Add constructor logic here
		//
	}

    public override System.Data.DataTable getInvntoryByID(int FishID, int FishGradeID, int FishSizeID)
    {
        return base.getInvntoryByID(FishID, FishGradeID, FishSizeID);
    }

    public override int CreateModifyInvoice(Invoice_BAL_Temp BALInvoice, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.CreateModifyInvoice(BALInvoice, Trans);
    }
    public override System.Data.DataTable getInvoiceByID(int invoiceID)
    {
        try { return base.getInvoiceByID(invoiceID); }
        catch (Exception ex) { throw ex; }
    }

    public override System.Data.DataTable getInvoiceDetail(int InvoiceID)
    {
        return base.getInvoiceDetail(InvoiceID);
    }

    public override bool GetCOGS(Invoice_BAL_Temp BALInvoice, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.GetCOGS(BALInvoice, Trans);
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
    public override System.Data.DataTable GetMaxProformaInvoiceId()
    {
        return base.GetMaxProformaInvoiceId();
    }
    public override bool Delete_InvoiceDetail(int InvoiceID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.Delete_InvoiceDetail(InvoiceID, Trans);
    }
    public override bool Delete_Transaction(int InvoiceID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.Delete_Transaction(InvoiceID, Trans);
    }
    public override System.Data.DataTable getInvoiceByCustomerID(int CustomerID)
    {
        return base.getInvoiceByCustomerID(CustomerID);
    }
    //public override bool CreateModifyInvoiceDetail(Invoice_BAL_Temp BALInvoice, System.Data.SqlClient.SqlTransaction Trans)
    //{
    //    return base.CreateModifyInvoiceDetail(BALInvoice, Trans);
    //}
    public override System.Data.DataTable getCogs_Rate(Invoice_BAL_Temp BALInvoice, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.getCogs_Rate(BALInvoice, Trans);
    }
    //public override int CreateModifyProformaInvoice(Invoice_BAL_Temp BALInvoice, System.Data.SqlClient.SqlTransaction Trans)
    //{
    //    return base.CreateModifyProformaInvoice(BALInvoice, Trans);
    //}

    public override System.Data.SqlClient.SqlDataReader Get_Rows_InvoiceDetail_byID(int InvoiceID)
    {
        return base.Get_Rows_InvoiceDetail_byID(InvoiceID);
    }
    public override System.Data.SqlClient.SqlDataReader Get_Rows_InvoiceDutiesDetail_byID(int InvoiceID)
    {
        return base.Get_Rows_InvoiceDutiesDetail_byID(InvoiceID);
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
    public override System.Data.DataTable GetMaxPurchaseDate(int FinYearID)
    {
        return base.GetMaxPurchaseDate(FinYearID);
    }

    public override System.Data.DataTable GetMaxExcessShortDate(int FinYearID)
    {
        return base.GetMaxExcessShortDate(FinYearID);
    }

    public override System.Data.DataTable getCurrentInventory(Invoice_BAL_Temp BALInvoice, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.getCurrentInventory(BALInvoice, Trans);
    }

    public override System.Data.DataTable getCurrentSalesforValuation(Invoice_BAL_Temp BALInvoice, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.getCurrentSalesforValuation(BALInvoice, Trans);
    }

    public override bool InsertCurrentSalesforValuation(Invoice_BAL_Temp BALInvoice, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.InsertCurrentSalesforValuation(BALInvoice, Trans);
    }

    public override bool Delete_CurrentSalesforValuation(System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.Delete_CurrentSalesforValuation(Trans);
    }
    //public override bool CreateModifyProformaInvoiceDetail(Invoice_BAL_Temp BALInvoice, System.Data.SqlClient.SqlTransaction Trans)
    //{
    //    return base.CreateModifyProformaInvoiceDetail(BALInvoice, Trans);
    //}
    public override bool Delete_ProformaInvoiceDetail(int InvoiceID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.Delete_ProformaInvoiceDetail(InvoiceID, Trans);
    }
    public override System.Data.SqlClient.SqlDataReader Get_Rows_ProformaInvoiceDetail_byID(int InvoiceID)
    {
        return base.Get_Rows_ProformaInvoiceDetail_byID(InvoiceID);
    }
    public override System.Data.SqlClient.SqlDataReader Get_Rows_ProformaInvoiceDetail10_byID(int InvoiceID)
    {
        return base.Get_Rows_ProformaInvoiceDetail10_byID(InvoiceID);
    }
    public override System.Data.SqlClient.SqlDataReader Get_Rows_ProformaInvoiceDetail11_byID(int InvoiceID)
    {
        return base.Get_Rows_ProformaInvoiceDetail11_byID(InvoiceID);
    }

    public override System.Data.SqlClient.SqlDataReader Get_Rows_ProformaInvoiceDetail12_byID(int InvoiceID)
    {
        return base.Get_Rows_ProformaInvoiceDetail12_byID(InvoiceID);
    }

    public override System.Data.SqlClient.SqlDataReader Get_Rows_ProformaInvoiceDetail13_byID(int InvoiceID)
    {
        return base.Get_Rows_ProformaInvoiceDetail13_byID(InvoiceID);
    }

    public override System.Data.SqlClient.SqlDataReader Get_Rows_ProformaInvoiceDetail14_byID(int InvoiceID)
    {
        return base.Get_Rows_ProformaInvoiceDetail14_byID(InvoiceID);
    }
    public override System.Data.SqlClient.SqlDataReader Get_Rows_ProformaInvoiceDetail15_byID(int InvoiceID)
    {
        return base.Get_Rows_ProformaInvoiceDetail15_byID(InvoiceID);
    }
    public override System.Data.SqlClient.SqlDataReader Get_Rows_ProformaInvoiceDetail2_byID(int InvoiceID)
    {
        return base.Get_Rows_ProformaInvoiceDetail2_byID(InvoiceID);
    }
    public override System.Data.SqlClient.SqlDataReader Get_Rows_ProformaInvoiceDetail3_byID(int InvoiceID)
    {
        return base.Get_Rows_ProformaInvoiceDetail3_byID(InvoiceID);
    }
    public override System.Data.SqlClient.SqlDataReader Get_Rows_ProformaInvoiceDetail4_byID(int InvoiceID)
    {
        return base.Get_Rows_ProformaInvoiceDetail4_byID(InvoiceID);
    }
    public override System.Data.SqlClient.SqlDataReader Get_Rows_ProformaInvoiceDetail5_byID(int InvoiceID)
    {
        return base.Get_Rows_ProformaInvoiceDetail5_byID(InvoiceID);
    }
    public override System.Data.SqlClient.SqlDataReader Get_Rows_ProformaInvoiceDetail6_byID(int InvoiceID)
    {
        return base.Get_Rows_ProformaInvoiceDetail6_byID(InvoiceID);
    }
    public override System.Data.SqlClient.SqlDataReader Get_Rows_ProformaInvoiceDetail7_byID(int InvoiceID)
    {
        return base.Get_Rows_ProformaInvoiceDetail7_byID(InvoiceID);
    }
    public override System.Data.SqlClient.SqlDataReader Get_Rows_ProformaInvoiceDetail8_byID(int InvoiceID)
    {
        return base.Get_Rows_ProformaInvoiceDetail8_byID(InvoiceID);
    }
    public override System.Data.SqlClient.SqlDataReader Get_Rows_ProformaInvoiceDetail9_byID(int InvoiceID)
    {
        return base.Get_Rows_ProformaInvoiceDetail9_byID(InvoiceID);
    }
    public override System.Data.DataTable getProformaInvoiceByID(int invoiceID)
    {
        return base.getProformaInvoiceByID(invoiceID);
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    public override System.Data.DataTable GetInvoiceDetailTable()
    {
        return base.GetInvoiceDetailTable();
    }
    public override System.Data.DataTable getCogs_RateforUpdation(Invoice_BAL_Temp BALInvoice, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.getCogs_RateforUpdation(BALInvoice, Trans);
    }
    public override int ModifyInvoiceDetailCOGS(Invoice_BAL_Temp BALInvoice, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.ModifyInvoiceDetailCOGS(BALInvoice, Trans);
    }
    public override int ModifyGLTransactionCOGS(Invoice_BAL_Temp BALInvoice, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.ModifyGLTransactionCOGS(BALInvoice, Trans);
    }
    public override System.Data.DataTable GetPSCTable()
    {
        return base.GetPSCTable();
    }
    public override int DeleteExcessShortandGLTrans(Invoice_BAL_Temp BALInvoice, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.DeleteExcessShortandGLTrans(BALInvoice, Trans);
    }
    public override System.Data.DataTable getOnHandStock(Invoice_BAL_Temp BALInvoice, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.getOnHandStock(BALInvoice, Trans);
    }
    public override System.Data.DataTable GetLastExcessShortID(System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.GetLastExcessShortID(Trans);
    }
    public override int CreateModifyExcessShort(Invoice_BAL_Temp BALInvoice, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.CreateModifyExcessShort(BALInvoice, Trans);
    }
    public override System.Data.DataTable getProformaInvoiceDetail(int InvoiceID)
    {
        return base.getProformaInvoiceDetail(InvoiceID);
    }
}


