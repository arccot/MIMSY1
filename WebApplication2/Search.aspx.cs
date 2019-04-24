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
       
        //selects entries from database that match search queries, and displays in table
        protected void DisplayTable()
        {
            string searchQuery = Request.QueryString["query"].Replace('\'', ' ');
            string typeQuery = Request.QueryString["type"].Replace('\'', ' ');
            string query = "Select counter, Sorts, objname, description, objtype, Key1 FROM MIMSY1 WHERE [objname] LIKE '%"
                + searchQuery + "%' AND [objtype]='" + typeQuery + "' ";
            OleDbDataReader reader = DBManager.dBManager.RunCMD(query);

            GridView2.DataSource = reader;
            GridView2.DataBind();
            reader.Close();

        }
        //when button is clicked, redirect to Details page,
        //passing the counter of the selected row
        protected void DisplayFullRecord(object sender, EventArgs e)
        {
            if (GridView2.SelectedRow != null)
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
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            DisplayTable();
        }
        static void OnProcessExit(object sender, EventArgs e)
        {
            DBManager.dBManager.Close();
        }
    }
}