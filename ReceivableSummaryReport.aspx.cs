using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.Data.SqlClient;
using SW.SW_Common;

public partial class ReceivableSummaryReport : System.Web.UI.Page
{
    ReportDocument transactionReport = new ReportDocument();
    public static DataTable dt = new DataTable();
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "ReceivableSummaryReport.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "ReceivableSummaryReport.aspx" && view == true)
                {
                  
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
        }
       
    }
    
    private void ConfigureCrystalReports()
    {
        string reportPath = Server.MapPath("GL_Report\\ReceivableAgingReport2.rpt");
        transactionReport.Load(reportPath);
        SqlConnectionStringBuilder conf = new SqlConnectionStringBuilder(SCGL_Common.ConnectionString);
        transactionReport.SetDataSource(getreport());
        transactionReport.SetDatabaseLogon(conf.UserID, conf.Password, conf.DataSource, conf.InitialCatalog);
        transactionReport.VerifyDatabase();
        CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX;
        //CrystalReportViewer1.DisplayToolbar = true;
       // CrystalReportViewer1.HasExportButton = true;
        CrystalReportViewer1.ReportSource = transactionReport;
        CrystalReportViewer1.DataBind();
        CrystalReportViewer1.HasPrintButton = false;
        
    }
     //vt_SCGL_INV_SPGetReceivableReport
    private DataTable getreport()
    {
    //     SELECT c.DisplayName as CustomerName,DATEDIFF(day,i.invoiceDate,GetDate()) AS DiffDate,i.invoiceid,i.InvoiceDate,i.Balance from vt_SCGL_invoice  i
  //inner join vt_SCGL_Customer c On i.CustomerID=c.CustomerID
        DataSet ds = new DataSet();
        string str = string.Empty;
        SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("vt_SCGL_INV_SPGetReceivableReport", con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        adpt.Fill(ds);
        ViewState["COA"] = ds;
        SetReport();
        ds = ViewState["COA"] as DataSet;
        con.Close();
        return ds.Tables[0];
    }
    

    protected void CrystalReportViewer1_Navigate(object source, CrystalDecisions.Web.NavigateEventArgs e)
    {
        ConfigureCrystalReports();
    }

    protected void CrystalReportViewer1_Init(object sender, EventArgs e)
    {
        ConfigureCrystalReports();
    }

    protected void chk_balance_CheckedChanged(object sender, EventArgs e)
    {
        ConfigureCrystalReports();
    }
    protected void ddl_Status_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConfigureCrystalReports();
    }


    private void SetReport()
    {
        if (ViewState["COA"] != null)
        {
            DataSet ds;
            DataTable dt;

            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            ds = ViewState["COA"] as DataSet;
            dt = ds.Tables[0].Copy();
            dt.Columns.Add("CompanyName");
            dt.Columns.Add("AsOn");
           
            if (ViewState["ID"] != null)
            {
                
                ViewState["ID"] = null;
            }


            foreach (DataRow dr in dt.Rows)
            {
                dr["CompanyName"] = SBO.SiteName;
                dr["AsOn"] = "As On";
              
            }

            ds.Tables[0].Clear();
            ds.Tables[0].Merge(dt);
            ViewState["COA"] = ds;
        }
    }

    protected void lnkConYes_Click(object sender, EventArgs e)
    {
        int Copies = Convert.ToInt32(TextCopies.Text == "" ? "1" : TextCopies.Text);
        int GivenSPages = Convert.ToInt32(TextStartPages.Text == "" ? "0" : TextStartPages.Text);
        int GivenEPages = Convert.ToInt32(TextEndpages.Text == "" ? "0" : TextEndpages.Text);
        if (GivenEPages != null)
        {
            ConfigureCrystalReports();
            transactionReport.PrintToPrinter(Copies, true, GivenSPages, GivenEPages);
            JQ.closeDialog(this, "ControlConfirmation");
            JQ.showDialog(this, "Confirmation");
            lblDeleteMsg.Text = "Receivable Aging Report Summary Print Successfully ! ";
        }
        else
        {
            JQ.showDialog(this, "Confirmation");
            lblDeleteMsg.Text = "Pages Range Not Valid  ! ";
        }
    }
    protected void btnPrintJava_Click(object sender, EventArgs e)
    {
        ConfigureCrystalReports();
        JQ.showDialog(this, "ControlConfirmation");
        TextCopies.Attributes.Add("placeholder", "1");
        TextStartPages.Attributes.Add("placeholder", "0");
        TextEndpages.Attributes.Add("placeholder", CrystalReportViewer1.ViewInfo.LastPageNumber.ToString());
    }
   
}
