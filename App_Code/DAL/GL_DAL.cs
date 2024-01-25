using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SW.SW_Common;
using System.Data;

public class GL_DAL
{
	public GL_DAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public virtual DataTable GetVoucherByVoucherTypeID(string VoucherTypeID, int FinYearID)
    {
        SqlParameter[] param = { new SqlParameter("@VoucherTypeID", VoucherTypeID),
                                 new SqlParameter("@FinYearID", FinYearID)};
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetTransactionByVoucherTypeID", param).Tables[0];
    }
    public virtual void UpdateOpeningBalance(int SubsidaryID, double OpeningBalance)
    {
        SqlParameter[] parameter =  {new SqlParameter("@SubsidaryID",SubsidaryID)
                                        ,new SqlParameter("@OpeningBalance",OpeningBalance)};
        SqlHelper.ExecuteNonQuery(SCGL_Common.ConnectionString, "vt_SCGL_SpUpdateOpeningBalance", parameter);
    }
    public virtual void insertUpdateOpeningBalance(int YearID,string Code ,double OpeningBalance)
    {
        SqlParameter[] parameters =  {new SqlParameter("@YearID",YearID)
                                    ,new SqlParameter("@Code",Code)
                                        ,new SqlParameter("@OpeningBalance",OpeningBalance)};
        SqlHelper.ExecuteNonQuery(SCGL_Common.ConnectionString, "vt_SCGL_SpinsertOpeningBalance", parameters);
    }
    public virtual int DeleteGL(int VoucherNumber)
    {
        SqlParameter[] param = { new SqlParameter("@VoucherNumber", VoucherNumber) };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SpDeleteGL", param));
    }
    public virtual DataTable GetVoucherType()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SpGetVoucherType").Tables[0];
    }
    public virtual bool IsRefCodeAvailable(string ReferenceNumber)
    {
        SqlParameter[] param = { new SqlParameter("@ReferenceNumber", ReferenceNumber) };
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_Sp_IsRefCodeAvailable", param));
    }
    public virtual DataTable GetOpeningBalanceByCode(string Code,int YearID,string Code2)
    {
        SqlParameter[] parama = { new SqlParameter("@Code", Code)
                               , new SqlParameter("@YearID", YearID)
                                , new SqlParameter("@Code2", Code2)};
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpOpeningBalanceByCode", parama).Tables[0];
    }
    public virtual DataTable GetCashBankAccount()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_Sp_GetCashBankAccount").Tables[0];
    }
    public virtual DataTable GetDepositAccount()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SP_GetDepositAccount").Tables[0];
    }
    public virtual DataTable GetWithdrawalAccount()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SP_GetWithdrawalAccount").Tables[0];
    }

    public virtual int Get_CodenFinYearID(int YearID, string Code)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@YearID",YearID),
                                    new SqlParameter("@Code",Code)
                               };
        return SCGL_Common.Convert_ToInt(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "aei_sp_getOpeningBalancebyFinID", param));
    }
    public virtual int DeleteOpening(int YearID)
    {
        SqlParameter[] param = { new SqlParameter("@YearID", YearID)
                                };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SpDeleteOpening", param));
    }
    public virtual DataTable SearchVoucherListByVoucherType(string VoucherType,int FinYearID)
    {
        SqlParameter[] param = {new SqlParameter("@VoucherType", VoucherType),
                                new SqlParameter("@FinYearID", FinYearID)};

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpSearchVoucherListByVoucherType", param).Tables[0];
        return dt;
    }
    public virtual DataTable SearchVoucherListByVoucherNo(int VoucherNo, int FinYearID)
    {
        SqlParameter[] param = { new SqlParameter("@VoucherNo", VoucherNo),
                                new SqlParameter("@FinYearID", FinYearID)};

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpSearchVoucherListByVoucherNo", param).Tables[0];
        return dt;
    }
}
