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

public partial class ReceivePayment : System.Web.UI.Page
{
    ReceivePayment_BAL BAl = new ReceivePayment_BAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionBO"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        Reload_JS();
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "ReceivePayment.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "ReceivePayment.aspx" && view == true)
                {
                    GetMaxPaymentId();
                    Bind_Customer();
                    Bind_DepositTo();
                    Bind_Grid(Convert.ToInt16(ddlCustomer.SelectedValue));
                    if (Request.QueryString["Id"] != null)
                    {
                        BindControl(Convert.ToInt32(Request.QueryString["Id"]));
                        ddlCustomer.Enabled = false;
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

    
    public void BindControl(int Id)
    {
        if (Request.QueryString["view"] != null)
        {
            btnSave.Visible = false;
        }
        ReceivePayment_BAL Bal = new ReceivePayment_BAL();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        DataTable dtReceivePayment = Bal.getReceivePaymentByID(Id,FinYearID);
        if (dtReceivePayment.Rows.Count > 0)
        {
            ddlCustomer.SelectedValue = dtReceivePayment.Rows[0]["CustomerID"].ToString();
            txtPaymentDate.Text = SCGL_Common.CheckDateTime(dtReceivePayment.Rows[0]["PaymentDate"]).ToShortDateString();
            //ddlPaymentMethod.SelectedValue = dt.Rows[0][""].ToString();
            ddlCurrency.SelectedValue = dtReceivePayment.Rows[0]["Currency"].ToString();
            txtConversionRate.Text = dtReceivePayment.Rows[0]["ConversionRate"].ToString();
            txtReferenceNo.Text = dtReceivePayment.Rows[0]["ReferenceNo"].ToString();
            ddlDepositTo.SelectedValue = dtReceivePayment.Rows[0]["SubsidaryID"].ToString();
            txtPrintMessage.Text = dtReceivePayment.Rows[0]["Memo"].ToString();
            txtTotalAmount.Text = dtReceivePayment.Rows[0]["PKRTotal"].ToString();
            btnSave.Text = "Update";
        }
        //DataTable dtReceivePaymentDetail2;
        DataTable dtReceivePaymentDetail;
        if (btnSave.Text == "Update")
        {
            dtReceivePaymentDetail = Bal.getReceivePaymentDetailByPaymentID_update(Id);
            //decimal Balance =SCGL_Common.Convert_ToDecimal(dtReceivePaymentDetail2.Rows[0]["Amount"].ToString());
            //decimal BalancePKRAmount = SCGL_Common.Convert_ToDecimal(dtReceivePaymentDetail2.Rows[0]["PKRAmount"].ToString());
            //decimal Tot = Balance + BalancePKRAmount;

            //dtReceivePaymentDetail2.

            //dtReceivePaymentDetail = dtReceivePaymentDetail2;
        }
        else 
        {
            dtReceivePaymentDetail = Bal.getReceivePaymentDetailByPaymentID(Id);
        }
            
        if (dtReceivePaymentDetail.Rows.Count > 0)
        {
            
           
            ViewState["CurrentTable"] = dtReceivePaymentDetail;
            SCGL_Common.Bind_GridView(GV_ReceivePayment, dtReceivePaymentDetail);
        
            btnSave.Text = "Update";
        }
    }

    public void Reload_JS()
    {
        SCGL_Common.ReloadJS(this, "MyDate();");
        SCGL_Common.ReloadJS(this, "vale();");
        SCGL_Common.ReloadJS(this, "setInvoiceBalance();");
        SCGL_Common.ReloadJS(this, " TotalSelectedInvoices();");
        SCGL_Common.ReloadJS(this, " GrossTotalReceived();");
        SCGL_Common.ReloadJS(this, " GrossTotal();");
        SCGL_Common.ReloadJS(this, " GrossTotalPKR();");
        SCGL_Common.ReloadJS(this, " PKRConversion();");
        SCGL_Common.ReloadJS(this, " GrandPKRTotals();");
       // SCGL_Common.ReloadJS(this, " GrossTotalDeduction();"); 
    }

    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("InvoiceID", typeof(string)));
        dt.Columns.Add(new DataColumn("DueDate", typeof(string)));
        dt.Columns.Add(new DataColumn("Total", typeof(string)));
        dt.Columns.Add(new DataColumn("txtAmount", typeof(string)));
        dt.Columns.Add(new DataColumn("Amount", typeof(string)));
        dt.Columns.Add(new DataColumn("Amt", typeof(string)));
        dt.Columns.Add(new DataColumn("txtPKRAmount", typeof(string)));
        dt.Columns.Add(new DataColumn("PKRAmt", typeof(string)));
        for (int i = 0; i < 1; i++)
        {
            dr = dt.NewRow();
            dt.Rows.Add(dr);
        }


        //Store the DataTable in ViewState for future reference
        ViewState["CurrentTable"] = dt;



        GV_ReceivePayment.DataSource = dt;
        GV_ReceivePayment.DataBind();
    }

    public void Bind_Customer()
    {
        SCGL_Common.Bind_DropDown(ddlCustomer, "vt_SCGL_BindCustomer", "CustomerName", "ID");
    }
   
    public void Bind_DepositTo()
    {
        SCGL_Common.Bind_DropDown(ddlDepositTo, "vt_SCGL_GetDepositAcount", "Title", "SubsidaryID");
    }
    //vt_SCGL_BindAccounts
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Grid(SCGL_Common.Convert_ToInt(ddlCustomer.SelectedItem.Value));
        GV_ReceivePayment.Visible = true;
        
    }

    public void Bind_Grid(int ID)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        Invoice_BAL Invoice = new Invoice_BAL();
        DataTable dt = Invoice.getInvoiceByCustomerID(Convert.ToInt16(ddlCustomer.SelectedValue),SBO.FinYearID);
        
        SCGL_Common.Bind_GridView(GV_ReceivePayment, dt);

       // SCGL_Common.ReloadJS(this, "setInvoiceBalance();TotalSelectedInvoices();vale()");
        SCGL_Common.ReloadJS(this, " setInvoiceBalance();");
        SCGL_Common.ReloadJS(this, " TotalSelectedInvoices();");
        SCGL_Common.ReloadJS(this, " vale();");
        SCGL_Common.ReloadJS(this, " GrossTotalReceived();"); 
            
        
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            if (SBO.Can_Insert == true)
            {
                    if (Insert_Payment())
                    {
                        if (btnSave.Text == "Save")
                        {
                            btnSave.Visible = false;
                            lblSuccessMsg.InnerHtml = "Payment Receive Successfully";
                            Response.Redirect("ReceivePayment_Views.aspx");
                        }
                        else
                        {
                            lblSuccessMsg.InnerHtml = "Payment Updated Successfully";
                            Response.Redirect("ReceivePayment_Views.aspx");
                        }
                        SCGL_Common.Success_Message(this.Page, "ReceivePayment.aspx");
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
  

    public bool Check_Validation()
    {
        bool IsValid = true;
        TextBox txtAmount = (TextBox)GV_ReceivePayment.Rows[1].FindControl("txtAmount");

        if (txtAmount.Text == "")
        {

            SCGL_Common.Error_Amount(this);

            return false;
        }

        return IsValid;
    }
    
    public bool Insert_Payment()
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
                BAl.ReceivePayment_ID = -1;
            }
            else
            {
                BAl.ReceivePayment_ID = SCGL_Common.Convert_ToInt(Request.QueryString["Id"]);
            }
            BAl.CustomerID = SCGL_Common.Convert_ToInt(ddlCustomer.SelectedValue);
            BAl.PaymentDate = SCGL_Common.CheckDateTime(txtPaymentDate.Text);
            BAl.ReferenceNo = txtReferenceNo.Text;
            BAl.Memo = txtPrintMessage.Text;
            BAl.LoginID = SBO.UserID;
            BAl.SubsidaryID = SCGL_Common.Convert_ToInt(ddlDepositTo.SelectedValue);
            BAl.FinYearID = SBO.FinYearID;
            BAl.Currency = SCGL_Common.Convert_ToInt(ddlCurrency.SelectedValue);
            BAl.ConversionRate = SCGL_Common.Convert_ToDecimal(txtConversionRate.Text);
            BAl.PKRTotal = SCGL_Common.Convert_ToDecimal(txtTotalAmount.Text);
            BAl.Total = BAl.PKRTotal / BAl.ConversionRate;
            int ReceivePaymentID = BAl.CreateModifyReceivePayment(BAl, trans);
            if (ReceivePaymentID>0)
            {
                lblPaymentID.Text = ReceivePaymentID.ToString();
                BAl.ReceivePayment_ID = SCGL_Common.Convert_ToInt(lblPaymentID.Text);
                if (btnSave.Text == "Update")
                {
                    //BAl.ReceivePayment_ID = SCGL_Common.Convert_ToInt(Request.QueryString["Id"]);
                    BAl.Delete_ReceivePaymentDetail(BAl.ReceivePayment_ID, trans);
                    BAl.Update_ReceivePaymentTransaction(BAl.ReceivePayment_ID, trans);
                }
                int Counter = 0;
                DataTable dt = Record_for_Insert();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BAl.InvoiceID = SCGL_Common.Convert_ToInt(dt.Rows[i]["InvoiceID"].ToString());
                    BAl.Amount = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtAmount"].ToString());
                    BAl.Currency = SCGL_Common.Convert_ToInt(ddlCurrency.SelectedValue);
                    BAl.ConversionRate = SCGL_Common.Convert_ToDecimal(txtConversionRate.Text);
                    //BAl.PKRAmount = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtPKRAmount"].ToString());
                    BAl.PKRAmount = BAl.Amount * BAl.ConversionRate;
                    if (BAl.CreateModifyReceivePaymentDetail(BAl, trans))
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
            dtInsert.Rows[p]["InvoiceID"] = Product_Table.Rows[p]["InvoiceID"].ToString();
            dtInsert.Rows[p]["DueDate"] = Product_Table.Rows[p]["DueDate"].ToString();
            dtInsert.Rows[p]["Total"] = Product_Table.Rows[p]["Total"].ToString();
        //    dtInsert.Rows[p]["Amt"] = Product_Table.Rows[p]["Amt"].ToString();
            dtInsert.Rows[p]["txtAmount"] = Product_Table.Rows[p]["txtAmount"].ToString();
            dtInsert.Rows[p]["txtPKRAmount"] = Product_Table.Rows[p]["txtPKRAmount"].ToString();
        }

        return dtInsert;
    }


    public DataTable Product_DataSource()
    {
        DataTable dtProduct = Product_Table; dtProduct.Rows.Clear();
        for (int i = 0; i < GV_ReceivePayment.Rows.Count; i++)
        {
            if (GV_ReceivePayment.Rows[i].Visible)
            {

                LinkButton txtInvoiceID = (LinkButton)GV_ReceivePayment.Rows[i].Cells[1].FindControl("lbtnInvoiceID");
                Label txtDueDate = (Label)GV_ReceivePayment.Rows[i].Cells[2].FindControl("lblDueDate");
                Label txtTotal = (Label)GV_ReceivePayment.Rows[i].Cells[3].FindControl("lblOrigAmt");
                TextBox txtAmount = (TextBox)GV_ReceivePayment.Rows[i].Cells[4].FindControl("txtAmount");
                TextBox txtPKRAmount = (TextBox)GV_ReceivePayment.Rows[i].Cells[5].FindControl("txtPKRAmount");
                dtProduct.Rows.Add(dtProduct.NewRow());
                if (txtAmount.Text!="" )
                {

                    dtProduct.Rows[i]["InvoiceID"] = txtInvoiceID.Text;
                    dtProduct.Rows[i]["DueDate"] = txtDueDate.Text;
                    dtProduct.Rows[i]["Total"] = txtTotal.Text;
                    dtProduct.Rows[i]["txtAmount"] =txtAmount.Text;
                    dtProduct.Rows[i]["txtPKRAmount"] = txtPKRAmount.Text;
                    
                }
            }
            else
            {
                dtProduct.Rows.Add(dtProduct.NewRow());
            }
        }
        for (int pr = 0; pr < Product_Table.Rows.Count; pr++)
       {
            if (Product_Table.Rows[pr]["txtAmount"].ToString() == "")
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
                dt.Columns.Add(new DataColumn("InvoiceID", typeof(string)));
                dt.Columns.Add(new DataColumn("DueDate", typeof(string)));
                dt.Columns.Add(new DataColumn("Total", typeof(string)));
                dt.Columns.Add(new DataColumn("txtAmount", typeof(string)));
                dt.Columns.Add(new DataColumn("txtPKRAmount", typeof(string)));
                
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

    public void GetMaxPaymentId()
    {
        DataTable dt = new DataTable();
        dt = BAl.GetMaxPaymentId();
        foreach (DataRow row in dt.Rows)
        {
            int staticID = 1;

            int DynamicID = Convert.ToInt32(row["ReceivePayment_ID"]);
            int totalDraftID = staticID + DynamicID;

            lblPaymentID.Text = Convert.ToInt32(totalDraftID).ToString();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReceivePayment_Views.aspx");
    }
}
