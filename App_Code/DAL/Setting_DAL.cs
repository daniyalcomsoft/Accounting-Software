using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SW.SW_Common;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Setting_DAL
/// </summary>
public class Setting_DAL
{
    public Setting_DAL()
    {

    }
    public virtual DataTable GetSetingData()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SPGetSettingsRecord").Tables[0];
    }
    public virtual Setting_BAL GetSettingInfo(int Setting_Id)
    {
        Setting_BAL Setting = new Setting_BAL();
        SqlParameter[] param = { new SqlParameter("@Setting_Id", Setting_Id) };
        using (SqlDataReader dr = SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_SCGL_SPGetSettingsRecordByID", param))
        {
            if (dr.Read())
            {
                Setting.Setting_Id = SCGL_Common.Convert_ToInt(dr["Setting_Id"]);
               
                Setting.InventoryIncomeAccount = dr["InventoryIncomeAccount"].ToString();
                
                Setting.DepositAccount = dr["DepositAccount"].ToString();
                Setting.CustomerAccount = dr["CustomerAccount"].ToString();
                Setting.CostOfGoodsSoldAccount = dr["CostOfGoodsSoldAccount"].ToString();
                Setting.InventoryAccount = dr["InventoryAccount"].ToString();
                Setting.InventoryAdjAccount = dr["InventoryAdjAccount"].ToString();
                Setting.PurchaseAccount = dr["PurchaseAccount"].ToString();
                Setting.PurchaseDiscountAccount = dr["PurchaseDiscountAccount"].ToString();
                Setting.SaleDiscountAccount = dr["SaleDiscountAccount"].ToString();
                Setting.ExpenseAccount = dr["ExpenseAccount"].ToString();
                Setting.SalesTaxAccount = dr["SalesTaxAccount"].ToString();
                Setting.ShippingAccount = dr["ShippingAccount"].ToString();
                Setting.DetentionExpenseAccount = dr["DetentionExpenseAccount"].ToString();
                Setting.ImpressedAccount = dr["ImpressedAccount"].ToString();
                Setting.CashAccount = dr["CashAccount"].ToString();
            }
        }
        return Setting;
    }
    public virtual bool CreateModifySetting(Setting_BAL SetBAL)
    {
        SqlParameter[] param = {new SqlParameter("@Setting_Id", SetBAL.Setting_Id)
                                  
                                      ,new SqlParameter("@InventoryIncomeAccount",SetBAL.InventoryIncomeAccount)
                                      ,new SqlParameter("@DepositAccount",SetBAL.DepositAccount)
                                      ,new SqlParameter("@CustomerAccount",SetBAL.CustomerAccount)
                                      ,new SqlParameter("@CostOfGoodsSoldAccount",SetBAL.CostOfGoodsSoldAccount)
                                      ,new SqlParameter("@InventoryAccount",SetBAL.InventoryAccount)
                                      ,new SqlParameter("@InventoryAdjAccount",SetBAL.InventoryAdjAccount)
                                      ,new SqlParameter("@PurchaseAccount",SetBAL.PurchaseAccount)
                                      ,new SqlParameter("@PurchaseDiscountAccount",SetBAL.PurchaseDiscountAccount)
                                      ,new SqlParameter("@SaleDiscountAccount",SetBAL.SaleDiscountAccount)
                                      ,new SqlParameter("@ExpenseAccount",SetBAL.ExpenseAccount)
                                      ,new SqlParameter("@SalesTaxAccount",SetBAL.SalesTaxAccount)
                                      ,new SqlParameter("@ShippingAccount",SetBAL.ShippingAccount)
                                      ,new SqlParameter("@DetentionExpenseAccount",SetBAL.DetentionExpenseAccount)
                                      ,new SqlParameter("@ImpressedAccount",SetBAL.ImpressedAccount)
                                      ,new SqlParameter("@CashAccount",SetBAL.CashAccount)
                                    };
        int i = SqlHelper.ExecuteNonQuery(SCGL_Common.ConnectionString, "vt_SCGL_SpModifySettings", param);
        return i >= 1;
    }

    public virtual int GetCheckIncomeAccountinuse(int FinYearID)
    {
        SqlParameter param = new SqlParameter("@FinYearID", FinYearID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_GetCheckIncomeAccountinuse", param));
    }

    public virtual int GetCheckReceivableAccountinuse(int FinYearID)
    {
        SqlParameter param = new SqlParameter("@FinYearID", FinYearID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_GetCheckReceivableAccountinuse", param));
    }
    public virtual int GetCheckCOGSAccountinuse(int FinYearID)
    {
        SqlParameter param = new SqlParameter("@FinYearID", FinYearID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_GetCheckCOGSAccountinuse", param));
    }
    public virtual int GetCheckInventoryAccountinuse(int FinYearID)
    {
        SqlParameter param = new SqlParameter("@FinYearID", FinYearID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_GetCheckInventoryAccountinuse", param));
    }
    public virtual int GetCheckInventoryAdjAccountinuse(int FinYearID)
    {
        SqlParameter param = new SqlParameter("@FinYearID", FinYearID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_GetCheckInventoryAdjAccountinuse", param));
    }

    public virtual int GetCheckPurchaseAccountinuse(int FinYearID)
    {
        SqlParameter param = new SqlParameter("@FinYearID", FinYearID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_GetCheckPurchaseAccountinuse", param));
    }
    public virtual int GetCheckPurchaseDiscountAccountinuse(int FinYearID)
    {
        SqlParameter param = new SqlParameter("@FinYearID", FinYearID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_GetCheckPurchaseDiscountAccountinuse", param));
    }
    public virtual int GetCheckSaleDiscountAccountinuse(int FinYearID)
    {
        SqlParameter param = new SqlParameter("@FinYearID", FinYearID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_GetCheckSaleDiscountAccountinuse", param));
    }
    public virtual int GetCheckExpenseAccountinuse(int FinYearID)
    {
        SqlParameter param = new SqlParameter("@FinYearID", FinYearID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_GetCheckExpenseAccountinuse", param));
    }
    public virtual int GetCheckSalesTaxAccountinuse(int FinYearID)
    {
        SqlParameter param = new SqlParameter("@FinYearID", FinYearID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_GetCheckSalesTaxAccountinuse", param));
    }
    public virtual int GetCheckShippingAccountinuse(int FinYearID)
    {
        SqlParameter param = new SqlParameter("@FinYearID", FinYearID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_GetCheckShippingTaxAccountinuse", param));
    }
    public virtual int GetCheckDetentionExpenseAccountinuse(int FinYearID)
    {
        SqlParameter param = new SqlParameter("@FinYearID", FinYearID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_GetCheckDetentionExpenseAccountinuse", param));
    }
    public virtual int GetCheckImpressedAccountinuse(int FinYearID)
    {
        SqlParameter param = new SqlParameter("@FinYearID", FinYearID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_GetCheckImpressedAccountinuse", param));
    }
    public virtual int GetCheckCashAccountinuse(int FinYearID)
    {
        SqlParameter param = new SqlParameter("@FinYearID", FinYearID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_GetCheckCashAccountinuse", param));
    }
}
