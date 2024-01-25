using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SW.SW_Common;
using System.Data;

/// <summary>
/// Summary description for JobSheet_DAL
/// </summary>
public class JobSheet_DAL
{
	public JobSheet_DAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public virtual int CreateModifyJobSheet(JobSheet_BAL BALInvoice, SqlTransaction Trans)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@JobID", BALInvoice.JobID)
                                   ,new SqlParameter("@JobNo",BALInvoice.JobNo)
                                   ,new SqlParameter("@CustomerID",BALInvoice.CustomerID)
                                   ,new SqlParameter("@Description2",BALInvoice.Description2)
                                   ,new SqlParameter("@OtherDetails",BALInvoice.OtherDetails)
                                   ,new SqlParameter("@Total",BALInvoice.Total)
                                 
                               };
        int i = Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, CommandType.StoredProcedure, "vt_SCGL_SpCreateJobSheet", param));
        return i;
    }

    public virtual bool CreateModifyJobSheetDetail(JobSheet_BAL BALInvoice, SqlTransaction Trans)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@JobID",BALInvoice.JobID)
                                   ,new SqlParameter("@JobNo",BALInvoice.JobNo)
                                   ,new SqlParameter("@Date",BALInvoice.Date)
                                   ,new SqlParameter("@PaymentType",BALInvoice.PaymentType)
                                   ,new SqlParameter("@InfoType",BALInvoice.InfoType)
                                   ,new SqlParameter("@PONo",BALInvoice.PONo)
                                   ,new SqlParameter("@ExpenseAcc",BALInvoice.ExpenseAcc)
                                   ,new SqlParameter("@PaymentThrough",BALInvoice.PaymentThrough)
                                   ,new SqlParameter("@Description",BALInvoice.Description)
                                   ,new SqlParameter("@Amount",BALInvoice.Amount)

                               };
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_CreateModifyJobSheetDetail", param);
        return i > 0;
    }

    public virtual DataTable getCustomerNamebyJobNumber(int JobSheetID)
    {
        SqlParameter param = new SqlParameter("@JobSheetID", JobSheetID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_getCustomerNamebyJobNumber", param).Tables[0];
        return dt;
    }

    public virtual DataTable getjobSheet(int JobSheetID)
    {
        SqlParameter param = new SqlParameter("@JobSheetID", JobSheetID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetJobSheet", param).Tables[0];
        return dt;
    }

    public virtual DataTable getJobSheetDetail(int JobSheetID)
    {
        SqlParameter param = new SqlParameter("@JobSheetID", JobSheetID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_sp_GetJobSheetDetail", param).Tables[0];
        return dt;
    }

    public virtual DataTable getallJobSheets()
    {
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetallJobSheets").Tables[0];
        return dt;
    }

    public virtual bool Delete_JobSheetDetail(int JobSheetID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@JobSheetID", JobSheetID);
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_Delete_JobSheetDetail", param);
        return i > 0;
    }

    public virtual bool Delete_JobSheetTrans(int JobSheetID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@JobSheetID", JobSheetID);
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_Delete_JobSheetDetailTrans", param);
        return i > 0;
    }

    public virtual int DeleteJobSheet(int JobSheetID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@JobSheetID", JobSheetID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, "vt_SCGL_SPDeleteJobSheet", param));
    }
 
    public virtual DataTable GetMaxInvoiceId()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_sp_getmax_InvoiceID_I").Tables[0];
    }
    public virtual DataTable GetMaxProformaInvoiceId()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_sp_getmax_ProformaInvoiceID_I").Tables[0];
    }
    public virtual bool Delete_InvoiceDetail(int InvoiceID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_Delete_InvoiceDetail", param);
        return i > 0;
    }
    public virtual bool Delete_ProformaInvoiceDetail(int InvoiceID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_Delete_ProformaInvoiceDetail", param);
        return i > 0;
    }

    public virtual bool Delete_Transaction(int InvoiceID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_Delete_Transaction", param);
        return i > 0;
    }



    public virtual DataTable getInvoiceByID(int invoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", invoiceID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetInvoice", param).Tables[0];
        return dt;
    }
    public virtual DataTable getProformaInvoiceByID(int invoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", invoiceID);
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetproforma_Invoice", param).Tables[0];
        return dt;
    }

    public virtual DataTable getInvntoryByID(int FishID, int FishGradeID, int FishSizeID)
    {
        SqlParameter[] param = {new SqlParameter("@FishId", FishID),
                             new SqlParameter("@GradeId", FishGradeID),
                              new SqlParameter("@SizeId", FishSizeID) };

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SPGetInventoryId_ByFishId_ByGrade_IdBySizeId", param).Tables[0];
        return dt;
    }
    //public virtual DataTable getCogs_Rate(JobSheet_BAL BALInvoice,int InventoryID, int CostCenterID, DateTime InvoiceDate)
    //{
    //    SqlParameter[] param = {new SqlParameter("@InventoryID", BALInvoice.InventoryID),
    //                         new SqlParameter("@CostCenterID", BALInvoice.CostCenterID),
    //                          new SqlParameter("@InvoiceDate", BALInvoice.InvoiceDate) };

    //    DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "sp_Get_Cogs", param).Tables[0];
    //    return dt;
    //}


    public virtual DataTable getInvoiceByCustomerID(int CustomerID)
    {
        SqlParameter param = new SqlParameter("@CustomerID", CustomerID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetInvoiceByCustomerID", param).Tables[0];
        return dt;
    }
    public virtual int DeleteInvoice(int InvoiceID)
    {
        SqlParameter[] param = { new SqlParameter("@InvoiceID", InvoiceID) };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SPDeleteInvoice", param));
    }
    // overload method for deleting invoice
    public virtual int DeleteInvoice(int InvoiceID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, "vt_SCGL_SPDeleteInvoice", param));
    }


    public virtual SqlDataReader Get_Rows_JobSheetDetail_byID(int JobSheetID)
    {
        SqlParameter param = new SqlParameter("@JobSheetID", JobSheetID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetJobSheetDetailRowNumber_byID", param);
    }

    public virtual SqlDataReader Get_Rows_ProformaInvoiceDetail_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetProformaInvoiceDetailRowNumber_byID", param);
    }

    public virtual SqlDataReader Get_Rows_InvoiceDetail2_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetInvoiceDetailRowNumber2_byID", param);
    }
    public virtual SqlDataReader Get_Rows_ProformaInvoiceDetail2_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetProformaInvoiceDetailRowNumber2_byID", param);
    }
    public virtual SqlDataReader Get_Rows_ProformaInvoiceDetail3_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetProformaInvoiceDetailRowNumber3_byID", param);
    }
    public virtual SqlDataReader Get_Rows_ProformaInvoiceDetail4_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetProformaInvoiceDetailRowNumber4_byID", param);
    }
    public virtual SqlDataReader Get_Rows_ProformaInvoiceDetail5_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetProformaInvoiceDetailRowNumber5_byID", param);
    }
    public virtual SqlDataReader Get_Rows_ProformaInvoiceDetail6_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetProformaInvoiceDetailRowNumber6_byID", param);
    }
    public virtual SqlDataReader Get_Rows_ProformaInvoiceDetail7_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetProformaInvoiceDetailRowNumber7_byID", param);
    }
    public virtual SqlDataReader Get_Rows_ProformaInvoiceDetail8_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetProformaInvoiceDetailRowNumber8_byID", param);
    }
    public virtual SqlDataReader Get_Rows_ProformaInvoiceDetail9_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetProformaInvoiceDetailRowNumber9_byID", param);
    }
    public virtual SqlDataReader Get_Rows_ProformaInvoiceDetail10_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetProformaInvoiceDetailRowNumber10_byID", param);
    }
    public virtual SqlDataReader Get_Rows_ProformaInvoiceDetail11_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetProformaInvoiceDetailRowNumber11_byID", param);
    }
    public virtual SqlDataReader Get_Rows_ProformaInvoiceDetail12_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetProformaInvoiceDetailRowNumber12_byID", param);
    }
    public virtual SqlDataReader Get_Rows_ProformaInvoiceDetail13_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetProformaInvoiceDetailRowNumber13_byID", param);
    }
    public virtual SqlDataReader Get_Rows_ProformaInvoiceDetail14_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetProformaInvoiceDetailRowNumber14_byID", param);
    }
    public virtual SqlDataReader Get_Rows_ProformaInvoiceDetail15_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetProformaInvoiceDetailRowNumber15_byID", param);
    }
    public virtual SqlDataReader Get_Rows_InvoiceDetail3_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetInvoiceDetailRowNumber3_byID", param);
    }

    public virtual SqlDataReader Get_Rows_InvoiceDetail4_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetInvoiceDetailRowNumber4_byID", param);
    }
    public virtual SqlDataReader Get_Rows_InvoiceDetail5_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetInvoiceDetailRowNumber5_byID", param);
    }
    public virtual SqlDataReader Get_Rows_InvoiceDetail6_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetInvoiceDetailRowNumber6_byID", param);
    }
    public virtual SqlDataReader Get_Rows_InvoiceDetail7_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetInvoiceDetailRowNumber7_byID", param);
    }
    public virtual SqlDataReader Get_Rows_InvoiceDetail8_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetInvoiceDetailRowNumber8_byID", param);
    }
    public virtual SqlDataReader Get_Rows_InvoiceDetail9_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetInvoiceDetailRowNumber9_byID", param);
    }
    public virtual SqlDataReader Get_Rows_InvoiceDetail10_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetInvoiceDetailRowNumber10_byID", param);
    }
    public virtual SqlDataReader Get_Rows_InvoiceDetail11_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetInvoiceDetailRowNumber11_byID", param);
    }
    public virtual SqlDataReader Get_Rows_InvoiceDetail12_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetInvoiceDetailRowNumber12_byID", param);
    }
    public virtual SqlDataReader Get_Rows_InvoiceDetail13_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetInvoiceDetailRowNumber13_byID", param);
    }
    public virtual SqlDataReader Get_Rows_InvoiceDetail14_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetInvoiceDetailRowNumber14_byID", param);
    }
    public virtual SqlDataReader Get_Rows_InvoiceDetail15_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetInvoiceDetailRowNumber15_byID", param);
    }
    public virtual DataTable GetMaxPurchaseDate(int FinYearID)
    {
        SqlParameter[] param = { new SqlParameter("@FinYearID", FinYearID) };
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SPGetMaxPurchaseDate", param).Tables[0];
        return dt;
    }

    public virtual DataTable GetMaxExcessShortDate(int FinYearID)
    {
        SqlParameter[] param = { new SqlParameter("@FinYearID", FinYearID) };
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SPGetMaxExcessShortDate", param).Tables[0];
        return dt;
    }

    public virtual bool Delete_CurrentSalesforValuation(SqlTransaction Trans)
    {

        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_Delete_CurrentSalesforValuation");
        return i > 0;
    }

    public virtual DataTable GetInvoiceDetailTable()
    {

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_getInvoiceDetailTable").Tables[0];
        return dt;
    }

  
    public virtual DataTable GetPSCTable()
    {

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_getPhysicalStockCountTable").Tables[0];
        return dt;
    }

    public virtual int DeleteExcessShortandGLTrans(JobSheet_BAL BALInvoice, SqlTransaction Trans)
    {

        return Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, "vt_SCGL_SPDeleteExcessShortandGLTrans"));
    }

 

    public virtual DataTable GetLastExcessShortID(SqlTransaction Trans)
    {
        DataTable dt = SqlHelper.ExecuteDataset(Trans, "vt_sp_GetLastExcessShortID").Tables[0];
        return dt;
    }



    public virtual DataTable getProformaInvoiceDetail(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_sp_GetProformaInvoiceDetail", param).Tables[0];
        return dt;
    }

    public virtual DataTable GetJobNumber(string JobNo)
    {
        SqlParameter[] param = {
                                   new SqlParameter("@Match", JobNo)
                                  
                               };
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SPGetJobNumberLikeAll", param).Tables[0];
    }

    public virtual DataTable GetJobNo(String JobNo)
    {
        SqlParameter[] param = {
                                   new SqlParameter("@Match", JobNo)

                               };
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SPGetAllJobNo",param).Tables[0];
    }

}