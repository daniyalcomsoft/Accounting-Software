using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SW.SW_Common;
using System.Data;

public partial class VendorForm_Views : System.Web.UI.Page
{
    VendorForm_BAL BLL = new VendorForm_BAL();
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "VendorForm_Views.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "VendorForm_Views.aspx" && view == true)
                {
                    GridVendorView.DataSource = BLL.GetVendorData();
                    GridVendorView.DataBind();
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            } 
        }
    }

    

    protected void lbtnEdit_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Update == true)
        {
            Response.Redirect("VendorForm.aspx?Id=" + e.CommandArgument.ToString());
        }
        else
        { JQ.showStatusMsg(this, "3", "User not Allowed to Update Record"); }
    }
    protected void lbtnView_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_View == true)
        {
            int view = 1;
            Response.Redirect("VendorForm.aspx?Id=" + e.CommandArgument.ToString() + "&view=" + view);
        }

        else
        { JQ.showStatusMsg(this, "3", "User not Allowed to View Record"); }
    }

    protected void lbtnYes_Click(object sender, EventArgs e)
    {
        lblDeleteMsg.Text = BLL.DeleteVendor(Convert.ToInt32(lblGroupID.Text));
        PM.BindDataGrid(GridVendorView, BLL.GetVendorData());
        lbtnYes.Visible = false;
        lbtnNo.Text = "Ok";
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
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to Delete Record");
        }


        //SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        //if (SBO.Can_Delete == true)
        //{
        //    if (e.CommandName == "Del")
        //    {
        //        int a = BLL.DeleteVendor(Convert.ToInt32(e.CommandArgument));
        //        if (a != 0)
        //        {
        //            BLL.DeleteVendor(Convert.ToInt32(e.CommandArgument));
        //            GridVendorView.DataSource = null;
        //            GridVendorView.DataSource = BLL.GetVendorData();
        //            GridVendorView.DataBind();
        //            JQ.showStatusMsg(this, "1", "Record Successfully Delete");
        //        }
        //        else
        //        {
        //            JQ.showStatusMsg(this, "3", "Can not delete this record");
        //        }
        //    }
        //}
        //else
        //{ JQ.showStatusMsg(this, "3", "User not Allowed to Delete Record"); }

    }
    protected void GridCustomerView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (txtVendorName.Text != "")
        {
            if (ddlSearch.Text == "Vendor Name")
            {
                PM.BindDataGrid(GridVendorView, BLL.searchVendorByVendorName(txtVendorName.Text));
                GridVendorView.PageIndex = e.NewPageIndex;
                GridVendorView.DataBind();
            }
        }
        else if (txtMobileNo.Text != "")
        {
            if (ddlSearch.Text == "Mobile No")
            {
                PM.BindDataGrid(GridVendorView, BLL.searchVendorByMobileNumber(txtMobileNo.Text));
                GridVendorView.PageIndex = e.NewPageIndex;
                GridVendorView.DataBind();
            }
        }
        else if (txtEmail.Text != "")
        {
            if (ddlSearch.Text == "Email")
            {
                PM.BindDataGrid(GridVendorView, BLL.searchVendorByEmail(txtEmail.Text));
                GridVendorView.PageIndex = e.NewPageIndex;
                GridVendorView.DataBind();
            }
        }
        else
        {
            GridVendorView.DataSource = BLL.GetVendorData();
            GridVendorView.PageIndex = e.NewPageIndex;
            GridVendorView.DataBind();
        }

        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Insert == true)
        {
            Response.Redirect("VendorForm.aspx");
        }

        else
        { JQ.showStatusMsg(this, "3", "User not Allowed to Add Vendor Record"); }
       
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        if (txtVendorName.Text != "")
        {
            if (ddlSearch.Text == "Vendor Name")
            {
                PM.BindDataGrid(GridVendorView, BLL.searchVendorByVendorName(txtVendorName.Text));
                txtMobileNo.Text = "";
                txtEmail.Text = "";
            }
        }
        if (txtMobileNo.Text != "")
        {
            if (ddlSearch.Text == "Mobile No")
            {
                PM.BindDataGrid(GridVendorView, BLL.searchVendorByMobileNumber(txtMobileNo.Text));
                txtVendorName.Text = "";
                txtEmail.Text = "";
            }
        }

        if (txtEmail.Text != "")
        {
            if (ddlSearch.Text == "Email")
            {
                PM.BindDataGrid(GridVendorView, BLL.searchVendorByEmail(txtEmail.Text));
                txtVendorName.Text = "";
                txtMobileNo.Text = "";
            }
        }
        SCGL_Common.ReloadJS(this, "setSearchElem();");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtVendorName.Text = "";
        txtMobileNo.Text = "";
        txtEmail.Text = "";
        PM.BindDataGrid(GridVendorView, BLL.GetVendorData());
    }
    protected void txtVendorName_TextChanged(object sender, EventArgs e)
    {
        if (txtVendorName.Text == "")
        {
            PM.BindDataGrid(GridVendorView, BLL.GetVendorData());
        }
    }
    protected void txtMobileNo_TextChanged(object sender, EventArgs e)
    {
        if (txtMobileNo.Text == "")
        {
            PM.BindDataGrid(GridVendorView, BLL.GetVendorData());
        }
    }
    protected void txtEmail_TextChanged(object sender, EventArgs e)
    {
        if (txtEmail.Text == "")
        {
            PM.BindDataGrid(GridVendorView, BLL.GetVendorData());
        }
    }
}
