using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;

namespace Momo.Forms.Controls
{
    public partial class MFileChoice : UserControl
    {
        public MFileChoice()
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

        [Browsable(true), Category("Momo"), Description("指示对话框是否允许选择多个文件"), DefaultValue("")]
        public string Filter { get; set; }

        [Browsable(true), Category("Momo"), Description("包含在文件对话框中选定的文件名的字符串"), DefaultValue("")]
        public string FileName
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

        [Browsable(true), Category("Momo"), Description("文件对话框显示的初始目录"), DefaultValue("")]
        public string InitialDirectory { get; set; }

        [Browsable(true), Category("Momo"), Description("文件对话框标题"), DefaultValue("")]
        public string FileDialogTitle { get; set; }

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
            var dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Title = this.FileDialogTitle;
            dialog.FileName = this.FileName;
            dialog.Filter = this.Filter;
            dialog.InitialDirectory = this.InitialDirectory;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.FileName = dialog.FileName;
            }
        }
    }
}
