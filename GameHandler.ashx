<%@ WebHandler Language="C#" Class="GameHandler" %>

using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using System.Xml;
using System.Text;
using System.Data;
using BeerGame;

public class GameHandler : IHttpHandler {

    Scenario s = new Scenario();
    Game g = new Game();
    Account a = new Account();
    DataTable DT = new DataTable();
    
    public void ProcessRequest (HttpContext context) {

        int Week;
        bool Signal=false;
        int SID = Int32.Parse(context.Request["SID"].ToString());
        string AID = context.Request["AID"].ToString();

        DT = g.QueryGameInfo(Int32.Parse(g.QuerySupplyInfo(SID).Rows[0]["G_ID"].ToString()));

        if (context.Request["Week"] != null)
            Week = Int32.Parse(context.Request["Week"].ToString());
        else
            Week = Int32.Parse(g.Information(SID.ToString()).Rows.Count.ToString());

        a.SetAccount(Int32.Parse(AID));
        
        //工廠Week調整        
        if (a.Type == 1 && !g.Check_Synchron(DT.Rows[0]["ID"].ToString(), Week.ToString(), AID) && g.Check_Input(DT.Rows[0]["ID"].ToString(), Convert.ToString(Week), AID.ToString()))    //看看有沒有同步
        {
            Week--;
            Signal = true;
        }        
        
        
        //工廠要做的事情
        if (a.Type == 1 && g.Check_Input(DT.Rows[0]["ID"].ToString(), Convert.ToString(Week + 1), AID.ToString()))    //看看四個人有沒有輸入喔^_<
        {
            g.Synchron(DT.Rows[0]["ID"].ToString(), Convert.ToString(Week + 1), AID.ToString());   //同步囉~

            if (Signal)
                Week++;            
        }


        if (context.Request["REQ"] != null) //CASE：有需求數量時============================================================
        {
            string Req = context.Request["REQ"].ToString();

            if (!g.Check_Self(DT.Rows[0]["ID"].ToString(), Convert.ToString(Week + 1), AID) || Week == 0)
            {
                //新增
                if (Req != "")
                    g.Order(context.Request["SID"].ToString(), Convert.ToString(Week+1), Req);
            }
            GetSupplyInfo(SID, AID, Week + 1, context);
        }
        else{   //CASE：無需求數量時============================================================
            
            GetSupplyInfo(SID,AID,Week, context);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    public void GetSupplyInfo(int Supply_ID,string AID,int Week, HttpContext Context)    //產生XML物件
    {        

        DT = g.QueryGameInfo(Int32.Parse(g.QuerySupplyInfo(Supply_ID).Rows[0]["G_ID"].ToString()));
        
        s.SetScenario(Int32.Parse(DT.Rows[0]["Scenario_ID"].ToString()));
        int S_ID = 0;   //腳本ID

        if (g.Check_Synchron(DT.Rows[0]["ID"].ToString(), Week.ToString(), AID) || g.Information(Supply_ID.ToString()).Rows.Count == 0)
        {            
            Context.Response.Clear();
            Context.Response.ContentType = "text/xml";
            XmlTextWriter XmlTW = new XmlTextWriter(Context.Response.OutputStream, Encoding.UTF8);
            XmlTW.WriteStartDocument(true);
            XmlTW.WriteComment("SupplyInfo");
            XmlTW.WriteStartElement("Root");

            XmlTW.WriteStartElement("GAME");

            S_ID = Int32.Parse(DT.Rows[0]["Scenario_ID"].ToString());

            if (g.Information(Supply_ID.ToString()).Rows.Count == s.Week)   //遊戲狀態
            {
                XmlTW.WriteAttributeString("State", "0");
                g.DisableGame(Int32.Parse(DT.Rows[0]["ID"].ToString()));
            }
            else
                XmlTW.WriteAttributeString("State", "1");
            
            XmlTW.WriteAttributeString("Gid", DT.Rows[0]["ID"].ToString());
            XmlTW.WriteAttributeString("Week", s.Week.ToString());
            a.SetAccount(Int32.Parse(AID));
            s.SetScenario(S_ID);
            
            switch (a.Type) //判定角色決定送貨週期設定
            {
                case 1:
                    XmlTW.WriteAttributeString("Delivery_cycle", s.F_Delivery.ToString());
                    XmlTW.WriteAttributeString("Order_cycle", s.F_Order.ToString());
                    break;
                case 2:
                    XmlTW.WriteAttributeString("Delivery_cycle", s.D_Delivery.ToString());
                    XmlTW.WriteAttributeString("Order_cycle", s.D_Order.ToString());
                    break;
                case 3:
                    XmlTW.WriteAttributeString("Delivery_cycle", s.W_Delivery.ToString());
                    XmlTW.WriteAttributeString("Order_cycle", s.W_Order.ToString());
                    break;
                case 4:
                    XmlTW.WriteAttributeString("Delivery_cycle", s.R_Delivery.ToString());
                    XmlTW.WriteAttributeString("Order_cycle", s.R_Order.ToString());
                    break;
            }
            XmlTW.WriteElementString("TITLE", DT.Rows[0]["Name"].ToString());
            DT = s.QueryMarketingRequest(S_ID);
            string msg = "";
            for (int i = 0; i < DT.Rows.Count; i++) //設定事件訊息
                if (DT.Rows[i]["Tip_Week"].ToString() == Week.ToString())
                    msg += "第" + DT.Rows[i]["Week"].ToString() + "週時 " + DT.Rows[i]["Contents"].ToString();
            DT = g.Information(Supply_ID.ToString());
            try
            {
                msg += "<font color=\"red\" ><strong>本週到貨量 " + DT.Rows[DT.Rows.Count - 1]["Arrive_Amount"].ToString() + "</strong></font>";
            }
            catch (Exception) { }
            
            XmlTW.WriteElementString("INFO", msg);
            XmlTW.WriteEndElement();   //GAME

            XmlTW.WriteStartElement("ROUND", null);

            DataTable NextDT = new DataTable();
            int UpperLine = 0;//下游計量值
            
 
            for (int k = 0; k < DT.Rows.Count; k++) //自己腳色提出的需求量-自己的到貨量之累計
                UpperLine += Int32.Parse(DT.Rows[k]["Order_Amount"].ToString()) - Int32.Parse(DT.Rows[k]["Arrive_Amount"].ToString());

            try
            {
                XmlTW.WriteAttributeString("LowerLine", DT.Rows[DT.Rows.Count - 1]["Total_Short"].ToString());
                XmlTW.WriteAttributeString("UpperLine", UpperLine.ToString());
            }
            catch (Exception)
            {
            }
            
            
            for (int i = 0; i < DT.Rows.Count; i++) //抓取每週資訊(已發生)
            {
                XmlTW.WriteStartElement("Week");
                XmlTW.WriteAttributeString("Arrive_Amount", DT.Rows[i]["Arrive_Amount"].ToString());
                XmlTW.WriteAttributeString("Stock_Amount", DT.Rows[i]["Stock_Amount"].ToString());
                XmlTW.WriteAttributeString("Total_Short", "-" + DT.Rows[i]["Total_Short"].ToString());  //就是自己的計量值Order-Arrive
                XmlTW.WriteAttributeString("Request_Amount", DT.Rows[i]["Request_Amount"].ToString());
                XmlTW.WriteAttributeString("Order_Amount", DT.Rows[i]["Order_Amount"].ToString());
                XmlTW.WriteAttributeString("Cost_Amount", DT.Rows[i]["Cost_Amount"].ToString());
                XmlTW.WriteAttributeString("Send_Amount", DT.Rows[i]["Send_Amount"].ToString());
                XmlTW.WriteAttributeString("Total_Cost", DT.Rows[i]["Total_Cost"].ToString());
                XmlTW.WriteAttributeString("Market_Amount", s.QueryMarketingRequest(S_ID, i + 1).Rows[0]["Amount"].ToString());
                int step = 0;
                switch (a.Type)
                {
                    case 1:
                        step = 3;
                        break;
                    case 2:
                        step = 2;
                        break;
                    case 3:
                        step = 1;
                        break;
                    case 4:
                        break;
                }

                int total_stock = 0;
                for (int j = 1; j < step + 1; j++)
                {
                    NextDT = g.Information(Convert.ToString(Supply_ID + j));
                    for (int k = 0; k < i; k++)
                        total_stock += Int32.Parse(NextDT.Rows[k]["Stock_Amount"].ToString());
                }

                if (a.Type != 4 && s.IsStock != 0)
                    XmlTW.WriteAttributeString("Total_Stock", total_stock.ToString());
                else
                    XmlTW.WriteAttributeString("Total_Stock", "- -");

                XmlTW.WriteEndElement();   //Week
            }
            XmlTW.WriteEndElement();   //ROUND

            XmlTW.WriteEndElement();   //Root
            XmlTW.Flush();
            XmlTW.Close();
            Context.Response.End();
            
        }
        else
        {
            Context.Response.Write(null);
        }
    }
}