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

public partial class admin_admin_script_edit : System.Web.UI.Page
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

        if (!IsPostBack)
        {
            int SID = Int32.Parse(Request["SID"].ToString());
            s.SetScenario(SID);
            TB_D_Delivery.Text = s.D_Delivery.ToString();
            TB_D_Order.Text = s.D_Order.ToString();
            TB_F_Delivery.Text = s.F_Delivery.ToString();
            TB_F_Order.Text = s.F_Order.ToString();
            TB_Name.Text = s.Name;
            TB_R_Delivery.Text = s.R_Delivery.ToString();
            TB_R_Order.Text = s.R_Order.ToString();
            TB_ShortCost.Text = s.ShortCost.ToString();
            TB_StockCost.Text = s.StockCost.ToString();
            TB_W_Delivery.Text = s.W_Delivery.ToString();
            TB_W_Order.Text = s.W_Order.ToString();
            TB_Week.Text = s.Week.ToString();
            TB_Yield.Text = s.Yield.ToString();
            if (s.IsSale == 1)
                Ckb_IsSale.Checked = true;
            else
                Ckb_IsSale.Checked = false;
            if (s.IsStock == 1)
                Ckb_IsStock.Checked = true;
            else
                Ckb_IsStock.Checked = false;
            DataTable DT = s.QueryMarketingRequest(SID);
            Hid_Memo.Value = DT.Rows[0]["Contents"].ToString();
            Hid_Request.Value = DT.Rows[0]["Amount"].ToString();
            Hid_TipWeek.Value = DT.Rows[0]["Tip_Week"].ToString();
            for (int i = 1; i < DT.Rows.Count; i++)
            {
                Hid_Memo.Value += "," + DT.Rows[i]["Contents"].ToString();
                Hid_Request.Value += "," + DT.Rows[i]["Amount"].ToString();
                Hid_TipWeek.Value += "," + DT.Rows[i]["Tip_Week"].ToString();
            }
        }
    }


    protected void BT_EditScenario_Click(object sender, EventArgs e)
    {
        int SID = Int32.Parse(Request["SID"].ToString());
        s.SetScenario(SID);
        DataTable DT = new DataTable();
        DT = g.GetStartList(0);
        for (int i = 0; i < DT.Rows.Count; i++)
            if (SID.ToString() == DT.Rows[i]["Scenario_ID"].ToString())
                break;
            else
            {
                int IsSale, IsStock;
                if (Ckb_IsSale.Checked == true)
                    IsSale = 1;
                else
                    IsSale = 0;
                if (Ckb_IsStock.Checked == true)
                    IsStock = 1;
                else
                    IsStock = 0;
                s.EditScenario(SID, TB_Name.Text, Int32.Parse(TB_F_Order.Text), Int32.Parse(TB_F_Delivery.Text), Int32.Parse(TB_D_Order.Text), Int32.Parse(TB_D_Delivery.Text), Int32.Parse(TB_W_Order.Text), Int32.Parse(TB_W_Delivery.Text), Int32.Parse(TB_R_Order.Text), Int32.Parse(TB_R_Delivery.Text), IsSale, IsStock, Int32.Parse(TB_StockCost.Text), Int32.Parse(TB_ShortCost.Text), float.Parse(TB_Yield.Text));

                string[] Week_Req = new string[s.Week];
                string[] Week_Memo = new string[s.Week];
                string[] Week_Tip = new string[s.Week];
                Week_Req = Hid_Request.Value.Split(',');
                Week_Memo = Hid_Memo.Value.Split(',');
                Week_Tip = Hid_TipWeek.Value.Split(',');
                for (int j = 0; j < s.Week; j++)
                    s.EditMarketingRequest(SID, j + 1, Int32.Parse(Week_Req[j]), Week_Memo[j], Int32.Parse(Week_Tip[j]));
                Response.Redirect("~/admin/admin_script.aspx");
            }
    }
}
