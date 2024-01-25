using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SW.SW_Common;
using System.Data;

public partial class FishSize : System.Web.UI.Page
{
    Fish_BAL Fish_Bal = new Fish_BAL();
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "FishSize.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "FishSize.aspx" && view == true)
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

    protected void btnNew_Click(object sender, EventArgs e)
    {
        
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        
        if (SBO.Can_Insert == true)
        {
            RefreshControl();
            JQ.showDialog(this, "FishSize");
            txtFishSize.ReadOnly = false;
        }
        else
        { JQ.showStatusMsg(this, "3", "User not Allowed to Insert New Record"); }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (txtSizeID.Text == "")
        {
            if (SBO.Can_Insert == true)
            {
                SaveFishSize();
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "temp", "closeDialog('FishSize')", true);
                JQ.showStatusMsg(this, "3", "User not Allowed to Insert New Record");
            }
        }
        else
        {
            if (SBO.Can_Update == true)
            {
                UpdateFishSize();
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "temp", "closeDialog('FishSize')", true);
                JQ.showStatusMsg(this, "3", "User not Allowed to Update Record");
            }
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
        { JQ.showStatusMsg(this, "3", "User not Allowed to Delete Record"); }
    }

    protected void lbtnYes_Click(object sender, EventArgs e)
    {
        lblDeleteMsg.Text = Fish_Bal.DeleteFishSize(Convert.ToInt32(lblGroupID.Text));
        PM.BindDataGrid(GridFish, Fish_Bal.GetFishSize());
        lbtnYes.Visible = false;
        lbtnNo.Text = "Ok";

    }
    private void SaveFishSize()
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        Fish_Bal.FishSizeID = txtSizeID.Text.Equals("") ? 0 : Convert.ToInt32(txtSizeID.Text);
        Fish_Bal.FishSize = txtFishSize.Text;
        Fish_Bal.SortOrder =SCGL_Common.Convert_ToInt(txtSortOrder.Text);
        int AlreadyFishSize = Fish_Bal.CheckFishSize(txtFishSize.Text, SCGL_Common.Convert_ToInt(txtSizeID.Text));
        if (AlreadyFishSize > 0)
        {
            JQ.showStatusMsg(this, "2", "Fish Size Already Existing");
        }
        else 
        {
            Fish_Bal.CreateModifyFishSize(Fish_Bal, (SCGL_Session)Session["SessionBO"]);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "temp", "closeDialog('FishSize')", true);
            JQ.showStatusMsg(this, "1", "Successfull Record Insert");
        }
        PM.BindDataGrid(GridFish, Fish_Bal.GetFishSize());
    }

    private void UpdateFishSize()
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        Fish_Bal.FishSizeID = txtSizeID.Text.Equals("") ? 0 : Convert.ToInt32(txtSizeID.Text);
        Fish_Bal.FishSize = txtFishSize.Text;
        Fish_Bal.SortOrder = SCGL_Common.Convert_ToInt(txtSortOrder.Text);
        int AlreadyFishSize = Fish_Bal.CheckFishSize(txtFishSize.Text,SCGL_Common.Convert_ToInt(txtSizeID.Text));
        if (AlreadyFishSize > 0)
        {
            JQ.showStatusMsg(this, "2", "Fish Size Already Existing");
        }
        else
        {
            Fish_Bal.CreateModifyFishSize(Fish_Bal, (SCGL_Session)Session["SessionBO"]);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "temp", "closeDialog('FishSize')", true);
            JQ.showStatusMsg(this, "1", "Successfull Record Update");
        }
        PM.BindDataGrid(GridFish, Fish_Bal.GetFishSize());
    }

    protected void lbtnEdit_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Update == true)
       {
            if (e.CommandArgument.ToString() != "")
            {
                Fish_BAL BO = Fish_Bal.GetFishSizeByID(Convert.ToInt32(e.CommandArgument));
                txtSizeID.Text = BO.FishSizeID.ToString();
                txtFishSize.Text = BO.FishSize.ToString();
                txtSortOrder.Text = BO.SortOrder.ToString();
                int IsEnabled = SCGL_Common.Convert_ToInt(BO.IsEnabled.ToString());
                if (IsEnabled == 0)
                {
                    txtFishSize.ReadOnly = true;
                }
                else
                {
                    txtFishSize.ReadOnly = false;
                }

                JQ.showDialog(this, "FishSize");
            }
        }
       else
        { JQ.showStatusMsg(this, "3", "User not Allowed to Update Record"); }
    }

    #region Method
    private void OnLoad()
    {
        PM.BindDataGrid(GridFish, Fish_Bal.GetFishSize());
    }
    private void RefreshControl()
    {
        
        txtSizeID.Text = "";
        txtFishSize.Text = "";
        txtSortOrder.Text = "";
        
    }
    #endregion
    protected void GridFish_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (txtSearchFishSize.Text != "")
        {
           
                PM.BindDataGrid(GridFish, Fish_Bal.searchFishSize(txtSearchFishSize.Text));
                GridFish.PageIndex = e.NewPageIndex;
                GridFish.DataBind();
            
        }
        else
        {

            OnLoad();
            GridFish.PageIndex = e.NewPageIndex;
            GridFish.DataBind();
        }
        
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        if (txtSearchFishSize.Text != "")
        {
            
                PM.BindDataGrid(GridFish, Fish_Bal.searchFishSize(txtSearchFishSize.Text));
            
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtSearchFishSize.Text = "";
        PM.BindDataGrid(GridFish, Fish_Bal.GetFishSize());
    }
    protected void txtSearchFishSize_TextChanged(object sender, EventArgs e)
    {
        if (txtSearchFishSize.Text == "")
        {
            PM.BindDataGrid(GridFish, Fish_Bal.GetFishSize());
        }
    }
}
