using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SW.SW_Common;
using System.Data;
using System.Data.SqlClient;

public partial class PayPayment : System.Web.UI.Page
{
    PayPayment_BAL BAl = new PayPayment_BAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionBO"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "PayPayment.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "PayPayment.aspx" && view == true)
                {
                    GetMaxPayPaymentId();
                    Bind_Vendor();
                    Bind_DepositTo();
                    Bind_Grid(Convert.ToInt16(ddlVendor.SelectedValue));
                    //GV_PayPayment.Visible = false;
                    if (Request.QueryString["Id"] != null)
                    {
                        BindControl(Convert.ToInt32(Request.QueryString["Id"]));
                        ddlVendor.Enabled = false;
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
          
            //SetInitialRow();
        }
        Reload_JS();
    }

    public void BindControl(int Id)
    {
        if (Request.QueryString["view"] != null)
        {
            btnSave.Visible = false;
        }
        PayPayment_BAL Bal = new PayPayment_BAL();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        DataTable dtPayPayment = Bal.getPayPaymentByID(Id,FinYearID);
        if (dtPayPayment.Rows.Count > 0)
        {
            ddlVendor.SelectedValue = dtPayPayment.Rows[0]["VendorID"].ToString();
            txtPaymentDate.Text = SCGL_Common.CheckDateTime(dtPayPayment.Rows[0]["PaymentDate"]).ToShortDateString();
            txtReferenceNo.Text = dtPayPayment.Rows[0]["ReferenceNo"].ToString();
            ddlDepositTo.SelectedValue = dtPayPayment.Rows[0]["SubsidaryID"].ToString();
            txtPrintMessage.Text = dtPayPayment.Rows[0]["Memo"].ToString();
            txtTotalAmount.Text = dtPayPayment.Rows[0]["Total"].ToString();
           
        }

        DataTable dtPayPaymentDetail = Bal.getPayPaymentDetailByPaymentID(Id);
        if (dtPayPaymentDetail.Rows.Count > 0)
        {
            ViewState["CurrentTable"] = dtPayPaymentDetail;
            SCGL_Common.Bind_GridView(GV_PayPayment, dtPayPaymentDetail);
            btnSave.Text = "Update";
        }
    }

    public void Reload_JS()
    {
        SCGL_Common.ReloadJS(this, "MyDate();");
        SCGL_Common.ReloadJS(this, "vale();");
        SCGL_Common.ReloadJS(this, "setInvoiceBalance();");
        SCGL_Common.ReloadJS(this, " TotalSelectedInvoices();");
        SCGL_Common.ReloadJS(this, " GrossTotalPaid();");
    }

    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("pInvoiceID", typeof(string)));
        dt.Columns.Add(new DataColumn("DueDate", typeof(string)));
        dt.Columns.Add(new DataColumn("Total", typeof(string)));
        dt.Columns.Add(new DataColumn("txtAmount", typeof(string)));
        dt.Columns.Add(new DataColumn("Amount", typeof(string)));
        dt.Columns.Add(new DataColumn("Amt", typeof(string)));
        for (int i = 0; i < 1; i++)
        {
            dr = dt.NewRow();
            dt.Rows.Add(dr);
        }
        ViewState["CurrentTable"] = dt;
        GV_PayPayment.DataSource = dt;
        GV_PayPayment.DataBind();
    }

    
    public void Bind_Vendor()
    {
        SCGL_Common.Bind_DropDown(ddlVendor, "vt_SCGL_BindVendor", "VendorName", "ID");
    }
    public void Bind_DepositTo()
    {
        SCGL_Common.Bind_DropDown(ddlDepositTo, "vt_SCGL_GetDepositAcount", "Title", "SubsidaryID");
    }

    protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Grid(SCGL_Common.Convert_ToInt(ddlVendor.SelectedItem.Value));
        GV_PayPayment.Visible = true;
    }

    public void Bind_Grid(int ID)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        PurchaseInvoice_BAL Invoice = new PurchaseInvoice_BAL();
        DataTable dt = Invoice.getInvoiceByVendorID(Convert.ToInt16(ddlVendor.SelectedValue),SBO.FinYearID);
        //GV_PayPayment.DataSource = dt;
        //GV_PayPayment.DataBind();
        SCGL_Common.Bind_GridView(GV_PayPayment, dt);
        SCGL_Common.ReloadJS(this, "setInvoiceBalance();TotalSelectedInvoices();vale();GrossTotalPaid();");
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
                        lblSuccessMsg.InnerHtml = "Payment Paid Successfully";
                        Response.Redirect("PayPayment_Views.aspx");
                    }
                    else
                    {
                        lblSuccessMsg.InnerHtml = "Payment Updated Successfully";
                        Response.Redirect("PayPayment_Views.aspx");
                    }
                    SCGL_Common.Success_Message(this.Page, "PayPayment.aspx");
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
        TextBox txtAmount = (TextBox)GV_PayPayment.Rows[0].FindControl("txtAmount");

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
                BAl.PayPayment_ID = -1;
            }
            else
            {
                BAl.PayPayment_ID = SCGL_Common.Convert_ToInt(Request.QueryString["Id"]);
            }
            BAl.VendorID = SCGL_Common.Convert_ToInt(ddlVendor.SelectedValue);
            BAl.PaymentDate = SCGL_Common.CheckDateTime(txtPaymentDate.Text);
            BAl.ReferenceNo = txtReferenceNo.Text;
            BAl.Memo = txtPrintMessage.Text;
            BAl.LoginID = SBO.UserID;
            BAl.SubsidaryID = SCGL_Common.Convert_ToInt(ddlDepositTo.SelectedValue);
            BAl.FinYearID = SBO.FinYearID;
            BAl.Total = SCGL_Common.Convert_ToDecimal(txtTotalAmount.Text);
            int PayPaymentID = BAl.CreateModifyPayPayment(BAl, trans);
            if (PayPaymentID>0)
            {
                lblPayPayment_ID.Text = PayPaymentID.ToString();
                BAl.PayPayment_ID = SCGL_Common.Convert_ToInt(lblPayPayment_ID.Text);
                if (btnSave.Text == "Update")
                {
                    //BAl.PayPayment_ID = SCGL_Common.Convert_ToInt(Request.QueryString["Id"]);
                    BAl.Delete_PayPaymentDetail(BAl.PayPayment_ID, trans);
                    BAl.Update_PayPaymentTransaction(BAl.PayPayment_ID, trans);
                }
                int Counter = 0;
                DataTable dt = Record_for_Insert();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BAl.pInvoiceID = SCGL_Common.Convert_ToInt(dt.Rows[i]["pInvoiceID"].ToString());
                    BAl.Amount = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["txtAmount"].ToString());
                    
                    if (BAl.CreateModifyPayPaymentDetail(BAl, trans))
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
            dtInsert.Rows[p]["pInvoiceID"] = Product_Table.Rows[p]["pInvoiceID"].ToString();
            dtInsert.Rows[p]["DueDate"] = Product_Table.Rows[p]["DueDate"].ToString();
            dtInsert.Rows[p]["Total"] = Product_Table.Rows[p]["Total"].ToString();
            dtInsert.Rows[p]["txtAmount"] = Product_Table.Rows[p]["txtAmount"].ToString();
        }
        return dtInsert;
    }

    public DataTable Product_DataSource()
    {
        DataTable dtProduct = Product_Table;
        dtProduct.Rows.Clear();
        for (int i = 0; i < GV_PayPayment.Rows.Count; i++)
        {
            if (GV_PayPayment.Rows[i].Visible)
            {
                LinkButton txtInvoiceID = (LinkButton)GV_PayPayment.Rows[i].Cells[1].FindControl("lbtnInvoiceID");
                Label txtDueDate = (Label)GV_PayPayment.Rows[i].Cells[2].FindControl("lblDueDate");
                Label txtTotal = (Label)GV_PayPayment.Rows[i].Cells[3].FindControl("lblOrigAmt");
                TextBox txtAmount = (TextBox)GV_PayPayment.Rows[i].Cells[4].FindControl("txtAmount");
                dtProduct.Rows.Add(dtProduct.NewRow());
                if (txtAmount.Text != "")
                {
                    dtProduct.Rows[i]["pInvoiceID"] = txtInvoiceID.Text;
                    dtProduct.Rows[i]["DueDate"] = txtDueDate.Text;
                    dtProduct.Rows[i]["Total"] = txtTotal.Text;
                    dtProduct.Rows[i]["txtAmount"] = txtAmount.Text;
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
                dt.Columns.Add(new DataColumn("pInvoiceID", typeof(string)));
                dt.Columns.Add(new DataColumn("DueDate", typeof(string)));
                dt.Columns.Add(new DataColumn("Total", typeof(string)));
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

    public void GetMaxPayPaymentId()
    {
        DataTable dt = new DataTable();
        dt = BAl.GetMaxPayPaymentId();
        foreach (DataRow row in dt.Rows)
        {
            int staticID = 1;

            int DynamicID = Convert.ToInt32(row["PayPayment_ID"]);
            int totalDraftID = staticID + DynamicID;

            lblPayPayment_ID.Text = Convert.ToInt32(totalDraftID).ToString();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PayPayment_Views.aspx");
    }
}
