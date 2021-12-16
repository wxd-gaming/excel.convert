using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Excel.Convert
{
    public partial class FormMain : Form
    {

        /// <summary>
        /// 显示日志
        /// </summary>         
        public static Action<string> ShowLog { get; set; }

        public FormMain()
        {
            InitializeComponent();
        }

        void AddFileViwe(string[] fileNames)
        {
            var temps = (this.lb_files.Tag as List<ItemFileInfo>);
            foreach (var itemFile in fileNames)
            {
                string extname = System.IO.Path.GetExtension(itemFile);
                string name = System.IO.Path.GetFileName(itemFile);
                string extnameLower = extname.ToLower();
                if (".xml".Equals(extnameLower) || ".proto".Equals(extnameLower) || ".xml".Equals(extnameLower))
                {
                    var tifi = temps.FirstOrDefault(l => l.Name == name);
                    if (tifi == null)
                    {
                        var ifi = new ItemFileInfo(itemFile);
                        (this.lb_files.Tag as List<ItemFileInfo>).Add(ifi);
                        this.lb_files.Items.Add(ifi);
                    }
                    else
                    {
                        ShowLog.Invoke("无需添加的文件：" + name);
                    }
                }
            }
            if (this.lb_files.Items.Count > 0)
            {
                this.lb_files.SelectedIndex = 0;
            }
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
                this.tb_log.Invoke(new Action(() =>
                {
                    this.tb_log.Text = str + "\r\n" + this.tb_log.Text;
                }));
            };

            this.lb_files.Tag = new List<ItemFileInfo>();

            ShowLog.Invoke("欢迎使用 無心道 软件");
        }

        private void tsmi_clear_log_Click(object sender, EventArgs e)
        {
            this.tb_log.Text = "";
        }

        /// <summary>
        /// 选择文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_check_dir_Click(object sender, EventArgs e)
        {

        }

        private void tsmi_clear_file_Click(object sender, EventArgs e)
        {
            //删除一个文件
        }
    }
}
