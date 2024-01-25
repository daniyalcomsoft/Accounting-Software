<%@ Page Title="Setting Accounts" Language="C#" MasterPageFile="Main.master" 
AutoEventWireup="true" CodeFile="SettingForm.aspx.cs" Inherits="SettingForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
$(document).ready(function() {
 Load_AutoComplete_Code();

});

function Load_AutoComplete_Code() {
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
                                }
                            }))
                        }
                    });
                },
                minLength: 3,
                select: function(event, ui) {
                    $('#' + $(this).attr('id') + 'lbl').text(ui.item.Title);
                    $("[id$=titlecode]").val(ui.item.Title);
                }

            })
            };
</script>
    <style type="text/css">
        .style1
        {
            width: 138px;
        }
        .style2
        {
            width: 180px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="width:100%;" align="center">
<div id="StausMsg">
    </div>
    <div class="Heading">
        <table>
            <tr>
                <td style="width: 0px;">
                </td>
                <td style="text-align: center; width: 832px;">
                    <b>Create/Modify Account Settings</b>
                </td>
                
            </tr>
        </table>
    </div>
    <table border="0" cellpadding="0" cellspacing="0" width="620">
        <tr align="left" style="display:none;">
            <td class="style2">
             <label>Asset Account</label>
            </td>
            <td class="style1" >
                <asp:TextBox ID="txtAsetAcc" runat="server" Style="width: 190px; height:28px; padding-right: 1px;"
                      CssClass="autoCompleteCodes" 
                     class="textfield" placeholder="Asset Account" 
                    AutoPostBack="True"
                    MaxLength="9"></asp:TextBox>
            </td>
            <td class="style3" style="width:150px">
               <asp:Label ID="txtCodelblAssetAccount" style="text-align:left" runat="server" 
                    ForeColor="#2C8CB4" Width="202px"></asp:Label>
               <input id="titlecode" runat="server" type="hidden" />
            </td>
            
        </tr>
        <tr align="left">
            <td class="style2">
             <label>Income Account</label>
            </td>
            <td class="style1">
            <asp:TextBox ID="txtIncomAcc" runat="server" Style="width: 190px; height:28px; padding-right: 1px;"
               validate="group" require="Please enter a Income Account" CssClass="autoCompleteCodes"
                  class="textfield" placeholder="Income Account" AutoPostBack="True" 
                    ontextchanged="txtIncomAcc_TextChanged" MaxLength="9" ></asp:TextBox>
            </td>
            <td class="style3" style="width:150px">
               <asp:Label ID="txtCodelblIncomeAccount" runat="server" ForeColor="#2C8CB4" Width="202px"></asp:Label>
           </td>
        </tr>
        <%--<tr align="left" style="display:none;">
            <td class="style2">
             <label>Expense Account</label> 
            </td>
            <td class="style1">
            <asp:TextBox ID="txtExpensAcc" runat="server" Style="width: 190px; height:28px; padding-right: 1px;"
               CssClass="autoCompleteCodes"
                  class="textfield" placeholder="Expense Account" 
                    AutoPostBack="True" MaxLength="9"></asp:TextBox>
            
            </td>
            <td class="style3" style="width:150px">
               <asp:Label ID="txtCodelblExpenseAccount" runat="server" ForeColor="#2C8CB4" Width="202px" 
                    Height="16px"></asp:Label>
           </td>
        </tr>--%>
        
        
         <tr align="left">
            <td class="style2">
             <label>Deposit Account</label> 
            </td>
            <td class="style1">
            <asp:TextBox ID="txtDepositAcc" runat="server" Style="width: 190px; height:28px; padding-right: 1px;"
               validate="group" require="Please enter a Deposit Account" CssClass="autoCompleteCodes"
                  class="textfield" placeholder="Deposit Account" AutoPostBack="True" 
                    ontextchanged="txtDepositAcc_TextChanged" MaxLength="9"></asp:TextBox>
            </td>
            <td class="style3" style="width:150px">
               <asp:Label ID="txtCodelblDepositAccount" runat="server" ForeColor="#2C8CB4" Width="201px" 
                    Height="16px"></asp:Label>
           </td>
        </tr>
           <tr align="left">
            <td class="style2">
             <label>Customer Account</label> 
            </td>
            <td class="style1">
            <asp:TextBox ID="txtCustomerAcc" runat="server" Style="width: 190px; height:28px; padding-right: 1px;"
               validate="group" require="Please enter a Customer Account" CssClass="autoCompleteCodes"
                  class="textfield" placeholder="Deposit Account" AutoPostBack="True" 
                    ontextchanged="txtCustomerAcc_TextChanged" MaxLength="9"></asp:TextBox>
            </td>
            <td class="style3" style="width:150px">
               <asp:Label ID="txtCodelblCustomerAccount" runat="server" ForeColor="#2C8CB4" Width="204px"></asp:Label>
           </td>
        </tr>
        <tr align="left">
            <td class="style2">
             <label>COGS Account</label> 
            </td>
            <td class="style1">
            <asp:TextBox ID="txtCOGSAcc" runat="server" Style="width: 190px; height:28px; padding-right: 1px;"
               validate="group" require="Please enter Cost Of Goods Sold Account" CssClass="autoCompleteCodes"
                  class="textfield" placeholder="Cost Of Goods Sold Account" AutoPostBack="True" 
                    ontextchanged="txtCOGSAcc_TextChanged" MaxLength="9"></asp:TextBox>
            </td>
            <td class="style3" style="width:150px">
               <asp:Label ID="txtCodelblCOGSAccount" runat="server" ForeColor="#2C8CB4" Width="204px"></asp:Label>
           </td>
        </tr>
        <tr align="left">
            <td class="style2">
             <label>Inventory Account</label> 
            </td>
            <td class="style1">
            <asp:TextBox ID="txtInvAcc" runat="server" Style="width: 190px; height:28px; padding-right: 1px;"
               validate="group" require="Please enter an Inventory Account" CssClass="autoCompleteCodes"
                  class="textfield" placeholder="Inventory Account" AutoPostBack="True" 
                    ontextchanged="txtInvAcc_TextChanged" MaxLength="9"></asp:TextBox>
            </td>
            <td class="style3" style="width:150px">
               <asp:Label ID="txtCodelblInventoryAccount" runat="server" ForeColor="#2C8CB4" Width="204px"></asp:Label>
           </td>
        </tr>
        <tr align="left">
            <td class="style2">
             <label>Inventory Adjsutment Account</label> 
            </td>
            <td class="style1">
            <asp:TextBox ID="txtInvAdjAcc" runat="server" Style="width: 190px; height:28px; padding-right: 1px;"
               validate="group" require="Please enter an Inventory Adjustment Account" CssClass="autoCompleteCodes"
                  class="textfield" placeholder="Inventory Adjustment Account" AutoPostBack="True" 
                    ontextchanged="txtInvAdjAcc_TextChanged" MaxLength="9"></asp:TextBox>
            </td>
            <td class="style3" style="width:150px">
               <asp:Label ID="txtCodelblInventoryAdjAccount" runat="server" ForeColor="#2C8CB4" Width="204px"></asp:Label>
           </td>
        </tr>
        <tr align="left">
            <td class="style2">
             <label>Purchase Account</label> 
            </td>
            <td class="style1">
            <asp:TextBox ID="txtPurchaseAcc" runat="server" Style="width: 190px; height:28px; padding-right: 1px;"
               validate="group" require="Please enter a Purchase Account" CssClass="autoCompleteCodes"
                  class="textfield" placeholder="Purchase Account" AutoPostBack="True" 
                    ontextchanged="txtPurchaseAcc_TextChanged" MaxLength="9"></asp:TextBox>
            </td>
            <td class="style3" style="width:150px">
               <asp:Label ID="txtCodelblPurchase" runat="server" ForeColor="#2C8CB4" Width="204px"></asp:Label>
           </td>
        </tr>
         <tr align="left">
            <td class="style2">
             <label>Purchase Discount Account</label> 
            </td>
            <td class="style1">
            <asp:TextBox ID="txtPurchaseDiscountAcc" runat="server" Style="width: 190px; height:28px; padding-right: 1px;"
                CssClass="autoCompleteCodes"  validate="group" require="Please enter a Purchase Discount Account"
                  class="textfield" placeholder="Purchase Discount Account" AutoPostBack="True" ontextchanged="txtPurchaseDiscountAcc_TextChanged"
                     MaxLength="9"></asp:TextBox>
            </td>
            <td class="style3" style="width:150px">
               <asp:Label ID="txtCodelblPurchaseDiscount" runat="server" ForeColor="#2C8CB4" Width="204px"></asp:Label>
           </td>
        </tr>
         <tr align="left">
            <td class="style2">
             <label>Sales Discount Account</label> 
            </td>
            <td class="style1">
            <asp:TextBox ID="txtSaleDiscountAcc" runat="server" Style="width: 190px; height:28px; padding-right: 1px;"
               CssClass="autoCompleteCodes"  validate="group" require="Please enter a Sale Discount Account"
                  class="textfield" placeholder="Sale Disocunt  Account" AutoPostBack="True" ontextchanged="txtSaleDiscountAcc_TextChanged"
                    MaxLength="9"></asp:TextBox>
            </td>
            <td class="style3" style="width:150px">
               <asp:Label ID="txtCodelblSaleDiscount" runat="server" ForeColor="#2C8CB4" Width="204px"></asp:Label>
           </td>
        </tr>
         <tr align="left">
            <td class="style2">
             <label>Expense Account</label> 
            </td>
            <td class="style1">
            <asp:TextBox ID="txtExpenseAcc" runat="server" Style="width: 190px; height:28px; padding-right: 1px;"
               CssClass="autoCompleteCodes"  validate="group" require="Please enter an Expense Account"
                  class="textfield" placeholder="Expense Account" AutoPostBack="True" ontextchanged="txtExpenseAcc_TextChanged"
                    MaxLength="9"></asp:TextBox>
            </td>
            <td class="style3" style="width:150px">
               <asp:Label ID="txtCodelblExpense" runat="server" ForeColor="#2C8CB4" Width="204px"></asp:Label>
           </td>
        </tr>
        <tr align="left">
            <td class="style2">
             <label>Sales Tax Account</label> 
            </td>
            <td class="style1">
            <asp:TextBox ID="txtSalesTaxAcc" runat="server" Style="width: 190px; height:28px; padding-right: 1px;"
               CssClass="autoCompleteCodes"  validate="group" require="Please enter a Sales Tax Account"
                  class="textfield" placeholder="Sales Tax Account" AutoPostBack="True" ontextchanged="txtSalesTaxAcc_TextChanged"
                    MaxLength="9"></asp:TextBox>
            </td>
            <td class="style3" style="width:150px">
               <asp:Label ID="txtCodelblSalesTax" runat="server" ForeColor="#2C8CB4" Width="204px"></asp:Label>
           </td>
        </tr>
        <tr align="left">
            <td class="style2">
             <label>Shipping Account</label> 
            </td>
            <td class="style1">
            <asp:TextBox ID="txtShippingAcc" runat="server" Style="width: 190px; height:28px; padding-right: 1px;"
               CssClass="autoCompleteCodes"  validate="group" require="Please enter a Shipping Account"
                  class="textfield" placeholder="Shipping Account" AutoPostBack="True" ontextchanged="txtShippingAcc_TextChanged"
                    MaxLength="9"></asp:TextBox>
            </td>
            <td class="style3" style="width:150px">
               <asp:Label ID="txtCodelblShippingAccount" runat="server" ForeColor="#2C8CB4" Width="204px"></asp:Label>
           </td>
        </tr>
        <tr align="left">
            <td class="style2">
             <label>Detention Expense Account</label> 
            </td>
            <td class="style1">
            <asp:TextBox ID="txtDetentionExpAcc" runat="server" Style="width: 190px; height:28px; padding-right: 1px;"
               CssClass="autoCompleteCodes"  validate="group" require="Please enter a Detention Expense Account"
                  class="textfield" placeholder="Detention Expense Account" AutoPostBack="True" ontextchanged="txtDetentionExpAcc_TextChanged"
                    MaxLength="9"></asp:TextBox>
            </td>
            <td class="style3" style="width:150px">
               <asp:Label ID="txtCodelblDetentionExpAccount" runat="server" ForeColor="#2C8CB4" Width="204px"></asp:Label>
           </td>
        </tr>
        <tr align="left">
            <td class="style2">
             <label>Impressed Account</label> 
            </td>
            <td class="style1">
            <asp:TextBox ID="txtImpressedAcc" runat="server" Style="width: 190px; height:28px; padding-right: 1px;"
               CssClass="autoCompleteCodes"  validate="group" require="Please enter an Impressed Account"
                  class="textfield" placeholder="Impressed Account" AutoPostBack="True" ontextchanged="txtImpressedAcc_TextChanged"
                    MaxLength="9"></asp:TextBox>
            </td>
            <td class="style3" style="width:150px">
               <asp:Label ID="txtCodelblImpressedAccount" runat="server" ForeColor="#2C8CB4" Width="204px"></asp:Label>
           </td>
        </tr>
        <tr align="left">
            <td class="style2">
             <label>Cash Account</label> 
            </td>
            <td class="style1">
            <asp:TextBox ID="txtCashAcc" runat="server" Style="width: 190px; height:28px; padding-right: 1px;"
               CssClass="autoCompleteCodes"  validate="group" require="Please enter a Cash Account"
                  class="textfield" placeholder="Cash Account" AutoPostBack="True" ontextchanged="txtCashAcc_TextChanged"
                    MaxLength="9"></asp:TextBox>
            </td>
            <td class="style3" style="width:150px">
               <asp:Label ID="txtCodelblCashAccount" runat="server" ForeColor="#2C8CB4" Width="204px"></asp:Label>
           </td>
        </tr>
        <tr>
            <td class="style2">
           </td>
            <td align="center" class="style1">
            
                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                 Style="width: 75px; height:25px; margin:5px 24px 0 0px;" class="buttonImp" 
                    onclick="btnCancel_Click"  />
                <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" 
                Style="width: 75px; height:25px; margin:5px 19px 0 0;"
                 OnClientClick="return validate('group');" class="buttonImp" />
                
                      
            </td>
        </tr>
    </table>

</div>

</asp:Content>

