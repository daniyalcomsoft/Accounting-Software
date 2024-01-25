using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Text;
using SCGL.BAL;
using System.Data.SqlClient;

/// <summary>
/// Summary description for GetData
/// </summary>
[WebService(Namespace = "SyncDevice")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class GetData : System.Web.Services.WebService
{
    [WebMethod(true)]
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
    


    [WebMethod(true)]
    [ScriptMethod(ResponseFormat=ResponseFormat.Json)]
    public List<SubAccountInfo> GetAccountCodeTitle(string Match)
    
    {
        List<SubAccountInfo> list = new List<SubAccountInfo>();
        GLSubsidiary_BAL SubBL = new GLSubsidiary_BAL();
       
    
        SCGL_Session SBO = (SCGL_Session)HttpContext.Current.Session["SessionBO"];
      
        GLGeneralVoucher_BAL GGV = new GLGeneralVoucher_BAL();
        DataTable dts = GGV.GetYear_Account(SBO.FinYearID);
        DataTable dt = SubBL.GetSubCodeTitleLike(Match, SBO.FinYearID, dts.Rows[0]["YearFrom"].ToString(),dts.Rows[0]["YearTo"].ToString());
       
    
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            SubAccountInfo acc = new SubAccountInfo();
            acc.AccMain = dt.Rows[i]["MainCode"].ToString();
            acc.AccControl = dt.Rows[i]["ControlCode"].ToString();
            acc.AccSubsidary = dt.Rows[i]["SubsidaryCode"].ToString();
            acc.CodeTitle = dt.Rows[i]["CodeTitle"].ToString();
            acc.AccCode = dt.Rows[i]["Code"].ToString();
            acc.Title = dt.Rows[i]["Title"].ToString();
            acc.Balance = Convert.ToDouble(dt.Rows[i]["CurrentBal"]).ToString("#,##,0.00");
        
            list.Add(acc);
        }
        return list;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public List<Job> GetJobByNumber(string Match)
    {
        Job j = new Job();
        return j.ReadByJobNumber(Match);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat=ResponseFormat.Json)]
    public List<SubAccountInfo2> GetAccountCodeTitle2(string Match)
    {
        List<SubAccountInfo2> list = new List<SubAccountInfo2>();
        GLSubsidiary_BAL SubBL = new GLSubsidiary_BAL();
       

        SCGL_Session SBO = (SCGL_Session)HttpContext.Current.Session["SessionBO"];
       
        GLGeneralVoucher_BAL GGV = new GLGeneralVoucher_BAL();
        DataTable dts = GGV.GetYear_Account(SBO.FinYearID);
        DataTable dt = SubBL.GetSubCodeTitleLike2(Match, SBO.FinYearID, dts.Rows[0]["YearFrom"].ToString(), dts.Rows[0]["YearTo"].ToString());

       
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            SubAccountInfo2 acc = new SubAccountInfo2();
            acc.AccMain = dt.Rows[i]["MainCode"].ToString();
            acc.AccControl = dt.Rows[i]["ControlCode"].ToString();
            acc.AccSubsidary = dt.Rows[i]["SubsidaryCode"].ToString();
            acc.CodeTitle = dt.Rows[i]["CodeTitle"].ToString();
            acc.AccCode = dt.Rows[i]["Code"].ToString();
            acc.Title = dt.Rows[i]["Title"].ToString();
            acc.Balance = Convert.ToDouble(dt.Rows[i]["CurrentBal"]).ToString("#,##,0.00");

            list.Add(acc);
        }
        return list;
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public List<EventInfo> Sync()
    {
        List<EventInfo> list = new List<EventInfo>();

        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(@"Data Source=flax.arvixe.com;Initial Catalog=vt_SCGL;Persist Security Info=True;User ID=ilyas;Password=Dev5123net!");
        
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM vt_SCGL_Events", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
        

       
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            EventInfo acc = new EventInfo();
            acc.EventID = Convert.ToInt32(dt.Rows[i]["EventID"].ToString());
            acc.Event = dt.Rows[i]["Event"].ToString();
            acc.BusNo = dt.Rows[i]["BusNo"].ToString();
            acc.Sort = Convert.ToInt32(dt.Rows[i]["Sort"].ToString());
            acc.IsSync = Convert.ToBoolean(dt.Rows[i]["IsSync"].ToString());
            

            list.Add(acc);
        }
        return list;
    }

    

}

public class SubAccountInfo 
{
    public string AccCode { get; set; }
    public string AccMain { get; set; }
    public string AccControl { get; set; }
    public string AccSubsidary { get; set; }
    public string CodeTitle { get; set; }
    public string Title { get; set; }
    public string Balance { get; set; }
}

public class SubAccountInfo2
{
    public string AccCode { get; set; }
    public string AccMain { get; set; }
    public string AccControl { get; set; }
    public string AccSubsidary { get; set; }
    public string CodeTitle { get; set; }
    public string Title { get; set; }
    public string Balance { get; set; }
}

public class EventInfo
{
    public int EventID { get; set; }
    public string Event { get; set; }
    public string BusNo { get; set; }
    public int Sort { get; set; }
    public bool IsSync { get; set; }

}


