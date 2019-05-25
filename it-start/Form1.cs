using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace it_start
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            offerScreen1.BringToFront();

            pointerPanel.Width = button1.Width;
            pointerPanel.Top = button1.Top + button1.Height;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            offerScreen1.BringToFront();

            pointerPanel.Width = button1.Width;
            pointerPanel.Top = button1.Top + button1.Height;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mapScreen1.BringToFront();
            mapScreen1.APoint = offerScreen1.APoint;
            mapScreen1.BPoint = offerScreen1.BPoint;

            pointerPanel.Width = button2.Width;
            pointerPanel.Top = button2.Top + button2.Height;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            adminPanel1.BringToFront();

            pointerPanel.Width = button3.Width;
            pointerPanel.Top = button3.Top + button2.Height;
        }
    }
}