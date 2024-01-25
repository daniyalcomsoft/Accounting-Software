using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SW.SW_Common;
using System.Data.SqlClient;

public partial class SalesTaxInvoice_View : System.Web.UI.Page
{
    Invoice_BAL bal = new Invoice_BAL();
    SalesTax_BAL BALSalesTax = new SalesTax_BAL();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionBO"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        //Bind_Customer(); .aspx

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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "SalesTaxInvoice_View.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "SalesTaxInvoice_View.aspx" && view == true)
                {
                    SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
                    int FinYearID = SBO.FinYearID;
                    PM.BindDataGrid(GridSalesTaxInvoiceView, BALSalesTax.getallSalesTaxInvoices());
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
            Response.Redirect("SalesTaxInvoice.aspx?Id=" + e.CommandArgument.ToString());
        else
            JQ.showStatusMsg(this, "3", "User not Allowed to Update Record");
    }

    protected void lbtnView_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_View == true)
        {
            int view = 1;
            Response.Redirect("SalesTaxInvoice.aspx?Id=" + e.CommandArgument.ToString() + "&view=" + view);
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
        SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString);
        con.Open();
        using (SqlTransaction trans = con.BeginTransaction())
        {
            try
            {
                if (BALSalesTax.DeleteSalesTaxInvoice(Convert.ToInt32(lblGroupID.Text), trans))
                {
                    //BALSalesTax.DeleteTransaction_SalesTaxInvoice(Convert.ToInt32(lblGroupID.Text), trans);
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
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;

        //if (txtInvoiceID.Text != "")
        //{
        //    if (ddlSearch.Text == "Invoice ID")
        //    {
        //        PM.BindDataGrid(GridSalesTaxInvoiceView, bal.getInvoiceByID(SCGL_Common.Convert_ToInt(txtInvoiceID.Text), FinYearID));

        //    }
        //}

        //if (txtCustomerName.Text != "")
        //{
        //    if (ddlSearch.Text == "Customer Name")
        //    {
        //        PM.BindDataGrid(GridSalesTaxInvoiceView, bal.getInvoiceByCustomer(txtCustomerName.Text, FinYearID));

        //    }
        //}
        //else
        //{
        //    PM.BindDataGrid(GridSalesTaxInvoiceView, BALSalesTax.getallJobSheets());
        //}

        PM.BindDataGrid(GridSalesTaxInvoiceView, BALSalesTax.getallSalesTaxInvoices());
        lbtnYes.Visible = false;
        lbtnNo.Text = "Ok";
    }
    protected void btnCreateSalesInvoice_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Insert == true)
            Response.Redirect("SalesTaxInvoice.aspx");
        else
            JQ.showStatusMsg(this, "3", "User not Allowed to Insert Sales Tax Invoice Record");
    }
    protected void GridSalesTaxInvoiceView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        //if (txtCustomerName.Text != "")
        //{
        //    if (ddlSearch.Text == "Customer Name")
        //    {
        //        GridSalesTaxInvoiceView.DataSource = bal.getInvoiceByCustomer(txtCustomerName.Text, FinYearID);
        //        GridSalesTaxInvoiceView.PageIndex = e.NewPageIndex;
        //        GridSalesTaxInvoiceView.DataBind();
        //    }
        //}
        //else if (txtInvoiceID.Text != "")
        //{
        //    if (ddlSearch.Text == "Invoice ID")
        //    {
        //        GridSalesTaxInvoiceView.DataSource = bal.getInvoiceByID(SCGL_Common.Convert_ToInt(txtInvoiceID.Text), FinYearID);
        //        GridSalesTaxInvoiceView.PageIndex = e.NewPageIndex;
        //        GridSalesTaxInvoiceView.DataBind();
        //    }
        //}
        //else
        //{
        //    GridSalesTaxInvoiceView.DataSource = bal.getallInvoice(0, FinYearID);
        //    GridSalesTaxInvoiceView.PageIndex = e.NewPageIndex;
        //    GridSalesTaxInvoiceView.DataBind();
        //}

        GridSalesTaxInvoiceView.DataSource = BALSalesTax.getallSalesTaxInvoices();
        GridSalesTaxInvoiceView.PageIndex = e.NewPageIndex;
        GridSalesTaxInvoiceView.DataBind();

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        //int FinYearID = SBO.FinYearID;
        //DataTable dt = new DataTable();
        //if (txtInvoiceID.Text != "")
        //{
        //    if (ddlSearch.Text == "Invoice ID")
        //    {
        //        PM.BindDataGrid(GridSalesTaxInvoiceView, bal.getInvoiceByID(SCGL_Common.Convert_ToInt(txtInvoiceID.Text), FinYearID));
        //        txtCustomerName.Text = "";
        //    }
        //}

        //if (txtCustomerName.Text != "")
        //{
        //    if (ddlSearch.Text == "Customer Name")
        //    {
        //        PM.BindDataGrid(GridSalesTaxInvoiceView, bal.getInvoiceByCustomer(txtCustomerName.Text, FinYearID));
        //        txtInvoiceID.Text = "";
        //    }
        //}


        //SCGL_Common.ReloadJS(this, "setSearchElem();");


    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        //SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        //int FinYearID = SBO.FinYearID;
        //txtCustomerName.Text = "";
        //txtInvoiceID.Text = "";
        //ddlSearch.SelectedValue = "Invoice ID";
        //PM.BindDataGrid(GridSalesTaxInvoiceView, bal.getallInvoice(0, FinYearID));
    }

    protected void txtInvoiceID_TextChanged(object sender, EventArgs e)
    {
        //if (txtInvoiceID.Text == "")
        //{
        //    SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        //    int FinYearID = SBO.FinYearID;
        //    PM.BindDataGrid(GridSalesTaxInvoiceView, bal.getallInvoice(0, FinYearID));
        //}
    }
}
