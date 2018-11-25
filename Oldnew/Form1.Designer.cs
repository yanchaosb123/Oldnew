namespace Oldnew
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.seachText = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvLogs = new System.Windows.Forms.DataGridView();
            this.genOldNew = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).BeginInit();
            this.SuspendLayout();
            // 
            // seachText
            // 
            this.seachText.Location = new System.Drawing.Point(75, 31);
            this.seachText.Name = "seachText";
            this.seachText.Size = new System.Drawing.Size(211, 21);
            this.seachText.TabIndex = 0;
            this.seachText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchForVersion);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvLogs);
            this.panel1.Location = new System.Drawing.Point(13, 72);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1232, 465);
            this.panel1.TabIndex = 1;
            // 
            // dgvLogs
            // 
            this.dgvLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLogs.Location = new System.Drawing.Point(0, 0);
            this.dgvLogs.Name = "dgvLogs";
            this.dgvLogs.RowTemplate.Height = 23;
            this.dgvLogs.Size = new System.Drawing.Size(1232, 465);
            this.dgvLogs.TabIndex = 0;
            // 
            // genOldNew
            // 
            this.genOldNew.Location = new System.Drawing.Point(342, 28);
            this.genOldNew.Name = "genOldNew";
            this.genOldNew.Size = new System.Drawing.Size(75, 23);
            this.genOldNew.TabIndex = 2;
            this.genOldNew.Text = "产生oldNew";
            this.genOldNew.UseVisualStyleBackColor = true;
            this.genOldNew.Click += new System.EventHandler(this.GenerateOldNew);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 564);
            this.Controls.Add(this.genOldNew);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.seachText);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox seachText;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvLogs;
        private System.Windows.Forms.Button genOldNew;
    }
}

