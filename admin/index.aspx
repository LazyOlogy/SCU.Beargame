<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="admin_index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>BeerGame Beta</title>
<link href="css/admin.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language="javascript" src="javascript/global.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <center>
    <div id="ALL">
    	<div id="TOP"><img src="images/logo.gif" width="250" height="120" alt="BeerGame" class="logo" />        	
            <a href="../index.aspx">Home</a>            
        </div>
        <div id="MIDDLE">
        	<div class="hmenu" style="display:none;">
            	<a href="#" class="active">選單一</a>
                <a href="#">選單一</a>
                <a href="#">選單一</a>
                <a href="#">選單一</a>
                <a href="#">選單一</a>
            </div>            
            <div class="vmenu">
            	<a href="index.aspx" class="active">管理首頁</a> 
            	<a href="admin_account.aspx">帳號管理</a>
                <a href="admin_script.aspx">腳本管理</a>
                <a href="admin_game.aspx">遊戲管理</a>
                <a href="admin_record.aspx">歷史記錄</a>
                <a href="admin_logout.aspx">登出</a>                
          </div>
        	<div class="content">
            	<div class="crumb">
                	管理首頁                           
                    <div class="blank"></div>
                </div>
                <div class="info"><br />
	        <img src="images/logo_02.gif" width="300" height="128" />
              		<h1 style="display:block;">啤酒遊戲</h1>
                	<p style="display:block;">啤酒遊戲(Beer Game)是20世紀60年代，MIT的Sloan管理學院所發展出來的一種模擬供應鍊的策略遊戲。本系統核心是參考MIT模型，由東吳大學資管系開發。參加遊戲的成員各自扮演不同的角色：零售商、大盤商(Wholesaler)、配銷商(Distributor)和工廠(Factory)。他們每週的決策就是訂購多少啤酒，唯一的目標是儘量扮演好自己的角色，使得供應鏈的總成本最小，總價值最大。四個角色之間的聯繫只能通過訂貨傳送單來溝通資訊，不能有其他方式的資訊交流。本模擬假設啤酒供應鍊有四種廠商參與處理工作。即整個供應鍊上的四個角色都只有一家廠商，雖和實際世界同一供應鍊有多個相同角色的廠商有所差異，但已能夠充分表現所要討論的議題。</p>
                    <a href="admin_account.aspx"><img src="images/icon_08.gif" width="210" height="70" border="0" /></a> 
                	<h2><a href="#" onclick="info_switch(this);">說明</a></h2>
              	</div>
           	  <div class="maincontent">              	          
           	    <ul>
           	      <li><a href="#" class="active">目前進行中</a></li>
           	    </ul>
           	    <table width="100%" cellpadding="0" cellspacing="0" class="table1">
           	      <thead>
                            <tr>
                                <td colspan="5" style="height: 18px">目前進行中的遊戲</td>
                            </tr>
                            <tr>
                              <th width="5%" style="height: 19px">編號</th>
                              <th width="25%" style="height: 19px">遊戲名稱</th>
                                <th width="10%" style="height: 19px">回合</th>
                                <th width="50%" style="height: 19px">說明</th>
                                <th width="10%" style="height: 19px">結束遊戲</th>
                            </tr>
                        </thead>
                        <tbody>
                            
                            <asp:Repeater ID="GameListRepeater" runat="server" OnItemCommand="GameListRepeater_ItemCommand">
                            <ItemTemplate>                            
                            <tr>
                                <td align="center"><%# DataBinder.Eval(Container.DataItem, "ID") %></td>
                                <td><a href="admin_setting.aspx"><%# DataBinder.Eval(Container.DataItem, "NAME") %></a></td>
                                <td align="center">第1週</td>
                                <td><%# DataBinder.Eval(Container.DataItem, "Memo") %></td>
                                <td align="center"><asp:Button ID="BT_Interrupt" runat="server" Text="結束遊戲" CommandName="Interrupt" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID") %>' UseSubmitBehavior="false"/></td>
                            </tr>
                            </ItemTemplate>
                            </asp:Repeater>
                    	        						
                            
                                                    	
                        </tbody>                        
                     </table>
             </div>
             <div class="crumb">
               <p>&nbsp;</p>
               <div class="blank"></div>
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
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>

</body>
</html>
