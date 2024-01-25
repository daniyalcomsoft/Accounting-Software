<%@ Page Title="" Language="C#" MasterPageFile="Main.master" AutoEventWireup="true"
    CodeFile="InventoryForm.aspx.cs" Inherits="InventoryForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  <script src="Script/jquery-1.6.2.min.js" type="text/javascript" charset="utf-8"></script>
<script src="Script/jquery-ui-1.8.16.custom.min.js" type="text/javascript" charset="utf-8"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            CreateModalPopUp('#InventoryName', 330, 220, 'Add/Modify Inventory Name');
            CreateModalPopUp('#Confirmation', 290, 100, 'ALERT');
            MyDate();
            Verify();
        });
        
        function MyDate() {
        $(".DateTimePicker").datepicker();
    }
    
     function Verify(event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode != 9) {
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    if (charCode != 46) {
                    event.preventDefault();
                }
            }

            return true;
        }
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
       .box_body
        {
            border: 1px solid #029FE2;
            margin-bottom: 10px;
            min-height: 75px;
            color: #444;
        }
         .box_body_Search
        {
            border: 1px solid #029FE2;
            margin-bottom: 10px;
            min-height: 75px;
            color: #444;
            margin-left:200px;
            width: 597px;
        }
        .box_header
        {
        background-color: #029FE2;
        height: 28px;
        }
       
        .box_txt
        {
        font-family: arial;
        text-align: left;
        color: white;
        padding: 4px 0px 0px 8px;
        font-weight: bolder;
        font-size: 15px;
        }
        .box_inner
        {
        padding: 10px;
        }
        .btn_1 {
        height: 25px;
        padding: 0px 10px;
        line-height: 20px;
        background-color: #029FE2;
        border-radius: 4px;
        color: #EDF6E3;
        font-size: 12px;
        border: 1px solid #029FE2;
        border-image: initial;
        font-weight: bold;
        margin-left: 5px;
        }
        .btn_1:hover {
        background-color: #2C8CB4;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="Update_area">
        <div class="Heading">
            <table>
                <tr>
                    <td style="width: 332px;">
                    </td>
                    <td style="width: 332px; text-align: center;">
                        Inventory
                    </td>
                    <td style="width: 332px; float: right;">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:LinkButton ID="btnNew" runat="server" CssClass="buttonImp" OnClick="btnNew_Click">Create Inventory Name</asp:LinkButton>
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
        <div id="content">
<br />
   <div class="box_body_Search">
        <div class="box_header"><span class="box_txt">Search Inventory Name</span></div>
        <div class="box_inner">
           
            <asp:Label ID="Label1" runat="server" Text="Search By Inventory Name : "></asp:Label>
        
            <asp:TextBox ID="txtSearchInventoryName" runat="server" 
                 ></asp:TextBox> 
                        
            <asp:Button ID="btnSearch" runat="server" Text="Search" cssclass="btn_1" 
                onclick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" cssclass="btn_1" 
                onclick="btnClear_Click" />
        </div>
    </div>
    

    <div class="box_body_Search" style="border: 0px;">
    <div class="box_header" style="margin-bottom: 8px;"><span class="box_txt">Manage Inventory Name</span></div>
 
        
        <asp:Label ID="lblGroupID" runat="server" Visible="false"></asp:Label>
        <div id="Confirmation" style="display: none;">
            <asp:UpdatePanel ID="upConfirmation" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblDeleteMsg" runat="server" Text=""></asp:Label>
                    <br /><br /><asp:LinkButton ID="lbtnYes" CssClass="Button1" runat="server" OnClick="lbtnYes_Click">Yes</asp:LinkButton>&nbsp&nbsp
                    <asp:LinkButton ID="lbtnNo" CssClass="Button1" runat="server" OnClientClick="return closeDialog('Confirmation');">No</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true">
            <ContentTemplate>
                <div align="center">
                    <div style="width: 600px;">
                        <asp:GridView ID="GridInventory" CssClass="main data" runat="server" AutoGenerateColumns="False"
                            DataKeyNames="InventoryID" AllowPaging="true" PageSize="14" OnPageIndexChanging="GridInventory_PageIndexChanging">
                            <Columns>
                               <%-- <asp:BoundField DataField="FishID" HeaderText="FishID" InsertVisible="False" ItemStyle-HorizontalAlign="Center"
                                    ReadOnly="True" SortExpression="FishID" ItemStyle-Width="15%" />--%>
                                <asp:BoundField DataField="InventoryName" HeaderText="InventoryName" SortExpression="InventoryName" />
                                <%--<asp:BoundField DataField="InitialQuantity" HeaderText="Initial Quantity" SortExpression="InitialQuantity" />--%>
                                <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
                                <asp:BoundField DataField="AsOfDate" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}" SortExpression="AsOfDate" />
                                <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="31px">
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# Eval("InventoryID") %>'
                                                OnCommand="lbtnEdit_Command" CssClass="edit"></asp:LinkButton>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="lbtnEdit" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="41px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="delete" CommandArgument='<%# Eval("InventoryID") %>' 
                                            OnCommand="lbtnDelete_Command"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="InventoryName" style="padding-top: 19px; display: none;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                <label style="width: 100px;">
                                    Inventory ID:
                                </label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtnventoryID" runat="server" AutoComplete="Off" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label style="width: 126px;">
                                    Inventory Name:
                                </label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtInventoryName" require="Enter Inventory Name" placeholder="Inventory Name" validate="SaveInventory" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <%--<tr>
                            <td>
                                <label style="width: 98px;">
                                Initial Quantity
                                </label>
                            </td>
                            <td>
                               <asp:TextBox ID="txtInitialQuantity" onkeypress="return Verify(event);" require="Enter Initial Quantity" CssClass="decimalOnly" placeholder="Initial Quantity" validate="SaveInventory" runat="server"></asp:TextBox>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                <label style="width: 98px;">
                                Date
                                </label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDate" CssClass="DateTimePicker" require="Enter Date" placeholder="Date" validate="SaveInventory" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                          <tr>
                            <td>
                                <label style="width: 98px;">
                                Rate
                                </label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRate" onkeypress="return Verify(event);" CssClass="decimalOnly" require="Enter Rate" placeholder="Rate" validate="SaveInventory" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label style="width: 98px;">
                                </label>
                            </td>
                            <td>
                                <asp:LinkButton ID="btnSave" CssClass="Button1" runat="server" OnClick="btnSave_Click"
                                    OnClientClick="return validate('SaveInventory')">Save</asp:LinkButton>
                                <asp:LinkButton ID="btnCancal" CssClass="Button1" runat="server" OnClientClick="return closeDialog('InventoryName');">Cancel</asp:LinkButton>
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
