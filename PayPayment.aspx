<%@ Page Title="" Language="C#" MasterPageFile="Main.master" AutoEventWireup="true" CodeFile="PayPayment.aspx.cs" Inherits="PayPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        body
        {
           
        }
        .container
        {
             font-size:12px;
            font-family:arial;
            font-weight:bold;
          margin:0 auto;
          width:850px;
        }
        .top_heading
        {
            text-align:center;
        }
        select
        {
            border:01px solid #ccc;
            height:30px;
            width:250px;
        }
        input
        {
            height: 30px;
            line-height: 30px;
            border: outset 1px #CCC;
            font-size: 14px;
            border-image: initial;
        }
        .linkbutton
        {
            color: #3965ce;
            line-height: 34px;
        }
        .linkbutton:hover
        {
            color: #3965ce;
            text-decoration: underline;
            line-height: 34px;
            }
        .blance
        {
            text-align: right;
            font-size: 16px;
            font-weight: bold;
        }
        .float_right
        {
            float:right;
        }

        .amount
        {
            font-size:16px;
        }

        .textarea
        {
            width: 200px;
            height: 50px;
            position: relative;
            top: 8px;
        }
        .sam_textbox
        {
            width:160px;
            margin-right: 10px;
        }
        .btn_1 
        {
            height: 30px;
            width: 100px;
            line-height: 27px;
            background-color: #029FE2;
            border-radius: 4px;
            color: #EDF6E3;
            font-size:12px;
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
           float:right;
        }
        .subtotal table
        {
            width: 480px;
            text-align: right;
        }
        .subtotal table tr
        {
            line-height:40px;
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
            margin-bottom: 10px;
        }
        .file_uploader
        {
            margin-top:15px;
        }
    </style> 

    <script type="text/javascript">
        $(document).ready(function() {

            MyDate();
            ChangeDateEvent();
            // TotalSelectedInvoices();
            setInvoiceBalance();
            vale();
            TotalSelectedInvoices();
            GrossTotalPaid();
        });

        function MyDate() {
            dateMin = $("[id $= hdnMinDate]").val();
            dateMax = $("[id $= hdnMaxDate]").val();
            $(".DateTimePicker").datepicker({ minDate: new Date(dateMin), maxDate: new Date(dateMax) });

        }
        
        function GrossTotal() {
            grosstotal = 0;
            $("[id $= GV_PayPayment] .gv_txtAmount").each(function(index, item) {
                if ($(item).val() == "") { $(item).val(0); }


                var op_temp = $(item).closest('tr').find(".gv_lblOpBal").text();
                var amount_temp = $(item).closest('tr').find(".gv_txtAmount").val();
                var openingBalance = parseFloat(op_temp)
                var amount = parseFloat(amount_temp)
                if (amount <= openingBalance) {
                    grosstotal = grosstotal + parseFloat($(item).val());
                }
            });
            $("[id $= txtTotalAmount]").val(grosstotal.toFixed(3)).change();
        }

        function GrossTotalPaid() {
            $("[id $= GV_PayPayment] .gv_txtAmount").change(function() {
                GrossTotal();
            });
        }  

        function ChangeDateEvent() {

            $("[id $= txtPaymentDate]").change(function() {

                dateMin = $("[id $= hdnMinDate]").val();
                dateMax = $("[id $= hdnMaxDate]").val();
                var invoiceDate = $("[id$=txtPaymentDate]").val();


                if (Date.parse(invoiceDate) < Date.parse(dateMin) || Date.parse(invoiceDate) > Date.parse(dateMax)) {
                    $("[id$=txtPaymentDate]").val('');
                }


            });
        }

        function setInvoiceBalance() {

            $("[id $= chkSelect]").click(function() {

                var IsSelect = $(this).closest('tr').find("[id$=chkSelect]").is(":checked");

                if (IsSelect == true) {
                    var cc = $(this).closest('tr').find(".gv_lblOpBal").text();
                    var OpenBalance = parseInt(cc).toFixed(3);
                    $(this).closest('tr').find(".gv_txtAmount").val(OpenBalance);

                }

                else {
                    $(this).closest('tr').find(".gv_txtAmount").val('');
                }
                TotalSelectedInvoices();
                GrossTotal();


            });
        }


        function TotalSelectedInvoices() {
            gross = 0;
            $("[id*=GV_PayPayment] .gv_txtAmount").each(function(index, item) {
                if ($(item).text() == "") { $(item).text(0); }
                var IsSelect = $(this).parent().siblings().find("[id$=chkSelect]").is(":checked");
                if (IsSelect) {
                    gross = gross + parseFloat($(item).val());
                }
            });
            $("[id*=txtAmountPaid]").val(gross.toFixed(3));

        }
        
        
//         function TotalSelectedInvoices() {
//            gross = 0;
//            $("[id*=GV_PayPayment][id*=txtAmount]").each(function(index, item) {
//                if ($(item).text() == "") { $(item).text(0); }
//                var IsSelect = $(this).parent().siblings().find("[id$=chkSelect]").is(":checked");
//                if (IsSelect) {
//                    gross = gross + parseFloat($(item).val());
//                }
//            });
//            $("[id*=txtAmountPaid]").val(gross.toFixed(3));

//        }



//        function vale() {
//            $(".gv_txtAmount").blur(function() {


//                var needToPay = $(this).closest('tr').find("span[id*=lblOrigAmt]").text();
//                var cc = $(this).closest('tr').find("[id$=lblOpBal]").text();
//                var OpenBalance = parseInt(cc).toFixed(3);
//                var currentPayment = $(this).closest('tr').find(".gv_txtAmount").val();
//                if (currentPayment > OpenBalance) {

//                    $(this).closest('tr').find(".gv_txtAmount").val('');


//                }

//            });
//        }

        function vale() {
            $(".gv_txtAmount").blur(function() {
                var op_temp = $(this).closest('tr').find(".gv_lblOpBal").text();
                var amount_temp = $(this).closest('tr').find(".gv_txtAmount").val();
                var openingBalance = parseFloat(op_temp)
                var amount = parseFloat(amount_temp)
                if (amount > openingBalance) {
                    $(this).closest('tr').find(".gv_txtAmount").val('0');
                }
            });
        }
       
        
        
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:HiddenField ID="hdnMinDate" runat="server" />
       <asp:HiddenField ID="hdnMaxDate" runat="server" />
    <div class="Heading">
        <table style="width: 100%;">
            <tbody>
                <tr>
                    <td style="text-align: center;">
                        <b class="top_heading">PAY PAYMENT</b>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    
    <div class="container">
    <asp:UpdatePanel ID="Upd1" runat="server">
    <ContentTemplate>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate >
                <div id="processMessage">
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        
        <table style="width:100%;height: 60px;background-color: #EBF4FA;">
            <tr>
                <td style="width:260px;">
                    <asp:DropDownList ID="ddlVendor" validate="SavePayPayment" custom="Select Vendor" customFn="var goal = parseInt(this.value); return goal > 0;" runat="server" AutoPostBack="true"
                    onselectedindexchanged="ddlVendor_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        
        <div style="height:155px;">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="lblPayPayment_ID" Visible="false"  runat="server"></asp:Label>
                        Payment date
                    </td>
                </tr>
                <tr>
                    <td style="width:170px">
                        <asp:TextBox ID="txtPaymentDate" Width="160px" placeholder="Date" CssClass="DateTimePicker" runat="server" require="Select Payment Date" validate="SavePayPayment">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="display:none">
                        Payment method
                    </td>
                    <td>
                        Reference no.
                    </td>
                    <td>
                        Deposit to
                    </td>
                    <td>
                        Amount received
                    </td>
                </tr>
                <tr>
                    <td style="width: 210px; display:none">
                        <asp:DropDownList ID="ddlPaymentMethod" runat="server" style="width:200px;">
                            <asp:ListItem Value="0">Enter Text</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="txtReferenceNo"  placeholder="Reference No" runat="server" CssClass="sam_textbox"></asp:TextBox>
                    </td>
                    <td style="width: 210px;">
                        <asp:DropDownList ID="ddlDepositTo" validate="SavePayPayment" custom="Select Deposit Account" customFn="var goal = parseInt(this.value); return goal > 0;" runat="server" style="width:200px;">
                      
                        </asp:DropDownList>
                    </td>
                    <%--<td>
                        <asp:TextBox ID="txtAmountPaid" runat="server" CssClass="sam_textbox" 
                            ReadOnly="True"></asp:TextBox>
                    </td>--%>
                    <td>
                  
                <asp:TextBox ID="txtTotalAmount"  Enabled="false"  runat="server" CssClass="sam_textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="display:none;">
                        <asp:LinkButton CssClass="linkbutton" ID="lbtnPaymentOnline" runat="server" Text="Accept credit card payments online"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
        
        <div>
            <asp:Label ID="lblOT" Text="Outstanding Transactions" runat="server"></asp:Label>
           <asp:GridView ID="GV_PayPayment" Width="100%" runat="server" CssClass="data main"
           AutoGenerateColumns="False" style="float:left">
               <Columns>
               
                   <asp:TemplateField ItemStyle-Width="40px">
                       <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                       </ItemTemplate>
                   </asp:TemplateField>
               
                   <asp:TemplateField HeaderText="Description" ItemStyle-Width="341px">
                       <ItemTemplate>
                            <asp:LinkButton ID="lbtnInvoiceID" runat="server" Text='<%#Bind("pInvoiceID") %>'></asp:LinkButton>
                       </ItemTemplate>
                        <ItemStyle/>
                   </asp:TemplateField>
                   
                   <asp:TemplateField HeaderText="Due Date" ItemStyle-Width="110px">
                       <ItemTemplate>
                            <asp:Label ID="lblDueDate" runat="server" Text='<%#Bind("DueDate","{0:MM-dd-yyyy}") %>'></asp:Label>
                       </ItemTemplate>
                   </asp:TemplateField>
                   
                   <asp:TemplateField HeaderText="Original Amount" ItemStyle-Width="150px">
                       <ItemTemplate>
                            <asp:Label ID="lblOrigAmt" runat="server" Text='<%#Bind("Total") %>'></asp:Label>
                       </ItemTemplate>
                       <ItemStyle/>
                   </asp:TemplateField>
                   
                   <asp:TemplateField HeaderText="Open Balance" ItemStyle-Width="150px">
                       <ItemTemplate>
                            <asp:Label ID="lblOpBal" class="gv_lblOpBal" runat="server" Text='<%#Bind("Amount") %>'></asp:Label>
                       </ItemTemplate>
                         <ItemStyle/>
                   </asp:TemplateField>
                   
                   <asp:TemplateField HeaderText="Payment" ItemStyle-Width="150px">
                       <ItemTemplate>
                            <asp:TextBox ID="txtAmount" class="gv_txtAmount" Text='<%#Bind("Amt") %>' runat="server" ></asp:TextBox>
                       </ItemTemplate>
                       <ItemStyle/>
                   </asp:TemplateField>
                   
               </Columns>
           </asp:GridView>
           
        <div class="subtotal">
            <table style="display:none;">
                <tr>
                    <td style="width:320px">Amount to Apply</td>
                    <td style="width:160px">$0.00</td>
                </tr>
                 <tr>
                    <td>Amount to Credit</td>
                    <td>$0.00</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnClearPayment" Text="Clear Payment" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
       </div>
        
        <div></div>
        
        
        <div style="clear:both"></div>
        
        <div style="margin-bottom: 15px;">
            Memo  <br />
            <asp:TextBox ID="txtPrintMessage" runat="server" TextMode="MultiLine" CssClass="comment_box"></asp:TextBox> 
        </div>
        
        <hr />
        <br />
        
         <div id="Notification_Amount" style="display:none; width:98%; margin:auto;">
                <div class="alert-red">  
                    <h4>Error!</h4>
                        <label id="IdError" runat="server" style="color:White">Select Amount</label> 
                    </div>
                </div>
         
         <div id="Notification_Success" style="display:none; width:98%; margin:auto;">
             <div class="alert-green">
                 <h4>Success</h4>
                 <label id="lblSuccessMsg" runat="server" style="color:White">Payment Receive Successfully</label>
             </div>
         </div>
         
         <%--Error Message--%>
         <div id="Notification_Error" style="display:none; width:98%; margin:auto;">
             <div class="alert-red">
                 <h4>Error!</h4>
                 <label id="lblNewError" runat="server" style="color:White"></label> 
             </div>
         </div>
        
        <div>
            Attachment Maximum Size:25 MB  <br />
            <div class="file_uploader">
                <asp:FileUpload ID="fuAttachment" runat="server" />
                
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                CssClass="btn_1 btn_spacing float_right" onclick="btnCancel_Click"   />
                <asp:Button ID="btnSave" runat="server" Text="Save" 
                CssClass="btn_1 btn_spacing float_right" OnClientClick="return validate('SavePayPayment');" 
                     onclick="btnSave_Click"   />
            </div>    
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    
    
    
    
    
    
    
    
    </div>
</asp:Content>

