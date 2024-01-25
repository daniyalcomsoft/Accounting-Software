using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for JQ
/// </summary>
public class JQ
{
    public JQ()
    {
        
    }
    public static void showDialog(Page page,string DivID)
    {
        ScriptManager.RegisterStartupScript(page,page.GetType(), Guid.NewGuid().ToString(),"showDialog('"+DivID+"');",true);
    }
    public static void closeDialog(Page page, string DivID)
    {
        ScriptManager.RegisterStartupScript(page, page.GetType(), Guid.NewGuid().ToString(), "closeDialog('" + DivID + "');", true);
    }
    public static void RecallJS(Page page,string FunctionName)
    {
        ScriptManager.RegisterStartupScript(page, page.GetType(), Guid.NewGuid().ToString(),FunctionName, true);
    }
    public static void showStatusMsg(Page page,string MsgType, string Msg)
    {
        ScriptManager.RegisterStartupScript(page, page.GetType(), Guid.NewGuid().ToString(), "showStatusMsg('" + MsgType + "','" + Msg + "');", true);
    }
    public static void DatePicker(Page page)
    {
        ScriptManager.RegisterStartupScript(page, page.GetType(), Guid.NewGuid().ToString(), "DateTimePicker();", true);
    }
}
