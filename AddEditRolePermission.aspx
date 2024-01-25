<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddEditRolePermission.aspx.cs"
    Inherits="AddEditRolePermission" Title="Add/Edit Role Permission" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script src="Script/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="Script/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="Script/func.js" type="text/javascript"></script>
    <link href="Script/JQuery/development-bundle/demos/demos.css" rel="stylesheet" type="text/css" />
    <link href="Script/JQuery/development-bundle/themes/base/jquery.ui.all.css" rel="stylesheet"
        type="text/css" />
    <link rel="Stylesheet" href="Css/Style.css"type="text/css" />
    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            $("#tabsRole").tabs();
        });

        function saveRole() {
            var Name;
            Name = document.getElementById("txtRoleName").value;
           //lblText = document.getElementById("lblSaveRole").value;
            if (Name == '') {
                document.getElementById("lblSaveRole").innerHTML = "Enter Role Name";
                return false;
            }

        }
        
          function closeIframe(){
             window.parent.__doPostBack();
             $(parent.top.document.getElementById("DialogRolePermission").parentNode).find(".ui-icon-closethick").trigger("click")   
             //window.parent.$("#DialogRolePermission").dialog('close');
             //window.parent.location.href = window.parent.location.href;
             //return false;
         }
        
        
        
        function saveandclose(){
        //window.parent.top.document.getElementById('#DialogRolePermission').style.display = "none";
        //
        //return true;
        }

        
    </script>

</head>
<body style="background-image:none;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="NewRole" style="padding-left: 31px; padding-top: 33px;">
        <table>
            <tr>
                <td style="width: 100px;">
                    <label>
                        Role Name</label>
                </td>
                <td>
                    <div class="input_text_container">
                        <asp:TextBox ID="txtRoleName" runat="server" ValidationGroup="VgRole" CssClass="txtStyle" autocomplete="off"></asp:TextBox>
                        <asp:Label ID="lblSaveRole" runat="server" ></asp:Label>
                        <%--<asp:RequiredFieldValidator ID="RFieldtxtRoleName" runat="server" ErrorMessage="Role name is required"
                            ControlToValidate="txtRoleName" ValidationGroup="VgRole">*</asp:RequiredFieldValidator>--%>
                        
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        User Status</label>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="cmbRoleStatus">
                        <asp:ListItem Value="0">Inactive</asp:ListItem>
                        <asp:ListItem Value="1">Active</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanelNewRole" runat="server" >
                        <ContentTemplate>
                            <asp:LinkButton ID="btnSaveRole"  CssClass="Button1" runat="server" Text="Save" OnClientClick="return saveRole();" OnClick="btnSaveRole_Click"  />
                            <%--ValidationGroup="VgRole"--%>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="btnCancelRole" runat="server" CssClass="Button1" 
                                OnClientClick="return closeIframe();" >Cancel</asp:LinkButton>
                            <asp:Label ID="lblSave" runat="server" ></asp:Label>
                             <asp:Label ID="lblRoleNameError" runat="server" ForeColor="Red" ></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="format">
                        <h3> page or module permission</h3><br />
                    </div>
                    <div id="tabsRole">
                        <ul>
                            <li><a href="#Module"><span>All Module</span></a></li>
                            <li><a href="#GeneralLedger"><span>Financial</span></a></li>
                             <li><a href="#Sales"><span>Sales</span></a></li>
                             <li><a href="#Purchase"><span>Purchase</span></a></li>
                           <li><a href="#Inventory"><span>Inventory</span></a></li>
                            <li><a href="#Security"><span>Security</span></a></li>
                            <%--<li><a href="#Mortgage"><span>Mortgage</span></a></li>
                            <li><a href="#InterBank"><span>Inter Bank</span></a></li>
--%>                    </ul>
                        <div id="Module" class="grid">
                            <p>
                            <asp:UpdatePanel ID="ModuleUpdatePanel" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridModule" CssClass="GridModule" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="ModuleID" onrowcommand="GridModule_RowCommand" 
                                    onrowdatabound="GridModule_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="ChkSelect_CheckedChanged" />
                                                <asp:Label ID="lblModuleID" runat="server" Text='<%# Eval("ModuleID") %>' Visible="False"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ModuleName" HeaderText="ModuleName" SortExpression="ModuleName" />
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkView" runat="server" AutoPostBack="True" 
                                                    oncheckedchanged="ChkView_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Insert">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkInsert" runat="server" AutoPostBack="True" 
                                                    oncheckedchanged="ChkInsert_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Update">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkUpdate" runat="server" AutoPostBack="True" 
                                                    oncheckedchanged="ChkUpdate_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkDelete" runat="server" AutoPostBack="True" 
                                                    oncheckedchanged="ChkDelete_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                    </Columns>
                                </asp:GridView>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </p>
                        </div>
                        <div id="GeneralLedger" class="grid">
                            <p>
                            <asp:UpdatePanel ID="GeneralLedgerUpdatePanel" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridGeneralLedger" CssClass="GridModule" runat="server" AutoGenerateColumns="False" DataKeyNames="Page_Id">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkSelect" runat="server" AutoPostBack="True" 
                                                    oncheckedchanged="ChkSelect_CheckedChanged1" />
                                                <asp:Label ID="lblPageID" runat="server" Text='<%# Eval("Page_Id") %>' Visible="False"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Page_Name" HeaderText="Page_Name" SortExpression="Page_Name" />
                                        <asp:BoundField DataField="PageDescription" HeaderText="PageDescription" SortExpression="PageDescription" />
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkView" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Insert">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkInsert" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Update">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkUpdate" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkDelete" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                            </p>
                        </div>
                        
                       
                     <%--   <div id="Mortgage" class="grid">
                            <p>
                            <asp:UpdatePanel ID="MortgageUpdatePanel" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridMortgage" CssClass="GridModule" runat="server" AutoGenerateColumns="False" DataKeyNames="Page_Id">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkSelect" runat="server" AutoPostBack="True" 
                                                    oncheckedchanged="ChkSelect_CheckedChanged1" />
                                                <asp:Label ID="lblPageID" runat="server" Text='<%# Eval("Page_Id") %>' Visible="False"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Page_Name" HeaderText="Page_Name" SortExpression="Page_Name" />
                                        <asp:BoundField DataField="PageDescription" HeaderText="PageDescription" SortExpression="PageDescription" />
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkView" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Insert">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkInsert" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Update">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkUpdate" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkDelete" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approve">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkApprove" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                            </p>
                        </div>
                        <div id="InterBank" class="grid">
                            <p>
                            <asp:UpdatePanel ID="InterBankUpdatePanel" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridInterBank" CssClass="GridModule" runat="server" 
                                    AutoGenerateColumns="False" DataKeyNames="Page_Id" Height="148px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkSelect" runat="server" AutoPostBack="True" 
                                                    oncheckedchanged="ChkSelect_CheckedChanged1" />
                                                <asp:Label ID="lblPageID" runat="server" Text='<%# Eval("Page_Id") %>' Visible="False"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Page_Name" HeaderText="Page_Name" SortExpression="Page_Name" />
                                        <asp:BoundField DataField="PageDescription" HeaderText="PageDescription" SortExpression="PageDescription" />
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkView" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Insert">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkInsert" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Update">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkUpdate" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkDelete" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approve">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkApprove" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                            </p>
                        </div>--%>
                        
                        <div id="Sales" class="grid">
                            <p>
                            <asp:UpdatePanel ID="SalesUpdatePanel" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GrdSales" CssClass="GridModule" runat="server" AutoGenerateColumns="False" DataKeyNames="Page_Id">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkSelect" runat="server" AutoPostBack="True" 
                                                    oncheckedchanged="ChkSelect_CheckedChanged1" />
                                                <asp:Label ID="lblPageID" runat="server" Text='<%# Eval("Page_Id") %>' Visible="False"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Page_Name" HeaderText="Page_Name" SortExpression="Page_Name" />
                                        <asp:BoundField DataField="PageDescription" HeaderText="PageDescription" SortExpression="PageDescription" />
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkView" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Insert">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkInsert" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Update">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkUpdate" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkDelete" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                            </p>
                        </div>
                        
                        <div id="Purchase" class="grid">
                            <p>
                            <asp:UpdatePanel ID="PurchaseUpdatePanel" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GrdPurchase" CssClass="GridModule" runat="server" AutoGenerateColumns="False" DataKeyNames="Page_Id">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkSelect" runat="server" AutoPostBack="True" 
                                                    oncheckedchanged="ChkSelect_CheckedChanged1" />
                                                <asp:Label ID="lblPageID" runat="server" Text='<%# Eval("Page_Id") %>' Visible="False"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Page_Name" HeaderText="Page_Name" SortExpression="Page_Name" />
                                        <asp:BoundField DataField="PageDescription" HeaderText="PageDescription" SortExpression="PageDescription" />
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkView" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Insert">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkInsert" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Update">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkUpdate" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkDelete" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                            </p>
                        </div>
                        
                        <div id="Inventory" class="grid">
                            <p>
                            <asp:UpdatePanel ID="InventoryUpdatePanel" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GrdInventory" CssClass="GridModule" runat="server" AutoGenerateColumns="False" DataKeyNames="Page_Id">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkSelect" runat="server" AutoPostBack="True" 
                                                    oncheckedchanged="ChkSelect_CheckedChanged1" />
                                                <asp:Label ID="lblPageID" runat="server" Text='<%# Eval("Page_Id") %>' Visible="False"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Page_Name" HeaderText="Page_Name" SortExpression="Page_Name" />
                                        <asp:BoundField DataField="PageDescription" HeaderText="PageDescription" SortExpression="PageDescription" />
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkView" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Insert">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkInsert" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Update">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkUpdate" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkDelete" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                            </p>
                        </div>
                        
                      
                        
                        <div id="Security" class="grid">
                            <p>
                            <asp:UpdatePanel ID="SecurityUpdatePanel" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridSecurity" CssClass="GridModule" runat="server" AutoGenerateColumns="False" DataKeyNames="Page_Id">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkSelect" runat="server" AutoPostBack="True" 
                                                    oncheckedchanged="ChkSelect_CheckedChanged1" />
                                                <asp:Label ID="lblPageID" runat="server" Text='<%# Eval("Page_Id") %>' Visible="False"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Page_Name" HeaderText="Page_Name" SortExpression="Page_Name" />
                                        <asp:BoundField DataField="PageDescription" HeaderText="PageDescription" SortExpression="PageDescription" />
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkView" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Insert">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkInsert" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Update">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkUpdate" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkDelete" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                     
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                            </p>
                        </div>
                        
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:ValidationSummary ID="ValidSummaryRole" runat="server" ValidationGroup="VgRole" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
