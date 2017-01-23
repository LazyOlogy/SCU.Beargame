<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin_record.aspx.cs" Inherits="admin_admin_record" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
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
                	歷史記錄                           
                  <div class="blank"></div>
                </div>
                <div class="info">
              		<h1 style="display:none;">歷史記錄說明</h1>
                	<p style="display:none;">您可以在歷史記錄中觀看所有進行過的遊戲記錄圖表及資料。</p>
                	<h2><a href="#" onclick="info_switch(this);">說明</a></h2>
              	</div>
           	  <div class="maincontent">              
           	    <ul>
           	      <li><a href="#" class="active">紀錄列表</a></li>
           	    </ul>
           	    <table id="RecordList" width="100%" cellpadding="0" cellspacing="0" class="table1">
<thead>
                            <tr>
                                <td colspan="4">紀錄列表</td>
                            </tr>
                            <tr>
                              <th width="5%"></th>
                              <th width="5%">編號</th>
                                <th width="80%">遊戲名稱</th>
                                <th width="10%">供應鏈</th>                                
                            </tr>
      </thead>
                        <tbody>                        	
                          <tr>
                              <th colspan="4" align="left">選擇腳本
                              <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                    OnSelectedIndexChanged="Selection_Change">
                                    </asp:DropDownList>                                                              
                              </th>
                            </tr>
                            <asp:Repeater ID="GameList_Repeater" runat="server">
                            <ItemTemplate>                            
                            <tr>
                              <td align="center"><asp:CheckBox ID="CB_CheckGame" runat="server" ToolTip='<%# DataBinder.Eval(Container.DataItem, "ID") %>'/></td>
                              <td align="center"><%# DataBinder.Eval(Container.DataItem, "ID") %></td>
                                <td align="left"><a href="admin_record_report.aspx?G_ID=<%# DataBinder.Eval(Container.DataItem, "ID") %>&target=Composite"><%# DataBinder.Eval(Container.DataItem, "NAME") %></a></td>
                                <td align="center"><%# DataBinder.Eval(Container.DataItem, "Supply_Amount") %></td>                                     
                               
                            </tr>
                            </ItemTemplate>
                            </asp:Repeater>                            
                        </tbody>
                        <tfoot>
                            <tr>
                                <td colspan="4" align="left"><asp:Button ID="BT_DelGameRecord" runat="server" Text="刪除記錄" OnClick="BT_DelGameRecord_Click" />
                                </td>
                            </tr>
                        </tfoot>
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
</body>
</html>
