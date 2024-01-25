using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using CrystalDecisions.Shared;
using System.Management;
using SW.SW_Common;

public partial class ExpenseSheetReport : System.Web.UI.Page
{
    public static string strUser;
    ReportDocument rd = new ReportDocument();
    DataSet ds = new DataSet();
    SqlConnectionStringBuilder conf = new SqlConnectionStringBuilder(SCGL_Common.ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionBO"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        //else
        //{
        //    JQ.RecallJS(this.Page, "ReloadJQ();");
        //}
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "ExpenseSheetReport.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "ExpenseSheetReport.aspx" && view == true)
                {
                    //ConfigCrystalReport();
                    CrystalReportViewer1.Visible = false;
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }

        }
        Reload_JS();
        Invoice_BAL BALInvoice = new Invoice_BAL();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataTable dt = PM.getFinancialYearByID(SBO.FinYearID);
        hdnMinDate.Value = SCGL_Common.CheckDateTime(dt.Rows[0]["yearFrom"]).ToShortDateString();
        hdnMaxDate.Value = SCGL_Common.CheckDateTime(dt.Rows[0]["YearTo"]).ToShortDateString();
        //ConfigCrystalReport();
    }
    public void Reload_JS()
    {
        SCGL_Common.ReloadJS(this, "ChangeDateFrom();");
        SCGL_Common.ReloadJS(this, "ChangeDateTo();");
        SCGL_Common.ReloadJS(this, "MyDate();");
        SCGL_Common.ReloadJS(this, "ReloadJQ();");
    }
    private void ConfigCrystalReport()
    {

        if (txtJobNo.Text != "")
        {
            string reportPath = Server.MapPath("GL_Report\\Expense_Sheet.rpt");
            rd.Load(reportPath);
            rd.SetDataSource(getreport());
            CrystalReportViewer1.Visible = true;
            rd.SetDatabaseLogon(conf.UserID, conf.Password, conf.DataSource, conf.InitialCatalog);
            rd.VerifyDatabase();
            CrystalReportViewer1.HasPrintButton = true;
            rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Expense Sheet");
            //btnPrintJava.Style.Add("display", "block");
        }
    }
    private DataTable getreport()
    {
        DataSet ds = new DataSet();
        if (txtJobNo.Text != "")
        {
            SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("vt_SCGL_rptExpenseSheet", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            cmd.Parameters.AddWithValue("@JobNumber", txtJobNo.Text);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(ds);
            ViewState["Report"] = ds;
            ds = ViewState["Report"] as DataSet;
            DataTable dt;
            dt = ds.Tables[0].Copy();
            dt.Columns.Add("CompanyName");
            dt.Columns.Add("ReportName");
            dt.Columns.Add("AsOnDate");
            foreach (DataRow dr in dt.Rows)
            {
                dr["CompanyName"] = SBO.SiteName;
                dr["ReportName"] = "Trial Balance";
                dr["AsOnDate"] = "As On Date";

            }
            ds.Tables[0].Clear();
            ds.Tables[0].Merge(dt);
            con.Close();
        }
        return ds.Tables[0];
    }


    protected void CrystalReportViewer1_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CrystalReportViewer1.ReportSource = rd;
            CrystalReportViewer1.DataBind();
        }
    }

    protected void CrystalReportViewer1_Navigate(object source, CrystalDecisions.Web.NavigateEventArgs e)
    {
        ConfigCrystalReport();
    }
    protected void CrystalReportViewer1_Init(object sender, EventArgs e)
    {

        CrystalReportViewer1.ReportSource = rd;
        CrystalReportViewer1.DataBind();
    }
    //private void SetReport()
    //{
    //    SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
    //    if (ViewState["TB"] != null)
    //    {
    //        DataSet ds;
    //        DataTable dt;
    //        ds = ViewState["TB"] as DataSet;
    //        dt = ds.Tables[0].Copy();
    //        dt.Columns.Add("CompanyName");
    //        dt.Columns.Add("AsOf");
    //        foreach (DataRow dr in dt.Rows)
    //        {
    //            dr["CompanyName"] = SBO.SiteName;
    //            dr["AsOf"] = txt_Date.Text;
    //        }

    //        ds.Tables[0].Clear();
    //        ds.Tables[0].Merge(dt);
    //        ViewState["TB"] = ds;

    //    }
    //}
    private void SetParameterInReport()
    {
        SCGL_Session Sbo = (SCGL_Session)Session["SessionBO"];
        // ds.Tables[0].Columns.Add("SiteName");
        ds.Tables[0].Columns.Add("@YearFrom");
  
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            //  ds.Tables[0].Rows[i]["SiteName"] = Sbo.SiteName;
            ds.Tables[0].Rows[i]["YearFrom"] = hdnMinDate.Value;
      
        }
    }
    protected void CheckBoxBalance_CheckedChanged(object sender, EventArgs e)
    {
        ConfigCrystalReport();
    }

    protected void lnkConYes_Click(object sender, EventArgs e)
    {
        int Copies = Convert.ToInt32(TextCopies.Text == "" ? "1" : TextCopies.Text);
        int GivenSPages = Convert.ToInt32(TextStartPages.Text == "" ? "0" : TextStartPages.Text);
        int GivenEPages = Convert.ToInt32(TextEndpages.Text == "" ? "0" : TextEndpages.Text);
        if (GivenEPages != null)
        {
            ConfigCrystalReport();
            rd.PrintToPrinter(Copies, true, GivenSPages, GivenEPages);
            JQ.closeDialog(this, "ControlConfirmation");
            JQ.showDialog(this, "Confirmation");
            lblDeleteMsg.Text = "Expense Sheet Print Successfully ! ";
        }
        else
        {
            JQ.showDialog(this, "Confirmation");
            lblDeleteMsg.Text = "Pages Range Not Valid  ! ";
        }

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        CrystalReportViewer1.ReportSource = rd;
        CrystalReportViewer1.DataBind();
        JQ.RecallJS(this, "DateTime();");
    }
    protected void LinkButtonSearch_Click1(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataTable dt = new DataTable();
        if (SBO.Can_View == true)
        {
            dt = getreport();
            if (dt.Rows.Count == 0)
            {
                JQ.showStatusMsg(this, "2", "No Record Found");
                CrystalReportViewer1.Visible = false;
            }
            else
            {
                ConfigCrystalReport();
                CrystalReportViewer1.Visible = true;
                JQ.DatePicker(this);
            }
        }
        else
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to View Record");
            CrystalReportViewer1.Visible = false;
        }
        JQ.DatePicker(this);
    }
    //protected void DropdownlistDetails_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (txt_Date.Text != "")
    //    {
    //        ConfigCrystalReport();
    //    }
    //    JQ.DatePicker(this);
    //}
    protected void btnPrintJava_Click(object sender, EventArgs e)
    {
        ConfigCrystalReport();
        JQ.showDialog(this, "ControlConfirmation");
    }

    protected void lnkbtnfind2_Click(object sender, EventArgs e)
    {
        HdnFindCode.Value = "0";
        JQ.showDialog(this, "FindAccount");
        CrystalReportViewer1.Visible = false;
    }

    protected void btnFindAcc_Click(object sender, EventArgs e)
    {
        JobSheet_BAL GGV = new JobSheet_BAL();
        // GrdAccounts.DataSource = GGV.GetAccountName(txtJobNoSearch.Text);
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataTable dt = GGV.GetJobNumber(txtJobNoSearch.Text);
        GrdAccounts.DataSource = dt;
        if (dt.Rows.Count > 0)
        {
            GrdAccounts.DataBind();
        }
        else
        {
            GrdAccounts.DataSource = null;
            GrdAccounts.DataBind();
            JQ.showDialog(this, "Confirmation");
        }
    }
    protected void lnkSelect_Click(object sender, EventArgs e)
    {
        var row = (GridViewRow)((System.Web.UI.Control)sender).NamingContainer;
        var rowIndex = row.RowIndex;
        if (HdnFindCode.Value == "0")
        {
            txtJobNo.Text = GrdAccounts.Rows[rowIndex].Cells[1].Text;

            //txtCodelbl.Text = GrdAccounts.Rows[rowIndex].Cells[2].Text.ToString();
            //titlecode.Value = GrdAccounts.Rows[rowIndex].Cells[2].Text.ToString();
            //txtBalance.Text = GrdAccounts.Rows[rowIndex].Cells[3].Text.ToString();
            //txtBalanceHidden.Value = GrdAccounts.Rows[rowIndex].Cells[3].Text.ToString();
        }
        else
        {

        }
        JQ.closeDialog(this, "FindAccount");
    }


    protected void GrdAccounts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        JobSheet_BAL GGV = new JobSheet_BAL();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        GrdAccounts.DataSource = GGV.GetJobNumber(txtJobNoSearch.Text);
        GrdAccounts.PageIndex = e.NewPageIndex;
        GrdAccounts.DataBind();
    }

    protected void txtJobNo_TextChanged(object sender, EventArgs e)
    {
        ExpenseSheet_BAL BALInvoice = new ExpenseSheet_BAL();
        int CountJobNo = BALInvoice.CheckJobNo(txtJobNo.Text);
        if (CountJobNo > 0)
        { }
        else
        {
            JQ.showStatusMsg(this, "2", "Job Number Does not Exist in the records");
            txtJobNo.Text = "";
        }

    }
}
