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
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_action_text = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_excel_json = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_excel_xml = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_action_sql = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_action_mysql = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_action_mongo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_action_protobuf = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_help = new System.Windows.Forms.ToolStripMenuItem();
            this.tstb_help = new System.Windows.Forms.ToolStripTextBox();
            this.tsmi_clear_log = new System.Windows.Forms.ToolStripMenuItem();
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
            this.gb_files.Size = new System.Drawing.Size(257, 441);
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
            this.lb_files.Size = new System.Drawing.Size(251, 421);
            this.lb_files.TabIndex = 0;
            // 
            // ms_main
            // 
            this.ms_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_action_file,
            this.tsmi_action_text,
            this.tsmi_action_sql,
            this.tsmi_action_protobuf,
            this.tsmi_help,
            this.tsmi_clear_log});
            this.ms_main.Location = new System.Drawing.Point(0, 0);
            this.ms_main.Name = "ms_main";
            this.ms_main.Size = new System.Drawing.Size(878, 25);
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
            // tsmi_action_text
            // 
            this.tsmi_action_text.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_excel_json,
            this.tsmi_excel_xml});
            this.tsmi_action_text.Name = "tsmi_action_text";
            this.tsmi_action_text.Size = new System.Drawing.Size(68, 21);
            this.tsmi_action_text.Text = "文本数据";
            // 
            // tsmi_excel_json
            // 
            this.tsmi_excel_json.Name = "tsmi_excel_json";
            this.tsmi_excel_json.Size = new System.Drawing.Size(180, 22);
            this.tsmi_excel_json.Text = "Excel to Json";
            this.tsmi_excel_json.Click += new System.EventHandler(this.tsmi_excel_json_Click);
            // 
            // tsmi_excel_xml
            // 
            this.tsmi_excel_xml.Name = "tsmi_excel_xml";
            this.tsmi_excel_xml.Size = new System.Drawing.Size(180, 22);
            this.tsmi_excel_xml.Text = "Excel to Xml";
            // 
            // tsmi_action_sql
            // 
            this.tsmi_action_sql.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_action_mysql,
            this.tsmi_action_mongo});
            this.tsmi_action_sql.Name = "tsmi_action_sql";
            this.tsmi_action_sql.Size = new System.Drawing.Size(56, 21);
            this.tsmi_action_sql.Text = "数据库";
            // 
            // tsmi_action_mysql
            // 
            this.tsmi_action_mysql.Name = "tsmi_action_mysql";
            this.tsmi_action_mysql.Size = new System.Drawing.Size(119, 22);
            this.tsmi_action_mysql.Text = "Mysql";
            // 
            // tsmi_action_mongo
            // 
            this.tsmi_action_mongo.Name = "tsmi_action_mongo";
            this.tsmi_action_mongo.Size = new System.Drawing.Size(119, 22);
            this.tsmi_action_mongo.Text = "Mongo";
            // 
            // tsmi_action_protobuf
            // 
            this.tsmi_action_protobuf.Name = "tsmi_action_protobuf";
            this.tsmi_action_protobuf.Size = new System.Drawing.Size(72, 21);
            this.tsmi_action_protobuf.Text = "protobuf";
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
            // tsmi_clear_log
            // 
            this.tsmi_clear_log.Name = "tsmi_clear_log";
            this.tsmi_clear_log.Size = new System.Drawing.Size(68, 21);
            this.tsmi_clear_log.Text = "清理输出";
            this.tsmi_clear_log.Click += new System.EventHandler(this.tsmi_clear_log_Click);
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
            this.groupBox1.Size = new System.Drawing.Size(621, 441);
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
            this.tb_log.Size = new System.Drawing.Size(615, 421);
            this.tb_log.TabIndex = 0;
            this.tb_log.WordWrap = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 466);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gb_files);
            this.Controls.Add(this.ms_main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.ms_main;
            this.Name = "FormMain";
            this.Text = "数据 读取 OR 转化 & Wu xin dao";
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
        private System.Windows.Forms.ToolStripMenuItem tsmi_action_text;
        private System.Windows.Forms.ToolStripMenuItem tsmi_excel_json;
        private System.Windows.Forms.ToolStripMenuItem tsmi_excel_xml;
        private System.Windows.Forms.ToolStripMenuItem tsmi_action_sql;
        private System.Windows.Forms.ToolStripMenuItem tsmi_action_protobuf;
        private System.Windows.Forms.FolderBrowserDialog open_dir;
        private System.Windows.Forms.OpenFileDialog open_file;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_log;
        private System.Windows.Forms.ToolStripMenuItem tsmi_clear_log;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_clear_file;
        private System.Windows.Forms.ToolStripMenuItem tsmi_clear_all;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmi_action_mysql;
        private System.Windows.Forms.ToolStripMenuItem tsmi_action_mongo;
    }
}

