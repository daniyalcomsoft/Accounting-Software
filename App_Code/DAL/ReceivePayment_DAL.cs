using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SW.SW_Common;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for ReceivePayment_DAL
/// </summary>
public class ReceivePayment_DAL
{
	public ReceivePayment_DAL()
	{
	}

    public virtual int CreateModifyReceivePayment(ReceivePayment_BAL BAL, SqlTransaction Trans)
    {
        SqlParameter[] param2 = {
                                   new SqlParameter("@ReceivePayment_ID", BAL.ReceivePayment_ID)
                                   ,new SqlParameter("@CustomerID",BAL.CustomerID)
                                   ,new SqlParameter("@PaymentDate",BAL.PaymentDate)
                                   ,new SqlParameter("@ReferenceNo",BAL.ReferenceNo)
                                   ,new SqlParameter("@Memo",BAL.Memo)
                                    ,new SqlParameter("@LoginID",BAL.LoginID)
                                    ,new SqlParameter("@SubsidaryID",BAL.SubsidaryID)
                                     ,new SqlParameter("@FinYearID",BAL.FinYearID)
                                     ,new SqlParameter("@Total",BAL.Total)
                                   ,new SqlParameter("@Currency",BAL.Currency)
                                   ,new SqlParameter("@ConversionRate",BAL.ConversionRate)
                                   ,new SqlParameter("@PKRTotal",BAL.PKRTotal)
                                   
                               };
        int i = Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, CommandType.StoredProcedure, "vt_SCGL_SpCreateModifyReceivePayment", param2));
        return i;
    }

    public virtual bool CreateModifyReceivePaymentDetail(ReceivePayment_BAL BAL, SqlTransaction Trans)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@ReceivePayment_ID",BAL.ReceivePayment_ID)
                                   ,new SqlParameter("@InvoiceID",BAL.InvoiceID)
                                   ,new SqlParameter("@Amount",BAL.Amount)
                                   ,new SqlParameter("@Currency",BAL.Currency)
                                   ,new SqlParameter("@ConversionRate",BAL.ConversionRate)
                                   ,new SqlParameter("@PKRAmount",BAL.PKRAmount)
                                   
                               };
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_CreateModifyPaymentReceiveDetail", param);
        return i > 0;
    }

    public virtual DataTable GetMaxPaymentId()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_sp_getmax_PaymentID").Tables[0];
    }
    // Delete from vt_SCGL_PaymentReceive table 
    public virtual bool Delete_ReceivePayment(int ReceivePayment_ID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@ReceivePayment_ID", ReceivePayment_ID);
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_Delete_ReceivePayment", param);
        return i > 0;
    }
    // Delete from vt_SCGL_PaymentReceiveDetail table 
    public virtual bool Delete_ReceivePaymentDetail(int ReceivePayment_ID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@ReceivePayment_ID", ReceivePayment_ID);
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_Delete_ReceivePaymentDetail", param);
        return i > 0;
    }

    public virtual DataTable getReceivePaymentByID(int id,int FinYearID)
    {
        SqlParameter[] param = {new SqlParameter("@ReceivePayment_ID", id),
                               new SqlParameter("@FinYearID", FinYearID)};

        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetReceivePayment", param).Tables[0];
    }

    public virtual DataTable SearchReceivePaymentByCustomerName(string CustomerName,int FinYearID)
    {
        SqlParameter[] param = { new SqlParameter("@CustomerName", CustomerName),
                               new SqlParameter("@FinYearID", FinYearID)};

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpSearchReceivePayment_BYCustomerName", param).Tables[0];
        return dt;
    }

    public virtual DataTable SearchReceivePaymentByInvoiceID(int InvoiceID, int FinYearID)
    {
        SqlParameter[] param = { new SqlParameter("@InvoiceID", InvoiceID),
                               new SqlParameter("@FinYearID", FinYearID)};

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpSearchReceivePayment_BYInvoiceID", param).Tables[0];
        return dt;
    }

    public virtual DataTable SearchReceivePaymentByPaymentID(int ReceivePayment_ID, int FinYearID)
    {
        SqlParameter[] param = { new SqlParameter("@ReceivePayment_ID", ReceivePayment_ID),
                               new SqlParameter("@FinYearID", FinYearID)};

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpSearchReceivePayment_BYPaymentID", param).Tables[0];
        return dt;
    }

    public virtual DataTable getReceivePaymentByCustomerID(int CustomerID)
    {
        SqlParameter param = new SqlParameter("@CustomerID", CustomerID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetInvoiceByCustomerID", param).Tables[0];
        return dt;
    }

    public virtual DataTable getReceivePaymentDetailByPaymentID(int ReceivePayment_ID)
    {
        SqlParameter param = new SqlParameter("@ReceivePayment_ID", ReceivePayment_ID);

        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetReceivePaymentDetail", param).Tables[0];
    }

    public virtual DataTable getReceivePaymentDetailByPaymentID_update(int ReceivePayment_ID)
    {
        SqlParameter param = new SqlParameter("@ReceivePayment_ID", ReceivePayment_ID);

        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetReceivePaymentDetail_update", param).Tables[0];
    }
    public virtual DataTable GetDetailReceivePayment(int ReceivePayment_ID)
    {
        SqlParameter[] param = {new SqlParameter("@ReceivePayment_ID", ReceivePayment_ID),
                              };
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SPGetPaymentDetail_BYID",param).Tables[0];
    }
    //public virtual DataTable UpdateBalance_ByInvoiceID(int InvoiceID)
    //{
    //    SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);

    //    return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SPUpdateBalance_BYID", param).Tables[0];
    //}


    public virtual SqlDataReader UpdateBalance_ByInvoiceID(int InvoiceID, int ReceivePayment_ID)
    {
        SqlParameter[] param = {new SqlParameter("@InvoiceID", InvoiceID)
                               ,new SqlParameter("@ReceivePayment_ID", ReceivePayment_ID)
                               };
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_SCGL_SPUpdateBalance_BYID", param);
    }

    public virtual int DeleteReceivePaymentTransaction(int ReceivePayment_ID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@ReceivePayment_ID", ReceivePayment_ID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, "vt_SCGL_SPDeleteTransaction_ReceivePayment", param));
    }

    public virtual bool Update_ReceivePaymentTransaction(int ReceivePayment_ID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@ReceivePayment_ID", ReceivePayment_ID);
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_Update_ReceivePaymentTransaction", param);
        return i > 0;
    }
}
