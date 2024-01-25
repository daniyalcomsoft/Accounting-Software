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
using SCGL.BAL;
using SW.SW_Common;

public partial class InvoiceStatusForm : System.Web.UI.Page
{
    //Invoice_BAL bal = new Invoice_BAL();
    InvoiceStatus_BAL Statusbal = new InvoiceStatus_BAL();

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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "InvoiceStatusForm.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "InvoiceStatusForm.aspx" && view == true)
                {
                    //SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
                    //int FinYearID = SBO.FinYearID;
                    //objInvoice.SelectParameters["FinYearID"].DefaultValue = FinYearID.ToString();
                    objInvoice.DataBind();
                    //BindControl();
                    //PM.BindDataGrid(GridSalesInvoiceStatusView, bal.getallInvoice(0, FinYearID));
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
        }
    }

    public void BindControl()
    {
        InvoiceStatus_BAL InvStatus = new InvoiceStatus_BAL();
        DataTable dt = InvStatus.getallInvoice();

        
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    DropDownList ddlStatus = (DropDownList)GridSalesInvoiceStatusView.Rows[i].Cells[7].FindControl("ddlStatus");



        //    ddlStatus.SelectedValue = dt.Rows[i]["Status"].ToString();

        //}

    }
    
    protected void GridSalesInvoiceStatusView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        filterdata();

        GridSalesInvoiceStatusView.PageIndex = e.NewPageIndex;

    }

    void filterdata()
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        DataTable dt = new DataTable();
        if (txtInvoiceID.Text != "")
        {
            if (ddlSearch.Text == "Invoice ID")
            {
                objInvoice.FilterExpression = string.Format("InvoiceID = {0}", SCGL_Common.Convert_ToInt(txtInvoiceID.Text));
                //(GridSalesInvoiceStatusView.DataSource as DataTable).DefaultView.RowFilter = string.Format("InvoiceID = {0}", SCGL_Common.Convert_ToInt(txtInvoiceID.Text));
                //PM.BindDataGrid(GridSalesInvoiceStatusView, bal.getInvoiceByID(SCGL_Common.Convert_ToInt(txtInvoiceID.Text),FinYearID));
                txtCustomerName.Text = "";
                txtJobNumber.Text = "";
            }
        }

        if (txtCustomerName.Text != "")
        {
            if (ddlSearch.Text == "Customer Name")
            {
                objInvoice.FilterExpression = string.Format("DisplayName Like '%{0}%'", txtCustomerName.Text);
                //PM.BindDataGrid(GridSalesInvoiceStatusView, bal.getInvoiceByCustomer(txtCustomerName.Text,FinYearID));
                txtInvoiceID.Text = "";
                txtJobNumber.Text = "";
            }
        }

        if (txtJobNumber.Text != "")
        {
            if (ddlSearch.Text == "Job Number")
            {
                objInvoice.FilterExpression = string.Format("JobNumber Like '%{0}%'", txtJobNumber.Text);
                //PM.BindDataGrid(GridSalesInvoiceStatusView, bal.getInvoiceByCustomer(txtCustomerName.Text,FinYearID));
                txtInvoiceID.Text = "";
                txtCustomerName.Text = "";
            }
        }

        SCGL_Common.ReloadJS(this, "setSearchElem();");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        filterdata();
    }

    //void searchInvoice()
    //{ 
    //GridSalesInvoiceStatusView.DataSource
    //}

    protected void btnClear_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        txtCustomerName.Text = "";
        txtInvoiceID.Text = "";
        ddlSearch.SelectedValue = "Invoice ID";
        objInvoice.FilterExpression = "";
        GridSalesInvoiceStatusView.DataBind();
        //PM.BindDataGrid(GridSalesInvoiceStatusView, bal.getallInvoice(0,FinYearID));
    }

    protected void txtInvoiceID_TextChanged(object sender, EventArgs e)
    {
        if (txtInvoiceID.Text == "")
        {
            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            int FinYearID = SBO.FinYearID;
            //PM.BindDataGrid(GridSalesInvoiceStatusView, bal.getallInvoice(0,FinYearID));
        }
    }

    protected void GridSalesInvoiceStatusView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridSalesInvoiceStatusView.EditIndex = e.NewEditIndex;
        //GridViewRow row = GridSalesInvoiceStatusView.Rows[e.NewEditIndex];
        //row.FindControl("LbtnEdit").Visible = false;
        //row.FindControl("btnCancel").Visible = true;
        //row.FindControl("btnUpdate").Visible = true;
        GridSalesInvoiceStatusView.DataBind();
    }

    string NewVal = null;
    protected void GridSalesInvoiceStatusView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        NewVal = ((TextBox)GridSalesInvoiceStatusView.Rows[e.RowIndex].FindControl("txtChequeNo")).Text;
        objInvoice.DataBind();
        GridSalesInvoiceStatusView.EditIndex = -1;
        GridSalesInvoiceStatusView.DataBind();

    }
    //protected void ObjInvoice_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    //{
    //    if (NewVal != null)
    //    {
    //        InvoiceStatus_BAL InvStatus = (InvoiceStatus_BAL)e.InputParameters[0];
    //        InvStatus.ChequeNo = NewVal;
    //    }
    //}
    protected void LbtnEdit_Command(object sender, CommandEventArgs e)
    {
        InvoiceStatus_BAL InvStatus = new InvoiceStatus_BAL();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Update == true)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GridSalesInvoiceStatusView.Rows[index];
            Label lblInvoiceID = (Label)GridSalesInvoiceStatusView.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblInvoiceID");
            TextBox txtChequeNo = (TextBox)GridSalesInvoiceStatusView.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txtChequeNo");
            DropDownList ddlStatus = (DropDownList)GridSalesInvoiceStatusView.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("ddlStatus");
            TextBox txtRecDate = (TextBox)GridSalesInvoiceStatusView.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txtRecDate");
            //int index = Convert.ToInt32(e.CommandArgument);
            //TextBox txtChequeNo = GridSalesInvoiceStatusView.Rows[index].FindControl("txtChequeNo") as TextBox;
            InvStatus.InvoiceID = SCGL_Common.Convert_ToInt(lblInvoiceID.Text);
            InvStatus.CheqNo = txtChequeNo.Text;
            InvStatus.Status = SCGL_Common.Convert_ToInt(ddlStatus.SelectedValue);
            InvStatus.RecDate = SCGL_Common.CheckDateTime(txtRecDate.Text);
            if (InvStatus.UpdateDetails(InvStatus)) 
            {
                JQ.showStatusMsg(this, "1", "Record Updated Successfully");
            };
            objInvoice.DataBind();

        }
        else
            JQ.showStatusMsg(this, "3", "User not Allowed to Update Record");
    }
}
