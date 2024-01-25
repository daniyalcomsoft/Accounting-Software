using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SW.SW_Common;
using System.Data;

/// <summary>
/// Summary description for GLMain_DAL
/// </summary>
public class GLMain_DAL
{
	public GLMain_DAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public virtual bool IsMainCodeExists(string MainCode)
    {
        bool IsExists = false;
        SqlParameter[] param = { new SqlParameter("@MainCode", MainCode) };
        return IsExists = Convert.ToBoolean(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SpIsMainCodeExists", param));
    }

    public virtual int InsertUpdateGLMain(GLMain_BAL MainBO, SCGL_Session SBO)
    {
        SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlTransaction trans = con.BeginTransaction();
        int MainCode = int.MinValue;
        try
        {
            SqlParameter[] param = {new SqlParameter("@MainCode",MainBO.MainCode)
                                       ,new SqlParameter("@Title",MainBO.Title)
                                       ,new SqlParameter("@Nature",MainBO.Nature)
                                       ,new SqlParameter("@ActivityBy",SBO.UserID)
                                       ,new SqlParameter("@ActivityDate",DateTime.UtcNow.ToString())
                                       ,new SqlParameter("@SiteID",SBO.SiteID)
                                       ,new SqlParameter("@UserIP",SBO.UserIP)
                                       ,new SqlParameter("@IsActive",MainBO.IsActive)
                                       ,new SqlParameter("@ActiveChild",MainBO.ActiveChild)
                                       };
            MainCode = Convert.ToInt32(SqlHelper.ExecuteScalar(trans, "vt_SCGL_SpInsertUpdateGLMain", param));
            trans.Commit();
        }
        catch (Exception ex)
        {
            trans.Rollback();
            throw ex;
        }
        finally
        {
            trans.Dispose();
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Dispose();
        }
        return MainCode;
    }

    public virtual DataTable GetAllMainAccType()
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString))
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            using (SqlTransaction trans = con.BeginTransaction())
            {
                try
                {
                    dt = SqlHelper.ExecuteDataset(trans, CommandType.StoredProcedure, "vt_SCGL_SpGetAllMainAccType").Tables[0];
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
                finally
                {
                    trans.Dispose();
                    con.Dispose();
                }
            }
        }
        return dt;
    }

    public virtual GLMain_BAL GetMainAccType(Int16 MainCode)
    {

        GLMain_BAL MainBO = new GLMain_BAL();
        using (SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString))
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            using (SqlTransaction trans = con.BeginTransaction())
            {
                try
                {
                    SqlParameter[] param = { new SqlParameter("@MainCode", MainCode) };
                    using (SqlDataReader dr = SqlHelper.ExecuteReader(trans, "vt_SCGL_Sp_GetMainAccount", param))
                    {
                        if (dr.Read())
                        {
                            MainBO.MainCode = dr["MainCode"].ToString();
                            MainBO.Title = dr["Title"].ToString();
                            MainBO.Nature = Convert.ToInt32(dr["Nature"]);
                            MainBO.UnDeleteable = Convert.ToBoolean(dr["UnDeleteable"]);
                            MainBO.IsActive = Convert.ToInt16(dr["IsActive"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }
        return MainBO;
    }

    public virtual DataTable DeleteMainCodeNotInUse(string MainCode)
    {
        SqlParameter[] param = { new SqlParameter("@MainCode", MainCode) };
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpDeleteMainCodeNotInUse", param).Tables[0];
    }
}
