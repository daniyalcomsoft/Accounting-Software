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
using System.Globalization;

public partial class Sales_Invoice_Report : System.Web.UI.Page
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "Sales_Invoice_Report.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "Sales_Invoice_Report.aspx" && view == true)
                {
                    Bind_CostCenter();
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
        //if (!IsPostBack)
        //{
        //  CrystalReportViewer1.Visible = false;
        //}
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
            string reportPath = Server.MapPath("GL_Report\\SalesReport.rpt");
            rd.Load(reportPath);
            rd.SetDataSource(getreport());
            rd.SetDatabaseLogon(conf.UserID, conf.Password, conf.DataSource, conf.InitialCatalog);
            CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX;
            CrystalReportViewer1.ReportSource = rd;
            CrystalReportViewer1.DataBind();
            //rd.VerifyDatabase();
            //CrystalReportViewer1.Visible = true;
            //CrystalReportViewer1.HasPrintButton = false;
            //btnPrintJava.Style.Add("display", "block");
            if (!IsPostBack)
            {
                btnPrintJava.Visible = false;
                CrystalReportViewer1.Visible = false;
            }
            else
            {
                CrystalReportViewer1.Visible = true;
                btnPrintJava.Visible = false;
            }
            CrystalReportViewer1.HasPrintButton = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "MyDate();", true);
      
    }
    private DataSet getreport()
    {
        DataSet ds = new DataSet();
        //if (txtFromDate.Text != "" && txtToDate.Text != "")
        //{
            SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("vt_SCGL_Sp_SalesInvoiceReport2", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IsActive", ddl_Status.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@DateFrom", txtFromDate.Text);
            cmd.Parameters.AddWithValue("@DateTo", txtToDate.Text);
            cmd.Parameters.AddWithValue("@CostCenterID", ddlCostCenter.SelectedValue);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(ds);
            ViewState["COA"] = ds;
            SetReport();
            ds = ViewState["COA"] as DataSet;
            con.Close();
        //}
        return ds;

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
    //protected void CrystalReportViewer1_Load(object sender, EventArgs e)
    //{
    //    if (!IsPostBack)
    //    {
    //        CrystalReportViewer1.ReportSource = rd;
    //        CrystalReportViewer1.DataBind();
    //    }
    //}
    protected void CrystalReportViewer1_Init(object sender, EventArgs e)
    {
        ConfigureCrystalReports();
    }


    private void SetReport()
    {
        if (ViewState["COA"] != null)
        {
            DataSet ds;
            DataTable dt = new DataTable("SalesInvoiceReport");
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
                        dr["IsActive"] = "Active";
                    }
                    else
                    {
                        dr["IsActive"] = "InActive";
                    }
                }
                ViewState["ID"] = null;
            }
            foreach (DataRow dr in dt.Rows)
            {
                dr["CompanyName"] = SBO.SiteName;
                dr["VoucherTypeName"] = "Sales Invoice Report";
            }
            ds.Tables[0].Clear();
            ds.Tables[0].Merge(dt);
            ds.Tables[0].TableName = "SalesInvoiceReport";
            ViewState["COA"] = ds;

        }
    }
    protected void btn_Search_Click(object sender, EventArgs e)
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
    protected void lnkConYes_Click(object sender, EventArgs e)
    {
        int Copies = Convert.ToInt32(TextCopies.Text == "" ? "1" : TextCopies.Text);
        int GivenSPages = Convert.ToInt32(TextStartPages.Text == "" ? "0" : TextStartPages.Text);
        int GivenEPages = Convert.ToInt32(TextEndpages.Text == "" ? "0" : TextEndpages.Text);
        if (GivenEPages != null)
        {
            ConfigureCrystalReports();
            rd.PrintToPrinter(Copies, true, GivenSPages, GivenEPages);
            JQ.closeDialog(this, "ControlConfirmation");
            JQ.showDialog(this, "Confirmation");
            lblDeleteMsg.Text = "Sales Invoice Report Print Successfully ! ";
        }
        else
        {
            JQ.showDialog(this, "Confirmation");
            lblDeleteMsg.Text = "Pages Range Not Valid  ! rd";
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
