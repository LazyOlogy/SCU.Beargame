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

public partial class game : System.Web.UI.Page
{
    Account account = new Account();
    Scenario scenario = new Scenario();
    Game g = new Game();
    public string Role;
    public DataTable DT = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sessionID"] == null || Request["SID"] == null)        
            Response.Redirect("index.aspx");

        

        account.SetAccount(Int32.Parse(Session["sessionID"].ToString()));

        switch (account.Type.ToString())
        {
            case "1":
                PN_photo.CssClass = "photo1";
                Role = "工廠";
                break;
            case "2":
                PN_photo.CssClass = "photo2";
                Role = "配銷商";
                break;
            case "3":
                PN_photo.CssClass = "photo3";
                Role = "大盤商";
                break;
            case "4":
                PN_photo.CssClass = "photo4";
                Role = "零售商";
                break;
        }

        DT = g.QueryGameInfo(Int32.Parse(g.QuerySupplyInfo(Int32.Parse(Request["SID"].ToString())).Rows[0]["G_ID"].ToString()));
        scenario.SetScenario(Int32.Parse(DT.Rows[0]["Scenario_ID"].ToString()));

        LB_state.Text = "使用者：" + account.Name;
        LB_state.Text += "　角色：" + Role;
        LB_state.Text += "　訂單週數：" + scenario.F_Order.ToString() + "週";
        LB_state.Text += "　送貨週數：" + scenario.F_Delivery.ToString() + "週";
    }

    protected void LB_Logout_Click(object sender, EventArgs e)
    {
        account.Logout();
        Response.Redirect("index.aspx");
    }
}
