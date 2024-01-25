using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SW.SW_Common;


public partial class SE_SuperCostCenter : System.Web.UI.Page
{
    SuperAdmin_BAL LnBLL = new SuperAdmin_BAL();
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "SE_SuperCostCenter.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "SE_SuperCostCenter.aspx" && view == true)
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
    #region Method
    private void OnLoad()
    {
        PM.BindDataGrid(GridCostCenter, LnBLL.GetCostCenterTable());
    }
    private void RefreshControl()
    {
        txtCostCenterID.Text = "";
        txtCostCenterName.Text = "";
        ChkIsActive.Checked = false;
    }
    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (txtCostCenterID.Text == "")
        {
            if (SBO.Can_Insert == true)
            {
                SaveCostCenter();
                JQ.showStatusMsg(this, "1", "Successfull Record Insert");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "temp", "closeDialog('NewCostCenter')", true);
                JQ.showStatusMsg(this, "3", "User not Allowed to Insert New Record");
            }
        }
        else
        {
            if (SBO.Can_Update == true)
            {
                SaveCostCenter();
                JQ.showStatusMsg(this, "1", "Successfull Record Update");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "temp", "closeDialog('NewCostCenter')", true);
                JQ.showStatusMsg(this, "3", "User not Allowed to Update Record");
            }
        }
    }
    protected void lbtnEdit_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Update == true)
        {
            if (e.CommandArgument.ToString() != "")
            {
                SuperAdmin_BAL BO = LnBLL.GetCostCenterByID(Convert.ToInt32(e.CommandArgument));
                txtCostCenterID.Text = BO.CostCenterID.ToString();
                txtCostCenterName.Text = BO.CostCenterName.ToString();
                ChkIsActive.Checked = Convert.ToBoolean(BO.IsAction);
                JQ.showDialog(this, "NewCostCenter");
            }
        }
        else
        { JQ.showStatusMsg(this, "3", "User not Allowed to Update Record"); }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Insert == true)
        {
            RefreshControl();
            JQ.showDialog(this, "NewCostCenter");
        }
        else
        { JQ.showStatusMsg(this, "3", "User not Allowed to Insert New Record"); }
    }
    protected void lbtnYes_Click(object sender, EventArgs e)
    {
        lblDeleteMsg.Text = LnBLL.DeleteCostCenter(Convert.ToInt32(lblGroupID.Text));
        PM.BindDataGrid(GridCostCenter, LnBLL.GetCostCenterTable());
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
        { JQ.showStatusMsg(this, "3", "User not Allowed to Delete Record"); }
    }
    protected void GridCostCenter_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        OnLoad();
        GridCostCenter.PageIndex = e.NewPageIndex;
        GridCostCenter.DataBind();
    }
    private void SaveCostCenter()
    {
        SuperAdmin_BAL BO = new SuperAdmin_BAL();
        BO.CostCenterID = txtCostCenterID.Text.Equals("") ? 0 : Convert.ToInt32(txtCostCenterID.Text);
        BO.CostCenterName = txtCostCenterName.Text;
        BO.IsAction = Convert.ToInt16(ChkIsActive.Checked);
        LnBLL.CreateModifyCostCenter(BO, (SCGL_Session)Session["SessionBO"]);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "temp", "closeDialog('NewCostCenter')", true);
        PM.BindDataGrid(GridCostCenter, LnBLL.GetCostCenterTable());
    }
}

