using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using SW.SW_Common;
using System.Data;

public partial class ReceivePayment_Views : System.Web.UI.Page
{
    ReceivePayment_BAL bal = new ReceivePayment_BAL();
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "ReceivePayment_Views.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "ReceivePayment_Views.aspx" && view == true)
                {
                    SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
                    int FinYearID = SBO.FinYearID;
                    PM.BindDataGrid(GridSalesReceiptView, bal.getReceivePaymentByID(0, FinYearID));
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
            
        }
    }

    protected void LbtnEdit_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Update == true)
            Response.Redirect("ReceivePayment.aspx?Id=" + e.CommandArgument.ToString());
        else
            JQ.showStatusMsg(this, "3", "User not Allowed to Update Record");
    }

    protected void lbtnView_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_View == true)
        {
            int view = 1;
            Response.Redirect("ReceivePayment.aspx?Id=" + e.CommandArgument.ToString() + "&view=" + view);
        }
        else
            JQ.showStatusMsg(this, "3", "User not Allowed to View Record");
    }

    protected void lbtnDelete_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Delete == true)
        {
            lblGroupID.Text = e.CommandArgument.ToString();
            lblDeleteMsg.Text = "Are you sure to want to delete !";
            lbtnYes.Visible = true;
            lbtnNo.Text = "No";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Confirmation", "showDialog('Confirmation');", true);
        }
        else
            JQ.showStatusMsg(this, "3", "User not Allowed to Delete Record");
    }

    protected void lbtnYes_Click(object sender, EventArgs e)
    {

        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        DataTable dt = new DataTable();
        dt = bal.GetDetailReceivePayment(Convert.ToInt32(lblGroupID.Text));
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bal.UpdateBalance_ByInvoiceID(SCGL_Common.Convert_ToInt(dt.Rows[i]["InvoiceID"].ToString()), Convert.ToInt32(lblGroupID.Text));
            }
        }
        SqlConnection con = new SqlConnection(SW.SW_Common.SCGL_Common.ConnectionString);
        con.Open();
        using (SqlTransaction trans = con.BeginTransaction())
        {
            try
            {
                if (bal.Delete_ReceivePaymentDetail(Convert.ToInt32(lblGroupID.Text), trans))
                {
                    bal.Delete_ReceivePayment(Convert.ToInt32(lblGroupID.Text), trans);
                    bal.DeleteReceivePaymentTransaction(Convert.ToInt32(lblGroupID.Text), trans);
                    lblDeleteMsg.Text = "Record successfully deleted";
                   // DataTable dt = new DataTable();
                    //dt = bal.GetDetailReceivePayment(Convert.ToInt32(lblGroupID.Text));
                    trans.Commit();
                }
                else
                {
                    lblDeleteMsg.Text = "Record could not be deleted.";
                    trans.Rollback();
                }
            }
            catch (Exception ex)
            {
                lblDeleteMsg.Text = ex.Message;
                trans.Rollback();
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }
        if (txtCustomerName.Text != "")
        {
            if (ddlSearch.Text == "Customer Name")
            {
                PM.BindDataGrid(GridSalesReceiptView, bal.SearchReceivePaymentByCustomerName(txtCustomerName.Text, FinYearID));
            }
        }
        //else if (txtInvoiceID.Text != "")
        //{
        //    if (ddlSearch.Text == "Invoice ID")
        //    {
        //        PM.BindDataGrid(GridSalesReceiptView, bal.SearchReceivePaymentByInvoiceID(SCGL_Common.Convert_ToInt(txtInvoiceID.Text), FinYearID));
        //    }
        //}
        else if (txtPaymentID.Text != "")
        {
            if (ddlSearch.Text == "Receive Payment ID")
            {
                PM.BindDataGrid(GridSalesReceiptView, bal.SearchReceivePaymentByPaymentID(SCGL_Common.Convert_ToInt(txtPaymentID.Text), FinYearID));
            }
        }
        else
        {
            PM.BindDataGrid(GridSalesReceiptView, bal.getReceivePaymentByID(0, FinYearID));
        }

        lbtnYes.Visible = false;
        lbtnNo.Text = "Ok";
    }
    protected void btnCreateSalesReceipt_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Insert == true)
            Response.Redirect("ReceivePayment.aspx");
        else
            JQ.showStatusMsg(this, "3", "User not Allowed to Insert Purchase Invoice Record");
    }
    protected void GridSalesReceiptView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        if (txtCustomerName.Text != "")
        {
            if (ddlSearch.Text == "Customer Name")
            {
                PM.BindDataGrid(GridSalesReceiptView, bal.SearchReceivePaymentByCustomerName(txtCustomerName.Text, FinYearID));
                GridSalesReceiptView.PageIndex = e.NewPageIndex;
                GridSalesReceiptView.DataBind();
            }
        }
        //else if (txtInvoiceID.Text != "")
        //{
        //    if (ddlSearch.Text == "Invoice ID")
        //    {
        //        PM.BindDataGrid(GridSalesReceiptView, bal.SearchReceivePaymentByInvoiceID(SCGL_Common.Convert_ToInt(txtInvoiceID.Text), FinYearID));
        //        GridSalesReceiptView.PageIndex = e.NewPageIndex;
        //        GridSalesReceiptView.DataBind();
        //    }
        //}
        else if (txtPaymentID.Text != "")
        {
            if (ddlSearch.Text == "Receive Payment ID")
            {
                PM.BindDataGrid(GridSalesReceiptView, bal.SearchReceivePaymentByPaymentID(SCGL_Common.Convert_ToInt(txtPaymentID.Text), FinYearID));
                GridSalesReceiptView.PageIndex = e.NewPageIndex;
                GridSalesReceiptView.DataBind();
            }
        }
        else
        {
            GridSalesReceiptView.DataSource = bal.getReceivePaymentByID(0,FinYearID);
            GridSalesReceiptView.PageIndex = e.NewPageIndex;
            GridSalesReceiptView.DataBind();
        }

        

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        DataTable dt = new DataTable();
        if (txtCustomerName.Text != "")
        {
            if (ddlSearch.Text == "Customer Name")
            {
                PM.BindDataGrid(GridSalesReceiptView, bal.SearchReceivePaymentByCustomerName(txtCustomerName.Text,FinYearID));
                txtInvoiceID.Text = "";
                txtPaymentID.Text = "";
            }
        }
        //if (txtInvoiceID.Text != "")
        //{
        //    if (ddlSearch.Text == "Invoice ID")
        //    {
        //        PM.BindDataGrid(GridSalesReceiptView, bal.SearchReceivePaymentByInvoiceID(SCGL_Common.Convert_ToInt(txtInvoiceID.Text), FinYearID));
        //        txtCustomerName.Text = "";
        //        txtPaymentID.Text = "";
        //    }
        //}
        if (txtPaymentID.Text != "")
        {
            if (ddlSearch.Text == "Receive Payment ID")
            {
                PM.BindDataGrid(GridSalesReceiptView, bal.SearchReceivePaymentByPaymentID(SCGL_Common.Convert_ToInt(txtPaymentID.Text), FinYearID));
                txtInvoiceID.Text = "";
                txtCustomerName.Text = "";
            }
        }
        SCGL_Common.ReloadJS(this, "setSearchElem();");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        txtCustomerName.Text = "";
        txtInvoiceID.Text = "";
        txtPaymentID.Text = "";
        
        PM.BindDataGrid(GridSalesReceiptView, bal.getReceivePaymentByID(0,FinYearID));
    }
    protected void txtCustomerName_TextChanged(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        if (txtCustomerName.Text == "")
        {
            PM.BindDataGrid(GridSalesReceiptView, bal.getReceivePaymentByID(0, FinYearID));
        }
    }
    protected void txtInvoiceID_TextChanged(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        if (txtInvoiceID.Text == "")
        {
            PM.BindDataGrid(GridSalesReceiptView, bal.getReceivePaymentByID(0, FinYearID));
        }
    }
    protected void txtPaymentID_TextChanged(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        if (txtPaymentID.Text == "")
        {
            PM.BindDataGrid(GridSalesReceiptView, bal.getReceivePaymentByID(0, FinYearID));
        }
    }
   
}
