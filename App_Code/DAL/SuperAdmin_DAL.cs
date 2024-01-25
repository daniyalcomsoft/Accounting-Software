using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SW.SW_Common;
using System.Data;

/// <summary>
/// Summary description for SuperAdmin_DAL
/// </summary>
public class SuperAdmin_DAL
{
	public SuperAdmin_DAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public virtual int CreateModifySetup(SuperAdmin_BAL BO, SCGL_Session SBO)
    {
        SqlParameter[] param = {new SqlParameter("@SetupID",BO.SetupID)
                                   ,new SqlParameter("@SiteCode",BO.SiteCode)
                                   ,new SqlParameter("@SiteName",BO.SiteName)
                                   ,new SqlParameter("@Description",BO.Description)
                                   ,new SqlParameter("@CreatedDate",BO.CreatedDate)
                                   ,new SqlParameter("@Address",BO.Address)
                                   ,new SqlParameter("@ContactNumber",BO.ContactNumber)
                                   ,new SqlParameter("@ContactPerson",BO.ContactPerson)
                                   ,new SqlParameter("@ContactPersonDesg",BO.ContactPersonDesg)
                                   ,new SqlParameter("@ContactPersonEmail",BO.ContactPersonEmail)
                                   ,new SqlParameter("@CustomAgent",BO.CustomAgent)
                                   ,new SqlParameter("@SNTN",BO.SNTN)
                                   ,new SqlParameter("@SalesTaxRegNo",BO.SalesTaxRegNo)
                                   ,new SqlParameter("@Mod_Financials",BO.Mod_Financials)
                                   ,new SqlParameter("@Mod_DepositAccount",BO.Mod_DepositAccount)
                                   ,new SqlParameter("@Mod_TermDeposit",BO.Mod_TermDeposit)
                                   ,new SqlParameter("@Mod_Loan",BO.Mod_Loan)
                                   ,new SqlParameter("@CreatedBy",BO.CreatedBy)};
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SE_SPAddEditSuperAdminSetup", param));
    }

    public virtual DataTable SelectSetupInfoBySetupID(SuperAdmin_BAL BO, SCGL_Session SBO)
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SE_SpGetSetupInfoBySetupID").Tables[0];
    }
    public virtual DataTable GetSiteName()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SE_SpGetSitname").Tables[0];
    }
    public virtual int CreateModifyCostCenter(SuperAdmin_BAL BO, SCGL_Session SBO)
    {
        SqlParameter[] param = {new SqlParameter("@CostCenterID",BO.CostCenterID)
                                   ,new SqlParameter("@CostCenterName",BO.CostCenterName)
                                   ,new SqlParameter("@ActivityBy",SBO.UserID)
                                   ,new SqlParameter("@ActivityDate",DateTime.UtcNow.ToString())
                                   ,new SqlParameter("@SiteID",SBO.SiteID)
                                   ,new SqlParameter("@IP",SBO.UserIP)
                                   ,new SqlParameter("@IsActive",BO.IsAction)};
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_Sp_CreateModifyCostCenter", param));
    }
    public virtual DataTable GetCostCenterTable()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_Sp_GetCostCenterTable").Tables[0];
    }
    public virtual DataTable GetCostCenterList()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_Sp_GetCostCenterList").Tables[0];
    }

    public virtual DataTable GetCostCenterListforProfitLoss()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_Sp_GetCostCenterListforProfitLoss").Tables[0];
    }

    public virtual string DeleteCostCenter(int CostCenterID)
    {
        SqlParameter[] param = { new SqlParameter("@CostCenterID", CostCenterID) };
        return SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "[vt_SCGL_Sp_DeleteCostCenter]", param).ToString();
    }

    public virtual SuperAdmin_BAL GetCostCenterByID(int CostCenterID)
    {

        SuperAdmin_BAL BO = new SuperAdmin_BAL();
        SqlParameter[] param = { new SqlParameter("@CostCenterID", CostCenterID) };
        using (SqlDataReader dr = SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "[vt_SCGL_Sp_GetCostCenterByCenterID]", param))
        {
            if (dr.Read())
            {
                BO.CostCenterID = Convert.ToInt32(dr["CostCenterID"]);
                BO.CostCenterName = dr["CostCenterName"].ToString();
                BO.IsAction = Convert.ToInt16(dr["IsActive"]);
            }
        }
        return BO;
    }



    
}
