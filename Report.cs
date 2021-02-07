using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

namespace stats_reporter
{
    class Report
    {
        private DataTable dataTable;
        private Dictionary<string, HashSet<string>> cols2UniqueVals;
        
        public Report()
        {
            dataTable = new DataTable();
            cols2UniqueVals = new Dictionary<string, HashSet<string>>();
        }

        public void ReadData(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            string[] headers = lines[0].Split(new char[] { ',' });

            // Lowercase column names
            for (int i = 0; i < headers.GetLength(0); i++)
                headers[i] = headers[i].ToLower();

            initializeKeys(headers);

            int numCols = headers.GetLength(0);
            string[] Fields;

            //1st row must be column names; force lower case to ensure matching later on.
            for (int i = 0; i < numCols; i++)
                dataTable.Columns.Add(headers[i], typeof(string));

            DataRow Row;
            for (int i = 1; i < lines.GetLength(0); i++)
            {
                Fields = lines[i].Split(new char[] { ',' });
                if (Fields.GetLength(0) == numCols)
                {
                    Row = dataTable.NewRow();
                    for (int f = 0; f < numCols; f++) {
                        Row[f] = Fields[f];
                        // Add value to list of unique values of 
                        cols2UniqueVals[headers[f]].Add(Fields[f]);
                    }
                    dataTable.Rows.Add(Row);
                }
            }
        }

        private void initializeKeys(string[] headers)
        {
            for(int i = 0; i < headers.GetLength(0); i++)
                this.cols2UniqueVals.Add(headers[i], new HashSet<string>());
        }

        public DataTable getDataTable()
        {
            return this.dataTable;
        }

        public Dictionary<string, HashSet<string>> getCols2Unique()
        {
            return cols2UniqueVals;
        }
    }
}
