using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Momo.Forms
{
    public sealed class MRichTextBox : RichTextBox
    {
        private readonly Label lblWater = new Label();
        public MRichTextBox()
        {
            //this.SetStyle(ControlStyles.ResizeRedraw, true);//调整大小时重绘
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);// 双缓冲
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);// 禁止擦除背景.
            //this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //this.SetStyle(ControlStyles.UserPaint, true);                        

            this.lblWater = new Label();
            this.lblWater.Location = new Point(3, 3);
            this.lblWater.Text = "水印文字";
            this.lblWater.MaximumSize = this.Size;
            this.lblWater.AutoSize = true;
            this.lblWater.Click += LblWater_Click;
            this.Controls.Add(this.lblWater);
        }

        private void LblWater_Click(object sender, EventArgs e)
        {
            this.Focus();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true), Category("Momo"), Description("最大行数，达到后，将自动清除")]
        public int MaxLines { get; set; }

        [Browsable(true), Category("Momo"), Description("水印文字颜色")]
        public Color WaterForeColor { get { return this.lblWater.ForeColor; } set { this.lblWater.ForeColor = value; } }

        [Browsable(true), Category("Momo"), Description("水印文字")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string WaterText { get { return this.lblWater.Text; } set { this.lblWater.Text = value; this.Invalidate(); } }

        public new void AppendText(string text)
        {
            if (this.MaxLines > 0 && this.Lines.Length >= this.MaxLines)
            {
                this.Clear();
            }

            base.AppendText(text);
        }

        public void AppendFormat(string format, params string[] values)
        {
            this.AppendText(string.Format(format, values));
        }

        public void AppendNewLine()
        {
            base.AppendText(Environment.NewLine);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.lblWater.Visible = false;
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.lblWater.Visible = this.Text.Length == 0;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.lblWater.MaximumSize = this.Size;
        }
    }
}
