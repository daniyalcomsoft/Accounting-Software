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

public partial class ProfitandLoss_Trial : System.Web.UI.Page
{
    public static string strUser;
    string reportPath = "";
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "ProfitandLoss_Trial.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "ProfitandLoss_Trial.aspx" && view == true)
                {
                    Bind_CostCenter();
                    ConfigCrystalReport();
                    CrystalReportViewer1.Visible = false;
                    ChkBox_ZeroField.Checked = true;
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

    public void Bind_CostCenter()
    {
        DataTable dt = new SuperAdmin_BAL().GetCostCenterListforProfitLoss();
        PM.BindDropDown(ddlCostCenter, dt, "CostCenterID", "CostCenterName");
        ddlCostCenter.Items.Insert(0, new ListItem("All Centers", "0"));
        //SCGL_Common.Bind_DropDown(ddlVendor, "vt_SCGL_BindVendor", "VendorName", "ID");
    }

    private void ConfigCrystalReport()
    {
        if (txt_DateFrom.Text != "" && txt_DateTo.Text != "")
        {
            if (ChkBox_ZeroField.Checked == true)
            {
                reportPath = Server.MapPath("GL_Report\\ProfitandLoss_Trial.rpt");
            }
            else 
            {
                reportPath = Server.MapPath("GL_Report\\ProfitandLoss_Trial2.rpt");
            }
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
            CrystalReportViewer1.HasPrintButton = true;
            //btnPrintJava.Style.Add("display", "block");
        }
    }
    private DataTable getreport()
    {
        DataSet ds = new DataSet();
        if (txt_DateFrom.Text != "" && txt_DateTo.Text != "")
        {
            SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString);
            con.Open();
            SqlCommand cmd;
            if (ChkBox_ZeroField.Checked == true)
            {
                cmd = new SqlCommand("vt_SCGL_Sp_Profitandloss_trial2", con);
            }
            else
            {
                cmd = new SqlCommand("vt_SCGL_Sp_Profitandloss_trial5", con);
            }
            cmd.CommandType = CommandType.StoredProcedure;
            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            cmd.Parameters.AddWithValue("@YearFrom", txt_DateFrom.Text);
            cmd.Parameters.AddWithValue("@YearTo", txt_DateTo.Text);
            cmd.Parameters.AddWithValue("@CostCenterID", ddlCostCenter.SelectedValue);
            cmd.Parameters.AddWithValue("@FinYearID", SBO.FinYearID);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(ds);
            ViewState["Report"] = ds;
            ds = ViewState["Report"] as DataSet;
            DataTable dt;
            dt = ds.Tables[0].Copy();
            dt.Columns.Add("CompanyName");
            dt.Columns.Add("ReportName");
            dt.Columns.Add("From");
            dt.Columns.Add("To");
            foreach (DataRow dr in dt.Rows)
            {
                dr["CompanyName"] = SBO.SiteName;
                dr["ReportName"] = "Profit & Loss Trial";
                dr["From"] = txt_DateFrom.Text;
                dr["To"] = txt_DateTo.Text;
            }
            ds.Tables[0].Clear();
            ds.Tables[0].Merge(dt);
            con.Close();
        }
        return ds.Tables[0];
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

    private void SetParameterInReport()
    {
        SCGL_Session Sbo = (SCGL_Session)Session["SessionBO"];
        ds.Tables[0].Columns.Add("SiteName");
        ds.Tables[0].Columns.Add("@YearFrom");
        ds.Tables[0].Columns.Add("@From");
        ds.Tables[0].Columns.Add("@To");
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ds.Tables[0].Rows[i]["SiteName"] = Sbo.SiteName;
            ds.Tables[0].Rows[i]["YearFrom"] = hdnMinDate.Value;
            ds.Tables[0].Rows[i]["From"] = txt_DateFrom.Text;
            ds.Tables[0].Rows[i]["To"] = txt_DateTo.Text;
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
            lblDeleteMsg.Text = "Profit And Loss Trial Report Print Successfully ! ";
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
        DataTable ds = new DataTable();
        if (SBO.Can_View == true)
        {

            ds = getreport();
            if (ds.Rows.Count > 0)
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


        //if (SBO.Can_View == true)
        //{
        //    ConfigCrystalReport();
        //}
        //else
        //{
        //    JQ.showStatusMsg(this, "3", "User not Allowed to View Record");
        //    CrystalReportViewer1.Visible = false;
        //}
        //JQ.DatePicker(this);


    }
    protected void btnPrintJava_Click(object sender, EventArgs e)
    {
        ConfigCrystalReport();
        JQ.showDialog(this, "ControlConfirmation");
    }
}
