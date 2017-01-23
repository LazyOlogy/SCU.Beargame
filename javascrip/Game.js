// JScript 檔
var check=0;
var GameXml;

//以下變數都是本周的
var State=1;
var TotalWeek=0;
var Week=0;
var Title="";
var Info="";
var Delivery_cycle=0;
var Order_cycle=0;

var Arrive_Amount;
var Stock_Amount;
var Request_Amount;
var Order_Amount;
var Cost_Amount;
var Total_Cost;
var Market_Amount;
var Total_Stock;
var Send_Amount;
var Total_Short;

//Timer
var Req;
var Amt;
var CAmt;
var CoverValue=0;

window.onload = function()
{   
	GameXmlLoad("GameHandler.ashx?SID="+GetUrlVar('SID')+"&AID="+GetUrlVar('AID'));
	document.onmousemove = gamelineInfo_mouse_move;
}
/*===========================GameLine===========================*/
function gameline_mouse_down(e)
{
	var e=(e)?e:event;
	check=1;
	sx = e.clientX;  //儲存滑鼠所在的X座標
	sy = e.clientY;  //儲存滑鼠所在的Y座
	document.onmousemove=gameline_mouse_move;
}	
function gameline_mouse_move(e)
{
	if(!e) e=window.event; 
	var x = e.clientX;
	var GameLine = document.getElementById('GameLine').getElementsByTagName('table')[0];
	if(check==1 && parseInt(GameLine.style.left) < (-1*(GameLine.offsetWidth-document.documentElement.clientWidth+300)))
	{
		//check=0;
		GameLine.style.left = (-1*(GameLine.offsetWidth-document.documentElement.clientWidth+300))+'px';
	}
	else if(check==1 && parseInt(GameLine.style.left) <= 50)
	{			
		var x = e.clientX-sx;  //利用現在的滑鼠的X座標 - 原本儲存的滑鼠X座標，表示滑鼠的水平移動			
		sx = e.clientX; //將現在的滑鼠的X座標儲存，用於下一次的移動			
		GameLine.style.left = (parseInt(GameLine.style.left)+x) +'px';  //取的層與左邊的距離+水平移動→新的與左邊的距離
	}
	else if(check==1 && parseInt(GameLine.style.left) > 50)
	{
		//check=0;
		GameLine.style.left = '50px';
	}
}
function gameline_mouse_up(){		
		check = 0;  //當設定為跟隨滑鼠移動時
		document.onmousemove = gamelineInfo_mouse_move;
}	

function gamelineInfo_mouse_move(e) //設定時間軸說明
{
	if(!e) e=window.event;	
	document.getElementById('GAMELINEINFO').style.left = 5+e.clientX;
	document.getElementById('GAMELINEINFO').style.top = e.clientY-45;	
}


/*===========================GameData===========================*/
function GameXmlRequest()   //目前沒用到= =
{
    
    var http = new ActiveXObject("Microsoft.XMLDOM");   
	var url = "GameHandler.ashx";
	var params = "Supply_ID=68";
	http.open("POST",url,true);
	
	http.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
	http.setRequestHeader("Content-length", params.length);
	http.setRequestHeader("Connection", "close");
	
	http.onreadystatechange = function() {  //Call a function when the state changes.
		if(http.readyState == 4 || http.readystate=='complete')
		{		       
		    GameXmlLoad(http.responseText);
		}				
	}
	http.send(params);
	
}

function GameXmlLoad(file)
{
      
	var moz =(typeof document.implementation != 'undefined')&&(typeof document.implementation.createDocument != 'undefined');
	var ie = (typeof window.ActiveXObject != 'undefined');   
	if (moz){   	//FireFox
		GameXml = document.implementation.createDocument("","",null);
		GameXml.load(file);	  	 	
		GameXml.onload = GameXmlLoadCompeleted;  
	}else if (ie){   //IE
		GameXml = new ActiveXObject("Microsoft.XMLDOM");   
		GameXml.async = false;
		while(GameXml.readyState != 4) {};
		GameXml.load(file);				
		
		if(GameXml.text !="")
		    GameXmlLoadCompeleted();		
	}	
} 	
function GameXmlLoadCompeleted()	//xml載入完成
{	
	window.clearInterval(Req);
	document.getElementById('REQ').className = 'req';
	
	Setting();
	CreateGameLine();	//產生GameLine
	CreateGameData();	//產生GameData
	
	
	Amt=setInterval("Animation()",50);
	CAmt = setInterval('CoverAmt(0)',50);
}

function Setting()
{
    try
    {
        State = parseInt(GameXml.getElementsByTagName("GAME")[0].getAttribute("State"));
        TotalWeek = parseInt(GameXml.getElementsByTagName("GAME")[0].getAttribute("Week"));
        Week = GameXml.getElementsByTagName("Week").length;
        Title = GameXml.getElementsByTagName("TITLE")[0].childNodes[0].nodeValue;
        Info = GameXml.getElementsByTagName("INFO")[0].childNodes[0].nodeValue;
        Delivery_cycle = parseInt(GameXml.getElementsByTagName("GAME")[0].getAttribute("Delivery_cycle"));
        Order_cycle = parseInt(GameXml.getElementsByTagName("GAME")[0].getAttribute("Order_cycle"));
    }catch(e){}
	
	
	//標題設定
	if(State != '0')
	    document.getElementById('week').innerHTML = '<p>'+ Title +'</p>Week ' + (Week+1);
	else
    {
        document.getElementById('week').innerHTML = '<p>'+ Title +'</p>Game Over';
        document.getElementById('REQ').style.display = 'none';
    }
	
	//本週快訊設定
	document.getElementById('GAME').getElementsByTagName('DIV')[0].innerHTML = '<div><p>本周快訊：'+Info+'</p></div>';	
	alert(document.getElementById('GAME').getElementsByTagName('DIV')[0].innerText);
	
	
	//場景設定
	if(Week<=TotalWeek/3)
		document.getElementById('GAME').className='scene1';
	else if(Week<=TotalWeek*2/3)
		document.getElementById('GAME').className='scene2';
	else
		document.getElementById('GAME').className='scene3';	
	
}

function ReqCompeleted()	//結束本週
{
	document.getElementById('REQ').className += ' active';
	document.getElementById('GAME').getElementsByTagName('DIV')[0].style.top = '-40px';
	CAmt = setInterval('CoverAmt(1)',50);
	var MyReq = document.getElementById('MyReq').value;
	
	if(MyReq=='')
	    MyReq = 0;
	
    var str = 'GameXmlLoad("GameHandler.ashx?SID='+GetUrlVar('SID')+'&AID='+GetUrlVar('AID')+'&REQ='+MyReq+'&Week='+Week+'")';   
	
	Req = setInterval('GameXmlLoad('+str+')',1000);
}

function SentReqByEnter()   //按Enter送出
{
    if(event.keyCode==13)
    {
        ReqCompeleted();
    }
}


function CreateGameLine()	//產生GameLine
{
	var str='';
	
	str += '<table border="0" align="center" cellpadding="0" cellspacing="0" class="game_table" width="'+(TotalWeek*50+150)+'">';
	
	//truck_red
	str += '<tr class="truck_red">';
	str += '<td align="center">&nbsp;</td>';
	for(var i=0;i<TotalWeek;i++)
	{
		if(i+1<=Week-Delivery_cycle-Order_cycle)
			str += '<td align="center" class="stock" onmouseover="DisplayGameLineInfo(\''+GameXml.getElementsByTagName("Week")[i].getAttribute("Send_Amount")+'箱啤酒己經在第'+(i+1)+'週送達下游了\');" onmouseout="HideGameLineInfo();">'+GameXml.getElementsByTagName("Week")[i].getAttribute("Send_Amount")+'</td>';
		else if(i+1>Week-Delivery_cycle-Order_cycle && i+1<=Week)
			str += '<td align="center" class="active" onmouseover="DisplayGameLineInfo(\'第'+(i+1)+'週 下游向你訂購了'+GameXml.getElementsByTagName("Week")[i].getAttribute("Request_Amount")+'箱啤酒\');" onmouseout="HideGameLineInfo();">'+GameXml.getElementsByTagName("Week")[i].getAttribute("Request_Amount")+'</td>';
		else
			str += '<td align="center">&nbsp;</td>';
	}		
	str += '</tr>';		
	
	//timeline_red
	str += '<tr class="timeline_red">';
	str += '<td align="center" class="title">&nbsp;</td>';
	for(var i=0;i<TotalWeek;i++)
	{
		if(i==0)
			str += '<td align="center" class="start" onmouseover="DisplayGameLineInfo(\'第'+(i+1)+'週\');" onmouseout="HideGameLineInfo();">'+(i+1)+'</td>';
		else if(i+1==TotalWeek)
			str += '<td align="center" class="end" onmouseover="DisplayGameLineInfo(\'第'+(i+1)+'週\')" onmouseout="HideGameLineInfo();">'+(i+1)+'</td>';
		else
			str += '<td align="center" onmouseover="DisplayGameLineInfo(\'第'+(i+1)+'週\')" onmouseout="HideGameLineInfo();">'+(i+1)+'</td>';
	}
	str += '</tr>';
	
	//空格
	str += '<tr><td colspan="'+(Week+1)+'" height="10"></td></tr>';
	
	//truck_blue
	str += '<tr class="truck_blue">';
	str += '<td align="center">&nbsp;</td>';
	for(var i=0;i<TotalWeek;i++)
	{
		if(i+1<=Week-Delivery_cycle-Order_cycle)
			str += '<td align="center" class="stock" onmouseover="DisplayGameLineInfo(\''+GameXml.getElementsByTagName("Week")[i].getAttribute("Arrive_Amount")+'箱啤酒己經在第'+(i+1)+'週送逹了!\');" onmouseout="HideGameLineInfo();">'+GameXml.getElementsByTagName("Week")[i].getAttribute("Arrive_Amount")+'</td>';
		else if(i+1>Week-Delivery_cycle-Order_cycle && i+1<=Week)
			str += '<td align="center" class="active" onmouseover="DisplayGameLineInfo(\'第'+(i+1)+'週 你訂購了'+GameXml.getElementsByTagName("Week")[i].getAttribute("Order_Amount")+'箱啤酒\');" onmouseout="HideGameLineInfo();">'+GameXml.getElementsByTagName("Week")[i].getAttribute("Order_Amount")+'</td>';
		else
			str += '<td align="center">&nbsp;</td>';
	}		
	str += '</tr>';
	
	//timeline_blue
	str += '<tr class="timeline_blue">';
	str += '<td align="center" class="title">&nbsp;</td>';
	for(var i=0;i<TotalWeek;i++)
	{
		if(i==0)
			str += '<td align="center" class="start" onmouseover="DisplayGameLineInfo(\'第'+(i+1)+'週\');" onmouseout="HideGameLineInfo();">'+(i+1)+'</td>';
		else if(i+1==TotalWeek)
			str += '<td align="center" class="end" onmouseover="DisplayGameLineInfo(\'第'+(i+1)+'週\');" onmouseout="HideGameLineInfo();">'+(i+1)+'</td>';
		else
			str += '<td align="center" onmouseover="DisplayGameLineInfo(\'第'+(i+1)+'週\');" onmouseout="HideGameLineInfo();">'+(i+1)+'</td>';
	}
	str += '</tr>';
		
	//Now
	str += '<tr>';
	str += '<td align="center">&nbsp;</td>';
	for(var i=0;i<TotalWeek;i++)
	{
		if(i==Week)
			str += '<td align="center" valign="bottom"><img src="images/game_now.png" width="40" height="42" /></td>';			
		else
			str += '<td align="center" valign="bottom"></td>';
	}
	str += '</tr>';
	str += '</table>';
	document.getElementById('GameLine').innerHTML = str;
	
	
	/**/
	var GameLine = document.getElementById('GameLine').getElementsByTagName('table')[0];
	
	if(parseInt(GameLine.clientWidth)>parseInt(document.documentElement.clientWidth))
	{
		document.onmouseup=gameline_mouse_move;
		document.onmouseup=gameline_mouse_up;
		GameLine.onmousedown=gameline_mouse_down;
		GameLine.style.left = ((Week*50+50)*-1)+document.documentElement.clientWidth/2+'px';
	}
}	



function CreateGameData(){	//產生GameData
	var str='';
	str += '<table width="620" border="0" cellpadding="0" cellspacing="0" class="table2">';
	str += '<thead><tr>';
	str += '<th>週次</th>';
	str += '<th onmouseover="DisplayGameLineInfo(\'當週的到貨量\');">到貨量</th>';
	str += '<th onmouseover="DisplayGameLineInfo(\'當週總共欠下游的貨量\');">缺貨量</th>';
	str += '<th onmouseover="DisplayGameLineInfo(\'當週實際在庫存的貨量\');">實際庫存量</th>';
	str += '<th onmouseover="DisplayGameLineInfo(\'當週收到下游的訂單數量\');">需求量</th>';
	str += '<th onmouseover="DisplayGameLineInfo(\'當週向上游的訂貨量\');">訂購量</th>';
	str += '<th onmouseover="DisplayGameLineInfo(\'當週的成本\');">當期成本</th>';
	str += '<th onmouseover="DisplayGameLineInfo(\'總共累計的成本\');">成本累計</th>';
	str += '<th onmouseover="DisplayGameLineInfo(\'當週市場銷售量\');">市場銷售量</th>';
	str += '<th onmouseover="DisplayGameLineInfo(\'下游總庫存量\');">下游總庫存</th>';
	str += '</tr></thead>';
	
	for(var i=GameXml.getElementsByTagName("Week").length;i>0;i--)
	{
		if(i==GameXml.getElementsByTagName("Week").length)
		{
			str += '<tr class="mark">';
			Arrive_Amount = GameXml.getElementsByTagName("Week")[i-1].getAttribute("Arrive_Amount");
			Total_Short = GameXml.getElementsByTagName("Week")[i-1].getAttribute("Total_Short");
			Send_Amount = GameXml.getElementsByTagName("Week")[i-1].getAttribute("Send_Amount");
			Stock_Amount = GameXml.getElementsByTagName("Week")[i-1].getAttribute("Stock_Amount");
			Request_Amount = GameXml.getElementsByTagName("Week")[i-1].getAttribute("Request_Amount");
			Order_Amount = GameXml.getElementsByTagName("Week")[i-1].getAttribute("Order_Amount");
			Cost_Amount = GameXml.getElementsByTagName("Week")[i-1].getAttribute("Cost_Amount");
			Total_Cost = GameXml.getElementsByTagName("Week")[i-1].getAttribute("Total_Cost");
			Market_Amount = GameXml.getElementsByTagName("Week")[i-1].getAttribute("Market_Amount");
			Total_Stock = GameXml.getElementsByTagName("Week")[i-1].getAttribute("Total_Stock");				
		}
		else
			str += '<tr>';
			
		str += '<th align="center">'+i+'</th>';
		str += '<td align="center">'+GameXml.getElementsByTagName("Week")[i-1].getAttribute("Arrive_Amount")+'</th>';
		str += '<td align="center">'+GameXml.getElementsByTagName("Week")[i-1].getAttribute("Total_Short")+'</th>';		
		str += '<td align="center">'+GameXml.getElementsByTagName("Week")[i-1].getAttribute("Stock_Amount")+'</th>';
		str += '<td align="center">'+GameXml.getElementsByTagName("Week")[i-1].getAttribute("Request_Amount")+'</th>';
		str += '<td align="center">'+GameXml.getElementsByTagName("Week")[i-1].getAttribute("Order_Amount")+'</th>';
		str += '<td align="center">'+GameXml.getElementsByTagName("Week")[i-1].getAttribute("Cost_Amount")+'</th>';
		str += '<td align="center">'+GameXml.getElementsByTagName("Week")[i-1].getAttribute("Total_Cost")+'</th>';
		str += '<td align="center">'+GameXml.getElementsByTagName("Week")[i-1].getAttribute("Market_Amount")+'</th>';
		str += '<td align="center">'+GameXml.getElementsByTagName("Week")[i-1].getAttribute("Total_Stock")+'</th>';
		str += '</tr>';
	}
	str += '</table>';
	document.getElementById('LIST').innerHTML = str;
}	


//動畫
function Animation()
{
	//Data Animation	
	if(parseInt(document.getElementById('LIST').getElementsByTagName('table')[0].clientHeight) > parseInt(document.getElementById('LIST').clientHeight))
	{
		document.getElementById('LIST').style.height = parseInt(document.getElementById('LIST').clientHeight)+100+'px';			
	}
	else
	{
		document.getElementById('LIST').className='';	//去除等待
	}
	
	//GameLine Animation
	if(parseInt(document.getElementById('GAME').getElementsByTagName('DIV')[0].style.top) < 0)
	{			
		document.getElementById('GAME').getElementsByTagName('DIV')[0].style.top = parseInt(document.getElementById('GAME').getElementsByTagName('DIV')[0].style.top) + 5 + 'px';
	}
	
	
	if(parseInt(document.getElementById('GAME').getElementsByTagName('DIV')[0].style.top) >= 0 && parseInt(document.getElementById('LIST').getElementsByTagName('table')[0].clientHeight) <= parseInt(document.getElementById('LIST').clientHeight))
	{
		window.clearInterval(Amt);
	}
}

function CoverAmt(flag)
{		
	if(flag && CoverValue<=8)
	{
	    document.getElementById('COVER').style.display = 'block';
		document.getElementById('COVER').style.opacity = CoverValue*0.1;
		document.getElementById('COVER').style.filter = 'alpha(opacity=' + CoverValue*10 + ')';
		CoverValue++;
	}
	else
	{
	    document.getElementById('COVER').style.display = 'none';
		document.getElementById('COVER').style.opacity = 0;
		document.getElementById('COVER').style.filter = 'alpha(opacity=' + 0 + ')';
		CoverValue = 0;
	}		
	if(CoverValue==8 || CoverValue==0)
	{
		window.clearInterval(CAmt);
	}
}

function GetUrlVar(Var)
{    
    var url=window.location.toString();   
    if(url.indexOf("?")!=-1){ //url裡有"?"符號
        var ary=url.split("#")[0].split("?")[1].split("&");
        
        for(var i in ary){
          if(ary[i].split("=")[0] == Var)
          {            
            return ary[i].split("=")[1];          
          }
        }        
    }
    else
    { 
        return null;
    }
}

function DisplayGameLineInfo(Str)
{
    obj = document.getElementById('GAMELINEINFO');
    obj.style.display = 'block';
    obj.innerHTML=Str;
}
function HideGameLineInfo()
{
    obj = document.getElementById('GAMELINEINFO');
    obj.style.display = 'none';
}


