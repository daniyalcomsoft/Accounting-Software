using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using SW.SW_Common;

public partial class AddEditRolePermission : System.Web.UI.Page
{
    UserRole_BAL Role = new UserRole_BAL();
    public static string Role_Name = string.Empty;
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "AddEditRolePermission.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "AddEditRolePermission.aspx" && view == true)
                {
                    FillGrids();

                    if (Request.QueryString["RoleID"] != null)
                    {
                        SetRoleValue(Convert.ToInt32(Request.QueryString["RoleID"]));
                        GetModulPermission(Convert.ToInt32(Request.QueryString["RoleID"]));
                        GetRolePagePermission(Convert.ToInt32(Request.QueryString["RoleID"]));
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
            
        }
    }

    private void SetRoleValue(int RoleID)
    {
        UserRole_BAL Bo = Role.GetRoleInfo(RoleID);
        txtRoleName.Text = Bo.RoleName;
        Role_Name = Bo.RoleName;
        cmbRoleStatus.SelectedValue = Bo.Active.ToString();
    }
    #region Retrival Methods

    private void GetModulPermission(int RoleID)
    {
        ModulePage_BAL mod = new ModulePage_BAL();
        DataTable dt = mod.GetModuleRightsByRoleID(RoleID);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            for (int j = 0; j < GridModule.Rows.Count; j++)
            {
                Label lblModulID = (Label)GridModule.Rows[j].FindControl("lblModuleID");
                if (lblModulID.Text == dt.Rows[i]["ModuleID"].ToString())
                {
                    CheckBox ChkSelect = (CheckBox)GridModule.Rows[j].FindControl("ChkSelect");
                    ChkSelect.Checked = true;
                    ((CheckBox)GridModule.Rows[j].FindControl("ChkView")).Checked = Convert.ToBoolean(dt.Rows[i]["Can_View"]);
                    ((CheckBox)GridModule.Rows[j].FindControl("ChkInsert")).Checked = Convert.ToBoolean(dt.Rows[i]["Can_Insert"]);
                    ((CheckBox)GridModule.Rows[j].FindControl("ChkUpdate")).Checked = Convert.ToBoolean(dt.Rows[i]["Can_Update"]);
                    ((CheckBox)GridModule.Rows[j].FindControl("ChkDelete")).Checked = Convert.ToBoolean(dt.Rows[i]["Can_Delete"]);
                    //((CheckBox)GridModule.Rows[j].FindControl("ChkApprove")).Checked = Convert.ToBoolean(dt.Rows[i]["Can_ApproveOrReject"]);
                }
            }
        }
    }

    private void GetRolePagePermission(int RoleID)
    {
        RolePage_BAL RolePage = new RolePage_BAL();
        DataTable dt = RolePage.GetPagePermissionpPagesByRole(RoleID);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            bool Continue;
            Continue = SetRolePagePermissionOnGrid(GridGeneralLedger, dt.Rows[i]);
            if (Continue)
                continue;
            //Continue = SetRolePagePermissionOnGrid(GridMortgage, dt.Rows[i]);
            //if (Continue)
            //    continue;
            Continue = SetRolePagePermissionOnGrid(GrdSales, dt.Rows[i]);
            if (Continue)
                continue;
            Continue = SetRolePagePermissionOnGrid(GrdPurchase, dt.Rows[i]);
            if (Continue)
                continue;
            Continue = SetRolePagePermissionOnGrid(GrdInventory, dt.Rows[i]);
            if (Continue)
                continue;
            //Continue = SetRolePagePermissionOnGrid(GridInterBank, dt.Rows[i]);
            //if (Continue)
            //    continue;
            Continue = SetRolePagePermissionOnGrid(GridSecurity, dt.Rows[i]);
            
        }
    }

    private bool SetRolePagePermissionOnGrid(GridView Grid, DataRow Drow)
    {
        for (int i = 0; i < Grid.Rows.Count; i++)
        {
            Label lblPageId = (Label)Grid.Rows[i].FindControl("lblPageID");
            if (lblPageId.Text == Drow["PageId"].ToString())
            {
                ((CheckBox)Grid.Rows[i].FindControl("ChkSelect")).Checked = true;
                ((CheckBox)Grid.Rows[i].FindControl("ChkView")).Checked = Convert.ToBoolean(Drow["Can_View"]);
                ((CheckBox)Grid.Rows[i].FindControl("ChkInsert")).Checked = Convert.ToBoolean(Drow["Can_Insert"]);
                ((CheckBox)Grid.Rows[i].FindControl("ChkUpdate")).Checked = Convert.ToBoolean(Drow["Can_Update"]);
                ((CheckBox)Grid.Rows[i].FindControl("ChkDelete")).Checked = Convert.ToBoolean(Drow["Can_Delete"]);
                //((CheckBox)Grid.Rows[i].FindControl("ChkApprove")).Checked = Convert.ToBoolean(Drow["Can_ApproveOrReject"]);
                return true;
            }
        }
        return false;
    }

    #endregion


    private void InsertUpdateModulePermission(int RoleID)
    {
        ModulePage_BAL mod = new ModulePage_BAL();
        mod.DeleteModulePermissionByRoleID(RoleID);
        for (int i = 0; i < GridModule.Rows.Count; i++)
        {
            ModulePage_BAL modPage = new ModulePage_BAL();
            CheckBox chk = (CheckBox)GridModule.Rows[i].FindControl("ChkSelect");
            CheckBox ChkView = (CheckBox)GridModule.Rows[i].FindControl("ChkView");
            CheckBox ChkInsert = (CheckBox)GridModule.Rows[i].FindControl("ChkInsert");
            CheckBox ChkUpdate = (CheckBox)GridModule.Rows[i].FindControl("ChkUpdate");
            CheckBox ChkDelete = (CheckBox)GridModule.Rows[i].FindControl("ChkDelete");
            //CheckBox ChkApprove = (CheckBox)GridModule.Rows[i].FindControl("ChkApprove");
            if (chk.Checked)
            {
                if (ChkView.Checked || ChkInsert.Checked || ChkUpdate.Checked || ChkDelete.Checked)
                {
                    modPage.RoleID = RoleID;
                    modPage.ModuleID = Convert.ToInt32(((Label)GridModule.Rows[i].FindControl("lblModuleID")).Text);
                    modPage.Can_View = ChkView.Checked;
                    modPage.Can_Insert = ChkInsert.Checked;
                    modPage.Can_Update = ChkUpdate.Checked;
                    modPage.Can_Delete = ChkDelete.Checked;
                    //modPage.Can_ApproveOrReject = ChkApprove.Checked;
                    modPage.Active = Convert.ToInt16(cmbRoleStatus.SelectedValue);
                    mod.InsertUpdateModulePermissionByRoleID(modPage, (SCGL_Session)Session["SessionBO"]);
                    lblSave.ForeColor = Color.Black;
                    lblSave.Text = "the record has been updated";
                }
            }
        }
    }

    private void InsertUpdateRolePagePermission(GridView Grid, int RoleID)
    {
        for (int i = 0; i < Grid.Rows.Count; i++)
        {
            RolePage_BAL RolePage = new RolePage_BAL();
            CheckBox chk = (CheckBox)Grid.Rows[i].FindControl("ChkSelect");
            CheckBox chkviewm = (CheckBox)Grid.Rows[i].FindControl("ChkView");
            CheckBox chkinsertm = (CheckBox)Grid.Rows[i].FindControl("ChkInsert");
            CheckBox chkupdatem = (CheckBox)Grid.Rows[i].FindControl("ChkUpdate");
            CheckBox chkdeletem = (CheckBox)Grid.Rows[i].FindControl("ChkDelete");
            //CheckBox chkapprovem = (CheckBox)Grid.Rows[i].FindControl("ChkApprove");

            if (chk.Checked)
            {
                if (chkviewm.Checked || chkinsertm.Checked || chkupdatem.Checked || chkdeletem.Checked)
                {
                    RolePage.RoleId = RoleID;
                    RolePage.PageId = Convert.ToInt32(((Label)Grid.Rows[i].FindControl("lblPageID")).Text);
                    RolePage.Can_View = chkviewm.Checked; //((CheckBox)Grid.Rows[i].FindControl("ChkView")).Checked;
                    RolePage.Can_Insert = chkinsertm.Checked; //((CheckBox)Grid.Rows[i].FindControl("ChkInsert")).Checked;
                    RolePage.Can_Update = chkupdatem.Checked;// ((CheckBox)Grid.Rows[i].FindControl("ChkUpdate")).Checked;
                    RolePage.Can_Delete = chkdeletem.Checked;// ((CheckBox)Grid.Rows[i].FindControl("ChkDelete")).Checked;
                    //RolePage.Can_ApproveOrReject = chkapprovem.Checked;// ((CheckBox)Grid.Rows[i].FindControl("ChkApprove")).Checked;
                    RolePage.Active = Convert.ToInt16(cmbRoleStatus.SelectedValue);
                    RolePage_BAL roleBLL = new RolePage_BAL();
                    roleBLL.InsertUpdatePagePermission(RolePage, (SCGL_Session)Session["SessionBO"]);
                    lblSave.ForeColor = Color.Black;
                    lblSave.Text = "the record has been updated";
                }

            }
        }
    }

    private void FillGrids()
    {

        ModulePage_BAL Mod = new ModulePage_BAL();
        PM.BindDataGrid(GridModule, Mod.GetAllActiveModule());
        PM.BindDataGrid(GridGeneralLedger, Mod.GetModulePages(PM.ModuleName.GeneralLedger));
        //PM.BindDataGrid(GridMortgage, Mod.GetModulePages(PM.ModuleName.Mortgage));
        //PM.BindDataGrid(GridInterBank, Mod.GetModulePages(PM.ModuleName.InterBank));
        PM.BindDataGrid(GridSecurity, Mod.GetModulePages(PM.ModuleName.Security));
        PM.BindDataGrid(GrdSales, Mod.GetModulePages(PM.ModuleName.Sales));
        PM.BindDataGrid(GrdPurchase, Mod.GetModulePages(PM.ModuleName.Purchase));
        PM.BindDataGrid(GrdInventory, Mod.GetModulePages(PM.ModuleName.Inventory));
      
    }

    private void SaveRole()
    {
        UserRole_BAL RoleBO = new UserRole_BAL();
        if (Request.QueryString["RoleID"] != null)
        {
            RoleBO.RoleID = Convert.ToInt32(Request.QueryString["RoleID"]);
        }
        RoleBO.RoleName = txtRoleName.Text;
        RoleBO.Active = Convert.ToInt16(cmbRoleStatus.SelectedValue);

        int RoleID = Role.CreateModifyUserRole(RoleBO, (SCGL_Session)Session["SessionBO"]);
        RolePage_BAL RolePage = new RolePage_BAL();
        RolePage.DeletePagePermissionPagesByRole(RoleID);
        InsertUpdateModulePermission(RoleID);
        InsertUpdateRolePagePermission(GridGeneralLedger, RoleID);
        //InsertUpdateRolePagePermission(GridMortgage, RoleID);
        //InsertUpdateRolePagePermission(GridInterBank, RoleID);
        InsertUpdateRolePagePermission(GridSecurity, RoleID);
        InsertUpdateRolePagePermission(GrdSales, RoleID);
        InsertUpdateRolePagePermission(GrdPurchase, RoleID);
        InsertUpdateRolePagePermission(GrdInventory, RoleID);
        JQ.RecallJS(this, "closeIframe();");


        //Response.Redirect("CreateUser.aspx");
        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "RemoveCard", "alert('Operation Success ! Your Role has been Inserted !');", true);

        //ClientScript.RegisterStartupScript(typeof(string), "CloseParent", "parent.top.document.getElementsByClass('ui-dialog')[0].style.display = 'none';", true);
    }

    protected void btnSaveRole_Click(object sender, EventArgs e)
    {
        lblRoleNameError.Text = "";
        lblSave.Text = "";
        if (!Role.CheckRoleName(txtRoleName.Text))
        {
            SaveRole();
        }
        else if (Role.CheckRoleName(txtRoleName.Text) && Request.QueryString["RoleID"] != null)
        {
            SaveRole();
        }
        else
        {
            lblRoleNameError.Text = "Role Name already exist";
        }
    }
    protected void GridModule_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string a = e.CommandArgument.ToString();
    }
    protected void GridModule_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chk = (CheckBox)e.Row.FindControl("ChkView") as CheckBox;
            ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(chk);
        }
    }
    protected void ChkView_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        GridViewRow dr = (GridViewRow)chk.Parent.Parent;
        Label lblModID = (Label)dr.FindControl("lblModuleID");
        CheckBox ChkSelected = (CheckBox)dr.FindControl("ChkSelect");
        if (ChkSelected.Checked)
            CheckApply(Convert.ToInt32(lblModID.Text), chk.ID, chk.Checked);
    }
    private void CheckApply(int ModuleID, string CheckBoxName, bool IsCheck)
    {
        GridView Grid = GetGridName(ModuleID);
        for (int i = 0; i < Grid.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)Grid.Rows[i].FindControl(CheckBoxName);
            chk.Checked = IsCheck;
        }
    }

    private GridView GetGridName(int ModuleID)
    {
        GridView Grid = null;
        if (ModuleID == (int)PM.ModuleName.GeneralLedger)
        {
            Grid = GridGeneralLedger;
        }
      
      
        //else if (ModuleID == (int)PM.ModuleName.Mortgage)
        //{
        //    Grid = GridMortgage;
        //}
        else if (ModuleID == (int)PM.ModuleName.Sales)
        {
            Grid = GrdSales;
        }
        else if (ModuleID == (int)PM.ModuleName.Purchase)
        {
            Grid = GrdPurchase;
        }
        else if (ModuleID == (int)PM.ModuleName.Inventory)
        {
            Grid = GrdInventory;
        }
      
        //else if (ModuleID == (int)PM.ModuleName.InterBank)
        //{
        //    Grid = GridInterBank;
        //}
        else if (ModuleID == (int)PM.ModuleName.Security)
        {
            Grid = GridSecurity;
        }
        
        return Grid;
    }
    protected void ChkInsert_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        GridViewRow dr = (GridViewRow)chk.Parent.Parent;
        Label lblModID = (Label)dr.FindControl("lblModuleID");
        CheckBox ChkSelected = (CheckBox)dr.FindControl("ChkSelect");
        if (ChkSelected.Checked)
            CheckApply(Convert.ToInt32(lblModID.Text), chk.ID, chk.Checked);
    }
    protected void ChkUpdate_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        GridViewRow dr = (GridViewRow)chk.Parent.Parent;
        Label lblModID = (Label)dr.FindControl("lblModuleID");
        CheckBox ChkSelected = (CheckBox)dr.FindControl("ChkSelect");
        if (ChkSelected.Checked)
            CheckApply(Convert.ToInt32(lblModID.Text), chk.ID, chk.Checked);
    }
    protected void ChkDelete_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        GridViewRow dr = (GridViewRow)chk.Parent.Parent;
        Label lblModID = (Label)dr.FindControl("lblModuleID");
        CheckBox ChkSelected = (CheckBox)dr.FindControl("ChkSelect");
        if (ChkSelected.Checked)
            CheckApply(Convert.ToInt32(lblModID.Text), chk.ID, chk.Checked);
    }

    //protected void ChkApprove_CheckedChanged(object sender, EventArgs e)
    //{
    //    CheckBox chk = (CheckBox)sender;
    //    GridViewRow dr = (GridViewRow)chk.Parent.Parent;
    //    Label lblModID = (Label)dr.FindControl("lblModuleID");
    //    CheckBox ChkSelected = (CheckBox)dr.FindControl("ChkSelect");
    //    if (ChkSelected.Checked)
    //        CheckApply(Convert.ToInt32(lblModID.Text), chk.ID, chk.Checked);
    //}
    protected void ChkUnlock_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        GridViewRow dr = (GridViewRow)chk.Parent.Parent;
        Label lblModID = (Label)dr.FindControl("lblModuleID");
        CheckBox ChkSelected = (CheckBox)dr.FindControl("ChkSelect");
        if (ChkSelected.Checked)
            CheckApply(Convert.ToInt32(lblModID.Text), chk.ID, chk.Checked);
    }
    protected void ChkSelect_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        GridViewRow dr = (GridViewRow)chk.Parent.Parent;
        Label lblModID = (Label)dr.FindControl("lblModuleID");
        CheckBox ChkSelected = (CheckBox)dr.FindControl("ChkSelect");
        CheckApply(Convert.ToInt32(lblModID.Text), chk.ID, chk.Checked);
        (dr.FindControl("ChkView") as CheckBox).Checked = ChkSelected.Checked;
        CheckBox chkView = (CheckBox)dr.FindControl("ChkView");
        CheckApply(Convert.ToInt32(lblModID.Text), chkView.ID, chk.Checked);
        (dr.FindControl("ChkInsert") as CheckBox).Checked = ChkSelected.Checked;
        CheckBox ChkInsert = (CheckBox)dr.FindControl("ChkInsert");
        CheckApply(Convert.ToInt32(lblModID.Text), ChkInsert.ID, chk.Checked);
        (dr.FindControl("ChkUpdate") as CheckBox).Checked = ChkSelected.Checked;
        CheckBox ChkUpdate = (CheckBox)dr.FindControl("ChkUpdate");
        CheckApply(Convert.ToInt32(lblModID.Text), ChkUpdate.ID, chk.Checked);
        (dr.FindControl("ChkDelete") as CheckBox).Checked = ChkSelected.Checked;
        CheckBox ChkDelete = (CheckBox)dr.FindControl("ChkDelete");
        CheckApply(Convert.ToInt32(lblModID.Text), ChkDelete.ID, chk.Checked);
        //(dr.FindControl("ChkApprove") as CheckBox).Checked = ChkSelected.Checked;
        //CheckBox ChkApprove = (CheckBox)dr.FindControl("ChkApprove");
        //CheckApply(Convert.ToInt32(lblModID.Text), ChkApprove.ID, chk.Checked);

    }
    protected void ChkSelect_CheckedChanged1(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        GridViewRow dr = (GridViewRow)chk.Parent.Parent;
        CheckBox ChkSelected = (CheckBox)dr.FindControl("ChkSelect");
        (dr.FindControl("ChkView") as CheckBox).Checked = ChkSelected.Checked;
        (dr.FindControl("ChkInsert") as CheckBox).Checked = ChkSelected.Checked;
        (dr.FindControl("ChkUpdate") as CheckBox).Checked = ChkSelected.Checked;
        (dr.FindControl("ChkDelete") as CheckBox).Checked = ChkSelected.Checked;
        //(dr.FindControl("ChkApprove") as CheckBox).Checked = ChkSelected.Checked;
    }
}
