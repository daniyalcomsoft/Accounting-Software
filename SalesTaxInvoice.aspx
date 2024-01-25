<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="SalesTaxInvoice.aspx.cs"
    Inherits="SalesTaxInvoice" Title="Create Sales Tax Invoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        $(document).ready(function() {
        CreateModalPopUp('#Confirmation', 280, 120, 'ALERT');
        MyDate();
        $('#FindJob').dialog({
            autoOpen: false,
            draggable: true,
            title: "Find",
            width: 972,
            height: 450,
            open: function(type, data) {
                $(this).parent().appendTo("form");
            }
        });
        
        });

        

        function MyDate() {
            dateMin = $("[id $= hdnMinDate]").val();
            dateMax = $("[id $= hdnMaxDate]").val();
            $(".DateTimePicker").datepicker({ minDate: new Date(dateMin), maxDate: new Date(dateMax) });
        }
        
    </script>

    <style type="text/css">
        .btn_1
        {
            height: 30px;
            width: 100px;
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
        .btn_spacing
        {
            margin-right: 5px;
        }
        .float_right
        {
            float: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnMinDate" runat="server" />
    <asp:HiddenField ID="hdnMaxDate" runat="server" />
    
    <div class="Update_area">
        <div id="StausMsg">
        </div>
        <div class="Heading">
            <table>
                <tr>
                    <td style="width: 328px;">
                    </td>
                    <td style="text-align: center; width: 328px;">
                        <b>Create/Modify Sales tax Invoice</b>
                    </td>
                </tr>
            </table>
        </div>
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
          <ContentTemplate>
        <div class="head">

            <div class="bodyarea" style="margin: 0 auto; width: 630px;">
                <table>
                    <tr>
                        <td>
                            Sales Tax Invoice ID
                        </td>
                        <td>
                            Date
                        </td>
                        <td>
                            JobNo
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtSalesTaxInvoiceID" runat="server" Style="padding-right: 1px; width: 200px;"
                                Cssclass="decimalOnly" placeholder="Sales Tax Invoice ID"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtdate" runat="server" Style="padding-right: 1px; width: 200px;"
                                CssClass="DateTimePicker" require="Enter Date" validate="SaveInvoice" placeholder="Date"></asp:TextBox>
                        </td>
                        <td>
                           <asp:TextBox ID="txtJobNo" runat="server" Style="padding-right: 1px;width: 150px;"
                                class="textfield" placeholder="Job No" ontextchanged="txtJobNo_TextChanged" AutoPostBack="true"
                                require="Enter Job No." validate="SaveInvoice"></asp:TextBox>
                           <asp:LinkButton ID="btnFind" runat="server" CausesValidation="False" style="margin-left:2px;" CssClass="search" onclick="btnFind_Click1"/>
                           
                           <asp:Label ID="lblJobID" runat="server" Visible="false" ></asp:Label>
                        </td>
                        
                    </tr>
                    <tr>
                        <td colspan="2">
                            Packages
                        </td>
                         <td>
                            Reference Number
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtPackages" runat="server" Style="padding-right: 1px; width: 406px;"
                                class="textfield" placeholder="Packages"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRefNo" runat="server" Style="padding-right: 1px; width: 200px;"
                                class="textfield" placeholder="Reference Number" AutoPostBack="True" 
                                ontextchanged="txtRefNo_TextChanged"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            Service Charges
                        </td>
                        <td>
                            Other Unrecepient Expenses
                        </td>
                        <td>
                            Amount of Additional Tax
                        </td>
                       
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtServiceCharges" runat="server" Style="padding-right: 1px; width: 200px"
                                CssClass="decimalOnly" placeholder="Service Charges"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOUE" runat="server" Style="padding-right: 1px; width: 200px;"
                                CssClass="decimalOnly" placeholder="Other Unrecepient Expenses"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAmtAT" runat="server" Style="padding-right: 1px; width: 200px;"
                                CssClass="decimalOnly" placeholder="Amount of Additional Tax"></asp:TextBox>
                        </td>

                    </tr>
                    <%--  <tr>
                        <td>
                            <table style="float: right; width: 200px;">
                                <tr>
                                    <td rowspan="2">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" Style="width: 80px; height: 25px; margin: 5px 0 0 20px;"
                                            OnClientClick="return validate('group');" CssClass="buttonImp" OnClick="btnSave_Click" />
                                    </td>
                                    <td rowspan="2">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Style="width: 80px; height: 25px; margin: 5px 0 0 08px;"
                                            CssClass="buttonImp" OnClick="btnCancel_Click" />
                                    </td>
                                    <td rowspan="2"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>--%>
                </table>
            </div>
            <%-- <table style="float: left; width: 800px; margin-left: 15px;">
                <tr>
                    <td rowspan="6" style="text-align: left">
                        
                        <div id="Notification_Success" style="display: none; width: 98%; margin: auto;">
                            <div class="alert-green">
                                <h4>Success:
                                </h4>
                                <asp:Label ID="lblmsg" runat="server" Style="color: White"></asp:Label>
                            </div>
                        </div>
                       
                        <div id="Notification_Error" style="display: none; width: 98%; margin: auto;">
                            <div class="alert-red">
                                <h4>Error!</h4>
                                <asp:Label ID="lblNewError" runat="server" Style="color: White">
                                </asp:Label>
                            </div>
                        </div>
                        
                    </td>
                </tr>
            </table>--%>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <div style="margin: 0 auto; width: 630px;">
            <asp:UpdatePanel ID="updpnlbtn" runat="server">
                <ContentTemplate>
                    <div style="float: right;">
                        <table style="width: 620px;">
                            <tr>
                                <td style="text-align: right">
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                        CssClass="btn_1 btn_spacing float_right" />
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn_1 btn_spacing float_right"
                                        OnClick="btnSave_Click" OnClientClick="return validate('SaveInvoice');" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div id="Notification_ItemID" style="display: none; width: 98%; margin: auto;">
                        <div class="alert-red">
                            <h4>
                                Error!</h4>
                            <label id="IdError" runat="server" style="color: White">Add Atleast One Record</label>
                        </div>
                    </div>
                    <div id="Notification_Success" style="display: none; width: 98%; margin: auto;">
                        <div class="alert-green">
                            <h4>
                                Success</h4>
                            <label id="lblSuccessMsg" runat="server" style="color: White">Invoice Created Successfully</label>
                        </div>
                    </div>
                    <div id="Notification_Error" style="display: none; width: 98%; margin: auto;">
                        <div class="alert-red">
                            <h4>
                                Error!</h4>
                            <label id="lblNewError" runat="server" style="color: White">Please Select Item</label>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div style="clear: both;"></div>    
    
    </div>
    <div id="FindJob" style="padding: 0;">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
             <ContentTemplate>
                 <br /><br /><label style="padding-left:20px">Enter Job No. </label>
                 &nbsp;<asp:TextBox ID="txtAccountNo" CssClass="autoCompleteCodes" runat="server"></asp:TextBox><asp:Button ID="btnFindAcc" runat="server" Text="Find" CssClass="buttonImp" 
                     style="float:none;width:60px;" onclick="btnFindAcc_Click" />
                     <br />
                 <asp:GridView ID="GrdAccounts" runat="server" CssClass="data main" 
                     AllowPaging="true" PageSize="15" style="clear:both;"
                     onpageindexchanging="GrdAccounts_PageIndexChanging">
                     <Columns>
                         <asp:TemplateField>
                             <ItemTemplate>
                                 <asp:LinkButton ID="lnkSelect" runat="server" onclick="lnkSelect_Click" >Select</asp:LinkButton>
                             </ItemTemplate>
                             
                         </asp:TemplateField>
                         
                   <%--     <asp:TemplateField>
                             <ItemTemplate>
                                 <asp:Label ID="lblgrdJobID" runat="server" Text='<%#Eval("JobID") %>'></asp:Label>
                             </ItemTemplate>
                             
                         </asp:TemplateField>--%>
                     
                     </Columns>
                  </asp:GridView>
             </ContentTemplate>
          </asp:UpdatePanel>
      </div>
      <div id="Confirmation" style="display: none;">
            <asp:UpdatePanel ID="upConfirmation" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblDeleteMsg" runat="server" Text="No Record Found"></asp:Label>
                    <asp:HiddenField ID="HidDeleteID" runat="server" />
                    <br />
                    <br />
                    <br />
                    <asp:LinkButton ID="lbtnNo" CssClass="Button1" runat="server" OnClientClick="return closeDialog('Confirmation');">OK</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
</asp:Content>
