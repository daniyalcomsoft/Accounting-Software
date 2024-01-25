<%@ Page Title="Opening Balance" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="GLOpeningBalance.aspx.cs" Inherits="GLOpeningBalance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--
    <script src="Script/jquery-1.6.2.min.js" type="text/javascript" charset="utf-8"></script>

    <script src="Script/jquery-ui-1.8.16.custom.min.js" type="text/javascript" charset="utf-8"></script>--%>
    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            
            CreateModalPopUp('#Confirmation', 280, 120, 'ALERT');

            $('#FindAccount1').dialog({
                autoOpen: false,
                draggable: true,
                title: "Find",
                width: 726,
                height: 449,
                open: function(type, data) {
                    $(this).parent().appendTo("form");
                }
            });
            Load_AutoComplete_Code();
        });
    function OpenUserPermission() {
        showDialog('FindAccount1');
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
    
    function Load_AutoComplete_Code() {
        $("[id$=txtSearchAccount]").autocomplete({
            source: function(request, response) {
                $("[id $= txtTitle]").val('');
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
                                Title: item.Title
                            }
                        }))
                    }
                });
            },
            minLength: 3,
            select: function(event, ui) {
            $("[id$=txtSearchAccount]").val(ui.item.AccCode);
            $("[id$=HidSearchAccount]").val(ui.item.AccCode);
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
                                Title: item.Title
                                    }
                                                                  
                            }))
                        }
                    });
                },
               

            });
    }
//    function NullVal() {
//        if ($("[id $= _txtSearchAccount]").val().length < 3) {
//            $("[id $= _txtSearchAccount]").val("");
//        }
//    }
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
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="HidSearchAccount" runat="server" />
    <div class="Update_area">
    <div class="Heading">
        Opening Balance
    </div>
    <div id="StausMsg">
                    </div>
     
    <div style="width: 1000px; margin-top: 10px;" align="center" >
        <div style="width: 700px;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                   
                    <div style="">
                    <label style="margin-left: 25px;margin-right:5px;">Search Account:</label>
                    
                    <asp:Label ID="lblTransID" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtSearchAccount" runat="server" AutoComplete="Off" CssClass="autoCompleteCodes" 
                        Width="183px" require="Enter Account No." validate="FindAccount"></asp:TextBox></div>
                    <div style="margin-left: 6px;">
                    <asp:LinkButton ID="btnFind" runat="server" class="search" CausesValidation="False" 
                    OnClientClick="return showDialog('FindAccount1');" style="margin-left:10px;" ></asp:LinkButton>
                        &nbsp;&nbsp;
                        <asp:LinkButton ID="lbtnFind" runat="server" CssClass="buttonNew" OnClick="lbtnFind_Click"
                            OnClientClick="return validate('FindAccount');">Filter</asp:LinkButton>
                        <asp:LinkButton ID="btnClearFilter" CssClass="buttonNew" runat="server" 
                            onclick="btnClearFilter_Click">Clear Filter</asp:LinkButton>
                    </div>
                    <div style=" margin-top: 15px;">
                        <asp:GridView ID="GridOpeningBalance" CssClass="data main" Style="border: 0; border: 2px solid #029FE2;float:left;"
                            runat="server" AutoGenerateColumns="False" ShowFooter="True" PageSize="15">
                            <Columns>
                                <asp:BoundField DataField="Code" HeaderText="Account Code" SortExpression="Code"
                                    HeaderStyle-Width="100px" ReadOnly="True">
                                    <HeaderStyle Width="100px"></HeaderStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Title">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTitle" runat="server" HeaderStyle-Width="290px" Text='<%# Eval("Title") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Debit" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="128px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDebit" Text='<%# Eval("Debit","{0:n}") %>' CssClass="RightAlign_Textbox" 
                                            runat="server" AutoComplete="Off" AutoPostBack="True" 
                                            ontextchanged="txtDebit_TextChanged"></asp:TextBox>
                                        <asp:Label ID="lblSubsidaryID" runat="server" Visible="false" Text='<%# Eval("SubsidaryID") %>'></asp:Label>
                                           <asp:Label ID="lblCode" runat="server" Visible="false" Text='<%# Eval("Code") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtTotalDebit" ReadOnly="true" runat="server" Height="16px" CssClass="RightAlign_Textbox"
                                            AutoComplete="Off"></asp:TextBox>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="128px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Credit" HeaderStyle-Width="128px" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCredit" Text='<%# Eval("Credit","{0:n}") %>' CssClass="RightAlign_Textbox"
                                            runat="server" AutoPostBack="True" ontextchanged="txtCredit_TextChanged"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtTotalCredit" ReadOnly="true" CssClass="RightAlign_Textbox" runat="server" ></asp:TextBox>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="128px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    
                    <div>
                        <asp:Label ID="lblError" runat="server" Text="Debit and Credit should be equal" Visible="false"
                            ForeColor="Red"></asp:Label>
                        <asp:LinkButton ID="lbtnUpdate" CssClass="Button1" Style="float: right; margin-bottom: 10px;
                            margin-top: 5px;" runat="server" OnClick="lbtnUpdate_Click" OnClientClick="return ValidateDebitCredit();">Update</asp:LinkButton>
                    </div>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                        SelectCommand="vt_CB_GL_SpOpeningBalance" SelectCommandType="StoredProcedure">
                    </asp:SqlDataSource>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </div>
        <div id="FindAccount1" style="padding: 0;">
          <asp:UpdatePanel ID="UpdatePanel3" runat="server">
             <ContentTemplate>
                 <br /><br /><label style="padding-left:20px">Enter Account No. </label>
                 &nbsp;
                 <asp:TextBox ID="txtAccountNo" runat="server" CssClass="autoCompleteCodes"></asp:TextBox>
                 <asp:Button ID="btnFindAcc" runat="server" Text="Find" CssClass="buttonImp" 
                     style="float:none" onclick="btnFindAcc_Click" />
                     <br />
                 <asp:GridView ID="GrdAccounts" runat="server" CssClass="data main" 
                     AllowPaging="True" PageSize="15" 
                     onpageindexchanging="GrdAccounts_PageIndexChanging" 
                     AutoGenerateColumns="False" onrowcommand="GrdAccounts_RowCommand" 
                     onrowdatabound="GrdAccounts_RowDataBound" >
                     <Columns>
                         <asp:TemplateField HeaderText="Select">
                             <ItemTemplate>
                                 <asp:LinkButton ID="lnkSelect" runat="server" OnClick="lnkSelect_Click">Select</asp:LinkButton>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
                         <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                         <asp:BoundField DataField="CurrentBal" HeaderText="Current Balance" ReadOnly="True" 
                             SortExpression="CurrentBal" DataFormatString="{0:n}" >
                             <ItemStyle HorizontalAlign="Right" />
                         </asp:BoundField>
                     </Columns>
                  </asp:GridView>
                 <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
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
