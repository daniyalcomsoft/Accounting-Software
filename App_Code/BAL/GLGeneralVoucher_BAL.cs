using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GLGeneralVoucher_BAL
/// </summary>
public class GLGeneralVoucher_BAL:GLGeneralVoucher_DAL
{

    public int? VoucherTypeID { get; set; }
    public string VoucherTypeName { get; set; }
    public string VoucherNumber { get; set; }
    public string ReferenceNo { get; set; }
    public string Narration { get; set; }
    public string VoucharDate { get; set; }
    public string Dimension { get; set; }
    public Int16? IsActive { get; set; }
    public bool? IsPosted { get; set; }
	public GLGeneralVoucher_BAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public override int DeleteTransaction(List<string> list)
    {
        return base.DeleteTransaction(list);
    }
    public override System.Data.DataTable GetAccountName(string AccountCode, int YearID, string YearFrom, string YearTo)
    {
        return base.GetAccountName(AccountCode, YearID, YearFrom, YearTo);
    }
    public override System.Data.DataTable GetAccountName2(string AccountCode, int YearID, string YearFrom, string YearTo)
    {
        return base.GetAccountName2(AccountCode, YearID, YearFrom, YearTo);
    }
    public override System.Data.DataSet GetRecordByVoucherNumber(string VoucherNumber)
    {
        return base.GetRecordByVoucherNumber(VoucherNumber);
    }
    public override System.Data.DataSet InsertIntoTransaction(GLGeneralVoucher_BAL BO, SCGL_Session SBO, System.Data.DataTable GeneralEntries)
    {
        return base.InsertIntoTransaction(BO, SBO, GeneralEntries);
    }
    public override System.Data.DataTable GetYear_Account(int YearID)
    {
        return base.GetYear_Account(YearID);
    }
    public override System.Data.DataTable GetVoucherType()
    {
        return base.GetVoucherType();
    }

    public override int getAccountNature(int Code)
    {
        return base.getAccountNature(Code);
    }
}
