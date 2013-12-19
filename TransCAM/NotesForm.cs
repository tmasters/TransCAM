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
    public partial class NotesForm : Form
    {
        public NotesForm(String header, String notes)
        {
            InitializeComponent();
            this.Text = "Notes - " + header;
            this.textBox1.Text = notes;
        }

        public String Notes
        {
            get { return this.textBox1.Text; }
        }
    }
}
