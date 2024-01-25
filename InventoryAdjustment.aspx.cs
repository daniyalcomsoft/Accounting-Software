using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SW.SW_Common;

public partial class InventoryAdjustment : System.Web.UI.Page
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "InventoryAdjustment.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "InventoryAdjustment.aspx" && view == true)
                {
                    Bind_InventoryItem();
                    if (Request.QueryString["Id"] != null)
                    {
                        FillControl();
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }

            
        }
        Reload_JS();
        Invoice_BAL BALInvoice = new Invoice_BAL();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataTable dt = PM.getFinancialYearByID(SBO.FinYearID);
        hdnMinDate.Value = SCGL_Common.CheckDateTime(dt.Rows[0]["yearFrom"]).ToShortDateString();
        hdnMaxDate.Value = SCGL_Common.CheckDateTime(dt.Rows[0]["YearTo"]).ToShortDateString();
    }
    public void FillControl()
    {
        
        if (Request.QueryString["view"] != null)
        {
            btnSave.Visible = false;
        }
        int ID = SCGL_Common.Convert_ToInt(Request.QueryString["ID"].ToString());
        DataTable dt = IFBAL.GetAdjustment_byID(ID);
        if (dt.Rows.Count > 0)
        {
            ddlInventoryItem.SelectedValue = dt.Rows[0]["Inventory_ID"].ToString();
            txtDate.Text = SCGL_Common.CheckDateTime(dt.Rows[0]["Date"]).ToShortDateString();
            ddlAction.SelectedIndex =SCGL_Common.Convert_ToInt(dt.Rows[0]["Action"].ToString());
            txtQuantity.Text = dt.Rows[0]["Quantity"].ToString();
            txtRate.Text = dt.Rows[0]["Rate"].ToString();            
            btnSave.Text = "Update";
        }
    }
    public void Reload_JS()
    {
        SCGL_Common.ReloadJS(this, "ChangeDate();");
        SCGL_Common.ReloadJS(this, "MyDate();");
    }
  

    public void Bind_InventoryItem()
    {
        SCGL_Common.Bind_DropDown(ddlInventoryItem, "vt_SCGL_SP_GetFishCart", "item", "Inventory_ID");
    }

    public bool Check_Validation()
    {
        bool IsValid = true;        
        return IsValid;
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {       
        try
        {
            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            if (Check_Validation())
            {
                if (btnSave.Text == "Save")
                {
                    IFBAL.InventoryAdjustmentId = -1;
                }
                else
                {
                    IFBAL.InventoryAdjustmentId = SCGL_Common.Convert_ToInt(Request.QueryString["Id"]);
                }
                IFBAL.AdjustmentInventory_Id = SCGL_Common.Convert_ToInt(ddlInventoryItem.SelectedValue);
                IFBAL.AdjustmentDate = SCGL_Common.CheckDateTime(txtDate.Text);
                IFBAL.AdjustmentAction = ddlAction.Text;
                IFBAL.AdjustmentQuantity = SCGL_Common.Convert_ToDecimal(txtQuantity.Text);
                IFBAL.AdjustmentRate = SCGL_Common.Convert_ToDecimal(txtRate.Text);
                if (IFBAL.CreateModifyAdjustmentInventory(IFBAL))
                {
                    if (btnSave.Text == "Save")
                    {
                        lblSuccessMsg.InnerHtml = "Adjustment Created Successfully";
                    }
                    else
                    {
                        lblSuccessMsg.InnerHtml = "Adjustment Updated Successfully";
                        //Response.Redirect("InventoryAdjustment_Views.aspx");
                    }
                    SCGL_Common.Success_Message(this.Page, "InventoryAdjustment_Views.aspx");
                }
            }
            txtDate.Text = "";
            txtQuantity.Text = "";
            txtRate.Text = "";
            ddlAction.SelectedIndex = 0;
            ddlInventoryItem.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            SCGL_Common.Error_Message(this.Page);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("InventoryAdjustment_Views.aspx");
    }
}
