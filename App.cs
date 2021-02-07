﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stats_reporter
{
    public partial class App : Form
    {
        private Report report;

        private HashSet<string> villages;
        private DataTable dt;
        private int villagesId;
        private readonly string villageColName = "nombre departamento";


        public App()
        {
            InitializeComponent();
            report = new Report();

            this.villages = new HashSet<string>();
            this.dt = new DataTable();

        }

        private void App_Load(object sender, EventArgs e)
        {

        }
        private void BtnOpen_Click(object sender, EventArgs e)
        {

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                report.ReadData(filePath);
                dt = report.getDataTable();
                comboBox.DataSource = villages.ToList();
                dataGrid.DataSource = dt;
                lblOpen.Text = "File succesfully loaded from: '" + filePath + "'";
            }
            else
            {
                lblOpen.Text = "No file opened";
            }
        }


        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        

        private void chart_Click(object sender, EventArgs e)
        {

        }
    }
}
