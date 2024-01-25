<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="PayPayment_Views.aspx.cs" Inherits="PayPayment_Views" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            CreateModalPopUp('#Confirmation', 290, 100, 'ALERT');
            ddlSearch();
        });

        function ddlSearch() {
            $("[id $= ddlSearch]").change(function() {
                var ddlVal = $("[id $= ddlSearch]").val();
                if (ddlVal == "Vendor Name") {
                    $("[id $= txtVendorName]").show();
                     $("[id $= txtInvoiceID]").val('');
                    $("[id $= txtInvoiceID]").hide();
                    $("[id $= txtPayPayment_ID]").val('');
                    $("[id $= txtPayPayment_ID]").hide();
                                 
                }
                else if (ddlVal == "Invoice ID") {
                    $("[id $= txtInvoiceID]").show();
                    $("[id $= txtVendorName]").val('');
                    $("[id $= txtVendorName]").hide();
                    $("[id $= txtPayPayment_ID]").val('');
                    $("[id $= txtPayPayment_ID]").hide();
                 
                }
                else if (ddlVal == "Pay Payment ID") {
                $("[id $= txtPayPayment_ID]").show();
                    $("[id $= txtVendorName]").val('');
                    $("[id $= txtVendorName]").hide();
                    $("[id $= txtInvoiceID]").val('');
                    $("[id $= txtInvoiceID]").hide();
                   
                }
                else {

                    $("[id $= txtVendorName]").hide();
                    $("[id $= txtInvoiceID]").val('');
                    $("[id $= txtInvoiceID]").hide();
                    $("[id $= txtPayPayment_ID]").val('');
                    $("[id $= txtPayPayment_ID]").hide();
                }
            });
        }

        function setSearchElem() {
            if ($("[id$=ddlSearch]").val() == "Vendor Name") {
                $("[id $= txtVendorName]").show();
                $("[id $= txtInvoiceID]").hide();
                $("[id $= txtPayPayment_ID]").hide();
               
            }
            else if ($("[id$=ddlSearch]").val() == "Invoice ID") {
                $("[id $= txtInvoiceID]").show();
                $("[id $= txtVendorName]").hide();
                $("[id $= txtPayPayment_ID]").hide();
                
            }
           
            else {
                $("[id $= txtPayPayment_ID]").show();
                $("[id $= txtVendorName]").hide();
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
                 <td style="width: 328px;"></td>
                 <td style="text-align: center; width: 328px;">
                    Purchases Payment View
                 </td>
                 <td style="text-align: right; width: 328px;">
                     <asp:LinkButton ID="btnCreatePurchasesPayment" CssClass="buttonImp" runat="server" 
                         onclick="btnCreatePurchasesPayment_Click" >
                        Create Purchases Payment
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
        <div class="box_header"><span class="box_txt">Search Purchase Payment</span></div>
        <div class="box_inner">
           
            <asp:Label ID="Label1" runat="server" Text="Search By :"></asp:Label>
            <asp:DropDownList ID="ddlSearch"  runat="server" 
                >
            <asp:ListItem Selected="True" Value="Vendor Name" Text="Vendor Name"></asp:ListItem>
            <asp:ListItem Value="Pay Payment ID" Text="Pay Payment ID"></asp:ListItem>
          
            </asp:DropDownList>
            <asp:TextBox ID="txtVendorName" runat="server" 
                ontextchanged="txtVendorName_TextChanged" ></asp:TextBox> 
             <asp:TextBox ID="txtInvoiceID" CssClass="decimalOnly" style="display:none;" runat="server" ontextchanged="txtInvoiceID_TextChanged"></asp:TextBox>
             <asp:TextBox ID="txtPayPayment_ID" CssClass="decimalOnly" style="display:none;" runat="server" ontextchanged="txtPayPayment_ID_TextChanged"></asp:TextBox>           
            
            <asp:Button ID="btnSearch" runat="server" Text="Search" cssclass="btn_1" 
                onclick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" cssclass="btn_1" 
                onclick="btnClear_Click" />
        </div>
    </div>
    

    <div class="box_body" style="border: 0px;">
    <div class="box_header" style="margin-bottom: 8px;"><span class="box_txt">Manage Purchase Payment</span></div>
 
     
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:GridView ID="GridPurchasesPaymentView" runat="server" CssClass="data main" AutoGenerateColumns="False"
        DataKeyNames="PayPayment_ID" AllowPaging="true" PageSize="15"
        onpageindexchanging="GridPurchasesPaymentView_PageIndexChanging" >
        <%--onpageindexchanging="GridCustomerView_PageIndexChanging"--%>
            <Columns>        
            
                <asp:BoundField DataField="PayPayment_ID" HeaderText="Pay Payment ID" SortExpression="InvoiceID"/>
                <asp:BoundField DataField="DisplayName" HeaderText="Vendor Name" SortExpression="InvoiceNo" />
                <asp:BoundField DataField="PaymentDate" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Payment Date" SortExpression="DisplayName" />
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="InvoiceDate" />                
                <asp:BoundField DataField="Total" HeaderText="Total" dataformatstring="{0:#,0.00}" SortExpression="Total" />
                <asp:TemplateField HeaderText="View" HeaderStyle-Width="35px">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnView" runat="server" cssClass="view"
                        CommandArgument='<%# Eval("PayPayment_ID") %>' OnCommand="lbtnView_Command"></asp:LinkButton>                        
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="31px">
                    <ItemTemplate>
                        <asp:LinkButton ID="LbtnEdit" runat="server" cssClass="edit"
                        CommandArgument='<%# Eval("PayPayment_ID") %>' OnCommand="LbtnEdit_Command"></asp:LinkButton>                        
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="41px">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="delete"
                        CommandArgument='<%# Eval("PayPayment_ID") %>' OnCommand="lbtnDelete_Command"></asp:LinkButton>                        
                    </ItemTemplate>
                </asp:TemplateField>            
                
            </Columns>
        </asp:GridView>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="vt_SCGL_SpGetPayPayment" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
    </div>
    
  <asp:Label ID="lblGroupID" runat="server" Visible="false"></asp:Label>
    <div id="Confirmation" style="display:none;">
        <asp:UpdatePanel ID="upConfirmation" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblDeleteMsg" runat="server" Text=""></asp:Label>
                <br /><br /><asp:LinkButton ID="lbtnYes" CssClass="Button1" runat="server" OnClick="lbtnYes_Click" >
                    Yes
                </asp:LinkButton>&nbsp&nbsp
                <asp:LinkButton ID="lbtnNo" CssClass="Button1" runat="server"
                OnClientClick="return closeDialog('Confirmation');">No</asp:LinkButton>            
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

