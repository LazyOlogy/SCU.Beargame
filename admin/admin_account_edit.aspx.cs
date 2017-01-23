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
using System.Data.OleDb;
using BeerGame;

public partial class admin_admin_account_edit : System.Web.UI.Page
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
    }    
}
