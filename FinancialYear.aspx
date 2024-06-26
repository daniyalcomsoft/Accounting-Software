﻿<%@ Page Title="Setting Financial Year" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="FinancialYear.aspx.cs" Inherits="FinancialYear" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script src="Script/jquery-1.6.2.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="Script/jquery.validator.js" type="text/javascript"></script>

    <script src="Script/jquery-ui-1.8.16.custom.min.js" type="text/javascript" charset="utf-8"></script>
    <link href="Css/validator.css" rel="stylesheet" type="text/css" />
    <link rel="Stylesheet" href="Css/Style.css" type="text/css" />
<script type="text/javascript" language="javascript">
    $(document).ready(function() {
        
            CreateModalPopUp('#NewFinancial', 330, 220, 'Add/Modify Financial Year');
            CreateModalPopUp('#Confirmation', 290, 100, 'ALERT');
        });
        function MyDate() {

            $(".DateTimePicker").datepicker();
        }
        
    </script>
    <style type="text/css">
    .CheckBoxs
        {
            float: left;
        }
        .CheckBoxs input[type=checkbox]
        {
            float: left;
            margin-right: 4px;
            margin-top: 2px;
        }
         .alertbox div
       {
           margin-top:0px;
       }
    
       <style type="text/css">
        body
        {
            background-image: none;
        }
        .input
        {
            height: 26px;
            line-height: 40px;
            border: outset 1px #ccc;
            font-size: 14px;
        }
        .btn_1
        {
            height: 30px;
            width: 120px;
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
    </style>
      </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="Update_area">
    <div class="Heading">
    <table>
        <tr>
            <td style="width:332px;"></td>
            <td style="width:332px;text-align:center;">Financial Year</td>
            <td style="width:332px;float:right;">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:LinkButton ID="btnNew" runat="server" CssClass="buttonImp" OnClick="btnNew_Click">Create Financial Year</asp:LinkButton>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnNew" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>    
        </tr>
    </table>
        
    </div>
    
   
<div id="StausMsg">
</div>
    <asp:Label ID="lblGroupID" runat="server" Visible="false"></asp:Label>
    
    <div id="Confirmation" style="display:none;">
        <asp:UpdatePanel ID="upConfirmation" runat="server">
            <ContentTemplate>
                
                <asp:Label ID="lblDeleteMsg" runat="server" Text=""></asp:Label>
                <br /><br /><asp:LinkButton ID="lbtnYes" CssClass="Button1" runat="server" OnClick="lbtnYes_Click">Yes</asp:LinkButton>&nbsp&nbsp
                <asp:LinkButton ID="lbtnNo" CssClass="Button1" runat="server"
                        OnClientClick="return closeDialog('Confirmation');">No</asp:LinkButton>
            
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
   <br />
    <div style="width:400px; height:30px;margin-left: 274px;">
                   <div>
                        <label style="font-size: 16px;margin-top: 5px;">
                            Financial Year:</label>
                        <asp:DropDownList runat="server" CssClass="input" ID="ddlFinYear" validate="group"
                         custom="Select Financial Year" customFn="var goal = parseInt(this.value); return goal > 0;">
                       </asp:DropDownList>
                    </div> 
                    <div>
                         <asp:Button ID="lbtnsetasdefault" CssClass="btn_1" Text="Set as Default" runat="server"
                         OnClientClick="return validate('group');" onclick="lbtnsetasdefault_Click"  />
                    </div>
    </div>
    <br />
         <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true">
    <ContentTemplate> 
    <div align="center">
    <div style="width:600px;">
    <asp:GridView ID="GridFinancial" CssClass="main data" runat="server" AutoGenerateColumns="False" 
              DataKeyNames="FinYearID" AllowPaging="true" PageSize="14" OnRowDataBound="GridFinancial_RowDataBound"
            onpageindexchanging="GridFinancial_PageIndexChanging">
        <Columns>
            <asp:BoundField DataField="FinYearID" HeaderText="Financial Year ID" InsertVisible="False" ItemStyle-HorizontalAlign="Center"
                ReadOnly="True" SortExpression="FinYearID"  ItemStyle-Width="20%" />
            
            
            <asp:BoundField DataField="FinYearTitle" HeaderText="Financial Year Title" 
                SortExpression="FinYearTitle" />
                 <asp:BoundField DataField="YearFrom"  HeaderText="Year From" 
                SortExpression="YearFrom" dataformatstring="{0:MM/dd/yyyy}" />
                  <asp:BoundField DataField="YearTo" HeaderText="Year To" 
                SortExpression="YearTo" dataformatstring="{0:MM/dd/yyyy}" />
            <%-- DataFormatString="{0:dd/MM/yyyy}"--%>
            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="31px">
                <ItemTemplate>
                
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:LinkButton ID="lbtnEdit" runat="server" 
                        CommandArgument='<%# Eval("FinYearID") %>' Enabled='<%# Eval("ISEnabled") %>'  oncommand="lbtnEdit_Command" cssClass="edit"></asp:LinkButton>
            </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lbtnEdit" EventName="Click" />
              </Triggers>
            </asp:UpdatePanel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="41px">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnDelete" runat="server" cssClass="delete" 
                        CommandArgument='<%# Eval("FinYearID") %>' Enabled='<%# Eval("ISEnabled") %>' oncommand="lbtnDelete_Command"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
        
      <div id="NewFinancial" style="padding-top: 19px;display:none;"> 
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <ContentTemplate>       
              <table>
                <tr>
                    <td>
                        <label style="width: 100px;">
                            Financial Year ID:
                        </label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFinancialYearID" runat="server" AutoComplete="Off" 
                        ReadOnly="True" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label style="width: 126px;">
                            Financial Year Title:
                        </label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFinancialYear" require="Enter Financial Year Title" 
                        validate="FinancialYear" runat="server" placeholder="Year Title"></asp:TextBox>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        <label style="width: 126px;">
                            Date From:
                        </label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDateFrom" CssClass="DateTimePicker"
                         require="Enter Date From" placeholder="Date From" validate="FinancialYear" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label style="width: 126px;">
                            Date To:
                        </label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDateTo"  CssClass="DateTimePicker" 
                        require="Enter Date To" validate="FinancialYear" placeholder="Date To" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label style="width: 98px;">
                        </label>
                    </td>
                    <td>
                        <asp:LinkButton ID="btnSave" CssClass="Button1" runat="server" 
                                    onclick="btnSave_Click" OnClientClick="return validate('FinancialYear')">Save</asp:LinkButton>
                                <asp:LinkButton ID="btnCancal" CssClass="Button1" runat="server" OnClientClick="return closeDialog('NewFinancial');">Cancel</asp:LinkButton>
                    </td>
                </tr>
              </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
        </Triggers>
        </asp:UpdatePanel>
      </div>
     <br />
</div>
</asp:Content>

