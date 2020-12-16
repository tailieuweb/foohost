using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualHostManager.Models;

namespace VirtualHostManager
{
    public partial class VirtualHostItem : UserControl
    {
        public VirtualHostItem()
        {
            InitializeComponent();
        }

        #region Property
        public bool Status
        {
            get { return statusCheckBox.Checked; }
            set { statusCheckBox.Checked = value; }
        }
        public string ServerName
        {
            get { return serverNameText.Text; }
            set { serverNameText.Text = value; }
        }
        public string Port
        {
            get { return portText.Text; }
            set { portText.Text = value; }
        }
        public string Directory
        {
            get { return directoryText.Text; }
            set { directoryText.Text = value; }
        }
        public string DirectoryConfig
        {
            get { return directoryConfigText.Text; }
            set { directoryConfigText.Text = value; }
        }
        public Action DeleteCallback;
        #endregion

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(DeleteCallback != null)
            {
                DeleteCallback.Invoke();
            }
        }

        private void statusCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (statusCheckBox.Checked)
            {
                statusCheckBox.ForeColor = Color.White;
                statusCheckBox.BackColor = Color.Green;
            }
            else
            {
                statusCheckBox.ForeColor = Color.Black;
                statusCheckBox.BackColor = Color.DarkGray;
            }
        }
    }
}
