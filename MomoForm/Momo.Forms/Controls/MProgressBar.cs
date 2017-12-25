using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Momo.Forms.Controls
{
    /// <summary>
    /// 进度条
    /// </summary>
    public sealed class MProgressBar : UserControl
    {
        protected override Size DefaultSize
        {
            get
            {
                return new Size(100, 30);
            }
        }

        public MProgressBar()
        {
            this.totalColor = new GradientColor(GradientMode.Vertical, Color.FromArgb(26, 188, 156), Color.FromArgb(22, 160, 133));
            this.valueColor = new GradientColor(GradientMode.Vertical, Color.FromArgb(46, 204, 113), Color.FromArgb(39, 174, 96));
            this.showText = true;
        }

        public override Size MinimumSize
        {
            get
            {
                return base.MinimumSize;
            }
            set
            {
                if (value.Height < 30)
                {
                    value = new Size(value.Width, 30);
                }

                if (value.Width < 100)
                {
                    value = new Size(100, value.Height);
                }

                base.MinimumSize = value;
            }
        }

        public override Size MaximumSize
        {
            get
            {
                return base.MaximumSize;
            }
            set
            {
                if (value.Height > 100)
                {
                    value = new Size(value.Width, 100);
                }

                base.MaximumSize = value;
            }
        }

        private GradientColor totalColor;

        /// <summary>
        /// 背景色
        /// </summary>
        [Description("背景色与渐变处理")]
        public GradientColor TotalColor { get { return this.totalColor; } set { this.totalColor = value; this.Invalidate(); } }

        private GradientColor valueColor;
        /// <summary>
        /// 背景色
        /// </summary>
        [Description("背景色与渐变处理")]
        public GradientColor ValueColor { get { return this.valueColor; } set { this.valueColor = value; this.Invalidate(); } }

        private bool showText;
        public bool ShowText { get { return this.showText; } set { this.showText = value; this.Invalidate(); } }

        private int total = 100;
        public int Total
        {
            get { return total; }
            set { total = value; this.Invalidate(); }
        }

        private int value = 0;
        public int Value
        {
            get { return value; }
            set { this.value = value; this.Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var text = string.Format("{0}%", Math.Round(((decimal)value / (decimal)total) * 100, 2));

            var fontSize = Size.Ceiling(e.Graphics.MeasureString(text, this.Font));
            var x = this.Width - fontSize.Width - 6 + e.ClipRectangle.X;
            var y = (this.Height - fontSize.Height) / 2;

            using (var brush = new SolidBrush(this.ForeColor))
            {
                e.Graphics.DrawString(text, this.Font, brush, x, y);
            }
            //base.OnPaint(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
            var graphics = e.Graphics;
            if (this.TotalColor.GradientMode == GradientMode.None || this.TotalColor.ToColor.IsEmpty)
            {
                using (var brush = new SolidBrush(this.TotalColor.FromColor))
                {
                    graphics.FillRectangle(brush, e.ClipRectangle);
                }
            }
            else
            {
                using (var b = new LinearGradientBrush(e.ClipRectangle, this.TotalColor.FromColor, this.TotalColor.ToColor, (LinearGradientMode)this.TotalColor.GradientMode))
                {
                    graphics.FillRectangle(b, e.ClipRectangle);
                }
            }

            var width = (int)Math.Floor(this.Width * ((decimal)this.value / (decimal)this.total));
            if (width == 0) { return; }
            var rec = new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y, width, e.ClipRectangle.Height);
            if (this.ValueColor.GradientMode == GradientMode.None || this.ValueColor.ToColor.IsEmpty)
            {
                using (var brush = new SolidBrush(this.ValueColor.FromColor))
                {
                    graphics.FillRectangle(brush, rec);
                }
            }
            else
            {
                using (var b = new LinearGradientBrush(rec, this.ValueColor.FromColor, this.ValueColor.ToColor, (LinearGradientMode)this.ValueColor.GradientMode))
                {
                    graphics.FillRectangle(b, rec);
                }
            }
        }
    }
}
