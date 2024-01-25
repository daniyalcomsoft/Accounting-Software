<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="InvoiceStatusForm.aspx.cs" Inherits="InvoiceStatusForm" Title="Invoice Status Manager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            CreateModalPopUp('#Confirmation', 290, 100, 'ALERT');
            ddlSearch();
            bindCustomers();
            MyDate();
        });

        function ddlSearch() {
            $("[id $= ddlSearch]").change(function() {
            var ddlVal = $("[id $= ddlSearch]").val();
                if (ddlVal == "Invoice ID") {
                    $("[id $= txtInvoiceID]").show();
                    $("[id $= txtCustomerName]").val('');
                    $("[id $= txtCustomerName]").hide();
                    $("[id $= txtJobNumber]").val('');
                    $("[id $= txtJobNumber]").hide();
                   // $("[id $= txtCustomerName]").val()='';
                }
                else if (ddlVal == "Customer Name") {
                    $("[id $= txtCustomerName]").show();
                       $("[id $= txtInvoiceID]").val('');
                       $("[id $= txtInvoiceID]").hide();
                       $("[id $= txtJobNumber]").val('');
                       $("[id $= txtJobNumber]").hide();
                   }
                   else {
                       $("[id $= txtJobNumber]").show();
                       $("[id $= txtInvoiceID]").val('');
                       $("[id $= txtInvoiceID]").hide();
                       $("[id $= txtCustomerName]").val('');
                       $("[id $= txtCustomerName]").hide();
                   }
         
            });
        }

        function bindCustomers() {


            $("[id$=txtCustomerName]").autocomplete({
                source: function(request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "WebMethod.aspx/Customers",
                        data: "{ 'Match': '" + request.term + "'}",
                        dataType: "json",
                        success: function(Data) {
                            response($.map(Data.d, function(item) {
                                return {
                                    CusID: item.CustomerID,
                                    label: item.CustomerName
                                }
                            }))
                        }
                    });
                },
                minLength: 1,
                select: function(event, ui) {
                    $("[id$=ddlCustomer]").val(ui.item.CusID);
                }
            });
        }

        function MyDate() {
            //            $("[id $= GridSalesInvoiceStatusView] [id $= txtRecDate]").datepicker(dateFormat: 'mm/dd-yy');
                $(".DateTimePicker").datepicker();
        }



        function setSearchElem() {
            if ($("[id $=ddlSearch]").val() == "Invoice ID") {
                $("[id $= txtInvoiceID]").show();
                $("[id $= txtCustomerName]").hide();
                $("[id $= txtJobNumber]").hide();
            }
            else if ($("[id $=ddlSearch]").val() == "Customer Name") {
                $("[id $= txtCustomerName]").show();
                $("[id $= txtInvoiceID]").hide();
                $("[id $= txtJobNumber]").hide();
            }
            else {
                $("[id $= txtJobNumber]").show();
                $("[id $= txtCustomerName]").hide();
                $("[id $= txtInvoiceID]").hide();
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
        font-weight: bold;
        margin-left: 5px;
        }
        .btn_1:hover {
        background-color: #2C8CB4;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="Heading">
         <table>
             <tr>
                 <td style="width: 328px;"></td>
                 <td style="text-align: center; width: 328px;">
                    Invoice Status
                 </td>
             </tr>
         </table>
    </div>
    <div id="StausMsg">
    </div>
    <div id="content">
    <br />
   <div class="box_body">
        <div class="box_header">
        <span class="box_txt">Search Invoice</span>
        </div>
        <div class="box_inner">
           
            <asp:Label ID="Label1" runat="server" Text="Search By :"></asp:Label>
            <asp:DropDownList ID="ddlSearch"  runat="server" >
            <asp:ListItem Selected="True" Value="Invoice ID" Text="Invoice ID"></asp:ListItem>
            <asp:ListItem Value="Customer Name" Text="Customer Name"></asp:ListItem>
            <asp:ListItem Value="Job Number" Text="Job Number"></asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtInvoiceID" CssClass="decimalOnly" runat="server" 
                ontextchanged="txtInvoiceID_TextChanged"></asp:TextBox> 
             <asp:TextBox ID="txtCustomerName" style="display:none;" runat="server"></asp:TextBox>  
             <asp:TextBox ID="txtJobNumber" style="display:none;" runat="server"></asp:TextBox>           
            <asp:Button ID="btnSearch" runat="server" Text="Search" cssclass="btn_1" 
                onclick="btnSearch_Click"  />
            <asp:Button ID="btnClear" runat="server" Text="Clear" cssclass="btn_1" 
                onclick="btnClear_Click" />
        </div>
    </div>
    

    <div class="box_body" style="border: 0px;">
    <div class="box_header" style="margin-bottom: 8px;"><span class="box_txt">Manage Invoice Status</span></div>    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:GridView ID="GridSalesInvoiceStatusView" runat="server" 
            DataSourceID="objInvoice" CssClass="data main" AutoGenerateColumns="False"
        DataKeyNames="InvoiceID" AllowPaging="True" PageSize="20" 
            onpageindexchanging="GridSalesInvoiceStatusView_PageIndexChanging" >
            <%--OnRowEditing="GridSalesInvoiceStatusView_RowEditing"
                OnRowUpdating="GridSalesInvoiceStatusView_RowUpdating"--%>
            <Columns>        
            
                <%--<asp:BoundField DataField="InvoiceID" HeaderText="Invoice ID" SortExpression="InvoiceID"/>
                <asp:BoundField DataField="InvoiceDate" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Invoice Date" SortExpression="InvoiceDate" />
                <asp:BoundField DataField="JobNumber" HeaderText="Job Number" SortExpression="JobNumber" />
                <asp:BoundField DataField="DisplayName" HeaderText="Customer Name" SortExpression="DisplayName" />
                <asp:BoundField DataField="CurrentRec" HeaderText="Current Rec." dataformatstring="{0:#,0.00}" SortExpression="CurrentRec"/>
                <asp:BoundField DataField="Advance" HeaderText="Advance" dataformatstring="{0:#,0.00}" SortExpression="Advance"/>
                <asp:BoundField DataField="ChequeNo" HeaderText="Cheque No" SortExpression="ChequeNo"/>--%>
                    <asp:TemplateField HeaderText="Invoice ID">
                        <ItemTemplate>
                            <%--<label id="lblInvoiceID" runat="server">
                                <%#Eval("InvoiceID")%></label>--%>
                           <asp:Label ID="lblInvoiceID" runat="server" Style="width: 100%;" Text='<%#Eval("InvoiceID")%>'></asp:Label>     
                        </ItemTemplate>
                                                
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Invoice Date">
                        <ItemTemplate>
                            <label>
                                <%#Eval("InvoiceDate")%></label>
                        </ItemTemplate>
                                                
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Job Number">
                        <ItemTemplate>
                            <label>
                                <%#Eval("JobNumber")%></label>
                        </ItemTemplate>
                                                
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Customer Name">
                        <ItemTemplate>
                            <label>
                                <%#Eval("DisplayName")%></label>
                        </ItemTemplate>
                                                
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Current Rec.">
                        <ItemTemplate>
                            <label>
                                <%#Eval("CurrentRec")%></label>
                        </ItemTemplate>
                                                
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Advance">
                        <ItemTemplate>
                            <label>
                                <%#Eval("Advance")%></label>
                        </ItemTemplate>                          
                   </asp:TemplateField> 
                   <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlStatus" SelectedValue='<%# Eval("Status") %>'>
                            <asp:ListItem Value="0" Text="Open"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Close"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        
                      </asp:TemplateField>
                   <asp:TemplateField HeaderText="Cheque No">
                        <ItemTemplate>
                            <asp:TextBox ID="txtChequeNo" runat="server" Style="width: 100%;" Text='<%#Eval("ChequeNo")%>'></asp:TextBox>
                        </ItemTemplate>                          
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="Rec. Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRecDate" runat="server" CssClass="DateTimePicker" Style="width: 100%;" Text='<%#Eval("ReceivedDate")%>'></asp:TextBox>
                        </ItemTemplate>                          
                   </asp:TemplateField> 
                   
                      <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="31px">
                    <ItemTemplate>
                        <asp:LinkButton ID="LbtnEdit" runat="server" cssClass="edit"
                        CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' OnCommand="LbtnEdit_Command" ></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                   <%--<asp:TemplateField HeaderText="Advance">
                        <ItemTemplate>
                            <label>
                                <%#Eval("Advance")%></label>
                        </ItemTemplate>                          
                   </asp:TemplateField>--%> 
                   <%--<asp:BoundField DataField="ChequeNo" HeaderText="Cheque No" SortExpression="ChequeNo"/>--%>
                   <%--<asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblStatus" Text='<%# Bind("InvoiceStatus") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlStatus">
                            <asp:ListItem Value="0" Text="Open"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Close"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                      </asp:TemplateField>--%>
                     
               <%-- <asp:TemplateField HeaderText="Status">
               
                        <ItemTemplate>
                            <label> <%#Eval("Status")%></label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtInvoiceStatus" runat="server" Style="width: 100%;" Text='<%#Eval("Status")%>'></asp:TextBox>
                        </EditItemTemplate>
                        
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Cheque No">
                        <ItemTemplate>
                            <label> <%#Eval("ChequeNo")%></label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtChequeNo" runat="server" Style="width: 100%;" Text='<%#Eval("ChequeNo")%>'></asp:TextBox>
                        </EditItemTemplate>
                        
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="40px">
                        <ItemTemplate>
                            <center>
                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="edit" CommandName="Edit" ToolTip="Edit"></asp:LinkButton>
                            </center>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <center>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="btnUpdate" runat="server" CssClass="update" CommandName="Update"
                                                ToolTip="Update"></asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="btnCancel" runat="server" CssClass="cancel" CommandName="Cancel"
                                                ToolTip="Cancel"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </EditItemTemplate>
                        <HeaderStyle Width="40px"></HeaderStyle>
                    </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="objInvoice" runat="server" 
            SelectMethod="getallInvoice" TypeName="InvoiceStatus_BAL" >
            <%--UpdateMethod="Update" DataObjectTypeName="InvoiceStatus_BAL"--%>
            
                 
        </asp:ObjectDataSource>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    <%----%>
        
    
<%--    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="vt_SCGL_SpGetallInvoiceStatus" SelectCommandType="StoredProcedure"
        UpdateCommand="UPDATE [vt_SCGL_Invoice] SET [ChequeNo] = 
              @ChequeNo,[Status]=@Status WHERE [InvoiceID] = @InvoiceID" >
               <UpdateParameters>
                <asp:Parameter Type="String" 
                  Name="ChequeNo"></asp:Parameter>
                  <asp:Parameter Name="Status" Type="Int32"/>
              </UpdateParameters></asp:SqlDataSource>
    </div>--%>
    
  <asp:Label ID="lblGroupID" runat="server" Visible="false"></asp:Label>
    
</asp:Content>

