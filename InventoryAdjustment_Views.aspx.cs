using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SW.SW_Common;
using System.Data;

public partial class InventoryAdjustment_Views : System.Web.UI.Page
{
    InventoryForm_BAL Bal = new InventoryForm_BAL();
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "InventoryAdjustment_Views.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "InventoryAdjustment_Views.aspx" && view == true)
                {
                    OnLoad();
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
        }
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Insert == true)
        {
            Response.Redirect("InventoryAdjustment.aspx");
        }
        else
        { JQ.showStatusMsg(this, "3", "User not Allowed to Insert Record"); }
    }
    protected void lbtnEdit_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Update == true)
        {
            Response.Redirect("InventoryAdjustment.aspx?Id=" + e.CommandArgument.ToString());
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
            Response.Redirect("InventoryAdjustment.aspx?Id=" + e.CommandArgument.ToString() + "&view=" + view);
        }

        else
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to View Record");
        }
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
    protected void GridAdjustmentInventoryView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        if(txtAdjustmentID.Text!="")
        {
            if (ddlSearch.Text == "Adjustment ID")
            {

                PM.BindDataGrid(GridAdjustmentInventoryView, Bal.SearchAdjustmentInventoryRecordByAdjustmentID(SCGL_Common.Convert_ToInt(txtAdjustmentID.Text),FinYearID));
                GridAdjustmentInventoryView.PageIndex = e.NewPageIndex;
                GridAdjustmentInventoryView.DataBind();
            }
         }
        else if(txtRate.Text != "")
        {
            if (ddlSearch.Text == "Rate")
            {
                PM.BindDataGrid(GridAdjustmentInventoryView, Bal.SearchAdjustmentInventoryRecordByRate(txtRate.Text,FinYearID));
                GridAdjustmentInventoryView.PageIndex = e.NewPageIndex;
                GridAdjustmentInventoryView.DataBind();
            }
        }
        else if(ddlSearch.Text == "Action")
        {
            
                PM.BindDataGrid(GridAdjustmentInventoryView, Bal.SearchAdjustmentInventoryRecordByAction(SCGL_Common.Convert_ToInt(ddlAction.SelectedValue),FinYearID));
                GridAdjustmentInventoryView.PageIndex = e.NewPageIndex;
                GridAdjustmentInventoryView.DataBind();
            
        }
       
        else
        {
            PM.BindDataGrid(GridAdjustmentInventoryView, Bal.GetAdjustmentInventoryData(SCGL_Common.Convert_ToInt(FinYearID)));
            GridAdjustmentInventoryView.PageIndex = e.NewPageIndex;
            GridAdjustmentInventoryView.DataBind();
        }
        
    }
    private void OnLoad()
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        PM.BindDataGrid(GridAdjustmentInventoryView, Bal.GetAdjustmentInventoryData(SCGL_Common.Convert_ToInt(FinYearID)));
    }

    protected void lbtnYes_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        lblDeleteMsg.Text = Bal.DeleteAdjustmentInventory(Convert.ToInt32(lblGroupID.Text));
        if (txtAdjustmentID.Text != "")
        {
            if (ddlSearch.Text == "Adjustment ID")
            {
                PM.BindDataGrid(GridAdjustmentInventoryView, Bal.SearchAdjustmentInventoryRecordByAdjustmentID(SCGL_Common.Convert_ToInt(txtAdjustmentID.Text), FinYearID));
            }
        }
        else if (txtRate.Text != "")
        {
            if (ddlSearch.Text == "Rate")
            {
                PM.BindDataGrid(GridAdjustmentInventoryView, Bal.SearchAdjustmentInventoryRecordByRate(txtRate.Text, FinYearID));
            }
        }
        else if (ddlSearch.Text == "Action")
        {
            PM.BindDataGrid(GridAdjustmentInventoryView, Bal.SearchAdjustmentInventoryRecordByAction(SCGL_Common.Convert_ToInt(ddlAction.SelectedValue), FinYearID));
        }

        else
        {
            OnLoad();
        }
        
        
        lbtnYes.Visible = false;
        lbtnNo.Text = "Ok";

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        DataTable dt = new DataTable();
        if (txtAdjustmentID.Text != "")
        {
            if (ddlSearch.Text == "Adjustment ID")
            {
                PM.BindDataGrid(GridAdjustmentInventoryView, Bal.SearchAdjustmentInventoryRecordByAdjustmentID(SCGL_Common.Convert_ToInt(txtAdjustmentID.Text),FinYearID));
            }
        }
        if (txtRate.Text != "")
        {
            if (ddlSearch.Text == "Rate")
            {
                PM.BindDataGrid(GridAdjustmentInventoryView, Bal.SearchAdjustmentInventoryRecordByRate(txtRate.Text, FinYearID));
            }
        }


        if (ddlSearch.Text == "Action")
        {
            PM.BindDataGrid(GridAdjustmentInventoryView, Bal.SearchAdjustmentInventoryRecordByAction(SCGL_Common.Convert_ToInt(ddlAction.SelectedValue), FinYearID));
        }
       
       
        SCGL_Common.ReloadJS(this, "setSearchElem();");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        txtAdjustmentID.Text = "";
        txtRate.Text = "";
        ddlAction.SelectedIndex=0;
        PM.BindDataGrid(GridAdjustmentInventoryView, Bal.GetAdjustmentInventoryData(FinYearID));
    }
    protected void txtAdjustmentID_TextChanged(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        if (txtAdjustmentID.Text == "")
        {
            PM.BindDataGrid(GridAdjustmentInventoryView, Bal.GetAdjustmentInventoryData(FinYearID));
        }
    }
    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        if (txtRate.Text == "")
        {
            PM.BindDataGrid(GridAdjustmentInventoryView, Bal.GetAdjustmentInventoryData(FinYearID));
        }
    }
   
}
