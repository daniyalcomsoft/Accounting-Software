<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Main.master" 
CodeFile="UpdateCOGS.aspx.cs" Inherits="UpdateCOGS" Title="Update COGS" %>

<asp:Content ContentPlaceHolderID="head" ID="Content1" runat="server">
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
        CreateModalPopUp('#Confirmation', 290, 100, 'ALERT');
    });
</script>

</asp:Content>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="ContentDBbackupBody" runat="server">
    <div class="Update_area">
    <div class="Heading">
        <table>
            <tr>
                <td style="width:333px;"></td>
                <td style="width:333px;text-align:center;">Update COGS</td>
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
                <td>
                <asp:Label ID="lblUpdateStatus" runat="server" Text="Please don't press back button, refresh and close browser.  This process might take a while 
                depending upon number of transactions"></asp:Label>
                
                </td>
                <td>
                <div >
                   
                    <asp:Button ID="btnUpdateCOGS" runat="server" CssClass="buttonImp" Text="Update Process" OnClick="btnUpdateCOGS_Click" />
                </div>
                </td>
            </tr>
        </table>
    </center>
    </div>
    </ContentTemplate>
</asp:UpdatePanel>
</div>
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
</asp:Content>