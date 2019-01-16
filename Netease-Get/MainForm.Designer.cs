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
            this.downloadList = new System.Windows.Forms.CheckedListBox();
            this.addSingle = new System.Windows.Forms.Button();
            this.addPlayList = new System.Windows.Forms.Button();
            this.addSearch = new System.Windows.Forms.Button();
            this.downloadSelected = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // downloadList
            // 
            this.downloadList.Font = new System.Drawing.Font("DengXian Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.downloadList.FormattingEnabled = true;
            this.downloadList.Location = new System.Drawing.Point(25, 36);
            this.downloadList.Name = "downloadList";
            this.downloadList.Size = new System.Drawing.Size(607, 372);
            this.downloadList.TabIndex = 0;
            // 
            // addSingle
            // 
            this.addSingle.BackColor = System.Drawing.Color.White;
            this.addSingle.Font = new System.Drawing.Font("DengXian Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addSingle.Location = new System.Drawing.Point(652, 36);
            this.addSingle.Name = "addSingle";
            this.addSingle.Size = new System.Drawing.Size(197, 58);
            this.addSingle.TabIndex = 1;
            this.addSingle.Text = "Add Single";
            this.addSingle.UseVisualStyleBackColor = false;
            // 
            // addPlayList
            // 
            this.addPlayList.BackColor = System.Drawing.Color.White;
            this.addPlayList.Font = new System.Drawing.Font("DengXian Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addPlayList.Location = new System.Drawing.Point(653, 140);
            this.addPlayList.Name = "addPlayList";
            this.addPlayList.Size = new System.Drawing.Size(197, 58);
            this.addPlayList.TabIndex = 2;
            this.addPlayList.Text = "Add PlayList";
            this.addPlayList.UseVisualStyleBackColor = false;
            // 
            // addSearch
            // 
            this.addSearch.BackColor = System.Drawing.Color.White;
            this.addSearch.Font = new System.Drawing.Font("DengXian Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addSearch.Location = new System.Drawing.Point(652, 244);
            this.addSearch.Name = "addSearch";
            this.addSearch.Size = new System.Drawing.Size(197, 58);
            this.addSearch.TabIndex = 3;
            this.addSearch.Text = "Search";
            this.addSearch.UseVisualStyleBackColor = false;
            // 
            // downloadSelected
            // 
            this.downloadSelected.BackColor = System.Drawing.Color.White;
            this.downloadSelected.Font = new System.Drawing.Font("DengXian Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.downloadSelected.Location = new System.Drawing.Point(653, 348);
            this.downloadSelected.Name = "downloadSelected";
            this.downloadSelected.Size = new System.Drawing.Size(197, 58);
            this.downloadSelected.TabIndex = 4;
            this.downloadSelected.Text = "Download Selected";
            this.downloadSelected.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 450);
            this.Controls.Add(this.downloadSelected);
            this.Controls.Add(this.addSearch);
            this.Controls.Add(this.addPlayList);
            this.Controls.Add(this.addSingle);
            this.Controls.Add(this.downloadList);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox downloadList;
        private System.Windows.Forms.Button addSingle;
        private System.Windows.Forms.Button addPlayList;
        private System.Windows.Forms.Button addSearch;
        private System.Windows.Forms.Button downloadSelected;
    }
}

