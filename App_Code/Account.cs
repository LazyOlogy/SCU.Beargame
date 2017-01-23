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
/// Account 的摘要描述
/// </summary>
public class Account
{
    DBClass obj = new DBClass();
    public string Name, Password, Mail, Memo;
    public int ID, Type;

	public Account()
	{
        Name = string.Empty;
        Password = string.Empty;
        Mail = string.Empty;
        Memo = string.Empty;        
        Type = 0;
	}

    //設定帳戶
    public Boolean SetAccount(int ID)
    {
        DataTable DT = new DataTable();
        DT = obj.DB_GetAccount(ID);

        if (DT != null)
        {
            this.ID = ID;
            this.Name = DT.Rows[0]["Name"].ToString();
            this.Password = DT.Rows[0]["Password"].ToString();
            this.Mail = DT.Rows[0]["Mail"].ToString();
            this.Memo = DT.Rows[0]["Memo"].ToString();
            this.Type = Int32.Parse(DT.Rows[0]["Type"].ToString());

            return true;
        }
        else
        {
            return false;
        }
    }

    //帳戶登入
    public Boolean Login(string Name, string PW) //會員登入
    {        
        DataTable DT = new DataTable();

        DT = obj.DB_CheckAccount(Name, PW);

        if (DT.Rows.Count > 0)
        {
            SetAccount(Int16.Parse(DT.Rows[0]["ID"].ToString()));

            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session["sessionID"] = this.ID.ToString();            

            return true;
        }
        else
            return false;
    }

    //帳戶登出
    public Boolean Logout()
    {
        try
        {
            HttpContext.Current.Session.Abandon();
            return true;
        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Write(ex);
            return false;
        }
    }    

    //新增帳戶
    public Boolean CreateAccount(string Name, string Password, string Mail, string Memo, int Type)
    {        
        DataTable DT = new DataTable();

        DT = obj.DB_CheckAccount(Name);

        if (DT.Rows.Count > 0)
            return false;
        else
            return obj.DB_CreateAccount(Name, Password, Mail, Memo, Type);
    }

    //編輯帳戶
    public Boolean EditAccount(int ID, string Name, string Password, string Mail, string Memo, int Type)
    {
        return obj.DB_EditAccount(ID, Name, Password, Mail, Memo, Type);
    }

    //刪除帳戶
    public Boolean DeleteAccount(int ID)
    {
        return obj.DB_DeleteAccount(ID);
    }

    //查詢特定角色之帳戶
    public DataTable QueryAccountList(int Type)
    {
        return obj.DB_QueryAccountList(Type);
    }

}
