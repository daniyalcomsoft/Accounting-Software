using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using SW.SW_Common;
using System.Data;

public partial class JobSheetForm_Views : System.Web.UI.Page
{
    Invoice_BAL bal = new Invoice_BAL();
    JobSheet_BAL jsbal = new JobSheet_BAL();

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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "JobSheetForm_Views.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "JobSheetForm_Views.aspx" && view == true)
                {
                    SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
                    int FinYearID = SBO.FinYearID;
                    PM.BindDataGrid(GridJobSheetView, jsbal.getallJobSheets());
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
            Response.Redirect("JobSheetForm.aspx?Id=" + e.CommandArgument.ToString());
        else
            JQ.showStatusMsg(this, "3", "User not Allowed to Update Record");
    }

    protected void lbtnView_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_View == true)
        {
            int view = 1;
            Response.Redirect("JobSheetForm.aspx?Id=" + e.CommandArgument.ToString() + "&view=" + view);
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
                if (jsbal.Delete_JobSheetDetail(Convert.ToInt32(lblGroupID.Text), trans))
                {

                    jsbal.DeleteJobSheet(Convert.ToInt32(lblGroupID.Text), trans);
                    jsbal.Delete_JobSheetTrans(Convert.ToInt32(lblGroupID.Text), trans);
                    //bal.DeleteTransaction_Invoice(Convert.ToInt32(lblGroupID.Text), trans);
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
        //        PM.BindDataGrid(GridJobSheetView, bal.getInvoiceByID(SCGL_Common.Convert_ToInt(txtInvoiceID.Text), FinYearID));

        //    }
        //}

        //if (txtCustomerName.Text != "")
        //{
        //    if (ddlSearch.Text == "Customer Name")
        //    {
        //        PM.BindDataGrid(GridJobSheetView, bal.getInvoiceByCustomer(txtCustomerName.Text, FinYearID));

        //    }
        //}
        //else
        //{
        //    PM.BindDataGrid(GridJobSheetView, jsbal.getallJobSheets());
        //}

        PM.BindDataGrid(GridJobSheetView, jsbal.getallJobSheets());
        lbtnYes.Visible = false;
        lbtnNo.Text = "Ok";
    }
    protected void btnCreateSalesInvoice_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Insert == true)
            Response.Redirect("JobSheetForm.aspx");
        else
            JQ.showStatusMsg(this, "3", "User not Allowed to Insert Sales Invoice Record");
    }
    protected void GridJobSheetView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        //if (txtCustomerName.Text != "")
        //{
        //    if (ddlSearch.Text == "Customer Name")
        //    {
        //        GridJobSheetView.DataSource = bal.getInvoiceByCustomer(txtCustomerName.Text, FinYearID);
        //        GridJobSheetView.PageIndex = e.NewPageIndex;
        //        GridJobSheetView.DataBind();
        //    }
        //}
        //else if (txtInvoiceID.Text != "")
        //{
        //    if (ddlSearch.Text == "Invoice ID")
        //    {
        //        GridJobSheetView.DataSource = bal.getInvoiceByID(SCGL_Common.Convert_ToInt(txtInvoiceID.Text), FinYearID);
        //        GridJobSheetView.PageIndex = e.NewPageIndex;
        //        GridJobSheetView.DataBind();
        //    }
        //}
        //else
        //{
        //    GridJobSheetView.DataSource = bal.getallInvoice(0, FinYearID);
        //    GridJobSheetView.PageIndex = e.NewPageIndex;
        //    GridJobSheetView.DataBind();
        //}

        GridJobSheetView.DataSource = jsbal.getallJobSheets();
        GridJobSheetView.PageIndex = e.NewPageIndex;
        GridJobSheetView.DataBind();

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
        //        PM.BindDataGrid(GridJobSheetView, bal.getInvoiceByID(SCGL_Common.Convert_ToInt(txtInvoiceID.Text), FinYearID));
        //        txtCustomerName.Text = "";
        //    }
        //}

        //if (txtCustomerName.Text != "")
        //{
        //    if (ddlSearch.Text == "Customer Name")
        //    {
        //        PM.BindDataGrid(GridJobSheetView, bal.getInvoiceByCustomer(txtCustomerName.Text, FinYearID));
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
        //PM.BindDataGrid(GridJobSheetView, bal.getallInvoice(0, FinYearID));
    }

    protected void txtInvoiceID_TextChanged(object sender, EventArgs e)
    {
        //if (txtInvoiceID.Text == "")
        //{
        //    SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        //    int FinYearID = SBO.FinYearID;
        //    PM.BindDataGrid(GridJobSheetView, bal.getallInvoice(0, FinYearID));
        //}
    }
}
