using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SW.SW_Common;


public partial class GLHome : System.Web.UI.Page
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "GLHome.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "GLHome.aspx" && view == true)
                {
                    FillVoucherTypeList();
                    SetVoucherGrid("0");
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
           
        }
    }
    #region Methods
    private void FillVoucherTypeList()
    {
        GL_BAL GL = new GL_BAL();
        cmbTransactionFilter.DataSource = GL.GetVoucherType();
        cmbTransactionFilter.DataTextField = "VoucherTypeName";
        cmbTransactionFilter.DataValueField = "VoucherTypeID";
        cmbTransactionFilter.DataBind();
    }
    private void SetVoucherGrid(string VoucherTypeID)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        GL_BAL GL = new GL_BAL();
        GridVoucher.DataSource = GL.GetVoucherByVoucherTypeID(VoucherTypeID,FinYearID);
        GridVoucher.DataBind();
    }
    #endregion
    protected void cmbAddNewVoucher_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbAddNewVoucher.SelectedValue == "1")
        {

            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            if (SBO != null)
            {
                DataRow[] dr = SBO.PermissionTable.Select("Page_Url='GLGeneralVoucher.aspx'");
                if (dr.Length > 0)
                {
                    if (Convert.ToBoolean(dr[0]["Can_Insert"]) == true)
                    {
                        Response.Redirect("GLGeneralVoucher.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('you do not have rights to Insert new Record ');</script>");
                    }
                }
            }
        }
        if (cmbAddNewVoucher.SelectedValue == "3")
        {
            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            if (SBO != null)
            {
                DataRow[] dr = SBO.PermissionTable.Select("Page_Url='GLCashRecievedVoucher.aspx'");
                if (dr.Length > 0)
                {
                    if (Convert.ToBoolean(dr[0]["Can_Insert"]) == true)
                    {
                        Response.Redirect("GLCashRecievedVoucher.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('you do not have rights to Insert new Record ');</script>");
                    }
                }
            }
        }
        if (cmbAddNewVoucher.SelectedValue == "2")
        {
            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            if (SBO != null)
            {
                DataRow[] dr = SBO.PermissionTable.Select("Page_Url='GLCashPaymentVoucher.aspx'");
                if (dr.Length > 0)
                {
                    if (Convert.ToBoolean(dr[0]["Can_Insert"]) == true)
                    {
                        Response.Redirect("GLCashPaymentVoucher.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('you do not have rights to Insert new Record ');</script>");
                    }
                }
            }
        }
    }


    protected void GridVoucher_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
        }
    }

    protected void cmbTransactionFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbTransactionFilter.SelectedValue != "")
        {
            SetVoucherGrid(cmbTransactionFilter.SelectedValue);
        }
    }

    protected void lbtnEdit_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        var row = (GridViewRow)((Control)sender).NamingContainer;
        int index = row.RowIndex;
        Label lblVoucherTypeID = (Label)GridVoucher.Rows[index].FindControl("lblVoucherTypeID");
        Label lblVoucherNumber = (Label)GridVoucher.Rows[index].FindControl("lblVoucherNumber");
        if (SBO.Can_Update == true)
        {
            if (Convert.ToInt32(lblVoucherTypeID.Text) == (int)PM.VoucherType.General_Voucher)
            {
                Response.Redirect("GLGeneralVoucher.aspx?VoucherNo=" + lblVoucherNumber.Text);
            }
            if (Convert.ToInt32(lblVoucherTypeID.Text) == (int)PM.VoucherType.Cash_Payment_Voucher || Convert.ToInt32(lblVoucherTypeID.Text) == (int)PM.VoucherType.Bank_Payment_Voucher)
            {
                Response.Redirect("GLCashPaymentVoucher.aspx?VoucherNo=" + lblVoucherNumber.Text);
            }
            if (Convert.ToInt32(lblVoucherTypeID.Text) == (int)PM.VoucherType.Cash_Recievalbe_Voucher || Convert.ToInt32(lblVoucherTypeID.Text) == (int)PM.VoucherType.Bank_Recievable_Voucher)
            {
                Response.Redirect("GLCashRecievedVoucher.aspx?VoucherNo=" + lblVoucherNumber.Text);
            }
        }
        else
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to Update Record");
        }
    }
    protected void LbtnView_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        var row = (GridViewRow)((Control)sender).NamingContainer;
        int index = row.RowIndex;
        Label lblVoucherTypeID = (Label)GridVoucher.Rows[index].FindControl("lblVoucherTypeID");
        Label lblVoucherNumber = (Label)GridVoucher.Rows[index].FindControl("lblVoucherNumber");
        int view = 1;
        if (SBO.Can_View == true)
        {
            if (Convert.ToInt32(lblVoucherTypeID.Text) == (int)PM.VoucherType.General_Voucher)
            {
                Response.Redirect("GLGeneralVoucher.aspx?VoucherNo=" + lblVoucherNumber.Text + "&view=" + view);
            }
            if (Convert.ToInt32(lblVoucherTypeID.Text) == (int)PM.VoucherType.Cash_Payment_Voucher || Convert.ToInt32(lblVoucherTypeID.Text) == (int)PM.VoucherType.Bank_Payment_Voucher)
            {
                Response.Redirect("GLCashPaymentVoucher.aspx?VoucherNo=" + lblVoucherNumber.Text + "&view=" + view);
            }
            if (Convert.ToInt32(lblVoucherTypeID.Text) == (int)PM.VoucherType.Cash_Recievalbe_Voucher || Convert.ToInt32(lblVoucherTypeID.Text) == (int)PM.VoucherType.Bank_Recievable_Voucher)
            {
                Response.Redirect("GLCashRecievedVoucher.aspx?VoucherNo=" + lblVoucherNumber.Text + "&view=" + view);
            }
        }
        else
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to View Record");
        }
    }
    protected void lbtnDelete_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Delete == true)
        {
            var row = (GridViewRow)((Control)sender).NamingContainer;
            int index = row.RowIndex;
            ViewState["Index"] = index;
            Label lblVoucherNumber = ((Label)GridVoucher.Rows[index].FindControl("lblVoucherNumberDel"));
            lblDeleteMsg.Text = "Are you sure to want to Delete Voucher # [ " + lblVoucherNumber.Text + " ] ?";
            lbtnYes.Visible = true;
            lbtnNo.Text = "No";
            JQ.showDialog(this, "Confirmation");
        }
        else
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to Delete Record");
        }
    }
    protected void lbtnYes_Click(object sender, EventArgs e)
    {
        GL_BAL GBLL = new GL_BAL();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        int index = Convert.ToInt32(ViewState["Index"]);
        Label lblVoucherTypeID = (Label)GridVoucher.Rows[index].FindControl("lblVoucherTypeID");
        Label lblVoucherNumber = ((Label)GridVoucher.Rows[index].FindControl("lblVoucherNumberDel"));
        if (Convert.ToInt32(lblVoucherTypeID.Text) == (int)PM.VoucherType.General_Voucher)
        {
            DataRow[] dr = SBO.PermissionTable.Select("Page_Url='GLGeneralVoucher.aspx'");
            if (dr.Length > 0)
            {
                if (Convert.ToBoolean(dr[0]["Can_Delete"]) == true)
                {
                    int a = GBLL.DeleteGL(Convert.ToInt32(lblVoucherNumber.Text));
                    if (a != 0)
                    {
                        GBLL.DeleteGL(Convert.ToInt32(lblVoucherNumber.Text));
                        GridVoucher.DataSource = null;
                        GridVoucher.DataSource = GBLL.GetVoucherByVoucherTypeID("0",FinYearID);
                        GridVoucher.DataBind();
                    }
                }
                else
                    Response.Write("<script>alert('You do not have rights to Delete');</script>");
            }
        }
        if (Convert.ToInt32(lblVoucherTypeID.Text) == (int)PM.VoucherType.Cash_Payment_Voucher)
        {
            DataRow[] dr = SBO.PermissionTable.Select("Page_Url='GLCashPaymentVoucher.aspx'");
            if (dr.Length > 0)
            {
                if (Convert.ToBoolean(dr[0]["Can_Delete"]) == true)
                {
                    int a = GBLL.DeleteGL(Convert.ToInt32(lblVoucherNumber.Text));
                    if (a != 0)
                    {
                        GBLL.DeleteGL(Convert.ToInt32(lblVoucherNumber.Text));
                        GridVoucher.DataSource = null;
                        GridVoucher.DataSource = GBLL.GetVoucherByVoucherTypeID("0",FinYearID);
                        GridVoucher.DataBind();
                    }
                }
                else
                    Response.Write("<script>alert('You do not have rights to Delete');</script>");
            }
        }
        if (Convert.ToInt32(lblVoucherTypeID.Text) == (int)PM.VoucherType.Cash_Recievalbe_Voucher)
        {
            DataRow[] dr = SBO.PermissionTable.Select("Page_Url='GLCashRecievedVoucher.aspx'");
            if (dr.Length > 0)
            {
                if (Convert.ToBoolean(dr[0]["Can_Delete"]) == true)
                {
                    int a = GBLL.DeleteGL(Convert.ToInt32(lblVoucherNumber.Text));
                    if (a != 0)
                    {
                        GBLL.DeleteGL(Convert.ToInt32(lblVoucherNumber.Text));
                        GridVoucher.DataSource = null;
                        GridVoucher.DataSource = GBLL.GetVoucherByVoucherTypeID("0",FinYearID);
                        GridVoucher.DataBind();
                    }
                }
                else
                    Response.Write("<script>alert('You do not have rights to Delete');</script>");
            }
        }
        JQ.closeDialog(this, "Confirmation");
    }
    protected void GridVoucher_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        if (txtVoucherType.Text != "")
        {
            if (ddlSearch.Text == "Voucher Type")
            {
                GL_BAL GL = new GL_BAL();
                GridVoucher.DataSource = GL.SearchVoucherListByVoucherType(txtVoucherType.Text, FinYearID);
                GridVoucher.DataBind();
            }
        }
        else if (txtVoucherNo.Text != "")
        {
            if (ddlSearch.Text == "Voucher No")
            {
                GL_BAL GL = new GL_BAL();
                GridVoucher.DataSource = GL.SearchVoucherListByVoucherNo(SCGL_Common.Convert_ToInt(txtVoucherNo.Text), FinYearID);
                GridVoucher.DataBind();
            }
        }
       
        else
        {
            GL_BAL GL = new GL_BAL();
            GridVoucher.DataSource = GL.GetVoucherByVoucherTypeID("0",FinYearID);
            GridVoucher.PageIndex = e.NewPageIndex;
            GridVoucher.DataBind();
        }

        
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        DataTable dt = new DataTable();
        if (txtVoucherType.Text != "")
        {
            if (ddlSearch.Text == "Voucher Type")
            {
                GL_BAL GL = new GL_BAL();
                GridVoucher.DataSource = GL.SearchVoucherListByVoucherType(txtVoucherType.Text,FinYearID);
                GridVoucher.DataBind();
                txtVoucherNo.Text = "";
                
            }
        }
        if (txtVoucherNo.Text != "")
        {
            if (ddlSearch.Text == "Voucher No")
            {
                GL_BAL GL = new GL_BAL();
                GridVoucher.DataSource = GL.SearchVoucherListByVoucherNo(SCGL_Common.Convert_ToInt(txtVoucherNo.Text), FinYearID);
                GridVoucher.DataBind();
                txtVoucherType.Text = "";
               
            }
        }

        SCGL_Common.ReloadJS(this, "setSearchElem();");

        
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        txtVoucherType.Text = "";
        txtVoucherNo.Text = "";
       
        GL_BAL GL = new GL_BAL();
        GridVoucher.DataSource = GL.GetVoucherByVoucherTypeID("0",FinYearID);
        GridVoucher.DataBind();
    }
    protected void txtVoucherType_TextChanged(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        if (txtVoucherType.Text == "")
        {
            GL_BAL GL = new GL_BAL();
            GridVoucher.DataSource = GL.GetVoucherByVoucherTypeID("0",FinYearID);
            GridVoucher.DataBind();
        }
    }
    protected void txtVoucherNo_TextChanged(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        int FinYearID = SBO.FinYearID;
        if (txtVoucherNo.Text == "")
        {
            GL_BAL GL = new GL_BAL();
            GridVoucher.DataSource = GL.GetVoucherByVoucherTypeID("0",FinYearID);
            GridVoucher.DataBind();
        }
    }
}
