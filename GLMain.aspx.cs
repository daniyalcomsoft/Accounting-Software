using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SW.SW_Common;
using System.Data;

public partial class GLMain : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BAL_PagePermissions PP = new BAL_PagePermissions();
        
        if (Session["SessionBO"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (!IsPostBack)
        {
            //BAL_PagePermissions PP = new BAL_PagePermissions();
            DataTable dtRole = new DataTable();
            SCGL_Session AdSes = (Session["SessionBO"]) as SCGL_Session;
            dtRole = PP.GetPermissionByUserId(SCGL_Common.Convert_ToInt(AdSes.RoleId));
            string pageName = null;
            bool view = false;
            foreach (DataRow dr in dtRole.Rows)
            {
                int row = dtRole.Rows.IndexOf(dr);
                if (dtRole.Rows[row]["Page_Url"].ToString() == "GLMain.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "GLMain.aspx" && view == true)
                {
                    GetOnLoad();
                    ListMain.SelectedIndex = 0;
                    MainCodeSelectedAccount("Select");
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }

            //DataTable dtRole = new DataTable();
            //SCGL_Session AdSes = (Session["SessionBO"]) as SCGL_Session;
            //dtRole=PP.GetPermissionByUserId(SCGL_Common.Convert_ToInt(AdSes.RoleId));
            //if (dtRole.Rows.Count > 0)
            //{
            //    if (dtRole.Rows[0]["Page_Url"].ToString() == "GLMain.aspx")
            //    {
                    
            //    }
            //    else
            //    {
            //        Response.Redirect("Default.aspx",false);
            //    }
            //}
            
        }
        if (Request.QueryString["view"] != null)
        {
            btnAddMain.Enabled = false;
            btnEditMain.Enabled = false;
            btnDeleteMain.Enabled = false;
            btnAddControl.Enabled = false;
        }
    }



    #region Methods
    private void GetOnLoad()
    {
        
        PM.BindDropDown(cmbMainNature, new AccountNature_BAL.AccountNatureBase());
        GLMain_BAL Main = new GLMain_BAL();
        ListMain.DataSource = Main.GetAllMainAccType();
        ListMain.DataTextField = "Title";
        ListMain.DataValueField = "MainCode";
        ListMain.DataBind();
    }

    private void MainCodeSelectedAccount(string Filter)
    {
        txtMainCode.Enabled = false;
        txtMainTitle.Enabled = false;
        lblMainMsg.Text = "";
        cmbMainNature.Enabled = false;
        cmbMainStatus.Enabled = false;
        btnSaveMain.Enabled = false;
        btnCancelMain.Enabled = false;
        if (Filter == "Select")
        {
            //btnEditMain.Enabled = true;
            //btnDeleteMain.Enabled = true;
        }

        btnAddMain.Enabled = true;

        lblMainCode.Text = "";
        ListSubsidary.Items.Clear();
        txtSubMainCode.Text = "";
        txtSubControlCode.Text = "";
        txtSubCode.Text = "";
        txtSubTitle.Text = "";
        cmbSubStatus.SelectedIndex = 0;

        txtControlMainCode.Text = "";
        txtControlCode.Text = "";
        txtControlTitle.Text = "";
        cmbControlStatus.SelectedIndex = 0;
        if (ListMain.SelectedValue != "")
        {
            GetMainAccRecord(Convert.ToInt16(ListMain.SelectedValue));
            GetControlAccByMain(ListMain.SelectedValue);
            btnEditMain.Enabled = true;
            btnDeleteMain.Enabled = true;
        }
        ListControlAcc.Enabled = true;
        lblControlMsg.Text = "";
        btnAddControl.Enabled = true;
        btnEditControl.Enabled = false;
        btnControlDelete.Enabled = false;
        txtControlMainCode.Enabled = false;
        txtControlCode.Enabled = false;
        txtControlTitle.Enabled = false;
        cmbControlStatus.Enabled = false;

        ListSubsidary.Enabled = false;
        lblSubMsg.Text = "";
        btnSubAdd.Enabled = false;
        btnSubEdit.Enabled = false;
        btnSubDelete.Enabled = false;
        txtSubMainCode.Enabled = false;
        txtSubControlCode.Enabled = false;
        txtSubCode.Enabled = false;
        txtSubTitle.Enabled = false;
        cmbSubStatus.Enabled = false;
    }
    private void GetControlAccByMain(string MainCode)
    {

        GLControl_BAL ctrl = new GLControl_BAL();
        ListControlAcc.DataSource = ctrl.GetAllControlMainAccType(MainCode);
        ListControlAcc.DataTextField = "Title";
        ListControlAcc.DataValueField = "ControlCode";
        ListControlAcc.DataBind();
    }
    private void GetMainAccRecord(Int16 MainCode)
    {

        GLMain_BAL main = new GLMain_BAL();
        GLMain_BAL BO = new GLMain_BAL();
        BO = main.GetMainAccType(MainCode);
        txtMainCode.Text = BO.MainCode;
        txtMainTitle.Text = BO.Title;
        cmbMainNature.SelectedValue = BO.Nature.ToString();
        cmbMainStatus.SelectedValue = BO.IsActive.ToString();
        txtControlMainCode.Text = BO.MainCode;
        HidIsMainUpdatable.Value = BO.UnDeleteable.ToString();
        //ModalPopupExtender1.Enabled = false;
    }
    protected void btnSaveMain_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Insert == true)
        {
            InsertUpdateMainAcc();
            GetOnLoad();
            //JQ.showStatusMsg(this, "1", "Record Insert Successfully");
        }
        else
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to Insert New Record");
        }
    }
    private void InsertUpdateMainAcc()
    {
        lblMainCode.Text = "";

        GLMain_BAL main = new GLMain_BAL();
        if (btnSaveMain.Text == "Save")
        {
            if (!main.IsMainCodeExists(txtMainCode.Text) && (txtMainCode.Text != "00" || txtMainCode.Text != ""))
            {
                GLMain_BAL MainBO = new GLMain_BAL();
                MainBO.MainCode = txtMainCode.Text;
                MainBO.Title = txtMainTitle.Text;
                MainBO.Nature = Convert.ToInt32(cmbMainNature.SelectedValue);
                MainBO.IsActive = Convert.ToInt16(cmbMainStatus.SelectedValue);
                MainBO.ActiveChild = false;
                main.InsertUpdateGLMain(MainBO, (SCGL_Session)Session["SessionBO"]);
                JQ.showStatusMsg(this, "1", "Record Insert Successfully");
            }
            else
            {
                lblMainCode.Text = "Maincode already exists!";
            }
        }
        else if (btnSaveMain.Text == "Update" && (txtMainCode.Text != "00" || txtMainCode.Text != ""))
        {
            GLMain_BAL MainBO = new GLMain_BAL();
            MainBO.MainCode = txtMainCode.Text;
            //MainBO.MainCodeWhere = ListMain.SelectedValue;
            MainBO.Title = txtMainTitle.Text;
            MainBO.Nature = Convert.ToInt32(cmbMainNature.SelectedValue);
            MainBO.IsActive = Convert.ToInt16(cmbMainStatus.SelectedValue);
            MainBO.ActiveChild = lblStatus.Text.Equals("") ? false : Convert.ToBoolean(lblStatus.Text);
            main.InsertUpdateGLMain(MainBO, (SCGL_Session)Session["SessionBO"]);
            JQ.showStatusMsg(this, "1", "Record Update Successfully");
        }
    }

    #endregion
    protected void ListMain_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1300);
        MainCodeSelectedAccount("Select");
    }
    protected void btnEditMain_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        
        if (SBO.Can_Update == true)
        {
            EditMainAccount();
        }
        else
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to Update Record");
        }
    }

    private void EditMainAccount()
    {
        if (HidIsMainUpdatable.Value != "")
        {
            if (Convert.ToBoolean(HidIsMainUpdatable.Value))
            {
                lblMainErrMsg.Text = "Main Code is predefined not editable";
                JQ.showDialog(this, "Msg");
            }
            else
            {
                txtMainTitle.Enabled = true;
                cmbMainNature.Enabled = true;
                cmbMainStatus.Enabled = true;
                btnSaveMain.Enabled = true;
                btnCancelMain.Enabled = true;
                btnSaveMain.Text = "Update";

                ListControlAcc.Enabled = false;
                txtControlCode.Enabled = false;
                txtControlTitle.Enabled = false;
                cmbControlStatus.Enabled = false;
                btnAddControl.Enabled = false;
                btnEditControl.Enabled = false;
                btnSaveControl.Enabled = false;
                btnCancelControl.Enabled = false;

                ListSubsidary.Enabled = false;
                txtSubCode.Enabled = false;
                txtSubTitle.Enabled = false;
                cmbSubStatus.Enabled = false;
                btnSubAdd.Enabled = false;
                btnSubEdit.Enabled = false;
                btnSubSave.Enabled = false;
                btnSubCancel.Enabled = false;
            }
        }

       // GetMainAccountz();
    }
    protected void btnDeleteMain_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Delete == true)
        {
            if (HidIsMainUpdatable.Value != "")
            {
                if (Convert.ToBoolean(HidIsMainUpdatable.Value))
                {
                    lblMainErrMsg.Text = "Main Code is predefined not Deletable";
                    JQ.showDialog(this, "Msg");
                }
                else
                {
                    lblDeleteMsg.Text = "Are you sure you want to delete ? ";
                    JQ.showDialog(this, "Confirmation");
                }
            }
        }
        else
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to Delete Record");
        }
    }

    protected void btnAddControl_Click(object sender, EventArgs e)
    {
        NewControlAccount();
    }


    private void NewControlAccount()
    {
        txtControlCode.Text = "";
        txtControlTitle.Text = "";
        cmbControlStatus.SelectedIndex = 0;
        txtControlCode.Enabled = true;
        txtControlTitle.Enabled = true;
        cmbControlStatus.Enabled = true;

        btnSaveControl.Enabled = true;
        btnCancelControl.Enabled = true;
        btnEditControl.Enabled = false;
        btnControlDelete.Enabled = false;
        btnSaveControl.Text = "Save";

        ListMain.Enabled = false;
        txtMainCode.Enabled = false;
        txtMainTitle.Enabled = false;
        cmbMainStatus.Enabled = false;
        btnAddMain.Enabled = false;
        btnEditMain.Enabled = false;
        btnDeleteMain.Enabled = false;
        btnSaveMain.Enabled = false;
        btnCancelMain.Enabled = false;

        ListSubsidary.Enabled = false;
        txtSubCode.Enabled = false;
        txtSubTitle.Enabled = false;
        cmbSubStatus.Enabled = false;
        btnSubAdd.Enabled = false;
        btnSubEdit.Enabled = false;
        btnSubSave.Enabled = false;
        btnSubCancel.Enabled = false;
    }

    protected void btnEditControl_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Update == true)
        {
            EditControlAccount();
        }
        else
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to Insert New Record");
        }
    }

    private void EditControlAccount()
    {
        if (cmbMainStatus.SelectedValue == "1")
        {
            //txtControlCode.Enabled = true;
            txtControlTitle.Enabled = true;
            cmbControlStatus.Enabled = true;
            btnSaveControl.Enabled = true;
            btnCancelControl.Enabled = true;
            btnSaveControl.Text = "Update";

            ListMain.Enabled = false;
            txtMainCode.Enabled = false;
            txtMainTitle.Enabled = false;
            cmbMainStatus.Enabled = false;
            btnAddMain.Enabled = false;
            btnEditMain.Enabled = false;
            btnSaveMain.Enabled = false;
            btnCancelMain.Enabled = false;

            ListSubsidary.Enabled = false;
            txtSubCode.Enabled = false;
            txtSubTitle.Enabled = false;
            cmbSubStatus.Enabled = false;
            btnSubAdd.Enabled = false;
            btnSubEdit.Enabled = false;
            btnSubSave.Enabled = false;
            btnSubCancel.Enabled = false;
        }
        else
        {
            lblMainErrMsg.Text = "Main A/c is InActive Not Editable";
            JQ.showDialog(this, "Msg");
        }
    }
    protected void lbtnYes_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        lbtnNo.Text = "Ok";
        lbtnYes.Visible = false;
        GLMain_BAL Main = new GLMain_BAL();
        lblDeleteMsg.Text = PM.GetMsgListFromMsgTable(Main.DeleteMainCodeNotInUse(ListMain.SelectedValue));
        JQ.showDialog(this, "Msg");
        lblMainErrMsg.Text = "Main Record Deleted Sucessfully";
        JQ.closeDialog(this, "Confirmation");
        GetOnLoad();
    }


   
    protected void lbtnConYes_Click(object sender, EventArgs e)
    {
        lblControlMsg.Text = "";

        GLControl_BAL Control = new GLControl_BAL();
        if (Control.DeleteControlCodeNotInUse(ListMain.SelectedValue, ListControlAcc.SelectedValue) == 0)
        {
            lblControlMsg.Text = "Can not delete Control Code in use";
        }
        else
        {
            JQ.closeDialog(this, "ControlConfirmation");
            JQ.showDialog(this, "Msg");
            lblMainErrMsg.Text = "Control Record Deleted Sucessfully ";
            GetControlAccByMain(ListMain.SelectedValue);

        }
        txtControlCode.Text = "";
        txtControlTitle.Text = "";

    }
    protected void lnkSubYes_Click(object sender, EventArgs e)
    {
        if (ListMain.SelectedValue == "04" && ListControlAcc.SelectedValue=="004")
        {
            lblSubMsg.Text = "";
            txtSubCode.Text = "";
            txtSubTitle.Text = "";
            JQ.closeDialog(this, "SubsidaryConfirmation");
            JQ.showDialog(this, "Msg");
            lblMainErrMsg.Text = "Can not delete Control Code in use";
        }
        else
        {
            lblSubMsg.Text = "";
            GLSubsidiary_BAL Sub = new GLSubsidiary_BAL();
            Sub.DeleteSubsidaryCodeNotInUse(ListMain.SelectedValue, ListControlAcc.SelectedValue, ListSubsidary.SelectedValue);
            FillSubListByMainNControl();
            txtSubCode.Text = "";
            txtSubTitle.Text = "";
            JQ.closeDialog(this, "SubsidaryConfirmation");
            JQ.showDialog(this, "Msg");
            lblMainErrMsg.Text = "Subsidary Record Deleted Succesfully";
        }
        
    }


    protected void btnControlDelete_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Delete == true)
        {
            if (hdnControlDeleteable.Value == "False")
            {
                lblControlMsg.Text = "Can not delete ControlCode in use";
            }
            else
            {
                JQ.showDialog(this, "ControlConfirmation");
                lblContDelete.Text = "Are you sure you want to delete ? ";
            }
        }
        else
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to Delete Record");
        }
    }
    protected void btnAddMain_Click(object sender, EventArgs e)
    {
        NewMainAccount();
    }
    private void NewMainAccount()
    {
        txtMainCode.Text = "";
        txtMainTitle.Text = "";
        lblStatus.Text = "";
        cmbMainNature.SelectedIndex = 0;
        cmbMainStatus.SelectedIndex = 0;
        txtMainCode.Enabled = true;
        txtMainTitle.Enabled = true;
        cmbMainNature.Enabled = true;
        cmbMainStatus.Enabled = true;
        btnSaveMain.Enabled = true;
        btnCancelMain.Enabled = true;
        btnEditMain.Enabled = false;
        btnDeleteMain.Enabled = false;
        btnSaveMain.Text = "Save";

        ListControlAcc.Enabled = false;
        txtControlCode.Enabled = false;
        txtControlTitle.Enabled = false;
        cmbControlStatus.Enabled = false;
        btnAddControl.Enabled = false;
        btnEditControl.Enabled = false;
        btnControlDelete.Enabled = false;
        btnSaveControl.Enabled = false;
        btnCancelControl.Enabled = false;

        ListSubsidary.Enabled = false;
        txtSubCode.Enabled = false;
        txtSubTitle.Enabled = false;
        cmbSubStatus.Enabled = false;
        btnSubAdd.Enabled = false;
        btnSubEdit.Enabled = false;
        btnSubSave.Enabled = false;
        btnSubCancel.Enabled = false;
    }
    protected void btnCancelMain_Click(object sender, EventArgs e)
    {
        MainCodeSelectedAccount("Cancel");
    }


    protected void ListControlAcc_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1300);
        ControlSelectedAccount("Select");
    }
    private void ControlSelectedAccount(string Filter)
    {
        txtControlCode.Enabled = false;
        txtControlTitle.Enabled = false;
        cmbControlStatus.Enabled = false;
        btnSaveControl.Enabled = false;
        btnCancelControl.Enabled = false;
        lblControlMsg.Text = "";
        if (Filter == "Select")
        {
            //btnEditControl.Enabled = true;
            //btnControlDelete.Enabled = true;
        }
        btnAddControl.Enabled = true;

        lblSub.Text = "";
        ListSubsidary.Items.Clear();
        txtSubMainCode.Text = "";
        txtSubControlCode.Text = "";
        txtSubCode.Text = "";
        txtSubTitle.Text = "";
        cmbSubStatus.SelectedIndex = 0;

        //txtMainCode.Text = "";
        //txtMainTitle.Text = "";
        //cmbMainNature.SelectedIndex = 0;
        //cmbMainStatus.SelectedIndex = 0;
        if (ListMain.SelectedValue != "" && ListControlAcc.SelectedValue != "")
        {
            GetControlAccRecord(ListMain.SelectedValue, ListControlAcc.SelectedValue);
            FillSubListByMainNControl();
            btnEditControl.Enabled = true;
            btnControlDelete.Enabled = true;
        }

        ListMain.Enabled = true;
        btnAddMain.Enabled = true;
        btnEditMain.Enabled = false;
        btnDeleteMain.Enabled = false;
        lblMainMsg.Text = "";
        //btnSaveMain.Enabled = true;
        //btnCancelMain.Enabled = true;

        ListSubsidary.Enabled = true;
        btnSubAdd.Enabled = true;
        btnSubEdit.Enabled = false;
        btnSubDelete.Enabled = false;
        lblSubMsg.Text = "";
        //btnSubSave.Enabled = true;
        //btnSubCancel.Enabled = true;

        txtSubMainCode.Text = txtControlMainCode.Text;
        txtSubControlCode.Text = txtControlCode.Text;
    }

    private void FillSubListByMainNControl()
    {
        GLSubsidiary_BAL sub = new GLSubsidiary_BAL();
        ListSubsidary.DataSource = sub.GetAllSubsidiaryByMainNControl(ListMain.SelectedValue, ListControlAcc.SelectedValue);
        ListSubsidary.DataTextField = "Title";
        ListSubsidary.DataValueField = "SubsidaryCode";
        ListSubsidary.DataBind();
    }

    private void GetControlAccRecord(string MainCode, string ControlCode)
    {

        GLControl_BAL CtrlBLL = new GLControl_BAL();
        GLControl_BAL CtrlBO = CtrlBLL.GetControlAccType(MainCode, ControlCode);
        txtControlMainCode.Text = CtrlBO.MainCode;
        txtControlCode.Text = CtrlBO.ControlCode;
        txtControlTitle.Text = CtrlBO.Title;
        cmbControlStatus.SelectedValue = CtrlBO.IsActive.ToString();
        hdnControlDeleteable.Value = CtrlBO.Deleteable.ToString();
    }

    protected void btnCancelControl_Click(object sender, EventArgs e)
    {
        ControlSelectedAccount("Cancel");
    }

    protected void btnSubAdd_Click(object sender, EventArgs e)
    {
        NewSubAccount();
    }

    private void NewSubAccount()
    {
        txtSubCode.Text = "";
        txtSubTitle.Text = "";
        cmbSubStatus.SelectedIndex = 0;
        txtSubCode.Enabled = true;
        txtSubTitle.Enabled = true;
        cmbSubStatus.Enabled = true;

        btnSubSave.Enabled = true;
        btnSubCancel.Enabled = true;
        btnSubEdit.Enabled = false;
        btnSubDelete.Enabled = false;
        btnDeleteMain.Enabled = false;
        btnSubSave.Text = "Save";

        ListMain.Enabled = false;
        txtMainCode.Enabled = false;
        txtMainTitle.Enabled = false;
        cmbControlStatus.Enabled = false;
        btnAddMain.Enabled = false;
        btnEditMain.Enabled = false;
        btnDeleteMain.Enabled = false;
        btnSaveMain.Enabled = false;
        btnCancelMain.Enabled = false;

        ListControlAcc.Enabled = false;
        txtControlCode.Enabled = false;
        txtControlTitle.Enabled = false;
        cmbControlStatus.Enabled = false;
        btnAddControl.Enabled = false;
        btnEditControl.Enabled = false;
        btnControlDelete.Enabled = false;
        btnSaveControl.Enabled = false;
        btnCancelControl.Enabled = false;
    }

    protected void btnSaveControl_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Insert == true)
        {
            InsertUpdateControlAcc();
            GetControlAccByMain(ListMain.SelectedValue);
            //JQ.showStatusMsg(this, "1", "Record Insert Successfully");
        }
        else
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to Insert New Record");
        }
    }

    private void InsertUpdateControlAcc()
    {
        lblControl.Text = "";
        GLControl_BAL Control = new GLControl_BAL();
        if (btnSaveControl.Text == "Save")
        {
            if (!Control.IsControlCodeExists(txtControlMainCode.Text, txtControlCode.Text))
            {
                GLControl_BAL ControlBO = new GLControl_BAL();
                ControlBO.MainCode = txtControlMainCode.Text;
                ControlBO.ControlCode = txtControlCode.Text; //txtControlCode.Text;
                ControlBO.Title = txtControlTitle.Text;
                ControlBO.IsActive = Convert.ToInt16(cmbControlStatus.SelectedValue);
                Control.InsertUpdateGLControl(ControlBO, (SCGL_Session)Session["SessionBO"]);
                JQ.showStatusMsg(this, "1", "Record Insert Successfully");
            }
            else
            {
                lblControl.Text = "Controlcode already exists!";
            }
        }
        else if (btnSaveControl.Text == "Update")
        {
            GLControl_BAL ControlBO = new GLControl_BAL();
            ControlBO.MainCode = txtControlMainCode.Text;
            ControlBO.ControlCode = txtControlCode.Text;
            ControlBO.Title = txtControlTitle.Text;
            ControlBO.IsActive = Convert.ToInt16(cmbControlStatus.SelectedValue);
            Control.InsertUpdateGLControl(ControlBO, (SCGL_Session)Session["SessionBO"]);
            JQ.showStatusMsg(this, "1", "Record Update Successfully");
        }
    }
    protected void ListSubsidary_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1300);
        SubSelectAccount("Select");

    }

    private void SubSelectAccount(string Filter)
    {
        txtSubCode.Enabled = false;
        txtSubTitle.Enabled = false;
        cmbSubStatus.Enabled = false;
        btnSubSave.Enabled = false;
        lblSubMsg.Text = "";
        if (Filter == "Select")
        {
            //btnSubEdit.Enabled = true;
            //btnSubDelete.Enabled = true;
        }
        btnSubAdd.Enabled = true;
        btnSubCancel.Enabled = false;
        //lblSub.Text = "";
        //ListSubsidary.Items.Clear();
        txtSubMainCode.Text = "";
        txtSubControlCode.Text = "";
        txtSubCode.Text = "";
        txtSubTitle.Text = "";
        //cmbSubStatus.SelectedIndex = 0;

        //txtMainCode.Text = "";
        //txtMainTitle.Text = "";
        //cmbMainNature.SelectedIndex = 0;
        //cmbMainStatus.SelectedIndex = 0;
        if (ListMain.SelectedValue != "" && ListControlAcc.SelectedValue != "" && ListSubsidary.SelectedValue != "")
        {
            GetSubAccRecord(ListMain.SelectedValue, ListControlAcc.SelectedValue, ListSubsidary.SelectedValue);
            btnSubEdit.Enabled = true;
            btnSubDelete.Enabled = true;
        }

        ListMain.Enabled = true;
        btnAddMain.Enabled = true;
        btnEditMain.Enabled = false;
        btnDeleteMain.Enabled = false;
        lblMainMsg.Text = "";

        ListControlAcc.Enabled = true;
        btnAddControl.Enabled = true;
        btnEditControl.Enabled = false;
        btnControlDelete.Enabled = false;
        lblControlMsg.Text = "";

        txtSubMainCode.Text = txtControlMainCode.Text;
        txtSubControlCode.Text = txtControlCode.Text;
    }

    private void GetSubAccRecord(string MainCode, string ControlCode, string SubCode)
    {
        txtSubCode.Enabled = false;
        txtSubTitle.Enabled = false;
        cmbSubStatus.Enabled = false;
        btnSubSave.Enabled = false;
        GLSubsidiary_BAL subBL = new GLSubsidiary_BAL();
        GLSubsidiary_BAL SubBO = subBL.GetSubsidiaryAccType(MainCode, ControlCode, SubCode);
        txtSubMainCode.Text = SubBO.MainCode;
        txtSubControlCode.Text = SubBO.ControlCode;
        txtSubCode.Text = SubBO.SubsidaryCode;
        txtSubTitle.Text = SubBO.Title;
        cmbSubStatus.SelectedValue = SubBO.IsActive.ToString();
        hdnSubsidaryDeleteable.Value = SubBO.Deleteable.ToString();
    }


    protected void btnSubEdit_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Insert == true)
        {
            EditSubAccount();
        }
        else
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to Update Record");
        }
    }

    private void EditSubAccount()
    {
        if (cmbControlStatus.SelectedValue == "1")
        {
            //txtSubCode.Enabled = true;
            txtSubTitle.Enabled = true;
            cmbSubStatus.Enabled = true;
            btnSubSave.Enabled = true;
            btnSubCancel.Enabled = true;
            btnSubSave.Text = "Update";

            ListMain.Enabled = false;
            txtMainCode.Enabled = false;
            txtMainTitle.Enabled = false;
            cmbMainStatus.Enabled = false;
            btnAddMain.Enabled = false;
            btnEditMain.Enabled = false;
            btnSaveMain.Enabled = false;
            btnCancelMain.Enabled = false;

            ListControlAcc.Enabled = false;
            txtControlCode.Enabled = false;
            txtControlTitle.Enabled = false;
            cmbControlStatus.Enabled = false;
            btnAddControl.Enabled = false;
            btnEditControl.Enabled = false;
            btnSaveControl.Enabled = false;
            btnCancelControl.Enabled = false;
        }
        else
        {
            lblMainErrMsg.Text = "Control A/c is InActive Not Editable";
            JQ.showDialog(this, "Msg");
        }
    }


    protected void btnSubDelete_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Delete == true)
        {
            if (hdnSubsidaryDeleteable.Value == "False")
            {
                lblSubMsg.Text = "Can not delete SubsidaryCode in use";
            }
            else
            {
                JQ.showDialog(this, "SubsidaryConfirmation");
                lblSubsDelete.Text = "Are you sure you want to delete ?";
            }
        }
        else
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to Delete Record");
        }
    }

    protected void btnSubSave_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Insert == true)
        {
            InsertUpdateSubsidiaryAcc();
            FillSubListByMainNControl();
            //JQ.showStatusMsg(this, "1", "Record Insert Successfully");
        }
        else
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to Insert New Record");
        }
    }

    private void InsertUpdateSubsidiaryAcc()
    {
        lblSub.Text = "";
        GLSubsidiary_BAL Sub = new GLSubsidiary_BAL();
        if (btnSubSave.Text == "Save")
        {
            if (!Sub.IsExistsSubsidiaryAccount(txtSubMainCode.Text, txtSubControlCode.Text, txtSubCode.Text))
            {
                GLSubsidiary_BAL SubBO = new GLSubsidiary_BAL();
                SubBO.MainCode = txtSubMainCode.Text;
                SubBO.ControlCode = txtSubControlCode.Text;
                //SubBO.SubsidaryCodeWhere = txtSubCode.Text;
                SubBO.SubsidaryCode = txtSubCode.Text;
                SubBO.Title = txtSubTitle.Text;
                SubBO.IsActive = Convert.ToInt16(cmbSubStatus.SelectedValue);
                Sub.InsertUpdateSubsidiary(SubBO, (SCGL_Session)Session["SessionBO"]);
                JQ.showStatusMsg(this, "1", "Record Insert Successfully");
            }
            else
            {
                lblSub.Text = "Account already exists!";
            }
        }
        else if (btnSubSave.Text == "Update")
        {
            GLSubsidiary_BAL SubBO = new GLSubsidiary_BAL();
            SubBO.MainCode = txtSubMainCode.Text;
            SubBO.ControlCode = txtSubControlCode.Text;
            //SubBO.SubsidaryCodeWhere = ListSubsidary.SelectedValue;
            SubBO.SubsidaryCode = txtSubCode.Text;
            SubBO.Title = txtSubTitle.Text;
            SubBO.IsActive = Convert.ToInt16(cmbSubStatus.SelectedValue);
            Sub.InsertUpdateSubsidiary(SubBO, (SCGL_Session)Session["SessionBO"]);
            JQ.showStatusMsg(this, "1", "Record Update Successfully");
        }
    }
    protected void btnSubCancel_Click(object sender, EventArgs e)
    {
        SubSelectAccount("Cancel");
    }

   
}
