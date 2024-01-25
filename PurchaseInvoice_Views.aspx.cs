using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using SW.SW_Common;
using System.Data;

public partial class PurchaseInvoice_Views : System.Web.UI.Page
{

    PurchaseInvoice_BAL_Temp bal = new PurchaseInvoice_BAL_Temp();
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "PurchaseInvoice_Views.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "PurchaseInvoice_Views.aspx" && view == true)
                {
                    SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
                    int FinYearID = SBO.FinYearID;
                    PM.BindDataGrid(GridPurchasesInvoiceView, bal.getallVendorInvoice(0, FinYearID));
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
            
        }
            //Reload_JS();       
        //{
        //    GridSalesInvoiceView.DataSource = bal.getInvoiceByID(0);
        //    GridSalesInvoiceView.DataBind();
        //}
    }

    //public void Reload_JS()
    //{

    //    SCGL_Common.ReloadJS(this, "ddlSearch();");
    //}

    protected void LbtnEdit_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Update == true)
            Response.Redirect("PurchaseInvoice_Temp.aspx?Id=" + e.CommandArgument.ToString());
        else
            JQ.showStatusMsg(this, "3", "User not Allowed to Update Record");
    }
    protected void lbtnView_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_View == true)
        {
            int view = 1;
            Response.Redirect("PurchaseInvoice_Temp.aspx?Id=" + e.CommandArgument.ToString() + "&view=" + view);
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
        SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString);        
        con.Open();
        using (SqlTransaction trans = con.BeginTransaction())
        {
            try
            {
                if (bal.Delete_InvoiceDetail(Convert.ToInt32(lblGroupID.Text), trans))
                {
                    bal.DeleteInvoice(Convert.ToInt32(lblGroupID.Text), trans);
                    bal.Delete_Transaction(Convert.ToInt32(lblGroupID.Text), trans);
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
                PM.BindDataGrid(GridPurchasesInvoiceView, bal.getInvoiceByVendor(txtVendorName.Text,FinYearID));
            }
        }
        else if (txtInvoiceID.Text != "")
        {
            if (ddlSearch.Text == "Invoice ID")
            {
                PM.BindDataGrid(GridPurchasesInvoiceView, bal.getInvoiceByID(SCGL_Common.Convert_ToInt(txtInvoiceID.Text),FinYearID));
            }
        }
        else
        {
            PM.BindDataGrid(GridPurchasesInvoiceView, bal.getallVendorInvoice(0, FinYearID));
        }
               
        lbtnYes.Visible = false;
        lbtnNo.Text = "Ok";
    }
    protected void btnCreatePurchasesInvoice_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Insert == true)
            Response.Redirect("PurchaseInvoice_Temp.aspx");
        else
            JQ.showStatusMsg(this, "3", "User not Allowed to Insert Purchase Invoice Record");
    }
    protected void GridPurchasesInvoiceView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        if (txtVendorName.Text != "")
        {
            if (ddlSearch.Text == "Vendor Name")
            {
                GridPurchasesInvoiceView.DataSource = bal.getInvoiceByVendor(txtVendorName.Text,FinYearID);
                GridPurchasesInvoiceView.PageIndex = e.NewPageIndex;
                GridPurchasesInvoiceView.DataBind();
            }
        }
        else if (txtInvoiceID.Text != "")
        {
            if (ddlSearch.Text == "Invoice ID")
            {
                GridPurchasesInvoiceView.DataSource = bal.getInvoiceByID(SCGL_Common.Convert_ToInt(txtInvoiceID.Text),FinYearID);
                GridPurchasesInvoiceView.PageIndex = e.NewPageIndex;
                GridPurchasesInvoiceView.DataBind();
            }
        }
        else
        {
            GridPurchasesInvoiceView.DataSource = bal.getInvoiceByID(0,FinYearID);
            GridPurchasesInvoiceView.PageIndex = e.NewPageIndex;
            GridPurchasesInvoiceView.DataBind();
        }
        
    }
   
    protected void txtInvoiceID_TextChanged(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        if (txtInvoiceID.Text == "")
        {
            PM.BindDataGrid(GridPurchasesInvoiceView, bal.getallVendorInvoice(0,FinYearID));
        }
    }

    protected void txtVendorName_TextChanged(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        if (txtVendorName.Text == "")
        {
            PM.BindDataGrid(GridPurchasesInvoiceView, bal.getallVendorInvoice(0,FinYearID));
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        DataTable dt = new DataTable();
        if (txtInvoiceID.Text != "")
        {
            if (ddlSearch.Text == "Invoice ID")
            {
                PM.BindDataGrid(GridPurchasesInvoiceView, bal.getInvoiceByID(SCGL_Common.Convert_ToInt(txtInvoiceID.Text),FinYearID));
                txtVendorName.Text="";
            }
        }

        if (txtVendorName.Text != "")
        {
            if (ddlSearch.Text == "Vendor Name")
            {
                PM.BindDataGrid(GridPurchasesInvoiceView, bal.getInvoiceByVendor(txtVendorName.Text,FinYearID));
                txtInvoiceID.Text = "";
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
        PM.BindDataGrid(GridPurchasesInvoiceView, bal.getallVendorInvoice(0,FinYearID));
    }
}
