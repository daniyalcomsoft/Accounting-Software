using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GLCashPaymentVoucher_BAL
/// </summary>
public class GLCashPaymentVoucher_BAL:GLCashPaymentVoucher_DAL
{
    public int TransactionID { get; set; }
    public int? VoucherTypeID { get; set; }
    public string VoucherTypeName { get; set; }
    public string VoucherNumber { get; set; }
    public string ReferenceNo { get; set; }
    public string Narration { get; set; }
    public DateTime VoucharDate { get; set; }
    public string Dimension { get; set; }
    public string MainCode { get; set; }
    public string ControlCode { get; set; }
    public string SubsidiaryCode { get; set; }
    public string Code { get; set; }
    public double? Debit { get; set; }
    public double? Credit { get; set; }
    public int? CostCenterID { get; set; }
    public string Remarks { get; set; }
    public Int16? IsActive { get; set; }
    public bool? IsPosted { get; set; }
    public int FinYearID { get; set; }
    public int JobID { get; set; }

	public GLCashPaymentVoucher_BAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public override System.Data.DataSet GetCashPaymentRecordByVoucherNumber(string VoucherNumber)
    {
        return base.GetCashPaymentRecordByVoucherNumber(VoucherNumber);
    }
    public override System.Data.DataTable GetSubAccNameCashPaymentVoucherLike(string Match)
    {
        return base.GetSubAccNameCashPaymentVoucherLike(Match);
    }

    public override System.Data.DataSet InsertUpdateTransaction(GLCashPaymentVoucher_BAL BO, SCGL_Session SBO, System.Data.DataTable TransTable)
    {
        return base.InsertUpdateTransaction(BO, SBO, TransTable);
    }

    public override string GetTitleByCodeNumber(string CodeNumber)
    {
        return base.GetTitleByCodeNumber(CodeNumber);
    }
    public override string GetTitleByCodeNumber2(string CodeNumber)
    {
        return base.GetTitleByCodeNumber2(CodeNumber);
    }
    
}
