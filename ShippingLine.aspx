<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="ShippingLine.aspx.cs" 
Inherits="ShippingLine" Title="Shipping Line Manager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <div id="StausMsg">
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="grdShippingLine" runat="server" CssClass="data main" DataKeyNames="ShippingLineID"
                DataSourceID="ObjShippingLine" AutoGenerateColumns="False" OnRowEditing="grdShippingLine_RowEditing"
                OnRowUpdating="grdShippingLine_RowUpdating">
                <Columns>
                    <asp:TemplateField HeaderText="Shipping Line Description">
                        <ItemTemplate>
                            <label>
                                <%#Eval("ShippingLine")%></label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtShippingLine" runat="server" Style="width: 100%;" Text='<%#Eval("ShippingLine")%>'></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="txtShippingLine" runat="server" Style="width: 100%;" Text='<%#Eval("ShippingLine")%>'></asp:TextBox>
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="40px">
                        <ItemTemplate>
                            <center>
                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="edit" CommandName="Edit" ToolTip="Edit"></asp:LinkButton>
                            </center>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <center>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="btnUpdate" runat="server" CssClass="update" CommandName="Update"
                                                ToolTip="Update"></asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="btnCancel" runat="server" CssClass="cancel" CommandName="Cancel"
                                                ToolTip="Cancel"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </EditItemTemplate>
                        <HeaderStyle Width="40px"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="41px">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                runat="server" CommandName="Delete" Visible='<%#Eval ("IsShippingExist") %>'  CssClass="delete"></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="41px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjShippingLine" runat="server" SelectMethod="Read" TypeName="SCGL.BAL.ShippingLine_BAL"
                UpdateMethod="Update" DataObjectTypeName="SCGL.BAL.ShippingLine_BAL" OnUpdating="ObjShippingLine_Updating"
                DeleteMethod="Delete" InsertMethod="Create"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
