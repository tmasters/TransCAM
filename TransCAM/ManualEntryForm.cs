using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TransCAM
{
    public partial class ManualEntryForm : Form
    {
        public ManualEntryForm(String windowHeader, String xHeader, String yHeader, Dictionary<double, double> values)
        {
            InitializeComponent();
            this.Text = "Manual Entry - " + windowHeader;
            this.dataGridView1.Columns["X"].HeaderText = xHeader;
            this.dataGridView1.Columns["Y"].HeaderText = yHeader;
            foreach (var point in values)
            {
                this.dataGridView1.Rows.Add(new object[] { point.Key, point.Value });
            }
        }
        
        /// <summary>
        /// Get the values entered by the user
        /// </summary>
        public Dictionary<double, double> Values
        {
            get
            {
                var values = new Dictionary<double, double>();
                foreach (DataGridViewRow row in this.dataGridView1.Rows)
                {
                    double x, y;
                    if (row.Cells["X"].Value != null && double.TryParse(row.Cells["X"].Value.ToString(), out x)
                        && row.Cells["Y"].Value != null && double.TryParse(row.Cells["Y"].Value.ToString(), out y))
                    {
                        if (!values.ContainsKey(x))
                            values.Add(x, y);
                    }
                }
                return values.OrderBy(a => a.Key).ToDictionary(k => k.Key, k => k.Value);
            }
        }
    }
}
