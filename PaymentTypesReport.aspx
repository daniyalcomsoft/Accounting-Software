﻿<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="PaymentTypesReport.aspx.cs" Inherits="PaymentTypesReport" Title="Payment Type Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
    .ui-datepicker
    {
        z-index: 1003 !important; /* must be > than popup editor (1002) */
    }
    #CrytalReport span
    {
    	float:right !important;
    }
   td[align='right'] > span
   {
   	    float:right !important;
   }
   td[align='center'] > span
   {
   	    float:none !important;
   }
   td[align='left'] > span
   {
   	    float:none !important;
   }
   .Hidden{display:none;}
</style>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            //ReloadJQ();

            MyDate();
            ChangeDateFrom();
            ChangeDateTo();
            CreateModalPopUp('#ControlConfirmation', 320, 120, 'Print Options');
            CreateModalPopUp('#Confirmation', 280, 120, 'ALERT');
        });
            
        function MyDate() {
            dateMin = $("[id $= hdnMinDate]").val();
            dateMax = $("[id $= hdnMaxDate]").val();
            $(".DateTimePicker").datepicker({ minDate: new Date(dateMin), maxDate: new Date(dateMax) });
        }


        function ChangeDateFrom() {
            $("[id $= txtDateFrom]").change(function() {
                dateMin = $("[id $= hdnMinDate]").val();
                dateMax = $("[id $= hdnMaxDate]").val();
                var invoiceDate = $("[id$=txtDateFrom]").val();
                if (Date.parse(invoiceDate) < Date.parse(dateMin) || Date.parse(invoiceDate) > Date.parse(dateMax)) {
                    $("[id$=txtDateFrom]").val('');
                }
            });
        }

        function ChangeDateTo() {
            $("[id $= txtDateTo]").change(function() {
                dateMin = $("[id $= hdnMinDate]").val();
                dateMax = $("[id $= hdnMaxDate]").val();
                var invoiceDate = $("[id$=txtDateTo]").val();
                if (Date.parse(invoiceDate) < Date.parse(dateMin) || Date.parse(invoiceDate) > Date.parse(dateMax)) {
                    $("[id$=txtDateTo]").val('');
                }
            });
        }
        
    </script>
        </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnMinDate" runat="server" />
       <asp:HiddenField ID="hdnMaxDate" runat="server" />
    <div class="Update_area">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
        <ContentTemplate>
   
    <div class="Heading">Payment Report</div>
    <div id="StausMsg">
    </div>
        <div>
           <table>
                        <tr>
                            <td>
            
           <b><asp:Label ID="lbl_PaymentType" runat="server" Text="Select Payment Type"></asp:Label>            </b>
           </td>
           <td>
           
             
             <asp:DropDownList ID="ddlPaymentType" runat="server" custom="Select Payment Type" customFn="var age = parseInt(this.value); return age > -1;"
                                        validate="Rpt" style="width:128px;"> 
                            <asp:ListItem Text="--Please Select--" Selected="True" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="Receipt" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Advance" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Pay Order(Shipping)" Value="12"></asp:ListItem>
                            <asp:ListItem Text="Deposit(Shipping)" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Refunded(Shipping)" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Detention(Shipping)" Value="5"></asp:ListItem>
                            <asp:ListItem Text="Pay Order(Expense)" Value="13"></asp:ListItem>
                            <asp:ListItem Text="Payment Directly" Value="6"></asp:ListItem>
                            <asp:ListItem Text="Payment to Imprest" Value="7"></asp:ListItem>
                            <asp:ListItem Text="Payment through Imprest" Value="8"></asp:ListItem>
                            <asp:ListItem Text="Payment From Imprest" Value="9"></asp:ListItem>
                            <asp:ListItem Text="Deposit in Bank" Value="10"></asp:ListItem>
                            <asp:ListItem Text="Withdrawal From Bank" Value="11"></asp:ListItem>            
                            
              </asp:DropDownList>
            
           
           </td>
           <td>
                <asp:DropDownList DataSourceID="sqlDSUser" DataValueField="CustomerID" DataTextField="DisplayName"
                    ID="ddlUser" runat="server" Style="height: 19px; width: 203px; float: left;"
                    custom="Select Customer" customFn="var u = parseInt(this.value); return u > -1;"
                    validate="Rpt" class="textfield jumpmenuheight">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sqlDSUser" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand="SELECT CustomerID,DisplayName FROM vt_SCGL_Customer" SelectCommandType="Text" runat="server"></asp:SqlDataSource>
           </td>  
            
           <td>
           <asp:LinkButton CssClass="buttonImp" 
                style="float:left; margin-left:3px; margin-top:-2px;" ID="LinkButtonSearch" 
                runat="server" onclick="LinkButtonSearch_Click1"  OnClientClick="return validate('Rpt');" >Search</asp:LinkButton>
           </td>
           
           <td>
           <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                <ContentTemplate>
            <asp:LinkButton ID="btnPrintJava" CssClass="buttonImp" 
                style="float:left; display:none; margin-left:3px; margin-top:-2px;" 
                runat="server" onclick="btnPrintJava_Click">Print</asp:LinkButton>
                 </ContentTemplate>
                          </asp:UpdatePanel>
                          </td>
                           </tr>
                    </table>
        </div>
    <div id="Reports">
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" DisplayGroupTree="False" 
            EnableDatabaseLogonPrompt="False" oninit="CrystalReportViewer1_Init" 
            onnavigate="CrystalReportViewer1_Navigate"  />
    </div>
    </ContentTemplate>
    <%--</asp:UpdatePanel>--%>
    </div>
    <div id="Confirmation" style="display: none;">
            <asp:UpdatePanel ID="upConfirmation" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblDeleteMsg" runat="server" Text=""></asp:Label>
                    <asp:HiddenField ID="HidDeleteID" runat="server" />
                    <br />
                    <br />
                    <asp:LinkButton ID="lbtnNo" CssClass="Button1" runat="server" OnClientClick="return closeDialog('Confirmation');">OK</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
         <div id="ControlConfirmation" style="display: none;">
          
            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                <ContentTemplate>
                 <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                                <td>No of Copies</td>
                                <td>
                                    <asp:TextBox ID="TextCopies" runat="server" Style="width: 60px;" MaxLength="3" onkeypress="return Verify(event);"></asp:TextBox>
                                </td>
                                <td>All Page</td>
                                <td>
                                    <asp:CheckBox ID="chkAllPage" runat="server" />
                                </td>
                    </tr>
                   <tr>
                                <td>Start Page:</td>
                                <td>
                                    <asp:TextBox ID="TextStartPages" runat="server"  Style="width: 60px;" onkeypress="return Verify(event);"
                                                 MaxLength="3"></asp:TextBox>
                                    <asp:HiddenField ID="HDStartPages" runat="server" />
                                </td>
                                 <td>End Page:</td>
                                 <td>
                                     <asp:TextBox ID="TextEndpages" runat="server"  Style="width: 60px;" onkeypress="return Verify(event);"
                                             MaxLength="3"></asp:TextBox>
                                         <asp:HiddenField ID="HDEndPages" runat="server" />
                                </td>
                 </tr>
                </table>
                   <asp:LinkButton ID="lnkConYes" CssClass="Button1" runat="server" onclick="lnkConYes_Click">OK</asp:LinkButton>
                    <asp:LinkButton ID="lnkConNo" CssClass="Button1" runat="server" OnClientClick="return closeDialog('ControlConfirmation');">Cancel</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div> 
        
    <div id="Confirmation" style="display: none;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <br />
                    <br />
                    <br />
                    <asp:LinkButton ID="LinkButton1" CssClass="Button1" runat="server" OnClientClick="return closeDialog('Confirmation');">OK</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div> 
</asp:Content>
