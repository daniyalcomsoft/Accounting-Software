<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="SalesTaxInvoice_View.aspx.cs" 
Inherits="SalesTaxInvoice_View" Title="View Sales Tax Invoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            CreateModalPopUp('#Confirmation', 290, 100, 'ALERT');
            ddlSearch();
            bindCustomers();
        });

        function ddlSearch() {
            $("[id $= ddlSearch]").change(function () {
                var ddlVal = $("[id $= ddlSearch]").val();
                if (ddlVal == "Invoice ID") {
                    $("[id $= txtInvoiceID]").show();
                    $("[id $= txtCustomerName]").val('');
                    $("[id $= txtCustomerName]").hide();
                    // $("[id $= txtCustomerName]").val()='';
                }
                else {
                    $("[id $= txtCustomerName]").show();
                    $("[id $= txtInvoiceID]").val('');
                    $("[id $= txtInvoiceID]").hide();
                }

            });
        }

        function bindCustomers() {


            $("[id$=txtCustomerName]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "WebMethod.aspx/Customers",
                        data: "{ 'Match': '" + request.term + "'}",
                        dataType: "json",
                        success: function (Data) {
                            response($.map(Data.d, function (item) {
                                return {
                                    CusID: item.CustomerID,
                                    label: item.CustomerName
                                }
                            }))
                        }
                    });
                },
                minLength: 1,
                select: function (event, ui) {
                    $("[id$=ddlCustomer]").val(ui.item.CusID);
                }
            });
        }





        function setSearchElem() {
            if ($("[id $=ddlSearch]").val() == "Invoice ID") {
                $("[id $= txtInvoiceID]").show();

                $("[id $= txtCustomerName]").hide();
            }
            else {
                $("[id $= txtCustomerName]").show();

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
                    Sales Tax Invoice View
                 </td>
                 <td style="text-align: right; width: 328px;">
                     <asp:LinkButton ID="btnCreateSalesInvoice" CssClass="buttonImp" runat="server" 
                         onclick="btnCreateSalesInvoice_Click">
                        Create Sales Tax Invoice
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
        <div class="box_header"><span class="box_txt">Search Sales Tax Invoice</span></div>
        <div class="box_inner">
           
            <asp:Label ID="Label1" runat="server" Text="Search By :"></asp:Label>
            <asp:DropDownList ID="ddlSearch"  runat="server" 
                >
            <asp:ListItem Selected="True" Value="Invoice ID" Text="Expense ID"></asp:ListItem>
            <asp:ListItem Value="Customer Name" Text="Customer Name"></asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtInvoiceID" CssClass="decimalOnly" runat="server" 
                ontextchanged="txtInvoiceID_TextChanged"></asp:TextBox> 
             <asp:TextBox ID="txtCustomerName" style="display:none;" runat="server"></asp:TextBox>            
            <asp:Button ID="btnSearch" runat="server" Text="Search" cssclass="btn_1" 
                onclick="btnSearch_Click"  />
            <asp:Button ID="btnClear" runat="server" Text="Clear" cssclass="btn_1" 
                onclick="btnClear_Click" />
        </div>
    </div>
    

    <div class="box_body" style="border: 0px;">
    <div class="box_header" style="margin-bottom: 8px;"><span class="box_txt">Manage Sales Tax Invoice</span></div>    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:GridView ID="GridSalesTaxInvoiceView" runat="server" CssClass="data main" AutoGenerateColumns="False"
        DataKeyNames="SalesTaxInvoiceID" AllowPaging="true" PageSize="15" 
            onpageindexchanging="GridSalesTaxInvoiceView_PageIndexChanging">
        
            <Columns>        
            
                <asp:BoundField DataField="SalesTaxInvoiceID" HeaderText="Sales Tax ID" SortExpression="SalesTaxInvoiceID"/>
                <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}" SortExpression="Date"/>
                <asp:BoundField DataField="JobNumber" HeaderText="Job No." SortExpression="JobNumber"/>
                <asp:BoundField DataField="Reference" HeaderText="Reference" SortExpression="Reference"/>
         
                <asp:TemplateField HeaderText="View" HeaderStyle-Width="35px">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnView" runat="server" cssClass="view"
                        CommandArgument='<%# Eval("SalesTaxInvoiceID") %>' OnCommand="lbtnView_Command"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="31px">
                    <ItemTemplate>
                        <asp:LinkButton ID="LbtnEdit" runat="server" cssClass="edit"
                        CommandArgument='<%# Eval("SalesTaxInvoiceID") %>' OnCommand="LbtnEdit_Command" ></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="41px">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="delete"
                        CommandArgument='<%# Eval("SalesTaxInvoiceID") %>' OnCommand="lbtnDelete_Command"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>            
                
            </Columns>
        </asp:GridView>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="vt_SCGL_SpGetallSalesTaxInvoices" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
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

