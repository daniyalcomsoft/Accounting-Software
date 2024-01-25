using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SW.SW_Common;
using System.Data;

/// <summary>
/// Summary description for Financial_BAL
/// </summary>
public class Financial_BAL : Financial_DAL
{
    public Financial_BAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int FinYearID { get; set; }
    public string FinYearTitle { get; set; }
    public string YearFrom { get; set; }
    public string YearTo { get; set; }
    public override DataTable getFinancialYear()
    {
        return base.getFinancialYear();
    }
    public override Financial_BAL GetFinancialYearByID(int FinYearID)
    {
        return base.GetFinancialYearByID(FinYearID);
    }
    public override string DeleteFinancialYear(int FinYearID, string StartDate, string EndDate)
    {
        return base.DeleteFinancialYear(FinYearID, StartDate, EndDate);
    }
    public override int CreateModifyFinancial(Financial_BAL FY, SCGL_Session SBO)
    {
        return base.CreateModifyFinancial(FY, SBO);
    }
    public override int SetDefaultFinancialYear(int FinYearID)
    {
        return base.SetDefaultFinancialYear(FinYearID);
    }
    public override int CountOverlapPeriods(int FinYearID, DateTime StartDate, DateTime EndDate)
    {
        return base.CountOverlapPeriods(FinYearID, StartDate, EndDate);
    }
}
