using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SW.SW_Common;

public partial class Main : System.Web.UI.MasterPage
{
    protected int timeout;
    bool Allowpage = false;
    string PageRefrence = string.Empty;
    string PageUrl = string.Empty;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        LbtnEditSetup.Visible = false;
        Session.Timeout = 300;
        timeout = (Session.Timeout * 1660000) - 60000;
        timeout = 3000;
        if (!IsPostBack)
        {
            if (Session["SessionBO"] != null)
            {
                CheckAllowedPages();
                Check();
            }
            else
                Response.Redirect("Login.aspx");
        }
        
        if (Session["SessionBO"] != null)
        {
            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            lblUserName.Text = SBO.UserName;
            LabelSiteName.Text = SBO.SiteName;
            getFinYear();
            
        }
    }

    #region Method
    
    #endregion

  
    private int getSettingID()
    {
        try
        {
            int returnID = 0;
            Setting_BAL BLL = new Setting_BAL();
            DataTable dt = BLL.GetSetingData();
            if (dt.Rows.Count > 0)
            {
                returnID = Convert.ToInt32(dt.Rows[0]["Setting_Id"]);
            }
            return returnID;
        }
        catch (Exception ex)
        { return 0; }
    }

   
    public static string getUserIP()
    {
        string VisitorsIPAddr = string.Empty;
        //Users IP Address.                
        if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            //To get the IP address of the machine and not the proxy
            VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
        else if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
            VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
        else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
        return VisitorsIPAddr;

    }
      
    #region PageRights


    private void CheckAllowedPages()
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.UserName != "root")
        {
            if (SBO.Permission != null)
            {
                PageUrl = Request.Url.Segments.Last();
                DataRow[] dr = SBO.PermissionTable.Select("Page_Url='" + PageUrl.ToString() + "'");
                if (dr.Length > 0)
                {
                    if (true)
                    {
                        SBO.Can_Insert = dr.Length > 0 ? Convert.ToBoolean(dr[0]["Can_Insert"]) : false;
                        SBO.Can_Update = dr.Length > 0 ? Convert.ToBoolean(dr[0]["Can_Update"]) : false;
                        SBO.Can_Delete = dr.Length > 0 ? Convert.ToBoolean(dr[0]["Can_Delete"]) : false;
                        SBO.Can_View = dr.Length > 0 ? Convert.ToBoolean(dr[0]["Can_View"]) : false;
                        //SBO.Can_Approve = dr.Length > 0 ? Convert.ToBoolean(dr[0]["Can_ApproveOrReject"]) : false;
                    }

                    else
                    {
                        Response.Redirect("" + SBO.PageRefrence.ToString() + "");
                    }
                }
                //else
                //{
                //    Response.Redirect("Default.aspx");
                //}

              
            }
        
        }
    }




    private void Check()
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.UserName == "root")
        {
            RootUser();
        }
        else
        {
            string Page = string.Empty;
            for (int i = 0; i < SBO.PermissionTable.Rows.Count; i++)
            {
                Page = SBO.PermissionTable.Rows[i]["Page_Url"].ToString();
                string[] PageName = Page.Split('.');
                string PageUrl = PageName[0].ToString();
                this.FindControl(PageUrl).Visible = true;
                //this.FindControl("SalesTaxInvoice").Visible = true;
                //this.FindControl("SalesTaxInvoice_View").Visible = true;
            }
            SBO.Permission = "Checked";
            SBO.PageRefrence = Request.Url.Segments.Last();
            //SBO.PageRefrence = Request.Url.Segments[2].ToString();
        }
    }
    #endregion

    #region MasterUserRoot
    private void RootUser()
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        GLMain.Visible = true;
        GLHome.Visible = true;
        GLGeneralVoucher.Visible = true;
        PurchaseInvoiceReport.Visible = true;
        GLCashRecievedVoucher.Visible = true;
        GLCashPaymentVoucher.Visible = true;
        GLOpeningBalance.Visible = true; 
        CustomerForm_Views.Visible = true;
        ExpenseSheetForm.Visible = true;
        Invoice_Views.Visible = true;
        ReceivePayment_Views.Visible = true;
        InventoryForm_Views.Visible = true;
        InventoryAdjustment_Views.Visible = true;
        FishName.Visible = true;
        FishSize.Visible = true;
        FishGrade.Visible = true;
        PurchaseInvoice_Views.Visible = true;
        PayPayment.Visible = true;
        ProformaInvoice_Views.Visible = true;
        VendorForm_Views.Visible = true;
        SE_AdminUsers.Visible = true;
        SE_SetupList.Visible = true;
        SE_SuperAdminSetup.Visible = true;
        CreateUser.Visible = true;
        DBbackup.Visible = true;
        SE_SuperCostCenter.Visible = true;
        PhysicalStockCount.Visible = true;
        CreateUser.Visible = true;
        SE_AdminUsers.Visible = true;
        SE_SuperCostCenter.Visible = true;
        SalesTax.Visible = true;
        FinancialYear.Visible = true;
        SettingForm_Views.Visible = true;
        SE_SuperAdminSetup.Visible = true;
        SE_SetupList.Visible = true;
        //Reports

        GL_Report.Visible = true;
        GL_ChartOfAccount.Visible = true;
        TrialBalanceReport_new.Visible = true;
        BalanceSheetReportSummary.Visible = true;
        GL_Subsidiary.Visible = true;
        ProfitandLossReportSummary.Visible = true;
        Sales_Invoice_Report.Visible = true;

        commercialinvoicereport.Visible = true;
        proformainvoicereport.Visible = true;
        ReceivableSummaryReport.Visible = true;
        PurchaseInvoiceReport.Visible = true;
        PhysicalStockCount.Visible = true;
       
        
        SBO.Can_Insert = Convert.ToBoolean("true");
        SBO.Can_Update = Convert.ToBoolean("true");
        SBO.Can_Delete = Convert.ToBoolean("true");
        SBO.Can_View =   Convert.ToBoolean("true");
        //SBO.Can_Approve =Convert.ToBoolean("true");
    }
#endregion

    # region Redirects

   
    protected void LbtnCostCenter_Click(object sender, EventArgs e)
    {
        Response.Redirect("Se_SuperCostCenter.aspx");
    }
    protected void LbtnBarchart_Click(object sender, EventArgs e)
    {
        Response.Redirect("BarChart.aspx");
    }

    protected void lnkDBbackup_Click(object sender, EventArgs e)
    {
        Response.Redirect("DBbackup.aspx");
    }

    protected void lnkExecuteQuery_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExecuteQuery.aspx");
    }
    protected void LbtnGeneralVoucher_Click(object sender, EventArgs e)
    {
        Response.Redirect("GLGeneralVoucher.aspx");
    }
    protected void LbtnChartOfAcc_Click(object sender, EventArgs e)
    {
        Response.Redirect("GLMain.aspx");
    }
    protected void LbtnCustomer_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerForm_Views.aspx");

    }
    protected void lbtnJob_Click(object sender, EventArgs e)
    {
        Response.Redirect("JobForm_Views.aspx");

    }
    protected void lbtnExpenseSheetForm_Views_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExpenseSheetForm_Views.aspx");

    }
    protected void lbtnDepositSheetForm_Views_Click(object sender, EventArgs e)
    {
        Response.Redirect("DepositSheetForm_Views.aspx");

    }
    protected void lbtnSalesTaxInvoice_View_Click(object sender, EventArgs e)
    {
        Response.Redirect("SalesTaxInvoice_View.aspx");

    }
    protected void LbtnSecurity_Click(object sender, EventArgs e)
    {
        Response.Redirect("SE_AdminUsers.aspx");

    }
    protected void LbtnManageInvoiceDescription_Click(object sender, EventArgs e)
    {
        Response.Redirect("InvoiceDescription.aspx");

    }
    protected void LbtnManageDutiesDescription_Click(object sender, EventArgs e)
    {
        Response.Redirect("DutiesDescription.aspx");

    }
    protected void LbtnManageShippingLine_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShippingLine.aspx");

    }
    protected void lbtnJobSheet_Click(object sender, EventArgs e)
    {
        Response.Redirect("JobSheetForm_Views.aspx");

    }
    protected void LbtnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.RemoveAll();
        Response.Redirect("Login.aspx");
    }

    protected void LbtnSettings_Click(object sender, EventArgs e)
    {
        Response.Redirect("SettingForm.aspx?Id=" + getSettingID());
    }
    protected void lbtnSalesTax_Click(object sender, EventArgs e)
    {
        Response.Redirect("SalesTax.aspx");
    }
    protected void lbtnFinYear_Click(object sender, EventArgs e)
    {
        Response.Redirect("FinancialYear.aspx");
    }
   
    protected void ltbnCreateUser_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateUser.aspx");
    }
    protected void lbtnGLReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("GL_Report.aspx");
    }
    protected void lbtnChartOfAccReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("GL_ChartOfAccount.aspx");
    }
    protected void lbtnTrailBalanceRpt_Click(object sender, EventArgs e)
    {
        Response.Redirect("GLReport_TrailBalance.aspx");
    }

    protected void LbtnSalesInvoice_Click(object sender, EventArgs e)
    {
        Response.Redirect("Invoice_views.aspx");
    }

    protected void LbtnInvoiceStatusForm_Click(object sender, EventArgs e)
    {
        Response.Redirect("InvoiceStatusForm.aspx");
    }

    protected void LbtnProformaInvoice_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProformaInvoice_views.aspx");
    }

    protected void LbtnUpdateCOGS_Click(object sender, EventArgs e)
    {
        Response.Redirect("UpdateCOGS.aspx");
    }

    protected void LbtnReceivePayment_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReceivePayment_Views.aspx");
    }

    protected void LbtnPayPayment_Click(object sender, EventArgs e)
    {
        Response.Redirect("PayPayment_Views.aspx");
    }

    protected void LbtnPurchaseInvoice_Click(object sender, EventArgs e)
    {
        Response.Redirect("PurchaseInvoice_views.aspx");
    }

    protected void LbtnVendor_Click(object sender, EventArgs e)
    {
        Response.Redirect("VendorForm_Views.aspx");
    }
    protected void LbtnCreateInventory_Click(object sender, EventArgs e)
    {
        Response.Redirect("InventoryForm.aspx");
    }
    protected void LbtnAdjustmentInventory_Click(object sender, EventArgs e)
    {
        Response.Redirect("InventoryAdjustment_Views.aspx");
    }

    protected void lbtnPhysicalStockCount_Click(object sender, EventArgs e)
    {
        Response.Redirect("PhysicalStockCount.aspx");
    }
    protected void lbtnInventoryReportSummary_Click(object sender, EventArgs e)
    {
        Response.Redirect("InventoryReportSummary.aspx");
    }
    protected void lbtnCOGSReportSummary_Click(object sender, EventArgs e)
    {
        Response.Redirect("COGSReportSummary.aspx");
    }
    protected void lbtnPackingListSummaryReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("PackingListSummaryReport.aspx");
    }
    protected void LbtnFishName_Click(object sender, EventArgs e)
    {
        Response.Redirect("FishName.aspx");
    }
    protected void LbtnFishSize_Click(object sender, EventArgs e)
    {
        Response.Redirect("FishSize.aspx");
    }
    protected void LbtnFishGrade_Click(object sender, EventArgs e)
    {
        Response.Redirect("FishGrade.aspx");
    }
    protected void LbtnCashRecieptVoucher_Click(object sender, EventArgs e)
    {
        Response.Redirect("GLCashRecievedVoucher.aspx");
    }
    protected void LbtnAddNewVoucher_Click(object sender, EventArgs e)
    {
        Response.Redirect("GLHome.aspx");
    }
    protected void LbtnCashPaymentVoucher_Click(object sender, EventArgs e)
    {
        Response.Redirect("GLCashPaymentVoucher.aspx");
    }

    protected void lbtnBalanceSheetRpt_Click(object sender, EventArgs e)
    {
        Response.Redirect("GLReport_BalanceSheet.aspx");
    }
    protected void lbtnBalanceSheetRpt_Test_Click(object sender, EventArgs e)
    {
        Response.Redirect("BalanceSheetReportSummary.aspx");
    }
    protected void LbtnGLReport_TrailBalance_Click(object sender, EventArgs e)
    {
        Response.Redirect("GLReport_TrailBalance.aspx");
    }

    protected void lbtnSubsidaryRpt_Click(object sender, EventArgs e)
    {
        Response.Redirect("GL_Subsidiary.aspx");
    }

    protected void lbtnProfitandLossRpt_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProfitandLossReportSummary.aspx");
    }

    protected void lbtnProfitandLoss_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProfitandLoss_Trial.aspx");
    }



    protected void LbtnOpeningBalance_Click(object sender, EventArgs e)
    {
        Response.Redirect("GLOpeningBalance.aspx");
    }

    protected void lbtnSicColumnRpt_Click(object sender, EventArgs e)
    {
        Response.Redirect("GL_SixColumns_TB.aspx");
    }

    protected void lbtnTrialBalanceRpt_Click(object sender, EventArgs e)
    {
        Response.Redirect("TrialBalanceReport_new.aspx");
    }

    protected void lbtnTrialBalanceRpt_control_Click(object sender, EventArgs e)
    {
        Response.Redirect("TrialBalanceReport_new_control.aspx");
    }

    protected void LbtnNewSetup_Click(object sender, EventArgs e)
    {
        Response.Redirect("SE_SuperAdminSetup.aspx");
    }

    protected void lbtnExpenseSheet_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExpenseSheetReport.aspx");
    }

    protected void lbtnDepositSheet_Click(object sender, EventArgs e)
    {
        Response.Redirect("DepositSheetReport.aspx");
    }

    protected void lbtnSaleReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("Sales_Invoice_Report.aspx");
    }

    protected void lbtnSalesTaxInvoice_Report_Click(object sender, EventArgs e)
    {
        Response.Redirect("SalesTaxInvoice_Report.aspx");
    }
   
    protected void lbtnCommercialInvoiceReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("CommercialInvoiceReport.aspx");
    }
    protected void lbtnDailyReportSheet_Click(object sender, EventArgs e)
    {
        Response.Redirect("DailyReportSheet.aspx");
    }
    protected void lbtnInvoiceStatusReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("InvoiceStatusReport.aspx");
    }
    protected void lbtnDailyStatementSheet_Click(object sender, EventArgs e)
    {
        Response.Redirect("DailyStatementSheet.aspx");
    }
    protected void lbtnPaymentTypesReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("PaymentTypesReport.aspx");
    }
    protected void lbtnTotalSalestaxReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("TotalSalestaxReport.aspx");
    }
    protected void lbtnCNFImportValueReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("CNFImportValueReport.aspx");
    }
    protected void lbtnProformaInvoiceReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProformaInvoiceReport.aspx");
    }
    protected void lbtnReceivableSummaryReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReceivableSummaryReport.aspx");
    }
    protected void lbtnPurchasereport_Click(object sender, EventArgs e)
    {
        Response.Redirect("PurchaseInvoiceReport.aspx");
    }

    protected void LbtnEditSetup_Click(object sender, EventArgs e)
    {
        Response.Redirect("SE_SetupList.aspx");
    }
    protected void lbtnMyAccount_Click(object sender, EventArgs e)
    {

        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int UserID = SBO.UserID;
        string UserID_encrypt = Uri.EscapeDataString(EncryptDecrypt.Encrypt(UserID.ToString()));
        Response.Redirect("CreateUser.aspx?UserID=" + UserID_encrypt);


    }

    #endregion
    private void getFinYear()
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinId = SBO.FinYearID;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("vt_SCGL_SpGetFinancialYearTitle", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FinYearID", FinId);

        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        adpt.Fill(ds);
        DataTable dt = ds.Tables[0];
        LabelFinYear.Text = dt.Rows[0]["FinYearTitle"].ToString() + " " +" Ver 1.07";


    }
}
