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
using SW.SW_Common;
using SCGL.BAL;


public partial class GLCashPaymentVoucher : System.Web.UI.Page
{
    public static string ReferenceNo = string.Empty;
    public static DataTable dt = new DataTable();
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

        SCGL_Session sbo = new SCGL_Session();
        string name = sbo.UserName;
        JQ.RecallJS(this, "Get_Current_Bal();");
        JQ.RecallJS(this, "Load_AutoComplete_Code();");
        JQ.RecallJS(this, "Load_AutoComplete_Code2();");
        JQ.DatePicker(this);
        txtBalance.Text = txtBalanceHidden.Value;
        txtCodelbl.Text = titlecode.Value;
        lblValidation.Text = "";
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "GLCashPaymentVoucher.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "GLCashPaymentVoucher.aspx" && view == true)
                {
                    //   txtDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    ViewState["TransTable"] = null;
                    ViewState["DeletedRows"] = null;

                    LinkButtonBack.Visible = false;
                    if (Request.QueryString["VoucherNo"] != null)
                    {
                        GLCashPaymentVoucher_BAL CPV = new GLCashPaymentVoucher_BAL();
                        DataSet ds = CPV.GetCashPaymentRecordByVoucherNumber(Request.QueryString["VoucherNo"].ToString());

                        TransTable = ds.Tables[1];
                        TotalAmount();
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
                        btnSave.Visible = false;
                        GridTrans.FooterRow.Visible = false;
                        lbltotal.Style["padding-left"] = "36px";
                        lblTotalAmt.Style["padding-left"] = "55px";
                        GridTrans.Columns[6].Visible = false;
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtCodelbl.Text != "")
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
                    SaveVoucher();
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
                        SaveVoucher();

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
                //        SaveVoucher();
                //    }
                //    else
                //    {
                //        JQ.showStatusMsg(this, "3", "User not Allowed to Update Record");
                //    }
                //}
                //else
                //{
                //    lblRefNo.Text = "Reference number already exists !";
                //}
            }
            //else
            //{
            //    lblRefNo.Text = "Reference number already exists !";
            //}
        }
        else
        {
            txtCodelbl.Text = "Please add correct acc no: ";
        }
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
                Table.Columns.Add(new DataColumn("Debit", typeof(double)));
                Table.Columns.Add(new DataColumn("Credit", typeof(double)));
                Table.Columns.Add(new DataColumn("Remarks", typeof(string)));
                Table.Columns.Add(new DataColumn("CostCenterName", typeof(string)));
                Table.Columns.Add(new DataColumn("CostCenterID", typeof(int)));
                TransTable = Table;
            }
            return Table;
        }
        set
        {
            ViewState["TransTable"] = value;
        }
    }
    private void EditEntry(string Sno)
    {
        if (Sno != "")
        {
            DataRow[] Drow = TransTable.Select("Sno=" + Sno);
            if (Drow.Length > 0)
            {
                ((TextBox)GridTrans.FooterRow.FindControl("txtCodeGrid")).Text = Drow[0]["Code"].ToString();
                ((TextBox)GridTrans.FooterRow.FindControl("txtTitleGrid")).Text = Drow[0]["Title"].ToString();
                ((HiddenField)GridTrans.FooterRow.FindControl("HiddenFieldTitle")).Value = Drow[0]["Title"].ToString();
                ((TextBox)GridTrans.FooterRow.FindControl("txtRemarks")).Text = Drow[0]["Remarks"].ToString();
                ((DropDownList)GridTrans.FooterRow.FindControl("cmbCostCenter")).SelectedValue = Drow[0]["CostCenterID"].ToString();
                ((TextBox)GridTrans.FooterRow.FindControl("txtDebit")).Text = Drow[0]["Debit"].ToString();
                ((Label)GridTrans.FooterRow.FindControl("lblSno2")).Text = Drow[0]["Sno"].ToString();
                ((LinkButton)GridTrans.FooterRow.FindControl("btnAdd")).Text = "Update";
            }
        }
    }
    private void SetMainAccountInfo()
    {
        if (dt.Rows.Count > 0)
        {
            DataRow[] Code = dt.Select("CodeTitle ='" + txtCode.Text + "'");
            if (Code.Length > 0)
            {
                txtCode.Text = Code[0]["Code"].ToString();
                txtCodelbl.Text = Code[0]["Title"].ToString();
                txtBalance.Text = Code[0]["CurrentBal"].ToString();
            }
            else
            {
                txtCode.Text = "";
                txtCodelbl.Text = "";
                txtBalance.Text = "";
            }
        }
    }
    private void SetEntryAccountHeadInfo()
    {
        TextBox FooterCode = (TextBox)GridTrans.FooterRow.FindControl("txtCodeGrid");
        HiddenField FooterTitle = (HiddenField)GridTrans.FooterRow.FindControl("HiddenFieldTitle");
        if (FooterCode.Text != "")
        {
            if (dt.Rows.Count > 0)
            {
                DataRow[] Code = dt.Select("CodeTitle ='" + FooterCode.Text + "'");
                if (Code.Length > 0)
                {
                    FooterCode.Text = Code[0]["Code"].ToString();
                    FooterTitle.Value = Code[0]["Title"].ToString();
                }
                else
                {
                    FooterCode.Text = "";
                    FooterTitle.Value = "";
                }
            }
        }
    }
    private void AddNewEntry()
    {
        LinkButton btn = (LinkButton)GridTrans.FooterRow.FindControl("btnAdd");
        if (btn.Text == "Add")
        {
            TextBox FooterCode = (TextBox)GridTrans.FooterRow.FindControl("txtCodeGrid");
            HiddenField FooterTitle = (HiddenField)GridTrans.FooterRow.FindControl("HiddenFieldTitle");
            TextBox FooterRemarks = (TextBox)GridTrans.FooterRow.FindControl("txtRemarks");
            DropDownList FooterCostCenter = (DropDownList)GridTrans.FooterRow.FindControl("cmbCostCenter");
            TextBox FooterDebit = (TextBox)GridTrans.FooterRow.FindControl("txtDebit");
            if (TransTable.Rows[0]["Title"].ToString() == "" && FooterCode.Text != "")
            {
                ViewState["TransTable"] = null;
                TransTable.Rows.Clear();
                ViewState["TransTable"] = TransTable;
            }
            DataRow row = TransTable.NewRow();
            row["Sno"] = TransTable.Rows.Count + 1;
            row["TransactionID"] = 0;
            row["Code"] = FooterCode.Text;
            row["Title"] = FooterTitle.Value;
            if (FooterDebit.Text != "")
                row["Debit"] = FooterDebit.Text;
            row["Remarks"] = FooterRemarks.Text;
            row["CostCenterName"] = FooterCostCenter.SelectedItem.Text;
            row["CostCenterID"] = FooterCostCenter.SelectedValue;
            TransTable.Rows.Add(row);
            TotalAmount();
            GridTrans.DataSource = TransTable;
            GridTrans.DataBind();
        }
        else if (btn.Text == "Update")
        {
            Label lblSno = (Label)GridTrans.FooterRow.FindControl("lblSno2");
            if (lblSno.Text != "")
            {
                DataRow[] Drow = TransTable.Select("Sno=" + lblSno.Text);
                if (Drow.Length > 0)
                {
                    Drow[0]["Code"] = ((TextBox)GridTrans.FooterRow.FindControl("txtCodeGrid")).Text;
                    //HiddenField Title = (HiddenField)GridTrans.FooterRow.FindControl("HiddenFieldTitle");
                    Drow[0]["Title"] = ((HiddenField)GridTrans.FooterRow.FindControl("HiddenFieldTitle")).Value;  //((TextBox)GridTrans.FooterRow.FindControl("txtTitleGrid")).Text;
                    Drow[0]["Remarks"] = ((TextBox)GridTrans.FooterRow.FindControl("txtRemarks")).Text;
                    Drow[0]["CostCenterName"] = ((DropDownList)GridTrans.FooterRow.FindControl("cmbCostCenter")).SelectedItem.Text;
                    Drow[0]["CostCenterID"] = ((DropDownList)GridTrans.FooterRow.FindControl("cmbCostCenter")).SelectedValue;
                    if (((TextBox)GridTrans.FooterRow.FindControl("txtDebit")).Text != "")
                        Drow[0]["Debit"] = ((TextBox)GridTrans.FooterRow.FindControl("txtDebit")).Text;
                    TotalAmount();
                    GridTrans.DataSource = TransTable;
                    GridTrans.DataBind();
                }
            }
        }
    }
    private void TotalAmount()
    {
        lblTotalAmt.Text = TransTable.Compute("Sum(Debit)", "").ToString();
        if (lblTotalAmt.Text != "")
        {
            lblTotalAmt.Text = Convert.ToDouble(lblTotalAmt.Text).ToString("#,##,0.00");
        }
    }
    private void SaveVoucher()
    {
        lblValidation.Text = "";
        if (GridTrans.Rows.Count > 0 && GridTrans.Rows[0].Cells[1].ToString() != "")
        {
            if (txtBalanceHidden.Attributes["value"] != "" && lblTotalAmt.Text != "")
            {

                GLCashPaymentVoucher_BAL GLB = new GLCashPaymentVoucher_BAL();
                GLB.TransactionID = lblTransID.Text.Equals("") ? 0 : Convert.ToInt32(lblTransID.Text);
                GLB.VoucherTypeID = (int)PM.VoucherType.Cash_Payment_Voucher; //Convert.ToInt32(cmbVoucherType.SelectedValue);
                GLB.VoucherTypeName = PM.VoucherType.Cash_Payment_Voucher.ToString();//cmbVoucherType.Text;
                GLB.VoucherNumber = txtVoucherNumber.Text;
                GLB.ReferenceNo = txtRefNumber.Text;
                GLB.Narration = txtNarration.Text;
                //GLB.VoucharDate = DateTime.ParseExact(txtDate.Text, "MM/dd/yyyy", null).ToString();
                GLB.VoucharDate = SCGL_Common.CheckDateTime(txtDate.Text);
                GLB.Code = txtCode.Text;
                GLB.Credit = lblTotalAmt.Text.Equals("") ? 0 : Convert.ToDouble(lblTotalAmt.Text);
                GLB.IsActive = 1;
                GLB.IsPosted = false;
                SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
                GLB.FinYearID = SBO.FinYearID;
                if (txtJobNumber.Text != "") 
                {
                    GLB.JobID = SCGL_Common.CheckInt(hdnJobNumber.Value);
                }
                
                GLGeneralVoucher_BAL GV = new GLGeneralVoucher_BAL();
                GV.DeleteTransaction(DeletedRows);
                GLCashPaymentVoucher_BAL GVBLL = new GLCashPaymentVoucher_BAL();
                if (txtVoucherNumber.Text != "")
                {
                    DataSet ds = GVBLL.InsertUpdateTransaction(GLB, (SCGL_Session)Session["SessionBO"], TransTable);
                    ViewState["TransTable"] = ds.Tables[1];
                    TransTable = ds.Tables[1];
                    TotalAmount();
                    GridTrans.DataSource = TransTable;
                    GridTrans.DataBind();
                    SetVoucherValues(ds.Tables[0]);
                    DataTable dt2 = ds.Tables[0];
                    if (dt2.Rows.Count > 1)
                    {
                        JQ.showStatusMsg(this.Page, dt2.Rows[1][16].ToString(), dt2.Rows[1][15].ToString());
                        ViewState["Print"] = "";
                    }
                    JQ.showStatusMsg(this, "1", "Successfull Record Save");
                }
                else if (Convert.ToDouble(txtBalanceHidden.Attributes["value"]) >= Convert.ToDouble(lblTotalAmt.Text))
                {
                    DataSet ds = GVBLL.InsertUpdateTransaction(GLB, (SCGL_Session)Session["SessionBO"], TransTable);
                    ViewState["TransTable"] = ds.Tables[1];
                    TransTable = ds.Tables[1];
                    TotalAmount();
                    GridTrans.DataSource = TransTable;
                    GridTrans.DataBind();
                    SetVoucherValues(ds.Tables[0]);
                    DataTable dt2 = ds.Tables[0];
                    if (dt2.Rows.Count > 1)
                    {
                        JQ.showStatusMsg(this.Page, dt2.Rows[1][16].ToString(), dt2.Rows[1][15].ToString());
                        ViewState["Print"] = "";
                        btnPrint.Visible = true;
                    }
                    JQ.showStatusMsg(this, "1", "Successfull Record Save");
                }
                else { JQ.showStatusMsg(this, "2", "Cash Account Amount " + txtBalance.Text + " Not Greater Then Current Amount"); }

            }
            else
            {
                JQ.showStatusMsg(this, "2", "Total amount and balance can not be empty");
            }
        }
        JQ.RecallJS(this, "Get_Current_Bal();");
        JQ.RecallJS(this, "Load_AutoComplete_Code();");
        JQ.RecallJS(this, "Load_AutoComplete_Code2();");
    }
    private void SetVoucherValues(DataTable tbl)
    {
        if (tbl.Rows.Count > 0)
        {
            lblTransID.Text = tbl.Rows[0]["TransactionID"].ToString();
            txtCode.Text = tbl.Rows[0]["Code"].ToString();
            txtCodelbl.Text = tbl.Rows[0]["Title"].ToString();
            txtBalance.Text = tbl.Rows[0]["CurrentBal"].ToString();
            titlecode.Value = tbl.Rows[0]["Title"].ToString();
            txtBalanceHidden.Value = tbl.Rows[0]["CurrentBal"].ToString();
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

    #endregion

    protected void txtCodeGrid_TextChanged(object sender, EventArgs e)
    {
        GridViewRow Row = (sender as TextBox).NamingContainer as GridViewRow;
        (Row.FindControl("txtTitleGrid") as TextBox).Text = "";

        SetEntryAccountHeadInfo();
        JQ.RecallJS(this, "Load_AutoComplete_Code();");
    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        LinkButton btnCancel = (LinkButton)GridTrans.FooterRow.FindControl("btnCancel");
        btnCancel.Visible = true;
        System.Threading.Thread.Sleep(1300);
        EditEntry(e.CommandArgument.ToString());
        JQ.RecallJS(this, "Get_Current_Bal();");
        JQ.RecallJS(this, "Load_AutoComplete_Code();");
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1300);
        AddNewEntry();
        JQ.RecallJS(this, "Get_Current_Bal();");
        JQ.RecallJS(this, "Load_AutoComplete_Code();");
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
        System.Threading.Thread.Sleep(1300);
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
                    DataRow TR = TransTable.NewRow();
                    TransTable.Rows.Add(TR);
                    TransTable.AcceptChanges();
                    TotalAmount();
                    TransTable.AcceptChanges();
                    GridTrans.DataSource = null;
                    GridTrans.DataBind();
                    ViewState["TransTable"] = TransTable;
                    GridTrans.DataSource = TransTable;
                    GridTrans.DataBind();
                    ((LinkButton)GridTrans.Rows[0].FindControl("LbtnRemoveGridRow")).Visible = false;
                }
                else
                {
                    TotalAmount();
                    TransTable.AcceptChanges();
                    GridTrans.DataSource = null;
                    GridTrans.DataBind();
                    ViewState["TransTable"] = TransTable;
                    GridTrans.DataSource = TransTable;
                    GridTrans.DataBind();
                }
            }
        }
        JQ.RecallJS(this, "Get_Current_Bal();");
        JQ.RecallJS(this, "Load_AutoComplete_Code();");
    }
    protected void LinkButtonBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("GLHome.aspx");
    }
    protected void btnFindAcc_Click(object sender, EventArgs e)
    {
       
            GLGeneralVoucher_BAL GGV = new GLGeneralVoucher_BAL();
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

    protected void btnFindAcc2_Click(object sender, EventArgs e)
    {
        
            GLGeneralVoucher_BAL GGV = new GLGeneralVoucher_BAL();
            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            DataTable dts = GGV.GetYear_Account(SBO.FinYearID);
            DataTable dt = GGV.GetAccountName2(txtAccountNo.Text, SBO.FinYearID, dts.Rows[0]["YearFrom"].ToString(), dts.Rows[0]["YearTo"].ToString());
            GrdAccounts2.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                GrdAccounts2.DataBind();
            }
            else
            {
                GrdAccounts2.DataSource = null;
                GrdAccounts2.DataBind();
                JQ.showDialog(this, "Confirmation");
            }
        


    }

    protected void lnkSelect_Click(object sender, EventArgs e)
    {
        var row = (GridViewRow)((Control)sender).NamingContainer;
        var rowIndex = row.RowIndex;
        
            ((TextBox)GridTrans.FooterRow.FindControl("txtCodeGrid")).Text = GrdAccounts.Rows[rowIndex].Cells[1].Text.ToString();
            ((TextBox)GridTrans.FooterRow.FindControl("txtTitleGrid")).Text = GrdAccounts.Rows[rowIndex].Cells[2].Text.ToString();
            ((HiddenField)GridTrans.FooterRow.FindControl("HiddenFieldTitle")).Value = GrdAccounts.Rows[rowIndex].Cells[2].Text.ToString();
        
        JQ.closeDialog(this, "FindAccount");
    }

    protected void lnkSelect2_Click(object sender, EventArgs e)
    {
        var row = (GridViewRow)((Control)sender).NamingContainer;
        var rowIndex = row.RowIndex;
       
            txtCode.Text = GrdAccounts2.Rows[rowIndex].Cells[1].Text.ToString();
            txtCodelbl.Text = GrdAccounts2.Rows[rowIndex].Cells[2].Text.ToString();
            titlecode.Value = GrdAccounts2.Rows[rowIndex].Cells[2].Text.ToString();
            txtBalance.Text = GrdAccounts2.Rows[rowIndex].Cells[3].Text.ToString();
            txtBalanceHidden.Value = GrdAccounts2.Rows[rowIndex].Cells[3].Text.ToString();
        
        
        JQ.closeDialog(this, "FindAccount2");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        JQ.showDialog(this, "PrintReport");
        if (IsPostBack)
        {
            ConfigCrystalReport();
        }
    }
    private void ConfigCrystalReport()
    {
        DataSetCashPayment Data = new DataSetCashPayment();
        DataSet ds = new DataSet();
        string reportPath = Server.MapPath("GL_Report\\CashPaymentPrint.rpt");
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
        GLCashPaymentVoucher_BAL CPV = new GLCashPaymentVoucher_BAL();
        DataSet dsw = CPV.GetCashPaymentRecordByVoucherNumber(txtVoucherNumber.Text);
        DataTable TableWord = new DataTable();
        DataSet ds = new DataSet();
        DataTable dt;
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        dt = TransTable;
        TableWord = dsw.Tables[0];
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
            dt.Columns.Add("MainAccount");
            dt.Columns.Add("MainAccountCode");
            dt.Columns.Add("VoucherTypeName");
            dt.Columns.Add("TotalAmount");
            dt.Columns.Add("TransDate");
            dt.Columns.Add("VoucherNumber");
            dt.Columns.Add("Narration");
            dt.Columns.Add("NumericToWord");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["NumericToWord"] = TableWord.Rows[0]["NumericToWord"];
            }
            dt.Rows[0]["CompanyName"] = SBO.SiteName;
            dt.Rows[0]["Date"] = DateTime.Now.ToShortDateString();
            dt.Rows[0]["MainAccount"] = txtCodelbl.Text;
            dt.Rows[0]["MainAccountCode"] = txtCode.Text;
            dt.Rows[0]["VoucherTypeName"] = "Cash Payment Voucher";
            dt.Rows[0]["TotalAmount"] = lblTotalAmt.Text;
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
        
            GLGeneralVoucher_BAL GGV = new GLGeneralVoucher_BAL();
            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            DataTable dts = GGV.GetYear_Account(SBO.FinYearID);
            GrdAccounts.DataSource = GGV.GetAccountName(txtAccountNo.Text, SBO.FinYearID, dts.Rows[0]["YearFrom"].ToString(), dts.Rows[0]["YearTo"].ToString());
            GrdAccounts.PageIndex = e.NewPageIndex;
            GrdAccounts.DataBind();
        
    }

    protected void GrdAccounts2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       
            GLGeneralVoucher_BAL GGV = new GLGeneralVoucher_BAL();
            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            DataTable dts = GGV.GetYear_Account(SBO.FinYearID);
            GrdAccounts2.DataSource = GGV.GetAccountName2(txtAccountNo.Text, SBO.FinYearID, dts.Rows[0]["YearFrom"].ToString(), dts.Rows[0]["YearTo"].ToString());
            GrdAccounts2.PageIndex = e.NewPageIndex;
            GrdAccounts2.DataBind();
       
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        HdnFindAccount.Value = "0";
        //GrdAccounts.DataBind();
        JQ.showDialog(this, "FindAccount2");
    }
    protected void btnFind_Click1(object sender, EventArgs e)
    {
        HdnFindAccount.Value = "1";
        //GrdAccounts.DataBind();
        JQ.showDialog(this, "FindAccount");
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
        //            lblDeleteMsg.Text = "Cash Payment Voucher Print Successfully!";
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
    protected void btnCancel_Click(object sender, EventArgs e)
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
        Response.Redirect("GLCashPaymentVoucher.aspx");
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
        JQ.RecallJS(this, "Load_AutoComplete_Code2();");
    }
}
