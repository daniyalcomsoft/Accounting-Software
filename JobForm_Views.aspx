<%@ Page Title="View Jobs" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="JobForm_Views.aspx.cs" Inherits="JobForm_Views" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            CreateModalPopUp('#Confirmation', 290, 100, 'ALERT');
            $("[id$=ddlSearch]").change(function () {
                $("[id$=txtSearch]").val("");
            });
        });       
    </script>
    <style type="text/css">
        .box_body {
            border: 1px solid #029FE2;
            margin-bottom: 10px;
            min-height: 75px;
            color: #444;
        }

        .box_header {
            background-color: #029FE2;
            height: 28px;
        }

        .box_txt {
            font-family: arial;
            text-align: left;
            color: white;
            padding: 4px 0px 0px 8px;
            font-weight: bolder;
            font-size: 15px;
        }

        .box_inner {
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
    <div class="Heading">
        <table>
            <tr>
                <td style="width: 328px;"></td>
                <td style="text-align: center; width: 328px;">Job Form View
                </td>
                <td style="text-align: right; width: 328px;">
                    <asp:LinkButton ID="btnNewJob" CssClass="buttonImp" runat="server" OnClick="btnNewJob_Click">Create Job</asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <div id="StausMsg">
    </div>
    <div id="content">
        <br />
        <div class="box_body">
            <div class="box_header"><span class="box_txt">Search Job</span></div>
            <div class="box_inner">
                <asp:Label ID="Label1" runat="server" Text="Search By :"></asp:Label>
                <asp:DropDownList ID="ddlSearch" runat="server" >
                    <asp:ListItem Selected="True" Value="JobNumber" Text="Job Number"></asp:ListItem>
                    <asp:ListItem Value="DisplayName" Text="Customer Name"></asp:ListItem>
                    <asp:ListItem Value="Completed" Text="Completed"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtSearch" runat="server" ></asp:TextBox>                
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn_1" OnClick="btnSearch_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn_1" OnClick="btnClear_Click" />
            </div>
        </div>

        <div class="box_body" style="border: 0px;">
            <div class="box_header" style="margin-bottom: 8px;"><span class="box_txt">Manage Job</span></div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grdJob" runat="server" CssClass="data main" AutoGenerateColumns="False"
                        DataKeyNames="JobID" AllowPaging="true" PageSize="15" DataSourceID="SqlDataSource1"
                        OnPageIndexChanging="grdJob_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="JobNumber" HeaderText="Number"
                                SortExpression="JobNumber" />
                            <asp:BoundField DataField="JobDescription" HeaderText="Job Description"
                                SortExpression="JobDescription" />
                            <asp:BoundField DataField="DisplayName" HeaderText="Customer Name"
                                SortExpression="DisplayName" />
                            <asp:BoundField DataField="Completed" HeaderText="Completed"
                                SortExpression="Completed" />

                            <asp:TemplateField HeaderText="View" HeaderStyle-Width="35px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnView" runat="server" CssClass="view"
                                        CommandArgument='<%# Eval("JobID") %>' OnCommand="lbtnView_Command"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="31px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LbtnEdit" runat="server" CssClass="edit"
                                        CommandArgument='<%# Eval("JobID") %>' OnCommand="LbtnEdit_Command"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="41px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="delete"
                                        CommandArgument='<%# Eval("JobID") %>' OnCommand="lbtnDelete_Command"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="SELECT J.[JobID],J.[JobNumber],J.[JobDescription],C.[DisplayName]
	,CASE WHEN J.[Completed] = 1 THEN 'TRUE' ELSE 'FALSE' END Completed FROM [vt_SCGL_Job] J
	INNER JOIN vt_SCGL_Customer C ON J.[CustomerID] = C.[CustomerID]" SelectCommandType="Text"></asp:SqlDataSource>
    </div>

    <asp:Label ID="lblJobID" runat="server" Visible="false"></asp:Label>
    <div id="Confirmation" style="display: none;">
        <asp:UpdatePanel ID="upConfirmation" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblDeleteMsg" runat="server" Text=""></asp:Label>
                <br />
                <br />
                <asp:LinkButton ID="lbtnYes" CssClass="Button1" runat="server" OnClick="lbtnYes_Click">Yes</asp:LinkButton>&nbsp&nbsp
                <asp:LinkButton ID="lbtnNo" CssClass="Button1" runat="server"
                    OnClientClick="return closeDialog('Confirmation');">No</asp:LinkButton>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

