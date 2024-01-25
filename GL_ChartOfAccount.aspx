<%@ Page Language="C#" MasterPageFile="~/Main.master" EnableViewState="true" AutoEventWireup="true"
    CodeFile="GL_ChartOfAccount.aspx.cs" Inherits="GL_ChartOfAccount" Title="Chart Of Accounts Report" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
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
<script language="javascript" type="text/javascript">
        $(document).ready(function() {
            CreateModalPopUp('#ControlConfirmation', 320, 120, 'Print Options');
            CreateModalPopUp('#Confirmation', 280, 120, 'ALERT');
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="Update_area">
       <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
            <ContentTemplate>
                <div class="Heading">
                    Chart of Account</div>
                <div>
                    <table>
                        <tr>
                            <td>
                                <label>
                                    Select Account Status :</label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_Status" Width="100px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_Status_SelectedIndexChanged">
                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="0">InActive</asp:ListItem>
                                    <asp:ListItem Value="2">All</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtnViewReport" OnClick="btn_Search_Click" runat="server" CssClass="buttonImp"
                                    Style="float: none;margin-top:-3px">Search</asp:LinkButton>
                            </td>
                           
                            <td>
                          <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                <ContentTemplate>
                                <asp:LinkButton ID="btnPrintJava" CssClass="buttonImp" Style="float: none;margin-top:-3px" runat="server"
                                    OnClick="btnPrintJava_Click">Print</asp:LinkButton>
                                      </ContentTemplate>
                          </asp:UpdatePanel>
                            </td>
                            
                        </tr>
                    </table>
                    </div>
                 
                        <%--<asp:UpdatePanel ID="UpdatePanel20" runat="server">--%>
                          
                                <div id="Reports">
                                    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
                                        DisplayGroupTree="False" OnNavigate="CrystalReportViewer1_Navigate" EnableDatabaseLogonPrompt="False"
                                        HasZoomFactorList="False" OnInit="CrystalReportViewer1_Init" />
                                </div>
                        
                        <%--</asp:UpdatePanel>--%>
               
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
