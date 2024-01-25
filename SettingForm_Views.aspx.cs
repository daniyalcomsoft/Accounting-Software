using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SW.SW_Common;
using System.Data;

public partial class SettingForm_Views : System.Web.UI.Page
{
    Setting_BAL BLL = new Setting_BAL();
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "SettingForm_Views.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "SettingForm_Views.aspx" && view == true)
                {
                    GridSettingView.DataSource = BLL.GetSetingData();
                    GridSettingView.DataBind();
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
            
        }
    }
    protected void lbtnEdit_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Update == true)
        {
            Response.Redirect("SettingForm.aspx?Id=" + e.CommandArgument.ToString());
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
            Response.Redirect("SettingForm.aspx?Id=" + e.CommandArgument.ToString() + "&view=" + view);
        }
        else
        { JQ.showStatusMsg(this, "3", "User not Allowed to View Record"); }
    }
}
