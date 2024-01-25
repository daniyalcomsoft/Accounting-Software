using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SW.SW_Common;
using System.Data;

/// <summary>
/// Summary description for RolePage_DAL
/// </summary>
public class RolePage_DAL
{
	public RolePage_DAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public virtual int InsertUpdatePagePermission(RolePage_BAL RolePage, SCGL_Session SBO)
    {
        SqlParameter[] param ={new SqlParameter("@Permission_Id",RolePage.Permission_Id) 
                                 ,new SqlParameter("@RoleId",RolePage.RoleId)
                                 ,new SqlParameter("@UserId",RolePage.UserId)
                                 ,new SqlParameter("@PageId",RolePage.PageId)
                                 ,new SqlParameter("@Can_View",RolePage.Can_View)
                                 ,new SqlParameter("@Can_Insert",RolePage.Can_Insert)
                                 ,new SqlParameter("@Can_Update",RolePage.Can_Update)
                                 ,new SqlParameter("@Can_Delete",RolePage.Can_Delete)
                                 ,new SqlParameter("@Can_ApproveOrReject",RolePage.Can_ApproveOrReject)
                                 ,new SqlParameter("@Active",RolePage.Active)
                                 ,new SqlParameter("@Activity_By",SBO.UserID)
                                 ,new SqlParameter("@Activity_Date",DateTime.UtcNow.ToString())
                                 ,new SqlParameter("@User_IP",SBO.UserIP)
                                 ,new SqlParameter("@Site_ID",SBO.UserID)
                                 };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SE_SpInsertUpdatePagePermission", param));
    }
    public virtual DataTable GetPagePermissionpPagesByRole(int RoleID)
    {
        SqlParameter[] param = {new SqlParameter("@RoleID",RoleID)
                                   ,new SqlParameter("@UserID",0)};
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SE_SpGetPagePermissionPages", param).Tables[0];
    }
    public virtual DataTable GetPagePermissionpPagesByUser(int UserID)
    {
        SqlParameter[] param = {new SqlParameter("@RoleID",0)
                                   ,new SqlParameter("@UserID",UserID)};
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SE_SpGetPagePermissionPages", param).Tables[0];
    }

    public virtual int DeletePagePermissionPagesByRole(int RoleID)
    {
        SqlParameter[] param = {new SqlParameter("@RoleID", RoleID)
                                   ,new SqlParameter("@UserID",0)};
        return SqlHelper.ExecuteNonQuery(SCGL_Common.ConnectionString, "vt_SCGL_SE_SpDeleteRolePagePermission", param);
    }
    public virtual int DeletePagePermissionPagesByUser(int UserID)
    {
        SqlParameter[] param = {new SqlParameter("@RoleID",0)
                                   ,new SqlParameter("@UserID",UserID)};
        return SqlHelper.ExecuteNonQuery(SCGL_Common.ConnectionString, "vt_SCGL_SE_SpDeleteRolePagePermission", param);
    }
}
