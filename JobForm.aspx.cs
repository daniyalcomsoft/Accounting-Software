using SCGL.BAL;
using SW.SW_Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class JobForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionBO"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (!IsPostBack)
        {
            ddlUser.DataBind();
            ddlUser.Items.Insert(0, new ListItem("Select Customer", "0"));
            CheckPagePermissions();            
        }
        Reload_JS();
    }

    public void Reload_JS()
    {
        SCGL_Common.ReloadJS(this, "MyDate();");

    }

    public void Bind_ShippingLine_Dropdown()
    {
            string oldval = ddlShippingLine.SelectedValue;
            ShippingLine_BAL IDesc = new ShippingLine_BAL();
            ddlShippingLine.DataSource = IDesc.ReadDataTable();
            ddlShippingLine.DataTextField = "ShippingLine";
            ddlShippingLine.DataValueField = "ShippingLineID";
            ddlShippingLine.DataBind();
            ddlShippingLine.Items.Insert(0, new ListItem("--Please Select--", "0"));
            ddlShippingLine.Items.Insert(1, new ListItem("+ Add New", "-1"));
            ddlShippingLine.SelectedValue = oldval;

    }

    protected void btnSaveShippingLine_Click(object sender, EventArgs e)
    {
        //SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString);
        //con.Open();
        SCGL_Session SBO = (SCGL_Session)System.Web.HttpContext.Current.Session["SessionBO"];
        if (SBO.Can_Insert == true)
        {
            if (txtShippingLine.Text.Trim() != "" && txtShippingLine.Text != null)
            {

                using (SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString))
                {
                    con.Open();
                    SqlTransaction trans = con.BeginTransaction();
                    ShippingLine_BAL IDesc = new ShippingLine_BAL();
                    IDesc.CreatedDate = DateTime.Now;
                    IDesc.ShippingLine = txtShippingLine.Text;
                    try
                    {
                        CustomerForm_BAL cfbal = new CustomerForm_BAL();
                        int ShippingLineID = IDesc.Create(IDesc, trans);
                        if (ShippingLineID > 0)
                        {
                            cfbal.ShippingLineID = ShippingLineID;
                            JQ.closeDialog(this, "NewShippingLine");

                            string ShippingAccount = cfbal.GetShippingAccount(trans);

                            string MCode = ShippingAccount.Substring(0, 2);
                            cfbal.MainCode = MCode;

                            string CCode = ShippingAccount.Substring(2, 3);
                            cfbal.ControlCode = CCode;
                            // chek exist of not if exist + 1 in max sucsidarycode  
                            int SubSidiary_RecordCount = cfbal.SpSubsidiaryCodeCount(cfbal, trans);
                            if (SubSidiary_RecordCount != 0)
                            {
                                if (SubSidiary_RecordCount < 9)
                                {
                                    SubSidiary_RecordCount = SubSidiary_RecordCount + 1;
                                    cfbal.SubsidaryCode = "000" + SubSidiary_RecordCount;
                                }
                                else if (SubSidiary_RecordCount == 9)
                                {
                                    SubSidiary_RecordCount = SubSidiary_RecordCount + 1;
                                    cfbal.SubsidaryCode = "00" + SubSidiary_RecordCount;

                                }
                                else if (SubSidiary_RecordCount > 9 && SubSidiary_RecordCount < 99)
                                {
                                    SubSidiary_RecordCount = SubSidiary_RecordCount + 1;
                                    cfbal.SubsidaryCode = "00" + SubSidiary_RecordCount;

                                }
                                else if (SubSidiary_RecordCount == 99)
                                {
                                    SubSidiary_RecordCount = SubSidiary_RecordCount + 1;
                                    cfbal.SubsidaryCode = "0" + SubSidiary_RecordCount;

                                }
                                else if (SubSidiary_RecordCount > 99 && SubSidiary_RecordCount < 999)
                                {
                                    SubSidiary_RecordCount = SubSidiary_RecordCount + 1;
                                    cfbal.SubsidaryCode = "0" + SubSidiary_RecordCount;

                                }
                                else
                                {
                                    SubSidiary_RecordCount = SubSidiary_RecordCount + 1;
                                    cfbal.SubsidaryCode = SubSidiary_RecordCount.ToString();

                                }
                            }
                            else
                            {
                                cfbal.SubsidaryCode = "0001";
                            }

                            cfbal.Title = txtShippingLine.Text;
                            cfbal.Code = MCode + CCode + cfbal.SubsidaryCode;

                            if (cfbal.SubsidaryCode != "10000")
                            {
                                cfbal.InsertShippingLineSubsidiary(cfbal, trans);
                                txtShippingLine.Text = "";
                                trans.Commit();
                                Bind_ShippingLine_Dropdown();
                            }
                            else
                            {
                                trans.Rollback();
                                JQ.showStatusMsg(this, "2", "You cannot Create Control Accounts more than 9999");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        trans.Dispose();
                    }
                }
            }
            else
            {
                JQ.showStatusMsg(this, "2", "Shipping Line Cannot Be Blank");
            }
        }
        else
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to Insert Record");
        }
        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Job j = new Job();
        int CountExistingJob = j.CheckExsistingJobNumber(txtJobNumber.Text.Trim());
        
        j.JobNumber = txtJobNumber.Text;
        j.JobDescription = txtJobDescription.Text;
        j.CustomerID = SCGL_Common.CheckInt(ddlUser.SelectedValue);
        j.Completed = chkComplete.Checked;
        j.StartDate = DateTime.Now;
        j.ContactNo = txtContactNum.Text;
        j.Container = txtContainer.Text;
        j.ContainerNo = txtContainerNo.Text;
        j.ContainerDate = SCGL_Common.CheckDateTime(txtContainerDate.Text);
        j.IGMNo = txtIGMNo.Text;
        j.IGMDate = SCGL_Common.CheckDateTime(txtIGMDate.Text);
        j.IndexNo = txtIndexNo.Text;
        j.SS = txtSS.Text;
        j.QTY = txtQTY.Text;
        j.BECashNo = txtBECashNo.Text;
        j.MachineNo = txtMachineNo.Text;
        j.MachineDate = SCGL_Common.CheckDateTime(txtMachineDate.Text);
        j.DeliveryDate = SCGL_Common.CheckDateTime(txtDeliveryDate.Text);
        j.CNFValue = SCGL_Common.Convert_ToDecimal(txtCNFValue.Text);
        j.ImportValue = SCGL_Common.Convert_ToDecimal(txtImportValue.Text);
        j.LCNo = txtLCNo.Text;
        j.BLNo = txtBLNo.Text;
        j.ShippingLineID = SCGL_Common.CheckInt(ddlShippingLine.SelectedValue);

        if (j.Completed)
            j.EndDate = DateTime.Now;
        else
            j.EndDate = SCGL_Common.GetDefaultDate();

        try
        {
            if (Request.QueryString["id"] != null)
            {
                j.JobID = SCGL_Common.CheckInt(Request.QueryString["id"]);
                if (j.Update(j))
                    Response.Redirect("JobForm_Views.aspx");
            }
            else
            {
               
                if (CountExistingJob > 0)
                {
                    JQ.showStatusMsg(this, "2", "Job Already Existing. Cannot Add Duplicate");
                }
                else
                {
                    if (j.Create(j) > 0)
                    {
                        Response.Redirect("JobForm_Views.aspx");
                    }
                }
                
            }
        }
        catch (SqlException ex)
        {
            if (ex.Number == 2627)
            {
                lblNewError.Text = "Job Already Existing. Cannot Add Duplicate";
                SCGL_Common.Error_Message(this.Page, "");
            }
            else 
            {
                lblNewError.Text = ex.Message;
                SCGL_Common.Error_Message(this.Page, "");
            }
            
        }
    }

    #region Helper Method

    void CheckPagePermissions()
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
            if (dtRole.Rows[row]["Page_Url"].ToString() == "JobForm.aspx")
            {
                pageName = dtRole.Rows[row]["Page_Url"].ToString();
                view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                break;
            }
        }
        if (dtRole.Rows.Count > 0)
        {
            if (pageName == "JobForm.aspx" && view == true)
            {
                Bind_ShippingLine_Dropdown();
                if (Request.QueryString["Id"] != null)
                {
                    fillform(SCGL_Common.CheckInt(Request.QueryString["id"]));
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

    void fillform(int JobID)
    {
        if (Request.QueryString["view"] != null)
        {
            btnSave.Visible = false;
        }
        Job j = new Job();
        j = j.Read(JobID);
        txtJobNumber.Text = j.JobNumber;
        txtJobDescription.Text = j.JobDescription;
        ddlUser.SelectedValue = j.CustomerID.ToString();
        chkComplete.Checked = j.Completed;
        txtContactNum.Text = j.ContactNo;
        txtContainer.Text = j.Container;
        txtContainerNo.Text = j.ContainerNo;
        txtContainerDate.Text = SCGL_Common.CheckDateTime(j.ContainerDate).ToString("MM/dd/yyyy"); 
        txtIGMNo.Text = j.IGMNo;
        txtIGMDate.Text = SCGL_Common.CheckDateTime(j.IGMDate).ToString("MM/dd/yyyy");
        txtIndexNo.Text = j.IndexNo;
        txtSS.Text = j.SS;
        txtQTY.Text = j.QTY.ToString();
        txtBECashNo.Text = j.BECashNo;
        txtMachineNo.Text = j.MachineNo;
        txtMachineDate.Text = SCGL_Common.CheckDateTime(j.MachineDate).ToString("MM/dd/yyyy");
        txtDeliveryDate.Text = SCGL_Common.CheckDateTime(j.DeliveryDate).ToString("MM/dd/yyyy");
        txtCNFValue.Text = j.CNFValue.ToString();
        txtImportValue.Text = j.ImportValue.ToString();
        txtLCNo.Text = j.LCNo;
        txtBLNo.Text = j.BLNo;
        ddlShippingLine.SelectedValue = j.ShippingLineID.ToString();
    }
    #endregion   
}