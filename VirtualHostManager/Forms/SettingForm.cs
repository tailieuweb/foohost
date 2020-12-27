using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using VirtualHostManager.Extensions;
using VirtualHostManager.Models;
using VirtualHostManager.Service;

namespace VirtualHostManager.Forms
{
    public partial class SettingForm : BaseForm
    {
        private DataStorageService dataStorageService;
        private VirtualHostDataGridViewColumns hostDataGridViewColumns;
        public SettingForm()
        {
            InitializeComponent();

            dataStorageService = new DataStorageService();

            #region Init config column
            hostDataGridViewColumns = dataStorageService.Read<VirtualHostDataGridViewColumns>(AppConst.VirtualHostColumns);
            if (hostDataGridViewColumns == null)
            {
                hostDataGridViewColumns = new VirtualHostDataGridViewColumns()
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

            var list = hostDataGridViewColumns.GetType()
                                              .GetProperties()
                                              .Select(x => new { Name = x.Name, Value = (bool)x.GetValue(hostDataGridViewColumns)})
                                              .ToList();
            list.ForEach(x =>
            {
                checkedListBox1.Items.Add(x.Name);
                checkedListBox1.SetItemChecked(checkedListBox1.Items.Count - 1, x.Value);
            });
            #endregion

            #region Init virtual host template
            virtualHostTemplateTxt.Text = dataStorageService.VirualHostTemplateRead(AppConst.VirtualHostTemplate);
            #endregion
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var value = e.NewValue.ToString() == "Checked" ? true : false ;

            hostDataGridViewColumns.GetType()
                                   .GetProperty(checkedListBox1.Items[e.Index].ToString())
                                   .SetValue(hostDataGridViewColumns, value);
            dataStorageService.Save(AppConst.VirtualHostColumns, hostDataGridViewColumns);
        }

        private void SettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            dataStorageService.Save(AppConst.VirtualHostTemplate, virtualHostTemplateTxt.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
