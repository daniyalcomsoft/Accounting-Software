using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SW.SW_Common;

public class PayPayment_DAL
{
	public PayPayment_DAL()
	{
    }
    public virtual int CreateModifyPayPayment(PayPayment_BAL BAL, SqlTransaction Trans)
    {
        SqlParameter[] param3 = 
                                {
                                    new SqlParameter("@PayPayment_ID", BAL.PayPayment_ID)
                                    ,new SqlParameter("@VendorID",BAL.VendorID)
                                    ,new SqlParameter("@PaymentDate",BAL.PaymentDate)
                                    ,new SqlParameter("@ReferenceNo",BAL.ReferenceNo)
                                    ,new SqlParameter("@Memo",BAL.Memo)
                                    ,new SqlParameter("@LoginID",BAL.LoginID)
                                     ,new SqlParameter("@SubsidaryID",BAL.SubsidaryID)
                                     ,new SqlParameter("@FinYearID",BAL.FinYearID)
                                     ,new SqlParameter("@Total",BAL.Total)
                                };
        int i = Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, CommandType.StoredProcedure, "vt_SCGL_SpCreateModifyPayPayment", param3));
        return i;
    }

    public virtual bool CreateModifyPayPaymentDetail(PayPayment_BAL BAL, SqlTransaction Trans)
    {
        SqlParameter[] SQLParam = 
                                {
                                    new SqlParameter("@PayPayment_ID",BAL.PayPayment_ID)
                                    ,new SqlParameter("@pInvoiceID",BAL.pInvoiceID)
                                    ,new SqlParameter("@Amount",BAL.Amount)
                                };
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_CreateModifyPaymentPayDetail", SQLParam);
        return i > 0;
    }

    public virtual DataTable GetMaxPayPaymentId()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_sp_getmax_PayPaymentID").Tables[0];
    }
    // Delete from vt_SCGL_PaymentPay table 
    public virtual bool Delete_PayPayment(int PayPayment_ID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@PayPayment_ID", PayPayment_ID);
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_Delete_PayPayment", param);
        return i > 0;
    }
    // Delete from vt_SCGL_PaymentPayDetail table 
    public virtual bool Delete_PayPaymentDetail(int PayPayment_ID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@PayPayment_ID", PayPayment_ID);
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_Delete_PayPaymentDetail", param);
        return i > 0;
    }

    public virtual DataTable getPayPaymentByID(int id,int FinYearID)
    {
        SqlParameter[] param = {new SqlParameter("@PayPayment_ID", id),
                                new SqlParameter("@FinYearID", FinYearID) };

        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetPayPayment", param).Tables[0];
    }

    public virtual DataTable SearchPayPaymentByVendorName(string VendorName, int FinYearID)
    {
        SqlParameter[] param = { new SqlParameter("@VendorName", VendorName),
                               new SqlParameter("@FinYearID", FinYearID)};

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpSearchPayPayment_BYVendorName", param).Tables[0];
        return dt;
    }

    public virtual DataTable SearchPayPaymentByInvoiceID(int InvoiceID, int FinYearID)
    {
        SqlParameter[] param = { new SqlParameter("@InvoiceID", InvoiceID),
                               new SqlParameter("@FinYearID", FinYearID)};

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpSearchPayPayment_BYInvoiceID", param).Tables[0];
        return dt;
    }

    public virtual DataTable SearchPayPaymentByPaymentID(int PayPayment_ID, int FinYearID)
    {
        SqlParameter[] param = { new SqlParameter("@PayPayment_ID", PayPayment_ID),
                               new SqlParameter("@FinYearID", FinYearID)};

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpSearchPayPayment_BYPaymentID", param).Tables[0];
        return dt;
    }

    public virtual DataTable getPayPaymentByVendorID(int VendorID)
    {
        SqlParameter param = new SqlParameter("@VendorID", VendorID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetInvoiceByVendorID", param).Tables[0];
        return dt;
    }

    public virtual DataTable getPayPaymentDetailByPaymentID(int PayPayment_ID)
    {
        SqlParameter param = new SqlParameter("@PayPayment_ID", PayPayment_ID);

        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetPayPaymentDetail", param).Tables[0];
    }

    public virtual DataTable GetDetailPayPayment(int PayPayment_ID)
    {
        SqlParameter[] param = {new SqlParameter("@PayPayment_ID", PayPayment_ID),
                              };
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SPGetPayPaymentDetail_BYID", param).Tables[0];
    }

    public virtual SqlDataReader UpdateBalance_ByInvoiceID(int InvoiceID, int PayPayment_ID)
    {
        SqlParameter[] param = {new SqlParameter("@InvoiceID", InvoiceID)
                                ,new SqlParameter("@PayPayment_ID", PayPayment_ID)
                               };
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_SCGL_SPUpdatePayBalance_BYID", param);
    }

    public virtual int DeletePayPaymentTransaction(int PayPayment_ID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@PayPayment_ID", PayPayment_ID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, "vt_SCGL_SPDeleteTransaction_PayPayment", param));
    }

    public virtual bool Update_PayPaymentTransaction(int PayPayment_ID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@PayPayment_ID", PayPayment_ID);
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_Update_PayPaymentTransaction", param);
        return i > 0;
    }
   
}