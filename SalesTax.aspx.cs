using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SW.SW_Common;
using System.Data;

public partial class SalesTax : System.Web.UI.Page
{
    SalesTax_BAL STBLL = new SalesTax_BAL();
    SuperAdmin_BAL SABL = new SuperAdmin_BAL();
    User_BAL UBO = new User_BAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionBO"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        Reload_JS();
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "SalesTax.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "SalesTax.aspx" && view == true)
                {
                    OnLoad();
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }

        }
    }
    #region Method
    private void OnLoad()
    {
        PM.BindDataGrid(GridSalesTax, STBLL.getSalesTax());
    }

    //protected void GridSalesTax_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
    //    DataTable dt = PM.getFinancialYearByID(SBO.FinYearID);
    //    string FinYearID = SCGL_Common.Convert_ToString(dt.Rows[0]["FinYearID"]);

    //    //DataTable dtn = STBLL.getSalesTaxYear();
    //    //DataTable dts = STBLL.getalloverSalesTaxYear();
    //    //for (int i = 0; i < dtn.Rows.Count; i++)
    //    //{



    //    //        int rowID = SCGL_Common.Convert_ToInt(dtn.Rows[i]["FinyearID"].ToString());


    //    //       // int ID = SCGL_Common.Convert_ToInt(dts.Rows[0]["FinYearID"].ToString());
    //    //        if (rowID == 3)
    //    //        {
    //    //            e.Row.Cells[0].BackColor = System.Drawing.Color.LightSkyBlue;
    //    //        } 


    //    //}


    //    if (e.Row.Cells[0].Text == FinYearID)
    //    {
    //        e.Row.Cells[0].BackColor = System.Drawing.Color.LightSkyBlue;
    //        e.Row.Cells[1].BackColor = System.Drawing.Color.LightSkyBlue;
    //        e.Row.Cells[2].BackColor = System.Drawing.Color.LightSkyBlue;
    //        e.Row.Cells[3].BackColor = System.Drawing.Color.LightSkyBlue;
    //        e.Row.Cells[4].BackColor = System.Drawing.Color.LightSkyBlue;
    //        e.Row.Cells[5].BackColor = System.Drawing.Color.LightSkyBlue;
    //        e.Row.Cells[4].Enabled = false;
    //        e.Row.Cells[5].Enabled = false;

    //    }
    //}

    private void RefreshControl()
    {
        txtSalesTaxYearID.Text = "";
        txtSalesTaxYear.Text = "";
        txtDateFrom.Text = "";
        txtDateTo.Text = "";
    }
    #endregion


    public void Reload_JS()
    {
        SCGL_Common.ReloadJS(this, "MyDate();");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (txtSalesTaxYearID.Text == "")
        {
            if (SBO.Can_Insert == true)
            {
                SaveSalesTaxYear();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "temp", "closeDialog('NewSalesTax')", true);
                JQ.showStatusMsg(this, "3", "User not Allowed to Insert New Record");
            }
        }
        else
        {
            if (SBO.Can_Update == true)
            {
                SaveSalesTaxYear();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "temp", "closeDialog('NewSalesTax')", true);
                JQ.showStatusMsg(this, "3", "User not Allowed to Update Record");
            }
        }
    }
    protected void lbtnEdit_Command(object sender, CommandEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Update == true)
        {
            if (e.CommandArgument.ToString() != "")
            {
                SalesTax_BAL STBAL = STBLL.GetSalesTaxByID(Convert.ToInt32(e.CommandArgument));
                txtSalesTaxYearID.Text = SCGL_Common.Convert_ToString(STBAL.SalesTaxID);
                txtSalesTaxYear.Text = STBAL.SalesTax;
                //txtDateFrom.Text = SCGL_Common.CheckDateTime(STBAL.YearFrom).ToShortDateString();
                txtDateFrom.Text = STBAL.YearFrom.ToString();
                //txtDateTo.Text = SCGL_Common.CheckDateTime(STBAL.YearTo).ToShortDateString();
                txtDateTo.Text = STBAL.YearTo.ToString();
                JQ.showDialog(this, "NewSalesTax");
            }
        }
        else
        { JQ.showStatusMsg(this, "3", "User not Allowed to Update Record"); }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {

        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Insert == true)
        {
            RefreshControl();

            JQ.showDialog(this, "NewSalesTax");
        }
        else
        { JQ.showStatusMsg(this, "3", "User not Allowed to Insert New Record"); }
    }
    protected void lbtnYes_Click(object sender, EventArgs e)
    {
        SalesTax_BAL STBAL = STBLL.GetSalesTaxByID(Convert.ToInt32(lblGroupID.Text));
        string StarDate = STBAL.YearFrom;
        string EndDate = STBAL.YearTo;
        lblDeleteMsg.Text = STBLL.DeleteSalesTax(Convert.ToInt32(lblGroupID.Text), StarDate, EndDate);
        PM.BindDataGrid(GridSalesTax, STBLL.getSalesTax());
        lbtnYes.Visible = false;
        lbtnNo.Text = "Ok";

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

    protected void GridSalesTax_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        OnLoad();
        GridSalesTax.PageIndex = e.NewPageIndex;
        GridSalesTax.DataBind();
    }

    private void SaveSalesTaxYear()
    {
        int CurrentSalesTaxID = txtSalesTaxYearID.Text.Equals("") ? 0 : SCGL_Common.Convert_ToInt(txtSalesTaxYearID.Text);
        int countoverlapperiod = STBLL.CountSalesTaxOverlapPeriods(CurrentSalesTaxID, SCGL_Common.CheckDateTime(txtDateFrom.Text), SCGL_Common.CheckDateTime(txtDateTo.Text));
        if (countoverlapperiod > 0)
        {
            JQ.showStatusMsg(this, "2", "Cannot add overlap period");
        }
        else
        {
            STBLL.SalesTaxID = txtSalesTaxYearID.Text.Equals("") ? 0 : SCGL_Common.Convert_ToInt(txtSalesTaxYearID.Text);
            STBLL.SalesTax = txtSalesTaxYear.Text;
            //STBLL.YearFrom = txtDateFrom.Text.Equals("") ? DateTime.Now : SCGL_Common.CheckDateTime(txtDateFrom.Text);
            STBLL.YearFrom = txtDateFrom.Text.ToString();
            //STBLL.YearTo = txtDateTo.Text.Equals("") ? DateTime.Now : SCGL_Common.CheckDateTime(txtDateTo.Text);
            STBLL.YearTo = txtDateTo.Text.ToString();
            STBLL.CreateModifySalesTax(STBLL, (SCGL_Session)Session["SessionBO"]);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "temp", "closeDialog('NewSalesTax')", true);
            PM.BindDataGrid(GridSalesTax, STBLL.getSalesTax());
            JQ.showStatusMsg(this, "1", "Successfull Record Update");
        }
    }

}
