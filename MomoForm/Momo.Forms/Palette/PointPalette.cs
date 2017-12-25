using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Momo.Forms
{
    public sealed class PointPalette : Palette
    {
        //public PointPalette() { }
        //public PointPalette(Control container) : base(container)
        //{
        //}

        [Category("圆点"), Description("边线颜色"), DefaultValue(typeof(Color), "Red")]
        public Color BorderColor { get; set; }

        [Category("圆点"), Description("圆点颜色"), DefaultValue(typeof(Color), "White")]
        public Color PointColor { get; set; }

        [Category("圆点"), Description("边框大小"), DefaultValue(typeof(int), "1")]
        public int BorderWidth { get; set; }

        public override void Draw(Graphics graphics)
        {
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using (var pen = new Pen(this.BorderColor, this.BorderWidth))
            {
                graphics.DrawEllipse(pen, this.Rectangle);
            }

            using (var brush = new SolidBrush(this.PointColor))
            {
                graphics.FillEllipse(brush, this.Rectangle);
            }
        }        
    }
}
