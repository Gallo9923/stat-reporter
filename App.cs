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
        public App()
        {
            InitializeComponent();
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
                DataTable dt = ReadData(filePath);
                dataGrid.DataSource = dt;
            } else
            {
                lblOpen.Text = "No file opened";
            }
        }

        public DataTable ReadData(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            string[] headers = lines[0].Split(new char[] { ',' });
            int numCols = headers.GetLength(0);
            DataTable dt = new DataTable();
            string[] Fields;

            //1st row must be column names; force lower case to ensure matching later on.
            for (int i = 0; i < numCols; i++)
                dt.Columns.Add(headers[i].ToLower(), typeof(string));
            DataRow Row;
            for (int i = 1; i < lines.GetLength(0); i++)
            {
                Fields = lines[i].Split(new char[] { ',' });
                if(Fields.GetLength(0) == numCols)
                {
                    Row = dt.NewRow();
                    for (int f = 0; f < numCols; f++)
                        Row[f] = Fields[f];
                    dt.Rows.Add(Row);
                }
            }
            return dt;
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void chart_Click(object sender, EventArgs e)
        {

        }
    }
}
