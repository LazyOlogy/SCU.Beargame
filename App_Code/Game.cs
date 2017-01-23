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

/// <summary>
/// Game 的摘要描述
/// </summary>
/// 
public class Game
{
    DBClass obj = new DBClass();
    public string Week, SupplyID, GameID, ID, OrderAmount, TotalCost, RequestAmount, CostAmount, StockAmount, ArriveAmount;
   
    //init
	public Game()
	{
        
        
        Week = string.Empty;
        SupplyID = string.Empty;
        GameID = string.Empty;
        ID = string.Empty;
        OrderAmount = string.Empty;
        TotalCost = string.Empty;
        RequestAmount = string.Empty;
        CostAmount = string.Empty;
        StockAmount = string.Empty;
        ArriveAmount = string.Empty;
        
	}



    /******************************PLAYING******************************/
    public Boolean Check_Self(string G_ID, string Week, string A_ID)
    {
        Boolean Flag;
        int ChainNum, temp;
        int[] TypeNum = new int[4];

        DataTable DT = new DataTable();

        Flag = false;
        //檢查是否此供應鏈上所有的角色都下完訂單1.先找出這是哪條供應鏈2.再用供應鏈找出四個supply然後和當周星期比對
        //看是不是有四個，都下完訂單了

        DT = obj.DB_GetSupplyChianNum(G_ID, A_ID);
        ChainNum = Int32.Parse(DT.Rows[0][0].ToString());

        //然後再依序得到該供應鏈的角色，從零售、中游、大盤、工廠
        DT = obj.DB_GetChianNumID(G_ID, ChainNum.ToString());
        for (int i = 0; i < 4; i++)
            TypeNum[i] = Int32.Parse(DT.Rows[i]["ID"].ToString());

        for (int i = 0; i < 4; i++)
            for (int j = i; j < 4; j++)
            {
                if (TypeNum[i] < TypeNum[j])//將零售放到位置0中盤放到1大盤放到2工廠放到3
                {
                    temp = TypeNum[i];
                    TypeNum[i] = TypeNum[j];
                    TypeNum[j] = temp;
                }
            }


        DT = obj.DB_GetAccount(Int32.Parse(A_ID));
        ChainNum = Int32.Parse(DT.Rows[0]["Type"].ToString());

        ChainNum = 4 - ChainNum;


        Flag = true;

        DT = obj.DB_FindOrder(TypeNum[ChainNum].ToString(), Week);
        if (DT.Rows.Count == 0)
            Flag = false;
        



        return Flag;

    }



    public Boolean Check_Input(string G_ID, string Week, string A_ID)
    {
        Boolean Flag;
        int ChainNum, temp;
        int[] TypeNum = new int[4];
        
        DataTable DT = new DataTable();
        
        Flag = false;
        //檢查是否此供應鏈上所有的角色都下完訂單1.先找出這是哪條供應鏈2.再用供應鏈找出四個supply然後和當周星期比對
        //看是不是有四個，都下完訂單了

        DT = obj.DB_GetSupplyChianNum(G_ID, A_ID);
        ChainNum = Int32.Parse(DT.Rows[0][0].ToString());

        //然後再依序得到該供應鏈的角色，從零售、中游、大盤、工廠
        DT = obj.DB_GetChianNumID(G_ID, ChainNum.ToString());
        for (int i = 0; i < 4; i++)
            TypeNum[i] = Int32.Parse(DT.Rows[i]["ID"].ToString());

        for (int i = 0; i < 4; i++)
            for (int j = i; j < 4; j++)
            {
                if (TypeNum[i] < TypeNum[j])//將零售放到位置0中盤放到1大盤放到2工廠放到3
                {
                    temp = TypeNum[i];
                    TypeNum[i] = TypeNum[j];
                    TypeNum[j] = temp;
                }
            }

        Flag = true;
        DT = obj.DB_FindOrder(TypeNum[0].ToString(), Week);
        if (DT.Rows.Count == 0)
            Flag = false;
        DT = obj.DB_FindOrder(TypeNum[1].ToString(), Week);
        if (DT.Rows.Count == 0)
            Flag = false;
        DT = obj.DB_FindOrder(TypeNum[2].ToString(), Week);
        if (DT.Rows.Count == 0)
            Flag = false;
        DT = obj.DB_FindOrder(TypeNum[3].ToString(), Week);
        if (DT.Rows.Count == 0)
            Flag = false;



        return Flag;
    }
    public Boolean Check_Synchron(string G_ID, string Week, string A_ID)
    {
        Boolean Flag;
        int ChainNum, temp;
        int[] TypeNum = new int[4];
        DataTable DT = new DataTable();
        Flag = false;
        //檢查是否此供應鏈上所有的角色都下完訂單1.先找出這是哪條供應鏈2.再用供應鏈找出四個supply然後和當周星期比對
        //看是不是有四個，都下完訂單了

        DT = obj.DB_GetSupplyChianNum(G_ID, A_ID);
        ChainNum = Int32.Parse(DT.Rows[0][0].ToString());
        //然後再依序得到該供應鏈的角色，從零售、中游、大盤、工廠
        DT = obj.DB_GetChianNumID(G_ID, ChainNum.ToString());
        for (int i = 0; i < 4; i++)
            TypeNum[i] = Int32.Parse(DT.Rows[i]["ID"].ToString());

        for (int i = 0; i < 4; i++)
            for (int j = i; j < 4; j++)
            {
                if (TypeNum[i] < TypeNum[j])//將零售放到位置0中盤放到1大盤放到2工廠放到3
                {
                    temp = TypeNum[i];
                    TypeNum[i] = TypeNum[j];
                    TypeNum[j] = temp;
                }
            }

        Flag = true;
        DT = obj.DB_FindSynchron(TypeNum[0].ToString(), Week);
        if (DT.Rows.Count == 0)
            Flag = false;
        DT = obj.DB_FindSynchron(TypeNum[1].ToString(), Week);
        if (DT.Rows.Count == 0)
            Flag = false;
        DT = obj.DB_FindSynchron(TypeNum[2].ToString(), Week);
        if (DT.Rows.Count == 0)
            Flag = false;
        DT = obj.DB_FindSynchron(TypeNum[3].ToString(), Week);
        if (DT.Rows.Count == 0)
            Flag = false;
        return Flag;
        
    }



    public Boolean Synchron(string G_ID,string Week,string A_ID)
    {
        Boolean Flag;
        int ChainNum,temp;
        int[] TypeNum = new int[4];
        string Week_New;






        
        DataTable DT = new DataTable();
        DataTable DT_Week = new DataTable();



        Flag = false;
        //檢查是否此供應鏈上所有的角色都下完訂單1.先找出這是哪條供應鏈2.再用供應鏈找出四個supply然後和當周星期比對
        //看是不是有四個，都下完訂單了

        DT = obj.DB_GetSupplyChianNum(G_ID, A_ID);
        ChainNum = Int32.Parse(DT.Rows[0][0].ToString());



        //然後再依序得到該供應鏈的角色，從零售、中游、大盤、工廠
        DT = obj.DB_GetChianNumID(G_ID, ChainNum.ToString());
        for (int i = 0; i < 4; i++)
            TypeNum[i] = Int32.Parse(DT.Rows[i]["ID"].ToString());
                
        for(int i = 0;i<4;i++)
            for (int j = i; j < 4; j++)
            {
                if (TypeNum[i] < TypeNum[j])//將零售放到位置0中盤放到1大盤放到2工廠放到3
                {
                    temp = TypeNum[i];
                    TypeNum[i] = TypeNum[j];
                    TypeNum[j] = temp;
                }
            }
        //先得到supplychain的號碼然後比對有沒有四條有的話代表四個角色都下完訂單了
        /*
        for (int i = 0; i < 4; i++)
        {

            DT = obj.DB_FindOrder(TypeNum[i].ToString(), Week);
            if (DT.Rows.Count == 0)
            {
                Flag = false;
            }
            else
            {
                Flag = true;
            }
            
        }
        */
        Flag = true;
        DT = obj.DB_FindOrder(TypeNum[0].ToString(), Week);
        if (DT.Rows.Count == 0)
            Flag = false;
        DT = obj.DB_FindOrder(TypeNum[1].ToString(), Week);
        if (DT.Rows.Count == 0)
            Flag = false;
        DT = obj.DB_FindOrder(TypeNum[2].ToString(), Week);
        if (DT.Rows.Count == 0)
            Flag = false;
        DT = obj.DB_FindOrder(TypeNum[3].ToString(), Week);
        if (DT.Rows.Count == 0)
            Flag = false;


        if (Flag == true)
        {
            //先得到該星期的市場需求量
            DT = obj.DB_GetWeekAmount(G_ID, Week);


            //temp = Int32.Parse(Week) + Int32.Parse(DT_Week.Rows[0][0].ToString());
            //Week = temp.ToString();
            //再分別把該星期市場需求量放入零售的要求數量、零售的訂單放到中游的要求數量、以此類推
            obj.DB_SetRequestAmount(TypeNum[0].ToString(), DT.Rows[0][0].ToString(), Week);





            //在得到腳本的訂單的延遲周數
            DT_Week = obj.DB_FindDelayWeek(G_ID, "R_Order");
            temp = Int32.Parse(Week) - Int32.Parse(DT_Week.Rows[0][0].ToString());
            Week_New = temp.ToString();
            DT = obj.DB_GetOtherAmount(TypeNum[0].ToString(), Week_New);
            if (DT.Rows.Count == 0)
            {
                int[] Zero = new int[1];
                Zero[0] = 0;
                DT.Columns.Add("OtherRequest");
                DT.Rows.Add(Zero[0].ToString());

            }
            obj.DB_SetRequestAmount(TypeNum[1].ToString(), DT.Rows[0][0].ToString(), Week);



            DT_Week = obj.DB_FindDelayWeek(G_ID, "W_Order");
            temp = Int32.Parse(Week) - Int32.Parse(DT_Week.Rows[0][0].ToString());
            Week_New = temp.ToString();
            DT = obj.DB_GetOtherAmount(TypeNum[1].ToString(), Week_New);
            if (DT.Rows.Count == 0)
            {
                int[] Zero = new int[1];
                Zero[0] = 0;
                DT.Columns.Add("OtherRequest");
                DT.Rows.Add(Zero[0].ToString());

            }
            obj.DB_SetRequestAmount(TypeNum[2].ToString(), DT.Rows[0][0].ToString(), Week);

            DT_Week = obj.DB_FindDelayWeek(G_ID, "D_Order");
            temp = Int32.Parse(Week) - Int32.Parse(DT_Week.Rows[0][0].ToString());
            Week_New = temp.ToString();
            DT = obj.DB_GetOtherAmount(TypeNum[2].ToString(), Week_New);
            if (DT.Rows.Count == 0)
            {
                int[] Zero = new int[1];
                Zero[0] = 0;
                DT.Columns.Add("OtherRequest");
                DT.Rows.Add(Zero[0].ToString());

            }
            obj.DB_SetRequestAmount(TypeNum[3].ToString(), DT.Rows[0][0].ToString(), Week);

            /*
            DT = obj.DB_GetOtherAmount(TypeNum[3].ToString(), Week);
            DT_Week = obj.DB_FindOrderWeek(G_ID, "F_Order");
            temp = Int32.Parse(Week) - Int32.Parse(DT_Week.Rows[0][0].ToString());
            Week_New = temp.ToString();
            obj.DB_SetRequestAmount(TypeNum[3].ToString(), DT.Rows[0][0].ToString(), Week_New);
            */
            //抓取到貨數量，並且發送貨物出去1.先讓工廠的貨進貨存入Arrive_Amount，然後將Arrive_Amount和庫存相加
            //2.讀取工廠下游的訂單，從自己的庫存扣，然後存入Send_Amount中，
            //3.然後不夠的要累加到Total_Short(總欠貨)中
            
            Arrive_Short(G_ID, Week, TypeNum);




            //然後算出每一條供應鏈的每個角色的成本回存1.先算庫存成本2.再算缺貨成本

            CountCost(G_ID, Week, TypeNum);

            //將每個供應鏈的角色當周已經同步完的synchron設定為true
            for(int i = 0;i<4;i++)
            obj.DB_SetSynchron(TypeNum[i].ToString(), Week);

            //Flag = true;


        }
        else
        {
            return Flag;
        }



        return Flag;

    }


    public void Arrive_Short(string G_ID, string Week, int[] TypeNum)
    {
        //Boolean Flag;
        DataTable DT = new DataTable();
        DataTable DT_Delay = new DataTable();
        int Delay,StockAmount,RequestAmount,ShortAmount,ShortNum;
        string Week_New;
        /*******************************Factory***********************************/
        StockAmount = 0;
        Delay = 0;
        DT_Delay = obj.DB_FindDelayWeek(G_ID, "F_Order");
        Delay = Delay + Int32.Parse(DT_Delay.Rows[0][0].ToString());
        DT_Delay = obj.DB_FindDelayWeek(G_ID, "F_Delivery");
        Delay = Delay + Int32.Parse(DT_Delay.Rows[0][0].ToString());
        Delay = Int32.Parse(Week) - Delay;
        Week_New = Delay.ToString();
        //Label1.Text = Week_New;
        DT = obj.DB_GetOtherAmount(TypeNum[3].ToString(), Week_New);
        if (DT.Rows.Count == 0)
        {
            int[] Zero = new int[1];
            Zero[0] = 0;
            DT.Columns.Add("Amount");
            DT.Rows.Add(Zero[0].ToString());

        }
        obj.DB_SetArriveAmount(TypeNum[3].ToString(), CountArrive(G_ID, DT.Rows[0][0].ToString()).ToString(), Week);
        
        DT = obj.DB_GetArriveAmount(TypeNum[3].ToString(), Week);
        StockAmount = StockAmount + Int32.Parse(DT.Rows[0][0].ToString());
        Delay = Int32.Parse(Week) - 1;
        DT = obj.DB_GetStockAmount(TypeNum[3].ToString(), Delay.ToString());
        if (DT.Rows.Count == 0)
        {
            int[] Zero = new int[1];
            Zero[0] = 0;
            DT.Columns.Add("Amount");
            DT.Rows.Add(Zero[0].ToString());

        }
        StockAmount = StockAmount + Int32.Parse(DT.Rows[0][0].ToString());
        obj.DB_SetStockAmount(TypeNum[3].ToString(), StockAmount.ToString(),Week);
         
        DT = obj.DB_GetShortAmount(TypeNum[3].ToString(), Delay.ToString());
        if (DT.Rows.Count == 0)
        {
            int[] Zero = new int[1];
            Zero[0] = 0;
            DT.Columns.Add("Amount");
            DT.Rows.Add(Zero[0].ToString());

        }
        
        obj.DB_SetShortAmount(TypeNum[3].ToString(), DT.Rows[0][0].ToString(), Week);
        
        //RequestAmount,StockAmount,ShortAmount
        DT = obj.DB_FindAllAmount(TypeNum[3].ToString(), Week);
        if (DT.Rows.Count == 0)
        {
            DT.Columns.Add("Stock_Amount");
            DT.Columns.Add("Request_Amount");
            DT.Columns.Add("Total_Short");
            DT.Rows.Add("0", "0", "0");
        }
        StockAmount = Int32.Parse(DT.Rows[0]["Stock_Amount"].ToString());
        RequestAmount = Int32.Parse(DT.Rows[0]["Request_Amount"].ToString());
        ShortAmount = Int32.Parse(DT.Rows[0]["Total_Short"].ToString());
        RequestAmount = RequestAmount + ShortAmount;
        ShortNum = StockAmount - RequestAmount;
        if (ShortNum < 0)
        {
            ShortNum =  Math.Abs(ShortNum);
            ShortAmount = ShortNum;
            //obj.DB_SetArriveAmount(TypeNum[2].ToString(), StockAmount.ToString(), Week);
            obj.DB_SetShortAmount(TypeNum[3].ToString(), ShortAmount.ToString(), Week);
            obj.DB_SetSendAmount(TypeNum[3].ToString(), StockAmount.ToString(), Week);
            obj.DB_SetStockAmount(TypeNum[3].ToString(), "0", Week);
        }
        else
        {

            ShortAmount = 0;
            obj.DB_SetShortAmount(TypeNum[3].ToString(), ShortAmount.ToString(), Week);
            obj.DB_SetSendAmount(TypeNum[3].ToString(), RequestAmount.ToString(), Week);
            obj.DB_SetStockAmount(TypeNum[3].ToString(), ShortNum.ToString(), Week);
        }










        /***********************************************************************/
        /*******************************    D    ***********************************/
        StockAmount = 0;
        Delay = 0;
        DT_Delay = obj.DB_FindDelayWeek(G_ID, "D_Delivery");
        Delay = Delay + Int32.Parse(DT_Delay.Rows[0][0].ToString());
        Delay = Int32.Parse(Week) - Delay;
        Week_New = Delay.ToString();
        DT = obj.DB_GetSendAmount(TypeNum[3].ToString(), Week_New.ToString());
        if (DT.Rows.Count == 0)
        {
            int[] Zero = new int[1];
            Zero[0] = 0;
            DT.Columns.Add("Amount");
            DT.Rows.Add(Zero[0].ToString());

        }
        obj.DB_SetArriveAmount(TypeNum[2].ToString(), CountArrive(G_ID, DT.Rows[0][0].ToString()).ToString(), Week);
        DT = obj.DB_GetArriveAmount(TypeNum[2].ToString(), Week);
        StockAmount = StockAmount + Int32.Parse(DT.Rows[0][0].ToString());
        Delay = Int32.Parse(Week) - 1;
        DT = obj.DB_GetStockAmount(TypeNum[2].ToString(), Delay.ToString());
        if (DT.Rows.Count == 0)
        {
            int[] Zero = new int[1];
            Zero[0] = 0;
            DT.Columns.Add("Amount");
            DT.Rows.Add(Zero[0].ToString());

        }
        StockAmount = StockAmount + Int32.Parse(DT.Rows[0][0].ToString());
        obj.DB_SetStockAmount(TypeNum[2].ToString(), StockAmount.ToString(), Week);
        DT = obj.DB_GetShortAmount(TypeNum[2].ToString(), Delay.ToString());
        if (DT.Rows.Count == 0)
        {
            int[] Zero = new int[1];
            Zero[0] = 0;
            DT.Columns.Add("Amount");
            DT.Rows.Add(Zero[0].ToString());

        }
        obj.DB_SetShortAmount(TypeNum[2].ToString(), DT.Rows[0][0].ToString(), Week);
        //RequestAmount,StockAmount,ShortAmount
        DT = obj.DB_FindAllAmount(TypeNum[2].ToString(), Week);
        if (DT.Rows.Count == 0)
        {
            DT.Columns.Add("Stock_Amount");
            DT.Columns.Add("Request_Amount");
            DT.Columns.Add("Total_Short");
            DT.Rows.Add("0", "0", "0");
        }
        StockAmount = Int32.Parse(DT.Rows[0]["Stock_Amount"].ToString());
        RequestAmount = Int32.Parse(DT.Rows[0]["Request_Amount"].ToString());
        ShortAmount = Int32.Parse(DT.Rows[0]["Total_Short"].ToString());
        RequestAmount = RequestAmount + ShortAmount;
        ShortNum = StockAmount - RequestAmount;
        if (ShortNum < 0)
        {
            ShortNum = Math.Abs(ShortNum);
            ShortAmount = ShortNum;
            //obj.DB_SetArriveAmount(TypeNum[2].ToString(), StockAmount.ToString(), Week);
            obj.DB_SetShortAmount(TypeNum[2].ToString(), ShortAmount.ToString(), Week);
            obj.DB_SetSendAmount(TypeNum[2].ToString(), StockAmount.ToString(), Week);
            obj.DB_SetStockAmount(TypeNum[2].ToString(), "0", Week);
        }
        else
        {

            ShortAmount = 0;
            obj.DB_SetShortAmount(TypeNum[2].ToString(), ShortAmount.ToString(), Week);
            obj.DB_SetSendAmount(TypeNum[2].ToString(), RequestAmount.ToString(), Week);
            obj.DB_SetStockAmount(TypeNum[2].ToString(), ShortNum.ToString(), Week);
        }
        /**********************************************************************/
        /**************************      W      *******************************/

        StockAmount = 0;
        Delay = 0;
        DT_Delay = obj.DB_FindDelayWeek(G_ID, "W_Delivery");
        Delay = Delay + Int32.Parse(DT_Delay.Rows[0][0].ToString());
        Delay = Int32.Parse(Week) - Delay;
        Week_New = Delay.ToString();
        DT = obj.DB_GetSendAmount(TypeNum[2].ToString(), Week_New.ToString());
        if (DT.Rows.Count == 0)
        {
            int[] Zero = new int[1];
            Zero[0] = 0;
            DT.Columns.Add("Amount");
            DT.Rows.Add(Zero[0].ToString());

        }
        obj.DB_SetArriveAmount(TypeNum[1].ToString(), CountArrive(G_ID,DT.Rows[0][0].ToString()).ToString(), Week);
        DT = obj.DB_GetArriveAmount(TypeNum[1].ToString(), Week);
        StockAmount = StockAmount + Int32.Parse(DT.Rows[0][0].ToString());
        Delay = Int32.Parse(Week) - 1;
        DT = obj.DB_GetStockAmount(TypeNum[1].ToString(), Delay.ToString());
        if (DT.Rows.Count == 0)
        {
            int[] Zero = new int[1];
            Zero[0] = 0;
            DT.Columns.Add("Amount");
            DT.Rows.Add(Zero[0].ToString());

        }
        StockAmount = StockAmount + Int32.Parse(DT.Rows[0][0].ToString());
        obj.DB_SetStockAmount(TypeNum[1].ToString(), StockAmount.ToString(), Week);
        DT = obj.DB_GetShortAmount(TypeNum[1].ToString(), Delay.ToString());
        if (DT.Rows.Count == 0)
        {
            int[] Zero = new int[1];
            Zero[0] = 0;
            DT.Columns.Add("Amount");
            DT.Rows.Add(Zero[0].ToString());

        }
        obj.DB_SetShortAmount(TypeNum[1].ToString(), DT.Rows[0][0].ToString(), Week);
        //RequestAmount,StockAmount,ShortAmount
        DT = obj.DB_FindAllAmount(TypeNum[1].ToString(), Week);
        if (DT.Rows.Count == 0)
        {
            DT.Columns.Add("Stock_Amount");
            DT.Columns.Add("Request_Amount");
            DT.Columns.Add("Total_Short");
            DT.Rows.Add("0", "0", "0");
        }
        StockAmount = Int32.Parse(DT.Rows[0]["Stock_Amount"].ToString());
        RequestAmount = Int32.Parse(DT.Rows[0]["Request_Amount"].ToString());
        ShortAmount = Int32.Parse(DT.Rows[0]["Total_Short"].ToString());
        RequestAmount = RequestAmount + ShortAmount;
        ShortNum = StockAmount - RequestAmount;
        if (ShortNum < 0)
        {
            ShortNum = Math.Abs(ShortNum);
            ShortAmount = ShortNum;
            //obj.DB_SetArriveAmount(TypeNum[2].ToString(), StockAmount.ToString(), Week);
            obj.DB_SetShortAmount(TypeNum[1].ToString(), ShortAmount.ToString(), Week);
            obj.DB_SetSendAmount(TypeNum[1].ToString(), StockAmount.ToString(), Week);
            obj.DB_SetStockAmount(TypeNum[1].ToString(), "0", Week);
        }
        else
        {

            ShortAmount = 0;
            obj.DB_SetShortAmount(TypeNum[1].ToString(), ShortAmount.ToString(), Week);
            obj.DB_SetSendAmount(TypeNum[1].ToString(), RequestAmount.ToString(), Week);
            obj.DB_SetStockAmount(TypeNum[1].ToString(), ShortNum.ToString(), Week);
        }

        /**********************************************************************/
        /*****************************   R    ********************************/

        StockAmount = 0;
        Delay = 0;
        DT_Delay = obj.DB_FindDelayWeek(G_ID, "R_Delivery");
        Delay = Delay + Int32.Parse(DT_Delay.Rows[0][0].ToString());
        Delay = Int32.Parse(Week) - Delay;
        Week_New = Delay.ToString();
        DT = obj.DB_GetSendAmount(TypeNum[1].ToString(), Week_New.ToString());
        if (DT.Rows.Count == 0)
        {
            int[] Zero = new int[1];
            Zero[0] = 0;
            DT.Columns.Add("Amount");
            DT.Rows.Add(Zero[0].ToString());

        }
        obj.DB_SetArriveAmount(TypeNum[0].ToString(), CountArrive(G_ID, DT.Rows[0][0].ToString()).ToString(), Week);
        DT = obj.DB_GetArriveAmount(TypeNum[0].ToString(), Week);
        StockAmount = StockAmount + Int32.Parse(DT.Rows[0][0].ToString());
        Delay = Int32.Parse(Week) - 1;
        DT = obj.DB_GetStockAmount(TypeNum[0].ToString(), Delay.ToString());
        if (DT.Rows.Count == 0)
        {
            int[] Zero = new int[1];
            Zero[0] = 0;
            DT.Columns.Add("Amount");
            DT.Rows.Add(Zero[0].ToString());

        }
        StockAmount = StockAmount + Int32.Parse(DT.Rows[0][0].ToString());
        obj.DB_SetStockAmount(TypeNum[0].ToString(), StockAmount.ToString(), Week);
        DT = obj.DB_GetShortAmount(TypeNum[0].ToString(), Delay.ToString());
        if (DT.Rows.Count == 0)
        {
            int[] Zero = new int[1];
            Zero[0] = 0;
            DT.Columns.Add("Amount");
            DT.Rows.Add(Zero[0].ToString());

        }
        obj.DB_SetShortAmount(TypeNum[0].ToString(), DT.Rows[0][0].ToString(), Week);
        //RequestAmount,StockAmount,ShortAmount
        DT = obj.DB_FindAllAmount(TypeNum[0].ToString(), Week);
        if (DT.Rows.Count == 0)
        {
            DT.Columns.Add("Stock_Amount");
            DT.Columns.Add("Request_Amount");
            DT.Columns.Add("Total_Short");
            DT.Rows.Add("0", "0", "0");
        }
        StockAmount = Int32.Parse(DT.Rows[0]["Stock_Amount"].ToString());
        RequestAmount = Int32.Parse(DT.Rows[0]["Request_Amount"].ToString());
        ShortAmount = Int32.Parse(DT.Rows[0]["Total_Short"].ToString());
        RequestAmount = RequestAmount + ShortAmount;
        ShortNum = StockAmount - RequestAmount;
        if (ShortNum < 0)
        {
            ShortNum = Math.Abs(ShortNum);
            ShortAmount = ShortNum;
            //obj.DB_SetArriveAmount(TypeNum[2].ToString(), StockAmount.ToString(), Week);
            obj.DB_SetShortAmount(TypeNum[0].ToString(), ShortAmount.ToString(), Week);
            obj.DB_SetSendAmount(TypeNum[0].ToString(), StockAmount.ToString(), Week);
            obj.DB_SetStockAmount(TypeNum[0].ToString(), "0", Week);
        }
        else
        {

            ShortAmount = 0;
            obj.DB_SetShortAmount(TypeNum[0].ToString(), ShortAmount.ToString(), Week);
            obj.DB_SetSendAmount(TypeNum[0].ToString(), RequestAmount.ToString(), Week);
            obj.DB_SetStockAmount(TypeNum[0].ToString(), ShortNum.ToString(), Week);
        }


        /********************************************************************/


        //讀取運送時間_Am
        //把庫存減掉需求的量，假如是正數，那麼把正數 * 1 放入Costount，然後把數值更新到庫存中
        //假如是負數，那麼把負數 * 2 放入Cost_Amount
















        //Flag = true;

        //return Flag;
    }


    public void CountCost(string G_ID,string Week,int[] TypeNum)
    {
        int TotalCost,CostAmount,Week_New,StockCost,ShortCost;
        DataTable DT = new DataTable();
        Week_New = Int32.Parse(Week) - 1;
        /***********************R****************************/
        TotalCost = 0;
        CostAmount = 0;
        StockCost = 1;
        ShortCost = 2;
        DT = obj.DB_GetCost(G_ID, "StockCost");
        StockCost = Int32.Parse(DT.Rows[0][0].ToString());
        DT = obj.DB_GetCost(G_ID, "ShortCost");
        ShortCost = Int32.Parse(DT.Rows[0][0].ToString());



        //從腳本讀取庫存缺貨成本並且算出缺貨成本
        //讀取Stock_Amount(庫存的數量然後*1)加上Total_Short(欠貨的數量*2)
        DT = obj.DB_GetStockAmount(TypeNum[0].ToString(), Week);
        if (DT.Rows.Count == 0)
        {
            int[] Zero = new int[1];
            Zero[0] = 0;
            DT.Columns.Add("Amount");
            DT.Rows.Add(Zero[0].ToString());

        }
        CostAmount = CostAmount + (Int32.Parse(DT.Rows[0][0].ToString()) * StockCost);
        DT = obj.DB_GetShortAmount(TypeNum[0].ToString(), Week);
        if (DT.Rows.Count == 0)
        {
            int[] Zero = new int[1];
            Zero[0] = 0;
            DT.Columns.Add("Amount");
            DT.Rows.Add(Zero[0].ToString());

        }
        CostAmount = CostAmount + (Int32.Parse(DT.Rows[0][0].ToString()) * ShortCost);

        obj.DB_SetCostAmount(TypeNum[0].ToString(), CostAmount.ToString(), Week);
        
        DT = obj.DB_GetTotalCost(TypeNum[0].ToString(), Week_New.ToString());
        if (DT.Rows.Count == 0)
        {
            int[] Zero = new int[1];
            Zero[0] = 0;
            DT.Columns.Add("Amount");
            DT.Rows.Add(Zero[0].ToString());

        }
        TotalCost = Int32.Parse(DT.Rows[0][0].ToString());
        TotalCost = TotalCost + CostAmount;
        obj.DB_SetTotalCost(TypeNum[0].ToString(), TotalCost.ToString(), Week);



        /******************************W******************/

        TotalCost = 0;
        CostAmount = 0;
        //從腳本讀取庫存缺貨成本並且算出缺貨成本
        //讀取Stock_Amount(庫存的數量然後*1)加上Total_Short(欠貨的數量*2)
        DT = obj.DB_GetStockAmount(TypeNum[1].ToString(), Week);
        if (DT.Rows.Count == 0)
        {
            int[] Zero = new int[1];
            Zero[0] = 0;
            DT.Columns.Add("Amount");
            DT.Rows.Add(Zero[0].ToString());

        }
        CostAmount = CostAmount + (Int32.Parse(DT.Rows[0][0].ToString()) * StockCost);
        DT = obj.DB_GetShortAmount(TypeNum[1].ToString(), Week);
        if (DT.Rows.Count == 0)
        {
            int[] Zero = new int[1];
            Zero[0] = 0;
            DT.Columns.Add("Amount");
            DT.Rows.Add(Zero[0].ToString());

        }
        CostAmount = CostAmount + (Int32.Parse(DT.Rows[0][0].ToString()) * ShortCost);

        obj.DB_SetCostAmount(TypeNum[1].ToString(), CostAmount.ToString(), Week);
        
        DT = obj.DB_GetTotalCost(TypeNum[1].ToString(), Week_New.ToString());
        if (DT.Rows.Count == 0)
        {
            int[] Zero = new int[1];
            Zero[0] = 0;
            DT.Columns.Add("Amount");
            DT.Rows.Add(Zero[0].ToString());

        }
        TotalCost = Int32.Parse(DT.Rows[0][0].ToString());
        TotalCost = TotalCost + CostAmount;
        obj.DB_SetTotalCost(TypeNum[1].ToString(), TotalCost.ToString(), Week);

        /********************************D*****************************/


        TotalCost = 0;
        CostAmount = 0;
        //從腳本讀取庫存缺貨成本並且算出缺貨成本
        //讀取Stock_Amount(庫存的數量然後*1)加上Total_Short(欠貨的數量*2)
        DT = obj.DB_GetStockAmount(TypeNum[2].ToString(), Week);
        if (DT.Rows.Count == 0)
        {
            int[] Zero = new int[1];
            Zero[0] = 0;
            DT.Columns.Add("Amount");
            DT.Rows.Add(Zero[0].ToString());

        }
        CostAmount = CostAmount + (Int32.Parse(DT.Rows[0][0].ToString()) * StockCost);
        DT = obj.DB_GetShortAmount(TypeNum[2].ToString(), Week);
        if (DT.Rows.Count == 0)
        {
            int[] Zero = new int[1];
            Zero[0] = 0;
            DT.Columns.Add("Amount");
            DT.Rows.Add(Zero[0].ToString());

        }
        CostAmount = CostAmount + (Int32.Parse(DT.Rows[0][0].ToString()) * ShortCost);

        obj.DB_SetCostAmount(TypeNum[2].ToString(), CostAmount.ToString(), Week);
        
        DT = obj.DB_GetTotalCost(TypeNum[2].ToString(), Week_New.ToString());
        if (DT.Rows.Count == 0)
        {
            int[] Zero = new int[1];
            Zero[0] = 0;
            DT.Columns.Add("Amount");
            DT.Rows.Add(Zero[0].ToString());

        }
        TotalCost = Int32.Parse(DT.Rows[0][0].ToString());
        TotalCost = TotalCost + CostAmount;
        obj.DB_SetTotalCost(TypeNum[2].ToString(), TotalCost.ToString(), Week);

        /****************************F*********************************/
        TotalCost = 0;
        CostAmount = 0;
        //從腳本讀取庫存缺貨成本並且算出缺貨成本
        //讀取Stock_Amount(庫存的數量然後*1)加上Total_Short(欠貨的數量*2)
        DT = obj.DB_GetStockAmount(TypeNum[3].ToString(), Week);
        if (DT.Rows.Count == 0)
        {
            int[] Zero = new int[1];
            Zero[0] = 0;
            DT.Columns.Add("Amount");
            DT.Rows.Add(Zero[0].ToString());

        }
        CostAmount = CostAmount + (Int32.Parse(DT.Rows[0][0].ToString()) * StockCost);
        DT = obj.DB_GetShortAmount(TypeNum[3].ToString(), Week);
        if (DT.Rows.Count == 0)
        {
            int[] Zero = new int[1];
            Zero[0] = 0;
            DT.Columns.Add("Amount");
            DT.Rows.Add(Zero[0].ToString());

        }
        CostAmount = CostAmount + (Int32.Parse(DT.Rows[0][0].ToString()) * ShortCost);

        obj.DB_SetCostAmount(TypeNum[3].ToString(), CostAmount.ToString(), Week);
        
        DT = obj.DB_GetTotalCost(TypeNum[3].ToString(), Week_New.ToString());
        if (DT.Rows.Count == 0)
        {
            int[] Zero = new int[1];
            Zero[0] = 0;
            DT.Columns.Add("Amount");
            DT.Rows.Add(Zero[0].ToString());

        }
        TotalCost = Int32.Parse(DT.Rows[0][0].ToString());
        TotalCost = TotalCost + CostAmount;
        obj.DB_SetTotalCost(TypeNum[3].ToString(), TotalCost.ToString(), Week);




    }


    public DataTable Information(string SupplyID,string Week)
    {
        //用supply_ID列出該周資訊
        return obj.DB_GetRecordList(SupplyID, Week);


    }
    //多載
    public DataTable Information(string SupplyID)
    {
        //用supply_ID列出該周資訊
        return obj.DB_GetRecordList(SupplyID);


    }




    public Boolean Order(string SupplyID,string Week,string OrderAmount)
    {
        //下達訂單
        return obj.DB_SetOrderAmount(SupplyID, Week, OrderAmount);
    }


    
 
    public int CountArrive(string G_ID, string Amount)    
    {
        int Number, Yield, RandNum, TheSeed, MinusNum;
        Double Minus;
        DataTable DT = new DataTable();
        
        Number = 0;
        Number = Int32.Parse(Amount);
        //用G_ID取得良率，並且和傳過來的Amount作亂數運算
        DT = obj.DB_GetYield(G_ID);
        Yield = Int32.Parse(DT.Rows[0][0].ToString());
        Yield = 100 - Yield;

        Minus = (Number * Yield) / 100;
        MinusNum = (int)Minus;
        TheSeed = (int)DateTime.Now.Ticks;
        Random rndNum = new Random(TheSeed);
        RandNum = rndNum.Next(0, MinusNum);




        Number = Number - RandNum;

        return Number;
    }
    
    public DataTable WeekInfo(string G_ID)
    {
        //用GameID去取得腳本ID在去Market_Request抓取每星期欄位資料

        return obj.DB_GetGameWeekinfo(Int32.Parse(G_ID));

    }
    public DataTable GetGameID(string A_ID)
    {
        //使用者登入用A_ID去找G_ID得到候就有腳本ID



        return obj.DB_GetUserGame(A_ID);

    }
    public DataTable GetAccountGame(string A_ID)
    {
        //用使用者ID查詢他可以玩的GAMELIST
        return obj.DB_GetAccountGameList(A_ID);
    }





    /******************************Admin**********************************/








    //找出所有腳本的名稱
    public DataTable GetScenarioNAME()
    {
        return obj.DB_QueryScenarioNAME();
    }
    //關閉遊戲
    public Boolean DisableGame(int ID)
    {
        return obj.DB_DisableGame(ID);
    }
    //開啟遊戲
    public Boolean EnableGame(int ID)
    {
        return obj.DB_EnableGame(ID);
    }
    //新增遊戲
    public Boolean CreateGame(string Name, string Memo,int Scenario_ID,string Supply_Chain)
    {
        //Boolean Flag;
        int New_PK;


        New_PK = obj.DB_CreateGame(Name, Memo, Scenario_ID,Supply_Chain);
        return obj.DB_InitSupplyChain(New_PK.ToString(), Supply_Chain);
    }
    //刪除遊戲
    public Boolean DelGame(int GID)
    {
        DataTable DT = new DataTable();

        DT = GetSupplyChainList(GID);

        for(int i=0;i<DT.Rows.Count;i++)
        {
            obj.DB_DelGameRecord(Int32.Parse(DT.Rows[i]["ID"].ToString()));
        }

        obj.DB_DelGameSupply(GID);
        obj.DB_DelGame(GID);
        return true;
    }

    public Boolean RestartGame(int GID)
    {
        DataTable DT = new DataTable();

        DT = GetSupplyChainList(GID);

        for (int i = 0; i < DT.Rows.Count; i++)
        {
            obj.DB_DelGameRecord(Int32.Parse(DT.Rows[i]["ID"].ToString()));
        }
        
        return true;
    }

    //讀取指定的Game
    public DataTable GetGame(string ID)
    {
        return obj.DB_GetGame(ID);
    }

    //取得遊戲列表
    public DataTable GetGameList(int SID)
    {
        return obj.DB_GetGameList(SID);
    }

    //設定Game
    public Boolean EditGame(string ID,string Name,int S_ID,string Memo)
    {
        return obj.DB_EditGame(ID, Name, S_ID, Memo);
    }
    //讀取遊戲角色
    public DataTable GetAccountType(int AID)
    {
        return obj.DB_GetAccountType(AID);
    }
    //讀取帳號角色
    public DataTable GetAccount_Type(string ID)
    {
        return obj.DB_GetAccount_Type(ID);
    }
    //設定供應鏈的角色
    public Boolean SetSupplyAccount(int G_ID, int SupplyNum, int FC, int Big, int Mid, int Sma)
    {
        return obj.DB_SetSupplyAccount(G_ID, SupplyNum, FC, Big, Mid, Sma);
    }
    //刪除供應鍊
    public Boolean DelSupplyChain(int G_ID, int SupplyNum)
    {
        return obj.DB_DelSupplyChain(G_ID, SupplyNum);
    }
    //刪除供應鍊帳號
    public Boolean DelSupplyChainAccount(int G_ID, int SupplyNum,int A_ID)
    {
        return obj.DB_DelSupplyChainAccount(G_ID, SupplyNum,A_ID);
    }

    //新增供應鍊
    public Boolean AddSupplyChain(string G_ID)
    {
        return true;
    }
    //取得供應該資訊列表
    public DataTable GetSupplyChainList(int GameID)
    {
        return obj.DB_GetSupplyChainList(GameID);
    }

    //取得此遊戲的每週資訊列表
    public DataTable GetGameWeekinfo(int SID)
    {
        return obj.DB_GetGameWeekinfo(SID);
    }

    //取得此遊戲目前某供應鍊的記錄列表
    public DataTable GetGameRecordList(int SupplyID)
    {
        return obj.DB_GetGameRecordList(SupplyID);
    }


    /*
    //取得supplychainID,ID是使用者帳號ID
    public DataTable GetSupplyChainID(int GameID,int ID) 
    {
        return obj.DB_GetSupplyChainID(GameID,ID);
    }
    //取得該劇本的良率
    public DataTable GetYield(int GameID)
    {
        return obj.DB_GetYield(GameID);
    }
    //取得其他角色的訂單數量
    public DataTable GetOtherAmount(int Supply_ID, int Week)
    {
        return obj.DB_GetOtherAmount(Supply_ID, Week);
    }
    //讀取庫存數量
    public DataTable GetStockAmount(int Supply_ID, int Week)
    {
        return obj.DB_GetStockAmount(Supply_ID, Week);
    }
    //讀取累計庫存
    public DataTable GetTotalCost(int Supply_ID, int Week)
    {
        return obj.DB_GetTotalCost(Supply_ID, Week);
    }
    //讀取到貨數量
    public DataTable GetArriveAmount(int Supply_ID, int Week)
    {
        return obj.DB_GetArriveAmount(Supply_ID, Week);
    }







    //設定訂單數量
    public Boolean SetOrderAmount(int Supply_ID, int Week,int OrderAmount)
    {
        return obj.DB_SetOrderAmount(Supply_ID, Week, OrderAmount);
    }
    //設定累計成本
    public Boolean SetTotalCost(int Supply_ID, int TotalCost)
    {
        return obj.DB_SetTotalCost(Supply_ID, TotalCost);
    }
    //記錄其他角色的要求數量
    public Boolean SetRequestAmount(int Supply_ID, int RequestAmount)
    {
        return obj.DB_SetRequestAmount(Supply_ID, RequestAmount);
    }
    //寫入當期的成本
    public Boolean SetCostAmount(int Supply_ID, int CostAmount)
    {
        return obj.DB_SetCostAmount(Supply_ID, CostAmount);
    }
    //寫入庫存數量
    public Boolean SetStockAmount(int Supply_ID, int StockAmount)
    {
        return obj.DB_SetStockAmount(Supply_ID, StockAmount);
    }
    //寫入到貨數量
    public Boolean SetArriveAmount(int Supply_ID, int ArriveAmount)
    {
        return obj.DB_SetArriveAmount(Supply_ID, ArriveAmount);
    }

    */



    public DataTable QueryChainNum(int G_id, int A_id)
    {
        return obj.DB_QueryChainNum(G_id, A_id);
    }

    public DataTable QueryChainNum(int G_id)
    {
        return obj.DB_QueryChainNum(G_id);
    }

    public DataTable QuerySupplyList(int G_id, int ChainNum)
    {
        return obj.DB_QuerySupplyList(G_id, ChainNum);
    }

    public DataTable CheckChainNum(int Supply_ID, int WeekNow)
    {
        return obj.DB_CheckChainNum(Supply_ID, WeekNow);
    }
    public DataTable QuerySupplyInfo(int Supply_ID)
    {
        return obj.DB_QuerySupplyInfo(Supply_ID);
    }
    public DataTable QueryGameInfo(int G_ID)
    {
        return obj.DB_QueryGameInfo(G_ID);
    }
    public DataTable GetStartList(int State)
    {
        return obj.DB_GetStartList(State);
    }



    //書包新增
    
    //可進行的遊戲列表
    public DataTable QueryAvailableGameList(int A_id)
    {
        return obj.DB_QueryAvailableGameList(A_id);
    }
}
