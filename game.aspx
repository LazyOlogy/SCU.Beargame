<%@ Page Language="C#" AutoEventWireup="true" CodeFile="game.aspx.cs" Inherits="game" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BeerGame Beta</title>
    <script type="text/javascript" language="javascript" src="javascrip/Game.js"></script>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
</head>
<body>
<center>
    <form id="form1" runat="server">        
        <input id="HiddenWeek" name="" type="hidden" value="5" />
	    
        <div id="ALL">
   		    <div id="TOP">
              <asp:Panel ID="PN_photo" runat="server"></asp:Panel>
              <div id="COVER" class="cover" style="display:none;"></div>
              <div id="GAMELINEINFO" style="display:none;">這是說明喔</div>
   	  	      <div id="week" class="week"><p>遊戲名稱</p>目前週數</div>          		      	    
          	    <div class="menu">                        
            	    <asp:LinkButton ID="LBT_Logout" runat="server" OnClick="LB_Logout_Click">登出</asp:LinkButton>            	    
            	    <a href="#">關於啤酒遊戲</a>
            	    <a href="index.aspx">首頁</a>
          	    </div>             
            </div>
            
    	    <div id="MIDDLE">        	
    		    <div id="GAME" class="" onSelectStart="event.returnValue=false">
            	    <div class="info" style="top:-40px;">
          	   		    <div>
          	    		    <p>本周快訊</p>
       	       		    </div>
       	    	    </div>                
                    <div id="GameLine"></div>                             
  	    	    </div>
    	  	    <div id="INFO">
          		    <center>
              		    <div class="control">
              			    <div id="REQ" class="req active">                	
               	  			    <h1>本週訂購量</h1>
                  			    <div class="wait"></div>
                  			    <input id="MyReq" name="input" type="text" class="input" value="0" maxlength="4" onkeypress="SentReqByEnter();"/>                  
                  			    <a href="#" class="button" onclick="ReqCompeleted();"></a>                    
                		    </div>
                		    <div id="DATA" class="data">
               	  			    <div class="state">
                                         <asp:Label ID="LB_state" runat="server" Text="狀態"></asp:Label></div>
                  			    <div id="LIST" class="wait">
                  				    <table width="620" border="0" cellpadding="0" cellspacing="0" class="table2">
                    				    <thead>
                      					    <tr>
                                                <th>週次</th>
                                                <th>到貨量</th>
                                                <th>庫存量</th>
                                                <th>需求量</th>
                                                <th>訂購量</th>
                                                <th>當期成本</th>
                                                <th>成本累計</th>
                                                <th>市場銷售量</th>
                                                <th>下游總庫存</th>
                      					    </tr>
                    				    </thead>                      				
                  				    </table>
                  			    </div>
                		    </div>                
                	    </div>
          	  		    <hr />
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
