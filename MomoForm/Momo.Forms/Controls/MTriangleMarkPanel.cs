using System.ComponentModel;
using System.Drawing;

namespace Momo.Forms
{
    public class MTriangleMarkPanel : MPanel
    {
        /// <summary>
        /// 获取或设置三角标志模式
        /// </summary>
        [Browsable(true), Category("Momo"), Description("三角标志模式")]
        public TriangleMarkMode TriangleMarkMode { get; set; }

        /// <summary>
        /// 获取或设置三角标志大小
        /// </summary>
        [Browsable(true), Category("Momo"), Description("三角标志大小")]
        public Size TriangleMarkSize { get; set; }

        /// <summary>
        /// 获取或设置三角标志颜色
        /// </summary>
        [Browsable(true), Category("Momo"), Description("三角标志颜色")]
        public Color TriangleColor { get; set; }

        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);

            if (this.TriangleMarkMode != TriangleMarkMode.None && TriangleColor != Color.Empty && TriangleMarkSize != null && !TriangleMarkSize.IsEmpty)
            {
                if (this.TriangleMarkMode != TriangleMarkMode.All)
                {
                    this.DrawTriangle(pevent.Graphics, this.TriangleMarkMode);
                }
                else
                {
                    this.DrawTriangle(pevent.Graphics, TriangleMarkMode.Top);
                    this.DrawTriangle(pevent.Graphics, TriangleMarkMode.Right);
                    this.DrawTriangle(pevent.Graphics, TriangleMarkMode.Left);
                    this.DrawTriangle(pevent.Graphics, TriangleMarkMode.Bottom);
                }
            }
        }
        private void DrawTriangle(Graphics graphics, TriangleMarkMode mode)
        {
            var ps = new Point[3];
            if ((mode & TriangleMarkMode.Top) == TriangleMarkMode.Top)
            {
                ps[0] = new Point((this.Width - TriangleMarkSize.Width) / 2, -1);
                ps[1] = new Point((this.Width - TriangleMarkSize.Width) / 2 + TriangleMarkSize.Width, -1);
                ps[2] = new Point(this.Width / 2, TriangleMarkSize.Height);
                graphics.FillPolygon(new SolidBrush(this.TriangleColor), ps);
            }

             if ((mode & TriangleMarkMode.Bottom) == TriangleMarkMode.Bottom)
            {
                ps[0] = new Point((this.Width - TriangleMarkSize.Width) / 2, this.Height);
                ps[1] = new Point((this.Width - TriangleMarkSize.Width) / 2 + TriangleMarkSize.Width, this.Height);
                ps[2] = new Point(this.Width / 2, this.Height - TriangleMarkSize.Height);
                graphics.FillPolygon(new SolidBrush(this.TriangleColor), ps);
            }

            if ((mode & TriangleMarkMode.Left) == TriangleMarkMode.Left)
            {
                ps[0] = new Point(-1, (this.Height - TriangleMarkSize.Width) / 2);
                ps[1] = new Point(-1, (this.Height - TriangleMarkSize.Width) / 2 + TriangleMarkSize.Width);
                ps[2] = new Point(TriangleMarkSize.Height, this.Height / 2);
                graphics.FillPolygon(new SolidBrush(this.TriangleColor), ps);
            }

            if ((mode & TriangleMarkMode.Right) == TriangleMarkMode.Right)
            {
                ps[0] = new Point(this.Width, (this.Height - TriangleMarkSize.Width) / 2);
                ps[1] = new Point(this.Width, (this.Height - TriangleMarkSize.Width) / 2 + TriangleMarkSize.Width);
                ps[2] = new Point(this.Width - TriangleMarkSize.Height, this.Height / 2);
                graphics.FillPolygon(new SolidBrush(this.TriangleColor), ps);
            }

        }
    }
}
