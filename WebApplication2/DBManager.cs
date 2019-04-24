using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace WebApplication2
{
    public class DBManager : Page
    {

        public static DBManager dBManager = new DBManager();
        OleDbConnection conn = null;
        readonly Object x = false;
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

        public OleDbDataReader RunCMD(string cmd)
        {
            LoadDB();
            lock (x)
            {
                return new OleDbCommand(cmd, conn).ExecuteReader();
            }
        }

        public void Close()
        {
            lock (x)
            {
                if (conn != null)
                {
                    conn.Close();
                    conn = null;
                }
            }
        }
    }

}