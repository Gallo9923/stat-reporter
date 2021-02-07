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
        private DataTable dt;

        private HashSet<string> deptType;

        public Report()
        {

        }

        public void ReadData(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            string[] headers = lines[0].Split(new char[] { ',' });
            int numCols = headers.GetLength(0);
            dt = new DataTable();
            string[] Fields;

            //1st row must be column names; force lower case to ensure matching later on.
            for (int i = 0; i < numCols; i++)
                dt.Columns.Add(headers[i].ToLower(), typeof(string));
            DataRow Row;
            for (int i = 1; i < lines.GetLength(0); i++)
            {
                Fields = lines[i].Split(new char[] { ',' });
                if (Fields.GetLength(0) == numCols)
                {
                    Row = dt.NewRow();
                    for (int f = 0; f < numCols; f++)
                        Row[f] = Fields[f];
                    dt.Rows.Add(Row);
                }
            }

        }

        public DataTable getDataTable()
        {
            return dt;
        }

    }
}
