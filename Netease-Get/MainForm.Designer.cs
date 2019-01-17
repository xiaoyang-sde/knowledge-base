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
            this.DownloadList = new System.Windows.Forms.CheckedListBox();
            this.AddSingle = new System.Windows.Forms.Button();
            this.AddPlayList = new System.Windows.Forms.Button();
            this.AddAlbum = new System.Windows.Forms.Button();
            this.DownloadSelected = new System.Windows.Forms.Button();
            this.InputBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // DownloadList
            // 
            this.DownloadList.Font = new System.Drawing.Font("DengXian Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DownloadList.FormattingEnabled = true;
            this.DownloadList.Location = new System.Drawing.Point(25, 92);
            this.DownloadList.Name = "DownloadList";
            this.DownloadList.Size = new System.Drawing.Size(607, 368);
            this.DownloadList.TabIndex = 0;
            // 
            // AddSingle
            // 
            this.AddSingle.BackColor = System.Drawing.Color.White;
            this.AddSingle.Font = new System.Drawing.Font("DengXian Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AddSingle.Location = new System.Drawing.Point(653, 36);
            this.AddSingle.Name = "AddSingle";
            this.AddSingle.Size = new System.Drawing.Size(197, 72);
            this.AddSingle.TabIndex = 1;
            this.AddSingle.Text = "Add Single";
            this.AddSingle.UseVisualStyleBackColor = false;
            this.AddSingle.Click += new System.EventHandler(this.AddSingle_Click);
            // 
            // AddPlayList
            // 
            this.AddPlayList.BackColor = System.Drawing.Color.White;
            this.AddPlayList.Font = new System.Drawing.Font("DengXian Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AddPlayList.Location = new System.Drawing.Point(653, 153);
            this.AddPlayList.Name = "AddPlayList";
            this.AddPlayList.Size = new System.Drawing.Size(197, 72);
            this.AddPlayList.TabIndex = 2;
            this.AddPlayList.Text = "Add PlayList";
            this.AddPlayList.UseVisualStyleBackColor = false;
            this.AddPlayList.Click += new System.EventHandler(this.AddPlayList_Click);
            // 
            // AddAlbum
            // 
            this.AddAlbum.BackColor = System.Drawing.Color.White;
            this.AddAlbum.Font = new System.Drawing.Font("DengXian Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AddAlbum.Location = new System.Drawing.Point(652, 270);
            this.AddAlbum.Name = "AddAlbum";
            this.AddAlbum.Size = new System.Drawing.Size(197, 72);
            this.AddAlbum.TabIndex = 3;
            this.AddAlbum.Text = "Add Album";
            this.AddAlbum.UseVisualStyleBackColor = false;
            this.AddAlbum.Click += new System.EventHandler(this.AddAlbum_Click);
            // 
            // DownloadSelected
            // 
            this.DownloadSelected.BackColor = System.Drawing.Color.White;
            this.DownloadSelected.Font = new System.Drawing.Font("DengXian Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DownloadSelected.Location = new System.Drawing.Point(652, 387);
            this.DownloadSelected.Name = "DownloadSelected";
            this.DownloadSelected.Size = new System.Drawing.Size(197, 72);
            this.DownloadSelected.TabIndex = 4;
            this.DownloadSelected.Text = "Download Selected";
            this.DownloadSelected.UseVisualStyleBackColor = false;
            // 
            // InputBox
            // 
            this.InputBox.Font = new System.Drawing.Font("Microsoft YaHei Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.InputBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.InputBox.Location = new System.Drawing.Point(26, 36);
            this.InputBox.Name = "InputBox";
            this.InputBox.Size = new System.Drawing.Size(606, 39);
            this.InputBox.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 489);
            this.Controls.Add(this.InputBox);
            this.Controls.Add(this.DownloadSelected);
            this.Controls.Add(this.AddAlbum);
            this.Controls.Add(this.AddPlayList);
            this.Controls.Add(this.AddSingle);
            this.Controls.Add(this.DownloadList);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox DownloadList;
        private System.Windows.Forms.Button AddSingle;
        private System.Windows.Forms.Button AddPlayList;
        private System.Windows.Forms.Button AddAlbum;
        private System.Windows.Forms.Button DownloadSelected;
        private System.Windows.Forms.TextBox InputBox;
    }
}

