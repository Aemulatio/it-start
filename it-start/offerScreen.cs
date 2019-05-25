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
        float aLat, aLon, bLat, bLon;

        public float ALat
        {
            get { return aLat; }
            set { aLat = value; }
        }

        public float ALon
        {
            get { return aLon; }
            set { aLon = value; }
        }

        public float BLat
        {
            get { return bLat; }
            set { bLat = value; }
        }

        public float BLon
        {
            get { return bLon; }
            set { bLon = value; }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0: // "Кузнечная(Ленина)":
                    ALat = 50.256451f;
                    ALon = 127.547085f;
                    MessageBox.Show(ALat + ", " +ALon);
                    break;
                case 1: // "Театральная(Зейская)":
                    ALat = 50.259320f;
                    ALon = 127.550686f;
                    MessageBox.Show(ALat + ", " + ALon);
                    break;
                case 2: //"Шимановского(Горького)":
                    ALat = 50.265439f;
                    ALon = 127.542868f;
                    MessageBox.Show(ALat + ", " + ALon);
                    break;
                case 3: //"Универмаг":
                    ALat = 50.262894f;
                    ALon = 127.534690f;
                    MessageBox.Show(ALat + ", " + ALon);
                    break;
                case 4: //"Шимановского(Ленина)":
                    ALat = 50.257093f;
                    ALon = 127.540564f;
                    MessageBox.Show(ALat + ", " + ALon);
                    break;
                default:
                    ALat = 0f;
                    ALon = 0f;
                    break;
            }

            switch (comboBox2.SelectedIndex)
            {
                case 0: // "Кузнечная(Ленина)":
                    BLat = 50.256451f;
                    BLon = 127.547085f;
                    MessageBox.Show(BLat + ", " + BLon);
                    break;
                case 1: // "Театральная(Зейская)":
                    BLat = 50.259320f;
                    BLon = 127.550686f;
                    MessageBox.Show(BLat + ", " + BLon);
                    break;
                case 2: //"Шимановского(Горького)":
                    BLat = 50.265439f;
                    BLon = 127.542868f;
                    MessageBox.Show(BLat + ", " + BLon);
                    break;
                case 3: //"Универмаг":
                    BLat = 50.262894f;
                    BLon = 127.534690f;
                    MessageBox.Show(BLat + ", " + BLon);
                    break;
                case 4: //"Шимановского(Ленина)":
                    BLat = 50.257093f;
                    BLon = 127.540564f;
                    MessageBox.Show(BLat + ", " + BLon);
                    break;
                default:
                    BLat = 0f;
                    BLon = 0f;
                    break;
            }

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=bus.db; Version=3;"))
            {
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                
                try
                {
                    cmd.CommandText = "INSERT INTO ActiveResp (Astop, Bstop, ALon, ALat, BLon, BLat) VALUES ('"+comboBox1.Text+"','"+comboBox2.Text+"',"+ALon+","+ALat+","+BLon+","+BLat+")";
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
