using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using SW.SW_Common;

public partial class PurchaseInvoiceReport : System.Web.UI.Page
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "PurchaseInvoiceReport.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "PurchaseInvoiceReport.aspx" && view == true)
                {
                    Bind_CostCenter();
                    ConfigureCrystalReports();
                    CrystalReportViewer1.Visible = false;
                    btnPrintJava.Visible = false;
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
           
        }
        Reload_JS();
        ConfigureCrystalReports();
        Invoice_BAL BALInvoice = new Invoice_BAL();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataTable dt = PM.getFinancialYearByID(SBO.FinYearID);
        hdnMinDate.Value = SCGL_Common.CheckDateTime(dt.Rows[0]["yearFrom"]).ToShortDateString();
        hdnMaxDate.Value = SCGL_Common.CheckDateTime(dt.Rows[0]["YearTo"]).ToShortDateString();
    }
    public void Reload_JS()
    {   
        SCGL_Common.ReloadJS(this, "ChangeDateFrom();");
        SCGL_Common.ReloadJS(this, "ChangeDateTo();");
        SCGL_Common.ReloadJS(this, "MyDate();");
    }

    public void Bind_CostCenter()
    {
        DataTable dt = new SuperAdmin_BAL().GetCostCenterList();
        PM.BindDropDown(ddlCostCenter, dt, "CostCenterID", "CostCenterName");
        ddlCostCenter.Items.Insert(0, new ListItem("All Centers", "0"));

    }

    private void ConfigureCrystalReports()
    {
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            string reportPath = Server.MapPath("GL_Report\\PurchaseReport.rpt");
            rd.Load(reportPath);
            rd.SetDataSource(getreport());
            rd.SetDatabaseLogon(conf.UserID, conf.Password, conf.DataSource, conf.InitialCatalog);
            CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX;
            CrystalReportViewer1.ReportSource = rd;
            CrystalReportViewer1.DataBind();
            if (!IsPostBack)
            {
                btnPrintJava.Visible = false;
                CrystalReportViewer1.Visible = false;
            }
            else
            {
                btnPrintJava.Visible = true;
                CrystalReportViewer1.Visible = true;
            }
            CrystalReportViewer1.HasPrintButton = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "MyDate();", true);
        }
    }

    private DataSet getreport()
    {
        DataSet ds = new DataSet();
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("vt_SCGL_Sp_PurchaseInvoiceReport", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IsActive", ddl_Status.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@DateFrom", txtFromDate.Text);
            cmd.Parameters.AddWithValue("@DateTo", txtToDate.Text);
            cmd.Parameters.AddWithValue("@CostCenterID", ddlCostCenter.SelectedValue);
            //if (txtFromDate.Text == "")
            //{
            //    string fromdate = "11/04/2013";
            //    cmd.Parameters.Add("@DateFrom", DateTime.ParseExact(fromdate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
            //}
            //else
            //{
            //    cmd.Parameters.Add("@DateFrom", DateTime.ParseExact(txtFromDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture));
            //}
            //if (txtToDate.Text == "")
            //{
            //    string todate = "12/04/2013";
            //    cmd.Parameters.Add("@DateTo", DateTime.ParseExact(todate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
            //}
            //else
            //{
            //    cmd.Parameters.Add("@DateTo", DateTime.ParseExact(txtToDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture));
            //}
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(ds);
            ViewState["COA"] = ds;
            SetReport();
            ds = ViewState["COA"] as DataSet;
            con.Close();
        }
        return ds;
    }

    private void SetReport()
    {
        if (ViewState["COA"] != null)
        {
            DataTable dt = new DataTable("PurchaseInvoiceReport");
            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            ds = ViewState["COA"] as DataSet;
            dt = ds.Tables[0].Copy();
            dt.Columns.Add("CompanyName");
            dt.Columns.Add("VoucherTypeName");
            if (ViewState["ID"] != null)
            {
                dt.Columns.Add("StatusType");
                foreach (DataRow dr in dt.Rows)
                {
                    int Acitve = Convert.ToInt32(dr["Active"]);
                    if (Acitve == 1)
                    {
                        dr["StatusType"] = "Active";
                    }
                    else
                    {
                        dr["StatusType"] = "InActive";
                    }
                }
                ViewState["ID"] = null;
            }
            foreach (DataRow dr in dt.Rows)
            {
                dr["CompanyName"] = SBO.SiteName;
                dr["VoucherTypeName"] = "Purchase Invoice Report";
            }

            ds.Tables[0].Clear();
            ds.Tables[0].Merge(dt);
            ds.Tables[0].TableName = "PurchaseInvoiceReport";
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
    //protected void btn_Search_Click(object sender, EventArgs e)
    //{
    //    ConfigureCrystalReports();
    //}
    protected void lnkConYes_Click(object sender, EventArgs e)
    {
        int Copies = SCGL_Common.Convert_ToInt(TextCopies.Text == "" ? "1" : TextCopies.Text);
        int GivenSPages = SCGL_Common.Convert_ToInt(TextStartPages.Text == "" ? "0" : TextStartPages.Text);
        int GivenEPages = SCGL_Common.Convert_ToInt(TextEndpages.Text == "" ? "0" : TextEndpages.Text);
        if (GivenEPages != null)
        {
            ConfigureCrystalReports();
            rd.PrintToPrinter(Copies, true, GivenSPages, GivenEPages);
            JQ.closeDialog(this, "ControlConfirmation");
            JQ.showDialog(this, "Confirmation");
            lblDeleteMsg.Text = "Purchase Invoice Report Print Successfully ! ";
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
                btnPrintJava.Visible = true;
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
