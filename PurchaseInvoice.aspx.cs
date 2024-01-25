using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SW.SW_Common;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class PurchaseInvoice : System.Web.UI.Page
{
    PurchaseInvoice_BAL BALInvoice = new PurchaseInvoice_BAL();
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
        if (!IsPostBack)
        {
           
                    GetMaxInvoiceId();
                    Bind_Vendor();
                    Bind_Terms();

                    if (Request.QueryString["Id"] != null)
                        BindControl(Convert.ToInt32(Request.QueryString["Id"]));
                    else
                        SetInitialRow();
                
               
            }

            
        
    }
    public void BindControl(int Id)
    {
        if (Request.QueryString["view"] != null)
        {
            btnSave.Visible = false;
            btnAddLines.Visible = btnClearAllLines.Visible = false;
        }
        PurchaseInvoice_BAL Pinvoice_BAL = new PurchaseInvoice_BAL();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        DataTable dt = Pinvoice_BAL.getInvoiceByID(Id,FinYearID);
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
            txtDueDate.Text = SCGL_Common.CheckDateTime(dt.Rows[0]["DueDate"]).ToShortDateString();
            txttotal2.Value = dt.Rows[0]["Total"].ToString();
            txtDiscount.Text = dt.Rows[0]["Discount"].ToString();
            txtPrintMessage.Text = dt.Rows[0]["PrintMessage"].ToString();
            txtStatementMemo.Text = dt.Rows[0]["StatementMemo"].ToString();
           
        }
        
        PurchaseInvoiceDetail_BAL inv_dtl = new PurchaseInvoiceDetail_BAL();
        DataTable dtInvoiceDetail = inv_dtl.getInvoiceDetailByInvoiceID(Id);
        if (dtInvoiceDetail.Rows.Count > 0)
        {
            dtInvoiceDetail.Columns.RemoveAt(0); dtInvoiceDetail.Columns.RemoveAt(0);
            dtInvoiceDetail.Columns.Add("RowNumber", typeof(string)).SetOrdinal(0);
            dtInvoiceDetail.Columns.Add("Item", typeof(string)).SetOrdinal(1);
            //dtInvoiceDetail.Columns["Description"].ColumnName = "txtDescription";
            dtInvoiceDetail.Columns["Quantity"].ColumnName = "txtQuantity";
            dtInvoiceDetail.Columns["Rate"].ColumnName = "txtRate";
            dtInvoiceDetail.Columns["Amount"].ColumnName = "txtAmount";
            dtInvoiceDetail.Columns.Add("CostCenter", typeof(string));
            dtInvoiceDetail.Columns.Add("Cost_CenterValue", typeof(string));
            dtInvoiceDetail.Columns.Add("Item_Value", typeof(string));
            dtInvoiceDetail.Columns["ProductServiceID"].SetOrdinal(dtInvoiceDetail.Columns.Count - 1);
            ViewState["CurrentTable"] = dtInvoiceDetail;
            SCGL_Common.Bind_GridView(GV_PurchaseInvoiceDetail, dtInvoiceDetail);
            Bind_Product_Dropdown();
            for (int i = 0; i < dtInvoiceDetail.Rows.Count; i++)
            {
                DropDownList ddl = GV_PurchaseInvoiceDetail.Rows[i].FindControl("ddlItem") as DropDownList;
                ddl.SelectedValue = dtInvoiceDetail.Rows[i]["ProductServiceID"].ToString();
                DropDownList ddlCostCenter = GV_PurchaseInvoiceDetail.Rows[i].FindControl("ddlCostCenter") as DropDownList;
                ddlCostCenter.SelectedValue = dtInvoiceDetail.Rows[i]["CostCenterID"].ToString();
            }
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
   
    public void Bind_Product_Dropdown()
    {
        //  dont know which procedure to be run here...
        for (int i = 0; i < GV_PurchaseInvoiceDetail.Rows.Count; i++)
        {
            DropDownList ddlItem = GV_PurchaseInvoiceDetail.Rows[i].FindControl("ddlItem") as DropDownList;
            SCGL_Common.Bind_DropDown(ddlItem, "vt_SCGL_SP_GetFishCart", "item", "Inventory_ID");
            DropDownList ddlCostCenter = GV_PurchaseInvoiceDetail.Rows[i].FindControl("ddlCostCenter") as DropDownList;
            SCGL_Common.Bind_DropDown(ddlCostCenter, "vt_SCGL_Sp_GetCostCenter", "CostCenter", "CostCenterID");
        }
    }

    private void SetInitialRow()
    {

        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Item", typeof(string)));
        //dt.Columns.Add(new DataColumn("txtDescription", typeof(string)));
        dt.Columns.Add(new DataColumn("txtQuantity", typeof(string)));
        dt.Columns.Add(new DataColumn("txtRate", typeof(string)));
        dt.Columns.Add(new DataColumn("txtAmount", typeof(string)));
        dt.Columns.Add(new DataColumn("CostCenter", typeof(string)));
        dt.Columns.Add(new DataColumn("Cost_CenterValue", typeof(string)));
        dt.Columns.Add(new DataColumn("Item_Value", typeof(string)));
        for (int i = 0; i < 1; i++)
        {
            dr = dt.NewRow();
            dt.Rows.Add(dr);
        }


        //Store the DataTable in ViewState for future reference


        ViewState["CurrentTable"] = dt;



        GV_PurchaseInvoiceDetail.DataSource = dt;
        GV_PurchaseInvoiceDetail.DataBind();



        Bind_Product_Dropdown();

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
                    DropDownList ddlItem = (DropDownList)GV_PurchaseInvoiceDetail.Rows[i].Cells[1].FindControl("ddlItem");
                    //TextBox txtDescription = (TextBox)GV_PurchaseInvoiceDetail.Rows[i].Cells[2].FindControl("txtDescription");
                    TextBox txtQuantity = (TextBox)GV_PurchaseInvoiceDetail.Rows[i].Cells[2].FindControl("txtQuantity");
                    TextBox txtRate = (TextBox)GV_PurchaseInvoiceDetail.Rows[i].Cells[3].FindControl("txtRate");
                    HtmlInputText txtTotalPrice = (HtmlInputText)GV_PurchaseInvoiceDetail.Rows[i].Cells[4].FindControl("txtAmount");
                    DropDownList ddlCostCenter = (DropDownList)GV_PurchaseInvoiceDetail.Rows[i].Cells[5].FindControl("ddlCostCenter");

                    drCurrentRow["Item"] = ddlItem.SelectedItem.Text;
                    drCurrentRow["Item_value"] = ddlItem.SelectedValue;
                    //drCurrentRow["txtDescription"] = txtDescription.Text;
                    drCurrentRow["txtQuantity"] = txtQuantity.Text;
                    drCurrentRow["txtRate"] = txtRate.Text;
                    drCurrentRow["txtAmount"] = txtTotalPrice.Value;
                    drCurrentRow["CostCenter"] = ddlCostCenter.SelectedItem.Text;
                    drCurrentRow["Cost_CenterValue"] = ddlCostCenter.SelectedValue;

                    Product_Table.Rows.Add(drCurrentRow);
                }
            }
            DataRow dr = Product_Table.NewRow();
            dr[0] = "--Select Item--";
            Product_Table.Rows.Add(dr);
            GV_PurchaseInvoiceDetail.DataSource = Product_Table;
            GV_PurchaseInvoiceDetail.DataBind();
            Bind_Product_Dropdown();
            for (int i = 0; i < GV_PurchaseInvoiceDetail.Rows.Count; i++)
            {
                DropDownList cbox = GV_PurchaseInvoiceDetail.Rows[i].FindControl("ddlItem") as DropDownList;
                cbox.SelectedIndex = cbox.Items.IndexOf(cbox.Items.FindByText(Product_Table.Rows[i]["Item"].ToString()));
                TextBox txtQuantity = (TextBox)GV_PurchaseInvoiceDetail.Rows[i].FindControl("txtQuantity");
                txtQuantity.Text = (Product_Table.Rows[i]["txtQuantity"].ToString()); 
            }
            for (int i = 0; i < GV_PurchaseInvoiceDetail.Rows.Count; i++)
            {
                DropDownList cbox = GV_PurchaseInvoiceDetail.Rows[i].FindControl("ddlCostCenter") as DropDownList;
                cbox.SelectedIndex = cbox.Items.IndexOf(cbox.Items.FindByText(Product_Table.Rows[i]["CostCenter"].ToString()));
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
                DropDownList ddlItem = (DropDownList)GV_PurchaseInvoiceDetail.Rows[i].Cells[1].FindControl("ddlItem");
                //TextBox txtDescription = (TextBox)GV_PurchaseInvoiceDetail.Rows[i].Cells[2].FindControl("txtDescription");
                TextBox txtQuantity = (TextBox)GV_PurchaseInvoiceDetail.Rows[i].Cells[2].FindControl("txtQuantity");
                TextBox txtRate = (TextBox)GV_PurchaseInvoiceDetail.Rows[i].Cells[3].FindControl("txtRate");
                HtmlInputText txtTotalPrice = (HtmlInputText)GV_PurchaseInvoiceDetail.Rows[i].Cells[4].FindControl("txtAmount");
                DropDownList ddlCostCenter = (DropDownList)GV_PurchaseInvoiceDetail.Rows[i].Cells[5].FindControl("ddlCostCenter");
                dtProduct.Rows.Add(dtProduct.NewRow());
                if (ddlItem.SelectedValue.ToString() != "0" && ddlCostCenter.SelectedValue.ToString() != "0")
                {
                    dtProduct.Rows[i]["Item"] = ddlItem.SelectedItem.Text;
                    dtProduct.Rows[i]["Item_value"] = ddlItem.SelectedValue;
                    //dtProduct.Rows[i]["txtDescription"] = txtDescription.Text;
                    dtProduct.Rows[i]["txtQuantity"] = txtQuantity.Text;
                    dtProduct.Rows[i]["txtRate"] = txtRate.Text;
                    dtProduct.Rows[i]["txtAmount"] = txtTotalPrice.Value;
                    dtProduct.Rows[i]["CostCenter"] = ddlCostCenter.SelectedItem.Text;
                    dtProduct.Rows[i]["Cost_CenterValue"] = ddlCostCenter.SelectedValue;
                }
            }
            else
            {
                dtProduct.Rows.Add(dtProduct.NewRow());
            }
        }
        for (int pr = 0; pr < Product_Table.Rows.Count; pr++)
        {
            if (Product_Table.Rows[pr]["Item_Value"].ToString() == "")
            {
                Product_Table.Rows.RemoveAt(pr);
                pr--;
            }
            if (Product_Table.Rows[pr]["Cost_CenterValue"].ToString() == "")
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
                dt.Columns.Add(new DataColumn("Item", typeof(string)));
                //dt.Columns.Add(new DataColumn("txtDescription", typeof(string)));
                dt.Columns.Add(new DataColumn("txtQuantity", typeof(string)));
                dt.Columns.Add(new DataColumn("txtRate", typeof(string)));
                dt.Columns.Add(new DataColumn("txtAmount", typeof(string)));
                dt.Columns.Add(new DataColumn("CostCenter", typeof(string)));
                dt.Columns.Add(new DataColumn("Cost_CenterValue", typeof(string)));
                dt.Columns.Add(new DataColumn("Item_Value", typeof(string)));
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

    public void Reload_JS()
    {
        SCGL_Common.ReloadJS(this, "vale();");
        SCGL_Common.ReloadJS(this, "GrossTotalDeduction();");
        SCGL_Common.ReloadJS(this, "MyDate();");
        SCGL_Common.ReloadJS(this, "TotalGridAmount();");
    }

    protected void GV_PurchaseInvoiceDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GV_PurchaseInvoiceDetail.Rows[e.RowIndex].Visible = false;

        DropDownList ddlDeductionItem = (DropDownList)GV_PurchaseInvoiceDetail.Rows[e.RowIndex].Cells[1].FindControl("ddlItem");
        //TextBox txtDescription = (TextBox)GV_PurchaseInvoiceDetail.Rows[e.RowIndex].Cells[2].FindControl("txtDescription");
        TextBox txtQuantity = (TextBox)GV_PurchaseInvoiceDetail.Rows[e.RowIndex].Cells[2].FindControl("txtQuantity");
        TextBox txtRate = (TextBox)GV_PurchaseInvoiceDetail.Rows[e.RowIndex].Cells[3].FindControl("txtRate");
        HtmlInputText txtAmount = GV_PurchaseInvoiceDetail.Rows[e.RowIndex].Cells[4].FindControl("txtAmount") as HtmlInputText;
        DropDownList ddlCostCenter = (DropDownList)GV_PurchaseInvoiceDetail.Rows[e.RowIndex].Cells[5].FindControl("ddlCostCenter");
        if (txtAmount.Value == "") { txtAmount.Value = "0"; }

        ddlDeductionItem.SelectedIndex = 0;
        //txtDescription.Text = "";
        txtQuantity.Text = "";
        txtRate.Text = "";
        txtAmount.Value = "";
        ddlCostCenter.SelectedIndex = 0;
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
            BALInvoice.DueDate = SCGL_Common.CheckDateTime(txtDueDate.Text);
            //BALInvoice.Discount = txtDiscount.Text;
            //  changed the above line to the following.

            BALInvoice.Discount = txtDiscount.Text == "" ? SCGL_Common.Convert_ToDecimal("0")
                : SCGL_Common.Convert_ToDecimal(txtDiscount.Text);

            BALInvoice.PrintMessage = txtPrintMessage.Text;
            BALInvoice.StatementMemo = txtStatementMemo.Text;
            BALInvoice.Total = SCGL_Common.Convert_ToDecimal(txttotal2.Value) - SCGL_Common.Convert_ToDecimal(txtDiscount.Text);
            BALInvoice.LoginID = SBO.UserID;
            BALInvoice.FinYearID = SBO.FinYearID;

            if (BALInvoice.CreateModifyInvoice(BALInvoice, trans))
            {
                //Insert in Invoice Detail
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
                    BALInvoice.ProductServiceID = SCGL_Common.Convert_ToInt(dt.Rows[i]["Item"].ToString());
                    //BALInvoice.Description = dt.Rows[i]["txtDescription"].ToString();
                    BALInvoice.Quantity = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtQuantity"].ToString());
                    BALInvoice.Rate = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtRate"].ToString());
                    BALInvoice.Amount = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtAmount"].ToString());
                    BALInvoice.CostCenterID = SCGL_Common.Convert_ToInt(dt.Rows[i]["CostCenter"].ToString());


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
            dtInsert.Rows[p]["Item"] = Product_Table.Rows[p]["Item_Value"].ToString();
            //dtInsert.Rows[p]["txtDescription"] = Product_Table.Rows[p]["txtDescription"].ToString();
            dtInsert.Rows[p]["txtQuantity"] = Product_Table.Rows[p]["txtQuantity"].ToString();
            dtInsert.Rows[p]["txtRate"] = Product_Table.Rows[p]["txtRate"].ToString();

            dtInsert.Rows[p]["txtAmount"] = Product_Table.Rows[p]["txtAmount"].ToString();
            dtInsert.Rows[p]["CostCenter"] = Product_Table.Rows[p]["Cost_CenterValue"].ToString();
        }

        return dtInsert;
    }

    public bool Check_Validation()
    {
        bool IsValid = true;
        for (int i = 0; i < GV_PurchaseInvoiceDetail.Rows.Count; i++)
        {
            DropDownList ddlProduct = (DropDownList)GV_PurchaseInvoiceDetail.Rows[i].FindControl("ddlItem");
            DropDownList ddlCostCenter = (DropDownList)GV_PurchaseInvoiceDetail.Rows[i].FindControl("ddlCostCenter");
            //DropDownList ddlCostCenter = (DropDownList)GV_PurchaseInvoiceDetail.Rows[0].FindControl("ddlCostCenter");

            if (ddlProduct.SelectedValue != "0")
            {
                    if(ddlCostCenter.SelectedValue == "0")
                {

                    SCGL_Common.Error_Message(this);

                    return false;
                }
            }
        }

        DropDownList ddlItem = (DropDownList)GV_PurchaseInvoiceDetail.Rows[0].FindControl("ddlItem");
        if (ddlItem.SelectedValue == "0")
        {
            SCGL_Common.Error_ItemID(this);
            return false;
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

    protected void btnClearAllLines_Click(object sender, EventArgs e)
    {
        SetInitialRow();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PurchaseInvoice_views.aspx");
    }
}
