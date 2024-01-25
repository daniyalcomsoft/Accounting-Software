using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SW.SW_Common;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ModulePage_DAL
/// </summary>
public class ModulePage_DAL
{
	public ModulePage_DAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public virtual DataTable GetAllActiveModule()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SE_SPGetActiveModule").Tables[0];
    }
    public virtual DataTable GetModulePages(PM.ModuleName Module)
    {
        SqlParameter[] param = { new SqlParameter("@Module_Id", (int)Module) };
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SE_SPGetModulePages", param).Tables[0];
    }

   
    public virtual int InsertUpdateModulePermissionByRoleID(ModulePage_BAL ModPage, SCGL_Session SessionBo)
    {
        SqlParameter[] param = {new SqlParameter("@ModulePermissionID",ModPage.ModulePermissionID)
                                   ,new SqlParameter("@RoleID",ModPage.RoleID)
                                   ,new SqlParameter("@ModuleID",ModPage.ModuleID)
                                   ,new SqlParameter("@Can_View",ModPage.Can_View)
                                   ,new SqlParameter("@Can_Insert",ModPage.Can_Insert)
                                   ,new SqlParameter("@Can_Update",ModPage.Can_Update)
                                   ,new SqlParameter("@Can_Delete",ModPage.Can_Delete)
                                   ,new SqlParameter("@Can_ApproveOrReject",ModPage.Can_ApproveOrReject)
                                   ,new SqlParameter("@Active",ModPage.Active)
                                   ,new SqlParameter("@Activity_By",SessionBo.UserID)
                                   ,new SqlParameter("@Activity_Date",DateTime.UtcNow.ToString())
                                   ,new SqlParameter("@User_IP",SessionBo.UserIP)
                                   ,new SqlParameter("@Site_ID",SessionBo.SiteID)
                                   };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SE_SPInsertUpdateModulPermissionByRoleID", param));
    }
    public virtual int InsertUpdateModulePermissionByUserID(ModulePage_BAL ModPage,SCGL_Session SessionBo)
    {
        SqlParameter[] param = {new SqlParameter("@ModulePermissionID",ModPage.ModulePermissionID)
                                   ,new SqlParameter("@UserID",ModPage.UserID)
                                   ,new SqlParameter("@ModuleID",ModPage.ModuleID)
                                   ,new SqlParameter("@Can_View",ModPage.Can_View)
                                   ,new SqlParameter("@Can_Insert",ModPage.Can_Insert)
                                   ,new SqlParameter("@Can_Update",ModPage.Can_Update)
                                   ,new SqlParameter("@Can_Delete",ModPage.Can_Delete)
                                   ,new SqlParameter("@Can_ApproveOrReject",ModPage.Can_ApproveOrReject)
                                   ,new SqlParameter("@Can_UnLock",ModPage.Can_UnLock)
                                   ,new SqlParameter("@Active",ModPage.Active)
                                   ,new SqlParameter("@Activity_By",SessionBo.UserID)
                                   ,new SqlParameter("@Activity_Date",DateTime.UtcNow.ToString())
                                   ,new SqlParameter("@User_IP",SessionBo.UserIP)
                                   ,new SqlParameter("@Site_ID",SessionBo.SiteID)
                                   };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SE_SPInsertUpdateModulPermissionByUserID", param));
    }

    public virtual DataTable GetModuleRightsByRoleID(int RoleID)
    {
        SqlParameter[] param = {new SqlParameter("@RoleID", RoleID)
                                   ,new SqlParameter("@UserID",0)};
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SE_SpGetModuleRights", param).Tables[0];
    }
    public virtual DataTable GetModuleRightsByUserID(int UserID)
    {
        SqlParameter[] param = {new SqlParameter("@RoleID",0)
                                   ,new SqlParameter("@UserID", UserID)};
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SE_SpGetModuleRights", param).Tables[0];
    }
    public virtual int DeleteModulePermissionByRoleID(int RoleID)
    {
        SqlParameter[] param = {new SqlParameter("@RoleID", RoleID)
                                   ,new SqlParameter("@UserID",0)};
        return Convert.ToInt32(SqlHelper.ExecuteNonQuery(SCGL_Common.ConnectionString, "vt_SCGL_SE_DeleteModulePermission", param));
    }
    public virtual int DeleteModulePermissionByUserID(int UserID)
    {
        SqlParameter[] param = {new SqlParameter("@RoleID",0)
                                   ,new SqlParameter("@UserID", UserID)};
        return Convert.ToInt32(SqlHelper.ExecuteNonQuery(SCGL_Common.ConnectionString, "vt_SCGL_SE_DeleteModulePermission", param));
    }
}
