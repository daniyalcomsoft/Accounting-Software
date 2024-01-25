using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CustomerForm_BAL
/// </summary>
public class CustomerForm_BAL: CustomerForm_DAL
{
	public CustomerForm_BAL()
	{
	}
    public override System.Data.DataTable CreateModifyCustForm(CustomerForm_BAL CustBAL, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.CreateModifyCustForm(CustBAL, Trans);
    }
    public override System.Data.DataRow GetCustFormByDispName(string DisplayName)
    {
        return base.GetCustFormByDispName(DisplayName);
    }
    public override System.Data.DataTable GetCustomerData()
    {
        return base.GetCustomerData();
    }

    public override System.Data.DataTable searchCustomerByCustomerName(string CustomerName)
    {
        return base.searchCustomerByCustomerName(CustomerName);
    }

    public override System.Data.DataTable searchCustomerByMobileNumber(string MobileNumber)
    {
        return base.searchCustomerByMobileNumber(MobileNumber);
    }

    public override System.Data.DataTable searchCustomerByEmail(string Email)
    {
        return base.searchCustomerByEmail(Email);
    }

    public override string DeleteCustomer(int CustomerID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.DeleteCustomer(CustomerID, Trans);
    }
    public override CustomerForm_BAL GetCustomerInfo(int CustomerID)
    {
        return base.GetCustomerInfo(CustomerID);
    }
    public override System.Data.DataTable getCustomerByID(int CustomerID)
    {
        return base.getCustomerByID(CustomerID);
    }
    public override int SpSubsidiaryCodeCount(CustomerForm_BAL cfBal, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.SpSubsidiaryCodeCount(cfBal, Trans);
    }
    public override bool InsertApartSubsidiary(CustomerForm_BAL cfBal, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.InsertApartSubsidiary(cfBal, Trans);
    }
    public override bool Delete_Apartsubsidary(int CustomerID, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.Delete_Apartsubsidary(CustomerID, Trans);
    }


    public int CustomerID { get; set; }
    public string title { get; set; }
    public string fName { get; set; }
    public string lName { get; set; }
    public string Suffix { get; set; }
    public string Email { get; set; }
    public string CompanyName { get; set; }
    public string Phone { get; set; }
    public string Mobile { get; set; }
    public string Fax { get; set; }
    public string DisplayName { get; set; }
    public bool displayNameClick { get; set; }
    public string NTN { get; set; }
    public string SalesTaxRegNo { get; set; }
    public string BankName { get; set; }
    public string AccNo { get; set; }
    public string IBAN { get; set; }
    public string BillAddressStreet { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public int Zip { get; set; }
    public string Country { get; set; }
    public bool ShippingAddressCheck { get; set; }
    public string ShippingAddressStreet { get; set; }
    public string ShippingCity { get; set; }
    public string ShippingState { get; set; }
    public int ShippingZip { get; set; }
    public string ShippingCountry { get; set; }
    public string Terms { get; set; }
    public string FacebookId { get; set; }
    public string MessangerId { get; set; }
    public string SkypeId { get; set; }
    public string GooglePlusId { get; set; }
    public decimal OpeningBalance { get; set; }
    public string Date { get; set; }
    public string PortofDischarge { get; set; }
    public string DestCountry { get; set; }
    public string Consignee { get; set; }
    public string Buyer { get; set; }
    public string MainCode { get; set; }
    public string ControlCode { get; set; }
    public string SubsidaryCode { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public int CustID { get; set; }
    //
    public int ShippingLineID { get; set; }
}
