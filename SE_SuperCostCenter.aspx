<%@ Page Title="Setting Cost Center" Language="C#" MasterPageFile="Main.master" AutoEventWireup="true"
 CodeFile="SE_SuperCostCenter.aspx.cs" Inherits="SE_SuperCostCenter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            CreateModalPopUp('#NewCostCenter', 330, 165, 'Add/Modify Cost Center');
            CreateModalPopUp('#Confirmation', 290, 100, 'ALERT');
        });
    </script>
    
    <style type="text/css">
    .CheckBoxs
        {
            float: left;
        }
        .CheckBoxs input[type=checkbox]
        {
            float: left;
            margin-right: 4px;
            margin-top: 2px;
        }
         .alertbox div
       {
           margin-top:0px;
       }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="Update_area">
    <div class="Heading">
    <table>
        <tr>
            <td style="width:332px;"></td>
            <td style="width:332px;text-align:center;">Cost Center</td>
            <td style="width:332px;float:right;">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:LinkButton ID="btnNew" runat="server" CssClass="buttonImp" OnClick="btnNew_Click">New CostCenter</asp:LinkButton>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnNew" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>    
        </tr>
    </table>
        
    </div>
<div id="StausMsg">
</div>
    <asp:Label ID="lblGroupID" runat="server" Visible="false"></asp:Label>
    
    <div id="Confirmation" style="display:none;">
        <asp:UpdatePanel ID="upConfirmation" runat="server">
            <ContentTemplate>
                
                <asp:Label ID="lblDeleteMsg" runat="server" Text=""></asp:Label>
                <br /><br /><asp:LinkButton ID="lbtnYes" CssClass="Button1" runat="server" OnClick="lbtnYes_Click">Yes</asp:LinkButton>&nbsp&nbsp
                <asp:LinkButton ID="lbtnNo" CssClass="Button1" runat="server"
                        OnClientClick="return closeDialog('Confirmation');">No</asp:LinkButton>
            
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true">
    <ContentTemplate> 
    <div align="center">
    <div style="width:600px;">
    <asp:GridView ID="GridCostCenter" CssClass="main data" runat="server" AutoGenerateColumns="False" 
              DataKeyNames="CostCenterID" AllowPaging="true" PageSize="14" 
            onpageindexchanging="GridCostCenter_PageIndexChanging">
        <Columns>
            <asp:BoundField DataField="CostCenterID" HeaderText="CostCenterID" InsertVisible="False" ItemStyle-HorizontalAlign="Center"
                ReadOnly="True" SortExpression="CostCenterID"  ItemStyle-Width="15%" />
            <asp:BoundField DataField="CostCenterName" HeaderText="CostCenterName" 
                SortExpression="CostCenterName" />
            <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" ReadOnly="True" 
                SortExpression="IsActive" HeaderStyle-Width="41px"/>
            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="31px">
                <ItemTemplate>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:LinkButton ID="lbtnEdit" runat="server" 
                        CommandArgument='<%# Eval("CostCenterID") %>' oncommand="lbtnEdit_Command" cssClass="edit"></asp:LinkButton>
            </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lbtnEdit" EventName="Click" />
              </Triggers>
            </asp:UpdatePanel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="41px">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnDelete" runat="server" cssClass="delete" 
                        CommandArgument='<%# Eval("CostCenterID") %>' oncommand="lbtnDelete_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
        
      <div id="NewCostCenter" style="padding-top: 19px;display:none;"> 
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <ContentTemplate>       
              <table>
                <tr>
                    <td>
                        <label style="width: 100px;">
                            Cost Center ID:
                        </label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCostCenterID" runat="server" AutoComplete="Off" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label style="width: 126px;">
                            Cost Center Name:
                        </label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCostCenterName" placeholder="Cost Center" require="Enter Cost Center Name" validate="CostCenterL" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label style="width: 98px;">
                        </label>
                    </td>
                    <td>
                        <asp:CheckBox ID="ChkIsActive" CssClass="CheckBoxs" Text="Active" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label style="width: 98px;">
                        </label>
                    </td>
                    <td>
                        <asp:LinkButton ID="btnSave" CssClass="Button1" runat="server" 
                                    onclick="btnSave_Click" OnClientClick="return validate('CostCenterL')">Save</asp:LinkButton>
                                <asp:LinkButton ID="btnCancal" CssClass="Button1" runat="server" OnClientClick="return closeDialog('NewCostCenter');">Cancel</asp:LinkButton>
                    </td>
                </tr>
              </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
        </Triggers>
        </asp:UpdatePanel>
      </div>
                <br />
</div>                
</asp:Content>

