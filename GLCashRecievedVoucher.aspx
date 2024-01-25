<%@ Page Title="Received Voucher" Language="C#" MasterPageFile="Main.master" AutoEventWireup="true"
    CodeFile="GLCashRecievedVoucher.aspx.cs" Inherits="GLCashRecievedVoucher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="Script/jquery-1.6.2.min.js" type="text/javascript" charset="utf-8"></script>

    <script src="Script/jquery-ui-1.8.16.custom.min.js" type="text/javascript" charset="utf-8"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            CreateModalPopUp("#PrintReport", 820, 630, "Print Report");
            CreateModalPopUp('#Confirmation', 280, 120, 'ALERT');
            //  $(".ui-icon-closethick:eq(0)").click(function() {
            //   $("[id $= txtBalance]").text("0");
            //         });
            //            $("[id $= lbtnNo]").click(function() {
            //               $("[id $= txtBalance]").text("0");
            //                        });
            MyDate();
            ChangeDateEvent();
            Load_AutoComplete_Code();
            Load_AutoComplete_Code2();
     
            $('#FindAccount,#FindAccount2,#FindJobs').dialog({
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
        
       

       
        function Load_AutoComplete_Code() {  
        
            $("[id$=txtCodeGrid]").autocomplete({
                source: function(request, response) {
                $("[id $= txtTitleGrid]").val('');  
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
                                    value: item.AccCode,
                                    Title: item.Title,
                                    accMain: item.AccMain,
                                    accControl: item.AccControl,
                                    accSub: item.AccSubsidary
                                }
                            }))
                        }
                    });
                },
                minLength: 3,
                select: function(event, ui) {
                
                    $("[id$=txtTitleGrid]").val(ui.item.Title);
                    $("[id$=HidTitle]").val(ui.item.Title);
                }
            });
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
                                    value: item.AccCode,
                                    Title: item.Title,
                                    currBal: item.Balance
                                }

                            }))
                        }
                    });
                },
                minLength: 3,
                select: function(event, ui) {
                //if ($(this).attr("id").slice($(this).attr("id").lastIndexOf("_") + 1) != "") 
                if ($(this).attr("id").indexOf("txtAccountNo") < 0)
                {
                        $('#' + $(this).attr('id') + 'lbl').text(ui.item.Title);
                        $("[id$=txtBalance]").text(ui.item.currBal);
                        $("[id$=txtBalanceHidden]").val(ui.item.currBal);
                        $("[id$=titlecode]").val(ui.item.Title);
                    }
                }
            });
        }
        
        function Load_AutoComplete_Code2() {

            $("[id$=txtCode]").autocomplete({
                source: function (request, response) {
                    $("[id $= txtTitleGrid]").val('');
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: 'Services/GetData.asmx/GetAccountCodeTitle2',
                        data: "{ 'Match': '" + request.term + "'}",
                        dataType: "json",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.CodeTitle,
                                    value: item.AccCode,
                                    Title: item.Title,
                                    accMain: item.AccMain,
                                    accControl: item.AccControl,
                                    accSub: item.AccSubsidary
                                }
                            }))
                        }
                    });
                },
                minLength: 3,
                select: function (event, ui) {

                    $("[id$=txtTitleGrid]").val(ui.item.Title);
                    $("[id$=HidTitle]").val(ui.item.Title);
                }
            });
            $('.autoCompleteCodes2').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: 'Services/GetData.asmx/GetAccountCodeTitle2',
                        data: "{ 'Match': '" + request.term + "'}",
                        dataType: "json",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.CodeTitle,
                                    value: item.AccCode,
                                    Title: item.Title,
                                    currBal: item.Balance
                                }

                            }))
                        }
                    });
                },
                minLength: 3,
                select: function (event, ui) {
                    //if ($(this).attr("id").slice($(this).attr("id").lastIndexOf("_") + 1) != "") 
                    if ($(this).attr("id").indexOf("txtAccountNo2") < 0) {
                        $('#' + $(this).attr('id') + 'lbl').text(ui.item.Title);
                        $("[id$=txtBalance]").text(ui.item.currBal);
                        $("[id$=txtBalanceHidden]").val(ui.item.currBal);
                        $("[id$=titlecode]").val(ui.item.Title);
                    }
                }
            });


            $("[id$=txtJobNumber]").autocomplete({
                source: function (request, response) {
                    $("[id $= txtTitle],[id$=hdnJobNumber]").val('');
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: 'Services/GetData.asmx/GetJobByNumber',
                        data: "{ 'Match': '" + request.term + "'}",
                        dataType: "json",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.JobNumber,
                                    value: item.JobNumber,
                                    JobID: item.JobID
                                }
                            }))
                        }
                    });
                },
                minLength: 2,
                select: function (event, ui) {
                    $("[id$=txtJobNumber]").val(ui.item.value);
                    $("[id$=hdnJobNumber]").val(ui.item.JobID);
                }
            });
        }       
       

        function MyDate() {
        dateMin = $("[id $= hdnMinDate]").val();
        dateMax = $("[id $= hdnMaxDate]").val();
        $(".DateTimePicker").datepicker({ minDate: new Date(dateMin), maxDate: new Date(dateMax) });
    }
        
        
        
        
         function ChangeDateEvent() {

            $("[id $= txtDate]").change(function() {

                dateMin = $("[id $= hdnMinDate]").val();
                dateMax = $("[id $= hdnMaxDate]").val();
                var invoiceDate = $("[id$=txtDate]").val();


                if (Date.parse(invoiceDate) < Date.parse(dateMin) || Date.parse(invoiceDate) > Date.parse(dateMax)) {
                    $("[id$=txtDate]").val('');
                }


            });
        }
        function ValidateDebitCredit(){    
        if($('.Credit').val() == "")
        {
	        $('.Credit').css('border-color','red');
	        return false;
        }
        else
        {
            $('.Credit').removeAttr('border-color');
        }
        return validate('codeadd');
        }
        function Verify(event) {
         var charCode = (event.which) ? event.which : event.keyCode
         if (charCode != 9) {
         if (charCode > 31 && (charCode < 48 || charCode > 57))
            if(charCode != 46)
            {
                event.preventDefault();
            } 
        }
        return true;
        }
        function NullVal() {
            if ($("[id$=txtCodeGrid]").val().length < 3) {
                $("[id$=txtCodeGrid]").val("");
            }
        }
        function CheckNullVal(elem) {
            if ($(elem).val().length < 2) {
                $("[id$=hdnJobNumber]").val("");
            }
        }
       
    </script>

    <style type="text/css">
        td[align='right'] > span
        {
            float: none !important;
        }
        td[align='center'] > span
        {
            float: none !important;
        }
        td[align='left'] > span
        {
            float: none !important;
        }
        .textarea > .alertbox div
        {
            margin-top: -20px;
        }
        .alertbox div
        {
            margin-top: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
        <ContentTemplate>
            <div class="Update_area">
                <div class="Heading">
                    <table>
                        <tr>
                            <td style="width: 328px;">
                            </td>
                            <td style="text-align: center; width: 328px;">
                                Cash Received Voucher
                            </td>
                            <td style="text-align: right; width: 328px;">
                                <asp:LinkButton ID="lnkNew" CssClass="buttonImp" runat="server" OnClick="lnkNew_Click">New Cash Receive</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="StausMsg">
                </div>
                <br />
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table class="text_input_main_container">
                            <tr>
                                <td class="text_container">
                                    <label>
                                        Number:</label>
                                </td>
                                <td class="input_text_container">
                                    <asp:TextBox ID="txtVoucherNumber" placeholder="Number"  runat="server" Width="130px" ReadOnly="True" AutoPostBack="True"></asp:TextBox>
                                </td>
                                <td>Job Number</td>
                                <td>
                                    <asp:TextBox ID="txtJobNumber" Width="300" runat="server" ></asp:TextBox>
                                   <%-- require="Enter JobNumber" onblur='CheckNullVal(this);' validate="savevoucher"--%>
                                    <div class="SearchDiv">
                                        <asp:LinkButton ID="btnFind" runat="server" CssClass="search" CausesValidation="False" OnClientClick="return showDialog('FindJobs');" />
                                    </div>
                                    <asp:HiddenField ID="hdnJobNumber" runat="server" />
                                </td>
                            </tr>
                            <tr style="display:none;">
                                <td class="text_container">
                                    <label>
                                        Reference Number:</label>
                                </td>
                                <td class="input_text_container">
                                    <asp:TextBox ID="txtRefNumber" Width="130px" runat="server" MaxLength="50" placeholder="Reference Number" ></asp:TextBox><asp:Label ID="lblRefNo" runat="server" ForeColor="#2C8CB4"></asp:Label> 
                                </td>
                            </tr>
                            <tr>
                                <td class="text_container">
                                    <label>
                                        Date:</label>
                                </td>
                                <td class="input_text_container">
                                    <asp:TextBox ID="txtDate" CssClass="DateTimePicker" Width="130px" runat="server"
                                        require="Enter Voucher Date" validate="savevoucher" placeholder="Date" AutoComplete="Off"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <div class="text_input_main_container">
                            <div>
                            </div>
                            <div class="input_text_container" style="margin-left: -2px; margin-top: -2px; padding-right: 0px">
                                <table>
                                    <tr>
                                        <td class="text_container">
                                            <label>
                                                Cash Account:</label>
                                        </td>
                                        <td class="input_text_container" style="margin-top: -2px; margin-left: 1px;">
                                            <asp:Label ID="lblTransID" runat="server" Text="" Visible="false"></asp:Label>
                                            <asp:TextBox require="Enter Cash Account" CssClass="autoCompleteCodes2" Width="130px"
                                                validate="savevoucher" ID="txtCode" runat="server" 
                                                placeholder="Cash Account" ></asp:TextBox>
                                            <asp:LinkButton ID="lnkbtnfind2" runat="server" CausesValidation="False" CssClass="search"
                                                OnClick="lnkbtnfind2_Click" />
                                        </td>
                                        <td>
                                            <asp:Label ID="txtCodelbl" runat="server" Text="" ForeColor="#2C8CB4"></asp:Label>
                                            <input id="titlecode" runat="server" type="hidden" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblbalance" runat="server" ForeColor="#2C8CB4" Text=""></asp:Label>
                                            <asp:HiddenField ID="txtBalanceHidden" runat="server" />
                                            <asp:Label ID="txtBalance" ForeColor="#2C8CB4" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="text_input_main_container" style="float: left; margin-top: -1px; height: 52px;">
                            <div class="text_container">
                                <label>
                                    Narration:</label>
                            </div>
                            <div class="input_text_container textarea">
                                <asp:TextBox ID="txtNarration" runat="server"
                                    AutoComplete="Off" Height="34px"  placeholder="Narration"  TextMode="MultiLine" Width="179px"></asp:TextBox>
                            </div>
                            <div style="float: right; margin-top: 6px;">
                                <asp:LinkButton ID="LinkButtonBack" CssClass="buttonImp" runat="server" Text="Back To List"
                                    OnClick="LinkButtonBack_Click" />
                            </div>
                        </div>
                        <div style="clear: both;">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GridTrans" CssClass="data main" runat="server" AutoGenerateColumns="False"
                                DataKeyNames="TransactionID" ShowFooter="True" OnRowDataBound="GridTrans_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSno" runat="server" Text='<%# Eval("Sno") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCode" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div>
                                                <asp:TextBox require="Enter Code" validate="codeadd" ID="txtCodeGrid" onblur='NullVal()'
                                                    runat="server" ValidationGroup="VGTrans" Width="90px" Height="16px"></asp:TextBox>
                                                <div class="SearchDiv">
                                                    <asp:LinkButton ID="btnFind" runat="server" CssClass="search" CausesValidation="False"
                                                        OnClick="btnFind_Click" />
                                                </div>
                                            </div>
                                        </FooterTemplate>
                                        <HeaderStyle Width="155px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Title">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtTitleGrid" require="Title not Select on Given Code" validate="codeadd"
                                                runat="server" ReadOnly="true"></asp:TextBox>
                                            <asp:HiddenField ID="HidTitle" runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="128px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCredit" Style="float: none;" runat="server" Text='<%# Eval("Credit","{0:n}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtCredit" runat="server" CssClass="Credit" require="Enter Amount"
                                                validate="codeadd" onkeypress="return Verify(event);" AutoComplete="Off"></asp:TextBox>
                                        </FooterTemplate>
                                        <HeaderStyle Width="128px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtRemarks" runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cost Center" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCosterCenterName" runat="server" Text='<%# Eval("CostCenterName") %>'></asp:Label>
                                            <asp:Label ID="lblCostCenterID" runat="server" Text='<%# Eval("CostCenterID") %>'
                                                Visible="False"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="cmbCostCenter" runat="server" Width="116px" DataTextField="CostCenterName"
                                                DataValueField="CostCenterID" >
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" CssClass="edit" runat="server" CommandArgument='<%# Eval("Sno") %>'
                                                CausesValidation="False" OnCommand="btnEdit_Command" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="btnAdd" Style="float: left;" CssClass="buttonTnew" runat="server"
                                                Text="Add" CommandName="Add" ValidationGroup="VGTrans" OnClick="btnAdd_Click"
                                                OnClientClick="return validate('codeadd');" />
                                            <asp:Label ID="lblSno2" runat="server" Text='<%# Eval("Sno") %>' Visible="False"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LbtnRemoveGridRow" CssClass="delete" runat="server" CausesValidation="False"
                                                CommandArgument='<%# ((GridViewRow)Container).RowIndex%>' CommandName="Del" OnCommand="LbtnRemoveGridRow_Command"
                                                Style="width: 18px"></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton runat="server" Text="Cancel" CssClass="buttonNew" Visible="false"
                                                ID="btnCancel" OnClick="btnCancel_Click"></asp:LinkButton>
                                        </FooterTemplate>
                                        <ControlStyle Width="50px" />
                                        <ItemStyle Width="50px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div style="float: left; margin-top: 6px;">
                                <asp:LinkButton ID="btnSave" OnClientClick="return validate('savevoucher');" CssClass="buttonImp"
                                    runat="server" Text="Save Voucher" OnClick="btnSave_Click" />
                            </div>
                            <div style="float: left; margin-top: 6px;">
                                <asp:LinkButton ID="btnPrint" runat="server" CssClass="buttonImp" OnClick="btnPrint_Click"
                                    Visible="False">Print View</asp:LinkButton>
                            </div>
                            <div style="white-space: nowrap; color: green; margin-top: 10px; padding-left: 350px">
                                <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#2C8CB4" Text="Total"></asp:Label>
                            </div>
                            <div style="white-space: nowrap; color: green; padding-left: 470px; text-align: right;"
                                align="right">
                                <asp:Label ID="lblTotalAmt" runat="server" Font-Bold="True" ForeColor="#2C8CB4"></asp:Label>
                            </div>
                            <div style="padding-left: 500px; white-space: nowrap">
                                <asp:Label ID="lblValidation" runat="server" Font-Bold="True" ForeColor="Red" Visible="false"
                                    Text="Balance Should be greater or equal to Amount"></asp:Label>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hdnMinDate" runat="server" />
    <asp:HiddenField ID="hdnMaxDate" runat="server" />
    <div id="PrintReport">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="buttonNew" OnClick="ButtonPrint_Click"
                    OnClientClick="printSelection(document.getElementById('Reports'));return false" />
                <div id="Reports">
                    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" SeparatePages="False"
                        AutoDataBind="true" EnableDrillDown="False" DisplayGroupTree="False" OnNavigate="CrystalReportViewer1_Navigate1"
                        DisplayToolbar="False" OnInit="CrystalReportViewer1_Init" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="FindAccount" style="padding: 0;">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <br />
                <br />
                <label style="padding-left: 20px">Enter Account No. </label>
                &nbsp;<asp:TextBox CssClass="autoCompleteCodes" ID="txtAccountNo" runat="server"></asp:TextBox><asp:Button ID="btnFindAcc"
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
     <div id="FindAccount2" style="padding: 0;">
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
                <br />
                <br />
                <label style="padding-left: 20px">Enter Account No. </label>
                &nbsp;<asp:TextBox CssClass="autoCompleteCodes2" ID="txtAccountNo2" runat="server"></asp:TextBox><asp:Button ID="btnFindAcc2"
                    runat="server" Text="Find" CssClass="buttonImp" Style="float: none" OnClick="btnFindAcc2_Click" />
                <br />
                <asp:GridView ID="GrdAccounts2" runat="server" CssClass="data main" AllowPaging="true"
                    PageSize="15" OnPageIndexChanging="GrdAccounts2_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSelect2" runat="server" OnClick="lnkSelect2_Click">Select</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:HiddenField ID="HdnFindCode2" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="FindJobs" style="padding: 0;">
        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
                <br />
                <br />
                <label style="padding-left: 20px">Enter Job Number. </label>
                &nbsp;<asp:TextBox ID="txtJobNumberSearch" runat="server"> </asp:TextBox><asp:Button ID="btnFindJob" runat="server" Text="Find" CssClass="buttonImp" Style="float: none" OnClick="btnFindJob_Click"/>
                <br />
                <asp:GridView ID="grdJobs" runat="server" CssClass="data main"                    
                    AutoGenerateColumns="False" DataSourceID="sqlDSJobs" EnableModelValidation="True">
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSelectJob" runat="server" OnClick="lnkSelectJob_Click" JobID='<%#Eval("JobID") %>'>Select</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="JobNumber" HeaderText="Job Number" SortExpression="JobNumber" />
                        <asp:BoundField DataField="JobDescription" HeaderText="Job Description" SortExpression="JobDescription" />
                        <asp:BoundField DataField="DisplayName" HeaderText="Customer Name" SortExpression="DisplayName" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="sqlDSJobs" runat="server"
                    ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand="SELECT J.[JobID],J.[JobNumber],J.[JobDescription],C.[DisplayName]
	                            ,CASE WHEN J.[Completed] = 1 THEN 'TRUE' ELSE 'FALSE' END Completed FROM [vt_SCGL_Job] J
	                            INNER JOIN vt_SCGL_Customer C ON J.[CustomerID] = C.[CustomerID]">                    
                </asp:SqlDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="Confirmation" style="display: none;">
        <asp:UpdatePanel ID="upConfirmation" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblDeleteMsg" runat="server" Text="No Record Found"></asp:Label>
                <asp:HiddenField ID="HidDeleteID" runat="server" />
                <br />
                <br />
                <br />
                <asp:LinkButton ID="lbtnNo" CssClass="Button1" runat="server"  OnClientClick="return closeDialog('Confirmation');">OK</asp:LinkButton>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
