using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SW.SW_Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


public partial class ExecuteQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionBO"] == null)
        {
            Response.Redirect("Login.aspx");
        }
       
        
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlDataSource1.SelectCommand = TextBox1.Text;
        GridView1.DataSourceID = "SqlDataSource1";
        SqlDataSource1.DataBind();
        GridView1.DataBind();
        GridView1.Visible = true;

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
       
            
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            try
            {
              
                cmd.CommandText = TextBox1.Text;
                cn.Open();
                cmd.ExecuteNonQuery();
                int exec = Convert.ToInt32(cmd.ExecuteNonQuery());

                if (Convert.ToInt32(cmd.ExecuteNonQuery()) != 0)
                {
                    JQ.showStatusMsg(this, "1", "Query Execute Successfully");
                    TextBox1.Text = "";

                }
                else 
                {
                    JQ.showStatusMsg(this, "2", "Query Not Executed");
                }

            }
            catch (Exception ex)
            {

                
                string Msg = ex.Message;
               Msg= Msg.Replace("'", "");
                JQ.showStatusMsg(this, "2",Msg);
               
            }
            finally
            {
                cn.Close();
                cmd.Dispose();

            }
        }
       
       
    
}
