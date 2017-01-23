using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BeerGame;

/// <summary>
/// Scenario 的摘要描述
/// </summary>
public class Scenario
{
    public int F_Order, F_Delivery, D_Order, D_Delivery, W_Order, W_Delivery, R_Order, R_Delivery, IsSale, IsStock, StockCost, ShortCost, Week;
    public float Yield;
    public string Name;
    DBClass obj = new DBClass();

    /*初始化設定*/
	public Scenario()
	{
        Name = string.Empty;
        F_Order = 0;
        F_Delivery = 0;
        D_Order = 0;
        D_Delivery = 0;
        W_Order = 0;
        W_Delivery = 0;
        R_Order = 0;
        R_Delivery = 0;
        IsSale = 0;
        IsStock = 0;
        StockCost = 1;
        ShortCost = 2;
        Week = 0;
        Yield = 1.0F;
	}

    /*讀取腳本*/
    public void SetScenario(int ID)
    {
        DataTable DT = new DataTable();
        DT = obj.DB_SetScenario(ID);
        Name = DT.Rows[0]["Name"].ToString();
        F_Order = Int32.Parse(DT.Rows[0]["F_Order"].ToString());
        F_Delivery = Int32.Parse(DT.Rows[0]["F_Delivery"].ToString());
        D_Order = Int32.Parse(DT.Rows[0]["D_Order"].ToString());
        D_Delivery = Int32.Parse(DT.Rows[0]["D_Delivery"].ToString());
        W_Order = Int32.Parse(DT.Rows[0]["W_Order"].ToString());
        W_Delivery = Int32.Parse(DT.Rows[0]["W_Delivery"].ToString());
        R_Order = Int32.Parse(DT.Rows[0]["R_Order"].ToString());
        R_Delivery = Int32.Parse(DT.Rows[0]["R_Delivery"].ToString());
        IsSale = Int32.Parse(DT.Rows[0]["IsSale"].ToString());
        IsStock = Int32.Parse(DT.Rows[0]["IsStock"].ToString());
        StockCost = Int32.Parse(DT.Rows[0]["StockCost"].ToString());
        ShortCost = Int32.Parse(DT.Rows[0]["ShortCost"].ToString());
        Yield = float.Parse(DT.Rows[0]["Yield"].ToString());
        Week = obj.DB_QueryMarketingRequest(ID).Rows.Count;
    }

    /*創造腳本*/
    public int CreateScenario(string Name, int F_Order, int F_Delivery, int D_Order, int D_Delivery, int W_Order, int W_Delivery, int R_Order, int R_Delivery, int IsSale, int IsStock, int StockCost, int ShortCost, float Yield)
    {
        return obj.DB_CreateScenario(Name, F_Order, F_Delivery, D_Order, D_Delivery, W_Order, W_Delivery, R_Order, R_Delivery, IsSale, IsStock, StockCost, ShortCost, Yield);
    }

    /*編輯腳本*/
    public Boolean EditScenario(int ID, string Name, int F_Order, int F_Delivery, int D_Order, int D_Delivery, int W_Order, int W_Delivery, int R_Order, int R_Delivery, int IsSale, int IsStock, int StockCost, int ShortCost, float Yield)
    {
        return obj.DB_EditScenario(ID, Name, F_Order, F_Delivery, D_Order, D_Delivery, W_Order, W_Delivery, R_Order, R_Delivery, IsSale, IsStock, StockCost, ShortCost, Yield);
    }

    /*刪除腳本*/
    public Boolean DeleteScenario(int ID)
    {
        return obj.DB_DeleteScenario(ID);
    }

    /*設定腳本每週市場需求資訊*/
    public Boolean SetMarketingRequest(int Scenario_ID, int Week, int Amount, string Contents, int Tip_Week)
    {
        return obj.DB_SetMarketingRequest(Scenario_ID, Week, Amount, Contents, Tip_Week);
    }

    /*查詢腳本每週市場需求資訊*/
    public DataTable QueryMarketingRequest(int Scenario_ID, int Week)
    {
        return obj.DB_QueryMarketingRequest(Scenario_ID, Week);
    }

    /*查詢腳本市場需求資訊*/
    public DataTable QueryMarketingRequest(int Scenario_ID)
    {
        return obj.DB_QueryMarketingRequest(Scenario_ID);
    }

    /*修改腳本每週市場需求資訊*/
    public Boolean EditMarketingRequest(int Scenario_ID, int Week, int Amount, string Contents, int Tip_Week)
    {
        return obj.DB_EditMarketingRequest(Scenario_ID, Week, Amount, Contents, Tip_Week);
    }

    /*刪除腳本市場需求資訊*/
    public Boolean DeleteMarketingRequest(int Scenario_ID)
    {
        return obj.DB_DeleteMarketingRequest(Scenario_ID);
    }

    /*查詢腳本列表*/
    public DataTable QueryScenarioList()
    {
        return obj.DB_QueryScenarioList();
    }

}
