using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Momo.Forms
{
    /// <summary>
    /// 标题栏顶部填充按钮
    /// </summary>
    public sealed class CaptionFullButton : CaptionControlButton
    {
        public CaptionFullButton()
        {
            this.Font = new Font("微软雅黑", 14);
            this.ForeColor = Color.White;
        }
        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; }

        public Font Font { get; set; }

        public Color ForeColor { get; set; }

        public Size ImageSize { get; set; }

        [Obsolete("此项无效")]
        [Browsable(false)]
        public override Padding Padding
        {
            get
            {
                return base.Padding;
            }

            set
            {
                base.Padding = value;
            }
        }

        public override void Draw(Graphics graphics)
        {
            base.DrawBackground(graphics);
            if (this.Image != null)
            {
                ImageAttributes ImgAtt = new ImageAttributes();
                ImgAtt.SetWrapMode(System.Drawing.Drawing2D.WrapMode.Clamp);
                var fontSize = Size.Ceiling(graphics.MeasureString(this.Text, this.Font));
                var x = (this.Width - fontSize.Width - 4 - this.ImageSize.Width) / 2;//图片与文字间隔4个像素
                var y = (this.Height - ImageSize.Height) / 2;

                var rec = new Rectangle(this.X + x, this.Y + y, this.ImageSize.Width, this.ImageSize.Height);
                graphics.DrawImage(this.Image, rec, 0, 0, this.Image.Width, this.Image.Height, GraphicsUnit.Pixel, ImgAtt);
                x += ImageSize.Width + 4;
                using (var brush = new SolidBrush(this.ForeColor))
                {
                    graphics.DrawString(this.Text, this.Font, brush, x + this.X, (this.Height - fontSize.Height) / 2);
                }
            }

            if (this.Notify != null)
            {
                this.Notify.Draw(graphics);
            }
        }
    }
}
