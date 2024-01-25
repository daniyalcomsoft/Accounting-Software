using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SW.SW_Common;

public partial class VendorForm : System.Web.UI.Page
{

    VendorForm_BAL cfbal = new VendorForm_BAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionBO"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (!IsPostBack)
        {
            BAL_PagePermissions PP = new BAL_PagePermissions();
            DataTable dtRole = new DataTable();
            SCGL_Session AdSes = (Session["SessionBO"]) as SCGL_Session;
            dtRole = PP.GetPermissionByUserId(SCGL_Common.Convert_ToInt(AdSes.RoleId));
            string pageName = null;
            bool view = false;
            foreach (DataRow dr in dtRole.Rows)
            {
                int row = dtRole.Rows.IndexOf(dr);
                if (dtRole.Rows[row]["Page_Url"].ToString() == "VendorForm.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "VendorForm.aspx" && view == true)
                {
                    if (Request.QueryString["Id"] != null)
                    {
                        BindControl(SCGL_Common.Convert_ToInt(Request.QueryString["Id"]));
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
            
        }
        Reload_JS();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataTable dt = PM.getFinancialYearByID(SBO.FinYearID);
        hdnMinDate.Value = SCGL_Common.CheckDateTime(dt.Rows[0]["yearFrom"]).ToShortDateString();
        hdnMaxDate.Value = SCGL_Common.CheckDateTime(dt.Rows[0]["YearTo"]).ToShortDateString(); 
    }
    public void BindControl(int Id)
    {
        if (Request.QueryString["view"] != null)
        {
            btnSave.Visible = false;
        }
        VendorForm_BAL VendorBL = new VendorForm_BAL();
        VendorForm_BAL Vendor = VendorBL.GetVendorInfo(Id);
        txtTitle.Text = Vendor.title;
        txtfirstName.Text = Vendor.fName;
        txtlastName.Text = Vendor.lName;
        txtSuffix.Text = Vendor.Suffix;
        txtEmail.Text = Vendor.Email;
        txtCompany.Text = Vendor.CompanyName;
        txtPhone.Text = Vendor.Phone;
        txtMobile.Text = Vendor.Mobile;
        txtFax.Text = Vendor.Fax;
        txtDisplayName.Text = Vendor.DisplayName;
        chkPrintDispName.Checked = Vendor.displayNameClick;
        txtOther.Text = Vendor.Other;
        txtWebsite.Text = Vendor.Website;
        txtBank.Text = Vendor.BankName;
        txtAccNo.Text = Vendor.AccNo;
        TxtIBAN.Text = Vendor.IBAN;
        txtStreet.Text = Vendor.BillAddressStreet;
        txtCity.Text = Vendor.City;
        txtState.Text = Vendor.State;
        txtZip.Text = (Vendor.Zip).ToString();
        txtCountry.Text = Vendor.Country;
        chkShipAddress.Checked = Vendor.ShippingAddressCheck;
        txtShipStreet.Text = Vendor.ShippingAddressStreet;
        txtShipCity.Text = Vendor.ShippingCity;
        txtShipState.Text = Vendor.ShippingState;
        txtShipZip.Text = (Vendor.ShippingZip).ToString();
        txtShipCountry.Text = Vendor.ShippingCountry;
        txtTerms.Text = Vendor.Terms;
        txtFacebook.Text = Vendor.FacebookId;
        txtMessanger.Text = Vendor.MessangerId;
        txtSkype.Text = Vendor.SkypeId;
        txtGooglePlus.Text = Vendor.GooglePlusId;
        txtOpeningBlnc.Text = Vendor.OpeningBalance;
        txtASDate.Text =  Vendor.Date;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        save();
        emptyfiled();
        if (dt.Rows.Count > 0 && Request.QueryString["Id"]!=null)
        {
            Response.Redirect("VendorForm_Views.aspx");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("VendorForm_Views.aspx");
    }
    public void save()
    {
        cfbal.Vendor_ID = 0;
        if (Request.QueryString["Id"] == null)
        {
            cfbal.Vendor_ID = 0;
        }
        else
        {
            cfbal.Vendor_ID = SCGL_Common.Convert_ToInt(Request.QueryString["Id"]);
        }
        cfbal.title = txtTitle.Text;
        cfbal.fName = txtfirstName.Text;
        cfbal.lName = txtlastName.Text;
        cfbal.Suffix = txtSuffix.Text;
        cfbal.Email = txtEmail.Text;
        cfbal.CompanyName = txtCompany.Text;
        cfbal.Phone = txtPhone.Text;
        cfbal.Mobile = txtMobile.Text;
        cfbal.Fax = txtFax.Text;
        cfbal.DisplayName = txtDisplayName.Text;
        if (chkPrintDispName.Checked)
        {
            cfbal.displayNameClick = true;
        }
        else
        {
            cfbal.displayNameClick = false;
        }
        cfbal.Other = txtOther.Text;
        cfbal.Website = txtWebsite.Text;
        cfbal.BankName = txtBank.Text;
        cfbal.AccNo = txtAccNo.Text;
        cfbal.IBAN = TxtIBAN.Text;
        cfbal.BillAddressStreet = txtStreet.Text;
        cfbal.City = txtCity.Text;
        cfbal.State = txtState.Text;
        if (txtZip.Text == "")
        {
            cfbal.Zip = 00000;
        }
        else
        {
            cfbal.Zip = Convert.ToInt32(txtZip.Text);
        }
        cfbal.Country = txtCountry.Text;
        if (chkShipAddress.Checked)
        {
            cfbal.ShippingAddressStreet = "null";
            cfbal.ShippingCity = "null";
            cfbal.ShippingState = "null";
            cfbal.ShippingZip = 00000;
            cfbal.ShippingCountry = "null";
        }
        else
        {
            cfbal.ShippingAddressStreet = txtShipStreet.Text;
            cfbal.ShippingCity = txtShipCity.Text;
            cfbal.ShippingState = txtShipState.Text;
            if (txtShipZip.Text == "")
            {
                cfbal.ShippingZip = 00000;
            }
            else
            {
                cfbal.ShippingZip = Convert.ToInt32(txtShipZip.Text);
            }
            cfbal.ShippingCountry = txtShipCountry.Text;
        }
        cfbal.Terms = txtTerms.Text;
        cfbal.FacebookId = txtFacebook.Text;
        cfbal.MessangerId = txtMessanger.Text;
        cfbal.SkypeId = txtSkype.Text;
        cfbal.GooglePlusId = txtGooglePlus.Text;
        cfbal.OpeningBalance = txtOpeningBlnc.Text;
        cfbal.Date = txtASDate.Text;
        //Here all ids from session but here problem RoleId not available in Session.
        cfbal.UserID = int.Parse(((SCGL_Session)(Session["SessionBO"])).UserID.ToString());
        cfbal.Site_ID = int.Parse(((SCGL_Session)(Session["SessionBO"])).SiteID.ToString()); 
        cfbal.RoleID = 1;

        dt = cfbal.CreateModifyVendorForm(cfbal);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["Msg"].ToString() == "Record inserted successfully." || dt.Rows[0]["Msg"].ToString() == "Record updated successfully.")
            {
                lblmsg.Text = dt.Rows[0]["Msg"].ToString();
                SCGL_Common.Success_Message(this.Page);
            }
            else
            {
                lblNewError.Text = dt.Rows[0]["Msg"].ToString();
                SCGL_Common.Error_Message(this.Page, "");
            };
        }
    }
    DataTable dt;
    public void emptyfiled()
    {
        txtTitle.Text = "";
        txtfirstName.Text = "";
        txtlastName.Text = ""; 
        txtSuffix.Text = ""; 
        txtEmail.Text = ""; 
        txtCompany.Text = "";
        txtPhone.Text = ""; 
        txtMobile.Text = "";
        txtFax.Text = ""; 
        txtDisplayName.Text = ""; 
        chkPrintDispName.Checked = false; 
        txtOther.Text = ""; 
        txtWebsite.Text = ""; 
        txtBank.Text = ""; 
        txtAccNo.Text = "";
        TxtIBAN.Text = ""; 
        txtStreet.Text = "";
        txtCity.Text = "";
        txtState.Text = "";
        txtZip.Text = ""; 
        txtCountry.Text = ""; 
        chkShipAddress.Checked = false; 
        txtShipStreet.Text = ""; 
        txtShipCity.Text = ""; 
        txtShipState.Text = "";
        txtShipZip.Text = ""; 
        txtShipCountry.Text = ""; 
        txtTerms.Text = ""; 
        txtFacebook.Text = ""; 
        txtMessanger.Text = "";
        txtSkype.Text = ""; 
        txtGooglePlus.Text = ""; 
        txtOpeningBlnc.Text = ""; 
        txtASDate.Text = ""; 
    }
    public void Reload_JS()
    {
        SCGL_Common.ReloadJS(this, "MyDate();");
    }
    protected void chkShipAddress_CheckedChanged(object sender, EventArgs e)
    {
        if (chkShipAddress.Checked)
        {
            txtShipZip.ReadOnly = true;
            txtShipCity.ReadOnly = true;
            txtShipState.ReadOnly = true;
            txtShipStreet.ReadOnly = true;
            txtShipCountry.ReadOnly = true;
        }
        else
        {
            txtShipZip.ReadOnly = false;
            txtShipCity.ReadOnly = false;
            txtShipState.ReadOnly = false;
            txtShipStreet.ReadOnly = false;
            txtShipCountry.ReadOnly = false;
        }
    }
}
