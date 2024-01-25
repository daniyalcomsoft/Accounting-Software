using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using SW.SW_Common;
using System.Data;

public partial class PayPayment_Views : System.Web.UI.Page
{
    PayPayment_BAL bal = new PayPayment_BAL();
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "PayPayment_Views.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "PayPayment_Views.aspx" && view == true)
                {
                    SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
                    int FinYearID = SBO.FinYearID;
                    PM.BindDataGrid(GridPurchasesPaymentView, bal.getPayPaymentByID(0, FinYearID));
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
        int i = SBO.FinYearID;
        if (SBO.Can_Update == true)
            Response.Redirect("PayPayment.aspx?Id=" + e.CommandArgument.ToString());
        else
            JQ.showStatusMsg(this, "3", "User not Allowed to Update Record");
    }
    protected void lbtnView_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_View == true)
        {
            int view = 1;
            Response.Redirect("PayPayment.aspx?Id=" + e.CommandArgument.ToString() + "&view=" + view);
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
        dt = bal.GetDetailPayPayment(Convert.ToInt32(lblGroupID.Text));
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bal.UpdateBalance_ByInvoiceID(SCGL_Common.Convert_ToInt(dt.Rows[i]["pInvoiceID"].ToString()), Convert.ToInt32(lblGroupID.Text));
            }
        }
        SqlConnection con = new SqlConnection(SW.SW_Common.SCGL_Common.ConnectionString);
        con.Open();
        using (SqlTransaction trans = con.BeginTransaction())
        {
            try
            {
                if (bal.Delete_PayPaymentDetail(Convert.ToInt32(lblGroupID.Text), trans))
                {
                    bal.Delete_PayPayment(Convert.ToInt32(lblGroupID.Text), trans);
                    bal.DeletePayPaymentTransaction(Convert.ToInt32(lblGroupID.Text), trans);
                    lblDeleteMsg.Text = "Record successfully deleted";
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
        if (txtVendorName.Text != "")
        {
            if (ddlSearch.Text == "Vendor Name")
            {
                PM.BindDataGrid(GridPurchasesPaymentView, bal.SearchPayPaymentByVendorName(txtVendorName.Text, FinYearID));
            }
        }
        //else if (txtInvoiceID.Text != "")
        //{
        //    if (ddlSearch.Text == "Invoice ID")
        //    {
        //        PM.BindDataGrid(GridPurchasesPaymentView, bal.SearchPayPaymentByInvoiceID(SCGL_Common.Convert_ToInt(txtInvoiceID.Text), FinYearID));
        //    }
        //}
        else if (txtPayPayment_ID.Text != "")
        {
            if (ddlSearch.Text == "Pay Payment ID")
            {
                PM.BindDataGrid(GridPurchasesPaymentView, bal.SearchPayPaymentByPaymentID(SCGL_Common.Convert_ToInt(txtPayPayment_ID.Text), FinYearID));
            }
        }

        else
        {
            PM.BindDataGrid(GridPurchasesPaymentView, bal.getPayPaymentByID(0, FinYearID));
        }
        PM.BindDataGrid(GridPurchasesPaymentView, bal.getPayPaymentByID(0,FinYearID));
        lbtnYes.Visible = false;
        lbtnNo.Text = "Ok";
    }
    protected void btnCreatePurchasesPayment_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Insert == true)
        {
            Response.Redirect("PayPayment.aspx");
        }
        else
            JQ.showStatusMsg(this, "3", "User not Allowed to Insert Purchase Invoice Record");
    }
    protected void GridPurchasesPaymentView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        if (txtVendorName.Text != "")
        {
            if (ddlSearch.Text == "Vendor Name")
            {
                PM.BindDataGrid(GridPurchasesPaymentView, bal.SearchPayPaymentByVendorName(txtVendorName.Text,FinYearID));
                GridPurchasesPaymentView.PageIndex = e.NewPageIndex;
                GridPurchasesPaymentView.DataBind();
            }
        }
        //else if (txtInvoiceID.Text != "")
        //{
        //    if (ddlSearch.Text == "Invoice ID")
        //    {
        //        PM.BindDataGrid(GridPurchasesPaymentView, bal.SearchPayPaymentByInvoiceID(SCGL_Common.Convert_ToInt(txtInvoiceID.Text),FinYearID));
        //        GridPurchasesPaymentView.PageIndex = e.NewPageIndex;
        //        GridPurchasesPaymentView.DataBind();
        //    }
        //}
        else if (txtPayPayment_ID.Text != "")
        {
            if (ddlSearch.Text == "Pay Payment ID")
            {
                PM.BindDataGrid(GridPurchasesPaymentView, bal.SearchPayPaymentByPaymentID(SCGL_Common.Convert_ToInt(txtPayPayment_ID.Text), FinYearID));
                GridPurchasesPaymentView.PageIndex = e.NewPageIndex;
                GridPurchasesPaymentView.DataBind();
            }
        }

        else
        {
            GridPurchasesPaymentView.DataSource = bal.getPayPaymentByID(0,FinYearID);
            GridPurchasesPaymentView.PageIndex = e.NewPageIndex;
            GridPurchasesPaymentView.DataBind();
        }

        

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        DataTable dt = new DataTable();
        if (txtVendorName.Text != "")
        {
            if (ddlSearch.Text == "Vendor Name")
            {
                PM.BindDataGrid(GridPurchasesPaymentView, bal.SearchPayPaymentByVendorName(txtVendorName.Text,FinYearID));
                txtInvoiceID.Text = "";
                txtPayPayment_ID.Text = "";
            }
        }
        //if (txtInvoiceID.Text != "")
        //{
        //    if (ddlSearch.Text == "Invoice ID")
        //    {
        //        PM.BindDataGrid(GridPurchasesPaymentView, bal.SearchPayPaymentByInvoiceID(SCGL_Common.Convert_ToInt(txtInvoiceID.Text),FinYearID));
        //        txtVendorName.Text = "";
        //        txtPayPayment_ID.Text = "";
        //    }
        //}
        if (txtPayPayment_ID.Text != "")
        {
            if (ddlSearch.Text == "Pay Payment ID")
            {
                PM.BindDataGrid(GridPurchasesPaymentView, bal.SearchPayPaymentByPaymentID(SCGL_Common.Convert_ToInt(txtPayPayment_ID.Text), FinYearID));
                txtInvoiceID.Text = "";
                txtVendorName.Text = "";
            }
        }
        SCGL_Common.ReloadJS(this, "setSearchElem();");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        txtVendorName.Text = "";
        txtInvoiceID.Text = "";
        txtPayPayment_ID.Text = "";

        PM.BindDataGrid(GridPurchasesPaymentView, bal.getPayPaymentByID(0,FinYearID));
    }
    protected void txtVendorName_TextChanged(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        if (txtVendorName.Text == "")
        {
            PM.BindDataGrid(GridPurchasesPaymentView, bal.getPayPaymentByID(0, FinYearID));
        }
    }
    protected void txtInvoiceID_TextChanged(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        if (txtInvoiceID.Text == "")
        {
            PM.BindDataGrid(GridPurchasesPaymentView, bal.getPayPaymentByID(0, FinYearID));
        }
    }
    protected void txtPayPayment_ID_TextChanged(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        if (txtPayPayment_ID.Text == "")
        {
            PM.BindDataGrid(GridPurchasesPaymentView, bal.getPayPaymentByID(0, FinYearID));
        }
    }
}