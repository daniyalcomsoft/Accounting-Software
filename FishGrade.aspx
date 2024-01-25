<%@ Page Title="" Language="C#" MasterPageFile="Main.master" AutoEventWireup="true"
    CodeFile="FishGrade.aspx.cs" Inherits="FishGrade" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            CreateModalPopUp('#FishGrade', 330, 150, 'Add/Modify Fish Grade');
            CreateModalPopUp('#Confirmation', 290, 100, 'ALERT');
        });
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
       .box_body
        {
            border: 1px solid #029FE2;
            margin-bottom: 10px;
            min-height: 75px;
            color: #444;
        }
          .box_body_Search
        {
            border: 1px solid #029FE2;
            margin-bottom: 10px;
            min-height: 75px;
            color: #444;
            margin-left:200px;
            width: 597px;
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="Update_area">
        <div class="Heading">
            <table>
                <tr>
                    <td style="width: 332px;">
                    </td>
                    <td style="width: 332px; text-align: center;">
                        Fish Grade
                    </td>
                    <td style="width: 332px; float: right;">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:LinkButton ID="btnNew" runat="server" CssClass="buttonImp" OnClick="btnNew_Click">Create Fish Grade</asp:LinkButton>
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
        <div id="content">
<br />
   <div class="box_body_Search">
        <div class="box_header"><span class="box_txt">Search Fish Grade</span></div>
        <div class="box_inner">
           
            <asp:Label ID="Label1" runat="server" Text="Search By Fish Grade : "></asp:Label>
          
            <asp:TextBox ID="txtSearchFishGrade" runat="server" 
                ontextchanged="txtSearchFishGrade_TextChanged" ></asp:TextBox> 
                        
            <asp:Button ID="btnSearch" runat="server" Text="Search" cssclass="btn_1" 
                onclick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" cssclass="btn_1" 
                onclick="btnClear_Click" />
        </div>
    </div>
    

    <div class="box_body_Search" style="border: 0px;">
    <div class="box_header" style="margin-bottom: 8px;"><span class="box_txt">Manage Fish Grade</span></div>
        
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
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true">
            <ContentTemplate>
                <div align="center">
                    <div style="width: 600px;">
                        <asp:GridView ID="GridFish" CssClass="main data" runat="server" AutoGenerateColumns="False"
                            DataKeyNames="FishGradeID" AllowPaging="true" PageSize="14" OnPageIndexChanging="GridFish_PageIndexChanging">
                            <Columns>
                               <%-- <asp:BoundField DataField="FishGradeID" HeaderText="FishGradeID" InsertVisible="False"
                                    ItemStyle-HorizontalAlign="Center" ReadOnly="True" SortExpression="FishGradeID"
                                    ItemStyle-Width="15%" />--%>
                                <asp:BoundField DataField="FishGrade" HeaderText="FishGrade" SortExpression="FishGrade" />
                                <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="31px">
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# Eval("FishGradeID") %>' Enabled='<%# Eval("ISEnabled") %>'
                                                    OnCommand="lbtnEdit_Command" CssClass="edit"></asp:LinkButton>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="lbtnEdit" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="41px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="delete" CommandArgument='<%# Eval("FishGradeID") %>' Enabled='<%# Eval("ISEnabled") %>'
                                            OnCommand="lbtnDelete_Command"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="FishGrade" style="padding-top: 19px; display: none;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                <label style="width: 100px;">
                                    Fish Grade ID:
                                </label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFishGradeID" runat="server" AutoComplete="Off" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label style="width: 126px;">
                                    Fish Grade:
                                </label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFishGrade" require="Enter Fish Grade" placeholder="Fish Grade" validate="CostCenterL"
                                    runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label style="width: 98px;">
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label style="width: 98px;">
                                </label>
                            </td>
                            <td>
                                <asp:LinkButton ID="btnSave" CssClass="Button1" runat="server" OnClick="btnSave_Click"
                                    OnClientClick="return validate('CostCenterL')">Save</asp:LinkButton>
                                <asp:LinkButton ID="btnCancal" CssClass="Button1" runat="server" OnClientClick="return closeDialog('FishGrade');">Cancel</asp:LinkButton>
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
