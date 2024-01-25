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

public partial class ExpenseSheet : System.Web.UI.Page
{
    JobSheet_BAL JSB = new JobSheet_BAL();
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "ExpenseSheet.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "ExpenseSheet.aspx" && view == true)
                {
                    
                    ConfigCrystalReport();
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
        ConfigCrystalReport();
    }
    public void Reload_JS()
    {
        SCGL_Common.ReloadJS(this, "ChangeDate();");
        SCGL_Common.ReloadJS(this, "MyDate();");
    }

   
    private void ConfigCrystalReport()
    {
        if (txtJobNo.Text != "")
        {
            string reportPath = Server.MapPath("GL_Report\\Expense_Sheet.rpt");
            rd.Load(reportPath);
            //DataTable dt = new DataTable();
            DataSet dt = new DataSet();
            rd.SetDataSource(getreport());
            if (!IsPostBack)
            {

                CrystalReportViewer1.Visible = false;
            }
            else
            {
                CrystalReportViewer1.Visible = true;
            }

            rd.SetDatabaseLogon(conf.UserID, conf.Password, conf.DataSource, conf.InitialCatalog);
            rd.VerifyDatabase();
            CrystalReportViewer1.HasPrintButton = false;
            btnPrintJava.Style.Add("display", "block");


        }
    }
    private DataTable getreport()
    {
        DataSet ds = new DataSet();
        if (txtJobNo.Text != "")
        {
            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString);
            con.Open();
            //SqlCommand cmd = new SqlCommand("vt_SCGL_Sp_SubsidaryTrialBalanceReport", con);
            SqlCommand cmd = new SqlCommand("vt_SCGL_rptExpenseSheet", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@AccountCode", "");
            //cmd.Parameters.AddWithValue("@DateFrom", "");
            //cmd.Parameters.AddWithValue("@DateTo", "");
            //cmd.Parameters.AddWithValue("@YearID", SBO.FinYearID);
            //cmd.Parameters.AddWithValue("@DateFrom", lblJobNo.Text);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(ds);
            ViewState["Report"] = ds;
            ds = ViewState["Report"] as DataSet;
            DataTable dt;
            dt = ds.Tables[0].Copy();
            dt.Columns.Add("CompanyName");
            dt.Columns.Add("ReportName");
            dt.Columns.Add("Fromreport");
            dt.Columns.Add("ToReport");

            foreach (DataRow dr in dt.Rows)
            {
                dr["CompanyName"] = SBO.SiteName;
                dr["ReportName"] = "Profit & Loss Statement";
                dr["FromReport"] = "For The Period From";
                dr["ToReport"] = "To";
               
            }
            ds.Tables[0].Clear();
            ds.Tables[0].Merge(dt);
            con.Close();
        }
        return ds.Tables[0];
    }

//GLGeneralVoucher_BAL GGV = new GLGeneralVoucher_BAL();
//            string reportPath = Server.MapPath("GL_Report\\GL_Subsidary.rpt");
//            rd.Load(reportPath);
//            DataSet dtOrg = new DataSet();
//            DataSet ds = new DataSet();
//            DataSet dtNew = new DataSet();
//            dtOrg = getreport();
//            dtNew.Merge(dtOrg.Tables[0]);

//            rd.SetDataSource(dtNew);

//            rd.SetDatabaseLogon(conf.UserID, conf.Password, conf.DataSource, conf.InitialCatalog);

//            rd.VerifyDatabase();
//            CrystalReportViewer1.ReportSource = rd;
//            CrystalReportViewer1.DataBind();
//            if (!IsPostBack)
//            {
//                CrystalReportViewer1.Visible = false;
//                btnPrintJava.Visible = false;
//            }
//            else
//            {
//                CrystalReportViewer1.Visible = true;
//                btnPrintJava.Visible = true;
//            }

//            CrystalReportViewer1.HasPrintButton = false;
//        }
    //private DataSet getreport()
    //{
    //    DataSet ds = new DataSet();
    //    SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString);
    //    if (con.State != ConnectionState.Open)
    //    {
    //        con.Open();
    //        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
    //        SqlCommand cmd = new SqlCommand("vt_SCGL_Sp_SubsidaryTrialBalanceReport", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@AccountCode", "");
    //        cmd.Parameters.AddWithValue("@DateFrom", "");
    //        cmd.Parameters.AddWithValue("@DateTo", "");
    //        cmd.Parameters.AddWithValue("@YearID", SBO.FinYearID);
    //        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
    //        adpt.Fill(ds);
    //    }
    //    else { con.Close(); }
    //    return ds;
    //}

    //private void SetParameterInReport()
    //{
    //    SCGL_Session Sbo = (SCGL_Session)Session["SessionBO"];
    //    ds.Tables[0].Columns.Add("SiteName");
    //    ds.Tables[0].Columns.Add("@YearFrom");

    //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //    {
    //        ds.Tables[0].Rows[i]["SiteName"] = Sbo.SiteName;
    //        ds.Tables[0].Rows[i]["YearFrom"] = hdnMinDate.Value;
    //    }
    //}

    //protected void CheckBoxBalance_CheckedChanged(object sender, EventArgs e)
    //{
    //    ConfigCrystalReport();
    //}
    
    //protected void LinkButton1_Click(object sender, EventArgs e)
    //{
    //    CrystalReportViewer1.ReportSource = rd;
    //    CrystalReportViewer1.DataBind();
    //    JQ.RecallJS(this, "DateTime();");
    //}
    //protected void LinkButtonSearch_Click1(object sender, EventArgs e)
    //{
    //    SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
    //    DataTable ds = new DataTable();
    //    if (SBO.Can_View == true)
    //    {

    //        ds = getreport();
    //        if (ds.Rows.Count > 0)
    //        {
    //            ConfigCrystalReport();
    //            CrystalReportViewer1.Visible = true;
    //        }
    //        else
    //        {
    //            JQ.showStatusMsg(this, "2", "No Record Found");
    //            CrystalReportViewer1.Visible = false;
    //            btnPrintJava.Visible = false;
    //        }
    //    }
    //    else
    //    {
    //        JQ.showStatusMsg(this, "3", "User not Allowed to View Record");
    //        CrystalReportViewer1.Visible = false;
    //    }

    //}
    protected void Button1_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        
        if (SBO.Can_View == true)
        {
            DataTable dt = getreport().Copy();
            if (dt.Rows.Count > 0)
            {
                ConfigCrystalReport();
                CrystalReportViewer1.Visible = true;
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
            JQ.showStatusMsg(this, "3", "User not Allowed to View Record");
            CrystalReportViewer1.Visible = false;
        }
    }

    protected void CrystalReportViewer1_Navigate(object source, CrystalDecisions.Web.NavigateEventArgs e)
    {
        ConfigCrystalReport();
        //CrystalReportViewer1.ReportSource = rd;
        //CrystalReportViewer1.DataBind();
    }
    protected void CrystalReportViewer1_Init(object sender, EventArgs e)
    {
        ConfigCrystalReport();
        //CrystalReportViewer1.ReportSource = rd;
        //CrystalReportViewer1.DataBind();
    }

    protected void CrystalReportViewer1_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CrystalReportViewer1.ReportSource = rd;
            CrystalReportViewer1.DataBind();
        }
    }

    protected void btnPrintJava_Click(object sender, EventArgs e)
    {
        JQ.showDialog(this, "ControlConfirmation");
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
    protected void btnFindAcc_Click(object sender, EventArgs e)
    {
        
        // GrdAccounts.DataSource = JSB.GetAccountName(txtAccountNo.Text);
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        //DataTable dts = JSB.GetYear_Account(SBO.FinYearID);
        DataTable dt = JSB.GetJobNumber(txtJobNo2.Text);
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
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        GrdAccounts.DataSource = JSB.GetJobNumber(txtJobNo2.Text);
        GrdAccounts.PageIndex = e.NewPageIndex;
        GrdAccounts.DataBind();
    }
    protected void lnkbtnfind2_Click(object sender, EventArgs e)
    {
        HdnFindCode.Value = "0";
        JQ.showDialog(this, "FindAccount");
        CrystalReportViewer1.Visible = false;
    }
}
