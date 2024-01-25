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

public partial class Invoice_Temp : System.Web.UI.Page
{
    Invoice_BAL_Temp BALInvoice = new Invoice_BAL_Temp();
    //decimal CurrentInventory = 0;
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "Invoice_Temp.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "Invoice_Temp.aspx" && view == true)
                {
                    //Bind_Customer();
                    if (Request.QueryString["Id"] != null)
                    {
                        Gv_GetRows1();
                        SetInitialRow_For_Edit();
                        Gv_Duties_GetRows1();
                        SetInitialRow_For_Edit_duties();
                        BindControl(Convert.ToInt32(Request.QueryString["Id"]));
                    }
                    else
                    {
                        SetInitialRow();
                        SetInitialRow_duties();
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

    public void Bind_Product_Dropdown()
    {
        for (int i = 0; i < GV_CommercialInvoiceDetail.Rows.Count; i++)
        {
            DropDownList ddlDescription = GV_CommercialInvoiceDetail.Rows[i].FindControl("ddlDescription") as DropDownList;
            string oldval = ddlDescription.SelectedValue;
            //if (Request.QueryString["ID"] != null)
            //{
            //    //InvoiceDetail_BAL inv_dtl = new InvoiceDetail_BAL();
            //    DataTable dtInvoiceDetail = BALInvoice.getInvoiceDetail(SCGL_Common.Convert_ToInt(Request.QueryString["ID"]));
            //    //DataTable dtInvoiceDetail = BALInvoice.getInvoiceDetail(Id);
            //    InventoryID = SCGL_Common.Convert_ToInt(dtInvoiceDetail.Rows[0]["InventoryID"].ToString());
            //}

            InvoiceDesc IDesc = new InvoiceDesc();
            ddlDescription.DataSource = IDesc.ReadDataTable();
            ddlDescription.DataTextField = "InvoiceDescription";
            ddlDescription.DataValueField = "InvoiceDescID";
            ddlDescription.DataBind();
            ddlDescription.Items.Insert(0, new ListItem("--Please Select--", "0"));
            ddlDescription.Items.Insert(1, new ListItem("+ Add New", "-1"));
            ddlDescription.SelectedValue = oldval;
            //SCGL_Common.Bind_DropDown(ddlDescription, "vt_SCGL_Sp_GetInventory", "InventoryName", "InventoryID");
            //ddlDescription.Items.Insert(1, new ListItem("+ Add New", "-1"));
        }
    }

    public void Bind_Product_Duties_Dropdown()
    {
        for (int i = 0; i < GV_CommercialInvoiceDutiesDetail.Rows.Count; i++)
        {
            DropDownList ddlDescription_duties = GV_CommercialInvoiceDutiesDetail.Rows[i].FindControl("ddlDescription_duties") as DropDownList;
            string oldval = ddlDescription_duties.SelectedValue;

            InvoiceDutiesDesc IDesc = new InvoiceDutiesDesc();
            ddlDescription_duties.DataSource = IDesc.ReadDataTable();
            ddlDescription_duties.DataTextField = "InvoiceDutiesDescription";
            ddlDescription_duties.DataValueField = "InvoiceDutiesDescID";
            ddlDescription_duties.DataBind();
            ddlDescription_duties.Items.Insert(0, new ListItem("--Please Select--", "0"));
            ddlDescription_duties.Items.Insert(1, new ListItem("+ Add New", "-1"));
            ddlDescription_duties.SelectedValue = oldval;
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
        DataTable dt = invoiceBal.getInvoiceByID(Id);
        if (dt.Rows.Count > 0)
        {
            //ddlCustomer.SelectedValue = dt.Rows[0]["CustomerID"].ToString();
            //txtEmail.Text = dt.Rows[0]["Email"].ToString();
            //txtBillingAddress.Text = dt.Rows[0]["BillingAddress"].ToString();
            txtInvoiceID.Text = dt.Rows[0]["InvoiceID"].ToString();
            //txtInvoiceNumber.Text = dt.Rows[0]["InvoiceNo"].ToString();
            txtTerm.Text = dt.Rows[0]["TermID"].ToString();
            txtInvoiceDate.Text = SCGL_Common.CheckDateTime(dt.Rows[0]["InvoiceDate"]).ToShortDateString();
            //txtInvoiceDate.Text = dt.Rows[0]["InvoiceDate"].ToString();
            txtReferenceNo.Text = dt.Rows[0]["ReferenceNo"].ToString();
            //txtDueDate.Text = dt.Rows[0]["DueDate"].ToString();
            //txtTot.Value = dt.Rows[0]["Total"].ToString();
            //ddlCurrency.SelectedValue = dt.Rows[0]["Currency"].ToString();
            //txtConversionRate.Text = dt.Rows[0]["ConversionRate"].ToString();
            //txtPKRTotal.Value = dt.Rows[0]["PKRTotal"].ToString();
            //txtDiscount.Text = dt.Rows[0]["Discount"].ToString();
            //txtFrom.Text = dt.Rows[0]["From"].ToString();
            //txtTo.Text = dt.Rows[0]["To"].ToString();
            //txtContainerNo.Text = dt.Rows[0]["ContainerNo"].ToString();
            //txtOrCountry.Text = dt.Rows[0]["Origin_Country"].ToString();
            //txtDestCountry.Text = dt.Rows[0]["Destination_Country"].ToString();
            //txtVessel.Text = dt.Rows[0]["Vessel"].ToString();
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
            txtBillNumber.Text = dt.Rows[0]["BillNumber"].ToString();
            txtCustInvoiceNo.Text = dt.Rows[0]["CustInvoiceNo"].ToString();
            chkAbbottInvoice.Checked = (dt.Rows[0]["IsAbbott"].ToString() != "False") ? true : false;
            chkNoAdvance.Checked = (dt.Rows[0]["NoAdvance"].ToString() != "False") ? true : false;
            //txtCustomDuty.Text = dt.Rows[0]["CustomDuty"].ToString();
            txtCustomPONo.Text = dt.Rows[0]["CustomPONo"].ToString();
            txtCustomPODate.Text = SCGL_Common.CheckDateTime(dt.Rows[0]["CustomPODate"]).ToShortDateString();
            txtCustomByParty.Text = dt.Rows[0]["CustomByParty"].ToString();
            txtCustomByUs.Text = dt.Rows[0]["CustomByUs"].ToString();
            txtSalesTaxPONo.Text = dt.Rows[0]["SalesTaxPONo"].ToString();
            txtSalesTaxFine.Text = dt.Rows[0]["SalesTaxFine"].ToString();
            txtSalesTaxByParty.Text = dt.Rows[0]["SalesTaxByParty"].ToString();
            txtSalesTaxByUs.Text = dt.Rows[0]["SalesTaxByUs"].ToString();
            txtIncomeTaxPONo.Text = dt.Rows[0]["IncomeTaxPONo"].ToString();
            txtIncomeTaxAddition.Text = dt.Rows[0]["IncomeTaxAddition"].ToString();
            txtIncomeTaxByParty.Text = dt.Rows[0]["IncomeTaxByParty"].ToString();
            txtIncomeTaxByUs.Text = dt.Rows[0]["IncomeTaxByUs"].ToString();

            txtCEDPercent.Text = dt.Rows[0]["CEDPercent"].ToString();
            txtCEDByParty.Text = dt.Rows[0]["CEDByParty"].ToString();
            txtCEDByUs.Text = dt.Rows[0]["CEDByUs"].ToString();

            txtEOCPercent.Text = dt.Rows[0]["EOCPercent"].ToString();
            txtEOCByParty.Text = dt.Rows[0]["EOCByParty"].ToString();
            txtEOCByUs.Text = dt.Rows[0]["EOCByUs"].ToString();

            txtFEDPercent.Text = dt.Rows[0]["FEDPercent"].ToString();
            txtFEDByParty.Text = dt.Rows[0]["FEDByParty"].ToString();
            txtFEDByUs.Text = dt.Rows[0]["FEDByUs"].ToString();

            txtOthersPercent.Text = dt.Rows[0]["OthersPercent"].ToString();
            txtOthersByParty.Text = dt.Rows[0]["OthersByParty"].ToString();
            txtOthersByUs.Text = dt.Rows[0]["OthersByUs"].ToString();

            txtExcessShortDutyPONo.Text = dt.Rows[0]["ExcessShortDutyPONo"].ToString();
            txtExcessShortDutyByParty.Text = dt.Rows[0]["ExcessShortDutyByParty"].ToString();
            txtExcessShortDutyByUs.Text = dt.Rows[0]["ExcessShortDutyByUs"].ToString();

            ddlStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
            txtChequeNo.Text = dt.Rows[0]["ChequeNo"].ToString();
            if (SCGL_Common.CheckDateTime(dt.Rows[0]["ReceivedDate"]).ToShortDateString() != "1/1/1900")
            {
                txtReceivedDate.Text = SCGL_Common.CheckDateTime(dt.Rows[0]["ReceivedDate"]).ToShortDateString();
            }
            else 
            {
                txtReceivedDate.Text = "";
            }
            chkDeliveryChallan.Checked = (dt.Rows[0]["DeliveryChallan"].ToString()!="False")?true:false ;
            chkLCContract.Checked = (dt.Rows[0]["LCContract"].ToString() != "False") ? true : false;
            chkCertificates.Checked = (dt.Rows[0]["Certificates"].ToString() != "False") ? true : false;
            chkPackingList.Checked = (dt.Rows[0]["PackingList"].ToString() != "False") ? true : false;
            chkInvoice.Checked = (dt.Rows[0]["Invoice"].ToString() != "False") ? true : false;
            chkWeboc.Checked = (dt.Rows[0]["WebocGD"].ToString() != "False") ? true : false;
            chkPaccsCoupon.Checked = (dt.Rows[0]["PaccsCoupon"].ToString() != "False") ? true : false;
            chkCashPayReceipt.Checked = (dt.Rows[0]["CashPaymentReceipt"].ToString() != "False") ? true : false;
            chkExciseDutyChallan.Checked = (dt.Rows[0]["ExciseDutyChallan"].ToString() != "False") ? true : false;
            chkExciseTaxChallan.Checked = (dt.Rows[0]["ExciseTaxChallan"].ToString() != "False") ? true : false;
            chkDOR.Checked = (dt.Rows[0]["DeliveryOrderReceipt"].ToString() != "False") ? true : false;
            chkPICTLInv.Checked = (dt.Rows[0]["PICTLInvoice"].ToString() != "False") ? true : false;
            chkTransportBill.Checked = (dt.Rows[0]["TrasnportationBill"].ToString() != "False") ? true : false;
            chkGSTInv.Checked = (dt.Rows[0]["GSTInvoice"].ToString() != "False") ? true : false;

            chkITChallan.Checked = (dt.Rows[0]["ITChallan"].ToString() != "False") ? true : false;
            chkBEImporter.Checked = (dt.Rows[0]["BEImporter"].ToString() != "False") ? true : false;
            chkKPTWharfage.Checked = (dt.Rows[0]["KPTWharfage"].ToString() != "False") ? true : false;
            chkKPTStorage.Checked = (dt.Rows[0]["KPTStorage"].ToString() != "False") ? true : false;
            chkMTOLift.Checked = (dt.Rows[0]["MTOLift"].ToString() != "False") ? true : false;
            chkYardCharges.Checked = (dt.Rows[0]["YardCharges"].ToString() != "False") ? true : false;
            chkEForm.Checked = (dt.Rows[0]["EForm"].ToString() != "False") ? true : false;
            chkBL.Checked = (dt.Rows[0]["BLCopy"].ToString() != "False") ? true : false;
            chkAirwayBL.Checked = (dt.Rows[0]["AirwaysBL"].ToString() != "False") ? true : false;
            chkInsuranceDoc.Checked = (dt.Rows[0]["InsuranceDoc"].ToString() != "False") ? true : false;
            chkBondPapers.Checked = (dt.Rows[0]["BondPapers"].ToString() != "False") ? true : false;
            chkBEExchange.Checked = (dt.Rows[0]["BEExchange"].ToString() != "False") ? true : false;
            chkOriginal.Checked = (dt.Rows[0]["Original"].ToString() != "False") ? true : false;
            chkDuplicate.Checked = (dt.Rows[0]["Duplicate"].ToString() != "False") ? true : false;
            chkEndorsmentReceipt.Checked = (dt.Rows[0]["EndorsmentReceipt"].ToString() != "False") ? true : false;
            chkOtherDocs1.Checked = (dt.Rows[0]["OtherDocs1"].ToString() != "False") ? true : false;
            chkOtherDocs2.Checked = (dt.Rows[0]["OtherDocs2"].ToString() != "False") ? true : false;
            chkOtherDocs3.Checked = (dt.Rows[0]["OtherDocs3"].ToString() != "False") ? true : false;
            chkOtherDocs4.Checked = (dt.Rows[0]["OtherDocs4"].ToString() != "False") ? true : false;
            chkOtherDocs5.Checked = (dt.Rows[0]["OtherDocs5"].ToString() != "False") ? true : false;
            chkOtherDocs6.Checked = (dt.Rows[0]["OtherDocs6"].ToString() != "False") ? true : false;
            chkOtherDocs7.Checked = (dt.Rows[0]["OtherDocs7"].ToString() != "False") ? true : false;
            chkOtherDocs8.Checked = (dt.Rows[0]["OtherDocs8"].ToString() != "False") ? true : false;
            chkOtherDocs9.Checked = (dt.Rows[0]["OtherDocs9"].ToString() != "False") ? true : false;
            chkOtherDocs10.Checked = (dt.Rows[0]["OtherDocs10"].ToString() != "False") ? true : false;
            chkOtherDocs11.Checked = (dt.Rows[0]["OtherDocs11"].ToString() != "False") ? true : false;

            OtherDocs1.Text = dt.Rows[0]["OtherDocs1_name"].ToString();
            OtherDocs2.Text = dt.Rows[0]["OtherDocs2_name"].ToString();
            OtherDocs3.Text = dt.Rows[0]["OtherDocs3_name"].ToString();
            OtherDocs4.Text = dt.Rows[0]["OtherDocs4_name"].ToString();
            OtherDocs5.Text = dt.Rows[0]["OtherDocs5_name"].ToString();
            OtherDocs6.Text = dt.Rows[0]["OtherDocs6_name"].ToString();
            OtherDocs7.Text = dt.Rows[0]["OtherDocs7_name"].ToString();
            OtherDocs8.Text = dt.Rows[0]["OtherDocs8_name"].ToString();
            OtherDocs9.Text = dt.Rows[0]["OtherDocs9_name"].ToString();
            OtherDocs10.Text = dt.Rows[0]["OtherDocs10_name"].ToString();
            OtherDocs11.Text = dt.Rows[0]["OtherDocs11_name"].ToString();
            
            Job j = new Job();
            j = j.Read(SCGL_Common.CheckInt(dt.Rows[0]["JobID"]));
            hdnJobNumber.Value = j.JobID.ToString();
            txtJobNumber.Text= j.JobNumber;
            FillJobDetail(j);
            btnSave.Text = "Update";
        }

        InvoiceDetail IDetail = new InvoiceDetail();
        List<InvoiceDetail> IDetailList = IDetail.ReadByInvoiceID(Id);
        //DataTable dtInvoiceDetail = BALInvoice.getInvoiceDetail(Id);

        for (int i = 0; i < IDetailList.Count; i++)
        {
            DropDownList ddlDescription = (DropDownList)GV_CommercialInvoiceDetail.Rows[i].Cells[1].FindControl("ddlDescription");
            TextBox txtNumber = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[2].FindControl("txtNumber");
            TextBox txtRemarks = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[3].FindControl("txtRemarks");
            TextBox txtDate = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[4].FindControl("txtDate");
            TextBox txtByParty = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[5].FindControl("txtByParty");
            TextBox txtByUs = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[6].FindControl("txtByUs");

            ddlDescription.SelectedValue = IDetailList[i].InvoiceDescID.ToString();
            txtNumber.Text = IDetailList[i].Number;
            txtRemarks.Text = IDetailList[i].Remarks;
            txtDate.Text = IDetailList[i].Date.ToString();
            txtByParty.Text = IDetailList[i].ByParty.ToString();
            txtByUs.Text = IDetailList[i].ByUS.ToString();

            btnSave.Text = "Update";
        }

        InvoiceDutiesDetail IDutiesDetail = new InvoiceDutiesDetail();
        List<InvoiceDutiesDetail> IDutiesDetailList = IDutiesDetail.ReadByInvoiceID(Id);

        for (int i = 0; i < IDutiesDetailList.Count; i++)
        {
            DropDownList ddlDescription_duties = (DropDownList)GV_CommercialInvoiceDutiesDetail.Rows[i].Cells[1].FindControl("ddlDescription_duties");
            TextBox txtNumber_duties = (TextBox)GV_CommercialInvoiceDutiesDetail.Rows[i].Cells[2].FindControl("txtNumber_duties");
            TextBox txtRemarks_duties = (TextBox)GV_CommercialInvoiceDutiesDetail.Rows[i].Cells[3].FindControl("txtRemarks_duties");
            TextBox txtDate_duties = (TextBox)GV_CommercialInvoiceDutiesDetail.Rows[i].Cells[4].FindControl("txtDate_duties");
            TextBox txtByParty_duties = (TextBox)GV_CommercialInvoiceDutiesDetail.Rows[i].Cells[5].FindControl("txtByParty_duties");
            TextBox txtByUs_duties = (TextBox)GV_CommercialInvoiceDutiesDetail.Rows[i].Cells[6].FindControl("txtByUs_duties");

            ddlDescription_duties.SelectedValue = IDutiesDetailList[i].InvoiceDutiesDescID.ToString();
            txtNumber_duties.Text = IDutiesDetailList[i].DutiesNumber;
            txtRemarks_duties.Text = IDutiesDetailList[i].DutiesRemarks;
            txtDate_duties.Text = IDutiesDetailList[i].DutiesDate.ToString();
            txtByParty_duties.Text = IDutiesDetailList[i].DutiesByParty.ToString();
            txtByUs_duties.Text = IDutiesDetailList[i].DutiesByUS.ToString();

            btnSave.Text = "Update";
        }

        
    }

    //public void Bind_Customer()
    //{
    //    SCGL_Common.Bind_DropDown(ddlCustomer, "vt_SCGL_BindCustomer", "CustomerName", "ID");
    //}




    public void Reload_JS()
    {
        SCGL_Common.ReloadJS(this, "MyDate();totalAmount();recalculate();");
        //SCGL_Common.ReloadJS(this, "calculateSum();");
        //SCGL_Common.ReloadJS(this, "vale();");
        //SCGL_Common.ReloadJS(this, "GrossTotalDeduction();");
        //SCGL_Common.ReloadJS(this, "TxtBlur();");
        //SCGL_Common.ReloadJS(this, "TotalGridAmount();");
        //SCGL_Common.ReloadJS(this, "ChangeConversionRate();");
        SCGL_Common.ReloadJS(this, "Job_AutoCom_Dialog();");
    }

    public void Gv_GetRows1()
    {
        SqlDataReader dr = BALInvoice.Get_Rows_InvoiceDetail_byID(SCGL_Common.Convert_ToInt(Request.QueryString["ID"]));
        while (dr.Read())
        {
            Session["GV1"] = dr["RowNumber"].ToString();
        }
    }

    public void Gv_Duties_GetRows1()
    {
        SqlDataReader dr = BALInvoice.Get_Rows_InvoiceDutiesDetail_byID(SCGL_Common.Convert_ToInt(Request.QueryString["ID"]));
        while (dr.Read())
        {
            Session["GV1_duties"] = dr["RowNumber"].ToString();
        }
    }

    private void SetInitialRow_For_Edit()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlDescription", typeof(string)));
        dt.Columns.Add(new DataColumn("txtNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("txtRemarks", typeof(string)));
        dt.Columns.Add(new DataColumn("txtDate", typeof(string)));
        dt.Columns.Add(new DataColumn("txtByParty", typeof(string)));
        dt.Columns.Add(new DataColumn("txtByUs", typeof(string)));

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
        Bind_Product_Dropdown();
        //Bind_CostCenter_Dropdown();
    }

    private void SetInitialRow_For_Edit_duties()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlDescription_duties", typeof(string)));
        dt.Columns.Add(new DataColumn("txtNumber_duties", typeof(string)));
        dt.Columns.Add(new DataColumn("txtRemarks_duties", typeof(string)));
        dt.Columns.Add(new DataColumn("txtDate_duties", typeof(string)));
        dt.Columns.Add(new DataColumn("txtByParty_duties", typeof(string)));
        dt.Columns.Add(new DataColumn("txtByUs_duties", typeof(string)));

        if (Convert.ToInt32(Session["GV1_duties"]) < 1)
        {
            for (int i = 0; i < 1; i++)
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
            }
        }
        else
        {
            for (int i = 0; i < Convert.ToInt32(Session["GV1_duties"]); i++)
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
            }
        }
        ViewState["CurrentTable_duties"] = dt;
        GV_CommercialInvoiceDutiesDetail.DataSource = dt;
        GV_CommercialInvoiceDutiesDetail.DataBind();
        Bind_Product_Duties_Dropdown();
        //Bind_CostCenter_Dropdown();
    }

    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlDescription", typeof(string)));
        dt.Columns.Add(new DataColumn("txtNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("txtRemarks", typeof(string)));
        dt.Columns.Add(new DataColumn("txtDate", typeof(string)));
        dt.Columns.Add(new DataColumn("txtByParty", typeof(string)));
        dt.Columns.Add(new DataColumn("txtByUs", typeof(string)));

        for (int i = 0; i < 1; i++)
        {
            dr = dt.NewRow();
            dt.Rows.Add(dr);
        }

        ViewState["CurrentTable"] = dt;

        GV_CommercialInvoiceDetail.DataSource = dt;
        GV_CommercialInvoiceDetail.DataBind();
        Bind_Product_Dropdown();
        //Bind_CostCenter_Dropdown();
        //Bind_Product_Dropdown();
    }

    private void SetInitialRow_duties()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("ddlDescription_duties", typeof(string)));
        dt.Columns.Add(new DataColumn("txtNumber_duties", typeof(string)));
        dt.Columns.Add(new DataColumn("txtRemarks_duties", typeof(string)));
        dt.Columns.Add(new DataColumn("txtDate_duties", typeof(string)));
        dt.Columns.Add(new DataColumn("txtByParty_duties", typeof(string)));
        dt.Columns.Add(new DataColumn("txtByUs_duties", typeof(string)));

        for (int i = 0; i < 1; i++)
        {
            dr = dt.NewRow();
            dt.Rows.Add(dr);
        }

        ViewState["CurrentTable_duties"] = dt;

        GV_CommercialInvoiceDutiesDetail.DataSource = dt;
        GV_CommercialInvoiceDutiesDetail.DataBind();
        Bind_Product_Duties_Dropdown();
        //Bind_CostCenter_Dropdown();
        //Bind_Product_Dropdown();
    }

    protected void btnAddLines_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }

    protected void btnAddLines_duties_Click(object sender, EventArgs e)
    {
        AddNewDutiesRowToGrid();
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

                    DropDownList ddlDescription = (DropDownList)GV_CommercialInvoiceDetail.Rows[i].Cells[1].FindControl("ddlDescription");
                    TextBox txtNumber = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[2].FindControl("txtNumber");
                    TextBox txtRemarks = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[3].FindControl("txtRemarks");
                    TextBox txtDate = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[4].FindControl("txtDate");
                    TextBox txtByParty = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[5].FindControl("txtByParty");
                    TextBox txtByUs = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[6].FindControl("txtByUs");

                    drCurrentRow["ddlDescription"] = ddlDescription.SelectedValue;
                    drCurrentRow["txtNumber"] = txtNumber.Text;
                    drCurrentRow["txtRemarks"] = txtRemarks.Text;
                    drCurrentRow["txtDate"] = txtDate.Text;
                    drCurrentRow["txtByParty"] = txtByParty.Text;
                    drCurrentRow["txtByUs"] = txtByUs.Text;

                    Product_Table.Rows.Add(drCurrentRow);
                }
            }
            DataRow dr = Product_Table.NewRow();
            dr[1] = "0";
            Product_Table.Rows.Add(dr);
            GV_CommercialInvoiceDetail.DataSource = Product_Table;
            GV_CommercialInvoiceDetail.DataBind();
            Bind_Product_Dropdown();
            //Bind_CostCenter_Dropdown();
            for (int i = 0; i < GV_CommercialInvoiceDetail.Rows.Count; i++)
            {
                DropDownList cbox = GV_CommercialInvoiceDetail.Rows[i].FindControl("ddlDescription") as DropDownList;
                cbox.SelectedValue = Product_Table.Rows[i]["ddlDescription"].ToString();
            }            
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void AddNewDutiesRowToGrid()
    {
        try
        {
            Product_Table_duties.Rows.Clear();
            for (int i = 0; i < GV_CommercialInvoiceDutiesDetail.Rows.Count; i++)
            {
                if (GV_CommercialInvoiceDutiesDetail.Rows[i].Visible)
                {
                    DataRow drCurrentRow_duties = Product_Table_duties.NewRow();

                    DropDownList ddlDescription_duties = (DropDownList)GV_CommercialInvoiceDutiesDetail.Rows[i].Cells[1].FindControl("ddlDescription_duties");
                    TextBox txtNumber_duties = (TextBox)GV_CommercialInvoiceDutiesDetail.Rows[i].Cells[2].FindControl("txtNumber_duties");
                    TextBox txtRemarks_duties = (TextBox)GV_CommercialInvoiceDutiesDetail.Rows[i].Cells[3].FindControl("txtRemarks_duties");
                    TextBox txtDate_duties = (TextBox)GV_CommercialInvoiceDutiesDetail.Rows[i].Cells[4].FindControl("txtDate_duties");
                    TextBox txtByParty_duties = (TextBox)GV_CommercialInvoiceDutiesDetail.Rows[i].Cells[5].FindControl("txtByParty_duties");
                    TextBox txtByUs_duties = (TextBox)GV_CommercialInvoiceDutiesDetail.Rows[i].Cells[6].FindControl("txtByUs_duties");

                    drCurrentRow_duties["ddlDescription_duties"] = ddlDescription_duties.SelectedValue;
                    drCurrentRow_duties["txtNumber_duties"] = txtNumber_duties.Text;
                    drCurrentRow_duties["txtRemarks_duties"] = txtRemarks_duties.Text;
                    drCurrentRow_duties["txtDate_duties"] = txtDate_duties.Text;
                    drCurrentRow_duties["txtByParty_duties"] = txtByParty_duties.Text;
                    drCurrentRow_duties["txtByUs_duties"] = txtByUs_duties.Text;

                    Product_Table_duties.Rows.Add(drCurrentRow_duties);
                }
            }
            DataRow dr_duties = Product_Table_duties.NewRow();
            dr_duties[1] = "0";
            Product_Table_duties.Rows.Add(dr_duties);
            GV_CommercialInvoiceDutiesDetail.DataSource = Product_Table_duties;
            GV_CommercialInvoiceDutiesDetail.DataBind();
            Bind_Product_Duties_Dropdown();
            //Bind_CostCenter_Dropdown();
            for (int i = 0; i < GV_CommercialInvoiceDutiesDetail.Rows.Count; i++)
            {
                DropDownList cbox = GV_CommercialInvoiceDutiesDetail.Rows[i].FindControl("ddlDescription_duties") as DropDownList;
                cbox.SelectedValue = Product_Table_duties.Rows[i]["ddlDescription_duties"].ToString();
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
                DataRow drCurrentRow = Product_Table.NewRow();
                DropDownList ddlDescription = (DropDownList)GV_CommercialInvoiceDetail.Rows[i].Cells[1].FindControl("ddlDescription");
                TextBox txtNumber = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[2].FindControl("txtNumber");
                TextBox txtRemarks = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[3].FindControl("txtRemarks");
                TextBox txtDate = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[4].FindControl("txtDate");
                TextBox txtByParty = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[5].FindControl("txtByParty");
                TextBox txtByUs = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[6].FindControl("txtByUs");

                drCurrentRow["ddlDescription"] = ddlDescription.SelectedValue;
                drCurrentRow["txtNumber"] = txtNumber.Text;
                drCurrentRow["txtRemarks"] = txtRemarks.Text;
                drCurrentRow["txtDate"] = txtDate.Text;
                drCurrentRow["txtByParty"] = txtByParty.Text;
                drCurrentRow["txtByUs"] = txtByUs.Text;

                Product_Table.Rows.Add(drCurrentRow);
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

            if (Product_Table.Rows[pr]["ddlDescription"].ToString() == "")
            {
                Product_Table.Rows.RemoveAt(pr);
                pr--;
            }


        }
        return dtProduct;
    }

    public DataTable Product_DataSource_duties()
    {
        DataTable dtProduct_duties = Product_Table_duties; dtProduct_duties.Rows.Clear();
        for (int i = 0; i < GV_CommercialInvoiceDutiesDetail.Rows.Count; i++)
        {
            if (GV_CommercialInvoiceDutiesDetail.Rows[i].Visible)
            {
                DataRow drCurrentRow_duties = Product_Table_duties.NewRow();
                DropDownList ddlDescription_duties = (DropDownList)GV_CommercialInvoiceDutiesDetail.Rows[i].Cells[1].FindControl("ddlDescription_duties");
                TextBox txtNumber_duties = (TextBox)GV_CommercialInvoiceDutiesDetail.Rows[i].Cells[2].FindControl("txtNumber_duties");
                TextBox txtRemarks_duties = (TextBox)GV_CommercialInvoiceDutiesDetail.Rows[i].Cells[3].FindControl("txtRemarks_duties");
                TextBox txtDate_duties = (TextBox)GV_CommercialInvoiceDutiesDetail.Rows[i].Cells[4].FindControl("txtDate_duties");
                TextBox txtByParty_duties = (TextBox)GV_CommercialInvoiceDutiesDetail.Rows[i].Cells[5].FindControl("txtByParty_duties");
                TextBox txtByUs_duties = (TextBox)GV_CommercialInvoiceDutiesDetail.Rows[i].Cells[6].FindControl("txtByUs_duties");

                drCurrentRow_duties["ddlDescription_duties"] = ddlDescription_duties.SelectedValue;
                drCurrentRow_duties["txtNumber_duties"] = txtNumber_duties.Text;
                drCurrentRow_duties["txtRemarks_duties"] = txtRemarks_duties.Text;
                drCurrentRow_duties["txtDate_duties"] = txtDate_duties.Text;
                drCurrentRow_duties["txtByParty_duties"] = txtByParty_duties.Text;
                drCurrentRow_duties["txtByUs_duties"] = txtByUs_duties.Text;

                Product_Table_duties.Rows.Add(drCurrentRow_duties);
            }
            else
            {
                dtProduct_duties.Rows.Add(dtProduct_duties.NewRow());
            }
        }
        for (int pr = 0; pr < Product_Table_duties.Rows.Count; pr++)
        {
            //if (Product_Table.Rows[pr]["ddlInventory"].ToString() == "" || Product_Table.Rows[pr]["txtDescription"].ToString() == "" || Product_Table.Rows[pr]["txtItemSize"].ToString() == "")
            //{
            //    Product_Table.Rows.RemoveAt(pr);
            //    pr--;
            //}

            if (Product_Table_duties.Rows[pr]["ddlDescription_duties"].ToString() == "")
            {
                Product_Table_duties.Rows.RemoveAt(pr);
                pr--;
            }


        }
        return dtProduct_duties;
    }

    private DataTable Product_Table
    {
        get
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt == null)
            {
                dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dt.Columns.Add(new DataColumn("ddlDescription", typeof(string)));
                dt.Columns.Add(new DataColumn("txtNumber", typeof(string)));
                dt.Columns.Add(new DataColumn("txtRemarks", typeof(string)));
                dt.Columns.Add(new DataColumn("txtDate", typeof(string)));
                dt.Columns.Add(new DataColumn("txtByParty", typeof(string)));
                dt.Columns.Add(new DataColumn("txtByUs", typeof(string)));
                
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

    private DataTable Product_Table_duties
    {
        get
        {
            DataTable dt = (DataTable)ViewState["CurrentTable_duties"];
            if (dt == null)
            {
                dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dt.Columns.Add(new DataColumn("ddlDescription_duties", typeof(string)));
                dt.Columns.Add(new DataColumn("txtNumber_duties", typeof(string)));
                dt.Columns.Add(new DataColumn("txtRemarks_duties", typeof(string)));
                dt.Columns.Add(new DataColumn("txtDate_duties", typeof(string)));
                dt.Columns.Add(new DataColumn("txtByParty_duties", typeof(string)));
                dt.Columns.Add(new DataColumn("txtByUs_duties", typeof(string)));

                for (int i = 0; i < 10; i++)
                {
                    dt.Rows.Add(dt.NewRow());
                }
            }
            ViewState["CurrentTable_duties"] = dt;
            return dt;
        }
        set
        {
            ViewState["CurrentTable_duties"] = value;
        }
    }

    protected void GV_CommercialInvoiceDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = e.RowIndex;
        if (index != 0)
        {
            GV_CommercialInvoiceDetail.Rows[e.RowIndex].Visible = false;

            DropDownList ddlDescription = (DropDownList)GV_CommercialInvoiceDetail.Rows[e.RowIndex].Cells[1].FindControl("ddlDescription");
            TextBox txtNumber = (TextBox)GV_CommercialInvoiceDetail.Rows[e.RowIndex].Cells[2].FindControl("txtNumber");
            TextBox txtRemarks = (TextBox)GV_CommercialInvoiceDetail.Rows[e.RowIndex].Cells[3].FindControl("txtRemarks");
            TextBox txtDate = (TextBox)GV_CommercialInvoiceDetail.Rows[e.RowIndex].Cells[4].FindControl("txtDate");
            TextBox txtByParty = (TextBox)GV_CommercialInvoiceDetail.Rows[e.RowIndex].Cells[5].FindControl("txtByParty");
            TextBox txtByUs = (TextBox)GV_CommercialInvoiceDetail.Rows[e.RowIndex].Cells[6].FindControl("txtByUs");

            if (txtByParty.Text == "") { txtByParty.Text = "0"; }
            if (txtByUs.Text == "") { txtByUs.Text = "0"; }

            ddlDescription.SelectedIndex = 0;
            txtNumber.Text = "";
            txtRemarks.Text = "";
            txtDate.Text = "";
            txtByParty.Text = "";
            txtByUs.Text = "";
            Reload_JS();
        }
    }

    protected void GV_CommercialInvoiceDutiesDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = e.RowIndex;
        if (index != 0)
        {
            GV_CommercialInvoiceDutiesDetail.Rows[e.RowIndex].Visible = false;

            DropDownList ddlDescription_duties = (DropDownList)GV_CommercialInvoiceDutiesDetail.Rows[e.RowIndex].Cells[1].FindControl("ddlDescription_duties");
            TextBox txtNumber_duties = (TextBox)GV_CommercialInvoiceDutiesDetail.Rows[e.RowIndex].Cells[2].FindControl("txtNumber_duties");
            TextBox txtRemarks_duties = (TextBox)GV_CommercialInvoiceDutiesDetail.Rows[e.RowIndex].Cells[3].FindControl("txtRemarks_duties");
            TextBox txtDate_duties = (TextBox)GV_CommercialInvoiceDutiesDetail.Rows[e.RowIndex].Cells[4].FindControl("txtDate_duties");
            TextBox txtByParty_duties = (TextBox)GV_CommercialInvoiceDutiesDetail.Rows[e.RowIndex].Cells[5].FindControl("txtByParty_duties");
            TextBox txtByUs_duties = (TextBox)GV_CommercialInvoiceDutiesDetail.Rows[e.RowIndex].Cells[6].FindControl("txtByUs_duties");

            if (txtByParty_duties.Text == "") { txtByParty_duties.Text = "0"; }
            if (txtByUs_duties.Text == "") { txtByUs_duties.Text = "0"; }

            ddlDescription_duties.SelectedIndex = 0;
            txtNumber_duties.Text = "";
            txtRemarks_duties.Text = "";
            txtDate_duties.Text = "";
            txtByParty_duties.Text = "";
            txtByUs_duties.Text = "";
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
            if (Product_Table.Rows[p]["ddlDescription"].ToString() != null && Product_Table.Rows[p]["ddlDescription"].ToString() != "")
            {
                dtInsert.Rows[p]["ddlDescription"] = Product_Table.Rows[p]["ddlDescription"].ToString();
                dtInsert.Rows[p]["txtNumber"] = Product_Table.Rows[p]["txtNumber"].ToString();
                dtInsert.Rows[p]["txtRemarks"] = Product_Table.Rows[p]["txtRemarks"].ToString();
                dtInsert.Rows[p]["txtDate"] = Product_Table.Rows[p]["txtDate"].ToString();
                dtInsert.Rows[p]["txtByParty"] = Product_Table.Rows[p]["txtByParty"].ToString();
                dtInsert.Rows[p]["txtByUs"] = Product_Table.Rows[p]["txtByUs"].ToString();
            }
        }

        return dtInsert;
    }

    public DataTable Record_for_Insert_duties()
    {
        Product_Table_duties = Product_DataSource_duties();

        int Product_Rows_duties = Product_Table_duties.Rows.Count;

        int TotalRows_duties = 0;
        if (Product_Rows_duties > 0)
        {
            TotalRows_duties = Product_Rows_duties;
        }


        DataTable dtInsert_duties = new DataTable();
        dtInsert_duties.Merge(Product_Table_duties);

        dtInsert_duties.Rows.Clear();
        for (int r = 0; r < TotalRows_duties; r++)
        {
            dtInsert_duties.Rows.Add(dtInsert_duties.NewRow());
        }
        for (int p = 0; p < Product_Rows_duties; p++)
        {
            if (Product_Table_duties.Rows[p]["ddlDescription_duties"].ToString() != null && Product_Table_duties.Rows[p]["ddlDescription_duties"].ToString() != "")
            {
                dtInsert_duties.Rows[p]["ddlDescription_duties"] = Product_Table_duties.Rows[p]["ddlDescription_duties"].ToString();
                dtInsert_duties.Rows[p]["txtNumber_duties"] = Product_Table_duties.Rows[p]["txtNumber_duties"].ToString();
                dtInsert_duties.Rows[p]["txtRemarks_duties"] = Product_Table_duties.Rows[p]["txtRemarks_duties"].ToString();
                dtInsert_duties.Rows[p]["txtDate_duties"] = Product_Table_duties.Rows[p]["txtDate_duties"].ToString();
                dtInsert_duties.Rows[p]["txtByParty_duties"] = Product_Table_duties.Rows[p]["txtByParty_duties"].ToString();
                dtInsert_duties.Rows[p]["txtByUs_duties"] = Product_Table_duties.Rows[p]["txtByUs_duties"].ToString();
            }
        }

        return dtInsert_duties;
    }

    protected void btnClearAllLines_Click(object sender, EventArgs e)
    {
        SetInitialRow();
    }

    protected void btnClearAllLines_duties_Click(object sender, EventArgs e)
    {
        SetInitialRow_duties();
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
        //decimal CogRate = 0;
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
            //BALInvoice.CustomerID = SCGL_Common.Convert_ToInt(ddlCustomer.SelectedValue);
            //BALInvoice.Email = txtEmail.Text;
            //BALInvoice.BillingAddress = txtBillingAddress.Text;
            //BALInvoice.Invoice_No = txtInvoiceNumber.Text;  //  Database column name for this field : InvoiceNo
            BALInvoice.TermID = txtTerm.Text;
            BALInvoice.InvoiceDate = SCGL_Common.CheckDateTime(txtInvoiceDate.Text);
            //BALInvoice.InvoiceDate = txtInvoiceDate.Text.ToString();
            BALInvoice.ReferenceNo = txtReferenceNo.Text;
            //BALInvoice.DueDate = txtDueDate.Text.ToString();
            //BALInvoice.Discount = txtDiscount.Text == "" ? SCGL_Common.Convert_ToDecimal("0")
            //   : SCGL_Common.Convert_ToDecimal(txtDiscount.Text);
            BALInvoice.TotalByParty = SCGL_Common.Convert_ToDecimal(txttotalByParty.Value);
            //BALInvoice.Currency = SCGL_Common.Convert_ToInt(ddlCurrency.SelectedValue);
            //BALInvoice.ConversionRate = SCGL_Common.Convert_ToDecimal(txtConversionRate.Text);
            BALInvoice.TotalByUs = SCGL_Common.Convert_ToDecimal(txtTotalByUS.Value);
            BALInvoice.LoginID = SBO.UserID;
            BALInvoice.FinYearID = SBO.FinYearID;
            //BALInvoice.From = txtFrom.Text;
            //BALInvoice.To = txtTo.Text;
            BALInvoice.GrossWeight = SCGL_Common.Convert_ToDecimal(txtGrossWeight.Text);
            //BALInvoice.Origin_Country = txtOrCountry.Text;
            //BALInvoice.Destination_Country = txtDestCountry.Text;
            //BALInvoice.Vessel = txtVessel.Text;
            BALInvoice.FormENo = txtFormENo.Text;
            BALInvoice.Freight = txtFreight.Text;
            BALInvoice.NetWeight = SCGL_Common.Convert_ToDecimal(txtNetWeight.Text);
            //BALInvoice.ContainerNo = txtContainerNo.Text;
            BALInvoice.ProformaNo = txtproformaNo.Text;
            //BALInvoice.Insurance = txtInsurance.Text;
            BALInvoice.Exporter = txtExporter.Text;
            BALInvoice.Consignee = txtConsignee.Text;
            BALInvoice.Buyer = txtBuyer.Text;
            BALInvoice.ExportersRef = txtExportersRef.Text;
            BALInvoice.Note = txtNote.Text;
            BALInvoice.JobID = SCGL_Common.CheckInt(hdnJobNumber.Value);
            BALInvoice.BillNumber = txtBillNumber.Text;
            BALInvoice.CustInvoiceNo = txtCustInvoiceNo.Text;
            BALInvoice.IsAbbott = chkAbbottInvoice.Checked;
            BALInvoice.NoAdvance = chkNoAdvance.Checked;
            //BALInvoice.CustomDuty = SCGL_Common.Convert_ToDecimal(txtCustomDuty.Text);
            BALInvoice.CustomPONo = txtCustomPONo.Text;
            BALInvoice.CustomPODate = SCGL_Common.CheckDateTime(txtCustomPODate.Text);
            BALInvoice.CustomByParty = SCGL_Common.Convert_ToDecimal(txtCustomByParty.Text);
            BALInvoice.CustomByUs = SCGL_Common.Convert_ToDecimal(txtCustomByUs.Text);
            BALInvoice.SalesTaxPONo = txtSalesTaxPONo.Text;
            BALInvoice.SalesTaxFine = SCGL_Common.Convert_ToDecimal(txtSalesTaxFine.Text);
            BALInvoice.SalesTaxByParty = SCGL_Common.Convert_ToDecimal(txtSalesTaxByParty.Text);
            BALInvoice.SalesTaxByUs = SCGL_Common.Convert_ToDecimal(txtSalesTaxByUs.Text);
            BALInvoice.IncomeTaxPONo = txtIncomeTaxPONo.Text;
            BALInvoice.IncomeTaxAddition = SCGL_Common.Convert_ToDecimal(txtIncomeTaxAddition.Text);
            BALInvoice.IncomeTaxByParty = SCGL_Common.Convert_ToDecimal(txtIncomeTaxByParty.Text);
            BALInvoice.IncomeTaxByUs = SCGL_Common.Convert_ToDecimal(txtIncomeTaxByUs.Text);

            BALInvoice.CEDPercent = txtCEDPercent.Text;
            BALInvoice.CEDByParty = SCGL_Common.Convert_ToDecimal(txtCEDByParty.Text);
            BALInvoice.CEDByUs = SCGL_Common.Convert_ToDecimal(txtCEDByUs.Text);

            BALInvoice.EOCPercent = txtEOCPercent.Text;
            BALInvoice.EOCByParty = SCGL_Common.Convert_ToDecimal(txtEOCByParty.Text);
            BALInvoice.EOCByUs = SCGL_Common.Convert_ToDecimal(txtEOCByUs.Text);

            BALInvoice.FEDPercent = txtFEDPercent.Text;
            BALInvoice.FEDByParty = SCGL_Common.Convert_ToDecimal(txtFEDByParty.Text);
            BALInvoice.FEDByUs = SCGL_Common.Convert_ToDecimal(txtFEDByUs.Text);

            BALInvoice.OthersPercent = txtOthersPercent.Text;
            BALInvoice.OthersByParty = SCGL_Common.Convert_ToDecimal(txtOthersByParty.Text);
            BALInvoice.OthersByUs = SCGL_Common.Convert_ToDecimal(txtOthersByUs.Text);

            BALInvoice.ExcessShortDutyPONo = txtExcessShortDutyPONo.Text;
            BALInvoice.ExcessShortDutyByParty = SCGL_Common.Convert_ToDecimal(txtExcessShortDutyByParty.Text);
            BALInvoice.ExcessShortDutyByUs = SCGL_Common.Convert_ToDecimal(txtExcessShortDutyByUs.Text);

            BALInvoice.Status = SCGL_Common.Convert_ToInt(ddlStatus.SelectedValue);
            BALInvoice.ChequeNo = txtChequeNo.Text;
            BALInvoice.ReceivedDate = SCGL_Common.CheckDateTime(txtReceivedDate.Text);
            BALInvoice.DeliveryChallan = chkDeliveryChallan.Checked;
            BALInvoice.LCContract = chkLCContract.Checked;
            BALInvoice.Certificates = chkCertificates.Checked;
            BALInvoice.PackingList = chkPackingList.Checked;
            BALInvoice.Invoice = chkInvoice.Checked;
            BALInvoice.WebocGD = chkWeboc.Checked;
            BALInvoice.PaccsCoupon = chkPaccsCoupon.Checked;
            BALInvoice.CashPaymentReceipt = chkCashPayReceipt.Checked;
            BALInvoice.ExciseDutyChallan = chkExciseDutyChallan.Checked;
            BALInvoice.ExciseTaxChallan = chkExciseTaxChallan.Checked;
            BALInvoice.DeliveryOrderReceipt = chkDOR.Checked;
            BALInvoice.PICTLInvoice = chkPICTLInv.Checked;
            BALInvoice.TrasnportationBill = chkTransportBill.Checked;
            BALInvoice.GSTInvoice = chkGSTInv.Checked;

            BALInvoice.ITChallan = chkITChallan.Checked;
            BALInvoice.BEImporter = chkBEImporter.Checked;
            BALInvoice.KPTWharfage = chkKPTWharfage.Checked;
            BALInvoice.KPTStorage = chkKPTStorage.Checked;
            BALInvoice.MTOLift = chkMTOLift.Checked;
            BALInvoice.YardCharges = chkYardCharges.Checked;
            BALInvoice.EForm = chkEForm.Checked;
            BALInvoice.BLCopy = chkBL.Checked;
            BALInvoice.AirwaysBL = chkAirwayBL.Checked;
            BALInvoice.InsuranceDoc = chkInsuranceDoc.Checked;
            BALInvoice.BondPapers = chkBondPapers.Checked;
            BALInvoice.BEExchange = chkBEExchange.Checked;
            BALInvoice.Original = chkOriginal.Checked;
            BALInvoice.Duplicate = chkDuplicate.Checked;
            BALInvoice.EndorsmentReceipt = chkEndorsmentReceipt.Checked;
            BALInvoice.OtherDocs1_name = OtherDocs1.Text;
            BALInvoice.OtherDocs2_name = OtherDocs2.Text;
            BALInvoice.OtherDocs3_name = OtherDocs3.Text;
            BALInvoice.OtherDocs4_name = OtherDocs4.Text;
            BALInvoice.OtherDocs5_name = OtherDocs5.Text;
            BALInvoice.OtherDocs6_name = OtherDocs6.Text;
            BALInvoice.OtherDocs7_name = OtherDocs7.Text;
            BALInvoice.OtherDocs8_name = OtherDocs8.Text;
            BALInvoice.OtherDocs9_name = OtherDocs9.Text;
            BALInvoice.OtherDocs10_name = OtherDocs10.Text;
            BALInvoice.OtherDocs11_name = OtherDocs11.Text;

            BALInvoice.OtherDocs1 = chkOtherDocs1.Checked;
            BALInvoice.OtherDocs2 = chkOtherDocs2.Checked;
            BALInvoice.OtherDocs3 = chkOtherDocs3.Checked;
            BALInvoice.OtherDocs4 = chkOtherDocs4.Checked;
            BALInvoice.OtherDocs5 = chkOtherDocs5.Checked;
            BALInvoice.OtherDocs6 = chkOtherDocs6.Checked;
            BALInvoice.OtherDocs7 = chkOtherDocs7.Checked;
            BALInvoice.OtherDocs8 = chkOtherDocs8.Checked;
            BALInvoice.OtherDocs9 = chkOtherDocs9.Checked;
            BALInvoice.OtherDocs10 = chkOtherDocs10.Checked;
            BALInvoice.OtherDocs11 = chkOtherDocs11.Checked;
            

            int InvoiceID = BALInvoice.CreateModifyInvoice(BALInvoice, trans);
            if (InvoiceID > 0)
            {
                txtInvoiceID.Text = InvoiceID.ToString();
                //BALInvoice.InvoiceNo = SCGL_Common.Convert_ToInt(txtInvoiceID.Text);
                if (btnSave.Text == "Update")
                {
                    BALInvoice.Delete_InvoiceDetail(InvoiceID, trans);
                    BALInvoice.Delete_Transaction(InvoiceID, trans);
                }
                //trans.Commit();
                int Counter = 0;
                DataTable dt = Record_for_Insert();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    InvoiceDetail IDetail = new InvoiceDetail();
                    IDetail.InvoiceID = SCGL_Common.CheckInt(txtInvoiceID.Text);
                    IDetail.InvoiceDescID = SCGL_Common.CheckInt(dt.Rows[i]["ddlDescription"].ToString());
                    IDetail.Number = dt.Rows[i]["txtNumber"].ToString();
                    IDetail.Remarks = dt.Rows[i]["txtRemarks"].ToString();
                    IDetail.Date = dt.Rows[i]["txtDate"].ToString();
                    IDetail.ByParty = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtByParty"].ToString());
                    IDetail.ByUS = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtByUs"].ToString());
                    
                    //BALInvoice.Description2 = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtDescription"].ToString());
                    //BALInvoice.Quantity = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtQuantity"].ToString());
                    //BALInvoice.Rate = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtRate"].ToString());
                    //BALInvoice.Amount = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtAmount"].ToString());
                    //BALInvoice.Currency = SCGL_Common.Convert_ToInt(ddlCurrency.SelectedValue);
                    //BALInvoice.ConversionRate = SCGL_Common.Convert_ToDecimal(txtConversionRate.Text);
                    //BALInvoice.PKRAmount = BALInvoice.Amount * BALInvoice.ConversionRate;
                    //BALInvoice.GridName = dt.Rows[i]["txtGridname"].ToString();
                    //BALInvoice.InventoryID = SCGL_Common.Convert_ToInt(dt.Rows[i]["ddl_Inventoryvalue"].ToString());
                    //BALInvoice.CostCenterID = SCGL_Common.Convert_ToInt(dt.Rows[i]["ddl_CostCentervalue"].ToString());
                    //BALInvoice.InvoiceDate = SCGL_Common.CheckDateTime(txtInvoiceDate.Text);

                    //DataTable dtn = new DataTable();
                    //dtn = BALInvoice.getCogs_Rate(SCGL_Common.Convert_ToInt(dt.Rows[i]["lblInventoryId"].ToString()), int.Parse(dt.Rows[i]["Cost_CenterValue"].ToString()), SCGL_Common.CheckDateTime(txtInvoiceDate.Text));
                    //dtn = BALInvoice.getCogs_Rate(BALInvoice, trans);
                    //CogRate = SCGL_Common.Convert_ToDecimal(dtn.Rows[0]["GOGSRate"].ToString());

                    //BALInvoice.COGSRate = SCGL_Common.Convert_ToDecimal(CogRate);
                    if (IDetail.Create(IDetail)>=0)
                    {
                        Counter++;
                    }
                    else
                    {
                        Counter = 0;
                        break;
                    }
                }

                int Counter2 = 0;
                DataTable dt2 = Record_for_Insert_duties();
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    InvoiceDutiesDetail IDutiesDetail = new InvoiceDutiesDetail();
                    IDutiesDetail.InvoiceID = SCGL_Common.CheckInt(txtInvoiceID.Text);
                    IDutiesDetail.InvoiceDutiesDescID = SCGL_Common.CheckInt(dt2.Rows[i]["ddlDescription_duties"].ToString());
                    IDutiesDetail.DutiesNumber = dt2.Rows[i]["txtNumber_duties"].ToString();
                    IDutiesDetail.DutiesRemarks = dt2.Rows[i]["txtRemarks_duties"].ToString();
                    IDutiesDetail.DutiesDate = dt2.Rows[i]["txtDate_duties"].ToString();
                    IDutiesDetail.DutiesByParty = SCGL_Common.Convert_ToDecimal(dt2.Rows[i]["txtByParty_duties"].ToString());
                    IDutiesDetail.DutiesByUS = SCGL_Common.Convert_ToDecimal(dt2.Rows[i]["txtByUs_duties"].ToString());


                    if (IDutiesDetail.Create(IDutiesDetail) >= 0)
                    {
                        Counter2++;
                    }
                    else
                    {
                        Counter2 = 0;
                        break;
                    }
                }

               
                if (Counter > 0 && Counter2>0)
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
        //    DropDownList ddlDescription = (DropDownList)GV_CommercialInvoiceDetail.Rows[i].Cells[1].FindControl("ddlDescription");
        //    TextBox txtNumber = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[2].FindControl("txtNumber");
        //    TextBox txtRemarks = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[3].FindControl("txtRemarks");
        //    TextBox txtDate = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[4].FindControl("txtDate");
        //    TextBox txtByParty = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[5].FindControl("txtByParty");
        //    TextBox txtByUs = (TextBox)GV_CommercialInvoiceDetail.Rows[i].Cells[6].FindControl("txtByUs");

        //    if (SCGL_Common.CheckInt(ddlDescription.SelectedValue) <= 0)
        //    {
        //        SCGL_Common.Error_Message(this);
        //        return false;
        //    }
        //}

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
            //txtInvoiceNumber.Text = Convert.ToInt32(totalInvoiceID).ToString();
        }
        //txtInvoiceNumber.ReadOnly = true;
    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Invoice_Views.aspx");
    }

    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {        
        CustomerForm_BAL custbal = new CustomerForm_BAL();
        //DataTable dt = custbal.getCustomerByID(SCGL_Common.Convert_ToInt(ddlCustomer.SelectedValue));
        //txtBillingAddress.Text = dt.Rows[0]["BillAddressStreet"].ToString();
        //txtEmail.Text = dt.Rows[0]["Email"].ToString();
        //txtBuyer.Text = dt.Rows[0]["Buyer"].ToString();
        //txtConsignee.Text = dt.Rows[0]["Consignee"].ToString();
        //txtDestCountry.Text = dt.Rows[0]["DestCountry"].ToString();
        //txtTo.Text = dt.Rows[0]["PortOfDischarge"].ToString();
    }


    //protected void txtInvoiceDate_TextChanged(object sender, EventArgs e)
    //{
    //    SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
    //    DataTable dt = BALInvoice.GetMaxPurchaseDate(SCGL_Common.Convert_ToInt(SBO.FinYearID));
    //    DateTime PurchaseDate = SCGL_Common.CheckDateTime(dt.Rows[0]["MaxDate"]);
    //    DataTable dt2 = BALInvoice.GetMaxExcessShortDate(SCGL_Common.Convert_ToInt(SBO.FinYearID));
    //    DateTime PhysicalDate = SCGL_Common.CheckDateTime(dt2.Rows[0]["PhysicalDate"]);
    //    if (SCGL_Common.CheckDateTime(txtInvoiceDate.Text) < PurchaseDate || SCGL_Common.CheckDateTime(txtInvoiceDate.Text) < PhysicalDate) 
    //    {

    //        if (SCGL_Common.CheckDateTime(txtInvoiceDate.Text) < PurchaseDate && SCGL_Common.CheckDateTime(txtInvoiceDate.Text) < PhysicalDate)
    //        {
    //            JQ.showStatusMsg(this, "2", "Record Exists in Purchases and ExcessShort in the Future.So Invoice cannot be entered in the past date"); 

    //        }
    //        else if (SCGL_Common.CheckDateTime(txtInvoiceDate.Text) < PurchaseDate)
    //        {
    //            JQ.showStatusMsg(this, "2", "Record Exists in Purchases in the Future.So Invoice cannot be entered in the past date");

    //        }
    //        else if (SCGL_Common.CheckDateTime(txtInvoiceDate.Text) < PhysicalDate)
    //        {
    //            JQ.showStatusMsg(this, "2", "Record Exists in ExcessShort in the Future.So Invoice cannot be entered in the past date");

    //        }


    //        txtInvoiceDate.Text = "";
    //    }
    //    CheckInventory();
    //}

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
        string JobNumber = grdJobs.Rows[rowIndex].Cells[1].Text.ToString();
        txtJobNumber.Text = JobNumber;

        Job j = new Job();
        j = j.GetJobByJobNumber(JobNumber);
        FillJobDetail(j);
        JQ.closeDialog(this, "FindJobs");
    }

    void FillJobDetail(Job j)
    {
        lblCustomerName.Text = j.CustomerName;
        //lblContNumber.Text = j.ContactNo;
        //lblContDated.Text = "";
        lblDescription.Text = j.JobDescription;
        lblContainer.Text = j.Container;
        lblContainerNo.Text = j.ContainerNo;
        lblContainerDate.Text = j.ContainerDate.ToShortDateString();
        lblIGMNo.Text = j.IGMNo;
        lblIGMDated.Text = j.IGMDate.ToShortDateString();
        lblIndexNo.Text = j.IndexNo;
        lblSS.Text = j.SS;
        lblQty.Text = j.QTY.ToString();
        lblBECashNo.Text = j.BECashNo;
        lblBECashDated.Text = j.MachineDate.ToShortDateString();
        lblMachineNo.Text = j.MachineNo;
        lblMachineDate.Text = j.MachineDate.ToShortDateString();
        //lblMachineNo.Text = j.MachineNo.ToString("MM/dd/yyyy");
        //lblDT.Text = "";
        lblDeliveryDate.Text = j.DeliveryDate.ToShortDateString();
        lblCNFValue.Text = j.CNFValue.ToString();
        lblImportValue.Text = j.ImportValue.ToString();
        tblJobDetail.Style.Add("display", "block");
        UpJobDetail.Update();
    }

    protected void btnSaveInvoiceDesc_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)System.Web.HttpContext.Current.Session["SessionBO"];
        if (SBO.Can_Insert == true)
        {
            InvoiceDesc IDesc = new InvoiceDesc();
            IDesc.CreatedDate = DateTime.Now;
            IDesc.InvoiceDescription = txtInvoiceDesc.Text;
            if (IDesc.Create(IDesc) >= 1)
            {
                JQ.closeDialog(this, "NewInvoiceDesc");
                txtInvoiceDesc.Text = "";
                Bind_Product_Dropdown();
            }
        }
        else
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to Insert Record");
        }
    }

    protected void btnSaveInvoiceDutiesDesc_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)System.Web.HttpContext.Current.Session["SessionBO"];
        if (SBO.Can_Insert == true)
        {
            InvoiceDutiesDesc IDesc = new InvoiceDutiesDesc();
            IDesc.CreatedDate = DateTime.Now;
            IDesc.InvoiceDutiesDescription = txtInvoiceDutiesDesc.Text;
            if (IDesc.Create(IDesc) >= 1)
            {
                JQ.closeDialog(this, "NewInvoiceDutiesDesc");
                txtInvoiceDutiesDesc.Text = "";
                Bind_Product_Duties_Dropdown();
            }
        }
        else
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to Insert Record");
        }
    }

    protected void txtJobNumber_TextChanged(object sender, EventArgs e)
    {
        ExpenseSheet_BAL BALInvoice = new ExpenseSheet_BAL();
        int CountJobNo = BALInvoice.CheckJobNo(txtJobNumber.Text);
        if (CountJobNo > 0)
        {
            Job j = new Job();
            j = j.GetJobByJobNumber(txtJobNumber.Text);
            FillJobDetail(j);
            hdnJobNumber.Value = SCGL_Common.Convert_ToString(BALInvoice.getJobIDbyJobNo(txtJobNumber.Text));
        }
        else
        {
            JQ.showStatusMsg(this, "2", "Job Number Does not Exist in the records");
            txtJobNumber.Text = "";
        }
       
    }

    protected void txtReferenceNo_TextChanged(object sender, EventArgs e)
    {
        int CountRefNo = BALInvoice.CheckInvoiceReferenceNo(txtReferenceNo.Text, Convert.ToInt32(Request.QueryString["Id"]));
        if (CountRefNo > 0)
        {
            JQ.showStatusMsg(this, "2", "Reference Number Already Exist in the record");
            txtReferenceNo.Text = "";
        }
    }
}
