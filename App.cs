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
                report = new Report();
                report.ReadData(filePath);
                this.dt = report.getDataTable();
                loadFields();
                dataGrid.DataSource = dt;
                lblOpen.Text = "File succesfully loaded from: '" + filePath + "'";

                barChart();
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


        private void barChart()
        {
            DataTable dt = report.getDataTable();

            Dictionary<string, int> types = new Dictionary<string, int>();

            foreach(DataRow row in dt.Rows)
            {
                //Console.WriteLine(row.Field<string>(4));
                string curr = row.Field<string>(4);
                if(types.ContainsKey(curr))
                {
                    types[curr] = types[curr] + 1;
                }
                else
                {
                    types.Add(curr, 1);
                }
            }

            

            
            foreach (KeyValuePair<string, int> entry in types)
            {
                Console.WriteLine(entry.Key + " " + entry.Value);
                //chart.Series.AddXY(entry.Key, entry.Value);
                chart.Series["Cantidad"].Points.AddXY(entry.Key, entry.Value);

            }

            

        }

        private void CBoxUniqueVals_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedField = (string)CBoxField.SelectedItem;
            string selectedValue = (string)CBoxUniqueVals.SelectedItem;

            dataGrid.DataSource = dt.AsEnumerable()
                                    .Where(row => row.Field<string>(selectedField) == selectedValue)
                                    .CopyToDataTable();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            dataGrid.DataSource = dt;
        }

        private void lblOpen_Click(object sender, EventArgs e)
        {

        }
    }
}
