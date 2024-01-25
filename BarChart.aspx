<%@ Page Title="Bar Chart" Language="C#" MasterPageFile="Main.master" AutoEventWireup="true" 
CodeFile="BarChart.aspx.cs" Inherits="BarChart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Css/jquery-ui-1.8.20.css" rel="stylesheet" type="text/css" />
    <link href="Css/jqChart.css" rel="stylesheet" type="text/css" />
    <link href="Css/jqRangeSlider.css" rel="stylesheet" type="text/css" />
    <script src="Script/jqChart.min.js" type="text/javascript"></script>
    <script src="Script/mousewheel.js" type="text/javascript"></script>
    <script src="Script/jqRangeSlider.min.js" type="text/javascript"></script>
    
    

<script language="javascript" type="text/javascript">
    function Createjqchart(mydata) {
    
       var mydata1 = mydata.split('|||')[0];
        var mydata2 = mydata.split('|||')[1];
         var mydata3 = mydata.split('|||')[2];
        $('#jqChartGawadar').jqChart({
            title: { text: 'Gawadar' },
            shadows: {
                    enabled: true
                },
            legend:{visible:false},
            animation: { duration: 1 },
            axes: [
                    {
                        name: 'y1',
                        location: 'left'
                    }
                ],
            series: [
                    {
                    
                        type: 'column',
                        title: 'Income',
                        fillStyle: '#FFBF00',
                        axisY: 'y1',
                        data: JSON.parse(mydata1)
                    }
                   
                ]
                 
                
        });
        
          $('#jqChartPasni').jqChart({
            title: { text: 'Pasni' },
            shadows: {
                    enabled: true
                },
            legend:{visible:false},
            animation: { duration: 1 },
            axes: [
                    {
                        name: 'y1',
                        location: 'left'
                    }
                ],
            series: [
                    {
                    
                        type: 'column',
                           fillStyle: '#04B431',
                        title: 'Income',
                        axisY: 'y1',
                        data: JSON.parse(mydata3)
                    }
                   
                ]
                 
                
        });
         $('#jqChartKarachi').jqChart({
            title: { text: 'Karachi' },
            shadows: {
                    enabled: true
                },
            legend:{visible:false},
            animation: { duration: 1 },
            axes: [
                    {
                        name: 'y1',
                        location: 'left'
                    }
                ],
            series: [
                    {
                    
                        type: 'column',
                           fillStyle: '#418CF0',
                        title: 'Income',
                        axisY: 'y1',
                        data: JSON.parse(mydata2)
                    }
                   
                ]
                 
                
        });
    }
 


    $(document).ready(function() {
  //  CreateModalPopUp('#NewCostCenter', 330, 165, 'Add/Modify Cost Center');
        $.ajax({
        url: "WebMethod.aspx/SyncDevice",

            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",

            success: function(data) {

                //Createjqchart(data.d);
                // Createjqchart2(data.d);
                // Createjqchart3(data.d);
                //  Createjqchart4(data.d);
            }
              
        });




       // CreateModalPopUp('#NewCostCenter', 330, 165, 'Add/Modify Cost Center');
       




    });
    
    
    
</script>

<style type="text/css">
    .left_side
    {
        width:810px;float:left
    }
    
    .right_side
    {
        width:168px;
        float:left;
        margin-left:15px;
        margin-top:10px
        
    }    
     .right_side2
    {
        width:168px;
        float:left;
        margin-left:22px;
        margin-top:10px
        
    }  
    .first_box
    {
        width: 410px;
        float: left;
        margin-top: 10px;
    }
    .second_box
    {
        width: 400px;
        float: left;
        margin-top: 10px;
    }
    .right_side span
    {
        float:inherit;
    }
    .ammount
    {
        margin-bottom: 10px;
    }
    
    .net_ammount
    {
        font-size: 20px;
        font-weight: 100;
        color: #404040;
        font-family: "DINNextLTPro-Regular", 
    }
    
    .net_txt
    {
        color: #6E8BA8;
        font-size: 14px;
    }
    .small_box
    {
        width:265px;
        float:left;
        margin-top:10px;
    }
    .bg_color
    {
        width:180px;
        height:50px;
        margin-bottom:10px;
    }
    
    .bg_1
    {
        background-color:rgb(255, 158, 0);
    }
    
    .bg_2
    {
         background-color:rgb(255, 46, 46);
    }
    
    .bg_3
    {
        background-color:rgba(54, 172, 28, 0.92);
    }
    
    .small_box span
    {
        font-size: 24px;
        font-weight:bold;
        font-family: arial !important;
        float:none;
        
    }
    .small_box h4
    {
        font-size: 16px;
    }
    .main_left
    {
        float: left;
        width: 810px;
    }   
    .main_right
    {
        float: right;
        width: 180px;
    } 
    .float_left
    {

    }
    .float_right
    {
        float:right !important;
    }
    .r_account_info
    {
        padding-top: 12px;
        height: 41px;
        border-bottom: 1px solid #ddd;
    }
     .r_CostCenter
    {
        padding-top: 0px;
        height: 12px;
        border-bottom: 1px solid #ddd;
    }
    
    
    
table.imagetable {
	font-family: verdana,arial,sans-serif;
	font-size:11px;
	color:#333333;
	border-width: 1px;
	border-color: #999999;
	border-collapse: collapse;
        width: 146px;
        font-weight:bold;
    }
table.imagetable th {

	border-width: 1px;
	padding: 8px;
	border-style: solid;
	border-color: #999999;
	  font-weight:bold;
}
table.imagetable td {

	border-width: 1px;
	padding: 8px;
	border-style: solid;
	border-color: #999999;
	  font-weight:bold;
}
    
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="main_left">
    <div class="left_side" style="margin-bottom:10px;">
        <h1></h1>
        <div class="small_box">
            <div class="bg_color bg_1" id="per1" runat="server"></div>
             <asp:Label style="display:none;" ID="lbltotal" runat="server" ></asp:Label>
              <asp:Label style="color: rgb(255, 158, 0);background-color: rgb(255, 158, 0);font-size: 33px; display:none;" ID="lblperc1" runat="server" ></asp:Label>
                <br />
            <asp:Label ID="lblopeninvoices" runat="server" ></asp:Label>
         
            <%--<h2>$13,1254</h2>--%>
            <h4>Open Invoices</h4>
        </div>
        <div class="small_box">
            <div class="bg_color bg_2" id="per2" runat="server"></div>
             <asp:Label style="color: rgb(255, 46, 46);background-color: rgb(255, 46, 46);font-size: 33px;display:none;" ID="lblperc2" runat="server" ></asp:Label>
             <br />
             <asp:Label ID="lblOverDue" runat="server"></asp:Label>
            <h4>OverDue</h4>
        </div>
        <div class="small_box">
            <div class="bg_color bg_3" id="per3" runat="server" ></div>
             <asp:Label style="color:transparent;background-color: rgba(54, 172, 28, 0.92);font-size: 33px;display:none;" ID="lblperc3" runat="server" ></asp:Label>
             <br />
             <asp:Label ID="lbllastdays" runat="server"    ></asp:Label>
            <h4>Paid Last 30 Days</h4>
        </div>
    </div>
    <div class="left_side">
    <div style="font-size:large; text-align:center; font-weight:bold">Monthly Sales</div>
          <div class="first_box">
          <div id="jqChartGawadar" style="width: 800px; height: 200px;"></div>
          </div>
          <%--<div class="second_box">
          <div id="jqChart2" style="width: 400px; height: 200px;"></div>
          </div>
          <div class="first_box">
          <div id="jqChart3" style="width: 400px; height: 200px;"></div>
          </div>
          <div class="second_box">
          <div id="jqChart4" style="width: 400px; height: 200px;"></div>
          </div>--%>
          <asp:HiddenField ID="hdn" runat="server" />
    </div>
    <br />
     <div class="first_box">
          <div id="jqChartPasni" style="width: 800px; height: 200px;"></div>
          </div>
           <br />
     <div class="first_box">
          <div id="jqChartKarachi" style="width: 800px; height: 200px;"></div>
          </div>
  </div>
    <div style="position:absolute;width:1px;background-color:#ddd;height:117%;display: inline;"></div>
  <div class="main_right">
  <div class="right_side">
        <h2>Bank Account</h2>
        <div class="r_account_info">
            <span id="Bank1Text" class="float_left" runat="server"></span><br />
            
             <span id="Bank1Value" runat="server" class="float_left"></span>
            
           
        </div>
        <div class="r_account_info">
            <span id="Bank2Text" class="float_left" runat="server"></span><br />
            <span id="Bank2Value" class="float_left" runat="server"></span>
        </div>
        
        
          <div class="r_account_info">
            <span id="Bank3Text" class="float_left" runat="server"></span><br />
            <span id="Bank3Value" class="float_left" runat="server"></span>
         <%--    <asp:LinkButton ID="lnkBank" runat="server" ForeColor="Blue" Font-Size=Medium onclick="lnkBank_Click" 
                 > Details</asp:LinkButton>--%>
        </div>
        
         
       
        
  </div>
  <div class="right_side" style="border-bottom: 1px solid #ddd;">
  <div class="ammount">
  <asp:Label ID="txtTotalSales" runat="server"    CssClass="net_ammount"></asp:Label>
  <br />
  <span class="net_txt">NET INCOME</span>
  </div> 
 
  <div class="ammount">
  <asp:Label ID="txtTotalExpense" runat="server" style="font-size: 16px;"></asp:Label>
  <br />
  <span class="net_txt">EXPENSE</span>
  </div>
</div>

        
 <div class="right_side" >
  <div class="ammount">
    <span class="net_txt">Sales Margin</span>
      <br />
  <asp:Label ID="lblSalesMargin" runat="server"    style="font-size: 16px;"></asp:Label>


  </div> 
 
  <div class="ammount">
  <span class="net_txt">Return On Equity</span>  <br />
  <asp:Label ID="lblReturnOnEquity" runat="server"   style="font-size: 16px;"></asp:Label>

  
  </div>
</div>
  
<div class="right_side">

        <div >
            
        
            
           
            
<table class="imagetable">
<tr>
	<td colspan="3">EXPENSES PER KG OF PRODUCTION </td>
</tr>
<tr>
	<td>GWADAR</td><td colspan="2" ><asp:Label ID="txtGwadar" class="float_right" runat="server"></asp:Label></td>
</tr>
<tr>
	<td>PASNI</td><td colspan="2"><asp:Label ID="txtPasni" class="float_right" runat="server"></asp:Label></td>
</tr>
<tr>
	<td>KARACHI </td><td colspan="2"><asp:Label ID="txtKarachi" class="float_right" runat="server"></asp:Label></td>
</tr>
</table>
            
        
            
           
        </div>
      
        
        
          <div class="r_CostCenter">
           
        </div>
        
         
       
        
  </div>
  
<div class="right_side">

        <div >
            
        
            
           
            
<table class="imagetable">
<tr>
	<td colspan="3">FREEZING FOR THE MONTH </td>
</tr>
<tr>
	<td>GWADAR</td><td colspan="2" ><asp:Label ID="txtPurchasesGwadar" class="float_right" runat="server"></asp:Label></td>
</tr>
<tr>
	<td>PASNI</td><td colspan="2"><asp:Label ID="txtPurchasesPasni" class="float_right" runat="server"></asp:Label></td>
</tr>
<tr>
	<td>KARACHI </td><td colspan="2"><asp:Label ID="txtPurchasesKarachi" class="float_right" runat="server"></asp:Label></td>
</tr>
</table>
            
        
            
           
        </div>    
         
       
        
  </div>  
  

</div>

<div runat="server" id="Dynamic">

</div>
 <div id="NewCostCenter" style="padding-top: 19px; display:none;"> 
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
       <ContentTemplate>
       <asp:Label ID="sd" runat="server">sdsd</asp:Label>
       
       </ContentTemplate>
        </asp:UpdatePanel>
      </div>

</asp:Content>

