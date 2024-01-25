<%@ Page Title="Voucher List" Language="C#" MasterPageFile="~/Main.master"
 AutoEventWireup="true" CodeFile="GLHome.aspx.cs" Inherits="GLHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {

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
            ddlSearch();
           
        });
    function OpenUserPermission() {
        showDialog('Confirmation');
        return false;
    }
    function ddlSearch() {
           $("[id $= ddlSearch]").change(function() {
               var ddlVal = $("[id $= ddlSearch]").val();
               if (ddlVal == "Voucher Type") {
                   $("[id $= txtVoucherType]").show();
                   $("[id $= txtVoucherNo]").val('');
                   $("[id $= txtVoucherNo]").hide();
               }
               else if (ddlVal == "Voucher No") 
               {
                    $("[id $= txtVoucherNo]").show();
                     $("[id $= txtVoucherType]").val('');
                    $("[id $= txtVoucherType]").hide();
               }
           });
       }

       function setSearchElem() {
           if ($("[id $=ddlSearch]").val() == "Voucher Type") {
               $("[id $= txtVoucherType]").show();
               $("[id $= txtVoucherNo]").hide();

           }
           else {
               $("[id $= txtVoucherNo]").show();
               $("[id $= txtVoucherType]").hide();

           }
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div  class="Update_area">
   <div class="Heading">
     Voucher List
   </div>
   <div id="StausMsg">
                    </div>
    <table style="width: 100%;">
        <tr>
            <td class="style1">
                <label>New Transaction</label>
            </td>
            <td class="style2">
                 <asp:DropDownList ID="cmbAddNewVoucher" runat="server" 
                    onselectedindexchanged="cmbAddNewVoucher_SelectedIndexChanged" 
                    AutoPostBack="True" CssClass="style2">
                    <asp:ListItem Selected="True" Value="0">Add New</asp:ListItem>
                    <asp:ListItem Value="1">General Voucher</asp:ListItem>
                    <asp:ListItem Value="3">Cash Reciept Voucher</asp:ListItem>
                    <asp:ListItem Value="2">Cash Payment Voucher</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
              
            </td>
        </tr>
        <tr>
            <td class="style1">
                <label>Show</label>
            </td>
            <td class="style2">
                <asp:DropDownList ID="cmbTransactionFilter" runat="server" 
                    AppendDataBoundItems="True" AutoPostBack="True" CssClass="style2"
                    onselectedindexchanged="cmbTransactionFilter_SelectedIndexChanged">
                    <asp:ListItem Value="0">All Transaction</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <div class="box_inner">
           
            <asp:Label ID="Label1" runat="server" Text="Search By :"></asp:Label>
            <asp:DropDownList ID="ddlSearch"  runat="server" >
            <asp:ListItem Selected="True" Value="Voucher Type" Text="Voucher Type"></asp:ListItem>
            <asp:ListItem Value="Voucher No" Text="Voucher No"></asp:ListItem>
              </asp:DropDownList>
            <asp:TextBox ID="txtVoucherType" runat="server"  
                ontextchanged="txtVoucherType_TextChanged" ></asp:TextBox> 
             <asp:TextBox ID="txtVoucherNo" CssClass="decimalOnly" style="display:none;"  runat="server" ontextchanged="txtVoucherNo_TextChanged"></asp:TextBox>         
            <asp:Button ID="btnSearch" runat="server" Text="Search" cssclass="btn_1" 
                onclick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" cssclass="btn_1" 
                onclick="btnClear_Click" />
        </div>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <br />
                <asp:GridView ID="GridVoucher" runat="server" CssClass="data main" 
                    AutoGenerateColumns="False" 
                    onrowdatabound="GridVoucher_RowDataBound" AllowPaging="True"
                            PageSize="15"
                    onpageindexchanging="GridVoucher_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="VoucharDate" HeaderText="Date" 
                            ReadOnly="True" SortExpression="VoucharDate" HeaderStyle-Width="84px" dataformatstring="{0:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="VoucherNumber" HeaderText="Number" HeaderStyle-Width="50px"
                            SortExpression="VoucherNumber" />
                        <asp:BoundField DataField="VoucherTypeName" HeaderText="Type" HeaderStyle-Width="104px"
                            SortExpression="VoucherTypeName" />
                        <asp:BoundField DataField="Narration" HeaderText="Narration" HeaderStyle-Width="300px"
                            SortExpression="Narration" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-Width="80px" ReadOnly="True" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" 
                            SortExpression="Amount" />
                        <asp:TemplateField HeaderStyle-Width="15px">
                            <ItemTemplate>
                                <asp:Label ID="lblVoucherTypeID" runat="server" 
                                    Text='<%# Eval("VoucherTypeID") %>' Visible="False"></asp:Label>
                                <asp:LinkButton ID="LbtnView" runat="server" cssClass="view" CommandName="View" 
                                    CommandArgument='<%# Container.DataItemIndex %>' oncommand="LbtnView_Command"></asp:LinkButton>
                                <asp:Label ID="lblVoucherNumber" runat="server" Text='<%# Eval("VoucherNumber") %>' Visible="False"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="15px">
                            <ItemTemplate>
                                <%--<asp:LinkButton ID="lbtnEdit" runat="server" 
                                    CommandArgument='<%# Eval("VoucherTypeID") %>' CommandName="Edited" 
                                    oncommand="lbtnEdit_Command" >Edit</asp:LinkButton>--%>
                                    <asp:LinkButton ID="lbtnEdit" runat="server" cssClass="edit" 
                                    CommandArgument='<%# Container.DataItemIndex %>' CommandName="Edited" 
                                    oncommand="lbtnEdit_Command" ></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField >
                        <asp:TemplateField  HeaderStyle-Width="15px">
                            <ItemTemplate >
                                <asp:LinkButton ID="lbtnDelete" runat="server" cssClass="delete" CommandName="deleted"
                                    CommandArgument='<%# Container.DataItemIndex %>' 
                                    oncommand="lbtnDelete_Command" OnClientClick = "return OpenRolePermission('Add');"></asp:LinkButton>
                                <asp:Label ID="lblVoucherNumberDel" runat="server" 
                                    Text='<%# Eval("VoucherNumber") %>' Visible="False"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT  DISTINCT Convert(varchar(20),VoucharDate,101) AS VoucharDate, VoucherNumber,VoucherTypeID, VoucherTypeName, Narration,ISNULL(Debit,Credit) AS Amount
FROM         vt_SCGL_Transaction"></asp:SqlDataSource>
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
                <asp:LinkButton ID="lbtnYes" CssClass="Button1" runat="server" 
                    onclick="lbtnYes_Click">Yes</asp:LinkButton>
                <asp:LinkButton ID="lbtnNo" CssClass="Button1" runat="server" OnClientClick="return closeDialog('Confirmation');">No</asp:LinkButton>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

