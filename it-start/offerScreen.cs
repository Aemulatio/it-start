using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace it_start
{
    public partial class offerScreen : UserControl
    {
        public offerScreen()
        {
            InitializeComponent();
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
    }
}
