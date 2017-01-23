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

public partial class index : System.Web.UI.Page
{
    Account account = new Account();
    Game game = new Game();
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sessionID"] == null)
        {
            Panel_GameList.Visible = false;
            LBT_Logout.Visible = false;            
        }
        else
        {
            account.SetAccount(Int32.Parse(Session["sessionID"].ToString()));

            if (account.Type == 5)
            {
                Session["admin"] = true;
                Response.Redirect("admin/");
            }

            Panel_Login.Visible = false;            
            Repeater_GameList.DataSource = game.QueryAvailableGameList(Int32.Parse(Session["sessionID"].ToString()));
            Repeater_GameList.DataBind();
        }
    }
    protected void BT_Login_Click(object sender, EventArgs e)
    {
        account.Login(TB_UserID.Text, TB_UserPW.Text);
        Response.Redirect(Request.Url.ToString());
    }
    protected void LB_Logout_Click(object sender, EventArgs e)
    {
        account.Logout();
        Response.Redirect(Request.Url.ToString());
    }
}
