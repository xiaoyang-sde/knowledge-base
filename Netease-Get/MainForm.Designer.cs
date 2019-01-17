namespace Netease_Get
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Add = new System.Windows.Forms.Button();
            this.Download = new System.Windows.Forms.Button();
            this.InputBox = new System.Windows.Forms.TextBox();
            this.DownloadList = new System.Windows.Forms.ListBox();
            this.RemoveSelected = new System.Windows.Forms.Button();
            this.RemoveAll = new System.Windows.Forms.Button();
            this.Label = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.GithubBox = new System.Windows.Forms.PictureBox();
            this.Close = new System.Windows.Forms.PictureBox();
            this.Minimize = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.GithubBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Minimize)).BeginInit();
            this.SuspendLayout();
            // 
            // Add
            // 
            this.Add.BackColor = System.Drawing.Color.White;
            this.Add.Font = new System.Drawing.Font("Microsoft JhengHei UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Add.ForeColor = System.Drawing.Color.Gray;
            this.Add.Location = new System.Drawing.Point(716, 60);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(196, 72);
            this.Add.TabIndex = 2;
            this.Add.Text = "Add Song";
            this.Add.UseVisualStyleBackColor = false;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // Download
            // 
            this.Download.BackColor = System.Drawing.Color.White;
            this.Download.Font = new System.Drawing.Font("Microsoft JhengHei UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Download.ForeColor = System.Drawing.Color.Gray;
            this.Download.Location = new System.Drawing.Point(716, 184);
            this.Download.Name = "Download";
            this.Download.Size = new System.Drawing.Size(196, 72);
            this.Download.TabIndex = 3;
            this.Download.Text = "Download All";
            this.Download.UseVisualStyleBackColor = false;
            this.Download.Click += new System.EventHandler(this.Download_Click);
            // 
            // InputBox
            // 
            this.InputBox.Font = new System.Drawing.Font("Microsoft JhengHei UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.InputBox.Location = new System.Drawing.Point(42, 60);
            this.InputBox.Name = "InputBox";
            this.InputBox.Size = new System.Drawing.Size(636, 38);
            this.InputBox.TabIndex = 0;
            // 
            // DownloadList
            // 
            this.DownloadList.Font = new System.Drawing.Font("Microsoft JhengHei UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownloadList.ForeColor = System.Drawing.Color.Gray;
            this.DownloadList.FormattingEnabled = true;
            this.DownloadList.ItemHeight = 30;
            this.DownloadList.Location = new System.Drawing.Point(42, 142);
            this.DownloadList.Name = "DownloadList";
            this.DownloadList.Size = new System.Drawing.Size(635, 364);
            this.DownloadList.TabIndex = 7;
            // 
            // RemoveSelected
            // 
            this.RemoveSelected.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.RemoveSelected.BackColor = System.Drawing.Color.White;
            this.RemoveSelected.Font = new System.Drawing.Font("Microsoft JhengHei UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RemoveSelected.ForeColor = System.Drawing.Color.Gray;
            this.RemoveSelected.Location = new System.Drawing.Point(716, 308);
            this.RemoveSelected.Name = "RemoveSelected";
            this.RemoveSelected.Size = new System.Drawing.Size(196, 72);
            this.RemoveSelected.TabIndex = 8;
            this.RemoveSelected.Text = "Remove Selected";
            this.RemoveSelected.UseVisualStyleBackColor = false;
            this.RemoveSelected.Click += new System.EventHandler(this.RemoveSelected_Click);
            // 
            // RemoveAll
            // 
            this.RemoveAll.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.RemoveAll.BackColor = System.Drawing.Color.White;
            this.RemoveAll.Font = new System.Drawing.Font("Microsoft JhengHei UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RemoveAll.ForeColor = System.Drawing.Color.Gray;
            this.RemoveAll.Location = new System.Drawing.Point(716, 432);
            this.RemoveAll.Name = "RemoveAll";
            this.RemoveAll.Size = new System.Drawing.Size(196, 72);
            this.RemoveAll.TabIndex = 9;
            this.RemoveAll.Text = "Remove All";
            this.RemoveAll.UseVisualStyleBackColor = false;
            this.RemoveAll.Click += new System.EventHandler(this.RemoveAll_Click);
            // 
            // Label
            // 
            this.Label.AutoSize = true;
            this.Label.Font = new System.Drawing.Font("Microsoft JhengHei UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label.ForeColor = System.Drawing.SystemColors.GrayText;
            this.Label.Location = new System.Drawing.Point(37, 20);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(203, 30);
            this.Label.TabIndex = 12;
            this.Label.Text = "Netsease-Get 1.01";
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Font = new System.Drawing.Font("Microsoft JhengHei UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.StatusLabel.Location = new System.Drawing.Point(37, 527);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(221, 30);
            this.StatusLabel.TabIndex = 13;
            this.StatusLabel.Text = "Apache License 2.0";
            // 
            // GithubBox
            // 
            this.GithubBox.Image = global::Netease_Get.Properties.Resources.github;
            this.GithubBox.ImageLocation = "";
            this.GithubBox.Location = new System.Drawing.Point(827, 12);
            this.GithubBox.Name = "GithubBox";
            this.GithubBox.Size = new System.Drawing.Size(32, 32);
            this.GithubBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.GithubBox.TabIndex = 14;
            this.GithubBox.TabStop = false;
            this.GithubBox.Click += new System.EventHandler(this.GithubBox_Click);
            // 
            // Close
            // 
            this.Close.Image = global::Netease_Get.Properties.Resources.CloseW;
            this.Close.Location = new System.Drawing.Point(918, 6);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(47, 48);
            this.Close.TabIndex = 11;
            this.Close.TabStop = false;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // Minimize
            // 
            this.Minimize.Image = global::Netease_Get.Properties.Resources.MinimizeW;
            this.Minimize.Location = new System.Drawing.Point(865, 6);
            this.Minimize.Name = "Minimize";
            this.Minimize.Size = new System.Drawing.Size(47, 48);
            this.Minimize.TabIndex = 10;
            this.Minimize.TabStop = false;
            this.Minimize.Click += new System.EventHandler(this.Minimize_Click);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(972, 566);
            this.Controls.Add(this.GithubBox);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.Label);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.Minimize);
            this.Controls.Add(this.RemoveAll);
            this.Controls.Add(this.RemoveSelected);
            this.Controls.Add(this.DownloadList);
            this.Controls.Add(this.InputBox);
            this.Controls.Add(this.Download);
            this.Controls.Add(this.Add);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.GithubBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Minimize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button Download;
        private System.Windows.Forms.TextBox InputBox;
        private System.Windows.Forms.ListBox DownloadList;
        private System.Windows.Forms.Button RemoveSelected;
        private System.Windows.Forms.Button RemoveAll;
        private System.Windows.Forms.PictureBox Minimize;
        private System.Windows.Forms.PictureBox Close;
        private System.Windows.Forms.Label Label;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.PictureBox GithubBox;
    }
}

