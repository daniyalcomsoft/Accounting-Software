using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Text;

namespace SW.SW_Common
{
    /// <summary>
    /// Summary description for Common
    /// </summary>
    /// 
    public class SCGL_Common
    {
        public SCGL_Common()
        {

        }

        //static byte[] key1 = ASCIIEncoding.ASCII.GetBytes(ConfigurationManager.AppSettings["EncryptKey"].ToString());

        public static string SendStatus;

        public static string ConnectionString
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings["ConnectionString"] == null)
                {
                    return null;
                }
                string myCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                return myCon;
            }
        }

        public static string RemoveHTML(string StringWithHTML)
        {
            return Regex.Replace(StringWithHTML, @"<(.|\n)*?>", string.Empty);
        }

        public static string Find(string input, string StartStr, string LastStr)
        {
            int Start = input.IndexOf(StartStr);
            int length = (input.LastIndexOf(LastStr) - Start) + LastStr.Length;
            return input.Substring(Start, length);
        }

       

        public static string getVisitorsIP()
        {
            string VisitorsIPAddr = string.Empty;
            //Users IP Address.                
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                //To get the IP address of the machine and not the proxy
                VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            else if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
                VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
                VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;

            return VisitorsIPAddr;
        }

        public static string SubStr(string str, int length)
        {
            if (str.Length > length)
                return str.Substring(0, length) + "...";
            else
                return str;
        }

        public static string SubStrSimple(string str, int length)
        {
            if (str.Length > length)
                return str.Substring(0, length);
            else
                return str;
        }

        public static void ShowAlertMessage(Page page, string Message)
        {
            ScriptManager.RegisterStartupScript(page, typeof(string), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);
        }
        public static object ValidateValue(int intValue)
        {
            if (intValue == 0)
            {
                return DBNull.Value;
            }
            else
            {
                return intValue;
            }
        }

        public static object ValidateValue(string strValue)
        {
            if (strValue == null)
            {
                return DBNull.Value;
            }
            else
            {
                return strValue;
            }
        }

        public static object ValidateValue(DateTime dtValue)
        {
            if ((dtValue == null) || (dtValue == DateTime.MinValue))
            {
                return DBNull.Value;
            }
            else
            {
                return dtValue;
            }
        }

        public static object ValidateValue(double dblValue)
        {
            if ((dblValue == null))
            {
                return DBNull.Value;
            }
            else
            {
                return dblValue;
            }
        }

        public static int CheckInt(object value)
        {
            if ((value == null) || (value == DBNull.Value))
            {
                return 0;
            }
            else
            {
                //if (value.GetType() != typeof(int))
                //{
                //    throw new InvalidCastException("Object can not be cast to integer.");
                //    //return 0;
                //}
                return int.Parse(value.ToString());
            }
        }

        public static double CheckDouble(object value)
        {
            if ((value == null) || (value == DBNull.Value))
            {
                return 0;
            }
            else
            {
                //try
                //{
                return double.Parse(value.ToString());
                //}
                //catch (Exception ex)
                //{
                //    throw ex;
                //}                
            }
        }

        public static DateTime CheckDateTime(object value)
        {
            if ((value == null) || (value == DBNull.Value) || value == "")
            {
                return GetDefaultDate();
              
            }
            else
            {
                return DateTime.Parse(value.ToString());
            }
        }
       
        public static string CheckString(object value)
        {
            if ((value == null) || (value == DBNull.Value))
            {
                return GetDefaultString();
            }
            else
            {
                return value.ToString();
            }
        }

        public static bool CheckBoolean(object value)
        {
            if ((value == null) || (value == DBNull.Value))
            {
                return GetDefaultBoolean();
            }
            else
            {
                return bool.Parse(value.ToString());
            }
        }

        public static DateTime GetDefaultDate()
        {
            return new DateTime(1900, 1, 1);
        }


        public static string GetNullDate()
        {
            return string.Empty;
        }
       
        public static bool GetDefaultBoolean()
        {
            return false;
        }

        public static string GetDefaultString()
        {
            return string.Empty;
        }

       

        public static bool SendEmail(string from, string to, string subject, string body)
        {
            return SendEmail(from, to, null, null, subject, body);
        }

        public static bool SendEmail(string from, string to, string bcc, string subject, string body)
        {
            return SendEmail(from, to, null, bcc, subject, body);
        }

        public static bool SendEmail(string to, string subject, string body)
        {
            return SendEmail(null, to, null, null, subject, body);
        }


        public static bool SendEmail(string from, string to, string cc, string bcc, string subject, string body)
        {
            MailMessage msg = new MailMessage();
            //Sender email and displayName
            msg.From = new MailAddress(from, "Thompson Oil Company");
            msg.To.Add(to);
            if (cc != null && cc != "")
            {
                msg.CC.Add(cc);
            }
            //msg.Bcc.Add(new MailAddress(bcc));
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.vif-tech.com";
            smtp.Credentials = new NetworkCredential("info@vif-tech.com", "inter123net");
            smtp.Port = 25;
            smtp.EnableSsl = false;

            try
            {
                smtp.Send(msg);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
        public static bool SendEmailWithCC(string from, string to, string cc, string bcc, string subject, string body)
        {
            MailMessage msg = new MailMessage();
            //Sender email and displayName
            msg.From = new MailAddress("smtp@vif-tech.com", "LOS");
            msg.To.Add(to);
            if (cc != null && cc != "")
            {
                msg.CC.Add(cc);
            }
            if (bcc != null && bcc != "")
            {
                msg.Bcc.Add(bcc);
            }
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "mail.vif-tech.com";
            smtp.Credentials = new NetworkCredential("smtp@vif-tech.com", "inter123net");
            smtp.Port = 25;
            smtp.EnableSsl = false;

            try
            {
                smtp.Send(msg);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public static bool WriteErrorFile(string contents, string strPath)
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter(strPath, true);
            writer.Write(contents);
            writer.Flush();
            writer.Close();
            writer.Dispose();
            writer = null;
            return true;
        }

        //public static bool SendAttachmentEmail(string from, string to, string cc, string bcc, string subject, string body, System.Collections.ArrayList attachments)
        //{
        //    try
        //    {
        //        MailMessage message = new MailMessage();

        //        message.From = new MailAddress(from);
        //        message.To.Add(new MailAddress(to));
        //        if (cc != null && cc != "")
        //        {
        //            message.CC.Add(new MailAddress(cc));
        //        }
        //        if (bcc != null && bcc != "")
        //        {
        //            message.Bcc.Add(new MailAddress(bcc));
        //        }
        //        message.IsBodyHtml = true;

        //        message.Subject = subject;
        //        message.Body = body;

        //        if (attachments != null && attachments.Count > 0)
        //        {
        //            foreach (string fileName in attachments)
        //            {
        //                Attachment attach = new Attachment(fileName);
        //                message.Attachments.Add(attach);
        //            }
        //        }

        //        SmtpClient client = new SmtpClient();
        //        client.Credentials = new
        //            System.Net.NetworkCredential(
        //            ConfigurationManager.AppSettings["EmailUserName"],
        //            ConfigurationManager.AppSettings["EmailUserPassword"]);
        //        client.Send(message);

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Reloads current page
        /// </summary>
        /// <param name="UseSSL">Use SSL</param>
        public static void ReloadCurrentPage(bool UseSSL)
        {
            string result = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_HOST"] != null)
            {
                result = HttpContext.Current.Request.ServerVariables["HTTP_HOST"].ToString();
            }
            result = "http://" + result;
            if (!result.EndsWith("/"))
            {
                result += "/";
            }

            if (UseSSL)
            {
                result = result.Replace("http:/", "https:/");
                result = result.Replace("www.www", "www");
            }



            if (result.EndsWith("/"))
            {
                result = result.Substring(0, result.Length - 1);
            }
            string URL = result + HttpContext.Current.Request.RawUrl;
            HttpContext.Current.Response.Redirect(URL);
        }

        /// <summary>
        /// Ensures that requested page is secured (https://)
        /// </summary>
        public static void EnsureSSL()
        {
            if (!HttpContext.Current.Request.IsSecureConnection)
            {
                if (!HttpContext.Current.Request.Url.IsLoopback)
                {
                    ReloadCurrentPage(true);
                }
            }
        }

        /// <summary>
        /// Ensures that requested page is not secured (http://)
        /// </summary>
        public static void EnsureNonSSL()
        {
            if (HttpContext.Current.Request.IsSecureConnection)
            {
                ReloadCurrentPage(false);
            }
        }

        public static string ConvertCurrency(string To, string Amount, HttpRequest Request)
        {
            string Expression = Amount + "USD" + "=?" + To;
            string url = "http://www.google.com/ig/calculator?hl=en&q=" + Expression;

            string response = "";
            string responseMsg = Request.Params.ToString();
            string post = responseMsg;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = post.Length;

            StreamWriter writer = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            writer.Write(post);
            writer.Close();

            StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream());
            response = reader.ReadToEnd();
            reader.Close();

            char[] cChar = new char[3];
            string[] _params = new string[100];

            cChar[0] = ',';
            _params = response.Split(cChar[0]);

            string ConvertedAmount = "";

            ConvertedAmount = _params[1];
            ConvertedAmount = ConvertedAmount.Replace("\"", "");
            ConvertedAmount = ConvertedAmount.Replace("rhs", "");
            ConvertedAmount = ConvertedAmount.Replace(":", "");
            ConvertedAmount = ConvertedAmount.Trim();
            ConvertedAmount = ConvertedAmount.Remove(ConvertedAmount.IndexOf(' '), ConvertedAmount.Length - ConvertedAmount.IndexOf(' '));

            return ConvertedAmount;

        }

        // Detact Bots And Crawlers
        public static bool IsBot
        {
            get
            {
                // If this method can't access the current context that means the executing thread doesn't have access
                // to the current request's properties ... since we can't pull any agent information we have to assume
                // this is not a bot.
                if (HttpContext.Current == null)
                    return false;

                string HTTP_USER_AGENT = "";
                if (HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"] != null)
                    HTTP_USER_AGENT = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].ToLower();

                // Check to see if the user agent field contains any of the terms in the botRegex set in the web.config
                string expression = ConfigurationManager.AppSettings["botRegex"];
                Regex botRegex = new Regex(expression);
                return botRegex.IsMatch(HTTP_USER_AGENT);
            }
        }
        public static void ReloadJS(Page page, string Function)
        {
            ScriptManager.RegisterStartupScript(page, typeof(string), Guid.NewGuid().ToString(), Function, true);
        }
        public static void Success_Message(Page page)
        {
            SCGL_Common.ReloadJS(page, "$('#Notification_Success').fadeIn('slow').delay('4000').fadeOut('slow');");
        }
        public static void Success_Message_with_Iframe(Page page)
        {
            SCGL_Common.ReloadJS(page, "$('#Notification_Success').fadeIn('slow').delay('4000').fadeOut('slow');");
            SCGL_Common.ReloadJS(page, "setTimeout(function(){window.parent.$('#StoreInformation').bPopup().close();},5000);");
        }

        public static void Success_Message(Page page, string Redirect)
        {
            SCGL_Common.ReloadJS(page, "$('#bgDiv').show()");
            SCGL_Common.ReloadJS(page, "$('#Notification_Success').fadeIn('slow').delay('50').fadeOut('slow');");
            SCGL_Common.ReloadJS(page, "setTimeout(function() {window.location.href = '" + Redirect + "';}, 60);");
            
        }

        public static void Success_Message(Page page, string DivID, string Redirect)
        {
            SCGL_Common.ReloadJS(page, "$('#" + DivID + "').fadeIn('slow').delay('4000').fadeOut('slow');");
            SCGL_Common.ReloadJS(page, "setTimeout(function() {window.location.href = '" + Redirect + "';}, 5000);");
        }
        public static void Success_Message(Page page, string DivID, string Time, string Redirect)
        {
            SCGL_Common.ReloadJS(page, "$('#" + DivID + "').fadeIn('slow').delay('" + Time + "').fadeOut('slow');");
            SCGL_Common.ReloadJS(page, "setTimeout(function() {window.location.href = '" + Redirect + "';}, 5000);");
        }
        public static void Notification(Page page, string DivID)
        {
            SCGL_Common.ReloadJS(page, "$('#" + DivID + "').fadeIn('slow').delay('10000').fadeOut('slow');");
        }
        public static void Notification(Page page, string DivID, string time)
        {
            SCGL_Common.ReloadJS(page, "$('#" + DivID + "').fadeIn('slow').delay('" + time + "').fadeOut('slow');");
        }
        public static void Error_Message(Page page)
        {
            SCGL_Common.ReloadJS(page, "$('#Notification_Error').fadeIn('slow').delay('6000').fadeOut('slow');");
            SCGL_Common.ReloadJS(page, "$('#bgDiv').hide()");
        }
        public static void Error_Message(Page page, string Msg)
        {
            SCGL_Common.ReloadJS(page, "$('#Notification_Error').fadeIn('slow').delay('6000').fadeOut('slow');");

        }
        public static void Error_ItemID(Page page)
        {
            SCGL_Common.ReloadJS(page, "$('#Notification_ItemID').fadeIn('slow').delay('6000').fadeOut('slow');");
            //SCGL_Common.ReloadJS(page, "$('#bgDiv').hide()");
        }
        public static void Error_Amount(Page page)
        {
            SCGL_Common.ReloadJS(page, "$('#Notification_Amount').fadeIn('slow').delay('6000').fadeOut('slow');");
        }
        public static void Error_TaxID(Page page)
        {
            SCGL_Common.ReloadJS(page, "$('#Notification_TaxID').fadeIn('slow').delay('6000').fadeOut('slow');");
        }
        public static void Delete_Message(Page page)
        {
            SCGL_Common.ReloadJS(page, "$('#Notification_Delete').fadeIn('slow').delay('6000').fadeOut('slow');");
        }
        public static void Bind_DropDown(DropDownList ComboboxName, string SpName, string DataTextField, string DataValueField)
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(ConnectionString, SpName).Tables[0];
            ComboboxName.DataTextField = DataTextField;
            ComboboxName.DataValueField = DataValueField;
            DataRow row = dt.NewRow();
            row[1] = "--Please Select--";
            row[0] = 0;
            dt.Rows.InsertAt(row, 0);
            ComboboxName.DataSource = dt;
            ComboboxName.DataBind();
            ComboboxName.SelectedIndex = 0;
        }
        public static void Bind_DropDown(DropDownList ComboboxName, string SpName, string DataTextField, string DataValueField, string ParameterName, int ParameterValue)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = { new SqlParameter(ParameterName, ParameterValue) };
            dt = SqlHelper.ExecuteDataset(ConnectionString, SpName, param).Tables[0];
            ComboboxName.DataTextField = DataTextField;
            ComboboxName.DataValueField = DataValueField;
            DataRow row = dt.NewRow();
            row[DataTextField] = "--Please Select--";
            row[DataValueField] = 0;
            dt.Rows.InsertAt(row, 0);
            ComboboxName.DataSource = dt;
            ComboboxName.DataBind();
            ComboboxName.SelectedIndex = 0;
        }

        public static void Bind_DropDown(DropDownList ComboboxName, string SpName, string DataTextField, string DataValueField, string ParameterName, int ParameterValue, string ParameterName1, int ParameterValue1)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = { new SqlParameter(ParameterName, ParameterValue)
                                   ,new SqlParameter(ParameterName1, ParameterValue1)};
            dt = SqlHelper.ExecuteDataset(ConnectionString, SpName, param).Tables[0];
            ComboboxName.DataTextField = DataTextField;
            ComboboxName.DataValueField = DataValueField;
            DataRow row = dt.NewRow();
            row[DataTextField] = "--Please Select--";
            row[DataValueField] = 0;
            dt.Rows.InsertAt(row, 0);
            ComboboxName.DataSource = dt;
            ComboboxName.DataBind();
            ComboboxName.SelectedIndex = 0;
        }

        public static void Bind_DropDown(DropDownList ComboboxName, string SpName, string DataTextField, string DataValueField, string ParameterName, string ParameterValue)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = { new SqlParameter(ParameterName, ParameterValue) };
            dt = SqlHelper.ExecuteDataset(ConnectionString, SpName, param).Tables[0];
            ComboboxName.DataTextField = DataTextField;
            ComboboxName.DataValueField = DataValueField;
            DataRow row = dt.NewRow();
            row[DataTextField] = "--Please Select--";
            row[DataValueField] = 0;
            dt.Rows.InsertAt(row, 0);
            ComboboxName.DataSource = dt;
            ComboboxName.DataBind();
            ComboboxName.SelectedIndex = 0;
        }
        public static void Bind_DropDown(DropDownList ComboboxName, string SpName, string DataTextField, string DataValueField, string ParameterName, DateTime ParameterValue)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = { new SqlParameter(ParameterName, ParameterValue) };
            dt = SqlHelper.ExecuteDataset(ConnectionString, SpName, param).Tables[0];
            ComboboxName.DataTextField = DataTextField;
            ComboboxName.DataValueField = DataValueField;
            DataRow row = dt.NewRow();
            row[DataTextField] = "--Please Select--";
            row[DataValueField] = 0;
            dt.Rows.InsertAt(row, 0);
            ComboboxName.DataSource = dt;
            ComboboxName.DataBind();
            ComboboxName.SelectedIndex = 0;
        }
        public static void Bind_DropDown(DropDownList ComboboxName, string SpName, string DataTextField, string DataValueField, string ParameterName, int ParameterValue, string ParameterName2, string ParameterValue2)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = {
                                       new SqlParameter(ParameterName, ParameterValue),
                                       new SqlParameter(ParameterName2,ParameterValue2)
                                   };
            dt = SqlHelper.ExecuteDataset(ConnectionString, SpName, param).Tables[0];
            ComboboxName.DataTextField = DataTextField;
            ComboboxName.DataValueField = DataValueField;
            DataRow row = dt.NewRow();
            row[DataTextField] = "--Please Select--";
            row[DataValueField] = 0;
            dt.Rows.InsertAt(row, 0);
            ComboboxName.DataSource = dt;
            ComboboxName.DataBind();
            ComboboxName.SelectedIndex = 0;
        }
        public static void Bind_DropDown(DropDownList ComboboxName, string SpName, string DataTextField, string DataValueField, string ParameterName, string ParameterValue, string ParameterName2, string ParameterValue2)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = {
                                       new SqlParameter(ParameterName, ParameterValue),
                                       new SqlParameter(ParameterName2,ParameterValue2)
                                   };
            dt = SqlHelper.ExecuteDataset(ConnectionString, SpName, param).Tables[0];
            ComboboxName.DataTextField = DataTextField;
            ComboboxName.DataValueField = DataValueField;
            DataRow row = dt.NewRow();
            row[DataTextField] = "--Please Select--";
            row[DataValueField] = 0;
            dt.Rows.InsertAt(row, 0);
            ComboboxName.DataSource = dt;
            ComboboxName.DataBind();
            ComboboxName.SelectedIndex = 0;
        }
        public static void Bind_DropDown(DropDownList ComboboxName, string SpName, string DataTextField, string DataValueField, string ParameterName, string ParameterValue, string ParameterName2, DateTime ParameterValue2)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = {
                                       new SqlParameter(ParameterName, ParameterValue),
                                       new SqlParameter(ParameterName2,ParameterValue2)
                                   };
            dt = SqlHelper.ExecuteDataset(ConnectionString, SpName, param).Tables[0];
            ComboboxName.DataTextField = DataTextField;
            ComboboxName.DataValueField = DataValueField;
            DataRow row = dt.NewRow();
            row[DataTextField] = "--Please Select--";
            row[DataValueField] = 0;
            dt.Rows.InsertAt(row, 0);
            ComboboxName.DataSource = dt;
            ComboboxName.DataBind();
            ComboboxName.SelectedIndex = 0;
        }
        public static void Bind_DropDown(DropDownList ComboboxName, string SpName, string DataTextField, string DataValueField, string ParameterName, string ParameterValue, string ParameterName2, DateTime ParameterValue2, string Parameter3, string ParameterValue3)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = {
                                       new SqlParameter(ParameterName, ParameterValue),
                                       new SqlParameter(ParameterName2,ParameterValue2),
                                       new SqlParameter(Parameter3,ParameterValue3)
                                   };
            dt = SqlHelper.ExecuteDataset(ConnectionString, SpName, param).Tables[0];
            ComboboxName.DataTextField = DataTextField;
            ComboboxName.DataValueField = DataValueField;
            DataRow row = dt.NewRow();
            row[DataTextField] = "--Please Select--";
            row[DataValueField] = 0;
            dt.Rows.InsertAt(row, 0);
            ComboboxName.DataSource = dt;
            ComboboxName.DataBind();
            ComboboxName.SelectedIndex = 0;
        }
        public static void Bind_GridView(GridView GridViewName, DataTable DataSource)
        {
            GridViewName.DataSource = DataSource;
            GridViewName.DataBind();
        }
        public static int Convert_ToInt(object GridValue)
        {
            int Value = 0;
            if (GridValue == null)
            {
                Value = 0;
            }
            else if (GridValue.ToString() == "")
            {
                Value = 0;
            }
            else
            {
                Value = Convert.ToInt32(GridValue);
            }
            return Value;
        }
        public static int Convert_ToInt(string Value)
        {
            int Val = 0;
            if (Value == null)
            {
                Val = 0;
            }
            else if (Value == "")
            {
                Val = 0;
            }
            else
            {
                Val = Convert.ToInt32(Value);
            }
            return Val;
        }
        public static int Convert_ToInt(double Value)
        {
            int Val = 0;
            if (Value == null)
            {
                Val = 0;
            }
            else if (Value.ToString() == "")
            {
                Val = 0;
            }
            else
            {
                Val = Convert.ToInt32(Value);
            }
            return Val;
        }
        public static double Convert_ToDouble(object GridValue)
        {
            double Value = 0;
            if (GridValue == null)
            {
                Value = 0;
            }
            else if (GridValue == "")
            {
                Value = 0;
            }
            else
            {
                Value = Convert.ToDouble(GridValue);
            }
            return Value;
        }





        public static decimal Convert_ToDecimal(object GridValue)
        {
            decimal Value = 0;
            if (GridValue == null)
            {
                Value = 0;
            }
            if (GridValue == "")
            {
                Value = 0;
            }
            else
            {
                Value = Convert.ToDecimal(GridValue);
            }

            return Value;
        }
        public static string Convert_ToString(object GVVal)
        {
            string Value = "";
            if (GVVal == null)
            {
                Value = "";
            }
            else
            {
                Value = GVVal.ToString();
            }
            return Value;
        }
        public static bool Validate_Field(object GVVal)
        {
            bool validate = false;
            if (GVVal == null)
            {
                validate = true;
            }
            else if (GVVal.ToString() == "")
            {
                validate = true;
            }
            else
            {
                validate = false;
            }
            return validate;
        }
        public static int RowIndex(object sender)
        {
            var row = (GridViewRow)((Control)sender).NamingContainer;
            int index = row.RowIndex;
            return index;
        }

    }
}