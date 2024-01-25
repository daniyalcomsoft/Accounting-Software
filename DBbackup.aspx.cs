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
using System.Collections.Generic;
using System.Xml.Linq;
using System.Net;
using Ionic.Zip;
using System.IO;
using System.Text;
using SW.SW_Common;

public partial class DBbackup : System.Web.UI.Page
{
    string path = ConfigurationManager.AppSettings["DBBackupPath"].ToString();
    //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
   // string path = "C://";
    string fileP = "";
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
                if (dtRole.Rows[row]["Page_Url"].ToString() == "DBbackup.aspx")
                {
                    pageName = dtRole.Rows[row]["Page_Url"].ToString();
                    view = Convert.ToBoolean(dtRole.Rows[row]["Can_View"].ToString());
                    break;
                }
            }
            if (dtRole.Rows.Count > 0)
            {
                if (pageName == "DBbackup.aspx" && view == true)
                {
                    lblPath.Text = path + DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
        }
        
    }
    protected void lnkCreateDBbackup_Click(object sender, EventArgs e)
    {
        bool Result = false;
        fileP = path + txtFileName.Text + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + ".bak";

        Result = GenerateBackup("vt_SCGL", fileP);
        if (Result)
        {
            using (var zip = new ZipFile())
            {
                zip.AddEntry("ZIPInfo.txt", "This ZIP file was created on " + DateTime.Now);
                zip.Password = "saima123";
                zip.Encryption = EncryptionAlgorithm.PkzipWeak;

                var imagePath = fileP;
                zip.AddFile(imagePath, string.Empty);
                var saveToFilePath = fileP;
                zip.Save(saveToFilePath.Replace("bak", "zip"));
            }
            File.Delete(fileP);
            JQ.showStatusMsg(this, "1", "Create Backup Successfull");
        }
        else
        { JQ.showDialog(this, "Confirmation"); }
    }

    public bool GenerateBackup(string DBName, string Location)
    {
        if (SCGL_Common.ConnectionString.Length > 0)
        {
            System.Data.SqlClient.SqlConnection dbConn = null;
            try
            {
                string Query = "BACKUP DATABASE " + DBName + " TO DISK = '" + Location + "';";

                dbConn = new System.Data.SqlClient.SqlConnection(SCGL_Common.ConnectionString);
                dbConn.Open();
                System.Data.SqlClient.SqlCommand dbCmd = new System.Data.SqlClient.SqlCommand(Query, dbConn);
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                lblDeleteMsg.Text = ex.ToString();
                return false;
                //LogError(ex);
            }
            finally
            {
                if (dbConn.State.ToString().Equals("Open"))
                {
                    dbConn.Close();
                }
                dbConn.Dispose();
            }
        }
        return true;
    }
}
