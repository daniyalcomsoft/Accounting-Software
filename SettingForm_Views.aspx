<%@ Page Title="View Settings" Language="C#" MasterPageFile="~/Main.master" 
AutoEventWireup="true" CodeFile="SettingForm_Views.aspx.cs" Inherits="SettingForm_Views" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="Heading">
        <table>
            <tr>
                <td style="width: 328px;">
                </td>
                <td style="text-align: center; width: 328px;">
                    Acc Setting Form View
                </td>
            </tr>
        </table>
    </div>
    
    <div id="StausMsg">
    </div>
      <div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
        <asp:GridView ID="GridSettingView" runat="server" CssClass="data main" AutoGenerateColumns="False"
        DataKeyNames="Setting_Id" AllowPaging="true" PageSize="15" >
        <Columns>
                   
            <asp:BoundField DataField="InventoryAssetAccount" HeaderText="Inventory Asset Account"
                SortExpression="InventoryAssetAccount"/>
            <asp:BoundField DataField="InventoryIncomeAccount" HeaderText="Inventory Income Account" 
                SortExpression="InventoryIncomeAccount" />
            <asp:BoundField DataField="InventoryExpenseAccount" HeaderText="Inventory Expense Account" 
                SortExpression="InventoryExpenseAccount" />
                  <asp:BoundField DataField="DepositAccount" HeaderText="Deposit Account" 
                SortExpression="DepositAccount" />
            
            <asp:TemplateField HeaderText="View" HeaderStyle-Width="35px">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnView" runat="server" cssClass="view"
                        CommandArgument='<%# Eval("Setting_Id") %>' oncommand="lbtnView_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="31px">
                <ItemTemplate>
                    <asp:LinkButton ID="LbtnEdit" runat="server" cssClass="edit"
                        CommandArgument='<%# Eval("Setting_Id") %>' oncommand="lbtnEdit_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            
        </Columns>
    </asp:GridView>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
</asp:Content>

