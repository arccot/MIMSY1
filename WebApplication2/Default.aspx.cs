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


        private void LoadDB()
        {
            conn = new OleDbConnection(
    "Provider=Microsoft.ACE.OLEDB.12.0; " +
    "Data Source=" + Server.MapPath("Data/Mmsydata.accdb"));
            conn.Open();
        }

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

                //        catch (Exception e)
                //        {
                //            Response.Write(e.Message);
                //            Response.End();
                //        }
            }
        }
        protected void Btn_Click(object sender, EventArgs e)
        {
            //sanitize input
            string value = txtSearch.Text.Trim();
            value = value.Replace('\'', ' ');
            string selection = DropDownList1.SelectedValue;
            selection = selection.Replace('\'', ' ');

            string url = "Search.aspx?query=" + HttpUtility.UrlEncode(value)
                + "&type=" + selection;
            Response.Redirect(url);

            //OleDbDataReader reader2 = null;
            //string value = txtSearch.Text.Trim();
            //string selection = DropDownList1.SelectedValue;

            ////sanitize input
            //value = value.Replace('\'', ' ');
            //selection = selection.Replace('\'', ' ');
            //string query = "Select * FROM MIMSY1 WHERE [objname] LIKE '%"
            //    + value + "%' AND [objtype]='" + selection + "' ";
            //OleDbCommand cmd =
            //    new OleDbCommand(query, conn);
            //reader2 = cmd.ExecuteReader();

            //GridView1.DataSource = reader2;
            //GridView1.DataBind();
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