using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ReceivePayment_BAL
/// </summary>
public class ReceivePayment_BAL:ReceivePayment_DAL
{

    public int ReceivePayment_ID { get; set; }
    public int SubsidaryID { get; set; }
    public int InvoiceID { get; set; }
    public int CustomerID { get; set; }
    public DateTime PaymentDate { get; set; }
    public string ReferenceNo { get; set; }
    public string Memo { get; set; }
    public bool IsPaymentReceive { get; set; }
    public string Status { get; set; }
    public int LoginID { get; set; }
    public decimal Amount { get; set; }
    public int Currency { get; set; }
    public decimal ConversionRate { get; set; }
    public decimal PKRAmount { get; set; }
    public decimal Total { get; set; }
     public decimal PKRTotal { get; set; }
    public int FinYearID { get; set; }
	public ReceivePayment_BAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public override int CreateModifyReceivePayment(ReceivePayment_BAL BAL, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.CreateModifyReceivePayment(BAL, Trans);
    }
    public override bool CreateModifyReceivePaymentDetail(ReceivePayment_BAL BAL, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.CreateModifyReceivePaymentDetail(BAL, Trans);
    }
    public override bool Delete_ReceivePayment(int ReceivePayment_ID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.Delete_ReceivePayment(ReceivePayment_ID, Trans);
    }
    public override System.Data.DataTable GetDetailReceivePayment(int ReceivePayment_ID)
    {
        return base.GetDetailReceivePayment(ReceivePayment_ID);
    }
    public override System.Data.SqlClient.SqlDataReader UpdateBalance_ByInvoiceID(int InvoiceID, int ReceivePayment_ID)
    {
        return base.UpdateBalance_ByInvoiceID(InvoiceID, ReceivePayment_ID);
    }
    public override bool Delete_ReceivePaymentDetail(int ReceivePayment_ID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.Delete_ReceivePaymentDetail(ReceivePayment_ID, Trans);
    }
    public override int DeleteReceivePaymentTransaction(int ReceivePayment_ID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.DeleteReceivePaymentTransaction(ReceivePayment_ID, Trans);
    }
    public override System.Data.DataTable GetMaxPaymentId()
    {
        return base.GetMaxPaymentId();
    }
    public override System.Data.DataTable getReceivePaymentByCustomerID(int CustomerID)
    {
        return base.getReceivePaymentByCustomerID(CustomerID);
    }
    public override System.Data.DataTable getReceivePaymentByID(int id, int FinYearID)
    {
        return base.getReceivePaymentByID(id, FinYearID);
    }
    public override System.Data.DataTable getReceivePaymentDetailByPaymentID(int ReceivePayment_ID)
    {
        return base.getReceivePaymentDetailByPaymentID(ReceivePayment_ID);
    }
    public override System.Data.DataTable getReceivePaymentDetailByPaymentID_update(int ReceivePayment_ID)
    {
        return base.getReceivePaymentDetailByPaymentID_update(ReceivePayment_ID);
    }
    public override System.Data.DataTable SearchReceivePaymentByCustomerName(string CustomerName, int FinYearID)
    {
        return base.SearchReceivePaymentByCustomerName(CustomerName, FinYearID);
    }
    public override System.Data.DataTable SearchReceivePaymentByInvoiceID(int InvoiceID, int FinYearID)
    {
        return base.SearchReceivePaymentByInvoiceID(InvoiceID, FinYearID);
    }
    public override System.Data.DataTable SearchReceivePaymentByPaymentID(int ReceivePayment_ID, int FinYearID)
    {
        return base.SearchReceivePaymentByPaymentID(ReceivePayment_ID, FinYearID);
    }

    public override bool Update_ReceivePaymentTransaction(int ReceivePayment_ID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.Update_ReceivePaymentTransaction(ReceivePayment_ID, Trans);
    }
}
