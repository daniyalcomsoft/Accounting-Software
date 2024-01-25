using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InventoryForm_BAL
/// </summary>
public class InventoryForm_BAL:InventoryForm_DAL
{
	public InventoryForm_BAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public override System.Data.DataTable GetInventoryData()
    {
        return base.GetInventoryData();
    }

    public override System.Data.DataTable searchInventoryByFishRate(int Rate)
    {
        return base.searchInventoryByFishRate(Rate);
    }

    public override string DeleteInventory(int InventId)
    {
        return base.DeleteInventory(InventId);
    }
    public override string DeleteAdjustmentInventory(int AdjustmentId)
    {
        return base.DeleteAdjustmentInventory(AdjustmentId);
    }
    public override System.Data.DataTable GetAdjustment_byID(int AdjustmentID)
    {
        return base.GetAdjustment_byID(AdjustmentID);
    }
    public override InventoryForm_BAL GetInventoryInfo(int Id)
    {
        return base.GetInventoryInfo(Id);
    }

    public override System.Data.DataTable CreateModifyInventoryForm(InventoryForm_BAL InventBAL)
    {
        return base.CreateModifyInventoryForm(InventBAL);
    }

  

    // load Bank dropdowns
    public override System.Data.DataTable GetInventAssetAcc()
    {
        return base.GetInventAssetAcc();
    }
    public override System.Data.DataTable GetIncomAcc()
    {
        return base.GetIncomAcc();
    }
    public override System.Data.DataTable GetExpenseAcc()
    {
        return base.GetExpenseAcc();
    }
    public override bool CreateModifyAdjustmentInventory(InventoryForm_BAL InventBAL)
    {
        return base.CreateModifyAdjustmentInventory(InventBAL);
    }
    public override System.Data.DataTable GetAdjustmentInventoryData(int FinYearID)
    {
        return base.GetAdjustmentInventoryData(FinYearID);
    }
    public override System.Data.DataTable SearchAdjustmentInventoryRecordByAdjustmentID(int AdjustmentID, int FinYearID)
    {
        return base.SearchAdjustmentInventoryRecordByAdjustmentID(AdjustmentID, FinYearID);
    }

    public override System.Data.DataTable SearchAdjustmentInventoryRecordByRate(string Rate, int FinYearID)
    {
        return base.SearchAdjustmentInventoryRecordByRate(Rate, FinYearID);
    }


    public override System.Data.DataTable SearchAdjustmentInventoryRecordByAction(int Action, int FinYearID)
    {
        return base.SearchAdjustmentInventoryRecordByAction(Action, FinYearID);
    }

    public override int CheckInventoryName(string InventoryName, int InventoryID)
    {
        return base.CheckInventoryName(InventoryName, InventoryID);
    }

    public override System.Data.DataTable searchInventoryName(string InventoryName)
    {
        return base.searchInventoryName(InventoryName);
    }


    public int Inventory_Id { get; set; }
    public int AdjustmentInventory_Id { get; set; }
    public int Inventory_AdjustmentId { get; set; }
    public int InventoryAdjustmentId { get; set; }
    public int FishId { get; set; }
    public int FishGradeId { get; set; }
    public int FishSizeId { get; set; }
    public string InventoryName { get; set; }
    public decimal InitialQuantity { get; set; }
    public int CostCenterID { get; set; }
    public DateTime AsOfDate { get; set; }
    public DateTime AdjustmentDate { get; set; }
    public string InventoryAssetAcc { get; set; }
    public string DescOnSalesForm { get; set; }
    public decimal Rate { get; set; }
    public decimal AdjustmentRate { get; set; }
    public string AdjustmentAction { get; set; }
    public decimal AdjustmentQuantity { get; set; }
    public string IncomeAccount { get; set; }
    public string DescOnPurchaseForm { get; set; }
    public decimal Cost { get; set; }
    public string ExpensiveAccount { get; set; }
    public int RoleID { get; set; }
    public int UserID { get; set; }
    public int Site_ID { get; set; }

    
}
