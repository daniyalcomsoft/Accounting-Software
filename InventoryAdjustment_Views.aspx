<%@ Page Title="" Language="C#" MasterPageFile="Main.master" AutoEventWireup="true" CodeFile="InventoryAdjustment_Views.aspx.cs" Inherits="InventoryAdjustment_Views" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script type="text/javascript" language="javascript">
       $(document).ready(function() {
           CreateModalPopUp('#Confirmation', 290, 100, 'ALERT');
           ddlSearch();
       });
       
       function ddlSearch() {
           $("[id $= ddlSearch]").change(function() {
               var ddlVal = $("[id $= ddlSearch]").val();
               if (ddlVal == "Adjustment ID") {
                   $("[id $= txtAdjustmentID]").show();
                   $("[id $= txtRate]").hide();
                   $("[id $= ddlAction]").hide();
                  
               }
               else if (ddlVal == "Rate") {
                   $("[id $= txtRate]").show();
                   $("[id $= txtAdjustmentID]").hide();
                   $("[id $= ddlAction]").hide();
                   
               }
               else {
                   $("[id $= ddlAction]").show();
                   $("[id $= txtRate]").hide();
                   $("[id $= txtAdjustmentID]").hide(); ;
               }
           });
       }

       function setSearchElem() {
           if ($("[id$=ddlSearch]").val() == "Adjustment ID") {
                   $("[id $= txtAdjustmentID]").show();
                   $("[id $= txtRate]").hide();
                   $("[id $= ddlAction]").hide();
              
           }
           else if ($("[id$=ddlSearch]").val() == "Rate") {
                   $("[id $= txtRate]").show();
                   $("[id $= txtAdjustmentID]").hide();
                   $("[id $= ddlAction]").hide();
               
           }
           else {
                   $("[id $= ddlAction]").show();
                   $("[id $= txtAdjustmentID]").hide();
                   $("[id $= txtRate]").hide();
               
           }
       }
        
        
    </script>
        <style type="text/css">
        .box_body
        {
            border: 1px solid #029FE2;
            margin-bottom: 10px;
            min-height: 75px;
            color: #444;
        }
        .box_header
        {
        background-color: #029FE2;
        height: 28px;
        }
        .box_txt
        {
        font-family: arial;
        text-align: left;
        color: white;
        padding: 4px 0px 0px 8px;
        font-weight: bolder;
        font-size: 15px;
        }
        .box_inner
        {
        padding: 10px;
        }
        .btn_1 {
        height: 25px;
        padding: 0px 10px;
        line-height: 20px;
        background-color: #029FE2;
        border-radius: 4px;
        color: #EDF6E3;
        font-size: 12px;
        border: 1px solid #029FE2;
        border-image: initial;
        margin-left: 5px;
        }
        .btn_1:hover {
        background-color: #2C8CB4;
        }
         .input
        {
            height: 30px;
            line-height: 30px;
            border: outset 1px #ccc;
            font-size: 14px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="Heading">
        <table>
            <tr>
                <td style="width: 328px;">
                </td>
                <td style="text-align: center; width: 328px;">
                    Inventory Adjustment View
                </td>
                <td style="text-align: right; width: 328px;">
                    <asp:LinkButton ID="btnCreate" CssClass="buttonImp" runat="server" onclick="btnCreate_Click" 
                        >Create Adjustment Inventory</asp:LinkButton>
                </td>
                
            </tr>
        </table>
    </div>
    <div id="StausMsg">
    </div>
       
    <div id="content">
     <br />
   <div class="box_body">
        <div class="box_header"><span class="box_txt">Search Adjustment Inventory</span></div>
        <div class="box_inner">
           
            <asp:Label ID="Label1" runat="server" Text="Search By :"></asp:Label>
            <asp:DropDownList ID="ddlSearch"  runat="server" 
                >
            <asp:ListItem Selected="True" Value="Adjustment ID" Text="Adjustment ID"></asp:ListItem>
            <asp:ListItem Value="Rate" Text="Rate"></asp:ListItem>
            <asp:ListItem Value="Action" Text="Action"></asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtAdjustmentID" runat="server" CssClass="decimalOnly" 
                ontextchanged="txtAdjustmentID_TextChanged"></asp:TextBox> 
             <asp:TextBox ID="txtRate" CssClass="decimalOnly" style="display:none;" runat="server" ontextchanged="txtRate_TextChanged"></asp:TextBox>
             <asp:DropDownList style="display:none; height:20px;" runat="server" CssClass="input" ID="ddlAction" validate="group"
                        custom="Select Action" customFn="var goal = parseInt(this.value); return goal > 0;">
                        
                        <asp:ListItem Value="1" Text="ADD">ADD</asp:ListItem>
                        <asp:ListItem Value="2" Text="DEDUCT">DEDUCT</asp:ListItem>
                    </asp:DropDownList>          
            <asp:Button ID="btnSearch" runat="server" Text="Search" cssclass="btn_1" 
                onclick="btnSearch_Click"  />
            <asp:Button ID="btnClear" runat="server" Text="Clear" cssclass="btn_1" 
                onclick="btnClear_Click" />
        </div>
    </div>
    

    <div class="box_body" style="border: 0px;">
    <div class="box_header" style="margin-bottom: 8px;"><span class="box_txt">Manage Adjustment Inventory</span></div>   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
        <asp:GridView ID="GridAdjustmentInventoryView" runat="server" 
             CssClass="data main" AutoGenerateColumns="False"
        DataKeyNames="InventoryAdjustmentId" AllowPaging="true" PageSize="15" 
             onpageindexchanging="GridAdjustmentInventoryView_PageIndexChanging" >
        <Columns>
                   
            <asp:BoundField DataField="InventoryAdjustmentId" HeaderText="Adjustment ID"
                SortExpression="InventoryAdjustmentId"/>
                 <asp:BoundField DataField="Action" HeaderText="Action"
                SortExpression="Action"/> 
            <asp:BoundField DataField="Date" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Date" 
                SortExpression="Date" />
            <asp:BoundField DataField="Quantity"  dataformatstring="{0:#,0.00}"  HeaderText="Quantity"
                SortExpression="Quantity"/> 
           <asp:BoundField DataField="Rate" HeaderText="Rate"
                SortExpression="Rate"/> 
     
     
            <asp:TemplateField HeaderText="View" HeaderStyle-Width="35px">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnView" runat="server" cssClass="view"
                        CommandArgument='<%# Eval("InventoryAdjustmentId") %>' oncommand="lbtnView_Command" ></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="31px">
                <ItemTemplate>
                    <asp:LinkButton ID="LbtnEdit" runat="server" cssClass="edit"
                        CommandArgument='<%# Eval("InventoryAdjustmentId") %>' oncommand="lbtnEdit_Command" ></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="41px">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="delete" 
                    CommandArgument='<%# Eval("InventoryAdjustmentId") %>' OnCommand="lbtnDelete_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            
        </Columns>
    </asp:GridView>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server"   OnClientClick="return confirm('Are you sure to delete');"
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="vt_SCGL_SPGetInventoryRecord" SelectCommandType="StoredProcedure"></asp:SqlDataSource>--%>
    </div>
    
    <asp:Label ID="lblGroupID" runat="server" Visible="false"></asp:Label>
     <div id="Confirmation" style="display: none;">
            <asp:UpdatePanel ID="upConfirmation" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblDeleteMsg" runat="server" Text=""></asp:Label>
                    <asp:LinkButton ID="lbtnYes" CssClass="Button1" runat="server" OnClick="lbtnYes_Click">Yes</asp:LinkButton>&nbsp&nbsp
                    <asp:LinkButton ID="lbtnNo" CssClass="Button1" runat="server" OnClientClick="return closeDialog('Confirmation');">No</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    
    
    

</asp:Content>

