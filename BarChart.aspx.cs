using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SW.SW_Common;
using System.Data;
using System.IO;
using System.Text;

public partial class BarChart : System.Web.UI.Page
{
    string Html="";
    Invoice_BAL BAL = new Invoice_BAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionBO"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (!IsPostBack)
        {
            BAL_PagePermissions PP = new BAL_PagePermissions();
            DataTable dtRole = new DataTable();
            SCGL_Session AdSes = (Session["SessionBO"]) as SCGL_Session;
            dtRole = PP.GetPermissionByUserId(SCGL_Common.Convert_ToInt(AdSes.RoleId));
            string pageName = null;
            bool view = false;
            foreach (DataRow dr in dtRole.Rows)
            {
                int row = dtRole.Rows.IndexOf(dr);
                if (dtRole.Rows[row]["Page_Url"].ToString() == "BarChart.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "BarChart.aspx" && view == true)
                {
                    //allFunctions();
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
                   
          //  CreateHtml();
        }
    }
    public void allFunctions()
    {
        GetTotalSales_Graph();
        GetTotalExpense_Graph();
        GetTotalBalance_Graph();
        GetTotalBanks_Graph();
        GetTotalGwadar_Production();
        GetTotalKarachi_Production();
        GetTotalPasni_Production();
        GetTotalPasniPurchasesInKg();
        GetTotalGwadarPurchasesInKg();
        GetTotalKarachiPurchasesInKg();
    }

    public void GetTotalSales_Graph()
    { 
    SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
    DataTable dtFinYear = new DataTable();
    DataTable dt = new DataTable();
    DataTable TotalIncome = new DataTable();
    dtFinYear = BAL.GetCurrentFisYear(SBO.FinYearID);

    dt = BAL.GetTotalSales_Graph(dtFinYear.Rows[0]["YearFrom"].ToString(), dtFinYear.Rows[0]["YearTo"].ToString());
    TotalIncome = BAL.GetTotalIncome_Graph(dtFinYear.Rows[0]["YearFrom"].ToString(), dtFinYear.Rows[0]["YearTo"].ToString());
    DataTable TotalMargin = new DataTable();
    TotalMargin = BAL.GetTotalMargin_Graph(dtFinYear.Rows[0]["YearFrom"].ToString(), dtFinYear.Rows[0]["YearTo"].ToString());

    double TotalMarginResult =SCGL_Common.Convert_ToDouble(TotalMargin.Rows[0]["Result"].ToString());

    double TotalMarginIncome = SCGL_Common.Convert_ToDouble(TotalMargin.Rows[0]["TotalIncome"].ToString());
    if (TotalMarginIncome == 0.00)
    {
        TotalMarginIncome = 1;
    }
    double Result = (TotalMarginResult) / (TotalMarginIncome) * 100;


    DataTable TotalEquity = new DataTable();
    TotalEquity = BAL.GetTotalEquity_Graph(dtFinYear.Rows[0]["YearFrom"].ToString(), dtFinYear.Rows[0]["YearTo"].ToString());


    DataTable dts = new DataTable();
    dts = BAL.GetTotalBalance_Graph();
    lblOverDue.Text = "$" + dts.Rows[0]["Balance"].ToString();

    DataTable lastdaysdt = new DataTable();
    lastdaysdt = BAL.GetLastDaysReceipts();

    double d = SCGL_Common.Convert_ToDouble(TotalIncome.Rows[0]["NetIncome"].ToString());
    txtTotalSales.Text = d.ToString("#,#0.00");

    double open = SCGL_Common.Convert_ToDouble(dt.Rows[0]["OpenSales"].ToString());
    lblopeninvoices.Text ="$" +open.ToString("#0.00");

    double lastdays = SCGL_Common.Convert_ToDouble(lastdaysdt.Rows[0]["lastDays"].ToString());
    lbllastdays.Text = "$" + lastdays.ToString("#0.00");



    double lblTotalMargins = SCGL_Common.Convert_ToDouble(Result);
    lblSalesMargin.Text = lblTotalMargins.ToString("#0.00") + "%";

    double lblTotalEquity = SCGL_Common.Convert_ToDouble(TotalEquity.Rows[0]["column1"].ToString());
    lblReturnOnEquity.Text = lblTotalEquity.ToString("#0.00") + "%";
       
        
    double a = SCGL_Common.Convert_ToDouble(dt.Rows[0]["OpenSales"]);
    double b = SCGL_Common.Convert_ToDouble(dts.Rows[0]["Balance"]);
    double c = SCGL_Common.Convert_ToDouble(lastdaysdt.Rows[0]["lastDays"]);

    double Total = SCGL_Common.Convert_ToDouble(a + b + c);
    lbltotal.Text = Total.ToString("#0.00");

    double Result1 = SCGL_Common.Convert_ToDouble(a / Total) * 100;

   // float.IsNaN(Result1);
    //if (SCGL_Common.Convert_ToDouble(Result1).ToString() == "Nan")
    //{
    //    Result1
    //    StringBuilder b = new StringBuilder(s);
    
    //}
    if (Double.IsNaN(Result1))
    {
        Result1 = 0.00;
    }
    lblperc1.Text = Result1.ToString("#0.00");
    per1.Style.Add("Width", lblperc1.Text+"px");

    double Result2 = SCGL_Common.Convert_ToDouble(b / Total) * 100;

    if (Double.IsNaN(Result2))
    {
        Result2 = 0.00;
    }
    lblperc2.Text = Result2.ToString("#0.00");
    per2.Style.Add("Width", lblperc2.Text + "px");

    double Result3 = SCGL_Common.Convert_ToDouble(c / Total) * 100;
    if (Double.IsNaN(Result3))
    {
        Result3 = 0.00;
    }
    lblperc3.Text = Result3.ToString("#0.00");

    per3.Style.Add("Width", lblperc3.Text + "px");
    }
    public void GetTotalBalance_Graph()
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataTable dtFinYear = new DataTable();
        DataTable dt = new DataTable();
        dtFinYear = BAL.GetCurrentFisYear(SBO.FinYearID);
        dt = BAL.GetTotalBalance_Graph();
        double over = SCGL_Common.Convert_ToDouble(dt.Rows[0]["Balance"].ToString());
        lblOverDue.Text = "$" +  over.ToString("#0.00");
      
      

    }

    public void GetTotalExpense_Graph()
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataTable dtFinYear = new DataTable();
        DataTable dt = new DataTable();
        dtFinYear = BAL.GetCurrentFisYear(SBO.FinYearID);
        dt = BAL.GetTotalExpense_Graph(dtFinYear.Rows[0]["YearFrom"].ToString(), dtFinYear.Rows[0]["YearTo"].ToString());
        double s =SCGL_Common.Convert_ToDouble(dt.Rows[0]["TotalExpense"].ToString());
        txtTotalExpense.Text =s.ToString("#,#0.00");
       
        
    }


    public void GetTotalPasni_Production()
    {
        
        DataTable dt = new DataTable();
       
        dt = BAL.GetPasniProduction();
        double s = SCGL_Common.Convert_ToDouble(dt.Rows[0]["Total"].ToString());
        txtPasni.Text = s.ToString("#0.00");
        

    }
    public void GetTotalGwadar_Production()
    {

        DataTable dt = new DataTable();

        dt = BAL.GetGwadarProduction();
        double s = SCGL_Common.Convert_ToDouble(dt.Rows[0]["Total"].ToString());
        txtGwadar.Text =  s.ToString("#0.00");


    }
    public void GetTotalKarachi_Production()
    {

        DataTable dt = new DataTable();

        dt = BAL.GetKarachiProduction();
        double s = SCGL_Common.Convert_ToDouble(dt.Rows[0]["Total"].ToString());
        txtKarachi.Text =  s.ToString("#0.00");
        

    }

    public void GetTotalPasniPurchasesInKg()
    {

        DataTable dt = new DataTable();

        dt = BAL.GetPasniPurchasesInKg();
        double s = SCGL_Common.Convert_ToDouble(dt.Rows[0]["Total"].ToString());
        txtPurchasesPasni.Text = s.ToString("#0.00");


    }
    public void GetTotalGwadarPurchasesInKg()
    {

        DataTable dt = new DataTable();

        dt = BAL.GetGwadarPurchasesInKg();
        double s = SCGL_Common.Convert_ToDouble(dt.Rows[0]["Total"].ToString());
        txtPurchasesGwadar.Text = s.ToString("#0.00");


    }
    public void GetTotalKarachiPurchasesInKg()
    {

        DataTable dt = new DataTable();

        dt = BAL.GetKarachiPurchasesInKg();
        double s = SCGL_Common.Convert_ToDouble(dt.Rows[0]["Total"].ToString());
        txtPurchasesKarachi.Text = s.ToString("#0.00");


    }




    public string CreateHtml()
    {
    SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
    DataTable dtFinYear = new DataTable();
    DataTable dt = new DataTable();
    dtFinYear = BAL.GetCurrentFisYear(SBO.FinYearID);
    dt = BAL.GetTotalSales_Temp(dtFinYear.Rows[0]["YearFrom"].ToString(), dtFinYear.Rows[0]["YearTo"].ToString());
   // dt = BAL.GetTotalSales_Graph(dtFinYear.Rows[0]["YearFrom"].ToString(), dtFinYear.Rows[0]["YearTo"].ToString());
    //txtTotalSales.Text = "$" + dt.Rows[0]["TotalSales"].ToString();

    if (dt.Rows[0]["OpenSales"] != null && dt.Rows[0]["OpenSales"] != "")
        {

            Html+="<div>";
     
            for (int i = 0; i < dt.Rows.Count; i++)
            {
     
        //         <div class="r_account_info">
        //    <span class="float_left">Creadit Card</span>
        //    <span class="float_right">$0.0</span>
        //</div>
                
                Html += "<div class='r_account_info'>" +
 "<Span  runat='server' CssClass='float_left'> sdh </Span>" +
 " <br />" +
 "<span class='float_left'>NET INCOME</span>"
 + "</div>" + "</div>";
            
            }
            
            Html+="</div>";
        }
        else
        {

        }
        Dynamic.InnerHtml = Html;
        return Html;
        
    }




    public void GetTotalBanks_Graph()
    {

        DataTable dt = new DataTable();
        DataTable dtFinYear = new DataTable();
       SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        dtFinYear = BAL.GetCurrentFisYear(SBO.FinYearID);
        dt = BAL.GetAllBanks(SBO.FinYearID, dtFinYear.Rows[0]["YearFrom"].ToString(), dtFinYear.Rows[0]["YearTo"].ToString());

        if (dt.Rows.Count > 0)
        {
            if (dt.Rows.Count >= 1)
            {
                Bank1Text.InnerText = dt.Rows[0]["Title"].ToString();
                double Bank1 =SCGL_Common.Convert_ToDouble(dt.Rows[0]["CurrentBal"].ToString());
                Bank1Value.InnerText = Bank1.ToString("#0.00");
                
            }
            if (dt.Rows.Count >= 2)
            {
                Bank2Text.InnerText = dt.Rows[1]["Title"].ToString();
                double Bank2 = SCGL_Common.Convert_ToDouble(dt.Rows[1]["CurrentBal"].ToString());
                Bank2Value.InnerText = Bank2.ToString("#0.00");
            }
            if (dt.Rows.Count >= 3)
            {
                Bank3Text.InnerText = dt.Rows[2]["Title"].ToString();
                double Bank3 = SCGL_Common.Convert_ToDouble(dt.Rows[2]["CurrentBal"].ToString());
                Bank3Value.InnerText = Bank3.ToString("#0.00");
            }
           
        }
        }

    protected void lnkBank_Click(object sender, EventArgs e)
    {
        JQ.showDialog(this, "NewCostCenter");
    }
}
   
       
   
       

    






