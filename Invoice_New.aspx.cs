using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SW.SW_Common;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class invoice_New : System.Web.UI.Page
{
    Invoice_BAL BALInvoice = new Invoice_BAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionBO"] == null)
        {
            Response.Redirect("Login.aspx");

        }
        Reload_JS();
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "invoice_New.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "invoice_New.aspx" && view == true)
                {
                    GetMaxInvoiceId();
                    Bind_Customer();
                    Bind_Discount();


                    if (Request.QueryString["Id"] != null)
                    {
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
      
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataTable dt = PM.getFinancialYearByID(SBO.FinYearID);
        hdnMinDate.Value = SCGL_Common.CheckDateTime(dt.Rows[0]["yearFrom"]).ToShortDateString();
        hdnMaxDate.Value = SCGL_Common.CheckDateTime(dt.Rows[0]["YearTo"]).ToShortDateString();
    }
    public void BindControl(int Id)
    {
        if (Request.QueryString["view"] != null)
        {
            btnSave.Visible = false;
            btnaddnewRow.Visible = Button2.Visible = false;
        }
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID; 
        Invoice_BAL invoiceBal = new Invoice_BAL();
        DataTable dt = invoiceBal.getInvoiceByID(Id,FinYearID);
        if (dt.Rows.Count > 0)
        {
            ddlCustomer.SelectedValue = dt.Rows[0]["CustomerID"].ToString();
            txtEmail.Text = dt.Rows[0]["Email"].ToString();
            txtBillingAddress.Text = dt.Rows[0]["BillingAddress"].ToString();
            txtInvoiceID.Text = dt.Rows[0]["InvoiceID"].ToString();
            txtInvoiceNumber.Text = dt.Rows[0]["InvoiceNo"].ToString();
            txtTerm.Text = dt.Rows[0]["TermID"].ToString();
            txtInvoiceDate.Text = SCGL_Common.CheckDateTime(dt.Rows[0]["InvoiceDate"]).ToShortDateString();
            txtDueDate.Text = SCGL_Common.CheckDateTime(dt.Rows[0]["DueDate"]).ToShortDateString();
            txttotal2.Value = dt.Rows[0]["Total"].ToString();
            txtDiscount.Text = dt.Rows[0]["Discount"].ToString();
            txtFrom.Text = dt.Rows[0]["From"].ToString();
            txtTo.Text = dt.Rows[0]["To"].ToString();
            txtVessel.Text = dt.Rows[0]["Vessel"].ToString();
            txtFormENo.Text = dt.Rows[0]["FormENo"].ToString();
            txtFreight.Text = dt.Rows[0]["Freight"].ToString();
            txtNetWeight.Text = dt.Rows[0]["NetWeight"].ToString();
            txtGrossWeight.Text = dt.Rows[0]["GrossWeight"].ToString();
            txtContainerNo.Text = dt.Rows[0]["ContainerNo"].ToString();
            txtproformaNo.Text = dt.Rows[0]["ProformaNo"].ToString();
            txtInsurance.Text = dt.Rows[0]["Insurance"].ToString();



            ddlDiscount.SelectedValue = dt.Rows[0]["DiscountID"].ToString();
        }
        InvoiceDetail_BAL inv_dtl = new InvoiceDetail_BAL();
        DataTable dtInvoiceDetail = inv_dtl.getInvoiceDetailByInvoiceID(Id);
        if (dtInvoiceDetail.Rows.Count > 0)
        {
            dtInvoiceDetail.Columns.RemoveAt(0); dtInvoiceDetail.Columns.RemoveAt(0);
            dtInvoiceDetail.Columns.Add("RowNumber", typeof(string)).SetOrdinal(0);
            dtInvoiceDetail.Columns.Add("Item", typeof(string)).SetOrdinal(1);
            dtInvoiceDetail.Columns["Description"].ColumnName = "txtDescription";
            dtInvoiceDetail.Columns["Quantity"].ColumnName = "txtQuantity";
            dtInvoiceDetail.Columns["Rate"].ColumnName = "txtRate";
            dtInvoiceDetail.Columns["Amount"].ColumnName = "txtAmount";
            dtInvoiceDetail.Columns["GridName"].ColumnName = "txtGrid";
            dtInvoiceDetail.Columns.Add("Item_Value", typeof(string));
            dtInvoiceDetail.Columns["ProductServiceID"].SetOrdinal(dtInvoiceDetail.Columns.Count - 1);
            ViewState["CurrentTable"] = dtInvoiceDetail;
            SCGL_Common.Bind_GridView(GV_InvoiceDetail, dtInvoiceDetail);
            Bind_Product_Dropdown();
            for (int i = 0; i < dtInvoiceDetail.Rows.Count; i++)
            {
                DropDownList ddl = GV_InvoiceDetail.Rows[i].FindControl("ddlItem") as DropDownList;
                ddl.SelectedValue = dtInvoiceDetail.Rows[i]["ProductServiceID"].ToString();
            }
            btnSave.Text = "Update";
        }
    }
    public void Bind_Customer()
    {
        SCGL_Common.Bind_DropDown(ddlCustomer, "vt_SCGL_BindCustomer", "CustomerName", "ID");
    }

    public void Bind_Discount()
    {
        SCGL_Common.Bind_DropDown(ddlDiscount, "vt_SCGL_BindCustomer", "CustomerName", "ID");
    }

    public void Bind_Product_Dropdown()
    {
        for (int i = 0; i < GV_InvoiceDetail.Rows.Count; i++)
        {
            DropDownList ddlItem = GV_InvoiceDetail.Rows[i].FindControl("ddlItem") as DropDownList;
            SCGL_Common.Bind_DropDown(ddlItem, "vt_SCGL_SP_GetFishCart", "item", "Inventory_ID");
        }


    }

    public void Bind_FishName_Dropdown()
    {
        for (int i = 0; i < GV_InvoiceDetail.Rows.Count; i++)
        {
            DropDownList ddlFish = GV_InvoiceDetail.Rows[i].FindControl("ddlFish") as DropDownList;
            SCGL_Common.Bind_DropDown(ddlFish, "vt_SCGL_Sp_GetFishName_", "FishName", "FishID");
        }
        for (int i = 0; i < GV_InvoiceDetail1.Rows.Count; i++)
        {
            DropDownList ddlFish = GV_InvoiceDetail1.Rows[i].FindControl("ddlFish") as DropDownList;
            SCGL_Common.Bind_DropDown(ddlFish, "vt_SCGL_Sp_GetFishName_", "FishName", "FishID");
        }

    }

    public void Bind_Product_Dropdown1()
    {

        for (int i = 0; i < GV_InvoiceDetail1.Rows.Count; i++)
        {
            DropDownList ddlItem = GV_InvoiceDetail1.Rows[i].FindControl("ddlItem") as DropDownList;
            SCGL_Common.Bind_DropDown(ddlItem, "vt_SCGL_SP_GetFishCart", "item", "Inventory_ID");
        }


    }
     public void Bind_FishGrade_Dropdown()
    {
        for (int i = 0; i < GV_InvoiceDetail.Rows.Count; i++)
        {
            DropDownList ddlFish = GV_InvoiceDetail.Rows[i].FindControl("ddlFishGrade") as DropDownList;
            SCGL_Common.Bind_DropDown(ddlFish, "vt_SCGL_Sp_GetFishGrade_", "FishGrade", "FishGradeID");
        }
        for (int i = 0; i < GV_InvoiceDetail1.Rows.Count; i++)
        {
            DropDownList ddlFish = GV_InvoiceDetail1.Rows[i].FindControl("ddlFishGrade") as DropDownList;
            SCGL_Common.Bind_DropDown(ddlFish, "vt_SCGL_Sp_GetFishGrade_", "FishGrade", "FishGradeID");
        }

    }
     public void Bind_FishSize_Dropdown()
    {
        for (int i = 0; i < GV_InvoiceDetail.Rows.Count; i++)
        {
            DropDownList ddlFish = GV_InvoiceDetail.Rows[i].FindControl("ddlFishSize") as DropDownList;
            SCGL_Common.Bind_DropDown(ddlFish, "vt_SCGL_Sp_GetFishSize_", "FishSize", "FishSizeID");
        }
        for (int i = 0; i < GV_InvoiceDetail1.Rows.Count; i++)
        {
            DropDownList ddlFish = GV_InvoiceDetail1.Rows[i].FindControl("ddlFishSize") as DropDownList;
            SCGL_Common.Bind_DropDown(ddlFish, "vt_SCGL_Sp_GetFishSize_", "FishSize", "FishSizeID");
        }


    }
    
    

    private void SetInitialRow()
    {

        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
      
        dt.Columns.Add(new DataColumn("txtDescription", typeof(string)));
        dt.Columns.Add(new DataColumn("txtQuantity", typeof(string)));
        dt.Columns.Add(new DataColumn("txtRate", typeof(string)));
        dt.Columns.Add(new DataColumn("txtAmount", typeof(string)));
    
        dt.Columns.Add(new DataColumn("Fish_Item", typeof(string)));
        dt.Columns.Add(new DataColumn("Fish_Value", typeof(string)));
        dt.Columns.Add(new DataColumn("Fish_Grade", typeof(string)));
        dt.Columns.Add(new DataColumn("Fish_GradeValue", typeof(string)));
        dt.Columns.Add(new DataColumn("Fish_Size", typeof(string)));
        dt.Columns.Add(new DataColumn("Fish_SizeValue", typeof(string)));
        dt.Columns.Add(new DataColumn("txtGridName", typeof(string)));
        for (int i = 0; i < 1; i++)
        {
            dr = dt.NewRow();
            dt.Rows.Add(dr);
        }
        DataTable dt2 = new DataTable();
        DataRow dr2 = null;
        dt2.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt2.Columns.Add(new DataColumn("Fish_Item", typeof(string)));
        dt2.Columns.Add(new DataColumn("Fish_Value", typeof(string)));
        dt2.Columns.Add(new DataColumn("Fish_Grade", typeof(string)));
        dt2.Columns.Add(new DataColumn("Fish_GradeValue", typeof(string)));
        dt2.Columns.Add(new DataColumn("Fish_Size", typeof(string)));
        dt2.Columns.Add(new DataColumn("Fish_SizeValue", typeof(string)));
        dt2.Columns.Add(new DataColumn("txtDescription", typeof(string)));
        dt2.Columns.Add(new DataColumn("txtQuantity", typeof(string)));
        dt2.Columns.Add(new DataColumn("txtRate", typeof(string)));
        dt2.Columns.Add(new DataColumn("txtAmount", typeof(string)));

        dt2.Columns.Add(new DataColumn("txtGridName", typeof(string)));
        for (int i = 0; i < 1; i++)
        {
            dr2 = dt2.NewRow();
            dt2.Rows.Add(dr2);
        }


        //Store the DataTable in ViewState for future reference


        ViewState["CurrentTable"] = dt;
        ViewState["CurrentTable2"] = dt2;


        GV_InvoiceDetail.DataSource = dt;
        GV_InvoiceDetail.DataBind();

        GV_InvoiceDetail1.DataSource = dt2;
        GV_InvoiceDetail1.DataBind();
        Bind_FishName_Dropdown();
       // Bind_Product_Dropdown();
       // Bind_Product_Dropdown1();
        Bind_FishGrade_Dropdown();
        Bind_FishSize_Dropdown();
    }

    protected void btnaddnewRow_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }

    public void AddNewRowToGrid()
    {
        try
        {
            Product_Table.Rows.Clear();
            for (int i = 0; i < GV_InvoiceDetail.Rows.Count; i++)
            {
                if (GV_InvoiceDetail.Rows[i].Visible)
                {
                    DataRow drCurrentRow = Product_Table.NewRow();

                    DropDownList ddlFish = (DropDownList)GV_InvoiceDetail.Rows[i].Cells[1].FindControl("ddlFish");
                    DropDownList ddlFishGrade = (DropDownList)GV_InvoiceDetail.Rows[i].Cells[2].FindControl("ddlFishGrade");
                    DropDownList ddlFishSize = (DropDownList)GV_InvoiceDetail.Rows[i].Cells[3].FindControl("ddlFishSize");
                    TextBox txtDescription = (TextBox)GV_InvoiceDetail.Rows[i].Cells[4].FindControl("txtDescription");
                    TextBox txtQuantity = (TextBox)GV_InvoiceDetail.Rows[i].Cells[5].FindControl("txtQuantity");
                    TextBox txtRate = (TextBox)GV_InvoiceDetail.Rows[i].Cells[6].FindControl("txtRate");
                    HtmlInputText txtTotalPrice = (HtmlInputText)GV_InvoiceDetail.Rows[i].Cells[7].FindControl("txtAmount");
                    Label txtGrid = (Label)GV_InvoiceDetail.Rows[i].Cells[8].FindControl("txtGridName");

                 
                    drCurrentRow["Fish_Item"] = ddlFish.SelectedItem.Text;
                    drCurrentRow["Fish_value"] = ddlFish.SelectedValue;
                    drCurrentRow["Fish_Grade"] = ddlFishGrade.SelectedItem.Text;
                    drCurrentRow["Fish_Gradevalue"] = ddlFishGrade.SelectedValue;
                    drCurrentRow["Fish_Size"] = ddlFishSize.SelectedItem.Text;
                    drCurrentRow["Fish_Sizevalue"] = ddlFishSize.SelectedValue;
                    drCurrentRow["txtDescription"] = txtDescription.Text;
                    drCurrentRow["txtQuantity"] = txtQuantity.Text;
                    drCurrentRow["txtRate"] = txtRate.Text;
                    drCurrentRow["txtAmount"] = Convert.ToInt64(Convert.ToDouble(txtTotalPrice.Value));
                    drCurrentRow["txtGridName"] = txtGrid.Text;
                    Product_Table.Rows.Add(drCurrentRow);
                }
            }
            DataRow dr = Product_Table.NewRow();
            dr[0] = "--Select Item--";
            Product_Table.Rows.Add(dr);
            GV_InvoiceDetail.DataSource = Product_Table;
            GV_InvoiceDetail.DataBind();
           // Bind_Product_Dropdown();
            Bind_FishName_Dropdown();
            Bind_FishGrade_Dropdown();
            Bind_FishSize_Dropdown();

            for (int i = 0; i < GV_InvoiceDetail.Rows.Count; i++)
            {
                DropDownList cbox = GV_InvoiceDetail.Rows[i].FindControl("ddlFish") as DropDownList;
                if (i > 0) { cbox.Visible = false; }
                  cbox.SelectedIndex = cbox.Items.IndexOf(cbox.Items.FindByText(Product_Table.Rows[i]["Fish_Item"].ToString()));
                  
            }
            for (int i = 0; i < GV_InvoiceDetail.Rows.Count; i++)
            {
                DropDownList cbox = GV_InvoiceDetail.Rows[i].FindControl("ddlFishGrade") as DropDownList;
                if (i > 0) { cbox.Visible = false; }
                cbox.SelectedIndex = cbox.Items.IndexOf(cbox.Items.FindByText(Product_Table.Rows[i]["Fish_Grade"].ToString()));
            }
            for (int i = 0; i < GV_InvoiceDetail.Rows.Count; i++)
            {
                DropDownList cbox = GV_InvoiceDetail.Rows[i].FindControl("ddlFishSize") as DropDownList;
                cbox.SelectedIndex = cbox.Items.IndexOf(cbox.Items.FindByText(Product_Table.Rows[i]["Fish_Size"].ToString()));
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
        for (int i = 0; i < GV_InvoiceDetail.Rows.Count; i++)
        {
            if (GV_InvoiceDetail.Rows[i].Visible)
            {
           
                DropDownList ddlFish = (DropDownList)GV_InvoiceDetail.Rows[i].Cells[1].FindControl("ddlFish");
                DropDownList ddlFishGrade = (DropDownList)GV_InvoiceDetail.Rows[i].Cells[2].FindControl("ddlFishGrade");
                DropDownList ddlFishSize = (DropDownList)GV_InvoiceDetail.Rows[i].Cells[3].FindControl("ddlFishSize");
                TextBox txtDescription = (TextBox)GV_InvoiceDetail.Rows[i].Cells[4].FindControl("txtDescription");
                TextBox txtQuantity = (TextBox)GV_InvoiceDetail.Rows[i].Cells[5].FindControl("txtQuantity");
                TextBox txtRate = (TextBox)GV_InvoiceDetail.Rows[i].Cells[6].FindControl("txtRate");
                HtmlInputText txtTotalPrice = (HtmlInputText)GV_InvoiceDetail.Rows[i].Cells[7].FindControl("txtAmount");
                Label txtGrid = (Label)GV_InvoiceDetail.Rows[i].Cells[8].FindControl("txtGridName");
                dtProduct.Rows.Add(dtProduct.NewRow());
                if (ddlFish.SelectedValue.ToString() != "0")
                {
                    
                    dtProduct.Rows[i]["Fish_Item"] = ddlFish.SelectedItem.Text;
                    dtProduct.Rows[i]["Fish_value"] = ddlFish.SelectedValue;
                    dtProduct.Rows[i]["Fish_Grade"] = ddlFishGrade.SelectedItem.Text;
                    dtProduct.Rows[i]["Fish_Gradevalue"] = ddlFishGrade.SelectedValue;
                    dtProduct.Rows[i]["Fish_Size"] = ddlFishSize.SelectedItem.Text;
                    dtProduct.Rows[i]["Fish_Sizevalue"] = ddlFishSize.SelectedValue;
                    dtProduct.Rows[i]["txtDescription"] = txtDescription.Text;
                    dtProduct.Rows[i]["txtQuantity"] = txtQuantity.Text;
                    dtProduct.Rows[i]["txtRate"] = txtRate.Text;
                    dtProduct.Rows[i]["txtAmount"] = Convert.ToInt64(Convert.ToDouble(txtTotalPrice.Value));
                    dtProduct.Rows[i]["txtGridName"] = txtGrid.Text;

                }
            }
            else
            {
                dtProduct.Rows.Add(dtProduct.NewRow());
            }
        }
       
        for (int pr = 0; pr < Product_Table.Rows.Count; pr++)
        {
            if (Product_Table.Rows[pr]["Fish_Value"].ToString() == "")
            {
                Product_Table.Rows.RemoveAt(pr);
                pr--;
            }
        }
        for (int pr = 0; pr < Product_Table.Rows.Count; pr++)
        {
            if (Product_Table.Rows[pr]["Fish_GradeValue"].ToString() == "")
            {
                Product_Table.Rows.RemoveAt(pr);
                pr--;
            }
        }
        for (int pr = 0; pr < Product_Table.Rows.Count; pr++)
        {
            if (Product_Table.Rows[pr]["Fish_SizeValue"].ToString() == "")
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
         
                dt.Columns.Add(new DataColumn("Fish_Item", typeof(string)));
                dt.Columns.Add(new DataColumn("Fish_Grade", typeof(string)));
                dt.Columns.Add(new DataColumn("Fish_Size", typeof(string)));
                dt.Columns.Add(new DataColumn("txtDescription", typeof(string)));
                dt.Columns.Add(new DataColumn("txtQuantity", typeof(string)));
                dt.Columns.Add(new DataColumn("txtRate", typeof(string)));
                dt.Columns.Add(new DataColumn("txtAmount", typeof(string)));
            
                dt.Columns.Add(new DataColumn("Fish_Value", typeof(string)));
                dt.Columns.Add(new DataColumn("Fish_GradeValue", typeof(string)));
                dt.Columns.Add(new DataColumn("Fish_SizeValue", typeof(string)));

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
        SCGL_Common.ReloadJS(this, "DecimalOnly();");
        SCGL_Common.ReloadJS(this, "GrossTotalDeduction();");
        SCGL_Common.ReloadJS(this, "MyDate();");
        SCGL_Common.ReloadJS(this, "$('#Mytab').tabs();"); 
    }

    protected void GV_InvoiceDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GV_InvoiceDetail.Rows[e.RowIndex].Visible = false;
        
        DropDownList ddlFishName = (DropDownList)GV_InvoiceDetail.Rows[e.RowIndex].Cells[1].FindControl("ddlFish");
        TextBox txtDescription = (TextBox)GV_InvoiceDetail.Rows[e.RowIndex].Cells[2].FindControl("txtDescription");
        TextBox txtQuantity = (TextBox)GV_InvoiceDetail.Rows[e.RowIndex].Cells[3].FindControl("txtQuantity");
        TextBox txtRate = (TextBox)GV_InvoiceDetail.Rows[e.RowIndex].Cells[4].FindControl("txtRate");
        HtmlInputText txtAmount = GV_InvoiceDetail.Rows[e.RowIndex].Cells[5].FindControl("txtAmount") as HtmlInputText;

        if (txtAmount.Value == "") { txtAmount.Value = "0"; }

        ddlFishName.SelectedIndex = 0;
        txtDescription.Text = "";
        txtQuantity.Text = "";
        txtRate.Text = "";
        txtAmount.Value = "";
    }

    protected void GV_InvoiceDetail1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GV_InvoiceDetail1.Rows[e.RowIndex].Visible = false;

        DropDownList ddlFishName = (DropDownList)GV_InvoiceDetail1.Rows[e.RowIndex].Cells[1].FindControl("ddlFish");
        TextBox txtDescription = (TextBox)GV_InvoiceDetail1.Rows[e.RowIndex].Cells[2].FindControl("txtDescription");
        TextBox txtQuantity = (TextBox)GV_InvoiceDetail1.Rows[e.RowIndex].Cells[3].FindControl("txtQuantity");
        TextBox txtRate = (TextBox)GV_InvoiceDetail1.Rows[e.RowIndex].Cells[4].FindControl("txtRate");
        HtmlInputText txtAmount = GV_InvoiceDetail1.Rows[e.RowIndex].Cells[5].FindControl("txtAmount") as HtmlInputText;

        if (txtAmount.Value == "") { txtAmount.Value = "0"; }

        ddlFishName.SelectedIndex = 0;
        txtDescription.Text = "";
        txtQuantity.Text = "";
        txtRate.Text = "";
        txtAmount.Value = "";
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
                        SCGL_Common.Success_Message(this.Page, "Invoice_Views.aspx");
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
        try
        {
            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            if (btnSave.Text == "Save")
            {

                BALInvoice.InvoiceID = -1;
            }
            else
            {
                //BALInvoice.InvoiceID = SCGL_Common.Convert_ToInt(Request.QueryString["InvoiceID"]);
                BALInvoice.InvoiceID = SCGL_Common.Convert_ToInt(Request.QueryString["Id"]);
            }
            BALInvoice.CustomerID = SCGL_Common.Convert_ToInt(ddlCustomer.SelectedValue);
            BALInvoice.Email = txtEmail.Text;
            BALInvoice.BillingAddress = txtBillingAddress.Text;
            BALInvoice.Invoice_No = txtInvoiceNumber.Text;  //  Database column name for this field : InvoiceNo
            BALInvoice.TermID = txtTerm.Text;
            BALInvoice.InvoiceDate = SCGL_Common.CheckDateTime(txtInvoiceDate.Text);
            BALInvoice.DueDate = SCGL_Common.CheckDateTime(txtDueDate.Text);
            //BALInvoice.DiscountID = SCGL_Common.Convert_ToInt(ddlDiscount.SelectedValue);

            //BALInvoice.Discount = txtDiscount.Text == "" ? SCGL_Common.Convert_ToDecimal("0")
            //    : SCGL_Common.Convert_ToDecimal(txtDiscount.Text);


            BALInvoice.Total = SCGL_Common.Convert_ToDecimal(txttotal2.Value);
            BALInvoice.LoginID = SBO.UserID;
            BALInvoice.FinYearID = SBO.FinYearID;
            BALInvoice.From = txtFrom.Text;
            BALInvoice.To = txtTo.Text;
            BALInvoice.Vessel = txtVessel.Text;
            BALInvoice.FormENo = txtFormENo.Text;
            BALInvoice.Freight = txtFreight.Text;
            BALInvoice.NetWeight = SCGL_Common.Convert_ToDecimal(txtNetWeight.Text);
            BALInvoice.GrossWeight = SCGL_Common.Convert_ToDecimal(txtGrossWeight.Text);
            BALInvoice.ContainerNo = txtContainerNo.Text;
            BALInvoice.ProformaNo = txtproformaNo.Text;
            BALInvoice.Insurance = txtInsurance.Text;


            if (BALInvoice.CreateModifyInvoice(BALInvoice, trans))
            {

                //Insert in Invoice Detail
                BALInvoice.InvoiceNo = SCGL_Common.Convert_ToInt(txtInvoiceID.Text);
                if (btnSave.Text == "Update")
                {
                    BALInvoice.Delete_InvoiceDetail(BALInvoice.InvoiceNo, trans);
                }
                int Counter = 0;
                DataTable dt = Record_for_Insert();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BALInvoice.ProductServiceID = SCGL_Common.Convert_ToInt(dt.Rows[i]["Item_Value"].ToString());
                    BALInvoice.Description = dt.Rows[i]["txtDescription"].ToString();
                    BALInvoice.Quantity = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtQuantity"].ToString());
                    BALInvoice.Rate = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtRate"].ToString());
                    BALInvoice.Amount = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtAmount"].ToString());
                    BALInvoice.GridName = dt.Rows[i]["txtGridname"].ToString();

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
        Product_Table2 = Product_DataSource1();

        int Product_Rows = Product_Table.Rows.Count;
        int Deduction_Rows = Product_Table2.Rows.Count;

        int TotalRows = Product_Rows + Deduction_Rows;
        //if (Product_Rows >0 )
        //{
        //    TotalRows = Product_Rows;
        //}
        //if (Deduction_Rows > 0)
        //{
        //    TotalRows = Deduction_Rows;
        //}


        DataTable dtInsert = new DataTable();
        dtInsert.Merge(Product_Table);
        dtInsert.Merge(Product_Table2);

        //dtInsert.Rows.Clear();
        //for (int r = 0; r < TotalRows; r++)
        //{
        //    dtInsert.Rows.Add(dtInsert.NewRow());
        //}
        ////for (int p = 0; p < Product_Rows; p++)
        ////{
        ////    dtInsert.Rows[p]["Item"] = Product_Table.Rows[p]["Item_Value"].ToString();
        ////    dtInsert.Rows[p]["txtDescription"] = Product_Table.Rows[p]["txtDescription"].ToString();
        ////    dtInsert.Rows[p]["txtQuantity"] = Product_Table.Rows[p]["txtQuantity"].ToString();
        ////    dtInsert.Rows[p]["txtRate"] = Product_Table.Rows[p]["txtRate"].ToString();

        ////    dtInsert.Rows[p]["txtAmount"] = Product_Table.Rows[p]["txtAmount"].ToString();
        ////}


        return dtInsert;
    }

    public bool Check_Validation()
    {
        bool IsValid = true;
        DropDownList ddlItem = (DropDownList)GV_InvoiceDetail.Rows[0].FindControl("ddlItem");

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

            int DynamicID = Convert.ToInt32(row["InvoiceID"]);
            int totalDraftID = staticID + DynamicID;
            txtInvoiceID.Text = Convert.ToInt32(totalDraftID).ToString();
        }
        txtInvoiceID.ReadOnly = true;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        SetInitialRow();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Invoice_Views.aspx");
    }
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomerForm_BAL custbal = new CustomerForm_BAL();
        DataTable dt = custbal.getCustomerByID(SCGL_Common.Convert_ToInt(ddlCustomer.SelectedValue));
        txtBillingAddress.Text = dt.Rows[0]["BillAddressStreet"].ToString();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {


        //DataTable dtsa = Product_Table;
        //GVGroup.DataSource = dtsa;
        //GVGroup.DataBind();





    }

    protected void btnaddlines2_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid2();
    }

    public void AddNewRowToGrid2()
    {
        try
        {
            Product_Table2.Rows.Clear();
            for (int i = 0; i < GV_InvoiceDetail1.Rows.Count; i++)
            {
                if (GV_InvoiceDetail1.Rows[i].Visible)
                {
                    DataRow drCurrentRow2 = Product_Table2.NewRow();
                    DropDownList ddlFish = (DropDownList)GV_InvoiceDetail1.Rows[i].Cells[1].FindControl("ddlFish");
                    DropDownList ddlFishGrade = (DropDownList)GV_InvoiceDetail1.Rows[i].Cells[2].FindControl("ddlFishGrade");
                    DropDownList ddlFishSize = (DropDownList)GV_InvoiceDetail1.Rows[i].Cells[3].FindControl("ddlFishSize");
                    TextBox txtDescription = (TextBox)GV_InvoiceDetail1.Rows[i].Cells[4].FindControl("txtDescription");
                    TextBox txtQuantity = (TextBox)GV_InvoiceDetail1.Rows[i].Cells[5].FindControl("txtQuantity");
                    TextBox txtRate = (TextBox)GV_InvoiceDetail1.Rows[i].Cells[6].FindControl("txtRate");
                    HtmlInputText txtTotalPrice = (HtmlInputText)GV_InvoiceDetail1.Rows[i].Cells[7].FindControl("txtAmount");
                    Label txtGrid = (Label)GV_InvoiceDetail1.Rows[i].Cells[8].FindControl("txtGridName");

                    drCurrentRow2["Fish_Item"] = ddlFish.SelectedItem.Text;
                    drCurrentRow2["Fish_value"] = ddlFish.SelectedValue;
                    drCurrentRow2["Fish_Grade"] = ddlFishGrade.SelectedItem.Text;
                    drCurrentRow2["Fish_Gradevalue"] = ddlFishGrade.SelectedValue;
                    drCurrentRow2["Fish_Size"] = ddlFishSize.SelectedItem.Text;
                    drCurrentRow2["Fish_Sizevalue"] = ddlFishSize.SelectedValue;
                    drCurrentRow2["txtDescription"] = txtDescription.Text;
                    drCurrentRow2["txtQuantity"] = txtQuantity.Text;
                    drCurrentRow2["txtRate"] = txtRate.Text;
                    drCurrentRow2["txtAmount"] = txtTotalPrice.Value;
                    drCurrentRow2["txtGridName"] = txtGrid.Text;

                    Product_Table2.Rows.Add(drCurrentRow2);
                }
            }
            DataRow dr = Product_Table2.NewRow();
            dr[0] = "--Select Item--";
            Product_Table2.Rows.Add(dr);
            GV_InvoiceDetail1.DataSource = Product_Table2;
            GV_InvoiceDetail1.DataBind();
            Bind_FishGrade_Dropdown();
            Bind_FishName_Dropdown();
            Bind_FishSize_Dropdown();
            for (int i = 0; i < GV_InvoiceDetail1.Rows.Count; i++)
            {
                DropDownList cbox = GV_InvoiceDetail1.Rows[i].FindControl("ddlFish") as DropDownList;
                if (i > 0) { cbox.Visible = false; }
                cbox.SelectedIndex = cbox.Items.IndexOf(cbox.Items.FindByText(Product_Table2.Rows[i]["Fish_Item"].ToString()));

            }
            for (int i = 0; i < GV_InvoiceDetail1.Rows.Count; i++)
            {
                DropDownList cbox = GV_InvoiceDetail1.Rows[i].FindControl("ddlFishGrade") as DropDownList;
                if (i > 0) { cbox.Visible = false; }
                cbox.SelectedIndex = cbox.Items.IndexOf(cbox.Items.FindByText(Product_Table2.Rows[i]["Fish_Grade"].ToString()));
            }
            for (int i = 0; i < GV_InvoiceDetail1.Rows.Count; i++)
            {
                DropDownList cbox = GV_InvoiceDetail1.Rows[i].FindControl("ddlFishSize") as DropDownList;
                cbox.SelectedIndex = cbox.Items.IndexOf(cbox.Items.FindByText(Product_Table2.Rows[i]["Fish_Size"].ToString()));
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private DataTable Product_Table2
    {
        get
        {
            DataTable dt2 = (DataTable)ViewState["CurrentTable2"];
            if (dt2 == null)
            {

                dt2.Columns.Add(new DataColumn("RowNumber", typeof(string)));

                dt2.Columns.Add(new DataColumn("Fish_Item", typeof(string)));
                dt2.Columns.Add(new DataColumn("Fish_Grade", typeof(string)));
                dt2.Columns.Add(new DataColumn("Fish_Size", typeof(string)));
                dt2.Columns.Add(new DataColumn("txtDescription", typeof(string)));
                dt2.Columns.Add(new DataColumn("txtQuantity", typeof(string)));
                dt2.Columns.Add(new DataColumn("txtRate", typeof(string)));
                dt2.Columns.Add(new DataColumn("txtAmount", typeof(string)));

                dt2.Columns.Add(new DataColumn("Fish_Value", typeof(string)));
                dt2.Columns.Add(new DataColumn("Fish_GradeValue", typeof(string)));
                dt2.Columns.Add(new DataColumn("Fish_SizeValue", typeof(string)));
                for (int i = 0; i < 10; i++)
                {
                    dt2.Rows.Add(dt2.NewRow());
                }
            }
            ViewState["CurrentTable2"] = dt2;
            return dt2;
        }
        set
        {
            ViewState["CurrentTable2"] = value;
        }
    }

    public DataTable Product_DataSource1()
    {

        DataTable dtProduct = Product_Table2; dtProduct.Rows.Clear();
        for (int i = 0; i < GV_InvoiceDetail1.Rows.Count; i++)
        {
            if (GV_InvoiceDetail1.Rows[i].Visible)
            {
                DropDownList ddlFish = (DropDownList)GV_InvoiceDetail.Rows[i].Cells[1].FindControl("ddlFish");
                DropDownList ddlFishGrade = (DropDownList)GV_InvoiceDetail.Rows[i].Cells[2].FindControl("ddlFishGrade");
                DropDownList ddlFishSize = (DropDownList)GV_InvoiceDetail.Rows[i].Cells[3].FindControl("ddlFishSize");
                TextBox txtDescription = (TextBox)GV_InvoiceDetail1.Rows[i].Cells[4].FindControl("txtDescription");
                TextBox txtQuantity = (TextBox)GV_InvoiceDetail1.Rows[i].Cells[5].FindControl("txtQuantity");
                TextBox txtRate = (TextBox)GV_InvoiceDetail1.Rows[i].Cells[6].FindControl("txtRate");
                HtmlInputText txtTotalPrice = (HtmlInputText)GV_InvoiceDetail1.Rows[i].Cells[7].FindControl("txtAmount");
                Label txtGrid = (Label)GV_InvoiceDetail1.Rows[i].Cells[8].FindControl("txtGridName");
                dtProduct.Rows.Add(dtProduct.NewRow());
                if (ddlFish.SelectedValue.ToString() != "0")
                {
                    dtProduct.Rows[i]["Fish_Item"] = ddlFish.SelectedItem.Text;
                    dtProduct.Rows[i]["Fish_value"] = ddlFish.SelectedValue;
                    dtProduct.Rows[i]["Fish_Grade"] = ddlFishGrade.SelectedItem.Text;
                    dtProduct.Rows[i]["Fish_Gradevalue"] = ddlFishGrade.SelectedValue;
                    dtProduct.Rows[i]["Fish_Size"] = ddlFishSize.SelectedItem.Text;
                    dtProduct.Rows[i]["Fish_Sizevalue"] = ddlFishSize.SelectedValue;
                    dtProduct.Rows[i]["txtDescription"] = txtDescription.Text;
                    dtProduct.Rows[i]["txtQuantity"] = txtQuantity.Text;
                    dtProduct.Rows[i]["txtRate"] = txtRate.Text;
                    dtProduct.Rows[i]["txtAmount"] = txtTotalPrice.Value;
                    dtProduct.Rows[i]["txtGridName"] = txtGrid.Text;
                }
            }
            else
            {
                dtProduct.Rows.Add(dtProduct.NewRow());
            }
        }
        for (int pr = 0; pr < Product_Table.Rows.Count; pr++)
        {
            if (Product_Table.Rows[pr]["Fish_Value"].ToString() == "")
            {
                Product_Table.Rows.RemoveAt(pr);
                pr--;
            }
        }
        for (int pr = 0; pr < Product_Table.Rows.Count; pr++)
        {
            if (Product_Table.Rows[pr]["Fish_GradeValue"].ToString() == "")
            {
                Product_Table.Rows.RemoveAt(pr);
                pr--;
            }
        }
        for (int pr = 0; pr < Product_Table.Rows.Count; pr++)
        {
            if (Product_Table.Rows[pr]["Fish_SizeValue"].ToString() == "")
            {
                Product_Table.Rows.RemoveAt(pr);
                pr--;
            }
        }
        return dtProduct;
    }
    protected void btnClear2_Click(object sender, EventArgs e)
    {
        //SetInitialRow();
    }
    protected void GV_InvoiceDetail1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
