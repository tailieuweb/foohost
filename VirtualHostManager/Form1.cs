
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VirtualHostManager.Models;

namespace VirtualHostManager
{
    public partial class Form1 : Form
    {

        private VirtualHostContext context;
        public Form1()
        {
            InitializeComponent();
            context = new VirtualHostContext();
            setItems();
        }

        private void setItems()
        {
            var items = context.data;
            List<VirtualHostItem> virtualHostItems = new List<VirtualHostItem>();
            items.ForEach(x =>
            {
                var newItem = new VirtualHostItem();
                newItem.ServerName = x.ServerName;
                newItem.Port = x.Port;
                newItem.Directory = x.Directory;
                newItem.Status = x.Status;
                newItem.DirectoryConfig = x.DirectoryConfig;
                newItem.DeleteCallback = () =>
                {
                    flowLayoutPanel1.Controls.Remove(newItem);
                };

                if (virtualHostItems.Count % 2 == 0)
                {
                    newItem.BackColor = Color.LightGray;
                }
                virtualHostItems.Add(newItem);
                flowLayoutPanel1.Controls.Add(newItem);
            });
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void elementHost1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void materialTabSelector1_Click(object sender, EventArgs e)
        {

        }

        private void materialSingleLineTextField1_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var newItem = new VirtualHostItem();
            if (flowLayoutPanel1.Controls.Count % 2 == 0)
            {
                newItem.BackColor = Color.LightGray;
            }
            flowLayoutPanel1.Controls.Add(newItem);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var listData = new List<VirtualHost>();
            foreach (VirtualHostItem c in flowLayoutPanel1.Controls)
            {
                listData.Add(new VirtualHost()
                {
                    Directory = c.Directory,
                    DirectoryConfig = c.DirectoryConfig,
                    Port = c.Port,
                    ServerName = c.ServerName,
                    Status = c.Status
                });
            }

            context.data = listData;
            context.SaveChanges();
        }
    }
}
