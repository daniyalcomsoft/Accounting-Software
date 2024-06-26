﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="PurchaseInvoice_Views.aspx.cs" Inherits="PurchaseInvoice_Views" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            CreateModalPopUp('#Confirmation', 290, 100, 'ALERT');
            ddlSearch();
            bindCustomers();
        });

        function ddlSearch() {
            $("[id $= ddlSearch]").change(function() {
                var ddlVal = $("[id $= ddlSearch]").val();
                if (ddlVal == "Invoice ID") {
                    $("[id $= txtInvoiceID]").show();
                     $("[id $= txtVendorName]").val('');
                    $("[id $= txtVendorName]").hide();
                    
                }
                else {
                    $("[id $= txtVendorName]").show();
                    $("[id $= txtInvoiceID]").val('');
                    $("[id $= txtInvoiceID]").hide();
                }

            });
        }

             function setSearchElem() {
            if ($("[id $=ddlSearch]").val() == "Invoice ID") {
                $("[id $= txtInvoiceID]").show();
                $("[id $= txtVendorName]").hide();
            }
            else {
                $("[id $= txtVendorName]").show();
                $("[id $= txtInvoiceID]").hide();
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
                 <td style="width: 328px;"></td>
                 <td style="text-align: center; width: 328px;">
                     Purchase Invoice View
                 </td>
                 <td style="text-align: right; width: 328px;">
                     <asp:LinkButton ID="btnCreatePurchasesInvoice" CssClass="buttonImp" runat="server" 
                         onclick="btnCreatePurchasesInvoice_Click">
                         Create Purchases Invoice
                     </asp:LinkButton>
                 </td>
             </tr>
         </table>
    </div>
    <div id="StausMsg">
    </div>
    <div id="content">
        <br />
   <div class="box_body">
        <div class="box_header"><span class="box_txt">Search Invoice</span></div>
        <div class="box_inner">
           
            <asp:Label ID="Label1" runat="server" Text="Search By :"></asp:Label>
            <asp:DropDownList ID="ddlSearch"  runat="server" 
                >
            <asp:ListItem Selected="True" Value="Invoice ID" Text="Invoice ID"></asp:ListItem>
            <asp:ListItem Value="Vendor Name" Text="Vendor Name"></asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtInvoiceID" runat="server" CssClass="decimalOnly"
                ontextchanged="txtInvoiceID_TextChanged"></asp:TextBox> 
             <asp:TextBox ID="txtVendorName" style="display:none;" runat="server" ontextchanged="txtVendorName_TextChanged"></asp:TextBox>            
            <asp:Button ID="btnSearch" runat="server" Text="Search" cssclass="btn_1" 
                onclick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" cssclass="btn_1" 
                onclick="btnClear_Click" />
        </div>
    </div>
    

    <div class="box_body" style="border: 0px;">
    <div class="box_header" style="margin-bottom: 8px;"><span class="box_txt">Manage Invoice</span></div>    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:GridView ID="GridPurchasesInvoiceView" runat="server" CssClass="data main" AutoGenerateColumns="False"
        DataKeyNames="pInvoiceID" AllowPaging="true" PageSize="15"
        onpageindexchanging="GridPurchasesInvoiceView_PageIndexChanging">
        <%--onpageindexchanging="GridCustomerView_PageIndexChanging"--%>
            <Columns>        
            
                <asp:BoundField DataField="pInvoiceID" HeaderText="Invoice ID" SortExpression="pInvoiceID"/>
              
                <asp:BoundField DataField="DisplayName" HeaderText="Vendor Name" SortExpression="DisplayName" />
                <asp:BoundField DataField="InvoiceDate" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Invoice Date" SortExpression="InvoiceDate" />
                <asp:BoundField DataField="Total" HeaderText="Amount" dataformatstring="{0:#,0.00}" SortExpression="Total"/>
         
                <asp:TemplateField HeaderText="View" HeaderStyle-Width="35px">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnView" runat="server" cssClass="view"
                        CommandArgument='<%# Eval("pInvoiceID") %>' OnCommand="lbtnView_Command"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="35px" />
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="31px">
                    <ItemTemplate>
                        <asp:LinkButton ID="LbtnEdit" runat="server" cssClass="edit"
                        CommandArgument='<%# Eval("pInvoiceID") %>' ToolTip='<%# Eval("TMessage") %>' OnCommand="LbtnEdit_Command" ></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="31px" />
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="41px">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="delete"
                        CommandArgument='<%# Eval("pInvoiceID") %>' ToolTip='<%# Eval("TMessage") %>' OnCommand="lbtnDelete_Command"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="41px" />
                </asp:TemplateField>            
                
            </Columns>
        </asp:GridView>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="vt_SCGL_SpGetpInvoice" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
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
</asp:Content>


