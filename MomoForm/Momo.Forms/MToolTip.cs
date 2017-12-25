using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Momo.Forms
{
    public sealed class MToolTip : ToolTip
    {
        public MToolTip()
        {
            this.Draw += MToolTip_Draw;
            this.Popup += MToolTip_Popup;
            this.OwnerDraw = true;
            this.BorderColor = Color.FromArgb(187, 223, 255);
            this.BorderWidth = 4;
            this.ToolTipSize = new Size(140, 30);
        }

        [Browsable(true), Category("Momo"), Description("边框颜色")]
        public Color BorderColor { get; set; }

        [Browsable(true), Category("Momo"), Description("边框宽度")]
        public int BorderWidth { get; set; }

        [Browsable(true), Category("Momo"), Description("提示大小")]
        public Size ToolTipSize { get; set; }

        private void MToolTip_Popup(object sender, PopupEventArgs e)
        {
            e.ToolTipSize = ToolTipSize;
        }

        private void MToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //e.DrawBackground();

            using (var brush = new SolidBrush(this.BackColor))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }

            using (var pen = new Pen(this.BorderColor, this.BorderWidth))
            {
                e.Graphics.DrawRectangle(pen, e.Bounds);
            }
            //e.DrawBorder();

            var fontSize = e.Graphics.MeasureString(e.ToolTipText, e.Font);

            using (var brush = new SolidBrush(this.ForeColor))
            {
                e.Graphics.DrawString(e.ToolTipText, e.Font, brush, BorderWidth * 2, (e.Bounds.Height - fontSize.Height) / 2);
            }
        }
    }
}
