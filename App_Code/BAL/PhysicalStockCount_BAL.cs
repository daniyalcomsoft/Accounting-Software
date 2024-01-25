using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class PhysicalStockCount_BAL : PhysicalStockCount_DAL
{
    public int Inventory_Id { get; set; }
    public int FishId { get; set; }
    public int FishGradeId { get; set; }
    public int FishSizeId { get; set; }
    public decimal PhysicalStockCount { get; set; }
    public decimal OnHand { get; set; }
    public decimal Difference { get; set; }
    public string date { get; set; }
    public int CostCenterID { get; set; }
    public int FinYearID { get; set; }
    public int ExcessShortID { get; set; }

    
    public PhysicalStockCount_BAL()
    {}

    ////public override int CreateModifyPhysicalStockCount(PhysicalStockCount_BAL PSC_BAL)
    ////{
    ////    try { return base.CreateModifyPhysicalStockCount(PSC_BAL); }
    ////    catch (Exception ex)
    ////    { throw ex; }        
    ////}
    public override System.Data.DataTable getPhysicalStockCountByID(int dayID)
    {
        try { return base.getPhysicalStockCountByID(dayID); }
        catch (Exception ex)
        { throw ex; }        
    }

    public override int DeletePhysicalStockCountandExcess(PhysicalStockCount_BAL PSC_BAL, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.DeletePhysicalStockCountandExcess(PSC_BAL, Trans);
    }
    public override System.Data.DataTable getValuationByDate(int FinYearID, int CostCenterID, string Date)
    {
        return base.getValuationByDate(FinYearID, CostCenterID, Date);
    }

    public override int DeleteExcessShortfromGL(PhysicalStockCount_BAL PSC_BAL, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.DeleteExcessShortfromGL(PSC_BAL, Trans);
    }

    public override System.Data.SqlClient.SqlDataReader getValuationByDate1(int FinYearID, int CostCenterID, string Date)
    {
        return base.getValuationByDate1(FinYearID, CostCenterID, Date);
    }
    public override bool CreateModifyPhysicalStock(PhysicalStockCount_BAL PSC_BAL, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.CreateModifyPhysicalStock(PSC_BAL, Trans);
    }
    public override int CreateModifyExcessShort(PhysicalStockCount_BAL PSC_BAL, System.Data.SqlClient.SqlTransaction Trans)
    {
        return base.CreateModifyExcessShort(PSC_BAL, Trans);
    }

    public override int CountTotalPurchasesandSalesInvoices(DateTime Date)
    {
        return base.CountTotalPurchasesandSalesInvoices(Date);
    }

    public override int CheckValuationByDate(DateTime Date, int CostCenterID)
    {
        return base.CheckValuationByDate(Date, CostCenterID);
    }
    public override System.Data.DataTable GetAlreadyPresentPhysicalStock(int FinYearID, int CostCenterID, string Date)
    {
        return base.GetAlreadyPresentPhysicalStock(FinYearID, CostCenterID, Date);
    }
    public override System.Data.DataTable GetLastExcessShortID()
    {
        return base.GetLastExcessShortID();
    }

    public override System.Data.DataTable GetUpdateExcessShortID(DateTime Date, int CostCenterID)
    {
        return base.GetUpdateExcessShortID(Date, CostCenterID);
    }

    public override System.Data.DataTable getPhysicalStockCountByDate(string date)
    {
        return base.getPhysicalStockCountByDate(date);
    }

}
