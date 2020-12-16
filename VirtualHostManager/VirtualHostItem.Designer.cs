
namespace VirtualHostManager
{
    partial class VirtualHostItem
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.serverNameText = new System.Windows.Forms.TextBox();
            this.portText = new System.Windows.Forms.TextBox();
            this.directoryText = new System.Windows.Forms.TextBox();
            this.directoryConfigText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.statusCheckBox = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // serverNameText
            // 
            this.serverNameText.Location = new System.Drawing.Point(83, 42);
            this.serverNameText.Name = "serverNameText";
            this.serverNameText.Size = new System.Drawing.Size(218, 20);
            this.serverNameText.TabIndex = 0;
            // 
            // portText
            // 
            this.portText.Location = new System.Drawing.Point(339, 42);
            this.portText.Name = "portText";
            this.portText.Size = new System.Drawing.Size(59, 20);
            this.portText.TabIndex = 1;
            // 
            // directoryText
            // 
            this.directoryText.Location = new System.Drawing.Point(83, 68);
            this.directoryText.Name = "directoryText";
            this.directoryText.Size = new System.Drawing.Size(315, 20);
            this.directoryText.TabIndex = 2;
            // 
            // directoryConfigText
            // 
            this.directoryConfigText.Location = new System.Drawing.Point(20, 109);
            this.directoryConfigText.Multiline = true;
            this.directoryConfigText.Name = "directoryConfigText";
            this.directoryConfigText.Size = new System.Drawing.Size(378, 63);
            this.directoryConfigText.TabIndex = 3;
            this.directoryConfigText.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tên Server";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(307, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Đường dẫn";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Cấu hình thư mục";
            // 
            // statusCheckBox
            // 
            this.statusCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.statusCheckBox.AutoSize = true;
            this.statusCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.statusCheckBox.FlatAppearance.BorderSize = 0;
            this.statusCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.Lime;
            this.statusCheckBox.Location = new System.Drawing.Point(20, 13);
            this.statusCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.statusCheckBox.Name = "statusCheckBox";
            this.statusCheckBox.Size = new System.Drawing.Size(47, 23);
            this.statusCheckBox.TabIndex = 8;
            this.statusCheckBox.Text = "Active";
            this.statusCheckBox.UseVisualStyleBackColor = true;
            this.statusCheckBox.CheckedChanged += new System.EventHandler(this.statusCheckBox_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox2.AutoSize = true;
            this.checkBox2.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox2.FlatAppearance.CheckedBackColor = System.Drawing.Color.Lime;
            this.checkBox2.Location = new System.Drawing.Point(362, 13);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(36, 23);
            this.checkBox2.TabIndex = 9;
            this.checkBox2.Text = "Xoá";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // VirtualHostItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.statusCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.directoryConfigText);
            this.Controls.Add(this.directoryText);
            this.Controls.Add(this.portText);
            this.Controls.Add(this.serverNameText);
            this.Name = "VirtualHostItem";
            this.Size = new System.Drawing.Size(412, 185);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox serverNameText;
        private System.Windows.Forms.TextBox portText;
        private System.Windows.Forms.TextBox directoryText;
        private System.Windows.Forms.TextBox directoryConfigText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox statusCheckBox;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}
