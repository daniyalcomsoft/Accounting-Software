﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SW.SW_Common;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

public partial class ProformaInvoice_NEW : System.Web.UI.Page
{
    Invoice_BAL_Temp BALInvoice = new Invoice_BAL_Temp();
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "ProformaInvoice_NEW.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "ProformaInvoice_NEW.aspx" && view == true)
                {
                    
                    Bind_Customer();
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
        hdnMinDate.Value = dt.Rows[0]["yearFrom"].ToString();
        hdnMaxDate.Value = dt.Rows[0]["YearTo"].ToString();

    }

    public void Bind_Product_Dropdown()
    {
        for (int i = 0; i < GV_ProformaInvoiceDetail.Rows.Count; i++)
        {
            DropDownList ddlProduct = GV_ProformaInvoiceDetail.Rows[i].FindControl("ddlInventory") as DropDownList;
            if (Request.QueryString["ID"] != null)
            {
                //InvoiceDetail_BAL inv_dtl = new InvoiceDetail_BAL();
                DataTable dtInvoiceDetail = BALInvoice.getProformaInvoiceDetail(SCGL_Common.Convert_ToInt(Request.QueryString["ID"]));
                //DataTable dtInvoiceDetail = BALInvoice.getInvoiceDetail(Id);

                InventoryID = SCGL_Common.Convert_ToInt(dtInvoiceDetail.Rows[0]["InventoryID"].ToString());
            }
            SCGL_Common.Bind_DropDown(ddlProduct, "vt_SCGL_Sp_GetInventory", "InventoryName", "InventoryID");
        }

    }

    public void Bind_CostCenter_Dropdown()
    {
        for (int i = 0; i < GV_ProformaInvoiceDetail.Rows.Count; i++)
        {
            DropDownList ddlCostCenter = GV_ProformaInvoiceDetail.Rows[i].FindControl("ddlCostCenter") as DropDownList;
            if (Request.QueryString["ID"] != null)
            {
                //InvoiceDetail_BAL inv_dtl = new InvoiceDetail_BAL();
                DataTable dtInvoiceDetail = BALInvoice.getProformaInvoiceDetail(SCGL_Common.Convert_ToInt(Request.QueryString["ID"]));
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
            btnAddLines.Visible = btnClearAllLines.Visible = false;
           
        }
        Invoice_BAL_Temp invoiceBal = new Invoice_BAL_Temp();
        DataTable dt = invoiceBal.getProformaInvoiceByID(Id);
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
            //txtDueDate.Text = dt.Rows[0]["DueDate"].ToString();
            txtTot.Value = dt.Rows[0]["Total"].ToString();
            ddlCurrency.SelectedValue = dt.Rows[0]["Currency"].ToString();
            txtConversionRate.Text = dt.Rows[0]["ConversionRate"].ToString();
            txtPKRTotal.Value = dt.Rows[0]["PKRTotal"].ToString();
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
            txtBankDetails.Text = dt.Rows[0]["BankDetails"].ToString();
            txtQuantities.Text = dt.Rows[0]["Quantities"].ToString();
            txtDateOfShipment.Text = dt.Rows[0]["ShipmentDate"].ToString();
            txtNote.Text = dt.Rows[0]["Note"].ToString();
            txtOldProformaNo.Text = dt.Rows[0]["OldProformaNo"].ToString();
            btnSave.Text = "Update";
        }

        DataTable dtInvoiceDetail = BALInvoice.getProformaInvoiceDetail(Id);
       
        for (int i = 0; i < dtInvoiceDetail.Rows.Count; i++)
        {

            DropDownList ddlInventory = (DropDownList)GV_ProformaInvoiceDetail.Rows[i].Cells[1].FindControl("ddlInventory");
            TextBox txtDescription = (TextBox)GV_ProformaInvoiceDetail.Rows[i].Cells[2].FindControl("txtDescription");
            TextBox txtQuantity = (TextBox)GV_ProformaInvoiceDetail.Rows[i].Cells[3].FindControl("txtQuantity");
            TextBox txtRate = (TextBox)GV_ProformaInvoiceDetail.Rows[i].Cells[4].FindControl("txtRate");
            HtmlInputText txtAmount = GV_ProformaInvoiceDetail.Rows[i].Cells[5].FindControl("txtAmount") as HtmlInputText;
            DropDownList ddlCostCenter = (DropDownList)GV_ProformaInvoiceDetail.Rows[i].Cells[6].FindControl("ddlCostCenter");

            ddlInventory.SelectedValue = dtInvoiceDetail.Rows[i]["InventoryID"].ToString();
            txtDescription.Text = dtInvoiceDetail.Rows[i]["Description"].ToString();
            txtQuantity.Text = dtInvoiceDetail.Rows[i]["Quantity"].ToString();
            txtRate.Text = dtInvoiceDetail.Rows[i]["Rate"].ToString();
            txtAmount.Value = dtInvoiceDetail.Rows[i]["Amount"].ToString();
            ddlCostCenter.SelectedValue = dtInvoiceDetail.Rows[i]["CostCenterID"].ToString();
            btnSave.Text = "Update";
        }
    }

    public void Bind_Customer()
    {
        SCGL_Common.Bind_DropDown(ddlCustomer, "vt_SCGL_BindCustomer", "CustomerName", "ID");
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
        SqlDataReader dr = BALInvoice.Get_Rows_ProformaInvoiceDetail_byID(SCGL_Common.Convert_ToInt(Request.QueryString["ID"]));
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
        GV_ProformaInvoiceDetail.DataSource = dt;
        GV_ProformaInvoiceDetail.DataBind();
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



        GV_ProformaInvoiceDetail.DataSource = dt;
        GV_ProformaInvoiceDetail.DataBind();
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
            for (int i = 0; i < GV_ProformaInvoiceDetail.Rows.Count; i++)
            {
                if (GV_ProformaInvoiceDetail.Rows[i].Visible)
                {
                    DataRow drCurrentRow = Product_Table.NewRow();
                    DropDownList ddlInventory = (DropDownList)GV_ProformaInvoiceDetail.Rows[i].Cells[1].FindControl("ddlInventory");
                    TextBox txtDescription = (TextBox)GV_ProformaInvoiceDetail.Rows[i].Cells[2].FindControl("txtDescription");
                    TextBox txtQuantity = (TextBox)GV_ProformaInvoiceDetail.Rows[i].Cells[3].FindControl("txtQuantity");
                    TextBox txtRate = (TextBox)GV_ProformaInvoiceDetail.Rows[i].Cells[4].FindControl("txtRate");
                    HtmlInputText txtTotalPrice = (HtmlInputText)GV_ProformaInvoiceDetail.Rows[i].Cells[5].FindControl("txtAmount");
                    DropDownList ddlCostCenter = (DropDownList)GV_ProformaInvoiceDetail.Rows[i].Cells[6].FindControl("ddlCostCenter");

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
            GV_ProformaInvoiceDetail.DataSource = Product_Table;
            GV_ProformaInvoiceDetail.DataBind();
            Bind_Product_Dropdown();
            Bind_CostCenter_Dropdown();
            for (int i = 0; i < GV_ProformaInvoiceDetail.Rows.Count; i++)
            {
                DropDownList cbox = GV_ProformaInvoiceDetail.Rows[i].FindControl("ddlInventory") as DropDownList;
                cbox.SelectedIndex = cbox.Items.IndexOf(cbox.Items.FindByText(Product_Table.Rows[i]["ddlInventory"].ToString()));
            }
            for (int i = 0; i < GV_ProformaInvoiceDetail.Rows.Count; i++)
            {
                DropDownList cbox = GV_ProformaInvoiceDetail.Rows[i].FindControl("ddlCostCenter") as DropDownList;
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
        for (int i = 0; i < GV_ProformaInvoiceDetail.Rows.Count; i++)
        {
            if (GV_ProformaInvoiceDetail.Rows[i].Visible)
            {
                DropDownList ddlInventory = (DropDownList)GV_ProformaInvoiceDetail.Rows[i].Cells[1].FindControl("ddlInventory");
                TextBox txtDescription = (TextBox)GV_ProformaInvoiceDetail.Rows[i].Cells[2].FindControl("txtDescription");
                TextBox txtQuantity = (TextBox)GV_ProformaInvoiceDetail.Rows[i].Cells[3].FindControl("txtQuantity");
                TextBox txtRate = (TextBox)GV_ProformaInvoiceDetail.Rows[i].Cells[4].FindControl("txtRate");
                HtmlInputText txtTotalPrice = (HtmlInputText)GV_ProformaInvoiceDetail.Rows[i].Cells[5].FindControl("txtAmount");
                DropDownList ddlCostCenter = (DropDownList)GV_ProformaInvoiceDetail.Rows[i].Cells[6].FindControl("ddlCostCenter");


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

    protected void GV_ProformaInvoiceDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = e.RowIndex;
        if (index != 0)
        {
            GV_ProformaInvoiceDetail.Rows[e.RowIndex].Visible = false;

            DropDownList ddlInventory = (DropDownList)GV_ProformaInvoiceDetail.Rows[e.RowIndex].Cells[1].FindControl("ddlInventory");
            //TextBox txtDescription = (TextBox)GV_ProformaInvoiceDetail.Rows[e.RowIndex].Cells[2].FindControl("txtDescription");
            TextBox txtDescription = (TextBox)GV_ProformaInvoiceDetail.Rows[e.RowIndex].Cells[2].FindControl("txtDescription");
            TextBox txtQuantity = (TextBox)GV_ProformaInvoiceDetail.Rows[e.RowIndex].Cells[3].FindControl("txtQuantity");
            TextBox txtRate = (TextBox)GV_ProformaInvoiceDetail.Rows[e.RowIndex].Cells[4].FindControl("txtRate");
            HtmlInputText txtAmount = GV_ProformaInvoiceDetail.Rows[e.RowIndex].Cells[5].FindControl("txtAmount") as HtmlInputText;
            DropDownList ddlCostCenter = (DropDownList)GV_ProformaInvoiceDetail.Rows[e.RowIndex].Cells[6].FindControl("ddlCostCenter");

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
                            lblSuccessMsg.InnerHtml = "Proforma Invoice Created Successfully";
                        }
                        else
                        {
                            lblSuccessMsg.InnerHtml = "Proforma Invoice Updated Successfully";
                        }
                        SCGL_Common.Success_Message(this.Page, "ProformaInvoice_Views.aspx");
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
            BALInvoice.TermID =txtTerm.Text;
            BALInvoice.InvoiceDate = SCGL_Common.CheckDateTime(txtInvoiceDate.Text);
            BALInvoice.DueDate = SCGL_Common.CheckDateTime(txtDueDate.Text);
            //BALInvoice.Discount = txtDiscount.Text == "" ? SCGL_Common.Convert_ToDecimal("0")
            //   : SCGL_Common.Convert_ToDecimal(txtDiscount.Text);
            BALInvoice.Total = SCGL_Common.Convert_ToDecimal(txtTot.Value);
            BALInvoice.Currency = SCGL_Common.Convert_ToInt(ddlCurrency.SelectedValue);
            BALInvoice.ConversionRate = SCGL_Common.Convert_ToDecimal(txtConversionRate.Text);
            BALInvoice.PKRTotal = BALInvoice.Total * BALInvoice.ConversionRate;
            BALInvoice.LoginID = SBO.UserID;
            BALInvoice.FinYearID = SBO.FinYearID;
            //BALInvoice.From = txtFrom.Text;
            //BALInvoice.To = txtTo.Text;
            //BALInvoice.GrossWeight = SCGL_Common.Convert_ToDecimal(txtGrossWeight.Text);
            //BALInvoice.Origin_Country = txtOrCountry.Text;
            //BALInvoice.Destination_Country = txtDestCountry.Text;
            //BALInvoice.Vessel = txtVessel.Text;
            //BALInvoice.FormENo = txtFormENo.Text;
            //BALInvoice.Freight = txtFreight.Text;
            //BALInvoice.NetWeight = SCGL_Common.Convert_ToDecimal(txtNetWeight.Text);
            //BALInvoice.ContainerNo = txtContainerNo.Text;
            //BALInvoice.ProformaNo = txtproformaNo.Text;
            //BALInvoice.Insurance = txtInsurance.Text;
            //BALInvoice.Exporter = txtExporter.Text;
            //BALInvoice.Consignee = txtConsignee.Text;
            //BALInvoice.Buyer = txtBuyer.Text;
            //BALInvoice.ExportersRef = txtExportersRef.Text;
            //BALInvoice.BankDetails = txtBankDetails.Text;
            //BALInvoice.Quantities = txtQuantities.Text;
            //BALInvoice.ShipmentDate = txtDateOfShipment.Text;
            //BALInvoice.Note = txtNote.Text;
            BALInvoice.OldProformaNo = txtOldProformaNo.Text;
            
           
            int InvoiceID = BALInvoice.CreateModifyProformaInvoice(BALInvoice, trans);
            if (InvoiceID > 0)
            {
                txtInvoiceID.Text = InvoiceID.ToString();
                BALInvoice.InvoiceNo = SCGL_Common.Convert_ToInt(txtInvoiceID.Text);
                if (btnSave.Text == "Update")
                {
                    BALInvoice.Delete_ProformaInvoiceDetail(BALInvoice.InvoiceNo, trans);
                 //   BALInvoice.Delete_Transaction(BALInvoice.InvoiceNo, trans);
                }
                int Counter = 0;
                DataTable dt = Record_for_Insert();
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    BALInvoice.InvoiceNo = int.Parse(txtInvoiceID.Text);
                    //BALInvoice.Description2 = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtDescription"].ToString());
                    //BALInvoice.Quantity = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtQuantity"].ToString());
                    //BALInvoice.Rate = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtRate"].ToString());
                    //BALInvoice.Amount = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtAmount"].ToString());
                    BALInvoice.Currency = SCGL_Common.Convert_ToInt(ddlCurrency.SelectedValue);
                    BALInvoice.ConversionRate = SCGL_Common.Convert_ToDecimal(txtConversionRate.Text);
                    //BALInvoice.PKRAmount = BALInvoice.Amount * BALInvoice.ConversionRate;
                    //BALInvoice.InventoryID = SCGL_Common.Convert_ToInt(dt.Rows[i]["ddl_Inventoryvalue"].ToString());
                    //BALInvoice.CostCenterID = SCGL_Common.Convert_ToInt(dt.Rows[i]["ddl_CostCentervalue"].ToString());
                    BALInvoice.InvoiceDate = SCGL_Common.CheckDateTime(txtInvoiceDate.Text);

                    if (BALInvoice.CreateModifyProformaInvoiceDetail(BALInvoice, trans))
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


        for (int i = 0; i < GV_ProformaInvoiceDetail.Rows.Count; i++)
        {
            DropDownList ddlProduct = GV_ProformaInvoiceDetail.Rows[i].FindControl("ddlInventory") as DropDownList;
            TextBox txtDescription = (TextBox)GV_ProformaInvoiceDetail.Rows[i].FindControl("txtDescription");
            TextBox txtQuantity = (TextBox)GV_ProformaInvoiceDetail.Rows[i].FindControl("txtQuantity");
            TextBox txtRate = (TextBox)GV_ProformaInvoiceDetail.Rows[i].FindControl("txtRate");
            DropDownList ddlCostCenter = GV_ProformaInvoiceDetail.Rows[i].FindControl("ddlCostCenter") as DropDownList;
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
            int DynamicID = Convert.ToInt32(row["InvoiceID"]);
            int totalInvoiceID = staticID + DynamicID;
            txtInvoiceNumber.Text = Convert.ToInt32(totalInvoiceID).ToString();
        }
        txtInvoiceNumber.ReadOnly = true;
    }


    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProformaInvoice_Views.aspx");
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

   

  
   
}
