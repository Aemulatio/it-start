using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace it_start
{
    public partial class offerScreen : UserControl
    {
        public offerScreen()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=bus.db; Version=3;"))
            {
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM ActiveResp";
                try
                {
                    SQLiteDataReader r = cmd.ExecuteReader();
                    string line = String.Empty;
                    while (r.Read())
                    {
                        
                        line = r["id"].ToString() + ", "
                             + r["username"].ToString() + ", "
                             + r["Astop"].ToString() + ", "
                             + r["Lat"].ToString();
                        Console.WriteLine(line);
                        MessageBox.Show("wowo");
                    }
                    r.Close();
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                conn.Dispose();
            }
        }
    }
}
