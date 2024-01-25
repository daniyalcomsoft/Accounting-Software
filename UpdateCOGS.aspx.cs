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
using System.IO;
using System.Text;
using SW.SW_Common;
using System.Data.SqlClient;

public partial class UpdateCOGS : System.Web.UI.Page
{
    Invoice_BAL_Temp BALInvoice = new Invoice_BAL_Temp();

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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "UpdateCOGS.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "UpdateCOGS.aspx" && view == true)
                {
                    
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
        }

    }

    protected void btnUpdateCOGS_Click(object sender, EventArgs e)
    {
      
            lblDeleteMsg.Text = "Are you sure to want to Update COGS !";
            lbtnYes.Visible = true;
            lbtnNo.Text = "No";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Confirmation", "showDialog('Confirmation');", true);
       
    }
    protected void lbtnYes_Click(object sender, EventArgs e)
    {
        PhysicalStockCount_BAL PSC = new PhysicalStockCount_BAL();
        SqlConnection con = new SqlConnection(SCGL_Common.ConnectionString);
        con.Open();
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5 = new DataTable();
        dt = BALInvoice.GetInvoiceDetailTable();
        dt3 = BALInvoice.GetPSCTable();
    
        using (SqlTransaction trans = con.BeginTransaction())
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BALInvoice.InvoiceDetailID = SCGL_Common.Convert_ToInt(dt.Rows[i]["InvoiceDetailID"].ToString());
                        BALInvoice.InvoiceID = SCGL_Common.Convert_ToInt(dt.Rows[i]["InvoiceID"].ToString());
                        //BALInvoice.InventoryID = SCGL_Common.Convert_ToInt(dt.Rows[i]["InventoryID"].ToString());
                        //BALInvoice.CostCenterID = SCGL_Common.Convert_ToInt(dt.Rows[i]["CostCenterID"].ToString());
                        BALInvoice.InvoiceDate = SCGL_Common.CheckDateTime(dt.Rows[i]["InvoiceDate"].ToString());
                        //BALInvoice.Quantity = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["Quantity"].ToString());

                        dt2 = BALInvoice.getCogs_RateforUpdation(BALInvoice, trans);
                        //BALInvoice.COGSRate = SCGL_Common.Convert_ToDecimal(dt2.Rows[0]["GOGSRate"].ToString());
                        //BALInvoice.COGSAmount = BALInvoice.Quantity * BALInvoice.COGSRate;
                        int ID = BALInvoice.ModifyInvoiceDetailCOGS(BALInvoice, trans);
                        int ID2 = BALInvoice.ModifyGLTransactionCOGS(BALInvoice, trans);
                        

                    }
                    trans.Commit();
                    
                }
                else 
                {
                    trans.Rollback();
                }

                    
               
            }
            catch (Exception ex)
            {
                lblDeleteMsg.Text = ex.Message;
                trans.Rollback();
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }
        con.Open();
        using (SqlTransaction trans = con.BeginTransaction())
        {
            try
            {
               

                if (dt3.Rows.Count > 0)
                {
                    BALInvoice.DeleteExcessShortandGLTrans(BALInvoice, trans);
                   
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        //BALInvoice.PscID = SCGL_Common.Convert_ToInt(dt3.Rows[i]["ID"].ToString());
                        //BALInvoice.InventoryID = SCGL_Common.Convert_ToInt(dt3.Rows[i]["Inventory_Id"].ToString());
                        //BALInvoice.FishID = SCGL_Common.Convert_ToInt(dt3.Rows[i]["FishId"].ToString());
                        //BALInvoice.FishGradeID = SCGL_Common.Convert_ToInt(dt3.Rows[i]["FishGradeId"].ToString());
                        //BALInvoice.FishSizeID = SCGL_Common.Convert_ToInt(dt3.Rows[i]["FishSizeId"].ToString());
                        //PSC.OnHand = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["ONHand"].ToString());
                        //BALInvoice.PhysicalStockCount = SCGL_Common.Convert_ToDecimal(dt3.Rows[i]["PhysicalCount"].ToString());
                        //PSC.Difference = SCGL_Common.Convert_ToDecimal(dt.Rows[i]["Difference"].ToString());
                        //BALInvoice.PSCDate = SCGL_Common.CheckDateTime(dt3.Rows[i]["Date"].ToString());
                        //BALInvoice.CostCenterID = SCGL_Common.Convert_ToInt(dt3.Rows[i]["CostCenterID"].ToString());
                        BALInvoice.FinYearID = SCGL_Common.Convert_ToInt(dt3.Rows[i]["FinYearID"].ToString());
                        dt4 = BALInvoice.getOnHandStock(BALInvoice, trans);
                        //BALInvoice.OnHand = SCGL_Common.Convert_ToDecimal(dt4.Rows[0]["OnHand"].ToString());
                        //BALInvoice.Difference = BALInvoice.PhysicalStockCount - BALInvoice.OnHand;
                        int PhysicalID = BALInvoice.ModifyPSCTable(BALInvoice, trans);
                        //if (BALInvoice.Difference != 0) 
                        {
                            dt5 = BALInvoice.GetLastExcessShortID(trans);
                            if (dt5.Rows.Count > 0)
                            {
                                int ExcessShortID = SCGL_Common.Convert_ToInt(dt5.Rows[0]["ExcessShortID"]);
                                //BALInvoice.ExcessShortID = ExcessShortID + 1;
                            }
                            else
                            {
                                //BALInvoice.ExcessShortID = 1;
                            }

                            BALInvoice.CreateModifyExcessShort(BALInvoice, trans);
                        
                        }
                    }
                    trans.Commit();
                }
                else
                {
                    trans.Rollback();
                }

            }
            catch (Exception ex)
            {
                lblDeleteMsg.Text = ex.Message;
                trans.Rollback();
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }

        }
        lblDeleteMsg.Text = "Process Updated Successfully";
        lbtnYes.Visible = false;
        lbtnNo.Text = "Ok";
        
    }


}
