using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SW.SW_Common;
using System.Data;

/// <summary>
/// Summary description for Invoice_DAL_Temp
/// </summary>
public class Invoice_DAL_Temp
{
	public Invoice_DAL_Temp()
	{
		
	}
    public virtual int CreateModifyInvoice(Invoice_BAL_Temp BALInvoice, SqlTransaction Trans)
    {
        SqlParameter[] sql_param = {
                                   new SqlParameter("@InvoiceID", BALInvoice.InvoiceID)
                                   ,new SqlParameter("@TermID",BALInvoice.TermID)
                                   ,new SqlParameter("@InvoiceDate",BALInvoice.InvoiceDate)
                                   ,new SqlParameter("@ReferenceNo",BALInvoice.ReferenceNo)
                                   //,new SqlParameter("@Discount",BALInvoice.Discount)
                                   ,new SqlParameter("@LoginID",BALInvoice.LoginID)                         
                                   ,new SqlParameter("@FinYearID",BALInvoice.FinYearID)
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
                                   ,new SqlParameter("@JobID",BALInvoice.JobID)
                                   ,new SqlParameter("@BillNumber",BALInvoice.BillNumber)
                                   ,new SqlParameter("@CustInvoiceNo",BALInvoice.CustInvoiceNo)
                                   ,new SqlParameter("@IsAbbott",BALInvoice.IsAbbott)
                                   ,new SqlParameter("@NoAdvance",BALInvoice.NoAdvance)
                                   ,new SqlParameter("@CustomPONo",BALInvoice.CustomPONo)
                                   ,new SqlParameter("@CustomPODate",BALInvoice.CustomPODate)
                                   ,new SqlParameter("@CustomByParty",BALInvoice.CustomByParty)
                                   ,new SqlParameter("@CustomByUs",BALInvoice.CustomByUs)
                                   ,new SqlParameter("@SalesTaxPONo",BALInvoice.SalesTaxPONo)
                                   ,new SqlParameter("@SalesTaxFine",BALInvoice.SalesTaxFine)
                                   ,new SqlParameter("@SalesTaxByParty",BALInvoice.SalesTaxByParty)
                                   ,new SqlParameter("@SalesTaxByUs",BALInvoice.SalesTaxByUs)
                                   ,new SqlParameter("@IncomeTaxPONo",BALInvoice.IncomeTaxPONo)
                                   ,new SqlParameter("@IncomeTaxAddition",BALInvoice.IncomeTaxAddition)
                                   ,new SqlParameter("@IncomeTaxByParty",BALInvoice.IncomeTaxByParty)
                                   ,new SqlParameter("@IncomeTaxByUs",BALInvoice.IncomeTaxByUs)
                                   ,new SqlParameter("@CEDPercent",BALInvoice.CEDPercent)
                                   ,new SqlParameter("@CEDByParty",BALInvoice.CEDByParty)
                                   ,new SqlParameter("@CEDByUs",BALInvoice.CEDByUs)
                                   ,new SqlParameter("@EOCPercent",BALInvoice.EOCPercent)
                                   ,new SqlParameter("@EOCByParty",BALInvoice.EOCByParty)
                                   ,new SqlParameter("@EOCByUs",BALInvoice.EOCByUs)
                                   ,new SqlParameter("@FEDPercent",BALInvoice.FEDPercent)
                                   ,new SqlParameter("@FEDByParty",BALInvoice.FEDByParty)
                                   ,new SqlParameter("@FEDByUs",BALInvoice.FEDByUs)
                                   ,new SqlParameter("@OthersPercent",BALInvoice.OthersPercent)
                                   ,new SqlParameter("@OthersByParty",BALInvoice.OthersByParty)
                                   ,new SqlParameter("@OthersByUs",BALInvoice.OthersByUs)
                                   ,new SqlParameter("@ExcessShortDutyPONo",BALInvoice.ExcessShortDutyPONo)
                                   ,new SqlParameter("@ExcessShortDutyByParty",BALInvoice.ExcessShortDutyByParty)
                                   ,new SqlParameter("@ExcessShortDutyByUs",BALInvoice.ExcessShortDutyByUs)
                                   ,new SqlParameter("@Status",BALInvoice.Status)
                                   ,new SqlParameter("@ChequeNo",BALInvoice.ChequeNo)
                                   ,new SqlParameter("@ReceivedDate",BALInvoice.ReceivedDate)

                                   ,new SqlParameter("@TotalByParty",BALInvoice.TotalByParty)
                                   ,new SqlParameter("@TotalByUs",BALInvoice.TotalByUs)
                                   ,new SqlParameter("@DeliveryChallan",BALInvoice.DeliveryChallan)
                                   ,new SqlParameter("@LCContract",BALInvoice.LCContract)
                                   ,new SqlParameter("@Certificates",BALInvoice.Certificates)
                                   ,new SqlParameter("@PackingList",BALInvoice.PackingList)
                                   ,new SqlParameter("@Invoice",BALInvoice.Invoice)
                                   ,new SqlParameter("@WebocGD",BALInvoice.WebocGD)
                                   ,new SqlParameter("@PaccsCoupon",BALInvoice.PaccsCoupon)
                                   ,new SqlParameter("@CashPaymentReceipt",BALInvoice.CashPaymentReceipt)
                                   ,new SqlParameter("@ExciseDutyChallan",BALInvoice.ExciseDutyChallan)
                                   ,new SqlParameter("@ExciseTaxChallan",BALInvoice.ExciseTaxChallan)
                                   ,new SqlParameter("@DeliveryOrderReceipt",BALInvoice.DeliveryOrderReceipt)
                                   ,new SqlParameter("@PICTLInvoice",BALInvoice.PICTLInvoice)
                                   ,new SqlParameter("@TrasnportationBill",BALInvoice.TrasnportationBill)
                                   ,new SqlParameter("@GSTInvoice",BALInvoice.GSTInvoice)
 
                                   ,new SqlParameter("@ITChallan",BALInvoice.ITChallan)
                                   ,new SqlParameter("@BEImporter",BALInvoice.BEImporter)
                                   ,new SqlParameter("@KPTWharfage",BALInvoice.KPTWharfage)
                                   ,new SqlParameter("@KPTStorage",BALInvoice.KPTStorage)
                                   ,new SqlParameter("@MTOLift",BALInvoice.MTOLift)
                                   ,new SqlParameter("@YardCharges",BALInvoice.YardCharges)
                                   ,new SqlParameter("@EForm",BALInvoice.EForm)
                                   ,new SqlParameter("@BLCopy",BALInvoice.BLCopy)
                                   ,new SqlParameter("@AirwaysBL",BALInvoice.AirwaysBL)
                                   ,new SqlParameter("@InsuranceDoc",BALInvoice.InsuranceDoc)
                                   ,new SqlParameter("@BondPapers",BALInvoice.BondPapers)
                                   ,new SqlParameter("@BEExchange",BALInvoice.BEExchange)
                                   ,new SqlParameter("@Original",BALInvoice.Original)
                                   ,new SqlParameter("@Duplicate",BALInvoice.Duplicate)
                                   ,new SqlParameter("@EndorsmentReceipt",BALInvoice.EndorsmentReceipt)
                                   ,new SqlParameter("@OtherDocs1",BALInvoice.OtherDocs1)
                                   ,new SqlParameter("@OtherDocs2",BALInvoice.OtherDocs2)
                                   ,new SqlParameter("@OtherDocs3",BALInvoice.OtherDocs3)
                                   ,new SqlParameter("@OtherDocs4",BALInvoice.OtherDocs4)
                                   ,new SqlParameter("@OtherDocs5",BALInvoice.OtherDocs5)
                                   ,new SqlParameter("@OtherDocs6",BALInvoice.OtherDocs6)
                                   ,new SqlParameter("@OtherDocs7",BALInvoice.OtherDocs7)
                                   ,new SqlParameter("@OtherDocs8",BALInvoice.OtherDocs8)
                                   ,new SqlParameter("@OtherDocs9",BALInvoice.OtherDocs9)
                                   ,new SqlParameter("@OtherDocs10",BALInvoice.OtherDocs10)
                                   ,new SqlParameter("@OtherDocs11",BALInvoice.OtherDocs11)
                                   
                                   ,new SqlParameter("@OtherDocs1_name",BALInvoice.OtherDocs1_name)
                                   ,new SqlParameter("@OtherDocs2_name",BALInvoice.OtherDocs2_name)
                                   ,new SqlParameter("@OtherDocs3_name",BALInvoice.OtherDocs3_name)
                                   ,new SqlParameter("@OtherDocs4_name",BALInvoice.OtherDocs4_name)
                                   ,new SqlParameter("@OtherDocs5_name",BALInvoice.OtherDocs5_name)
                                   ,new SqlParameter("@OtherDocs6_name",BALInvoice.OtherDocs6_name)
                                   ,new SqlParameter("@OtherDocs7_name",BALInvoice.OtherDocs7_name)
                                   ,new SqlParameter("@OtherDocs8_name",BALInvoice.OtherDocs8_name)
                                   ,new SqlParameter("@OtherDocs9_name",BALInvoice.OtherDocs9_name)
                                   ,new SqlParameter("@OtherDocs10_name",BALInvoice.OtherDocs10_name)
                                   ,new SqlParameter("@OtherDocs11_name",BALInvoice.OtherDocs11_name)
                                  };

        int i = Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, CommandType.StoredProcedure, "vt_SCGL_SpCreateModifyInvoice", sql_param));
        return i;
    }

    //public virtual int CreateModifyProformaInvoice(Invoice_BAL_Temp BALInvoice, SqlTransaction Trans)
    //{
    //    SqlParameter[] param2 = {
    //                               new SqlParameter("@InvoiceID", BALInvoice.InvoiceID)
    //                               ,new SqlParameter("@CustomerID",BALInvoice.CustomerID)
    //                               ,new SqlParameter("@Email",BALInvoice.Email)
    //                               ,new SqlParameter("@BillingAddress",BALInvoice.BillingAddress)
    //                               ,new SqlParameter("@TermID",BALInvoice.TermID)
    //                               ,new SqlParameter("@InvoiceDate",BALInvoice.InvoiceDate)
    //                               ,new SqlParameter("@DueDate",BALInvoice.DueDate)
    //                              //,new SqlParameter("@Discount",BALInvoice.Discount)
    //                               ,new SqlParameter("@LoginID",BALInvoice.LoginID)
    //                               ,new SqlParameter("@Total",BALInvoice.Total)
    //                               ,new SqlParameter("@Currency",BALInvoice.Currency)
    //                               ,new SqlParameter("@ConversionRate",BALInvoice.ConversionRate)
    //                               ,new SqlParameter("@PKRTotal",BALInvoice.PKRTotal)
    //                               ,new SqlParameter("@InvoiceNo",BALInvoice.Invoice_No)                                 
    //                               ,new SqlParameter("@FinYearID",BALInvoice.FinYearID)
    //                               //,new SqlParameter("@From",BALInvoice.From)
    //                               //,new SqlParameter("@To",BALInvoice.To)
    //                               //,new SqlParameter("@ContainerNo",BALInvoice.ContainerNo)
    //                               //,new SqlParameter("@Origin_Country",BALInvoice.Origin_Country)
    //                               //,new SqlParameter("@Destination_Country",BALInvoice.Destination_Country)
    //                               //,new SqlParameter("@Vessel",BALInvoice.Vessel)
    //                               //,new SqlParameter("@FormENo",BALInvoice.FormENo)
    //                               //,new SqlParameter("@Freight",BALInvoice.Freight)
    //                               //,new SqlParameter("@NetWeight",BALInvoice.NetWeight)
    //                               //,new SqlParameter("@GrossWeight",BALInvoice.GrossWeight)
    //                               //,new SqlParameter("@ProformaNo",BALInvoice.ProformaNo)
    //                               //,new SqlParameter("@Insurance",BALInvoice.Insurance)                                  
    //                               //,new SqlParameter("@Exporter",BALInvoice.Exporter)
    //                               //,new SqlParameter("@Consignee",BALInvoice.Consignee)
    //                               //,new SqlParameter("@Buyer",BALInvoice.Buyer)
    //                               //,new SqlParameter("@ExportersRef",BALInvoice.ExportersRef)
    //                               //,new SqlParameter("@BankDetails",BALInvoice.BankDetails)
    //                               //,new SqlParameter("@Quantities",BALInvoice.Quantities)
    //                               //,new SqlParameter("@ShipmentDate",BALInvoice.ShipmentDate)
    //                               //,new SqlParameter("@Note",BALInvoice.Note)
    //                               ,new SqlParameter("@OldProformaNo",BALInvoice.OldProformaNo)
    //                           };
     
    //    int i = Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, CommandType.StoredProcedure, "vt_SCGL_SpCreateModifyProformaInvoice", param2));
    //    return i;
    //}
    //public virtual bool CreateModifyInvoiceDetail(Invoice_BAL_Temp BALInvoice, SqlTransaction Trans)
    //{
    //    SqlParameter[] param = {
    //                                new SqlParameter("@InvoiceID",BALInvoice.InvoiceNo)
    //                               //,new SqlParameter("@ProductServiceID",BALInvoice.ProductServiceID)
    //                               //,new SqlParameter("@Description",BALInvoice.Description2)
    //                               //,new SqlParameter("@KgPerCtn",BALInvoice.KgPerCan)
    //                               //,new SqlParameter("@Quantity",BALInvoice.Quantity)
    //                               //,new SqlParameter("@Rate",BALInvoice.Rate)
    //                               //,new SqlParameter("@Amount",BALInvoice.Amount)
    //                               ,new SqlParameter("@Currency",BALInvoice.Currency)
    //                               ,new SqlParameter("@ConversionRate",BALInvoice.ConversionRate)
    //                               //,new SqlParameter("@PKRAmount",BALInvoice.PKRAmount)
    //                               //,new SqlParameter("@GridName",BALInvoice.GridName)
    //                               //,new SqlParameter("@InventoryID",BALInvoice.InventoryID)
    //                               //,new SqlParameter("@FishID",BALInvoice.FishID)
    //                               //,new SqlParameter("@FishGradeID",BALInvoice.FishGradeID)
    //                               //,new SqlParameter("@FishSizeID",BALInvoice.FishSizeID)
    //                               // ,new SqlParameter("@CostCenterID",BALInvoice.CostCenterID)
    //                                ,new SqlParameter("@InvoiceDate",BALInvoice.InvoiceDate)
    //                                //,new SqlParameter("@CogsRate",BALInvoice.COGSRate)

                                    
    //                           };
    //    int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_CreateModifyInvoiceDetail2", param);
    //    return i > 0;
    //}
    //public virtual bool CreateModifyProformaInvoiceDetail(Invoice_BAL_Temp BALInvoice, SqlTransaction Trans)
    //{
    //    SqlParameter[] param = {
    //                                new SqlParameter("@InvoiceID",BALInvoice.InvoiceNo)
    //                               //,new SqlParameter("@ProductServiceID",BALInvoice.ProductServiceID)
    //                               //,new SqlParameter("@Description",BALInvoice.Description)
    //                               //,new SqlParameter("@Quantity",BALInvoice.Quantity)
    //                               //,new SqlParameter("@Rate",BALInvoice.Rate)
    //                               //,new SqlParameter("@Amount",BALInvoice.Amount)
    //                               ,new SqlParameter("@Currency",BALInvoice.Currency)
    //                               ,new SqlParameter("@ConversionRate",BALInvoice.ConversionRate)
    //                               //,new SqlParameter("@PKRAmount",BALInvoice.PKRAmount)
    //                               //,new SqlParameter("@GridName",BALInvoice.GridName)
    //                               //,new SqlParameter("@InventoryID",BALInvoice.InventoryID)
    //                               //,new SqlParameter("@FishID",BALInvoice.FishID)
    //                               //,new SqlParameter("@FishGradeID",BALInvoice.FishGradeID)
    //                               //,new SqlParameter("@FishSizeID",BALInvoice.FishSizeID)
    //                               // ,new SqlParameter("@CostCenterID",BALInvoice.CostCenterID)
    //                                ,new SqlParameter("@InvoiceDate",BALInvoice.InvoiceDate)
    //                                //,new SqlParameter("@CogsRate",BALInvoice.COGSRate)

                                    
    //                           };
    //    int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_CreateModifyProformaInvoiceDetail", param);
    //    return i > 0;
    //}

    public virtual DataTable getInvoiceDetail(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);

        DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_sp_GetInvoiceDetail", param).Tables[0];
        return dt;
    }


    public virtual bool GetCOGS(Invoice_BAL_Temp BALInvoice, SqlTransaction Trans)
    {
        SqlParameter[] param = {new SqlParameter("@InvoiceID",BALInvoice.InvoiceID)
                                   //,new SqlParameter("@InventoryID",BALInvoice.InventoryID)
                                   ,new SqlParameter("@InvoiceDate",BALInvoice.InvoiceDate)
                               
                               };
        int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_GetCOGS", param);
        return i > 0;
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
    //public virtual DataTable getCogs_Rate(Invoice_BAL_Temp BALInvoice,int InventoryID, int CostCenterID, DateTime InvoiceDate)
    //{
    //    SqlParameter[] param = {new SqlParameter("@InventoryID", BALInvoice.InventoryID),
    //                         new SqlParameter("@CostCenterID", BALInvoice.CostCenterID),
    //                          new SqlParameter("@InvoiceDate", BALInvoice.InvoiceDate) };

    //    DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "sp_Get_Cogs", param).Tables[0];
    //    return dt;
    //}

    public virtual DataTable getCogs_Rate(Invoice_BAL_Temp BALInvoice, SqlTransaction Trans)
    {
        SqlParameter[] param = { 
                                 //  new SqlParameter("@InventoryID", BALInvoice.InventoryID)
                                 //,new SqlParameter("@CostCenterID", BALInvoice.CostCenterID),
                                 new SqlParameter("@InvoiceDate", BALInvoice.InvoiceDate)
                               };
        DataTable dt = SqlHelper.ExecuteDataset(Trans, "sp_Get_Cogs", param).Tables[0];
        return dt;
    }

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


    public virtual SqlDataReader Get_Rows_InvoiceDetail_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetInvoiceDetailRowNumber_byID", param);
    }

    public virtual SqlDataReader Get_Rows_InvoiceDutiesDetail_byID(int InvoiceID)
    {
        SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);
        return SqlHelper.ExecuteReader(SCGL_Common.ConnectionString, "vt_sp_GetInvoiceDutiesDetailRowNumber_byID", param);
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

     public virtual DataTable getCurrentInventory(Invoice_BAL_Temp BALInvoice, SqlTransaction Trans)
    {
        SqlParameter[] param = { 
                                   //new SqlParameter("@InventoryID", BALInvoice.InventoryID),
                                   new SqlParameter("@FinYearID", BALInvoice.FinYearID)
                                //,new SqlParameter("@CostCenterID", BALInvoice.CostCenterID)
                                ,new SqlParameter("@Date", BALInvoice.InvoiceDate)
                                //,new SqlParameter("@SalesQuantity", BALInvoice.SalesQuantity)
                                ,new SqlParameter("@InvoiceID", BALInvoice.InvoiceID)
                               };
        DataTable dt = SqlHelper.ExecuteDataset(Trans, "vt_SCGL_SPCheckCurrentInventory", param).Tables[0];
        return dt;
    }

     public virtual bool InsertCurrentSalesforValuation(Invoice_BAL_Temp BALInvoice, SqlTransaction Trans)
     {
         SqlParameter[] param = { 
                                 //   new SqlParameter("@InventoryID", BALInvoice.InventoryID)
                                 //,new SqlParameter("@Quantity", BALInvoice.SalesQuantity1)
                                 //,new SqlParameter("@CostCenterID", BALInvoice.CostCenterID),
                                 new SqlParameter("@FinYearID", BALInvoice.FinYearID)
                                 ,new SqlParameter("@Date", BALInvoice.InvoiceDate) 
                                 ,new SqlParameter("@InvoiceID", BALInvoice.InvoiceID)
                               };
         int i = SqlHelper.ExecuteNonQuery(Trans, "vt_sp_InsertCurrentSalesforValuation", param);
         return i > 0;
     }

     public virtual DataTable getCurrentSalesforValuation(Invoice_BAL_Temp BALInvoice, SqlTransaction Trans)
     {
         SqlParameter[] param = { 
                                 //   new SqlParameter("@InventoryID", BALInvoice.InventoryID)
                                 //,new SqlParameter("@CostCenterID", BALInvoice.CostCenterID),
                                 new SqlParameter("@FinYearID", BALInvoice.FinYearID)
                                 ,new SqlParameter("@Date", BALInvoice.InvoiceDate)
                                 ,new SqlParameter("@InvoiceID", BALInvoice.InvoiceID)
                               };
         DataTable dt = SqlHelper.ExecuteDataset(Trans, "vt_sp_getCurrentSalesforValuation", param).Tables[0];
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

     public virtual DataTable getCogs_RateforUpdation(Invoice_BAL_Temp BALInvoice, SqlTransaction Trans)
     {
         SqlParameter[] param = { 
                                    //new SqlParameter("@InventoryID", BALInvoice.InventoryID),
                                    new SqlParameter("@InvoiceID", BALInvoice.InvoiceID)
                                 //,new SqlParameter("@CostCenterID", BALInvoice.CostCenterID)   
                                 , new SqlParameter("@InvoiceDate", BALInvoice.InvoiceDate)
                               };
         DataTable dt = SqlHelper.ExecuteDataset(Trans, "vt_SCGL_sp_GetCogsforUpdation", param).Tables[0];
         return dt;
     }

     public virtual int ModifyInvoiceDetailCOGS(Invoice_BAL_Temp BALInvoice, SqlTransaction Trans)
     {
         SqlParameter[] param = {
                                   new SqlParameter("@InvoiceDetailID", BALInvoice.InvoiceDetailID)
                                   //,new SqlParameter("@COGSRate",BALInvoice.COGSRate)
                                   //,new SqlParameter("@COGSAmount",BALInvoice.COGSAmount)
                                   
                               };
         int i = Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, CommandType.StoredProcedure, "vt_SCGL_SpModifyInvoiceDetailCOGS", param));
         return i;
     }

     public virtual int ModifyGLTransactionCOGS(Invoice_BAL_Temp BALInvoice, SqlTransaction Trans)
     {
         SqlParameter[] param = {
                                   new SqlParameter("@InvoiceDetailID", BALInvoice.InvoiceDetailID)
                                   //,new SqlParameter("@COGSRate",BALInvoice.COGSRate)
                                   //,new SqlParameter("@COGSAmount",BALInvoice.COGSAmount)
                                   
                               };
         int i = Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, CommandType.StoredProcedure, "vt_SCGL_SpModifyGLTransactionCOGS", param));
         return i;
     }
     public virtual DataTable GetPSCTable()
     {

         DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_getPhysicalStockCountTable").Tables[0];
         return dt;
     }

     public virtual int DeleteExcessShortandGLTrans(Invoice_BAL_Temp BALInvoice, SqlTransaction Trans)
     {

         return Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, "vt_SCGL_SPDeleteExcessShortandGLTrans"));
     }

     public virtual DataTable getOnHandStock(Invoice_BAL_Temp BALInvoice, SqlTransaction Trans)
     {
         SqlParameter[] param = {new SqlParameter("@FinYearID", BALInvoice.FinYearID)
                               //,new SqlParameter("@InventoryID", BALInvoice.InventoryID)
                               //,new SqlParameter("@CostCenterID", BALInvoice.CostCenterID)
                               //,new SqlParameter("@Date", BALInvoice.PSCDate)
        
        };

         DataTable dt = SqlHelper.ExecuteDataset(Trans, "vt_SCGL_SPGetOnHandStock", param).Tables[0];
         return dt;
     }

     public virtual DataTable GetLastExcessShortID(SqlTransaction Trans)
     {
         DataTable dt = SqlHelper.ExecuteDataset(Trans, "vt_sp_GetLastExcessShortID").Tables[0];
         return dt;
     }

     public virtual int CreateModifyExcessShort(Invoice_BAL_Temp BALInvoice, SqlTransaction Trans)
     {
         SqlParameter[] param =
                            {   
                                //new SqlParameter("@ExcessShortID",BALInvoice.ExcessShortID),
                                //new SqlParameter("@CostCenterID",BALInvoice.CostCenterID),
                                //new SqlParameter("@Date",BALInvoice.PSCDate),
                                //new SqlParameter("@Inventory_Id",BALInvoice.InventoryID),
                                //new SqlParameter("@Difference",BALInvoice.Difference),
                                //new SqlParameter("@FishId",BALInvoice.FishID)  ,   
                                //new SqlParameter("@FishGradeId",BALInvoice.FishGradeID),
                                //new SqlParameter("@FishSizeId",BALInvoice.FishSizeID),
                                new SqlParameter("@FinYearID",BALInvoice.FinYearID)
                                
                            };
         return Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, "vt_sp_CreateExcessShortDetail", param));
     }

     public virtual int ModifyPSCTable(Invoice_BAL_Temp BALInvoice, SqlTransaction Trans)
     {
         SqlParameter[] param = {
                                   //new SqlParameter("@OnHand", BALInvoice.OnHand)
                                   //,new SqlParameter("@Difference",BALInvoice.Difference)
                                   //,new SqlParameter("@PscID",BALInvoice.PscID)
                                   
                               };
         int i = Convert.ToInt32(SqlHelper.ExecuteScalar(Trans, CommandType.StoredProcedure, "vt_SCGL_SpModifyPSCTable", param));
         return i;
     }

     public virtual DataTable getProformaInvoiceDetail(int InvoiceID)
     {
         SqlParameter param = new SqlParameter("@InvoiceID", InvoiceID);

         DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_sp_GetProformaInvoiceDetail", param).Tables[0];
         return dt;
     }

     public virtual int CheckInvoiceReferenceNo(string RefNo, int InvoiceID)
     {
         SqlParameter[] param = { new SqlParameter("@RefNo", RefNo), 
                                 new SqlParameter("@InvoiceID", InvoiceID)};
         return Convert.ToInt32(SqlHelper.ExecuteScalar(SCGL_Common.ConnectionString, "vt_SCGL_SPCheckInvoiceReferenceNo", param));
     }

}
