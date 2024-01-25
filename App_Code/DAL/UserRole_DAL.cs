using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SW.SW_Common;
using System.Data;

/// <summary>
/// Summary description for UserRole_DAL
/// </summary>
public class UserRole_DAL
{
	public UserRole_DAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public virtual int CreateModifyUserRole(UserRole_BAL UserRole, SCGL_Session BOSession)
    {
        SqlParameter[] param = {new SqlParameter("@RoleName",UserRole.RoleName)
                                   ,new SqlParameter("@ActivityBy",BOSession.UserID)
                                   ,new SqlParameter("@ActivityDate",DateTime.UtcNow.ToString())
                                   ,new SqlParameter("@SiteID",BOSession.SiteID)
                                   ,new SqlParameter("@Active",UserRole.Active)
                                   ,new SqlParameter("@RoleID",UserRole.RoleID)
                                   ,new SqlParameter("@UserIP",BOSession.UserIP)
                                   };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SpCreateModifyUserRole", param));
    }
    public virtual DataTable GetAllRole()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.Text, "SELECT RoleID,RoleName FROM vt_SCGL_UserRole").Tables[0];
    }

    public virtual UserRole_BAL GetRoleInfo(int RoleID)
    {

        UserRole_BAL RoleBO = new UserRole_BAL();
        SqlParameter[] param = { new SqlParameter("@RoleID", RoleID) };
        using (SqlDataReader dr = SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_SCGL_SPGetRoleInfo", param))
        {
            if (dr.Read())
            {
                RoleBO.RoleID = Convert.ToInt32(dr["RoleID"]);
                RoleBO.RoleName = dr["RoleName"].ToString();
                RoleBO.Active = Convert.ToInt16(dr["Active"].Equals(DBNull.Value) ? 0 : 1);
            }
        }
        return RoleBO;
    }

    public virtual bool CheckRoleName(string RoleName)
    {
        bool CheckRoleName = false;
        SqlParameter[] param = { new SqlParameter("@RoleName", RoleName) };
        return CheckRoleName = Convert.ToBoolean(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SPCheckRoleName", param));
    }
}
