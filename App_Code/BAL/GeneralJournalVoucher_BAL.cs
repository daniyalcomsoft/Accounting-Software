using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GeneralJournalVoucher_BAL
/// </summary>
public class GeneralJournalVoucher_BAL:GeneralJournalVoucher_DAL
{

    public int? VoucherTypeID { get; set; }
    public string VoucherTypeName { get; set; }
    public string VoucherNumber { get; set; }
    public string ReferenceNo { get; set; }
    public string Narration { get; set; }
    public DateTime VoucharDate { get; set; }
    public string Dimension { get; set; }
    public Int16? IsActive { get; set; }
    public bool? IsPosted { get; set; }
    public int FinYearID { get; set; }
    public int JobID { get; set; }
	public GeneralJournalVoucher_BAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public override int DeleteTransaction(List<string> list)
    {
        return base.DeleteTransaction(list);
    }
    public override System.Data.DataTable GetAccountName(string AccountCode)
    {
        return base.GetAccountName(AccountCode);
    }
    public override System.Data.DataSet GetRecordByVoucherNumber(string VoucherNumber)
    {
        return base.GetRecordByVoucherNumber(VoucherNumber);
    }
    public override System.Data.DataSet InsertIntoTransaction(GeneralJournalVoucher_BAL BO, SCGL_Session SBO, System.Data.DataTable GeneralEntries)
    {
        return base.InsertIntoTransaction(BO, SBO, GeneralEntries);
    }
    public override System.Data.DataTable GetVoucherType()
    {
        return base.GetVoucherType();
    }
   
}
