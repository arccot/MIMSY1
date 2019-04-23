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
        static OleDbDataReader reader = null;
        //Display full MIMSY1 record and all associated documents with same counter
        protected void DisplayTable()
        {
            string counter = Request.QueryString["counter"];
            string query = "Select * FROM MIMSY1 WHERE [counter]=" + counter;

            OleDbCommand cmd =
                new OleDbCommand(query, conn);
            reader = cmd.ExecuteReader();
            GridView3.DataSource = reader;
            GridView3.DataBind();

            query = "Select [Document Name], DateCreated FROM DOCUMENTS WHERE [counter]=" + counter;
            cmd = new OleDbCommand(query, conn);
            reader = cmd.ExecuteReader();
            GridView4.DataSource = reader;
            GridView4.DataBind();
        }
        static Object x = false;
        private void LoadDB()
        {
            //prevent multiple threads from accessing database
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

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadDB();
            DisplayTable();
        }
        static void OnProcessExit(object sender, EventArgs e)
        {
            if (reader != null) reader.Close();
            if (conn != null) conn.Close();
        }
    }
}