using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class _Details : System.Web.UI.Page
    {
        static OleDbConnection conn = null;

        protected void DisplayTable()
        {
            string counter = Request.QueryString["counter"];
            string query = "Select * FROM MIMSY1 WHERE [counter]=" + counter;

            OleDbDataReader reader2 = null;
            OleDbCommand cmd =
                new OleDbCommand(query, conn);
            reader2 = cmd.ExecuteReader();
            GridView3.DataSource = reader2;
            GridView3.DataBind();

            query = "Select [Document Name], DateCreated FROM DOCUMENTS WHERE [counter]=" + counter;
            cmd = new OleDbCommand(query, conn);
            reader2 = cmd.ExecuteReader();
            GridView4.DataSource = reader2;
            GridView4.DataBind();
        }

        private void LoadDB()
        {
            conn = new OleDbConnection(
    "Provider=Microsoft.ACE.OLEDB.12.0; " +
    "Data Source=" + Server.MapPath("Data/Mmsydata.accdb"));
            conn.Open();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadDB();
            DisplayTable();
        }
    }
}