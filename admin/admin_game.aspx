<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin_game.aspx.cs" Inherits="admin_admin_game" %>

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
                <a href="admin_game.aspx" class="active">遊戲管理</a>                
                <a href="admin_record.aspx">歷史記錄</a>
                <a href="admin_logout.aspx">登出</a>                
          </div>
        	<div class="content">
            	<div class="crumb">
			  <div class="step">
                    	<h1>Step1</h1>
                        <p>建立帳號</p>
                    </div>
                    <div class="step">
                    	<h1>Step2</h1>
                   	  <p>建立腳本</p>
                    </div>
               	  <div class="step active">
               		<h1>Step3</h1>
               	  	<p>建立遊戲 </p>
                    </div>
                    <div class="step">
                    	<h1>Step4</h1>
                      <p>遊戲設定</p>
                    </div>
					<div class="step" style="width:80px;">
                    	<h1>Step5</h1>
                        <p>開始遊戲</p>
                    </div>                    
                    <div class="blank"></div>
                </div>
                <div class="info">
              		<h1 style="display:none;">遊戲管理說明</h1>
                	<p style="display:none;">您可以從遊戲列表中選取遊戲進行管理，或者建立一個新遊戲</p>
                	<h2><a onclick="info_switch(this);">說明</a></h2>
              	</div>
           	  <div class="maincontent">              
           	    <ul>
           	      <li><a href="#" id="GameListClick" class="active" onclick="On_GameListClick(); return false;">遊戲列表</a></li>
                  <li><a href="#" id="CreateGameClick" onclick="On_CreateGameClick(); return false;">建立遊戲</a></li>
           	    </ul>
           	    <table id="CreateGame_Table" width="100%" cellpadding="0" cellspacing="0" class="table1" style="display:none">
                    <thead>
                            <tr>
                                <td colspan="4">建立遊戲</td>
                            </tr>                            
                        </thead>
                        <tbody>                        	
                            
                            <tr>
                              <th width="20%" align="center">遊戲名稱</th>
                                <td width="80%" colspan="3">
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                              <th align="center">遊戲腳本</th>
                              <td colspan="3"><asp:DropDownList ID="DropDownList2" runat="server" AppendDataBoundItems="true"
                                    OnSelectedIndexChanged="Selection_Change">
                                    </asp:DropDownList>
                              </td>
                            </tr>
                            <tr>
                              <th align="center">供應鏈數目</th>
                              <td colspan="3"><asp:TextBox ID="TextBox3" runat="server" Text="1"></asp:TextBox></td>
                            </tr>
                            <tr>
                              <th align="center">遊戲說明</th>
                              <td colspan="3">
                                  <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                            </tr>
                        </tbody>
                        <tfoot>
                            <tr>
                                <td colspan="4"><asp:Button ID="BT_CreateGame" runat="server" Text="建立遊戲" OnClick="BT_CreateGame_Click" />
                                </td>
                            </tr>
                        </tfoot>
                     </table>
           	    <table id="GameList_Table" width="100%" cellpadding="0" cellspacing="0" class="table1">
<thead>
                            <tr>
                                <td colspan="8">遊戲列表</td>
                            </tr>
                            <tr>
                              <th width="5%"><input type="checkbox" name="checkbox" id="checkbox" onclick="selectAll('GameList');" /></th>
                              <th width="5%">編號</th>
                                <th width="35%">遊戲名稱</th>
                                <th width="10%">供應鏈數量</th>
                                <th width="10%">週數</th>
                                <th width="20%">狀態</th>
                                <th width="5%">編輯</th>
                                <th width="10%">遊戲控制</th>
                            </tr>
                        </thead>
                        <tbody>                        	
                            <tr>
                              <th colspan="8" align="left">選擇腳本                              
                                
                                    <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                    OnSelectedIndexChanged="Selection_Change">
                                    </asp:DropDownList>                                
                              </th>
                            </tr>
                            <asp:Repeater ID="GameList_Repeater" runat="server" OnItemCommand="GameListRepeater_ItemCommand">
                            <ItemTemplate>                          
                            <tr>
                              <td align="center">
                                  <asp:CheckBox ID="CB_CheckGame" runat="server" ToolTip='<%# DataBinder.Eval(Container.DataItem, "ID") %>'/>
                              </td>
                              <td align="center"><%# DataBinder.Eval(Container.DataItem, "ID") %></td>
                                <td align="center"><%# DataBinder.Eval(Container.DataItem, "NAME") %></td>
                                <td align="center"><%# DataBinder.Eval(Container.DataItem, "Supply_Amount") %></td>
                                <td align="center"><%# DataBinder.Eval(Container.DataItem, "PlayWeek") %>/<%# DataBinder.Eval(Container.DataItem, "TotalWeek") %></td>
                                <td align="center" class="state"><%# DataBinder.Eval(Container.DataItem, "StateInfo") %></td>
                                <td align="center"><asp:Button ID="BT_Edit" runat="server" Text="設定" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID") %>' UseSubmitBehavior="false"/></td>
                                <td align="center">
                                    <asp:Button ID="BT_Start" runat="server" Text="開始遊戲" CommandName="Start" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID") %>' Visible="false" UseSubmitBehavior="false"/>
                                    <asp:Button ID="BT_End" runat="server" Text="結束遊戲" CommandName="End" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID") %>' Visible="false" UseSubmitBehavior="false"/>
                                    <asp:Button ID="BT_Restart" runat="server" Text="重置遊戲" CommandName="Restart" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID") %>' Visible="false" UseSubmitBehavior="false"/>
                                </td>
                            </tr>
                            </ItemTemplate>
                            </asp:Repeater>                                  
                        </tbody>
                        <tfoot>
                            <tr>
                                <td colspan="8" align="left">
                                    <asp:Button ID="BT_DelGame" runat="server" Text="刪除遊戲" OnClick="BT_DelGame_Click" />
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
<script type="text/javascript">
function On_GameListClick()
{
    document.getElementById('GameListClick').className="active";
    document.getElementById('CreateGameClick').className="null";
    document.getElementById('CreateGame_Table').style.display="none";
    document.getElementById('GameList_Table').style.display="block";
}

function On_CreateGameClick()
{
    document.getElementById('GameListClick').className="null";
    document.getElementById('CreateGameClick').className="active";
    document.getElementById('CreateGame_Table').style.display="block";
    document.getElementById('GameList_Table').style.display="none";
}
</script>