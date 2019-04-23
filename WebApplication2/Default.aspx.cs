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
        static OleDbConnection conn = null;
        static OleDbDataReader reader = null;
        static Object x = false;
        private void LoadDB()
        {
            //prevent multiple threads from accessing database at same time
            lock(x)
            {
                if (conn == null)
                {
                    conn = new OleDbConnection(
                    "Provider=Microsoft.ACE.OLEDB.12.0; " +
                    "Data Source=" + Server.MapPath("Data/Mmsydata.accdb"));
                    conn.Open();
                }
            }
        }
        //creates dropdownlist populated with Type table
        private void CreateTypeList()
        {

            if (conn == null || reader == null)
            {
                    OleDbCommand cmd =
                        new OleDbCommand("Select * FROM Type", conn);
                    reader = cmd.ExecuteReader();
                    DropDownList1.DataSource = reader;
                    DropDownList1.DataTextField = "objtype";
                    DropDownList1.DataBind();
            }
        }
        //when button is clicked, redirect to search page
        //with selected type and search string
        protected void Btn_Click(object sender, EventArgs e)
        {
            //sanitize input to prevent SQL injection
            string value = txtSearch.Text.Trim();
            value = value.Replace('\'', ' ');
            string selection = DropDownList1.SelectedValue;
            selection = selection.Replace('\'', ' ');

            string url = "Search.aspx?query=" + HttpUtility.UrlEncode(value)
                + "&type=" + selection;
            Response.Redirect(url);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadDB();
            if (!IsPostBack)
            {
                CreateTypeList();
            }
        }
        static void OnProcessExit(object sender, EventArgs e)
        {
            if (reader != null) reader.Close();
            if (conn != null) conn.Close();
        }
    }
}