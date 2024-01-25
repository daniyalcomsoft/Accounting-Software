<%@ Page Title="General Voucher" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="GLGeneralVoucher.aspx.cs" Inherits="GLGeneralVoucher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            CreateModalPopUp("#PrintReport", 820, 630, "Print Report");
            CreateModalPopUp('#Confirmation', 280, 120, 'ALERT');
            ChangeDateEvent();
            MyDate();
            Load_AutoComplete_Code();
            Load_AutoComplete_JobNumber();
            $('#FindAccount,#FindJobs').dialog({
                autoOpen: false,
                draggable: true,
                title: "Find",
                width: 726,
                height: 449,
                open: function (type, data) {
                    $(this).parent().appendTo("form");
                }
            });
        });
        function Load_AutoComplete_Code() {
            $("[id$=txtCode]").autocomplete({
                source: function (request, response) {
                    $("[id $= txtTitle]").val('');
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: 'Services/GetData.asmx/GetAccountCodeTitle',
                        data: "{ 'Match': '" + request.term + "'}",
                        dataType: "json",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.CodeTitle,
                                    value: item.AccCode,
                                    Title: item.Title
                                }
                            }))
                        }
                    });
                },
                minLength: 3,
                select: function (event, ui) {
                    $("[id$=txtTitle]").val(ui.item.Title);
                    $("[id$=HidTitle]").val(ui.item.Title);
                    $("[id$=lblSubCode]").val(ui.item.accSub);
                }
            });
        }

        function Load_AutoComplete_JobNumber() {
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

        function Check(trn, event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode != 9) {
                if ($('.Credit').val() != '' && trn == 'd') {
                    return false;
                }
                if ($('.Debit').val() != '' && trn == 'c') {
                    return false;
                }
                var charCode = (event.which) ? event.which : event.keyCode
                if (charCode != 9) {
                    if (charCode > 31 && (charCode < 48 || charCode > 57))
                        if (charCode != 46) {
                            event.preventDefault();
                        }
                }
                return true;
            }
            if (($('.Debit').val() != "") || ($('.Credit').val() != "")) {
                $('.Debit').removeAttr('style');
                $('.Credit').removeAttr('style');
            }
            return true;
        }
        function ValidateDebitCredit() {

            if (($('.Debit').val() == "") && ($('.Credit').val() == "")) {
                $('.Debit').css('border-color', 'red');
                $('.Credit').css('border-color', 'red');
                return false;
            }
            else {
                $('.Debit').removeAttr('border-color');
                $('.Credit').removeAttr('border-color');
            }
            return validate('checkcode');
        }
        function OpenUserPermission() {
            showDialog('FindAccount');
            return false;
        }
        function SelectRow(row) {
            var _selectColor = "#303030";
            var _normalColor = "#909090";
            var _selectFontSize = "3em";
            var _normalFontSize = "2em";
            // get all data rows - siblings to current
            var _rows = row.parentNode.childNodes;
            // deselect all data rows
            try {
                for (i = 0; i < _rows.length; i++) {
                    var _firstCell = _rows[i].getElementsByTagName("td")[0];
                    _firstCell.style.color = _normalColor;
                    _firstCell.style.fontSize = _normalFontSize;
                    _firstCell.style.fontWeight = "normal";
                }
            }
            catch (e) { }
            // select current row (formatting applied to first cell)
            var _selectedRowFirstCell = row.getElementsByTagName("td")[0];
            _selectedRowFirstCell.style.color = _selectColor;
            _selectedRowFirstCell.style.fontSize = _selectFontSize;
            _selectedRowFirstCell.style.fontWeight = "bold";
        }
        function NullVal() {
            if ($("[id $= _txtCode]").val().length < 3) {
                $("[id $= _txtCode]").val("");
            }
        }

        function CheckNullVal(elem) {
            if ($(elem).val().length < 2) {
                $("[id$=hdnJobNumber]").val("");
            }
        }


        function MyDate() {
            dateMin = $("[id $= hdnMinDate]").val();
            dateMax = $("[id $= hdnMaxDate]").val();
            $(".DateTimePicker").datepicker({ minDate: new Date(dateMin), maxDate: new Date(dateMax) });
        }




        function ChangeDateEvent() {

            $("[id $= txtDate]").change(function () {

                dateMin = $("[id $= hdnMinDate]").val();
                dateMax = $("[id $= hdnMaxDate]").val();
                var invoiceDate = $("[id$=txtDate]").val();


                if (Date.parse(invoiceDate) < Date.parse(dateMin) || Date.parse(invoiceDate) > Date.parse(dateMax)) {
                    $("[id$=txtDate]").val('');
                }


            });
        }






    </script>

    <style type="text/css">
        .DivStyle {
            display: block;
            height: 22px;
        }

            .DivStyle > span > label {
                white-space: nowrap;
                width: 160px;
            }

        .toggler {
            width: 500px;
            height: 200px;
        }

        #effectInterest, #effectDetail {
            margin-bottom: 9px;
            padding: 0.4em;
            position: relative;
            width: 870px;
        }

            #effectInterest h3, #effectDetail h3 {
                margin: 0;
                padding: 0.4em;
                text-align: center;
            }

        .messages {
            border: solid 1px black;
            margin: 10px 0px;
            background-color: #FFCA2A;
            font-size: 12px;
            font-weight: bold;
            padding: 5px 0px;
        }

            .messages p {
                color: rgb(36,60,130);
                margin: 7px;
            }

            .messages a {
                color: rgb(36,60,130);
                font-weight: bold;
                text-decoration: underline;
            }

        td[align='right'] > span {
            float: none !important;
        }

        td[align='center'] > span {
            float: none !important;
        }

        td[align='left'] > span {
            float: none !important;
        }

        .textarea > .alertbox div {
            margin-top: -20px;
        }

        .alertbox div {
            margin-top: 0px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnMinDate" runat="server" />
    <asp:HiddenField ID="hdnMaxDate" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="Update_area">
                <div class="Heading">
                    <table>
                        <tr>
                            <td style="width: 328px;"></td>
                            <td style="text-align: center; width: 328px;">General Voucher</td>
                            <td style="text-align: right; width: 328px;">
                                <asp:LinkButton ID="lnkNew" CssClass="buttonImp" runat="server"
                                    OnClick="lnkNew_Click">New General Voucher</asp:LinkButton>
                            </td>
                        </tr>
                    </table>

                </div>

                <div id="StausMsg">
                </div>

                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <table class="text_input_main_container">
                            <tr>
                                <td class="text_container">
                                    <label>Number:</label></td>
                                <td class="input_text_container">
                                    <asp:TextBox ID="txtVoucherNumber" runat="server" Width="130px" ReadOnly="True"
                                        AutoPostBack="True" placeholder="Number"></asp:TextBox></td>
                                <td>Job Number</td>
                                <td>
                                    <asp:TextBox ID="txtJobNumber" Width="300" runat="server" ></asp:TextBox>
                                    <%--onblur='CheckNullVal(this);' require="Enter JobNumber" validate="savevoucher"--%>
                                    <div class="SearchDiv">
                                        <asp:LinkButton ID="btnFind" runat="server" CssClass="search" CausesValidation="False" OnClientClick="return showDialog('FindJobs');" />
                                    </div>
                                    <asp:HiddenField ID="hdnJobNumber" runat="server" />
                                </td>
                            </tr>
                            <tr style="display:none;">
                                <td class="text_container">
                                    <label>Reference Number:</label></td>
                                <td class="input_text_container">
                                    <asp:TextBox ID="txtRefNumber" Width="130px" 
                                        runat="server" MaxLength="50" placeholder="Reference Number">
                                    </asp:TextBox><asp:Label ID="lblRefNo" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="text_container">
                                    <label>Date:</label></td>
                                <td class="input_text_container">
                                    <asp:TextBox ID="txtDate" CssClass="DateTimePicker" Width="130px" runat="server"
                                        require="Enter Voucher Date" validate="savevoucher" AutoComplete="Off"
                                        placeholder="Date"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="text_container">
                                    <label>Narration:</label></td>
                                <td class="input_text_container textarea">
                                    <asp:TextBox ID="txtNarration" runat="server"
                                        TextMode="MultiLine" Height="34px" Width="176px" placeholder="Narration"></asp:TextBox>
                                </td>
                            </tr>

                        </table>

                        <div style="float: right; margin-top: 6px;">
                            <asp:LinkButton ID="LinkButtonBack" CssClass="buttonImp" runat="server"
                                Text="Back To List" OnClick="LinkButtonBack_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:GridView ID="GridTrans" runat="server" AutoGenerateColumns="False" CssClass="data main"
                                        DataKeyNames="TransactionID" ShowFooter="True" OnRowDataBound="GridTrans_RowDataBound"
                                        EnableTheming="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text='<%# Eval("Sno") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code" ItemStyle-Width="160px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCode" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:HiddenField ID="HidtxtCode" runat="server" />
                                                    <asp:TextBox ID="txtCode" Width="75px" runat="server" require="Enter Code" validate="checkcode" onblur='NullVal()'
                                                        ValidationGroup="VGTrans"></asp:TextBox>
                                                    <div class="SearchDiv">
                                                        <asp:LinkButton ID="btnFind" runat="server" CssClass="search" CausesValidation="False" OnClientClick="return showDialog('FindAccount');" />
                                                    </div>
                                                    <asp:RequiredFieldValidator ID="Required1" runat="server" ErrorMessage01="*"
                                                        ControlToValidate="txtCode" ValidationGroup="VGTrans"></asp:RequiredFieldValidator>
                                                </FooterTemplate>
                                                <FooterStyle Width="121px" />
                                                <ItemStyle Width="160px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Title">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTitle" runat="server" onkeypress="return Check('c',event);" Text='<%# Eval("Title") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtTitle" runat="server" require="Title not Select on Given Code" validate="checkcode" ReadOnly="true"></asp:TextBox>
                                                    <asp:HiddenField ID="HidTitle" runat="server" />

                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Debit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDebit" Style="float: none;" runat="server" Text='<%# Eval("Debit","{0:n}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtDebit" runat="server" CssClass="Debit" onkeypress="return Check('d',event);"
                                                        AutoComplete="Off" CausesValidation="False" AutoCompleteType="None"></asp:TextBox>
                                                    <label id="lblDebit" style="color: Red;" runat="server" visible="false">*</label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Credit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCredit" Style="float: none;" runat="server" Text='<%# Eval("Credit","{0:n}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtCredit" runat="server" CssClass="Credit" onkeypress="return Check('c',event);"
                                                        AutoComplete="Off"></asp:TextBox>
                                                    <label id="lblCrdt" style="color: Red;" runat="server" visible="false">*</label>
                                                </FooterTemplate>
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
                                                    <asp:DropDownList ID="cmbCostCenter" runat="server" Width="116px" DataTextField="CostCenterName" DataValueField="CostCenterID">
                                                        <%--custom="Coster Center Name" validate="checkcode" customFn="var age = parseInt(this.value); return age > 0;"--%>
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" CssClass="edit" CommandArgument='<%# Eval("Sno") %>'
                                                        CausesValidation="False" OnCommand="btnEdit_Command" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="btnSave" Style="" CssClass="buttonTnew" runat="server" Text="Add" CommandName="Add"
                                                        OnClick="btnSave_Click" OnClientClick="return validate('checkcode');"  />
                                                       <%-- ValidationGroup="VGTrans"--%>
                                                    <asp:Label ID="lblSno2" runat="server" Text='<%# Eval("Sno") %>' Visible="False"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LbtnRemoveGridRow" CssClass="delete" runat="server" CausesValidation="False"
                                                        CommandArgument='<%# ((GridViewRow)Container).RowIndex%>' CommandName="Del" OnCommand="LbtnRemoveGridRow_Command"
                                                        Style="width: 18px" Text=""></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="buttonNew"
                                                        Visible="false" Text="Cancel" OnClick="btnCancel_Click1"></asp:LinkButton>
                                                </FooterTemplate>
                                                <ControlStyle Width="50px" />
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <div style="float: left; margin-top: 6px;">
                            <asp:LinkButton ID="btnSaveVoucher" CssClass="buttonImp" runat="server" OnClientClick="return validate('savevoucher');"
                                Text="Save Voucher" OnClick="btnSaveVoucher_Click" />
                        </div>
                        <div style="float: left; margin-top: 6px; display: none;">
                            <asp:LinkButton ID="btnPrint" runat="server" CssClass="buttonImp"
                                Visible="False" OnClick="btnPrint_Click">Print View</asp:LinkButton>
                        </div>
                        <div style="white-space: nowrap; margin-top: 10px; color: green; padding-left: 290px">
                            <asp:Label ID="lbltotal" runat="server" Text="Total" Font-Bold="True" ForeColor="#2C8CB4"></asp:Label>
                        </div>
                        <div style="white-space: nowrap; color: green; padding-left: 420px">
                            <asp:Label ID="lbltotaldbt" runat="server" Font-Bold="True" ForeColor="#2C8CB4" Style="text-align: right"></asp:Label>
                        </div>
                        <div style="white-space: nowrap; color: green; padding-left: 550px">
                            <asp:Label ID="lbltotalcrdt" runat="server" Font-Bold="True" ForeColor="#2C8CB4"></asp:Label>
                        </div>
                        <div style="white-space: nowrap; color: green; padding-left: 600px">
                            <asp:Label ID="lblValidation" runat="server" Text="Debit and Credit should be equal"
                                Font-Bold="False" ForeColor="Red" Visible="False"></asp:Label>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="PrintReport">
        <asp:UpdatePanel ID="UpdatePanel20" runat="server">
            <ContentTemplate>
                <asp:Button ID="ButtonPrint" runat="server" Text="Print"
                    OnClick="ButtonPrint_Click" OnClientClick="printSelection(document.getElementById('Reports'));return false" CssClass="buttonNew" />
                <div id="Reports">
                    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" SeparatePages="False"
                        AutoDataBind="true" EnableDrillDown="False" DisplayGroupTree="False"
                        DisplayToolbar="False" OnInit="CrystalReportViewer1_Init" OnNavigate="CrystalReportViewer1_Navigate1" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


    <div id="FindAccount" style="padding: 0;">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <br />
                <br />
                <label style="padding-left: 20px">Enter Account No. </label>
                &nbsp;<asp:TextBox ID="txtAccountNo" runat="server"> </asp:TextBox><asp:Button ID="btnFindAcc" runat="server" Text="Find" CssClass="buttonImp" Style="float: none" OnClick="btnFindAcc_Click" />
                <br />
                <asp:GridView ID="GrdAccounts" runat="server" CssClass="data main"
                    AllowPaging="True" PageSize="15"
                    OnPageIndexChanging="GrdAccounts_PageIndexChanging"
                    AutoGenerateColumns="False" OnRowCommand="GrdAccounts_RowCommand"
                    OnRowDataBound="GrdAccounts_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSelect" runat="server" OnClick="lnkSelect_Click">Select</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
                        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                        <asp:BoundField DataField="CurrentBal" HeaderText="Current Balance" ReadOnly="True"
                            SortExpression="CurrentBal" DataFormatString="{0:n}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                    ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand="vt_SCGL_SPGetSubCodeTitleLikeAll"
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter Name="Match" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div id="FindJobs" style="padding: 0;">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
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
                <asp:LinkButton ID="lbtnNo" CssClass="Button1" runat="server" OnClientClick="return closeDialog('Confirmation');">OK</asp:LinkButton>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>
