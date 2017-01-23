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

public partial class admin_admin_account : System.Web.UI.Page
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

        if (!IsPostBack)
            SetInit();
    }

    protected void QueryType( Repeater R, int Type)
    {
        DataTable DT = new DataTable();
        DT = a.QueryAccountList(Type);
        R.DataSource = DT;
        R.DataBind();
        for (int i = 0; i < DT.Rows.Count; i++)
        {
            Label LB_Name = (Label)R.Items[i].FindControl("LB_Name");
            Label LB_Password = (Label)R.Items[i].FindControl("LB_Password");
            HyperLink HL_Mail = (HyperLink)R.Items[i].FindControl("HL_Mail");
            Label LB_Memo = (Label)R.Items[i].FindControl("LB_Memo");
            DropDownList DDL_Type = (DropDownList)R.Items[i].FindControl("DDL_Type");
            a.SetAccount(Int32.Parse(DT.Rows[i]["ID"].ToString()));
            LB_Name.Text = a.Name;
            LB_Password.Text = a.Password;
            HL_Mail.Text = a.Mail;
            HL_Mail.NavigateUrl = "mailto:" + a.Mail;
            LB_Memo.Text = a.Memo;
            DDL_Type.SelectedValue = Type.ToString();
            DDL_Type.ToolTip = DT.Rows[i]["ID"].ToString();
        }
    }

    protected void SetInit()
    {
        QueryType(NoAssign_Repeater, 0);
        QueryType(Factory_Repeater, 1);
        QueryType(Distribution_Repeater, 2);
        QueryType(Wholesale_Repeater, 3);
        QueryType(Retailer_Repeater, 4);
    }

    protected void BT_DelAccount_Click(object sender, EventArgs e)
    {
        string[] AList = new string[Hid_Ckb.Value.Split(',').Length];
        AList = Hid_Ckb.Value.Split(',');
        for (int i = 0; i < AList.Length; i++)
            a.DeleteAccount(Int32.Parse(AList[i]));
        SetInit();
    }

    protected void DDL_SelectedIndexChanged(object sender, EventArgs e)
    {

        switch (Hid_Repeater.Value)
        { 
            case "NoAssign_Repeater":
                for (int i = 0; i < a.QueryAccountList(0).Rows.Count; i++)
                {
                    DropDownList DDL_Type = (DropDownList)NoAssign_Repeater.Items[i].FindControl("DDL_Type");
                    a.SetAccount(Int32.Parse(DDL_Type.ToolTip));
                    a.EditAccount(Int32.Parse(DDL_Type.ToolTip), a.Name, a.Password, a.Mail, a.Memo, Int32.Parse(DDL_Type.SelectedValue));
                }
                break;
            case "Factory_Repeater":
                for (int i = 0; i < a.QueryAccountList(1).Rows.Count; i++)
                {
                    DropDownList DDL_Type = (DropDownList)Factory_Repeater.Items[i].FindControl("DDL_Type");
                    a.SetAccount(Int32.Parse(DDL_Type.ToolTip));
                    a.EditAccount(Int32.Parse(DDL_Type.ToolTip), a.Name, a.Password, a.Mail, a.Memo, Int32.Parse(DDL_Type.SelectedValue));
                }
                break;
            case "Distribution_Repeater":
                for (int i = 0; i < a.QueryAccountList(2).Rows.Count; i++)
                {
                    DropDownList DDL_Type = (DropDownList)Distribution_Repeater.Items[i].FindControl("DDL_Type");
                    a.SetAccount(Int32.Parse(DDL_Type.ToolTip));
                    a.EditAccount(Int32.Parse(DDL_Type.ToolTip), a.Name, a.Password, a.Mail, a.Memo, Int32.Parse(DDL_Type.SelectedValue));
                }
                break;
            case "Wholesale_Repeater":
                for (int i = 0; i < a.QueryAccountList(3).Rows.Count; i++)
                {
                    DropDownList DDL_Type = (DropDownList)Wholesale_Repeater.Items[i].FindControl("DDL_Type");
                    a.SetAccount(Int32.Parse(DDL_Type.ToolTip));
                    a.EditAccount(Int32.Parse(DDL_Type.ToolTip), a.Name, a.Password, a.Mail, a.Memo, Int32.Parse(DDL_Type.SelectedValue));
                }
                break;
            case "Retailer_Repeater":
                for (int i = 0; i < a.QueryAccountList(4).Rows.Count; i++)
                {
                    DropDownList DDL_Type = (DropDownList)Retailer_Repeater.Items[i].FindControl("DDL_Type");
                    a.SetAccount(Int32.Parse(DDL_Type.ToolTip));
                    a.EditAccount(Int32.Parse(DDL_Type.ToolTip), a.Name, a.Password, a.Mail, a.Memo, Int32.Parse(DDL_Type.SelectedValue));
                }
                break;
        }
        SetInit();
    }

    protected void BT_CreateSingle_Click(object sender, EventArgs e)
    {
        a.CreateAccount(TB_Name.Text, TB_Password.Text, TB_Mail.Text, TB_Memo.Text, Int32.Parse(DDL_Type.SelectedValue));
        SetInit();
        TB_Name.Text = string.Empty;
        TB_Password.Text = string.Empty;
        TB_Mail.Text = string.Empty;
        TB_Memo.Text = string.Empty;
        DDL_Type.SelectedValue = "0";
    }

    protected void BT_CreateExcel_Click(object sender, EventArgs e)
    {
        if (ExcelUpload.HasFile && System.IO.Path.GetExtension(ExcelUpload.FileName).ToLower() == ".xls" || System.IO.Path.GetExtension(ExcelUpload.FileName).ToLower() == ".xlsx")
        {
            string path = Server.MapPath("member/") + DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Day.ToString();
            ExcelUpload.SaveAs(path + "-" + ExcelUpload.FileName);
            DataTable DT = new DataTable();
            string ExcelCS = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                    "Data Source=" + path + "-" + ExcelUpload.FileName + ";" +
                    "Extended Properties=\"Excel 8.0;HDR=YES\"";
            string ExcelSelect = "SELECT * from [Sheet1$]";
            DataSet ds = new DataSet();
            OleDbConnection ExcelConnection = new OleDbConnection(ExcelCS);
            OleDbCommand myExcelCommand = new OleDbCommand(ExcelSelect, ExcelConnection);
            OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myExcelCommand);
            ExcelConnection.Open();
            myDataAdapter.Fill(ds, "ExcelTable");
            ExcelConnection.Close();
            DT = ds.Tables["ExcelTable"];
            for (int i = 0; i < DT.Rows.Count; i++)
                if (DT.Rows[i]["帳號名稱"].ToString() != "" || DT.Rows[i]["密碼"].ToString() != "" || DT.Rows[i]["電子郵件"].ToString() != "" || DT.Rows[i]["角色"].ToString() != "")
                {
                    a.CreateAccount(DT.Rows[i]["帳號名稱"].ToString(), DT.Rows[i]["密碼"].ToString(), DT.Rows[i]["電子郵件"].ToString(), DT.Rows[i]["備註"].ToString(), Int32.Parse(DT.Rows[i]["角色"].ToString()));
                    SetInit();
                }
        }
            
    }
    protected void BT_Next_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/admin_script.aspx");
    }
}
