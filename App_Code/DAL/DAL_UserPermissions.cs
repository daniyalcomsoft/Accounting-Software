using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SW.SW_Common;
using System.Data.SqlClient;
public class DAL_UserPermissions
{
	public DAL_UserPermissions()
	{
	}

    public virtual DataTable GetPermissionByUserId(int RoleId)
    {
        SqlParameter[] para = { new SqlParameter("@RoleId", RoleId) };
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_sp_GetPermission", para).Tables[0];
    }
}
