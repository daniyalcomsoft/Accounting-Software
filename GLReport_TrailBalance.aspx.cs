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



public partial class testing : System.Web.UI.Page
{
    public static string strUser;
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "testing.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "testing.aspx" && view == true)
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
    }
    public void Reload_JS()
    {
        SCGL_Common.ReloadJS(this, "ChangeDate();");
        SCGL_Common.ReloadJS(this, "MyDate();");    
        
    }
    private void ConfigCrystalReport()
    {
        
        if (txt_Date.Text != "")
        {
            string reportPath = Server.MapPath("GL_Report\\GLReport_TB.rpt");
            rd.Load(reportPath);
            DataTable dt = new DataTable();
            dt.Columns.Add("CompanyName");
            dt.Columns.Add("VoucherType");
            dt.Columns.Add("AccountNature");
            rd.SetDataSource(getreport());
            CrystalReportViewer1.Visible = true;
            rd.SetDatabaseLogon(conf.UserID, conf.Password, conf.DataSource, conf.InitialCatalog);
            rd.VerifyDatabase();
            CrystalReportViewer1.HasPrintButton = true;
            //btnPrintJava.Style.Add("display", "block");
            //JQ.RecallJS(this, "DateTime();");
        }
        //JQ.DatePicker(this);
    }
    private DataSet getreport()
    {
        DataSet ds = new DataSet();
        if (txt_Date.Text != "")
        {
            SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("vt_SCGL_SPGetTrialBalanceTree", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Details", DropdownlistDetails.SelectedItem.Value );
            cmd.Parameters.AddWithValue("@Date", txt_Date.Text);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(ds);
            ViewState["TB"] = ds;
            SetReport();
            ds = ViewState["TB"] as DataSet;
            con.Close();
        }
       
        return ds;
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
    private void SetReport()
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (ViewState["TB"] != null)
        {
            DataSet ds;
            DataTable dt;
            ds = ViewState["TB"] as DataSet;
            dt = ds.Tables[0].Copy();
            dt.Columns.Add("CompanyName");
            dt.Columns.Add("SelectedDate");
            dt.Columns.Add("VoucherTypeName");
            foreach (DataRow dr in dt.Rows)
            {
                dr["CompanyName"] = SBO.SiteName;
                dr["SelectedDate"] = txt_Date.Text;
                dr["VoucherTypeName"] = "Trial Balance";
            }           
            
            ds.Tables[0].Clear();
            ds.Tables[0].Merge(dt);
            ViewState["TB"] = ds;
            
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
            rd.PrintToPrinter(Copies, true, GivenSPages, GivenSPages);
            JQ.closeDialog(this, "ControlConfirmation");
            JQ.showDialog(this, "Confirmation");
            lblDeleteMsg.Text = "GL Report Print Successfully ! ";
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
        if (SBO.Can_View == true)
        {
            ConfigCrystalReport();
        }
        else
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to View Record");
            CrystalReportViewer1.Visible = false;
        }
        JQ.DatePicker(this);
    }
    protected void DropdownlistDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txt_Date.Text != "")
        {
            ConfigCrystalReport();
        }
        JQ.DatePicker(this);
    }
    protected void btnPrintJava_Click(object sender, EventArgs e)
    {
        ConfigCrystalReport();
        JQ.showDialog(this, "ControlConfirmation");
    }
}
