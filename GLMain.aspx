<%@ Page Title="Chart Of Accounts" Language="C#" MasterPageFile="Main.master" AutoEventWireup="true"
    CodeFile="GLMain.aspx.cs" Inherits="GLMain" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            CreateModalPopUp('#Msg', 350, 90, 'Alert');
            CreateModalPopUp('#Confirmation', 280, 120, 'Delete Main Account');
            $("#MainConfirm").dialog({
                resizable: false,
                height: 150,
                autoOpen: false,
                modal: true,
                buttons: {
                    "Yes": function() {
                        $('#<%=lblStatus.ClientID%>').text('Yes');
                        $(this).dialog("close");
                    },
                    "No": function() {
                        $('#<%=lblStatus.ClientID%>').text('No');
                        $(this).dialog("close");
                    }
                }
            });
            CreateModalPopUp('#ControlConfirmation', 280, 120, 'Delete Control Account');
            $("#MainConfirm").dialog({
                resizable: false,
                height: 150,
                autoOpen: false,
                modal: true,
                buttons: {
                    "Yes": function() {
                        $('#<%=lblStatus.ClientID%>').text('Yes');
                        $(this).dialog("close");
                    },
                    "No": function() {
                        $('#<%=lblStatus.ClientID%>').text('No');
                        $(this).dialog("close");
                    }
                }
            });
            CreateModalPopUp('#SubsidaryConfirmation', 280, 120, 'Delete Subsidary Account');
            $("#MainConfirm").dialog({
                resizable: false,
                height: 150,
                autoOpen: false,
                modal: true,
                buttons: {
                    "Yes": function() {
                        $('#<%=lblStatus.ClientID%>').text('Yes');
                        $(this).dialog("close");
                    },
                    "No": function() {
                        $('#<%=lblStatus.ClientID%>').text('No');
                        $(this).dialog("close");
                    }
                }
            });

        });
        function mainclick() {
            showDialog('MainConfirm');
        }
       
    </script>

    <style type="text/css">
        .style1
        {
            width: 141px;
        }
        .style2
        {
            width: 417px;
        }
        .acc_list
        {
            float: left;
            width: 200px;
            height: 100px;
        }
        .acc_controls
        {
            float: left;
            font-size: 12px;
            height: 100px;
            margin-top: -20px;
        }
        input[type="text"]
        {
            width: 199px;
        }
        select
        {
            width: 203px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" OnAsyncPostBackError="ToolkitScriptManager1_AsyncPostBackError">
    </cc1:ToolkitScriptManager>--%>
    <div class="Update_area">
        <div class="Heading">
            Chart of Accounts
        </div>
        <div id="StausMsg">
                    </div>
        <div id="Msg">
            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                <ContentTemplate>
                
                    <asp:Label ID="lblMainErrMsg" runat="server" Text="Label"></asp:Label><br />
                    
                    <asp:LinkButton ID="LinkButton1" CssClass="Button1" runat="server" OnClientClick="return closeDialog('Msg');">Ok</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="Confirmation" style="display: none;">
            <asp:UpdatePanel ID="upConfirmation" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblDeleteMsg" runat="server" Text=""></asp:Label>
                    <asp:HiddenField ID="HidDeleteID" runat="server" />
                    <br />
                    <br />
                    <br />
                    <asp:LinkButton ID="lbtnYes" CssClass="Button1" runat="server" OnClick="lbtnYes_Click">Yes</asp:LinkButton>
                    <asp:LinkButton ID="lbtnNo" CssClass="Button1" runat="server" OnClientClick="return closeDialog('Confirmation');">No</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="ControlConfirmation" style="display: none;">
            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblContDelete" runat="server" Text=""></asp:Label>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <br />
                    <br />
                    <br />
                    <asp:LinkButton ID="lnkConYes" CssClass="Button1" OnClick="lbtnConYes_Click" runat="server" >Yes</asp:LinkButton>
                    <asp:LinkButton ID="lnkConNo" CssClass="Button1" runat="server" OnClientClick="return closeDialog('ControlConfirmation');">No</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="SubsidaryConfirmation" style="display: none;">
            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblSubsDelete" runat="server" Text=""></asp:Label>
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    <br />
                    <br />
                    <br />
                    <asp:LinkButton ID="lnkSubYes" CssClass="Button1" runat="server" OnClick="lnkSubYes_Click" >Yes</asp:LinkButton>
                    <asp:LinkButton ID="lnkSubNo" CssClass="Button1" runat="server" OnClientClick="return closeDialog('SubsidaryConfirmation');">No</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!---Main ACCOUNT TYPE ends --->
        <div class="Account_Type">
            <h3 style="text-align: left; font-size: 13px;">
                Main Account Type</h3>
            <div class="Account_Type_Child">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                    
                        <asp:ListBox ID="ListMain" runat="server" Style="height: 124px; width: 290px;" 
                            AutoPostBack="True" onselectedindexchanged="ListMain_SelectedIndexChanged"
                           ></asp:ListBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ListMain" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnSaveMain" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="acc_controls">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnAddMain" CssClass="buttonImp" Style="float: left" runat="server"
                                        Text="Add" onclick="btnAddMain_Click"  />
                                    <asp:LinkButton ID="btnEditMain" CssClass="buttonImp" Style="float: left" runat="server"
                                        Text="Edit" Enabled="False" onclick="btnEditMain_Click"   />
                                    <asp:LinkButton ID="btnDeleteMain" CssClass="buttonImp" Style="float: left" runat="server"
                                        Text="Delete" Enabled="False" onclick="btnDeleteMain_Click"  />
                                </td>
                                <td>
                                    <asp:Label ID="lblMainMsg" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr class="Account_form">
                                <td>
                                    <div class="Account_input_main_form">
                                        <div class="Account_text_form">
                                            Main Code:</div>
                                    </div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMainCode" require="enter main code" validate="mainadd" runat="server"
                                        onkeypress="return IsNumber(event);" onblur="Generate2DigitCode(this);" MaxLength="2"></asp:TextBox>
                                    <asp:HiddenField ID="HidIsMainUpdatable" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="lblMainCode" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Account_text_form">
                                    Main Title:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMainTitle" require="enter main title" validate="mainadd" runat="server"
                                        ValidationGroup="VGMain" MaxLength="30"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="Account_text_form" style="width: 150px">
                                    Select Account Nature:
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbMainNature" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="Account_text_form">
                                    Status:
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbMainStatus" runat="server" 
                                        AutoPostBack="True">
                                        <asp:ListItem Value="0">InActive</asp:ListItem>
                                        <asp:ListItem Value="1">Active</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnSaveMain" CssClass="Button1" runat="server" Text="Save"
                                        OnClientClick="return validate('mainadd')" Enabled="False" OnClick="btnSaveMain_Click" />
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" Enabled="false" runat="server" TargetControlID="btnSaveMain"
                                        PopupControlID="PNL" CancelControlID="btnPopupCancel" BackgroundCssClass="modalBackground" />
                                    <asp:Panel ID="PNL" runat="server" Style="display: none; width: 200px; background-color: White;
                                        border-width: 2px; border-color: Black; border-style: solid; padding: 20px;">
                                        Do you want to active all child account relavent to this main account
                                        <br />
                                        <br />
                                        <div style="text-align: right;">
                                            <asp:LinkButton ID="btnYes" CssClass="Button1" runat="server" Text="Yes"  />
                                            <asp:LinkButton ID="btnNo" CssClass="Button1" runat="server" Text="No"  />
                                            <asp:LinkButton ID="btnPopupCancel" CssClass="Button1" runat="server" Text="Cancel" />
                                        </div>
                                    </asp:Panel>
                                    <asp:LinkButton ID="btnCancelMain" CssClass="Button1" runat="server" Text="Cancel"
                                       OnClick="btnCancelMain_Click"  Enabled="false" />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAddMain" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnEditMain" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnSaveMain" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelMain" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="cmbMainStatus" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnYes" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnNo" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <!--Control Account Type-->
        <div class="Account_Type">
            <h3 style="text-align: left; font-size: 13px;">
                Control Account Type</h3>
            <!-- Control account Type list box-->
            <div class="Account_Type_Child">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:ListBox ID="ListControlAcc" runat="server" Style="height: 124px; width: 290px;"
                            AutoPostBack="True" 
                            onselectedindexchanged="ListControlAcc_SelectedIndexChanged" >
                        </asp:ListBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ListControlAcc" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="acc_controls">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnAddControl" CssClass="buttonImp" Style="float: left" runat="server"
                                        Text="Add" OnClick="btnAddControl_Click"  />
                                    <asp:LinkButton ID="btnEditControl" CssClass="buttonImp" Style="float: left" runat="server"
                                         Enabled="false" OnClick="btnEditControl_Click">Edit</asp:LinkButton>
                                    <asp:LinkButton ID="btnControlDelete" OnClick="btnControlDelete_Click" CssClass="buttonImp" Style="float: left"                                         Enabled="false" runat="server">Delete</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:Label ID="lblControlMsg" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="Account_input_main_form">
                                        <div class="Account_text_form">
                                            Main Code:</div>
                                    </div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtControlMainCode" runat="server" ReadOnly="True" MaxLength="2"></asp:TextBox>
                                    </div>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="Account_text_form" style="width: 150px;">
                                    Control Code:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtControlCode" require="Enter Control Account Code" validate="VGControl"
                                        onkeypress="return IsNumber(event);" onblur="Generate3DigitCode(this);" runat="server"
                                        MaxLength="3"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblControl" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Account_text_form">
                                    Control Title:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtControlTitle" runat="server" require="Enter Control Account Title"
                                        validate="VGControl" MaxLength="30"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="Account_text_form">
                                    Status:
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbControlStatus" runat="server">
                                        <asp:ListItem Value="0">InActive</asp:ListItem>
                                        <asp:ListItem Value="1">Active</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:HiddenField ID="hdnControlDeleteable" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnSaveControl" CssClass="Button1" runat="server" Text="Save"
                                        OnClick="btnSaveControl_Click" OnClientClick="return validate('VGControl')" Enabled="false" />
                                    <asp:LinkButton ID="btnCancelControl" CssClass="Button1" runat="server" Text="Cancel"
                                       OnClick="btnCancelControl_Click" Enabled="false" />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAddControl" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnEditControl" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnSaveControl" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelControl" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <!--- ACCOUNT TYPE ends --->
        <div class="Account_Type" style="height: 165px;">
            <h3 style="text-align: left; font-size: 13px;">
                Subsidiary Account Type</h3>
            <!-- Control account Type list box-->
            <div class="Account_Type_Child">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:ListBox ID="ListSubsidary" runat="server" Style="height: 124px; width: 290px;"
                            AutoPostBack="True" 
                            onselectedindexchanged="ListSubsidary_SelectedIndexChanged" >
                        </asp:ListBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ListSubsidary" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="acc_controls">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnSubAdd" CssClass="buttonNew" Style="float: left" runat="server"
                                       OnClick="btnSubAdd_Click" Text="Add"  />
                                    <asp:LinkButton ID="btnSubEdit" CssClass="buttonImp" Style="float: left" runat="server"
                                        Text="Edit" Enabled="False" OnClick="btnSubEdit_Click"  />
                                    <asp:LinkButton ID="btnSubDelete" CssClass="buttonImp" Style="float: left" runat="server"
                                        Text="Delete" Enabled="False" OnClick="btnSubDelete_Click" />
                                </td>
                                <td>
                                    <asp:Label ID="lblSubMsg" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="Account_input_main_form">
                                        <div class="Account_text_form">
                                            Main Code:</div>
                                    </div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSubMainCode" runat="server" ReadOnly="True" MaxLength="2"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="Account_text_form">
                                    Control Code:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSubControlCode" runat="server" ReadOnly="True" MaxLength="3"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="Account_text_form" style="width: 150px;">
                                    Subsidiary Code:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSubCode" onkeypress="return IsNumber(event);" require="Enter Subsidiary Code"
                                        validate="VGSub" onblur="Generate4DigitCode(this);" runat="server" MaxLength="4"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblSub" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Account_text_form">
                                    Subsidiary Title:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSubTitle" runat="server" require="Enter Subsidiary title" validate="VGSub"
                                        MaxLength="45"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="Account_text_form">
                                    Status:
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbSubStatus" runat="server">
                                        <asp:ListItem Value="0">InActive</asp:ListItem>
                                        <asp:ListItem Value="1">Active</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <%--<asp:Label ID="Label3" runat="server" Visible="False"></asp:Label>--%>
                                    <asp:HiddenField ID="hdnSubsidaryDeleteable" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnSubSave" CssClass="Button1" runat="server" Text="Save" 
                                        OnClientClick="return validate('VGSub')" Enabled="false" OnClick="btnSubSave_Click" />
                                    <asp:LinkButton ID="btnSubCancel" CssClass="Button1" runat="server" Text="Cancel"
                                      OnClick="btnSubCancel_Click"   Enabled="false" />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSubAdd" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnSubEdit" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnSubSave" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnSubCancel" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        </div>
</asp:Content>
