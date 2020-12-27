using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualHostManager.Models;
using VirtualHostManager.Service;

namespace VirtualHostManager.Forms
{
    public partial class BackupForm : BaseForm
    {
        private DataStorageService dataStorageService;
        
        public List<VirtualHost> result = null;
        public BackupForm()
        {
            InitializeComponent();
            dataStorageService = new DataStorageService();
            var filePath = Path.Combine(Application.UserAppDataPath, AppConst.BackupFolder);
            var files = Directory.GetFiles(filePath)
                .Select(x => new Backup() 
            {
                File = Path.GetFileName(x),
                Time = DateTime.ParseExact(Path.GetFileNameWithoutExtension(x), "MMddyyyyhhmmss", System.Globalization.CultureInfo.InvariantCulture)
                               .ToString(),
            }).ToList();
            var list = new BindingList<Backup>(files);
            dataGridView1.DataSource = list;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["ActionUse"].Index && e.RowIndex >= 0)
            {
                var fileName = ((BindingList<Backup>)dataGridView1.DataSource).ElementAt(e.RowIndex).File;
                var filePath = Path.Combine(AppConst.BackupFolder, fileName);
                result = dataStorageService.Read<List<VirtualHost>>(filePath);
                DialogResult = DialogResult.OK;
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView gridView = sender as DataGridView;
            if (null != gridView)
            {
                gridView.Rows[e.RowIndex].Cells["ActionUse"].Value = "Áp dụng";
            }
        }
    }
}
