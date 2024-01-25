using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SW.SW_Common;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Threading;
using SCGL.BAL;

public partial class ExpenseSheetForm : System.Web.UI.Page
{
    ExpenseSheet_BAL BALInvoice = new ExpenseSheet_BAL();
    int InventoryID = 0;
    int CostCenterID = 0;

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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "ExpenseSheetForm.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "ExpenseSheetForm.aspx" && view == true)
                {
                    ScriptManager _scriptMan = ScriptManager.GetCurrent(this);
                    _scriptMan.AsyncPostBackTimeout = 36000;

                    //Bind_JobNo();
                    if (Request.QueryString["Id"] != null)
                    {
                        Gv_GetRows1();
                        SetInitialRow_For_Edit();
                        BindControl(Convert.ToInt32(Request.QueryString["Id"]));

                    }

                    else
                    {
                        SetInitialRow();
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }

        }
        Reload_JS();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataTable dt = PM.getFinancialYearByID(SBO.FinYearID);
        hdnMinDate.Value = SCGL_Common.CheckDateTime(dt.Rows[0]["yearFrom"]).ToShortDateString();
        hdnMaxDate.Value = SCGL_Common.CheckDateTime(dt.Rows[0]["YearTo"]).ToShortDateString();

    }

    //public void Bind_Job_Dropdown()
    //{
    //    for (int i = 0; i < GV_CommercialInvoiceDetail.Rows.Count; i++)
    //    {
    //        DropDownList ddlJobNo = GV_CommercialInvoiceDetail.Rows[i].FindControl("ddlJobNo") as DropDownList;
    //        if (Request.QueryString["ID"] != null)
    //        {

    //            DataTable dtInvoiceDetail = BALInvoice.getJobSheetDetail(SCGL_Common.Convert_ToInt(Request.QueryString["ID"]));

    //            InventoryID = SCGL_Common.Convert_ToInt(dtInvoiceDetail.Rows[0]["JobNo"].ToString());
    //        }
    //        SCGL_Common.Bind_DropDown(ddlJobNo, "vt_SCGL_BindJobNumber", "JobNumber", "ID");
            
    //    }

    //}

    public void Bind_PaymentThrough_Dropdown()
    {
        for (int i = 0; i < GV_CommercialInvoiceDetail.Rows.Count; i++)
        {
            DropDownList ddlPaymentThrough = GV_CommercialInvoiceDetail.Rows[i].FindControl("ddlPaymentThrough") as DropDownList;
            if (Request.QueryString["ID"] != null)
            {

                DataTable dtInvoiceDetail = BALInvoice.getExpenseSheetDetail(SCGL_Common.Convert_ToInt(Request.QueryString["ID"]));

                InventoryID = SCGL_Common.Convert_ToInt(dtInvoiceDetail.Rows[0]["PaymentThrough"].ToString());
            }
            SCGL_Common.Bind_DropDown(ddlPaymentThrough, "vt_SCGL_BindDepositAccount", "Title", "Code");
        }

    }

    public void Bind_Expense_Dropdown()
    {
        for (int i = 0; i < GV_CommercialInvoiceDetail.Rows.Count; i++)
        {
            DropDownList ddlExpense = GV_CommercialInvoiceDetail.Rows[i].FindControl("ddlExpense") as DropDownList;
            if (Request.QueryString["ID"] != null)
            {

                DataTable dtInvoiceDetail = BALInvoice.getExpenseSheetDetail(SCGL_Common.Convert_ToInt(Request.QueryString["ID"]));

                InventoryID = SCGL_Common.Convert_ToInt(dtInvoiceDetail.Rows[0]["ExpenseAcc"].ToString());
            }
            SCGL_Common.Bind_DropDown(ddlExpense, "vt_SCGL_BindExpenseAccount", "Title", "Code");
        }

    }

    public void Bind_Impressed_Dropdown()
    {
        for (int i = 0; i < GV_CommercialInvoiceDetail.Rows.Count; i++)
        {
            DropDownList ddlImpressed = GV_CommercialInvoiceDetail.Rows[i].FindControl("ddlImpressed") as DropDownList;
            if (Request.QueryString["ID"] != null)
            {

                DataTable dtInvoiceDetail = BALInvoice.getExpenseSheetDetail(SCGL_Common.Convert_ToInt(Request.QueryString["ID"]));

                InventoryID = SCGL_Common.Convert_ToInt(dtInvoiceDetail.Rows[0]["ImpressedAcc"].ToString());
            }
            SCGL_Common.Bind_DropDown(ddlImpressed, "vt_SCGL_BindImpressedAccount", "Title", "Code");
        }

    }


    public void BindControl(int Id)
    {
        if (Request.QueryString["view"] != null)
        {
            btnSave.Visible = false;
            btnAddLines.Visible = btnClearAllLines.Visible = false;

        }

        ExpenseSheet_BAL BALinvoice = new ExpenseSheet_BAL();
        DataTable dt = BALinvoice.getExpenseSheet(Id);
        if (dt.Rows.Count > 0)
        {

            txtDate.Text = SCGL_Common.CheckDateTime(dt.Rows[0]["Date"]).ToShortDateString();
            txttotal2.Value = dt.Rows[0]["Total"].ToString();

            btnSave.Text = "Update";
        }
        DataTable dtInvoiceDetail = BALInvoice.getExpenseSheetDetail(Id);

        for (int i = 0; i < dtInvoiceDetail.Rows.Count; i++)
        {

            //TextBox txtDate = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[1].FindControl("txtDate");
            //ddlJobNo = (DropDownList)GV_CommercialInvoiceDetail.Rows[i].Cells[1].FindControl("ddlJobNo");
            TextBox txtJobNo = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[1].FindControl("txtJobNo");
            DropDownList ddlPaymentType = (DropDownList)GV_CommercialInvoiceDetail.Rows[i].Cells[2].FindControl("ddlPaymentType");
            CheckBox chkClearing = (CheckBox)GV_CommercialInvoiceDetail.Rows[i].Cells[3].FindControl("chkClearing");
            DropDownList ddlInfoType = (DropDownList)GV_CommercialInvoiceDetail.Rows[i].Cells[4].FindControl("ddlInfoType");
            TextBox txtPONo = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[5].FindControl("txtPONo");
            DropDownList ddlImpressed = (DropDownList)GV_CommercialInvoiceDetail.Rows[i].Cells[6].FindControl("ddlImpressed");
            DropDownList ddlExpense = (DropDownList)GV_CommercialInvoiceDetail.Rows[i].Cells[7].FindControl("ddlExpense");
            DropDownList ddlPaymentThrough = (DropDownList)GV_CommercialInvoiceDetail.Rows[i].Cells[8].FindControl("ddlPaymentThrough");
            TextBox txtDescription = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[9].FindControl("txtDescription");
            TextBox txtAmount = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[10].FindControl("txtAmount");

            //txtDate.Text = SCGL_Common.CheckDateTime(dtInvoiceDetail.Rows[i]["Date"]).ToShortDateString();
            //ddlJobNo.SelectedValue = dtInvoiceDetail.Rows[i]["JobNo"].ToString();
            string jobnum ="";
            if (dtInvoiceDetail.Rows[i]["JobNo"].ToString() != "0")
            {
                jobnum = BALinvoice.getJobNObyID(SCGL_Common.Convert_ToInt(dtInvoiceDetail.Rows[i]["JobNo"].ToString()));
            }
            txtJobNo.Text = jobnum;
            ddlPaymentType.SelectedValue = dtInvoiceDetail.Rows[i]["PaymentType"].ToString();
            ddlInfoType.SelectedValue = dtInvoiceDetail.Rows[i]["InfoType"].ToString();
            txtPONo.Text = dtInvoiceDetail.Rows[i]["PONo"].ToString();
            chkClearing.Checked = SCGL_Common.CheckBoolean(dtInvoiceDetail.Rows[i]["IsCleared"].ToString());
            if (ddlPaymentType.SelectedValue == "1" || ddlPaymentType.SelectedValue == "2")
            {
                ddlImpressed.Enabled = false;
                ddlExpense.Enabled = false;
                ddlExpense.SelectedIndex = 0;
                chkClearing.Enabled = true;
            }
            else if (ddlPaymentType.SelectedValue == "4")
            {
                chkClearing.Enabled = true;
            }
            else if (ddlPaymentType.SelectedValue == "5")
            {
                ddlPaymentThrough.Enabled = false;
                ddlPaymentThrough.SelectedIndex = 0;
                chkClearing.Enabled = false;
            }
            else if (ddlPaymentType.SelectedValue == "6" || ddlPaymentType.SelectedValue == "13")
            {
                //txtPONo.Enabled = false;
                //txtPONo.Text = "";
                ddlImpressed.Enabled = false;
                ddlExpense.Enabled = true;
                ddlExpense.SelectedValue = dtInvoiceDetail.Rows[i]["ExpenseAcc"].ToString();
                chkClearing.Enabled = false;
            }
            else if (ddlPaymentType.SelectedValue == "7" || ddlPaymentType.SelectedValue == "9")
            {
                ddlExpense.Enabled = false;
                ddlExpense.SelectedIndex = 0;
                ddlImpressed.Enabled = true;
                ddlImpressed.SelectedValue = dtInvoiceDetail.Rows[i]["ImpressedAcc"].ToString();
                chkClearing.Enabled = false;
            }
            else if (ddlPaymentType.SelectedValue == "8")
            {
                ddlPaymentThrough.Enabled = false;
                ddlPaymentThrough.SelectedIndex = 0;
                ddlExpense.Enabled = true;
                ddlExpense.SelectedValue = dtInvoiceDetail.Rows[i]["ExpenseAcc"].ToString();
                ddlImpressed.Enabled = true;
                ddlImpressed.SelectedValue = dtInvoiceDetail.Rows[i]["ImpressedAcc"].ToString();
                chkClearing.Enabled = false;
            }
            else
            {
                ddlExpense.Enabled = false;
                 ddlImpressed.Enabled = false;
                 chkClearing.Enabled = false;
            }
            //txtPONo.Text = dtInvoiceDetail.Rows[i]["PONo"].ToString();
            //ddlExpense.SelectedValue = dtInvoiceDetail.Rows[i]["ExpenseAcc"].ToString();
            if (ddlPaymentType.SelectedValue != "8") 
            {
                ddlPaymentThrough.SelectedValue = dtInvoiceDetail.Rows[i]["PaymentThrough"].ToString();
            }
            
            txtDescription.Text = dtInvoiceDetail.Rows[i]["Description"].ToString();
            txtAmount.Text = dtInvoiceDetail.Rows[i]["Amount"].ToString();

            btnSave.Text = "Update";
        }
    }

    public void Bind_JobNo()
    {
        //SCGL_Common.Bind_DropDown(ddlJobNo, "vt_SCGL_BindJobNumber", "JobNumber", "ID");
    }




    public void Reload_JS()
    {
        SCGL_Common.ReloadJS(this, "MyDate();");
        //SCGL_Common.ReloadJS(this, "calculateSum();");
        //SCGL_Common.ReloadJS(this, "vale();");
        SCGL_Common.ReloadJS(this, "GrossTotalDeduction();");
        //SCGL_Common.ReloadJS(this, "TxtBlur();");
        SCGL_Common.ReloadJS(this, "TotalGridAmount();");
        //SCGL_Common.ReloadJS(this, "ChangeConversionRate();");

    }

    public void Gv_GetRows1()
    {
        SqlDataReader dr = BALInvoice.Get_Rows_ExpenseSheetDetail_byID(SCGL_Common.Convert_ToInt(Request.QueryString["ID"]));
        while (dr.Read())
        {
            Session["GV1"] = dr["RowNumber"].ToString();
        }
    }

    private void SetInitialRow_For_Edit()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        //dt.Columns.Add(new DataColumn("txtDate", typeof(string)));
        //dt.Columns.Add(new DataColumn("ddlJobNo", typeof(string)));
        //dt.Columns.Add(new DataColumn("ddlJobNo_value", typeof(string)));
        dt.Columns.Add(new DataColumn("txtJobNo", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlPaymentType", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlPaymentType_value", typeof(string)));
        dt.Columns.Add(new DataColumn("chkClearing", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlInfoType", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlInfoType_value", typeof(string)));
        dt.Columns.Add(new DataColumn("txtPONo", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlImpressed", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlImpressed_value", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlExpense", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlExpense_value", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlPaymentThrough", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlPaymentThrough_value", typeof(string)));
        dt.Columns.Add(new DataColumn("txtDescription", typeof(string)));
        dt.Columns.Add(new DataColumn("txtAmount", typeof(string)));

        if (Convert.ToInt32(Session["GV1"]) < 1)
        {
            for (int i = 0; i < 1; i++)
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
            }
        }
        else
        {
            for (int i = 0; i < Convert.ToInt32(Session["GV1"]); i++)
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
            }
        }
        ViewState["CurrentTable"] = dt;
        GV_CommercialInvoiceDetail.DataSource = dt;
        GV_CommercialInvoiceDetail.DataBind();
        //Bind_Product_Dropdown();
        //Bind_CostCenter_Dropdown();
        Bind_PaymentThrough_Dropdown();
        Bind_Expense_Dropdown();
        Bind_Impressed_Dropdown();
        //Bind_Job_Dropdown();
    }

    private void SetInitialRow()
    {

        DataTable dt = new DataTable();
        DataRow dr = null;
        //dt.Columns.Add(new DataColumn("txtDate", typeof(string)));
        //dt.Columns.Add(new DataColumn("ddlJobNo", typeof(string)));
        //dt.Columns.Add(new DataColumn("ddlJobNo_value", typeof(string)));
        dt.Columns.Add(new DataColumn("txtJobNo", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlPaymentType", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlPaymentType_value", typeof(string)));
        dt.Columns.Add(new DataColumn("chkClearing", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlInfoType", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlInfoType_value", typeof(string)));
        dt.Columns.Add(new DataColumn("txtPONo", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlImpressed", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlImpressed_value", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlExpense", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlExpense_value", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlPaymentThrough", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlPaymentThrough_value", typeof(string)));
        dt.Columns.Add(new DataColumn("txtDescription", typeof(string)));
        dt.Columns.Add(new DataColumn("txtAmount", typeof(string)));

        for (int i = 0; i < 1; i++)
        {
            dr = dt.NewRow();
            dt.Rows.Add(dr);
        }


        ViewState["CurrentTable"] = dt;



        GV_CommercialInvoiceDetail.DataSource = dt;
        GV_CommercialInvoiceDetail.DataBind();
        //Bind_Product_Dropdown();
        //Bind_CostCenter_Dropdown();
        Bind_PaymentThrough_Dropdown();
        Bind_Expense_Dropdown();
        Bind_Impressed_Dropdown();
    }

    protected void btnAddLines_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }

    public void AddNewRowToGrid()
    {
        try
        {
            Product_Table.Rows.Clear();
            for (int i = 0; i < GV_CommercialInvoiceDetail.Rows.Count; i++)
            {
                if (GV_CommercialInvoiceDetail.Rows[i].Visible)
                {
                    DataRow drCurrentRow = Product_Table.NewRow();

                    //TextBox txtDate = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[1].FindControl("txtDate");
                    TextBox txtJobNo = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[1].FindControl("txtJobNo");
                    //DropDownList ddlJobNo = (DropDownList)GV_CommercialInvoiceDetail.Rows[i].Cells[1].FindControl("ddlJobNo");
                    DropDownList ddlPaymentType = (DropDownList)GV_CommercialInvoiceDetail.Rows[i].Cells[2].FindControl("ddlPaymentType");
                    CheckBox chkClearing = (CheckBox)GV_CommercialInvoiceDetail.Rows[i].Cells[3].FindControl("chkClearing");
                    DropDownList ddlInfoType = (DropDownList)GV_CommercialInvoiceDetail.Rows[i].Cells[4].FindControl("ddlInfoType");
                    TextBox txtPONo = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[5].FindControl("txtPONo");
                    DropDownList ddlImpressed = (DropDownList)GV_CommercialInvoiceDetail.Rows[i].Cells[6].FindControl("ddlImpressed");
                    DropDownList ddlExpense = (DropDownList)GV_CommercialInvoiceDetail.Rows[i].Cells[7].FindControl("ddlExpense");
                    DropDownList ddlPaymentThrough = (DropDownList)GV_CommercialInvoiceDetail.Rows[i].Cells[8].FindControl("ddlPaymentThrough");
                    TextBox txtDescription = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[9].FindControl("txtDescription");
                    TextBox txtAmount = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[10].FindControl("txtAmount");

                    //drCurrentRow["txtDate"] = txtDate.Text;
                    //drCurrentRow["ddlJobNo"] = ddlJobNo.SelectedItem.Text;
                    //drCurrentRow["ddlJobNo_value"] = ddlJobNo.SelectedValue;
                    drCurrentRow["txtJobNo"] = txtJobNo.Text;
                    drCurrentRow["ddlPaymentType"] = ddlPaymentType.SelectedItem.Text;
                    drCurrentRow["ddlPaymentType_value"] = ddlPaymentType.SelectedValue;
                    drCurrentRow["chkClearing"] = chkClearing.Checked;
                    drCurrentRow["ddlInfoType"] = ddlInfoType.SelectedItem.Text;
                    drCurrentRow["ddlInfoType_value"] = ddlInfoType.SelectedValue;
                    drCurrentRow["txtPONo"] = txtPONo.Text;
                    drCurrentRow["ddlImpressed"] = ddlImpressed.SelectedItem.Text;
                    drCurrentRow["ddlImpressed_value"] = ddlImpressed.SelectedValue;
                    drCurrentRow["ddlExpense"] = ddlExpense.SelectedItem.Text;
                    drCurrentRow["ddlExpense_value"] = ddlExpense.SelectedValue;
                    //if (ddlPaymentType.SelectedValue == "1")
                    //{
                    //    txtPONo.Enabled = true;
                    //    //drCurrentRow["txtPONo"] = txtPONo.Text;
                    //    ddlExpense.Enabled = false;
                    //    //drCurrentRow["ddlExpense_value"] = 0;
                    //}
                    //else if (ddlPaymentType.SelectedValue == "2")
                    //{
                    //    txtPONo.Enabled = false;
                    //    //drCurrentRow["txtPONo"] = "";
                    //    ddlExpense.Enabled = true;
                    //    //drCurrentRow["ddlExpense"] = ddlExpense.SelectedItem.Text;
                    //    //drCurrentRow["ddlExpense_value"] = ddlExpense.SelectedValue;
                    //}
                    //else
                    //{
                    //    txtPONo.Enabled = true;
                    //    ddlExpense.Enabled = true;
                    //}
                    drCurrentRow["ddlPaymentThrough"] = ddlPaymentThrough.SelectedItem.Text;
                    drCurrentRow["ddlPaymentThrough_value"] = ddlPaymentThrough.SelectedValue;
                    drCurrentRow["txtDescription"] = txtDescription.Text;
                    drCurrentRow["txtAmount"] = txtAmount.Text;


                    Product_Table.Rows.Add(drCurrentRow);
                }
            }
            DataRow dr = Product_Table.NewRow();
            //dr[0] = "--Select Item--";
            Product_Table.Rows.Add(dr);
            GV_CommercialInvoiceDetail.DataSource = Product_Table;
            GV_CommercialInvoiceDetail.DataBind();
            Bind_PaymentThrough_Dropdown();
            Bind_Expense_Dropdown();
            Bind_Impressed_Dropdown();
            //Bind_Job_Dropdown();
            //Bind_Product_Dropdown();
            //Bind_CostCenter_Dropdown();
            //for (int i = 0; i < GV_CommercialInvoiceDetail.Rows.Count; i++)
            //{
            //    DropDownList cbox = GV_CommercialInvoiceDetail.Rows[i].FindControl("ddlInventory") as DropDownList;
            //    cbox.SelectedIndex = cbox.Items.IndexOf(cbox.Items.FindByText(Product_Table.Rows[i]["ddlInventory"].ToString()));
            //}
            //for (int i = 0; i < GV_CommercialInvoiceDetail.Rows.Count; i++)
            //{
            //    DropDownList cbox = GV_CommercialInvoiceDetail.Rows[i].FindControl("ddlCostCenter") as DropDownList;
            //    cbox.SelectedIndex = cbox.Items.IndexOf(cbox.Items.FindByText(Product_Table.Rows[i]["ddlCostCenter"].ToString()));
            //}
            //for (int i = 0; i < GV_CommercialInvoiceDetail.Rows.Count; i++)
            //{
            //    DropDownList cbox = GV_CommercialInvoiceDetail.Rows[i].FindControl("ddlJobNo") as DropDownList;
            //    cbox.SelectedIndex = cbox.Items.IndexOf(cbox.Items.FindByText(Product_Table.Rows[i]["ddlJobNo"].ToString()));
            //}
            for (int i = 0; i < GV_CommercialInvoiceDetail.Rows.Count; i++)
            {
                DropDownList cbox = GV_CommercialInvoiceDetail.Rows[i].FindControl("ddlPaymentType") as DropDownList;
                cbox.SelectedIndex = cbox.Items.IndexOf(cbox.Items.FindByText(Product_Table.Rows[i]["ddlPaymentType"].ToString()));
            }
            for (int i = 0; i < GV_CommercialInvoiceDetail.Rows.Count; i++)
            {
                DropDownList cbox = GV_CommercialInvoiceDetail.Rows[i].FindControl("ddlInfoType") as DropDownList;
                cbox.SelectedIndex = cbox.Items.IndexOf(cbox.Items.FindByText(Product_Table.Rows[i]["ddlInfoType"].ToString()));
            }
            for (int i = 0; i < GV_CommercialInvoiceDetail.Rows.Count; i++)
            {
                DropDownList cbox = GV_CommercialInvoiceDetail.Rows[i].FindControl("ddlImpressed") as DropDownList;
                cbox.SelectedIndex = cbox.Items.IndexOf(cbox.Items.FindByText(Product_Table.Rows[i]["ddlImpressed"].ToString()));
            }
            for (int i = 0; i < GV_CommercialInvoiceDetail.Rows.Count; i++)
            {
                DropDownList cbox = GV_CommercialInvoiceDetail.Rows[i].FindControl("ddlExpense") as DropDownList;
                cbox.SelectedIndex = cbox.Items.IndexOf(cbox.Items.FindByText(Product_Table.Rows[i]["ddlExpense"].ToString()));
            }
            for (int i = 0; i < GV_CommercialInvoiceDetail.Rows.Count; i++)
            {
                DropDownList cbox = GV_CommercialInvoiceDetail.Rows[i].FindControl("ddlPaymentThrough") as DropDownList;
                cbox.SelectedIndex = cbox.Items.IndexOf(cbox.Items.FindByText(Product_Table.Rows[i]["ddlPaymentThrough"].ToString()));
            }

            for (int i = 0; i < GV_CommercialInvoiceDetail.Rows.Count; i++)
            {
                DropDownList ddlPaymentType = (DropDownList)GV_CommercialInvoiceDetail.Rows[i].Cells[2].FindControl("ddlPaymentType");
                CheckBox chkClearing = (CheckBox)GV_CommercialInvoiceDetail.Rows[i].Cells[3].FindControl("chkClearing");
                TextBox txtPONo = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[5].FindControl("txtPONo");
                DropDownList ddlImpressed = (DropDownList)GV_CommercialInvoiceDetail.Rows[i].Cells[6].FindControl("ddlImpressed");
                DropDownList ddlExpense = (DropDownList)GV_CommercialInvoiceDetail.Rows[i].Cells[7].FindControl("ddlExpense");
                DropDownList ddlPaymentThrough = (DropDownList)GV_CommercialInvoiceDetail.Rows[i].Cells[8].FindControl("ddlPaymentThrough");
                if (ddlPaymentType.SelectedValue == "1" || ddlPaymentType.SelectedValue == "2")
                {
                    ddlImpressed.Enabled = false;
                    ddlExpense.Enabled = false;
                    chkClearing.Enabled = true;
                }
                else if (ddlPaymentType.SelectedValue == "4")
                {
                    chkClearing.Enabled = true;
                }
                else if (ddlPaymentType.SelectedValue == "6" || ddlPaymentType.SelectedValue == "13")
                {
                    ddlImpressed.Enabled = false;
                    ddlExpense.Enabled = true;
                    chkClearing.Enabled = false;
                }
                else if (ddlPaymentType.SelectedValue == "7" || ddlPaymentType.SelectedValue == "9")
                {
                    ddlExpense.Enabled = false;
                    ddlImpressed.Enabled = true;
                    chkClearing.Enabled = false;
                }
                else if (ddlPaymentType.SelectedValue == "8")
                {
                    ddlPaymentThrough.Enabled = false;
                    ddlExpense.Enabled = true;
                    ddlImpressed.Enabled = true;
                    chkClearing.Enabled = false;
                }
                else
                {
                    ddlImpressed.Enabled = false;
                    ddlExpense.Enabled = false;
                    chkClearing.Enabled = false;
                }
            }

        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable Product_DataSource()
    {
        DataTable dtProduct = Product_Table; dtProduct.Rows.Clear();
        for (int i = 0; i < GV_CommercialInvoiceDetail.Rows.Count; i++)
        {
            if (GV_CommercialInvoiceDetail.Rows[i].Visible)
            {
                //TextBox txtDate = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[1].FindControl("txtDate");
                TextBox txtJobNo = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[1].FindControl("txtJobNo");
                //DropDownList ddlJobNo = (DropDownList)GV_CommercialInvoiceDetail.Rows[i].Cells[1].FindControl("ddlJobNo");
                DropDownList ddlPaymentType = (DropDownList)GV_CommercialInvoiceDetail.Rows[i].Cells[2].FindControl("ddlPaymentType");
                CheckBox chkClearing = (CheckBox)GV_CommercialInvoiceDetail.Rows[i].Cells[3].FindControl("chkClearing");
                DropDownList ddlInfoType = (DropDownList)GV_CommercialInvoiceDetail.Rows[i].Cells[4].FindControl("ddlInfoType");
                TextBox txtPONo = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[5].FindControl("txtPONo");
                DropDownList ddlImpressed = (DropDownList)GV_CommercialInvoiceDetail.Rows[i].Cells[6].FindControl("ddlImpressed");
                DropDownList ddlExpense = (DropDownList)GV_CommercialInvoiceDetail.Rows[i].Cells[7].FindControl("ddlExpense");
                DropDownList ddlPaymentThrough = (DropDownList)GV_CommercialInvoiceDetail.Rows[i].Cells[8].FindControl("ddlPaymentThrough");
                TextBox txtDescription = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[9].FindControl("txtDescription");
                TextBox txtAmount = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[10].FindControl("txtAmount");


                dtProduct.Rows.Add(dtProduct.NewRow());

                //dtProduct.Rows[i]["txtDate"] = txtDate.Text;
                //dtProduct.Rows[i]["ddlJobNo"] = ddlJobNo.SelectedItem.Text;
                //dtProduct.Rows[i]["ddlJobNo_value"] = ddlJobNo.SelectedValue;
                dtProduct.Rows[i]["txtJobNo"] = txtJobNo.Text;
                dtProduct.Rows[i]["ddlPaymentType"] = ddlPaymentType.SelectedItem.Text;
                dtProduct.Rows[i]["ddlPaymentType_value"] = ddlPaymentType.SelectedValue;
                dtProduct.Rows[i]["chkClearing"] = chkClearing.Checked;
                dtProduct.Rows[i]["ddlInfoType"] = ddlInfoType.SelectedItem.Text;
                dtProduct.Rows[i]["ddlInfoType_value"] = ddlInfoType.SelectedValue;
                dtProduct.Rows[i]["txtPONo"] = txtPONo.Text;
                dtProduct.Rows[i]["ddlImpressed"] = ddlImpressed.SelectedItem.Text;
                dtProduct.Rows[i]["ddlImpressed_value"] = ddlImpressed.SelectedValue;
                dtProduct.Rows[i]["ddlExpense"] = ddlExpense.SelectedItem.Text;
                dtProduct.Rows[i]["ddlExpense_value"] = ddlExpense.SelectedValue;
                dtProduct.Rows[i]["ddlPaymentThrough"] = ddlPaymentThrough.SelectedItem.Text;
                dtProduct.Rows[i]["ddlPaymentThrough_value"] = ddlPaymentThrough.SelectedValue;
                dtProduct.Rows[i]["txtDescription"] = txtDescription.Text;
                dtProduct.Rows[i]["txtAmount"] = txtAmount.Text;


            }
            else
            {
                dtProduct.Rows.Add(dtProduct.NewRow());
            }
        }
        for (int pr = 0; pr < Product_Table.Rows.Count; pr++)
        {
            //if (Product_Table.Rows[pr]["ddlInventory"].ToString() == "" || Product_Table.Rows[pr]["txtDescription"].ToString() == "" || Product_Table.Rows[pr]["txtItemSize"].ToString() == "")
            //{
            //    Product_Table.Rows.RemoveAt(pr);
            //    pr--;
            //}

            if (Product_Table.Rows[pr]["ddlPaymentType"].ToString() == "")
            {
                Product_Table.Rows.RemoveAt(pr);
                pr--;
            }


        }

        return dtProduct;
    }

    private DataTable Product_Table
    {
        get
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt == null)
            {

                //dt.Columns.Add(new DataColumn("txtDate", typeof(string)));
                //dt.Columns.Add(new DataColumn("ddlJobNo", typeof(string)));
                //dt.Columns.Add(new DataColumn("ddlJobNo_value", typeof(string)));
                dt.Columns.Add(new DataColumn("txtJobNo", typeof(string)));
                dt.Columns.Add(new DataColumn("ddlPaymentType", typeof(string)));
                dt.Columns.Add(new DataColumn("ddlPaymentType_value", typeof(string)));
                dt.Columns.Add(new DataColumn("chkClearing", typeof(string)));
                dt.Columns.Add(new DataColumn("ddlInfoType", typeof(string)));
                dt.Columns.Add(new DataColumn("ddlInfoType_value", typeof(string)));
                dt.Columns.Add(new DataColumn("txtPONo", typeof(string)));
                dt.Columns.Add(new DataColumn("ddlImpressed", typeof(string)));
                dt.Columns.Add(new DataColumn("ddlImpressed_value", typeof(string)));
                dt.Columns.Add(new DataColumn("ddlExpense", typeof(string)));
                dt.Columns.Add(new DataColumn("ddlExpense_value", typeof(string)));
                dt.Columns.Add(new DataColumn("ddlPaymentThrough", typeof(string)));
                dt.Columns.Add(new DataColumn("ddlPaymentThrough_value", typeof(string)));
                dt.Columns.Add(new DataColumn("txtDescription", typeof(string)));
                dt.Columns.Add(new DataColumn("txtAmount", typeof(string)));

                for (int i = 0; i < 10; i++)
                {
                    dt.Rows.Add(dt.NewRow());
                }
            }
            ViewState["CurrentTable"] = dt;
            return dt;
        }
        set
        {
            ViewState["CurrentTable"] = value;
        }
    }

    protected void GV_CommercialInvoiceDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = e.RowIndex;
        if (index != 0)
        {
            GV_CommercialInvoiceDetail.Rows[e.RowIndex].Visible = false;

            //TextBox txtDate = (TextBox)GV_CommercialInvoiceDetail.Rows[e.RowIndex].Cells[1].FindControl("txtDate");
            TextBox txtJobNo = (TextBox)GV_CommercialInvoiceDetail.Rows[e.RowIndex].Cells[1].FindControl("txtJobNo");
            //DropDownList ddlJobNo = (DropDownList)GV_CommercialInvoiceDetail.Rows[e.RowIndex].Cells[1].FindControl("ddlJobNo");
            DropDownList ddlPaymentType = (DropDownList)GV_CommercialInvoiceDetail.Rows[e.RowIndex].Cells[2].FindControl("ddlPaymentType");
            CheckBox chkClearing = (CheckBox)GV_CommercialInvoiceDetail.Rows[e.RowIndex].Cells[3].FindControl("chkClearing");
            DropDownList ddlInfoType = (DropDownList)GV_CommercialInvoiceDetail.Rows[e.RowIndex].Cells[4].FindControl("ddlInfoType");
            TextBox txtPONo = (TextBox)GV_CommercialInvoiceDetail.Rows[e.RowIndex].Cells[5].FindControl("txtPONo");
            DropDownList ddlImpressed = (DropDownList)GV_CommercialInvoiceDetail.Rows[e.RowIndex].Cells[6].FindControl("ddlImpressed");
            DropDownList ddlExpense = (DropDownList)GV_CommercialInvoiceDetail.Rows[e.RowIndex].Cells[7].FindControl("ddlExpense");
            DropDownList ddlPaymentThrough = (DropDownList)GV_CommercialInvoiceDetail.Rows[e.RowIndex].Cells[8].FindControl("ddlPaymentThrough");
            TextBox txtDescription = (TextBox)GV_CommercialInvoiceDetail.Rows[e.RowIndex].Cells[9].FindControl("txtDescription");
            TextBox txtAmount = (TextBox)GV_CommercialInvoiceDetail.Rows[e.RowIndex].Cells[10].FindControl("txtAmount");

            if (txtAmount.Text == "") { txtAmount.Text = "0"; }

            //txtDate.Text = "";
            //ddlJobNo.SelectedIndex = 0;
            txtJobNo.Text = "";
            ddlPaymentType.SelectedIndex = 0;
            chkClearing.Checked = false;
            ddlInfoType.SelectedIndex = 0;
            txtPONo.Text = "";
            ddlImpressed.SelectedIndex = 0;
            ddlExpense.SelectedIndex = 0;
            ddlPaymentThrough.SelectedIndex = 0;
            txtDescription.Text = "";
            txtAmount.Text = "";
            Reload_JS();
        }



    }


    public DataTable Record_for_Insert()
    {
        Product_Table = Product_DataSource();


        int Product_Rows = Product_Table.Rows.Count;


        int TotalRows = 0;
        if (Product_Rows > 0)
        {
            TotalRows = Product_Rows;
        }


        DataTable dtInsert = new DataTable();
        dtInsert.Merge(Product_Table);

        dtInsert.Rows.Clear();
        for (int r = 0; r < TotalRows; r++)
        {
            dtInsert.Rows.Add(dtInsert.NewRow());
        }
        for (int p = 0; p < Product_Rows; p++)
        {
            if (Product_Table.Rows[p]["ddlPaymentType"].ToString() != null && Product_Table.Rows[p]["ddlPaymentType"].ToString() != "")
            {
                //dtInsert.Rows[p]["txtDate"] = Product_Table.Rows[p]["txtDate"].ToString();
                //dtInsert.Rows[p]["ddlJobNo"] = Product_Table.Rows[p]["ddlJobNo"].ToString();
                //dtInsert.Rows[p]["ddlJobNo_value"] = Product_Table.Rows[p]["ddlJobNo_value"].ToString();
                dtInsert.Rows[p]["txtJobNo"] = Product_Table.Rows[p]["txtJobNo"].ToString();
                dtInsert.Rows[p]["ddlPaymentType"] = Product_Table.Rows[p]["ddlPaymentType"].ToString();
                dtInsert.Rows[p]["ddlPaymentType_value"] = Product_Table.Rows[p]["ddlPaymentType_value"].ToString();
                dtInsert.Rows[p]["chkClearing"] = Product_Table.Rows[p]["chkClearing"].ToString();
                dtInsert.Rows[p]["ddlInfoType"] = Product_Table.Rows[p]["ddlInfoType"].ToString();
                dtInsert.Rows[p]["ddlInfoType_value"] = Product_Table.Rows[p]["ddlInfoType_value"].ToString();
                dtInsert.Rows[p]["txtPONo"] = Product_Table.Rows[p]["txtPONo"].ToString();
                dtInsert.Rows[p]["ddlImpressed"] = Product_Table.Rows[p]["ddlImpressed"].ToString();
                dtInsert.Rows[p]["ddlImpressed_value"] = Product_Table.Rows[p]["ddlImpressed_value"].ToString();
                dtInsert.Rows[p]["ddlExpense"] = Product_Table.Rows[p]["ddlExpense"].ToString();
                dtInsert.Rows[p]["ddlExpense_value"] = Product_Table.Rows[p]["ddlExpense_value"].ToString();
                dtInsert.Rows[p]["ddlPaymentThrough"] = Product_Table.Rows[p]["ddlPaymentThrough"].ToString();
                dtInsert.Rows[p]["ddlPaymentThrough_value"] = Product_Table.Rows[p]["ddlPaymentThrough_value"].ToString();
                dtInsert.Rows[p]["txtDescription"] = Product_Table.Rows[p]["txtDescription"].ToString();
                dtInsert.Rows[p]["txtAmount"] = Product_Table.Rows[p]["txtAmount"].ToString();

            }


        }

        return dtInsert;
    }

    protected void btnClearAllLines_Click(object sender, EventArgs e)
    {
        SetInitialRow();
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            if (SBO.Can_Insert == true)
            {
                if (Check_Validation())
                {
                    if (Insert_Invoice())
                    {
                        if (btnSave.Text == "Save")
                        {
                            btnSave.Visible = false;
                            lblSuccessMsg.InnerHtml = "Expense Sheet Created Successfully";
                        }
                        else
                        {
                            lblSuccessMsg.InnerHtml = "Expense Sheet Updated Successfully";
                        }
                        SCGL_Common.Success_Message(this.Page, "ExpenseSheetForm_Views.aspx");
                    }
                }
            }
            else
                JQ.showStatusMsg(this, "3", "User not Allowed to Insert New Record");
        }
        catch (Exception ex)
        {
            lblNewError.InnerHtml = ex.Message;
            SCGL_Common.Error_Message(this.Page);
        }
    }

    public bool Insert_Invoice()
    {
        SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString);
        con.Open();
        SqlTransaction trans = con.BeginTransaction();
        

        bool isCreated = false;
        decimal CogRate = 0;
        try
        {
            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
           
            if (btnSave.Text == "Save")
            {
                BALInvoice.ExpenseID = -1;
            }
            else
            {
                BALInvoice.ExpenseID = SCGL_Common.Convert_ToInt(Request.QueryString["Id"]);
            }


            //BALInvoice.JobNo = SCGL_Common.Convert_ToInt(ddlJobNo.SelectedValue);
            //BALInvoice.CustomerID = SCGL_Common.Convert_ToInt(hdnCustomerID.Value);
            //BALInvoice.Description2 = txtDescription2.Text;
            //BALInvoice.OtherDetails = txtOtherDetails.Text;
            BALInvoice.Date = SCGL_Common.CheckDateTime(txtDate.Text);
            BALInvoice.Total = SCGL_Common.Convert_ToDecimal(txttotal2.Value);
            BALInvoice.FinYearID = SBO.FinYearID;

            int ExpenseID = BALInvoice.CreateModifyExpenseSheet(BALInvoice, trans);
            if (ExpenseID > 0)
            {
                BALInvoice.ExpenseID = SCGL_Common.Convert_ToInt(ExpenseID);
                if (btnSave.Text == "Update")
                {
                    BALInvoice.Delete_ExpenseSheetDetail(BALInvoice.ExpenseID, trans);
                    BALInvoice.Delete_ExpenseSheetTrans(BALInvoice.ExpenseID, trans);
                }
                //trans.Commit();
                int Counter = 0;
                DataTable dt = Record_for_Insert();
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    BALInvoice.ExpenseID = SCGL_Common.Convert_ToInt(ExpenseID);
                    //BALInvoice.Date = SCGL_Common.CheckDateTime(dt.Rows[i]["txtDate"].ToString());
                    int jobId= BALInvoice.getJobIDbyJobNo(dt.Rows[i]["txtJobNo"].ToString());
                    BALInvoice.JobID = jobId;
                    BALInvoice.PaymentType = SCGL_Common.Convert_ToInt(dt.Rows[i]["ddlPaymentType_value"].ToString());
                    BALInvoice.Clearing = SCGL_Common.CheckBoolean(dt.Rows[i]["chkClearing"].ToString());
                    BALInvoice.InfoType = SCGL_Common.Convert_ToInt(dt.Rows[i]["ddlInfoType_value"].ToString());
                    BALInvoice.PONo = dt.Rows[i]["txtPONo"].ToString();
                    BALInvoice.ImpressedAcc = dt.Rows[i]["ddlImpressed_value"].ToString();
                    BALInvoice.ExpenseAcc = dt.Rows[i]["ddlExpense_value"].ToString();
                    BALInvoice.PaymentThrough = dt.Rows[i]["ddlPaymentThrough_value"].ToString();
                    BALInvoice.Description = dt.Rows[i]["txtDescription"].ToString();
                    BALInvoice.Amount = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtAmount"].ToString());

                    if (BALInvoice.CreateModifyExpenseSheetDetail(BALInvoice, trans))
                    {

                        Counter++;
                    }
                    else
                    {
                        Counter = 0;
                        break;
                    }
                }
                //BALInvoice.GetCOGS(BALInvoice, trans);
                if (Counter > 0)
                {
                    trans.Commit();
                    isCreated = true;
                }
                else
                {
                    trans.Rollback();
                    isCreated = false;
                }
            }
            else
            {
                isCreated = false;
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch (Exception e)
        {
            trans.Rollback();
            isCreated = false;
            throw;
        }
        return isCreated;
    }

    public bool Check_Validation()
    {
        bool IsValid = true;


        //for (int i = 0; i < GV_CommercialInvoiceDetail.Rows.Count; i++)
        //{
        //    DropDownList ddlProduct = GV_CommercialInvoiceDetail.Rows[i].FindControl("ddlInventory") as DropDownList;
        //    TextBox txtDescription = (TextBox)GV_CommercialInvoiceDetail.Rows[i].FindControl("txtDescription");
        //    TextBox txtQuantity = (TextBox)GV_CommercialInvoiceDetail.Rows[i].FindControl("txtQuantity");
        //    TextBox txtRate = (TextBox)GV_CommercialInvoiceDetail.Rows[i].FindControl("txtRate");
        //    DropDownList ddlCostCenter = GV_CommercialInvoiceDetail.Rows[i].FindControl("ddlCostCenter") as DropDownList;
        //    if (ddlProduct.SelectedValue != "0")
        //    {
        //        if (ddlCostCenter.SelectedValue == "0")
        //        {
        //            SCGL_Common.Error_Message(this);
        //            return false;
        //        }
        //    }

        //}

        return IsValid;
    }





    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExpenseSheetForm_Views.aspx");
    }

    

    protected void ddlPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //int index = SCGL_Common.Convert_ToInt(e);
        //if (e != 0)
        //{

        DropDownList ddl = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddl.Parent.Parent;
        int idx = row.RowIndex;

        DropDownList ddlPaymentType = (DropDownList)row.Cells[0].FindControl("ddlPaymentType");
        CheckBox chkClearing = (CheckBox)row.Cells[0].FindControl("chkClearing");
        TextBox txtPONo = (TextBox)row.Cells[0].FindControl("txtPONo");
        DropDownList ddlImpressed = (DropDownList)row.Cells[0].FindControl("ddlImpressed");
        DropDownList ddlExpense = (DropDownList)row.Cells[0].FindControl("ddlExpense");
        DropDownList ddlPaymentThrough = (DropDownList)row.Cells[0].FindControl("ddlPaymentThrough");

        //DropDownList ddlPaymentType = (DropDownList)GV_CommercialInvoiceDetail.Rows[e].Cells[2].FindControl("ddlPaymentType");
        ////DropDownList ddlInfoType = (DropDownList)GV_CommercialInvoiceDetail.Rows[index].Cells[3].FindControl("ddlInfoType");
        //TextBox txtPONo = (TextBox)GV_CommercialInvoiceDetail.Rows[e].Cells[4].FindControl("txtPONo");
        //DropDownList ddlExpense = (DropDownList)GV_CommercialInvoiceDetail.Rows[index].Cells[5].FindControl("ddlExpense");



        if (ddlPaymentType.SelectedValue == "1" || ddlPaymentType.SelectedValue == "2")
        {
            ddlImpressed.Enabled = false;
            ddlExpense.Enabled = false;
            ddlExpense.SelectedIndex = 0;
            chkClearing.Enabled = true;
        }
        else if (ddlPaymentType.SelectedValue == "4")
        {
            chkClearing.Enabled = true;
        }
        else if (ddlPaymentType.SelectedValue == "6" || ddlPaymentType.SelectedValue == "13")
        {
            //txtPONo.Enabled = false;
            ddlImpressed.Enabled = false;
            ddlExpense.Enabled = true;
            chkClearing.Enabled = false;
        }
        else if (ddlPaymentType.SelectedValue == "7" || ddlPaymentType.SelectedValue == "9")
        {
            ddlExpense.Enabled = false;
            ddlImpressed.Enabled = true;
            chkClearing.Enabled = false;
        }
        else if (ddlPaymentType.SelectedValue == "8")
        {
            ddlPaymentThrough.Enabled = false;
            ddlExpense.Enabled = true;
            ddlImpressed.Enabled = true;
            chkClearing.Enabled = false;
        }
        else
        {
            ddlImpressed.Enabled = false;
            ddlExpense.Enabled = false;
            chkClearing.Enabled = false;

        }
        //}

       
    }

    protected void txtJobNo_TextChanged(object sender, EventArgs e)
    {
        TextBox ddl = (TextBox)sender;
        GridViewRow row = (GridViewRow)ddl.Parent.Parent;
        int idx = row.RowIndex;

        TextBox txtJobNo = (TextBox)row.Cells[0].FindControl("txtJobNo");
        int CountJobNo = BALInvoice.CheckJobNo(txtJobNo.Text);
        if (CountJobNo > 0)
        { }
        else 
        {
            JQ.showStatusMsg(this, "2", "Job Number Does not Exist in the records");
            txtJobNo.Text = "";
        }

    }

    protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlJobNo.SelectedValue != "0")
        //{
        //    DataTable dt = BALInvoice.getCustomerNamebyJobNumber(SCGL_Common.Convert_ToInt(ddlJobNo.SelectedValue));
        //    txtCustomer.Text = dt.Rows[0]["DisplayName"].ToString();
        //    hdnCustomerID.Value = dt.Rows[0]["CustomerID"].ToString();
        //}
    }

    protected void btnFind_Click1(object sender, EventArgs e)
    {
        JQ.showDialog(this, "FindAccount");
        hdnRowIndex.Value = (((sender as LinkButton).NamingContainer) as GridViewRow).RowIndex.ToString();
        lblRow.Text = (((sender as LinkButton).NamingContainer) as GridViewRow).RowIndex.ToString();
    }

    protected void btnFindAcc_Click(object sender, EventArgs e)
    {

        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataTable dt = BALInvoice.GetJobNo(txtAccountNo.Text);
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

    protected void GrdAccounts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        GrdAccounts.DataSource = BALInvoice.GetJobNo(txtAccountNo.Text);
        GrdAccounts.PageIndex = e.NewPageIndex;
        GrdAccounts.DataBind();

    }

    protected void lnkSelect_Click(object sender, EventArgs e)
    {
        var row = (GridViewRow)((Control)sender).NamingContainer;
        var rowIndex = row.RowIndex;

        var no = int.Parse(lblRow.Text);

        TextBox txtJobNo = (TextBox)GV_CommercialInvoiceDetail.Rows[no].Cells[1].FindControl("txtJobNo");
        txtJobNo.Text = GrdAccounts.Rows[rowIndex].Cells[1].Text.ToString();
        //((TextBox)GV_CommercialInvoiceDetail.FindControl("txtJobNo")).Text = GrdAccounts.Rows[rowIndex].Cells[1].Text.ToString();

        JQ.closeDialog(this, "FindAccount");
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        int CountAlreadyExistDate = BALInvoice.CheckAlreadyDateExist(SCGL_Common.CheckDateTime(txtDate.Text), Convert.ToInt32(Request.QueryString["Id"]));
        if (CountAlreadyExistDate > 0)
        {
            JQ.showStatusMsg(this, "2", "Date Already Exist So please Edit the Expense Sheet With this Date");
            txtDate.Text = "";
        }
        else
        {  
        }
    }
}
