<%@ Page Title="Execute Query" Language="C#" MasterPageFile="Main.master" AutoEventWireup="true" CodeFile="ExecuteQuery.aspx.cs" Inherits="ExecuteQuery" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  
    
    
  
   <style type="text/css">
        
        .btn_1 {
        height: 25px;
        padding: 0px 10px;
        line-height: 20px;
        background-color: #029FE2;
        border-radius: 4px;
        color: #EDF6E3;
        font-size: 12px;
        border: 1px solid #029FE2;
        border-image: initial;
        font-weight: bold;
        margin-left: 5px;
        }
        .btn_1:hover {
        background-color: #2C8CB4;
        }
    </style>
  
    
</asp:Content>

       



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <div>
  
       <div id="StausMsg">
        </div>
        <asp:TextBox ID="TextBox1" runat="server" Height="450px" TextMode="MultiLine" Width="100%" ></asp:TextBox>
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" Width="100%">
        </asp:GridView>
        <asp:Button ID="Button1" runat="server" CssClass="btn" OnClick="Button1_Click" 
            Text="Execute" Visible="False" />
            <br />
        <asp:Button ID="Button2" runat="server"  cssclass="btn_1" OnClick="Button2_Click" 
            Text="Execute Query" />
              <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"></asp:SqlDataSource>
       
    </div>





</asp:Content>

