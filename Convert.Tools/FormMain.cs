using Convert.Tools.Code;
using Convert.Tools.Excel;
using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Convert.Tools
{
    public partial class FormMain : Form
    {

        /// <summary>
        /// 显示日志
        /// </summary>         
        public static Action<string> ShowLog { get; set; }

        ScriptPool scriptPool = null;

        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 递归查找文件
        /// </summary>
        /// <param name="path"></param>
        void FindFiles(string path)
        {
            AddFileViwe(Directory.GetFiles(path));
            var paths = Directory.GetDirectories(path);
            foreach (var item in paths)
            {
                FindFiles(item);
            }
        }

        void AddFileViwe(string[] fileNames)
        {
            var temps = (this.lb_files.Tag as List<ItemFileInfo>);
            foreach (var itemFile in fileNames)
            {
                string extname = System.IO.Path.GetExtension(itemFile);
                string name = System.IO.Path.GetFileName(itemFile);
                string extnameLower = extname.ToLower();
                if (".xls".Equals(extnameLower, StringComparison.OrdinalIgnoreCase)
                    || ".xlsx".Equals(extnameLower, StringComparison.OrdinalIgnoreCase)
                    || ".proto".Equals(extnameLower, StringComparison.OrdinalIgnoreCase)
                    || ".xml".Equals(extnameLower, StringComparison.OrdinalIgnoreCase))
                {
                    var tifi = temps.FirstOrDefault(l => l.Name == name);
                    if (tifi == null)
                    {
                        var ifi = new ItemFileInfo(itemFile);
                        (this.lb_files.Tag as List<ItemFileInfo>).Add(ifi);
                        this.lb_files.Items.Add(ifi);
                        ShowLog("选择的文件：" + itemFile);
                    }
                    else
                    {
                        ShowLog("重复的文件：" + itemFile);
                    }
                }
            }
            if (this.lb_files.Items.Count > 0)
            {
                this.lb_files.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 获取选择的文件
        /// </summary>
        /// <param name="extendName">文件扩展名</param>
        private void CheckFiles(Action<string> call, params string[] extendName)
        {
            var temps = (this.lb_files.Tag as List<ItemFileInfo>);
            foreach (ItemFileInfo item in temps)
            {
                foreach (string ext in extendName)
                {
                    if (ext.Equals(item.ExtensionName, StringComparison.OrdinalIgnoreCase))
                    {
                        call.Invoke(item.Path);
                    }
                }
            }
        }

        private List<string> GetFiles(params string[] extendName)
        {
            List<string> files = new List<string>();
            CheckFiles((file) => files.Add(file), extendName);
            if (files.Count() == 0)
            {
                throw new RuntimeException("查找文件失败找到“" + string.Join(", ", extendName) + "”文件");
            }
            ShowLog("找到“" + string.Join(", ", extendName) + "”文件 数量：" + files.Count());
            return files;
        }

        private void tsmi_check_file_Click(object sender, EventArgs e)
        {
            if (open_file.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                AddFileViwe(open_file.FileNames);
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            ShowLog = (str) =>
            {
                this.tb_log.BeginInvoke(new Action(() =>
                {
                    this.tb_log.Text = str + "\r\n" + this.tb_log.Text;
                }));
                str.WriterLog();
            };

            this.lb_files.Tag = new List<ItemFileInfo>();
            ShowLog.Invoke("欢迎使用 無心道 软件");
            scriptPool = new ScriptPool();
            scriptPool.LoadCSharpFile("Plugs");
            ToolStripMenuItem tsmi_plugs_excel = (ToolStripMenuItem)this.ms_main.Items["tsmi_plugs_excel"];
            ToolStripMenuItem tsmi_plugs_xml = (ToolStripMenuItem)this.ms_main.Items["tsmi_plugs_xml"];
            ToolStripMenuItem tsmi_plugs_protobuf = (ToolStripMenuItem)this.ms_main.Items["tsmi_plugs_protobuf"];
            foreach (var item in scriptPool.Enumerable())
            {
                string plugName = item.PlugsName();

                ToolStripItem ts = new ToolStripMenuItem();
                ts.Name = plugName;
                ts.Text = plugName;
                ts.Tag = item;
                switch (item.plugEnum())
                {
                    case PlugEnum.Xml:
                        ts.Click += new EventHandler(Xml_Plug_Click);
                        tsmi_plugs_xml.DropDownItems.Add(ts);
                        break;
                    case PlugEnum.Excel:
                        ts.Click += new EventHandler(Excel_Plug_Click);
                        tsmi_plugs_excel.DropDownItems.Add(ts);
                        break;
                    case PlugEnum.Protobuf:
                        ts.Click += new EventHandler(Protobuf_Plug_Click);
                        tsmi_plugs_protobuf.DropDownItems.Add(ts);
                        break;
                }
            }
        }

        private void tsmi_excel_plus_all_Click(object sender, EventArgs e)
        {
            new Action(() =>
            {
                List<string> list = GetFiles(".xls", ".xlsx");
                foreach (var plug in scriptPool.Enumerable())
                {
                    if (plug.plugEnum() == PlugEnum.Excel)
                    {
                        plug.DoAction(list);
                    }
                }
            }).BeginInvoke(null, null);
        }

        private void tsmi_xml_plug_all_Click(object sender, EventArgs e)
        {
            new Action(() =>
            {
                List<string> list = GetFiles(".xml");
                foreach (var plug in scriptPool.Enumerable())
                {
                    if (plug.plugEnum() == PlugEnum.Xml)
                    {
                        plug.DoAction(list);
                    }
                }
            }).BeginInvoke(null, null);
        }

        private void tsmi_protobuf_all_Click(object sender, EventArgs e)
        {
            new Action(() =>
            {
                List<string> list = GetFiles(".proto");
                foreach (var plug in scriptPool.Enumerable())
                {
                    if (plug.plugEnum() == PlugEnum.Protobuf)
                    {
                        plug.DoAction(list);
                    }
                }
            }).BeginInvoke(null, null);
        }

        private void Excel_Plug_Click(object sender, EventArgs e)
        {
            new Action(() =>
            {
                List<string> list = GetFiles(".xls", ".xlsx");
                ToolStripItem ts = (ToolStripItem)sender;
                IOutPutPlugs outPutPlugs = ts.Tag as IOutPutPlugs;
                outPutPlugs.DoAction(list);
            }).BeginInvoke(null, null);
        }

        private void Xml_Plug_Click(object sender, EventArgs e)
        {
            new Action(() =>
            {
                List<string> list = GetFiles(".xml");
                ToolStripItem ts = (ToolStripItem)sender;
                IOutPutPlugs outPutPlugs = ts.Tag as IOutPutPlugs;
                outPutPlugs.DoAction(list);
            }).BeginInvoke(null, null);
        }

        private void Protobuf_Plug_Click(object sender, EventArgs e)
        {
            new Action(() =>
            {
                List<string> list = GetFiles(".proto");
                ToolStripItem ts = (ToolStripItem)sender;
                IOutPutPlugs outPutPlugs = ts.Tag as IOutPutPlugs;
                outPutPlugs.DoAction(list);
            }).BeginInvoke(null, null);
        }

        /// <summary>
        /// 选择文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_check_dir_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                if (open_dir.ShowDialog() == DialogResult.OK)
                    FindFiles(open_dir.SelectedPath);
            }));
        }

        private void tsmi_clear_file_Click(object sender, EventArgs e)
        {
            //删除一个文件
            ListBox.SelectedObjectCollection selectedItems = this.lb_files.SelectedItems;
            if (selectedItems != null && selectedItems.Count > 0)
            {
                var temps = (this.lb_files.Tag as List<ItemFileInfo>);
                int count = selectedItems.Count;
                for (int i = 0; i < count; i++)
                {
                    ItemFileInfo v = (ItemFileInfo)selectedItems[i];
                    temps.Remove(v);
                    this.lb_files.Items.Remove(v);
                }
            }
        }

        private void tsmi_clear_all_Click(object sender, EventArgs e)
        {
            this.lb_files.Items.Clear();
            var temps = (this.lb_files.Tag as List<ItemFileInfo>);
            temps.Clear();
        }

        private void tsmi_clear_show_log_Click(object sender, EventArgs e)
        {
            this.tb_log.Text = "";
        }


    }
}
