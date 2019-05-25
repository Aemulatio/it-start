﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using System.Data.SQLite;

namespace it_start
{
    public partial class mapScreen : UserControl
    {
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

        private void button1_Click(object sender, EventArgs e)
        {
            _points.Clear();
            gMapControl1.Overlays.Clear();
            var markers = new GMapOverlay("markers");
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
                        MessageBox.Show(username + " " + ALat + " " + ALon + " " + BLat + " " + BLon);
                        _points.Add(new PointLatLng(ALat, ALon));
                        _points.Add(new PointLatLng(BLat, BLon));
                        markers.Markers.Add(new GMarkerGoogle(new PointLatLng(ALat, ALon),
                            GMarkerGoogleType.green_small));
                        markers.Markers.Add(new GMarkerGoogle(new PointLatLng(BLat, BLon),
                            GMarkerGoogleType.red_small));

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

            var routes = new GMapOverlay("routes");
            for (int i = 0; i < _points.Count - 1; i++)
            {
                var route = GoogleMapProvider.Instance.GetRoute(_points[i], _points[i + 1], false, false, 13);
                var r = new GMapRoute(route.Points, "Route1")
                {
                    Stroke = new Pen(Color.Red, 5)
                };
                routes.Routes.Add(r);
            }

            gMapControl1.Overlays.Add(routes);


            //markers.Markers.Add(new GMarkerGoogle(new PointLatLng(ALat, ALon), GMarkerGoogleType.red_small));
            //gMapControl1.Overlays.Add(markers);


            //if (APoint != PointLatLng.Empty  && BPoint != PointLatLng.Empty)
            //{
            //    _points.Add(new PointLatLng(APoint.Lat, APoint.Lng));
            //    _points.Add(new PointLatLng(BPoint.Lat, BPoint.Lng));

            //    var markers = new GMapOverlay("markers");
            //    var marker = new GMarkerGoogle(APoint, GMarkerGoogleType.red_small);
            //    markers.Markers.Add(marker);
            //    markers.Markers.Add(new GMarkerGoogle(BPoint, GMarkerGoogleType.red_small));
            //    gMapControl1.Overlays.Add(markers);

            //    foreach (var VARIABLE in _points)
            //    {
            //        Console.WriteLine(VARIABLE.Lat);
            //    }

            //    var route = GoogleMapProvider.Instance.GetRoute(_points[0], _points[1], false, false, 13);

            //    foreach (var VARIABLE in route.Points)
            //    {
            //        Console.WriteLine(VARIABLE);
            //    }

            //    var r = new GMapRoute(route.Points, "Route")
            //    {
            //        Stroke = new Pen(Color.Red, 5)
            //    };

            //    foreach (var VARIABLE in r.LocalPoints)
            //    {
            //        Console.WriteLine(VARIABLE + " - r");
            //    }

            //    var routes = new GMapOverlay("routes");
            //    routes.Routes.Add(r);
            //    gMapControl1.Overlays.Add(routes);
            //}

            //if (APoint != PointLatLng.Empty && BPoint != PointLatLng.Empty)
            //{
            //    _points.Add(new PointLatLng(APoint.Lat, APoint.Lng));
            //    _points.Add(new PointLatLng(BPoint.Lat, BPoint.Lng));

            //    var markers = new GMapOverlay("markers");
            //    var marker = new GMarkerGoogle(APoint, GMarkerGoogleType.red_small);
            //    markers.Markers.Add(marker);
            //    markers.Markers.Add(new GMarkerGoogle(BPoint, GMarkerGoogleType.red_small));
            //    gMapControl1.Overlays.Add(markers);

            //    foreach (var VARIABLE in _points)
            //    {
            //        Console.WriteLine(VARIABLE.Lat);
            //    }

            //    var route = GoogleMapProvider.Instance.GetRoute(_points[0], _points[1], false, false, 13);

            //    foreach (var VARIABLE in route.Points)
            //    {
            //        Console.WriteLine(VARIABLE);
            //    }

            //    var r = new GMapRoute(route.Points, "Route")
            //    {
            //        Stroke = new Pen(Color.Red, 5)
            //    };

            //    foreach (var VARIABLE in r.LocalPoints)
            //    {
            //        Console.WriteLine(VARIABLE + " - r");
            //    }

            //    var routes = new GMapOverlay("routes");
            //    routes.Routes.Add(r);
            //    gMapControl1.Overlays.Add(routes);
            //}
        }
    }
}