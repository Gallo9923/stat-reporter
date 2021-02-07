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
        private DataTable dt;

        public App()
        {
            InitializeComponent();
            report = new Report();
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
                this.dt = report.getDataTable();
                loadFields();
                dataGrid.DataSource = dt;
                lblOpen.Text = "File succesfully loaded from: '" + filePath + "'";
            }
            else
            {
                lblOpen.Text = "No file opened";
            }
        }

        public void loadFields()
        {
            List<string> fields = new List<string>();
            foreach(DataColumn col in dt.Columns)
                fields.Add( col.ToString() );

            CBoxField.DataSource = fields;
        }

        private void CBoxField_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedField = (string)CBoxField.SelectedItem;
            CBoxUniqueVals.DataSource = report.getCols2Unique()[selectedField].ToList();
        }
        
        private void chart_Click(object sender, EventArgs e)
        {

        }

        private void CBoxUniqueVals_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
