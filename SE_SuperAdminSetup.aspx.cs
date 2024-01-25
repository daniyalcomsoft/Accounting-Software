using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SW.SW_Common;


public partial class SE_SuperAdminSetup : System.Web.UI.Page
{
    SuperAdmin_BAL BLL = new SuperAdmin_BAL();
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
                    if (dtRole.Rows[row]["Page_Url"].ToString() == "SE_SuperAdminSetup.aspx")
                    {
                        pageName = dtRole.Rows[row]["Page_Url"].ToString();
                        view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                        break;
                    }
                }
                if (dtRole.Rows.Count > 0)
                {
                    if (pageName == "SE_SuperAdminSetup.aspx" && view == true)
                    {
                        GetData();
                    }
                    else
                    {
                        Response.Redirect("Default.aspx", false);
                    }
                }
               
            }   
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (HiddenSetupID.Value == "")
        {
            if (SBO.Can_Insert == true)
            {
                CreateModifySuperAdmin();
            }
            else
            { JQ.showStatusMsg(this, "3", "User not Allowed to Insert New Record"); }
        }
        else
        {
            if (SBO.Can_Update == true)
            {
                CreateModifySuperAdmin();
            }
            else
            { JQ.showStatusMsg(this, "3", "User not Allowed to Update Record"); }
        }
    }
    private void CreateModifySuperAdmin()
    {
        SuperAdmin_BAL BO = new SuperAdmin_BAL();
        if (HiddenSetupID.Value != "")
        {
            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            DateTime Date =Convert.ToDateTime(DateTime.Now.ToShortDateString());
            BO.Description = txtDescription.Text;
            BO.SiteCode = txtSiteCode.Text;
            BO.SiteName = txtSiteName.Text;
            //BO.IsActive = Convert.ToInt16(chkActive.Checked);
            BO.SetupID = Convert.ToInt32(HiddenSetupID.Value);
            BO.CreatedDate = Date;
            BO.Address = txtContactAddress.Text;
            BO.ContactNumber = txtContactPhone.Text;
            BO.ContactPerson = txtContactPerson.Text;
            BO.ContactPersonDesg = txtDesignation.Text;
            BO.ContactPersonEmail = txtEmail.Text;
            BO.CustomAgent = txtCustomAgent.Text;
            BO.SNTN = txtSNTN.Text;
            BO.SalesTaxRegNo = txtSalesTaxRegNo.Text;
            BO.CreatedBy = SBO.UserID;
            BO.CreatedDate = Date;
            BO.Mod_Financials = Convert.ToInt16(ChkFinancials.Checked);
            BO.Mod_DepositAccount = Convert.ToInt16(ChkDepAccount.Checked);
            BO.Mod_TermDeposit = Convert.ToInt16(ChkTermDep.Checked);
            BO.Mod_Loan = Convert.ToInt16(ChkLoan.Checked);
            BLL.CreateModifySetup(BO, (SCGL_Session)Session["SessionBO"]);
            lblMainErrMsg.Text = "Setup Update Successfully";
            JQ.showDialog(this, "Msg"); ;

        }
        else
        {
            int SetupID = 0;
            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            DateTime Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            BO.Description = txtDescription.Text;
            BO.SiteCode = txtSiteCode.Text;
            BO.SiteName = txtSiteName.Text;
            //BO.IsActive = Convert.ToInt16(chkActive.Checked);
            BO.SetupID = Convert.ToInt32(HiddenSetupID.Value);
            BO.CreatedDate = Date;
            BO.Address = txtContactAddress.Text;
            BO.ContactNumber = txtContactPhone.Text;
            BO.ContactPerson = txtContactPerson.Text;
            BO.ContactPersonDesg = txtDesignation.Text;
            BO.ContactPersonEmail = txtEmail.Text;
            BO.CustomAgent = txtCustomAgent.Text;
            BO.SNTN = txtSNTN.Text;
            BO.SalesTaxRegNo = txtSalesTaxRegNo.Text;
            BO.CreatedBy = SBO.UserID;
            BO.CreatedDate = Date;
            BO.Mod_Financials = Convert.ToInt16(ChkFinancials.Checked);
            BO.Mod_DepositAccount = Convert.ToInt16(ChkDepAccount.Checked);
            BO.Mod_TermDeposit = Convert.ToInt16(ChkTermDep.Checked);
            BO.Mod_Loan = Convert.ToInt16(ChkLoan.Checked);
            BO.SetupID = SetupID;
            BLL.CreateModifySetup(BO, (SCGL_Session)Session["SessionBO"]);
            lblMainErrMsg.Text = "Setup Saved Successfully";
            JQ.showDialog(this, "Msg");
        }
    }
    private void GetData()
    {
        SuperAdmin_BAL BO = new SuperAdmin_BAL();
        DataTable dt = new DataTable();
        dt = BLL.SelectSetupInfoBySetupID(BO, (SCGL_Session)Session["SessionBO"]);
        if (dt.Rows.Count > 0)
        {
            txtDescription.Text = dt.Rows[0]["Description"].ToString();
            txtSiteCode.Text = dt.Rows[0]["SiteCode"].ToString();
            txtSiteName.Text = dt.Rows[0]["SiteName"].ToString();
            HiddenSetupID.Value = dt.Rows[0]["SetupID"].ToString();
            txtEmail.Text = dt.Rows[0]["ContactPersonEmail"].ToString();
            txtDesignation.Text = dt.Rows[0]["ContactPersonDesg"].ToString();
            txtContactPerson.Text = dt.Rows[0]["ContactPerson"].ToString();
            txtContactPhone.Text = dt.Rows[0]["ContactNumber"].ToString();
            txtContactAddress.Text = dt.Rows[0]["Address"].ToString();
            txtCustomAgent.Text = dt.Rows[0]["CustomAgent"].ToString();
            txtSNTN.Text = dt.Rows[0]["SNTN"].ToString();
            txtSalesTaxRegNo.Text = dt.Rows[0]["SalesTaxRegNo"].ToString();
            if (dt.Rows[0]["Mod_Financials"] != null && dt.Rows[0]["Mod_Loan"] != null || dt.Rows[0]["Mod_DepositAccount"] != null && dt.Rows[0]["Mod_TermDeposit"] != null)
            {
                ChkFinancials.Checked = Convert.ToBoolean(dt.Rows[0]["Mod_Financials"]);
                ChkDepAccount.Checked = Convert.ToBoolean(dt.Rows[0]["Mod_DepositAccount"]);
                ChkTermDep.Checked = Convert.ToBoolean(dt.Rows[0]["Mod_TermDeposit"]);
                ChkLoan.Checked = Convert.ToBoolean(dt.Rows[0]["Mod_Loan"]);
            }
            else
            {
                ChkFinancials.Checked = false;
                ChkDepAccount.Checked = false;
                ChkTermDep.Checked = false;
                ChkLoan.Checked = false;
            }
        }
    }
    protected void LnkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("SE_SetupList.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
}
