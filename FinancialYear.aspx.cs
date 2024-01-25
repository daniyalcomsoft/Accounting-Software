using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SW.SW_Common;
using System.Drawing;

public partial class FinancialYear : System.Web.UI.Page
{
    Financial_BAL FBLL = new Financial_BAL();
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "FinancialYear.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "FinancialYear.aspx" && view == true)
                {
                    LoadFinYear();
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
        PM.BindDataGrid(GridFinancial, FBLL.getFinancialYear());
    }

    protected void GridFinancial_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataTable dt = PM.getFinancialYearByID(SBO.FinYearID);
        string FinYearID = SCGL_Common.Convert_ToString(dt.Rows[0]["FinYearID"]);
        
        //DataTable dtn = FBLL.getFinancialYear();
        //DataTable dts = FBLL.getalloverFinancialYear();
        //for (int i = 0; i < dtn.Rows.Count; i++)
        //{
           
           
             
        //        int rowID = SCGL_Common.Convert_ToInt(dtn.Rows[i]["FinyearID"].ToString());


        //       // int ID = SCGL_Common.Convert_ToInt(dts.Rows[0]["FinYearID"].ToString());
        //        if (rowID == 3)
        //        {
        //            e.Row.Cells[0].BackColor = System.Drawing.Color.LightSkyBlue;
        //        } 
            
           
        //}


        if (e.Row.Cells[0].Text == FinYearID)
        {
            e.Row.Cells[0].BackColor = System.Drawing.Color.LightSkyBlue;
            e.Row.Cells[1].BackColor = System.Drawing.Color.LightSkyBlue;
            e.Row.Cells[2].BackColor = System.Drawing.Color.LightSkyBlue;
            e.Row.Cells[3].BackColor = System.Drawing.Color.LightSkyBlue;
            e.Row.Cells[4].BackColor = System.Drawing.Color.LightSkyBlue;
            e.Row.Cells[5].BackColor = System.Drawing.Color.LightSkyBlue;
            e.Row.Cells[4].Enabled = false;
            e.Row.Cells[5].Enabled = false;
            
        }
    }

    private void RefreshControl()
    {
        txtFinancialYearID.Text = "";
        txtFinancialYear.Text = "";
        txtDateFrom.Text = "";
        txtDateTo.Text = "";
    }
    #endregion

    public void LoadFinYear()
    {
        ddlFinYear.DataSource = UBO.GetFinacialYear();
        ddlFinYear.DataValueField = "FinYearID";
        ddlFinYear.DataTextField = "FinYearTitle";
        ddlFinYear.DataBind();
        ddlFinYear.Items.Insert(0, new ListItem("--Please Select--", "0"));
        ddlFinYear.SelectedValue = UBO.GetDefaultFinancialYear().ToString();
    }

    public void Reload_JS()
    {
        SCGL_Common.ReloadJS(this, "MyDate();");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (txtFinancialYearID.Text == "")
        {
            if (SBO.Can_Insert == true)
            {
                SaveFYear();
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "temp", "closeDialog('NewFinancial')", true);
                JQ.showStatusMsg(this, "3", "User not Allowed to Insert New Record");
            }
        }
        else
        {
            if (SBO.Can_Update == true)
            {
                SaveFYear();
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "temp", "closeDialog('NewFinancial')", true);
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
                Financial_BAL FBAL=FBLL.GetFinancialYearByID(Convert.ToInt32(e.CommandArgument));
                txtFinancialYearID.Text = SCGL_Common.Convert_ToString(FBAL.FinYearID);
                txtFinancialYear.Text = FBAL.FinYearTitle;
                //txtDateFrom.Text = SCGL_Common.CheckDateTime(FBAL.YearFrom).ToShortDateString();
                txtDateFrom.Text = FBAL.YearFrom.ToString();
                //txtDateTo.Text = SCGL_Common.CheckDateTime(FBAL.YearTo).ToShortDateString();
                txtDateTo.Text = FBAL.YearTo.ToString();
                JQ.showDialog(this, "NewFinancial");
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
          
            JQ.showDialog(this, "NewFinancial");
        }
        else
        { JQ.showStatusMsg(this, "3", "User not Allowed to Insert New Record"); }
    }
    protected void lbtnYes_Click(object sender, EventArgs e)
    {
        Financial_BAL FBAL = FBLL.GetFinancialYearByID(Convert.ToInt32(lblGroupID.Text));
        string StarDate = FBAL.YearFrom;
        string EndDate = FBAL.YearTo;
        lblDeleteMsg.Text = FBLL.DeleteFinancialYear(Convert.ToInt32(lblGroupID.Text), StarDate, EndDate);
        PM.BindDataGrid(GridFinancial, FBLL.getFinancialYear());
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

    protected void GridFinancial_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        OnLoad();
        GridFinancial.PageIndex = e.NewPageIndex;
        GridFinancial.DataBind();
    }

    private void SaveFYear()
    {
        int CurrentFinYearID=txtFinancialYearID.Text.Equals("") ? 0 :SCGL_Common.Convert_ToInt(txtFinancialYearID.Text);
        int countoverlapperiod = FBLL.CountOverlapPeriods(CurrentFinYearID,SCGL_Common.CheckDateTime(txtDateFrom.Text), SCGL_Common.CheckDateTime(txtDateTo.Text));
        if (countoverlapperiod > 0)
        {
            JQ.showStatusMsg(this, "2", "Cannot add overlap period");
        }
        else 
        { 
            FBLL.FinYearID = txtFinancialYearID.Text.Equals("") ? 0 :SCGL_Common.Convert_ToInt(txtFinancialYearID.Text);
            FBLL.FinYearTitle = txtFinancialYear.Text;
            //FBLL.YearFrom = txtDateFrom.Text.Equals("") ? DateTime.Now : SCGL_Common.CheckDateTime(txtDateFrom.Text);
            FBLL.YearFrom = txtDateFrom.Text.ToString();
            //FBLL.YearTo = txtDateTo.Text.Equals("") ? DateTime.Now : SCGL_Common.CheckDateTime(txtDateTo.Text);
            FBLL.YearTo = txtDateTo.Text.ToString();
             FBLL.CreateModifyFinancial(FBLL, (SCGL_Session)Session["SessionBO"]);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "temp", "closeDialog('NewFinancial')", true);
            PM.BindDataGrid(GridFinancial, FBLL.getFinancialYear());
            JQ.showStatusMsg(this, "1", "Successfull Record Update");
        }
    }

    protected void lbtnsetasdefault_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Insert == true)
        {

            int DefaultFinYearID = SCGL_Common.Convert_ToInt(ddlFinYear.SelectedValue);
            FBLL.SetDefaultFinancialYear(DefaultFinYearID);
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
        else
        { JQ.showStatusMsg(this, "3", "User not Allowed to Insert Record"); }
    }
}
