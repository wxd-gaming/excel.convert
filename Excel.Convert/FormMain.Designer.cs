namespace Excel.Convert
{
    partial class FormMain
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
            this.gb_files = new System.Windows.Forms.GroupBox();
            this.ms_main = new System.Windows.Forms.MenuStrip();
            this.tsmi_file = new System.Windows.Forms.ToolStripMenuItem();
            this.tstb_check_file = new System.Windows.Forms.ToolStripTextBox();
            this.tstb_check_dir = new System.Windows.Forms.ToolStripTextBox();
            this.tstb_exit = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_help = new System.Windows.Forms.ToolStripMenuItem();
            this.tstb_help = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lb_files = new System.Windows.Forms.ListBox();
            this.gb_files.SuspendLayout();
            this.ms_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_files
            // 
            this.gb_files.Controls.Add(this.lb_files);
            this.gb_files.Dock = System.Windows.Forms.DockStyle.Left;
            this.gb_files.Location = new System.Drawing.Point(0, 25);
            this.gb_files.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.gb_files.Name = "gb_files";
            this.gb_files.Size = new System.Drawing.Size(200, 425);
            this.gb_files.TabIndex = 0;
            this.gb_files.TabStop = false;
            this.gb_files.Text = "文件处理";
            // 
            // ms_main
            // 
            this.ms_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_file,
            this.tsmi_help});
            this.ms_main.Location = new System.Drawing.Point(0, 0);
            this.ms_main.Name = "ms_main";
            this.ms_main.Size = new System.Drawing.Size(800, 25);
            this.ms_main.TabIndex = 1;
            this.ms_main.Text = "ms_main";
            // 
            // tsmi_file
            // 
            this.tsmi_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tstb_check_file,
            this.tstb_check_dir,
            this.toolStripSeparator1,
            this.tstb_exit,
            this.toolStripMenuItem1});
            this.tsmi_file.Name = "tsmi_file";
            this.tsmi_file.Size = new System.Drawing.Size(44, 21);
            this.tsmi_file.Text = "文件";
            // 
            // tstb_check_file
            // 
            this.tstb_check_file.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.tstb_check_file.Name = "tstb_check_file";
            this.tstb_check_file.Size = new System.Drawing.Size(100, 23);
            this.tstb_check_file.Text = "选择文件";
            // 
            // tstb_check_dir
            // 
            this.tstb_check_dir.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.tstb_check_dir.Name = "tstb_check_dir";
            this.tstb_check_dir.Size = new System.Drawing.Size(100, 23);
            this.tstb_check_dir.Text = "选择文件夹";
            // 
            // tstb_exit
            // 
            this.tstb_exit.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.tstb_exit.Name = "tstb_exit";
            this.tstb_exit.Size = new System.Drawing.Size(100, 23);
            this.tstb_exit.Text = "退出";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // tsmi_help
            // 
            this.tsmi_help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tstb_help});
            this.tsmi_help.Name = "tsmi_help";
            this.tsmi_help.Size = new System.Drawing.Size(44, 21);
            this.tsmi_help.Text = "帮助";
            // 
            // tstb_help
            // 
            this.tstb_help.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.tstb_help.Name = "tstb_help";
            this.tstb_help.Size = new System.Drawing.Size(100, 23);
            this.tstb_help.Text = "帮助";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem1.Text = "wegg";
            // 
            // lb_files
            // 
            this.lb_files.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_files.FormattingEnabled = true;
            this.lb_files.ItemHeight = 12;
            this.lb_files.Location = new System.Drawing.Point(3, 17);
            this.lb_files.Name = "lb_files";
            this.lb_files.Size = new System.Drawing.Size(194, 405);
            this.lb_files.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gb_files);
            this.Controls.Add(this.ms_main);
            this.MainMenuStrip = this.ms_main;
            this.Name = "FormMain";
            this.Text = "Form1";
            this.gb_files.ResumeLayout(false);
            this.ms_main.ResumeLayout(false);
            this.ms_main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_files;
        private System.Windows.Forms.MenuStrip ms_main;
        private System.Windows.Forms.ToolStripMenuItem tsmi_file;
        private System.Windows.Forms.ToolStripTextBox tstb_check_file;
        private System.Windows.Forms.ToolStripTextBox tstb_check_dir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox tstb_exit;
        private System.Windows.Forms.ToolStripMenuItem tsmi_help;
        private System.Windows.Forms.ToolStripTextBox tstb_help;
        private System.Windows.Forms.ListBox lb_files;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}

