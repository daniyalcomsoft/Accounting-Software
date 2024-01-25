using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SW.SW_Common;
using System.Data.SqlClient;

public partial class SalesTaxInvoice : System.Web.UI.Page
{
    ExpenseSheet_BAL BALInvoice = new ExpenseSheet_BAL();
    SalesTax_BAL BALSalesTax = new SalesTax_BAL();

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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "SalesTaxInvoice.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "SalesTaxInvoice.aspx" && view == true)
                {
                    
                    if (Request.QueryString["Id"] != null)
                    {
                      
                        BindControl(Convert.ToInt32(Request.QueryString["Id"]));
                    }

                    else
                    {
                        
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }

        }
        Reload_JS();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataTable dt = PM.getFinancialYearByID(SBO.FinYearID);
        hdnMinDate.Value = SCGL_Common.CheckDateTime(dt.Rows[0]["yearFrom"]).ToShortDateString();
        hdnMaxDate.Value = SCGL_Common.CheckDateTime(dt.Rows[0]["YearTo"]).ToShortDateString();

    }

   
    public void BindControl(int Id)
    {
        if (Request.QueryString["view"] != null)
        {
            btnSave.Visible = false;
        }

        DataTable dt = BALSalesTax.getSalesTaxInvoiceByID(Id);
        if (dt.Rows.Count > 0)
        {
            txtSalesTaxInvoiceID.Text = SCGL_Common.Convert_ToInt(dt.Rows[0]["SalesTaxInvoiceID"]).ToString();
            txtdate.Text = SCGL_Common.CheckDateTime(dt.Rows[0]["Date"]).ToShortDateString();
            lblJobID.Text = SCGL_Common.Convert_ToInt(dt.Rows[0]["JobNo"]).ToString();
            txtJobNo.Text = dt.Rows[0]["JobNumber"].ToString();
            txtPackages.Text = dt.Rows[0]["Packages"].ToString();
            txtRefNo.Text = dt.Rows[0]["Reference"].ToString();
            txtServiceCharges.Text = SCGL_Common.Convert_ToDecimal(dt.Rows[0]["ServiceCharges"]).ToString();
            txtOUE.Text = SCGL_Common.Convert_ToDecimal(dt.Rows[0]["OtherUE"]).ToString();
            txtAmtAT.Text = SCGL_Common.Convert_ToDecimal(dt.Rows[0]["AdditionalAmt"]).ToString();
            btnSave.Text = "Update";
        }
    }

    public void Reload_JS()
    {
        SCGL_Common.ReloadJS(this, "MyDate();");
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            if (SBO.Can_Insert == true)
            {
                  if (Insert_Invoice())
                    {
                        if (btnSave.Text == "Save")
                        {
                            btnSave.Visible = false;
                            lblSuccessMsg.InnerHtml = "Sales Tax Invoice Created Successfully";
                        }
                        else
                        {
                            lblSuccessMsg.InnerHtml = "Sales Tax Invoice Updated Successfully";
                        }
                        SCGL_Common.Success_Message(this.Page, "SalesTaxInvoice_View.aspx");
                    }
               
            }
            else
                JQ.showStatusMsg(this, "3", "User not Allowed to Insert New Record");
        }
        catch (Exception ex)
        {
            lblNewError.InnerHtml = ex.Message;
            SCGL_Common.Error_Message(this.Page);
        }
    }

    public bool Insert_Invoice()
    {
        SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString);
        con.Open();
        SqlTransaction trans = con.BeginTransaction();

        bool isCreated = false;
        
        try
        {
            SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            BALSalesTax.SalesTaxInvoiceID = SCGL_Common.Convert_ToInt(txtSalesTaxInvoiceID.Text);
            if (btnSave.Text == "Save")
            {
                BALSalesTax.SalesTaxID = -1;
            }
            else
            {
                BALSalesTax.SalesTaxID = SCGL_Common.Convert_ToInt(Request.QueryString["Id"]);
            }
            int CountAlreadyExistSTI = BALSalesTax.CountAlreadyExistsSTI(BALSalesTax);
            if (CountAlreadyExistSTI > 0)
            {
                JQ.showStatusMsg(this, "2", "Sales Tax Invoice ID Already Existing");
                txtSalesTaxInvoiceID.Text = "";
            }
            else 
            {
                BALSalesTax.Date = SCGL_Common.CheckDateTime(txtdate.Text);
                BALSalesTax.JobID = SCGL_Common.Convert_ToInt(lblJobID.Text);
                BALSalesTax.Packages = txtPackages.Text;
                BALSalesTax.RefNo = txtRefNo.Text;
                BALSalesTax.ServiceCharges = SCGL_Common.Convert_ToDecimal(txtServiceCharges.Text);
                BALSalesTax.OUE = SCGL_Common.Convert_ToDecimal(txtOUE.Text);
                BALSalesTax.AmtAT = SCGL_Common.Convert_ToDecimal(txtAmtAT.Text);


                int SalesTaxID = BALSalesTax.CreateModifySalesTaxInvoice(BALSalesTax, trans);

                if (SalesTaxID > 0)
                {
                    BALSalesTax.SalesTaxID = SalesTaxID;
                   // BALSalesTax.CreateSalesTaxInvoiceGLTrans(BALSalesTax, trans);
                    trans.Commit();
                    isCreated = true;
                }
                else
                {
                    isCreated = false;
                }
            }
            
            
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch (Exception e)
        {
            trans.Rollback();
            isCreated = false;
            throw;
        }
        return isCreated;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("SalesTaxInvoice_View.aspx");
    }

    protected void btnFind_Click1(object sender, EventArgs e)
    {
        JQ.showDialog(this, "FindJob");
        
    }

    protected void btnFindAcc_Click(object sender, EventArgs e)
    {

        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataTable dt = BALInvoice.GetJobNo(txtAccountNo.Text);
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

    }

    protected void GrdAccounts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {


        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        GrdAccounts.DataSource = BALInvoice.GetJobNo(txtAccountNo.Text);
        GrdAccounts.PageIndex = e.NewPageIndex;
        GrdAccounts.DataBind();

    }

    protected void lnkSelect_Click(object sender, EventArgs e)
    {
        var row = (GridViewRow)((Control)sender).NamingContainer;
        var rowIndex = row.RowIndex;
        txtJobNo.Text = GrdAccounts.Rows[rowIndex].Cells[1].Text.ToString();
        lblJobID.Text = GrdAccounts.Rows[rowIndex].Cells[4].Text.ToString();
        
        JQ.closeDialog(this, "FindJob");
    }

    protected void txtJobNo_TextChanged(object sender, EventArgs e)
    {
        ExpenseSheet_BAL ExpBal = new ExpenseSheet_BAL();
        int CountJobNo = BALInvoice.CheckJobNo(txtJobNo.Text);
        if (CountJobNo > 0)
        {
            lblJobID.Text = SCGL_Common.Convert_ToString(ExpBal.getJobIDbyJobNo(txtJobNo.Text));
        }
        else
        {
            JQ.showStatusMsg(this, "2", "Job Number Does not Exist in the records");
            txtJobNo.Text = "";
        }

    }
    protected void txtRefNo_TextChanged(object sender, EventArgs e)
    {
        int CountRefNo = BALInvoice.CheckReferenceNo(txtRefNo.Text, Convert.ToInt32(Request.QueryString["Id"]));
        if (CountRefNo > 0)
        {
            JQ.showStatusMsg(this, "2", "Reference Number Already Exist in the record");
            txtRefNo.Text = "";
        }
    }
}
