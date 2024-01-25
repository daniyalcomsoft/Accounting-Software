using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for RolePage_BAL
/// </summary>
public class RolePage_BAL:RolePage_DAL
{
    public int Permission_Id { get; set; }
    public int? RoleId { get; set; }
    public int? UserId { get; set; }
    public int? PageId { get; set; }
    public bool? Can_View { get; set; }
    public bool? Can_Insert { get; set; }
    public bool? Can_Update { get; set; }
    public bool? Can_Delete { get; set; }
    public bool? Can_ApproveOrReject { get; set; }
    public bool? Can_Unlock { get; set; }
    public Int16? Active { get; set; }
	public RolePage_BAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public override int DeletePagePermissionPagesByRole(int RoleID)
    {
        return base.DeletePagePermissionPagesByRole(RoleID);
    }
    public override int DeletePagePermissionPagesByUser(int UserID)
    {
        return base.DeletePagePermissionPagesByUser(UserID);
    }
    public override DataTable GetPagePermissionpPagesByRole(int RoleID)
    {
        return base.GetPagePermissionpPagesByRole(RoleID);
    }
    public override DataTable GetPagePermissionpPagesByUser(int UserID)
    {
        return base.GetPagePermissionpPagesByUser(UserID);
    }
    public override int InsertUpdatePagePermission(RolePage_BAL RolePage, SCGL_Session SBO)
    {
        return base.InsertUpdatePagePermission(RolePage, SBO);
    }
    
}
