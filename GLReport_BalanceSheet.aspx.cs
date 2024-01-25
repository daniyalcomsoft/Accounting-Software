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
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using SW.SW_Common;



public partial class GLReport_BalanceSheet : System.Web.UI.Page
{

    ReportDocument rd = new ReportDocument();
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "GLReport_BalanceSheet.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "GLReport_BalanceSheet.aspx" && view == true)
                {
                    SetReport();
                    CrystalReportViewer1.Visible = false;
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
            
        }
        JQ.RecallJS(this, "DateTime();");

    }
    private void ConfigReport()
    {
        string reportPath = Server.MapPath("GL_Report\\GL_BalanceSheet.rpt");
        rd.Load(reportPath);
        rd.SetDataSource(getreport());
        rd.SetDatabaseLogon(conf.UserID, conf.Password, conf.DataSource, conf.InitialCatalog);
        rd.VerifyDatabase();
        btnPrint.Visible = true;
        CrystalReportViewer1.HasPrintButton = false;
    }
    private DataSet getreport()
    {
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("vt_SCGL_SPGetBalanceSheet", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        adpt.Fill(ds);
        ViewState["BS"] = ds;
        SetReport();
        ds = ViewState["BS"] as DataSet;
        con.Close();
        string str = ds.GetXmlSchema();
        return ds;
    }


    protected void lbtnViewReport_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_View == true)
        {
            ConfigReport();
            CrystalReportViewer1.ReportSource = rd;
            CrystalReportViewer1.DataBind();
            CrystalReportViewer1.Visible = true;
            btnPrint.Visible = true;
        }
        else
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to View Record");
            CrystalReportViewer1.Visible = false;
        }
    }
    protected void CrystalReportViewer1_Navigate(object source, CrystalDecisions.Web.NavigateEventArgs e)
    {
        ConfigReport();
        CrystalReportViewer1.ReportSource = rd;
        CrystalReportViewer1.DataBind();
    }
    protected void CrystalReportViewer1_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CrystalReportViewer1.ReportSource = rd;
            CrystalReportViewer1.DataBind();
        }
    }
    private void SetReport()
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (ViewState["BS"] != null)
        {
            DataSet ds;
            DataTable dt;
            ds = ViewState["BS"] as DataSet;
            dt = ds.Tables[0].Copy();
            dt.Columns.Add("CompanyName");
            dt.Columns.Add("VoucherTypeName");
            foreach (DataRow dr in dt.Rows)
            {
                dr["CompanyName"] = SBO.SiteName;
                dr["VoucherTypeName"] = "Balance Sheet";
            }
            ds.Tables[0].Clear();
            ds.Tables[0].Merge(dt);
            ViewState["BS"] = ds;
            ds = ViewState["TB"] as DataSet;
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ConfigReport();
        JQ.showDialog(this, "ControlConfirmation");
    }
    protected void lnkConYes_Click(object sender, EventArgs e)
    {
        int Copies = Convert.ToInt32(TextCopies.Text == "" ? "1" : TextCopies.Text);
        int GivenSPages = Convert.ToInt32(TextStartPages.Text == "" ? "0" : TextStartPages.Text);
        int GivenEPages = Convert.ToInt32(TextEndpages.Text == "" ? "0" : TextEndpages.Text);
        if (GivenEPages != null)
        {
            ConfigReport();
            rd.PrintToPrinter(Copies, true, GivenSPages, GivenSPages);
            JQ.closeDialog(this, "ControlConfirmation");
            JQ.showDialog(this, "Confirmation");
            lblDeleteMsg.Text = "Balance sheet Print Successfully ! ";
        }
        else
        {
            JQ.showDialog(this, "Confirmation");
            lblDeleteMsg.Text = "Pages Range Not Valid  ! ";
        }
    }
}