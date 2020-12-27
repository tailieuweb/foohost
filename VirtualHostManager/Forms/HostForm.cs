using MaterialSkin;
using MaterialSkin.Controls;
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

namespace VirtualHostManager.Forms
{
    public partial class HostForm : BaseForm
    {

        private HostContext context;
        public HostForm()
        {
            InitializeComponent();
            context = new HostContext();
            context.Read();
            var list = new BindingList<Hosts>();
            context.data.ForEach(x =>
            {
                list.Add(x);
            });
            //dataGridView1.DataSource = list;
            dataGridView1.DataSource = list;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0 && e.ColumnIndex == 5)
            {
                dataGridView1.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void HostForm_Load(object sender, EventArgs e)
        {

        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            var list = ((BindingList<Hosts>)dataGridView1.DataSource).ToList();
            context.data = list.Select(x =>
            {
                return x;
            }).ToList();
            context.SaveChanges();
            MessageBox.Show("Đã lưu thành công", "Đã lưu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void addNewBtn_Click(object sender, EventArgs e)
        {
            ((BindingList<Hosts>)dataGridView1.DataSource).Insert(0, new Hosts() { Status = true });
        }
    }
}
