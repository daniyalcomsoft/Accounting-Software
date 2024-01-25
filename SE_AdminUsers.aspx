<%@ Page Title="Edit/Delete Users" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="SE_AdminUsers.aspx.cs" Inherits="SE_AdminUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="Update_area">
    <div class="Heading">
        <table>
            <tr>
                <td style="width: 328px;">
                </td>
                <td style="text-align: center; width: 328px;">
                    Edit/Delete User
                </td>
                <td style="text-align: right; width: 328px;">
                    <asp:LinkButton ID="btnNewUser" CssClass="buttonImp" runat="server" 
                        onclick="btnNewUser_Click">New User</asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <div id="StausMsg">
    </div>
<div id="content">
<br />
<div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <asp:GridView ID="GridUser" runat="server" CssClass="data main" AutoGenerateColumns="False"
        DataKeyNames="UserID" AllowPaging="true" PageSize="15" 
                onpageindexchanging="GridUser_PageIndexChanging">
        <Columns>
        
            <asp:BoundField DataField="FirstName" HeaderText="First Name"
                SortExpression="FirstName"/>
            <asp:BoundField DataField="MiddleName" HeaderText="Middle Name" 
                SortExpression="MiddleName" />
            <asp:BoundField DataField="UserName" HeaderText="User Name" 
                SortExpression="UserName" />
            <asp:BoundField DataField="RoleName" HeaderText="Role Name" 
                SortExpression="RoleName" />
            <asp:BoundField DataField="Email" HeaderText="Email Address" SortExpression="Email"/>
     <%--       <asp:CheckBoxField DataField="SpecialPermission" HeaderText="SpecialPermission" 
                SortExpression="SpecialPermission" />--%>
            <asp:BoundField DataField="Active" HeaderText="IsActive"
                SortExpression="Active" />
            <asp:TemplateField HeaderText="View" HeaderStyle-Width="35px">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnView" runat="server" cssClass="view"
                        CommandArgument='<%# Eval("UserID") %>' oncommand="lbtnView_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="31px">
                <ItemTemplate>
                    <asp:LinkButton ID="LbtnEdit" runat="server" cssClass="edit"
                        CommandArgument='<%# Eval("UserID") %>' oncommand="LbtnEdit_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="41px">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnDelete" runat="server"  cssClass="delete"
                        CommandArgument='<%# Eval("UserID") %>' OnClientClick="return confirm('Are you sure to do this');" CommandName="Del" 
                        oncommand="lbtnDelete_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            
        </Columns>
    </asp:GridView>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="vt_SCGL_SE_SPGetAllUserInfo" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
    </div>
    </div>
</asp:Content>

