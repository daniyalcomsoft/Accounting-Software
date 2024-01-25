<%@ Page Title="" Language="C#" MasterPageFile="Main.master" AutoEventWireup="true" CodeFile="InventoryForm_Views.aspx.cs" Inherits="InventoryForm_Views" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            CreateModalPopUp('#Confirmation', 290, 100, 'ALERT');
            ddlSearch();
        });

        function ddlSearch() {
            $("[id $= ddlSearch]").change(function() {
                var ddlVal = $("[id $= ddlSearch]").val();
                if (ddlVal == "Fish Name") {
                    $("[id $= txtFishName]").show();
                     $("[id $= txtFishSize]").val('');
                    $("[id $= txtFishSize]").hide();
                     $("[id $= txtFishGrade]").val('');
                    $("[id $= txtFishGrade]").hide();
//                    $("[id $= txtRate]").val('');
//                    $("[id $= txtRate]").hide();
                }
                else if (ddlVal == "Fish Size") {
                    $("[id $= txtFishSize]").show();
                     $("[id $= txtFishName]").val('');
                    $("[id $= txtFishName]").hide();
                    $("[id $= txtFishGrade]").val('');
                    $("[id $= txtFishGrade]").hide();
//                    $("[id $= txtRate]").val('');
//                    $("[id $= txtRate]").hide();
                }
                else {
                    $("[id $= txtFishGrade]").show();
                     $("[id $= txtFishName]").val('');
                    $("[id $= txtFishName]").hide();
                     $("[id $= txtFishSize]").val('');
                    $("[id $= txtFishSize]").hide();
//                    $("[id $= txtRate").val('');
//                    $("[id $= txtRate").hide();
                }
               
            });
        }

        function setSearchElem() {
            if ($("[id$=ddlSearch]").val() == "Fish Name") {
                $("[id $= txtFishName]").show();
                $("[id $= txtFishSize]").hide();
                $("[id $= txtFishGrade]").hide();
//                $("[id $= txtRate]").hide();
            }
            else if ($("[id$=ddlSearch]").val() == "Fish Grade"){
                $("[id $= txtFishGrade]").show();
                $("[id $= txtFishName]").hide();
                $("[id $= txtFishSize]").hide();
              //  $("[id $= txtRate").hide();
            }
            else {
                $("[id $= txtFishSize]").show();
                $("[id $= txtFishName]").hide();
                $("[id $= txtFishGrade]").hide();
               // $("[id $= txtRate]").hide();
            }
            
        }
        
        
    </script>
    <style type="text/css">
        .box_body
        {
            border: 1px solid #029FE2;
            margin-bottom: 10px;
            min-height: 75px;
            color: #444;
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="Heading">
        <table>
            <tr>
                <td style="width: 328px;">
                </td>
                <td style="text-align: center; width: 328px;">
                    Product Information Form View
                </td>
                <td style="text-align: right; width: 328px;">
                    <asp:LinkButton ID="btnCancel" CssClass="buttonImp" runat="server" onclick="btnCancel_Click"
                        >Create Inventory</asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <div id="StausMsg">
    </div>
       
    <div id="content">
    <br />
   <div class="box_body">
        <div class="box_header"><span class="box_txt">Search Inventory</span></div>
        <div class="box_inner">
           
            <asp:Label ID="Label1" runat="server" Text="Search By :"></asp:Label>
            <asp:DropDownList ID="ddlSearch"  runat="server" 
                >
            <asp:ListItem Selected="True" Value="Inventory Name" Text="Inventory Name"></asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtInventoryName" runat="server" 
                ></asp:TextBox> 
            
            <asp:Button ID="btnSearch" runat="server" Text="Search" cssclass="btn_1" 
                onclick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" cssclass="btn_1" 
                onclick="btnClear_Click" />
        </div>
    </div>
    

    <div class="box_body" style="border: 0px;">
    <div class="box_header" style="margin-bottom: 8px;"><span class="box_txt">Manage Inventory</span></div>
    <div style="overflow:auto;">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
        <asp:GridView ID="GridInventoryView" runat="server" CssClass="data main" AutoGenerateColumns="False"
        DataKeyNames="Inventory_Id" AllowPaging="true" PageSize="15" onpageindexchanging="GridInventoryView_PageIndexChanging" >
        
        <Columns>
            <asp:BoundField DataField="InventoryID" HeaderText="Inventory ID"
                SortExpression="InventoryID"/>       
            <asp:BoundField DataField="InventoryName" HeaderText="Inventory Name"
                SortExpression="InventoryName"/>
            <asp:BoundField DataField="InitialQuantity" HeaderText="Initial Quantit" 
                SortExpression="InitialQuantity" />
            <asp:BoundField DataField="Rate" HeaderText="Rate" 
                SortExpression="Rate" />
            <asp:BoundField DataField="Date" HeaderText="Date" 
                SortExpression="Date" />
            
     
            <asp:TemplateField HeaderText="View" HeaderStyle-Width="35px">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnView" runat="server" cssClass="view"
                        CommandArgument='<%# Eval("InventoryID") %>' oncommand="lbtnView_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="31px">
                <ItemTemplate>
                    <asp:LinkButton ID="LbtnEdit" runat="server" cssClass="edit"
                        CommandArgument='<%# Eval("InventoryID") %>' oncommand="lbtnEdit_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="41px">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="delete" 
                    CommandArgument='<%# Eval("InventoryID") %>' OnCommand="lbtnDelete_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            
        </Columns>
    </asp:GridView>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    </div>
    <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server"   OnClientClick="return confirm('Are you sure to delete');"
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="vt_SCGL_SPGetInventoryRecord" SelectCommandType="StoredProcedure"></asp:SqlDataSource>--%>
    </div>
    
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
    
    
    
    
</asp:Content>

