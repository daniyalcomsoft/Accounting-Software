using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Text;
using System.IO;
using System.Collections;
using SW.SW_Common;
using SCGL.BAL;

public partial class GLGeneralVoucher : System.Web.UI.Page
{
    public static string VoucherNumber = string.Empty;
    public static string ReferenceNo = string.Empty;
    public static DataTable dt = new DataTable();
    int count = 0;
    ReportDocument rd = new ReportDocument();
    SqlConnectionStringBuilder conf = new SqlConnectionStringBuilder(SCGL_Common.ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionBO"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        Reload_JS();

        Invoice_BAL BALInvoice = new Invoice_BAL();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataTable dt = PM.getFinancialYearByID(SBO.FinYearID);
        hdnMinDate.Value = SCGL_Common.CheckDateTime(dt.Rows[0]["yearFrom"]).ToShortDateString();
        hdnMaxDate.Value = SCGL_Common.CheckDateTime(dt.Rows[0]["YearTo"]).ToShortDateString(); 

        JQ.RecallJS(this, "Load_AutoComplete_Code();");
        JQ.DatePicker(this);
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "GLGeneralVoucher.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "GLGeneralVoucher.aspx" && view == true)
                {
                    ViewState["TransTable"] = null;
                    ViewState["DeletedRows"] = null;
                    LinkButtonBack.Visible = false;
                    if (Request.QueryString["VoucherNo"] != null)
                    {
                        GeneralJournalVoucher_BAL GGV = new GeneralJournalVoucher_BAL();
                        DataSet ds = GGV.GetRecordByVoucherNumber(Request.QueryString["VoucherNo"].ToString());
                        TransTable = ds.Tables[1];
                        SumDebitCredit();
                        ViewState["TransTable"] = TransTable;
                        GridTrans.DataSource = TransTable;
                        GridTrans.DataBind();
                        SetVoucherValues(ds.Tables[0]);
                        LinkButtonBack.Visible = true;
                        btnPrint.Visible = true;                        
                    }
                    else
                    {
                        if (TransTable.Rows.Count == 0)
                        {
                            TransTable.Rows.Add(TransTable.NewRow());
                        }
                        GridTrans.DataSource = TransTable;
                        GridTrans.DataBind();
                        ((LinkButton)GridTrans.Rows[0].FindControl("btnEdit")).Visible = false;
                        ((LinkButton)GridTrans.Rows[0].FindControl("LbtnRemoveGridRow")).Visible = false;
                    }
                    if (Request.QueryString["view"] != null)
                    {
                        btnSaveVoucher.Visible = false;
                        lbltotaldbt.Style["padding-left"] = "30px";
                        lbltotalcrdt.Style["padding-left"] = "55px";
                        GridTrans.FooterRow.Visible = false;
                        GridTrans.Columns[8].Visible = false;
                        GridTrans.Columns[7].Visible = false;
                    }

                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }

            
        }
    }
    public void Reload_JS()
    {
        SCGL_Common.ReloadJS(this, "MyDate();");
    }
    #region Methods
    private List<string> DeletedRows
    {
        get
        {
            List<string> DelRows = (List<string>)ViewState["DeletedRows"];
            if (DelRows == null)
            {
                DelRows = new List<string>();
                DeletedRows = DelRows;
            }
            return DelRows;
        }
        set
        {
            ViewState["DeletedRows"] = value;
        }
    }

    private DataTable TransTable
    {
        get
        {
            DataTable Table = (DataTable)ViewState["TransTable"];
            if (Table == null)
            {
                Table = new DataTable();
                Table.Columns.Add(new DataColumn("Sno", typeof(int)));
                Table.Columns.Add(new DataColumn("TransactionID", typeof(int)));
                Table.Columns.Add(new DataColumn("Code", typeof(string)));
                Table.Columns.Add(new DataColumn("Title", typeof(string)));
                Table.Columns.Add(new DataColumn("Remarks", typeof(string)));
                Table.Columns.Add(new DataColumn("CostCenterName", typeof(string)));
                Table.Columns.Add(new DataColumn("CostCenterID", typeof(int)));
                Table.Columns.Add(new DataColumn("Debit", typeof(double)));
                Table.Columns.Add(new DataColumn("Credit", typeof(double)));
                TransTable = Table;
            }
            return Table;
        }
        set
        {
            ViewState["TransTable"] = value;
        }
    }

    private void SaveWholeTransaction()
    {
        if (GridTrans.Rows.Count > 0 && GridTrans.Rows[0].Cells[1].ToString() != "")
        {
            GridViewRow Row = GridTrans.Rows[GridTrans.Rows.Count - 1];
            double Debit = lbltotaldbt.Text.Equals("") ? 0 : Convert.ToDouble(lbltotaldbt.Text);
            double Credit = lbltotalcrdt.Text.Equals("") ? 0 : Convert.ToDouble(lbltotalcrdt.Text);
            if (Debit == Credit && Debit != 0 && Credit != 0)
            {
                
                lblValidation.Visible = false;
                GeneralJournalVoucher_BAL GLB = new GeneralJournalVoucher_BAL();
                GLB.VoucherTypeID = (int)GL_BAL.VoucherType.General_Voucher;
                GLB.VoucherTypeName = GL_BAL.VoucherType.General_Voucher.ToString();
                GLB.VoucherNumber = txtVoucherNumber.Text;
                GLB.ReferenceNo = txtRefNumber.Text;
                GLB.Narration = txtNarration.Text;
                if (txtJobNumber.Text != "")
                {
                    GLB.JobID = SCGL_Common.CheckInt(hdnJobNumber.Value);
                }
                
                // Voucher Date Issue Here DD/MM/YYY Please Save as mm/dd/yyy

                GLB.VoucharDate = SCGL_Common.CheckDateTime(txtDate.Text);
                
                GLB.IsActive = 1;
                GLB.IsPosted = false;
                SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
                GLB.FinYearID = SBO.FinYearID;
                GeneralJournalVoucher_BAL GVBLL = new GeneralJournalVoucher_BAL();
                GVBLL.DeleteTransaction(DeletedRows);
                DataSet ds = GVBLL.InsertIntoTransaction(GLB, (SCGL_Session)Session["SessionBO"], TransTable);
                ViewState["TransTable"] = ds.Tables[1];
                TransTable = ds.Tables[1];
                ViewState["TransTable"] = TransTable;
                GridTrans.DataSource = TransTable;
                GridTrans.DataBind();
                SetVoucherValues(ds.Tables[0]);
                DataTable dt2 = ds.Tables[0];
                if (dt2.Rows.Count > 1)
                {
                    JQ.showStatusMsg(this.Page, dt2.Rows[1][7].ToString(), dt2.Rows[1][6].ToString());
                }
                JQ.showStatusMsg(this, "1", "Successfully Record Save");
                btnPrint.Visible = true;
            }
            else
            {
                JQ.showStatusMsg(this, "2", "Debit and Credit Should be equal");
            }
        }
        else
        { JQ.showStatusMsg(this, "2", "Select Code In Grid"); }
    }

    private void SetVoucherValues(DataTable tbl)
    {
        if (tbl.Rows.Count > 0)
        {
            txtVoucherNumber.Text = tbl.Rows[0]["VoucherNumber"].ToString();
            txtRefNumber.Text = tbl.Rows[0]["ReferenceNo"].ToString();
            ReferenceNo = tbl.Rows[0]["ReferenceNo"].ToString();
            //txtDate.Text = tbl.Rows[0]["VoucharDate"].ToString();
            txtDate.Text = SCGL_Common.CheckDateTime(tbl.Rows[0]["VoucharDate"]).ToShortDateString();
            txtNarration.Text = tbl.Rows[0]["Narration"].ToString();
            Job j = new Job();
            j = j.Read(SCGL_Common.CheckInt(tbl.Rows[0]["JobID"]));
            hdnJobNumber.Value = j.JobID.ToString();
            txtJobNumber.Text = j.JobNumber;
        }
    }

    private void SumDebitCredit()
    {
        lbltotaldbt.Text = TransTable.Compute("Sum(Debit)", "").ToString();
        if (TransTable.Compute("Sum(Debit)", "").ToString() != "")
        {
            lbltotaldbt.Text = Convert.ToDouble(lbltotaldbt.Text).ToString("#,##,0.00");
        }
        lbltotalcrdt.Text = TransTable.Compute("Sum(Credit)", "").ToString();
        if (TransTable.Compute("Sum(Credit)", "").ToString() != "")
        {
            lbltotalcrdt.Text = Convert.ToDouble(lbltotalcrdt.Text).ToString("#,##,0.00");
        }
    }
    private void AddNewEntry()
    {
        System.Threading.Thread.Sleep(1300);
        LinkButton btn = (LinkButton)GridTrans.FooterRow.FindControl("btnSave");
        if (btn.Text == "Add")
        {
            lblValidation.Text = "";
            TextBox txtCode = (TextBox)GridTrans.FooterRow.FindControl("txtCode");
            HiddenField FooterTitle = (HiddenField)GridTrans.FooterRow.FindControl("HidTitle");
            TextBox FooterRemarks = (TextBox)GridTrans.FooterRow.FindControl("txtRemarks");
            DropDownList FooterCostCenter = (DropDownList)GridTrans.FooterRow.FindControl("cmbCostCenter");
            TextBox FooterDebit = (TextBox)GridTrans.FooterRow.FindControl("txtDebit");
            TextBox FooterCredit = (TextBox)GridTrans.FooterRow.FindControl("txtCredit");
            if (TransTable.Rows[0]["Code"].ToString() == "")
            {
                ViewState["TransTable"] = null;
                TransTable.Rows.Clear();
                ViewState["TransTable"] = TransTable;
            }
            DataRow row = TransTable.NewRow();
            row["Sno"] = TransTable.Rows.Count + 1;
            row["TransactionID"] = 0;
            row["Code"] = txtCode.Text;
            row["Title"] = FooterTitle.Value;
            row["Remarks"] = FooterRemarks.Text;
            row["CostCenterName"] = FooterCostCenter.SelectedItem.Text;
            row["CostCenterID"] = FooterCostCenter.SelectedValue;

            if (FooterDebit.Text != "")
                row["Debit"] = FooterDebit.Text;
            else
                row["Debit"] = DBNull.Value;
            if (FooterCredit.Text != "")
                row["Credit"] = FooterCredit.Text;
            else
                row["Credit"] = DBNull.Value;
            TransTable.Rows.Add(row);
            SumDebitCredit();
            GridTrans.DataSource = TransTable;
            GridTrans.DataBind();
            JQ.RecallJS(this, "Load_AutoComplete_Code();");
        }
        else if (btn.Text == "Update")
        {
            Label lblSno = (Label)GridTrans.FooterRow.FindControl("lblSno2");
            if (lblSno.Text != "")
            {
                DataRow[] Drow = TransTable.Select("Sno=" + lblSno.Text);
                if (Drow.Length > 0)
                {
                    Drow[0]["Code"] = ((TextBox)GridTrans.FooterRow.FindControl("txtCode")).Text;
                    Drow[0]["Title"] = ((HiddenField)GridTrans.FooterRow.FindControl("HidTitle")).Value;
                    Drow[0]["Remarks"] = ((TextBox)GridTrans.FooterRow.FindControl("txtRemarks")).Text;
                    Drow[0]["CostCenterName"] = ((DropDownList)GridTrans.FooterRow.FindControl("cmbCostCenter")).SelectedItem.Text;
                    Drow[0]["CostCenterID"] = ((DropDownList)GridTrans.FooterRow.FindControl("cmbCostCenter")).SelectedValue;
                    TextBox Debit = ((TextBox)GridTrans.FooterRow.FindControl("txtDebit"));
                    TextBox Credit = ((TextBox)GridTrans.FooterRow.FindControl("txtCredit"));
                    if (Debit.Text != "")
                        Drow[0]["Debit"] = Debit.Text;
                    else
                        Drow[0]["Debit"] = DBNull.Value;

                    if (Credit.Text != "")
                        Drow[0]["Credit"] = Credit.Text;
                    else
                        Drow[0]["Credit"] = DBNull.Value;
                    SumDebitCredit();
                    GridTrans.DataSource = TransTable;
                    GridTrans.DataBind();
                    JQ.RecallJS(this, "Load_AutoComplete_Code();");
                }
            }
        }
    }

    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        AddNewEntry();
        JQ.RecallJS(this, "Load_AutoComplete_Code();");
    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandArgument.ToString() != "")
        {
            LinkButton btnCancel = (LinkButton)GridTrans.FooterRow.FindControl("btnCancel");
            btnCancel.Visible = true;

            DataRow[] Drow = TransTable.Select("Sno=" + e.CommandArgument.ToString());
            if (Drow.Length > 0)
            {
                ((TextBox)GridTrans.FooterRow.FindControl("txtCode")).Text = Drow[0]["Code"].ToString();
                ((TextBox)GridTrans.FooterRow.FindControl("txtTitle")).Text = Drow[0]["Title"].ToString();
                ((HiddenField)GridTrans.FooterRow.FindControl("HidTitle")).Value = Drow[0]["Title"].ToString();
                ((TextBox)GridTrans.FooterRow.FindControl("txtRemarks")).Text = Drow[0]["Remarks"].ToString();
                ((DropDownList)GridTrans.FooterRow.FindControl("cmbCostCenter")).SelectedValue = Drow[0]["CostCenterID"].ToString();
                ((TextBox)GridTrans.FooterRow.FindControl("txtDebit")).Text = Drow[0]["Debit"].ToString();
                ((TextBox)GridTrans.FooterRow.FindControl("txtCredit")).Text = Drow[0]["Credit"].ToString();
                ((Label)GridTrans.FooterRow.FindControl("lblSno2")).Text = Drow[0]["Sno"].ToString();
                ((LinkButton)GridTrans.FooterRow.FindControl("btnSave")).Text = "Update";
             }
        }
    }
    protected void btnSaveVoucher_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1300);
        lblRefNo.Text = "";
        GL_BAL GLCom = new GL_BAL();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        //if (GLCom.IsRefCodeAvailable(txtRefNumber.Text) && txtVoucherNumber.Text == "")
        //{
        if (txtVoucherNumber.Text == "")
        {
            if (SBO.Can_Insert == true)
            {
                SaveWholeTransaction();
            }
            else
            {
                JQ.showStatusMsg(this, "3", "User not Allowed to Insert New Record");
            }
        }
        //else if (!GLCom.IsRefCodeAvailable(txtRefNumber.Text) && txtVoucherNumber.Text != "")
        //{
        else if (txtVoucherNumber.Text != "")
        {
            //if (ReferenceNo == txtRefNumber.Text)
            //{
                if (SBO.Can_Update == true)
                {
                    SaveWholeTransaction();
                }
                else
                {
                    JQ.showStatusMsg(this, "3", "User not Allowed to Update  Record");
                }
            //}
            //else if (GLCom.IsRefCodeAvailable(txtRefNumber.Text))
            //{
            //    if (SBO.Can_Update == true)
            //    {
            //        SaveWholeTransaction();
            //    }
            //    else
            //    {
            //        JQ.showStatusMsg(this, "3", "User not Allowed to Update Record");
            //    }
            //}
            //else
            //{
            //    JQ.showStatusMsg(this, "2", "Reference number already exists !");// lblRefNo.Text = "Reference number already exists !";
            //}
        }
        //else
        //{
        //    JQ.showStatusMsg(this, "2", "Reference number already exists !");
        //}
        JQ.RecallJS(this, "Load_AutoComplete_Code();");
        JQ.DatePicker(this);

    }

    protected void GridTrans_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = (Label)e.Row.FindControl("lblSno");
            if (lbl.Text == "")
            {
                ((LinkButton)e.Row.FindControl("btnEdit")).Visible = false;
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            DropDownList cmbCostCenter = e.Row.FindControl("cmbCostCenter") as DropDownList;
            DataTable dt = new SuperAdmin_BAL().GetCostCenterList();
            PM.BindDropDown(cmbCostCenter, dt, "CostCenterID", "CostCenterName");
            cmbCostCenter.Items.Insert(0, new ListItem("- Select One -", "0"));
        }
    }

    protected void LbtnRemoveGridRow_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            if (TransTable.Rows.Count > 0)
            {
                DataRow Rows = TransTable.Rows[Convert.ToInt32(e.CommandArgument.ToString())];//["TransactionID"].ToString()
                if (Rows["TransactionID"].ToString() != "0" && Rows["TransactionID"].ToString() != "")
                {
                    DeletedRows.Add(Rows["TransactionID"].ToString());
                }
                TransTable.Rows.RemoveAt(Convert.ToInt32(e.CommandArgument.ToString()));
                if (TransTable.Rows.Count == 0)
                {
                    TransTable.Rows.Add(TransTable.NewRow());
                    TransTable.AcceptChanges();
                    TransTable.AcceptChanges();
                    ViewState["TransTable"] = TransTable;
                    GridTrans.DataSource = TransTable;
                    GridTrans.DataBind();
                    SumDebitCredit();
                    ((LinkButton)GridTrans.Rows[0].FindControl("LbtnRemoveGridRow")).Visible = false;
                }
                else
                {
                    TransTable.AcceptChanges();
                    ViewState["TransTable"] = TransTable;
                    GridTrans.DataSource = TransTable;
                    GridTrans.DataBind();
                    SumDebitCredit();
                }
            }
        }
        JQ.RecallJS(this, "Load_AutoComplete_Code();");
    }

    protected void LinkButtonBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("GLHome.aspx");
    }
    protected void btnFindAcc_Click(object sender, EventArgs e)
    {
        GeneralJournalVoucher_BAL GGV = new GeneralJournalVoucher_BAL();
        GLGeneralVoucher_BAL GG = new GLGeneralVoucher_BAL();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataTable dts = GG.GetYear_Account(SBO.FinYearID);
        DataTable dt = GG.GetAccountName(txtAccountNo.Text, SBO.FinYearID, dts.Rows[0]["YearFrom"].ToString(), dts.Rows[0]["YearTo"].ToString());
        GrdAccounts.DataSource = dt;
        if (dt.Rows.Count > 0)
        {
            GrdAccounts.DataBind();
            JQ.RecallJS(this, "Load_AutoComplete_Code();");
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
        var row = (GridViewRow)((Control)sender).NamingContainer;
        var rowIndex = row.RowIndex;
        ((TextBox)GridTrans.FooterRow.FindControl("txtCode")).Text = GrdAccounts.Rows[rowIndex].Cells[1].Text.ToString();
        ((TextBox)GridTrans.FooterRow.FindControl("txtTitle")).Text = GrdAccounts.Rows[rowIndex].Cells[2].Text.ToString();
        ((HiddenField)GridTrans.FooterRow.FindControl("HidTitle")).Value = GrdAccounts.Rows[rowIndex].Cells[2].Text.ToString();
        JQ.closeDialog(this, "FindAccount");
        JQ.RecallJS(this, "Load_AutoComplete_Code();");
        JQ.DatePicker(this);
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string Debit = lbltotaldbt.Text;
        string Credit = lbltotalcrdt.Text;
        if (Debit == Credit)
        {
            JQ.showDialog(this, "PrintReport");
            if (IsPostBack)
            {
                ConfigCrystalReport();
            }
        }
    }
    private void ConfigCrystalReport()
    {
        DataSetGeneralVoucher Data = new DataSetGeneralVoucher();
        DataSet ds = new DataSet();
        string reportPath = Server.MapPath("GL_Report\\GeneralVoucherPrint.rpt");
        rd.Load(reportPath);
        ds = getreport();
        Data.Tables[0].Merge(ds.Tables[0]);
        rd.SetDataSource(Data);
        
        rd.SetDatabaseLogon(conf.UserID, conf.Password, conf.DataSource, conf.InitialCatalog);
        rd.VerifyDatabase();
        CrystalReportViewer1.ReportSource = rd;
        CrystalReportViewer1.DataBind();
        ViewState["Print"] = "ReportGenerated";
        CrystalReportViewer1.HasPrintButton = false;
    }
    private DataSet getreport()
    {
        GeneralJournalVoucher_BAL GGV = new GeneralJournalVoucher_BAL();
        DataSet dsw = GGV.GetRecordByVoucherNumber(txtVoucherNumber.Text);
        DataTable dtw = new DataTable();
        DataSet ds = new DataSet();
        DataTable dt;
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        dt = TransTable;
        dtw = dsw.Tables[2];
        // dt = ds.Tables[1].Copy();
        string Check = string.Empty;
        if (ViewState["Print"] != null)
        {
            Check = ViewState["Print"].ToString();
        }
        if (Check != "ReportGenerated")
        {
            dt.Columns.Add("CompanyName");
            dt.Columns.Add("Date");
            dt.Columns.Add("VoucherTypeName");
            dt.Columns.Add("TotalAmount");
            dt.Columns.Add("TransDate");
            dt.Columns.Add("VoucherNumber");
            dt.Columns.Add("Narration");
            dt.Columns.Add("AmountToWord");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["AmountToWord"] = dtw.Rows[0]["Fn_AmountWords"];
            }
            dt.Rows[0]["CompanyName"] = SBO.SiteName;
            dt.Rows[0]["Date"] = DateTime.Now.ToShortDateString();
            dt.Rows[0]["VoucherTypeName"] = "General Ledger Voucher";
            dt.Rows[0]["TotalAmount"] = lbltotaldbt.Text;
            dt.Rows[0]["TransDate"] = txtDate.Text;
            dt.Rows[0]["Narration"] = txtNarration.Text;
            dt.Rows[0]["VoucherNumber"] = txtVoucherNumber.Text;
        }
        ds.Merge(dt);
        return ds;
    }
    protected void CrystalReportViewer1_Init(object sender, EventArgs e)
    {
        if (IsPostBack)
            CrystalReportViewer1.ReportSource = rd;
        CrystalReportViewer1.DataBind();
    }
    protected void CrystalReportViewer1_Navigate1(object source, CrystalDecisions.Web.NavigateEventArgs e)
    {
        ConfigCrystalReport();
    }
    protected void GrdAccounts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GeneralJournalVoucher_BAL GGV = new GeneralJournalVoucher_BAL();
        GrdAccounts.PageIndex = e.NewPageIndex;
        GLGeneralVoucher_BAL GG = new GLGeneralVoucher_BAL();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataTable dts = GG.GetYear_Account(SBO.FinYearID);

        GrdAccounts.DataSource = GG.GetAccountName(txtAccountNo.Text, SBO.FinYearID, dts.Rows[0]["YearFrom"].ToString(), dts.Rows[0]["YearTo"].ToString());
        GrdAccounts.DataBind();
    }
    protected void ButtonPrint_Click(object sender, EventArgs e)
    {
        //JQ.closeDialog(this, "PrintReport");
        //JQ.showDialog(this, "PrintReport");
        //if (IsPostBack)
        //{
        //    ConfigCrystalReport();

        //    System.Drawing.Printing.PrinterSettings printerSettings = new System.Drawing.Printing.PrinterSettings();
        //    System.Windows.Forms.PrintDialog printDialog = new System.Windows.Forms.PrintDialog();
        //    printDialog.PrinterSettings = printerSettings;
        //    printDialog.AllowPrintToFile = false;
        //    printDialog.AllowSomePages = true;
        //    printDialog.UseEXDialog = true;

        //    try
        //    {
        //        System.Windows.Forms.DialogResult result = printDialog.ShowDialog();
        //        if (result == System.Windows.Forms.DialogResult.OK)
        //        {
        //            int frompage = printerSettings.FromPage;
        //            int topage = printerSettings.ToPage;
        //            rd.PrintOptions.PrinterName = printerSettings.PrinterName;
        //            rd.PrintToPrinter(printerSettings.Copies, false, frompage, topage);

        //            lbtnNo.Text = "OK";
        //            JQ.showDialog(this, "Confirmation");
        //            lblDeleteMsg.Text = "General Ledger Voucher Print Successfully!";
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
    protected void GrdAccounts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //lnkSelect_Click(sender, e);
    }
    protected void GrdAccounts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Attributes.Add("onClick", "javascript:void SelectRow(this);");
        //}
    }
    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        LinkButton btnCancel = (LinkButton)GridTrans.FooterRow.FindControl("btnCancel");
        btnCancel.Visible = false;
        ViewState["TransTable"] = TransTable as DataTable;
        GridTrans.DataSource = ViewState["TransTable"];
        GridTrans.DataBind();
        JQ.RecallJS(this, "Load_AutoComplete_Code();");
    }
    protected void lnkNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("GLGeneralVoucher.aspx");
    }

    protected void btnFindJob_Click(object sender, EventArgs e)
    {
        sqlDSJobs.FilterParameters.Clear();
        sqlDSJobs.FilterExpression = "JobNumber Like {0}";
        sqlDSJobs.FilterParameters.Add("JobNumber", "'%" + txtJobNumberSearch.Text + "%'");
        sqlDSJobs.DataBind();
        grdJobs.DataBind();
    }

    protected void lnkSelectJob_Click(object sender, EventArgs e)
    {
        LinkButton selectbtn = (LinkButton)sender;
        hdnJobNumber.Value = selectbtn.Attributes["JobID"];
        var row = (GridViewRow)(selectbtn).NamingContainer;
        var rowIndex = row.RowIndex;
        txtJobNumber.Text = grdJobs.Rows[rowIndex].Cells[1].Text.ToString();
        JQ.closeDialog(this, "FindJobs");
        JQ.RecallJS(this, "Load_AutoComplete_JobNumber();");        
    }
}
