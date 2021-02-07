using System;
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
        
        public App()
        {
            InitializeComponent();
            report = new Report();
        }

        private void App_Load(object sender, EventArgs e)
        {

        }
        private void BtnOpen_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                lblOpen.Text = "File read from: " + filePath;
                report.ReadData(filePath);
                dataGrid.DataSource = report.getDataTable();
            } else
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
