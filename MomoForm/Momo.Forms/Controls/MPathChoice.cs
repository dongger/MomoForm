using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;

namespace Momo.Forms.Controls
{
    public partial class MPathChoice : UserControl
    {
        public MPathChoice()
        {
            InitializeComponent();
            this.ActivtedBorderColor = Color.FromArgb(74, 182, 1);
            this.BorderColor = Color.FromArgb(189, 195, 199);
            this.borderPanel1.BorderWidth = 1;
        }

        private Color borderColor;
        [Category("Momo"), Description("默认状态下的边框颜色"), DefaultValue(typeof(Color), "236, 240, 241")]
        public Color BorderColor { get { return borderColor; } set { this.borderPanel1.BorderColor = borderColor = value; this.Invalidate(); } }

        [Category("Momo"), Description("激活状态下的边框颜色"), DefaultValue(typeof(Color), "127, 140, 141")]
        public Color ActivtedBorderColor { get; set; }

        /// <summary>
        /// 获取或设置水印文字
        /// </summary>
        [Browsable(true), Category("Momo"), Description("获取或设置水印文字")]
        public string WaterText
        {
            get { return this.lblWater.Text; }
            set
            {
                this.lblWater.Text = value;
                this.lblWater.Visible = this.Text.Trim() == string.Empty;
                this.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置只读
        /// </summary>
        [Browsable(true), Category("Momo"), Description("获取或设置只读")]
        public bool ReadOnly
        {
            get { return this.textBox1.ReadOnly; }
            set { this.textBox1.ReadOnly = value; }
        }

        /// <summary>
        /// 获取或设置标题
        /// </summary>
        [Browsable(true), Category("Momo"), Description("获取或设置标题")]
        public string Title
        {
            get { return this.label1.Text; }
            set
            {
                this.label1.Text = value;
                ChangeSize();
            }
        }

        [Browsable(true), Category("Momo"), Description("用户选定的路径"), DefaultValue("")]
        public string SelectedPath
        {
            get
            {
                return this.textBox1.Text;
            }
            set
            {
                this.textBox1.Text = value;
                this.Invalidate();
            }
        }

        [Browsable(true), Category("Momo"), Description("“新建文件夹”按钮是否显示在文件夹浏览对话框中"), DefaultValue(true)]
        public bool ShowNewFolderButton { get; set; }

        [Browsable(true), Category("Momo"), Description("对话框中在树视图控件上显示的说明文本"), DefaultValue("")]
        public string Description { get; set; }

        [Browsable(true), Category("Momo"), Description("从什么地方开始浏览的根文件夹，默认为 Desktop"), DefaultValue(Environment.SpecialFolder.Desktop)]
        public Environment.SpecialFolder RootFolder { get; set; }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            this.lblWater.Visible = false;
            borderPanel1.BorderColor = this.ActivtedBorderColor;
            this.Invalidate();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            this.lblWater.Visible = this.textBox1.Text.Trim() == string.Empty;
            borderPanel1.BorderColor = this.BorderColor;
            this.Invalidate();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ChangeSize();
            this.Invalidate();
        }

        private void ChangeSize()
        {
            this.label1.Location = new Point(3, (this.Height - this.label1.Height) / 2);
            this.textBox1.Location = new Point(this.label1.Width + 6, (this.Height - this.textBox1.Height) / 2);
            this.lblWater.Location = new Point(this.label1.Width + 6, (this.Height - this.lblWater.Height) / 2);            
            this.button1.Location = new Point(this.Width - 62, (this.Height - this.button1.Height) / 2);
            this.textBox1.Width = this.button1.Left - 5;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.lblWater.Visible = this.textBox1.Text.Trim() == string.Empty;
        }

        private void lblWater_Click(object sender, EventArgs e)
        {
            this.lblWater.Visible = false;
            this.textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.SelectedPath = this.SelectedPath;
            dialog.ShowNewFolderButton = this.ShowNewFolderButton;
            dialog.Description = this.Description;
            dialog.RootFolder = this.RootFolder;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.SelectedPath = dialog.SelectedPath;
            }
        }
    }
}
