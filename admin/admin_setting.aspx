<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin_setting.aspx.cs" Inherits="admin_admin_setting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
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
                <div id="TOP">
                    <img src="images/logo.gif" width="250" height="120" alt="BeerGame" class="logo" />
                     <a href="../index.aspx">Home</a>
                </div>
                <div id="MIDDLE">
                    <div class="vmenu">
                        <a href="index.aspx">管理首頁</a> <a href="admin_account.aspx">帳號管理</a> <a href="admin_script.aspx">
                            腳本管理</a> <a href="admin_game.aspx" class="active">遊戲管理</a> <a href="admin_record.aspx">
                                歷史記錄</a> <a href="admin_logout.aspx">登出</a>
                    </div>
                    <div class="content">
                        <div class="crumb">
                            <div class="step">
                                <h1>
                                    Step1</h1>
                                <p>
                                    建立帳號</p>
                            </div>
                            <div class="step">
                                <h1>
                                    Step2</h1>
                                <p>
                                    建立腳本</p>
                            </div>
                            <div class="step">
                                <h1>
                                    Step3</h1>
                                <p>
                                    建立遊戲
                                </p>
                            </div>
                            <div class="step active">
                                <h1>
                                    Step4</h1>
                                <p>
                                    遊戲設定</p>
                            </div>
                            <div class="step" style="width: 80px;">
                                <h1>
                                    Step5</h1>
                                <p>
                                    開始遊戲</p>
                            </div>
                            <div class="blank">
                            </div>
                        </div>
                        <div class="info">
                            <h1 style="display:none;">遊戲管理說明</h1>
                	<p style="display:none;">您可以從遊戲列表中選取遊戲進行管理，或者建立一個新遊戲</p>
                            <h2>
                                <a onclick="info_switch(this);">說明</a></h2>
                        </div>
                        <div class="maincontent">
                            <ul>
                                <li>
                                    <asp:HyperLink ID="HL_GameSetting" runat="server" >遊戲設定</asp:HyperLink>
                                </li>
                                <li><asp:HyperLink ID="HL_SupplySetting" runat="server">供應鏈設定</asp:HyperLink></li>
                            </ul>
                            <asp:Panel ID="PN_GameSetting" runat="server">                            
                            <table width="100%" cellpadding="0" cellspacing="0" class="table1">
                                <thead>
                                    <tr>
                                        <td colspan="4">
                                            遊戲設定</td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <th width="20%" align="center">
                                            遊戲名稱</th>
                                        <td width="80%" colspan="3">
                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <th align="center">
                                            遊戲腳本</th>
                                        <td colspan="3">
                                            <asp:DropDownList ID="DDL_Scenario" runat="server">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <th align="center" style="height: 40px">
                                            遊戲說明</th>
                                        <td colspan="3" style="height: 40px">
                                            &nbsp;<asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <td colspan="4" style="height: 24px">
                                            <asp:Button ID="BT_EditGame" runat="server" Text="修改遊戲" OnClick="BT_EditGame_Click" />
                                        </td>
                                    </tr>
                                </tfoot>
                            </table>
                            </asp:Panel>
                            <asp:Panel ID="PN_SupplySetting" runat="server">
                            <table width="100%" cellpadding="0" cellspacing="0" class="table1" >
                                <thead>
                                    <tr>
                                        <td colspan="3">供應鏈設定</td>
                                    </tr>
                                    <tr>                                        
                                        <th width="10%">
                                            供應鏈</th>
                                        <th width="80%">
                                            角色</th>
                                        <th width="10%">
                                            設定</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <th colspan="3" align="left" style="height: 19px">
                                            [Game]
                                            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></th>
                                    </tr>
                                    <asp:Repeater ID="GameListRepeater" runat="server" OnItemCommand="RP_GameListRepeater_ItemCommand">
                                        <ItemTemplate>
                                            <tr>                                                
                                                <td align="center">
                                                    第<asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ChainNum") %>'></asp:Label>條</td>
                                                <td>
                                                    <asp:Panel ID="PN_Setting" runat="server">                                                                                                                                                
                                                    <table width="100%" cellpadding="0" cellspacing="0" class="table1">
                                                        <tr>
                                                            <th width="25%">
                                                                工廠</th>
                                                            <th width="25%">
                                                                配銷商</th>
                                                            <th width="25%">
                                                                大盤商</th>
                                                            <th width="25%">
                                                                零售商</th>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Label ID="LB_Account1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Account1") %>'></asp:Label>
                                                                <asp:DropDownList ID="DDL_Account1" runat="server" Visible="false">
                                                                </asp:DropDownList></td>
                                                            <td align="center">
                                                                <asp:Label ID="LB_Account2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Account2") %>'></asp:Label>
                                                                <asp:DropDownList ID="DDL_Account2" runat="server" Visible="false">
                                                                </asp:DropDownList></td>
                                                            <td align="center">
                                                                <asp:Label ID="LB_Account3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Account3") %>'></asp:Label>
                                                                <asp:DropDownList ID="DDL_Account3" runat="server" Visible="false">
                                                                </asp:DropDownList></td>
                                                            <td align="center">
                                                                <asp:Label ID="LB_Account4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Account4") %>'></asp:Label>
                                                                <asp:DropDownList ID="DDL_Account4" runat="server" Visible="false">
                                                                </asp:DropDownList></td>
                                                        </tr>
                                                    </table>
                                                    </asp:Panel>
                                                </td>
                                                <td align="center"><asp:Button ID="BT_SettingSupplyChain" runat="server" Text="設定玩家" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ChainNum") %>' UseSubmitBehavior="false"/></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>                                    
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <td colspan="4" align="left" style="height: 24px">
                                            <asp:Button ID="Button2" runat="server" Text="新增一條供應鏈" OnClick="Button2_Click" />
                                            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="刪除一條供應鏈" />
                                        </td>
                                    </tr>
                                </tfoot>
                            </table>
                            </asp:Panel>
                        </div>
                        <div class="crumb">
                            <p>
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>&nbsp;</p>
                            <div class="blank">
                            </div>
                        </div>
                    </div>
                    <hr />
                </div>
                <div id="BOTTOM">
                    <p class="copyright">
                        &copy; 2009 Cluber 2.0. All Right Reserved.</p>
                </div>
            </div>
        </center>
    </form>
</body>
</html>

