﻿<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="DutiesDescription.aspx.cs" Inherits="DutiesDescription" Title="Duties Description Manager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <div id="StausMsg">
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="grdInvoiceDutiesDesc" runat="server" CssClass="data main" DataKeyNames="InvoiceDutiesDescID"
                DataSourceID="ObjInvoiceDutiesDesc" AutoGenerateColumns="False" OnRowEditing="grdInvoiceDutiesDesc_RowEditing"
                OnRowUpdating="grdInvoiceDutiesDesc_RowUpdating">
                <Columns>
                    <asp:TemplateField HeaderText="Duties Description">
                        <ItemTemplate>
                            <label>
                                <%#Eval("InvoiceDutiesDescription")%></label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtInvoiceDutiesDescription" runat="server" Style="width: 100%;" Text='<%#Eval("InvoiceDutiesDescription")%>'></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="txtInvoiceDutiesDescription" runat="server" Style="width: 100%;" Text='<%#Eval("InvoiceDutiesDescription")%>'></asp:TextBox>
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
                                runat="server" CommandName="Delete" Visible='<%#Eval ("IsInvoiceDutiesDescExist") %>' CssClass="delete"></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="41px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjInvoiceDutiesDesc" runat="server" SelectMethod="Read" TypeName="SCGL.BAL.InvoiceDutiesDesc"
                UpdateMethod="Update" DataObjectTypeName="SCGL.BAL.InvoiceDutiesDesc" OnUpdating="ObjInvoiceDutiesDesc_Updating"
                DeleteMethod="Delete" InsertMethod="Create"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
