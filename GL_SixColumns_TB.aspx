<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="GL_SixColumns_TB.aspx.cs" Inherits="GL_SixColumns_TB" 
Title="Six Column TB" %>
<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
    .ui-datepicker
    {
        z-index: 1003 !important; 
    }
    #CrytalReport span
    {
    	float:none;
    }
</style>
<script language="javascript">

    $(document).ready(function() {
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="Update_area">
    <div class="Heading">Six-Columns Trial Balance</div>
    <div id="StausMsg">
    </div>
    <div>
     <table style="vertical-align:bottom;">
        <tr>
            <td><asp:Label ID="lbl_from" runat="server" Text="From:"></asp:Label></td>
            <td><asp:TextBox ID="txtDateFrom"  require="Selet Date" validate="Rpt" CssClass="DateTimePicker" 
            placeholder="From Date" Width="171px" runat="server"></asp:TextBox></td>
            <td><asp:Label ID="Label2" runat="server" Text="To: "></asp:Label></td>
            <td><asp:TextBox ID="txtDateTo"  require="Selet Date" validate="Rpt" CssClass="DateTimePicker" 
            placeholder="To Date" Width="171px" runat="server"></asp:TextBox></td>
            <td><asp:LinkButton CssClass="buttonImp" style="float:left; margin-left:3px; margin-top:-2px;" ID="lbtnViewReport" OnClick="btn_Search_Click"  OnClientClick="return validate('Rpt');" runat="server">Search</asp:LinkButton></td>
            <td>
                <asp:LinkButton ID="btnPrintJava" CssClass="buttonImp" 
                    style="float:left; margin-left:2px; margin-top:-2px;" Visible="false" 
                    runat="server" onclick="btnPrintJava_Click">Print</asp:LinkButton>
            </td>
        </tr>
     </table>
    </div>
    <div id="CrytalReport" style="float:none;">
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" EnableDrillDown="False" DisplayGroupTree="False" 
            onnavigate="CrystalReportViewer1_Navigate1" 
            oninit="CrystalReportViewer1_Init" ReuseParameterValuesOnRefresh="True"/>
    </div>
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
                   <asp:LinkButton ID="lnkConYes" CssClass="Button1" runat="server" 
                        onclick="lnkConYes_Click">OK</asp:LinkButton>
                    <asp:LinkButton ID="lnkConNo" CssClass="Button1" runat="server" OnClientClick="return closeDialog('ControlConfirmation');">Cancel</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
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
</asp:Content>
