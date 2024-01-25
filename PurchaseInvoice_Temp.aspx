<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="PurchaseInvoice_Temp.aspx.cs" Inherits="PurchaseInvoice_Temp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
	        border-bottom-right-radius: 3px
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
            width: 250px;
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
            height:50px;
            position: relative;
            top: 8px;
        }
        .sam_textbox
        {
            width: 120px;
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
            margin-top:15px;
        }
        
        .subtotal_txt
        {
            float: right;
            max-width: 150px;
            border: 1px solid transparent !important;
            text-align: right;
            font-weight: bold;
        }
        .subtotal table tr
        {
            line-height:40px;
        }

        .subtotal table tr td div select
        {
            line-height:20px;
        }
        .float_right
        {
            float: right;
        }
    
         select .alertbox div
        {
           margin-top:-50px !important;
        }
               
        table.data td
        {
            font-size: 1.06em;
            border: none!important;
            color: #555;
            padding: 2px 2px !important;
            border-top: 1px solid #E6E6E6!important;
            border-left: 1px solid #F2F2F2!important;
            font-family: Verdana, Geneva, sans-serif;
            border-image: initial;
        }

        table.data {
        width: 100%;
        background: white;
        border: 2px solid #029FE2;
        font-size: 10px;
        }
        table.data th {
        font-size: 10px !important;
        }

        table.data input[type="text"] {
        width: 108px;
        line-height:20px;
        height:20px;
        }

        .data tr td:nth-child(8)
        {
            text-align:center;
            width:75px;
        }

    </style>

    <script type="text/javascript">
        $(document).ready(function() {
           MyDate();
        vale();
        GrossTotalDeduction();
        TotalGridAmount();
//        calculateSum();
        ChangeConversionRate();
            
        });

        function TxtBlur() {
            $(".txt").each(function() {
                $(this).blur(function() {
                    calculateSum();
                });
            });
        }
        
        function ChangeConversionRate() {
        $("[id $= txtConversionRate]").change(function() {

            GrandPKRTotals();

        });
    }

    function calculateSum() {
        var sum = 0;
        $(".sumition").each(function() {
            if (!isNaN(this.value) && this.value.length != 0) {
                sum += parseFloat(this.value);
            }
        });

        $("[id $= txttotal2]").val(sum.toFixed(2)).change();
    }
    
  
    function TotalGridAmount() {
        total = 0;
        GrandInvoiceTotal = 0;
        GrandTotal = 0;

        $("[id $= txttotal2],[id $= txtDiscount]").change(function() {
            GrandTotals();
            
        });
    }

    function GrandTotals() {
        var tot1 = $("[id $= txttotal2]").val();

        var discount = $("[id $= txtDiscount]").val();
        if(discount=="")
        {
        discount=0;
        }
        //   var dis = discount.toFixed(3);
        GrandInvoiceTotal = parseFloat(tot1 - discount);
        $("[id $= txtTot]").val(GrandInvoiceTotal.toFixed(2));
        GrandPKRTotals();
    }


    function GrandPKRTotals() {
        var tot1 = $("[id $= txtTot]").val();

        var ConversionRate = $("[id $= txtConversionRate]").val();
        var NetTotalAmount = parseFloat(tot1 * ConversionRate);
        //   var dis = discount.toFixed(3);
        //GrandInvoiceTotal = parseFloat(tot1 * ConversionRate);
        $("[id $= txtPKRTotal]").val(NetTotalAmount.toFixed(2));
    }

    function ChangeConversionRate() {
        $("[id $= txtConversionRate]").change(function() {

            GrandPKRTotals();

        });
    }
    
  

   

    function MyDate() {
        dateMin = $("[id $= hdnMinDate]").val();
        dateMax = $("[id $= hdnMaxDate]").val();
        $(".DateTimePicker").datepicker({ minDate: new Date(dateMin), maxDate: new Date(dateMax) });
    }


    function ChangeDateEvent() {
        $("[id $= txtInvoiceDate]").change(function() {
            dateMin = $("[id $= hdnMinDate]").val();
            dateMax = $("[id $= hdnMaxDate]").val();
            var invoiceDate = $("[id$=txtInvoiceDate]").val();
            if (Date.parse(invoiceDate) < Date.parse(dateMin) || Date.parse(invoiceDate) > Date.parse(dateMax)) {
                $("[id$=txtInvoiceDate]").val('');
            }
        });
    }

                      
         function vale() {
      
          $(".gv_txtQuantity").blur(function() {
               var quantity = $(this).val();
                var rate = $(this).parent().siblings().find(".gv_txtRate").val();
                var TotalPrice = parseFloat(quantity * rate);
                var Fixed = TotalPrice.toFixed(2);
                var Total = $(this).parent().siblings().find(".gv_txtAmount").val(Fixed);
                GrossTotalDeduction();
            });
           $(".gv_txtRate").blur(function() {
                var DeductionType = $(this).parent().siblings().find(".gv_txtDescription").val();
                var units = $(this).val();
                if (DeductionType == "Amount") {

                    var Total = $(this).parent().siblings().find(".gv_txtAmount").val(units);
                }
                else {
                    var quantity = $(this).parent().siblings().find(".gv_txtQuantity").val();
                    var TotalPrice = parseFloat(quantity * units);
                    var Fixed = TotalPrice.toFixed(2);
                    var Total = $(this).parent().siblings().find(".gv_txtAmount").val(Fixed);
                }
                GrossTotalDeduction();
            });
            
        }
        var gross;

        
         function GrossTotalDeduction() {
            grosstotal = 0;
            $(".gv_txtAmount").each(function(index, item) {
                if ($(item).val() == "") { $(item).val(0); }
                
                grosstotal = grosstotal + parseFloat($(item).val());
            });
            $("[id $= txttotal2]").val(grosstotal.toFixed(2)).change();
            
        }
        var grossdeduction; 
    var selectedRow = -1;
         
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    
    <asp:HiddenField ID="hdnMinDate" runat="server" />
       <asp:HiddenField ID="hdnMaxDate" runat="server" />
    <div class="Heading">
    <div id="StausMsg">
    </div>
        <table style="width: 100%;">
            <tbody>
                <tr>
                    <td style="text-align: center;">
                        <b class="top_heading">PURCHASE INVOICE</b>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="container">
        <asp:UpdatePanel ID="Upd1" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div id="processMessage">
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <table style="width: 100%; height: 60px; background-color: #EBF4FA;">
                    <tr>
                        <td style="width: 260px;">
                            <asp:DropDownList ID="ddlVendor" runat="server" validate="SaveInvoice" custom="Select Vendor Name"
                                customFn="var goal = parseInt(this.value); return goal > 0;">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail" placeholder="Email" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 150px;">
                            <span class="blance"></span>
                            <label class="amount">
                                </label>
                        </td>
                    </tr>
                </table>
                <br />
                <div style="height: 90px;">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                Billing address
                            </td>
                            <td>
                                Purchase ID
                            </td>
                            <td>
                                Invoice Number
                            </td>
                            <td>
                                Terms
                            </td>
                            <td>
                                Invoice date
                            </td>
                            <td>
                                Due date
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 210px">
                                <asp:TextBox ID="txtBillingAddress" runat="server" TextMode="MultiLine" placeholder="Billing Address" CssClass="textarea"> </asp:TextBox>
                            </td>
                            <td style="width: 170px">
                                <asp:TextBox ID="txtPurchaseID" runat="server" CssClass="sam_textbox"></asp:TextBox>
                            </td>
                            <td style="width: 170px">
                                <asp:TextBox ID="txtInvoiceNumber" runat="server" placeholder="Invoice Number" CssClass="sam_textbox"></asp:TextBox>
                            </td>
                            <td style="width: 170px">
                                <%--<asp:TextBox ID="txtTerms" runat="server" placeholder="Terms" CssClass="sam_textbox"></asp:TextBox>--%>
                               <asp:DropDownList  Style="width: 150px;" class="textfield" ID="DDLTerms" runat="server" validate="SaveInvoice" custom="Select Terms"
                                customFn="var goal = parseInt(this.value); return goal > 0;">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                               </asp:DropDownList>
                            </td>
                            <td style="width: 170px">
                                <asp:TextBox ID="txtInvoiceDate" CssClass="DateTimePicker sam_textbox" 
                                    placeholder="Invoice Date" runat="server" require="Select Invoice Date" 
                                    validate="SaveInvoice" ></asp:TextBox>
                            </td>
                            <td style="width: 170px">
                                <asp:TextBox ID="txtDueDate" runat="server" placeholder="Due Date" CssClass="DateTimePicker sam_textbox" require="Select Due Date" validate="SaveInvoice"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                 <div style="clear: both;"></div>    
    <div style="margin-bottom: 15px;">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GV_PurchaseInvoiceDetail" Width="100%" runat="server" CssClass="data main"
                                AutoGenerateColumns="False" OnRowDeleting="GV_PurchaseInvoiceDetail_RowDeleting"> 
                                <Columns>
                                   <asp:TemplateField HeaderText="Product" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="250px">
                                        <ItemTemplate>
                                           <%--<asp:TextBox ID="txtProduct" runat="server" AutoComplete="Off" CssClass="autoCompleteCodes" Width="250px" require="Enter Product" validate="SaveInvoice"  Text='<%#Bind("txtProduct") %>'></asp:TextBox>--%>
                                       <asp:DropDownList ID="ddlInventory" runat="server" Width="246px" >
                                             </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="250px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="114px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="gv_txtPackingSize autoCompleteCodes4" Width="140px" Text='<%#Bind("txtDescription") %>'
                                                ></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="114px" />
                                    </asp:TemplateField>
                                                                      
                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="114px">
                                        <ItemTemplate>
                                            <%--<asp:TextBox ID="txtQuantity" runat="server" ReadOnly="true" CssClass="gv_txtQuantity SmallTextbox decimalOnly" Text='<%#Bind("txtQuantity")%>' /> </asp:TextBox>--%>
 
                                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="gv_txtQuantity SmallTextbox" Width="110px" require="Enter Qauntity" validate="SaveInvoice" Text='<%#Bind("txtQuantity") %>'></asp:TextBox>   
                                        </ItemTemplate>
                                        <ItemStyle Width="114px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate" ItemStyle-Width="114px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRate" runat="server" CssClass="gv_txtRate SmallTextbox decimalOnly" Width="110px" require="Enter Unit Price" validate="SaveInvoice" Text='<%#Bind("txtRate")%>' ></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="114px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" ItemStyle-Width="114px">
                                        <ItemTemplate>
                                            <input id="txtAmount"  runat="server" class="gv_txtAmount SmallTextbox" ReadOnly="true" style="width: 110px;" value='<%#Bind("txtAmount") %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="114px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cost Center" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                           <%--<asp:TextBox ID="txtProduct" runat="server" AutoComplete="Off" CssClass="autoCompleteCodes" Width="250px" require="Enter Product" validate="SaveInvoice"  Text='<%#Bind("txtProduct") %>'></asp:TextBox>--%>
                                       <asp:DropDownList ID="ddlCostCenter" runat="server" Width="146px" >
                                             </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="150px" />
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
                    <asp:Button Visible="false" ID="btnClearAllLines" runat="server" Text="Clear all lines" CssClass="btn_1"
                        OnClick="btnClearAllLines_Click" />
                </div>
        
         <div class="subtotal">
                    <table>
                        <tr>
                            <td style="width: 320px">
                                Total
                            </td>
                            <td>
                                <input id="txttotal2" readonly="true" runat="server" class="subtotal_txt" />
                            </td>
                        </tr>
                        </table>
                 </div>  
             
 
    <div style="clear: both;"></div>    
   <div style="margin-bottom:10px;">  
       
           
            <asp:UpdatePanel ID="updpnlbtn" runat="server">
                <ContentTemplate>                
                    <table style="width: 950px;"> 
                        <tr>
                            <td style="text-align:right">
                               <asp:Button ID="btnCancel" runat="server" Text="Cancel" onclick="btnCancel_Click"
                    CssClass="btn_1 btn_spacing float_right"  />
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn_1 btn_spacing float_right" onclick="btnSave_Click"
                 OnClientClick="return validate('SaveInvoice');" />
                              </td>
                        </tr>
                    </table>
                    
            
          <div id="Notification_ItemID" style="display:none; width:98%; margin:auto;">
                <div class="alert-red">
                    <h4>Error!</h4>
                    <label id="IdError" runat="server" style="color:White">Add Atleast One Record</label> 
                </div>
            </div>
        
      
            <div id="Notification_Success" style="display:none; width:98%; margin:auto;">
                <div class="alert-green">
                    <h4>Success</h4>
                    <label id="lblSuccessMsg" runat="server" style="color:White">Invoice Created Successfully</label>
                </div>
            </div>
  
            <div id="Notification_Error" style="display:none; width:98%; margin:auto;">
                <div class="alert-red">  
                    <h4>Error!</h4>
                        <label id="lblNewError" runat="server" style="color:White">Please Select Item</label> 
                    </div>
                </div>
                    
            </ContentTemplate>
            </asp:UpdatePanel>
            </div>        

       <div style="clear: both;"></div>    
        <div class="subtotal">
        <table>
            <tr>
                <td style="width:320px">Discount</td>
                <td>
                    <asp:TextBox ID="txtDiscount" CssClass="decimalOnly" runat="server" Width="100px" style="float:right; text-align:right;"></asp:TextBox> 
                </td>
                
            </tr>
            <tr>
                <td style="width:320px">Net Total</td>
                <td>
                <%--<asp:TextBox ID="txttotal2"  runat="server" class="subtotal_txt" ></asp:TextBox>--%>
                <%--<asp:TextBox ID="txtTot" Enabled="false" style="background-color:White" runat="server" class="subtotal_txt" ></asp:TextBox>--%>
                <input id="txtTot" readonly="true" runat="server" class="subtotal_txt" style="background-color:White" />
                </td>
            </tr>
            
             <tr style="display:none">
                    <td>
                                Net Total in PKR
                     </td>
                            <td style="width:320px">
                                <%--<asp:TextBox ID="txtPKRTotal" class="subtotal_txt" style="background-color: White;"  runat="server" Enabled="false"  ></asp:TextBox>--%>
                                <input id="txtPKRTotal" readonly="true" runat="server" class="subtotal_txt" style="background-color:White" />
                            </td>
                </tr>
                            
                            <tr style="display:none">
                                <td>
                                    <div style="float: right">
                                    </div>
                                    Discount
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDiscount2" runat="server" class="sam_textbox" 
                                        style="margin-left: 28px; text-align:right"></asp:TextBox>
                                </td>
                            </tr>
               
              </tr>
            
        </table>
        </div>
                
                
                <div style="clear: both">
                </div>
                <div style="margin-top: 0px; margin-bottom: 15px;">
                    Print message to customer
                    <br />
                    <asp:TextBox ID="txtPrintMessage" runat="server" placeholder="Print message to customer" TextMode="MultiLine" CssClass="comment_box"></asp:TextBox>
                    <br />
                    Statement memo
                    <br />
                    <asp:TextBox ID="txtStatementMemo" runat="server" placeholder="Statement memo" TextMode="MultiLine" CssClass="comment_box"></asp:TextBox>
                </div>
                <hr />
                <br />
                <%--<div id="Notification_ItemID" style="display: none; width: 98%; margin: auto;">
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
                </div>--%>
                <%--Error Message--%>
                <%--<div id="Notification_Error" style="display: none; width: 98%; margin: auto;">
                    <div class="alert-red">
                        <h4>
                            Error!</h4>
                        <label id="lblNewError" runat="server" style="color: White">Please Select Item
                        </label>
                    </div>
                </div>--%>
                <div class="file_uploader">
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <%--<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn_1 btn_spacing float_right"
                    onclick="btnCancel_Click" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn_1 btn_spacing float_right"
                    OnClick="btnSave_Click" OnClientClick="return validate('SaveInvoice');" />--%>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
