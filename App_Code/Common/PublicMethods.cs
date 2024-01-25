using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using SW.SW_Common;


   
    public class PM
    {
        public enum ModuleName { GeneralLedger = 1,Sales, Purchase,  Inventory,m, Security, TermDeposite, Client }
        public enum VoucherType { General_Voucher = 1, Cash_Payment_Voucher, Cash_Recievalbe_Voucher, Bank_Payment_Voucher, Bank_Recievable_Voucher }
        public enum FormAction { Save = 1, Approve, Lock, Unlock, Cancel };
        public enum UserAction { Insert = 1, Update, Delete, Lock, Unlock, Cancel };
        public enum TransactionMode { Select_One = 0, Cheque = 1, Cash, Other };
        public enum TransactionType { Deposit = 1, Profit = 2, Withdrawl = 3 };

        public static void BindDropDown(DropDownList cmb,DataTable dt, string ValueMember, string TextMember)
        {
            cmb.DataSource = dt;
            cmb.DataValueField = ValueMember;
            cmb.DataTextField = TextMember;
            cmb.DataBind();
        }
        public static void BindDropDown(DropDownList cmb,DataTable dt)
        {
            cmb.DataSource = dt;
            cmb.DataBind();
        }
        public static void BindDropDown(DropDownList cmb,Enum EnumName)
        {
            List<int> NatureKey = Enum.GetValues(EnumName.GetType()).Cast<int>().ToList();
            List<string> NatureValue = Enum.GetNames(EnumName.GetType()).Cast<string>().ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add("Value", typeof(int));
            dt.Columns.Add("Text", typeof(string));
            for (int i = 0; i < NatureKey.Count; i++)
            {
                int Key = NatureKey[i];
                string Value =  NatureValue[i];
                Value = Value.Replace("Select_One", "- Select One -");
                Value = Value.Replace("_", " ");
                dt.Rows.Add(Key,Value);
            }
            cmb.DataSource = dt;
            cmb.DataValueField = "Value";
            cmb.DataTextField = "Text";
            cmb.DataBind();
        }



        public static DataTable getFinancialYearByID(int FinYearID)
        {
            SqlParameter param = new SqlParameter("@FinYearID", FinYearID);

            DataTable dt = SqlHelper.ExecuteDataset(SCGL_Common.ConnectionString, "vt_SCGL_SpGetFinYear", param).Tables[0];
            return dt;
        }
        public static void BindDataGrid(GridView Grid, DataTable dt)
        {
            Grid.DataSource = dt;
            Grid.DataBind();
        }
        
        public static DataTable ConvertEnumToTable(Enum EnumName)
        {
            List<int> NatureKey = Enum.GetValues(EnumName.GetType()).Cast<int>().ToList();
            List<string> NatureValue = Enum.GetNames(EnumName.GetType()).Cast<string>().ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add("Value", typeof(int));
            dt.Columns.Add("Text", typeof(string));
            for (int i = 0; i < NatureKey.Count; i++)
            {
                dt.Rows.Add(NatureKey[i],NatureValue[i]);
            }
            return dt;
        }
    
        public static byte[] GetImageByte(string ImagePath)
        {
            Bitmap bmp = new Bitmap(ImagePath);
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }
        public static string GetImageFromByte(byte[] Img,string ServerPath)
        {
            string ImgPath = string.Empty;
            using (System.Drawing.Image img = System.Drawing.Image.FromStream(new MemoryStream(Img)))
            {
                ImgPath =ServerPath+ @"\DBImg\" + Guid.NewGuid().ToString() +".jpg";
                img.Save(ImgPath, ImageFormat.Jpeg);
            }
            return ImgPath;
        }

        public static string GetMsgListFromMsgTable(DataTable dt)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<ul class='successmsg'>"+Environment.NewLine);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["MsgType"].ToString() == "1")
                {
                    str.Append("<li><div class='succmsg succmsg-ok'><p>" + dt.Rows[i]["Msg"] + "</p></div></li>" + Environment.NewLine);
                }
                else if (dt.Rows[i]["MsgType"].ToString() == "2")
                {
                    str.Append("<li><div class='succmsg succmsg-error'><p>" + dt.Rows[i]["Msg"] + "</p></div></li>" + Environment.NewLine);
                }
                else if (dt.Rows[i]["MsgType"].ToString() == "3")
                {
                    str.Append("<li><div class='succmsg succmsg-warn'><p>" + dt.Rows[i]["Msg"] + "</p></div></li>" + Environment.NewLine);
                }
            }
            str.Append("</ul>");
            return str.ToString();
        }
        //public static DataTable ConvertGridViewToDataTable(GridView Grid)
        //{
        //    DataTable dt = new DataTable();
        //    if (Grid.HeaderRow != null)
        //    {
        //        for (int i = 0; i < Grid.HeaderRow.Cells.Count; i++)
        //        {
        //            dt.Columns.Add(Grid.HeaderRow.Cells[i].Text);
        //        }
        //    }
        //    foreach (GridViewRow row in Grid.Rows)
        //    {
        //        DataRow dr;
        //        dr = dt.NewRow();

        //        for (int i = 0; i < row.Cells.Count; i++)
        //        {
        //            object obj= row.Cells[i].GetType();
        //            dr[i] = row.Cells[i].Text.Replace("&nbsp;", "");
        //        }
        //        dt.Rows.Add(dr);
        //    }
        //    //if (Grid.FooterRow != null)
        //    //{
        //    //    DataRow dr;
        //    //    dr = dt.NewRow();

        //    //    for (int i = 0; i < Grid.FooterRow.Cells.Count; i++)
        //    //    {
        //    //        dr[i] = Grid.FooterRow.Cells[i].Text.Replace("&nbsp;", "");
        //    //    }
        //    //    dt.Rows.Add(dr);
        //    //}
        //    return dt;
        //}
        public static bool SendEmail(string from, string to, string subject, string body)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(from);
                message.To.Add(new MailAddress(to));
                //message.CC.Add(new MailAddress(cc));
                //if (cc != null && cc != "")
                //{
                  //  message.CC.Add(new MailAddress(cc));
                //}
                //if (bcc != null && bcc != "")
                //{
                  //  message.Bcc.Add(new MailAddress(bcc));
                //}
                if (to.Contains(";"))
                {
                    string[] _EmailsTO = to.Split(";".ToCharArray());

                    for (int i = 0; i < _EmailsTO.Length; i++)
                    {

                        message.To.Add(new MailAddress(_EmailsTO[i]));

                    }
                }
                message.IsBodyHtml = true;
                message.Subject = subject;
                message.Body = body;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Credentials = new System.Net.NetworkCredential("mughaladeel2009@gmail.com","allahhuakber");
                client.EnableSsl = true;
                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }

