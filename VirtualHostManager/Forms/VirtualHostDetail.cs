using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualHostManager.Models;

namespace VirtualHostManager.Forms
{
    public partial class VirtualHostDetail : BaseForm
    {
        public VirtualHostDetail()
        {
            InitializeComponent();
        }
        public VirtualHostDetailType formType { set; get; }
        public string Url
        {
            get { return urlText.Text; }
            set { urlText.Text = value; }
        }
        public string Directory
        {
            get { return directoryText.Text; }
            set { directoryText.Text = value; }
        }
        public string CreateAt
        {
            get { return dateCreated.Value.ToString(); }
            set { dateCreated.Value = string.IsNullOrEmpty(value) ? DateTime.Now : DateTime.Parse(value); }
        }
        public string Description
        {
            get { return noteText.Text; }
            set { noteText.Text = value; }
        }
        public string Author
        {
            get { return authortxt.Text; }
            set { authortxt.Text = value; }
        }
        public bool Status
        {
            get { return statuschkBox.Checked; }
            set { statuschkBox.Checked = value; }
        }
        public string Context
        {
            get { return ContextText.Text; }
            set { ContextText.Text = value; }
        }

        public Action saveCallback { set; get; }

        private void VirtualHostDetail_Load(object sender, EventArgs e)
        {
            if(formType == VirtualHostDetailType.View)
            {
                ContextText.Enabled = false;
                statuschkBox.Checked = false;
                directoryText.Enabled = false;
                noteText.Enabled = false;
                urlText.Enabled = false;
                dateCreated.Enabled = false;
            }
        }

        private void statusCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            saveCallback?.Invoke();
            this.Close();
        }

        private void urlText_TextChanged(object sender, EventArgs e)
        {
            if(urlText.Modified)
            {
                if(!Regex.IsMatch(Context, @"ServerName(.*?)\n"))
                {
                    var match = Regex.Match(Context, @"<\s*VirtualHost[^>]*>(.*?)\n");
                    var position = match.Index + match.Length;
                    Context = Context.Substring(0, position) + string.Format("ServerName {0}", System.Environment.NewLine) + Context.Substring(position);
                }
                var serverName = string.Format("ServerName {0}{1}", urlText.Text, System.Environment.NewLine);
                Context = Regex.Replace(Context, @"ServerName(.*?)\n", serverName);
            }
        }

        private void ContextText_TextChanged(object sender, EventArgs e)
        {
            if (ContextText.Modified)
            {
                var serverName = Regex.Match(Context, @"ServerName(.*?)\n").Value.Replace("ServerName", "").Replace("\r\n", "");
                if (!string.IsNullOrEmpty(serverName))
                {
                    Url = serverName;
                }

                var directory = Regex.Match(Context, @"<Directory(.*?)>\s*\n").Value.Replace("<Directory", "").Replace(">", "").Replace("\r\n", "").Trim().Trim('"');
                if (!string.IsNullOrEmpty(directory))
                {
                    Directory = directory;
                }
            }
        }

        private void directoryText_TextChanged(object sender, EventArgs e)
        {
            var directory = string.Format("<Directory \"{0}\">{1}", Directory, System.Environment.NewLine);

            Context = Regex.Replace(Context, @"<Directory(.*?)>\s*\n", directory);
        }

        private void selectPathbtn_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    Directory = fbd.SelectedPath;
                }
            }
        }
    }
}
