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
        private HashSet<string> villages;
        private DataTable dt;
        private int villagesId;
        private readonly string villageColName = "nombre departamento";

        public App()
        {
            InitializeComponent();
            this.villages = new HashSet<string>();
            this.dt = new DataTable();
        }

        private void App_Load(object sender, EventArgs e)
        {

        }
        private void BtnOpen_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                dt = ReadData(filePath);
                comboBox.DataSource = villages.ToList();
                dataGrid.DataSource = dt;
                lblOpen.Text = "File succesfully loaded from: '" + filePath + "'";
            }
            else
            {
                lblOpen.Text = "No file opened";
            }
        }

        public DataTable ReadData(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            string[] headers = lines[0].Split(new char[] { ',' });
            int numCols = headers.GetLength(0);
            this.dt = new DataTable();
            string[] Fields;

            //1st row must be column names; force lower case to ensure matching later on.
            for (int i = 0; i < numCols; i++) 
            {
                string colName = headers[i].ToLower(); 
                if(colName == villageColName) villagesId = i;
                dt.Columns.Add(colName, typeof(string));
            }

            DataRow Row;
            for (int i = 1; i < lines.GetLength(0); i++)
            {
                Fields = lines[i].Split(new char[] { ',' });
                if(Fields.GetLength(0) == numCols)
                {
                    Row = dt.NewRow();
                    for (int f = 0; f < numCols; f++) {
                        Row[f] = Fields[f];
                        if (f == villagesId)
                            villages.Add(Fields[f]);
                    }
                    dt.Rows.Add(Row);
                }
            }
            return dt;
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
