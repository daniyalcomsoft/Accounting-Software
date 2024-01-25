<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" 
CodeFile="GLReport_BalanceSheet.aspx.cs" Inherits="GLReport_BalanceSheet" Title="Balance Sheet Report" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
 
    <script language="javascript">

        $(document).ready(function() {
            CreateModalPopUp('#ControlConfirmation', 320, 120, 'Print Options');
            CreateModalPopUp('#Confirmation', 280, 120, 'ALERT');
            CreateModalPopUp('#farhan', 800 , 600, 'Report');
        });
        function Verify(evet) {
            var charCode = (evet.which) ? evet.which : event.keyCode
            if (charCode != 9) {
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
            }
            return true;
        }
        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }

        function printSelection(node) {

            var content = $("[id $= CrystalReportViewer1]").html();  //;node.innerHTML
            var pwin = window.open('', 'print_content', 'width=100,height=100');

            pwin.document.open();
            pwin.document.write('<html><body onload="window.print()">' + content + '</body></html>');
            pwin.document.close();
            setTimeout(function() { pwin.close(); }, 1000);

        }
       
</script>   
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
.buttonImp a {
width: 90px;
height: 19px;
display: block;
float: left;
text-align: center;
padding-top: 5px;
color: #525252;
font-family: Tahoma, Geneva, sans-serif;
}
</style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="Update_area">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
<div id="content">
	<div class="Heading">Balance Sheet</div> 
	<div id="StausMsg">
    </div>
           <div> 
             <asp:LinkButton CssClass="buttonImp" ID="lbtnViewReport" style="float:left; margin-left:3px; margin-top:-2px;" 
             OnClick="lbtnViewReport_Click" runat="server" >View Report</asp:LinkButton>
             <br />
             
             <asp:LinkButton ID="btnPrint" CssClass="buttonImp" 
                style="float:left; display:none; margin-left:3px; margin-top:-2px;" 
                runat="server" onclick="btnPrint_Click">Print</asp:LinkButton>
            </div> 
        
             
 <div class="">
    <strong>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
        AutoDataBind="true" DisplayGroupTree="False" EnableDatabaseLogonPrompt="False" 
        EnableParameterPrompt="False" onload="CrystalReportViewer1_Load" 
        onnavigate="CrystalReportViewer1_Navigate" />
             
    </strong>
   
</div>
        
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
