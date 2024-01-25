<%@ Page Title="Create User" Language="C#" MasterPageFile="Main.master" AutoEventWireup="true"
    CodeFile="CreateUser.aspx.cs" Inherits="CreateUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script language="javascript" type="text/javascript">
    $(document).ready(function() {
        CreateModalPopUp('#Check', 280, 100, 'ALERT');
        CreateModalPopUp('#Confirmation', 280, 100, 'ALERT');
        $('#DialogRolePermission').dialog({
            autoOpen: false,
            draggable: true,
            title: "Add New Role",
            width: 972,
            height: 540,
            open: function(type, data) {
                $(this).parent().appendTo("form");
            }
        });        
        $('#DialogUserPermission').dialog({
            autoOpen: false,
            draggable: true,
            title: "User Extra Permission",
            width: 972,
            height: 540,
            open: function(type, data) {
                $(this).parent().appendTo("form");
            }
        });
    });

    function Verify(evet) {
        var charCode = (evet.which) ? evet.which : event.keyCode
        if (charCode != 9) {
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
        }
        return true;
    }

    function OpenRolePermission(btnCode) {
        if (btnCode == "Edit") {
            var QueryString = $("#<%=cmbUserRole.ClientID %>").val();
            $("#FramRole").attr("src", "AddEditRolePermission.aspx?RoleID=" + QueryString);
        }
        else {
            $("#FramRole").attr("src", "AddEditRolePermission.aspx");
        }
        showDialog('DialogRolePermission');
        return true;
    }
    
    function OpenUserPermission() {
        var UserID = $("#<%=hdnUserId.ClientID %>").text();
        if (UserID != "") {
            $("#FramUserID").attr("src", "SE_UserPermisssion.aspx?UserID=" + UserID);
        }
        showDialog('DialogUserPermission');
        return false;
    }
    </script>

    <style type="text/css">
        .jumpmenuheight
        {
            height: 19px;
        }
        .secfieldwidth
        {
            width: 138px;
        }
        .fullfieldwidth
        {
            width: 256px;
        }
        .CheckBox
        {
            float: left;
        }
        .CheckBox input[type=checkbox]
        {
            float: left;
            margin-right: 4px;
            margin-top: 2px;
        }
        table tr td
        {
            padding-right: 1px;
            text-align: left;
            font-family: Verdana, Geneva, sans-serif;
            font-size: 12px;
        }
        .forsmallfwidth
        {
            width: 105px;
        }
        .addresswidth
        {
            width: 264px;
        }
        .cumodifywidth
        {
            width: 121px;
        }
        .buttonImp
        {
            /*background: -ms-linear-gradient(center top , #90C356 0%, #649A27 100%) repeat scroll 0 0 transparent;
            background: -moz-linear-gradient(center top , #90C356 0%, #649A27 100%) repeat scroll 0 0 transparent;
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#90C356), color-stop(100%,#649A27));
            background: -o-linear-gradient(center top , #90C356 0%, #649A27 100%) repeat scroll 0 0 transparent;
            background: -xv-linear-gradient(center top , #90C356 0%, #649A27 100%) repeat scroll 0 0 transparent;*/
            border: 1px solid transparent;
            background-color: #029FE2;
            color: White;
            display: inline-block;
            font-family: Arial,Helvetica,sans-serif;
            padding: 4px 16px 4px 16px;
            text-decoration: none !important;
            vertical-align: middle;
            font-family: verdana;
            font-size: 12px;
            font-weight: normal;
            margin: 0 6px 0 0;
            padding: 2px 12px;
            text-decoration: none !important;
            vertical-align: middle;
        }
        label, input, button, span, select
        {
            color: #525252;
            font-family: verdana !important;
            font-size: 12px;
            padding: 1px;
            margin-bottom: 4px;
            float: left;
        }
        input[type="text"], textarea, input[type="password"]
        {
            border-radius: 2px 2px 2px 2px;
            border: 1px solid #CCC;
            height: 25px;
            border-image: initial;
        }
select
{
    border: 1px solid #CCC;
    height: 25px !important;
    line-height:20px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="Update_area">
    <div id="StausMsg">
    </div>
    <div class="Heading">
        <table>
            <tr>
                <td style="width: 328px;">
                </td>
                <td style="text-align: center; width: 328px;">
                    <b>Create/Modify User</b>
                </td>
                <td style="text-align: right; width: 328px;">
                    <asp:LinkButton ID="LnkBtnNew" class="buttonImp" runat="server" OnClick="LnkBtnNew_Click">New User</asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
<div id="Check" style="display: none;">
            <asp:UpdatePanel ID="upConfirmation" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblDeleteMsg" runat="server" Text=""></asp:Label>
                    <asp:HiddenField ID="HidDeleteID" runat="server" />
                    <br />
                    <asp:LinkButton ID="lbtnNo" CssClass="Button1" runat="server" OnClientClick="return closeDialog('Check');">OK</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="Confirmation" style="display: none;">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Label ID="LabelConf" runat="server" Text=""></asp:Label>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <br />
                    <br />
                    <asp:LinkButton ID="LinkButtonYes" CssClass="Button1" runat="server" 
                        onclick="LinkButtonYes_Click">Yes</asp:LinkButton>
                    <asp:LinkButton ID="LinkButtonNO" CssClass="Button1" runat="server" OnClientClick="return closeDialog('Confirmation');">No</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
            <div class="maindiv" align="center" style="text-align: center; margin-top: 15px;">
                <!-- Main Div Start-->
                <div class="content">
                    <!-- Heading Close-->
                    <div style="clear: both; margin-top: 15px;">
                    </div>
                    <br />
                    <div style="margin: auto; width: 540px; font-family: Verdana, Geneva, sans-serif;
                        font-size: 12px">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <label>
                                        Full Name:</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPrefix" runat="server" Style="width: 38px; padding-right: 1px;"
                                        class="textfield" placeholder="Prefix"></asp:TextBox><span>&nbsp</span>
                                    <asp:TextBox ID="txtFirst" runat="server" Style="width: 110px; padding-right: 1px;"
                                        class="textfield" placeholder="First" require="Enter FirstName" validate="UserValidate"></asp:TextBox><span>&nbsp</span>
                                    <asp:TextBox ID="txtMiddle" runat="server" Style="width: 110px; padding-right: 1px;"
                                        class="textfield" placeholder="Middle"></asp:TextBox><span>&nbsp</span>
                                    <asp:TextBox ID="txtLast" runat="server" Style="width: 104px; padding-right: 1px;"
                                        class="textfield" placeholder="Last" require="Enter LastName" validate="UserValidate"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        User Name:
                                    </label>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtUsername" runat="server" require="Enter UserName" validate="UserValidate"
                                                class="textfield cumodifywidth"  placeholder="User Name"></asp:TextBox></div>
                                        <div style="float: left; margin-left: 4px; margin-top: 2px;">
                                            <label>
                                                Email:
                                            </label>
                                        </div>
                                        <div style="float: left; margin-top: 0px;">
                                            <asp:TextBox ID="txtEmail" runat="server" Style="width: 223px;" email="example@address.com"
                                                require="Enter Email" placeholder="Email" validate="UserValidate"></asp:TextBox></div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Password:
                                    </label>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtPassowrdUsr" runat="server" class="textfield cumodifywidth"  placeholder="Password"
                                            require="Enter Password" validate="UserValidate" TextMode="Password"></asp:TextBox>
                                            </div>
                                        <div style="float: left; margin-left: 4px; margin-top: 2px; width: 142px;">
                                            <label>
                                                Confirm Password:
                                            </label>
                                        </div>
                                        <div style="float: left; margin-top: 0px;">
                                            <asp:TextBox ID="txtConfirmPasswordUsr" runat="server" compare="Password Mismatch"
                                             compareTo="ctl00_ContentPlaceHolder1_txtPassowrdUsr" placeholder="Password"
                                                require="Enter Conf Password" validate="UserValidate" class="textfield cumodifywidth" TextMode="Password"></asp:TextBox>
                                            </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Home Phone:
                                    </label>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtHomephone" runat="server" onkeypress="return Verify(event);"
                                                class="textfield cumodifywidth" MaxLength="11" placeholder="Phone"></asp:TextBox></div>
                                        <div style="float: left; margin-left: 4px; margin-top: 2px; width: 142px;">
                                            <label>
                                                Office Phone:
                                            </label>
                                        </div>
                                        <div style="float: left; margin-top: 0px;">
                                            <asp:TextBox ID="txtOfficephone" runat="server" class="textfield cumodifywidth" MaxLength="11"
                                                onkeypress="return Verify(event);" placeholder="Phone"></asp:TextBox></div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Cell Phone Primary:
                                    </label>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtCellPhonePrimary" runat="server" require="Enter Cell Phone No."
                                                validate="UserValidate" class="textfield cumodifywidth" MaxLength="11" 
                                                onkeypress="return Verify(event);" placeholder="Phone"></asp:TextBox></div>
                                        <div style="float: left; margin-left: 2px; margin-top: 2px;">
                                            <label>
                                                Cell Phone Secondary:
                                            </label>
                                        </div>
                                        <div style="float: left; margin-top: 0px;">
                                            <asp:TextBox ID="txtCellPhoneSecondary" runat="server" class="textfield cumodifywidth"
                                                MaxLength="11" onkeypress="return Verify(event);" placeholder="Phone"></asp:TextBox></div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        User Role:
                                    </label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbUserRole" runat="server" Style="height: 19px; width: 217px;
                                        float: left;" custom="Select User Role" customFn="var age = parseInt(this.value); return age > 0;"
                                        validate="UserValidate" class="textfield jumpmenuheight">
                                        <asp:ListItem Value="0">Select One</asp:ListItem>
                                    </asp:DropDownList>
                                    <div style="float: left; margin-left: 3px;margin-bottom:5px;margin-top: 2px;">
                                        <asp:LinkButton ID="btnEditRole" class="buttonImp" runat="server" 
                                            onclick="btnEditRole_Click">Edit Role</asp:LinkButton>
                                        <asp:LinkButton ID="btnNewRole"  class="buttonImp" runat="server" 
                                             onclick="btnNewRole_Click">New Roll</asp:LinkButton>
                                        <asp:LinkButton ID="btnDelRoll"  class="buttonImp" runat="server" 
                                            onclick="btnDelRoll_Click" >Delete Roll</asp:LinkButton>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Mailing Address:
                                    </label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMailingaddress" runat="server" require="Enter Mailing Address"
                                        validate="UserValidate" Style="margin-left: 0px; width: 392px;" placeholder="Mailing Address"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDistrict" runat="server" Style="width: 126px;" placeholder="District"
                                        require="Enter District" validate="UserValidate"></asp:TextBox><span>&nbsp</span>
                                    <asp:TextBox ID="txtCity" runat="server" Style="width: 126px;" placeholder="City"
                                        require="Enter City" validate="UserValidate"></asp:TextBox><span>&nbsp</span>
                                    <asp:TextBox ID="txtPostal" runat="server" Style="width: 120px;" placeholder="Postal"
                                        onkeypress="return Verify(event);"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <div>
                                        <div style="float: left; margin-top: 2px;">
                                            <asp:CheckBox ID="chkActive" Text="Active" runat="server" CssClass="CheckBox" />
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:LinkButton ID="btnSave" class="Button1" runat="server" OnClick="btnSave_Click"
                                            OnClientClick="return validate('UserValidate');">Save</asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" class="Button1" runat="server" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                    </div>
                                    <asp:HiddenField ID="hdnUserId" runat="server" />
                                    <asp:HiddenField ID="HiddenSelectedRole" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="DialogRolePermission" style="padding: 0;">
       <iframe src="AddEditRolePermission.aspx" height="509" width="953" id="FramRole" style="border:0 none;"></iframe>
    </div>
    <div id="DialogUserPermission" style="padding: 0;">
        <iframe src="SE_UserPermisssion.aspx" height="509" width="953" id="FramUserID" style="border: 0 none;">
        </iframe>
    </div>
    </div>
</asp:Content>
