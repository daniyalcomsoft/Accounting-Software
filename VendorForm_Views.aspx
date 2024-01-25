<%@ Page Title="" Language="C#" MasterPageFile="Main.master" AutoEventWireup="true" CodeFile="VendorForm_Views.aspx.cs" Inherits="VendorForm_Views" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" language="javascript">
    $(document).ready(function() {
       // CreateModalPopUp('#Confirmation', 290, 100, 'ALERT');
           CreateModalPopUp('#Confirmation', 290, 100, 'ALERT');
        ddlSearch();
    });

    function ddlSearch() {
        $("[id $= ddlSearch]").change(function() {
            var ddlVal = $("[id $= ddlSearch]").val();
            if (ddlVal == "Vendor Name") {
                $("[id $= txtVendorName]").show();
                $("[id $= txtMobileNo]").val('');
                $("[id $= txtMobileNo]").hide();
                 $("[id $= txtEmail]").val('');
                $("[id $= txtEmail]").hide();
            }
            else if (ddlVal == "Mobile No") {
                $("[id $= txtMobileNo]").show();
                $("[id $= txtVendorName]").val('');
                $("[id $= txtVendorName]").hide();
                $("[id $= txtEmail]").val('');
                $("[id $= txtEmail]").hide();
            }
            else {
                $("[id $= txtEmail]").show();
                $("[id $= txtVendorName]").val('');
                $("[id $= txtVendorName]").hide();
                $("[id $= txtMobileNo]").val('');
                $("[id $= txtMobileNo]").hide();
            }
        });
    }
    function setSearchElem() {
        if ($("[id$=ddlSearch]").val() == "Vendor Name") {
            $("[id $= txtVendorName]").show();
            $("[id $= txtMobileNo]").hide();
            $("[id $= txtEmail]").hide();

        }
        else if ($("[id$=ddlSearch]").val() == "Mobile No") {
            $("[id $= txtMobileNo]").show();
            $("[id $= txtVendorName]").hide();
            $("[id $= txtEmail]").hide();

        }

        else {
            $("[id $= txtEmail]").show();
            $("[id $= txtMobileNo]").hide();
            $("[id $= txtVendorName]").hide();

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
                    Vendor Form View
                </td>
                <td style="text-align: right; width: 328px;">
                    <asp:LinkButton ID="btnCancel" CssClass="buttonImp" runat="server" onclick="btnCancel_Click"
                        >Create Vendor</asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <div id="StausMsg">
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
          
    
    
    <div id="content">
       <br />
   <div class="box_body">
        <div class="box_header"><span class="box_txt">Search Vendor</span></div>
        <div class="box_inner">
           
            <asp:Label ID="Label1" runat="server" Text="Search By :"></asp:Label>
            <asp:DropDownList ID="ddlSearch"  runat="server" 
                >
            <asp:ListItem Selected="True" Value="Vendor Name" Text="Vendor Name"></asp:ListItem>
            <asp:ListItem Value="Mobile No" Text="Mobile No"></asp:ListItem>
            <asp:ListItem Value="Email" Text="Email"></asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtVendorName" runat="server" 
                ontextchanged="txtVendorName_TextChanged" ></asp:TextBox> 
             <asp:TextBox ID="txtMobileNo" CssClass="decimalOnly" style="display:none;" runat="server" ontextchanged="txtMobileNo_TextChanged"></asp:TextBox>
             <asp:TextBox ID="txtEmail" style="display:none;" runat="server" ontextchanged="txtEmail_TextChanged"></asp:TextBox>           
            <asp:Button ID="btnSearch" runat="server" Text="Search" cssclass="btn_1" 
                onclick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" cssclass="btn_1" 
                onclick="btnClear_Click" />
        </div>
    </div>
    

    <div class="box_body" style="border: 0px;">
    <div class="box_header" style="margin-bottom: 8px;"><span class="box_txt">Manage Vendor</span></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
        <asp:GridView ID="GridVendorView" runat="server" CssClass="data main" AutoGenerateColumns="False"
        DataKeyNames="Vendor_ID" AllowPaging="true" PageSize="15" >
        <Columns>
                   
            <asp:BoundField DataField="DisplayName" HeaderText="Name"
                SortExpression="DisplayName"/>
            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" 
                SortExpression="CompanyName" />
            <asp:BoundField DataField="Email" HeaderText="Email" 
                SortExpression="Email" />
            <asp:BoundField DataField="BankName" HeaderText="Bank" 
                SortExpression="BankName" />
            <asp:BoundField DataField="AccNo" HeaderText="Account No" SortExpression="AccNo"/>
     
            <asp:TemplateField HeaderText="View" HeaderStyle-Width="35px">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnView" runat="server" cssClass="view"
                        CommandArgument='<%# Eval("Vendor_ID") %>' oncommand="lbtnView_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="31px">
                <ItemTemplate>
                    <asp:LinkButton ID="LbtnEdit" runat="server" cssClass="edit"
                        CommandArgument='<%# Eval("Vendor_ID") %>' oncommand="lbtnEdit_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="41px">
                <ItemTemplate>
                  <%--  <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="delete" 
                    CommandArgument='<%# Eval("Vendor_ID") %>' OnCommand="lbtnDelete_Command"></asp:LinkButton>--%>
                     <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="delete" 
                    CommandArgument='<%# Eval("Vendor_ID") %>' OnCommand="lbtnDelete_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            
        </Columns>
    </asp:GridView>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    </div>
    
   
</asp:Content>

