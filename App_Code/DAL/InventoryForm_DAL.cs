using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SW.SW_Common;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for InventoryForm_DAL
/// </summary>
public class InventoryForm_DAL
{
	public InventoryForm_DAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public virtual DataTable GetInventoryData()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SPGetInventoryRecord").Tables[0];
    }

    public virtual DataTable searchInventoryByFishRate(int Rate)
    {
        SqlParameter[] param = { new SqlParameter("@Rate", Rate) };

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpSearchInventory_BYFishRate", param).Tables[0];
        return dt;
    }


    public virtual string DeleteInventory(int InventId)
    {
        SqlParameter[] param = { new SqlParameter("@InventId", InventId) };
        ;
        return SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SPDeleteInventoryRecord", param).ToString();
    }

    public virtual string DeleteAdjustmentInventory(int AdjustmentId)
    {
        SqlParameter[] param = { new SqlParameter("@AdjustmentId", AdjustmentId) };
        ;
        return SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SPDeleteAdjustmentInventoryRecord", param).ToString();
    }

    public virtual InventoryForm_BAL GetInventoryInfo(int InventId)
    {
        InventoryForm_BAL Invent = new InventoryForm_BAL();
        SqlParameter[] param = { new SqlParameter("@InventId", InventId) };
        using (SqlDataReader dr = SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_SCGL_SPGetInventoryRecordByID", param))
        {
            if (dr.Read())
            {
                Invent.Inventory_Id = SCGL_Common.Convert_ToInt(dr["InventoryID"]);
                Invent.InventoryName = dr["InventoryName"].ToString();
                Invent.InitialQuantity = SCGL_Common.Convert_ToDecimal(dr["InitialQuantity"].ToString());
                Invent.AsOfDate = SCGL_Common.CheckDateTime(dr["AsOfDate"]);
                //Invent.InventoryAssetAcc = SCGL_Common.Convert_ToInt(dr["InventoryAssetAcc"].ToString());
                //Invent.InventoryAssetAcc = dr["InventoryAssetAcc"].ToString();
                //Invent.DescOnSalesForm = dr["DescOnSalesForm"].ToString();
                Invent.Rate = SCGL_Common.Convert_ToDecimal(dr["Rate"].ToString());
                //Invent.IncomeAccount = SCGL_Common.Convert_ToInt(dr["IncomeAccount"].ToString());
                //Invent.IncomeAccount = dr["IncomeAccount"].ToString();
                //Invent.DescOnPurchaseForm = dr["DescOnPurchaseForm"].ToString();
                Invent.Cost = SCGL_Common.Convert_ToDecimal(dr["Cost"].ToString());
                //Invent.ExpensiveAccount = SCGL_Common.Convert_ToInt(dr["ExpensiveAccount"].ToString());
                //Invent.ExpensiveAccount = dr["ExpensiveAccount"].ToString();
            }
        }
        return Invent;
    }

    public virtual DataTable CreateModifyInventoryForm(InventoryForm_BAL InventBAL)
    {
        SqlParameter[] param = {
                                   new SqlParameter("@InventoryID", InventBAL.Inventory_Id)
                                   ,new SqlParameter("@InventoryName",InventBAL.InventoryName)
                                   ,new SqlParameter("@InitialQuantity",InventBAL.InitialQuantity)
                                   ,new SqlParameter("@AsOfDate",InventBAL.AsOfDate)
                                   ,new SqlParameter("@Rate",InventBAL.Rate)
                                   ,new SqlParameter("@Amount",InventBAL.Cost)
                                                                     
                                  };
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpCreateModifyInventory", param).Tables[0];
        return dt;
    }

   

    // load Bank dropdowns
    public virtual DataTable GetIncomAcc()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SPGetIncomAcc").Tables[0];
    }
    public virtual DataTable GetInventAssetAcc()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SPGetInventAssetAcc").Tables[0];
    }
    public virtual DataTable GetExpenseAcc()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SPGetExpenseAcc").Tables[0];
    }
    public virtual bool CreateModifyAdjustmentInventory(InventoryForm_BAL InventBAL)
    {
        SqlParameter[] param3 = {
                                   new SqlParameter("@InventoryAdjustmentID",InventBAL.InventoryAdjustmentId)
                                   , new SqlParameter("@Date",InventBAL.AdjustmentDate)
                                   , new SqlParameter("@InventoryID",InventBAL.AdjustmentInventory_Id)
                                   , new SqlParameter("@Action",InventBAL.AdjustmentAction)
                                   , new SqlParameter("@Quantity",InventBAL.AdjustmentQuantity)
                                   , new SqlParameter("@Rate",InventBAL.AdjustmentRate)
                                  

                               };
        int i = SqlHelper.ExecuteNonQuery(SCGL_Common.ConnectionString, "vt_sp_CreateModifyAdjustmentInventory", param3);
        return i > 0;

    }
    public virtual DataTable GetAdjustmentInventoryData(int FinYearID)
    {
        SqlParameter[] param = { new SqlParameter("@FinYearID", FinYearID) };
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SPGetAdjustmentInventoryRecord",param).Tables[0];
    }
    public virtual DataTable GetAdjustment_byID(int AdjustmentID)
    {
        SqlParameter[] param = {new SqlParameter("@AdjustmentID", AdjustmentID),
                               };
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SPGetAdjustmentBYID", param).Tables[0];

    }
    public virtual DataTable SearchAdjustmentInventoryRecordByAdjustmentID(int AdjustmentID, int FinYearID)
    {
        SqlParameter[] param = { new SqlParameter("@AdjustmentID", AdjustmentID),
                               new SqlParameter("@FinYearID", FinYearID)};

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SPSearchAdjustmentInventoryRecord_BYAdjustmentID", param).Tables[0];
        return dt;
    }
    public virtual DataTable SearchAdjustmentInventoryRecordByRate(string Rate, int FinYearID)
    {
        SqlParameter[] param = { new SqlParameter("@Rate", Rate),
                               new SqlParameter("@FinYearID", FinYearID)};

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SPSearchAdjustmentInventoryRecord_BYRate", param).Tables[0];
        return dt;
    }
    public virtual DataTable SearchAdjustmentInventoryRecordByAction(int Action, int FinYearID)
    {
        SqlParameter[] param = { new SqlParameter("@Rate", Action),
                               new SqlParameter("@FinYearID", FinYearID)};

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SPSearchAdjustmentInventoryRecord_BYAction", param).Tables[0];
        return dt;
    }


    public virtual int CheckInventoryName(string InventoryName,int InventoryID)
    {
        SqlParameter[] param = { new SqlParameter("@InventoryName", InventoryName)
                                ,new SqlParameter("@InventoryID", InventoryID)
                               };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SP_CheckInventory", param));
    }

    public virtual DataTable searchInventoryName(string InventoryName)
    {
        SqlParameter[] param = { new SqlParameter("@InventoryName", InventoryName) };

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_Sp_SearchInventoryName", param).Tables[0];
        return dt;
    }
}
