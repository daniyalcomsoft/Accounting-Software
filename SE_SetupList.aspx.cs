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


public partial class SE_SetupList : System.Web.UI.Page
{
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "SE_SetupList.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "SE_SetupList.aspx" && view == true)
                {
                    GridSetup.DataSource = SqlDataSource1;
                    GridSetup.DataBind();
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
            
        }
    }
    protected void LbtnView_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_View == true)
        {
            var row = (GridViewRow)((Control)sender).NamingContainer;
            int index = row.RowIndex;
            Label LblSetupID = (Label)GridSetup.Rows[index].FindControl("LblSetupID");
            int view = 1;
            if (LblSetupID.Text != "")
            {
                Response.Redirect("SE_SuperAdminSetup.aspx?SetupID=" + LblSetupID.Text + "&view=" + view);
            }
        }
        else
        { JQ.showStatusMsg(this, "3", "User not Allowed to View Record"); }
    }
    protected void lbtnEdit_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Update == true)
        {
            var row = (GridViewRow)((Control)sender).NamingContainer;
            int index = row.RowIndex;
            Label LblSetupID = (Label)GridSetup.Rows[index].FindControl("LblSetupID");
            if (LblSetupID.Text != "")
            {
                Response.Redirect("SE_SuperAdminSetup.aspx?SetupID=" + LblSetupID.Text + "");
            }
        }
        else
        { JQ.showStatusMsg(this, "3", "User not Allowed to Update Record"); }
    }
    protected void lbtnDelete_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Delete == true)
        {
            var row = (GridViewRow)((Control)sender).NamingContainer;
            int index = row.RowIndex;
            ViewState["Index"] = index;
            Label LabelSetupID = ((Label)GridSetup.Rows[index].FindControl("LabelSetupID"));
            lblDeleteMsg.Text = "Are you sure to want to Delete Voucher # [ " + LabelSetupID.Text + " ] ?";
            lbtnYes.Visible = true;
            lbtnNo.Text = "No";
            JQ.showDialog(this, "Confirmation");
        }
        else
        { JQ.showStatusMsg(this, "3", "User not Allowed to Delete Record"); }
    }
    protected void lbtnYes_Click(object sender, EventArgs e)
    {
        if (ViewState["Index"] != null)
        {
            int Index = Convert.ToInt32(ViewState["Index"]);
            Label LabelSetupID = ((Label)GridSetup.Rows[Index].FindControl("LabelSetupID"));
            JQ.closeDialog(this, "Confirmation");
        }
    }
}
