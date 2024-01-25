<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="Main.master" CodeFile="SE_SuperAdminSetup.aspx.cs"
 Inherits="SE_SuperAdminSetup" Title="Super Admin Setup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            CreateModalPopUp('#Msg', 350, 90, 'Create/Modify Setup');
            });     
    </script>
    
    
    <script language="javascript" type="text/javascript">
    function Verify(evet) {
            var charCode = (evet.which) ? evet.which : event.keyCode
            if (charCode != 9) {
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
            }
            return true;
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
            background: -ms-linear-gradient(center top , #90C356 0%, #649A27 100%) repeat scroll 0 0 transparent;
            background: -moz-linear-gradient(center top , #90C356 0%, #649A27 100%) repeat scroll 0 0 transparent;
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#90C356), color-stop(100%,#649A27));
            background: -o-linear-gradient(center top , #90C356 0%, #649A27 100%) repeat scroll 0 0 transparent;
            background: -xv-linear-gradient(center top , #90C356 0%, #649A27 100%) repeat scroll 0 0 transparent;
            border: 1px solid #AAAAAA; /*box-shadow: 0 1px 0 rgba(0, 0, 0, 0.1), 0 1px 0 rgba(255, 255, 255, 0.5) inset, 0 -1px 0 rgba(255, 255, 255, 0.3) inset;*/
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="Update_area">
    <div class="Heading">SuperAdmin Setup</div>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
            <div class="maindiv" align="center" style="text-align: center; margin-top: 15px;">
                <!-- Main Div Start-->
                <div class="content">
                   
                    <!-- Heading Close-->
                    <div style="clear: both; margin-top: 15px;">
                    </div>
                    <div style="height: 40px">
                        <div id="StausMsg">
                        </div>
                    </div>
                    <br />
                    <div style="margin: auto; width: 540px; font-family: Verdana, Geneva, sans-serif;
                        font-size: 12px">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <label>
                                        Site Code:</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSiteCode" runat="server" Style="width: 38px; padding-right: 1px;"
                                        class="txtSiteCode" placeholder="Code" require="Code" validate="UserValidate"></asp:TextBox><span>&nbsp</span>
                                <div style="float: left; margin-left: 4px; margin-top: 2px;">
                                            <label>
                                                SiteName:
                                            </label>
                                        </div>
                                    <asp:TextBox ID="txtSiteName" runat="server" Style="width: 110px; padding-right: 1px;"
                                        class="txtSiteName" placeholder="SiteName" require="Enter SiteName" validate="UserValidate"></asp:TextBox><span>&nbsp</span>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Description:
                                    </label>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <div style="float: left;">
                                        <asp:TextBox ID="txtDescription" runat="server" Style="width: 110px; padding-right: 1px;"
                                        class="txtDescription" placeholder="Description" require="Enter Description" validate="UserValidate"></asp:TextBox><span>&nbsp</span>
                                        </div>
                                        <div style="float: left; margin-left: 4px; margin-top: 2px;">
                                            <label>
                                                Email:
                                            </label>
                                        </div>
                                        <div style="float: left; margin-top: 0px;">
                                            <asp:TextBox ID="txtEmail" runat="server" Style="width: 223px;" email="example@address.com" placeholder="Contact Person Email"
                                                require="Enter Email" validate="UserValidate"></asp:TextBox></div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Contact Person:
                                    </label>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtContactPerson" runat="server" Style="width: 110px; padding-right: 1px;"
                                        class="txtContactPerson" placeholder="ContactPerson"  validate="UserValidate"></asp:TextBox></div>
                                        <div style="float: left; margin-left: 4px; margin-top: 2px; width: 142px;">
                                            <label>
                                                Contact Phone:
                                            </label>
                                        </div>
                                        <div style="float: left; margin-top: 0px;">
                                            <asp:TextBox ID="txtContactPhone" runat="server" Style="width: 110px; padding-right: 1px;" class="textfield cumodifywidth" MaxLength="11"  placeholder="ContactPhone" require="Enter Phone" validate="UserValidate"
                                                onkeypress="return Verify(event);"></asp:TextBox></div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Contact Address:
                                    </label>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <div style="float: left;">
                                            <asp:TextBox ID="txtContactAddress" runat="server" Style="width: 390px; padding-right: 1px;"
                                        class="txtContactAddress" placeholder="ContactAddress"></asp:TextBox></div>
                                    </div>
                                 </td>
                             </tr>
                             <tr>
                                <td>
                                    <label>
                                        Designation:
                                    </label>
                                 </td>
                                 <td>
                                       <div style="float: left;">
                                        <div style="float: left; margin-top: 0px;">
                                                <asp:TextBox ID="txtDesignation" runat="server" Style="width: 110px; padding-right: 1px;"
                                        class="txtDesignation" placeholder="Designation"></asp:TextBox></div>
                                        <div style="float: left; margin-left: 4px; margin-top: 2px; width: 142px;">
                                            <label>
                                                Custom Agent:
                                            </label>
                                        </div>
                                        <div style="float: left; margin-top: 0px;">
                                            <asp:TextBox ID="txtCustomAgent" runat="server" Style="width: 110px; padding-right: 1px;" class="textfield cumodifywidth" placeholder="Custom Agent" require="Enter Custom Agent" validate="UserValidate"></asp:TextBox>
                                        </div>
                                        
                                      </div>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        SNTN:
                                    </label>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <div style="float: left;">
                                        <asp:TextBox ID="txtSNTN" runat="server" MaxLength="9" Style="width: 110px; padding-right: 1px;"
                                        class="txtSNTN" placeholder="SNTN" require="Enter SNTN" validate="UserValidate"></asp:TextBox><span>&nbsp</span>
                                        </div>
                                        <div style="float: left; margin-left: 4px; margin-top: 2px;">
                                            <label>
                                                Sales Tax Reg No:
                                            </label>
                                        </div>
                                        <div style="float: left; margin-top: 0px;">
                                            <asp:TextBox ID="txtSalesTaxRegNo" runat="server" MaxLength="13" Style="width: 150px;" placeholder="Sales Tax Reg No."
                                                require="Enter Sales Tax Reg No." validate="UserValidate"></asp:TextBox></div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <div>
                                        <div style="float: left; margin-top: 2px; display:none;">
                                            <asp:CheckBox ID="ChkFinancials" Text="Financials" runat="server" CssClass="CheckBox" />
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <div>
                                        <div style="float: left; margin-top: 2px;display:none;">
                                            <asp:CheckBox ID="ChkDepAccount" Text="Deposit Account" runat="server" CssClass="CheckBox" />
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <div>
                                        <div style="float: left; margin-top: 2px;display:none;">
                                            <asp:CheckBox ID="ChkTermDep" Text="Term Desposit" runat="server" CssClass="CheckBox" />
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <div>
                                        <div style="float: left; margin-top: 2px;display:none;">
                                            <asp:CheckBox ID="ChkLoan" Text="Loan" runat="server" CssClass="CheckBox" />
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        <asp:LinkButton ID="btnSave" class="Button1" runat="server" 
                                            OnClientClick="return validate('UserValidate');" onclick="btnSave_Click">Save</asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" class="Button1" runat="server" 
                                            OnClientClick='javascript:history.back()' onclick="btnCancel_Click">Cancel</asp:LinkButton>
                                    </div>
                                    <asp:HiddenField ID="HiddenSetupID" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        
        </ContentTemplate>
    </asp:UpdatePanel>
        <div id="Msg">
            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblMainErrMsg" runat="server" Text="Label"></asp:Label>      
                     <asp:LinkButton ID="LinkButton1" CssClass="Button1" runat="server"  style="float:right;" OnClientClick="return closeDialog('Msg');">Ok</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        </div>
</asp:Content>
