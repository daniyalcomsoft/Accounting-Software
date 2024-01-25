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
/// Summary description for BAL_User
/// </summary>
public class AEI_BAL_User:AEI_DAL_User
{
    public int UserID{get;set;}
    public string UserName {get;set;}
    public string Password {get;set;}
    public string Gender {get;set;}
    public int Type {get;set;}
    public string Workphone {get;set;}
    public string HomePhone {get;set;}
    public string Fax {get;set;}
    public string CellPhone {get;set;}
    public string Email {get;set;}
    public string ContactName {get;set;}
    public string Address1 {get;set;}
    public string Address2 {get;set;}
    public string Note { get; set; }
    public int StateID{get;set;}
    public string City { get; set; }
    public string ZipCode {get;set;}
    public bool IsActive {get;set;}
    public int LoginID { get; set; }

	public AEI_BAL_User()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public override DataTable Authentication(string UserName, string Password)
    {
        return base.Authentication(UserName, Password);
    }
    public override bool CreateModifyUser(AEI_BAL_User BalUser)
    {
        return base.CreateModifyUser(BalUser);
    }
    public override DataTable GetAllUser()
    {
        return base.GetAllUser();
    }
    public override DataTable GetUser_byID(int UserID)
    {
        return base.GetUser_byID(UserID);
    }
}
