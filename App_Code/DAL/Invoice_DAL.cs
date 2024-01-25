using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using SW.SW_Common;

public class Invoice_DAL
{
    
	public Invoice_DAL()
	{
	}
 
    public virtual bool CreateModifyInvoice(Invoice_BAL BALInvoice, SqlTransaction Trans)
    {
        SqlParameter[] param1 = {
                                   new SqlParameter("@InvoiceID", BALInvoice.InvoiceID)
                                   ,new SqlParameter("@CustomerID",BALInvoice.CustomerID)
                                   ,new SqlParameter("@Email",BALInvoice.Email)
                                   ,new SqlParameter("@BillingAddress",BALInvoice.BillingAddress)
                                   ,new SqlParameter("@TermID",BALInvoice.TermID)
                                   ,new SqlParameter("@InvoiceDate",BALInvoice.InvoiceDate)
                                   ,new SqlParameter("@DueDate",BALInvoice.DueDate)                                  
                                   ,new SqlParameter("@LoginID",BALInvoice.LoginID)
                                   ,new SqlParameter("@Total",BALInvoice.Total)
                                   ,new SqlParameter("@InvoiceNo",BALInvoice.Invoice_No)                                 
                                   ,new SqlParameter("@FinYearID",BALInvoice.FinYearID)
                                   ,new SqlParameter("@From",BALInvoice.From)
                                   ,new SqlParameter("@To",BALInvoice.To)
                                   ,new SqlParameter("@ContainerNo",BALInvoice.ContainerNo)
                                   ,new SqlParameter("@Origin_Country",BALInvoice.Origin_Country)
                                   ,new SqlParameter("@Destination_Country",BALInvoice.Destination_Country)
                                   ,new SqlParameter("@Vessel",BALInvoice.Vessel)
                                   ,new SqlParameter("@FormENo",BALInvoice.FormENo)
                                   ,new SqlParameter("@Freight",BALInvoice.Freight)
                                   ,new SqlParameter("@NetWeight",BALInvoice.NetWeight)
                                   ,new SqlParameter("@GrossWeight",BALInvoice.GrossWeight)
                                   ,new SqlParameter("@ProformaNo",BALInvoice.ProformaNo)
                                   ,new SqlParameter("@Insurance",BALInvoice.Insurance)                                  
                                   ,new SqlParameter("@Exporter",BALInvoice.Exporter)
                                   ,new SqlParameter("@Consignee",BALInvoice.Consignee)
                                   ,new SqlParameter("@Buyer",BALInvoice.Buyer)
                                   ,new SqlParameter("@ExportersRef",BALInvoice.ExportersRef)
                                   ,new SqlParameter("@Note",BALInvoice.Note)
                               };
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_SCGL_SpCreateModifyInvoice", param1);
        return i > 0;
    }


    public virtual bool CreateModifyInvoiceDetail(Invoice_BAL BALInvoice, SqlTransaction Trans)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@InvoiceID",BALInvoice.InvoiceNo)
                                   ,new SqlParameter("@ProductServiceID",BALInvoice.ProductServiceID)
                                   ,new SqlParameter("@Description",BALInvoice.Description)
                                   ,new SqlParameter("@Quantity",BALInvoice.Quantity)
                                   ,new SqlParameter("@Rate",BALInvoice.Rate)
                                   ,new SqlParameter("@Amount",BALInvoice.Amount)
                                   ,new SqlParameter("@GridName",BALInvoice.GridName)
                                   ,new SqlParameter("@InventoryID",BALInvoice.InventoryID)
                                   ,new SqlParameter("@FishID",BALInvoice.FishID)
                                   ,new SqlParameter("@FishGradeID",BALInvoice.FishGradeID)
                                   ,new SqlParameter("@FishSizeID",BALInvoice.FishSizeID)
                               };
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_CreateModifyInvoiceDetail", param);
        return i > 0;
    }

    public virtual DataTable GetMaxInvoiceId()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_sp_getmax_InvoiceID_I").Tables[0];
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
    public virtual int DeleteTransaction_Invoice(int InvoiceID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, "vt_SCGL_SPDeleteTransaction_Invoice", param));
    }


    public virtual DataTable getInvoiceByCustomer(string CustomerName, int FinYearID)
    {
        SqlParameter[] param = {new SqlParameter("@CustomerName", CustomerName),
                                 new SqlParameter("@FinYearID", FinYearID)
                         
                              };

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpSearchInvoice_BYCustomer", param).Tables[0];
        return dt;
    }

    public virtual DataTable GetGraph(string StartDate, string EndDate)
    {
        SqlParameter[] param = {
                                 new SqlParameter("@StartDate",StartDate),
                                 new SqlParameter("@EndDate",EndDate)};
        //    return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SpALLDATA_Graph").Tables[0];
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpALLDATA_Graph", param).Tables[0];
        return dt;
    }
    public virtual DataTable GetGraph_ByDate(string StartDate, string EndDate)
    {
        SqlParameter[] param = {
                                 new SqlParameter("@StartDate",StartDate),
                                 new SqlParameter("@EndDate",EndDate)};
        //    return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SpALLDATA_Graph").Tables[0];
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "GetTotalByDate_Graph", param).Tables[0];
        return dt;
    }
    public virtual DataTable GetGraph_ByDate_2(string StartDate, string EndDate)
    {
        SqlParameter[] param = {
                                 new SqlParameter("@StartDate",StartDate),
                                 new SqlParameter("@EndDate",EndDate)};
        //    return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SpALLDATA_Graph").Tables[0];
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "GetTotalByDate_2", param).Tables[0];
        return dt;
    }

    //Graph
    public virtual DataTable GetGwadarGraph_ByDate()
    {

        //    return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SpALLDATA_Graph").Tables[0];
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "GetTotalByDate_Graph").Tables[0];
        return dt;
    }
    public virtual DataTable GetKarachiGraph_ByDate()
    {

        //    return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SpALLDATA_Graph").Tables[0];
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "GetTotalByDate_KarachiGraph").Tables[0];
        return dt;
    }
    public virtual DataTable GetPasniGraph_ByDate()
    {

        //    return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SpALLDATA_Graph").Tables[0];
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "GetTotalByDate_PasniGraph").Tables[0];
        return dt;
    }

    public virtual DataTable GetTotalSales_Graph(string StartDate, string EndDate)
    {
        SqlParameter[] param = {
                                 new SqlParameter("@StartDate",StartDate),
                                 new SqlParameter("@EndDate",EndDate)};
        //    return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SpALLDATA_Graph").Tables[0];
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "GetTotalSales_ByDate", param).Tables[0];
        return dt;
    }
    public virtual DataTable GetTotalMargin_Graph(string StartDate, string EndDate)
    {
        SqlParameter[] param = {
                                 new SqlParameter("@StartDate",StartDate),
                                 new SqlParameter("@EndDate",EndDate)};
        //    return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SpALLDATA_Graph").Tables[0];
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "GetTotalMargin_ByDate", param).Tables[0];
        return dt;
    }
    public virtual DataTable GetTotalEquity_Graph(string StartDate, string EndDate)
    {
        SqlParameter[] param = {
                                 new SqlParameter("@StartDate",StartDate),
                                 new SqlParameter("@EndDate",EndDate)};
        //    return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SpALLDATA_Graph").Tables[0];
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "GetTotalEquity_ByDate", param).Tables[0];
        return dt;
    }
    public virtual DataTable GetTotalIncome_Graph(string StartDate, string EndDate)
    {
        SqlParameter[] param = {
                                 new SqlParameter("@StartDate",StartDate),
                                 new SqlParameter("@EndDate",EndDate)};
        //    return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SpALLDATA_Graph").Tables[0];
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "GetTotalNetIncome_ByDate", param).Tables[0];
        return dt;
    }

    public virtual DataTable GetTotalBalance_Graph()
    {
      
        //    return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SpALLDATA_Graph").Tables[0];
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "GetTotalBalance_ByDate").Tables[0];
        return dt;
    }

    public virtual DataTable GetTotalSales_Temp(string StartDate, string EndDate)
    {
        SqlParameter[] param = {
                                 new SqlParameter("@StartDate",StartDate),
                                 new SqlParameter("@EndDate",EndDate)};
        //    return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SpALLDATA_Graph").Tables[0];
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "GetTotalSales_Temp", param).Tables[0];
        return dt;
    }
    public virtual DataTable GetTotalExpense_Graph(string StartDate, string EndDate)
    {
        SqlParameter[] param = {
                                 new SqlParameter("@StartDate",StartDate),
                                 new SqlParameter("@EndDate",EndDate)};
        //    return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SpALLDATA_Graph").Tables[0];
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "GetTotalExpense_ByDate", param).Tables[0];
        return dt;
    }
    public virtual DataTable GetCurrentFisYear(int FinYearID)
    {
        SqlParameter[] param = {
                                 new SqlParameter("@FinYearID", FinYearID)
                         
                              };

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_Getcurrent_FinYear", param).Tables[0];
        return dt;
    }

  


    public virtual DataTable getProformaInvoiceByCustomer(string CustomerName,int FinYearID)
    {
        SqlParameter[] param = {new SqlParameter("@CustomerName", CustomerName),
                                   new SqlParameter("@FinYearID", FinYearID)
                                 };
        
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpSearchProformaInvoice_BYCustomer", param).Tables[0];
        return dt;
    }

    public virtual DataTable getInvoiceByID(int InvoiceID,int FinYearID)
    {
        SqlParameter[] param = {new SqlParameter("@InvoiceID", InvoiceID),
                               new SqlParameter("@FinYearID", FinYearID)};

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpSearchInvoice_BYID", param).Tables[0];
        return dt;
    }
    public virtual DataTable getProformaInvoiceByID(int InvoiceID,int FinYearID)
    {
        SqlParameter[] param = {new SqlParameter("@InvoiceID", InvoiceID),
                                   new SqlParameter("@FinYearID", FinYearID)
                              };

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpSearchProformaInvoice_BYID", param).Tables[0];
        return dt;
    }

    public virtual DataTable getallInvoice(int InvoiceID,int FinYearID)
    {
        SqlParameter[] param = {new SqlParameter("@InvoiceID", InvoiceID),
                                new SqlParameter("@FinYearID", FinYearID) };

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetallInvoice", param).Tables[0];
        return dt;
    }
    public virtual DataTable getallProformaInvoice(int InvoiceID,int FinYearID)
    {
        SqlParameter[] param = {new SqlParameter("@InvoiceID", InvoiceID),
                                new SqlParameter("@FinYearID", FinYearID) };

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetallProformaInvoice", param).Tables[0];
        return dt;
    }



    public virtual DataTable searchInvoice2()
    {
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpSearchInvoice").Tables[0];
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
    public virtual DataTable getInvoiceByCustomerID(int CustomerID,int FinYearID)
    {
        SqlParameter[] param = {new SqlParameter("@CustomerID", CustomerID),
                                new SqlParameter("@FinYearID", FinYearID)};

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
        SqlParameter param =  new SqlParameter("@InvoiceID", InvoiceID) ;
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, "vt_SCGL_SPDeleteInvoice", param));
    }

    public virtual int DeleteProformaInvoice(int InvoiceID, SqlTransaction Trans)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, "vt_SCGL_SPDeleteProformaInvoice", param));
    }


    public virtual SqlDataReader Get_Rows_InvoiceDetail_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetInvoiceDetailRowNumber_byID", param);
    }
    public virtual SqlDataReader Get_Rows_InvoiceDetail2_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetInvoiceDetailRowNumber2_byID", param);
    }





    public virtual DataTable GetAllBanks(int FinID, string YearFrom, string YearTo)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@YearID",FinID),
                                 new SqlParameter("@YearFrom",YearFrom),
                                 new SqlParameter("@YearTo",YearTo)};
        //    return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "vt_SCGL_SpALLDATA_Graph").Tables[0];
        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SPGetBankBalnceForGraph", param).Tables[0];
        return dt;
    }
    public virtual DataTable GetLastDaysReceipts()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "GetLastDays_Pay").Tables[0];
    }

    public virtual DataTable GetPasniProduction()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "GetTotalPasni_ByProduction").Tables[0];
    }

    public virtual DataTable GetGwadarProduction()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "GetTotalGwadar_ByProduction").Tables[0];
    }
    public virtual DataTable GetKarachiProduction()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "GetTotalKarachi_ByProduction").Tables[0];
    }

    public virtual DataTable GetPasniPurchasesInKg()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "GetTotalPasniPurchasesInKg").Tables[0];
    }

    public virtual DataTable GetGwadarPurchasesInKg()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "GetTotalGwadarPurchasesInKg").Tables[0];
    }
    public virtual DataTable GetKarachiPurchasesInKg()
    {
        return SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, CommandType.StoredProcedure, "GetTotalKarachiPurchasesInKg").Tables[0];
    }
 
}
