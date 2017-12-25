using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Momo.Forms
{
    public sealed class MHeadLabel : Control
    {
        public MHeadLabel()
        {
            // 设置绘制样式
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer, true);
            base.UpdateStyles();

            this.lineColor = Color.FromArgb(221, 221, 221);
            this.textLineColor = Color.FromArgb(76, 146, 216);
            this.lineHeight = 4;
        }

        private Color lineColor;
        [Category("Momo"), Description("下划线颜色"), DefaultValue(typeof(Color), "221,221,221")]
        public Color LineColor { get { return lineColor; } set { lineColor = value; this.Invalidate(); } }

        private int lineHeight;
        [Category("Momo"), Description("下划线高度"), DefaultValue(4)]
        public int LineHeight { get { return lineHeight; } set { lineHeight = value; this.Invalidate(); } }

        private Color textLineColor;
        [Category("Momo"), Description("文字下划线颜色"), DefaultValue(typeof(Color), "76,146,216")]
        public Color TextLineColor { get { return textLineColor; } set { textLineColor = value; this.Invalidate(); } }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);

            var fontSize = e.Graphics.MeasureString(this.Text, this.Font);
            var textRect = new Rectangle(0, 0, (int)fontSize.Width + 20, this.Height);

            if (this.RightToLeft == RightToLeft.Yes)
            {
                textRect = new Rectangle(this.Width - (int)fontSize.Width - 20, 0, textRect.Width, textRect.Height);
            }

            using (var pen = new Pen(lineColor, lineHeight))
            {
                e.Graphics.DrawLine(pen, 0, this.Height - lineHeight, this.Width, this.Height - lineHeight);
            }

            using (var pen = new Pen(textLineColor, lineHeight))
            {
                e.Graphics.DrawLine(pen, textRect.X, this.Height - lineHeight, textRect.X + textRect.Width, this.Height - lineHeight);
            }

            using (var brush = new SolidBrush(this.ForeColor))
            {
                e.Graphics.DrawString(this.Text, this.Font, brush, textRect.X + 10, (this.Height - fontSize.Height) / 2);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
        }
    }
}
