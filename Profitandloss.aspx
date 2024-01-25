<%@ Page Title="Profit & Loss Statement" Language="C#" MasterPageFile="Main.master" AutoEventWireup="true" CodeFile="Profitandloss.aspx.cs" Inherits="Profitandloss" %>


<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
    .ui-datepicker
    {
        z-index: 1003 !important; /* must be > than popup editor (1002) */
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
            MyDate();
            ChangeDate();
            CreateModalPopUp('#Confirmation', 280, 120, 'ALERT');
            CreateModalPopUp('#ControlConfirmation', 320, 120, 'Print Options');
            CreateModalPopUp('#farhan', 800, 600, 'Report');
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
        function MyDate() {
            dateMin = $("[id $= hdnMinDate]").val();
            dateMax = $("[id $= hdnMaxDate]").val();
            $(".DateTimePicker").datepicker({ minDate: new Date(dateMin), maxDate: new Date(dateMax) });
        }
        function ChangeDate() {
            $("[id $= txt_Date]").change(function() {
                dateMin = $("[id $= hdnMinDate]").val();
                dateMax = $("[id $= hdnMaxDate]").val();
                var invoiceDate = $("[id$=txt_Date]").val();
                if (Date.parse(invoiceDate) < Date.parse(dateMin) || Date.parse(invoiceDate) > Date.parse(dateMax)) {
                    $("[id$=txt_Date]").val('');
                }
            });
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnMinDate" runat="server" />
       <asp:HiddenField ID="hdnMaxDate" runat="server" />
    <div class="Update_area">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
        <ContentTemplate>
   
    <div class="Heading">Profit & Loss Statement</div>
    <div id="StausMsg">
    </div>
        <div> 
         <table>
                        <tr>
                            <td>
        <b><asp:Label ID="lbl_From" runat="server" Text="From"></asp:Label>  
         </b></td>        
        <td>
             <asp:TextBox ID="txt_DateFrom" CssClass="DateTimePicker" placeholder="Date" Width="171px" runat="server" MaxLength="40" require="Select Date" validate="Rpt"></asp:TextBox>
         </td>    <b>
         <td>
         <asp:Label ID="lbl_To" runat="server" Text="To"></asp:Label>            </b>
          </td>
             <td>
             <asp:TextBox ID="txt_DateTo" CssClass="DateTimePicker" placeholder="Date" Width="171px" runat="server" MaxLength="40" require="Select Date" validate="Rpt"></asp:TextBox>
             </td>
             <td>
             <b><asp:Label ID="lbl_CostCenter" style="display:none;" runat="server" Text="Cost Center"></asp:Label>            </b>
            </td>
            <td>
             <asp:DropDownList ID="ddlCostCenter" runat="server" AutoPostBack="false"> </asp:DropDownList>                 
             </td>
            <td>
             <asp:LinkButton CssClass="buttonImp" 
                style="float:left; margin-left:3px; margin-top:-2px;" ID="LinkButtonSearch" 
                runat="server" onclick="LinkButtonSearch_Click1"  OnClientClick="return validate('Rpt');" >Search</asp:LinkButton>
             </td>     <td> 
             <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                <ContentTemplate>
       
            <asp:LinkButton ID="btnPrintJava" CssClass="buttonImp" 
                style="float:left; display:none; margin-left:3px; margin-top:-2px;" 
                runat="server" onclick="btnPrintJava_Click">Print</asp:LinkButton>
                 </ContentTemplate>
                          </asp:UpdatePanel>
                          </td>
        </div>
    
     </tr>
                    </table>
                                <div id="Reports">
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" DisplayGroupTree="False"  
            EnableDatabaseLogonPrompt="False" oninit="CrystalReportViewer1_Init" 
            onnavigate="CrystalReportViewer1_Navigate" />
             </div>
                            </ContentTemplate>

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
</asp:Content>
    
