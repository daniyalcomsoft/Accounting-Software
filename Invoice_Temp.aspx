<%@ Page Title="Create Commercial Invoice" Language="C#" MasterPageFile="Main.master" AutoEventWireup="true"
    CodeFile="Invoice_Temp.aspx.cs" Inherits="Invoice_Temp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="Script/jquery-1.6.2.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="Script/jquery-ui-1.8.16.custom.min.js" type="text/javascript" charset="utf-8"></script>


    <script type="text/javascript">

//    function ChangeConversionRate() {
//    $("[id $= txtConversionRate]").change(function() {

//    GrandPKRTotals();

//    });
//    }

//    function TotalGridAmount() {
//    total = 0;
//    GrandInvoiceTotal = 0;
//    GrandTotal = 0;

//    $("[id $= txttotal2],[id $= txtDiscount]").change(function() {
//    GrandTotals();

//    });
//    }

//    function GrandTotals() {
//    var tot1 = $("[id $= txttotal2]").val();

//    var discount = $("[id $= txtDiscount]").val();
//    if (discount == "") {
//    discount = 0;
//    }
//    //   var dis = discount.toFixed(3);
//    GrandInvoiceTotal = parseFloat(tot1 - discount);
//    $("[id $= txtTot]").val(GrandInvoiceTotal.toFixed(2));
//    GrandPKRTotals();
//    }


//    function GrandPKRTotals() {
//    var tot1 = $("[id $= txtTot]").val();

//    var ConversionRate = $("[id $= txtConversionRate]").val();
//    var NetTotalAmount = parseFloat(tot1 * ConversionRate);
//    //   var dis = discount.toFixed(3);
//    //GrandInvoiceTotal = parseFloat(tot1 * ConversionRate);
//    $("[id $= txtPKRTotal]").val(NetTotalAmount.toFixed(2));
//    }

//    function ChangeConversionRate() {
//    $("[id $= txtConversionRate]").change(function() {

//    GrandPKRTotals();

//    });
//    }



//    //    function vale() {
//    //        $("[id $= txtQuantity]").blur(function() {
//    //            var quantity = $(this).val();
//    //            var rate = $(this).parent().siblings().find("[id $= txtRate]").val();
//    //            var TotalPrice = parseFloat(quantity * rate);
//    //            var Fixed = TotalPrice.toFixed(2);
//    //            var Total = $(this).parent().siblings().find("[id $= txtAmount]").val(Fixed);
//    //            GrossTotalDeduction();
//    //        });
//    //        
//    //              
//    //        
//    //        

//    //        $("[id $= txtRate]").blur(function() {
//    //            var DeductionType = $(this).parent().siblings().find("[id $= txtDescription]").val();
//    //            var units = $(this).val();
//    //            if (DeductionType == "Amount") {

//    //                var Total = $(this).parent().siblings().find("[id $= txtAmount]").val(units);
//    //            }
//    //            else {
//    //                var quantity = $(this).parent().siblings().find("[id $= txtQuantity]").val();
//    //                var TotalPrice = parseFloat(quantity * units);
//    //                var Fixed = TotalPrice.toFixed(2);
//    //                var Total = $(this).parent().siblings().find("[id $= txtAmount]").val(Fixed);
//    //            }
//    //            GrossTotalDeduction();
//    //            
//    //            
//    //        });
//    //    }
//    //    var gross;


//    //    function GrossTotalDeduction() {
//    //        grosstotal = 0;
//    //        $("[id $= GV_InvoiceDetail] [id $= txtAmount]").each(function(index, item) {

//    //        if ($(item).val() == "") { $(item).val(0); }
//    //        grosstotal = grosstotal + parseFloat($(item).val());});
//    //        $("[id $= txtGrandInvoiceTotal]").val(grosstotal.toFixed(2)).change();
//    //    }
//    //    var grossdeduction;


//    /**/



//    function vale() {

//    $(".gv_txtQuantity").blur(function() {
//    var quantity = $(this).val();
//    var rate = $(this).parent().siblings().find(".gv_txtRate").val();
//    var TotalPrice = parseFloat(quantity * rate);
//    var Fixed = TotalPrice.toFixed(2);
//    var Total = $(this).parent().siblings().find(".gv_txtAmount").val(Fixed);
//    GrossTotalDeduction();
//    });
//    $(".gv_txtRate").blur(function() {
//    var DeductionType = $(this).parent().siblings().find(".gv_txtDescription").val();
//    var units = $(this).val();
//    if (DeductionType == "Amount") {

//    var Total = $(this).parent().siblings().find(".gv_txtAmount").val(units);
//    }
//    else {
//    var quantity = $(this).parent().siblings().find(".gv_txtQuantity").val();
//    var TotalPrice = parseFloat(quantity * units);
//    var Fixed = TotalPrice.toFixed(2);
//    var Total = $(this).parent().siblings().find(".gv_txtAmount").val(Fixed);
//    }
//    GrossTotalDeduction();
//    });

//    }
//    var gross;


//    function GrossTotalDeduction() {
//    grosstotal = 0;
//    $(".gv_txtAmount").each(function(index, item) {
//    if ($(item).val() == "") { $(item).val(0); }

//    grosstotal = grosstotal + parseFloat($(item).val());
//    });
//    $("[id $= txttotal2]").val(grosstotal.toFixed(2)).change();

//    }
//    var grossdeduction;
//    var selectedRow = -1;

        $(document).ready(function() {
            //        $("input[id$='btnaddlines2']").click(function() {
            //        });

            MyDate();
            ChangeDateEvent();
            //vale();
            //GrossTotalDeduction();
            //TotalGridAmount();
            totalAmount();
            //ChangeConversionRate();
            Job_AutoCom_Dialog();
            recalculate();
        });

//        function recalculate() {
//            $("[id$=txtByParty],[id$=txtCustomByParty],[id$=txtSalesTaxByParty],[id$=txtIncomeTaxByParty],[id$=txtCEDByParty],[id$=txtEOCByParty],[id$=txtFEDByParty],[id$=txtOthersByParty],[id$=txtExcessShortDutyByParty],[id$=txtByUs],[id$=txtCustomByUs],[id$=txtSalesTaxByUs],[id$=txtIncomeTaxByUs],[id$=txtCEDByUs],[id$=txtEOCByUs],[id$=txtFEDByUs],[id$=txtOthersByUs],[id$=txtExcessShortDutyByUs]").change(function() 
//            {
//                totalAmount();
//            });
//        }

        function recalculate() {
            $("[id$=txtByParty],[id$=txtByParty_duties],[id$=txtByUs],[id$=txtByUs_duties]").change(function() {
                totalAmount();
            });
        }
         
        function MyDate() {
            dateMin = $("[id $= hdnMinDate]").val();
            dateMax = $("[id $= hdnMaxDate]").val();
            $("[id$=txtInvoiceDate],[id$=txtCustomPODate],[id$=txtReceivedDate]").datepicker({ minDate: new Date(dateMin), maxDate: new Date(dateMax) });
        }

        function totalAmount() {
            var TotalByParty = 0;
            $("[id$=txtByParty],[id$=txtByParty_duties]").each(function() {
                if (!isNaN(this.value) && this.value.length != 0) {
                    TotalByParty += parseFloat(this.value);
                }
            });
            $("[id $= txttotalByParty]").val(TotalByParty.toFixed(2)).change();
            var TotalByUS = 0;
            $("[id$=txtByUs],[id$=txtByUs_duties]").each(function() {
                if (!isNaN(this.value) && this.value.length != 0) {
                    TotalByUS += parseFloat(this.value);
                }
            });
            $("[id $= txtTotalByUS]").val(TotalByUS.toFixed(2)).change();
        }

        function ChangeDateEvent() {
            $("[id $= txtInvoiceDate]").change(function() {
                dateMin = $("[id $= hdnMinDate]").val();
                dateMax = $("[id $= hdnMaxDate]").val();
                var invoiceDate = $(this).val();
                if (Date.parse(invoiceDate) < Date.parse(dateMin) || Date.parse(invoiceDate) > Date.parse(dateMax)) {
                    $(this).val('');
                }
            });
        }

        function Job_AutoCom_Dialog() {
            if ($("[id$=hdnJobNumber]").val() != "")
                $("[id $= tblJobDetail]").show();

            $("[id$=txtJobNumber]").autocomplete({
                source: function(request, response) {
                    $("[id $= txtTitle],[id$=hdnJobNumber]").val('');
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: 'Services/GetData.asmx/GetJobByNumber',
                        data: "{ 'Match': '" + request.term + "'}",
                        dataType: "json",
                        success: function(data) {
                            response($.map(data.d, function(item) {
                                return {
                                    label: item.JobNumber,
                                    value: item.JobNumber,
                                    JobID: item.JobID,
                                    Job: item
                                }
                            }))
                        }
                    });
                },
                minLength: 2,
                select: function(event, ui) {
                    $("[id$=txtJobNumber]").val(ui.item.value);
                    $("[id$=hdnJobNumber]").val(ui.item.JobID);
                    fillJobdetail(ui.item.Job);
                }
            });

            $('#FindJobs').dialog({
                autoOpen: false,
                draggable: true,
                title: "Find",
                width: 972,
                height: 450,
                open: function(type, data) {
                    $(this).parent().appendTo("form");
                }
            });

            $('#NewInvoiceDesc').dialog({
                autoOpen: false,
                draggable: true,
                title: "New Invoice Description",
                width: 670,
                height: 100,
                open: function(type, data) {
                    $(this).parent().appendTo("form");
                }
            });

            $('#NewInvoiceDutiesDesc').dialog({
                autoOpen: false,
                draggable: true,
                title: "New Invoice Duties Description",
                width: 670,
                height: 100,
                open: function(type, data) {
                    $(this).parent().appendTo("form");
                }
            });

            $("[id$=GV_CommercialInvoiceDetail] select[id$=ddlDescription]").change(function() {
                if ($(this).val() == "-1") {
                    showDialog('NewInvoiceDesc');
                    return false;
                }
                else {
                    return true;
                }
            });

            $("[id$=GV_CommercialInvoiceDutiesDetail] select[id$=ddlDescription_duties]").change(function() {
                if ($(this).val() == "-1") {
                    showDialog('NewInvoiceDutiesDesc');
                    return false;
                }
                else {
                    return true;
                }
            });

            //$("[ID$=txtDate]").datepicker();
        }

        function fillJobdetail(j) {
            $("[id $= lblCustomerName]").text(j.CustomerName);
//            $("[id $= lblContNumber]").text(j.ContactNo);
//            $("[id $= lblContDated]").text("");
            $("[id $= lblDescription]").text(j.JobDescription);
            $("[id $= lblContainer]").text(j.Container);
            $("[id $= lblContainerNo]").text(j.ContainerNo);
            $("[id $= lblContainerDate]").text(j.ContainerDate);
            $("[id $= lblIGMNo]").text(j.IGMNo);
            $("[id $= lblIGMDated]").text(toDateFromJson(j.IGMDate));
            $("[id $= lblIndexNo]").text(j.IndexNo);
            $("[id $= lblSS]").text(j.SS);
            $("[id $= lblQty]").text(j.QTY);
            $("[id $= lblBECashNo]").text(j.BECashNo);
            $("[id $= lblBECashDated]").text(toDateFromJson(j.MachineDate));
            $("[id $= lblMachineNo]").text(j.MachineNo);
            $("[id $= lblMachineDate]").text(toDateFromJson(j.MachineDate));
            $("[id $= lblDeliveryDate]").text(toDateFromJson(j.DeliveryDate));
            $("[id $= lblCNFValue]").text(j.CNFValue);
            $("[id $= lblImportValue]").text(j.ImportValue);
            $("[id $= tblJobDetail]").show();
        }

        function CheckNullVal(elem) {
            if ($(elem).val().length < 2) {
                $("[id$=hdnJobNumber]").val("");
            }
        }

        function toDateFromJson(src) {
            return convertDate(new Date(parseInt(src.substr(6))));
        }

        function convertDate(inputFormat) {
            function pad(s) { return (s < 10) ? '0' + s : s; }
            var d = new Date(inputFormat);
            return [pad(d.getMonth() + 1), pad(d.getDate()), d.getFullYear()].join('/');
        }
    </script>

    <style type="text/css">
        .block, .block-fluid
        {
            border: 1px solid #CCC;
            border-top: 0px;
            background-color: #F9F9F9;
            margin-bottom: 05px;
            -moz-border-bottom-left-radius: 3px;
            -moz-border-bottom-right-radius: 3px;
            -webkit-border-bottom-left-radius: 3px;
            -webkit-border-bottom-right-radius: 3px;
            -o-border-bottom-left-radius: 3px;
            -o-border-bottom-right-radius: 3px;
            -ms-border-bottom-left-radius: 3px;
            -ms-border-bottom-right-radius: 3px;
            border-bottom-left-radius: 3px;
            border-bottom-right-radius: 3px;
        }
        .container
        {
            margin: 0 auto;
            width: 960px;
            font-size: 12px;
            font-family: arial;
            font-weight: bold;
        }
        .top_heading
        {
            text-align: center;
        }
        select
        {
            border: 01px solid #ccc;
            height: 30px;
            width: 250px;
        }
        input
        {
            height: 30px;
            line-height: 30px;
            border: outset 1px #CCC;
            font-size: 14px;
            border-image: initial;
            width: 192px;
        }
        
        }
        .ui-tabs .ui-tabs-panel
        {
            display: block;
            border-width: 0;
            padding: 0.2em 0.2em;
            background: none;
        }
        .blance
        {
            text-align: right;
            font-size: 16px;
            font-weight: bold;
        }
        .amount
        {
            font-size: 16px;
        }
        .textarea
        {
            width: 200px;
            position: relative;
            top: -1px;
        }
        .sam_textbox
        {
            width: 120px;
        }
        .sam_textbox1
        {
            width: 70px;
        }
        .btn_1
        {
            height: 30px;
            width: 100px;
            line-height: 27px;
            background-color: #029FE2;
            border-radius: 4px;
            color: #EDF6E3;
            font-size: 12px;
            border: 1px solid #029FE2;
        }
        .btn_1:hover
        {
            background-color: #2C8CB4;
        }
        .btn_spacing
        {
            margin-right: 5px;
        }
        .subtotal
        {
            float: right;
        }
        .subtotal table
        {
            width: 480px;
            text-align: right;
        }
        .subtotal table tr
        {
            line-height: 40px;
        }
        .subtotal table tr td div select
        {
            line-height: 20px;
        }
        .discount_value
        {
            width: 150px;
            height: 35px;
        }
        .comment_box
        {
            height: 86px;
            width: 350px;
            resize: none;
        }
        hr
        {
            border-bottom: 1px solid #CCC;
        }
        .file_uploader
        {
            margin-top: 15px;
        }
        .subtotal_txt
        {
            float: right;
            max-width: 150px;
            border: 1px solid transparent !important;
            text-align: right;
            font-weight: bold;
        }
        .float_right
        {
            float: right;
        }
        select .alertbox div
        {
            margin-top: -50px !important;
        }
        table.data td
        {
            font-size: 1.06em;
            border: none !important;
            color: #555;
            padding: 2px 2px !important;
            border-top: 1px solid #E6E6E6 !important;
            border-left: 1px solid #F2F2F2 !important;
            font-family: Verdana, Geneva, sans-serif;
            border-image: initial;
        }
        table.data
        {
            width: 100%;
            background: white;
            border: 2px solid #029FE2;
            font-size: 10px;
        }
        table.data th
        {
            font-size: 10px !important;
        }
        table.data input[type="text"]
        {
            width: 83px;
            line-height: 20px;
            height: 20px;
        }
        .data tr td:nth-child(8)
        {
            text-align: center;
            width: 75px;
        }
        .note
        {
            width: 96.4%;
            padding-left: 10px;
            height: 130px;
            min-height: 130px;
            max-height: 130px;
        }
        #attachments tr td
        {
        	border: 1px solid black;
       }
 
        
        input[type="checkbox"]
        {
        	width: 15px;
            height: 15px;
            padding-left:5px;
            padding-right:5px;
            float:right;
       }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="NewInvoiceDesc" style="padding: 0;">
        <asp:UpdatePanel ID="UpInvoiceDesc" runat="server">
            <ContentTemplate>
                <table style="width: 650px; margin: 15px auto;">
                    <tr>
                        <td>
                            <label>
                                Invoice Description
                            </label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtInvoiceDesc" runat="server" Width="400"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSaveInvoiceDesc" runat="server" Text="Save" CssClass="btn_1 btn_spacing float_right"
                                OnClick="btnSaveInvoiceDesc_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="NewInvoiceDutiesDesc" style="padding: 0;">
        <asp:UpdatePanel ID="UpInvoiceDutiesDesc" runat="server">
            <ContentTemplate>
                <table style="width: 650px; margin: 15px auto;">
                    <tr>
                        <td>
                            <label>
                                Invoice Duties Description
                            </label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtInvoiceDutiesDesc" runat="server" Width="400"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSaveInvoiceDutiesDesc" runat="server" Text="Save" CssClass="btn_1 btn_spacing float_right"
                                OnClick="btnSaveInvoiceDutiesDesc_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="FindJobs" style="padding: 0;">
        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
                <br />
                <br />
                <label style="padding-left: 20px">
                    Enter Job Number.
                </label>
                &nbsp;
                <asp:TextBox ID="txtJobNumberSearch" runat="server"></asp:TextBox>
                <asp:Button ID="btnFindJob" runat="server" Text="Find" CssClass="buttonImp" Style="float: none"
                    OnClick="btnFindJob_Click" />
                <br />
                
                <asp:GridView ID="grdJobs" runat="server" CssClass="data main" AutoGenerateColumns="False"
                    DataSourceID="sqlDSJobs" EnableModelValidation="True" style="clear:both;" >
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
                <asp:SqlDataSource ID="sqlDSJobs" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand="SELECT J.[JobID],J.[JobNumber],J.[JobDescription],C.[DisplayName]
	                            ,CASE WHEN J.[Completed] = 1 THEN 'TRUE' ELSE 'FALSE' END Completed FROM [vt_SCGL_Job] J
	                            INNER JOIN vt_SCGL_Customer C ON J.[CustomerID] = C.[CustomerID]">
                </asp:SqlDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="Heading">
        <div id="StausMsg">
        </div>
        <table style="width: 100%;">
            <tbody>
                <tr>
                    <td style="text-align: center;">
                        <b class="top_heading">COMMERCIAL INVOICE</b>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="container">
        <asp:UpdatePanel ID="UpJobDetail" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Invoice Date:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtInvoiceDate" runat="server" require="Select Invoice Date" validate="SaveInvoice"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Reference No:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtReferenceNo" runat="server" 
                                ontextchanged="txtReferenceNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label18" runat="server" Text="Cust Inv. No:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustInvoiceNo" runat="server"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="Job Number:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <div style="width: 250px;">
                                <asp:TextBox ID="txtJobNumber" Style="text-align: left; line-height: normal;" Height="15"
                                    Width="192" runat="server" require="Enter JobNumber" onblur='CheckNullVal(this);'
                                    validate="SaveInvoice" ontextchanged="txtJobNumber_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <div class="SearchDiv">
                                    <asp:LinkButton ID="btnFind" runat="server" CssClass="search" CausesValidation="False"
                                        OnClientClick="return showDialog('FindJobs');" />
                                </div>
                                <asp:HiddenField ID="hdnJobNumber" runat="server" />
                            </div>
                        </td>
                        <td>
                            <asp:Label runat="server" Text="Bill Number:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBillNumber" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label23" runat="server" Text="No Advance:" Font-Bold="true"></asp:Label>
                            <asp:CheckBox ID="chkNoAdvance" runat="server" style="float:left;"  />
                        </td>
                        <td>
                            <asp:Label ID="Label19" runat="server" Text="Abbott Invoice:" Font-Bold="true"></asp:Label>
                            <asp:CheckBox ID="chkAbbottInvoice" runat="server" style="float:left;"  />
                        </td>
                    </tr>
                    </table>
                    <table style="width: 100%;display:none;">
                    <tr>
                        
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Custom P.O No:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomPONo" runat="server" Width="120px"></asp:TextBox>
                                
                        </td>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Custom P.O Date:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomPODate" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="By Party:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomByParty" CssClass="decimalOnly" runat="server" Width="120px"></asp:TextBox>
                                
                        </td>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="By Us:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomByUs" CssClass="decimalOnly" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        
                        
                    </tr>
                    <tr>
                                                                      
                         <td>
                            <asp:Label ID="Label3" runat="server" Text="Sales Tax PO No:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSalesTaxPONo" runat="server" Width="120px"></asp:TextBox>
                                
                        </td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Fine:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSalesTaxFine" CssClass="decimalOnly" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="By Party:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSalesTaxByParty" CssClass="decimalOnly" runat="server" Width="120px"></asp:TextBox>
                                
                        </td>
                        <td>
                            <asp:Label ID="Label10" runat="server" Text="By Us:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSalesTaxByUs" CssClass="decimalOnly" runat="server" Width="120px"></asp:TextBox>
                        </td>
                      
                    </tr>
                    <tr>
                        <%--<td>
                            <asp:Label ID="Label5" runat="server" Text="Income Tax:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtIncomeTax" runat="server"></asp:TextBox>
                        </td>--%>
                        
                         <td>
                            <asp:Label ID="Label11" runat="server" Text="Income Tax PO No:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtIncomeTaxPONo" runat="server" Width="120px"></asp:TextBox>
                              
                        </td>
                        <td>
                            <asp:Label ID="Label12" runat="server" Text="Addition:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtIncomeTaxAddition" CssClass="decimalOnly" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label13" runat="server" Text="By Party:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtIncomeTaxByParty" CssClass="decimalOnly" runat="server" Width="120px"></asp:TextBox>
                                
                        </td>
                        <td>
                            <asp:Label ID="Label14" runat="server" Text="By Us:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtIncomeTaxByUs" CssClass="decimalOnly" runat="server" Width="120px"></asp:TextBox>
                        </td>
                      
                    </tr>
                    <tr>
                                                                      
                         
                        <td>
                            <asp:Label ID="Label20" runat="server" Text="C.E.D%:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCEDPercent" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label21" runat="server" Text="By Party:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCEDByParty" CssClass="decimalOnly" runat="server" Width="120px"></asp:TextBox>
                                
                        </td>
                        <td>
                            <asp:Label ID="Label22" runat="server" Text="By Us:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCEDByUs" CssClass="decimalOnly" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td></td>
                      
                    </tr>
                    <tr>
                                                                      
                         
                        <td>
                            <asp:Label ID="Label24" runat="server" Text="E.O.C%:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEOCPercent" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label25" runat="server" Text="By Party:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEOCByParty" CssClass="decimalOnly" runat="server" Width="120px"></asp:TextBox>
                                
                        </td>
                        <td>
                            <asp:Label ID="Label26" runat="server" Text="By Us:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEOCByUs" CssClass="decimalOnly" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td></td>
                      
                    </tr>
                    <tr>
                                                                      
                        <td>
                            <asp:Label ID="Label28" runat="server" Text="F.E.D%:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFEDPercent" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label29" runat="server" Text="By Party:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFEDByParty" CssClass="decimalOnly" runat="server" Width="120px"></asp:TextBox>
                                
                        </td>
                        <td>
                            <asp:Label ID="Label30" runat="server" Text="By Us:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFEDByUs" CssClass="decimalOnly" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td></td>
                      
                    </tr>
                    <tr>
                                                                      
                        <td>
                            <asp:Label ID="Label32" runat="server" Text="Others%:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOthersPercent" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label33" runat="server" Text="By Party:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOthersByParty" CssClass="decimalOnly" runat="server" Width="120px"></asp:TextBox>
                                
                        </td>
                        <td>
                            <asp:Label ID="Label34" runat="server" Text="By Us:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOthersByUs" CssClass="decimalOnly" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td></td>
                      
                    </tr>
                    <tr>
                                                                      
                         <td>
                            <asp:Label ID="Label35" runat="server" Text="Short/Excess Duty PO No:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtExcessShortDutyPONo" runat="server" Width="120px"></asp:TextBox>
                                
                        </td>
                        <td>
                            <asp:Label ID="Label36" runat="server" Text="By Party:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtExcessShortDutyByParty" CssClass="decimalOnly" runat="server" Width="120px"></asp:TextBox>
                                
                        </td>
                        <td>
                            <asp:Label ID="Label37" runat="server" Text="By Us:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtExcessShortDutyByUs" CssClass="decimalOnly" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td></td>
                      
                    </tr>
                    
                    
                </table>
              
                <table id="tblJobDetail" runat="server" width="100%" style="display: none;">
                    <tr>
                        <td colspan="10">
                            <asp:Label ID="lblCustomerName" runat="server" Text="" Font-Bold="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="Cont No:" Font-Bold="true"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblContainerNo" runat="server" Text="" Width="100px" Font-Bold="false"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" Text="Dated:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblContainerDate" runat="server" Text="" Width="100px" Font-Bold="false"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" Text="Des:" Font-Bold="true"></asp:Label>
                        </td>
                        <td colspan="4">
                            <asp:Label ID="lblDescription" runat="server" Text="" Width="350px" Font-Bold="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="Container:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblContainer" runat="server" Text="" Width="100px" Font-Bold="false"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" Text="IGM No:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblIGMNo" runat="server" Text="" Width="100px" Font-Bold="false"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" Text="Dated:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblIGMDated" runat="server" Text="" Width="100px" Font-Bold="false"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" Text="Index No:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblIndexNo" runat="server" Text="" Width="100px" Font-Bold="false"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" Text="S.S:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblSS" runat="server" Text="" Width="100px" Font-Bold="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="QTY:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblQty" runat="server" Text="" Width="100px" Font-Bold="false"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" Text="B.E.Cash No:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblBECashNo" runat="server" Text="" Width="100px" Font-Bold="false"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" Text="Dated:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblBECashDated" runat="server" Text="" Width="100px" Font-Bold="false"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" Text="Machine Date:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblMachineNo" runat="server" Text="" Width="100px" Font-Bold="false"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" Text="DT:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblMachineDate" runat="server" Text="" Width="100px" Font-Bold="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                        </td>
                        <td style="text-align: center" colspan="2">
                            <asp:Label runat="server" Text="Delivery Date:" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblDeliveryDate" runat="server" Text="" Font-Bold="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="CNF Value Rs." Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCNFValue" runat="server" Text="" Font-Bold="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="Import Value Rs." Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblImportValue" runat="server" Text="" Font-Bold="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div id="processMessage">
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="Upd1" runat="server">
            <ContentTemplate>
              <div style="clear: both;">
                </div>
               <div style="margin-bottom: 15px;">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                           
                             <asp:GridView ID="GV_CommercialInvoiceDutiesDetail" Width="100%" runat="server" CssClass="data main"
                                AutoGenerateColumns="False" OnRowDeleting="GV_CommercialInvoiceDutiesDetail_RowDeleting">
                               
                                <Columns>
                                    <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="350px">
                                        <ItemTemplate>
                                            <%--<asp:TextBox ID="txtProduct" runat="server" AutoComplete="Off" CssClass="autoCompleteCodes" Width="250px" require="Enter Product" validate="SaveInvoice"  Text='<%#Bind("txtProduct") %>'></asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlDescription_duties" runat="server" Width="340px" validate="SaveInvoice" custom="Select Duties Description" customFn="var IInvoiceDutiesDescID = parseInt(this.value); return IInvoiceDutiesDescID > 0;">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="250px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Number" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNumber_duties" runat="server" CssClass="autoCompleteCodes4"
                                                Width="140px" Text='<%#Bind("txtNumber_duties") %>'>
                                            </asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="114px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-Width="150px" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks_duties" runat="server" CssClass="autoCompleteCodes4"
                                                Width="140px" Text='<%#Bind("txtRemarks_duties") %>'>
                                            </asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="114px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                            <%--<asp:TextBox ID="txtQuantity" runat="server" ReadOnly="true" CssClass="gv_txtQuantity SmallTextbox decimalOnly" Text='<%#Bind("txtQuantity")%>' /> </asp:TextBox>--%>
                                            <asp:TextBox ID="txtDate_duties" runat="server" CssClass="SmallTextbox" Width="140px"
                                                 Text='<%#Bind("txtDate_duties") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="114px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="By Party" ItemStyle-Width="130px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtByParty_duties" runat="server" CssClass="SmallTextbox decimalOnly"
                                                Width="120px" Text='<%#Bind("txtByParty_duties")%>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="114px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="By US" ItemStyle-Width="130px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtByUs_duties" runat="server" CssClass="SmallTextbox decimalOnly"
                                                Width="120px" Text='<%#Bind("txtByUs_duties")%>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="114px" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="true" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div>
                    <asp:Button ID="btnAddLines_duties" runat="server" Text="Add lines" CssClass="btn_1 btn_spacing"
                        OnClick="btnAddLines_duties_Click" />
                    <asp:Button Visible="false" ID="btnClearAllLines_duties" runat="server" Text="Clear all lines"
                        CssClass="btn_1" OnClick="btnClearAllLines_duties_Click" />
                </div>
              
              
                <div style="clear: both;">
                </div>
                <div style="margin-bottom: 15px;">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GV_CommercialInvoiceDetail" Width="100%" runat="server" CssClass="data main"
                                AutoGenerateColumns="False" OnRowDeleting="GV_CommercialInvoiceDetail_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="350px">
                                        <ItemTemplate>
                                            <%--<asp:TextBox ID="txtProduct" runat="server" AutoComplete="Off" CssClass="autoCompleteCodes" Width="250px" require="Enter Product" validate="SaveInvoice"  Text='<%#Bind("txtProduct") %>'></asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlDescription" runat="server" Width="340px" validate="SaveInvoice" custom="Select Invoice Description" customFn="var IDescID = parseInt(this.value); return IDescID > 0;">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="250px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Number" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNumber" runat="server" CssClass="autoCompleteCodes4"
                                                Width="140px" Text='<%#Bind("txtNumber") %>'>
                                            </asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="114px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="autoCompleteCodes4"
                                                Width="140px" Text='<%#Bind("txtRemarks") %>'>
                                            </asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="114px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date/Amount" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                            <%--<asp:TextBox ID="txtQuantity" runat="server" ReadOnly="true" CssClass="gv_txtQuantity SmallTextbox decimalOnly" Text='<%#Bind("txtQuantity")%>' /> </asp:TextBox>--%>
                                            <asp:TextBox ID="txtDate" runat="server" CssClass="SmallTextbox" Width="140px"
                                                 Text='<%#Bind("txtDate") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="114px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="By Party" ItemStyle-Width="130px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtByParty" runat="server" CssClass="SmallTextbox decimalOnly"
                                                Width="120px" require="Enter By Party Price" validate="SaveInvoice" Text='<%#Bind("txtByParty")%>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="114px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="By US" ItemStyle-Width="130px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtByUs" runat="server" CssClass="SmallTextbox decimalOnly"
                                                Width="120px" require="Enter By Us Price" validate="SaveInvoice" Text='<%#Bind("txtByUs")%>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="114px" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="true" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div>
                    <asp:Button ID="btnAddLines" runat="server" Text="Add lines" CssClass="btn_1 btn_spacing"
                        OnClick="btnAddLines_Click" />
                    <asp:Button Visible="false" ID="btnClearAllLines" runat="server" Text="Clear all lines"
                        CssClass="btn_1" OnClick="btnClearAllLines_Click" />
                </div>
                <div class="subtotal">
                    <table>
                        <tr>
                            <td style="width: 320px">
                                Total By Party
                            </td>
                            <td>
                                <input id="txttotalByParty" readonly="readonly" runat="server" class="subtotal_txt" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 320px">
                                Total By US
                            </td>
                            <td>
                                <input id="txtTotalByUS" readonly="readonly" runat="server" class="subtotal_txt" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="clear: both;">
                </div>
                <div style="margin-bottom: 10px;">
                    <asp:UpdatePanel ID="updpnlbtn" runat="server">
                        <ContentTemplate>
                            <table style="width: 950px;">
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                            CssClass="btn_1 btn_spacing float_right" />
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn_1 btn_spacing float_right"
                                            OnClick="btnSave_Click" OnClientClick="return validate('SaveInvoice');" />
                                    </td>
                                </tr>
                            </table>
                            <div id="Notification_ItemID" style="display: none; width: 98%; margin: auto;">
                                <div class="alert-red">
                                    <h4>
                                        Error!</h4>
                                    <label id="IdError" runat="server" style="color: White">
                                        Add Atleast One Record</label>
                                </div>
                            </div>
                            <div id="Notification_Success" style="display: none; width: 98%; margin: auto;">
                                <div class="alert-green">
                                    <h4>
                                        Success</h4>
                                    <label id="lblSuccessMsg" runat="server" style="color: White">
                                        Invoice Created Successfully</label>
                                </div>
                            </div>
                            <div id="Notification_Error" style="display: none; width: 98%; margin: auto;">
                                <div class="alert-red">
                                    <h4>
                                        Error!</h4>
                                    <label id="lblNewError" runat="server" style="color: White">
                                        Please Select Item</label>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div style="clear: both;">
                </div>
                
                <div style="clear: both">
                </div>
                
                <table id="attachments" style="width: 956px;" >
                    <tr>
                        <th colspan="7" style="font-size:large;">
                            Enclosed Documents/Attachments</th>
                        <tr>
                            <td>
                                <label>
                                Delivery Challan</label> &nbsp;
                                <asp:CheckBox ID="chkDeliveryChallan" runat="server" />
                            </td>
                            <td>
                                <label>
                                L/C Contract</label>
                                <asp:CheckBox ID="chkLCContract" runat="server" />
                            </td>
                            <td>
                                <label>
                                Certificates</label>
                                <asp:CheckBox ID="chkCertificates" runat="server" />
                            </td>
                            <td>
                                <label>
                                Packing List</label>
                                <asp:CheckBox ID="chkPackingList" runat="server" />
                            </td>
                            <td>
                                <label>
                                Invoice</label>
                                <asp:CheckBox ID="chkInvoice" runat="server" />
                            </td>
                            <td>
                                <label>
                                Weboc GD&#39;S</label>
                                <asp:CheckBox ID="chkWeboc" runat="server" />
                            </td>
                            <td>
                                <label>
                                Paccs Coupon</label>
                                <asp:CheckBox ID="chkPaccsCoupon" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                Cash Payment Receipt</label> &nbsp;
                                <asp:CheckBox ID="chkCashPayReceipt" runat="server" />
                            </td>
                            <td>
                                <label>
                                Excise Challan</label>
                                <asp:CheckBox ID="chkExciseDutyChallan" runat="server" />
                            </td>
                            <td>
                               
                                <label>
                                Bond Papers</label>
                                <asp:CheckBox ID="chkBondPapers" runat="server" />
                            </td>
                            <td>
                                <label>
                                Delivery Order Receipt</label>
                                <asp:CheckBox ID="chkDOR" runat="server" />
                            </td>
                            <td>
                                <label>
                                Q.I.C.T.L Invoice</label>
                                <asp:CheckBox ID="chkPICTLInv" runat="server" />
                            </td>
                            <td>
                                <label>
                                Transportation Bill</label>
                                <asp:CheckBox ID="chkTransportBill" runat="server" />
                            </td>
                            <td>
                                <label>
                                G.S.T Invoice</label>
                                <asp:CheckBox ID="chkGSTInv" runat="server" />
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td>
                                <label>
                                I/T Challan</label> &nbsp;
                                <asp:CheckBox ID="chkITChallan" runat="server" />
                            </td>
                            <td>
                                <label>
                                B/E Importer (Copy)</label>
                                <asp:CheckBox ID="chkBEImporter" runat="server" />
                            </td>
                            <td>
                                <label>
                                KPT Wharfage Bill/Coupen</label>
                                <asp:CheckBox ID="chkKPTWharfage" runat="server" />
                            </td>
                            <td>
                                <label>
                                KPT Storage Bill/Coupen</label>
                                <asp:CheckBox ID="chkKPTStorage" runat="server" />
                            </td>
                            <td>
                                <label>
                                M.T.O Lift On Off Receipt</label>
                                <asp:CheckBox ID="chkMTOLift" runat="server" />
                            </td>
                            <td>
                                <label>
                                Yard Charges Receipt</label>
                                <asp:CheckBox ID="chkYardCharges" runat="server" />
                            </td>
                            <td>
                                <label>
                                E Form (Copy)</label>
                                <asp:CheckBox ID="chkEForm" runat="server" />
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td>
                                <label>
                                B/L (Copy)</label> &nbsp;
                                <asp:CheckBox ID="chkBL" runat="server" />
                            </td>
                            <td>
                                <label>
                                Air way B/L</label>
                                <asp:CheckBox ID="chkAirwayBL" runat="server" />
                            </td>
                            <td>
                                <label>
                                Insurance</label>
                                <asp:CheckBox ID="chkInsuranceDoc" runat="server" />
                            </td>
                            <td>
                                 <label>
                                Excise &amp; Taxation Challan</label>
                                <asp:CheckBox ID="chkExciseTaxChallan" runat="server" />
                            </td>
                            <td>
                                <label>
                                B/E Exchange Control</label>
                                <asp:CheckBox ID="chkBEExchange" runat="server" />
                            </td>
                            <td>
                                <label>
                                Original</label>
                                <asp:CheckBox ID="chkOriginal" runat="server" />
                            </td>
                            <td>
                                <label>
                                Duplicate</label>
                                <asp:CheckBox ID="chkDuplicate" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="display:none;">
                                <label>
                                EndorsmentReceipt</label> &nbsp;
                                <asp:CheckBox ID="chkEndorsmentReceipt" runat="server" />
                            </td>
                            <td>
                            <asp:TextBox ID="OtherDocs1" runat="server" style="width:129px; font-size:12px;"></asp:TextBox>
                            <asp:CheckBox ID="chkOtherDocs1" runat="server" />
                            </td>
                            <td>
                            <asp:TextBox ID="OtherDocs2" runat="server" style="width:129px; font-size:12px;"></asp:TextBox>
                            <asp:CheckBox ID="chkOtherDocs2" runat="server" />
                            </td>
                            <td>
                            <asp:TextBox ID="OtherDocs3" runat="server" style="width:129px; font-size:12px;"></asp:TextBox>
                            <asp:CheckBox ID="chkOtherDocs3" runat="server" />
                            </td>
                            <td>
                            <asp:TextBox ID="OtherDocs4" runat="server" style="width:129px; font-size:12px;"></asp:TextBox>
                            <asp:CheckBox ID="chkOtherDocs4" runat="server" />
                            </td>
                            <td>
                            <asp:TextBox ID="OtherDocs5" runat="server" style="width:129px; font-size:12px;"></asp:TextBox>
                            <asp:CheckBox ID="chkOtherDocs5" runat="server" />
                            </td>
                            <td>
                            <asp:TextBox ID="OtherDocs6" runat="server" style="width:129px; font-size:12px;"></asp:TextBox>
                            <asp:CheckBox ID="chkOtherDocs6" runat="server" />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr style="display:none;">
                            <td>
                            <asp:TextBox ID="OtherDocs7" runat="server" style="width:129px; font-size:12px;"></asp:TextBox>
                            <asp:CheckBox ID="chkOtherDocs7" runat="server" />
                            </td>
                            <td>
                            <asp:TextBox ID="OtherDocs8" runat="server" style="width:129px; font-size:12px;"></asp:TextBox>
                            <asp:CheckBox ID="chkOtherDocs8" runat="server" />
                            </td>
                            <td>
                            <asp:TextBox ID="OtherDocs9" runat="server" style="width:129px; font-size:12px;"></asp:TextBox>
                            <asp:CheckBox ID="chkOtherDocs9" runat="server" />
                            </td>
                            <td>
                            <asp:TextBox ID="OtherDocs10" runat="server" style="width:129px; font-size:12px;"></asp:TextBox>
                            <asp:CheckBox ID="chkOtherDocs10" runat="server" />
                            </td>
                            <td>
                            <asp:TextBox ID="OtherDocs11" runat="server" style="width:129px; font-size:12px;"></asp:TextBox>
                            <asp:CheckBox ID="chkOtherDocs11" runat="server" />
                            </td>
                            <td>
                            </td>
                            <td>
                            
                            </td>
                        </tr>
                        
                    </tr>
                  
                </table>
                <p />
                
                <table style="width: 955px; height: 60px; display:none;" id="ShippingInfo">
                    <tr>
                        <td style="width: 90px;">
                            Exporter
                        </td>
                        <td style="width: 210px;">
                            <asp:TextBox ID="txtExporter" runat="server" TextMode="MultiLine" CssClass="textarea"
                                Width="202px" Text=""></asp:TextBox>
                        </td>
                        <td style="width: 90px;">
                            Consignee
                        </td>
                        <td style="width: 210px;">
                            <asp:TextBox ID="txtConsignee" placeholder="Consignee" runat="server" TextMode="MultiLine"
                                CssClass="textarea" Width="202px"></asp:TextBox>
                        </td>
                        <td style="width: 90px;">
                            Buyer
                        </td>
                        <td style="width: 210px;">
                            <asp:TextBox ID="txtBuyer" runat="server" placeholder="Buyer" TextMode="MultiLine"
                                CssClass="textarea" Width="206px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div style="clear: both">
                </div>
                <%--<table  style="width:70%;">--%>
                <table style="display:none;">
                    <tr>
                       
                        <%--<td style="width: 136px;">--%>
                        <td>
                            <asp:Label ID="Label15" runat="server" Text="Status:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStatus" runat="server" require="Select Status" validate="SaveInvoice" style="width:128px;"> 
                            <asp:ListItem Text="Open" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Close" Value="1"></asp:ListItem>
                           
                        </asp:DropDownList>
                                
                        </td>
                        
                        <%--<td style="width: 159px;">--%>
                        <td>
                            <asp:Label ID="Label17" runat="server" Text="Cheque No:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtChequeNo" CssClass="sam_textbox" runat="server" Width="120px"></asp:TextBox>
                                
                        </td>
                        <td>
                            <asp:Label ID="Label16" runat="server" Text="Received Date:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtReceivedDate" runat="server" Width="120px"></asp:TextBox>
                        </td>
                   </tr>
            </table>
                <table style="width: 960px; height: 60px;" id="shiping">
                    <tr style="display:none;">
                        <td style="width: 90px; white-space: nowrap;">
                            ExportersRef
                        </td>
                        <td style="width: 210px;">
                            <asp:TextBox ID="txtExportersRef" runat="server" placeholder="ExportersRef" CssClass="sam_textbox"
                                Width="200px"></asp:TextBox>
                        </td>
                        <td style="width: 90px; white-space: nowrap;">
                            Form 'E' No :
                        </td>
                        <td style="width: 210px;">
                            <asp:TextBox ID="txtFormENo" runat="server" placeholder="Form E No" CssClass="sam_textbox"
                                Width="200px"></asp:TextBox>
                        </td>
                        <td style="width: 90px;">
                            Freight :
                        </td>
                        <td style="width: 210px; white-space: nowrap;">
                            <asp:TextBox ID="txtFreight" runat="server" placeholder="Freight" CssClass="sam_textbox"
                                Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display:none;">
                        <td style="width: 90px; white-space: nowrap;">
                            Net Weight :
                        </td>
                        <td style="width: 210px;">
                            <asp:TextBox ID="txtNetWeight" runat="server" placeholder="Net Weight" CssClass="sam_textbox decimalOnly"
                                Width="200px"></asp:TextBox>
                        </td>
                        <td style="width: 90px; white-space: nowrap;">
                            Gross Weight :
                        </td>
                        <td style="width: 210px;">
                            <asp:TextBox ID="txtGrossWeight" runat="server" placeholder="Gross Weight" CssClass="sam_textbox decimalOnly"
                                Width="200px"></asp:TextBox>
                        </td>
                        <td style="width: 90px; white-space: nowrap;">
                            Proforma No :
                        </td>
                        <td style="width: 210px;">
                            <asp:TextBox ID="txtproformaNo" runat="server" placeholder="Proforma No" CssClass="sam_textbox"
                                Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display:none;">
                        <td style="width: 90px;">
                            Insurance :
                        </td>
                        <td style="width: 210px;" colspan="3">
                            <asp:TextBox ID="txtInsurance" runat="server" placeholder="Insurance" CssClass="sam_textbox"
                                Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display:none;">
                        <td style="display: none;">
                            Invoice ID
                        </td>
                        <td>
                            Terms
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="txtTerm" runat="server" CssClass="sam_textbox" placeholder="Terms"
                                Width="97%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display:none;">
                        <td style="display: none;">
                            <asp:TextBox ID="txtInvoiceID" runat="server" CssClass="sam_textbox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="text-align: left; padding-left: 5px;">
                            Note
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:TextBox ID="txtNote" runat="server" style="height:50px; min-height:0;"
                                TextMode="MultiLine" MaxLength="100" CssClass="note"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:HiddenField ID="hdnMinDate" runat="server" />
                <asp:HiddenField ID="hdnMaxDate" runat="server" />
                <hr />
                <div class="file_uploader" style="display:none;">
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
