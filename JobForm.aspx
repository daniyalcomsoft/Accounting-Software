<%@ Page Title="Create/Modify Job" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="JobForm.aspx.cs" Inherits="JobForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
 <script src="Script/jquery-1.6.2.min.js" type="text/javascript" charset="utf-8"></script>
 <script src="Script/jquery-ui-1.8.16.custom.min.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
        $(document).ready(function() {
         MyDate();

        $('#NewShippingLine').dialog({
            autoOpen: false,
            draggable: true,
            modal:true,
            title: "New Shipping Line",
            width: 670,
            height: 100,
            open: function(type, data) {
                $(this).parent().appendTo("form");
            }
        });

        $("[id$=ddlShippingLine]").change(function() {
            if ($(this).val() == "-1") {
                showDialog('NewShippingLine');
                return false;
            }
            else {
                return true;
            }
        });
            
   });


        
//        $(function () {
//            $("#txtDeliveryDate").datepicker();
        //        });

        function MyDate() {

            $(".DateTimePicker").datepicker();
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
    <div id="NewShippingLine" style="padding: 0;">
        <asp:UpdatePanel ID="UpShippingLine" runat="server">
            <ContentTemplate>
                <table style="width: 650px; margin: 15px auto;">
                    <tr>
                        <td>
                            <label>
                                Shipping Line
                            </label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtShippingLine" runat="server" Width="400"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSaveShippingLine" runat="server" Text="Save" CssClass="btn_1 btn_spacing float_right"
                                OnClick="btnSaveShippingLine_Click" />
                        </td>
                    </tr>
                </table>
                 
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div class="Update_area">
        <div id="StausMsg">
        </div>
       
        <div class="Heading">
            <table>
                <tr>
                    <td style="width: 328px;"></td>
                    <td style="text-align: center; width: 328px;">
                        <b>Create/Modify Job</b>
                    </td>
                </tr>
            </table>
        </div>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate> 
        <div class="head">
            <div class="bodyarea" style="margin: 0 auto; width: 630px;">
                <table style="float: left; width: 800px; margin-left: 15px;">
                <tr>
                    <td rowspan="6" style="text-align: left">
                        <%-- Success --%>
                        <div id="Notification_Success" style="display: none; width: 98%; margin: auto;">
                            <div class="alert-green">
                                <h4>Success:
                                </h4>
                                <asp:Label ID="lblmsg" runat="server" Style="color: White"></asp:Label>
                            </div>
                        </div>
                        <%--Error Message--%>
                        <div id="Notification_Error" style="display: none; width: 98%; margin: auto;">
                            <div class="alert-red">
                                <h4>Error!</h4>
                                <asp:Label ID="lblNewError" runat="server" Style="color: White">
                                </asp:Label>
                            </div>
                        </div>
                        <%-- End --%>
                    </td>
                </tr>
            </table>
            <div style="clear:both;"></div>
                <table>
                    <tr>
                        <td>Job Number
                        </td>
                        <td>Contact Number
                        </td>
                        <td>Customer
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtJobNumber" runat="server" Style="padding-right: 1px;width:200px;"
                                class="textfield" placeholder="Job Number" require="Enter Job Number" validate="group"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtContactNum" runat="server" Style="padding-right: 1px;width:200px;"
                                class="textfield" placeholder="Contact Number"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList DataSourceID="sqlDSUser" DataValueField="CustomerID" DataTextField="DisplayName"
                                ID="ddlUser" runat="server" Style="height: 19px; width: 203px; float: left;"
                                custom="Select User" customFn="var u = parseInt(this.value); return u > 0;"
                                validate="group" class="textfield jumpmenuheight">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="sqlDSUser" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                SelectCommand="SELECT CustomerID,DisplayName FROM vt_SCGL_Customer" SelectCommandType="Text" runat="server"></asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">Job Description
                        </td>
                        
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:TextBox ID="txtJobDescription" runat="server" Style="padding-right: 1px;width:612px;" require="Enter Job Description" validate="group"
                                class="textfield" placeholder="Job Description"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>Container</td>
                        <td style="display:none;">Container No</td>
                        <td>L/C No.</td>
                        <td>L/C Date</td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtContainer" runat="server" Style="padding-right: 1px;width:200px"
                                class="textfield" placeholder="Container"></asp:TextBox>
                        </td>
                         <td style="display:none;">
                            <asp:TextBox ID="txtContainerNo" runat="server" Style="padding-right: 1px;width:200px;"
                                class="textfield" placeholder="Container No"></asp:TextBox>
                        </td>
                        <td>
                             <asp:TextBox ID="txtLCNo" runat="server" Style="padding-right: 1px;width:200px;"
                                class="textfield" placeholder="L/C No."></asp:TextBox>
                         </td>
                        <td>
                            <asp:TextBox ID="txtContainerDate" runat="server" Style="padding-right: 1px;width:200px;"
                                CssClass="DateTimePicker" placeholder="L/C Date"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>IGM No</td>
                        <td>IGM Date</td>
                        <td>Index No</td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtIGMNo" runat="server" Style="padding-right: 1px;width:200px;"
                                class="textfield" placeholder="IGM No"></asp:TextBox>
                        </td>
                         <td>
                            <asp:TextBox ID="txtIGMDate" runat="server" Style="padding-right: 1px;width:200px;"
                                CssClass="DateTimePicker" placeholder="IGM Date"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtIndexNo" runat="server" Style="padding-right: 1px;width:200px;"
                                class="textfield" placeholder="Index No"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>S.S/Flight No.</td>
                        <td>QTY</td>
                        <td>B.E.Cash No</td>
                        
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtSS" runat="server" Style="padding-right: 1px;width:200px;"
                                class="textfield" placeholder="S.S"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtQTY" runat="server" Style="padding-right: 1px;width:200px;"
                                class="textfield" placeholder="Quantity"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBECashNo" runat="server" Style="padding-right: 1px;width:200px;"
                                class="textfield" placeholder="B.E.Cash No"></asp:TextBox>
                        </td>
                        
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>Machine No.</td>
                        <td>Machine Date</td>
                        <td>Delivery Date</td>
                        
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtMachineNo" runat="server" Style="padding-right: 1px;width:200px;"
                                class="textfield" placeholder="Machine No."></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMachineDate" runat="server" Style="padding-right: 1px;width:200px;"
                                CssClass="DateTimePicker" placeholder="Machine Date"></asp:TextBox>
                        </td>
                        
                        <td>
                            <asp:TextBox ID="txtDeliveryDate" runat="server" Style="padding-right: 1px;width:200px;"
                                CssClass="DateTimePicker" placeholder="Delivery Date"></asp:TextBox>
                        </td>
                        
                        
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                    <td>CNF Value</td>
                    <td>Import Value</td>
                    <td>B/L NO./AWB</td>
                    <td>&nbsp;</td>
                    </tr>
                    <tr>
                    <td>
                            <asp:TextBox ID="txtCNFValue" runat="server" Style="padding-right: 1px;width:200px;"
                                class="decimalOnly" placeholder="CNF Value"></asp:TextBox>
                        </td>
                    <td>
                            <asp:TextBox ID="txtImportValue" runat="server" Style="padding-right: 1px;width:200px;"
                                class="decimalOnly" placeholder="Import Value"></asp:TextBox>
                     </td>
                     <td>
                             <asp:TextBox ID="txtBLNo" runat="server" Style="padding-right: 1px;width:200px;"
                                class="textfield" placeholder="B/L No."></asp:TextBox>
                     </td>
                     <td>&nbsp;</td>
                     
                    </tr>
                    
                    
                    <tr>
                    
                    <td>Shipping Line</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    </tr>
                    <tr>
                     
                    <td>
                            <asp:DropDownList ID="ddlShippingLine" runat="server" Width="203px" validate="group" custom="Select Shipping Line" customFn="var IDescID = parseInt(this.value); return IDescID > 0;">
                                            </asp:DropDownList>
                    </td>
                     <td>
                         &nbsp;
                     </td>
                     <td>&nbsp;</td>
                     <td>&nbsp;</td>
                     
                    </tr>
                   
                    
                    <tr>
                        <td class="company">
                            <asp:CheckBox ID="chkComplete" runat="server" Text="Complete" style="margin-top:10px;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="float: right; width: 200px;">
                                <tr>
                                    <td rowspan="2">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" Style="width: 80px; height: 25px; margin: 5px 0 0 20px;"
                                            OnClientClick="return validate('group');" CssClass="buttonImp" OnClick="btnSave_Click" />
                                    </td>
                                    <td rowspan="2">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Style="width: 80px; height: 25px; margin: 5px 0 0 08px;"
                                            CssClass="buttonImp" PostBackUrl="~/JobForm_Views.aspx" />
                                    </td>
                                    <td rowspan="2"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                
            </div>
            
        </div>
        </ContentTemplate>
                </asp:UpdatePanel>
    </div>
</asp:Content>
