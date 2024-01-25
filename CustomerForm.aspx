<%@ Page Title="Create Customer" Language="C#" MasterPageFile="Main.master" AutoEventWireup="true"
    CodeFile="CustomerForm.aspx.cs" Inherits="CustomerForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            MyDate();
            $('#tabMemberShip').tabs();
            $('#btnSave').hide();

        });
        function MyDate() {
            dateMin = $("[id $= hdnMinDate]").val();
            dateMax = $("[id $= hdnMaxDate]").val();
            $(".DateTimePicker").datepicker({ minDate: new Date(dateMin), maxDate: new Date(dateMax) });
        }
        function Verify(evet) {
            var charCode = (evet.which) ? evet.which : event.keyCode
            if (charCode != 9) {
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
            }
            return true;
        }
    </script>

    <style type="text/css">
        body
        {
           
        }
        label
        {
            font-size: 12px;
            font-weight: bold;
        }
        select
        {
            border: 1px solid #CCC;
            height: 25px;
            line-height: 20px;
            border-image: initial;
        }
        input[type=text], textarea, input[type=password]
        {
            border-radius: 2px 2px 2px 2px;
            border: 1px solid #CCCCCC;
            height: 25px;
        }
        .bottomleftheading
        {
            font-family: Verdana, Geneva, sans-serif;
            font-size: 18px;
            float: left;
            margin-top: 14px;
            width: 227px;
            height: 40px;
            color: #525252;
        }
        .head
        {
            text-align: center;
            width: 100%;
            font-size: 12px;
        }
        .bodyarea
        {
             font-size: 12px;
            font-weight: bold;
            width: 800px;
            height: 480px auto;
        }
        .bodyarea h1
        {
            font-family: Arial;
            font-weight: bold;
            font-size: 18px;
            text-align: left;
        }
        .name
        {
            width: 150px;
            text-align: left;
        }
        .title
        {
            width: 50px;
            text-align: left;
        }
        .fname
        {
            width: 120px;
            text-align: left;
        }
        .lname
        {
            width: 120px;
            text-align: left;
        }
        .suffix
        {
            width: 70px;
            text-align: left;
        }
        .email
        {
            text-align: left;
        }
        .email label
        {
            margin-left: 20px;
        }
        .company
        {
            text-align: left;
        }
        .phone
        {
            text-align: left;
        }
        .mobile
        {
            text-align: left;
        }
        .fax
        {
            text-align: left;
        }
        .PortofDischarge
        {
            text-align: left;
        }
        .Dest
        {
            text-align: left;
        }
        .Consignee
        {
            text-align: left;
        }
        .Buyer
        {
            text-align: left;
        }
        .phone label
        {
            margin-left: 20px;
        }
        
        .mobile label
        {
            margin-left: 0px;
        }
        .fax label
        {
            margin-left: 0px;
        }
        .PortofDischarge label
        {
            margin-left: 0px;
        }
        .Dest label
        {
            margin-left: 0px;
        }
        .Consignee label
        {
            margin-left: 20px;
        }
        .Buyer label
        {
            margin-left: 0px;
        }
        
        .disp
        {
            text-align: left;
        }
        .other
        {
            text-align: left;
        }
        .website
        {
            text-align: left;
        }
        .disp span
        {
            color: #ee0000;
        }
        .other label
        {
            margin-left: 20px;
        }
        .website label
        {
            margin-left: 0px;
        }
        .bill
        {
            text-align: left;
        }
        .ship
        {
            text-align: left;
        }
        .ship label
        {
            margin-left: 20px;
        }
        .chkship
        {
            margin-left: 0px;
            float: left;
            width: 194px;
        }
        .terms
        {
            text-align: left;
        }
        .openBlnc
        {
            text-align: left;
        }
        .AsofDate label
        {
            text-align: left;
            margin-left: 20px;
        }
        table tr td
        {
            padding-right: 1px;
            text-align: left;
            font-family: Verdana, Geneva, sans-serif;
            font-size: 12px;
        }
        .tabfieldsbottom
        {
            width: 760px;
            height: 90%;
            margin: 0px 0px 0px -20px;
        }
        .highlight, highlight:focus
        {
            /*background-image: none !important;
    background-color: #fffacd !important;	
	background: url("../images/arrow.png") repeat-x scroll 50% 50% #FEF1EC;*/
            border: 1px solid #CD0A0A !important;
            color: Black;
            line-height: 33px;
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
                        <b>Create/Modify Customer</b>
                    </td>
                </tr>
            </table>
        </div>
        <table style="float: left; width: 800px; margin-left: 15px;">
                                <tr>
                                    <td rowspan="6" style="text-align: left">
                                        <%-- Success --%>
                                        <div id="Notification_Success" style="display: none; width: 98%; margin: auto;">
                                            <div class="alert-green">
                                                <h4>
                                                    Success:
                                                </h4>
                                                <asp:Label ID="lblmsg" runat="server" Style="color: White"></asp:Label>
                                            </div>
                                        </div>
                                        <%--Error Message--%>
                                        <div id="Notification_Error" style="display: none; width: 98%; margin: auto;">
                                            <div class="alert-red">
                                                <h4>
                                                    Error!</h4>
                                                <asp:Label ID="lblNewError" runat="server" Style="color: White">
                                                </asp:Label>
                                            </div>
                                        </div>
                                        <%-- End --%>
                                    </td>
                                </tr>
          </table>
          <div style="clear: both">
                    </div>
        <div class="head">
            <div class="bodyarea" style="margin: 0 auto;">
                <table>
                    <tr>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td class="title">
                                        Title
                                    </td>
                                    <td class="fname">
                                        First name
                                    </td>
                                    <td class="lname">
                                        Last name
                                    </td>
                                    <td class="suffix">
                                        Suffix
                                    </td>
                               
                                    <td class="other">
                                        <label>
                                            NTN</label>
                                    </td>
                                    <td class="website">
                                        <label>
                                           Sales Tax Reg No.</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtTitle" runat="server" Style="width: 44px; padding-right: 1px;"
                                            class="textfield" placeholder="Mr."></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtfirstName" runat="server" Style="width: 114px; padding-right: 1px;"
                                            class="textfield" placeholder="First name"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtlastName" runat="server" Style="width: 114px; padding-right: 1px;"
                                            class="textfield" placeholder="Last name"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSuffix" runat="server" Style="width: 75px; padding-right: 1px;"
                                            class="textfield" placeholder="Suffix"></asp:TextBox>
                                    </td>
                                     <td>
                                        <asp:TextBox ID="txtNTN" runat="server" Style="width: 130px; margin-left: 20px;
                                            padding-right: 1px;" class="textfield" placeholder="NTN"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSalesTaxReg" runat="server" Style="width: 268px; margin-left: 0px;
                                            padding-right: 1px;" class="textfield" placeholder="Sales Tax Reg No."></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td class="company">
                                        Company/ Customer
                                    </td>
                                    <td class="phone">
                                        <label>
                                            Phone</label>
                                    </td>
                                    <td class="mobile">
                                        <label>
                                            Mobile</label>
                                    </td>
                                    <td class="fax">
                                        <label>
                                            Fax</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtCompany" runat="server" Style="width: 365px; padding-right: 1px;"
                                            class="textfield" placeholder="Company Name"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPhone" runat="server" Style="width: 130px; margin-left: 20px;
                                            padding-right: 1px;" class="textfield" placeholder="Phone"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMobile" runat="server" Style="width: 120px; margin-left: 0px;
                                            padding-right: 1px;" class="textfield" placeholder="Mobile"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFax" runat="server" Style="width: 140px; margin-left: 0px; padding-right: 1px;"
                                            class="textfield" placeholder="Fax"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td class="disp">
                                        <span>*</span> Display name as
                                    </td>
                                      <td class="email">
                                        <label>
                                            Email</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtDisplayName" runat="server" Style="width: 365px; padding-right: 1px;"
                                            validate="group" require="Please enter a unique Display Name" class="textfield"
                                            placeholder="Display Name"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail" runat="server" Style="width: 405px; margin-left: 20px;
                                            padding-right: 1px;" class="textfield" validate="group" email="example@address.com" placeholder="Email"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td class="company">
                                        Print on check as
                                        <asp:CheckBox ID="chkPrintDispName" Text="Use display name" runat="server" />
                                    </td>
                                    <td class="phone">
                                        <label>
                                            Bank</label>
                                    </td>
                                    <td class="mobile">
                                        <label>
                                            Account No.</label>
                                    </td>
                                    <td class="fax">
                                        <label>
                                            IBAN No.</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtPrint" runat="server" Style="width: 365px; padding-right: 1px;"
                                            class="textfield"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBank" runat="server" Style="width: 130px; margin-left: 20px;
                                            padding-right: 1px;" class="textfield" placeholder="Bank"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAccNo" runat="server" Style="width: 120px; margin-left: 0px;
                                            padding-right: 1px;" class="textfield" placeholder="Account No."></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtIBAN" runat="server" Style="width: 140px; margin-left: 0px; padding-right: 1px;"
                                            class="textfield" placeholder="IBAN No."></asp:TextBox>
                                    </td>
                                </tr>

                            </table>
                            <table>
                                 <tr>
                                    <td class="PortofDischarge">
                                    <label>
                                               Port of Discharge
                                    </label>
                                    <td class="Dest">
                                    <label>
                                               Dest:Country
                                    </label>
                                    </td>
                                    <td class="Consignee">
                                    <label>
                                               Consignee
                                    </label>
                                    </td>
                                    <td class="Buyer">
                                    <label>
                                               Buyer
                                    </label>
                                    </td>
                                
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtportofdischarge" runat="server" Style="width: 179px; 
                                            padding-right: 1px;" class="textfield" placeholder="Port of Discharge"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtdestination" runat="server" Style="width: 179px;
                                             padding-right: 1px;" placeholder="Dest:Country"
                                            class="textfield"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtconsignee" runat="server" Style="width: 198px;margin-left: 20px; 
                                            padding-right: 1px;" placeholder="Consignee"
                                            class="textfield"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtbuyer" runat="server" Style="width: 198px; padding-right: 1px;"
                                            class="textfield" placeholder="Buyer" ></asp:TextBox>
                                    </td>
                                
                                
                                
                                </tr>
                 </table>
                            
                            
                            
                        </td>
                    </tr>
                </table>
                
                <div id="tabMemberShip" style="width: 800px; margin-top: 7px; margin-bottom: 10px;">
                    <ul>
                        <li><a href="#Address"><span>Address</span></a></li>
                        <li><a href="#Note"><span>Note</span></a></li>
                    </ul>
                    <div id="Address" style="">
                        <div class="tabfieldsbottom" align="left" style="margin-top: -12px;">
                            <div class="Resiinfo" style="width: 371px; float: left; margin-left: 5px;">
                                <div style="width: 700px;">
                                    <asp:UpdatePanel ID="updateAddress" runat="server">
                                        <ContentTemplate>
                                            <table id="tblAdd" runat="server" visible="true">
                                                <tr>
                                                    <td colspan="2" class="bill">
                                                        Bill Address <a href="#" style="color: #00e;">map</a>
                                                    </td>
                                                    <td class="ship" style="width: 194px;">
                                                        <label style="float: left; width: 194px;">
                                                            Shipping Address <a href="#" style="color: #00e;">map</a></label>
                                                    </td>
                                                    <td style="width: 194px;">
                                                        <asp:CheckBox ID="chkShipAddress" AutoPostBack="true" Text="Same as billing address"
                                                            CssClass="chkship" runat="server" OnCheckedChanged="chkShipAddress_CheckedChanged" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="txtStreet" runat="server" Style="width: 365px; padding-right: 1px;"
                                                            class="textfield" placeholder="Street"></asp:TextBox>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="txtShipStreet" runat="server" Style="width: 395px; margin-left: 20px;
                                                            padding-right: 1px;" class="textfield" placeholder="Street"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtCity" runat="server" Style="width: 179px; padding-right: 1px;"
                                                            class="textfield" placeholder="City"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtState" runat="server" Style="width: 179px; margin-left: 0px;
                                                            padding-right: 1px;" class="textfield" placeholder="State"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 180px;">
                                                        <asp:TextBox ID="txtShipCity" runat="server" Style="width: 194px; margin-left: 20px;
                                                            padding-right: 1px;" class="textfield" placeholder="City"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtShipState" runat="server" Style="width: 194px; margin-left: 0px;
                                                            padding-right: 1px;" class="textfield" placeholder="State"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtZip" runat="server" Style="width: 179px; padding-right: 1px;"
                                                            class="textfield"  onkeypress="return Verify(event);" placeholder="Zip Code"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCountry" runat="server" Style="width: 179px; margin-left: 0px;
                                                            padding-right: 1px;" class="textfield" placeholder="Country"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtShipZip" runat="server" Style="width: 194px; margin-left: 20px;
                                                            padding-right: 1px;" class="textfield"  onkeypress="return Verify(event);"
                                                             placeholder="Zip Code"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtShipCountry" runat="server" Style="width: 194px; margin-left: 0px;
                                                            padding-right: 1px;" class="textfield" placeholder="Country"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="clear: both">
                    </div>
                    <div id="Note" style="border-radius: 3px;">
                        <div class="tabfieldsbottom" align="left" style="margin-top: -12px;">
                            <asp:UpdatePanel ID="UpdPnlAdditionInf" runat="server">
                                <ContentTemplate>
                                    <div class="Resiinfo" style="width: 524px; height: 82px; float: left; margin-left: 5px;">
                                        <div class="bottomleftheading" style="font-size: 18px;">
                                            <table id="tblNote" runat="server">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtNote" TextMode="MultiLine" runat="server" Style="width: 785px;
                                                            max-width: 785px; min-width: 785px; max-height: 65px; min-height: 65px; height: 65px;
                                                            padding-right: 1px;" class="textfield" placeholder="Note"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div>
                                        </div>
                                    </div>
                                    <div style="clear: both">
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtFacebook" runat="server" Style="width: 181px; margin-left: 0px;
                                            padding-right: 1px;" class="textfield" placeholder="Facebook"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMessanger" runat="server" Style="width: 181px; margin-left: 0px;
                                            padding-right: 1px;" class="textfield" placeholder="(Messanger Type) ID"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSkype" runat="server" Style="width: 194px; margin-left: 20px;
                                            padding-right: 1px;" class="textfield" placeholder="Skype"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGooglePlus" runat="server" Style="width: 194px; margin-left: 0px;
                                            padding-right: 1px;" class="textfield" placeholder="Google+"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td class="terms" style="width: 186px;">
                                        <span>Terms</span>
                                        <asp:DropDownList ID="txtTerms" runat="server" Style="width: 181px; padding-right: 1px;"
                                            class="textfield">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                            <asp:ListItem Value="No">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="openBlnc" style="width: 186px;">
                                        <span>Opening balance</span>
                                        <asp:TextBox ID="txtOpeningBlnc" Text="0.00" runat="server" Style="width: 179px; padding-right: 1px;"
                                            class="textfield decimalOnly" placeholder="Balance"></asp:TextBox>
                                    </td>
                                    <td class="AsofDate" style="width: 186px;">
                                        <span style="margin-left: 20px;">As of</span>
                                        <asp:TextBox ID="txtASDate" CssClass="DateTimePicker" runat="server" Style="width: 194px;
                                            margin-left: 20px; padding-right: 1px;" class="textfield" placeholder="Date"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="float: right; width: 200px;">
                                <tr>
                                    <td rowspan="2">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" Style="width: 80px; height: 25px;
                                            margin: 5px 0 0 20px;" OnClientClick="return validate('group');" OnClick="btnSave_Click"
                                            class="buttonImp" />
                                    </td>
                                    <td rowspan="2">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Style="width: 80px; height: 25px;
                                            margin: 5px 0 0 08px;" OnClick="btnCancel_Click" class="buttonImp" />
                                    </td>
                                    <td rowspan="2">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
