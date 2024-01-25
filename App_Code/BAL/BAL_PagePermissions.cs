using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


public class BAL_PagePermissions:DAL_UserPermissions
{
	public BAL_PagePermissions()
	{
	}
    public int Permission_Id { get; set; }
    public int? RoleId { get; set; }
    public int? UserId { get; set; }
    public int? PageId { get; set; }
    public string Page_Url { get; set; }
    public bool? Can_View { get; set; }
    public bool? Can_Insert { get; set; }
    public bool? Can_Update { get; set; }
    public bool? Can_Delete { get; set; }
    public bool? Can_ApproveOrReject { get; set; }
    public bool? Can_Unlock { get; set; }
    public Int16? Active { get; set; }

    public override DataTable GetPermissionByUserId(int RoleId)
    {
        return base.GetPermissionByUserId(RoleId);
    }

   
}
