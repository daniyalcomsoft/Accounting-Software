using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using SW.SW_Common;
using System.Data;
using System.Globalization;

public partial class AgingSummaryReport : System.Web.UI.Page
{
    ReportDocument rd = new ReportDocument();
    SqlConnectionStringBuilder conf = new SqlConnectionStringBuilder(SCGL_Common.ConnectionString);
    public static DataTable dt = new DataTable();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["SessionBO"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            Reload_JS();
            Invoice_BAL BALInvoice = new Invoice_BAL();
            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            DataTable dt = PM.getFinancialYearByID(SBO.FinYearID);
            hdnMinDate.Value = SCGL_Common.CheckDateTime(dt.Rows[0]["yearFrom"]).ToShortDateString();
            hdnMaxDate.Value = SCGL_Common.CheckDateTime(dt.Rows[0]["YearTo"]).ToShortDateString();
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "AgingSummaryReport.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "AgingSummaryReport.aspx" && view == true)
                {
                    CrystalReportViewer1.Visible = false;
                    btnPrintJava.Visible = false;
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
            
        }
    }
    public void Reload_JS()
    {
        SCGL_Common.ReloadJS(this, "ChangeDateReport();");
        SCGL_Common.ReloadJS(this, "MyDate();");
    }
    private void ConfigureCrystalReports()
    {
        if (Session["SessionBO"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        string reportPath = Server.MapPath("GL_Report\\AgingSummaryReport2.rpt");
        rd.Load(reportPath);
        rd.SetDataSource(getreport());
        rd.SetDatabaseLogon(conf.UserID, conf.Password, conf.DataSource, conf.InitialCatalog);
        CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX;
        CrystalReportViewer1.ReportSource = rd;
        CrystalReportViewer1.DataBind();
        CrystalReportViewer1.Visible = true;
        btnPrintJava.Visible = false;
        CrystalReportViewer1.HasPrintButton = true;
        ScriptManager.RegisterStartupScript(this, GetType(), "Message", "MyDate();", true);
    }
    private DataSet getreport()
    {
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("vt_SCGL_SpAgingSummeryReport", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (txtReportDate.Text == "")
        {
            string fromdate = "12/15/2013";
            cmd.Parameters.Add("@Date", DateTime.ParseExact(fromdate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
        }
        else
        {
            cmd.Parameters.Add("@Date", DateTime.ParseExact(txtReportDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture));
        }
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        adpt.Fill(ds);
        ViewState["COA"] = ds;
        SetReport();
        ds = ViewState["COA"] as DataSet;
        con.Close();
        return ds;
    }

    private void SetReport()
    {
        if (ViewState["COA"] != null)
        {
            DataTable dt = new DataTable("AgingSummary_tbl");
            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            ds = ViewState["COA"] as DataSet;
            dt = ds.Tables[0].Copy();
            dt.Columns.Add("CompanyName");
            dt.Columns.Add("ReportDate");
            dt.Columns.Add("RemainingAmount");
            dt.Columns.Add("PlusAmount");
            dt.Columns.Add("NameAmount"); 
            foreach (DataRow dr in dt.Rows)
            {                
                dr["CompanyName"] = SBO.SiteName;                   
                if (txtReportDate.Text == "")
                {
                    dr["ReportDate"] = "12/15/2013";
                }
                else
                {
                    dr["ReportDate"] = txtReportDate.Text;
                }
            }

            double AmountSale = 0.0;
            double AmountPay = 0.0;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Type"].ToString() == "Sale")
                {

                    AmountSale = double.Parse(dr["Amount"].ToString());
                }
                if (dr["Type"].ToString() == "Pay")
                {
                    AmountPay = double.Parse(dr["Amount"].ToString());
                }
                if (AmountPay > 0 && AmountSale > 0)
                {
                    double Remaining = AmountSale - AmountPay;
                    dr["RemainingAmount"] = Remaining;
                    AmountSale = 0.0;
                    AmountPay = 0.0;

                }
            }

            ds.Tables[0].Clear();
            string DisplayName = "";
            double RemainAmt = 0.0;
            List<string> lst = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                if (row["RemainingAmount"].ToString() != "")
                {
                    DisplayName = row["DisplayName"].ToString();
                    bool chk = true;
                    for (int i = 0; i < lst.Count; i++)
                    {
                        if (lst[i].ToString() == DisplayName)
                        {
                            chk = false;
                        }
                    }
                    if (chk)
                    {
                        foreach (DataRow row1 in dt.Rows)
                        {
                            if (row1["DisplayName"].ToString() == DisplayName && row1["RemainingAmount"].ToString() != "")
                            {
                                RemainAmt += double.Parse(row1["RemainingAmount"].ToString());
                                row["PlusAmount"]= Convert.ToString(RemainAmt);
                                row["NameAmount"] = Convert.ToString(DisplayName);
                            }
                        }
                        lst.Add(DisplayName);
                    }

                    RemainAmt = 0.0;
                }
            }
            ds.Tables[0].Merge(dt);
            ds.Tables[0].TableName = "AgingSummary_tbl";
            ViewState["COA"] = ds;
        }
    }

    protected void chk_balance_CheckedChanged(object sender, EventArgs e)
    {
        ConfigureCrystalReports();
    }
    protected void ddl_Status_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConfigureCrystalReports();
    }
    protected void CrystalReportViewer1_Navigate(object source, CrystalDecisions.Web.NavigateEventArgs e)
    {
        ConfigureCrystalReports();
    }
    protected void CrystalReportViewer1_Init(object sender, EventArgs e)
    {
        ConfigureCrystalReports();
    }
    protected void CrystalReportViewer1_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CrystalReportViewer1.ReportSource = rd;
            CrystalReportViewer1.DataBind();
            
        }
    }

    protected void lnkConYes_Click(object sender, EventArgs e)
    {
        int Copies = SCGL_Common.Convert_ToInt(TextCopies.Text == "" ? "1" : TextCopies.Text);
        int GivenSPages = SCGL_Common.Convert_ToInt(TextStartPages.Text == "" ? "0" : TextStartPages.Text);
        int GivenEPages = SCGL_Common.Convert_ToInt(TextEndpages.Text == "" ? "0" : TextEndpages.Text);
        if (GivenEPages != null)
        {
            ConfigureCrystalReports();
            rd.PrintToPrinter(Copies, true, GivenSPages, GivenSPages);
            JQ.closeDialog(this, "ControlConfirmation");
            JQ.showDialog(this, "Confirmation");
            lblDeleteMsg.Text = "Aging Summary Report Print Successfully ! ";
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataSet ds = new DataSet();
        if (SBO.Can_View == true)
        {
            ds = getreport();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ConfigureCrystalReports();
                CrystalReportViewer1.Visible = true;
                btnPrintJava.Visible = false;
            }
            else
            {
                JQ.showStatusMsg(this, "2", "No Record Found");
                CrystalReportViewer1.Visible = false;
                btnPrintJava.Visible = false;
            }
        }
        else
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to View  Record");
        }
    }
}
