using System;
using System.Drawing;
using System.Windows.Forms;

namespace Momo.Forms
{
    public sealed class LabelPalette : Palette
    {
        public event EventHandler MouseEnter;
        public event EventHandler MouseLeave;
        public event EventHandler Click;

        public string Text { get; set; }

        public Font Font { get; set; }

        public Color ForeColor { get; set; }

        public TextAlignment Align { get; set; }

        public Color BackColor { get; set; }

        public Color HoverColor { get; set; }

        public RadiusMode RadiusMode { get; set; }

        public int Radius { get; set; }

        private bool isHover;

        protected override void MouseClick(MouseEventArgs e)
        {
            this.Click?.Invoke(this, e);
        }

        protected override void MouseMoveIn(MouseEventArgs e)
        {
            isHover = true;
            this.Invalidate();
            this.MouseEnter?.Invoke(this, e);
        }

        protected override void MouseMoveOut(MouseEventArgs e)
        {
            isHover = false;
            this.Invalidate();
            this.MouseLeave?.Invoke(this, e);
        }

        public override void Draw(Graphics graphics)
        {
            if (this.isHover)
            {
                GDIHelper.DrawString(graphics, this.Rectangle, Font, Text, ForeColor, this.HoverColor, this.Align, this.RadiusMode, this.Radius);
            }
            else
            {
                GDIHelper.DrawString(graphics, this.Rectangle, Font, Text, ForeColor, this.BackColor, this.Align, this.RadiusMode, this.Radius);
            }
        }
    }
}
