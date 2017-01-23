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

public partial class admin_index : System.Web.UI.Page
{
    Account a = new Account();
    Game g = new Game();
    Scenario s = new Scenario();
    DBClass obj = new DBClass();
    DataTable DT = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        //管理者檢查
        if (Session["admin"] == null)
        {
            Response.Redirect("../index.aspx");
        }


        DT = obj.DB_GetStartList(1);
        GameListRepeater.DataSource = DT;
        GameListRepeater.DataBind();
        //Label1.Text = g.Check("10", "10", "14").ToString();
        
    }
    protected void GameListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Button GameList_Detail = (Button)e.CommandSource;
        if (GameList_Detail.CommandName == "Interrupt")
        {
           
            //Response.Write(Int32.Parse( e.CommandArgument.ToString()));
            g.DisableGame(Int32.Parse(e.CommandArgument.ToString()));
            Response.Redirect("Index.aspx");
        }
    }


}
