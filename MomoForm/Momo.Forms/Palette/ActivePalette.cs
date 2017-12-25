using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Momo.Forms
{
    public abstract class ActivePalette : Palette
    {
        /// <summary>
        /// 是否显示帮助按钮
        /// </summary>
        [Description("按钮激活背景颜色")]
        public GradientColor ControlActivedColor { get; set; }

        /// <summary>
        /// 是否显示帮助按钮
        /// </summary>
        [Description("按钮默认背景色")]
        public GradientColor ControlBackColor { get; set; }

        /// <summary>
        /// 当前是否鼠标悬浮状态
        /// </summary>
        [Browsable(false)]
        public bool Hover { get; set; }

        protected virtual void DrawBackground(Graphics graphics)
        {
            var color = this.Hover ? this.ControlActivedColor : this.ControlBackColor;
            RadiusDrawable.DrawRadius(graphics, this.Rectangle, RadiusMode.None, 0, color.FromColor, color.ToColor, color.GradientMode, Color.Empty, 0);
            //using (var brush = new SolidBrush(this.Hover ? this.ControlActivedColor : this.ControlBackColor))
            //{
            //    graphics.FillRectangle(brush, this.Rectangle);
            //}
        }

        public override void Draw(Graphics graphics)
        {
            this.Draw(graphics);
        }

        protected override void MouseMoveIn(MouseEventArgs e)
        {
            this.Hover = true;
            this.Invalidate();
        }

        protected override void MouseMoveOut(MouseEventArgs e)
        {
            this.Hover = false;
            this.Invalidate();
        }
    }
}
