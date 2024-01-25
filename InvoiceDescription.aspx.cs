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

public partial class InvoiceDescription : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void grdInvoiceDesc_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdInvoiceDesc.EditIndex = e.NewEditIndex;
        //GridViewRow row = grdInvoiceDesc.Rows[e.NewEditIndex];
        //row.FindControl("LbtnEdit").Visible = false;
        //row.FindControl("btnCancel").Visible = true;
        //row.FindControl("btnUpdate").Visible = true;
        grdInvoiceDesc.DataBind();
    }

    string NewVal = null;
    protected void grdInvoiceDesc_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        NewVal = ((TextBox)grdInvoiceDesc.Rows[e.RowIndex].FindControl("txtInvoiceDescription")).Text;
        ObjInvoiceDesc.DataBind();
        grdInvoiceDesc.EditIndex = -1;
        grdInvoiceDesc.DataBind();
        
    }
    protected void ObjInvoiceDesc_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        if (NewVal != null)
        {
            InvoiceDesc InvDesc = (InvoiceDesc)e.InputParameters[0];
            InvDesc.InvoiceDescription = NewVal;
        }
    }   
}