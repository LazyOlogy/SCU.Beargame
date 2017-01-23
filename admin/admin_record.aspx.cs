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

public partial class admin_admin_record : System.Web.UI.Page
{
    Game g = new Game();
    DBClass obj = new DBClass();
    DataTable DT_GL = new DataTable();
    DataTable DT_Type = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        //管理者檢查
        if (Session["admin"] == null)
        {
            Response.Redirect("../index.aspx");
        }

        if (DropDownList1.Items.Count <= 0)
        {
            DropDownList1.Items.Clear();
            //DropDownList1.Items.Add("請選擇一個部門");
            DT_GL = g.GetScenarioNAME();
            DataView DV = new DataView(DT_GL);
            DropDownList1.DataSource = DV;
            DropDownList1.DataTextField = "NAME";
            DropDownList1.DataValueField = "ID";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "<-- Select -->");
        }

    }

    protected void Selection_Change(Object sender, EventArgs e)
    {
        //Response.Write("<script>alert(" + DropDownList1.SelectedValue + ")</script>");
        if (DropDownList1.SelectedIndex != 0)
        {
            DT_GL = g.GetGameList(Int32.Parse(DropDownList1.SelectedValue));
            GameList_Repeater.DataSource = DT_GL;
            GameList_Repeater.DataBind();

 
        }
    }
    protected void GetUserList_1()
    {
        DT_Type = obj.DB_GetUserList(DropDownList1.SelectedValue.Substring(0, 1), 1);
        for(int i = 0 ; i < DT_Type.Rows.Count ; i++){
        Response.Write(DT_Type.Rows [i]["A_NAME"].ToString());
        }
    }
    protected void GetUserList_2()
    {
        DT_Type = obj.DB_GetUserList(DropDownList1.SelectedValue.Substring(0, 1), 2);
        for (int i = 0; i < DT_Type.Rows.Count; i++)
        {
            Response.Write(DT_Type.Rows[i]["A_NAME"].ToString());
        }
    }
    protected void GetUserList_3()
    {
        DT_Type = obj.DB_GetUserList(DropDownList1.SelectedValue.Substring(0, 1), 3);
        for (int i = 0; i < DT_Type.Rows.Count; i++)
        {
            Response.Write(DT_Type.Rows[i]["A_NAME"].ToString());
        }
    }
    protected void GetUserList_4()
    {
        DT_Type = obj.DB_GetUserList(DropDownList1.SelectedValue.Substring(0, 1), 4);
        for (int i = 0; i < DT_Type.Rows.Count; i++)
        {
            Response.Write(DT_Type.Rows[i]["A_NAME"].ToString());
        }
    }

    protected void BT_DelGameRecord_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < GameList_Repeater.Items.Count; i++)
        {
            CheckBox CB = (CheckBox)GameList_Repeater.Items[i].FindControl("CB_CheckGame");
            if (CB.Checked)
                g.DelGame(Int32.Parse(CB.ToolTip));
        }

        Response.Redirect(Request.UrlReferrer.ToString());
    }
}
