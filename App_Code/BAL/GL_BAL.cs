using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GL_BAL
/// </summary>
public class GL_BAL:GL_DAL
{
	public GL_BAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public enum VoucherType { General_Voucher = 1, Cash_Payment_Voucher, Cash_Recievalbe_Voucher, Bank_Payment_Voucher, Bank_Recievable_Voucher }
    public override int DeleteGL(int VoucherNumber)
    {
        return base.DeleteGL(VoucherNumber);
    }
    public override System.Data.DataTable GetCashBankAccount()
    {
        return base.GetCashBankAccount();
    }
    public override System.Data.DataTable GetDepositAccount()
    {
        return base.GetDepositAccount();
    }
    public override System.Data.DataTable GetOpeningBalanceByCode(string Code, int YearID, string Code2)
    {
        return base.GetOpeningBalanceByCode(Code, YearID, Code2);
    }
    public override int DeleteOpening(int YearID)
    {
        return base.DeleteOpening(YearID);
    }
    public override System.Data.DataTable GetVoucherByVoucherTypeID(string VoucherTypeID, int FinYearID)
    {
        return base.GetVoucherByVoucherTypeID(VoucherTypeID, FinYearID);
    }
    public override System.Data.DataTable GetVoucherType()
    {
        return base.GetVoucherType();
    }
    public override System.Data.DataTable GetWithdrawalAccount()
    {
        return base.GetWithdrawalAccount();
    }
    public override bool IsRefCodeAvailable(string ReferenceNumber)
    {
        return base.IsRefCodeAvailable(ReferenceNumber);
    }
    public override void UpdateOpeningBalance(int SubsidaryID, double OpeningBalance)
    {
        base.UpdateOpeningBalance(SubsidaryID, OpeningBalance);
    }
    public override void insertUpdateOpeningBalance(int YearID, string Code, double OpeningBalance)
    {
        base.insertUpdateOpeningBalance(YearID, Code, OpeningBalance);
    }
    public override int Get_CodenFinYearID(int YearID, string Code)
    {
        return base.Get_CodenFinYearID(YearID, Code);
    }
    public override System.Data.DataTable SearchVoucherListByVoucherType(string VoucherType, int FinYearID)
    {
        return base.SearchVoucherListByVoucherType(VoucherType, FinYearID);
    }
    public override System.Data.DataTable SearchVoucherListByVoucherNo(int VoucherNo, int FinYearID)
    {
        return base.SearchVoucherListByVoucherNo(VoucherNo, FinYearID);
    }
}
