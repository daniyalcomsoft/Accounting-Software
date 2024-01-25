using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using CrystalDecisions.Shared;
using System.Drawing.Printing;
using SW.SW_Common;
using SCGL.BAL;


public partial class GLCashRecievedVoucher : System.Web.UI.Page
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
        
        txtBalance.Text = txtBalanceHidden.Value;
        txtCodelbl.Text = titlecode.Value;
       // txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        JQ.DatePicker(this);
        JQ.RecallJS(this, "Load_AutoComplete_Code();");
        JQ.RecallJS(this, "Load_AutoComplete_Code2();");
    
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "GLCashRecievedVoucher.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "GLCashRecievedVoucher.aspx" && view == true)
                {
                    ViewState["TransTable"] = null;
                    ViewState["DeletedRows"] = null;
                    LinkButtonBack.Visible = false;
                    if (Request.QueryString["VoucherNo"] != null)
                    {
                        GLCashRecVoucher_BAL CRV = new GLCashRecVoucher_BAL();
                        DataSet ds = CRV.GetCashRecVoucherRecord(Request.QueryString["VoucherNo"].ToString());
                        ViewState["TransTable"] = ds.Tables[1];
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
                        ((LinkButton)GridTrans.Rows[GridTrans.Rows.Count - 1].FindControl("btnEdit")).Visible = false;
                        ((LinkButton)GridTrans.Rows[GridTrans.Rows.Count - 1].FindControl("LbtnRemoveGridRow")).Visible = false;
                    }
                    if (Request.QueryString["view"] != null)
                    {
                        btnSave.Visible = false;
                        GridTrans.FooterRow.Visible = false;
                        lblTotal.Style["padding-left"] = "36px";
                        lblTotalAmt.Style["padding-left"] = "60px";
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
            ////else if (!GLCom.IsRefCodeAvailable(txtRefNumber.Text) && txtVoucherNumber.Text != "")
            ////{
            else if (txtVoucherNumber.Text != "")
            {
                //if (ReferenceNo == txtRefNumber.Text)
                //{
                    if (SBO.Can_Update == true)
                    {
                        SaveVoucher();
                        JQ.showStatusMsg(this, "1", "Successfull Record Update");
                        
                    }
                    else
                    {
                        JQ.showStatusMsg(this, "3", "User not Allowed to Update Record");
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
                ((System.Web.UI.WebControls.TextBox)GridTrans.FooterRow.FindControl("txtCodeGrid")).Text = Drow[0]["Code"].ToString();
                ((System.Web.UI.WebControls.TextBox)GridTrans.FooterRow.FindControl("txtTitleGrid")).Text = Drow[0]["Title"].ToString();
                ((System.Web.UI.WebControls.TextBox)GridTrans.FooterRow.FindControl("txtRemarks")).Text = Drow[0]["Remarks"].ToString();
                ((DropDownList)GridTrans.FooterRow.FindControl("cmbCostCenter")).SelectedValue = Drow[0]["CostCenterID"].ToString();
                ((System.Web.UI.WebControls.TextBox)GridTrans.FooterRow.FindControl("txtCredit")).Text = Drow[0]["Credit"].ToString();
                ((System.Web.UI.WebControls.Label)GridTrans.FooterRow.FindControl("lblSno2")).Text = Drow[0]["Sno"].ToString();
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
                txtBalance.Text = string.Format("{0}", "{0}");
                txtBalance.Text = Convert.ToDouble(txtBalance.Text).ToString("#,##,0.00");
                titlecode.Value = txtCodelbl.Text;
                txtBalanceHidden.Value = txtBalance.Text;
            }
            else
            {
                txtCode.Text = "";
                txtCodelbl.Text = "";
                txtBalance.Text = "";
            }
        }
        else
        {
            txtCode.Text = "";
            txtCodelbl.Text = "";
            txtBalance.Text = "";
        }
    }

    private void SetEntryAccountHeadInfo()
    {
        System.Web.UI.WebControls.TextBox FooterCode = (System.Web.UI.WebControls.TextBox)GridTrans.FooterRow.FindControl("txtCodeGrid");
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
            else
            {
                FooterCode.Text = "";
                FooterTitle.Value = "";
            }
        }
    }

    private void AddNewEntry()
    {

        LinkButton btn = (LinkButton)GridTrans.FooterRow.FindControl("btnAdd");
        if (btn.Text == "Add")
        {
            System.Web.UI.WebControls.TextBox FooterCode = (System.Web.UI.WebControls.TextBox)GridTrans.FooterRow.FindControl("txtCodeGrid");
            HiddenField FooterTitle = (HiddenField)GridTrans.FooterRow.FindControl("HidTitle");
            System.Web.UI.WebControls.TextBox FooterRemarks = (System.Web.UI.WebControls.TextBox)GridTrans.FooterRow.FindControl("txtRemarks");
            DropDownList FooterCostCenter = (DropDownList)GridTrans.FooterRow.FindControl("cmbCostCenter");
            System.Web.UI.WebControls.TextBox FooterCredit = (System.Web.UI.WebControls.TextBox)GridTrans.FooterRow.FindControl("txtCredit");
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
            if (FooterCredit.Text != "")
                row["Credit"] = FooterCredit.Text;
            row["Remarks"] = FooterRemarks.Text;
            row["CostCenterName"] = FooterCostCenter.SelectedItem.Text;
            row["CostCenterID"] = FooterCostCenter.SelectedValue;
            TransTable.Rows.Add(row);
            TotalAmount();
            GridTrans.DataSource = TransTable;
            GridTrans.DataBind();
            JQ.DatePicker(this);
        }
        else if (btn.Text == "Update")
        {
            System.Web.UI.WebControls.Label lblSno = (System.Web.UI.WebControls.Label)GridTrans.FooterRow.FindControl("lblSno2");
            if (lblSno.Text != "")
            {
                DataRow[] Drow = TransTable.Select("Sno=" + lblSno.Text);
                if (Drow.Length > 0)
                {
                    Drow[0]["Code"] = ((System.Web.UI.WebControls.TextBox)GridTrans.FooterRow.FindControl("txtCodeGrid")).Text;
                    Drow[0]["Title"] = ((System.Web.UI.WebControls.TextBox)GridTrans.FooterRow.FindControl("txtTitleGrid")).Text;
                    Drow[0]["Remarks"] = ((System.Web.UI.WebControls.TextBox)GridTrans.FooterRow.FindControl("txtRemarks")).Text;
                    Drow[0]["CostCenterName"] = ((DropDownList)GridTrans.FooterRow.FindControl("cmbCostCenter")).SelectedItem.Text;
                    Drow[0]["CostCenterID"] = ((DropDownList)GridTrans.FooterRow.FindControl("cmbCostCenter")).SelectedValue;
                    if (((System.Web.UI.WebControls.TextBox)GridTrans.FooterRow.FindControl("txtCredit")).Text != "")
                        Drow[0]["Credit"] = ((System.Web.UI.WebControls.TextBox)GridTrans.FooterRow.FindControl("txtCredit")).Text;
                    TotalAmount();
                    GridTrans.DataSource = TransTable;
                    GridTrans.DataBind();
                    JQ.DatePicker(this);
                }
            }
        }
    }

    private void TotalAmount()
    {
        lblTotalAmt.Text = TransTable.Compute("Sum(Credit)", "").ToString();
        if (TransTable.Compute("Sum(Credit)", "").ToString() != "")
        {
            lblTotalAmt.Text = Convert.ToDouble(lblTotalAmt.Text).ToString("#,##,0.00");
        }
        
    }

    private void SaveVoucher()
    {
        lblValidation.Visible = false;
        if (TransTable.Rows.Count > 0 && TransTable.Rows[0]["code"] != "")
        {
            GridViewRow Row = GridTrans.Rows[GridTrans.Rows.Count - 1];
            if (txtBalanceHidden.Value != "" && lblTotalAmt.Text != "")
            {
                GLCashRecVoucher_BAL GLB = new GLCashRecVoucher_BAL();
                GLB.TransactionID = lblTransID.Text.Equals("") ? 0 : Convert.ToInt32(lblTransID.Text);
                GLB.VoucherTypeID = (int)PM.VoucherType.Cash_Recievalbe_Voucher; //Convert.ToInt32(cmbVoucherType.SelectedValue);
                GLB.VoucherTypeName = PM.VoucherType.Cash_Recievalbe_Voucher.ToString();//cmbVoucherType.Text;
                GLB.VoucherNumber = txtVoucherNumber.Text;
                GLB.ReferenceNo = txtRefNumber.Text;
                GLB.Narration = txtNarration.Text;
                //GLB.VoucharDate = DateTime.ParseExact(txtDate.Text, "MM/dd/yyyy", null).ToString();
                GLB.VoucharDate = SCGL_Common.CheckDateTime(txtDate.Text);
                GLB.Code = txtCode.Text;
                GLB.Debit = Convert.ToDouble(lblTotalAmt.Text);//Convert.ToDouble(TransTable.Rows[TransTable.Rows.Count]["Credit"]);
                GLB.IsActive = 1;
                GLB.IsPosted = false;
                if (txtJobNumber.Text != "") 
                {
                    GLB.JobID = SCGL_Common.CheckInt(hdnJobNumber.Value);
                }
                
                SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
                GLB.FinYearID = SBO.FinYearID;
                GLGeneralVoucher_BAL GV = new GLGeneralVoucher_BAL();
                GV.DeleteTransaction(DeletedRows);
                GLCashRecVoucher_BAL GVBLL = new GLCashRecVoucher_BAL();
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
                }
                JQ.showStatusMsg(this, "1", "Successfull Record Insert");
                ViewState["Print"] = "";
                btnPrint.Visible = true;
            }
        }
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
            titlecode.Value = tbl.Rows[0]["Title"].ToString();
             txtBalance.Text = tbl.Rows[0]["CurrentBal"].ToString();
            txtBalanceHidden.Value = tbl.Rows[0]["CurrentBal"].ToString();
            txtVoucherNumber.Text = tbl.Rows[0]["VoucherNumber"].ToString();
            txtRefNumber.Text = tbl.Rows[0]["ReferenceNo"].ToString();
            ReferenceNo = tbl.Rows[0]["ReferenceNo"].ToString();
            txtDate.Text = SCGL_Common.CheckDateTime(tbl.Rows[0]["VoucharDate"]).ToShortDateString();
            txtNarration.Text = tbl.Rows[0]["Narration"].ToString();
            Job j = new Job();
            j = j.Read(SCGL_Common.CheckInt(tbl.Rows[0]["JobID"]));
            hdnJobNumber.Value = j.JobID.ToString();
            txtJobNumber.Text = j.JobNumber;
        }
    }
    #endregion



    protected void txtCode_TextChanged(object sender, EventArgs e)
    {
        SetMainAccountInfo();
    }

    protected void txtCodeGrid_TextChanged(object sender, EventArgs e)
    {
        SetEntryAccountHeadInfo();
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        EditEntry(e.CommandArgument.ToString());
        LinkButton btnCancel = (LinkButton)GridTrans.FooterRow.FindControl("btnCancel");
        btnCancel.Visible = true;
       // JQ.RecallJS(this, "Load_AutoComplete_Code();");
        JQ.DatePicker(this);
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1300);
        AddNewEntry();
        JQ.RecallJS(this, "Load_AutoComplete_Code();");
        JQ.DatePicker(this);
    }
    protected void GridTrans_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Web.UI.WebControls.Label lbl = (System.Web.UI.WebControls.Label)e.Row.FindControl("lblSno");
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
                    ViewState["TransTable"] = TransTable;
                    TotalAmount();
                    TransTable.AcceptChanges();
                    ViewState["TransTable"] = TransTable;
                    GridTrans.DataSource = TransTable;
                    GridTrans.DataBind();
                    ((LinkButton)GridTrans.Rows[GridTrans.Rows.Count - 1].FindControl("LbtnRemoveGridRow")).Visible = false;
                }
                else
                {
                    TotalAmount();
                    TransTable.AcceptChanges();
                    ViewState["TransTable"] = TransTable;
                    GridTrans.DataSource = TransTable;
                    GridTrans.DataBind();
                }
                
            }
        }
        JQ.RecallJS(this, "Load_AutoComplete_Code();");
        JQ.DatePicker(this);
    }
    protected void LinkButtonBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("GLHome.aspx");
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
        DataSetCashRecipt Data = new DataSetCashRecipt();
        DataSet ds = new DataSet();
        string reportPath = Server.MapPath("GL_Report\\CashReciptPrint.rpt");
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
        GLCashRecVoucher_BAL CRV = new GLCashRecVoucher_BAL();
        DataSet dsa = CRV.GetCashRecVoucherRecord(txtVoucherNumber.Text);
        DataTable dtw = new DataTable();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        dt = TransTable;
        dtw = dsa.Tables[0];
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
            dt.Columns.Add("AmountToWord");
            dt.Rows[0]["CompanyName"] = SBO.SiteName;
            dt.Rows[0]["Date"] = DateTime.Now.ToShortDateString();
            dt.Rows[0]["MainAccount"] = txtCodelbl.Text;
            dt.Rows[0]["MainAccountCode"] = txtCode.Text;
            dt.Rows[0]["VoucherTypeName"] = "Cash Reciept Voucher";
            dt.Rows[0]["TotalAmount"] = lblTotalAmt.Text;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["AmountToWord"] = dtw.Rows[0]["FN_AmountWords"];    
            }
            dt.Rows[0]["TransDate"] = txtDate.Text;
            dt.Rows[0]["Narration"] = txtNarration.Text;
            dt.Rows[0]["VoucherNumber"] = txtVoucherNumber.Text;
        }

        ds.Merge(dt);
        return ds;
    }
    protected void CrystalReportViewer1_Navigate1(object source, CrystalDecisions.Web.NavigateEventArgs e)
    {
        ConfigCrystalReport();
    }
    protected void CrystalReportViewer1_Init(object sender, EventArgs e)
    {
        if (IsPostBack)
            CrystalReportViewer1.ReportSource = rd;
        CrystalReportViewer1.DataBind();
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

    protected void btnFindAcc2_Click(object sender, EventArgs e)
    {
        
            GLGeneralVoucher_BAL GGV = new GLGeneralVoucher_BAL();
            // GrdAccounts.DataSource = GGV.GetAccountName(txtAccountNo.Text);
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
        var row = (GridViewRow)((System.Web.UI.Control)sender).NamingContainer;
        var rowIndex = row.RowIndex;
       
            ((System.Web.UI.WebControls.TextBox)GridTrans.FooterRow.FindControl("txtCodeGrid")).Text = GrdAccounts.Rows[rowIndex].Cells[1].Text.ToString();
            ((System.Web.UI.WebControls.TextBox)GridTrans.FooterRow.FindControl("txtTitleGrid")).Text = GrdAccounts.Rows[rowIndex].Cells[2].Text.ToString();
            ((HiddenField)GridTrans.FooterRow.FindControl("HidTitle")).Value = GrdAccounts.Rows[rowIndex].Cells[2].Text.ToString();
        
        JQ.closeDialog(this, "FindAccount");
    }

    protected void lnkSelect2_Click(object sender, EventArgs e)
    {
        var row = (GridViewRow)((System.Web.UI.Control)sender).NamingContainer;
        var rowIndex = row.RowIndex;
       
            txtCode.Text = GrdAccounts2.Rows[rowIndex].Cells[1].Text.ToString();
            txtCodelbl.Text = GrdAccounts2.Rows[rowIndex].Cells[2].Text.ToString();
            titlecode.Value = GrdAccounts2.Rows[rowIndex].Cells[2].Text.ToString();
            txtBalance.Text = GrdAccounts2.Rows[rowIndex].Cells[3].Text.ToString();
            txtBalanceHidden.Value = GrdAccounts2.Rows[rowIndex].Cells[3].Text.ToString();
      
        JQ.closeDialog(this, "FindAccount2");
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

    protected void lnkbtnfind2_Click(object sender, EventArgs e)
    {
        HdnFindCode.Value = "0";
        //GrdAccounts.DataBind();
        JQ.showDialog(this,"FindAccount2");
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        HdnFindCode.Value = "1";
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

        //    PrinterSettings printerSettings = new PrinterSettings();
        //    PrintDialog printDialog = new PrintDialog();
        //    printDialog.PrinterSettings = printerSettings;
        //    printDialog.AllowPrintToFile = false;
        //    printDialog.AllowSomePages = true;
        //    printDialog.UseEXDialog = true;

        //    try
        //    {
        //        DialogResult result = printDialog.ShowDialog();
        //        if (result == DialogResult.OK)
        //        {
        //            int frompage = printerSettings.FromPage;
        //            int topage = printerSettings.ToPage;
        //            rd.PrintOptions.PrinterName = printerSettings.PrinterName;
        //            rd.PrintToPrinter(printerSettings.Copies, false, frompage, topage);

                    
        //            lbtnNo.Text = "OK";
        //            JQ.showDialog(this, "Confirmation");
        //            lblDeleteMsg.Text = "Cash Reciept Voucher Print Successfully ! ";
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
    //protected void txtCodeGrid_TextChanged1(object sender, EventArgs e)
    //{
    //    //GridViewRow Row = (sender as TextBox).NamingContainer as GridViewRow;
    //    //(Row.FindControl("txtTitleGrid") as TextBox).Text = "";
    //    //JQ.RecallJS(this, "Load_AutoComplete_Code();");
    //}
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
        Response.Redirect("GLCashRecievedVoucher.aspx");
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
        JQ.RecallJS(this, "Load_AutoComplete_Code();");
    }
   
}
