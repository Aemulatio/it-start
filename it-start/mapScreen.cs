using System;
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

namespace it_start
{
    public partial class mapScreen : UserControl
    {
        public mapScreen()
        {
            InitializeComponent();
            _points = new List<PointLatLng>();
        }

        private PointF aPoint;
        private PointF bPoint;

        public PointF APoint
        {
            get { return aPoint; }
            set { aPoint = value; }
        }

        public PointF BPoint
        {
            get { return bPoint; }
            set { bPoint = value; }
        }

        private List<PointLatLng> _points;

        private void mapScreen_Load(object sender, EventArgs e)
        {
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gMapControl1.ShowCenter = false;
            gMapControl1.Position = new PointLatLng(50.273101, 127.537152);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(APoint);

            if (APoint != null && BPoint != null)
            {
                _points.Add(new PointLatLng(Convert.ToDouble(APoint.X), Convert.ToDouble(APoint.Y)));
                _points.Add(new PointLatLng(Convert.ToDouble(BPoint.X), Convert.ToDouble(BPoint.Y)));

                foreach (var VARIABLE in _points)
                {
                    Console.WriteLine(VARIABLE);
                }
                var route = GoogleMapProvider.Instance.GetRoute(_points[0], _points[1], false, false, 13);
                var r = new GMapRoute(route.Points, "Route")
                {
                    Stroke =  new Pen(Color.Red, 5)
                };

                var routes = new GMapOverlay("routes");
                routes.Routes.Add(r);
                gMapControl1.Overlays.Add(routes);
            }
        }
    }
}