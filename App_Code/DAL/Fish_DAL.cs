using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SW.SW_Common;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Fish_DAL
/// </summary>
public class Fish_DAL
{
	public Fish_DAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public virtual DataTable GetFishName()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_Sp_GetFishName_").Tables[0];
    }
    public virtual DataTable GetFishSize()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_Sp_GetFishSize_").Tables[0];
    }
    public virtual DataTable GetFishGrade()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_Sp_GetFishGrade_").Tables[0];
    }

    public virtual int CreateModifyFishName(Fish_BAL BO, SCGL_Session SBO)
    {
        SqlParameter[] param = {new SqlParameter("@FishID",BO.FishID)
                                   ,new SqlParameter("@FishName",BO.FishName)
                                  };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_Sp_CreateModifyFishName", param));
    }
   
    public virtual int CreateModifyFishSize(Fish_BAL BO, SCGL_Session SBO)
    {
        SqlParameter[] param = {new SqlParameter("@FishSizeID",BO.FishSizeID)
                                   ,new SqlParameter("@FishSize",BO.FishSize)
                                   ,new SqlParameter("@SortOrder",BO.SortOrder)
                                  };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_Sp_CreateModifyFishSize", param));
    }

    public virtual int CreateModifyFishGrade(Fish_BAL BO, SCGL_Session SBO)
    {
        SqlParameter[] param = {new SqlParameter("@FishGraID",BO.FishGradeID)
                                   ,new SqlParameter("@FishGrade",BO.FishGrade)
                                  };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_Sp_CreateModifyFishGrade", param));
    }


    public virtual Fish_BAL GetFishByID(int FishID)
    {

        Fish_BAL BO = new Fish_BAL();
        SqlParameter[] param = { new SqlParameter("@FishID", FishID) };
        using (SqlDataReader dr = SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_SCGL_Sp_GetFishByID", param))
        {
            if (dr.Read())
            {
                BO.FishID = Convert.ToInt32(dr["FishID"]);
                BO.FishName= dr["FishName"].ToString();
              
            }
        }
        return BO;
    }
    public virtual Fish_BAL GetFishSizeByID(int FishSizeID)
    {

        Fish_BAL BO = new Fish_BAL();
        SqlParameter[] param = { new SqlParameter("@FishSizeID", FishSizeID) };
        using (SqlDataReader dr = SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_SCGL_Sp_GetFishSizeByID", param))
        {
            if (dr.Read())
            {
                BO.FishSizeID = Convert.ToInt32(dr["FishSizeID"]);
                BO.FishSize = dr["FishSize"].ToString();
                BO.SortOrder = SCGL_Common.Convert_ToInt(dr["SortOrder"]);
                BO.IsEnabled = SCGL_Common.Convert_ToInt(dr["IsEnabled"]);

            }
        }
        return BO;
    }
    public virtual Fish_BAL GetFishGradeByID(int FishGradeID)
    {

        Fish_BAL BO = new Fish_BAL();
        SqlParameter[] param = { new SqlParameter("@FishGradeID", FishGradeID) };
        using (SqlDataReader dr = SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_SCGL_Sp_GetFishGradeByID", param))
        {
            if (dr.Read())
            {
                BO.FishGradeID = Convert.ToInt32(dr["FishGradeID"]);
                BO.FishGrade = dr["FishGrade"].ToString();

            }
        }
        return BO;
    }
    public virtual string DeleteFish(int FishID)
    {
        SqlParameter[] param = { new SqlParameter("@FishID", FishID)}
                                ;
        return SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_Sp_DeleteFish", param).ToString();
    }
    public virtual string DeleteFishSize(int FishSizeID)
    {
        SqlParameter[] param = { new SqlParameter("@FishSizeID", FishSizeID) }
                                ;
        return SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_Sp_DeleteFishSize", param).ToString();
    }
    public virtual string DeleteFishGrade(int FishGradeID)
    {
        SqlParameter[] param = { new SqlParameter("@FishGradeID", FishGradeID) }
                                ;
        return SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_Sp_DeleteFishGrade", param).ToString();
    }
    public virtual DataTable searchFishName(string FishName)
    {
        SqlParameter[] param = {new SqlParameter("@FishName", FishName)};

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_Sp_SearchFishName", param).Tables[0];
        return dt;
    }
    public virtual DataTable searchFishGrade(string FishGrade)
    {
        SqlParameter[] param = { new SqlParameter("@FishGrade", FishGrade) };

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_Sp_SearchFishGrade", param).Tables[0];
        return dt;
    }
    public virtual DataTable searchFishSize(string FishSize)
    {
        SqlParameter[] param = { new SqlParameter("@FishSize", FishSize) };

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_Sp_SearchFishSize", param).Tables[0];
        return dt;
    }

    public virtual int CheckFishSize(string FishSize, int FishSizeID)
    {
        SqlParameter[] param = { new SqlParameter("@FishSize", FishSize)
                                ,new SqlParameter("@FishSizeID", FishSizeID)
                               };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_CheckFishSize", param));
    }

    public virtual int CheckFishName(string FishName)
    {
        SqlParameter[] param = { new SqlParameter("@FishName", FishName)
                               };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_CheckFishName", param));
    }

    public virtual int CheckFishGrade(string FishGrade)
    {
        SqlParameter[] param = { new SqlParameter("@FishGrade", FishGrade)
                               };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_CheckFishGrade", param));
    }

    
}
