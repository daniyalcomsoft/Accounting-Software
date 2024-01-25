using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Configuration;


 public class EncryptDecrypt
    {

     static byte[] key1 = ASCIIEncoding.ASCII.GetBytes(ConfigurationManager.AppSettings["EncKey"]);

        public static string Encrypt(string originalString)
        {
            byte[] bytes = key1;
            try
            {
                if (String.IsNullOrEmpty(originalString))
                {
                    throw new ArgumentNullException("The string which needs to be encrypted can not be null.");
                }

                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);

                StreamWriter writer = new StreamWriter(cryptoStream);
                writer.Write(originalString);
                writer.Flush();
                cryptoStream.FlushFinalBlock();
                writer.Flush();

                return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public static string Decrypt(string cryptedString)
        {
            byte[] bytes = key1;
            try
            {
                if (String.IsNullOrEmpty(cryptedString))
                {
                    throw new ArgumentNullException("The string which needs to be decrypted can not be null.");
                }

                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cryptedString));
                CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(cryptoStream);

                return reader.ReadToEnd();
            }
            catch (Exception ex)
            { throw ex; }
        }
    }

/*   This is Key for Encoding.Add this in Project's Settings.Designer.cs

[global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Minhajjj")]
        public string Key1
        {
            get
            {
                return ((string)(this["Key1"]));
            }
            set
            {
                this["Key1"] = value;
            }


        }
*/

 /* ////////////////This is how to use Encoding

 if (obj.Login(txtUserName.Text, Inventory.Classes.MinhajEncrypt.Encrypt(txtPassword.Text, key1)))
                {
                    MainForm mf = new MainForm();
                    // obj.Insert_UserLogs(txtUserName.Text, DateTime.Now.ToShortTimeString(), "", Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                    //mf.Owner = this.Owner;
                    //mf.groupControl1.Text = "Welcome " + txtUserName.Text.ToUpper() + " !";
                    txtPassword.Text = "";
                    txtUserName.Text = "";
                    this.Visible = false;
                    this.Hide();

                    mf.Show();
                }
////////////////////////
Add this code first in cs file.
static byte[] key1 = ASCIIEncoding.ASCII.GetBytes(Inventory.Properties.Settings.Default.Key1);
*/