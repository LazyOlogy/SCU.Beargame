using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BeerGame;

public partial class admin_admin_game : System.Web.UI.Page
{
    Game g = new Game();
    DBClass obj = new DBClass();
    DataTable DT = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        //管理者檢查
        if (Session["admin"] == null)
        {
            Response.Redirect("../index.aspx");
        }

        if (DropDownList1.Items.Count <= 0)
        {
            DropDownList1.Items.Clear();
            DT = g.GetScenarioNAME();
            DataView DV = new DataView(DT);
            DropDownList1.DataSource = DV;
            DropDownList1.DataTextField = "NAME";
            DropDownList1.DataValueField = "ID";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "<-- Select -->");
            
        }

        if (DropDownList2.Items.Count <= 0)
        {
            DropDownList2.Items.Clear();
            DT = g.GetScenarioNAME();
            DataView DV = new DataView(DT);
            DropDownList2.DataSource = DV;
            DropDownList2.DataTextField = "NAME";
            DropDownList2.DataValueField = "ID";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, "<-- Select -->");
        }        
        
    }

    protected void Selection_Change(Object sender, EventArgs e)
    {
        //Response.Write("<script>alert(" + DropDownList1.SelectedValue + ")</script>");
        char[] charsToTrim = { '.' };

        if (DropDownList1.SelectedIndex != 0)
        {   
            DT = g.GetGameList(Int32.Parse(DropDownList1.SelectedValue));
            DT.Columns.Add("TotalWeek");
            DT.Columns.Add("PlayWeek");
            DT.Columns.Add("StateInfo");

            DataTable temp = new DataTable();

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                DT.Rows[i]["TotalWeek"] = g.GetGameWeekinfo(Int32.Parse(DT.Rows[i]["Scenario_ID"].ToString())).Rows.Count;
                try
                {
                    DT.Rows[i]["PlayWeek"] = g.GetGameRecordList(Int32.Parse(g.GetSupplyChainList(Int32.Parse(DT.Rows[i]["ID"].ToString())).Rows[0]["ID"].ToString())).Rows.Count;
                }
                catch (Exception)
                {
                    DT.Rows[i]["PlayWeek"] = 0;
                }
            }

            for (int i = 0; i < DT.Rows.Count; i++)
            {                
                if (g.GetSupplyChainList(Int32.Parse(DT.Rows[i]["ID"].ToString())).Rows.Count == 0)
                {
                    DT.Rows[i]["StateInfo"] = "<img src=\"images/icon_06.gif\" />未設定";
                    DT.Rows[i]["State"] = "2";
                }
                else if (DT.Rows[i]["State"].ToString() == "0")
                {
                    if(DT.Rows[i]["PlayWeek"].ToString()==DT.Rows[i]["TotalWeek"].ToString())
                    {
                        DT.Rows[i]["StateInfo"] = "<img src=\"images/icon_05.gif\" />己結束";
                        DT.Rows[i]["State"] = "3";
                    }
                    else
                        DT.Rows[i]["StateInfo"] = "<img src=\"images/icon_07.gif\" />未開始";
                }
                else if (DT.Rows[i]["State"].ToString() == "1")
                {
                    DT.Rows[i]["StateInfo"] = "<img src=\"images/icon_04.gif\" />己開始";
                }
            }

            GameList_Repeater.DataSource = DT;
            GameList_Repeater.DataBind();

            //整理遊戲控制Button
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                if(DT.Rows[i]["State"].ToString()=="0")
                {
                    Button BT = (Button)GameList_Repeater.Items[i].FindControl("BT_Start");
                    BT.Visible = true;
                }
                else if (DT.Rows[i]["State"].ToString() == "1")
                {
                    Button BT = (Button)GameList_Repeater.Items[i].FindControl("BT_End");
                    BT.Visible = true;
                }
                else if (DT.Rows[i]["State"].ToString() == "3")
                {
                    Button BT = (Button)GameList_Repeater.Items[i].FindControl("BT_Restart");
                    BT.Visible = true;
                }
            }

        }
    }
    protected void GameListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Button GameList_Detail = (Button)e.CommandSource;
        if (GameList_Detail.CommandName == "Edit")
        {
            Response.Redirect("admin_setting.aspx?ID=" + Int32.Parse(e.CommandArgument.ToString()));
        }
        else if (GameList_Detail.CommandName == "Start")
        {
            g.EnableGame(Int32.Parse(e.CommandArgument.ToString()));
            Response.Redirect("admin_game.aspx");
        }
        else if (GameList_Detail.CommandName == "End")
        {
            g.DisableGame(Int32.Parse(e.CommandArgument.ToString()));
            Response.Redirect("admin_game.aspx");
        }
        else if (GameList_Detail.CommandName == "Restart")
        {
            g.RestartGame(Int32.Parse(e.CommandArgument.ToString()));
            Response.Redirect("admin_game.aspx");
        }

    }

    protected void BT_CreateGame_Click(object sender, EventArgs e)
    {
        int S_ID;
        if (DropDownList2.SelectedIndex >= 1)
        {
            S_ID = Int32.Parse(DropDownList2.SelectedValue);
            if (TextBox1.Text != "" || S_ID >= 0)
            {
                g.CreateGame(TextBox1.Text, TextBox2.Text, S_ID,TextBox3.Text);
            }
        }

        Response.Redirect(Request.UrlReferrer.ToString());
    }

    protected void BT_DelGame_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < GameList_Repeater.Items.Count; i++)
        {            
            CheckBox CB = (CheckBox)GameList_Repeater.Items[i].FindControl("CB_CheckGame");
            if (CB.Checked)
                g.DelGame(Int32.Parse(CB.ToolTip));
        }

        Response.Redirect(Request.UrlReferrer.ToString());
    }
}
