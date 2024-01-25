using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SW.SW_Common;
using System.Data;
/// <summary>
/// Summary description for User_DAL
/// </summary>
public class User_DAL
{
    public virtual DataRow CreateModifyUser(User_BAL BOUser, SCGL_Session BOsession)
        {
            SqlParameter[] param = {new SqlParameter("@RoleID",BOUser.RoleID)
                                   ,new SqlParameter("@Prefix",BOUser.Prefix)
                                   ,new SqlParameter("@FirstName",BOUser.FirstName)
                                   ,new SqlParameter("@MiddleName",BOUser.MiddleName)
                                   ,new SqlParameter("@LastName",BOUser.LastName)
                                   ,new SqlParameter("@CellPhonePrimary",BOUser.CellPhonePrimary)
                                   ,new SqlParameter("@CellPhoneSecondary",BOUser.CellPhoneSecondary)
                                   ,new SqlParameter("@HomePhone",BOUser.HomePhone)
                                   ,new SqlParameter("@OfficePhone",BOUser.OfficePhone)
                                   ,new SqlParameter("@Email",BOUser.Email)
                                   ,new SqlParameter("@MailingAddress",BOUser.MailingAddress)
                                   ,new SqlParameter("@District",BOUser.District)
                                   ,new SqlParameter("@City",BOUser.City)
                                   ,new SqlParameter("@Postal",BOUser.Postal)
                                   ,new SqlParameter("@UserName",BOUser.UserName)
                                   ,new SqlParameter("@UserPassword",BOUser.UserPassword)
                                   ,new SqlParameter("@ActivityBy",BOsession.UserID)
                                   ,new SqlParameter("@ActivityDate",DateTime.UtcNow.ToString())
                                   ,new SqlParameter("@UserIP",BOsession.UserIP)
                                   ,new SqlParameter("@SiteID",BOsession.SiteID)
                                   ,new SqlParameter("@specialPermission",BOUser.specialPermission)
                                   ,new SqlParameter("@Active",BOUser.IsActive)
                                   ,new SqlParameter("@UserID",BOUser.UserID)
                                  };
            DataRow row = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SE_SpCreateModifyUser", param).Tables[0].Rows[0];
            return row;
        }

        public virtual int DeleteUser(int UserID)
        {
            SqlParameter[] param = {new SqlParameter("@UserID", UserID)};
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SE_SpDeleteUser", param));
        }

        public virtual DataTable GetAllUserInfo()
        {
            return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SE_SPGetAllUserInfo").Tables[0];
        }
        public virtual DataTable GetFinacialYear()
        {
            return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_Sp_GetFinancialYear").Tables[0];
        }

        public virtual int GetDefaultFinancialYear()
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_Sp_GetDefaultFinancialYear"));
        }

        public virtual User_BAL GetUserInfo(int UserID)
        {
            
            User_BAL User = new User_BAL();
            SqlParameter[] param = {new SqlParameter("@UserID",UserID)};
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_SCGL_SE_SpGetUserInfoByUserID", param))
            {
                if (dr.Read())
                {
                    User.UserID = Convert.ToInt32(dr["UserID"]);
                    User.Prefix = dr["Prefix"].ToString();
                    User.FirstName = dr["FirstName"].ToString();
                    User.MiddleName= dr["MiddleName"].ToString();
                    User.LastName = dr["LastName"].ToString();
                    User.CellPhonePrimary = dr["CellPhonePrimary"].ToString();
                    User.CellPhoneSecondary = dr["CellPhoneSecondary"].ToString();
                    User.HomePhone = dr["HomePhone"].ToString();
                    User.OfficePhone = dr["OfficePhone"].ToString();
                    User.Email = dr["Email"].ToString();
                    User.MailingAddress = dr["MailingAddress"].ToString();
                    User.District= dr["District"].ToString();
                    User.City = dr["City"].ToString();
                    User.Postal = dr["Postal"].ToString();
                    User.UserName = dr["UserName"].ToString();
                    User.RoleID =Convert.ToInt32(dr["RoleID"]);
                    User.UserPassword = dr["UserPassword"].ToString();
                    //User.specialPermission =Convert.ToBoolean(dr["SpecialPermission"]);
                    User.IsActive=Convert.ToInt16(dr["Active"]);
                }
            }
            return User;
        }
        public virtual void SetSpecialPermissionTrueFalse(int UserID)
        { 
            SqlParameter[] param = {new SqlParameter("@UserID",UserID)};
            SqlHelper.ExecuteNonQuery(SCGL_Common.ConnectionString, "vt_SCGL_SE_SpSetSpecialPermissionTrueFalse", param);
        }
        
        public virtual int IsUser(string UserName, string Password)
        {
            SqlParameter[] param = {new SqlParameter("@UserName", UserName)
                                    , new SqlParameter("@Password", Password)};
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SE_SpGetUserIDByUser", param));
        }
        public virtual int IsRole(string UserName, string Password)
        {
            SqlParameter[] param = {new SqlParameter("@UserName", UserName)
                                    , new SqlParameter("@Password", Password)};
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SE_SpGetRoleIDByUser", param));
        }
        public virtual DataTable GetUserPermissionByUserID(int UserID )
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = {new SqlParameter("@UserId", UserID),
                                   new SqlParameter("@RoleID", 0),
                                   new SqlParameter("@ModuleID", 0),
                                   new SqlParameter("@Site_ID", 0),
                                   new SqlParameter("@Is_Active", 0),};
            return dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SE_SpGetUserRightsByUserID", param).Tables[0];
        }
        public virtual DataTable CheckUserRolebeforeDelete(int RoleID )
        {
            SqlParameter[] param = { new SqlParameter("@RoleID", RoleID) };
            return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SpCheckUserRolebeforeDelete", param).Tables[0];
        }
        public virtual DataTable GetUserInfoDropdown()
        {
            return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_GetUserNamewithID").Tables[0];
        }
    }
	




