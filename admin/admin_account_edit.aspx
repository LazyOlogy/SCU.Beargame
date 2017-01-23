<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin_account_edit.aspx.cs" Inherits="admin_admin_account_edit" %>

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
            	<a href="admin_account.aspx" class="active">帳號管理</a>
                <a href="admin_script.aspx">腳本管理</a>
                <a href="admin_game.aspx">遊戲管理</a>
                <a href="admin_record.aspx">歷史記錄</a>
                <a href="admin_logout.aspx">登出</a>                
          </div>
        	<div class="content">
            	<div class="crumb">
                    <div class="step active">
                    	<h1>Step1</h1>
                        <p>建立帳號</p>
                    </div>
                    <div class="step">
                    	<h1>Step2</h1>
                      <p>建立腳本</p>
                    </div>
                  <div class="step">
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
              		<h1 style="display:none;">帳號管理說明</h1>
                	<p style="display:none;">您必須建立玩家帳號，並配置其角色，才能順利進行遊戲。每個遊戲共有四種角色：零售商、大盤商、配銷商、工廠</p>
                	<h2><a href="#" onclick="info_switch(this);">說明</a></h2>
              	</div>
           	  <div class="maincontent">              
           	    <ul>
           	      <li><a href="#" id="P_AccountList" class="active" onclick="On_P_AccountListClick();return false;">修改帳號</a></li>                  
       	        </ul>
           	    <table id="Single_Table" width="100%" cellpadding="0" cellspacing="0" class="table1">
       	                <thead>
                            <tr>
                                <td colspan="2">修改帳號</td>
                            </tr>                            
                        </thead>
                        <tbody>                        	
                            
                            <tr>
                              <th width="20%" align="center">帳號名稱</th>
                                <td width="80%"><asp:TextBox ID="TB_Name" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                              <th align="center">密碼</th>
                              <td><asp:TextBox ID="TB_Password" runat="server"></asp:TextBox></td>
                          </tr>
                            <tr>
                              <th align="center">電子郵件</th>
                              <td><asp:TextBox ID="TB_Mail" runat="server"></asp:TextBox></td>
                          </tr>
                            <tr>
                              <th align="center">角色</th>
                              <td><asp:DropDownList ID="DDL_Type" runat="server">
                                        <asp:ListItem Text="未設定" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="工廠" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="配銷商" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="大盤商" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="零售商" Value="4"></asp:ListItem>
                                        </asp:DropDownList></td>
                          </tr>
                            <tr>
                              <th align="center">備註</th>
                              <td><asp:TextBox ID="TB_Memo" runat="server" TextMode="MultiLine" Height="50"></asp:TextBox></td>
                          </tr>
                        </tbody>
                        <tfoot>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="BT_CreateSingle" runat="server" Text="修改" />
                                </td>
                            </tr>
                        </tfoot>
                     </table>
                     <asp:HiddenField ID="Hid_Ckb" runat="server" />
                     <asp:HiddenField ID="Hid_Repeater" runat="server" />           	        
             </div>
             <div class="crumb">
               <p>
                 
                 
               </p>
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
function On_P_AccountListClick()
{
    document.getElementById('P_SingleCreate').className="null";
    document.getElementById('P_BatchCreate').className="null";
    document.getElementById('P_AccountList').className="active";
    document.getElementById('Single_Table').style.display="none";
    document.getElementById('Batch_Table').style.display="none";
    document.getElementById('AccountList').style.display="block";
}

function On_P_SingleCreateClick()
{
    document.getElementById('P_SingleCreate').className="active";
    document.getElementById('P_BatchCreate').className="null";
    document.getElementById('P_AccountList').className="null";
    document.getElementById('Single_Table').style.display="block";
    document.getElementById('Batch_Table').style.display="none";
    document.getElementById('AccountList').style.display="none";
}

function On_P_BatchCreateClick()
{
    document.getElementById('P_SingleCreate').className="null";
    document.getElementById('P_BatchCreate').className="active";
    document.getElementById('P_AccountList').className="null";
    document.getElementById('Single_Table').style.display="none";
    document.getElementById('Batch_Table').style.display="block";
    document.getElementById('AccountList').style.display="none";
}

function GetRepeater(R)
{
    var Hid = document.getElementById('Hid_Repeater');
    Hid.value="";
    Hid.value =R;
}

function GetCkb()
{
    var Ckb = document.getElementsByName('AL_Ckb');
    var Hid = document.getElementById('Hid_Ckb');
    Hid.value="";
    for(var i=0;i<Ckb.length;i++)
    {
        if (Ckb[i].checked==true && Ckb[i].value!='all' && Ckb[i].type=='checkbox')
        {
            if (Hid.value.length>0)
            {                
                Hid.value += ","+Ckb[i].value;
            }
            else
            {            
                Hid.value = ""+Ckb[i].value;
            }
        }
    }
}
</script>