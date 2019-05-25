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
using GMap.NET;

namespace it_start
{
    public partial class offerScreen : UserControl
    {
        public offerScreen()
        {
            InitializeComponent();
        }

        PointLatLng aPoint, bPoint;

        public PointLatLng APoint
        {
            get { return aPoint; }
            set { aPoint = value; }
        }

        public PointLatLng BPoint
        {
            get { return bPoint; }
            set { bPoint = value; }
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0: // "Кузнечная(Ленина)":
                    APoint = PointLatLng.Add(new PointLatLng(50.256451, 127.547085), SizeLatLng.Empty);
                    break;
                case 1: // "Театральная(Зейская)":
                    APoint = PointLatLng.Add(new PointLatLng(50.259320, 127.550686), SizeLatLng.Empty);
                    break;
                case 2: //"Шимановского(Горького)":
                    APoint = PointLatLng.Add(new PointLatLng(50.265439, 127.542868), SizeLatLng.Empty);
                    break;
                case 3: //"Универмаг":
                    APoint = PointLatLng.Add(new PointLatLng(50.262894, 127.534690), SizeLatLng.Empty);
                    break;
                case 4: //"Шимановского(Ленина)":
                    APoint = PointLatLng.Add(new PointLatLng(50.257093, 127.540564), SizeLatLng.Empty);
                    break;
                default:
                    break;
            }

            switch (comboBox2.SelectedIndex)
            {
                case 0: // "Кузнечная(Ленина)":

                    BPoint = PointLatLng.Add(new PointLatLng(50.256451, 127.547085), SizeLatLng.Empty);
                    break;
                case 1: // "Театральная(Зейская)":

                    BPoint = PointLatLng.Add(new PointLatLng(50.259320, 127.550686), SizeLatLng.Empty);

                    break;
                case 2: //"Шимановского(Горького)":

                    BPoint = PointLatLng.Add(new PointLatLng(50.265439, 127.542868), SizeLatLng.Empty);

                    break;
                case 3: //"Универмаг":

                    BPoint = PointLatLng.Add(new PointLatLng(50.262894, 127.534690), SizeLatLng.Empty);

                    break;
                case 4: //"Шимановского(Ленина)":

                    BPoint = PointLatLng.Add(new PointLatLng(50.257093, 127.540564), SizeLatLng.Empty);
                    break;
                default:
                    break;
            }

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=bus.db; Version=3;"))
            {
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();

                try
                {
                    cmd.CommandText = "INSERT INTO ActiveResp (Astop, Bstop, ALon, ALat, BLon, BLat) VALUES ('" +
                                      comboBox1.Text + "','" + comboBox2.Text + "'," + APoint.Lng + "," + APoint.Lat +
                                      "," + BPoint.Lng +
                                      "," + BPoint.Lat + ")";
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