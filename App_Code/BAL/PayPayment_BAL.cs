using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class PayPayment_BAL : PayPayment_DAL
{
    public int PayPayment_ID { get; set; }
    public int SubsidaryID { get; set; }
    public int pInvoiceID { get; set; }
    public int VendorID { get; set; }
    public DateTime PaymentDate { get; set; }
    public string ReferenceNo { get; set; }
    public string Memo { get; set; }
    public bool IsPaymentReceive { get; set; }
    public string Status { get; set; }
    public int LoginID { get; set; }
    public decimal Amount { get; set; }
    public int FinYearID { get; set; }
    public decimal Total { get; set; }

    public PayPayment_BAL()
	{
	}

    public override int CreateModifyPayPayment(PayPayment_BAL BAL, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.CreateModifyPayPayment(BAL, Trans);
    }

    public override System.Data.DataTable getPayPaymentByID(int id, int FinYearID)
    {

        try { return base.getPayPaymentByID(id, FinYearID); }
        catch (Exception ex) { throw ex; } 
    }

    public override System.Data.DataTable GetMaxPayPaymentId()
    {
        return base.GetMaxPayPaymentId();
    }
    public override bool Delete_PayPaymentDetail(int PayPayment_ID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.Delete_PayPaymentDetail(PayPayment_ID, Trans);
    }

    public override System.Data.DataTable getPayPaymentByVendorID(int VendorID)
    {
        return base.getPayPaymentByVendorID(VendorID);
    }

    public override System.Data.DataTable SearchPayPaymentByVendorName(string VendorName, int FinYearID)
    {
        return base.SearchPayPaymentByVendorName(VendorName, FinYearID);
    }

    public override System.Data.DataTable SearchPayPaymentByInvoiceID(int InvoiceID, int FinYearID)
    {
        return base.SearchPayPaymentByInvoiceID(InvoiceID, FinYearID);
    }

    public override System.Data.DataTable SearchPayPaymentByPaymentID(int PayPayment_ID, int FinYearID)
    {
        return base.SearchPayPaymentByPaymentID(PayPayment_ID, FinYearID);
    }

    public override System.Data.DataTable getPayPaymentDetailByPaymentID(int PayPayment_ID)
    {
        return base.getPayPaymentDetailByPaymentID(PayPayment_ID);
    }

    public override bool CreateModifyPayPaymentDetail(PayPayment_BAL BAL, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.CreateModifyPayPaymentDetail(BAL, Trans);
    }

    public override System.Data.DataTable GetDetailPayPayment(int PayPayment_ID)
    {
        return base.GetDetailPayPayment(PayPayment_ID);
    }

    public override System.Data.SqlClient.SqlDataReader UpdateBalance_ByInvoiceID(int InvoiceID, int PayPayment_ID)
    {
        return base.UpdateBalance_ByInvoiceID(InvoiceID, PayPayment_ID);
    }

    public override int DeletePayPaymentTransaction(int PayPayment_ID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.DeletePayPaymentTransaction(PayPayment_ID, Trans);
    }

    public override bool Update_PayPaymentTransaction(int PayPayment_ID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.Update_PayPaymentTransaction(PayPayment_ID, Trans);
    }
}
