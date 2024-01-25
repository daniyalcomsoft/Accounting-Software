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
public partial class ShippingLine : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionBO"] == null)
        {
            Response.Redirect("Login.aspx");
        }
    }
    protected void grdShippingLine_RowEditing(object sender, GridViewEditEventArgs e)
    {
       
            grdShippingLine.EditIndex = e.NewEditIndex;
            //GridViewRow row = grdShippingLine.Rows[e.NewEditIndex];
            //row.FindControl("LbtnEdit").Visible = false;
            //row.FindControl("btnCancel").Visible = true;
            //row.FindControl("btnUpdate").Visible = true;
            grdShippingLine.DataBind();
       
       
    }

    string NewVal = null;
    protected void grdShippingLine_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
       
            NewVal = ((TextBox)grdShippingLine.Rows[e.RowIndex].FindControl("txtShippingLine")).Text;
            ObjShippingLine.DataBind();
            grdShippingLine.EditIndex = -1;
            grdShippingLine.DataBind();
       

    }
    protected void ObjShippingLine_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
       
            if (NewVal != null)
            {
                ShippingLine_BAL ShpLine = (ShippingLine_BAL)e.InputParameters[0];
                ShpLine.ShippingLine = NewVal;
            }
        
      
    }
}