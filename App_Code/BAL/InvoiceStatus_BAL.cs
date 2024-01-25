using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for InvoiceStatus_BAL
/// </summary>
public class InvoiceStatus_BAL:InvoiceStatus_DAL
{
    private int _InvoiceID;
    private string _ChequeNo;
    public string CheqNo { get; set; }
    public int Status { get; set; }
    public DateTime RecDate { get; set; }
	
    public InvoiceStatus_BAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int InvoiceID
    {
        get
        {
            return this._InvoiceID;
        }
        set
        {
            this._InvoiceID = value;
        }
    }

    public string ChequeNo
    {
        get
        {
            return this._ChequeNo;
        }
        set
        {
            this._ChequeNo = value;
        }
    }


    public override DataTable getallInvoice()
    {
        return base.getallInvoice();
    }

    public override bool Update(InvoiceStatus_BAL p)
    {
         SCGL_Session SBO = (SCGL_Session)System.Web.HttpContext.Current.Session["SessionBO"];
        if (SBO.Can_Update == true)
        {
             try
             {
                 return base.Update(p);
             }
         
            catch (Exception ex)
                {
                    throw ex;
                }
        }
        else
        {
            JQ.showStatusMsg((Page)(HttpContext.Current.Handler), "3", "User not Allowed to Update Record");
            return false;
        }
    }
    public override bool UpdateDetails(InvoiceStatus_BAL InvStatus)
    {
        return base.UpdateDetails(InvStatus);
    }
}
