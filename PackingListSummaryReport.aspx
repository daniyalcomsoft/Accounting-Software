<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="PackingListSummaryReport.aspx.cs" Inherits="PackingListSummaryReport" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            MyDate();
            ChangeDateFrom();
            ChangeDateTo();
            CreateModalPopUp('#ControlConfirmation', 320, 120, 'Print Options');
            CreateModalPopUp('#Confirmation', 280, 120, 'ALERT');
            CreateModalPopUp('#Record', 280, 120, 'ALERT');

        });
        function MyDate() {
            dateMin = $("[id $= hdnMinDate]").val();
            dateMax = $("[id $= hdnMaxDate]").val();
            $(".DateTimePicker").datepicker({ dateFormat: 'dd/mm/yy', minDate: dateMin, maxDate: dateMax });
        }


    function ChangeDateFrom() {
        $("[id $= txtFromDate]").change(function() {
            dateMin = $("[id $= hdnMinDate]").val();
            dateMax = $("[id $= hdnMaxDate]").val();
            var invoiceDate = $("[id$=txtFromDate]").val();
            if (Date.parse(invoiceDate) < Date.parse(dateMin) || Date.parse(invoiceDate) > Date.parse(dateMax)) {
                $("[id$=txtFromDate]").val('');
            }
        });
    }

    function ChangeDateTo() {
        $("[id $= txtToDate]").change(function() {
            dateMin = $("[id $= hdnMinDate]").val();
            dateMax = $("[id $= hdnMaxDate]").val();
            var invoiceDate = $("[id$=txtToDate]").val();
            if (Date.parse(invoiceDate) < Date.parse(dateMin) || Date.parse(invoiceDate) > Date.parse(dateMax)) {
                $("[id$=txtToDate]").val('');
            }
        });
    }
    </script>
     <style type="text/css">
        .ui-datepicker
        {
            z-index: 1003 !important;
        }
        td[align='right'] > span
        {
            float: right !important;
        }
        td[align='center'] > span
        {
            float: none !important;
        }
        td[align='left'] > span
        {
            float: none !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:HiddenField ID="hdnMinDate" runat="server" />
 <asp:HiddenField ID="hdnMaxDate" runat="server" />
    <div class="Update_area">
       <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
            <ContentTemplate>
                <div class="Heading">
                    Packing List Report</div>
                    <div id="StausMsg">
                     </div>
                <div>
                    <table>
                        <tr>
                             <td style="width:110px">
                                
                            </td>
                            <td>
                                <label>
                                    <%--Invoice Status:--%></label>
                            </td>
                            <td>
                                <asp:DropDownList style="display:none;" ID="ddl_Status" Width="100px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_Status_SelectedIndexChanged">
                                    <asp:ListItem Value="True">Active</asp:ListItem>
                                    <asp:ListItem Value="False">InActive</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <label>
                                    <%--From:--%>
                                    </label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFromDate" style="display:none;" placeholder="From Date" CssClass="datepicker" Width="150px" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <label>
                                    <%--To:--%>
                                    </label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtToDate" style="display:none;" placeholder="To Date" CssClass="datepicker" Width="150px" runat="server"></asp:TextBox>
                                 <asp:TextBox ID="txtInvoiceID" placeholder="Invoice ID"  Width="150px" runat="server" require="Insert Invoice No." validate="SearchReport"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" CssClass="buttonImp"
                                    Style="float: none" onclick="btnSearch_Click" Text="Search" OnClientClick="return validate('SearchReport');"/>
                            </td>
                            <td>
                              <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                <ContentTemplate>
                                <asp:LinkButton ID="btnPrintJava" CssClass="buttonImp" Style="float: none;" runat="server"
                                    OnClick="btnPrintJava_Click">Print</asp:LinkButton>
                                       </ContentTemplate>
                          </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                     <div style="margin: 0 auto; width: 775px; margin-top: 6px;" id="Reports">        
                          <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
                               DisplayGroupTree="False" OnNavigate="CrystalReportViewer1_Navigate" EnableDatabaseLogonPrompt="False"
                               HasZoomFactorList="False" onload="CrystalReportViewer1_Load" OnInit="CrystalReportViewer1_Init" />
                                
                            </div>
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
                <br />
                <asp:LinkButton ID="lbtnNo" CssClass="Button1" runat="server" OnClientClick="return closeDialog('Confirmation');">OK</asp:LinkButton>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
    <div id="Record" style="display: none;">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Label ID="Label1" runat="server" Text="No Record Found"></asp:Label>
               <br />
               <br />
               <br />
                
                <asp:LinkButton ID="LinkButton1" CssClass="Button1" runat="server" OnClientClick="return closeDialog('Record');">OK</asp:LinkButton>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
    <br />
    <div id="ControlConfirmation" style="display: none;">
        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
            <ContentTemplate>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            No of Copies
                        </td>
                        <td>
                            <asp:TextBox ID="TextCopies" runat="server" Style="width: 60px;" MaxLength="3" onkeypress="return Verify(event);"></asp:TextBox>
                        </td>
                        <td>
                            All Page
                        </td>
                        <td>
                            <asp:CheckBox ID="chkAllPage" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Start Page:
                        </td>
                        <td>
                            <asp:TextBox ID="TextStartPages" runat="server" Style="width: 60px;" onkeypress="return Verify(event);"
                                MaxLength="3"></asp:TextBox>
                            <asp:HiddenField ID="HDStartPages" runat="server" />
                        </td>
                        <td>
                            End Page:
                        </td>
                        <td>
                            <asp:TextBox ID="TextEndpages" runat="server" Style="width: 60px;" onkeypress="return Verify(event);"
                                MaxLength="3"></asp:TextBox>
                            <asp:HiddenField ID="HDEndPages" runat="server" />
                        </td>
                    </tr>
                </table>
                <asp:LinkButton ID="lnkConYes" CssClass="Button1" runat="server" OnClick="lnkConYes_Click">OK</asp:LinkButton>
                <asp:LinkButton ID="lnkConNo" CssClass="Button1" runat="server" OnClientClick="return closeDialog('ControlConfirmation');">Cancel</asp:LinkButton>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

