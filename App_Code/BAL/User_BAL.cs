using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for User_BAL
/// </summary>
public class User_BAL:User_DAL
{
    public int UserID { get; set; }
    public int? RoleID { get; set; }
    public string Prefix { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string CellPhonePrimary { get; set; }
    public string CellPhoneSecondary { get; set; }
    public string HomePhone { get; set; }
    public string OfficePhone { get; set; }
    public string Email { get; set; }
    public string MailingAddress { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public string Postal { get; set; }
    public string UserName { get; set; }

    private string _UserPassword;
    public string UserPassword
    {
        get
        {
            return _UserPassword;
        }
        set
        {
            _UserPassword = value;
        }
    }
    public bool? specialPermission { get; set; }
    public Int16? IsActive { get; set; }
	public User_BAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public override System.Data.DataTable CheckUserRolebeforeDelete(int RoleID)
    {
        return base.CheckUserRolebeforeDelete(RoleID);
    }
    public override System.Data.DataTable GetFinacialYear()
    {
        return base.GetFinacialYear();
    }

    public override int GetDefaultFinancialYear()
    {
        return base.GetDefaultFinancialYear();
    }

    public override System.Data.DataRow CreateModifyUser(User_BAL BOUser, SCGL_Session BOsession)
    {
        return base.CreateModifyUser(BOUser, BOsession);
    }
    public override int DeleteUser(int UserID)
    {
        return base.DeleteUser(UserID);
    }
    public override System.Data.DataTable GetAllUserInfo()
    {
        return base.GetAllUserInfo();
    }
    public override User_BAL GetUserInfo(int UserID)
    {
        return base.GetUserInfo(UserID);
    }
    public override System.Data.DataTable GetUserInfoDropdown()
    {
        return base.GetUserInfoDropdown();
    }
    public override System.Data.DataTable GetUserPermissionByUserID(int UserID)
    {
        return base.GetUserPermissionByUserID(UserID);
    }
    public override int IsRole(string UserName, string Password)
    {
        return base.IsRole(UserName, Password);
    }
    public override int IsUser(string UserName, string Password)
    {
        return base.IsUser(UserName, Password);
    }
    public override void SetSpecialPermissionTrueFalse(int UserID)
    {
        base.SetSpecialPermissionTrueFalse(UserID);
    }
}
