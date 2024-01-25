<%@ Page Title="Payment Voucher" Language="C#" MasterPageFile="Main.master" AutoEventWireup="true" CodeFile="GLCashPaymentVoucher.aspx.cs" Inherits="GLCashPaymentVoucher" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript" language="javascript">

        $(document).ready(function() {
            CreateModalPopUp("#PrintReport", 820, 630, "Print Report");
            CreateModalPopUp('#Confirmation', 280, 120, 'ALERT');
            Get_Current_Bal();
            MyDate();
            ChangeDateEvent();
            Load_AutoComplete_Code();
            Load_AutoComplete_Code2();
            $('#FindAccount,,#FindJobs,#FindAccount2').dialog({
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
                minLength: 2,
                select: function(event, ui) {
                    $("[id$=txtTitleGrid]").val(ui.item.Title);
                    $("[id$=HiddenFieldTitle]").val(ui.item.Title);
                    $("[id$=lblMainCodeGrid]").val(ui.item.accMain);
                    $("[id$=lblControlCodeGrid]").val(ui.item.accControl);
                    $("[id$=lblSubCodeGrid]").val(ui.item.accSub);

                }

            });
        }
            function Get_Current_Bal() {
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
                                        currBal: item.Balance,
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
                        //$('#ctl00_ContentPlaceHolder1_GridTrans_ctl03_txtTitleGrid').val(ui.item.Title);
                        if ($(this).attr("id").indexOf("txtAccountNo") < 0) {
                            $('#' + $(this).attr('id') + 'lbl').text(ui.item.Title);
                            $("[id$=txtBalance]").text(ui.item.currBal);
                            $("[id$=txtBalanceHidden]").val(ui.item.currBal);
                            $("[id$=titlecode]").val(ui.item.Title);
                            $("[id$=lblMainCode]").val(ui.item.accMain);
                            $("[id$=lblControlCode]").val(ui.item.accControl);
                            $("[id$=lblSubCode]").val(ui.item.accSub);
                        } 
                    }

                });
            }
            
            function Load_AutoComplete_Code2() {  
        
            $("[id$=txtCode]").autocomplete({
                source: function(request, response) {
                $("[id $= txtTitleGrid]").val('');  
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: 'Services/GetData.asmx/GetAccountCodeTitle2',
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
            $('.autoCompleteCodes2').autocomplete({
                source: function(request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: 'Services/GetData.asmx/GetAccountCodeTitle2',
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
                if ($(this).attr("id").indexOf("txtAccountNo2") < 0)
                {
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
        
        
        function ValidateDebitCredit(){    
        if($('.Credit').val() == "")
        {
            //$("#comment").validate();
            
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
                if (charCode != 46) {
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
    function MyDate() {
        dateMin = $("[id $= hdnMinDate]").val();
        dateMax = $("[id $= hdnMaxDate]").val();
        $(".DateTimePicker").datepicker({ minDate: new Date(dateMin), maxDate: new Date(dateMax) });
    }

    function CheckNullVal(elem) {
        if ($(elem).val().length < 2) {
            $("[id$=hdnJobNumber]").val("");
        }
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
    </script>
    <style type="text/css">
       td[align='right'] > span
       {
             float:none !important;
       }
       td[align='center'] > span
       {
            float:none !important;
       }
       td[align='left'] > span
       {
            float:none !important;
       }
       .textarea>.alertbox div
       {
           margin-top:-20px;
       }
       .alertbox div
       {
           margin-top:0px;
       }
    </style>

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>--%>
    <asp:HiddenField ID="hdnMinDate" runat="server" />
       <asp:HiddenField ID="hdnMaxDate" runat="server" />
    
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
    <ContentTemplate>
     
    <div class="Update_area">
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>

        <div class="Heading">
        <table>
                    <tr>
                        <td style="width:328px;"></td>
                        <td style="text-align:center;width:328px;">Cash Payment Voucher</td>
                        <td style="text-align:right;width:328px;">
                            <asp:LinkButton ID="lnkNew" CssClass="buttonImp" runat="server" 
                                onclick="lnkNew_Click">New Cash Payment</asp:LinkButton>
                        </td>
                    </tr>
                </table>
        </div>
            <div id="StausMsg">
            </div>
        <br />
                 <table class="text_input_main_container">
              <tr>
                    <td class="text_container"><label>Number:</label> </td>
                    <td class="input_text_container">
                             <asp:TextBox ID="txtVoucherNumber" runat="server" Width="130px" ReadOnly="True" 
                        AutoPostBack="True" placeholder="Number" ></asp:TextBox></td>
                  <td>Job Number</td>
                                <td>
                                    <asp:TextBox ID="txtJobNumber" Width="300" runat="server" ></asp:TextBox>
                                    <%--require="Enter JobNumber" onblur='CheckNullVal(this);' validate="savevoucher"--%>
                                    <div class="SearchDiv">
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="search" CausesValidation="False" OnClientClick="return showDialog('FindJobs');" />
                                    </div>
                                    <asp:HiddenField ID="hdnJobNumber" runat="server" />
                                </td>
              </tr>
              <tr style="display:none;">
                <td class="text_container"><label>Reference Number:</label></td>
                <td class="input_text_container">
                        <asp:TextBox ID="txtRefNumber" Width="130px"
                        runat="server" MaxLength="50" placeholder="Reference Number"  ></asp:TextBox><asp:Label ID="lblRefNo" runat="server" ForeColor="#2C8CB4"></asp:Label> 
                            </td>
              </tr>
              <tr>
                <td class="text_container"><label>Date:</label></td>
                <td class="input_text_container">
                        <asp:TextBox ID="txtDate" CssClass="DateTimePicker" Width="130px" runat="server"
                        require="Enter Voucher Date" placeholder="Date"  validate="savevoucher" AutoComplete="Off"></asp:TextBox>
                            </td>
              </tr>
              </table>
                </ContentTemplate>
        </asp:UpdatePanel>
                <div class="text_input_main_container">
                	<div ></div>
                    <div class="input_text_container" style="margin-left: -2px;margin-top: -2px;padding-right: 0px;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                            <td class="text_container"><label>Cash Account:</label></td>
                                <td class="input_text_container" style="margin-top: -2px;margin-left: 2px;">
                                    <asp:Label ID="lblTransID" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:TextBox ID="txtCode" Width="130px" placeholder="Cash Account"  require="Enter Cash Account" CssClass="autoCompleteCodes2" validate="savevoucher" runat="server"/>
                                    <div class="SearchDiv">
                                        <asp:LinkButton ID="btnFind" runat="server"
                                            CausesValidation="False"  CssClass="search" onclick="btnFind_Click"/>
                                    </div>
                                    
                                    
                                    
                                </td>
                                <td >
                                    <asp:Label ID="txtCodelbl" ForeColor="#2C8CB4" runat="server" Text=""></asp:Label>
                                    <input ID="titlecode" runat="server" type="hidden" />
                                    
                                </td>
                                <td>
                                    <asp:Label ID="lblbalance" ForeColor="#2C8CB4" runat="server" Text=""></asp:Label>
                                    <input ID="txtBalanceHidden" runat="server" type="hidden" />
                                    <asp:Label ID="txtBalance" ForeColor="#2C8CB4" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="HdnFindAccount" runat="server" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtCode" EventName="TextChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                   
                    </div>
                </div>
                
                 <div class="text_input_main_container"  style="height: 52px; margin-top:-1px; float:left;">
                	<div class="text_container" ><label>Narration:</label></div>
                    <div class="input_text_container textarea" style="margin-left: 2px;">
                     <asp:TextBox ID="txtNarration" runat="server"  placeholder="Narration"  AutoComplete="Off" Height="34px" TextMode="MultiLine"
                    Width="176px"></asp:TextBox>
                
                    </div>
                    <div style="float:right; margin-top: 6px;">
                                <asp:LinkButton ID="LinkButtonBack" CssClass="buttonImp" runat="server" 
                                    Text="Back To List" onclick="LinkButtonBack_Click"/>
                            </div>
                </div>
                <div style="clear:both;"></div>
        
   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
     <ContentTemplate>
    <table style="width:100%;">
        <tr>
            <td>
                <asp:GridView CssClass="data main" ID="GridTrans" runat="server" AutoGenerateColumns="False" DataKeyNames="TransactionID"
                    ShowFooter="True" onrowdatabound="GridTrans_RowDataBound">
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
                                  <asp:TextBox ID="txtCodeGrid" require="Enter Code" validate="codeadd" onblur='NullVal()'
                                      runat="server" ValidationGroup="VGTrans"></asp:TextBox>
                            <div class="SearchDiv">
                            <asp:LinkButton ID="btnFind" runat="server" CssClass="search" CausesValidation="False" onclick="btnFind_Click1"/>
                             </div>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Title">
                            <ItemTemplate>
                                <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                 <asp:TextBox ID="txtTitleGrid" runat="server" require="Title not Select on Given Code" validate="codeadd" ReadOnly="true"/>
                                 <asp:HiddenField ID="HiddenFieldTitle" runat="server" />
                                 <%--<input ID="HiddenFieldTitle" runat="server" type="hidden" />--%>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblDebit" style="float:right;"  runat="server" Text='<%# Eval("Debit","{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtDebit" runat="server" CssClass="Credit" require="Enter Amount" validate="codeadd" onkeypress="return Verify(event);" AutoComplete="Off"></asp:TextBox>
                            </FooterTemplate>
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
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit" cssClass="edit" runat="server" CommandArgument='<%# Eval("Sno") %>' 
                                    CausesValidation="False" oncommand="btnEdit_Command" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton ID="btnAdd" style="float:left;" CssClass="buttonTnew" runat="server" Text="Add" CommandName="Add" 
                                    onclick="btnAdd_Click" OnClientClick="return validate('codeadd');"  />
                                <asp:Label ID="lblSno2" runat="server" Text='<%# Eval("Sno") %>' Visible="False"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                         <asp:LinkButton ID="LbtnRemoveGridRow" cssClass="delete" runat="server" CausesValidation="False" 
                                CommandArgument='<%# ((GridViewRow)Container).RowIndex%>'
                                CommandName="Del" oncommand="LbtnRemoveGridRow_Command" Text="" style="width: 18px;"></asp:LinkButton>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:LinkButton ID="btnCancel" CssClass="buttonNew" Text="Cancel" 
                            Visible="false" runat="server" OnClick="btnCancel_Click"></asp:LinkButton>
                    </FooterTemplate>
                    <ControlStyle Width="50px" />
                    <ItemStyle Width="50px" />
                </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>  
      
        <div style="float:left; margin-top:6px;">
                <asp:LinkButton ID="btnSave" OnClientClick="return validate('savevoucher');" CssClass="buttonImp" runat="server" Text="Save Voucher" OnClick="btnSave_Click" />
            </div>
            <div style="float:left; margin-top:6px;">
                        <asp:LinkButton ID="btnPrint" runat="server" CssClass="buttonImp" 
                                 Visible="False" onclick="btnPrint_Click" >Print View</asp:LinkButton>
                        </div>
            <div style="white-space:nowrap; margin-top:10px; color:green; padding-left:370px">
                <asp:Label ID="lbltotal" runat="server" ForeColor="#2C8CB4" Text="Total" 
                    Font-Bold="True"></asp:Label>
                
            </div>
          <div style="white-space:nowrap; color:green; padding-left:495px">
            <asp:Label ID="lblTotalAmt" ForeColor="#2C8CB4" runat="server" Font-Bold="True"></asp:Label>
          </div>
           <div style="padding-left:427px; white-space:nowrap">
            <asp:Label ID="lblValidation" runat="server" ForeColor="Red"></asp:Label>
          </div>
            
    </ContentTemplate>
   </asp:UpdatePanel> 
        
</div>
  
          </ContentTemplate>
    </asp:UpdatePanel>
        <div id="PrintReport">
            <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                <ContentTemplate>
                   <asp:Button ID="ButtonPrint" runat="server" Text="Print" CssClass="buttonNew" 
                        OnClick="ButtonPrint_Click" OnClientClick="printSelection(document.getElementById('Reports'));return false" />
                <div id="Reports">
                    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" SeparatePages="False"
                        AutoDataBind="true" EnableDrillDown="False" DisplayGroupTree="False" 
                        onnavigate="CrystalReportViewer1_Navigate1" DisplayToolbar="False"
                        oninit="CrystalReportViewer1_Init" />
                </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
<div id="FindAccount" style="padding: 0;">
          <asp:UpdatePanel ID="UpdatePanel3" runat="server">
             <ContentTemplate>
                 <br /><br /><label style="padding-left:20px">Enter Account No. </label>
                 &nbsp;<asp:TextBox ID="txtAccountNo" CssClass="autoCompleteCodes" runat="server"></asp:TextBox><asp:Button ID="btnFindAcc" runat="server" Text="Find" CssClass="buttonImp" 
                     style="float:none" onclick="btnFindAcc_Click" />
                     <br />
                 <asp:GridView ID="GrdAccounts" runat="server" CssClass="data main" 
                     AllowPaging="true" PageSize="15" 
                     onpageindexchanging="GrdAccounts_PageIndexChanging">
                     <Columns>
                         <asp:TemplateField>
                             <ItemTemplate>
                                 <asp:LinkButton ID="lnkSelect" runat="server" onclick="lnkSelect_Click">Select</asp:LinkButton>
                             </ItemTemplate>
                         </asp:TemplateField>
                     </Columns>
                  </asp:GridView>
             </ContentTemplate>
          </asp:UpdatePanel>
      </div>
    <div id="FindAccount2" style="padding: 0;">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
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
                    <asp:LinkButton ID="lbtnNo" CssClass="Button1" runat="server" OnClientClick="return closeDialog('Confirmation');">OK</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
      
</asp:Content>

