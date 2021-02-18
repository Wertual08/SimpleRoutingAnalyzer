using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleRoutingAnalyzer
{
    static class DataGridViewExt
    {
        public static void ExportCSV(this DataGridView dgv, string path)
        {
            using (var file = File.CreateText(path))
            {
                var line = new StringBuilder();
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    if (line.Length > 0) line.Append(",");
                    line.Append(column.HeaderText);
                }
                file.WriteLine(line.ToString());

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    line.Clear();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (line.Length > 0) line.Append(",");
                        line.Append(cell.Value?.ToString() ?? "null");
                    }
                    file.WriteLine(line.ToString());
                }
            }
        }
    }
}
