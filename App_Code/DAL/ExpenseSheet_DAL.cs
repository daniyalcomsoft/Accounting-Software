using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using SW.SW_Common;

/// <summary>
/// Summary description for ExpenseSheet_DAL
/// </summary>
public class ExpenseSheet_DAL
{
	public ExpenseSheet_DAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public virtual int CreateModifyExpenseSheet(ExpenseSheet_BAL BALInvoice, SqlTransaction Trans)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@ExpenseID", BALInvoice.ExpenseID)
                                   ,new SqlParameter("@Date", BALInvoice.Date)
                                   ,new SqlParameter("@Total",BALInvoice.Total)
                                   ,new SqlParameter("@FinYearID",BALInvoice.FinYearID)
                               };
        int i = Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, CommandType.StoredProcedure, "vt_SCGL_SpCreateExpenseSheet", param));
        return i;
    }

    public virtual bool CreateModifyExpenseSheetDetail(ExpenseSheet_BAL BALInvoice, SqlTransaction Trans)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@ExpenseID",BALInvoice.ExpenseID)
                                   ,new SqlParameter("@JobID",BALInvoice.JobID)
                                   ,new SqlParameter("@PaymentType",BALInvoice.PaymentType)
                                   ,new SqlParameter("@Clearing",BALInvoice.Clearing)
                                   ,new SqlParameter("@InfoType",BALInvoice.InfoType)
                                   ,new SqlParameter("@PONo",BALInvoice.PONo)
                                   ,new SqlParameter("@ImpressedAcc",BALInvoice.ImpressedAcc)
                                   ,new SqlParameter("@ExpenseAcc",BALInvoice.ExpenseAcc)
                                   ,new SqlParameter("@PaymentThrough",BALInvoice.PaymentThrough)
                                   ,new SqlParameter("@Description",BALInvoice.Description)
                                   ,new SqlParameter("@Amount",BALInvoice.Amount)

                               };
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_CreateModifyExpenseSheetDetail", param);
        return i > 0;
    }

    public virtual bool Delete_ExpenseSheetDetail(int ExpenseSheetID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@ExpenseSheetID", ExpenseSheetID);
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_Delete_ExpenseSheetDetail", param);
        return i > 0;
    }

    public virtual bool Delete_ExpenseSheetTrans(int ExpenseSheetID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@ExpenseSheetID", ExpenseSheetID);
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_Delete_ExpenseSheetDetailTrans", param);
        return i > 0;
    }

    public virtual DataTable GetJobNo(String JobNo)
    {
        SqlParameter[] param = {
                                   new SqlParameter("@Match", JobNo)

                               };
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SPGetAllJobNo", param).Tables[0];
    }

    public virtual DataTable getExpenseSheetDetail(int ExpenseSheetID)
    {
        SqlParameter param = new SqlParameter("@ExpenseSheetID", ExpenseSheetID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_sp_GetExpenseSheetDetail", param).Tables[0];
        return dt;
    }

    public virtual SqlDataReader Get_Rows_ExpenseSheetDetail_byID(int ExpenseSheetID)
    {
        SqlParameter param = new SqlParameter("@ExpenseSheetID", ExpenseSheetID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetExpenseSheetDetailRowNumber_byID", param);
    }

    public virtual DataTable getExpenseSheet(int ExpenseSheetID)
    {
        SqlParameter param = new SqlParameter("@ExpenseSheetID", ExpenseSheetID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetExpenseSheet", param).Tables[0];
        return dt;
    }

    public virtual DataTable getallExpenseSheets()
    {
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetallExpenseSheets").Tables[0];
        return dt;
    }

    public virtual int DeleteExpenseSheet(int ExpenseSheetID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@ExpenseSheetID", ExpenseSheetID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, "vt_SCGL_SPDeleteExpenseSheet", param));
    }

    public virtual int CheckJobNo(string JobNo)
    {
        SqlParameter[] param = { new SqlParameter("@JobNo", JobNo) };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SPCheckJobNo", param));
    }

    public virtual int CheckAlreadyDateExist(DateTime Date, int ExpenseSheetID)
    {
        SqlParameter[] param = { new SqlParameter("@Date", Date)
                                ,new SqlParameter("@ExpenseSheetID", ExpenseSheetID)};
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SPCheckAlreadyDateExist", param));
    }

    public virtual string getJobNObyID(int JobID)
    {
        SqlParameter[] param = { new SqlParameter("@JobID", JobID) };
        ;
        return SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SP_GetJobNobyJobID", param).ToString();
    }

    public virtual int getJobIDbyJobNo(string JobNo)
    {
        SqlParameter[] param = { new SqlParameter("@JobNo", JobNo)
                                };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SP_GetJobIDbyJobNo", param));
    }

    public virtual int CheckReferenceNo(string RefNo,int SalesTaxID)
    {
        SqlParameter[] param = { new SqlParameter("@RefNo", RefNo), 
                                 new SqlParameter("@SalesTaxID", SalesTaxID)};
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SPCheckReferenceNo", param));
    }

}
