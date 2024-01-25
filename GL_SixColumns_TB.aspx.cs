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
using System.Globalization;
using System.Drawing.Printing;
using SW.SW_Common;

public partial class GL_SixColumns_TB : System.Web.UI.Page
{
    public static DataTable dt = new DataTable();
    ReportDocument transactionReport = new ReportDocument();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionBO"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (IsPostBack)
        {
            ConfigureCrystalReports();
            //JQ.DatePicker(this);
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "GL_SixColumns_TB.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "GL_SixColumns_TB.aspx" && view == true)
                {
                    FillVoucherTypeList();
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
    }
    public void Reload_JS()
    {
        SCGL_Common.ReloadJS(this, "ChangeDateFrom();");
        SCGL_Common.ReloadJS(this, "ChangeDateTo();");
        SCGL_Common.ReloadJS(this, "MyDate();");
    }
    #region Methods
      private void FillVoucherTypeList()
    {
        //GLBLL GL = new GLBLL();
        //cmbVoucherType.DataSource = GL.GetVoucherType();
        //cmbVoucherType.DataTextField = "VoucherTypeName";
        //cmbVoucherType.DataValueField = "VoucherTypeID";
        //cmbVoucherType.DataBind();
    }
    #endregion
    private void ConfigureCrystalReports()
    {
        if (txtDateFrom.Text != "" && txtDateTo.Text != "")
        {
            string reportPath = Server.MapPath("GL_Report\\Six_Columns_TB.rpt");
            transactionReport.Load(reportPath);
            SqlConnectionStringBuilder conf = new SqlConnectionStringBuilder(SCGL_Common.ConnectionString);            
            ds = getreport();
            SetParameterInReport();
            transactionReport.SetDataSource(ds);
            transactionReport.SetDatabaseLogon(conf.UserID, conf.Password, conf.DataSource, conf.InitialCatalog);
            transactionReport.VerifyDatabase();
            CrystalReportViewer1.ReportSource = transactionReport;
            CrystalReportViewer1.DataBind();
            btnPrintJava.Visible = true;
            CrystalReportViewer1.HasPrintButton = false;
        }
    }
    private DataSet getreport()
    {
        DataSet ds = new DataSet();
        if (txtDateFrom.Text != "" && txtDateTo.Text != "")
        {
            SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("vt_SCGL_SPSixColumnReportNew", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DateFrom", DateTime.ParseExact(txtDateFrom.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture));
            cmd.Parameters.Add("@DateTo", DateTime.ParseExact(txtDateTo.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture));
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(ds);
            ViewState["Report"] = ds;
            ds = ViewState["Report"] as DataSet;
            con.Close();
        }
        return ds;
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_View == true)
        {
            System.Threading.Thread.Sleep(1300);
            ConfigureCrystalReports();
            JQ.DatePicker(this);
        }
        else
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to View Record");
            CrystalReportViewer1.Visible = false;
        }
    }
    protected void CrystalReportViewer1_Navigate1(object source, CrystalDecisions.Web.NavigateEventArgs e)
    {
        ConfigureCrystalReports();
    }
    protected void CrystalReportViewer1_Init(object sender, EventArgs e)
    {

    }
    private void SetParameterInReport()
    {
        SCGL_Session Sbo = (SCGL_Session)Session["SessionBO"];
        ds.Tables[0].Columns.Add("SiteName");
        ds.Tables[0].Columns.Add("DateFrom");
        ds.Tables[0].Columns.Add("DateTo");
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ds.Tables[0].Rows[i]["SiteName"] = Sbo.SiteName;
            ds.Tables[0].Rows[i]["DateFrom"] = txtDateFrom.Text;
            ds.Tables[0].Rows[i]["DateTo"] = txtDateTo.Text;
        }
    }
    protected void btnPrintJava_Click(object sender, EventArgs e)
    {
        JQ.showDialog(this, "ControlConfirmation");
    }
    protected void lnkConYes_Click(object sender, EventArgs e)
    {
        int Copies = Convert.ToInt32(TextCopies.Text == "" ? "1" : TextCopies.Text);
        int GivenSPages = Convert.ToInt32(TextStartPages.Text == "" ? "0" : TextStartPages.Text);
        int GivenEPages = Convert.ToInt32(TextEndpages.Text == "" ? "0" : TextEndpages.Text);
        if (GivenEPages != null)
        {
            //ConfigureCrystalReports();
            transactionReport.PrintToPrinter(Copies, true, GivenSPages, GivenSPages);
            JQ.closeDialog(this, "ControlConfirmation");
            JQ.showDialog(this, "Confirmation");
            lblDeleteMsg.Text = "GL SixColumns T/B Print Successfully ! ";
        }
        else
        {
            JQ.showDialog(this, "Confirmation");
            lblDeleteMsg.Text = "Pages Range Not Valid  ! ";
        }
    }
}
