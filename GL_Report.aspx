<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="GL_Report.aspx.cs"
    Inherits="GL_Report" Title="GL Transaction" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script language="javascript" type="text/javascript">
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
            //$(".DateTimePicker").datepicker({ dateFormat: 'dd/mm/yy', minDate: dateMin, maxDate: dateMax });
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnMinDate" runat="server" />
       <asp:HiddenField ID="hdnMaxDate" runat="server" />
    <div class="Update_area">
      <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
            <ContentTemplate>
                <div class="Heading">
                    GL Transaction</div>
                <div id="StausMsg">
                </div>
                <div>
                <table>
                        <tr>
                            <td>
                  
                   
                        <asp:Label ID="lbl_SearchBy" runat="server" Text="Search By:" Width="69px"></asp:Label>
                       </td>
                       <td>
                        <asp:DropDownList ID="cmbVoucherType" runat="server" Height="20px" Width="156px">
                        </asp:DropDownList>
                        </td>
                        <td>
                        <asp:Label ID="lbl_from" runat="server" Text="From: "></asp:Label>
                        </td>
                        <td>
                        <asp:TextBox ID="txtDateFrom" placeholder="From Date" require="Select Date" validate="SearchReport" CssClass="DateTimePicker" Width="171px" runat="server"
                            Style="margin-right: 5px;">
                        </asp:TextBox>
                        </td>
                        <td>
                        <asp:Label ID="Label2" runat="server" Text="To: ">
                        </asp:Label>
                       </td><td> <asp:TextBox ID="txtDateTo" placeholder="To Date" require="Select Date" validate="SearchReport" CssClass="DateTimePicker" Width="171px" runat="server"
                            Style="margin-right: 5px;"></asp:TextBox>
                        </td>
                        <td style="display:none;"><asp:DropDownList ID="DropDownListPosted" runat="server" Height="20px" Width="156px"
                            OnSelectedIndexChanged="DropDownListPosted_SelectedIndexChanged">
                          
                         
                            <asp:ListItem Value="2">All Vouchers</asp:ListItem>
                          
                                </asp:DropDownList>
                         </td>
                         <td>
                        <asp:LinkButton ID="lbtnViewReport"  OnClick="btn_Search_Click" runat="server" CssClass="buttonImp"
                            Style="float: none; margin-top: -3px;" OnClientClick="return validate('SearchReport');">Search</asp:LinkButton>
              </td>
              <td>
               <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                <ContentTemplate>
                        <asp:LinkButton ID="btnPrintJava" CssClass="buttonImp" Style="float: none; margin-top: -3px;"
                            Visible="false" runat="server" OnClick="btnPrintJava_Click">Print</asp:LinkButton>
                      </ContentTemplate>
                          </asp:UpdatePanel>
                            </td>
                            </tr>
                    </table>
                    </div>
                        <div id="Reports">
                                    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
                                        EnableDrillDown="False"  DisplayGroupTree="False"  OnNavigate="CrystalReportViewer1_Navigate1"
                                        OnInit="CrystalReportViewer1_Init" />
                                       </div>
                                </ContentTemplate>
                                
                           <%-- </asp:UpdatePanel>--%>
                        </div>
                   
    <%-- </asp:UpdatePanel>--%>
    </div>
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
