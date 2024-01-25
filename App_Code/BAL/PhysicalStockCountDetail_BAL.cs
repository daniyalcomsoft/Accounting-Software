using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class PhysicalStockCountDetail_BAL : PhysicalStockCountDetail_DAL
{
    public int PhysicalStockCountID { get; set; }
    public int DayID { get; set; }
    public int Invontory_Id { get; set; }
    public int Quantity { get; set; }
    public int Rate { get; set; }
    public double Amount { get; set; }

    public PhysicalStockCountDetail_BAL()
    {
    }
    public override bool CreateModifyPhysicalStockCountDetail(PhysicalStockCountDetail_BAL PSCD_BAL)
    {
        try { return base.CreateModifyPhysicalStockCountDetail(PSCD_BAL); }
        catch (Exception ex)
        { throw ex; }
    }
    public override System.Data.DataTable getPhysicalStockCountDetailByDayID(int dayID)
    {
        try { return base.getPhysicalStockCountDetailByDayID(dayID); }
        catch (Exception ex)
        { throw ex; }
    }
    public override int DeletePhysicalStockCountDetail(int dayID)
    {
        try { return base.DeletePhysicalStockCountDetail(dayID); }
        catch (Exception ex)
        { throw ex; }
    }
    public override string DeletePhysicalStockCountPernmanent(int PhSCId)
    {
        return base.DeletePhysicalStockCountPernmanent(PhSCId);
    }
   
}
