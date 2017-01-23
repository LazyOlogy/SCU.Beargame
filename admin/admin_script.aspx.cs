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

public partial class admin_admin_script : System.Web.UI.Page
{
    Account a = new Account();
    Game g = new Game();
    Scenario s = new Scenario();

    protected void Page_Load(object sender, EventArgs e)
    {
        //管理者檢查
        if (Session["admin"] == null)
        {
            Response.Redirect("../index.aspx");
        }

        SetInit();
    }

    protected void BT_CreateScenario_Click(object sender, EventArgs e)
    {
        int sale, stock, New_PK;
        if (Ckb_IsSale.Checked)
            sale = 1;
        else
            sale = 0;
        if (Ckb_IsStock.Checked)
            stock = 1;
        else
            stock = 0;
        New_PK=s.CreateScenario(TB_Name.Text, Int32.Parse(TB_F_Order.Text), Int32.Parse(TB_F_Delivery.Text), Int32.Parse(TB_D_Order.Text), Int32.Parse(TB_D_Delivery.Text), Int32.Parse(TB_W_Order.Text), Int32.Parse(TB_W_Delivery.Text), Int32.Parse(TB_R_Order.Text), Int32.Parse(TB_R_Delivery.Text), sale, stock, Int32.Parse(TB_StockCost.Text), Int32.Parse(TB_ShortCost.Text), float.Parse(TB_Yield.Text));

        string[] Week_Req = new string[Int32.Parse(TB_Week.Text)];
        string[] Week_Memo = new string[Int32.Parse(TB_Week.Text)];
        string[] Week_Tip = new string[Int32.Parse(TB_Week.Text)];
        Week_Req = Hid_Request.Value.Split(',');
        Week_Memo = Hid_Memo.Value.Split(',');
        Week_Tip = Hid_TipWeek.Value.Split(',');
        for (int i = 0; i < Week_Req.Length; i++)
            s.SetMarketingRequest(New_PK, i + 1, Int32.Parse(Week_Req[i].ToString()), Week_Memo[i].ToString(), Int32.Parse(Week_Tip[i].ToString()));
        SetInit();
    }

    protected void SetInit()
    {
        DataTable DT = new DataTable();
        DT = s.QueryScenarioList();
        Scenario_Repeater.DataSource = DT;
        Scenario_Repeater.DataBind();
        for (int i = 0; i < Scenario_Repeater.Items.Count; i++)
        {
            Label LB_Num = (Label)Scenario_Repeater.Items[i].FindControl("LB_Num");
            Label LB_Name = (Label)Scenario_Repeater.Items[i].FindControl("LB_Name");
            Label LB_Week = (Label)Scenario_Repeater.Items[i].FindControl("LB_Week");
            s.SetScenario(Int32.Parse(DT.Rows[i]["ID"].ToString()));
            LB_Num.Text = DT.Rows[i]["ID"].ToString();
            LB_Name.Text = s.Name;
            LB_Week.Text = s.Week.ToString();
        }
    }
    protected void BT_DelScenario_Click(object sender, EventArgs e)
    {
        string[] Num_Scenario = new string[Hid_Ckb.Value.Split(',').Length];
        Num_Scenario = Hid_Ckb.Value.Split(',');

       
            for (int i = 0; i < Num_Scenario.Length; i++)
            {
                s.DeleteMarketingRequest(Int32.Parse(Num_Scenario[i]));
                s.DeleteScenario(Int32.Parse(Num_Scenario[i]));
            }
        
    }

    protected void Scenario_Repeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
            Response.Redirect("~/admin/admin_script_edit.aspx?SID=" + e.CommandArgument.ToString());

    }

    protected void BT_Next_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/admin_game.aspx");
    }
}
