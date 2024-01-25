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
using SW.SW_Common;

public partial class GL_Subsidiary : System.Web.UI.Page
{
    ReportDocument rd = new ReportDocument();
    GLSubsidiary_BAL GLSubs = new GLSubsidiary_BAL();
    SqlConnectionStringBuilder conf = new SqlConnectionStringBuilder(SCGL_Common.ConnectionString);
    SCGL_Session Sbo;
    string status = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionBO"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
             Sbo = (SCGL_Session)Session["SessionBO"];
             JQ.RecallJS(this.Page, "ReloadJQ();");
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "GL_Subsidiary.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "GL_Subsidiary.aspx" && view == true)
                {
                    //configCrystalReport();
                    //CrystalReportViewer1.Visible = false;
                    //btnPrintJava.Visible = false;
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
        status = "Load";
        configCrystalReport();
        
    }
    public void Reload_JS()
    {
        SCGL_Common.ReloadJS(this, "ChangeDateFrom();");
        SCGL_Common.ReloadJS(this, "ChangeDateTo();");
        SCGL_Common.ReloadJS(this, "MyDate();");
        SCGL_Common.ReloadJS(this, "ReloadJQ();");
    }
    private void configCrystalReport()
    {
        double final = 0;
        GLGeneralVoucher_BAL GGV = new GLGeneralVoucher_BAL();
        string reportPath = Server.MapPath("GL_Report\\GL_Subsidary.rpt");
        rd.Load(reportPath);
        DataSet dtOrg = new DataSet();
        DataSet ds = new DataSet();
        DataSet dtNew = new DataSet();
        dtOrg = getreport();
        dtNew.Merge(dtOrg.Tables[0]);

        dtNew.Tables[0].Columns.Add("SiteName");
      //  dtNew.Tables[0].Columns.Add("ReportName");
        dtNew.Tables[0].Columns.Add("DateFrom");
        dtNew.Tables[0].Columns.Add("DateTo");
        double OpeningBalance = 0;
        double Debit = 0;
        double Credit = 0;
        double CurrentDebit = 0;
        double CalculatedBalance = 0;
        int AccountNature = GGV.getAccountNature(SCGL_Common.Convert_ToInt(txtCode.Text));

        //if (status == "Search") 
        //{
            for (int i = 0; i < dtNew.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    OpeningBalance = Convert.ToDouble(dtNew.Tables[0].Rows[0]["OpeningBalance"].ToString() == "" ? 0 : dtNew.Tables[0].Rows[0]["OpeningBalance"]);
                }
                else
                {
                    OpeningBalance = 0;
                }
                CurrentDebit = Convert.ToDouble(dtNew.Tables[0].Rows[i]["Debit"].ToString() == "" ? 0 : dtNew.Tables[0].Rows[i]["Debit"]);
                if (AccountNature == 4 || AccountNature == 5 || AccountNature == 6 || AccountNature == 7 || AccountNature == 9)
                {
                    Debit = Convert.ToDouble(dtNew.Tables[0].Rows[i]["Debit"].ToString() == "" ? 0 : dtNew.Tables[0].Rows[i]["Debit"]);
                    //Credit = Convert.ToDouble(dtNew.Tables[0].Rows[i]["Credit"].ToString() == "" ? 0 : dtNew.Tables[0].Rows[i]["Credit"]) - OpeningBalance;

                    Credit = OpeningBalance - Convert.ToDouble(dtNew.Tables[0].Rows[i]["Credit"]);

                    // if (Credit < 0)
                    //{
                    final += Credit;
                    final = final + Debit;

                    //else 
                    //{
                    //    final += Credit;
                    //    final = final - Debit;
                    //}

                }
                else
                {
                    Debit = Convert.ToDouble(dtNew.Tables[0].Rows[i]["Debit"].ToString() == "" ? 0 : dtNew.Tables[0].Rows[i]["Debit"]) + OpeningBalance;
                    Credit = Convert.ToDouble(dtNew.Tables[0].Rows[i]["Credit"].ToString() == "" ? 0 : dtNew.Tables[0].Rows[i]["Credit"]);
                    final += Debit;
                    final = final - Credit;
                }

                dtNew.Tables[0].Rows[i]["CalculatedBalance"] = Convert.ToDouble(final);
                //CalculatedBalance = Convert.ToDouble(dtNew.Tables[0].Rows[i]["CalculatedBalance"]);
                dtNew.Tables[0].Rows[i]["LastRows"] = dtNew.Tables[0].Rows[dtNew.Tables[0].Rows.Count - 1]["CalculatedBalance"];
                dtNew.Tables[0].Rows[i]["SiteName"] = Sbo.SiteName;
                // dtNew.Tables[0].Rows[i]["ReportName"] = "General Ledger";
                dtNew.Tables[0].Rows[i]["DateFrom"] = txtDateFrom.Text;
                DateTime getDate = Convert.ToDateTime(txtDateTo.Text);
                dtNew.Tables[0].Rows[i]["DateTo"] = getDate.Day + "." + getDate.Month + "." + getDate.Year;
            }
        //}
        
        rd.SetDataSource(dtNew);

        rd.SetDatabaseLogon(conf.UserID, conf.Password, conf.DataSource, conf.InitialCatalog);

        rd.VerifyDatabase();
        CrystalReportViewer1.ReportSource = rd;
        CrystalReportViewer1.DataBind();
        if (!IsPostBack)
        {
            CrystalReportViewer1.Visible = false;
            btnPrintJava.Visible = false;
        }
        else 
        {
            CrystalReportViewer1.Visible = true;
            btnPrintJava.Visible = false;
        }
        
        CrystalReportViewer1.HasPrintButton = true;
    }
    private DataSet getreport()
    {
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString);
        if (con.State != ConnectionState.Open)
        {
            con.Open();
            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            SqlCommand cmd = new SqlCommand("vt_SCGL_Sp_SubsidaryTrialBalanceReport", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AccountCode", txtCode.Text);
            cmd.Parameters.AddWithValue("@DateFrom",txtDateFrom.Text );
            cmd.Parameters.AddWithValue("@DateTo", txtDateTo.Text );
            cmd.Parameters.AddWithValue("@YearID", SBO.FinYearID);
            
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(ds);
        }
        else { con.Close(); }
        string str = ds.GetXmlSchema();
        return ds;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataSet ds = new DataSet();
        if (SBO.Can_View == true)
        {

            ds = getreport();
            if (ds.Tables[0].Rows.Count > 0)
            {
                status = "Search";
                configCrystalReport();
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
 
    //protected void CrystalReportViewer1_Navigate(object source, CrystalDecisions.Web.NavigateEventArgs e)
    //{
    //    configCrystalReport();
    //    //CrystalReportViewer1.ReportSource = rd;
    //    //CrystalReportViewer1.DataBind();
    //}
    //protected void CrystalReportViewer1_Init(object sender, EventArgs e)
    //{
    //    configCrystalReport();
    //    //CrystalReportViewer1.ReportSource = rd;
    //    //CrystalReportViewer1.DataBind();
    //}

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
        configCrystalReport();
    }
    protected void lnkConYes_Click(object sender, EventArgs e)
    {

        int Copies = Convert.ToInt32(TextCopies.Text == "" ? "1" : TextCopies.Text);
        int GivenSPages = Convert.ToInt32(TextStartPages.Text == "" ? "0" : TextStartPages.Text);
        int GivenEPages = Convert.ToInt32(TextEndpages.Text == "" ? "0" : TextEndpages.Text);
        if (GivenEPages != null)
        {
            configCrystalReport();
            rd.PrintToPrinter(Copies, true, GivenSPages, GivenEPages);
            JQ.closeDialog(this, "ControlConfirmation");
            JQ.showDialog(this, "Confirmation");
            lblDeleteMsg.Text = "GL Subsidiary Report Print Successfully ! ";
        }
        else
        {
            JQ.showDialog(this, "Confirmation");
            lblDeleteMsg.Text = "Pages Range Not Valid  ! ";
        }
    }
    protected void btnFindAcc_Click(object sender, EventArgs e)
    {
        GLGeneralVoucher_BAL GGV = new GLGeneralVoucher_BAL();
        // GrdAccounts.DataSource = GGV.GetAccountName(txtAccountNo.Text);
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataTable dts = GGV.GetYear_Account(SBO.FinYearID);
        DataTable dt = GGV.GetAccountName(txtAccountNo.Text, SBO.FinYearID, dts.Rows[0]["YearFrom"].ToString(), dts.Rows[0]["YearTo"].ToString());
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
            txtCode.Text = GrdAccounts.Rows[rowIndex].Cells[1].Text;
          
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
        GLGeneralVoucher_BAL GGV = new GLGeneralVoucher_BAL();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataTable dts = GGV.GetYear_Account(SBO.FinYearID);
        GrdAccounts.DataSource = GGV.GetAccountName(txtAccountNo.Text, SBO.FinYearID, dts.Rows[0]["YearFrom"].ToString(), dts.Rows[0]["YearTo"].ToString());
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
