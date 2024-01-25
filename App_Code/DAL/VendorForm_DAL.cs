using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SW.SW_Common;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for VendorFormView_DAL
/// </summary>
public class VendorForm_DAL
{
	public VendorForm_DAL()
	{
	}
    public virtual DataTable GetVendorData()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SPGetVendorRecord").Tables[0];
    }

    public virtual DataTable searchVendorByVendorName(string VendorName)
    {
        SqlParameter[] param = {new SqlParameter("@VendorName", VendorName)
                         
                              };

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpSearchVendor_BYVendorName", param).Tables[0];
        return dt;
    }

    public virtual DataTable searchVendorByMobileNumber(string MobileNumber)
    {
        SqlParameter[] param = {new SqlParameter("@MobileNumber", MobileNumber)
                         
                              };

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpSearchVendor_BYMobile", param).Tables[0];
        return dt;
    }

    public virtual DataTable searchVendorByEmail(string Email)
    {
        SqlParameter[] param = {new SqlParameter("@Email", Email)
                         
                              };

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpSearchVendor_BYEmail", param).Tables[0];
        return dt;
    }
    
    public virtual string DeleteVendor(int Vendor_ID)
    {
        SqlParameter[] param = { new SqlParameter("@Id", Vendor_ID) };        
        return SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SPDeleteVendorRecord", param).ToString();
    }
    public virtual DataRow GetVendorByDispName(string DisplayName)
    {
        SqlParameter[] param = {new SqlParameter("@DisplayName", DisplayName)
                                   };
        DataRow row = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetVendorbyDispName", param).Tables[0].Rows[0];
        return row;
    }

    public virtual VendorForm_BAL GetVendorInfo(int Vendor_ID)
    {
        VendorForm_BAL Vendor = new VendorForm_BAL();
        SqlParameter[] param = { new SqlParameter("@Id", Vendor_ID) };
        using (SqlDataReader dr = SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_SCGL_SPGetVendorRecordByID", param))
        {
            if (dr.Read())
            {
                Vendor.Vendor_ID = Convert.ToInt32(dr["Vendor_ID"]);
                Vendor.title = dr["title"].ToString();
                Vendor.fName = dr["fName"].ToString();
                Vendor.lName = dr["lName"].ToString();
                Vendor.Suffix = dr["Suffix"].ToString();
                Vendor.Email = dr["Email"].ToString();
                Vendor.CompanyName = dr["CompanyName"].ToString();
                Vendor.Phone = dr["Phone"].ToString();
                Vendor.Mobile = dr["Mobile"].ToString();
                Vendor.Fax = dr["Fax"].ToString();
                Vendor.DisplayName = dr["DisplayName"].ToString();
                Vendor.displayNameClick = Convert.ToBoolean(dr["displayNameClick"]);
                Vendor.Other = dr["Other"].ToString();
                Vendor.Website = dr["Website"].ToString();
                Vendor.BankName = dr["BankName"].ToString();
                Vendor.AccNo = dr["AccNo"].ToString();
                Vendor.IBAN = dr["IBAN"].ToString();
                Vendor.BillAddressStreet = dr["BillAddressStreet"].ToString();
                Vendor.City = dr["City"].ToString();
                Vendor.State = dr["State"].ToString();
                Vendor.Zip = Convert.ToInt32(dr["Zip"]);
                Vendor.Country = dr["Country"].ToString();
                Vendor.ShippingAddressCheck = Convert.ToBoolean(dr["ShippingAddressCheck"]);
                Vendor.ShippingAddressStreet = dr["ShippingAddressStreet"].ToString();
                Vendor.ShippingCity = dr["ShippingCity"].ToString();
                Vendor.ShippingState = dr["ShippingState"].ToString();
                Vendor.ShippingZip = Convert.ToInt32(dr["ShippingZip"]);
                Vendor.ShippingCountry = dr["ShippingCountry"].ToString();
                Vendor.Terms = dr["Terms"].ToString();
                Vendor.FacebookId = dr["FacebookId"].ToString();
                Vendor.MessangerId = dr["MessangerId"].ToString();
                Vendor.SkypeId = dr["SkypeId"].ToString();
                Vendor.GooglePlusId = dr["GooglePlusId"].ToString();
                Vendor.OpeningBalance = dr["OpeningBalance"].ToString();
                Vendor.Date = dr["Created_Date"].ToString();

            }
        }
        return Vendor;
    }

    public virtual DataTable CreateModifyVendorForm(VendorForm_BAL VendBAL)
    {
        SqlParameter[] param = {new SqlParameter("@Id", VendBAL.Vendor_ID)
                                   ,new SqlParameter("@title",VendBAL.title)
                                   ,new SqlParameter("@fName",VendBAL.fName)
                                   ,new SqlParameter("@lName",VendBAL.lName)
                                   ,new SqlParameter("@Suffix",VendBAL.Suffix)
                                   ,new SqlParameter("@Email",VendBAL.Email)
                                   ,new SqlParameter("@CompanyName",VendBAL.CompanyName)
                                   ,new SqlParameter("@Phone",VendBAL.Phone)
                                   ,new SqlParameter("@Mobile",VendBAL.Mobile)
                                   ,new SqlParameter("@Fax",VendBAL.Fax)
                                   ,new SqlParameter("@DisplayName",VendBAL.DisplayName)
                                   ,new SqlParameter("@displayNameClick",VendBAL.displayNameClick)
                                   ,new SqlParameter("@Other",VendBAL.Other)
                                   ,new SqlParameter("@Website",VendBAL.Website)
                                   ,new SqlParameter("@BankName",VendBAL.BankName)
                                   ,new SqlParameter("@BranchNo",VendBAL.AccNo)
                                   ,new SqlParameter("@IBAN",VendBAL.IBAN)
                                   ,new SqlParameter("@BillAddressStreet",VendBAL.BillAddressStreet)
                                   ,new SqlParameter("@City",VendBAL.City)
                                   ,new SqlParameter("@State",VendBAL.State)
                                   ,new SqlParameter("@Zip",VendBAL.Zip)
                                   ,new SqlParameter("@Country",VendBAL.Country)
                                   ,new SqlParameter("@ShippingAddressCheck",VendBAL.ShippingAddressCheck)
                                   ,new SqlParameter("@ShippingAddressStreet",VendBAL.ShippingAddressStreet)
                                   ,new SqlParameter("@ShippingCity",VendBAL.ShippingCity)
                                   ,new SqlParameter("@ShippingState",VendBAL.ShippingState)
                                   ,new SqlParameter("@ShippingZip",VendBAL.ShippingZip)
                                   ,new SqlParameter("@ShippingCountry",VendBAL.ShippingCountry)
                                   ,new SqlParameter("@Terms",VendBAL.Terms)
                                   ,new SqlParameter("@FacebookId",VendBAL.FacebookId)
                                   ,new SqlParameter("@MessangerId",VendBAL.MessangerId)
                                   ,new SqlParameter("@SkypeId",VendBAL.SkypeId)
                                   ,new SqlParameter("@GooglePlusId",VendBAL.GooglePlusId)
                                   ,new SqlParameter("@OpeningBalance",VendBAL.OpeningBalance)
                                   ,new SqlParameter("@Date",DateTime.UtcNow.ToString())
                                   ,new SqlParameter("@RoleID",VendBAL.RoleID)
                                   ,new SqlParameter("@UserID",VendBAL.UserID)
                                   ,new SqlParameter("@Site_ID",VendBAL.Site_ID)
                                  };
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpCreateModifyVendor", param).Tables[0];
        return dt;
    }
}
