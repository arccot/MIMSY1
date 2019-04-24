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
        //Display full MIMSY1 record and all associated documents with same counter
        protected void DisplayTable()
        {
            string counter = Request.QueryString["counter"].Replace('\'', ' ');
            string query = "Select * FROM MIMSY1 WHERE [counter]=" + counter;

            OleDbDataReader reader = DBManager.dBManager.RunCMD(query);
            GridView3.DataSource = reader;
            GridView3.DataBind();
            reader.Close();

            query = "Select [Document Name], DateCreated FROM DOCUMENTS WHERE [counter]=" + counter;
            reader = DBManager.dBManager.RunCMD(query);
            GridView4.DataSource = reader;
            GridView4.DataBind();
            reader.Close();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DisplayTable();
        }
        static void OnProcessExit(object sender, EventArgs e)
        {
            DBManager.dBManager.Close();
        }
    }
}