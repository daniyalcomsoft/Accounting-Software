<%@ Page Title="Aging Summary Report" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AgingSummaryReport.aspx.cs" Inherits="AgingSummaryReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            MyDate();
            ChangeDateReport();
            CreateModalPopUp('#ControlConfirmation', 320, 120, 'Print Options');
            CreateModalPopUp('#Confirmation', 280, 120, 'ALERT');
        });
        
        function MyDate() {
            dateMin = $("[id $= hdnMinDate]").val();
            dateMax = $("[id $= hdnMaxDate]").val();
            $(".datepicker").datepicker({ minDate: new Date(dateMin), maxDate: new Date(dateMax) });
        }


        function ChangeDateReport() {
            $("[id $= txtReportDate]").change(function() {
            dateMin = $("[id $= hdnMinDate]").val();
            dateMax = $("[id $= hdnMaxDate]").val();
            var invoiceDate = $("[id$=txtReportDate]").val();
            if (Date.parse(invoiceDate) < Date.parse(dateMin) || Date.parse(invoiceDate) > Date.parse(dateMax)) {
                $("[id$=txtReportDate]").val('');
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
        .buttonImp1
        {
                float: left;
                height: 23.5px;
                margin-bottom: 2px;
                width: 85px;
                background-color: #029FE2;
                border: 1px solid rgba(0, 0, 0, 0);
                border-radius: 3px 3px 3px 3px;
                color: #FFFFFF;
                display: inline-block;
                float: right;
                font-family: Verdana;
                font-size: 12px;
                font-weight: normal;
                margin: 2px 6px 0 0;
                padding: 0 0 1px 0;
                text-decoration: none !important;
                /*vertical-align: middle;*/
        }
        .buttonImp1:active
        {
	        position: relative;
            top: 1px;
	    }
	
        .buttonImp1:hover
        {
	        background-color: #2C8CB4;
	        color: White;
	    }
	    
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="hdnMinDate" runat="server" />
 <asp:HiddenField ID="hdnMaxDate" runat="server" />
    <div class="Update_area">
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
            <ContentTemplate>
                <div class="Heading">
                    Aging Summary Report</div>
                    <div id="StausMsg">
                     </div>
                <div>
                    <table>
                        <tr>
                            <td>
                                <label style="margin-left:103px;">
                                    Report Date:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtReportDate" placeholder="Date" CssClass="datepicker" Width="150px" runat="server" require="Select Date" validate="Rpt"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" CssClass="buttonImp1"
                                    Style="float: none" onclick="btnSearch_Click" Text="Search" OnClientClick="return validate('Rpt');" />
                            </td>
                            <td>
                                <asp:LinkButton ID="btnPrintJava" CssClass="buttonImp" Style="float: none;" runat="server"
                                    OnClick="btnPrintJava_Click">Print</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                     <div style="margin: 0 auto; width: 775px; margin-top: 6px;" id="Reports"> 
                         <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" 
                          DisplayGroupTree="False" OnNavigate="CrystalReportViewer1_Navigate" EnableDatabaseLogonPrompt="False"
                               HasZoomFactorList="False" onload="CrystalReportViewer1_Load" OnInit="CrystalReportViewer1_Init"/>
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

