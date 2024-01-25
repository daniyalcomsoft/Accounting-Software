using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using SW.SW_Common;
using System.Data;


public partial class SE_AdminUsers : System.Web.UI.Page
{
    User_BAL BLL = new User_BAL();

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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "SE_AdminUsers.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "SE_AdminUsers.aspx" && view == true)
                {
                    GridUser.DataSource = BLL.GetAllUserInfo();
                    GridUser.DataBind();
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
            
        }
    }
    protected void LbtnEdit_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Update == true)
        {
          string UserID_encrypt = Uri.EscapeDataString(EncryptDecrypt.Encrypt(e.CommandArgument.ToString()));
          Response.Redirect("CreateUser.aspx?UserID=" +UserID_encrypt);
        }
        else
        { JQ.showStatusMsg(this, "3", "User not Allowed to Update Record"); }
    }
    protected void lbtnView_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_View == true)
        {
            int view = 1;
            string UserID_encrypt = Uri.EscapeDataString(EncryptDecrypt.Encrypt(e.CommandArgument.ToString()));
            Response.Redirect("CreateUser.aspx?UserID=" + UserID_encrypt + "&view=" + view);
        }
        else
        { JQ.showStatusMsg(this, "3", "User not Allowed to View Record"); }
    }
    protected void lbtnDelete_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Delete == true)
        {
            if (e.CommandName == "Del")
            {
                //string UserID_encrypt = Uri.EscapeDataString(EncryptDecrypt.Encrypt(e.CommandArgument.ToString()));
                int a = BLL.DeleteUser(Convert.ToInt32(e.CommandArgument.ToString()));
                if (a != 0)
                {
                    BLL.DeleteUser(Convert.ToInt32(e.CommandArgument.ToString()));
                    GridUser.DataSource = null;
                    GridUser.DataSource = BLL.GetAllUserInfo();
                    GridUser.DataBind();
                    JQ.showStatusMsg(this, "1", "Record Successfully Delete");
                }
                else
                {
                    JQ.showStatusMsg(this, "3", "Can not delete this record");
                }
            }
        }
        else
        { JQ.showStatusMsg(this, "3", "User not Allowed to Delete Record"); }

    }
    protected void GridUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridUser.DataSource = BLL.GetAllUserInfo();
        GridUser.PageIndex = e.NewPageIndex;
        GridUser.DataBind();
    }
    protected void btnNewUser_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateUser.aspx");
    }
}
