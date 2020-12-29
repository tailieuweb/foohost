using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualHostManager.Extensions;
using VirtualHostManager.Models;
using VirtualHostManager.Service;

namespace VirtualHostManager.Forms
{
    public partial class VirtualHostList : BaseForm
    {
        private VirtualHostContext context;
        BindingList<VirtualHost> listVirtualHostForm = new BindingList<VirtualHost>();
        private DataStorageService dataStorageService;

        private int CurrentPage = 1;
        int PagesCount = 1;
        int pageRows = 28;
        public VirtualHostList() : base()
        {

            InitializeComponent();

            /*
             * Add event listener to change status list label 
             */
            listVirtualHostForm.ListChanged += (object sender, ListChangedEventArgs e) =>
            {
                var disableNumber = context.data.Where(x => x.Status == false).Count();
                var enableNumber = context.data.Where(x => x.Status == true).Count();
                statusListlbl.Text = string.Format("Enable items: {0}  Disable items: {1}", enableNumber, disableNumber);
            };

            dataStorageService = new DataStorageService();

            var filePath = dataStorageService.Read<string>(AppConst.filePathConfig).Replace("\"", "");
            filePathlbl.Text = filePath;
            if (!string.IsNullOrEmpty(filePath))
            {
                context = new VirtualHostContext(filePath);
                context.Read();
                setItems();
            }

            getAllDrive();
            //var a = new GetDirectoryForm().ShowDialog();
            //setup();   //creating new column
        }

        

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void setup()
        {

            try
            {
                var filePath = getFilePath();
                if (filePath == "" || !File.Exists(filePath))
                {
                    MessageBox.Show("Vui lòng sửa lại đường dẫn", "Đường dẫn lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    dataStorageService.Save(AppConst.filePathConfig, filePath);
                    filePathlbl.Text = filePath;
                    context = new VirtualHostContext(filePath);
                    context.Read();
                    setItems();
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }


        private void setItems()
        {
            //flowLayoutPanel1.Controls.Clear();
            var items = context.data;
            listVirtualHostForm.Clear();
            items.ForEach(x =>
            {
                //flowLayoutPanel1.Controls.Add(newItem);
                listVirtualHostForm.Add(x);
            });
            dataGridView1.DataSource = items;
            PagesCount = Convert.ToInt32(Math.Ceiling(items.Count * 1.0 / pageRows));
            CurrentPage = 1;
            RefreshPagination();
            RebindGridForPageChange();

            var columnConfig = dataStorageService.Read<VirtualHostDataGridViewColumns>(AppConst.VirtualHostColumns);
            if (columnConfig == null)
            {
                columnConfig = new VirtualHostDataGridViewColumns()
                {
                    Author = true,
                    CreateAt = true,
                    Description = true,
                    Directory = true,
                    ErrorLogs = true,
                    Status = true,
                    UpdateAt = true,
                    Url = true,
                };
            }
            var list = columnConfig.GetType()
                                              .GetProperties()
                                              .Select(x => new { Name = x.Name, Value = (bool)x.GetValue(columnConfig) })
                                              .ToList();
            list.ForEach(x =>
            {
                dataGridView1.Columns[x.Name + "DataGridViewTextBoxColumn"].Visible = x.Value;
            });
        }
        private void RebindGridForPageChange()
        {
            try
            {
                var diskPath = comboBox1.SelectedItem.ToString().Replace(@"\\", @"\");
                var data = context.data.Where(x => x.Url.Contains(textBox1.Text) && (x.Directory.StartsWith(diskPath) || diskPath == "All")).ToList();

                PagesCount = Convert.ToInt32(Math.Ceiling(data.Count * 1.0 / pageRows));
                //Rebinding the Datagridview with data
                int datasourcestartIndex = (CurrentPage - 1) * pageRows;
                listVirtualHostForm.Clear();
                for (int i = datasourcestartIndex; i < datasourcestartIndex + pageRows; i++)
                {
                    if (i >= data.Count)
                        break;

                    listVirtualHostForm.Add(data[i]);
                }


                dataGridView1.DataSource = listVirtualHostForm;
                dataGridView1.Refresh();
            }
            catch (Exception ex) { }
        }

        //Method that handles the pagination button clicks
        private void ToolStripButtonClick(object sender, EventArgs e)
        {
            try
            {
                ToolStripButton ToolStripButton = ((ToolStripButton)sender);

                //Determining the current page
                if (ToolStripButton == btnBackward)
                    CurrentPage--;
                else if (ToolStripButton == btnForward)
                    CurrentPage++;
                else
                    CurrentPage = Convert.ToInt32(ToolStripButton.Text, CultureInfo.InvariantCulture);

                if (CurrentPage < 1)
                    CurrentPage = 1;
                else if (CurrentPage > PagesCount)
                    CurrentPage = PagesCount;

                //Rebind the Datagridview with the data.
                RebindGridForPageChange();

                //Change the pagiantions buttons according to page number
                RefreshPagination();
            }
            catch (Exception) { }
        }
        private void RefreshPagination()
        {
            ToolStripButton[] items = new ToolStripButton[] { toolStripButton1, toolStripButton2, toolStripButton3, toolStripButton4, toolStripButton5 };

            //pageStartIndex contains the first button number of pagination.
            int pageStartIndex = 1;

            if (PagesCount > 5 && CurrentPage > 2)
                pageStartIndex = CurrentPage - 2;

            if (PagesCount > 5 && CurrentPage > PagesCount - 2)
                pageStartIndex = PagesCount - 4;

            for (int i = pageStartIndex; i < pageStartIndex + 5; i++)
            {
                if (i > PagesCount)
                {
                    items[i - pageStartIndex].Visible = false;
                }
                else
                {
                    //Changing the page numbers
                    items[i - pageStartIndex].Text = i.ToString(CultureInfo.InvariantCulture);
                    items[i - pageStartIndex].Visible = true;

                    //Setting the Appearance of the page number buttons
                    if (i == CurrentPage)
                    {
                        items[i - pageStartIndex].BackColor = Color.Black;
                        items[i - pageStartIndex].ForeColor = Color.White;
                    }
                    else
                    {
                        items[i - pageStartIndex].BackColor = Color.White;
                        items[i - pageStartIndex].ForeColor = Color.Black;
                    }
                }
            }
        }


        private string getFilePath()
        {
            using (var fbd = new OpenFileDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.FileName))
                {
                    return fbd.FileName;
                }
                return "";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CurrentPage = 1;
            //Rebind the Datagridview with the data.
            RebindGridForPageChange();

            //Change the pagiantions buttons according to page number
            RefreshPagination();
        }

        private void thoátToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void đổiĐườngDẫnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setup();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView gridView = sender as DataGridView;
            if (null != gridView)
            {
                foreach (DataGridViewRow r in gridView.Rows)
                {
                    gridView.Rows[r.Index].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    gridView.Rows[r.Index].HeaderCell.Value = ((CurrentPage - 1) * pageRows + (r.Index + 1)).ToString();
                }
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView gridView = sender as DataGridView;
            if (null != gridView)
            {
                gridView.Rows[e.RowIndex].Cells["EditAction"].Value = "Edit";
                gridView.Rows[e.RowIndex].Cells["DeleteAction"].Value = "Delete";
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["EditAction"].Index && e.RowIndex >= 0)
            {
                DataGridView gridView = sender as DataGridView;
                var index = Int32.Parse((string)gridView.Rows[e.RowIndex].HeaderCell.Value) - 1;
                var model = context.data.ElementAt(index);
                var dialog = new VirtualHostManager.Forms.VirtualHostDetail();
                dialog.formType = VirtualHostDetailType.Edit;
                dialog.Url = model.Url;
                dialog.Directory = model.Directory;
                dialog.CreateAt = model.CreateAt;
                dialog.Description = model.Description;
                dialog.Context = model.Context;
                dialog.Status = model.Status;
                dialog.Author = model.Author;

                dialog.saveCallback = () =>
                {
                    generateBackup();
                    model.Url = dialog.Url;
                    model.Directory = dialog.Directory;
                    model.CreateAt = dialog.CreateAt;
                    model.UpdateAt = DateTime.Now.ToString();
                    model.Description = dialog.Description;
                    model.Context = dialog.Context;
                    model.Status = dialog.Status;
                    model.Author = dialog.Author;
                    context.data[index] = model;
                        //Rebind the Datagridview with the data.
                        RebindGridForPageChange();
                };
                using (Panel p = this.blurPanel())
                {
                    dialog.ShowDialog();
                }
            }
            else if (e.ColumnIndex == dataGridView1.Columns["DeleteAction"].Index && e.RowIndex >= 0)
            {
                DataGridView gridView = sender as DataGridView;
                var index = Int32.Parse((string)gridView.Rows[e.RowIndex].HeaderCell.Value) - 1;
                generateBackup();
                context.data.RemoveAt(index);
                RebindGridForPageChange();
            }

        }

        private void textBox1_Click(object sender, EventArgs e)
        {

        }

        private void hostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Panel p = this.blurPanel())
            {
                var dialog = new VirtualHostManager.Forms.HostForm();
                dialog.ShowDialog();
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Panel p = this.blurPanel())
            {
                var dialog = new VirtualHostManager.Forms.SettingForm();
                dialog.ShowDialog();
                setItems();
            }
        }


        private void restartWampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServiceController sc = new ServiceController("wampapache64");
            try
            {
                if (sc.CanStop == true && sc.Status == ServiceControllerStatus.Running)
                {
                    Task.Run(() =>
                    {
                        sc.Stop();
                        sc.WaitForStatus(ServiceControllerStatus.Stopped);
                        sc.Start();
                    });
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Không thể khởi dộng lại wamp", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            context.SaveChanges();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Can use dialog.FileName
                    // Save data
                    context.setPath(dialog.FileName);
                    context.SaveChanges();
                }
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void generateBackup()
        {
            var fileName = Path.Combine(AppConst.BackupFolder,DateTime.Now.ToString("MMddyyyyhhmmss")+".txt");

            if (!Directory.Exists(Path.Combine(Application.UserAppDataPath, AppConst.BackupFolder)))
            {
                Directory.CreateDirectory(Path.Combine(Application.UserAppDataPath, AppConst.BackupFolder));
            }
            dataStorageService.Save(fileName, context.data);
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Panel p = this.blurPanel())
            {
                var dialog = new VirtualHostManager.Forms.BackupForm();
                dialog.ShowDialog();
                if (dialog.DialogResult == DialogResult.OK)
                {
                    context.data = dialog.result;
                    setItems();
                }
            }
        }

        private void getAllDrive()
        {
            var drives = DriveInfo.GetDrives()
                                  .Select(x => x.Name)
                                  .ToList();
            var t = DriveInfo.GetDrives();
            drives = drives.Prepend("All").ToList();
            comboBox1.DataSource = drives;
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //Rebind the Datagridview with the data.
            RebindGridForPageChange();

            //Change the pagiantions buttons according to page number
            RefreshPagination();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();

                int columnIndex = dataGridView1.HitTest(e.X, e.Y).ColumnIndex;

                if (columnIndex == dataGridView1.Columns["statusDataGridViewTextBoxColumn"].Index)
                {
                    m.MenuItems.Add(new MenuItem("Enable all items", menuItemStatusClick(true)));
                    m.MenuItems.Add(new MenuItem("Disable all items", menuItemStatusClick(false)));

                    m.Show(dataGridView1, new Point(e.X, e.Y));
                }

            }
        }

        private EventHandler menuItemStatusClick(bool value)
        {
            return (object sender, EventArgs e) => 
            {
                for(int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    var rowIndex = Int32.Parse((string)dataGridView1.Rows[i].HeaderCell.Value) - 1;
                    context.data[rowIndex].Status = value;
                }
                RebindGridForPageChange();
            };
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["statusDataGridViewTextBoxColumn"].Index && e.RowIndex >= 0)
            {
                generateBackup();
                DataGridView gridView = sender as DataGridView;
                var index = Int32.Parse((string)gridView.Rows[e.RowIndex].HeaderCell.Value) - 1;
                var columnIndex = dataGridView1.Columns["statusDataGridViewTextBoxColumn"].Index;
                context.data[index].Status = !(bool)gridView.Rows[e.RowIndex].Cells[columnIndex].Value;
                RebindGridForPageChange();
                
            }
        }

        private void addAVirtualHostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var model = new VirtualHost();
            //listVirtualHostForm.Add(newItem);var dialog = new VirtualHostManager.Forms.VirtualHostDetail();
            var dialog = new VirtualHostManager.Forms.VirtualHostDetail();
            dialog.formType = VirtualHostDetailType.Edit;
            dialog.Url = "";
            dialog.Directory = "";
            dialog.CreateAt = "";
            dialog.Description = "";
            dialog.Context = dataStorageService.VirualHostTemplateRead(AppConst.VirtualHostTemplate);
            dialog.Status = true;
            dialog.saveCallback = () =>
            {
                try
                {
                    generateBackup();
                    model.Url = dialog.Url;
                    model.Directory = dialog.Directory;
                    model.CreateAt = DateTime.Now.ToString();
                    model.UpdateAt = DateTime.Now.ToString();
                    model.Description = dialog.Description;
                    model.Context = dialog.Context;
                    model.Status = dialog.Status;
                    model.Author = dialog.Author;
                    context.data.Add(model);
                    //Rebind the Datagridview with the data.
                    RebindGridForPageChange();
                    setItems();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Vui lòng sửa lại đường dẫn", "Đường dẫn lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            using (Panel p = this.blurPanel())
            {
                dialog.ShowDialog();
            }
        }
    }
}
