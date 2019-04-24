using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Drawing;

namespace WebApplication2
{

    public partial class _Default : Page
    {
        //creates dropdownlist populated with Type table
        private void CreateTypeList()
        {
            OleDbDataReader reader = DBManager.dBManager.RunCMD("Select * FROM Type");
            DropDownList1.DataSource = reader;
            DropDownList1.DataTextField = "objtype";
            DropDownList1.DataBind();
            reader.Close();
        }
        //when button is clicked, redirect to search page
        //with selected type and search string
        protected void Btn_Click(object sender, EventArgs e)
        {
            //sanitize input to prevent SQL injection
            string value = txtSearch.Text.Trim();
            value = value.Replace('\'', ' ');
            string selection = HttpUtility.UrlEncode(DropDownList1.SelectedValue);
            selection = selection.Replace('\'', ' ');

            string url = "Search.aspx?query=" + HttpUtility.UrlEncode(value)
                + "&type=" + selection;
            Response.Redirect(url);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!IsPostBack)
            {
                CreateTypeList();
            }
        }
        static void OnProcessExit(object sender, EventArgs e)
        {
            DBManager.dBManager.Close();
        }
    }
}