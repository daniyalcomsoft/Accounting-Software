using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VendorFormView_BAL
/// </summary>
public class VendorForm_BAL:VendorForm_DAL
{
	public VendorForm_BAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public override System.Data.DataTable GetVendorData()
    {
        return base.GetVendorData();
    }

    public override System.Data.DataTable searchVendorByVendorName(string VendorName)
    {
        return base.searchVendorByVendorName(VendorName);
    }

    public override System.Data.DataTable searchVendorByMobileNumber(string MobileNumber)
    {
        return base.searchVendorByMobileNumber(MobileNumber);
    }

    public override System.Data.DataTable searchVendorByEmail(string Email)
    {
        return base.searchVendorByEmail(Email);
    }

    public override string DeleteVendor(int Vendor_ID)
    {
        return base.DeleteVendor(Vendor_ID);
    }

    public override System.Data.DataRow GetVendorByDispName(string DisplayName)
    {
        return base.GetVendorByDispName(DisplayName);
    }
    public override System.Data.DataTable CreateModifyVendorForm(VendorForm_BAL VendBAL)
    {
        return base.CreateModifyVendorForm(VendBAL);
    }
    public override VendorForm_BAL GetVendorInfo(int Id)
    {
        return base.GetVendorInfo(Id);
    }
    public int Vendor_ID { get; set; }
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
    public string Other { get; set; }
    public string Website { get; set; }
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
    public string OpeningBalance { get; set; }
    public string Date { get; set; }
    public int RoleID { get; set; }
    public int UserID { get; set; }
    public int Site_ID { get; set; }
   
}
