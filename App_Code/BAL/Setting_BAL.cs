using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Setting_BAL
/// </summary>
public class Setting_BAL:Setting_DAL
{
	public Setting_BAL()
	{
	}
    public int Setting_Id { get; set; }
    public string InventoryAssetAccount { get; set; }
    public string InventoryIncomeAccount { get; set; }
    public string InventoryExpenseAccount { get; set; }
    public string DepositAccount { get; set; }
    public string CustomerAccount { get; set; }
    public string CostOfGoodsSoldAccount { get; set; }
    public string InventoryAccount { get; set; }
    public string InventoryAdjAccount { get; set; }
    public string PurchaseAccount { get; set; }
    public string PurchaseDiscountAccount { get; set; }
    public string SaleDiscountAccount { get; set; }
    public string ExpenseAccount { get; set; }
    public string SalesTaxAccount { get; set; }
    public string ShippingAccount { get; set; }
    public string DetentionExpenseAccount { get; set; }
    public string ImpressedAccount { get; set; }
    public string CashAccount { get; set; }

    public override System.Data.DataTable GetSetingData()
    {
        return base.GetSetingData();
    }
    public override Setting_BAL GetSettingInfo(int Setting_Id)
    {
        return base.GetSettingInfo(Setting_Id);
    }
    public override bool CreateModifySetting(Setting_BAL SetBAL)
    {
        return base.CreateModifySetting(SetBAL);
    }
    public override int GetCheckIncomeAccountinuse(int FinYearID)
    {
        return base.GetCheckIncomeAccountinuse(FinYearID);
    }
    public override int GetCheckReceivableAccountinuse(int FinYearID)
    {
        return base.GetCheckReceivableAccountinuse(FinYearID);
    }
    public override int GetCheckCOGSAccountinuse(int FinYearID)
    {
        return base.GetCheckCOGSAccountinuse(FinYearID);
    }
    public override int GetCheckInventoryAccountinuse(int FinYearID)
    {
        return base.GetCheckInventoryAccountinuse(FinYearID);
    }
    public override int GetCheckInventoryAdjAccountinuse(int FinYearID)
    {
        return base.GetCheckInventoryAdjAccountinuse(FinYearID);
    }
    public override int GetCheckPurchaseAccountinuse(int FinYearID)
    {
        return base.GetCheckPurchaseAccountinuse(FinYearID);
    }
    public override int GetCheckPurchaseDiscountAccountinuse(int FinYearID)
    {
        return base.GetCheckPurchaseDiscountAccountinuse(FinYearID);
    }
    public override int GetCheckSaleDiscountAccountinuse(int FinYearID)
    {
        return base.GetCheckSaleDiscountAccountinuse(FinYearID);
    }
    public override int GetCheckExpenseAccountinuse(int FinYearID)
    {
        return base.GetCheckExpenseAccountinuse(FinYearID);
    }
    public override int GetCheckSalesTaxAccountinuse(int FinYearID)
    {
        return base.GetCheckSalesTaxAccountinuse(FinYearID);
    }
    public override int GetCheckShippingAccountinuse(int FinYearID)
    {
        return base.GetCheckShippingAccountinuse(FinYearID);
    }
}
