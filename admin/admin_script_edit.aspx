<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin_script_edit.aspx.cs" Inherits="admin_admin_script_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>BeerGame Beta</title>
<link href="css/admin.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language="javascript" src="javascript/global.js"></script>
<script type="text/javascript">
    function TableInit() {
        var j = document.getElementById('TB_Week').value;
        var html = "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" class=\"table1\"><tr><th width=\"10%\">週數</th><th width=\"15%\">需求數量</th><th width=\"60%\">備註</th><th width=\"15%\">提示週數</th></tr>";
        var Request = new Array(j);
        var Memo = new Array(j);
        var Tip_Week = new Array(j);
        Request = document.getElementById('Hid_Request').value.split(',');
        Memo = document.getElementById('Hid_Memo').value.split(',');
        Tip_Week = document.getElementById('Hid_TipWeek').value.split(',');
        for (var i = 0; i < j; i++) {
            var row = "<tr><td align=\"center\">" + (i + 1) + "</td><td align=\"center\"><input name=\"TB_Request\" type=\"text\" id=\"TB_Request\" size=\"3\" value=" + Request[i] + " /></td><td align=\"left\"><input name=\"TB_Memo\" type=\"text\" id=\"TB_Memo\" size=\"60\" value=" + Memo[i] + " /></td><td align=\"center\"><input name=\"TB_TipWeek\" type=\"text\" id=\"TB_TipWeek\" size=\"3\" value=" + Tip_Week[i] + " /></td></tr>";
            html += row;
        }
        document.getElementById('Marketing').innerHTML = html;
    }
</script>
</head>
<body onload="TableInit();">
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
                <a href="admin_script.aspx" class="active">腳本管理</a>
                <a href="admin_game.aspx">遊戲管理</a>               
                <a href="admin_record.aspx">歷史記錄</a>
                <a href="admin_logout.aspx">登出</a>                
          </div>
        	<div class="content">
            	<div class="crumb">
			  <div class="step">
                    	<h1>Step1</h1>
                        <p>建立帳號</p>
                    </div>
                    <div class="step active">
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
              		<h1 style="display:none;">腳本管理說明</h1>
                	<p style="display:none;">您必須建立遊戲的腳本，才能由腳本建立遊戲。</p>
                	<h2><a onclick="info_switch(this);">說明</a></h2>
              	</div>
           	  <div class="maincontent">              
           	    <table id="Scenario_Table" width="100%" cellpadding="0" cellspacing="0" class="table1">
                    <thead>
                        <tr>
                            <td colspan="4">編輯腳本</td>
                        </tr>                            
                    </thead>
                    <tbody onload="BT_UpdateWeekClick">                        	  
                        <tr>
                          <th width="20%" align="center">腳本回合</th>
                          <td colspan="3"><asp:TextBox ID="TB_Week" runat="server" Enabled="false"></asp:TextBox>
                            週
                        </tr>
                        <tr>
                          <th align="center">腳本名稱</th>
                            <td colspan="3"><asp:TextBox ID="TB_Name" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                          <th align="center">各階角色訂貨週期</th>
                          <td colspan="3" class="mark">
                          <table width="100%" cellpadding="0" cellspacing="0" class="table1">                              
                            <tr>
                              <th colspan="2" width="25%">工廠</th>
                              <th colspan="2" width="25%">配銷商</th>
                              <th colspan="2" width="25%">大盤商</th>
                              <th colspan="2" width="25%">零售商</th>
                            </tr>
                            <tr>
                              <td align="center">訂單</td>
                              <td align="center">交貨</td>
                              <td align="center">訂單</td>
                              <td align="center">交貨</td>
                              <td align="center">訂單</td>
                              <td align="center">交貨</td>
                              <td align="center">訂單</td>
                              <td align="center">交貨</td>
                            </tr>
                            <tr class="mark">
                              <td align="center"><asp:TextBox ID="TB_F_Order" runat="server" Columns="1" ></asp:TextBox>週</td>
                              <td align="center"><asp:TextBox ID="TB_F_Delivery" runat="server" Columns="1"></asp:TextBox>週</td>
                              <td align="center"><asp:TextBox ID="TB_D_Order" runat="server" Columns="1"></asp:TextBox>週</td>
                              <td align="center"><asp:TextBox ID="TB_D_Delivery" runat="server" Columns="1"></asp:TextBox>週</td>
                              <td align="center"><asp:TextBox ID="TB_W_Order" runat="server" Columns="1"></asp:TextBox>週</td>
                              <td align="center"><asp:TextBox ID="TB_W_Delivery" runat="server" Columns="1"></asp:TextBox>週</td>
                              <td align="center"><asp:TextBox ID="TB_R_Order" runat="server" Columns="1"></asp:TextBox>週</td>
                              <td align="center"><asp:TextBox ID="TB_R_Delivery" runat="server" Columns="1"></asp:TextBox>週</td>
                            </tr>
                          </table></td>
                      </tr>
                        <tr>
                          <th width="20%" align="center">是否提供當週銷售資料</th>
                          <td width="30%"><asp:CheckBox ID="Ckb_IsSale" runat="server" /></td>
                          <th align="center">是否提供下游庫存資料</th>
                          <td><asp:CheckBox ID="Ckb_IsStock" runat="server" /></td>
                      </tr>
                        <tr>
                          <th align="center">每單位庫存成本</th>
                          <td><asp:TextBox ID="TB_StockCost" runat="server"></asp:TextBox></td>
                          <th align="center">每單位缺貨成本</th>
                          <td><asp:TextBox ID="TB_ShortCost" runat="server"></asp:TextBox></td>
                      </tr>
                      <tr>
                          <th width="20%" align="center">貨品運送良率</th>
                          <td width="30%"><asp:TextBox ID="TB_Yield" runat="server" Text="1"></asp:TextBox>
                          %</td>
                      </tr>
                        <tr>
                          <th align="center">實際市場需求</th>
                          <td id ="Marketing" colspan="3" class="mark">
                           </td>
                        </tr>
                    </tbody>
                       <asp:HiddenField ID="Hid_Request" runat="server" />
                       <asp:HiddenField ID="Hid_Memo" runat="server" />
                       <asp:HiddenField ID="Hid_TipWeek" runat="server" />
                    <tfoot>
                        <tr>
                            <td colspan="4"><asp:Button ID="BT_EditScenario" runat="server" Text="編輯腳本" OnClientClick="SetHidden();" OnClick="BT_EditScenario_Click" />
                            </td>
                        </tr>
                    </tfoot>
                </table>           	    
             </div>
             <div class="crumb">
               <p>
                 <input name="" type="button" value="下一步( 建立腳本 )" />
                 
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
    function On_ScenarioListClick()
    {
        document.getElementById('ScenarioList').className="active";
        document.getElementById('CreateScenario').className="null";
        document.getElementById('Scenario_Table').style.display="none";
        document.getElementById('ScenarioList_Table').style.display="block";
    }

    function On_CreateScenarioClick()
    {
        document.getElementById('ScenarioList').className="null";
        document.getElementById('CreateScenario').className="active";
        document.getElementById('Scenario_Table').style.display="block";
        document.getElementById('ScenarioList_Table').style.display="none";
    }

    function BT_UpdateWeekClick()
    {
        var j = document.getElementById('TB_Week').value;
        var html = "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" class=\"table1\"><tr><th width=\"10%\">週數</th><th width=\"15%\">需求數量</th><th width=\"60%\">備註</th><th width=\"15%\">提示週數</th></tr>";
        for (var i=0;i<j;i++)
        {
            var row = "<tr><td align=\"center\">"+(i+1)+"</td><td align=\"center\"><input name=\"TB_Request\" type=\"text\" id=\"TB_Request\" size=\"3\" /></td><td align=\"left\"><input name=\"TB_Memo\" type=\"text\" id=\"TB_Memo\" size=\"60\" /></td><td align=\"center\"><input name=\"TB_TipWeek\" type=\"text\" id=\"TB_TipWeek\" size=\"3\" /></td></tr>";
            html += row;
        }
        document.getElementById('Marketing').innerHTML = html;
    }

    function SetHidden()
    {
        var Req = document.getElementsByName('TB_Request');
        var Memo = document.getElementsByName('TB_Memo');
        var TipWeek = document.getElementsByName('TB_TipWeek');
        for (var i=0; i<Req.length;i++)
        if (i==0)
        {
            document.getElementById('Hid_Request').value = Req[i].value;
            document.getElementById('Hid_Memo').value = Memo[i].value;
            document.getElementById('Hid_TipWeek').value = TipWeek[i].value;
        }
        else
        {
            document.getElementById('Hid_Request').value += "," + Req[i].value;
            document.getElementById('Hid_Memo').value += "," + Memo[i].value;
            document.getElementById('Hid_TipWeek').value += "," + TipWeek[i].value;
        }
        return true;
    }

    function GetCkb()
    {
        var Ckb = document.getElementsByName('S_Ckb');
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
