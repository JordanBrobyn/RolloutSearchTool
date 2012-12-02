using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RolloutLister
{
    public partial class Manual_Entry : Form
    {
        String text;
        public Manual_Entry()
        {
            InitializeComponent();
        }

        private void done_Click(object sender, EventArgs e)
        {
            text = ACF2_List.Text;
            this.Hide();
        }

        public String get()
        {
            return text;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
