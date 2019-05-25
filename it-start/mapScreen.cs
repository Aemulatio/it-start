using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Data.SQLite;

namespace it_start
{
    public partial class mapScreen : UserControl
    {
        private static System.Timers.Timer deletingTimer;
        private static System.Timers.Timer addingTimer;

        public mapScreen()
        {
            InitializeComponent();
            _points = new List<PointLatLng>();
        }

        private PointLatLng aPoint;
        private PointLatLng bPoint;

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

        private List<PointLatLng> _points;

        private void mapScreen_Load(object sender, EventArgs e)
        {
            
            GMapProviders.GoogleMap.ApiKey = @"AIzaSyDFB7V9orDViDZtiduE-K6Q0SbIvoAT55U";
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            gMapControl1.ShowCenter = false;
            gMapControl1.Position = new PointLatLng(50.273101, 127.537152);
        }

        private GMapOverlay markers;
        private GMapOverlay routes;

        public void button1_Click(object sender, EventArgs e)
        {
            _points.Clear();
            gMapControl1.Overlays.Clear();
            markers = new GMapOverlay("markers");
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=bus.db; Version=3;"))
            {
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM ActiveResp";
                try
                {
                    SQLiteDataReader r = cmd.ExecuteReader();
                    double ALat = 0;
                    double ALon = 0;
                    double BLat = 0;
                    double BLon = 0;
                    string username = String.Empty;
                    while (r.Read())
                    {
                        username = "" + r["username"];
                        ALat = double.Parse(r["ALat"].ToString());
                        ALon = double.Parse(r["ALon"].ToString());
                        BLat = double.Parse(r["BLat"].ToString());
                        BLon = double.Parse(r["BLon"].ToString());
                        if (_points.Any(point => (point.Lat == ALat) && (point.Lng == ALon)))
                        {
                            continue;
                        }
                        else
                        {
                            _points.Add(new PointLatLng(ALat, ALon));
                            markers.Markers.Add(new GMarkerGoogle(new PointLatLng(ALat, ALon),
                                GMarkerGoogleType.green_small));
                        }

                        if (_points.Any(point => (point.Lat == BLat) && (point.Lng == BLon)))
                        {
                            continue;
                        }
                        else
                        {
                            _points.Add(new PointLatLng(BLat, BLon));
                            markers.Markers.Add(new GMarkerGoogle(new PointLatLng(BLat, BLon),
                                GMarkerGoogleType.red_small));
                        }

                        gMapControl1.Overlays.Add(markers);
                    }

                    r.Close();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                conn.Dispose();
            }

            routes = new GMapOverlay("routes");
            for (int i = 0; i < _points.Count - 1; i++)
            {
                var route = GoogleMapProvider.Instance.GetRoute(_points[i], _points[i + 1], false, false, 13);
                var r = new GMapRoute(route.Points, "Route" + i.ToString())
                {
                    Stroke = new Pen(Color.Red, 3)
                };
                routes.Routes.Add(r);
            }

            gMapControl1.Overlays.Add(routes);

            gMapControl1.Zoom++;
            gMapControl1.Zoom--;

            additionalPoints = new List<PointLatLng>();
            additionalPoints.Add(new PointLatLng(50.262690, 127.540563));
            additionalPoints.Add(new PointLatLng(50.259903, 127.513798));
            additionalPoints.Add(new PointLatLng(50.262110, 127.546652));
            additionalPoints.Add(new PointLatLng(50.260367, 127.566264));
            additionalPoints.Add(new PointLatLng(50.267106, 127.552631));
            additionalPoints.AddRange(_points);

            _points.Clear();
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (routes.Routes.Count > 0)
            {
                routes?.Routes.RemoveAt(0);
            }

            if (markers.Markers.Count > 1)
            {
                markers.Markers.RemoveAt(0);
            }
            else
            {
                markers.Clear();
            }
        }

        private List<PointLatLng> additionalPoints;

        private void AddingEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Random random = new Random();
            int temp = random.Next(0, additionalPoints.Count);
            int start = random.Next(0, additionalPoints.Count);
            while (start == temp)
            {
                start = random.Next(0, additionalPoints.Count);
            }

            if (routes.Routes.Count < 10)
            {
                markers.Markers.Add(new GMarkerGoogle(additionalPoints[start],
                    GMarkerGoogleType.green_small));
                markers.Markers.Add(new GMarkerGoogle(additionalPoints[temp],
                    GMarkerGoogleType.red_small));


                var route = GoogleMapProvider.Instance.GetRoute(additionalPoints[start], additionalPoints[temp], false,
                    false, 13);
                var r = new GMapRoute(route.Points, "Route")
                {
                    Stroke = new Pen(Color.Red, 3)
                };
                routes.Routes.Add(r);
            }

            //50.262690, 127.540563
            //50.259903, 127.513798
            //50.262110, 127.546652
            //50.260367, 127.566264
            //50.267106, 127.552631
        }

        private void mapScreen_Enter(object sender, EventArgs e)
        {
            deletingTimer = new System.Timers.Timer(); 
            addingTimer = new System.Timers.Timer();

            deletingTimer.Interval = 6000;
            addingTimer.Interval = 5000;
            // Hook up the Elapsed event for the timer. 
            deletingTimer.Elapsed += OnTimedEvent;
            addingTimer.Elapsed += AddingEvent;

            // Have the timer fire repeated events (true is the default)
            deletingTimer.AutoReset = true;
            addingTimer.AutoReset = true;

            // Start the timer
            deletingTimer.Enabled = true;
            addingTimer.Enabled = true;

        }
    }
}