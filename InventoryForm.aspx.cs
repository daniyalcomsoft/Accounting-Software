using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SW.SW_Common;
using System.Data;

public partial class InventoryForm : System.Web.UI.Page
{
    InventoryForm_BAL IFBAL = new InventoryForm_BAL();
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "InventoryForm.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "InventoryForm.aspx" && view == true)
                {
                    OnLoad();
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }

        }
        Reload_JS();
    }

    public void Reload_JS()
    {
        SCGL_Common.ReloadJS(this, "MyDate();");
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Insert == true)
        {
            RefreshControl();
            JQ.showDialog(this, "InventoryName");
        }
        else
        { JQ.showStatusMsg(this, "3", "User not Allowed to Insert New Record"); }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (txtnventoryID.Text == "")
        {
            if (SBO.Can_Insert == true)
            {
                SaveInventoryName();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "temp", "closeDialog('InventoryName')", true);
                JQ.showStatusMsg(this, "3", "User not Allowed to Insert New Record");
            }
        }
        else
        {
            if (SBO.Can_Update == true)
            {
                UpdateInventoryName();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "temp", "closeDialog('InventoryName')", true);
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
        lblDeleteMsg.Text = IFBAL.DeleteInventory(Convert.ToInt32(lblGroupID.Text));
        PM.BindDataGrid(GridInventory, IFBAL.GetInventoryData());
        lbtnYes.Visible = false;
        lbtnNo.Text = "Ok";

    }
    private void SaveInventoryName()
    {
        IFBAL.Inventory_Id = txtnventoryID.Text.Equals("") ? 0 : Convert.ToInt32(txtnventoryID.Text);
        IFBAL.InventoryName = txtInventoryName.Text;
        //IFBAL.InitialQuantity = SCGL_Common.Convert_ToDecimal(txtInitialQuantity.Text);
        IFBAL.AsOfDate = SCGL_Common.CheckDateTime(txtDate.Text);
        IFBAL.Rate = SCGL_Common.Convert_ToDecimal(txtRate.Text);
        IFBAL.Cost = IFBAL.InitialQuantity * IFBAL.Rate;
        int AlreadyInventoryName = IFBAL.CheckInventoryName(txtInventoryName.Text,0);
        if (AlreadyInventoryName > 0)
        {
            JQ.showStatusMsg(this, "2", "Inventory Already Existing");
        }
        else
        {
            IFBAL.CreateModifyInventoryForm(IFBAL);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "temp", "closeDialog('InventoryName')", true);
            JQ.showStatusMsg(this, "1", "Successfull Record Insert");
            txtInventoryName.Text = "";
            //txtInitialQuantity.Text = "";
            txtRate.Text = "";
            txtDate.Text = "";
            
        }
        PM.BindDataGrid(GridInventory, IFBAL.GetInventoryData());
    }

    private void UpdateInventoryName()
    {
        IFBAL.Inventory_Id = txtnventoryID.Text.Equals("") ? 0 : Convert.ToInt32(txtnventoryID.Text);
        IFBAL.InventoryName = txtInventoryName.Text;
        //IFBAL.InitialQuantity = SCGL_Common.Convert_ToDecimal(txtInitialQuantity.Text);
        IFBAL.AsOfDate = SCGL_Common.CheckDateTime(txtDate.Text);
        IFBAL.Rate = SCGL_Common.Convert_ToDecimal(txtRate.Text);
        IFBAL.Cost = IFBAL.InitialQuantity * IFBAL.Rate;
        int AlreadyInventoryName = IFBAL.CheckInventoryName(txtInventoryName.Text,SCGL_Common.Convert_ToInt(txtnventoryID.Text));
        if (AlreadyInventoryName > 0)
        {
            JQ.showStatusMsg(this, "2", "Inventory Already Existing");
        }
        else
        {
            IFBAL.CreateModifyInventoryForm(IFBAL);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "temp", "closeDialog('InventoryName')", true);
            JQ.showStatusMsg(this, "1", "Successfull Record Update");
        }
        PM.BindDataGrid(GridInventory, IFBAL.GetInventoryData());
    }

    protected void lbtnEdit_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Update == true)
        {
            if (e.CommandArgument.ToString() != "")
            {
                InventoryForm_BAL Invent = IFBAL.GetInventoryInfo(Convert.ToInt32(e.CommandArgument));
                txtnventoryID.Text = Invent.Inventory_Id.ToString();
                txtInventoryName.Text = Invent.InventoryName.ToString();
                //txtInitialQuantity.Text=Invent.InitialQuantity.ToString();
                txtDate.Text = Invent.AsOfDate.ToShortDateString();
                txtRate.Text = Invent.Rate.ToString();
                JQ.showDialog(this, "InventoryName");
            }
        }
        else
        { JQ.showStatusMsg(this, "3", "User not Allowed to Update Record"); }
    }

    #region Method
    private void OnLoad()
    {
        PM.BindDataGrid(GridInventory, IFBAL.GetInventoryData());
    }
    private void RefreshControl()
    {
        txtnventoryID.Text = "";
        txtInventoryName.Text = "";
    }
    #endregion
    protected void GridInventory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (txtSearchInventoryName.Text != "")
        {

            PM.BindDataGrid(GridInventory, IFBAL.searchInventoryName(txtSearchInventoryName.Text));
            GridInventory.PageIndex = e.NewPageIndex;
            GridInventory.DataBind();

        }
        else
        {
            OnLoad();
            GridInventory.PageIndex = e.NewPageIndex;
            GridInventory.DataBind();
        }
    }


   
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        if (txtSearchInventoryName.Text != "")
        {

            PM.BindDataGrid(GridInventory, IFBAL.searchInventoryName(txtSearchInventoryName.Text));

        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtSearchInventoryName.Text = "";
        PM.BindDataGrid(GridInventory, IFBAL.GetInventoryData());
    }
}
