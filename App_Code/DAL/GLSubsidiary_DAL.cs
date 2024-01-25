using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using SW.SW_Common;

/// <summary>
/// Summary description for GLSubsidiary_DAL
/// </summary>
public class GLSubsidiary_DAL
{
	public GLSubsidiary_DAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public virtual bool IsExistsSubsidiaryAccount(string MainCode, string ControlCode, string SubsidiaryCode)
    {
        bool IsExists = false;
        SqlParameter[] param = {new SqlParameter("@MainCode", MainCode) 
                                   ,new SqlParameter("@ControlCode", ControlCode)
                                   ,new SqlParameter("@SubsidaryCode", SubsidiaryCode)};
        return IsExists = Convert.ToBoolean(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SpIsSubsidiaryCodeExists", param));
    }
    public virtual void InsertUpdateSubsidiary(GLSubsidiary_BAL SubBO, SCGL_Session SBO)
    {
        using (SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString))
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            using (SqlTransaction trans = con.BeginTransaction())
            {
                try
                {
                    SqlParameter[] param = {new SqlParameter("@SubsidaryCode",SubBO.SubsidaryCode)
                                               ,new SqlParameter("@MainCode",SubBO.MainCode)
                                               ,new SqlParameter("@ControlCode",SubBO.ControlCode)
                                               ,new SqlParameter("@Title",SubBO.Title)
                                               ,new SqlParameter("@ActivityBy",SBO.UserID)
                                               ,new SqlParameter("@ActivityDate",DateTime.UtcNow.ToString())
                                               ,new SqlParameter("@SiteID",SBO.SiteID)
                                               ,new SqlParameter("@UserIP",SBO.UserIP)
                                               ,new SqlParameter("@IsActive",SubBO.IsActive)
                                               };
                    SqlHelper.ExecuteNonQuery(trans, "vt_SCGL_SpInsertUpdateGLSubsidiary", param);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }
    }

    public virtual DataTable GetAllSubsidiaryByMainNControl(string MainCode, string ControlCode)
    {
        SqlParameter[] param = {new SqlParameter("@MainCode",MainCode)
                                   ,new SqlParameter("@ControlCode",ControlCode)};
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetAllSubsidiaryByMainNControlType", param).Tables[0];
    }
    public virtual GLSubsidiary_BAL GetSubsidiaryAccType(string MainCode, string ControlCode, string SubCode)
    {
        GLSubsidiary_BAL SubBO = new GLSubsidiary_BAL();
        SqlParameter[] param = {new SqlParameter("@MainCode", MainCode) 
                                   ,new SqlParameter("@ControlCode",ControlCode)
                                   ,new SqlParameter("@SubsidaryCode ",SubCode)};

        using (SqlDataReader dr = SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_SCGL_SpGetSubsidiaryAccType", param))
        {
            if (dr.Read())
            {
                SubBO.MainCode = dr["MainCode"].ToString();
                SubBO.ControlCode = dr["ControlCode"].ToString();
                SubBO.SubsidaryCode = dr["SubsidaryCode"].ToString();
                SubBO.Title = dr["Title"].ToString();
                SubBO.IsActive = Convert.ToInt16(dr["IsActive"]);
                SubBO.Deleteable = Convert.ToBoolean(dr["Deleteable"]);
            }
        }
        return SubBO;
    }
    public virtual DataTable GetSubTitle(string Match)
    {
        DataTable dt = new DataTable();
        SqlParameter[] param = { new SqlParameter("@Match", "000") };
        return dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SPGetSubTitle", param).Tables[0];
    }
    public virtual DataTable GetSubCodeTitleLike(string Match,int YearID,string YearFrom,string YearTo)
    {
        DataTable dt = new DataTable();
        SqlParameter[] param = { 
                                   new SqlParameter("@Match", Match),
                                 new SqlParameter("@YearID",YearID),
                                   new SqlParameter("@YearFrom",YearFrom),
                                   new SqlParameter("@YearTo",YearTo)
                               };
        return dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SPGetSubCodeTitleLike", param).Tables[0];
    }

    public virtual DataTable GetSubCodeTitleLike2(string Match, int YearID, string YearFrom, string YearTo)
    {
        DataTable dt = new DataTable();
        SqlParameter[] param = { 
                                   new SqlParameter("@Match", Match),
                                 new SqlParameter("@YearID",YearID),
                                   new SqlParameter("@YearFrom",YearFrom),
                                   new SqlParameter("@YearTo",YearTo)
                               };
        return dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SPGetSubCodeTitleLike2", param).Tables[0];
    }

    public virtual int DeleteSubsidaryCodeNotInUse(string MainCode, string ControlCode, string SubsidaryCode)
    {
        SqlParameter[] param = {new SqlParameter("@MainCode", MainCode)
                                   ,new SqlParameter("@ControlCode", ControlCode)
                                   ,new SqlParameter("@SubsidaryCode", SubsidaryCode)};
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SpDeleteSubsidaryCodeNotInUse", param));
    }
}
