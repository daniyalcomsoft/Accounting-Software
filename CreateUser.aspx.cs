using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SW.SW_Common;

public partial class CreateUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionBO"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (Request.QueryString["RoleID"] != null)
        {

        }
        FillControls();
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "CreateUser.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "CreateUser.aspx" && view == true)
                {
                    if (Request.QueryString["UserID"] != null)
                    {
                        string UserID = EncryptDecrypt.Decrypt(Uri.UnescapeDataString(Request.QueryString["UserID"]));
                        if (UserID == "")
                        {
                            Response.Redirect("Login.aspx");

                        }
                        else
                        {
                            if (UserID != null && Request.QueryString["view"] != null)
                            {
                                hdnUserId.Value = UserID;
                                FillUserInfo(Convert.ToInt32(hdnUserId.Value));
                                LnkBtnNew.Visible = true;
                                ReadOnlyControl();
                            }
                            else
                            {
                                hdnUserId.Value = UserID;
                                FillUserInfo(Convert.ToInt32(hdnUserId.Value));
                                LnkBtnNew.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        hdnUserId.Value = null;
                        LnkBtnNew.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
            
        }
    }

    #region Methods
    private void FillControls()
    {
        if (cmbUserRole.Items.Count > 0)
        {
            HiddenSelectedRole.Value = cmbUserRole.SelectedItem.Value;
        }
        UserRole_BAL RoleBL = new UserRole_BAL();
        PM.BindDropDown(cmbUserRole, RoleBL.GetAllRole(), "RoleID", "RoleName");
        if (HiddenSelectedRole.Value != "" && HiddenSelectedRole.Value != null)
        {
            cmbUserRole.SelectedValue = HiddenSelectedRole.Value;
        }
    }
    public void ReadOnlyControl()
    {
        txtCellPhonePrimary.ReadOnly = true;
        txtCellPhoneSecondary.ReadOnly = true;
        txtCity.ReadOnly = true;
        txtConfirmPasswordUsr.ReadOnly = true;
        txtDistrict.ReadOnly = true;
        txtEmail.ReadOnly = true;
        txtFirst.ReadOnly = true;
        txtHomephone.ReadOnly = true;
        txtLast.ReadOnly = true;
        txtMailingaddress.ReadOnly = true;
        txtMiddle.ReadOnly = true;
        txtOfficephone.ReadOnly = true;
        txtPassowrdUsr.ReadOnly = true;
        txtPostal.ReadOnly = true;
        txtPrefix.ReadOnly = true;
        txtUsername.ReadOnly = true;
        btnSave.Visible = false;
        btnDelRoll.Visible = false;
        btnEditRole.Visible = false;
        btnNewRole.Visible = false;
    }
    private void FillUserInfo(int UserID)
    {
        if (Request.QueryString["view"] != null)
        {
            btnSave.Visible = false;
            //btnSpecialPermission.Visible = false;
            btnNewRole.Visible = false;
            btnEditRole.Visible = false;
        }
        User_BAL UserBL = new User_BAL();
        User_BAL UBO = UserBL.GetUserInfo(UserID);
        hdnUserId.Value = UBO.UserID.ToString();
        txtPrefix.Text = UBO.Prefix;
        txtFirst.Text = UBO.FirstName;
        txtMiddle.Text = UBO.MiddleName;
        txtLast.Text = UBO.LastName;
        txtCellPhonePrimary.Text = UBO.CellPhonePrimary;
        txtCellPhoneSecondary.Text = UBO.CellPhoneSecondary;
        txtHomephone.Text = UBO.HomePhone;
        txtOfficephone.Text = UBO.OfficePhone;
        txtEmail.Text = UBO.Email;
        txtMailingaddress.Text = UBO.MailingAddress;
        txtDistrict.Text = UBO.District;
        txtCity.Text = UBO.City;
        txtPostal.Text = UBO.Postal;
        //txtAddress.Text = UBO.ma;
        //txtPermenentAddress.Text = UBO.PermenentAddress;
        txtUsername.Text = UBO.UserName;
        cmbUserRole.SelectedValue = UBO.RoleID.ToString();
        txtPassowrdUsr.Attributes.Add("value", Encrypter.DecryptIt(UBO.UserPassword));
        txtConfirmPasswordUsr.Attributes.Add("value", Encrypter.DecryptIt(UBO.UserPassword));
        //lblSpecialPermission.Text = UBO.specialPermission.ToString().Equals("False") ? "NO" : "YES";
        chkActive.Checked = Convert.ToBoolean(UBO.IsActive);
    }

    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (hdnUserId.Value == "")
        {
            if (SBO.Can_Insert == true)
            {
                SaveUser();
            }
            else
            { JQ.showStatusMsg(this, "3", "User not Allowed to Create New User"); }
        }
        else
        {
            if (SBO.Can_Update == true)
            {
                SaveUser();
            }
            else
            { JQ.showStatusMsg(this, "3", "User not Allowed to Update User"); }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("SE_AdminUsers.aspx");
    }
    protected void LnkBtnNew_Click(object sender, EventArgs e)
    {
        NewUser();
    }
    public void NewUser()
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO != null)
        {
            DataRow[] dr = SBO.PermissionTable.Select("Page_Url='CreateUser.aspx'");
            if (dr.Length > 0)
            {
                if (Convert.ToBoolean(dr[0]["Can_Insert"]) == true)
                {
                    Response.Redirect("CreateUser.aspx");
                }
            }
            else
                Response.Write("<script>alert('You do not have rights to insert');</script>");
        }
    }
    protected void btnDelRoll_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Delete == true)
        {
            User_BAL Bll = new User_BAL();
            DataTable dt = new DataTable();
            string RollName = cmbUserRole.SelectedItem.Text;
            dt = Bll.CheckUserRolebeforeDelete(Convert.ToInt32(cmbUserRole.SelectedItem.Value));
            if (dt.Rows.Count > 0)
            {
                JQ.showDialog(this, "Check");
                lblDeleteMsg.Text = "Selected Role [" + RollName + "] Alredy in Used";
            }
            else
            {
                JQ.showDialog(this, "Confirmation");
                LabelConf.Text = "Are you Sure ! to deleted [" + RollName + "] Role";
            }
        }
        else
        { JQ.showStatusMsg(this, "3", "User not Allowed to Delete Roll"); }

    }
    protected void LinkButtonYes_Click(object sender, EventArgs e)
    {
        JQ.closeDialog(this, "Confirmation");
    }
    private void SaveUser()
    {
        User_BAL BO = new User_BAL();
        BO.Prefix = txtPrefix.Text;
        BO.UserID = hdnUserId.Value.Equals("") ? 0 : Convert.ToInt32(hdnUserId.Value);
        BO.FirstName = txtFirst.Text;
        BO.MiddleName = txtMiddle.Text;
        BO.LastName = txtLast.Text;
        BO.CellPhonePrimary = txtCellPhonePrimary.Text;
        BO.CellPhoneSecondary = txtCellPhoneSecondary.Text;
        BO.HomePhone = txtHomephone.Text;
        BO.OfficePhone = txtOfficephone.Text;
        BO.Email = txtEmail.Text;
        BO.MailingAddress = txtMailingaddress.Text;
        BO.District = txtDistrict.Text;
        BO.City = txtCity.Text;
        BO.Postal = txtPostal.Text;
        BO.UserName = txtUsername.Text.Trim();
        BO.RoleID = Convert.ToInt32(cmbUserRole.SelectedValue);
        BO.UserPassword = Encrypter.EncryptIt(txtPassowrdUsr.Text);
        BO.IsActive = Convert.ToInt16(chkActive.Checked);
        User_BAL UserBL = new User_BAL();
        DataRow row = UserBL.CreateModifyUser(BO, (SCGL_Session)Session["SessionBO"]);
        hdnUserId.Value = row["UserID"].ToString();
        txtPassowrdUsr.Attributes["value"] = Encrypter.DecryptIt(BO.UserPassword);
        txtConfirmPasswordUsr.Attributes["value"] = Encrypter.DecryptIt(BO.UserPassword);
        JQ.showStatusMsg(this, row["MsgType"].ToString(), row["Msg"].ToString());

    }
    protected void btnNewRole_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Insert == true)
        {
            JQ.RecallJS(this, "OpenRolePermission('Add');");
        }
        else
        { JQ.showStatusMsg(this, "3", "User not Allowed to Create New Roll"); }
    }
    protected void btnEditRole_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Update == true)
        {
            JQ.RecallJS(this, "OpenRolePermission('Edit');");
        }
        else
        { JQ.showStatusMsg(this, "3", "User not Allowed to Update Roll"); }
    }
}
