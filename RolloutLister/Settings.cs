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
    public partial class Settings : Form
    {
        public Settings(String server,int safety)
        {
            InitializeComponent();
            serverAddress.Text = server;
            safetyOptionBox.Text = safety.ToString();
        }

        public String get_Safety()
        {
            return safetyOptionBox.Text;
        }

        public String get_Server()
        {
            return serverAddress.Text;
        }

        public bool export_Rejection()
        {
            return rejectionCheck.Checked;
        }

        public bool export_NodeList()
        {
            return nodeCheck.Checked;
        }

        public bool export_Logging()
        {
            return loggingCheck.Checked;
        }
    }
}
