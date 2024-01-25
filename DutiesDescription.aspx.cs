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
using SCGL.BAL;
using SW.SW_Common;


public partial class DutiesDescription : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void grdInvoiceDutiesDesc_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdInvoiceDutiesDesc.EditIndex = e.NewEditIndex;
        //GridViewRow row = grdInvoiceDutiesDesc.Rows[e.NewEditIndex];
        //row.FindControl("LbtnEdit").Visible = false;
        //row.FindControl("btnCancel").Visible = true;
        //row.FindControl("btnUpdate").Visible = true;
        grdInvoiceDutiesDesc.DataBind();
    }

    string NewVal = null;
    protected void grdInvoiceDutiesDesc_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        NewVal = ((TextBox)grdInvoiceDutiesDesc.Rows[e.RowIndex].FindControl("txtInvoiceDutiesDescription")).Text;
        ObjInvoiceDutiesDesc.DataBind();
        grdInvoiceDutiesDesc.EditIndex = -1;
        grdInvoiceDutiesDesc.DataBind();

    }
    protected void ObjInvoiceDutiesDesc_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        if (NewVal != null)
        {
            InvoiceDutiesDesc InvDesc = (InvoiceDutiesDesc)e.InputParameters[0];
            InvDesc.InvoiceDutiesDescription = NewVal;
        }
    }
}