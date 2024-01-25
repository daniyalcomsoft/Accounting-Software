using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ModulePage_BAL
/// </summary>
public class ModulePage_BAL:ModulePage_DAL
{
    public int ModulePermissionID { get; set; }
    public int? RoleID { get; set; }
    public int? UserID { get; set; }
    public int? ModuleID { get; set; }
    public bool? Can_View { get; set; }
    public bool? Can_Insert { get; set; }
    public bool? Can_Update { get; set; }
    public bool? Can_Delete { get; set; }
    public bool? Can_ApproveOrReject { get; set; }
    public bool? Can_UnLock { get; set; }
    public Int16? Active { get; set; }

    
	public ModulePage_BAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public override int DeleteModulePermissionByRoleID(int RoleID)
    {
        return base.DeleteModulePermissionByRoleID(RoleID);
    }
    public override int DeleteModulePermissionByUserID(int UserID)
    {
        return base.DeleteModulePermissionByUserID(UserID);
    }
    public override DataTable GetAllActiveModule()
    {
        return base.GetAllActiveModule();
    }
    public override DataTable GetModulePages(PM.ModuleName Module)
    {
        return base.GetModulePages(Module);
    }
    public override DataTable GetModuleRightsByRoleID(int RoleID)
    {
        return base.GetModuleRightsByRoleID(RoleID);
    }
    public override DataTable GetModuleRightsByUserID(int UserID)
    {
        return base.GetModuleRightsByUserID(UserID);
    }
    public override int InsertUpdateModulePermissionByRoleID(ModulePage_BAL ModPage, SCGL_Session SessionBo)
    {
        return base.InsertUpdateModulePermissionByRoleID(ModPage, SessionBo);
    }
    public override int InsertUpdateModulePermissionByUserID(ModulePage_BAL ModPage, SCGL_Session SessionBo)
    {
        return base.InsertUpdateModulePermissionByUserID(ModPage, SessionBo);
    }
       
    
    
}
