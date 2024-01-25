using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Text;
using SW.SW_Common;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

public partial class WebMethod : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
       
            //SCGL_Session SBO = (SCGL_Session)Session["SessionBO"];
            //FinYearID.Value = SCGL_Common.Convert_ToInt(SBO.FinYearID).ToString();
        
    }
  

    [WebMethod]
    public static string GraphGwadar()
    {
        List<string[]> GraphList = new List<string[]>();
        //List<string[2]> GraphList=
        Invoice_BAL bal = new Invoice_BAL();
        DataTable dt = new DataTable();
         DataTable dt2 = new DataTable();
         DataTable dt3 = new DataTable();
        DataTable dtFinYear = new DataTable();
        SCGL_Session SBO = (SCGL_Session)HttpContext.Current.Session["SessionBO"];

        dtFinYear = bal.GetCurrentFisYear(SBO.FinYearID);

       // dt = bal.GetGraph(dtFinYear.Rows[0]["YearFrom"].ToString(), dtFinYear.Rows[0]["YearTo"].ToString());
        dt = bal.GetGwadarGraph_ByDate();

        dt2 = bal.GetKarachiGraph_ByDate();
        dt3 = bal.GetPasniGraph_ByDate();
        StringBuilder s = new StringBuilder();
        s.Append("[");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string comma = (i == dt.Rows.Count - 1) ? "" : ",";
          //s.Append("[\"" + dt.Rows[i]["DisplayName"].ToString() + "\"," + SCGL_Common.CheckDouble(dt.Rows[i]["Total"]) + "]" + comma + "");
            s.Append("[\"" + dt.Rows[i]["VoucharDate"].ToString() + "\"," + SCGL_Common.CheckDouble(dt.Rows[i]["Sales"]) + "]" + comma + "");
          
        }



        s.Append("]");
        s.Append("|||");
        s.Append("[");
        for (int i = 0; i < dt2.Rows.Count; i++)
        {
            string comma = (i == dt2.Rows.Count - 1) ? "" : ",";
            //s.Append("[\"" + dt.Rows[i]["DisplayName"].ToString() + "\"," + SCGL_Common.CheckDouble(dt.Rows[i]["Total"]) + "]" + comma + "");
            s.Append("[\"" + dt2.Rows[i]["VoucharDate"].ToString() + "\"," + SCGL_Common.CheckDouble(dt2.Rows[i]["Sales"]) + "]" + comma + "");
          
        }
        s.Append("]");
        s.Append("|||");

        s.Append("[");
        for (int i = 0; i < dt3.Rows.Count; i++)
        {
            string comma = (i == dt3.Rows.Count - 1) ? "" : ",";
            //s.Append("[\"" + dt.Rows[i]["DisplayName"].ToString() + "\"," + SCGL_Common.CheckDouble(dt.Rows[i]["Total"]) + "]" + comma + "");
            s.Append("[\"" + dt3.Rows[i]["VoucharDate"].ToString() + "\"," + SCGL_Common.CheckDouble(dt3.Rows[i]["Sales"]) + "]" + comma + "");

        }

        s.Append("]");
        


        




        //abc = s.ToString();
        return s.ToString();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string SyncDevice()
    {
        List<string[]> Events = new List<string[]>();
        DataTable dt = new DataTable();


        using (SqlConnection con = new SqlConnection(@"Data Source=Viftech-Server\SqlExpress;Initial Catalog=vt_Maxims;Persist Security Info=True;User ID=Ammar;Password=Dev7123net!"))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM vt_Maxims_Event", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }
        
        return GetDataTableToJSONString(dt);
    }

    public static string GetDataTableToJSONString(DataTable table)
    {

        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

        foreach (DataRow row in table.Rows)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            foreach (DataColumn col in table.Columns)
            {
                dict[col.ColumnName] = row[col];
            }
            list.Add(dict);
        }
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(list);
    }




}
