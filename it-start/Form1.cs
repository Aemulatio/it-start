﻿using System;
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            offerScreen1.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mapScreen1.BringToFront();
            mapScreen1.APoint = offerScreen1.APoint;
            mapScreen1.BPoint = offerScreen1.BPoint;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            adminPanel1.BringToFront();
        }
    }
}