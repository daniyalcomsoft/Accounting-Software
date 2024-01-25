using System;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using SW.SW_Common;

public partial class GL_Report : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    ReportDocument transactionReport = new ReportDocument();

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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "GL_Report.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "GL_Report.aspx" && view == true)
                {
                    FillVoucherTypeList();
                    ConfigureCrystalReports();
                    btnPrintJava.Visible = false;
                    //JQ.DatePicker(this);
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
        hdnMinDate.Value = dt.Rows[0]["yearFrom"].ToString();
        hdnMaxDate.Value = dt.Rows[0]["YearTo"].ToString();
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
        GL_BAL GL = new GL_BAL();
        cmbVoucherType.DataSource = GL.GetVoucherType();
        cmbVoucherType.DataTextField = "VoucherTypeName";
        cmbVoucherType.DataValueField = "VoucherTypeID";
        cmbVoucherType.DataBind();
    }
    #endregion

    private void ConfigureCrystalReports()
    {
        string reportPath = Server.MapPath("GL_Report\\CrystalReport_GL.rpt");
        transactionReport.Load(reportPath);
        SqlConnectionStringBuilder conf = new SqlConnectionStringBuilder(SCGL_Common.ConnectionString);
        transactionReport.SetDataSource(getreport());
        transactionReport.SetDatabaseLogon(conf.UserID, conf.Password, conf.DataSource, conf.InitialCatalog);
        transactionReport.VerifyDatabase();
        CrystalReportViewer1.ReportSource = transactionReport;
        CrystalReportViewer1.DataBind();
        if (!IsPostBack)
        {
            btnPrintJava.Visible = false;
        }
        else 
        {
            btnPrintJava.Visible = false;
            
        }
        CrystalReportViewer1.HasPrintButton = true;
    }

    private DataSet getreport()
    {
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString);
        string str = string.Empty;
        con.Open();
        if (txtDateFrom.Text != "" && txtDateTo.Text != "")
        {
            if (DropDownListPosted.SelectedItem.Text == "All Vouchers")
            {
               str = "SELECT  TR.VoucherNumber,  TR.Code , Sub.Title,  TR.Debit,  TR.Credit, 'IsPosted'=CASE when Tr.IsPosted>0 THEN 'True' else 'False' END,  TR.Narration FROM vt_SCGL_Transaction Tr INNER JOIN vt_SCGL_Subsidary Sub  ON Tr.Code = Sub.Code where VoucherTypeID= " + cmbVoucherType.SelectedValue + " AND VoucharDate Between '" + txtDateFrom.Text + "' And '" + txtDateTo.Text + "'AND VoucherNumber IN(SELECT Tr.VoucherNumber FROM vt_SCGL_Transaction Tr where  VoucherTypeID=" + cmbVoucherType.SelectedValue + " AND VoucharDate Between '" + txtDateFrom.Text + "' And '" + txtDateTo.Text + "') order by Tr.VoucherNumber;" +
                    "SELECT distinct Tr.VoucharDate, Tr.VoucherTypeName, Tr.VoucherNumber, Tr.Narration FROM vt_SCGL_Transaction Tr INNER JOIN vt_SCGL_Subsidary Sub ON Tr.MainCode = Sub.MainCode AND Tr.Code = Sub.Code where VoucherTypeID= " + cmbVoucherType.SelectedValue + " AND VoucharDate Between '" + txtDateFrom.Text + "' And '" + txtDateTo.Text + "' order by Tr.VoucharDate ASC";
            }
            //if (DropDownListPosted.SelectedItem.Text == "Posted")
            //{
            //    str = "SELECT TR.VoucherNumber, TR.Code, Sub.Title, TR.Debit, TR.Credit, 'IsPosted'=CASE when Tr.IsPosted>0 THEN 'True' else 'False' END, TR.Narration FROM vt_SCGL_Transaction Tr INNER JOIN vt_SCGL_Subsidary Sub ON Tr.Code = Sub.Code where IsPosted = 'True' AND VoucherTypeID=" + cmbVoucherType.SelectedValue + " AND VoucharDate Between '" + DateTime.ParseExact(txtDateFrom.Text, "MM/dd/yyyy", null).ToString() + "' And '" + DateTime.ParseExact(txtDateTo.Text, "MM/dd/yyyy", null).ToString() + "'  and VoucherNumber IN(SELECT Tr.VoucherNumber FROM vt_SCGL_Transaction Tr where  VoucherTypeID=" + cmbVoucherType.SelectedValue + " AND VoucharDate Between '" + DateTime.ParseExact(txtDateFrom.Text, "MM/dd/yyyy", null).ToString() + "' And '" + DateTime.ParseExact(txtDateTo.Text, "MM/dd/yyyy", null).ToString() + "'  ) order by Tr.VoucherNumber " +
            //          "SELECT distinct Tr.VoucharDate, Tr.VoucherTypeName, Tr.VoucherNumber, Tr.Narration FROM vt_SCGL_Transaction Tr INNER JOIN vt_SCGL_Subsidary Sub ON Tr.MainCode = Sub.MainCode AND Tr.Code = Sub.Code where IsPosted = 'True'AND VoucherTypeID=" + cmbVoucherType.SelectedValue + " AND VoucharDate Between '" + DateTime.ParseExact(txtDateFrom.Text, "MM/dd/yyyy", null).ToString() + "' And '" + DateTime.ParseExact(txtDateTo.Text, "MM/dd/yyyy", null).ToString() + "' order by Tr.VoucharDate ASC";
            //}
            //if (DropDownListPosted.SelectedItem.Text == "UnPosted")
            //{
            //    str = "SELECT Tr.VoucherNumber, Tr.Code, Sub.Title, Tr.Debit, Tr.Credit, 'IsPosted'=CASE when Tr.IsPosted>0 THEN 'True' else 'False' END, Tr.Narration FROM vt_SCGL_Transaction Tr INNER JOIN vt_SCGL_Subsidary Sub ON Tr.Code = Sub.Code where IsPosted = 'False' AND VoucherTypeID=" + cmbVoucherType.SelectedValue + " AND VoucharDate Between '" + DateTime.ParseExact(txtDateFrom.Text, "MM/dd/yyyy", null).ToString() + "' And '" + DateTime.ParseExact(txtDateTo.Text, "MM/dd/yyyy", null).ToString() + "'  and VoucherNumber IN(SELECT Tr.VoucherNumber FROM vt_SCGL_Transaction Tr where  VoucherTypeID=" + cmbVoucherType.SelectedValue + " AND VoucharDate Between '" + DateTime.ParseExact(txtDateFrom.Text, "MM/dd/yyyy", null).ToString() + "' And '" + DateTime.ParseExact(txtDateTo.Text, "MM/dd/yyyy", null).ToString() + "'  ) order by Tr.VoucherNumber " +
            //          "SELECT distinct Tr.VoucharDate, Tr.VoucherTypeName, Tr.VoucherNumber, Tr.Narration FROM vt_SCGL_Transaction Tr INNER JOIN vt_SCGL_Subsidary Sub ON Tr.MainCode = Sub.MainCode AND Tr.Code = Sub.Code where IsPosted = 'False'AND VoucherTypeID=" + cmbVoucherType.SelectedValue + " AND VoucharDate Between '" + DateTime.ParseExact(txtDateFrom.Text, "MM/dd/yyyy", null).ToString() + "' And '" + DateTime.ParseExact(txtDateTo.Text, "MM/dd/yyyy", null).ToString() + "' order by Tr.VoucharDate ASC";
            //}
            CrystalReportViewer1.Visible = true;
        }
        if (txtDateFrom.Text == "")
        {

            if (DropDownListPosted.SelectedItem.Text == "All Vouchers")
            {
                //str = "SELECT Tr.VoucherNumber,Tr.Code, Sub.Title, Tr.Debit, Tr.Credit, 'IsPosted'=CASE when Tr.IsPosted>0 THEN 'True' else 'False' END, Tr.Narration FROM vt_SCGL_Transaction Tr INNER JOIN vt_SCGL_Subsidary Sub ON Tr.Code = Sub.Code where VoucherTypeID=" + cmbVoucherType.SelectedValue + "  AND VoucharDate < CONVERT(VARCHAR(10), GETDATE(), 103) AND VoucherNumber IN(SELECT Tr.VoucherNumber FROM vt_SCGL_Transaction Tr where  VoucherTypeID=" + cmbVoucherType.SelectedValue + " AND VoucharDate < CONVERT(VARCHAR(10), GETDATE(), 103)  ) order by Tr.VoucherNumber " +
                //      "SELECT distinct Tr.VoucharDate, Tr.VoucherTypeName, Tr.VoucherNumber, Tr.Narration FROM vt_SCGL_Transaction Tr INNER JOIN vt_SCGL_Subsidary ON Tr.MainCode = vt_SCGL_Subsidary.MainCode AND Tr.Code = vt_SCGL_Subsidary.Code where VoucherTypeID=" + cmbVoucherType.SelectedValue + " AND VoucharDate < CONVERT(VARCHAR(10), GETDATE(), 103) order by Tr.VoucharDate ASC";
                str = "SELECT Tr.VoucherNumber,Tr.Code, Sub.Title, Tr.Debit, Tr.Credit, 'IsPosted'=CASE when Tr.IsPosted>0 THEN 'True' else 'False' END, Tr.Narration FROM vt_SCGL_Transaction Tr INNER JOIN vt_SCGL_Subsidary Sub ON Tr.Code = Sub.Code where VoucherTypeID=" + cmbVoucherType.SelectedValue + "  AND VoucharDate < CONVERT(datetime, GETDATE(), 103) AND VoucherNumber IN(SELECT Tr.VoucherNumber FROM vt_SCGL_Transaction Tr where  VoucherTypeID=" + cmbVoucherType.SelectedValue + " AND VoucharDate < CONVERT(datetime, GETDATE(), 103)  ) order by Tr.VoucherNumber " +
                     "SELECT distinct Tr.VoucharDate, Tr.VoucherTypeName, Tr.VoucherNumber, Tr.Narration FROM vt_SCGL_Transaction Tr INNER JOIN vt_SCGL_Subsidary ON Tr.MainCode = vt_SCGL_Subsidary.MainCode AND Tr.Code = vt_SCGL_Subsidary.Code where VoucherTypeID=" + cmbVoucherType.SelectedValue + " AND VoucharDate < CONVERT(datetime, GETDATE(), 103) order by Tr.VoucharDate ASC";
            }
            //if (DropDownListPosted.SelectedItem.Text == "Posted")
            //{
            //    str = "SELECT Tr.VoucherNumber,Tr.Code, Sub.Title, Tr.Debit, Tr.Credit, 'IsPosted'=CASE when Tr.IsPosted>0 THEN 'True' else 'False' END, Tr.Narration FROM vt_SCGL_Transaction Tr INNER JOIN vt_SCGL_Subsidary Sub ON Tr.Code = Sub.Code where IsPosted = 'True' AND VoucherTypeID=" + cmbVoucherType.SelectedValue + " AND VoucharDate < GETDATE()  and VoucherNumber IN(SELECT Tr.VoucherNumber FROM vt_SCGL_Transaction Tr where  VoucherTypeID=" + cmbVoucherType.SelectedValue + " AND VoucharDate < GETDATE()  ) order by Tr.VoucherNumber " +
            //          "SELECT distinct Tr.VoucharDate, Tr.VoucherTypeName, Tr.VoucherNumber, Tr.Narration FROM vt_SCGL_Transaction Tr INNER JOIN vt_SCGL_Subsidary ON Tr.MainCode = vt_SCGL_Subsidary.MainCode AND Tr.Code = vt_SCGL_Subsidary.Code where IsPosted = 'True'AND VoucherTypeID=" + cmbVoucherType.SelectedValue + " AND VoucharDate < GETDATE() order by Tr.VoucharDate ASC";
            //}
            //if (DropDownListPosted.SelectedItem.Text == "UnPosted")
            //{
            //    str = "SELECT Tr.VoucherNumber,Tr.Code, Sub.Title, Tr.Debit, Tr.Credit, 'IsPosted'=CASE when Tr.IsPosted>0 THEN 'True' else 'False' END, Tr.Narration FROM vt_SCGL_Transaction Tr INNER JOIN vt_SCGL_Subsidary Sub ON Tr.Code = Sub.Code where IsPosted = 'False' AND VoucherTypeID=" + cmbVoucherType.SelectedValue + " AND VoucharDate < GETDATE()  and VoucherNumber IN(SELECT Tr.VoucherNumber FROM vt_SCGL_Transaction Tr where  VoucherTypeID=" + cmbVoucherType.SelectedValue + " AND VoucharDate < GETDATE()  ) order by Tr.VoucherNumber " +
            //          "SELECT distinct Tr.VoucharDate, Tr.VoucherTypeName, Tr.VoucherNumber, Tr.Narration FROM vt_SCGL_Transaction Tr INNER JOIN vt_SCGL_Subsidary ON Tr.MainCode = vt_SCGL_Subsidary.MainCode AND Tr.Code = vt_SCGL_Subsidary.Code where IsPosted = 'False'AND VoucherTypeID=" + cmbVoucherType.SelectedValue + " AND VoucharDate < GETDATE() order by Tr.VoucharDate ASC";
            //}
            CrystalReportViewer1.Visible = false;
        }
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        adpt.Fill(ds);
        ViewState["Report"] = ds;
        SetReportStructure();
        ds = ViewState["Report"] as DataSet;
        con.Close();
        return ds;
       
        
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
                System.Threading.Thread.Sleep(1300);
                ConfigureCrystalReports();
                JQ.DatePicker(this);
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

    protected void CrystalReportViewer1_Navigate1(object source, CrystalDecisions.Web.NavigateEventArgs e)
    {
        ConfigureCrystalReports();
    }

    protected void CrystalReportViewer1_Init(object sender, EventArgs e)
    {
        //ConfigureCrystalReports();
    }

    private void SetReportStructure()
    {
        if (ViewState["Report"] != null)
        {
            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            DataSet ds;
            DataTable Main;
            DataTable Sub;
            ds = ViewState["Report"] as DataSet;
            Main = ds.Tables[1].Copy();
            Sub = ds.Tables[0].Copy();
            Main.Columns.Add("Posted");
            Main.Columns.Add("From");
            Main.Columns.Add("To");
            Main.Columns.Add("CompanyName");
            Main.Columns.Add("PostedType");
            Main.Columns.Add("Number");
            Main.Columns.Add("VoucherTypeNameNew");
            string PostedName = DropDownListPosted.SelectedItem.Text;
            if (Sub.Rows.Count > 0)
            {
                int i = 0;
                string VoucherNo = "";
                string Posted = "";
                string FromDate = string.Empty;
                string ToDate = string.Empty;
                if (txtDateFrom.Text != "")
                {
                    ToDate = "" + txtDateFrom.Text + "" + " " + "To" + " " + "" + txtDateTo.Text + "";
                }
                else
                {
                    ToDate = "           AS ON" + " " + "" + DateTime.Now.ToShortDateString() + "";
                }
                foreach (DataRow dr in Sub.Rows)
                {
                    if (i == 0)
                    {
                        VoucherNo = "0";
                        Posted = dr["IsPosted"].ToString();
                    }
                    if (VoucherNo != dr["VoucherNumber"].ToString())
                    {
                        VoucherNo = dr["VoucherNumber"].ToString();
                        Posted = dr["IsPosted"].ToString();
                        if (Posted == "False")
                        {
                            Posted = "No";
                        }
                        else
                        {
                            Posted = "Yes";
                        }
                        foreach (DataRow mn in Main.Rows)
                        {
                            if (VoucherNo == mn["VoucherNumber"].ToString())
                            {
                                mn["Posted"] = Posted.ToString();
                                mn["From"] = ToDate.ToString();
                                //mn["To"] = ToDate.ToString();
                                mn["CompanyName"] = SBO.SiteName;
                                mn["PostedType"] = PostedName.ToString();
                                mn["Number"] = VoucherNo.ToString();
                                mn["VoucherTypeNameNew"] = cmbVoucherType.SelectedItem.Text;
                            }
                        }
                        i++;
                    }
                }
                ds.Tables[0].Clear();
                ds.Tables[1].Clear();
                ds.Tables[0].Merge(Sub);
                ds.Tables[1].Merge(Main);
                ViewState["Report"] = ds;
                //ds.Tables[1] = Main as DataTable;
            }
        }
    }

    protected void DropDownListPosted_SelectedIndexChanged(object sender, EventArgs e)
    {
        getreport();
    }
    protected void lnkConYes_Click(object sender, EventArgs e)
    {
        int Copies = Convert.ToInt32(TextCopies.Text == "" ? "1" : TextCopies.Text);
        int GivenSPages = Convert.ToInt32(TextStartPages.Text == "" ? "0" : TextStartPages.Text);
        int GivenEPages = Convert.ToInt32(TextEndpages.Text == "" ? "0" : TextEndpages.Text);
        if (GivenEPages != null)
        {
            ConfigureCrystalReports();
            transactionReport.PrintToPrinter(Copies, true, GivenSPages, GivenEPages);
            JQ.closeDialog(this, "ControlConfirmation");
            JQ.showDialog(this, "Confirmation");
            lblDeleteMsg.Text = "General Voucher Print Successfully ! ";
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
    }
}
