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
/// Summary description for SCGL_Session
/// </summary>
public class SCGL_Session
{
	public SCGL_Session()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable PermissionTable = new DataTable();
    public int CooperativeSocietyID { get; set; }
    public int UserID { get; set; }
    public string UserIP { get; set; }
    public string UserName { get; set; }
    public int? SiteID { get; set; }
    public string SiteName { get; set; }
    public bool Can_Insert { get; set; }
    public bool Can_Update { get; set; }
    public bool Can_Delete { get; set; }
    public bool Can_View { get; set; }
    public bool Can_Approve { get; set; }
    public bool isRoot { get; set; }
    public string Permission { get; set; }
    public string PageRefrence { get; set; }
    public int FinYearID { get; set; }
    public int RoleId { get; set; }
   // public string FinYearTitle { get; set; }
    
}
