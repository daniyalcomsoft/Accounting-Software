<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="ExpenseSheetReport.aspx.cs" Inherits="ExpenseSheetReport" 
Title="Expense Sheet Report" EnableEventValidation="false"%>

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
            ReloadJQ();

            MyDate();
            ChangeDateFrom();
            ChangeDateTo();
            CreateModalPopUp('#ControlConfirmation', 320, 120, 'Print Options');
            CreateModalPopUp('#Confirmation', 280, 120, 'ALERT');

            $('#FindAccount').dialog({
                autoOpen: false,
                draggable: true,
                title: "Find",
                width: 972,
                height: 450,
                open: function(type, data) {
                    $(this).parent().appendTo("form");
                }
            });
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
        function ReloadJQ() {
            $('.autoCompleteCodes').autocomplete({
                source: function(request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: 'Services/GetData.asmx/GetAccountCodeTitle',
                        data: "{ 'Match': '" + request.term + "'}",
                        dataType: "json",
                        success: function(data) {
                            response($.map(data.d, function(item) {
                                return {
                                    label: item.CodeTitle,
                                    value: item.AccCode
                                }
                            }))
                        }
                    });
                },
                minLength: 3,
                select: function(event, ui) {
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
   
    <div class="Heading">Expense Sheet</div>
    <div id="StausMsg">
    </div>
        <div>
           <table>
                        <tr>
                            <td>
            
           <b><asp:Label ID="lbl_Date" runat="server" Text="Job Number"></asp:Label>            </b>
           </td>
           <td>
           <asp:UpdatePanel ID="MainUpdatePanel" runat="server">
            <ContentTemplate>
             
             <asp:TextBox ID="txtJobNo" placeholder="Job No." Width="171px" runat="server" MaxLength="40" require="Enter Job No." validate="Rpt"
             ontextchanged="txtJobNo_TextChanged" AutoPostBack="true"></asp:TextBox>                  
            
            <asp:LinkButton ID="lnkbtnfind2" runat="server" CausesValidation="False" CssClass="search"
             OnClick="lnkbtnfind2_Click" style="margin-left: 5px;"/>
            </ContentTemplate>
             </asp:UpdatePanel>
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
                   <asp:LinkButton ID="lnkConYes" CssClass="Button1" runat="server" 
                        onclick="lnkConYes_Click">OK</asp:LinkButton>
                    <asp:LinkButton ID="lnkConNo" CssClass="Button1" runat="server" OnClientClick="return closeDialog('ControlConfirmation');">Cancel</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div> 
        <div id="FindAccount" style="padding: 0;">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <br />
                <br />
                <label style="padding-left: 20px">Enter Job No. </label>
                &nbsp;<asp:TextBox CssClass="autoCompleteCodes" ID="txtJobNoSearch" runat="server"></asp:TextBox><asp:Button ID="btnFindAcc"
                    runat="server" Text="Find" CssClass="buttonImp" Style="float: none" OnClick="btnFindAcc_Click" />
                <br />
                <asp:GridView ID="GrdAccounts" runat="server" CssClass="data main" AllowPaging="true"
                    PageSize="15" OnPageIndexChanging="GrdAccounts_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSelect" runat="server" OnClick="lnkSelect_Click">Select</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:HiddenField ID="HdnFindCode" runat="server" />
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
