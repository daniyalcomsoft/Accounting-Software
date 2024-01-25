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

public partial class PurchaseInvoice_Temp : System.Web.UI.Page
{
    PurchaseInvoice_BAL_Temp BALInvoice = new PurchaseInvoice_BAL_Temp();
    int InventoryID = 0;
    int CostCenterID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionBO"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        Reload_JS();

        PurchaseInvoice_BAL_Temp BALInvoice = new PurchaseInvoice_BAL_Temp();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataTable dt = PM.getFinancialYearByID(SBO.FinYearID);
        hdnMinDate.Value = SCGL_Common.CheckDateTime(dt.Rows[0]["yearFrom"]).ToShortDateString();
        hdnMaxDate.Value = SCGL_Common.CheckDateTime(dt.Rows[0]["YearTo"]).ToShortDateString();
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "PurchaseInvoice_Temp.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "PurchaseInvoice_Temp.aspx" && view == true)
                {
                    GetMaxInvoiceId();
                    Bind_Vendor();
                    Bind_Terms();

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
    }

    public void Bind_Product_Dropdown()
    {
        for (int i = 0; i < GV_PurchaseInvoiceDetail.Rows.Count; i++)
        {
            DropDownList ddlProduct = GV_PurchaseInvoiceDetail.Rows[i].FindControl("ddlInventory") as DropDownList;
            if (Request.QueryString["ID"] != null)
            {
                //InvoiceDetail_BAL inv_dtl = new InvoiceDetail_BAL();
                DataTable dtInvoiceDetail = BALInvoice.getPurchaseInvoiceDetail(SCGL_Common.Convert_ToInt(Request.QueryString["ID"]));
                //DataTable dtInvoiceDetail = BALInvoice.getInvoiceDetail(Id);

                InventoryID = SCGL_Common.Convert_ToInt(dtInvoiceDetail.Rows[0]["InventoryID"].ToString());
            }
            SCGL_Common.Bind_DropDown(ddlProduct, "vt_SCGL_Sp_GetInventory", "InventoryName", "InventoryID");
        }

    }

    public void Bind_CostCenter_Dropdown()
    {
        for (int i = 0; i < GV_PurchaseInvoiceDetail.Rows.Count; i++)
        {
            DropDownList ddlCostCenter = GV_PurchaseInvoiceDetail.Rows[i].FindControl("ddlCostCenter") as DropDownList;
            if (Request.QueryString["ID"] != null)
            {
                //InvoiceDetail_BAL inv_dtl = new InvoiceDetail_BAL();
                DataTable dtInvoiceDetail = BALInvoice.getPurchaseInvoiceDetail(SCGL_Common.Convert_ToInt(Request.QueryString["ID"]));
                //DataTable dtInvoiceDetail = BALInvoice.getInvoiceDetail(Id);

                CostCenterID = SCGL_Common.Convert_ToInt(dtInvoiceDetail.Rows[0]["CostCenterID"].ToString());
            }
            SCGL_Common.Bind_DropDown(ddlCostCenter, "vt_SCGL_Sp_GetCostCenter", "CostCenter", "CostCenterID");
        }

    }

    public void BindControl(int Id)
    {
        if (Request.QueryString["view"] != null)
        {
            btnSave.Visible = false;
            
        }
        PurchaseInvoice_BAL_Temp invoiceBal = new PurchaseInvoice_BAL_Temp();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataTable dt = invoiceBal.getInvoiceByID(Id,SBO.FinYearID);
        if (dt.Rows.Count > 0)
        {
            ddlVendor.SelectedValue = dt.Rows[0]["VendorID"].ToString();
            txtEmail.Text = dt.Rows[0]["Email"].ToString();
            txtBillingAddress.Text = dt.Rows[0]["BillingAddress"].ToString();
            txtPurchaseID.Text = dt.Rows[0]["pInvoiceID"].ToString();
            txtInvoiceNumber.Text = dt.Rows[0]["InvoiceNo"].ToString();
            //txtTerms.Text = dt.Rows[0]["TermID"].ToString();
            DDLTerms.SelectedValue = dt.Rows[0]["TermID"].ToString();
            txtInvoiceDate.Text = SCGL_Common.CheckDateTime(dt.Rows[0]["InvoiceDate"]).ToShortDateString();
            //txtInvoiceDate.Text = dt.Rows[0]["InvoiceDate"].ToString();
            txtDueDate.Text = SCGL_Common.CheckDateTime(dt.Rows[0]["DueDate"]).ToShortDateString();
            //txtDueDate.Text = dt.Rows[0]["DueDate"].ToString();
            txtTot.Value = dt.Rows[0]["Total"].ToString();
            txtDiscount.Text = dt.Rows[0]["Discount"].ToString();
            txtPrintMessage.Text = dt.Rows[0]["PrintMessage"].ToString();
            txtStatementMemo.Text = dt.Rows[0]["StatementMemo"].ToString();
            btnSave.Text = "Update";
        }
        DataTable dtInvoiceDetail = BALInvoice.getPurchaseInvoiceDetail(Id);
      
        for (int i = 0; i < dtInvoiceDetail.Rows.Count; i++)
        {

            DropDownList ddlInventory = (DropDownList)GV_PurchaseInvoiceDetail.Rows[i].Cells[1].FindControl("ddlInventory");
            TextBox txtDescription = (TextBox)GV_PurchaseInvoiceDetail.Rows[i].Cells[2].FindControl("txtDescription");
            TextBox txtQuantity = (TextBox)GV_PurchaseInvoiceDetail.Rows[i].Cells[3].FindControl("txtQuantity");
            TextBox txtRate = (TextBox)GV_PurchaseInvoiceDetail.Rows[i].Cells[4].FindControl("txtRate");
            HtmlInputText txtAmount = GV_PurchaseInvoiceDetail.Rows[i].Cells[5].FindControl("txtAmount") as HtmlInputText;
            DropDownList ddlCostCenter = (DropDownList)GV_PurchaseInvoiceDetail.Rows[i].Cells[6].FindControl("ddlCostCenter");

            ddlInventory.SelectedValue = dtInvoiceDetail.Rows[i]["InventoryID"].ToString();
            txtDescription.Text = dtInvoiceDetail.Rows[i]["Description"].ToString();
            txtQuantity.Text = dtInvoiceDetail.Rows[i]["Quantity"].ToString();
            txtRate.Text = dtInvoiceDetail.Rows[i]["Rate"].ToString();
            txtAmount.Value = dtInvoiceDetail.Rows[i]["Amount"].ToString();
            ddlCostCenter.SelectedValue = dtInvoiceDetail.Rows[i]["CostCenterID"].ToString();

            btnSave.Text = "Update";
        }
    }

    public void Bind_Vendor()
    {
        SCGL_Common.Bind_DropDown(ddlVendor, "vt_SCGL_BindVendor", "VendorName", "ID");
    }

    public void Bind_Terms()
    {
        SCGL_Common.Bind_DropDown(DDLTerms, "vt_SCGL_BindTerms", "Terms", "ID");
    }

    public void Reload_JS()
    {
        SCGL_Common.ReloadJS(this, "MyDate();");
        //SCGL_Common.ReloadJS(this, "calculateSum();");
        SCGL_Common.ReloadJS(this, "vale();");
        SCGL_Common.ReloadJS(this, "GrossTotalDeduction();");
        SCGL_Common.ReloadJS(this, "TxtBlur();");
        SCGL_Common.ReloadJS(this, "TotalGridAmount();");
        SCGL_Common.ReloadJS(this, "ChangeConversionRate();");

    }

    public void Gv_GetRows1()
    {
        SqlDataReader dr = BALInvoice.Get_Rows_InvoiceDetail_byID(SCGL_Common.Convert_ToInt(Request.QueryString["ID"]));
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
        dt.Columns.Add(new DataColumn("ddlInventory", typeof(string)));
        dt.Columns.Add(new DataColumn("ddl_Inventoryvalue", typeof(string)));
        dt.Columns.Add(new DataColumn("txtDescription", typeof(string)));
        dt.Columns.Add(new DataColumn("txtQuantity", typeof(string)));
        dt.Columns.Add(new DataColumn("txtRate", typeof(string)));
        dt.Columns.Add(new DataColumn("txtAmount", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlCostCenter", typeof(string)));
        dt.Columns.Add(new DataColumn("ddl_CostCentervalue", typeof(string)));

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
        GV_PurchaseInvoiceDetail.DataSource = dt;
        GV_PurchaseInvoiceDetail.DataBind();
        Bind_Product_Dropdown();
        Bind_CostCenter_Dropdown();
    }

    private void SetInitialRow()
    {

        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlInventory", typeof(string)));
        dt.Columns.Add(new DataColumn("ddl_Inventoryvalue", typeof(string)));
        dt.Columns.Add(new DataColumn("txtDescription", typeof(string)));
        dt.Columns.Add(new DataColumn("txtQuantity", typeof(string)));
        dt.Columns.Add(new DataColumn("txtRate", typeof(string)));
        dt.Columns.Add(new DataColumn("txtAmount", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlCostCenter", typeof(string)));
        dt.Columns.Add(new DataColumn("ddl_CostCentervalue", typeof(string)));

        for (int i = 0; i < 1; i++)
        {
            dr = dt.NewRow();
            dt.Rows.Add(dr);
        }


        ViewState["CurrentTable"] = dt;



        GV_PurchaseInvoiceDetail.DataSource = dt;
        GV_PurchaseInvoiceDetail.DataBind();
        Bind_Product_Dropdown();
        Bind_CostCenter_Dropdown();

        //Bind_Product_Dropdown();

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
            for (int i = 0; i < GV_PurchaseInvoiceDetail.Rows.Count; i++)
            {
                if (GV_PurchaseInvoiceDetail.Rows[i].Visible)
                {
                    DataRow drCurrentRow = Product_Table.NewRow();
                    DropDownList ddlInventory = (DropDownList)GV_PurchaseInvoiceDetail.Rows[i].Cells[1].FindControl("ddlInventory");
                    TextBox txtDescription = (TextBox)GV_PurchaseInvoiceDetail.Rows[i].Cells[2].FindControl("txtDescription");
                    TextBox txtQuantity = (TextBox)GV_PurchaseInvoiceDetail.Rows[i].Cells[3].FindControl("txtQuantity");
                    TextBox txtRate = (TextBox)GV_PurchaseInvoiceDetail.Rows[i].Cells[4].FindControl("txtRate");
                    HtmlInputText txtTotalPrice = (HtmlInputText)GV_PurchaseInvoiceDetail.Rows[i].Cells[5].FindControl("txtAmount");
                    DropDownList ddlCostCenter = (DropDownList)GV_PurchaseInvoiceDetail.Rows[i].Cells[6].FindControl("ddlCostCenter");

                    drCurrentRow["ddlInventory"] = ddlInventory.SelectedItem.Text;
                    drCurrentRow["ddl_Inventoryvalue"] = ddlInventory.SelectedValue;
                    drCurrentRow["txtDescription"] = txtDescription.Text;
                    drCurrentRow["txtQuantity"] = txtQuantity.Text;
                    drCurrentRow["txtRate"] = txtRate.Text;
                    drCurrentRow["txtAmount"] = txtTotalPrice.Value;
                    drCurrentRow["ddlCostCenter"] = ddlCostCenter.SelectedItem.Text;
                    drCurrentRow["ddl_CostCentervalue"] = ddlCostCenter.SelectedValue;


                    Product_Table.Rows.Add(drCurrentRow);
                }
            }
            DataRow dr = Product_Table.NewRow();
            dr[0] = "--Select Item--";
            Product_Table.Rows.Add(dr);
            GV_PurchaseInvoiceDetail.DataSource = Product_Table;
            GV_PurchaseInvoiceDetail.DataBind();
            Bind_Product_Dropdown();
            Bind_CostCenter_Dropdown();
            for (int i = 0; i < GV_PurchaseInvoiceDetail.Rows.Count; i++)
            {
                DropDownList cbox = GV_PurchaseInvoiceDetail.Rows[i].FindControl("ddlInventory") as DropDownList;
                cbox.SelectedIndex = cbox.Items.IndexOf(cbox.Items.FindByText(Product_Table.Rows[i]["ddlInventory"].ToString()));
            }
            for (int i = 0; i < GV_PurchaseInvoiceDetail.Rows.Count; i++)
            {
                DropDownList cbox = GV_PurchaseInvoiceDetail.Rows[i].FindControl("ddlCostCenter") as DropDownList;
                cbox.SelectedIndex = cbox.Items.IndexOf(cbox.Items.FindByText(Product_Table.Rows[i]["ddlCostCenter"].ToString()));
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
        for (int i = 0; i < GV_PurchaseInvoiceDetail.Rows.Count; i++)
        {
            if (GV_PurchaseInvoiceDetail.Rows[i].Visible)
            {
                DropDownList ddlInventory = (DropDownList)GV_PurchaseInvoiceDetail.Rows[i].Cells[1].FindControl("ddlInventory");
                TextBox txtDescription = (TextBox)GV_PurchaseInvoiceDetail.Rows[i].Cells[2].FindControl("txtDescription");
                TextBox txtQuantity = (TextBox)GV_PurchaseInvoiceDetail.Rows[i].Cells[3].FindControl("txtQuantity");
                TextBox txtRate = (TextBox)GV_PurchaseInvoiceDetail.Rows[i].Cells[4].FindControl("txtRate");
                HtmlInputText txtTotalPrice = (HtmlInputText)GV_PurchaseInvoiceDetail.Rows[i].Cells[5].FindControl("txtAmount");
                DropDownList ddlCostCenter = (DropDownList)GV_PurchaseInvoiceDetail.Rows[i].Cells[6].FindControl("ddlCostCenter");


                dtProduct.Rows.Add(dtProduct.NewRow());

                dtProduct.Rows[i]["ddlInventory"] = ddlInventory.SelectedItem.Text;
                dtProduct.Rows[i]["ddl_Inventoryvalue"] = ddlInventory.SelectedValue;
                dtProduct.Rows[i]["txtDescription"] = txtDescription.Text;
                dtProduct.Rows[i]["txtQuantity"] = txtQuantity.Text;
                dtProduct.Rows[i]["txtRate"] = txtRate.Text;
                dtProduct.Rows[i]["txtAmount"] = txtTotalPrice.Value;
                dtProduct.Rows[i]["ddlCostCenter"] = ddlCostCenter.SelectedItem.Text;
                dtProduct.Rows[i]["ddl_CostCentervalue"] = ddlCostCenter.SelectedValue;

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

            if (Product_Table.Rows[pr]["ddlInventory"].ToString() == "")
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

                dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dt.Columns.Add(new DataColumn("ddlInventory", typeof(string)));
                //dt.Columns.Add(new DataColumn("txtDescription", typeof(string)));
                dt.Columns.Add(new DataColumn("txtDescription", typeof(string)));
                dt.Columns.Add(new DataColumn("txtQuantity", typeof(string)));
                dt.Columns.Add(new DataColumn("txtRate", typeof(string)));
                dt.Columns.Add(new DataColumn("txtAmount", typeof(string)));
                dt.Columns.Add(new DataColumn("ddl_Inventoryvalue", typeof(string)));
                dt.Columns.Add(new DataColumn("ddlCostCenter", typeof(string)));
                dt.Columns.Add(new DataColumn("ddl_CostCentervalue", typeof(string)));
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

    protected void GV_PurchaseInvoiceDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = e.RowIndex;
        if (index != 0)
        {
            GV_PurchaseInvoiceDetail.Rows[e.RowIndex].Visible = false;

            DropDownList ddlInventory = (DropDownList)GV_PurchaseInvoiceDetail.Rows[e.RowIndex].Cells[1].FindControl("ddlInventory");
            //TextBox txtDescription = (TextBox)GV_PurchaseInvoiceDetail.Rows[e.RowIndex].Cells[2].FindControl("txtDescription");
            TextBox txtDescription = (TextBox)GV_PurchaseInvoiceDetail.Rows[e.RowIndex].Cells[2].FindControl("txtDescription");
            TextBox txtQuantity = (TextBox)GV_PurchaseInvoiceDetail.Rows[e.RowIndex].Cells[3].FindControl("txtQuantity");
            TextBox txtRate = (TextBox)GV_PurchaseInvoiceDetail.Rows[e.RowIndex].Cells[4].FindControl("txtRate");
            HtmlInputText txtAmount = GV_PurchaseInvoiceDetail.Rows[e.RowIndex].Cells[5].FindControl("txtAmount") as HtmlInputText;
            DropDownList ddlCostCenter = (DropDownList)GV_PurchaseInvoiceDetail.Rows[e.RowIndex].Cells[6].FindControl("ddlCostCenter");

            if (txtAmount.Value == "") { txtAmount.Value = "0"; }

            ddlInventory.SelectedIndex = 0;
            txtDescription.Text = "";
            txtQuantity.Text = "";
            txtRate.Text = "";
            txtAmount.Value = "";
            ddlCostCenter.SelectedIndex = 0;
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
            if (Product_Table.Rows[p]["ddlInventory"].ToString() != null && Product_Table.Rows[p]["ddlInventory"].ToString() != "")
            {
                dtInsert.Rows[p]["ddlInventory"] = Product_Table.Rows[p]["ddlInventory"].ToString();
                dtInsert.Rows[p]["ddl_Inventoryvalue"] = Product_Table.Rows[p]["ddl_Inventoryvalue"].ToString();
                dtInsert.Rows[p]["txtDescription"] = Product_Table.Rows[p]["txtDescription"].ToString();
                dtInsert.Rows[p]["txtQuantity"] = Product_Table.Rows[p]["txtQuantity"].ToString();
                dtInsert.Rows[p]["txtRate"] = Product_Table.Rows[p]["txtRate"].ToString();
                dtInsert.Rows[p]["txtAmount"] = Product_Table.Rows[p]["txtAmount"].ToString();
                dtInsert.Rows[p]["ddlCostCenter"] = Product_Table.Rows[p]["ddlCostCenter"].ToString();
                dtInsert.Rows[p]["ddl_CostCentervalue"] = Product_Table.Rows[p]["ddl_CostCentervalue"].ToString();
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
                            lblSuccessMsg.InnerHtml = "Invoice Created Successfully";
                        }
                        else
                        {
                            lblSuccessMsg.InnerHtml = "Invoice Updated Successfully";
                        }
                        SCGL_Common.Success_Message(this.Page, "PurchaseInvoice_Views.aspx");
                    }


                }
            }
            else
            { JQ.showStatusMsg(this, "3", "User not Allowed to Insert New Record"); }
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
        try
        {
            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            if (btnSave.Text == "Save")
            {

                BALInvoice.pInvoiceID = -1;
            }
            else
            {
                BALInvoice.pInvoiceID = SCGL_Common.Convert_ToInt(Request.QueryString["Id"]);
            }
            BALInvoice.VendorID = SCGL_Common.Convert_ToInt(ddlVendor.SelectedValue);
            BALInvoice.Email = txtEmail.Text;
            BALInvoice.BillingAddress = txtBillingAddress.Text;
            BALInvoice.Invoice_No = txtInvoiceNumber.Text;  //  Database column name for this field : InvoiceNo
            BALInvoice.TermID = SCGL_Common.Convert_ToInt(DDLTerms.SelectedValue);
            BALInvoice.InvoiceDate = SCGL_Common.CheckDateTime(txtInvoiceDate.Text);
            //BALInvoice.InvoiceDate = txtInvoiceDate.Text.ToString();
            BALInvoice.DueDate = SCGL_Common.CheckDateTime(txtDueDate.Text);
           

            BALInvoice.Discount = txtDiscount.Text == "" ? SCGL_Common.Convert_ToDecimal("0")
                : SCGL_Common.Convert_ToDecimal(txtDiscount.Text);

            BALInvoice.PrintMessage = txtPrintMessage.Text;
            BALInvoice.StatementMemo = txtStatementMemo.Text;
            BALInvoice.Total = SCGL_Common.Convert_ToDecimal(txtTot.Value);
            BALInvoice.LoginID = SBO.UserID;
            BALInvoice.FinYearID = SBO.FinYearID;
            int InvoiceID = BALInvoice.CreateModifyInvoice(BALInvoice, trans);
            if (InvoiceID > 0)
            {
                //Insert in Invoice Detail

                txtPurchaseID.Text = InvoiceID.ToString();
                BALInvoice.InvoiceNo = SCGL_Common.Convert_ToInt(txtPurchaseID.Text);
                if (btnSave.Text == "Update")
                {
                    BALInvoice.Delete_InvoiceDetail(BALInvoice.InvoiceNo, trans);
                    BALInvoice.DeleteTransaction_pInvoice(BALInvoice.InvoiceNo, trans);
                }
                int Counter = 0;
                DataTable dt = Record_for_Insert();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BALInvoice.InvoiceNo = SCGL_Common.Convert_ToInt(txtPurchaseID.Text);
                    BALInvoice.Description2 = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtDescription"].ToString());
                    BALInvoice.Quantity = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtQuantity"].ToString());
                    BALInvoice.Rate = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtRate"].ToString());
                    BALInvoice.Amount = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtAmount"].ToString());
                    ////BALInvoice.Currency = SCGL_Common.Convert_ToInt(ddlCurrency.SelectedValue);
                    ////BALInvoice.ConversionRate = SCGL_Common.Convert_ToDecimal(txtConversionRate.Text);
                    ////BALInvoice.PKRAmount = BALInvoice.Amount * BALInvoice.ConversionRate;
                    BALInvoice.InventoryID = SCGL_Common.Convert_ToInt(dt.Rows[i]["ddl_Inventoryvalue"].ToString());
                    BALInvoice.CostCenterID = SCGL_Common.Convert_ToInt(dt.Rows[i]["ddl_CostCentervalue"].ToString());

                    if (BALInvoice.CreateModifyInvoiceDetail(BALInvoice, trans))
                    {
                        Counter++;
                    }
                    else
                    {
                        Counter = 0;
                        break;
                    }
                }
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


        for (int i = 0; i < GV_PurchaseInvoiceDetail.Rows.Count; i++)
        {
            DropDownList ddlProduct = GV_PurchaseInvoiceDetail.Rows[i].FindControl("ddlInventory") as DropDownList;
            TextBox txtDescription = (TextBox)GV_PurchaseInvoiceDetail.Rows[i].FindControl("txtDescription");
            TextBox txtQuantity = (TextBox)GV_PurchaseInvoiceDetail.Rows[i].FindControl("txtQuantity");
            TextBox txtRate = (TextBox)GV_PurchaseInvoiceDetail.Rows[i].FindControl("txtRate");
            DropDownList ddlCostCenter = GV_PurchaseInvoiceDetail.Rows[i].FindControl("ddlCostCenter") as DropDownList;
            if (ddlProduct.SelectedValue == "0" || ddlCostCenter.SelectedValue == "0")
            {
                SCGL_Common.Error_Message(this);
                return false;
            }

        }

        return IsValid;
    }



    public void GetMaxInvoiceId()
    {
        DataTable dt = new DataTable();
        dt = BALInvoice.GetMaxInvoiceId();
        foreach (DataRow row in dt.Rows)
        {
            int staticID = 1;

            int DynamicID = Convert.ToInt32(row["pInvoiceID"]);
            int totalDraftID = staticID + DynamicID;
            txtPurchaseID.Text = Convert.ToInt32(totalDraftID).ToString();
        }
        txtPurchaseID.ReadOnly = true;
    }

  
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PurchaseInvoice_Views.aspx");
    }

    
}
