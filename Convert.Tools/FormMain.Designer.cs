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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.gb_files = new System.Windows.Forms.GroupBox();
            this.lb_files = new System.Windows.Forms.ListBox();
            this.ms_main = new System.Windows.Forms.MenuStrip();
            this.tsmi_action_file = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_check_file = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_check_dir = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_clear_file = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_clear_all = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_clear_show_log = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_plugs_excel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_excel_plus_all = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_plugs_xml = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_xml_plug_all = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_plugs_protobuf = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_protobuf_all = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_help = new System.Windows.Forms.ToolStripMenuItem();
            this.tstb_help = new System.Windows.Forms.ToolStripTextBox();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.open_dir = new System.Windows.Forms.FolderBrowserDialog();
            this.open_file = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_log = new System.Windows.Forms.TextBox();
            this.gb_files.SuspendLayout();
            this.ms_main.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_files
            // 
            this.gb_files.Controls.Add(this.lb_files);
            this.gb_files.Dock = System.Windows.Forms.DockStyle.Left;
            this.gb_files.Location = new System.Drawing.Point(0, 25);
            this.gb_files.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.gb_files.Name = "gb_files";
            this.gb_files.Size = new System.Drawing.Size(257, 502);
            this.gb_files.TabIndex = 0;
            this.gb_files.TabStop = false;
            this.gb_files.Text = "文件处理";
            // 
            // lb_files
            // 
            this.lb_files.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_files.FormattingEnabled = true;
            this.lb_files.ItemHeight = 12;
            this.lb_files.Location = new System.Drawing.Point(3, 17);
            this.lb_files.Name = "lb_files";
            this.lb_files.Size = new System.Drawing.Size(251, 482);
            this.lb_files.TabIndex = 0;
            // 
            // ms_main
            // 
            this.ms_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_action_file,
            this.tsmi_plugs_excel,
            this.tsmi_plugs_xml,
            this.tsmi_plugs_protobuf,
            this.tsmi_help});
            this.ms_main.Location = new System.Drawing.Point(0, 0);
            this.ms_main.Name = "ms_main";
            this.ms_main.ShowItemToolTips = true;
            this.ms_main.Size = new System.Drawing.Size(851, 25);
            this.ms_main.TabIndex = 1;
            this.ms_main.Text = "ms_main";
            // 
            // tsmi_action_file
            // 
            this.tsmi_action_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_check_file,
            this.tsmi_check_dir,
            this.toolStripSeparator1,
            this.tsmi_clear_file,
            this.tsmi_clear_all,
            this.toolStripSeparator4,
            this.tsmi_clear_show_log,
            this.toolStripSeparator2,
            this.tsmi_exit});
            this.tsmi_action_file.Name = "tsmi_action_file";
            this.tsmi_action_file.Size = new System.Drawing.Size(44, 21);
            this.tsmi_action_file.Text = "选择";
            // 
            // tsmi_check_file
            // 
            this.tsmi_check_file.Name = "tsmi_check_file";
            this.tsmi_check_file.Size = new System.Drawing.Size(148, 22);
            this.tsmi_check_file.Text = "文件";
            this.tsmi_check_file.Click += new System.EventHandler(this.tsmi_check_file_Click);
            // 
            // tsmi_check_dir
            // 
            this.tsmi_check_dir.Name = "tsmi_check_dir";
            this.tsmi_check_dir.Size = new System.Drawing.Size(148, 22);
            this.tsmi_check_dir.Text = "文件夹";
            this.tsmi_check_dir.Click += new System.EventHandler(this.tsmi_check_dir_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // tsmi_clear_file
            // 
            this.tsmi_clear_file.Name = "tsmi_clear_file";
            this.tsmi_clear_file.Size = new System.Drawing.Size(148, 22);
            this.tsmi_clear_file.Text = "删除选中文件";
            this.tsmi_clear_file.Click += new System.EventHandler(this.tsmi_clear_file_Click);
            // 
            // tsmi_clear_all
            // 
            this.tsmi_clear_all.Name = "tsmi_clear_all";
            this.tsmi_clear_all.Size = new System.Drawing.Size(148, 22);
            this.tsmi_clear_all.Text = "清空选择文件";
            this.tsmi_clear_all.Click += new System.EventHandler(this.tsmi_clear_all_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(145, 6);
            // 
            // tsmi_clear_show_log
            // 
            this.tsmi_clear_show_log.Name = "tsmi_clear_show_log";
            this.tsmi_clear_show_log.Size = new System.Drawing.Size(148, 22);
            this.tsmi_clear_show_log.Text = "清理输出";
            this.tsmi_clear_show_log.Click += new System.EventHandler(this.tsmi_clear_show_log_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // tsmi_exit
            // 
            this.tsmi_exit.Name = "tsmi_exit";
            this.tsmi_exit.Size = new System.Drawing.Size(148, 22);
            this.tsmi_exit.Text = "退出";
            // 
            // tsmi_plugs_excel
            // 
            this.tsmi_plugs_excel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_excel_plus_all,
            this.toolStripSeparator3});
            this.tsmi_plugs_excel.Name = "tsmi_plugs_excel";
            this.tsmi_plugs_excel.Size = new System.Drawing.Size(78, 21);
            this.tsmi_plugs_excel.Text = "Excel-插件";
            this.tsmi_plugs_excel.ToolTipText = "excel(.xls, .xlsx)文件处理";
            // 
            // tsmi_excel_plus_all
            // 
            this.tsmi_excel_plus_all.Name = "tsmi_excel_plus_all";
            this.tsmi_excel_plus_all.Size = new System.Drawing.Size(124, 22);
            this.tsmi_excel_plus_all.Tag = ".xls,.xlsx";
            this.tsmi_excel_plus_all.Text = "执行全部";
            this.tsmi_excel_plus_all.Click += new System.EventHandler(this.tsmi_excel_plus_all_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(121, 6);
            // 
            // tsmi_plugs_xml
            // 
            this.tsmi_plugs_xml.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_xml_plug_all});
            this.tsmi_plugs_xml.Name = "tsmi_plugs_xml";
            this.tsmi_plugs_xml.Size = new System.Drawing.Size(71, 21);
            this.tsmi_plugs_xml.Text = "Xml-插件";
            this.tsmi_plugs_xml.ToolTipText = "对文件(.xml)处理插件";
            // 
            // tsmi_xml_plug_all
            // 
            this.tsmi_xml_plug_all.Name = "tsmi_xml_plug_all";
            this.tsmi_xml_plug_all.Size = new System.Drawing.Size(124, 22);
            this.tsmi_xml_plug_all.Tag = ".xml";
            this.tsmi_xml_plug_all.Text = "执行全部";
            this.tsmi_xml_plug_all.Click += new System.EventHandler(this.tsmi_xml_plug_all_Click);
            // 
            // tsmi_plugs_protobuf
            // 
            this.tsmi_plugs_protobuf.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_protobuf_all,
            this.toolStripSeparator5});
            this.tsmi_plugs_protobuf.Name = "tsmi_plugs_protobuf";
            this.tsmi_plugs_protobuf.Size = new System.Drawing.Size(100, 21);
            this.tsmi_plugs_protobuf.Text = "Protobuf-插件";
            this.tsmi_plugs_protobuf.ToolTipText = "对protobuff文件处理";
            // 
            // tsmi_protobuf_all
            // 
            this.tsmi_protobuf_all.Name = "tsmi_protobuf_all";
            this.tsmi_protobuf_all.Size = new System.Drawing.Size(124, 22);
            this.tsmi_protobuf_all.Text = "执行全部";
            this.tsmi_protobuf_all.Click += new System.EventHandler(this.tsmi_protobuf_all_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(121, 6);
            // 
            // tsmi_help
            // 
            this.tsmi_help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tstb_help,
            this.关于ToolStripMenuItem});
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
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.关于ToolStripMenuItem.Text = "关于";
            // 
            // open_file
            // 
            this.open_file.FileName = "open_file";
            this.open_file.Filter = "工具可读文件|*.xls;*.xlsx;*.proto;*.xml";
            this.open_file.Multiselect = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_log);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(257, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(594, 502);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "输出";
            // 
            // tb_log
            // 
            this.tb_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_log.Location = new System.Drawing.Point(3, 17);
            this.tb_log.Multiline = true;
            this.tb_log.Name = "tb_log";
            this.tb_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_log.Size = new System.Drawing.Size(588, 482);
            this.tb_log.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 527);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gb_files);
            this.Controls.Add(this.ms_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.ms_main;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "数据 读取 OR 转化 & 無心道";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.gb_files.ResumeLayout(false);
            this.ms_main.ResumeLayout(false);
            this.ms_main.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_files;
        private System.Windows.Forms.MenuStrip ms_main;
        private System.Windows.Forms.ToolStripMenuItem tsmi_action_file;
        private System.Windows.Forms.ToolStripMenuItem tsmi_help;
        private System.Windows.Forms.ToolStripTextBox tstb_help;
        private System.Windows.Forms.ListBox lb_files;
        private System.Windows.Forms.ToolStripMenuItem tsmi_check_file;
        private System.Windows.Forms.ToolStripMenuItem tsmi_check_dir;
        private System.Windows.Forms.ToolStripMenuItem tsmi_exit;
        private System.Windows.Forms.ToolStripMenuItem tsmi_plugs_protobuf;
        private System.Windows.Forms.FolderBrowserDialog open_dir;
        private System.Windows.Forms.OpenFileDialog open_file;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_log;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_clear_file;
        private System.Windows.Forms.ToolStripMenuItem tsmi_clear_all;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmi_plugs_excel;
        private System.Windows.Forms.ToolStripMenuItem tsmi_excel_plus_all;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmi_plugs_xml;
        private System.Windows.Forms.ToolStripMenuItem tsmi_xml_plug_all;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmi_clear_show_log;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_protobuf_all;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    }
}

