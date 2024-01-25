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

public partial class invoice : System.Web.UI.Page
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "invoice.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "invoice.aspx" && view == true)
                {
                    GetMaxInvoiceId();
                    Bind_Customer();
                    if (Request.QueryString["Id"] != null)
                    {
                        BindControl(Convert.ToInt32(Request.QueryString["Id"]));
                        Gv_GetRows1();
                        Gv_GetRows2();
                        SetInitialRow_For_Edit();
                        FillGridInvoiceDetail();
                        FillGridInvoiceDetail1();
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
        Invoice_BAL invoiceBal = new Invoice_BAL();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
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
            //txtInvoiceDate.Text = dt.Rows[0]["InvoiceDate"].ToString();
            txtDueDate.Text = SCGL_Common.CheckDateTime(dt.Rows[0]["DueDate"]).ToShortDateString();
            //txtDueDate.Text = dt.Rows[0]["DueDate"].ToString();
            txttotal2.Value = dt.Rows[0]["Total"].ToString();
            txtDiscount.Text = dt.Rows[0]["Discount"].ToString();
            txtFrom.Text = dt.Rows[0]["From"].ToString();
            txtTo.Text = dt.Rows[0]["To"].ToString();
            txtContainerNo.Text = dt.Rows[0]["ContainerNo"].ToString();
            txtOrCountry.Text = dt.Rows[0]["Origin_Country"].ToString();
            txtDestCountry.Text = dt.Rows[0]["Destination_Country"].ToString();
            txtVessel.Text = dt.Rows[0]["Vessel"].ToString();
            txtFormENo.Text = dt.Rows[0]["FormENo"].ToString();
            txtFreight.Text = dt.Rows[0]["Freight"].ToString();
            txtNetWeight.Text = dt.Rows[0]["NetWeight"].ToString();
            txtGrossWeight.Text = dt.Rows[0]["GrossWeight"].ToString();
            txtproformaNo.Text = dt.Rows[0]["ProformaNo"].ToString();
            txtInsurance.Text = dt.Rows[0]["Insurance"].ToString();
            txtExporter.Text = dt.Rows[0]["Exporter"].ToString();
            txtConsignee.Text = dt.Rows[0]["Consignee"].ToString();
            txtBuyer.Text = dt.Rows[0]["Buyer"].ToString();
            txtExportersRef.Text = dt.Rows[0]["ExportersRef"].ToString();
            txtNote.Text = dt.Rows[0]["Note"].ToString();
            btnSave.Text = "Update";
        }
    }

    public void Bind_Customer()
    {
        SCGL_Common.Bind_DropDown(ddlCustomer, "vt_SCGL_BindCustomer", "CustomerName", "ID");
    }

    public void Bind_Product_Dropdown()
    {
        for (int i = 0; i < GV_InvoiceDetail.Rows.Count; i++)
        {
            DropDownList ddlItem = GV_InvoiceDetail.Rows[i].FindControl("ddlItem") as DropDownList;
            if(ddlItem != null){
            SCGL_Common.Bind_DropDown(ddlItem, "vt_SCGL_SP_GetFishCart", "item", "Inventory_ID");
            }
        }
    }

    int FishId = 0;
    int FishGradeId = 0;
    int FishId1 = 0;
    int FishGradeId1 = 0;

    public void Bind_FishName_Dropdown()
    {
        for (int i = 0; i < GV_InvoiceDetail.Rows.Count; i++)
        {
            DropDownList ddlFish = GV_InvoiceDetail.Rows[i].FindControl("ddlFish") as DropDownList;
            SCGL_Common.Bind_DropDown(ddlFish, "vt_SCGL_Sp_GetFishName", "FishName", "FishID");
            ddlFish.SelectedValue = SCGL_Common.Convert_ToString(Product_Table.Rows[i]["Fish_Value"]);
            if (i == 0)
            {               
                FishId = int.Parse(ddlFish.SelectedValue);                
            }
        }
    }

    public void Bind_FishName_Dropdown1()
    {
        for (int i = 0; i < GV_InvoiceDetail1.Rows.Count; i++)
        {
            DropDownList ddlFish = GV_InvoiceDetail1.Rows[i].FindControl("ddlFish") as DropDownList;
            SCGL_Common.Bind_DropDown(ddlFish, "vt_SCGL_Sp_GetFishName", "FishName", "FishID");
            ddlFish.SelectedValue = SCGL_Common.Convert_ToString(Product_Table2.Rows[i]["Fish_Value"]);
            if (i == 0)
            {
                FishId1 = int.Parse(ddlFish.SelectedValue);
            }
        }
    }

    public void Bind_FishGrade_Dropdown()
    {        
        for (int i = 0; i < GV_InvoiceDetail.Rows.Count; i++)
        {
            DropDownList ddlFishDetail = GV_InvoiceDetail.Rows[i].FindControl("ddlFishGrade") as DropDownList;
            DropDownList ddlFish = GV_InvoiceDetail.Rows[i].FindControl("ddlFish") as DropDownList;
            if (i == 0)
            {                
                FishId = SCGL_Common.Convert_ToInt(ddlFish.SelectedValue);               
            }
            if (Request.QueryString["ID"] != null)
            {
                InvoiceDetail_BAL inv_dtl = new InvoiceDetail_BAL();
                DataTable dtInvoiceDetail = inv_dtl.getInvoiceDetailByInvoiceID(SCGL_Common.Convert_ToInt(Request.QueryString["ID"]));
                FishId = SCGL_Common.Convert_ToInt(dtInvoiceDetail.Rows[0]["FishID"].ToString());
            }
            SCGL_Common.Bind_DropDown(ddlFishDetail, "vt_SCGL_Sp_GetFishGrade", "FishGrade", "FishGradeID", "@FishID", FishId);
            ddlFishDetail.SelectedValue = SCGL_Common.Convert_ToString(Product_Table.Rows[i]["Fish_gradeValue"]);
            if (i == 0)
            {
                FishGradeId = int.Parse(ddlFishDetail.SelectedValue);
            }
        }
    }

    public void Bind_FishGrade_Dropdown1()
    {
        for (int i = 0; i < GV_InvoiceDetail1.Rows.Count; i++)
        {
            DropDownList ddlFish = GV_InvoiceDetail1.Rows[i].FindControl("ddlFishGrade") as DropDownList;
            if (Request.QueryString["ID"] != null)
            {
                InvoiceDetail_BAL inv_dtl = new InvoiceDetail_BAL();
                DataTable dtInvoiceDetail1 = inv_dtl.getInvoiceDetailByInvoiceID1(SCGL_Common.Convert_ToInt(Request.QueryString["ID"]));
                if (dtInvoiceDetail1.Rows.Count > 0)
                {
                    FishId1 = SCGL_Common.Convert_ToInt(dtInvoiceDetail1.Rows[0]["FishID"].ToString());
                }
            }
            SCGL_Common.Bind_DropDown(ddlFish, "vt_SCGL_Sp_GetFishGrade", "FishGrade", "FishGradeID", "@FishID", FishId1);
            ddlFish.SelectedValue = SCGL_Common.Convert_ToString(Product_Table2.Rows[i]["Fish_gradeValue"]);
            if (i == 0)
            {
                FishGradeId1 = int.Parse(ddlFish.SelectedValue);
            }
        }
    }

    public void Bind_FishSize_Dropdown()
    {
        for (int i = 0; i < GV_InvoiceDetail.Rows.Count; i++)
        {
            DropDownList ddlFish = GV_InvoiceDetail.Rows[i].FindControl("ddlFishSize") as DropDownList;
            if (Request.QueryString["ID"] != null)
            {
                InvoiceDetail_BAL inv_dtl = new InvoiceDetail_BAL();
                DataTable dtInvoiceDetail = inv_dtl.getInvoiceDetailByInvoiceID(SCGL_Common.Convert_ToInt(Request.QueryString["ID"]));

                FishGradeId = SCGL_Common.Convert_ToInt(dtInvoiceDetail.Rows[0]["FishGradeID"].ToString());
            }
            SCGL_Common.Bind_DropDown(ddlFish, "vt_SCGL_Sp_GetFishSize", "FishSize", "FishSizeID", "@FishID", FishId, "@GradId", FishGradeId);
        }
    }

    public void Bind_FishSize_Dropdown1()
    {
        for (int i = 0; i < GV_InvoiceDetail1.Rows.Count; i++)
        {
            DropDownList ddlFish = GV_InvoiceDetail1.Rows[i].FindControl("ddlFishSize") as DropDownList;
            if (Request.QueryString["ID"] != null)
            {
                InvoiceDetail_BAL inv_dtl = new InvoiceDetail_BAL();
                DataTable dtInvoiceDetail1 = inv_dtl.getInvoiceDetailByInvoiceID1(SCGL_Common.Convert_ToInt(Request.QueryString["ID"]));
                if (dtInvoiceDetail1.Rows.Count > 0)
                {
                    FishGradeId1 = SCGL_Common.Convert_ToInt(dtInvoiceDetail1.Rows[0]["FishGradeID"].ToString());
                }
            }
            SCGL_Common.Bind_DropDown(ddlFish, "vt_SCGL_Sp_GetFishSize", "FishSize", "FishSizeID", "@FishID", FishId1, "@GradId", FishGradeId1);
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
        dt.Columns.Add(new DataColumn("lblInventoryId", typeof(string)));
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
        dt2.Columns.Add(new DataColumn("lblInventoryId", typeof(string)));
        for (int i = 0; i < 1; i++)
        {
            dr2 = dt2.NewRow();
            dt2.Rows.Add(dr2);
        }
        ViewState["CurrentTable"] = dt;
        ViewState["CurrentTable2"] = dt2;
        GV_InvoiceDetail.DataSource = dt;
        GV_InvoiceDetail.DataBind();
        GV_InvoiceDetail1.DataSource = dt2;
        GV_InvoiceDetail1.DataBind();
        Bind_FishName_Dropdown();
        Bind_FishGrade_Dropdown();
        Bind_FishSize_Dropdown();
        Bind_FishName_Dropdown1();
        Bind_FishGrade_Dropdown1();
        Bind_FishSize_Dropdown1();
    }

    protected void btnaddnewRow_Click(object sender, EventArgs e)
    {
        DropDownList ddlFishRow2 = (DropDownList)GV_InvoiceDetail.Rows[0].Cells[1].FindControl("ddlFish");
        DropDownList ddlFishGradeRow2 = (DropDownList)GV_InvoiceDetail.Rows[0].Cells[2].FindControl("ddlFishGrade");
        if (int.Parse(ddlFishRow2.SelectedValue) > 0 && int.Parse(ddlFishGradeRow2.SelectedValue) > 0)
        {
            AddNewRowToGrid();
            for (int i = 1; i < GV_InvoiceDetail.Rows.Count; i++)
            {
                DropDownList ddlFishRow1 = (DropDownList)GV_InvoiceDetail.Rows[0].Cells[1].FindControl("ddlFish");
                DropDownList ddlFishGradeRow1 = (DropDownList)GV_InvoiceDetail.Rows[0].Cells[2].FindControl("ddlFishGrade");
                DropDownList ddlFish = (DropDownList)GV_InvoiceDetail.Rows[i].Cells[1].FindControl("ddlFish");
                DropDownList ddlFishGrade = (DropDownList)GV_InvoiceDetail.Rows[i].Cells[2].FindControl("ddlFishGrade");
                ddlFish.SelectedValue = ddlFishRow1.SelectedValue;
                ddlFish.Enabled = false;
                ddlFishGrade.SelectedValue = ddlFishGradeRow1.SelectedValue;
                ddlFishGrade.Enabled = false;
            }
        }       
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
                    Label InventoryId = (Label)GV_InvoiceDetail.Rows[i].Cells[9].FindControl("lblInventoryId");
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
                    if (SCGL_Common.Convert_ToInt(ddlFish.SelectedValue) > 0 && SCGL_Common.Convert_ToInt(ddlFishGrade.SelectedValue) > 0 && SCGL_Common.Convert_ToInt(ddlFishSize.SelectedValue) > 0)
                    {
                        DataTable dtRetInventory = BALInvoice.getInvntoryByID(int.Parse(ddlFish.SelectedValue), int.Parse(ddlFishGrade.SelectedValue), int.Parse(ddlFishSize.SelectedValue));
                        drCurrentRow["lblInventoryId"] = dtRetInventory.Rows[0]["Inventory_Id"].ToString();
                        Product_Table.Rows.Add(drCurrentRow);
                    }
                }
            }
            DataRow dr = Product_Table.NewRow();
            dr[0] = "--Select Item--";
            Product_Table.Rows.Add(dr);
            GV_InvoiceDetail.DataSource = Product_Table;
            GV_InvoiceDetail.DataBind();
            Bind_FishName_Dropdown();
            Bind_FishGrade_Dropdown();
            Bind_FishSize_Dropdown();
            for (int i = 0; i < GV_InvoiceDetail.Rows.Count; i++)
            {
                DropDownList cbox = GV_InvoiceDetail.Rows[i].FindControl("ddlFish") as DropDownList;
                if (i > 0)
                {
                    cbox.Visible = false;
                }
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
        DataTable dtProduct = Product_Table;
        dtProduct.Rows.Clear();
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
                Label lblInventory = (Label)GV_InvoiceDetail.Rows[i].Cells[9].FindControl("lblInventoryId");                
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
                    if (SCGL_Common.Convert_ToInt(ddlFish.SelectedValue) > 0 && SCGL_Common.Convert_ToInt(ddlFishGrade.SelectedValue) > 0 && SCGL_Common.Convert_ToInt(ddlFishSize.SelectedValue) > 0)
                    {
                        DataTable dtRetInventory = BALInvoice.getInvntoryByID(int.Parse(ddlFish.SelectedValue), int.Parse(ddlFishGrade.SelectedValue), int.Parse(ddlFishSize.SelectedValue));
                        dtProduct.Rows[i]["lblInventoryId"] = dtRetInventory.Rows[0]["Inventory_Id"].ToString();
                    }
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
                dt.Columns.Add(new DataColumn("lblInventoryId", typeof(string)));
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
        SCGL_Common.ReloadJS(this, "vale2();");                                                                                                        
        SCGL_Common.ReloadJS(this, "DecimalOnly();");
        SCGL_Common.ReloadJS(this, "GrossTotalDeduction();");
        SCGL_Common.ReloadJS(this, "GrossTotalDeduction2();");//calculateSum()
        SCGL_Common.ReloadJS(this, "MyDate();");
        SCGL_Common.ReloadJS(this, "$('#Mytab').tabs();");
        SCGL_Common.ReloadJS(this, "TxtBlur();");
        SCGL_Common.ReloadJS(this, "calculateSum();");
        SCGL_Common.ReloadJS(this, "setTabHref();");

    }

    protected void GV_InvoiceDetail1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = e.RowIndex;
        if (index != 0)
        {
            GV_InvoiceDetail1.Rows[e.RowIndex].Visible = false;
            DropDownList ddlFishName = (DropDownList)GV_InvoiceDetail1.Rows[e.RowIndex].Cells[1].FindControl("ddlFish");
            DropDownList ddlFishGrade = (DropDownList)GV_InvoiceDetail1.Rows[e.RowIndex].Cells[2].FindControl("ddlFishGrade");
            TextBox txtDescription = (TextBox)GV_InvoiceDetail1.Rows[e.RowIndex].Cells[3].FindControl("txtDescription");
            TextBox txtQuantity = (TextBox)GV_InvoiceDetail1.Rows[e.RowIndex].Cells[4].FindControl("txtQuantity");
            TextBox txtRate = (TextBox)GV_InvoiceDetail1.Rows[e.RowIndex].Cells[5].FindControl("txtRate");
            HtmlInputText txtAmount = GV_InvoiceDetail1.Rows[e.RowIndex].Cells[6].FindControl("txtAmount") as HtmlInputText;
            Label lblInventory = (Label)GV_InvoiceDetail1.Rows[e.RowIndex].Cells[7].FindControl("lblInventoryId");
            if (txtAmount.Value == "")
            {
                txtAmount.Value = "0";
            }
            ddlFishName.SelectedIndex = 0;
            txtDescription.Text = "";
            txtQuantity.Text = "";
            txtRate.Text = "";
            txtAmount.Value = "";
            lblInventory.Text = "";
        }
       
    }

    protected void GV_InvoiceDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = e.RowIndex;
        if (index != 0)
        {
            GV_InvoiceDetail.Rows[e.RowIndex].Visible = false;
            DropDownList ddlFishName = (DropDownList)GV_InvoiceDetail.Rows[e.RowIndex].Cells[1].FindControl("ddlFish");
            TextBox txtDescription = (TextBox)GV_InvoiceDetail.Rows[e.RowIndex].Cells[2].FindControl("txtDescription");
            TextBox txtQuantity = (TextBox)GV_InvoiceDetail.Rows[e.RowIndex].Cells[3].FindControl("txtQuantity");
            TextBox txtRate = (TextBox)GV_InvoiceDetail.Rows[e.RowIndex].Cells[4].FindControl("txtRate");
            HtmlInputText txtAmount = GV_InvoiceDetail.Rows[e.RowIndex].Cells[5].FindControl("txtAmount") as HtmlInputText;
            Label lblInventory = (Label)GV_InvoiceDetail.Rows[e.RowIndex].Cells[6].FindControl("lblInventoryId");
            if (txtAmount.Value == "") 
            {
                txtAmount.Value = "0";
            }
            ddlFishName.SelectedIndex = 0;
            txtDescription.Text = "";
            txtQuantity.Text = "";
            txtRate.Text = "";
            txtAmount.Value = "";
            lblInventory.Text = "";
        }
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
                BALInvoice.InvoiceID = SCGL_Common.Convert_ToInt(Request.QueryString["Id"]);
            }
            BALInvoice.CustomerID = SCGL_Common.Convert_ToInt(ddlCustomer.SelectedValue);
            BALInvoice.Email = txtEmail.Text;
            BALInvoice.BillingAddress = txtBillingAddress.Text;
            BALInvoice.Invoice_No = txtInvoiceNumber.Text;  //  Database column name for this field : InvoiceNo
            BALInvoice.TermID = txtTerm.Text;
            BALInvoice.InvoiceDate = Convert.ToDateTime(SCGL_Common.CheckDateTime(txtInvoiceDate.Text));
            //BALInvoice.InvoiceDate = txtInvoiceDate.Text.ToString();
            BALInvoice.DueDate = SCGL_Common.CheckDateTime(txtDueDate.Text);
            //BALInvoice.DueDate = txtDueDate.Text.ToString();
            BALInvoice.Total = SCGL_Common.Convert_ToDecimal(txttotal2.Value);
            BALInvoice.LoginID = SBO.UserID;
            BALInvoice.FinYearID = SBO.FinYearID;
            BALInvoice.From = txtFrom.Text;
            BALInvoice.To = txtTo.Text;
            BALInvoice.GrossWeight = SCGL_Common.Convert_ToDecimal(txtGrossWeight.Text);
            BALInvoice.Origin_Country = txtOrCountry.Text;
            BALInvoice.Destination_Country = txtDestCountry.Text;
            BALInvoice.Vessel = txtVessel.Text;
            BALInvoice.FormENo = txtFormENo.Text;
            BALInvoice.Freight = txtFreight.Text;
            BALInvoice.NetWeight = SCGL_Common.Convert_ToDecimal(txtNetWeight.Text);
            BALInvoice.ContainerNo = txtContainerNo.Text;
            BALInvoice.ProformaNo = txtproformaNo.Text;
            BALInvoice.Insurance = txtInsurance.Text;
            BALInvoice.Exporter = txtExporter.Text;
            BALInvoice.Consignee = txtConsignee.Text;
            BALInvoice.Buyer = txtBuyer.Text;
            BALInvoice.ExportersRef = txtExportersRef.Text;
            BALInvoice.Note = txtNote.Text;  
            if (BALInvoice.CreateModifyInvoice(BALInvoice, trans))
            {
                BALInvoice.InvoiceNo = SCGL_Common.Convert_ToInt(txtInvoiceID.Text);
                if (btnSave.Text == "Update")
                {
                    BALInvoice.Delete_InvoiceDetail(BALInvoice.InvoiceNo, trans);
                }
                int Counter = 0;
                DataTable dt = Record_for_Insert();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BALInvoice.InvoiceNo = int.Parse(txtInvoiceNumber.Text);
                    BALInvoice.Description = dt.Rows[i]["txtDescription"].ToString();
                    BALInvoice.Quantity = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtQuantity"].ToString());
                    BALInvoice.Rate = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtRate"].ToString());
                    BALInvoice.Amount = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtAmount"].ToString());
                    BALInvoice.GridName = dt.Rows[i]["txtGridname"].ToString();
                    BALInvoice.InventoryID =SCGL_Common.Convert_ToInt(dt.Rows[i]["lblInventoryId"].ToString());
                    BALInvoice.FishID = int.Parse(dt.Rows[i]["Fish_Value"].ToString());
                    BALInvoice.FishGradeID = int.Parse(dt.Rows[i]["Fish_GradeValue"].ToString());
                    BALInvoice.FishSizeID = int.Parse(dt.Rows[i]["Fish_SizeValue"].ToString());
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
        DataTable dtInsert = new DataTable();
        dtInsert.Merge(Product_Table);
        dtInsert.Merge(Product_Table2);
        return dtInsert;
    }
    
    public bool Check_Validation()
    {
        bool IsValid = true;
        for (int i = 0; i < GV_InvoiceDetail.Rows.Count; i++)
        {
            DropDownList ddlFish = GV_InvoiceDetail.Rows[i].FindControl("ddlFish") as DropDownList;
            DropDownList ddlFishGrade = GV_InvoiceDetail.Rows[i].FindControl("ddlFishGrade") as DropDownList;
            DropDownList ddlFishSize = GV_InvoiceDetail.Rows[i].FindControl("ddlFishSize") as DropDownList;
            if (ddlFish.SelectedValue != "0")
            {
                if (ddlFishGrade.SelectedValue == "0" || ddlFishSize.SelectedValue == "0")
                {
                    SCGL_Common.Error_Message(this);

                    return false;
                }
            }
        }

        for (int i = 0; i < GV_InvoiceDetail1.Rows.Count; i++)
        {
            DropDownList ddlFish = GV_InvoiceDetail1.Rows[i].FindControl("ddlFish") as DropDownList;
            DropDownList ddlFishGrade = GV_InvoiceDetail1.Rows[i].FindControl("ddlFishGrade") as DropDownList;
            DropDownList ddlFishSize = GV_InvoiceDetail1.Rows[i].FindControl("ddlFishSize") as DropDownList;
            if (ddlFish.SelectedValue != "0")
            {
                if (ddlFishGrade.SelectedValue == "0" || ddlFishSize.SelectedValue == "0")
                {
                    SCGL_Common.Error_Message(this);
                    return false;
                }
            }
        }
        DropDownList ddlItem = (DropDownList)GV_InvoiceDetail.Rows[0].FindControl("ddlFish");
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
            txtInvoiceNumber.Text = Convert.ToInt32(totalDraftID).ToString();
        }
        txtInvoiceNumber.ReadOnly = true;
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
        txtEmail.Text = dt.Rows[0]["Email"].ToString();
        txtBuyer.Text = dt.Rows[0]["Buyer"].ToString();
        txtConsignee.Text = dt.Rows[0]["Consignee"].ToString();
        txtDestCountry.Text = dt.Rows[0]["DestCountry"].ToString();
        txtTo.Text = dt.Rows[0]["PortOfDischarge"].ToString();
    }    

    protected void btnaddlines2_Click(object sender, EventArgs e)
    {
        DropDownList ddlFishRow2 = (DropDownList)GV_InvoiceDetail1.Rows[0].Cells[1].FindControl("ddlFish");
        DropDownList ddlFishGradeRow2 = (DropDownList)GV_InvoiceDetail1.Rows[0].Cells[2].FindControl("ddlFishGrade");
        if (int.Parse(ddlFishRow2.SelectedValue) > 0 && int.Parse(ddlFishGradeRow2.SelectedValue) > 0)
        {
            AddNewRowToGrid2();
            for (int i = 1; i < GV_InvoiceDetail1.Rows.Count; i++)
            {
                DropDownList ddlFishRow1 = (DropDownList)GV_InvoiceDetail1.Rows[0].Cells[1].FindControl("ddlFish");
                DropDownList ddlFishGradeRow1 = (DropDownList)GV_InvoiceDetail1.Rows[0].Cells[2].FindControl("ddlFishGrade");
                DropDownList ddlFish = (DropDownList)GV_InvoiceDetail1.Rows[i].Cells[1].FindControl("ddlFish");
                DropDownList ddlFishGrade = (DropDownList)GV_InvoiceDetail1.Rows[i].Cells[2].FindControl("ddlFishGrade");
                ddlFish.SelectedValue = ddlFishRow1.SelectedValue;
                ddlFish.Enabled = false;
                ddlFishGrade.SelectedValue = ddlFishGradeRow1.SelectedValue;
                ddlFishGrade.Enabled = false;
            }
        }
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
                    Label InventoryId = (Label)GV_InvoiceDetail1.Rows[i].Cells[9].FindControl("lblInventoryId");
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
                    if (int.Parse(ddlFish.SelectedValue) > 0 && int.Parse(ddlFishGrade.SelectedValue) > 0 && int.Parse(ddlFishSize.SelectedValue) > 0)
                    {
                        DataTable dtRetInventory = BALInvoice.getInvntoryByID(int.Parse(ddlFish.SelectedValue), int.Parse(ddlFishGrade.SelectedValue), int.Parse(ddlFishSize.SelectedValue));
                        drCurrentRow2["lblInventoryId"] = dtRetInventory.Rows[0]["Inventory_Id"].ToString();
                    }                    
                    Product_Table2.Rows.Add(drCurrentRow2);
                }
            }
            DataRow dr = Product_Table2.NewRow();
            dr[0] = "--Select Item--";
            Product_Table2.Rows.Add(dr);
            GV_InvoiceDetail1.DataSource = Product_Table2;
            GV_InvoiceDetail1.DataBind();
            Bind_FishName_Dropdown1();
            Bind_FishGrade_Dropdown1();
            Bind_FishSize_Dropdown1();
            for (int i = 0; i < GV_InvoiceDetail1.Rows.Count; i++)
            {
                DropDownList cbox = GV_InvoiceDetail1.Rows[i].FindControl("ddlFish") as DropDownList;
                if (i > 0)
                {
                    cbox.Visible = false;
                }
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
                dt2.Columns.Add(new DataColumn("lblInventoryId", typeof(string)));
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
        DataTable dtProduct = Product_Table2;
        dtProduct.Rows.Clear();
        for (int i = 0; i < GV_InvoiceDetail1.Rows.Count; i++)
        {
            if (GV_InvoiceDetail1.Rows[i].Visible)
            {
                DropDownList ddlFish = (DropDownList)GV_InvoiceDetail1.Rows[i].Cells[1].FindControl("ddlFish");
                DropDownList ddlFishGrade = (DropDownList)GV_InvoiceDetail1.Rows[i].Cells[2].FindControl("ddlFishGrade");
                DropDownList ddlFishSize = (DropDownList)GV_InvoiceDetail1.Rows[i].Cells[3].FindControl("ddlFishSize");
                TextBox txtDescription = (TextBox)GV_InvoiceDetail1.Rows[i].Cells[4].FindControl("txtDescription");
                TextBox txtQuantity = (TextBox)GV_InvoiceDetail1.Rows[i].Cells[5].FindControl("txtQuantity");
                TextBox txtRate = (TextBox)GV_InvoiceDetail1.Rows[i].Cells[6].FindControl("txtRate");
                HtmlInputText txtTotalPrice = (HtmlInputText)GV_InvoiceDetail1.Rows[i].Cells[7].FindControl("txtAmount");
                Label txtGrid = (Label)GV_InvoiceDetail1.Rows[i].Cells[8].FindControl("txtGridName");
                Label lblInventory = (Label)GV_InvoiceDetail1.Rows[i].Cells[9].FindControl("lblInventoryId");
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
                    if (SCGL_Common.Convert_ToInt(ddlFish.SelectedValue) > 0 && SCGL_Common.Convert_ToInt(ddlFishGrade.SelectedValue) > 0 && SCGL_Common.Convert_ToInt(ddlFishSize.SelectedValue) > 0)
                    {
                        DataTable dtRetInventory = BALInvoice.getInvntoryByID(int.Parse(ddlFish.SelectedValue), int.Parse(ddlFishGrade.SelectedValue), int.Parse(ddlFishSize.SelectedValue));
                        dtProduct.Rows[i]["lblInventoryId"] = dtRetInventory.Rows[0]["Inventory_Id"].ToString();
                    }
                }
            }
            else
            {
                dtProduct.Rows.Add(dtProduct.NewRow());
            }
        }
        if (dtProduct.Rows.Count > 0)
        {
            for (int pr = 0; pr < Product_Table2.Rows.Count; pr++)
            {
                if (Product_Table2.Rows[pr]["Fish_Value"].ToString() == "")
                {
                    Product_Table2.Rows.RemoveAt(pr);
                    //pr++;
                }
            }
            for (int pr = 0; pr < Product_Table2.Rows.Count; pr++)
            {
                if (Product_Table2.Rows[pr]["Fish_GradeValue"].ToString() == "")
                {
                    Product_Table2.Rows.RemoveAt(pr);
                    //pr++;
                }
            }
            for (int pr = 0; pr < Product_Table2.Rows.Count; pr++)
            {
                if (Product_Table2.Rows[pr]["Fish_SizeValue"].ToString() == "")
                {
                    Product_Table2.Rows.RemoveAt(pr);
                    //pr--;
                }
            }
            for (int dd = 0; dd < Product_Table2.Rows.Count; dd++)
            {
                if (Product_Table2.Rows[dd]["Fish_Item"].ToString() == "")
                {
                    Product_Table2.Rows.RemoveAt(dd);
                    //dd--;
                }
            }

        }
        return dtProduct;

    }

    protected void btnClear2_Click(object sender, EventArgs e)
    {
        SetInitialRow();
    }

    protected void ddlFish_SelectedIndexChanged(object sender, EventArgs e)
    {
        int ddlFishId = int.Parse(((System.Web.UI.WebControls.DropDownList)(sender)).SelectedValue);
        DropDownList ddlFish = (((sender as DropDownList).NamingContainer) as GridViewRow).FindControl("ddlFishGrade") as DropDownList;
        SCGL_Common.Bind_DropDown(ddlFish, "vt_SCGL_Sp_GetFishGrade", "FishGrade", "FishGradeID", "@FishID", ddlFishId);
        FishGradeId = int.Parse(ddlFish.SelectedValue);
        hf1g1.Value = SCGL_Common.Convert_ToString(ddlFishId);
        for (int i = 0; i < GV_InvoiceDetail1.Rows.Count; i++)
        {
            DropDownList ddlFish1 = (DropDownList)GV_InvoiceDetail1.Rows[i].Cells[1].FindControl("ddlFish");
            DropDownList ddlFishGrade = (DropDownList)GV_InvoiceDetail1.Rows[i].Cells[1].FindControl("ddlFishGrade");
            ddlFish1.SelectedValue = hf1g1.Value;
            SCGL_Common.Bind_DropDown(ddlFishGrade, "vt_SCGL_Sp_GetFishGrade", "FishGrade", "FishGradeID", "@FishID", ddlFish1.SelectedValue);
            DropDownList ddlFishSize = (DropDownList)GV_InvoiceDetail1.Rows[i].Cells[1].FindControl("ddlFishSize");
            SCGL_Common.Bind_DropDown(ddlFishSize, "vt_SCGL_Sp_GetFishSize", "FishSize", "FishSizeID", "@FishID", 0, "@GradId", 0);
        }
    }

    protected void ddlFishGrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        int ddlGradeId = int.Parse(((System.Web.UI.WebControls.DropDownList)(sender)).SelectedValue);
        int fieldFishId = SCGL_Common.Convert_ToInt(hf1g1.Value);
        DropDownList ddlFish = (((sender as DropDownList).NamingContainer) as GridViewRow).FindControl("ddlFishSize") as DropDownList;
        SCGL_Common.Bind_DropDown(ddlFish, "vt_SCGL_Sp_GetFishSize", "FishSize", "FishSizeID", "@FishID", fieldFishId, "@GradId", ddlGradeId);
        hf2g1.Value = SCGL_Common.Convert_ToString(ddlGradeId);        
            for (int i = 0; i < GV_InvoiceDetail1.Rows.Count; i++)
            {
                DropDownList ddlFish1 = (DropDownList)GV_InvoiceDetail1.Rows[i].Cells[1].FindControl("ddlFishGrade");
                ddlFish1.SelectedValue = hf2g1.Value;
                SCGL_Common.Bind_DropDown(ddlFish, "vt_SCGL_Sp_GetFishSize", "FishSize", "FishSizeID", "@FishID", fieldFishId, "@GradId", ddlGradeId);
                DropDownList ddlFishSize = (DropDownList)GV_InvoiceDetail1.Rows[i].Cells[1].FindControl("ddlFishSize");
                SCGL_Common.Bind_DropDown(ddlFishSize, "vt_SCGL_Sp_GetFishSize", "FishSize", "FishSizeID", "@FishID", fieldFishId, "@GradId", ddlGradeId);
           
            }
    }

    protected void ddlFish_SelectedIndexChanged1(object sender, EventArgs e)
    {
        int ddlFishId = int.Parse(((System.Web.UI.WebControls.DropDownList)(sender)).SelectedValue);
        DropDownList ddlFish = (((sender as DropDownList).NamingContainer) as GridViewRow).FindControl("ddlFishGrade") as DropDownList;
        SCGL_Common.Bind_DropDown(ddlFish, "vt_SCGL_Sp_GetFishGrade", "FishGrade", "FishGradeID", "@FishID", ddlFishId);
        hf1g2.Value = SCGL_Common.Convert_ToString(ddlFishId);
        for (int i = 0; i < GV_InvoiceDetail.Rows.Count; i++)
        {
            DropDownList ddlFish1 = (DropDownList)GV_InvoiceDetail.Rows[i].Cells[1].FindControl("ddlFish");
            DropDownList ddlFishGrade = (DropDownList)GV_InvoiceDetail.Rows[i].Cells[1].FindControl("ddlFishGrade");
            ddlFish1.SelectedValue = hf1g2.Value;
            SCGL_Common.Bind_DropDown(ddlFishGrade, "vt_SCGL_Sp_GetFishGrade", "FishGrade", "FishGradeID", "@FishID", ddlFish1.SelectedValue);
            DropDownList ddlFishSize = (DropDownList)GV_InvoiceDetail.Rows[i].Cells[1].FindControl("ddlFishSize");
            SCGL_Common.Bind_DropDown(ddlFishSize, "vt_SCGL_Sp_GetFishSize", "FishSize", "FishSizeID", "@FishID", 0, "@GradId", 0);
        }

    }

    protected void ddlFishGrade_SelectedIndexChanged1(object sender, EventArgs e)
    {
        int ddlGradeId = int.Parse(((System.Web.UI.WebControls.DropDownList)(sender)).SelectedValue);
        int fieldFishId = SCGL_Common.Convert_ToInt(hf1g2.Value);
        DropDownList ddlFish = (((sender as DropDownList).NamingContainer) as GridViewRow).FindControl("ddlFishSize") as DropDownList;
        SCGL_Common.Bind_DropDown(ddlFish, "vt_SCGL_Sp_GetFishSize", "FishSize", "FishSizeID", "@FishID", fieldFishId, "@GradId", ddlGradeId);
        hf2g2.Value = SCGL_Common.Convert_ToString(ddlGradeId);
        for (int i = 0; i < GV_InvoiceDetail.Rows.Count; i++)
        {
            DropDownList ddlFish1 = (DropDownList)GV_InvoiceDetail.Rows[i].Cells[1].FindControl("ddlFishGrade");
            ddlFish1.SelectedValue = hf2g2.Value;
            DropDownList ddlFishSize = (DropDownList)GV_InvoiceDetail.Rows[i].Cells[1].FindControl("ddlFishSize");
            SCGL_Common.Bind_DropDown(ddlFishSize, "vt_SCGL_Sp_GetFishSize", "FishSize", "FishSizeID", "@FishID", fieldFishId, "@GradId", ddlGradeId);
        }
    }

    public void Gv_GetRows1()
    {
        SqlDataReader dr = BALInvoice.Get_Rows_InvoiceDetail_byID(SCGL_Common.Convert_ToInt(Request.QueryString["ID"]));
        while (dr.Read())
        {
            Session["ItemRows"] = dr["RowNumber"].ToString();
        }
    }

    public void Gv_GetRows2()
    {
        SqlDataReader dr = BALInvoice.Get_Rows_InvoiceDetail2_byID(SCGL_Common.Convert_ToInt(Request.QueryString["ID"]));
        while (dr.Read())
        {
            Session["DeductionRows"] = dr["RowNumber"].ToString();
        }
    }



    private void SetInitialRow_For_Edit()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("txtDescription", typeof(string)));
        dt.Columns.Add(new DataColumn("txtQuantity", typeof(string)));
        dt.Columns.Add(new DataColumn("txtRate", typeof(string)));
        dt.Columns.Add(new DataColumn("txtAmount", typeof(string)));
        dt.Columns.Add(new DataColumn("lblInventoryId", typeof(string)));
        dt.Columns.Add(new DataColumn("Fish_Item", typeof(string)));
        dt.Columns.Add(new DataColumn("Fish_Value", typeof(string)));
        dt.Columns.Add(new DataColumn("Fish_Grade", typeof(string)));
        dt.Columns.Add(new DataColumn("Fish_GradeValue", typeof(string)));
        dt.Columns.Add(new DataColumn("Fish_Size", typeof(string)));
        dt.Columns.Add(new DataColumn("Fish_SizeValue", typeof(string)));
        dt.Columns.Add(new DataColumn("txtGridName", typeof(string)));
        if (Convert.ToInt32(Session["ItemRows"]) < 1)
        {
            for (int i = 0; i < 1; i++)
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
            }
        }
        else
        {
            for (int i = 0; i < Convert.ToInt32(Session["ItemRows"]); i++)
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
            }
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
        dt2.Columns.Add(new DataColumn("lblInventoryId", typeof(string)));
        dt2.Columns.Add(new DataColumn("txtGridName", typeof(string)));
        if (Convert.ToInt32(Session["DeductionRows"]) < 1)
        {
            for (int i = 0; i < 1; i++)
            {
                dr2 = dt2.NewRow();
                dt2.Rows.Add(dr2);
            }
        }
        else
        {
            for (int i = 0; i < Convert.ToInt32(Session["DeductionRows"]); i++)
            {
                dr2 = dt2.NewRow();
                dt2.Rows.Add(dr2);
            }
        }
        ViewState["CurrentTable"] = dt;
        ViewState["CurrentTable2"] = dt2;
        GV_InvoiceDetail.DataSource = dt;
        GV_InvoiceDetail.DataBind();
        GV_InvoiceDetail1.DataSource = dt2;
        GV_InvoiceDetail1.DataBind();
        Bind_Product_Dropdown();
        Bind_FishName_Dropdown();
        Bind_FishGrade_Dropdown();
        Bind_FishSize_Dropdown();
        Bind_FishName_Dropdown1();
        Bind_FishGrade_Dropdown1();
        Bind_FishSize_Dropdown1();
        for (int i = 0; i < GV_InvoiceDetail.Rows.Count; i++)
        {
            DropDownList cbox = GV_InvoiceDetail.Rows[i].FindControl("ddlFish") as DropDownList;
            if (i > 0)
            {
                cbox.Visible = false;
            }
            cbox.SelectedIndex = cbox.Items.IndexOf(cbox.Items.FindByText(Product_Table.Rows[i]["Fish_Item"].ToString()));
        }
        for (int i = 0; i < GV_InvoiceDetail.Rows.Count; i++)
        {
            DropDownList cbox = GV_InvoiceDetail.Rows[i].FindControl("ddlFishGrade") as DropDownList;
            if (i > 0) { cbox.Visible = false; }
            cbox.SelectedIndex = cbox.Items.IndexOf(cbox.Items.FindByText(Product_Table.Rows[i]["Fish_Grade"].ToString()));
        }

        for (int i = 0; i < GV_InvoiceDetail1.Rows.Count; i++)
        {
            DropDownList cbox = GV_InvoiceDetail1.Rows[i].FindControl("ddlFish") as DropDownList;
            if (i > 0)
            {
                cbox.Visible = false;
            }
            cbox.SelectedIndex = cbox.Items.IndexOf(cbox.Items.FindByText(Product_Table2.Rows[i]["Fish_Item"].ToString()));
        }
        for (int i = 0; i < GV_InvoiceDetail1.Rows.Count; i++)
        {
            DropDownList cbox = GV_InvoiceDetail1.Rows[i].FindControl("ddlFishGrade") as DropDownList;
            if (i > 0) { cbox.Visible = false; }
            cbox.SelectedIndex = cbox.Items.IndexOf(cbox.Items.FindByText(Product_Table2.Rows[i]["Fish_Grade"].ToString()));
        }
    }

    public void FillGridInvoiceDetail()
    {
        try
        {
            InvoiceDetail_BAL inv_dtl = new InvoiceDetail_BAL();
            DataTable dtInvoiceDetail = inv_dtl.getInvoiceDetailByInvoiceID(SCGL_Common.Convert_ToInt(Request.QueryString["ID"]));
            for (int i = 0; i < dtInvoiceDetail.Rows.Count; i++)
            {
                DropDownList ddlFish = GV_InvoiceDetail.Rows[i].FindControl("ddlFish") as DropDownList;

                DropDownList ddlFishGrade = GV_InvoiceDetail.Rows[i].FindControl("ddlFishGrade") as DropDownList;
                DropDownList ddlFishSize = GV_InvoiceDetail.Rows[i].FindControl("ddlFishSize") as DropDownList;
                TextBox txtDescription = GV_InvoiceDetail.Rows[i].FindControl("txtDescription") as TextBox;
                TextBox txtQuantity = GV_InvoiceDetail.Rows[i].FindControl("txtQuantity") as TextBox;
                TextBox txtRate = GV_InvoiceDetail.Rows[i].FindControl("txtRate") as TextBox;
                HtmlInputText txtTotalPrice = GV_InvoiceDetail.Rows[i].FindControl("txtAmount") as HtmlInputText;
                Label lblInventoryId = GV_InvoiceDetail.Rows[i].FindControl("lblInventoryId") as Label;
                if (dtInvoiceDetail.Rows[i]["FishSizeID"] != null && dtInvoiceDetail.Rows[i]["FishSizeID"].ToString() != "")
                {
                    ddlFish.SelectedValue = dtInvoiceDetail.Rows[i]["FishID"].ToString();
                    ddlFishGrade.SelectedValue = dtInvoiceDetail.Rows[i]["FishGradeID"].ToString();
                    ddlFishSize.SelectedValue = dtInvoiceDetail.Rows[i]["FishSizeID"].ToString();
                    txtDescription.Text = dtInvoiceDetail.Rows[i]["Description"].ToString();
                    txtQuantity.Text = dtInvoiceDetail.Rows[i]["Quantity"].ToString();
                    txtRate.Text = dtInvoiceDetail.Rows[i]["Rate"].ToString();
                    txtTotalPrice.Value = dtInvoiceDetail.Rows[i]["amount"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void FillGridInvoiceDetail1()
    {
        try
        {
            InvoiceDetail_BAL inv_dtl = new InvoiceDetail_BAL();
            DataTable dtInvoiceDetail = inv_dtl.getInvoiceDetailByInvoiceID1(SCGL_Common.Convert_ToInt(Request.QueryString["ID"]));
            for (int i = 0; i < dtInvoiceDetail.Rows.Count; i++)
            {
                DropDownList ddlFish = GV_InvoiceDetail1.Rows[i].FindControl("ddlFish") as DropDownList;
                DropDownList ddlFishGrade = GV_InvoiceDetail1.Rows[i].FindControl("ddlFishGrade") as DropDownList;
                DropDownList ddlFishSize = GV_InvoiceDetail1.Rows[i].FindControl("ddlFishSize") as DropDownList;
                TextBox txtDescription = GV_InvoiceDetail1.Rows[i].FindControl("txtDescription") as TextBox;
                TextBox txtQuantity = GV_InvoiceDetail1.Rows[i].FindControl("txtQuantity") as TextBox;
                TextBox txtRate = GV_InvoiceDetail1.Rows[i].FindControl("txtRate") as TextBox;
                HtmlInputText txtTotalPrice = GV_InvoiceDetail1.Rows[i].FindControl("txtAmount") as HtmlInputText;
                Label lblInventoryId = GV_InvoiceDetail1.Rows[i].FindControl("lblInventoryId") as Label;
                if (dtInvoiceDetail.Rows[i]["FishSizeID"] != null && dtInvoiceDetail.Rows[i]["FishSizeID"].ToString() != "")
                {
                    ddlFish.SelectedValue = dtInvoiceDetail.Rows[i]["FishID"].ToString();
                    ddlFishGrade.SelectedValue = dtInvoiceDetail.Rows[i]["FishGradeID"].ToString();
                    ddlFishSize.SelectedValue = dtInvoiceDetail.Rows[i]["FishSizeID"].ToString();
                    txtDescription.Text = dtInvoiceDetail.Rows[i]["Description"].ToString();
                    txtQuantity.Text = dtInvoiceDetail.Rows[i]["Quantity"].ToString();
                    txtRate.Text = dtInvoiceDetail.Rows[i]["Rate"].ToString();
                    txtTotalPrice.Value = dtInvoiceDetail.Rows[i]["amount"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    
}
