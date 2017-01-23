using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;

/// <summary>
/// DBClass 的摘要描述
/// </summary>

namespace BeerGame
{
    public class DBClass
    {
        private string DBName = ConfigurationManager.ConnectionStrings["BeerGameConnection"].ConnectionString;

        //資料表定義
        private string ACCOUNT = ConfigurationManager.AppSettings["ACCOUNT"],
                       GAMELIST = ConfigurationManager.AppSettings["GAMELIST"],
                       GAMERECORD = ConfigurationManager.AppSettings["GAMERECORD"],
                       MARKETING_REQUEST = ConfigurationManager.AppSettings["MARKETING_REQUEST"],
                       SUPPLYCHAIN = ConfigurationManager.AppSettings["SUPPLYCHAIN"],
                       SCENARIO = ConfigurationManager.AppSettings["SCENARIO"];

        public DBClass()
        {
        }

        /*--------------------------------------------Database Setting--------------------------------------------*/

        public SqlConnection GetConn()  //取得連線
        {
            return new SqlConnection(DBName);
        }

        public DataTable DB_Query(string SqlStr)    //資料表查詢
        {
            SqlConnection conn = GetConn();
            conn.Open();
            SqlDataAdapter comd = new SqlDataAdapter(SqlStr, conn);
            DataTable DT = new DataTable();
            try
            {
                comd.Fill(DT);

            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex);
            }
            finally
            {
                comd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            return DT;
        }

        public Boolean DB_CmdExecution(string SqlStr)   //資料新增、刪除的指令
        {
            SqlConnection conn = GetConn();
            conn.Open();
            SqlCommand comd = new SqlCommand(SqlStr, conn);
            try
            {
                comd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                //HttpContext.Current.Response.Write(ex);
                return false;
            }
            finally
            {
                comd.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }

        public int DB_CmdExecution(string Str, string PK)   //資料新增、刪除的指令（帶PK值）
        {
            Int32 PK_New = 0;
            string SqlStr = Str + PK;
            SqlConnection conn = GetConn();
            conn.Open();
            SqlCommand comd = new SqlCommand(SqlStr, conn);
            try
            {
                PK_New = Int32.Parse(comd.ExecuteScalar().ToString());
            }
            catch (Exception)
            {
                //HttpContext.Current.Response.Write(ex);
            }
            finally
            {
                comd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            return PK_New;
        }



        /*--------------------------------------------Database Setting--------------------------------------------*/

        /*--------------------------------------------------Account--------------------------------------------------*/


        /*--------------------------------------------------Account--------------------------------------------------*/




        /*--------------------------------------------------Game_PLAY--------------------------------------------------*/
        /*使用者ID可進行的遊戲列表*/
        public DataTable DB_QueryAvailableGameList(int A_id)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT dbo.SupplyChain.A_ID, dbo.SupplyChain.ID, dbo.GameList.Scenario_ID, dbo.GameList.Name, dbo.GameList.State, dbo.GameList.Memo,dbo.GameList.ID AS G_id, dbo.SupplyChain.ChainNum FROM dbo.GameList INNER JOIN dbo.SupplyChain ON dbo.GameList.ID = dbo.SupplyChain.G_ID WHERE (dbo.SupplyChain.A_ID = "+A_id+")";
            return DB_Query(SqlStr);
        }
        
        /*找出SupplyChain的ID,ID是使用者帳號ID*/
        public DataTable DB_GetSupplyChainID(int GameID,int ID)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT ID FROM " + SUPPLYCHAIN + " WHERE G_ID=" + GameID.ToString() + "AND A_ID=" + ID.ToString();
            return DB_Query(SqlStr);
        }
        /*抓取良率*/
        public DataTable DB_GetYield(string G_ID)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT dbo.Scenario.Yield FROM dbo.GameList INNER JOIN dbo.Scenario ON dbo.GameList.Scenario_ID = dbo.Scenario.ID WHERE (dbo.GameList.ID ='" +  G_ID + "')";
            return DB_Query(SqlStr);
        }
        /*找出其他角色的訂單*/
        public DataTable DB_GetOtherAmount(string SupplyID, string Week)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT Order_Amount FROM " + GAMERECORD + " WHERE Supply_ID='" + SupplyID + "' AND Week='" + Week + "'";
            return DB_Query(SqlStr);
        }
        /*讀取庫存數量*/
        public DataTable DB_GetStockAmount(string SupplyID, string Week)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT Stock_Amount FROM " + GAMERECORD + " WHERE Supply_ID='" + SupplyID + "' AND Week='" + Week + "'";
            return DB_Query(SqlStr);
        }
        /*讀取缺貨數量*/
        public DataTable DB_GetShortAmount(string SupplyID, string Week)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT Total_Short FROM " + GAMERECORD + " WHERE Supply_ID='" + SupplyID + "' AND Week='" + Week + "'";
            return DB_Query(SqlStr);
        }
        /*讀取到貨數量*/
        public DataTable DB_GetArriveAmount(string SupplyID, string Week)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT Arrive_Amount FROM " + GAMERECORD + " WHERE Supply_ID=" + SupplyID + "AND Week=" + Week;
            return DB_Query(SqlStr);
        }
        /*讀取累計庫存*/
        public DataTable DB_GetTotalCost(string SupplyID, string Week)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT Total_Cost FROM " + GAMERECORD + " WHERE Supply_ID='" + SupplyID + "' AND Week='" + Week + "'";
            return DB_Query(SqlStr);
        }
        /*讀取到貨數量*/
        public DataTable DB_GetArriveAmount(int Supply_ID, int Week)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT Arrive_Amount FROM " + GAMERECORD + " WHERE Supply_ID=" + Supply_ID.ToString() + "AND Week=" + Week.ToString();
            return DB_Query(SqlStr);
        }

        /*讀取已經開始的遊戲*/
        public DataTable DB_GetStartList(int State)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT ID, NAME, Scenario_ID, Supply_Amount, Memo, State FROM " + GAMELIST + " WHERE State=" + State.ToString() ;
            return DB_Query(SqlStr);
        }
        /*讀取使用者可參予的遊戲*/
        public DataTable DB_GetAccountGameList(string A_ID)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT * FROM " + SUPPLYCHAIN + " WHERE A_ID='" + A_ID + "'";
            return DB_Query(SqlStr);
        }
        /*讀取已經某腳本的遊戲列表*/
        public DataTable DB_GetGameList(int SID)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT * FROM " + GAMELIST + " WHERE Scenario_ID= " + SID;
            return DB_Query(SqlStr);
        }

        /*讀取SupplyID該周的資訊*/
        public DataTable DB_GetRecordList(string SupplyID,string Week)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT * FROM " + GAMERECORD + " WHERE Supply_ID= '" + SupplyID + "' AND Week ='" + Week + "'";
            return DB_Query(SqlStr);
        }

        /*讀取SupplyID該周的資訊(多載)*/
        public DataTable DB_GetRecordList(string SupplyID)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT * FROM " + GAMERECORD + " WHERE Supply_ID= '" + SupplyID + "'";
            return DB_Query(SqlStr);
        }


        /*讀取遊戲中不同角色的的使用者*/
        public DataTable DB_GetUserList(string Index,int Type)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT dbo.Scenario.ID, dbo.GameList.ID AS GL_ID, dbo.SupplyChain.A_ID, dbo.Account.ID AS Expr3, dbo.Account.Name AS A_NAME FROM dbo.Scenario INNER JOIN dbo.GameList ON dbo.Scenario.ID = dbo.GameList.Scenario_ID INNER JOIN dbo.SupplyChain ON dbo.GameList.ID = dbo.SupplyChain.G_ID INNER JOIN dbo.Account ON dbo.SupplyChain.A_ID = dbo.Account.ID WHERE (dbo.Scenario.ID = "+ Index +") AND (dbo.Account.Type = " + Type +")";
            return DB_Query(SqlStr);
        }


        /*建立紀錄並且存入每個星期訂單的數量*/
        public Boolean DB_SetOrderAmount(string SupplyID, string Week, string OrderAmount)
        {
            string SqlStr = string.Empty;
            SqlStr = "INSERT INTO " + GAMERECORD + " ([Supply_ID],[Week],[Arrive_Amount],[Stock_Amount],[Order_Amount],[Cost_Amount],[Total_Cost],[Total_Short],[Synchron]) VALUES ('" + SupplyID + "','" + Week + "', '0' , '0' ,'" + OrderAmount + "' , '0' , '0' , '0' , 'false')";
            //SqlStr = "UPDATE " + GAMERECORD + " SET [Order_Amount] = '" + OrderAmount + "' WHERE Supply_ID='" + SupplyID + "' AND Week='" + Week + "'";
            return DB_CmdExecution(SqlStr);
        }
        /*紀錄庫存成本*/
        public Boolean DB_SetTotalCost(string SupplyID, string TotalCost, string Week)
        {
            string SqlStr = string.Empty;
            SqlStr = "UPDATE " + GAMERECORD + " SET [Total_Cost] = '" + TotalCost + "' WHERE Supply_ID='" + SupplyID +"' AND Week='" + Week + "'";
            return DB_CmdExecution(SqlStr);
        }
        /*紀錄其他角色的數量要求
        public Boolean DB_SetRequestAmount(int Supply_ID, int RequestAmount)
        {
            string SqlStr = string.Empty;
            SqlStr = "UPDATE " + GAMERECORD + " SET [Request_Amount] = " + RequestAmount + " WHERE Supply_ID=" + Supply_ID.ToString();
            return DB_CmdExecution(SqlStr);
        }
        */ 
        /*紀錄當期成本*/
        public Boolean DB_SetCostAmount(string SupplyID, string CostAmount,string Week)
        {
            string SqlStr = string.Empty;
            SqlStr = "UPDATE " + GAMERECORD + " SET [Cost_Amount] = '" + CostAmount + "' WHERE Supply_ID='" + SupplyID + "' AND Week='" + Week + "'";
            return DB_CmdExecution(SqlStr);
        }
        /*計算庫存數量*/
        public Boolean DB_SetStockAmount(string SupplyID, string StockAmount,string Week)
        {
            string SqlStr = string.Empty;
            SqlStr = "UPDATE " + GAMERECORD + " SET [Stock_Amount] = '" + StockAmount + "' WHERE Supply_ID='" + SupplyID + "' AND Week='" + Week + "'";
            return DB_CmdExecution(SqlStr);
        }
        /*讀取該使用者可以加入的遊戲*/
        public DataTable DB_GetUserGame(string A_ID)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT dbo.GameList.* FROM dbo.GameList INNER JOIN dbo.SupplyChain ON dbo.GameList.ID = dbo.SupplyChain.G_ID WHERE (dbo.SupplyChain.A_ID ='" + A_ID + "')";
            return DB_Query(SqlStr);

        }
        /*讀取該遊戲的每周資訊*/
        public DataTable DB_GetGameWeekinfo(int G_ID)
        {

            string SqlStr = string.Empty;
            SqlStr = "SELECT DISTINCT dbo.Marketing_Request.* FROM dbo.GameList INNER JOIN dbo.Scenario ON dbo.GameList.Scenario_ID = dbo.Scenario.ID INNER JOIN dbo.Marketing_Request ON dbo.Scenario.ID = dbo.Marketing_Request.Scenario_ID WHERE (dbo.GameList.Scenario_ID ='" + G_ID + "')";
            return DB_Query(SqlStr);

        }
        /*得到自己是第幾條供應鏈*/
        public DataTable DB_GetSupplyChianNum(string G_ID, string A_ID)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT ChainNum FROM " + SUPPLYCHAIN + " WHERE G_ID='" + G_ID + "' AND A_ID='" + A_ID + "'";
            return DB_Query(SqlStr);
        }
        /*得到此供應鏈的四個ID*/
        public DataTable DB_GetChianNumID(string G_ID, string ChainNum)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT ID FROM " + SUPPLYCHAIN + " WHERE G_ID='" + G_ID + "' AND ChainNum='" + ChainNum + "'";
            return DB_Query(SqlStr);
        }
        /*找出供應鏈下訂單了沒*/
        public DataTable DB_FindOrder(string SupplyID, string Week)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT Supply_ID FROM " + GAMERECORD + " WHERE Supply_ID='" + SupplyID + "' AND Week='" + Week + "' AND (NOT (Order_Amount IS NULL))";
            return DB_Query(SqlStr);
        }
        /*找出供應鏈同步了沒*/
        public DataTable DB_FindSynchron(string SupplyID, string Week)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT Supply_ID FROM " + GAMERECORD + " WHERE Supply_ID='" + SupplyID + "' AND Week='" + Week + "' AND Synchron='true'";
            return DB_Query(SqlStr);
        }
        /*找到當週市場需求*/
        public DataTable DB_GetWeekAmount(string G_ID, string Week)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT dbo.Marketing_Request.Amount FROM dbo.GameList INNER JOIN dbo.Marketing_Request ON dbo.GameList.Scenario_ID = dbo.Marketing_Request.Scenario_ID INNER JOIN dbo.Scenario ON dbo.GameList.Scenario_ID = dbo.Scenario.ID AND dbo.Marketing_Request.Scenario_ID = dbo.Scenario.ID WHERE (dbo.GameList.ID ='" + G_ID + "') AND (dbo.Marketing_Request.Week ='" +  Week + "')";
            return DB_Query(SqlStr);
        }
        /*紀錄Request_Amount數量*/
        public Boolean DB_SetRequestAmount(string SupplyID, string RequestAmount,string Week)
        {
            string SqlStr = string.Empty;
            if (RequestAmount == null)
                RequestAmount = "0";
            SqlStr = "UPDATE " + GAMERECORD + " SET [Request_Amount] = '" + RequestAmount + "' WHERE Supply_ID='" + SupplyID + "' AND Week='" + Week + "'";
            //SqlStr = "INSERT INTO " + GAMERECORD + " ([Supply_ID],[Week],[Request_Amount]) VALUES ('" + SupplyID + "','" + Week + "','" + RequestAmount + "')";
            return DB_CmdExecution(SqlStr);
        }
        /*紀錄到貨(Arrive)數量*/
        public Boolean DB_SetArriveAmount(string SupplyID, string ArriveAmount, string Week)
        {
            string SqlStr = string.Empty;
            if (ArriveAmount == null)
                ArriveAmount = "0";
            SqlStr = "UPDATE " + GAMERECORD + " SET [Arrive_Amount] = '" + ArriveAmount + "' WHERE Supply_ID='" + SupplyID + "' AND Week='" + Week + "'";
            //SqlStr = "INSERT INTO " + GAMERECORD + " ([Supply_ID],[Week],[Request_Amount]) VALUES ('" + SupplyID + "','" + Week + "','" + RequestAmount + "')";
            return DB_CmdExecution(SqlStr);
        }
        /*紀錄Total_Short數量*/
        public Boolean DB_SetShortAmount(string SupplyID, string ShortAmount, string Week)
        {
            string SqlStr = string.Empty;
            SqlStr = "UPDATE " + GAMERECORD + " SET [Total_Short] = '" + ShortAmount + "' WHERE Supply_ID='" + SupplyID + "' AND Week='" + Week + "'";
            //SqlStr = "INSERT INTO " + GAMERECORD + " ([Supply_ID],[Week],[Request_Amount]) VALUES ('" + SupplyID + "','" + Week + "','" + RequestAmount + "')";
            return DB_CmdExecution(SqlStr);
        }
        /*紀錄Send_Amount數量*/
        public Boolean DB_SetSendAmount(string SupplyID, string SendAmount, string Week)
        {
            string SqlStr = string.Empty;
            SqlStr = "UPDATE " + GAMERECORD + " SET [Send_Amount] = '" + SendAmount + "' WHERE Supply_ID='" + SupplyID + "' AND Week='" + Week + "'";
            //SqlStr = "INSERT INTO " + GAMERECORD + " ([Supply_ID],[Week],[Request_Amount]) VALUES ('" + SupplyID + "','" + Week + "','" + RequestAmount + "')";
            return DB_CmdExecution(SqlStr);
        }


        /*找出訂單和運輸的延遲時間*/
        public DataTable DB_FindDelayWeek(string G_ID, string TypeNAME)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT dbo.Scenario." + TypeNAME + " FROM dbo.GameList INNER JOIN dbo.Scenario ON dbo.GameList.Scenario_ID = dbo.Scenario.ID WHERE (dbo.GameList.ID ='" + G_ID + "')";
            return DB_Query(SqlStr);
        }
        /*讀取送貨資料*/
        public DataTable DB_FindAllAmount(string SupplyID, string Week)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT Stock_Amount, Request_Amount, Total_Short FROM " + GAMERECORD + " WHERE Supply_ID='" + SupplyID + "' AND Week='" + Week + "'";
            return DB_Query(SqlStr);
        }
        /*讀取發送了多少貨務(Send_Amount)*/
        public DataTable DB_GetSendAmount(string SupplyID, string Week)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT Send_Amount FROM " + GAMERECORD + " WHERE Supply_ID='" + SupplyID + "' AND Week='" + Week + "'";
            return DB_Query(SqlStr);
        }
        /*讀取遊戲的腳本成本*/
        public DataTable DB_GetCost(string G_ID, string CostType)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT dbo.Scenario." + CostType + " FROM dbo.GameList INNER JOIN dbo.Scenario ON dbo.GameList.Scenario_ID = dbo.Scenario.ID WHERE (dbo.GameList.ID = '" + G_ID + "')";
            return DB_Query(SqlStr);
        }
        /*設定已經同步過的角色和周數*/
        public Boolean DB_SetSynchron(string TypeNum, string Week)
        {
            string SqlStr = string.Empty;
            SqlStr = "UPDATE " + GAMERECORD + " SET [Synchron] = 'true' WHERE Supply_ID='" + TypeNum + "' AND Week='" + Week + "'";
            return DB_CmdExecution(SqlStr);
        }


        public DataTable DB_QuerySupplyInfo(int Supply_ID)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT * FROM " + SUPPLYCHAIN + " WHERE [ID]= " + Supply_ID;
            return DB_Query(SqlStr);
        }
        public DataTable DB_QueryGameInfo(int G_ID)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT * FROM " + GAMELIST + " WHERE [ID]= " + G_ID;
            return DB_Query(SqlStr);
        }



        /****************************************************Game_Admin***************************************/


        /*關閉遊戲*/
        public Boolean DB_DisableGame(int ID)
        {
            string SqlStr = string.Empty;
            SqlStr = "UPDATE " + GAMELIST + " SET [State] = '0' WHERE (ID = " + ID + ")" ;
            return DB_CmdExecution(SqlStr);
        }
        /*開啟遊戲*/
        public Boolean DB_EnableGame(int ID)
        {
            string SqlStr = string.Empty;
            SqlStr = "UPDATE " + GAMELIST + " SET [State] = '1' WHERE (ID = " + ID + ")";
            return DB_CmdExecution(SqlStr);
        }
        /*創造遊戲*/
        public int DB_CreateGame(string Name,string Memo,int Scenario_ID,string Supply_Amount)
        {
            string SqlStr = string.Empty;
            SqlStr = "INSERT INTO " + GAMELIST + " ([Name],[Scenario_ID],[Supply_Amount],[Memo],[State]) VALUES ('" + Name + "'," + Scenario_ID + ",'" + Supply_Amount +"','" + Memo + "',0)";
            return DB_CmdExecution(SqlStr, "SELECT @@IDENTITY");
        }
        /*讀取Game*/
        public DataTable DB_GetGame(string ID)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT * FROM " + GAMELIST + " WHERE ID= '" + ID + "'";
            return DB_Query(SqlStr);
        }
        /*設定遊戲*/
        public Boolean DB_EditGame(string ID,string Name,int S_ID,string Memo)
        {
            string SqlStr = string.Empty;
            SqlStr = "UPDATE " + GAMELIST + " SET [Name] = '" + Name + "',[Scenario_ID] = '" + S_ID + "',[Memo] = '" + Memo +"' WHERE (ID = " + ID + ")";
            return DB_CmdExecution(SqlStr);
        }
        /*讀取遊戲角色*/
        public DataTable DB_GetAccountType(int Type)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT * FROM " + ACCOUNT  + " WHERE Type= " + Type;
            return DB_Query(SqlStr);
        }
        /*讀取帳號角色*/
        public DataTable DB_GetAccount_Type(string ID)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT * FROM " + ACCOUNT + " WHERE ID= '" + ID + "'";
            return DB_Query(SqlStr);
        }
        //設定供應鏈角色帳號
        public Boolean DB_SetSupplyAccount(int G_ID,int SupplyNum,int FC,int Big,int Mid,int Sma)
        {
            Boolean  Flag;
            DB_DelSupplyChain(G_ID, SupplyNum);

            string SqlStr = string.Empty;
            SqlStr = "INSERT INTO " + SUPPLYCHAIN + " ([G_ID],[ChainNum],[A_ID]) VALUES (" + G_ID + "," + SupplyNum + "," + FC  + ")";
            Flag =  DB_CmdExecution(SqlStr);
            SqlStr = "INSERT INTO " + SUPPLYCHAIN + " ([G_ID],[ChainNum],[A_ID]) VALUES (" + G_ID + "," + SupplyNum + "," + Big + ")";
            Flag = DB_CmdExecution(SqlStr);
            SqlStr = "INSERT INTO " + SUPPLYCHAIN + " ([G_ID],[ChainNum],[A_ID]) VALUES (" + G_ID + "," + SupplyNum + "," + Mid + ")";
            Flag = DB_CmdExecution(SqlStr);
            SqlStr = "INSERT INTO " + SUPPLYCHAIN + " ([G_ID],[ChainNum],[A_ID]) VALUES (" + G_ID + "," + SupplyNum + "," + Sma + ")";
            return DB_CmdExecution(SqlStr);
        }

        //建立初始供應鏈
        public Boolean DB_InitSupplyChain(string G_ID,string Supply_Amount)
        {
            Boolean Flag;
            Flag = false;
            string SqlStr = string.Empty;
            for (int i = 1; i <= Int32.Parse(Supply_Amount); i++) 
                for(int j = 1;j < 5;j++){

                SqlStr = "INSERT INTO " + SUPPLYCHAIN + " ([G_ID],[ChainNum]) VALUES ('" + G_ID + "','" + i + "')";
                Flag = DB_CmdExecution(SqlStr);
            }
            return Flag;
        }
        //增加一條供應鏈
        public Boolean DB_AddSupplyChain(string G_ID, string Supply_Amount)
        {
            Boolean Flag;
            Flag = false;
            string SqlStr = string.Empty;

            for(int j = 1;j < 5;j++){
            SqlStr = "INSERT INTO " + SUPPLYCHAIN + " ([G_ID],[ChainNum]) VALUES ('" + G_ID + "','" + Supply_Amount + "')";
            Flag = DB_CmdExecution(SqlStr);
            }

            return Flag;
        }

        //刪除供應鏈
        public Boolean DB_DelSupplyChain(int G_ID, int Supply_Amount)
        {
            string SqlStr = string.Empty;
            SqlStr = "DELETE FROM " + SUPPLYCHAIN + " WHERE G_ID=" + G_ID + " AND ChainNum = " + Supply_Amount;
            return DB_CmdExecution(SqlStr);
        }

        //刪除供應鏈帳號
        public Boolean DB_DelSupplyChainAccount(int G_ID, int Supply_Amount,int AID)
        {
            string SqlStr = string.Empty;
            SqlStr = "UPDATE " + SUPPLYCHAIN + " SET [A_ID] = NULL WHERE G_ID=" + G_ID + " AND ChainNum = " + Supply_Amount + " AND A_ID = " + AID;
            return DB_CmdExecution(SqlStr);
        }


        //取得遊戲供應鏈列表
        public DataTable DB_GetSupplyChainList(int GID)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT dbo.SupplyChain.G_ID, dbo.SupplyChain.ChainNum,dbo.SupplyChain.ID, dbo.Account.Name, dbo.SupplyChain.A_ID FROM " + GAMELIST + " INNER JOIN " + SUPPLYCHAIN + " ON dbo.GameList.ID = dbo.SupplyChain.G_ID  LEFT OUTER JOIN " + ACCOUNT + " ON dbo.SupplyChain.A_ID = dbo.Account.ID WHERE (dbo.SupplyChain.G_ID = " + GID + ") ORDER BY ChainNum";
            //SqlStr = "SELECT DISTINCT ChainNum FROM " + SUPPLYCHAIN + " WHERE G_ID= '" + GID + "' ORDER BY ChainNum";
            return DB_Query(SqlStr);
        }
        //取得供應鏈的最大數目
        public DataTable DB_GetMaxSupplyChain(string ID)
        {
            string SqlStr = string.Empty;
            //SqlStr = "SELECT COUNT(DISTINCT ChainNum) FROM " + SUPPLYCHAIN + " WHERE G_ID= '" + ID + "' ORDER BY ChainNum";
            SqlStr = "SELECT COUNT(DISTINCT ChainNum) FROM " + SUPPLYCHAIN + " WHERE G_ID= '" + ID + "'";
            return DB_Query(SqlStr);

        }
        /*更新Gamelist的供應鏈樹木*/
        public Boolean DB_EditGameListSupply(string G_ID, string ChainAmount)
        {
            string SqlStr = string.Empty;
            SqlStr = "UPDATE " + GAMELIST + " SET [Supply_Amount] = '" + ChainAmount + "' WHERE (ID = " + G_ID + ")";
            return DB_CmdExecution(SqlStr);
        }

        //刪除Game的Record
        public Boolean DB_DelGameRecord(int SupplyID)
        {
            string SqlStr = string.Empty;
            SqlStr = "DELETE FROM " + GAMERECORD + " WHERE Supply_ID=" + SupplyID;
            return DB_CmdExecution(SqlStr);
        }
        //刪除Game的全部供應鍊
        public Boolean DB_DelGameSupply(int GID)
        {
            string SqlStr = string.Empty;
            SqlStr = "DELETE FROM " + SUPPLYCHAIN + " WHERE G_ID=" + GID;
            return DB_CmdExecution(SqlStr);
        }
        //刪除Game
        public Boolean DB_DelGame(int GID)
        {
            string SqlStr = string.Empty;
            SqlStr = "DELETE FROM " + GAMELIST + " WHERE ID=" + GID;
            return DB_CmdExecution(SqlStr);
        }

        //取得此遊戲某供應鍊的記錄列表
        public DataTable DB_GetGameRecordList(int SupplyID)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT * FROM " + GAMERECORD + " WHERE Supply_ID= '" + SupplyID + "'";
            return DB_Query(SqlStr);
        }       


        /*--------------------------------------------------Scenario--------------------------------------------------*/

        /*讀取腳本*/
        public DataTable DB_SetScenario(int ID)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT * FROM " + SCENARIO + " WHERE ID=" + ID.ToString();
            return DB_Query(SqlStr);
        }

        /*創造腳本*/
        public int DB_CreateScenario(string Name, int F_Order, int F_Delivery, int D_Order, int D_Delivery, int W_Order, int W_Delivery, int R_Order, int R_Delivery, int IsSale, int IsStock, int StockCost, int ShortCost, float Yield)
        {
            string SqlStr = string.Empty;
            SqlStr = "INSERT INTO " + SCENARIO + " ([Name],[F_Order],[F_Delivery],[D_Order],[D_Delivery],[W_Order],[W_Delivery],[R_Order],[R_Delivery],[IsSale],[IsStock],[StockCost],[ShortCost],[Yield]) VALUES ('" + Name + "'," + F_Order + "," + F_Delivery + "," + D_Order + "," + D_Delivery + "," + W_Order + "," + W_Delivery + "," + R_Order + "," + R_Delivery + "," + IsSale + "," + IsStock + "," + StockCost + "," + ShortCost + "," + Yield + ")";
            return DB_CmdExecution(SqlStr, "SELECT @@IDENTITY");
        }

        /*編輯腳本*/
        public Boolean DB_EditScenario(int ID, string Name, int F_Order, int F_Delivery, int D_Order, int D_Delivery, int W_Order, int W_Delivery, int R_Order, int R_Delivery, int IsSale, int IsStock, int StockCost, int ShortCost, float Yield)
        {
            string SqlStr = string.Empty;
            SqlStr = "UPDATE " + SCENARIO + " SET [Name] = '" + Name + "'," + "[F_Order] = " + F_Order + ",[F_Delivery] = " + F_Delivery + ",[D_Order] = " + D_Order + ",[D_Delivery] = " + D_Delivery + ",[W_Order] = " + W_Order + ",[W_Delivery] = " + W_Delivery + ",[R_Order] = " + R_Order + ",[R_Delivery] = " + R_Delivery + ",[IsSale] = " + IsSale + ",[IsStock] = " + IsStock + ",[StockCost] = " + StockCost + ",[ShortCost] = " + ShortCost + ",[Yield] = " + Yield + " WHERE ID=" + ID.ToString();
            return DB_CmdExecution(SqlStr);
        }

        /*刪除腳本*/
        public Boolean DB_DeleteScenario(int ID)
        {
            string SqlStr = string.Empty;
            SqlStr = "DELETE FROM " + SCENARIO + " WHERE ID=" + ID;
            if (!DB_CmdExecution(SqlStr))
            {
                HttpContext.Current.Response.Write("<script language=javascript>alert('請先移除腳本包含的遊戲')</script>");                
                return false;
            }
            return true;
        }

        /*設定腳本每週市場需求資訊*/
        public Boolean DB_SetMarketingRequest(int Scenario_ID, int Week, int Amount, string Contents, int Tip_Week)
        {
            string SqlStr = string.Empty;
            SqlStr = "INSERT INTO " + MARKETING_REQUEST + " ([Scenario_ID],[Week],[Amount],[Contents],[Tip_Week]) VALUES (" + Scenario_ID + "," + Week + "," + Amount + ",'" + Contents + "'," + Tip_Week + ")";
            return DB_CmdExecution(SqlStr);
        }

        /*查詢腳本每週市場需求資訊*/
        public DataTable DB_QueryMarketingRequest(int Scenario_ID, int Week)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT * FROM " + MARKETING_REQUEST + " WHERE [Scenario_ID]=" + Scenario_ID + " AND [Week]=" + Week;
            return DB_Query(SqlStr);
        }

        /*查詢腳本市場需求資訊*/
        public DataTable DB_QueryMarketingRequest(int Scenario_ID)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT * FROM " + MARKETING_REQUEST + " WHERE [Scenario_ID]=" + Scenario_ID;
            return DB_Query(SqlStr);
        }

        /*修改腳本每週市場需求資訊*/
        public Boolean DB_EditMarketingRequest(int Scenario_ID, int Week, int Amount, string Contents, int Tip_Week)
        {
            string SqlStr = string.Empty;
            SqlStr = " INSERT INTO " + SCENARIO + " ([Amount],[Contents],[Tip_Week]) VALUE (" + Amount + ",'" + Contents + "'," + Tip_Week + ") WHERE [Scenario_ID]=" + Scenario_ID + " AND [Week]=" + Week;
            return DB_CmdExecution(SqlStr);
        }

        /*刪除腳本市場需求資訊*/
        public Boolean DB_DeleteMarketingRequest(int Scenario_ID)
        {
            string SqlStr = string.Empty;
            SqlStr = "DELETE FROM " + MARKETING_REQUEST + " WHERE [Scenario_ID]=" + Scenario_ID;
            return DB_CmdExecution(SqlStr);
        }

        /*查詢腳本列表*/
        public DataTable DB_QueryScenarioList()
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT * FROM " + SCENARIO;
            return DB_Query(SqlStr);
        }
        /*查詢腳本列表名稱*/
        public DataTable DB_QueryScenarioNAME()
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT ID,NAME FROM " + SCENARIO;
            return DB_Query(SqlStr);
        }

        /*--------------------------------------------------Account--------------------------------------------------*/

        /*取得此帳戶資訊*/
        internal DataTable DB_GetAccount(int ID)
        {            
            string SqlStr = "SELECT * FROM " + ACCOUNT + " WHERE [ID]=" + ID;
            return DB_Query(SqlStr);
        }

        /*帳密檢測*/
        internal DataTable DB_CheckAccount(string Name, string PW)
        {
            string SqlStr = "SELECT * FROM " + ACCOUNT + " WHERE (Name = '" + Name + "') AND (Password = '" + PW + "')";
            return DB_Query(SqlStr);
        }

        /*帳戶重覆檢測*/
        internal DataTable DB_CheckAccount(string Name)
        {
            string SqlStr = "SELECT * FROM " + ACCOUNT + " WHERE [Name] = '" + Name + "'";
            return DB_Query(SqlStr);
        }

        /*建立新帳戶*/
        internal bool DB_CreateAccount(string Name, string Password, string Mail, string Memo, int Type)
        {
            string SqlStr = "INSERT INTO " + ACCOUNT + " ([Name],[Password],[Mail],[Memo],[Type]) VALUES ('" + Name + "','" + Password + "','" + Mail + "','" + Memo + "'," + Type + ")";
            return DB_CmdExecution(SqlStr);
        }

        /*修改帳戶*/
        internal bool DB_EditAccount(int ID, string Name, string Password, string Mail, string Memo, int Type)
        {
            string SqlStr = "UPDATE " + ACCOUNT + " SET [Password] = '" + Password + "',[Mail] = '" + Mail + "',[Memo] = '" + Memo + "',[Type]=" + Type + " WHERE [ID] =" + ID;
            return DB_CmdExecution(SqlStr);
        }

        /*刪除帳戶*/
        internal bool DB_DeleteAccount(int ID)
        {
            string SqlStr = "DELETE FROM " + ACCOUNT + " WHERE [ID]=" + ID;
            return DB_CmdExecution(SqlStr);
        }

        //查詢特定角色之帳戶
        public DataTable DB_QueryAccountList(int Type)
        {
            string SqlStr = "SELECT * FROM " + ACCOUNT + " WHERE [Type]=" + Type;
            return DB_Query(SqlStr);
        }







        /*行政特區*/
        public DataTable DB_QueryChainNum(int G_id, int A_id)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT * FROM " + SUPPLYCHAIN + " WHERE (G_ID = " + G_id + ") AND (A_ID = " + A_id + ")";
            return DB_Query(SqlStr);
        }

        public DataTable DB_QueryChainNum(int G_id) //查詢供應鏈數目
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT DISTINCT G_ID, ChainNum FROM " + SUPPLYCHAIN + " WHERE(G_ID = " + G_id + ")";
            return DB_Query(SqlStr);
        }

        public DataTable DB_QuerySupplyList(int G_id, int ChainNum)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT * FROM " + SUPPLYCHAIN + " WHERE [G_ID]= " + G_id + " AND [ChainNum]=" + ChainNum;
            return DB_Query(SqlStr);
        }

        public DataTable DB_CheckChainNum(int Supply_ID, int WeekNow)
        {
            string SqlStr = string.Empty;
            SqlStr = "SELECT * FROM " + GAMERECORD + " WHERE [Supply_ID]= " + Supply_ID + " AND [Week]=" + WeekNow;
            return DB_Query(SqlStr);
        }
    }
}