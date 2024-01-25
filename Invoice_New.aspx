<%@ Page Title="" Language="C#" MasterPageFile="Main.master" AutoEventWireup="true" CodeFile="invoice_New.aspx.cs" Inherits="invoice_New" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script src="Script/jquery-1.6.2.min.js" type="text/javascript" charset="utf-8"></script>
 <script src="Script/jquery-ui-1.8.16.custom.min.js" type="text/javascript" charset="utf-8"></script>

<style type="text/css">
body
{
    font-size:12px;
    font-family:arial;
    font-weight:bold;
}
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

input {
height: 30px;
line-height: 30px;
border: outset 1px #CCC;
font-size: 14px;
border-image: initial;
width:250px;
}

.blance
{
    text-align: right;
    font-size: 16px;
    font-weight: bold;
}

.amount
{
    font-size:16px;
}

.textarea
{
    width: 190px;
    height: 50px;
    position: relative;
    top: 8px;
}

.sam_textbox
{
    width:120px;
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

.subtotal table tr td div select
{
    line-height:20px;
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
    border:1px solid transparent !important;
    text-align: right;
    font-weight:bold;
}

.float_right
{
    float:right;
}
select .alertbox div
       {
           margin-top:-50px !important;
       }
</style>

<script type="text/javascript">
    $(document).ready(function() {
        $("#Mytab").tabs();
        $("input[id$='btnaddlines2']").click(function() {
        
            
        });
        $("input[id$='btnaddnewRow']").click(function() {
        $("#tabs").tabs("option", "active", $("#tabs-2").tabs('option', 'active') + 1);
          
        });
        vale();
        MyDate();
        GrossTotalDeduction();
        ChangeDateEvent();
  
    });

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
        $("[id $= txtQuantity]").blur(function() {
            var quantity = $(this).val();
            var rate = $(this).parent().siblings().find("[id $= txtRate]").val();
            var TotalPrice = parseFloat(quantity) * parseFloat(rate);
            var Fixed = TotalPrice.toFixed(3);
            var Total = $(this).parent().siblings().find("[id $= txtAmount]").val(Fixed);
            GrossTotalDeduction();
        });
        $("[id $= txtRate]").blur(function() {
            var DeductionType = $(this).parent().siblings().find("[id $= txtDescription]").val();
            var units = $(this).val();
            if (DeductionType == "Amount") {

                var Total = $(this).parent().siblings().find("[id $= txtAmount]").val(units);
            }
            else {
                var quantity = $(this).parent().siblings().find("[id $= txtQuantity]").val();
                var TotalPrice = parseFloat(quantity) * parseFloat(units);
                var Fixed = TotalPrice.toFixed(3);
                var Total = $(this).parent().siblings().find("[id $= txtAmount]").val(Fixed);
            }
            GrossTotalDeduction();
        });
    }

    var gross;


    function GrossTotalDeduction() {
        grosstotal = 0;
        $("[id $= GV_InvoiceDetail] [id $= txtAmount]").each(function(index, item) {
            if ($(item).val() == "") { $(item).val(0); }
            grosstotal = grosstotal + parseFloat($(item).val());
        });
        $("[id $= txttotal2]").val(grosstotal.toFixed(3)).change();
    }

    var grossdeduction;


   

    
</script>

   


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="Heading">
        <table style="width: 100%;">
            <tbody>
            <tr>
                <td style="text-align: center;">
                    <b class="top_heading">COMMERCIAL INVOICE</b>
                </td>
            </tr>
        </tbody></table>
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
            
            <td style="width:48px;">
                  Sold To :
                </td>
            
            
            
            
                <td style="width:260px;">
                  <asp:DropDownList runat="server" CssClass="input" id="ddlCustomer" AutoPostBack="true"
         validate="SaveInvoice" custom="Select Customer Name" 
                        customFn="var goal = parseInt(this.value); return goal > 0;" 
                        onselectedindexchanged="ddlCustomer_SelectedIndexChanged" >        
       
        </asp:DropDownList>  
                </td>
                 <td style="width:48px;">
                  Email :
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                </td>
                <td style="width:150px;">
                <span class="blance">BALANCE DUE</span>
                <label class="amount">$0.00</label>
                </td>
            </tr>
            
        </table>
        <br />
        <div style="height:90px;">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                Billing address
                </td>
                <td style="display:none;">	
                    Invoice ID
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
                <td style="width:200px">
                    <asp:TextBox ID="txtBillingAddress" runat="server" TextMode="MultiLine" CssClass="textarea"></asp:TextBox>
                </td>
                <td  style="display:none;">
                    <asp:TextBox ID="txtInvoiceID" runat="server" CssClass="sam_textbox"></asp:TextBox>
                </td>
                <td style="width:130px">
                    <asp:TextBox ID="txtInvoiceNumber" runat="server" CssClass="sam_textbox"></asp:TextBox>
                </td>
                <td style="width:130px">
                    <asp:TextBox ID="txtTerm" runat="server" CssClass="sam_textbox decimalOnly"></asp:TextBox>
                </td>
                <td style="width:130px">
                    <asp:TextBox ID="txtInvoiceDate" CssClass="DateTimePicker" Width="120px" runat="server" ></asp:TextBox>
                </td>
                <td style="width:130px">
                    <asp:TextBox ID="txtDueDate"  CssClass="DateTimePicker" runat="server" Width="120px"></asp:TextBox>
                </td>
            </tr>
        </table>
        </div>
        
        
        
        
        
          <div style="height:60px;">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
             From
                </td>
                <td>	
                    To   
                </td>
                <td>	
                   Vessel
                </td>
               
            </tr>
            <tr>
                <td style="width:200px">
                  <asp:TextBox ID="txtFrom" Width="188px"  runat="server" ReadOnly="true" Text="KARACHI-PAKISTAN" CssClass="sam_textbox"></asp:TextBox>
                   
                </td>
                <td style="width:130px">
                 <asp:TextBox ID="txtTo" runat="server" CssClass="sam_textbox"></asp:TextBox>
                
                </td>
                <td style="width:130px">
                 <asp:TextBox ID="txtVessel" runat="server"  CssClass="sam_textbox"></asp:TextBox>
                  
                </td>
               
            </tr>
        </table>
        </div>
        
        <div style="margin-bottom:15px;width: 1130px;">
       
                <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                        
                            <asp:GridView ID="GV_InvoiceDetail" Width="100%" runat="server" CssClass="data main"
                            AutoGenerateColumns="False" onrowdeleting="GV_InvoiceDetail_RowDeleting" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Product" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="100%">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlItem" runat="server" AutoPostBack="true"></asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="100%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="114px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="SmallTextbox"  Text='<%#Bind("txtDescription") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="114px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Net Weight (KGS)" ItemStyle-Width="114px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="SmallTextbox decimalOnly"  Text='<%#Bind("txtQuantity") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="114px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Price" ItemStyle-Width="114px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRate" runat="server" CssClass="SmallTextbox decimalOnly" Text='<%#Eval("txtRate") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="114px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" ItemStyle-Width="114px">
                                        <ItemTemplate>
                                            <input ID="txtAmount" ReadOnly="true"  runat="server" Class="SmallTextbox" value='<%#Bind("txtAmount") %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="114px" />
                                    </asp:TemplateField>
                                
                                 <asp:CommandField ShowDeleteButton="true" />
                                    
                                </Columns>
                            </asp:GridView>
                            
                            
                          
                                    
                             
                        </ContentTemplate>
                 </asp:UpdatePanel>
            --%>
        
       <div id="Mytab" class="block-fluid tab divBorder" style="">
            <ul>
                 <li><a href="#tabs-1">Invoice Tab 1</a></li>
                 <li><a href="#tabs-2">Invoice Tab 2</a></li>
                 
            </ul>
            <div id="tabs-1">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                         <div class="block-fluid" style="float: left;overflow: hidden;width: 100%;">
                   
  
                            
                             <asp:GridView ID="GV_InvoiceDetail" runat="server" AutoGenerateColumns="False" 
                                 CssClass="data main" onrowdeleting="GV_InvoiceDetail_RowDeleting" Width="100%">
                                 <Columns>
                                     
                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Fish Name" 
                                         ItemStyle-Width="100%">
                                         <ItemTemplate>
                                             <asp:DropDownList ID="ddlFish" runat="server" Width="146px">
                                             </asp:DropDownList>
                                         </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Left" />
                                         <ItemStyle Width="100%" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Fish Grade" 
                                         ItemStyle-Width="100%">
                                         <ItemTemplate>
                                             <asp:DropDownList ID="ddlFishGrade" runat="server" Width="146px">
                                             </asp:DropDownList>
                                         </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Left" />
                                         <ItemStyle Width="100%" />
                                     </asp:TemplateField>
                                       <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Fish Size" 
                                         ItemStyle-Width="100%">
                                         <ItemTemplate>
                                             <asp:DropDownList ID="ddlFishSize" runat="server" Width="146px">
                                             </asp:DropDownList>
                                         </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Left" />
                                         <ItemStyle Width="100%" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Description" ItemStyle-Width="114px">
                                         <ItemTemplate>
                                             <asp:TextBox ID="txtDescription" runat="server" CssClass="sam_textbox" 
                                                 Text='<%#Bind("txtDescription") %>'></asp:TextBox>
                                         </ItemTemplate>
                                         <ItemStyle Width="114px" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Net Weight (KGS)" ItemStyle-Width="114px">
                                         <ItemTemplate>
                                             <asp:TextBox ID="txtQuantity" runat="server" 
                                                 CssClass="sam_textbox decimalOnly" Text='<%#Bind("txtQuantity") %>'></asp:TextBox>
                                         </ItemTemplate>
                                         <ItemStyle Width="114px" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Unit Price" ItemStyle-Width="114px">
                                         <ItemTemplate>
                                             <asp:TextBox ID="txtRate" runat="server" CssClass="sam_textbox decimalOnly" 
                                                 Text='<%#Eval("txtRate") %>'></asp:TextBox>
                                         </ItemTemplate>
                                         <ItemStyle Width="114px" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Amount" ItemStyle-Width="114px">
                                         <ItemTemplate>
                                             <input ID="txtAmount" runat="server" Class="sam_textbox" ReadOnly="true" 
                                                 value='<%#Bind("txtAmount") %>' />
                                         </ItemTemplate>
                                         <ItemStyle Width="114px" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="GridName" ItemStyle-Width="114px" 
                                         Visible="false">
                                         <ItemTemplate>
                                             <asp:Label ID="txtGridName" runat="server" Text="Grid1"></asp:Label>
                                         </ItemTemplate>
                                         <ItemStyle Width="114px" />
                                     </asp:TemplateField>
                                     <asp:CommandField ShowDeleteButton="true" />
                                 </Columns>
                             </asp:GridView>
                      </div>
                      
                    <asp:Button ID="btnaddnewRow" runat="server" Text="Add lines" CssClass="btn_1 btn_spacing" 
            onclick="btnaddnewRow_Click"  />
            <asp:Button ID="Button2" runat="server" Text="Clear all lines" CssClass="btn_1" 
                onclick="Button2_Click" /></ContentTemplate>
                </asp:UpdatePanel></div>
                            
                          
                           <div id="tabs-2">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                         <div class="block-fluid" style="float: left;overflow: hidden;width: 100%;">
                          
                          
                            
                             <asp:GridView ID="GV_InvoiceDetail1" runat="server" AutoGenerateColumns="False" 
                                 CssClass="data main" onrowdeleting="GV_InvoiceDetail1_RowDeleting" 
                                 onselectedindexchanged="GV_InvoiceDetail1_SelectedIndexChanged" Width="100%">
                                 <Columns>
                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Fish Name" 
                                         ItemStyle-Width="100%">
                                         <ItemTemplate>
                                             <asp:DropDownList ID="ddlFish" runat="server" Width="146px">
                                             </asp:DropDownList>
                                         </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Left" />
                                         <ItemStyle Width="100%" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Fish Grade" 
                                         ItemStyle-Width="100%">
                                         <ItemTemplate>
                                             <asp:DropDownList ID="ddlFishGrade" runat="server" Width="146px">
                                             </asp:DropDownList>
                                         </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Left" />
                                         <ItemStyle Width="100%" />
                                     </asp:TemplateField>
                                       <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Fish Size" 
                                         ItemStyle-Width="100%">
                                         <ItemTemplate>
                                             <asp:DropDownList ID="ddlFishSize" runat="server" Width="146px">
                                             </asp:DropDownList>
                                         </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Left" />
                                         <ItemStyle Width="100%" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Description" ItemStyle-Width="114px">
                                         <ItemTemplate>
                                             <asp:TextBox ID="txtDescription" runat="server" CssClass="SmallTextbox" 
                                                 Text='<%#Bind("txtDescription") %>'></asp:TextBox>
                                         </ItemTemplate>
                                         <ItemStyle Width="114px" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Net Weight (KGS)" ItemStyle-Width="114px">
                                         <ItemTemplate>
                                             <asp:TextBox ID="txtQuantity" runat="server" 
                                                 CssClass="SmallTextbox decimalOnly" Text='<%#Bind("txtQuantity") %>'></asp:TextBox>
                                         </ItemTemplate>
                                         <ItemStyle Width="114px" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Unit Price" ItemStyle-Width="114px">
                                         <ItemTemplate>
                                             <asp:TextBox ID="txtRate" runat="server" CssClass="SmallTextbox decimalOnly" 
                                                 Text='<%#Eval("txtRate") %>'></asp:TextBox>
                                         </ItemTemplate>
                                         <ItemStyle Width="114px" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Amount" ItemStyle-Width="114px">
                                         <ItemTemplate>
                                             <input ID="txtAmount" runat="server" Class="SmallTextbox" ReadOnly="true" 
                                                 value='<%#Bind("txtAmount") %>' />
                                         </ItemTemplate>
                                         <ItemStyle Width="114px" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="GridName" ItemStyle-Width="114px" 
                                         Visible="false">
                                         <ItemTemplate>
                                             <asp:Label ID="txtGridName" runat="server" Text="Grid2"></asp:Label>
                                         </ItemTemplate>
                                         <ItemStyle Width="114px" />
                                     </asp:TemplateField>
                                     <asp:CommandField ShowDeleteButton="true" />
                                 </Columns>
                             </asp:GridView>
                        </div>
                     <asp:Button ID="btnaddlines2" runat="server" Text="Add lines" CssClass="btn_1 btn_spacing" 
            onclick="btnaddlines2_Click"  />
            <asp:Button ID="btnClear2" runat="server" Text="Clear all lines" CssClass="btn_1" 
                onclick="btnClear2_Click" /></ContentTemplate>
                </asp:UpdatePanel>
                
               
            </div>
           
           
            <asp:UpdatePanel ID="updpnlbtn" runat="server">
                <ContentTemplate>
                    <table style="text-align:right; width:95%">
                       
                        <tr>
                            <td><span>Invoice Total:&nbsp;</span><input id="txtGrandInvoiceTotal" value="0" type="text" class="SmallTextbox" readonly="True" runat="server"/></td>
                        </tr>
                        <tr>
                            <td><span>Grand Total Invoice:&nbsp;</span><input id="txtGrandTotal" value="0" type="text" class="SmallTextbox" readonly="True" runat="server"/></td>
                        </tr>
                        <tr>
                            <td style="text-align:right">
                               <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                    CssClass="btn_1 btn_spacing float_right" onclick="btnCancel_Click" />
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn_1 btn_spacing float_right"
                onclick="btnSave_Click" OnClientClick="return validate('SaveInvoice');" />
                              </td>
                        </tr>
                    </table>
                    
            
          <div id="Notification_ItemID" style="display:none; width:98%; margin:auto;">
                <div class="alert-red">
                    <h4>Error!</h4>
                    <label id="IdError" runat="server" style="color:White">Select ProductID</label> 
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
                        <label id="lblNewError" runat="server" style="color:White"></label> 
                    </div>
                </div>

                    
                </ContentTemplate>
            </asp:UpdatePanel>
            </div>
        
        </div>
       
        
      <%--  <div>
         <asp:Button ID="Button1" runat="server" Text="Add Group" 
                CssClass="btn_1 btn_spacing" onclick="Button1_Click" 
              />
            <asp:Button ID="btnaddnewRow" runat="server" Text="Add lines" CssClass="btn_1 btn_spacing" 
            onclick="btnaddnewRow_Click"  />
            <asp:Button ID="Button2" runat="server" Text="Clear all lines" CssClass="btn_1" 
                onclick="Button2_Click" />
        </div>--%>
        
        
        
        
        <div class="subtotal">
        <table>
            <tr>
                <td style="width:320px">Subtotal</td>
                <td><input ID="txttotal2"  runat="server" class="subtotal_txt" /></td>
            </tr>
             <tr>
                <td>
                    <div style="float:right;line-height: 10px;">
                    <asp:DropDownList runat="server" CssClass="input" id="ddlDiscount"
         validate="SaveInvoice" custom="Select Discount" customFn="var goal = parseInt(this.value); return goal > 0;" >        
       
        </asp:DropDownList>
                    <asp:TextBox ID="txtDiscount" CssClass="decimalOnly" runat="server" Width="50px" style="height: 28px;line-height: 20px;"></asp:TextBox>
                    </div>
                </td>
                <td>$0.00</td>
            </tr>
            <tr>
                <td>Total</td>
                <td>$0.00</td>
            </tr>
             <tr>
                <td>Balance due</td>
                <td>$0.00</td>
            </tr>
        </table>
        </div>
        <div style="clear:both"></div>
        <div>The above goods are shipped under T/T Basis.</div>
        <br />
         <table style="width:50%;height: 60px;">
            <tr>
            
            <td style="width:20px;">
               Form E No :
                </td>
            
            
            
            
                <td style="width:210px;">
                 <asp:TextBox ID="txtFormENo" runat="server"  CssClass="sam_textbox"></asp:TextBox>
                </td>
                </tr>
                <tr>
                 <td style="width:20px;">
                 Freight :
                </td>
                <td>
                    <asp:TextBox ID="txtFreight" runat="server" CssClass="sam_textbox"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                 <td style="width:20px;">
                 Net Weight :
                </td>
                <td>
                    <asp:TextBox ID="txtNetWeight" runat="server" CssClass="sam_textbox decimalOnly"></asp:TextBox>
                </td>
                
            </tr>
             <tr>
                 <td style="width:20px;">
                 Gross Weight :
                </td>
                <td>
                    <asp:TextBox ID="txtGrossWeight" runat="server" CssClass="sam_textbox decimalOnly"></asp:TextBox>
                </td>
                
            </tr>
             <tr>
                 <td style="width:20px;">
                 Container No :
                </td>
                <td>
                    <asp:TextBox ID="txtContainerNo" runat="server" CssClass="sam_textbox"></asp:TextBox>
                </td>
                
            </tr>
             <tr>
                 <td style="width:20px;">
                 Proforma No :
                </td>
                <td>
                    <asp:TextBox ID="txtproformaNo" runat="server" CssClass="sam_textbox"></asp:TextBox>
                </td>
                
            </tr>
             <tr>
                 <td style="width:20px;">
                 Insurance :
                </td>
                <td>
                    <asp:TextBox ID="txtInsurance" runat="server" CssClass="sam_textbox"></asp:TextBox>
                </td>
                
            </tr>
        </table>
        <br />
       
           <asp:HiddenField ID="hdnMinDate" runat="server" />
           <asp:HiddenField ID="hdnMaxDate" runat="server" />
                <hr />
                <br />
                
           
            <div class="file_uploader">
                <asp:FileUpload ID="FileUpload1" runat="server" />
               
            </div>
         </ContentTemplate>
         </asp:UpdatePanel>
    </div>
     
</asp:Content>


