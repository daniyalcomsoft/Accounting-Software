using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SW.SW_Common;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Threading;

public partial class PhysicalStockCount : System.Web.UI.Page
{

    PhysicalStockCount_BAL PSC = new PhysicalStockCount_BAL();

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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "PhysicalStockCount.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "PhysicalStockCount.aspx" && view == true)
                {
                    Bind_CostCenter();

                    //  Gv_GetRows1();
                    if (btnSave.Visible == true)
                    {
                        SetInitialRow_For_Edit();
                    }
                    else
                    {
                        SetInitialRow();
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }


        }

        // SetInitialRow_For_Edit();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataTable dt = PM.getFinancialYearByID(SBO.FinYearID);
        hdnMinDate.Value = SCGL_Common.CheckDateTime(dt.Rows[0]["yearFrom"]).ToShortDateString();
        hdnMaxDate.Value = SCGL_Common.CheckDateTime(dt.Rows[0]["YearTo"]).ToShortDateString();

    }
    private void SetInitialRow()
    {


        DataTable dt = new DataTable();

        dt.Columns.Add("InventoryId");
        dt.Columns.Add("InventoryName");
        dt.Columns.Add("OnHand");
        dt.Columns.Add("PhysicalCount");
        dt.Columns.Add("Difference");

        dt.Rows.Add("", "", "", "", "");

        ViewState["CurrentTable"] = dt;

        bindPhysicalStockCountGrid(dt);
        //  Bind_Product_Dropdown();
    }

    public void Bind_CostCenter()
    {
        DataTable dt = new SuperAdmin_BAL().GetCostCenterList();
        PM.BindDropDown(ddlCostCenter, dt, "CostCenterID", "CostCenterName");
        ddlCostCenter.Items.Insert(0, new ListItem("Select CostCenter", "0"));
        //SCGL_Common.Bind_DropDown(ddlVendor, "vt_AEI_BindVendor", "VendorName", "ID");
    }

    public void Reload_JS()
    {
        SCGL_Common.ReloadJS(this, "MyDate();");
        SCGL_Common.ReloadJS(this, "Subtract();");

    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        try
        {
            ////DataTable dt = PSC.getPhysicalStockCountByDate(txtDate.Text);
            Bind_Grid();


            showHideControls(true);
        }
        catch (Exception ex)
        { Response.Write(ex.Message); }
    }
    private void showHideControls(bool show)
    {
        GV_PhysicalStockCount.Visible = show;
        // btnAddLines.Visible = show;
        // btnClearAllLines.Visible = show;
        //btnSave.Visible = show;
        btnCancel.Visible = show;
    }

    private void bindPhysicalStockCountGrid(DataTable dt)
    {
        GV_PhysicalStockCount.DataSource = dt;
        GV_PhysicalStockCount.DataBind();
    }
    //public void Bind_Product_Dropdown()
    //{
    //    for (int i = 0; i < GV_PhysicalStockCount.Rows.Count; i++)
    //    {
    //        DropDownList ddlItem = GV_PhysicalStockCount.Rows[i].FindControl("ddlItem") as DropDownList;
    //        SCGL_Common.Bind_DropDown(ddlItem, "vt_AEI_SP_GetFishCart", "item", "Inventory_ID");
    //    }
    //}

    protected void btnAddLines_Click(object sender, EventArgs e)
    {
        //    AddNewRowToGrid();
    }

    private void fillDataTable()
    {
        DataTable dt = (DataTable)ViewState["CurrentTable"];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < GV_PhysicalStockCount.Rows.Count; i++)
            {

                dt.Rows[i]["InventoryId"] = ((TextBox)GV_PhysicalStockCount.Rows[i].FindControl("txtInventory_Id")).Text;
                dt.Rows[i]["InventoryName"] = ((TextBox)GV_PhysicalStockCount.Rows[i].FindControl("txtInventoryName")).Text;
                dt.Rows[i]["OnHand"] = ((TextBox)GV_PhysicalStockCount.Rows[i].FindControl("txtOnHand")).Text;
                dt.Rows[i]["PhysicalCount"] = ((TextBox)GV_PhysicalStockCount.Rows[i].FindControl("txtPhysicalCount")).Text;
                dt.Rows[i]["Difference"] = ((TextBox)GV_PhysicalStockCount.Rows[i].FindControl("txtDifference")).Text;

            }
        }
        ViewState["CurrentTable"] = dt;
    }
    private void selectDdlSelectedItem()
    {
        try
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                    for (int i = 0; i < GV_PhysicalStockCount.Rows.Count; i++)
                    {
                        DropDownList cbox = GV_PhysicalStockCount.Rows[i].FindControl("ddlItem") as DropDownList;
                        cbox.SelectedValue = dt.Rows[i]["Invontory_Id"].ToString();
                    }
            }
        }
        catch (Exception ex)
        { Response.Write(ex.Message); }
    }

    protected void btnClearAllLines_Click(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {        //if (Convert.ToBoolean(ViewState["isItUpdate"]) == false)        
        //savePhysicalStockCount(Convert.ToBoolean(ViewState["isItUpdate"]));
        //  DataTable dt = Record_for_Insert();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Insert == true)
        {
            savePhysicalStockCount(Convert.ToBoolean(ViewState["isItUpdate"]));
        }
        else
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to Save Record");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        //if (SBO.Can_Delete == true)
        //{
        //    try
        //    {
        //        int ValuationDate = PSC.CheckValuationByDate(SCGL_Common.CheckDateTime(txtDate.Text), SCGL_Common.Convert_ToInt(ddlCostCenter.SelectedValue));
        //        int TotalCount = PSC.CountTotalPurchasesandSalesInvoices(SCGL_Common.CheckDateTime(txtDate.Text));
        //        if (TotalCount == 0 && ValuationDate > 0)
        //        {
        //            PSC.DeletePhysicalStockCountandExcess(SCGL_Common.CheckDateTime(txtDate.Text));
        //            JQ.showStatusMsg(this, "1", "Record Deleted");
        //            GV_PhysicalStockCount.Visible = false;
        //            btnDelete.Visible = false;
        //            btnUpdate.Visible = false;
        //            btnCancel.Visible = false;
        //            txtDate.Text = "";
        //            ddlCostCenter.SelectedIndex = 0;
        //        }
        //        else
        //        {
        //            JQ.showStatusMsg(this, "2", "Record Exists in Purchases and Sales in the Future Dates");
        //        }

        //    }
        //    catch (Exception ex)
        //    { Response.Write(ex.Message); }
        //}
        //else
        //{
        //    JQ.showStatusMsg(this, "3", "User not Allowed to Delete Record");
        //}

    }

    private void savePhysicalStockCount(bool update)
    {
        SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString);
        con.Open();
        SqlTransaction trans = con.BeginTransaction();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        try
        {
            DataTable dt2 = PSC.GetLastExcessShortID();
            if (dt2.Rows.Count > 0)
            {
                int ExcessShortID = SCGL_Common.Convert_ToInt(dt2.Rows[0]["ExcessShortID"]);
                PSC.ExcessShortID = ExcessShortID + 1;
            }
            else
            {
                PSC.ExcessShortID = 1;
            }
            int TotalCount = PSC.CountTotalPurchasesandSalesInvoices(SCGL_Common.CheckDateTime(txtDate.Text));

            if (ViewState["CurrentTable"] != null)
            {
                fillDataTable();
                DataTable dt = (DataTable)ViewState["CurrentTable"];


                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        PSC.Inventory_Id = Convert.ToInt32(dt.Rows[i]["InventoryId"]);
                        PSC.OnHand = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["ONHand"].ToString());
                        PSC.PhysicalStockCount = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["PhysicalCount"].ToString());
                        PSC.Difference = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["Difference"].ToString());
                        PSC.date = SCGL_Common.CheckDateTime(txtDate.Text).ToShortDateString();
                        PSC.CostCenterID = SCGL_Common.Convert_ToInt(ddlCostCenter.SelectedValue);
                        PSC.FinYearID = SCGL_Common.Convert_ToInt(SBO.FinYearID);
                        if (PSC.CreateModifyPhysicalStock(PSC, trans))
                        {
                            JQ.showStatusMsg(this, "1", "Record Saved Successfully");
                        }
                        if (PSC.Difference != 0)
                        {

                            PSC.CreateModifyExcessShort(PSC, trans);
                        }
                    }
                }
                GV_PhysicalStockCount.Visible = false;
                btnSave.Visible = false;
                btnCancel.Visible = false;
                txtDate.Text = "";
                ddlCostCenter.SelectedIndex = 0;
            }


            //else
            //{
            //    JQ.showStatusMsg(this, "2", "Record Exists in Purchases and Sales in the Future Dates");
            //}
        }
        //catch (Exception ex)
        //{ Response.Write(ex.Message); }
        catch (Exception ex)
        {
            trans.Rollback();
            throw ex;
        }
        finally
        {
            trans.Commit();
            con.Close();
        }

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        if (SBO.Can_Update == true)
        {
            UpdatePhysicalStockCount(Convert.ToBoolean(ViewState["isItUpdate"]));
        }
        else
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to Update Record");
        }

    }

    private void UpdatePhysicalStockCount(bool update)
    {
        SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString);
        con.Open();
        SqlTransaction trans = con.BeginTransaction();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        PSC.date = SCGL_Common.CheckDateTime(txtDate.Text).ToShortDateString();
        PSC.CostCenterID = SCGL_Common.Convert_ToInt(ddlCostCenter.SelectedValue);
        try
        {
            DataTable dt2 = PSC.GetUpdateExcessShortID(SCGL_Common.CheckDateTime(txtDate.Text), SCGL_Common.Convert_ToInt(ddlCostCenter.SelectedValue));
            int ExcessShortID = SCGL_Common.Convert_ToInt(dt2.Rows[0]["ExcessShortID"]);
            PSC.ExcessShortID = ExcessShortID;


            int ValuationDate = PSC.CheckValuationByDate(SCGL_Common.CheckDateTime(txtDate.Text), SCGL_Common.Convert_ToInt(ddlCostCenter.SelectedValue));
            int TotalCount = PSC.CountTotalPurchasesandSalesInvoices(SCGL_Common.CheckDateTime(txtDate.Text));
            if (ValuationDate > 0)
            {

                PSC.DeleteExcessShortfromGL(PSC, trans);
                PSC.DeletePhysicalStockCountandExcess(PSC, trans);


                if (ViewState["CurrentTable"] != null)
                {
                    fillDataTable();
                    DataTable dt = (DataTable)ViewState["CurrentTable"];


                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            PSC.Inventory_Id = Convert.ToInt32(dt.Rows[i]["InventoryId"]);
                            PSC.OnHand = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["ONHand"].ToString());
                            PSC.PhysicalStockCount = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["PhysicalCount"].ToString());
                            PSC.Difference = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["Difference"].ToString());
                            PSC.date = SCGL_Common.CheckDateTime(txtDate.Text).ToShortDateString();
                            PSC.CostCenterID = SCGL_Common.Convert_ToInt(ddlCostCenter.SelectedValue);
                            PSC.FinYearID = SCGL_Common.Convert_ToInt(SBO.FinYearID);
                            if (PSC.CreateModifyPhysicalStock(PSC, trans))
                            {
                                JQ.showStatusMsg(this, "1", "Record Updated Successfully");
                            }
                            if (PSC.Difference != 0)
                            {
                                PSC.CreateModifyExcessShort(PSC, trans);
                            }
                        }
                    }
                }
                GV_PhysicalStockCount.Visible = false;
                btnDelete.Visible = false;
                btnUpdate.Visible = false;
                btnCancel.Visible = false;
                txtDate.Text = "";
                ddlCostCenter.SelectedIndex = 0;
            }
            //else
            //{
            //    JQ.showStatusMsg(this, "2", "Record Exists in Purchases and Sales in the Future Dates");
            //}
        }
        //catch (Exception ex)
        //{ Response.Write(ex.Message); }
        catch (Exception ex)
        {
            trans.Rollback();
            throw ex;
        }
        finally
        {
            trans.Commit();
            con.Close();
        }
    }

    public void Bind_Grid()
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        DataTable dt = new DataTable();
        int ValuationDate = PSC.CheckValuationByDate(SCGL_Common.CheckDateTime(txtDate.Text), SCGL_Common.Convert_ToInt(ddlCostCenter.SelectedValue));

        if (ValuationDate > 0)
        {
            dt = PSC.GetAlreadyPresentPhysicalStock(SCGL_Common.Convert_ToInt(SBO.FinYearID), SCGL_Common.Convert_ToInt(ddlCostCenter.SelectedValue), SCGL_Common.CheckDateTime(txtDate.Text).ToShortDateString());
            btnDelete.Visible = true;
            btnUpdate.Visible = true;
            btnSave.Visible = false;


        }
        else
        {
            dt = PSC.getValuationByDate(SCGL_Common.Convert_ToInt(SBO.FinYearID), SCGL_Common.Convert_ToInt(ddlCostCenter.SelectedValue), SCGL_Common.CheckDateTime(txtDate.Text).ToShortDateString());
            btnDelete.Visible = false;
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0][0].ToString() == "0")
            {
                ViewState["isItUpdate"] = false;
                //   SetInitialRow();
            }
            else
            {
                ViewState["isItUpdate"] = true;
                GV_PhysicalStockCount.DataSource = ViewState["CurrentTable"] = dt;
                //dt.Columns.Add("PhysicalCount", typeof(string));
                if (ValuationDate == 0)
                {
                    dt.Columns.Add("Difference", typeof(string));
                }

                GV_PhysicalStockCount.DataBind();
                //btnSave.Visible = true;
                SCGL_Common.ReloadJS(this, "Subtract();");

                // Bind_Product_Dropdown();
                //  selectDdlSelectedItem();
            }
        }
    }

    protected void lbtnYes_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString);
        con.Open();
        SqlTransaction trans = con.BeginTransaction();
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        PSC.date = SCGL_Common.CheckDateTime(txtDate.Text).ToShortDateString();
        PSC.CostCenterID = SCGL_Common.Convert_ToInt(ddlCostCenter.SelectedValue);

        try
        {
            int ValuationDate = PSC.CheckValuationByDate(SCGL_Common.CheckDateTime(txtDate.Text), SCGL_Common.Convert_ToInt(ddlCostCenter.SelectedValue));
            int TotalCount = PSC.CountTotalPurchasesandSalesInvoices(SCGL_Common.CheckDateTime(txtDate.Text));
            if (ValuationDate > 0)
            {

                PSC.DeleteExcessShortfromGL(PSC, trans);
                PSC.DeletePhysicalStockCountandExcess(PSC, trans);
                JQ.showStatusMsg(this, "1", "Record Deleted");
                GV_PhysicalStockCount.Visible = false;
                btnDelete.Visible = false;
                btnUpdate.Visible = false;
                btnCancel.Visible = false;
                txtDate.Text = "";
                ddlCostCenter.SelectedIndex = 0;
                lbtnYes.Visible = false;
                lblDeleteMsg.Text = "Record Deleted Successfully !";
                lbtnNo.Text = "Ok";
            }
            //else
            //{
            //    JQ.showStatusMsg(this, "2", "Record Exists in Purchases and Sales in the Future Dates");
            //}

        }
        catch (Exception ex)
        {
            trans.Rollback();
            throw ex;
            //Response.Write(ex.Message);
        }
        finally
        {
            trans.Commit();
            con.Close();
        }



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
        {
            JQ.showStatusMsg(this, "3", "User not Allowed to Delete Record");
        }
    }

    private void SetInitialRow_For_Edit()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        // dt.Columns.Add(new DataColumn("txtDescription", typeof(string)));

        dt.Columns.Add(new DataColumn("txtInventoryName", typeof(string)));
        dt.Columns.Add(new DataColumn("txtOnHand", typeof(string)));
        dt.Columns.Add(new DataColumn("txtPhysicalStock", typeof(string)));
        dt.Columns.Add(new DataColumn("txtdifference", typeof(string)));
        if (Convert.ToInt32(Session["GV1"]) < 1)
        {
            for (int i = 0; i < 1; i++)
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
            }
        }
        else
        {
            for (int i = 0; i < Convert.ToInt32(Session["GV1"]); i++)
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
            }
        }
    }
    public void Gv_GetRows1()
    {
        SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
        SqlDataReader dr = PSC.getValuationByDate1(SCGL_Common.Convert_ToInt(SBO.FinYearID), SCGL_Common.Convert_ToInt(ddlCostCenter.SelectedValue), SCGL_Common.CheckDateTime(txtDate.Text).ToShortDateString());
        while (dr.Read())
        {
            Session["GV1"] = dr["RowNumber"].ToString();
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        GV_PhysicalStockCount.Visible = false;
        btnDelete.Visible = false;
        btnSave.Visible = false;
        btnUpdate.Visible = false;
        btnCancel.Visible = false;
        txtDate.Text = "";
        ddlCostCenter.SelectedIndex = 0;
    }

    //public void checkemail() 
    //{
    //    string emailadd= "ilyasgmail.com";
    //    if (System.Text.RegularExpressions.Regex.IsMatch(emailadd, "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$"))
    //    {
    //        lblemail.Text = "correct";
    //    }
    //    else 
    //    {
    //        lblemail.Text = "incorrect";
    //    }
    
    //}

    //protected void GV_PhysicalStockCount_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{

    //    //GV_PhysicalStockCount.PageIndex = e.NewPageIndex;
    //    //Bind_Grid();
    //}
}
