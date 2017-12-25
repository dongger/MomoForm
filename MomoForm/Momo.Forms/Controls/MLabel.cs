using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;

namespace Momo.Forms
{
    public sealed class MLabel : Control
    {
        public MLabel()
        {
            // 设置绘制样式
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            base.UpdateStyles();
            this.BackColor = Color.Transparent;
        }

        private MBorder border = new MBorder();
        [Browsable(true), Category("Momo"), Description("边框定义")]
        public MBorder Border { get { return this.border; } set { this.border = value; this.Invalidate(); } }

        private Color backColor = SystemColors.Control;
        [Browsable(true), Category("Momo"), Description("背景色")]
        public Color BackgroundColor { get { return this.backColor; } set { this.backColor = value; this.Invalidate(); } }

        private RadiusMode radiusMode = RadiusMode.None;
        [Browsable(true), Category("Momo"), Description("圆角模式")]
        public RadiusMode RadiusMode { get { return radiusMode; } set { radiusMode = value; this.Invalidate(); } }

        private int radius = 0;
        [Browsable(true), Category("Momo"), Description("圆角大小")]
        public int Radius { get { return radius; } set { radius = value; this.Invalidate(); } }

        private bool autoSize = false;
        [Browsable(true), Category("Momo"), Description("自动改变大小")]
        public override bool AutoSize { get { return autoSize; } set { autoSize = value; this.Invalidate(); } }

        private TextAlignment align = TextAlignment.MiddleCenter;
        [Browsable(true), Category("Momo"), Description("文字对齐方式")]
        public TextAlignment Align { get { return align; } set { align = value; this.Invalidate(); } }

        private string text;
        [Browsable(true), Category("Momo"), Description("文字内容")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public override string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (string.IsNullOrEmpty(this.Text))
            {
                this.text = string.Empty;
            }
            var size = e.Graphics.MeasureString(Text, this.Font);
            var row = (int)size.Width / (this.Width - 6);

            if (Text.Contains(System.Environment.NewLine))
            {
                row += this.Text.Split(new string[] { System.Environment.NewLine }, System.StringSplitOptions.None).Length;
            }
            else if (Text.Contains("\n"))
            {
                row += this.Text.Split(new string[] { "\n" }, System.StringSplitOptions.None).Length;
            }

            var height = size.Width > this.Width ? (row + 1) * this.Font.Height : 30;
            //using (var brush = new SolidBrush(Color.Black))
            //{
            //    e.Graphics.DrawString(Text, this.Font, brush, new Rectangle(3, this.Caption.Height + 3, this.Width - 6, height));
            //}


            if (this.AutoSize)
            {
                var left = this.border.Width > 0 ? this.border.Width : this.border.Left;
                var right = this.border.Width > 0 ? this.border.Width : this.border.Right;
                var bottom = this.border.Width > 0 ? this.border.Width : this.border.Bottom;
                var top = this.border.Width > 0 ? this.border.Width : this.border.Top;
                var w = left + right + size.Width;
                var h = top + bottom + height;

                if (this.Size.Height != (int)h || this.Size.Width != (int)w)
                {
                    this.Size = new Size((int)Math.Ceiling(w), h);
                }
            }

            // 注意，如果使用e.ClipRectangle，容器控件移动过程中，会出现重叠绘制
            if (this.radiusMode != RadiusMode.None && this.radius > 0)
            {
                GDIHelper.DrawString(e.Graphics, ClientRectangle, this.Font, this.Text, this.ForeColor, this.BackgroundColor, Align, RadiusMode, Radius);
            }
            else if (this.border != null && !this.border.IsEmpty)
            {
                var left = this.border.Width > 0 ? this.border.Width : this.border.Left;
                var right = this.border.Width > 0 ? this.border.Width : this.border.Right;
                var bottom = this.border.Width > 0 ? this.border.Width : this.border.Bottom;
                var top = this.border.Width > 0 ? this.border.Width : this.border.Top;
                #region 绘制边框
                using (var pen = new Pen(this.border.Color))
                {
                    if (top > 0)
                    {
                        pen.Width = top;
                        e.Graphics.DrawLine(pen, 0, 0, this.Width, 0);
                    }

                    if (left > 0)
                    {
                        pen.Width = left;
                        e.Graphics.DrawLine(pen, 0, 0, 0, this.Height);
                    }

                    if (right > 0)
                    {
                        pen.Width = right;
                        e.Graphics.DrawLine(pen, this.Width - right, 0, this.Width - right, this.Height);
                    }

                    if (bottom > 0)
                    {
                        pen.Width = bottom;
                        e.Graphics.DrawLine(pen, 0, this.Height - bottom, 0, this.Height - bottom);
                    }
                }
                #endregion

                GDIHelper.DrawString(e.Graphics, new Rectangle(ClientRectangle.X + left, ClientRectangle.Y + top, ClientRectangle.Width - left - right, ClientRectangle.Height - top - bottom), this.Font, this.Text, this.ForeColor, this.Align);
            }
            else
            {
                //using (var brush = new SolidBrush(this.ForeColor))
                //{
                //    e.Graphics.DrawString(Text, Font, brush, new Rectangle(0, 0, this.Width, height));
                //}
                if (row > 1)
                {
                    GDIHelper.DrawString(e.Graphics, new Rectangle(0, 0, this.Width, height), this.Font, this.Text, this.ForeColor, this.Align);
                }
                else
                {
                    GDIHelper.DrawString(e.Graphics, ClientRectangle, this.Font, this.Text, this.ForeColor, this.Align);
                }
            }
        }
    }
}
