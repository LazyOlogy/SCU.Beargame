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
using System.Text;
using BeerGame;

public partial class admin_admin_record_report : System.Web.UI.Page
{
    Account a = new Account();
    Game g = new Game();
    Scenario s = new Scenario();
    DataTable R_DT = new DataTable();
    DataTable A_DT = new DataTable();    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        

        //管理者檢查
        if (Session["admin"] == null)
        {
            Response.Redirect("../index.aspx");
        }

        int G_id = Int32.Parse(Request["G_id"].ToString());
        LB_Title.Text = "歷史記錄：[Game] <font color=Yellow>" + g.GetGame(G_id.ToString()).Rows[0]["Name"].ToString() + "</font>";
        Info_Repeater.DataSource = g.QueryChainNum(G_id);
        Info_Repeater.DataBind();
        AreaComposite_Repeater.DataSource = g.QueryChainNum(G_id);
        AreaComposite_Repeater.DataBind();
        
        for (int i = 1; i < g.QueryChainNum(G_id).Rows.Count + 1; i++)
        {
            DataTable DT = new DataTable();
            DT = g.QuerySupplyList(G_id, i);
            Label LB_ChainNum = (Label)Info_Repeater.Items[i - 1].FindControl("LB_ChainNum");
            LB_ChainNum.Text = "第" + i.ToString() + "條供應鏈";
            
            if (Request["target"].ToString() != "Composite")
                UnitInfo(DT, i-1);
            else
            {
                SupplyInfo(DT, g.QueryChainNum(G_id).Rows.Count);
                break;
            }
        }
    }

    protected void UnitInfo(DataTable DT, int i)
    {
        try
        {
            R_DT.Columns.Add("Index");
            R_DT.Columns.Add("Factory");
            R_DT.Columns.Add("Distribution");
            R_DT.Columns.Add("Wholesale");
            R_DT.Columns.Add("Retailer");

            A_DT.Columns.Add("Index");
            A_DT.Columns.Add("Factory");
            A_DT.Columns.Add("Distribution");
            A_DT.Columns.Add("Wholesale");
            A_DT.Columns.Add("Retailer");

            for (int j = 0; j < DT.Rows.Count; j++)
            {
                a.SetAccount(Int32.Parse(DT.Rows[j]["A_ID"].ToString()));
                if (Request["target"].ToString() == "AreaComposite")
                {
                    SetAreaTable(g.Information(DT.Rows[j]["ID"].ToString()), j + 1);
                }
                else
                    ShowFigure(g.Information(DT.Rows[j]["ID"].ToString()), j + 1, i);                
            }

            Repeater Composite_Repeater = new Repeater();
            Composite_Repeater = (Repeater)Info_Repeater.Items[i].FindControl("Composite_Repeater");
            Composite_Repeater.DataSource = R_DT;
            Composite_Repeater.DataBind();

            ShowCompositeFigure(i);
            if (Request["target"].ToString() == "AreaComposite")
                ShowAreaCompositeFigure(i);
            R_DT.Reset();
            A_DT.Reset();
        }
        catch (Exception)
        {
        }
    }


    protected void SetAreaTable(DataTable DT, int index)
    {
        string target = string.Empty;
        switch (DDL_AreaComposite.SelectedValue)
        {
            case "1":
                target = "Stock_Amount";
                break;
            case "2":
                target = "Order_Amount";
                break;
            case "3":
                target = "Cost_Amount";
                break;
            case "4":
                target = "Total_Cost";
                break;
        }
        string Unit = string.Empty;
        switch (index)
        {
            case 1:
                Unit = "Factory";
                break;
            case 2:
                Unit = "Distribution";
                break;
            case 3:
                Unit = "Wholesale";
                break;
            case 4:
                Unit = "Retailer";
                break;
        }
        for (int i = 0; i < DT.Rows.Count; i++)
        {
            try
            {
                A_DT.Rows[i][Unit] = DT.Rows[i][target].ToString();
            }
            catch (Exception)
            {
                A_DT.Rows.Add();
                A_DT.Rows[i][Unit] = DT.Rows[i][target].ToString();
            }
            A_DT.Rows[i]["Index"] = (i + 1).ToString();
        }
    }

    protected void SupplyInfo(DataTable DT, int ChainNum)
    {
        int G_id = Int32.Parse(Request["G_id"].ToString());
        int count = g.Information(DT.Rows[0]["ID"].ToString()).Rows.Count;
        DataTable InfoTable = new DataTable();
        int[,] Total_Cost = new int[ChainNum, count];
        string Unit = string.Empty;
        switch (DDL_Composite.SelectedValue)
        { 
            case "1":
                Unit = "Stock_Amount";
                break;
            case "2":
                Unit = "Order_Amount";
                break;
            case "3":
                Unit = "Cost_Amount";
                break;
            case "4":
                Unit = "Total_Cost";
                break;
        }
        for (int i = 0; i < ChainNum; i++)
            for (int k = 0; k < count; k++)
                for (int j = 0; j < DT.Rows.Count; j++)
                {
                    DT = g.QuerySupplyList(G_id, i + 1);
                    try
                    {
                        InfoTable = g.Information(DT.Rows[j]["ID"].ToString());
                        Total_Cost[i, k] += Int32.Parse(InfoTable.Rows[k][Unit].ToString());
                    }
                    catch (Exception ex)
                    {
                        Total_Cost[i, k] += 0;
                        //ChainNum = i + 1;
                        //break;
                    }
                }
        ShowSupplyFigure(ChainNum, count, Total_Cost);
    }

    protected void ShowSupplyFigure(int ChainNum, int count, int[,] Total_Cost)
    {
        //HtmlTableCell HTC = (HtmlTableCell)Info_Repeater.Items[ChainNum - 1].FindControl("Composite");

        string scr = "<script type=\"text/javascript\" src=\"http://www.google.com/jsapi\"></script>";
        scr += "<script type=\"text/javascript\"> google.load('visualization', '1', {packages: ['linechart']}) </script>";
        scr += "<script type=\"text/javascript\">";
        scr += "function Composite() {";
        // Create and populate the data table.
        scr += "var data = new google.visualization.DataTable();";
        //scr += "data.addColumn('string', '週數');";
        scr += "data.addRows(" + count.ToString() + ");";
        for (int j = 1; j < ChainNum + 1; j++)
        {
            scr += "data.addColumn('number', '第" + j.ToString() + "條供應鏈');";
            for (int i = 0; i < Total_Cost.Length / ChainNum; i++)
                //scr += "data.setCell(0, 0, '');";
                scr += "data.setCell(" + i.ToString() + ", " + (j - 1).ToString() + ", " + Total_Cost[j - 1, i].ToString() + ");";
        }
        // Create and draw the visualization.

        scr += "new google.visualization.LineChart(document.getElementById('Composite')).";
        scr += "draw(data, {width:500 ,height:250})}";

        scr += "google.setOnLoadCallback(Composite);";
        scr += "</script>";
        
        Page.RegisterStartupScript("Composite", scr);
    }

    protected void ShowAreaCompositeFigure(int type)
    {
        HtmlTableCell HTC = (HtmlTableCell)AreaComposite_Repeater.Items[type].FindControl("AreaComposite");
        string scr = "<script type=\"text/javascript\" src=\"http://www.google.com/jsapi\"></script>";
        scr += "<script type=\"text/javascript\"> google.load('visualization', '1', {packages: ['areachart']}) </script>";
        scr += "<script type=\"text/javascript\">";
        scr += "function " + HTC.ClientID.ToString() + "() {";
        // Create and populate the data table.
        scr += "var data = new google.visualization.DataTable();";
        //scr += "data.addColumn('string', '週數');";
        scr += "data.addColumn('number', '零售商');";
        scr += "data.addColumn('number', '大盤商');";
        scr += "data.addColumn('number', '配銷商');";
        scr += "data.addColumn('number', '工廠');";
        scr += "data.addRows(" + A_DT.Rows.Count.ToString() + ");";

        for (int i = 0; i < A_DT.Rows.Count; i++)
        {
            //scr += "data.setCell(0, 0, '');";
            scr += "data.setCell(" + i.ToString() + ", 0, " + Int32.Parse(A_DT.Rows[i]["Retailer"].ToString()) + ");";
            scr += "data.setCell(" + i.ToString() + ", 1, " + (Int32.Parse(A_DT.Rows[i]["Retailer"].ToString()) + Int32.Parse(A_DT.Rows[i]["Wholesale"].ToString())) + ");";
            scr += "data.setCell(" + i.ToString() + ", 2, " + (Int32.Parse(A_DT.Rows[i]["Retailer"].ToString()) + Int32.Parse(A_DT.Rows[i]["Wholesale"].ToString()) + Int32.Parse(A_DT.Rows[i]["Distribution"].ToString())) + ");";
            scr += "data.setCell(" + i.ToString() + ", 3, " + (Int32.Parse(A_DT.Rows[i]["Retailer"].ToString()) + Int32.Parse(A_DT.Rows[i]["Wholesale"].ToString()) + Int32.Parse(A_DT.Rows[i]["Distribution"].ToString()) + Int32.Parse(A_DT.Rows[i]["Factory"].ToString())) + ");";
        }
        // Create and draw the visualization.

        scr += "new google.visualization.AreaChart(document.getElementById('" + HTC.ClientID.ToString() + "')).";
        scr += "draw(data,  {title:'第" + Convert.ToString(type + 1) + "條供應鏈', width:500 ,height:250})}";

        scr += "google.setOnLoadCallback(" + HTC.ClientID.ToString() + ");";
        scr += "</script>";
        
        Page.RegisterStartupScript(HTC.ClientID.ToString(), scr);
    }

    protected void ShowCompositeFigure(int type)
    {
        HtmlTableCell HTC = (HtmlTableCell)Info_Repeater.Items[type].FindControl("Composite");
        string scr = "<script type=\"text/javascript\" src=\"http://www.google.com/jsapi\"></script>";
        scr += "<script type=\"text/javascript\"> google.load('visualization', '1', {packages: ['linechart']}) </script>";
        scr += "<script type=\"text/javascript\">";
        scr += "function " + HTC.ClientID.ToString() + "() {";
        // Create and populate the data table.
        scr += "var data = new google.visualization.DataTable();";
        //scr += "data.addColumn('string', '週數');";
        scr += "data.addColumn('number', '工廠');";
        scr += "data.addColumn('number', '配銷商');";
        scr += "data.addColumn('number', '大盤商');";
        scr += "data.addColumn('number', '零售商');";
        scr += "data.addRows(" + R_DT.Rows.Count.ToString() + ");";

        for (int i = 0; i < R_DT.Rows.Count; i++)
        {
            //scr += "data.setCell(0, 0, '');";
            scr += "data.setCell(" + i.ToString() + ", 0, " + Int32.Parse(R_DT.Rows[i]["Factory"].ToString()) + ");";
            scr += "data.setCell(" + i.ToString() + ", 1, " + Int32.Parse(R_DT.Rows[i]["Distribution"].ToString()) + ");";
            scr += "data.setCell(" + i.ToString() + ", 2, " + Int32.Parse(R_DT.Rows[i]["Wholesale"].ToString()) + ");";
            scr += "data.setCell(" + i.ToString() + ", 3, " + Int32.Parse(R_DT.Rows[i]["Retailer"].ToString()) + ");";
        }
        // Create and draw the visualization.

        scr += "new google.visualization.LineChart(document.getElementById('" + HTC.ClientID.ToString() + "')).";
        scr += "draw(data, null)}";

        scr += "google.setOnLoadCallback(" + HTC.ClientID.ToString() + ");";
        scr += "</script>";
        
        Page.RegisterStartupScript(HTC.ClientID.ToString(), scr);
    }


    protected void ShowFigure(DataTable DT, int index, int type)
    {
        string target = string.Empty;

        if (Request["target"] != null)
            target = Request["target"];
        else
            target = "Stock_Amount";
        string Unit = string.Empty;
        HtmlTableCell HTC = new HtmlTableCell();

        switch (index)
        {
            case 1:
                HTC = (HtmlTableCell)Info_Repeater.Items[type].FindControl("Factory");
                Unit = "Factory";
                break;
            case 2:
                HTC = (HtmlTableCell)Info_Repeater.Items[type].FindControl("Distribution");
                Unit = "Distribution";
                break;
            case 3:
                HTC = (HtmlTableCell)Info_Repeater.Items[type].FindControl("Wholesale");
                Unit = "Wholesale";
                break;
            case 4:
                HTC = (HtmlTableCell)Info_Repeater.Items[type].FindControl("Retailer");
                Unit = "Retailer";
                break;
        }
        string scr = "<script type=\"text/javascript\" src=\"http://www.google.com/jsapi\"></script>";
        scr += "<script type=\"text/javascript\"> google.load('visualization', '1', {packages: ['linechart']}) </script>";
        scr += "<script type=\"text/javascript\">";
        scr += "function " + HTC.ClientID.ToString() + "() {";
        // Create and populate the data table.
        scr += "var data = new google.visualization.DataTable();";
        //scr += "data.addColumn('string', '週數');";
        scr += "data.addColumn('number', '數量');";
        scr += "data.addRows(" + DT.Rows.Count.ToString() + ");";
    
        for (int i = 0; i < DT.Rows.Count; i++)
        {
            //scr += "data.setCell(" + i.ToString() + ", 0, '" + (i+1).ToString() + "');";
            scr += "data.setCell(" + i.ToString() + ", 0, " + Int32.Parse(DT.Rows[i][target].ToString()) + ");";
            try
            {
                if (DT.Rows[i][target].ToString() != "" || DT.Rows[i][target] != null)  //8月25新增之防呆，可視情況移除
                    R_DT.Rows[i][Unit] = DT.Rows[i][target].ToString();
                else
                    R_DT.Rows[i][Unit] = "0";
            }
            catch (Exception ex)
            {
                R_DT.Rows.Add();
                if (DT.Rows[i][target].ToString() != "" || DT.Rows[i][target] != null)
                    R_DT.Rows[i][Unit] = DT.Rows[i][target].ToString();
                else
                    R_DT.Rows[i][Unit] = "0";
            }
            R_DT.Rows[i]["Index"] = (i + 1).ToString();
        }
        // Create and draw the visualization.

        scr += "new google.visualization.LineChart(document.getElementById('" + HTC.ClientID.ToString() + "')).";
        scr += "draw(data, {title: '" + a.Name + "' , width:200 , height:200});  }";

        scr += "google.setOnLoadCallback(" + HTC.ClientID.ToString() + ");";
        scr += "</script>";
        
        Page.RegisterStartupScript(HTC.ClientID.ToString(), scr);
        
    }

    protected void BT_Export_Click(object sender, EventArgs e)
    {

    }
}
