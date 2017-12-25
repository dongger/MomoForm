using System.Drawing;

namespace Momo.Forms
{
    /// <summary>
    /// 更新通知或数字通知标志绘制
    /// </summary>
    public sealed class NotifyPalette : Palette
    {
        public NotifyPalette()
        {
            this.ForeColor = Color.White;
            this.BackColor = Color.Red;
            this.Font = new Font("宋体", 8);
        }

        internal Palette ParentPalette { get; set; }

        public string Text { get; set; }

        public Color ForeColor { get; set; }

        public Color BackColor { get; set; }

        public Font Font { get; set; }

        public override void Draw(Graphics graphics)
        {
            var fontSize = Size.Ceiling(graphics.MeasureString(this.Text, this.Font));
            var x = ParentPalette.Rectangle.X + ParentPalette.Width - fontSize.Width - 2;
            var y = ParentPalette.Rectangle.Y + 2;
            this.Rectangle = new Rectangle(x, y, fontSize.Width + 2, fontSize.Height + 2);

            // 画圆角背景
            RadiusDrawable.DrawRadius(graphics, this.Rectangle, RadiusMode.All, (fontSize.Height+2) / 2, this.BackColor, this.BackColor, GradientMode.None, Color.Empty, 0);
            using (var brush = new SolidBrush(this.ForeColor))
            {
                x += 1;
                y += 1;
                graphics.DrawString(this.Text, this.Font, brush, x, y);
            }
        }
    }
}
