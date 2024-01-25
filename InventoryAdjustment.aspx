<%@ Page Title="Inventory Adjsutment" Language="C#" MasterPageFile="Main.master" AutoEventWireup="true" 
CodeFile="InventoryAdjustment.aspx.cs" Inherits="InventoryAdjustment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
        body
        {
            
        }
        .tablediv
        {
            width: 850px;
            height: auto;
            margin: 0 auto;
            font-size: 12px;
            font-weight: bold;
        }
        .tablediv table
        {
            width: 100%;
        }
        .tablediv table td
        {
            padding: 3px;
        }
        .tablediv table td label, select, input
        {
            width: 100%;
            float: left;
        }
        input
        {
            height: 30px;
            line-height: 30px;
            border: outset 1px #ccc;
            font-size: 14px;
            width: 270px;
        }
        .tablediv table td select
        {
            height: 30px;
            line-height: 30px;
            border: outset 1px #ccc;
            font-size: 14px;
            width: 276px;
        }
        .input
        {
            height: 30px;
            line-height: 30px;
            border: outset 1px #ccc;
            font-size: 14px;
        }
        .tablediv table td select:focus
        {
            border-color: #48B1D4;
        }
        .btn_1
        {
            height: 30px;
            width: 80px;
            line-height: 27px;
            background-color: #029FE2;
            border-radius: 4px;
            color: #EDF6E3;
            font-size: 12px;
            border: 1px solid #029FE2;
        }
        .btn_1:hover
        {
            background-color: #2C8CB4;
        }
        .highlight, highlight:focus
        {
            border: 1px solid #CD0A0A !important;
            color: Black;
            line-height: 33px;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function() {
            MyDate();
            ChangeDate();
            $('#tabMemberShip').tabs();
            $('#btnSave').hide();
        });
        function MyDate() {
            dateMin = $("[id $= hdnMinDate]").val();
            dateMax = $("[id $= hdnMaxDate]").val();
            $(".DateTimePicker").datepicker({ minDate: new Date(dateMin), maxDate: new Date(dateMax) });
        }
        function ChangeDate() {
            $("[id $= txtDate]").change(function() {
                dateMin = $("[id $= hdnMinDate]").val();
                dateMax = $("[id $= hdnMaxDate]").val();
                var invoiceDate = $("[id$=txtDate]").val();
                if (Date.parse(invoiceDate) < Date.parse(dateMin) || Date.parse(invoiceDate) > Date.parse(dateMax)) {
                    $("[id$=txtDate]").val('');
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:HiddenField ID="hdnMinDate" runat="server" />
       <asp:HiddenField ID="hdnMaxDate" runat="server" />
    <div id="StausMsg">
    </div>
    <div class="Heading">
        <table>
            <tr>
                <td style="width: 328px;">
                </td>
                <td style="text-align: center; width: 328px;">
                    <b class="top_heading">Create/Modify Inventory Adjustment</b>
                </td>
            </tr>
        </table>
    </div>
    <div class="tablediv">
    <asp:UpdatePanel ID="updateAdjust" runat="server">
    <ContentTemplate>
    
        <table>
            <tr>
                <td>
                    <label>
                     <font style="color: red">*</font>Inventory Item</label><br />
                     <asp:DropDownList runat="server" CssClass="input" ID="ddlInventoryItem" validate="group"
                        custom="Select Inventory Item" customFn="var goal = parseInt(this.value); return goal > 0;">
                    </asp:DropDownList>
                </td>
                <td>
                   <label>
                   <font style="color: red">*</font>Action</label><br />
                     <asp:DropDownList runat="server" CssClass="input" ID="ddlAction" validate="group"
                        custom="Select Action" customFn="var goal = parseInt(this.value); return goal > 0;">
                        <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                        <asp:ListItem Value="1" Text="ADD">ADD</asp:ListItem>
                        <asp:ListItem Value="2" Text="DEDUCT">DEDUCT</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>                
                     <label>
                        Date</label><br />
                    <asp:TextBox class="DateTimePicker" placeholder="Date" runat="server" ID="txtDate" require="Select Inventory Adjustment Date" validate="group"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        Quantity</label><br />
                    <asp:TextBox type="text" placeholder="Quantity" CssClass="input decimalOnly" runat="server" ID="txtQuantity"></asp:TextBox>
                </td>
                <td colspan="2">
                    <label>
                        Rate</label><br />
                    <asp:TextBox type="text" placeholder="Rate" CssClass="input decimalOnly" runat="server" ID="txtRate"></asp:TextBox>
                </td>
            </tr>           
        </table>
        
        <table style="float: left; width: 800px; margin-left: 15px;">
            <tr>
                <td rowspan="6" style="text-align: left">
                    <%-- Success --%>
                      <div id="Notification_ItemID" style="display:none; width:98%; margin:auto;">
                <div class="alert-red">
                    <h4>Error!</h4>
                    <label id="IdError" runat="server" style="color:White">Add Atleast One Record</label> 
                </div>
            </div>
        
      
            <div id="Notification_Success" style="display:none; width:98%; margin:auto;">
                <div class="alert-green">
                    <h4>Success</h4>
                    <label id="lblSuccessMsg" runat="server" style="color:White">Adjustment Created Successfully</label>
                </div>
            </div>
  
            <div id="Notification_Error" style="display:none; width:98%; margin:auto;">
                <div class="alert-red">  
                    <h4>Error!</h4>
                        <label id="lblNewError" runat="server" style="color:White">Please Select Item</label> 
                    </div>
                </div>
                    <%-- End --%>
                </td>
            </tr>
        </table>
        <div style="border-top: 1px solid #CCC; padding-top: 10px;">
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn_1" 
                Style="float: right; margin-left: 10px;" onclick="btnCancel_Click" />
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="return validate('group');"
                CssClass="btn_1"  Style="float: right;" onclick="btnSave_Click" />
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    </div>
</asp:Content>

