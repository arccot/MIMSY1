using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class _Search : System.Web.UI.Page
    {
        static OleDbConnection conn = null;
        static OleDbDataReader reader = null;
       
        //selects entries from database that match search queries, and displays in table
        protected void DisplayTable()
        {
            string searchQuery = Request.QueryString["query"];
            string typeQuery = Request.QueryString["type"];
            string query = "Select counter, Sorts, objname, description, objtype, Key1 FROM MIMSY1 WHERE [objname] LIKE '%"
                + searchQuery + "%' AND [objtype]='" + typeQuery + "' ";

            OleDbCommand cmd =
                new OleDbCommand(query, conn);
            reader = cmd.ExecuteReader();

            GridView2.DataSource = reader;
            GridView2.DataBind();
        }
        //when button is clicked, redirect to Details page,
        //passing the counter of the selected row
        protected void DisplayFullRecord(object sender, EventArgs e)
        {
            if (GridView2.HasAttributes)
            {
                GridViewRow row = GridView2.SelectedRow;
                string value = row.Cells[0].Text;
                string url = "Details.aspx?counter=" + HttpUtility.UrlEncode(value);
                Response.Redirect(url);
            }

        }
        
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //hide counts column, which should not be displayed
            e.Row.Cells[0].Visible = false;
            //make rows selectable, add hover text
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] =
                    Page.ClientScript.GetPostBackClientHyperlink(GridView2, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
            //set headers for table
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Text = "Sorts";
                e.Row.Cells[2].Text = "Name";
                e.Row.Cells[3].Text = "Description";
                e.Row.Cells[4].Text = "Type";
                e.Row.Cells[5].Text = "Key";
            }
        }
        //select row when clicked on
        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView2.Rows)
            {
                if (row.RowIndex == GridView2.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = string.Empty;
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click to select this row.";
                }
            }
        }
        static Object x = false;
        private void LoadDB()
        {
            //prevent multiple threads from accessing database at same time
            lock (x)
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