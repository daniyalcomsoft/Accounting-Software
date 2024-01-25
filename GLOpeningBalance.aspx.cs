using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SW.SW_Common;

public partial class GLOpeningBalance : System.Web.UI.Page
{
    GL_BAL BLL = new GL_BAL();
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
            dtRole=PP.GetPermissionByUserId(SCGL_Common.Convert_ToInt(AdSes.RoleId));
            string pageName = null;
            bool view = false;
            foreach (DataRow dr in dtRole.Rows)
            {
                int row = dtRole.Rows.IndexOf(dr);
                if (dtRole.Rows[row]["Page_Url"].ToString() == "GLOpeningBalance.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "GLOpeningBalance.aspx" && view == true)
                {
                    RebindGrid("");
                    GetTotalDebitCredit();
                }
                else
                {
                    Response.Redirect("Default.aspx",false);
                }
            }
        }
        if (Request.QueryString["view"] != null)
        {
            lbtnUpdate.Enabled = false;
        }
    }

    #region Method
    private void RebindGrid(string Code)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        System.Threading.Thread.Sleep(1300);
        DataTable dt = BLL.GetOpeningBalanceByCode(Code,SBO.FinYearID,Code);
        PM.BindDataGrid(GridOpeningBalance,dt);
        ((TextBox)GridOpeningBalance.FooterRow.FindControl("txtTotalDebit")).Text = dt.Compute("SUM(Debit)","1=1").ToString();
        ((TextBox)GridOpeningBalance.FooterRow.FindControl("txtTotalCredit")).Text = dt.Compute("SUM(Credit)", "1=1").ToString();
       
    }
    
    #endregion
   
    
    protected void lbtnUpdate_Click(object sender, EventArgs e)
    {
        lblError.Visible = false;
        GetTotalDebitCredit();
        System.Threading.Thread.Sleep(1300);
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Insert == true && SBO.Can_Update == true)
        {
            if (GridOpeningBalance.Rows.Count > 0)
            {
                int DeleteFID = BLL.DeleteOpening(SBO.FinYearID);
                TextBox TotalDebit = ((TextBox)GridOpeningBalance.FooterRow.FindControl("txtTotalDebit"));
                TextBox TotalCredit = ((TextBox)GridOpeningBalance.FooterRow.FindControl("txtTotalCredit"));
                if (TotalCredit.Text != "" && TotalDebit.Text != "")
                {
                    double DebitTotal = Convert.ToDouble(TotalDebit.Text);
                    double CreditTotal = Convert.ToDouble(TotalCredit.Text);
                    if (DebitTotal == CreditTotal)
                    {
                        for (int i = 0; i < GridOpeningBalance.Rows.Count; i++)
                        {
                            Label lblSubsidaryID = (Label)GridOpeningBalance.Rows[i].FindControl("lblSubsidaryID");
                            Label lblCode = ((Label)GridOpeningBalance.Rows[i].FindControl("lblCode"));
                            if (lblSubsidaryID.Text != "")
                            {
                                double OpeningBal = 0;
                                TextBox Debit = ((TextBox)GridOpeningBalance.Rows[i].FindControl("txtDebit"));
                                TextBox Credit = ((TextBox)GridOpeningBalance.Rows[i].FindControl("txtCredit"));

                                if (Credit.Text != "")
                                {
                                    OpeningBal = Convert.ToDouble(Credit.Text) * -1;
                                }
                                else
                                {
                                    OpeningBal = Debit.Text.Equals("") ? 0 : Convert.ToDouble(Debit.Text);

                                }
                                if (OpeningBal != 0.0)
                                {
                                    {
                                       // int DeleteFID = BLL.DeleteOpening(SBO.FinYearID, lblCode.Text);
                                        BLL.insertUpdateOpeningBalance(SBO.FinYearID, lblCode.Text, OpeningBal);
                                        JQ.showStatusMsg(this, "1", "Record Insert Successfully");
                                    }
                                }
                               

                            }
                        }
                        RebindGrid(txtSearchAccount.Text);
                        JQ.RecallJS(this, "Load_AutoComplete_Code();");
                    }
                    else
                    {
                        lblError.Visible = true;
                    }
                }
            }
        }
        else
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to Update Record");
        }
    }

    protected void lbtnFind_Click(object sender, EventArgs e)
    {
        RebindGrid(txtSearchAccount.Text);
        if (GridOpeningBalance.Rows.Count > 0)
        {
            JQ.RecallJS(this, "Load_AutoComplete_Code();");
        }
        else
        {
            JQ.RecallJS(this, "Load_AutoComplete_Code();");
            JQ.showDialog(this, "Confirmation");
            lblDeleteMsg.Text = "Filter not Selected";
            RebindGrid("");
        }
    }
    protected void GrdAccounts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GeneralJournalVoucher_BAL GGV = new GeneralJournalVoucher_BAL();
        GLGeneralVoucher_BAL GV = new GLGeneralVoucher_BAL();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataTable dts = GV.GetYear_Account(SBO.FinYearID);
        GrdAccounts.PageIndex = e.NewPageIndex;
        //GrdAccounts.DataSource = GGV.GetAccountName(txtAccountNo.Text);
        GrdAccounts.DataSource = GV.GetAccountName(txtAccountNo.Text, SBO.FinYearID, dts.Rows[0]["YearFrom"].ToString(), dts.Rows[0]["YearTo"].ToString());
        GrdAccounts.DataBind();
    }

    protected void btnFindAcc_Click(object sender, EventArgs e)
    {
        GeneralJournalVoucher_BAL GGV = new GeneralJournalVoucher_BAL();
        GLGeneralVoucher_BAL GV = new GLGeneralVoucher_BAL();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataTable dts = GV.GetYear_Account(SBO.FinYearID);
       // DataTable dt = GGV.GetAccountName(txtAccountNo.Text);
        DataTable dt = GV.GetAccountName(txtAccountNo.Text,SBO.FinYearID,dts.Rows[0]["YearFrom"].ToString(),dts.Rows[0]["YearTo"].ToString());
        GrdAccounts.DataSource = dt;
        if (dt.Rows.Count > 0)
        {
            GrdAccounts.DataBind();
        }
        else
        {
            GrdAccounts.DataSource = null;
            GrdAccounts.DataBind();
            JQ.showDialog(this, "Confirmation");
        }
        JQ.RecallJS(this, "Load_AutoComplete_Code();");
    }
    protected void GrdAccounts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    protected void GrdAccounts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    protected void lnkSelect_Click(object sender, EventArgs e)
    {
        var row = (GridViewRow)((Control)sender).NamingContainer;
        var rowIndex = row.RowIndex;
        txtSearchAccount.Text = GrdAccounts.Rows[rowIndex].Cells[1].Text; 
        JQ.closeDialog(this, "FindAccount1");
        JQ.RecallJS(this, "Load_AutoComplete_Code();");
        JQ.DatePicker(this);
    }
    
    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        txtSearchAccount.Text = "";
        RebindGrid(txtSearchAccount.Text);
        JQ.RecallJS(this, "Load_AutoComplete_Code();");
    }
    double TDebit;
    double TCredit;
    private void GetTotalDebitCredit()
    {

        string TDebitstr;
        string TCreditstr;
        if (GridOpeningBalance.Rows.Count > 0)
        {
            for (int i = 0; i < GridOpeningBalance.Rows.Count; i++)
            {
                TextBox Debit = ((TextBox)GridOpeningBalance.Rows[i].FindControl("txtDebit"));
                TextBox Credit = ((TextBox)GridOpeningBalance.Rows[i].FindControl("txtCredit"));
                
                if (Debit.Text != "")
                {
                    TDebit += Convert.ToDouble(Debit.Text);
                }
                if (Credit.Text != "")
                {
                    TCredit += Convert.ToDouble(Credit.Text);
                }
                
            }
            TDebitstr = string.Format("{0:n}", TDebit);
            TCreditstr = string.Format("{0:n}", TCredit);
            (GridOpeningBalance.FooterRow.FindControl("txtTotalDebit") as TextBox).Text = TDebitstr;
            (GridOpeningBalance.FooterRow.FindControl("txtTotalCredit") as TextBox).Text = TCreditstr;
        }
    }
    protected void txtDebit_TextChanged(object sender, EventArgs e)
    {
        int RowIndex= ((GridViewRow)(sender as TextBox).NamingContainer).RowIndex;
        string txtDebit = (GridOpeningBalance.Rows[RowIndex].FindControl("txtDebit") as TextBox).Text;
        string txtCredit = (GridOpeningBalance.Rows[RowIndex].FindControl("txtCredit") as TextBox).Text;
        if (txtDebit != "")
        {
            (GridOpeningBalance.Rows[RowIndex].FindControl("txtCredit") as TextBox).Text = ""; 
        }
    }
    protected void txtCredit_TextChanged(object sender, EventArgs e)
    {
        int RowIndex = ((GridViewRow)(sender as TextBox).NamingContainer).RowIndex;
        string txtDebit = (GridOpeningBalance.Rows[RowIndex].FindControl("txtDebit") as TextBox).Text;
        string txtCredit = (GridOpeningBalance.Rows[RowIndex].FindControl("txtCredit") as TextBox).Text;
        if (txtCredit != "")
        {
            (GridOpeningBalance.Rows[RowIndex].FindControl("txtDebit") as TextBox).Text = "";
        }
    }
}
