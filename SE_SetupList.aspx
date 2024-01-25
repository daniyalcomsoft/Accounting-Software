<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Main.master" CodeFile="SE_SetupList.aspx.cs" 
Inherits="SE_SetupList" Title="Setup List" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript" language="javascript">
    $(document).ready(function() 
    {
        $('#Confirmation').dialog({
            autoOpen: false,
            draggable: true,
            title: "Delete Voucher",
            width: 386,
            height: 95,
            open: function(type, data) {
                $(this).parent().appendTo("form");
            }
        });
    }
    );
    function OpenUserPermission() {
        showDialog('Confirmation');
        return false;
    }
</script>
    <style type="text/css">
        .style1
        {
            width: 114px;
        }
        .style2
        {
            width:179px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
   <div class="Update_area">
   <div class="Heading">
     Setup List
   </div>
   <div id="StausMsg">
                        </div>
    <table style="width: 100%;">
        
        <tr>
            <td>
                <br />
                <asp:GridView ID="GridSetup" runat="server" CssClass="data main" 
                    AutoGenerateColumns="False" AllowPaging="True" PageSize="15">
                    <Columns>
                        <asp:BoundField DataField="CreatedDate" HeaderText="Date" 
                            ReadOnly="True" SortExpression="CreatedDate" HeaderStyle-Width="84px" 
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dddd, dd/M/yyyy}" >
                            <HeaderStyle Width="84px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SiteCode" HeaderText="Code" HeaderStyle-Width="50px"
                            SortExpression="SiteCode" >
                            <HeaderStyle Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SiteName" HeaderText="Name" HeaderStyle-Width="104px"
                            SortExpression="SiteName" >
                            <HeaderStyle Width="104px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-Width="300px"
                            SortExpression="Description" >
                            <HeaderStyle Width="300px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="View" HeaderStyle-Width="31px">
                            <ItemTemplate>
                                <asp:Label ID="LblSetupID" runat="server" 
                                    Text='<%# Eval("SetupID") %>' Visible="False"></asp:Label>
                                <asp:LinkButton ID="LbtnView" runat="server" cssClass="view" CommandName="View" 
                                    CommandArgument='<%# Container.DataItemIndex %>' 
                                    oncommand="LbtnView_Command"></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="31px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="31px">
                            <ItemTemplate>
                                <%--<asp:LinkButton ID="lbtnEdit" runat="server" 
                                    CommandArgument='<%# Eval("VoucherTypeID") %>' CommandName="Edited" 
                                    oncommand="lbtnEdit_Command" >Edit</asp:LinkButton>--%>
                                    <asp:LinkButton ID="lbtnEdit" runat="server" cssClass="edit" 
                                    CommandArgument='<%# Container.DataItemIndex %>' CommandName="Edited" oncommand="lbtnEdit_Command" 
                                    ></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="31px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="43px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDelete" runat="server" cssClass="delete" CommandName="deleted"
                                    CommandArgument='<%# Container.DataItemIndex %>' 
                                     OnClientClick = "return OpenRolePermission('Add');" 
                                    oncommand="lbtnDelete_Command"></asp:LinkButton>
                                <asp:Label ID="LabelSetupID" runat="server" 
                                    Text='<%# Eval("SetupID") %>' Visible="False"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="43px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                    
                    SelectCommand="SELECT [SetupID], [SiteName], [SiteCode], [Description], [CreatedDate] FROM [vt_SCGL_SE_SuperAdminSetup]"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
     <div id="Confirmation" style="display: none;">
        <asp:UpdatePanel ID="upConfirmation" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblDeleteMsg" runat="server" Text=""></asp:Label>
                <br /><br />
                <asp:LinkButton ID="lbtnYes" CssClass="Button1" runat="server" onclick="lbtnYes_Click" 
                    >Yes</asp:LinkButton>
                <asp:LinkButton ID="lbtnNo" CssClass="Button1" runat="server" OnClientClick="return closeDialog('Confirmation');">No</asp:LinkButton>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
</asp:Content>
