<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="PhysicalStockCount.aspx.cs" Inherits="PhysicalStockCount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
body
{
 
}
.container
{
    margin:0 auto;
    width:900px;
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
    width: 200px;
    height: 50px;
    position: relative;
    top: 8px;
}
.sam_textbox
{
    width:160px;
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
.float_right
{
    float:right;
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
}
.file_uploader
{
    margin-top:15px;
}
table.data input[type=text] {
width: 115px;
}
</style>
<script type="text/javascript">
    $(document).ready(function() {
        CreateModalPopUp('#Confirmation', 290, 100, 'ALERT');
        Subtract();
        MyDate();
        ChangeDateEvent();
     
    });

    function MyDate() {
        dateMin = $("[id $= hdnMinDate]").val();
        dateMax = $("[id $= hdnMaxDate]").val();
        $(".DateTimePicker").datepicker({ minDate: new Date(dateMin), maxDate: new Date(dateMax) });
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

    function Subtract() {
        $("[id $= txtPhysicalCount]").blur(function() {
            var PhysicalCount = $(this).val();
            var OnHand = $(this).parent().siblings().find("[id $= txtOnHand]").val();
            var SubtractAmount = parseFloat(PhysicalCount-OnHand);
            var Fixed = SubtractAmount.toFixed(2);
            var Total = $(this).parent().siblings().find("[id $= txtdifference]").val(Fixed);

        });
        TotalZeros();

    }


    function TotalZeros() {
        grosstotal = 0;
        $("[id $= GV_PhysicalStockCount] [id $= txtPhysicalCount]").each(function(index, item) {

            if ($(item).val() == "") { $(item).val(0); }
            grosstotal = grosstotal + parseFloat($(item).val());
           
        });
     
    }

  

    function removeDefaultValue() {
        c.each(function() {
            $(this).val();
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
                        <b class="top_heading">PHYSICAL STOCK COUNT</b>
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
           
            <td>
             <asp:label ID="lblCostCenter" runat="server">Please Select CostCenter </asp:label>
            </td>
                <td>
               
              
                <asp:DropDownList ID="ddlCostCenter" validate="grpPhysicalStockCount"  custom="Select CostCenter" customFn="var goal = parseInt(this.value); return goal > 0;"
                 runat="server" AutoPostBack="false"> </asp:DropDownList>
                </td>
                <td>
                      <asp:label ID="lblDate" runat="server">Please Select Date </asp:label>
                </td>
                        
                <td style="width: 150px;">                    
                    <asp:TextBox ID="txtDate" runat="server" CssClass="DateTimePicker"
                    validate="grpPhysicalStockCount" placeholder="Date" require="Select Date" ></asp:TextBox>                    
                </td>
                <td>                    
                    <asp:Button ID="btnSelect" runat="server" Text="Select" CssClass="btn_1"
                    OnClientClick="return validate('grpPhysicalStockCount');" onclick="btnSelect_Click" />
                </td>
            </tr>            
        </table>
        <br />
        <div style="margin-bottom:15px; overflow:auto;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
           <asp:GridView ID="GV_PhysicalStockCount" Width="100%" runat="server" AutoGenerateColumns="False"
           CssClass="data main" Visible="false">
           
               <Columns>
                   <asp:TemplateField HeaderText="Inventory ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="100%">
                       <ItemTemplate>
                           <asp:TextBox ID="txtInventory_Id" runat="server" ReadOnly="true" Text='<%#Bind("InventoryId") %>'></asp:TextBox>
                       </ItemTemplate>
                       <HeaderStyle HorizontalAlign="Left" />
                       <ItemStyle Width="100%" />
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="Inventory Name" ItemStyle-Width="114px">
                       <ItemTemplate>
                          <asp:TextBox ID="txtInventoryName" runat="server" Width="374px" ReadOnly="true" Text='<%#Bind("InventoryName") %>'></asp:TextBox>
                       </ItemTemplate>
                       <ItemStyle Width="114px" />
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="On Hand" ItemStyle-Width="114px">
                       <ItemTemplate>
                            <asp:TextBox ID="txtOnHand" runat="server" ReadOnly="true" Text='<%#Eval("OnHand") %>'></asp:TextBox>
                       </ItemTemplate>
                       <ItemStyle Width="114px" />
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="Physical Count" ItemStyle-Width="114px">
                       <ItemTemplate>
                            <asp:TextBox ID="txtPhysicalCount" Text='<%#Eval("Physicalcount") %>' runat="server"></asp:TextBox>
                       </ItemTemplate>
                       <ItemStyle Width="114px" />
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="Difference" ItemStyle-Width="114px">
                       <ItemTemplate>
                            <asp:TextBox ID="txtdifference" Text='<%#Eval("Difference") %>' runat="server"  ></asp:TextBox>
                       </ItemTemplate>
                       <ItemStyle Width="114px" />
                   </asp:TemplateField>

               </Columns>
           </asp:GridView>
           </ContentTemplate>
           </asp:UpdatePanel>
       </div>
        <div>
        </div>
        
     
        
        <hr />
        <br />
        
        <div id="Notification_ItemID" style="display:none; width:98%; margin:auto;">
            <div class="alert-red">  
                <h4>Error!</h4>
                <label id="IdError" runat="server" style="color:White">Select ProductID</label> 
            </div>
        </div>
        
        <div id="Notification_Success" style="display:none; width:98%; margin:auto;">
            <div class="alert-green">
                <h4>Success</h4>
                <label id="lblSuccessMsg" runat="server" style="color:White">Record Saved Successfully</label>
            </div>
        </div>
        
        <%--Error Message--%>
        <div id="Notification_Error" style="display:none; width:98%; margin:auto;">
            <div class="alert-red">
                <h4>Error!</h4>
                <label id="lblNewError" runat="server" style="color:White"></label> 
            </div>
        </div>        
        <div id="StausMsg">
    </div>
       
            <%--<asp:FileUpload ID="FileUpload1" runat="server" />--%>
            <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="false" oncommand="lbtnDelete_Command"
            CssClass="btn_1 btn_spacing float_right" OnClientClick="return validate('grpPhysicalStockCount');"/>
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                CssClass="btn_1 btn_spacing float_right" Visible="false" 
                onclick="btnCancel_Click"  />
            <asp:Button ID="btnSave" runat="server" Text="Save" Visible="false"
            CssClass="btn_1 btn_spacing float_right" 
                OnClientClick="return validate('grpPhysicalStockCount');" onclick="btnSave_Click" />
                 <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" CssClass="btn_1 btn_spacing float_right" 
                     OnClientClick="return validate('grpPhysicalStockCount');" onclick="btnUpdate_Click" />
       
        
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    
    
   <%--  <div id="StausMsg">
    </div>--%>
     <asp:Label ID="lblGroupID" runat="server" Visible="false"></asp:Label>
     <div id="Confirmation" style="display: none;">
            <asp:UpdatePanel ID="upConfirmation" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblDeleteMsg" runat="server" Text=""></asp:Label>
                    <br /><br /><asp:LinkButton ID="lbtnYes" CssClass="Button1" runat="server" OnClick="lbtnYes_Click">Yes</asp:LinkButton>&nbsp&nbsp
                    <asp:LinkButton ID="lbtnNo" CssClass="Button1" runat="server" OnClientClick="return closeDialog('Confirmation');">No</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
</asp:Content>

