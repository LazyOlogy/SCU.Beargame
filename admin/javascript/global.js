// JScript 檔
function info_switch(info)
{
	if(info.parentNode.parentNode.getElementsByTagName('H1')[0].style.display=='none')
	{
		info.parentNode.parentNode.getElementsByTagName('H1')[0].style.display = 'block';
		info.parentNode.parentNode.getElementsByTagName('P')[0].style.display = 'block';
	}
	else
	{
		info.parentNode.parentNode.getElementsByTagName('H1')[0].style.display = 'none';
		info.parentNode.parentNode.getElementsByTagName('P')[0].style.display = 'none';
	}
}

function selectAll(ID)
{
	var div = document.getElementById(ID);
	var cbx = div.getElementsByTagName("input"); 
	
	if (cbx[0].checked==true)
	{
		
		cbx = div.getElementsByTagName("input");
		for(var i=0;i<cbx.length;i++)
		{
			if(cbx[i].type == "checkbox")
			{
				cbx[i].checked=true;
				cbx[i].parentNode.parentNode.style.background = '#DCE7EE';
			}
		}
	}
	else
	{
		cbx = div.getElementsByTagName("input");		 
		for(var i=0;i<cbx.length;i++)
		{
			if(cbx[i].type == "checkbox")
			{
				cbx[i].checked=false;     
				cbx[i].parentNode.parentNode.style.background = '#FFFFFF';
			}
		}
	}
}

function selectRow(obj)
{	
	if(obj.checked)
		obj.parentNode.parentNode.style.background = '#DCE7EE';
	else
		obj.parentNode.parentNode.style.background = '#FFFFFF';
}