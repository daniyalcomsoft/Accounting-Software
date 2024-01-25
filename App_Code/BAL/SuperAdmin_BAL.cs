using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SuperAdmin_BAL
/// </summary>
public class SuperAdmin_BAL:SuperAdmin_DAL
{
    public int SetupID { get; set; }
    public string SiteName { get; set; }
    public string SiteCode { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Address { get; set; }
    public string ContactNumber { get; set; }
    public string ContactPerson { get; set; }
    public string ContactPersonDesg { get; set; }
    public string ContactPersonEmail { get; set; }
    public string CustomAgent { get; set; }
    public string SNTN { get; set; }
    public string SalesTaxRegNo { get; set; }
    public Int16 Mod_Financials { get; set; }
    public Int16 Mod_DepositAccount { get; set; }
    public Int16 Mod_TermDeposit { get; set; }
    public Int16 Mod_Loan { get; set; }
    public int CreatedBy { get; set; }

    public int CostCenterID { get; set; }
    public string CostCenterName { get; set; }
    public int IsAction { get; set; }
    public int SalesTaxID { get; set; }
    public string SalesTaxName { get; set; }
    public string AgencyName { get; set; }
    public decimal Rate { get; set; }
    public int IsActive { get; set; }
    public int LoginID { get; set; }
	public SuperAdmin_BAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public override int CreateModifyCostCenter(SuperAdmin_BAL BO, SCGL_Session SBO)
    {
        return base.CreateModifyCostCenter(BO, SBO);
    }
    public override int CreateModifySetup(SuperAdmin_BAL BO, SCGL_Session SBO)
    {
        return base.CreateModifySetup(BO, SBO);
    }
    public override string DeleteCostCenter(int CostCenterID)
    {
        return base.DeleteCostCenter(CostCenterID);
    }
    public override SuperAdmin_BAL GetCostCenterByID(int CostCenterID)
    {
        return base.GetCostCenterByID(CostCenterID);
    }
    public override System.Data.DataTable GetCostCenterList()
    {
        return base.GetCostCenterList();
    }

    public override System.Data.DataTable GetCostCenterListforProfitLoss()
    {
        return base.GetCostCenterListforProfitLoss();
    }

    public override System.Data.DataTable GetCostCenterTable()
    {
        return base.GetCostCenterTable();
    }
    public override System.Data.DataTable SelectSetupInfoBySetupID(SuperAdmin_BAL BO, SCGL_Session SBO)
    {
        return base.SelectSetupInfoBySetupID(BO, SBO);
    }
    public override System.Data.DataTable GetSiteName()
    {
        return base.GetSiteName();
    }
    

}
