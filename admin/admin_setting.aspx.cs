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
using System.Data.SqlClient;
using BeerGame;
public partial class admin_admin_setting : System.Web.UI.Page
{
    Account a = new Account();
    Game g = new Game();
    Scenario s = new Scenario();
    DBClass obj = new DBClass();
    DataTable DT = new DataTable();
    public int G_ID,PAGE;

    protected void Page_Load(object sender, EventArgs e)
    {
        //管理者檢查
        if (Session["admin"] == null)
        {
            Response.Redirect("../index.aspx");
        }

        if (Request["PAGE"] == null)
        {
            PAGE = 0;
        }
        else
        {
            PAGE = Int32.Parse(Request["PAGE"].ToString());            
        }

        if (PAGE == 0)
        {
            PN_SupplySetting.Visible = false;
            HL_GameSetting.CssClass = "active";
        }
        else if (PAGE == 1)
        {
            PN_GameSetting.Visible = false;
            HL_SupplySetting.CssClass = "active";
        }

        G_ID = Int32.Parse(Request["ID"].ToString());

        HL_GameSetting.NavigateUrl = "admin_setting.aspx?ID=" + G_ID + "&PAGE=0";
        HL_SupplySetting.NavigateUrl = "admin_setting.aspx?ID=" + G_ID + "&PAGE=1";
        

        if (!IsPostBack)
        {   

            DT = g.GetScenarioNAME();
            DDL_Scenario.DataSource = DT;
            DDL_Scenario.DataTextField = "NAME";
            DDL_Scenario.DataValueField = "ID";
            DDL_Scenario.DataBind();

            DT = g.GetGame(G_ID.ToString());
            DDL_Scenario.SelectedValue = DT.Rows[0]["Scenario_ID"].ToString();
            TextBox1.Text = DT.Rows[0]["Name"].ToString();
            TextBox2.Text = DT.Rows[0]["Memo"].ToString();
            Label2.Text = DT.Rows[0]["Name"].ToString();

            DT = g.GetSupplyChainList(G_ID);

            DataView DV = new DataView(DT);
            DataTable TempDT = new DataTable();

            TempDT = DV.ToTable(true, new string[] { "ChainNum" });
            TempDT.Columns.Add("Account1");
            TempDT.Columns.Add("Account2");
            TempDT.Columns.Add("Account3");
            TempDT.Columns.Add("Account4");

            for (int i = 0; i < TempDT.Rows.Count; i++)
            {
                DV = new DataView(DT);
                DV.RowFilter = "ChainNum=" + (i + 1).ToString();
                TempDT.Rows[i]["Account1"] = DV.ToTable(false, new string[] { "Name" }).Rows[0][0].ToString();
                TempDT.Rows[i]["Account2"] = DV.ToTable(false, new string[] { "Name" }).Rows[1][0].ToString();
                TempDT.Rows[i]["Account3"] = DV.ToTable(false, new string[] { "Name" }).Rows[2][0].ToString();
                TempDT.Rows[i]["Account4"] = DV.ToTable(false, new string[] { "Name" }).Rows[3][0].ToString();
            }

            GameListRepeater.DataSource = TempDT;
            GameListRepeater.DataBind();
        }

    }

    protected void BT_EditGame_Click(object sender, EventArgs e)
    {
        int S_ID;
        string ID;
        ID = Request["ID"];
        if (DDL_Scenario.SelectedIndex >= 1)
        {
            S_ID = Int32.Parse(DDL_Scenario.SelectedValue);
            //Label1 .Text = ID + TextBox1.Text + S_ID.ToString() + TextBox2.Text;
            if (TextBox1.Text != "" || S_ID >= 0)
            {                
                g.EditGame(ID, TextBox1.Text, S_ID, TextBox2.Text);
            }
        }

        Response.Redirect("admin_game.aspx");
    }
    
    protected void Button2_Click(object sender, EventArgs e) //增加供應鏈
    {
        DataTable DT = new DataTable();
        int G_ID;
        int MaxNum;
        G_ID = Int32.Parse(Request["ID"].ToString());
        DT = obj.DB_GetMaxSupplyChain(G_ID.ToString());
        MaxNum = Int32.Parse(DT.Rows[0][0].ToString());
        MaxNum++;
        obj.DB_AddSupplyChain(G_ID.ToString(), MaxNum.ToString());
        obj.DB_EditGameListSupply(G_ID.ToString(), MaxNum.ToString());

        Response.Redirect(Request.UrlReferrer.ToString());
        
      
    }
    protected void Button3_Click(object sender, EventArgs e) //刪除供應鏈
    {
        DataTable DT = new DataTable();
        int G_ID;
        int MaxNum;
        G_ID = Int32.Parse(Request["ID"].ToString());
        DT = obj.DB_GetMaxSupplyChain(G_ID.ToString());
        MaxNum = Int32.Parse(DT.Rows[0][0].ToString());
        obj.DB_DelSupplyChain(G_ID, MaxNum);
        MaxNum--;
        obj.DB_EditGameListSupply(G_ID.ToString(), MaxNum.ToString());
        Response.Redirect(Request.UrlReferrer.ToString());        
    }

    protected void RP_GameListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        G_ID = Int32.Parse(Request["ID"].ToString());
        Button SettingBT = (Button)e.CommandSource;
        if (SettingBT.CommandName == "Edit")
        {            
            Panel PN = (Panel)GameListRepeater.Items[Int32.Parse(e.CommandArgument.ToString()) - 1].FindControl("PN_Setting");
            Style css = new Style();           
            css.BorderWidth = 5;       
            PN.MergeStyle(css);

            for (int i = 1; i <= GameListRepeater.Items.Count; i++)
            {
                Button BT = (Button)GameListRepeater.Items[i-1].FindControl("BT_SettingSupplyChain");

                if (i == Int32.Parse(e.CommandArgument.ToString()))
                {
                    BT.Text = "確定修改";
                    BT.CommandName = "Setting";
                }
                else
                    BT.Visible = false;
            }

            for (int i = 1; i <= 4; i++)
            {
                DropDownList DDL = (DropDownList)GameListRepeater.Items[Int32.Parse(e.CommandArgument.ToString()) - 1].FindControl("DDL_Account" + i);
                Label LB = (Label)GameListRepeater.Items[Int32.Parse(e.CommandArgument.ToString()) - 1].FindControl("LB_Account" + i);

                DDL.Visible = true;
                LB.Visible = false;

                DT = g.GetAccountType(i);
                DDL.DataSource = DT;
                DDL.DataTextField = "Name";
                DDL.DataValueField = "ID";
                DDL.DataBind();
            }

        }
        else if (SettingBT.CommandName == "Setting")
        {
            DropDownList DDL1 = (DropDownList)GameListRepeater.Items[Int32.Parse(e.CommandArgument.ToString()) - 1].FindControl("DDL_Account1");
            DropDownList DDL2 = (DropDownList)GameListRepeater.Items[Int32.Parse(e.CommandArgument.ToString()) - 1].FindControl("DDL_Account2");
            DropDownList DDL3 = (DropDownList)GameListRepeater.Items[Int32.Parse(e.CommandArgument.ToString()) - 1].FindControl("DDL_Account3");
            DropDownList DDL4 = (DropDownList)GameListRepeater.Items[Int32.Parse(e.CommandArgument.ToString()) - 1].FindControl("DDL_Account4");

            g.SetSupplyAccount(G_ID, Int32.Parse(e.CommandArgument.ToString()),Int32.Parse(DDL1.SelectedValue), Int32.Parse(DDL2.SelectedValue), Int32.Parse(DDL3.SelectedValue),Int32.Parse(DDL4.SelectedValue));
            
            DT = g.GetSupplyChainList(G_ID);

            for (int i = 1; i <= GameListRepeater.Items.Count; i++)
            {
                for (int j = 0; j < DT.Rows.Count; j++)
                {
                    if (i != Int32.Parse(e.CommandArgument.ToString()) && DT.Rows[j]["A_ID"].ToString() == DDL1.SelectedValue)
                    {
                        g.DelSupplyChainAccount(G_ID, i, Int32.Parse(DDL1.SelectedValue));
                    }
                    if (i != Int32.Parse(e.CommandArgument.ToString()) && DT.Rows[j]["A_ID"].ToString() == DDL2.SelectedValue)
                    {
                        g.DelSupplyChainAccount(G_ID, i, Int32.Parse(DDL2.SelectedValue));
                    }
                    if (i != Int32.Parse(e.CommandArgument.ToString()) && DT.Rows[j]["A_ID"].ToString() == DDL3.SelectedValue)
                    {
                        g.DelSupplyChainAccount(G_ID, i, Int32.Parse(DDL3.SelectedValue));
                    }
                    if (i != Int32.Parse(e.CommandArgument.ToString()) && DT.Rows[j]["A_ID"].ToString() == DDL4.SelectedValue)
                    {
                        g.DelSupplyChainAccount(G_ID, i, Int32.Parse(DDL4.SelectedValue));
                    }
                }
            }

            Response.Redirect(Request.UrlReferrer.ToString());
        }
    }
}
