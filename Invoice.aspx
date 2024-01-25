<%@ Page Title="" Language="C#" MasterPageFile="Main.master" AutoEventWireup="true" CodeFile="invoice.aspx.cs" Inherits="invoice" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="Script/jquery-1.6.2.min.js" type="text/javascript" charset="utf-8"></script>
<script src="Script/jquery-ui-1.8.16.custom.min.js" type="text/javascript" charset="utf-8"></script>

<script type="text/javascript">
    $(document).ready(function() {
//        $("input[id$='btnaddlines2']").click(function() {
//        });
        TxtBlur();
        setTabHref();
        vale();
        vale2();
        MyDate();
        GrossTotalDeduction();
        GrossTotalDeduction2();
        ChangeDateEvent();
    });

    $(function() {
        setTabs();
    });


    function TxtBlur() {
        $(".txt").each(function() {
            $(this).blur(function() {
                calculateSum();
            });
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

    function setTabs() {
        var tid = $('2').val();
        if (tid == 1) {
            $("#tabs").tabs({
                enabled: [1, 2],
                selected: 0
            });
        }
        else {
            $("#tabs").tabs();
            setTabHref();
        }
    }
   
    function setTabHref() {
        var a = $("[id$=hfSelectedTAB]").val();
        if (a == 1) {
            $('#tabs a[href="#tabs-1"]').click();
        }
        else if (a == 2) {
            $('#tabs a[href="#tabs-2"]').click();
        }                                     
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
        //$("[id $= txttotal2]").val(grosstotal.toFixed(3)).change();
        $("[id $= txtGrandInvoiceTotal]").val(grosstotal.toFixed(3)).change();
    }
    var grossdeduction;
    
    function vale2() {
        $("[id $= txtQuantity]").blur(function() {
            var quantity2 = $(this).val();
            var rate2 = $(this).parent().siblings().find("[id $= txtRate]").val();
            var TotalPrice2 = parseFloat(quantity2) * parseFloat(rate2);
            var Fixed2 = TotalPrice2.toFixed(3);
            var Total2 = $(this).parent().siblings().find("[id $= txtAmount]").val(Fixed2);
            GrossTotalDeduction2();
        });

        $("[id $= txtRate]").blur(function() {
            var DeductionType2 = $(this).parent().siblings().find("[id $= txtDescription]").val();
            var units2 = $(this).val();
            if (DeductionType2 == "Amount") {

                var Total2 = $(this).parent().siblings().find("[id $= txtAmount]").val(units2);
            }
            else {
                var quantity2 = $(this).parent().siblings().find("[id $= txtQuantity]").val();
                var TotalPrice2 = parseFloat(quantity2) * parseFloat(units2);
                var Fixed2 = TotalPrice2.toFixed(3);
                var Total2 = $(this).parent().siblings().find("[id $= txtAmount]").val(Fixed2);
            }
            GrossTotalDeduction2();
        });
    }
    var gross2;

    function GrossTotalDeduction2() {
        grosstotal2 = 0;
        $("[id $= GV_InvoiceDetail1] [id $= txtAmount]").each(function(index, item) {
            if ($(item).val() == "") { $(item).val(0); }
            grosstotal2 = grosstotal2 + parseFloat($(item).val());
        });
        //$("[id $= txttotal2]").val(grosstotal.toFixed(3)).change();
        $("[id $= txtGrandInvoiceTotal2]").val(grosstotal2.toFixed(3)).change();
    }      
    var grossdeduction2;
    
    
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
	border-bottom-right-radius: 3px
}

.container
{
  margin:0 auto;
  width:960px;
  font-size:12px;
    font-family:arial;
    font-weight:bold;
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
    width:250px;
}

.ui-tabs .ui-tabs-panel
{
    display: block;
    border-width: 0;
    padding: 1em 0.4em;
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
.note
{
    width:930px;
    min-width:930px;
    max-width:930px;
    padding-left:10px;
    height:130px;
    min-height:130px;
    max-height:130px;
    
}
</style>
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
        <table cellpadding="0" cellspacing="0" style="float:left;">
            <tr>
                <td>
                 Port of Loading	
                </td>
                <td>	
                    Port of Dischang    
                </td>
                <td>	
                   ContainerNo
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
                 <asp:TextBox ID="txtContainerNo" runat="server"  CssClass="sam_textbox"></asp:TextBox>
                  
                </td>
               
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" style="float:left; margin-left:10px;">
            <tr>
                <td>
                 Origin Country	
                </td>
                <td>	
                  Dest: Country    
                </td>
                <td>	
                   Vessel
                </td>
               
            </tr>
            <tr>
                <td style="width:200px">
                  <asp:TextBox ID="txtOrCountry" Width="188px"  runat="server" ReadOnly="true" Text="PAKISTAN" CssClass="sam_textbox"></asp:TextBox>
                   
                </td>
                <td style="width:130px">
                 <asp:TextBox ID="txtDestCountry" runat="server" CssClass="sam_textbox"></asp:TextBox>
                
                </td>
                <td style="width:130px">
                 <asp:TextBox ID="txtVessel" runat="server"  CssClass="sam_textbox"></asp:TextBox>
                  
                </td>
               
            </tr>
        </table>
        </div>
        
        <div style="margin-bottom:10px;">    
                
        
       <div id="Mytab" class="block-fluid tab divBorder" style="">
            <ul>
                 <asp:HiddenField ID="hidtab" Value="0" runat="server" />
                 <li><a href="#tabs-1">Invoice Tab 1</a></li>
                 <li ><a href="#tabs-2">Invoice Tab 2</a></li>
                 
            </ul>
            
            <div id="tabs-1">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                         <div class="block-fluid" style="float: left;overflow: hidden;width: 100%;border: transparent;">  
                         <asp:HiddenField ID="hf2g2" runat="server" />
                          <asp:HiddenField ID="hf1g2" runat="server" />                      
                             <asp:GridView ID="GV_InvoiceDetail" runat="server" AutoGenerateColumns="False" 
                                 CssClass="data main" onrowdeleting="GV_InvoiceDetail_RowDeleting" Width="100%">
                                 <Columns>                                     
                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Fish Name" 
                                         >
                                         <ItemTemplate>
                                         <asp:DropDownList ID="ddlFish" runat="server" AutoPostBack="True" DataTextField="EventName"
                                                 DataValueField="EventID"  
                                        onselectedindexchanged="ddlFish_SelectedIndexChanged1" Width="123px">
                                        </asp:DropDownList>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Left" />
                                         <ItemStyle Width="127px" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Fish Grade" 
                                         >
                                         <ItemTemplate>
                                             <asp:DropDownList ID="ddlFishGrade" runat="server"
                                              
                                              Width="123px" 
                                                 AutoPostBack="True" onselectedindexchanged="ddlFishGrade_SelectedIndexChanged1">
                                             </asp:DropDownList>
                                         </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Left" />
                                         <ItemStyle Width="127px" />
                                     </asp:TemplateField>
                                       <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Fish Size" 
                                         ItemStyle-Width="100%">
                                         <ItemTemplate>
                                             <asp:DropDownList ID="ddlFishSize"
                                               
                                              runat="server" Width="123px">
                                             </asp:DropDownList>
                                         </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Left" />
                                         <ItemStyle Width="127px" />
                                     </asp:TemplateField>                                     
                                     <asp:TemplateField HeaderText="Description" ItemStyle-Width="100px">
                                         <ItemTemplate>
                                             <asp:TextBox ID="txtDescription" runat="server" CssClass="sam_textbox" 
                                                 Text='<%#Bind("txtDescription") %>'></asp:TextBox>
                                         </ItemTemplate>
                                         <ItemStyle Width="100px" />
                                     </asp:TemplateField>
                                     
                                     <asp:TemplateField HeaderText="Net Weight (KGS)" ItemStyle-Width="100px">
                                         <ItemTemplate>
                                             <asp:TextBox ID="txtQuantity" runat="server" 
                                                 CssClass="sam_textbox decimalOnly" Text='<%#Bind("txtQuantity") %>'></asp:TextBox>
                                         </ItemTemplate>
                                         <ItemStyle Width="100px" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Unit Price" ItemStyle-Width="100px">
                                         <ItemTemplate>
                                             <asp:TextBox ID="txtRate" runat="server" CssClass="sam_textbox decimalOnly txt" 
                                                 Text='<%#Eval("txtRate") %>'></asp:TextBox>
                                         </ItemTemplate>
                                         <ItemStyle Width="114px" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Amount" ItemStyle-Width="114px">
                                         <ItemTemplate>
                                             <input ID="txtAmount" runat="server" Class="sam_textbox txt" ReadOnly="true" 
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
                                     <asp:TemplateField HeaderText="InventoryId" ItemStyle-Width="114px" 
                                         Visible="false">
                                         <ItemTemplate>
                                             <asp:Label ID="lblInventoryId" runat="server" Text=""></asp:Label>
                                         </ItemTemplate>
                                         <ItemStyle Width="114px" />
                                     </asp:TemplateField>
                                     <asp:CommandField ShowDeleteButton="true" />
                                 </Columns>
                             </asp:GridView>
                      </div>
            <div style="clear:both"></div>
            <div style="width: 250px;float: left;">
            <asp:Button ID="btnaddnewRow" runat="server" Text="Add lines" CssClass="btn_1 btn_spacing" 
            onclick="btnaddnewRow_Click"  />
            <asp:Button ID="btnadd" runat="server" Text="Add Tab" CssClass="btn_1 btn_spacing" 
      />
            <asp:Button ID="Button2" runat="server" Text="Clear all lines" CssClass="btn_1" 
                onclick="Button2_Click" />
             </div>          
                <table style="text-align: right;width: 43%;float: right;margin-bottom: 3px;">
                        <tr>
                            <td><span style="margin-left:163px;">Total :&nbsp;</span><input id="txtGrandInvoiceTotal" value="0" type="text" style="width:110px;" class="SmallTextbox sumition" readonly="True" runat="server"/></td>
                        </tr>
                        <tr>
                            <td style="display:none;"><span>Grand Total Invoice:&nbsp;</span><input id="txtGrandTotal" value="0" type="text" class="SmallTextbox" readonly="True" runat="server"/></td>
                        </tr>
                </table>
                
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
                            
                          
            <div id="tabs-2">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                         <div class="block-fluid" style="float: left;overflow: hidden;width: 100%;border: transparent;">                        
                          <asp:HiddenField ID="hf1g1" runat="server" />
                          <asp:HiddenField ID="hf2g1" runat="server" />                        
                          <asp:HiddenField ID="HfFishID" runat="server" />                          
                          <asp:HiddenField ID="HfFishGradeId" runat="server" />
                            
                             <asp:GridView ID="GV_InvoiceDetail1" runat="server" AutoGenerateColumns="False" 
                                 CssClass="data main" onrowdeleting="GV_InvoiceDetail1_RowDeleting" 
                                 Width="100%" >
                                 <Columns>
                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Fish Name" itemStyle-Width="114px">
                                         <ItemTemplate>
                                             <asp:DropDownList ID="ddlFish" runat="server" Width="123px" AutoPostBack="True" 
                                                  DataTextField="EventName"
                                                 DataValueField="EventID" 
                                             
                                                 onselectedindexchanged="ddlFish_SelectedIndexChanged">
                                             </asp:DropDownList>
                                         </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Left" />
                                         <ItemStyle Width="114px" />
                                     </asp:TemplateField>
                                     
                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Fish Grade" 
                                         ItemStyle-Width="114px">
                                         <ItemTemplate>
                                             <asp:DropDownList ID="ddlFishGrade" runat="server"  Width="123px" 
                                              
                                             
                                                 AutoPostBack="True" onselectedindexchanged="ddlFishGrade_SelectedIndexChanged">
                                             </asp:DropDownList>
                                         </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Left" />
                                         <ItemStyle Width="114px" />
                                     </asp:TemplateField>
                                     
                                       <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Fish Size" 
                                         ItemStyle-Width="100%">
                                         <ItemTemplate>
                                             <asp:DropDownList ID="ddlFishSize"
                                            
                                              runat="server" Width="123px">
                                             </asp:DropDownList>
                                         </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Left" />
                                         <ItemStyle Width="114px" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Description" ItemStyle-Width="100px">
                                         <ItemTemplate>
                                             <asp:TextBox ID="txtDescription" runat="server" CssClass="SmallTextbox" 
                                                 Text='<%#Bind("txtDescription") %>'></asp:TextBox>
                                         </ItemTemplate>
                                         <ItemStyle Width="100px" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Net Weight (KGS)" ItemStyle-Width="100px">
                                         <ItemTemplate>
                                             <asp:TextBox ID="txtQuantity" runat="server" 
                                                 CssClass="SmallTextbox decimalOnly txt" Text='<%#Bind("txtQuantity") %>'></asp:TextBox>
                                         </ItemTemplate>
                                         <ItemStyle Width="100px" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Unit Price" ItemStyle-Width="114px">
                                         <ItemTemplate>
                                             <asp:TextBox ID="txtRate" runat="server" CssClass="SmallTextbox decimalOnly txt" 
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
                                     
                                    <asp:TemplateField HeaderText="lblInventory" ItemStyle-Width="114px" 
                                         Visible="false">
                                         <ItemTemplate>
                                             <asp:Label ID="lblInventoryId" runat="server" Text=""></asp:Label>
                                         </ItemTemplate>
                                         <ItemStyle Width="114px" />
                                     </asp:TemplateField>
                                     <asp:CommandField ShowDeleteButton="true" />
                                 </Columns>
                             </asp:GridView>
                        </div>
             <div style="clear:both"></div>
            <div style="width: 250px;float: left;">
            <asp:Button ID="btnaddlines2" runat="server" Text="Add lines" CssClass="btn_1 btn_spacing" 
            onclick="btnaddlines2_Click"  />
             
            <asp:Button ID="btnClear2" runat="server" Text="Clear all lines" CssClass="btn_1" 
            onclick="btnClear2_Click" />
            </div>
            <table style="text-align: right;width: 43%;float: right;margin-bottom: 3px;">                       
                        <tr>
                            <td><span style="margin-left:163px;">Total :&nbsp;</span><input id="txtGrandInvoiceTotal2" value="0" type="text" class="SmallTextbox sumition" style="width:110px;" readonly="True" runat="server"/></td>
                        </tr>
                        <tr>
                            <td style="display:none;"><span>Grand Total Invoice:&nbsp;</span><input id="txtGrandTotal2" value="0" type="text" class="SmallTextbox" readonly="True" runat="server"/></td>
                        </tr>
                </table>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>           
           
            <asp:UpdatePanel ID="updpnlbtn" runat="server">
                <ContentTemplate>                
                    <table style="width: 950px;">                       
                        <%--<tr>
                            <td><span>Invoice Total:&nbsp;</span><input id="txtGrandInvoiceTotal" value="0" type="text" class="SmallTextbox" readonly="True" runat="server"/></td>
                        </tr>
                        <tr>
                            <td><span>Grand Total Invoice:&nbsp;</span><input id="txtGrandTotal" value="0" type="text" class="SmallTextbox" readonly="True" runat="server"/></td>
                        </tr>--%>
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
        
        </div>
        
        <div class="subtotal">
        <table>
            <tr>
                <td style="width:320px">Sub Total</td>
                <td><input ID="txttotal2"  runat="server" class="subtotal_txt" /></td>
            </tr>
             <tr>
                <td>
                    <div style="float:right;line-height: 10px; display:none;">
                   
                    <asp:TextBox ID="txtDiscount" CssClass="decimalOnly" runat="server" Width="50px" style="height: 28px;line-height: 20px;"></asp:TextBox>
                    </div>
                </td>
                <td style="display:none;">$0.00</td>
            </tr>
            <tr>
                <td>Total</td>
                <td>$0.00</td>
            </tr>
             <tr style="display:none;">
                <td>Balance due</td>
                <td>$0.00</td>
            </tr>
        </table>
        </div>
        <div style="clear:both"></div>
        <table style="width:950px;height: 60px;" id="ShippingInfo">
        <tr>
            <td>Exporter
            </td>
            <td>
                  <asp:TextBox ID="txtExporter" runat="server"  TextMode="MultiLine" CssClass="textarea" ReadOnly="true"
                  Text="M/S KARIM IMPEX KARACHI-74000 PAKISTAN" >                  
                  </asp:TextBox>
            </td>
            
            <td>Consignee
            </td>
            <td>
                  <asp:TextBox ID="txtConsignee" runat="server"  TextMode="MultiLine" CssClass="textarea"></asp:TextBox>
            </td>
            
             <td>Buyer
            </td>
            <td>
                  <asp:TextBox ID="txtBuyer" runat="server" TextMode="MultiLine" CssClass="textarea"></asp:TextBox>
            </td>
           
        </tr>
        </table>
        <br />
        <div style="clear:both"></div>
        <div>The above goods are shipped under T/T Basis.</div>
        <br />
         <table style="width:950px;height: 60px;" id="shiping">
         
            <tr>
             
                <td style="width:85px;">ExportersRef
                </td>
                <td>
                      <asp:TextBox ID="txtExportersRef" runat="server"  CssClass="sam_textbox"></asp:TextBox>
                </td>
                <td >
                   Form E No :
                </td>
                <td>
                  <asp:TextBox ID="txtFormENo" runat="server"  CssClass="sam_textbox"></asp:TextBox>
                </td>
                <td>
                 Freight :
                </td>
                <td>
                  <asp:TextBox ID="txtFreight" runat="server" CssClass="sam_textbox"></asp:TextBox>
                </td>
                <td>
                    Net Weight :
                </td>
                <td>
                    <asp:TextBox ID="txtNetWeight" runat="server" CssClass="sam_textbox decimalOnly"></asp:TextBox>
                </td>
           </tr>
             <tr>
                 <td>
                 Gross Weight :
                </td>
                <td>
                    <asp:TextBox ID="txtGrossWeight" runat="server" CssClass="sam_textbox decimalOnly"></asp:TextBox>
                </td>
                 <td>
                 Proforma No :
                </td>
                <td>
                    <asp:TextBox ID="txtproformaNo" runat="server" CssClass="sam_textbox"></asp:TextBox>
                </td>
                 <td>
                 Insurance :
                </td>
                <td>
                    <asp:TextBox ID="txtInsurance" runat="server" CssClass="sam_textbox"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td colspan="8" style="text-align:left; padding-left:5px;">
                 Note
                </td>                
            </tr>
            <tr>
                <td colspan="8">
                    <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" CssClass="note"></asp:TextBox>
                </td>                 
            </tr>
        </table>
        <br />
       
           <asp:HiddenField ID="hdnMinDate" runat="server" />
           <asp:HiddenField ID="hdnMaxDate" runat="server" />
                <hr />               
            <div class="file_uploader">
                <asp:FileUpload ID="FileUpload1" runat="server" />
               
            </div>
         </ContentTemplate>
         </asp:UpdatePanel>
    </div>
     
</asp:Content>


