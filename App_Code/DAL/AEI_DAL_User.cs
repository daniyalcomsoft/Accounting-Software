using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SW.SW_Common;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DAL_User
/// </summary>
public class AEI_DAL_User
{
	public AEI_DAL_User()
	{
       
	}
    public virtual DataTable Authentication(string UserName,string Password)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@UserName",UserName),
                                    new SqlParameter("@Password",Password)
                               };
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_sp_Login", param).Tables[0];
    }
    public virtual bool CreateModifyUser(AEI_BAL_User BalUser)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@UserID",BalUser.UserID)
                                   ,new SqlParameter("@UserName",BalUser.UserName)
                                   ,new SqlParameter("@Password",BalUser.Password)
                                   ,new SqlParameter("@Gender",BalUser.Gender)
                                   ,new SqlParameter("@Type",BalUser.Type)
                                   ,new SqlParameter("@Workphone",BalUser.Workphone)
                                   ,new SqlParameter("@HomePhone",BalUser.HomePhone)
                                   ,new SqlParameter("@Fax",BalUser.Fax)
                                   ,new SqlParameter("@CellPhone",BalUser.CellPhone)
                                   ,new SqlParameter("@Email",BalUser.Email)
                                   ,new SqlParameter("@ContactName",BalUser.ContactName)
                                   ,new SqlParameter("@Address1",BalUser.Address1)
                                   ,new SqlParameter("@Address2",BalUser.Address2)
                                   ,new SqlParameter("@Note2",BalUser.Note)
                                   ,new SqlParameter("@StateID",BalUser.StateID)
                                   ,new SqlParameter("@City",BalUser.City)
                                   ,new SqlParameter("@ZipCode",BalUser.ZipCode)
                                   ,new SqlParameter("@IsActive",BalUser.IsActive)
                                   ,new SqlParameter("@LoginID",BalUser.LoginID)
                               };
        int i = SqlHelper.ExecuteNonQuery(SCGL_Common.ConnectionString, "vt_sp_CreateModifyUser", param);
        return i > 0;
    }
    public virtual DataTable GetAllUser()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_sp_GetUser").Tables[0];
        
    }
    public virtual DataTable GetUser_byID(int UserID)
    {
        SqlParameter param = new SqlParameter("@UserID", UserID);
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_sp_GetUser_byID", param).Tables[0];
    }
    public virtual bool Delete_User(int UserID)
    {
        try
        {
            SqlParameter param = new SqlParameter("@UserID", UserID);
            int i = SqlHelper.ExecuteNonQuery(SCGL_Common.ConnectionString, "vt_sp_Delete_User", param);
            return i > 0;
        }
        catch (Exception e)
        {
            throw;
        }
    }
}
