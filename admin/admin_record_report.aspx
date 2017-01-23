<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin_record_report.aspx.cs" Inherits="admin_admin_record_report" EnableViewState = "true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>BeerGame Beta</title>
    <link href="css/admin.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="javascript/global.js"></script>
    <script src="../javascrip/Game.js" type="text/javascript"></script>

    <script src="../javascrip/jquery.js" type="text/javascript"></script>
    <script src="../javascrip/thickbox.js" type="text/javascript"></script>
    <link href="../javascrip/thickbox.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
   <center>
    <div id="ALL">
    	<div id="TOP"><img src="images/logo.gif" width="250" height="120" alt="BeerGame" class="logo" />
        	
            <a href="../index.aspx">Home</a>            
        </div>
        <div id="MIDDLE">        	      
            <div class="vmenu">
            	<a href="index.aspx">管理首頁</a> 
            	<a href="admin_account.aspx">帳號管理</a>
                <a href="admin_script.aspx">腳本管理</a>
                <a href="admin_game.aspx">遊戲管理</a>
                <a href="admin_record.aspx" class="active">歷史記錄</a>
                <a href="admin_logout.aspx">登出</a>                
          </div>
        	<div class="content">
            	<div class="crumb">
                    <asp:Label ID="LB_Title" runat="server" Text=""></asp:Label>
                  <div class="blank"></div>
                </div>
                <div class="info">
              		<h1 style="display:none;">歷史紀錄說明</h1>
                	<p style="display:none;">您可以從歷史紀錄中選取遊戲，藉此觀察遊戲結果</p>
                	<h2><a href="#" onclick="info_switch(this);">說明</a></h2>
              	</div>
           	  <div class="maincontent">              
           	    <ul>
           	      <li><a id="P_Stock">庫存量</a></li>
                  <li><a id="P_Order">訂購量</a></li>
                  <li><a id="P_WeekCost" >每週成本</a></li>
                  <li><a id="P_TotalCost">成本累計</a></li>
                  <li><a id="P_AreaComposite">成長比較</a></li>
                  <li><a id="P_Composite">綜合比較</a></li>                  
           	    </ul>
           	    <table id="InfoTable" width="100%" cellpadding="0" cellspacing="0" class="table1" style="display:none">
                    <thead>
                        <tr>
                            <td colspan="5">庫存圖表</td>
                        </tr>
                        <tr>
                          <th width="25%">工廠</th>
                            <th width="25%">配銷商</th>
                            <th width="25%">大盤商</th>
                            <th width="25%">經銷商</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="Info_Repeater" runat="server">
                        <ItemTemplate>
                          <tr>
                              <th colspan="5" align="left"><asp:Label ID="LB_ChainNum" runat="server" Text=""></asp:Label></th>
                          </tr>
                            <tr >
                                <td runat="server" id="Factory" align="center"></td>
                                <td runat="server" id="Distribution" align="center" class="state"></td>
                                <td runat="server" id="Wholesale" align="center" class="state"></td>
                                <td runat="server" id="Retailer" align="center"></td>
                          </tr>
                           <tr>
                          <th id="Compostie_Title" colspan="5" align="left">綜合圖表</th>
                        </tr>
                        <tr>
                          <td colspan="1" class="mark"><table width="100%" cellpadding="0" cellspacing="0" class="table1">
                            <tr>
                              <th width="5%">週數</th>
                              <th width="23%">工廠</th>
                              <th width="23%">配銷商</th>
                              <th width="23%">大盤商</th>
                              <th width="23%">零售商</th>
                            </tr>                            
                            <asp:Repeater ID="Composite_Repeater" runat="server">
                            <ItemTemplate>
                                <tr>
                                  <th align="center"><%# DataBinder.Eval(Container.DataItem, "Index") %></th>
                                  <td align="center"><%# DataBinder.Eval(Container.DataItem, "Factory") %></td>
                                  <td align="center"><%# DataBinder.Eval(Container.DataItem, "Distribution")%></td>
                                  <td align="center"><%# DataBinder.Eval(Container.DataItem, "Wholesale")%></td>
                                  <td align="center"><%# DataBinder.Eval(Container.DataItem, "Retailer")%></td>
                                </tr>
                            </ItemTemplate>
                            </asp:Repeater>
                          </table></td>
                          <td colspan="3" runat="server" id="Composite" align="center"></td>
                        </tr>
                       </ItemTemplate>
                       </asp:Repeater>
                    </tbody>
                    <tfoot>
                            <tr>
                                <td colspan="5" align="left">
                                    <!--<asp:Button ID="BT_Export" runat="server" Text="匯出報表" OnClick="BT_Export_Click" />-->
                                </td>
                            </tr>
                        </tfoot>
                     </table>
                     <table id="CompositeTable" width="100%" cellpadding="0" cellspacing="0" class="table1" style="display:none">
                     <thead>
                        <tr>
                            <td colspan="4">綜合圖表</td>
                            <td ><asp:DropDownList ID="DDL_Composite" runat="server" AutoPostBack="true">
                            <asp:ListItem Text="庫存量" Value="1"></asp:ListItem>
                            <asp:ListItem Text="訂購量" Value="2"></asp:ListItem>
                            <asp:ListItem Text="每週成本" Value="3"></asp:ListItem>
                            <asp:ListItem Text="成本累計" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td colspan="4" id="Composite" align="center"></td>
                        </tr>
                     </tbody>
                     </table>
                     
                     <table id="AreaCompositeTable" width="100%" cellpadding="0" cellspacing="0" class="table1" style="display:none">
                     <thead>
                        <tr>
                            <td colspan="4">成長圖表</td>
                            <td ><asp:DropDownList ID="DDL_AreaComposite" runat="server" AutoPostBack="true">
                            <asp:ListItem Text="庫存量" Value="1"></asp:ListItem>
                            <asp:ListItem Text="訂購量" Value="2"></asp:ListItem>
                            <asp:ListItem Text="每週成本" Value="3"></asp:ListItem>
                            <asp:ListItem Text="成本累計" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="AreaComposite_Repeater" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td colspan="4" runat="server" id="AreaComposite" align="center"></td>
                            </tr>
                        </ItemTemplate>
                        </asp:Repeater>
                     </tbody>
                     </table>
                     <div align="center"></div>
           	  </div>
             <div class="crumb">
               <p>&nbsp;</p>
               <div class="blank" align="center"><asp:Label ID="LB_Tip" runat="server" Text="產生圖表中，請稍候。" BorderColor="White"></asp:Label></div>
              </div>
          	</div>
          <hr />                     
      </div>
        <div id="BOTTOM">
        	<p class="copyright">&copy; 2009 Cluber 2.0. All Right Reserved.</p>
        </div>
    </div>
    </center>
    </form>
</body>
</html>
<script type="text/javascript">
    function PanelSet() {
        document.getElementById('P_Stock').className = null;
        document.getElementById('P_Order').className = null;
        document.getElementById('P_WeekCost').className = null;
        document.getElementById('P_TotalCost').className = null;
        document.getElementById('P_Composite').className = null;
        document.getElementById('P_AreaComposite').className = null;
        if (GetUrlVar('target')!=null)
        switch (GetUrlVar('target')) {
            case "Stock_Amount":
                document.getElementById('P_Stock').className = "active";
                document.getElementById('InfoTable').style.display = "block";
                document.getElementById('AreaCompositeTable').style.display = "none";
                break;
            case "Order_Amount":
                document.getElementById('P_Order').className = "active";
                document.getElementById('InfoTable').style.display = "block";
                document.getElementById('AreaCompositeTable').style.display = "none";
                break;
            case "Cost_Amount":
                document.getElementById('P_WeekCost').className = "active";
                document.getElementById('InfoTable').style.display = "block";
                document.getElementById('AreaCompositeTable').style.display = "none";
                break;
            case "Total_Cost":
                document.getElementById('P_TotalCost').className = "active";
                document.getElementById('InfoTable').style.display = "block";
                document.getElementById('AreaCompositeTable').style.display = "none";
                break;
            case "Composite":
                document.getElementById('P_Composite').className = "active";
                document.getElementById('InfoTable').style.display = "none";
                document.getElementById('AreaCompositeTable').style.display = "none";
                document.getElementById('CompositeTable').style.display = "block";
                break;
            case "AreaComposite":
                document.getElementById('P_AreaComposite').className = "active";
                document.getElementById('InfoTable').style.display = "none";
                document.getElementById('CompositeTable').style.display = "none";
                document.getElementById('AreaCompositeTable').style.display = "block";
                break;
                
        }
        else
            document.getElementById('P_Stock').className = "active";
        document.getElementById('P_Stock').href = "admin_record_report.aspx?G_ID=" + GetUrlVar('G_ID') + "&target=Stock_Amount";
        document.getElementById('P_Order').href = "admin_record_report.aspx?G_ID=" + GetUrlVar('G_ID') + "&target=Order_Amount";
        document.getElementById('P_WeekCost').href = "admin_record_report.aspx?G_ID=" + GetUrlVar('G_ID') + "&target=Cost_Amount";
        document.getElementById('P_TotalCost').href = "admin_record_report.aspx?G_ID=" + GetUrlVar('G_ID') + "&target=Total_Cost";
        document.getElementById('P_Composite').href = "admin_record_report.aspx?G_ID=" + GetUrlVar('G_ID') + "&target=Composite";
        document.getElementById('P_AreaComposite').href = "admin_record_report.aspx?G_ID=" + GetUrlVar('G_ID') + "&target=AreaComposite";
    }
    window.onload = PanelSet;
    document.getElementById('LB_Tip').style.display = "none";
</script>