using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SW.SW_Common;
using System.Data;

public partial class InventoryForm_Views : System.Web.UI.Page
{
    InventoryForm_BAL BLL = new InventoryForm_BAL();
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "InventoryForm_Views.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "InventoryForm_Views.aspx" && view == true)
                {
                    GridInventoryView.DataSource = BLL.GetInventoryData();
                    GridInventoryView.DataBind();
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
            //Reload_JS();
        }
    }

    //public void Reload_JS()
    //{

    //    SCGL_Common.ReloadJS(this, "ddlSearch();");
    //}

    protected void GridCustomerView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridInventoryView.DataSource = BLL.GetInventoryData();
        GridInventoryView.DataBind();
    }
    protected void lbtnEdit_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Update == true)
        {
            Response.Redirect("InventoryForm.aspx?Id=" + e.CommandArgument.ToString());
        }
        else
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to Update Record");
        }
    }
    protected void lbtnView_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_View == true)
        {
            int view = 1;
            Response.Redirect("InventoryForm.aspx?Id=" + e.CommandArgument.ToString() + "&view=" + view);
        }

        else
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to View Record");
        }
    }
    protected void lbtnYes_Click(object sender, EventArgs e)
    {
        lblDeleteMsg.Text =BLL.DeleteInventory(Convert.ToInt32(lblGroupID.Text));
        //if (txtFishName.Text != "")
        //{
        //    if (ddlSearch.Text == "Fish Name")
        //    {
        //        PM.BindDataGrid(GridInventoryView, BLL.searchInventoryByFishName(txtFishName.Text));
             

        //    }
        //}
        //else if (txtFishGrade.Text != "")
        //{
        //    if (ddlSearch.Text == "Fish Grade")
        //    {
        //        PM.BindDataGrid(GridInventoryView, BLL.searchInventoryByFishGrade(txtFishGrade.Text));
               
        //    }
        //}
        //else if (txtFishSize.Text != "")
        //{
        //    if (ddlSearch.Text == "Fish Size")
        //    {
        //        PM.BindDataGrid(GridInventoryView, BLL.searchInventoryByFishSize(txtFishSize.Text));
                
        //    }
        //}
        //else 
        //{
        //    
        //}

        PM.BindDataGrid(GridInventoryView, BLL.GetInventoryData());
        
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

        
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Insert == true)
        {
            Response.Redirect("InventoryForm.aspx");
        }
        else
        { JQ.showStatusMsg(this, "3", "User not Allowed to Insert Record"); }
    }

    protected void GridFish_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //if (txtFishName.Text != "")
        //{
        //    if (ddlSearch.Text == "Fish Name")
        //    {
        //        PM.BindDataGrid(GridInventoryView, BLL.searchInventoryByFishName(txtFishName.Text));
        //        GridInventoryView.PageIndex = e.NewPageIndex;
        //        GridInventoryView.DataBind();
        //    }
        //}
        //else if (txtFishGrade.Text != "")
        //{
        //    if (ddlSearch.Text == "Fish Grade")
        //    {
        //        PM.BindDataGrid(GridInventoryView, BLL.searchInventoryByFishGrade(txtFishGrade.Text));
        //        GridInventoryView.PageIndex = e.NewPageIndex;
        //        GridInventoryView.DataBind();
        //    }
        //}
        //else if (txtFishSize.Text != "")
        //{
        //    if (ddlSearch.Text == "Fish Size")
        //    {
        //        PM.BindDataGrid(GridInventoryView, BLL.searchInventoryByFishSize(txtFishSize.Text));
        //        GridInventoryView.PageIndex = e.NewPageIndex;
        //        GridInventoryView.DataBind();
        //    }
        //}
       
        //else
        //{
        //    
        //}
            OnLoad();
            GridInventoryView.PageIndex = e.NewPageIndex;
            GridInventoryView.DataBind();
        
    }
    private void OnLoad()
    {
        PM.BindDataGrid(GridInventoryView, BLL.GetInventoryData());
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        //if (txtFishName.Text != "")
        //{
        //    if (ddlSearch.Text == "Fish Name")
        //    {
        //        PM.BindDataGrid(GridInventoryView, BLL.searchInventoryByFishName(txtFishName.Text));
        //        txtFishGrade.Text = "";
        //        txtFishSize.Text = "";
        //        //txtRate.Text = "";
                
        //    }
        //}
        //if (txtFishGrade.Text != "")
        //{
        //    if (ddlSearch.Text == "Fish Grade")
        //    {
        //        PM.BindDataGrid(GridInventoryView, BLL.searchInventoryByFishGrade(txtFishGrade.Text));
        //        txtFishSize.Text = "";
        //        //txtRate.Text = "";
        //        txtFishName.Text = "";
        //    }
        //}
        //if (txtFishSize.Text != "")
        //{
        //    if (ddlSearch.Text == "Fish Size")
        //    {
        //        PM.BindDataGrid(GridInventoryView, BLL.searchInventoryByFishSize(txtFishSize.Text));
        //        txtFishGrade.Text = "";
        //        //txtRate.Text = "";
        //        txtFishName.Text = "";
        //    }
        //}
        //if (txtRate.Text != "")
        //{
        //    if (ddlSearch.Text == "Rate")
        //    {
        //        PM.BindDataGrid(GridInventoryView, BLL.searchInventoryByFishRate(SCGL_Common.Convert_ToInt(txtRate.Text)));
        //        txtFishGrade.Text = "";
        //        txtFishSize.Text = "";
        //        txtFishName.Text = "";
        //    }
        //}
        SCGL_Common.ReloadJS(this, "setSearchElem();");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtInventoryName.Text = "";
       
        PM.BindDataGrid(GridInventoryView, BLL.GetInventoryData());
    }
    //protected void txtFishName_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtFishName.Text == "")
    //    {
    //        PM.BindDataGrid(GridInventoryView, BLL.GetInventoryData());
    //    }
    //}
    //protected void txtFishSize_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtFishSize.Text == "")
    //    {
    //        PM.BindDataGrid(GridInventoryView, BLL.GetInventoryData());
    //    }
    //}
    //protected void txtFishGrade_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtFishGrade.Text == "")
    //    {
    //        PM.BindDataGrid(GridInventoryView, BLL.GetInventoryData());
    //    }
    //}
    //protected void txtRate_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtRate.Text == "")
    //    {
    //        PM.BindDataGrid(GridInventoryView, BLL.GetInventoryData());
    //    }
    //}
    protected void GridInventoryView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //if (txtFishName.Text != "")
        //{
        //    if (ddlSearch.Text == "Fish Name")
        //    {
        //        GridInventoryView.DataSource = BLL.searchInventoryByFishName(txtFishName.Text);
        //        GridInventoryView.PageIndex = e.NewPageIndex;
        //        GridInventoryView.DataBind();
        //    }
        //}
        //else if (txtFishGrade.Text != "")
        //{
        //    if (ddlSearch.Text == "Fish Grade")
        //    {
        //        GridInventoryView.DataSource = BLL.searchInventoryByFishGrade(txtFishGrade.Text);
        //        GridInventoryView.PageIndex = e.NewPageIndex;
        //        GridInventoryView.DataBind();
        //    }
        //}
        //else if (txtFishSize.Text != "")
        //{
        //    if (ddlSearch.Text == "Fish Size")
        //    {
        //        GridInventoryView.DataSource = BLL.searchInventoryByFishSize(txtFishSize.Text);
        //        GridInventoryView.PageIndex = e.NewPageIndex;
        //        GridInventoryView.DataBind();
        //    }
        //}
        //else
        //{
            
        //}

        GridInventoryView.DataSource = BLL.GetInventoryData();
        GridInventoryView.PageIndex = e.NewPageIndex;
        GridInventoryView.DataBind();
    }
   
}
