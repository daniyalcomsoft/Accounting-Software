<%@ Page Title="Receivable Summary Report" Language="C#" MasterPageFile="Main.master" AutoEventWireup="true" CodeFile="ReceivableSummaryReport.aspx.cs" Inherits="ReceivableSummaryReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript">
        $(document).ready(function() {
            CreateModalPopUp('#ControlConfirmation', 320, 120, 'Print Options');
            CreateModalPopUp('#Confirmation', 280, 120, 'ALERT');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="Update_area">
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
            <ContentTemplate>
                <div class="Heading">
                    Receivable Aging Report</div>
                    <div id="StausMsg">
                     </div>
                <div>
                    <table>
                        <tr>
                             <td style="width:110px">
                                
                            </td>
                            <td>
                                
                            </td>
                            <td>
                               
                            </td>
                            <td>
                               
                            </td>
                            <td>
                              
                            </td>
                            <td>
                                
                            </td>
                            <td>
                             
                            </td>
                            <td>
                              
                            </td>
                            <td>
                               <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                <ContentTemplate>
                                <asp:LinkButton ID="btnPrintJava" CssClass="buttonImp" Style="float: none;" runat="server"
                                    OnClick="btnPrintJava_Click">Print</asp:LinkButton>
                                 </ContentTemplate>
                          </asp:UpdatePanel> </td>
                        </tr>
                    </table>
                     <div style="margin: 0 auto; width: 775px; margin-top: 6px;" id="Reports">        
                          <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
                               DisplayGroupTree="False" OnNavigate="CrystalReportViewer1_Navigate" EnableDatabaseLogonPrompt="False"
                               HasZoomFactorList="False"  OnInit="CrystalReportViewer1_Init" />
                                
                            </div>
                        </ContentTemplate>
                  <%--  </asp:UpdatePanel>--%>
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

