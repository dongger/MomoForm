using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Momo.Forms
{
    /// <summary>
    /// 带标题的Panel
    /// </summary>
    public class MCaptionPanel : MPanel
    {
        public MCaptionPanel()
        {
            this.CaptionHeight = 40;
            this.CaptionBackColor = Color.FromArgb(46, 204, 113);
            this.CaptionBackColorGradint = Color.FromArgb(39, 174, 96);
            this.CaptionLinearGradientMode = GradientMode.Horizontal;
            this.CaptionForeColor = Color.White;
            this.TitleLocation = CaptionTitleLocation.Center;
            this.title = "带标题面板";
        }

        private Rectangle captionRect;

        protected override void SetPadding()
        {
            Padding padding;

            var borderLeftWidth = this.BorderLeftColor.IsEmpty ? 0 : this.BorderLeftWidth;
            var borderTopWidth = this.BorderTopColor.IsEmpty ? 0 : this.BorderTopWidth;
            var borderRightWidth = this.BorderRightColor.IsEmpty ? 0 : this.BorderRightWidth;
            var borderBottomWidth = this.BorderBottomColor.IsEmpty ? 0 : this.BorderBottomWidth;

            if (this.RadiusMode != Forms.RadiusMode.None)
            {
                padding = new Padding(borderLeftWidth + this.Radius / 2, borderTopWidth + this.Radius / 2, borderRightWidth + this.Radius / 2, borderBottomWidth + this.Radius / 2);
            }
            else
            {
                padding = new Padding(borderLeftWidth, borderTopWidth, borderRightWidth, borderBottomWidth);
            }

            if (this.CaptionLocation == CaptionLocation.Top)
            {
                padding.Top += this.CaptionHeight;
                captionRect = new Rectangle(borderLeftWidth, borderTopWidth, this.Width - borderRightWidth - borderLeftWidth, this.CaptionHeight);
            }
            else if (this.CaptionLocation == CaptionLocation.Left)
            {
                padding.Left += this.CaptionHeight;
                captionRect = new Rectangle(borderLeftWidth, borderTopWidth, this.CaptionHeight, this.Height - borderTopWidth - borderBottomWidth);
            }
            else if (this.CaptionLocation == CaptionLocation.Right)
            {
                padding.Right += this.CaptionHeight;
                captionRect = new Rectangle(this.Width - borderRightWidth - this.CaptionHeight, borderTopWidth, this.CaptionHeight, this.Height - borderBottomWidth - borderTopWidth);
            }
            else if (this.CaptionLocation == CaptionLocation.Bottom)
            {
                padding.Bottom += this.CaptionHeight;
                captionRect = new Rectangle(borderLeftWidth, this.Height - borderRightWidth - this.CaptionHeight, this.Width - borderRightWidth + borderLeftWidth, this.CaptionHeight);
            }

            this.ResetPadding(padding);
            this.Invalidate();
        }

        private CaptionLocation captionLocation;
        [Category("Momo"), Description("标题栏高度"), DefaultValue(40)]
        public CaptionLocation CaptionLocation { get { return this.captionLocation; } set { this.captionLocation = value; this.SetPadding(); } }

        private int captionHeight;
        [Category("Momo"), Description("标题栏高度"), DefaultValue(40)]
        public int CaptionHeight { get { return this.captionHeight; } set { this.captionHeight = value; this.SetPadding(); } }

        [Category("Momo"), Description("标题栏背景色"), DefaultValue(typeof(Color), "#2ecc71")]
        public Color CaptionBackColor { get; set; }

        [Category("Momo"), Description("标题栏背景渐变色"), DefaultValue(typeof(Color), "27ae60")]
        public Color CaptionBackColorGradint { get; set; }

        [Browsable(true), Category("Momo"), Description("背景颜色渐变方向"), DefaultValue(typeof(GradientMode), "Horizontal")]
        public GradientMode CaptionLinearGradientMode { get; set; }

        private Color captionForeColor;
        [Category("Momo"), Description("标题栏文字颜色"), DefaultValue(typeof(Color), "White")]
        public Color CaptionForeColor { get { return this.captionForeColor; } set { this.captionForeColor = value; this.Invalidate(this.captionRect); } }

        [Category("Momo"), Description("标题栏文字位置"), DefaultValue(typeof(CaptionTitleLocation), "Center")]
        public CaptionTitleLocation TitleLocation { get; set; }

        private Font captionFont;
        [Category("Momo"), Description("标题栏文字字体样式")]
        public Font CaptionFont { get { return this.captionFont; } set { this.captionFont = value; this.Invalidate(this.captionRect); } }

        private string title;
        [Category("Momo"), Description("标题栏文字")]
        public string Title { get { return this.title; } set { this.title = value; this.Invalidate(this.captionRect); } }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            if (this.RadiusMode != RadiusMode.None)
            {
                RadiusDrawable.DrawRadius(pevent.Graphics, captionRect, this.RadiusMode != Forms.RadiusMode.None ? Forms.RadiusMode.TopLeft_TopRight : Forms.RadiusMode.None, this.Radius, CaptionBackColor, CaptionBackColorGradint, this.CaptionLinearGradientMode, Color.Empty, 0);
            }
            else
            {
                System.Drawing.Drawing2D.LinearGradientBrush brush;

                if (this.CaptionLinearGradientMode == GradientMode.None)
                {
                    brush = new System.Drawing.Drawing2D.LinearGradientBrush(this.captionRect, this.CaptionBackColor, this.CaptionBackColorGradint, System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                }
                else
                {
                    brush = new System.Drawing.Drawing2D.LinearGradientBrush(this.captionRect, this.CaptionBackColor, this.CaptionBackColorGradint, ((System.Drawing.Drawing2D.LinearGradientMode)CaptionLinearGradientMode));
                }

                pevent.Graphics.FillRectangle(brush, captionRect);
                brush.Dispose();
            }

            var font = this.captionFont == null ? this.Font : this.captionFont;
            if (this.CaptionLocation == CaptionLocation.Top || this.CaptionLocation == CaptionLocation.Bottom)
            {
                var fontSize = Size.Ceiling(pevent.Graphics.MeasureString(this.Title, font));
                var x = this.Radius > 0 && this.RadiusMode != Forms.RadiusMode.None ? this.Radius / 2 + 6 : 6;
                x += this.BorderColor.IsEmpty ? 0 : this.BorderWidth;
                var y = (this.CaptionHeight - fontSize.Height) / 2;
                if (this.TitleLocation == CaptionTitleLocation.Center)
                {
                    x = (this.Width - fontSize.Width) / 2;
                }
                else if (this.TitleLocation == CaptionTitleLocation.Right)
                {
                    x = this.Width - fontSize.Width - x;
                }

                if (this.CaptionLocation == CaptionLocation.Bottom)
                {
                    y += this.captionRect.Y;
                }
                else
                {
                    y += this.BorderWidth / 2;
                }

                using (var brush = new SolidBrush(this.CaptionForeColor))
                {
                    pevent.Graphics.DrawString(this.Title, font, brush, x, y);
                }
            }
            else
            {
                Size fontSize = Size.Empty;

                var height = 0;//总高度
                var length = this.Title.Length;
                for (var i = 0; i < length; i++)
                {
                    fontSize = Size.Ceiling(pevent.Graphics.MeasureString(this.Title.Substring(i, 1), font));
                    height += fontSize.Height + 3;
                }

                height -= 3;

                var y = this.Radius > 0 && this.RadiusMode != Forms.RadiusMode.None ? this.Radius / 2 + 6 : 6;
                y += this.BorderColor.IsEmpty ? 0 : this.BorderWidth / 2;
                var x = (this.CaptionHeight - fontSize.Width) / 2 + this.BorderWidth / 2;
                if (this.CaptionLocation == CaptionLocation.Right)
                {
                    x += this.captionRect.X - this.BorderWidth / 2;
                }

                if (this.TitleLocation == CaptionTitleLocation.Center)
                {
                    y = (this.Height - height) / 2;
                }
                else if (this.TitleLocation == CaptionTitleLocation.Right)
                {
                    y = this.Height - height - y;
                }

                for (var i = 0; i < length; i++)
                {
                    using (var brush = new SolidBrush(this.CaptionForeColor))
                    {
                        fontSize = Size.Ceiling(pevent.Graphics.MeasureString(this.Title.Substring(i, 1), font));
                        //height += fontSize.Height + font.Height;
                        pevent.Graphics.DrawString(this.Title.Substring(i, 1), font, brush, x, y);
                        y += 3 + fontSize.Height;
                    }
                }
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            this.SetPadding();
            base.OnSizeChanged(e);
        }
    }
}
