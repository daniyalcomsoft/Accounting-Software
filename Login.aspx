<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" 
Inherits="Login" Title="Login Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login</title>

    <script src="Script/jquery-1.6.2.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="Script/jquery.validator.js" type="text/javascript"></script>
    
    <script src="Script/jquery-ui-1.8.16.custom.min.js" type="text/javascript" charset="utf-8"></script>
    <link href="Css/validator.css" rel="stylesheet" type="text/css" />
    <link rel="Stylesheet" href="Css/Style.css" type="text/css" />
    <style type="text/css">
        body
        {
            background-image: none;
        }
        .input
        {
            height: 26px;
            line-height: 26px;
            border: outset 1px #ccc;
            font-size: 14px;
        }
        .btn_1
        {
            height: 30px;
            width: 80px;
            line-height: 27px;
            background-color: #029FE2;
            border-radius: 4px;
            color: #EDF6E3;
            font-size: 12px;
            border: 1px solid #029FE2;
        }
        .btn_1:hover
        {
            background-color: #2C8CB4;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Login_Container">
            <div id="Login_Content">
                <div id="Login_Heading">
                    <h2>
                        Login
                    </h2>
                </div>
                <div id="email_password_container" style="width: 388px; margin-left: -51px;">
                    <asp:Panel DefaultButton="lbtnLogin" ID="Panel1" runat="server">
                        <div class="email_password_text_style">
                            <label style="font-size: 16px;">
                                User ID:</label>
                            <asp:TextBox ID="txtUserName" CssClass="email_textbox" runat="server" Style="width: 222px;"></asp:TextBox>
                        </div>
                        <div class="email_password_text_style">
                            <label style="font-size: 16px; margin-top: 19px;">
                                Password:</label>
                            <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="password_textbox" Style="width: 222px;"
                                runat="server"></asp:TextBox>
                        </div>
                        <div class="email_password_text_style" style="margin-top: 15px;margin-right: 76px;">
                            <label style="font-size: 16px;margin-top: 5px;">
                                Financial Year:</label>
                               <asp:DropDownList runat="server" CssClass="input" ID="ddlFinYear" validate="group"
                            custom="Select Financial Year" customFn="var goal = parseInt(this.value); return goal > 0;">
                            </asp:DropDownList>
                        </div>
                        
                        <asp:Label ID="lblErrorMsg" Style="margin-top: 5px; margin-left: 122px;" runat="server"
                            ForeColor="#FF3300">
                        </asp:Label>
                        <div class="RememberMe_ForgotPassword">
                            <asp:CheckBox ID="ChkRememberMe" runat="server" Text="&amp;nbsp;&amp;nbsp;Remember Me" />
                        </div>
                        
                        <div id="login_btn">
                            <asp:Button ID="lbtnLogin" CssClass="btn_1" Text="Login" runat="server"
                             OnClientClick="return validate('group');" Style="float: right;" OnClick="lbtnLogin_Click" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
