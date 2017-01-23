<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>BeerGame Beta</title>
<link href="css/global.css" rel="stylesheet" type="text/css" />
</head>
<body><center>
    <form id="form1" runat="server">    
    <div id="ALL">
   		<div id="TOP">
	    <div id="week" class="week"></div>          		      	    
          	<div class="menu">                        
                <asp:LinkButton ID="LBT_Logout" runat="server" OnClick="LB_Logout_Click">登出</asp:LinkButton>            	
            	<a href="#">關於啤酒遊戲</a>
            	<a href="#">首頁</a>
          	</div>             
        </div>
        
    	<div id="MIDDLE" >        	
    		<div id="GAME" class="scene1">            	              
              <img src="images/logo_b.png" width="650" height="277" alt="BeerGame" style="padding:30px;" />                             
    	  </div>
    	  	<div id="INFO" style="background-color:#FFF;">
          		<center>
          		    <div id="Panel_Login" runat="server">
                	<table width="300" cellpadding="0" cellspacing="0" class="table1">
                        <thead>
                            <tr>
                                <th colspan="2" >會員登入</th>
                          </tr>                 
                        </thead>
                        <tbody>
                          <tr>
                            <td width="30%" align="right">登入帳號</td>
                            <td width="70%" align="left">
                                <asp:TextBox ID="TB_UserID" runat="server" Width="180"></asp:TextBox></td>
                          </tr>
                          <tr>
                            <td align="right">登入密碼</td>
                            <td align="left"><asp:TextBox ID="TB_UserPW" runat="server" TextMode="Password" Width="180"></asp:TextBox></td>
                          </tr>
                      </tbody>
                      <tfoot>          
                      <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="BT_Login" runat="server" Text="登入" OnClick="BT_Login_Click" />
                        <input type="reset" name="button2" id="button2" value="重填" /></td>
                        </tr>
                      </tfoot>
                  </table>
                  </div>
                  <div id="Panel_GameList" runat="server">
                  <table width="80%" border="0" cellpadding="0" cellspacing="0" class="table1">
        	          <thead>
        	            <tr>
        	              <th colspan="6" >遊戲列表</th>
      	              </tr>        	    
      	              </thead>
        	          <tbody>
                      <tr>
                        <th align="center">編號</th>
                        <th align="center">遊戲名稱</th>                        
                        <th align="center">供應鏈</th>                        
                        <th align="center">說明</th>
                        <th align="center">遊戲記錄</th>
                        <th align="center">進入遊戲</th>
                      </tr>
                      
                      <asp:Repeater ID="Repeater_GameList" runat="server">
                      <ItemTemplate>
                      <tr>
       	                <td width="5%" align="center"><%# DataBinder.Eval(Container.DataItem, "G_id") %></td>
        	              <td width="20%" align="center"><a href="game.aspx?SID=<%# DataBinder.Eval(Container.DataItem, "ID") %>&AID=<%# DataBinder.Eval(Container.DataItem, "A_ID") %>"><%# DataBinder.Eval(Container.DataItem, "Name") %></a></td>        	              
        	              <td width="10%" align="center"><%# DataBinder.Eval(Container.DataItem, "ChainNum") %></td>
        	              <td width="35%" align="center"><%# DataBinder.Eval(Container.DataItem, "Memo") %></td>
        	              <td width="10%" align="center"><a href="admin/record_report.aspx?G_ID=<%# DataBinder.Eval(Container.DataItem, "G_id") %>&target=Composite">記錄</a></td>
       	                    <td width="10%" align="center"><a href="game.aspx?SID=<%# DataBinder.Eval(Container.DataItem, "ID") %>&AID=<%# DataBinder.Eval(Container.DataItem, "A_ID") %>" style="background-color:ButtonFace; outline-style:outset">進入遊戲</a></td>
       	              </tr>
       	              </ItemTemplate>
      	              </asp:Repeater>
      	            </tbody>
        	          <tfoot>
        	            <tr>
        	              <td colspan="6" align="center">&nbsp;</td>
      	              </tr>
      	            </tfoot>
      	          </table>      	          
      	          </div>      	            	          
          		</center>            
          	</div>          
   	  	</div>
  
  		<div id="BOTTOM">
        	<p class="copyright">&copy; 2009 Cluber 2.0. All Right Reserved.</p>
   	  	</div>    
        
    </div>    
    </form>
    </center>
</body>
</html>

