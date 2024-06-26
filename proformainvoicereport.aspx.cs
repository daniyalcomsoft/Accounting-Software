﻿using System;
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

public partial class proformainvoicereport : System.Web.UI.Page
{
    ReportDocument rd = new ReportDocument();
    SqlConnectionStringBuilder conf = new SqlConnectionStringBuilder(SCGL_Common.ConnectionString);
    public static DataTable dt = new DataTable();
    DataSet ds = new DataSet();
    int i=0;
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "proformainvoicereport.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "proformainvoicereport.aspx" && view == true)
                {

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
        hdnMinDate.Value = dt.Rows[0]["yearFrom"].ToString();
        hdnMaxDate.Value = dt.Rows[0]["YearTo"].ToString();
        ConfigureCrystalReports();
        
    }
    public void Reload_JS()
    {
        SCGL_Common.ReloadJS(this, "ChangeDateFrom();");
        SCGL_Common.ReloadJS(this, "ChangeDateTo();");
        SCGL_Common.ReloadJS(this, "MyDate();");
    }
    private void ConfigureCrystalReports()
    {
        string reportPath = Server.MapPath("GL_Report\\ProformaInvoiceReport_2.rpt");
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
            btnPrintJava.Visible = false;
            CrystalReportViewer1.Visible = true;

        }
        CrystalReportViewer1.HasPrintButton = true;
        ScriptManager.RegisterStartupScript(this, GetType(), "Message", "MyDate();", true);
    }

    private DataSet getreport()
    {
        i++;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("vt_SCGL_Sp_ProformaInvoiceReport", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@InvoiceID", txtInvoiceID.Text);
      
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        adpt.Fill(ds);
        ViewState["COA"] = ds;
        SetReport();
        ds = ViewState["COA"] as DataSet;
        DataTable v = ds.Tables[0];
        if (v.Rows.Count >= 1)
        {
            CrystalReportViewer1.Visible = true;
        }
        else
        {
            CrystalReportViewer1.Visible = false;
        }
        if (CrystalReportViewer1.Visible == false && i==2) 
        {
            JQ.showDialog(this, "Record");
        }
        con.Close();
        return ds;


    }

    private void SetReport()
    {
        if (ViewState["COA"] != null)
        {
            DataTable dt = new DataTable("ProformaInvoiceReport");
            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            ds = ViewState["COA"] as DataSet;
            dt = ds.Tables[0].Copy();
            dt.Columns.Add("CompanyName");
            dt.Columns.Add("VoucherTypeName");
            dt.Columns.Add("ReportName");
            dt.Columns.Add("Email1");
            dt.Columns.Add("Email2");
            dt.Columns.Add("Web");
            dt.Columns.Add("Phone1");
            dt.Columns.Add("Phone2");
            dt.Columns.Add("Fax");
            dt.Columns.Add("OldPNo");
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
                dr["Email1"] = "info@karim-impex.com";
                dr["Email2"] = "rbb.karimimpex@gmail.com";
                dr["Fax"] = "(92-21)32310416";
                dr["Phone1"] = "32314322";
                dr["Phone2"] = "32310286";
                dr["ReportName"] = "PROFORMA INVOICE";
                dr["Web"] = "www.karim-impex.com";
                dr["OldPNo"] = "No.";
                dr["VoucherTypeName"] = "PROFORMA INVOICE";
            }

            ds.Tables[0].Clear();
            ds.Tables[0].Merge(dt);
            ds.Tables[0].TableName = "ProformaInvoiceReport";
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
            rd.PrintToPrinter(Copies, true, GivenSPages, GivenEPages);
            JQ.closeDialog(this, "ControlConfirmation");
            JQ.showDialog(this, "Confirmation");
            lblDeleteMsg.Text = "Proforma Invoice Summary Report Print Successfully ! ";
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
