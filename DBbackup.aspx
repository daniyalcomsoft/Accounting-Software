<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DBbackup.aspx.cs" 
MasterPageFile="Main.master" Inherits="DBbackup" Title="DB Backup" %>

<asp:Content ContentPlaceHolderID="head" ID="ContentDBbackup" runat="server">
<style type="text/css">
    .wrapper{
width: 455px;
height: auto;
background-color: #F5F5F5;
margin: auto;
padding: 20px;
border-radius: 6px 6px 6px 6px;
border: 1px solid #DDDADA;
}
</style>
 <script type="text/javascript" language="javascript">
    $(document).ready(function() {
        CreateModalPopUp('#Confirmation', 665, 307, 'ALERT');
    });
</script>

</asp:Content>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="ContentDBbackupBody" runat="server">
    <div class="Update_area">
    <div class="Heading">
        <table>
            <tr>
                <td style="width:333px;"></td>
                <td style="width:333px;text-align:center;">Create DataBase Backup</td>
                <td style="float:right;width:333px;">
                </td>
            </tr>
        </table>
    </div>
<asp:UpdatePanel ID="UpdatePanelBackUp" runat="server">
    <ContentTemplate>
    <div style="height:26px;">
    <div id="StausMsg">
    </div>
    </div>
    <div style="margin-top:136px;" class="wrapper">
    <center>
        <table>
            <tr>
                <td><label>Location Path:</label></td>
                <td><asp:Label runat="server" ID="lblPath" style="width: 262px;" CssClass="LabelwithBorder"></asp:Label><label>.bak</label></td>
            </tr>
            <tr style="display:none;">
                <td><label style="width:150px;">BackUp File Name:</label></td>
                <td><asp:TextBox ID="txtFileName" style="width: 262px;" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                <div style="margin-top: 10px;margin-right: 29%;">
                    <asp:Button ID="btnCancel" runat="server" CssClass="buttonImp" Text="Cancel" />
                    <asp:Button ID="lnkCreateDBbackup" runat="server" CssClass="buttonImp" Text="Backup" 
                        onclick="lnkCreateDBbackup_Click"/>
                </div>
                </td>
            </tr>
        </table>
    </center>
    </div>
    </ContentTemplate>
</asp:UpdatePanel>
</div>
<div id="Confirmation" style="display: none;">
    <asp:UpdatePanel ID="upConfirmation" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblDeleteMsg" runat="server" Text=""></asp:Label>
            <asp:HiddenField ID="hidDeleteID" runat="server" />
            <br />
            <center style="margin-top: 202px;margin-left: 45%;">
            <asp:LinkButton ID="lbtnYes" CssClass="Button1" runat="server" OnClientClick="return closeDialog('Confirmation');">Yes</asp:LinkButton>
            <asp:LinkButton ID="lbtnNo" CssClass="Button1" runat="server" style="display:none;" OnClientClick="return closeDialog('Confirmation');">No</asp:LinkButton>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
</asp:Content>