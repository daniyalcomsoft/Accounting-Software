using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserRole_BAL
/// </summary>
public class UserRole_BAL:UserRole_DAL
{
    public int RoleID { get; set; }
    public string RoleName { get; set; }
    public int? AuditID { get; set; }
    public Int16? Active { get; set; }
    public bool PageLevelPermission { get; set; }
    public bool ModuleLevelPermission { get; set; }
	public UserRole_BAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public override bool CheckRoleName(string RoleName)
    {
        return base.CheckRoleName(RoleName);
    }

    public override int CreateModifyUserRole(UserRole_BAL UserRole, SCGL_Session BOSession)
    {
        return base.CreateModifyUserRole(UserRole, BOSession);
    }
    public override System.Data.DataTable GetAllRole()
    {
        return base.GetAllRole();
    }
    public override UserRole_BAL GetRoleInfo(int RoleID)
    {
        return base.GetRoleInfo(RoleID);
    }
}
