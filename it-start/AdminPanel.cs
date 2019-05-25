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
    public partial class AdminPanel : UserControl
    {
        public AdminPanel()
        {
            InitializeComponent();
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=bus.db; Version=3;"))
            {
                dataGridView1.RowHeadersVisible = false;
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM ActiveResp";
                try
                {
                    SQLiteDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        dataGridView1.Rows.Add(r["username"].ToString(), r["Astop"].ToString(), r["Bstop"].ToString());
                    }

                    r.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                conn.Dispose();
                dataGridView1.BorderStyle = BorderStyle.None; dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249); dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise; dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke; dataGridView1.BackgroundColor = Color.White; dataGridView1.EnableHeadersVisualStyles = false; dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None; dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72); dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            }
        }
    }
}
