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
            int caseSwitch1 = comboBox1.SelectedIndex;
            int caseSwitch2 = comboBox2.SelectedIndex;
            string aLat, aLon, bLat, bLon;

            switch (caseSwitch1)
            {
                case 0: // "Кузнечная(Ленина)":
                    aLat = "50.256451";
                    aLon = "127.547085";
                    MessageBox.Show(aLat + ", " +aLon);
                    break;
                case 1: // "Театральная(Зейская)":
                    aLat = "50.259320";
                    aLon = "127.550686";
                    MessageBox.Show(aLat + ", " + aLon);
                    break;
                case 2: //"Шимановского(Горького)":
                    aLat = "50.265439";
                    aLon = "127.542868";
                    MessageBox.Show(aLat + ", " + aLon);
                    break;
                case 3: //"Универмаг":
                    aLat = "50.262894";
                    aLon = "127.534690";
                    MessageBox.Show(aLat + ", " + aLon);
                    break;
                case 4: //"Шимановского(Ленина)":
                    aLat = "50.257093";
                    aLon = "127.540564";
                    MessageBox.Show(aLat + ", " + aLon);
                    break;
                default:
                    aLat = "0";
                    aLon = "0";
                    break;
            }

            switch (caseSwitch2)
            {
                case 0: // "Кузнечная(Ленина)":
                    bLat = "50.256451";
                    bLon = "127.547085";
                    MessageBox.Show(bLat + ", " + bLon);
                    break;
                case 1: // "Театральная(Зейская)":
                    bLat = "50.259320";
                    bLon = "127.550686";
                    MessageBox.Show(bLat + ", " + bLon);
                    break;
                case 2: //"Шимановского(Горького)":
                    bLat = "50.265439";
                    bLon = "127.542868";
                    MessageBox.Show(bLat + ", " + bLon);
                    break;
                case 3: //"Универмаг":
                    bLat = "50.262894";
                    bLon = "127.534690";
                    MessageBox.Show(bLat + ", " + bLon);
                    break;
                case 4: //"Шимановского(Ленина)":
                    bLat = "50.257093";
                    bLon = "127.540564";
                    MessageBox.Show(bLat + ", " + bLon);
                    break;
                default:
                    bLat = "0";
                    bLon = "0";
                    break;
            }

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=bus.db; Version=3;"))
            {
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                
                try
                {
                    cmd.CommandText = "INSERT INTO ActiveResp (Astop, Bstop, ALon, ALat, BLon, BLat) VALUES ('"+comboBox1.Text+"','"+comboBox2.Text+"',"+aLon+","+aLat+","+bLon+","+bLat+")";
                    cmd.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                conn.Dispose();
            }
        }
    }
}
