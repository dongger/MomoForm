using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Momo.Forms
{
    public partial class MButton : Control, IButtonControl
    {
        protected override Size DefaultSize
        {
            get
            {
                return new Size(100, 30);
            }
        }

        public MButton()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);//调整大小时重绘
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);// 双缓冲
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);// 禁止擦除背景.
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            base.BackColor = Color.Transparent;
            this.backColor = new GradientColor(GradientMode.Vertical, Color.FromArgb(26, 188, 156), Color.FromArgb(22, 160, 133));
            this.borderColor = Color.FromArgb(46, 204, 113);
            this.BorderHoverColor = Color.FromArgb(39, 174, 96);
            this.BorderPressColor = Color.FromArgb(26, 188, 156);
            this.textImageRelation = ImageTextRelation.Overlay;

            this.AutoSize = false;
            this.ForeColor = Color.White;

            this.Cursor = Cursors.Hand;
            UpdateStyles();
            this.ResizeRedraw = true;
        }

        public DialogResult DialogResult { get; set; }
        private ButtonStatus status;
        private int borderWidth = 1;
        [Browsable(true), Category("Momo"), Description("边框大小")]
        public int BorderWidth { get { return borderWidth; } set { borderWidth = value; this.Invalidate(); } }

        private GradientColor backColor = new GradientColor(GradientMode.None, Color.White, Color.White);
        [Browsable(true), Category("Momo"), Description("按钮默认状态背景色")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GradientColor BackgroundColor { get { return this.backColor; } set { this.backColor = value; this.Invalidate(); } }

        private GradientColor hoverBackColor = new GradientColor(GradientMode.None, Color.White, Color.White);
        [Browsable(true), Category("Momo"), Description("按钮悬浮状态背景色")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GradientColor HoverBackColor { get { return this.hoverBackColor; } set { this.hoverBackColor = value; this.Invalidate(); } }

        private RadiusMode radiusMode = RadiusMode.None;
        [Browsable(true), Category("Momo"), Description("按钮圆角模式")]
        public RadiusMode RadiusMode { get { return radiusMode; } set { radiusMode = value; this.Invalidate(); } }

        private int radius = 0;
        [Browsable(true), Category("Momo"), Description("按钮圆角大小")]
        public int Radius { get { return radius; } set { radius = value; this.Invalidate(); } }

        private bool circleButton = false;
        [Browsable(true), Category("Momo"), Description("是否圆形按钮"), DefaultValue(typeof(bool), "false")]
        public bool CircleButton { get { return circleButton; } set { circleButton = value; this.Invalidate(); } }

        private ImageTextRelation textImageRelation = ImageTextRelation.TextAboveImage;
        [Browsable(true), Category("Momo"), Description("文字图片对齐方式"), DefaultValue(typeof(bool), "2")]
        public ImageTextRelation TextImageRelation { get { return textImageRelation; } set { textImageRelation = value; this.Invalidate(); } }

        private Image image;
        [Browsable(true), Category("Momo"), Description("按钮图片")]
        public Image Image { get { return image; } set { this.image = value; this.Invalidate(); } }

        private Size imageSize = new Size(16, 16);
        [Browsable(true), Category("Momo"), Description("图片大小")]
        public Size ImageSize { get { return imageSize; } set { this.imageSize = value; this.Invalidate(); } }

        private Color borderColor;
        [Browsable(true), Category("Momo"), Description("边框颜色"), DefaultValue(typeof(Color), "#27ae60")]
        public Color BorderColor { get { return borderColor; } set { this.borderColor = value; this.Invalidate(); } }

        [Browsable(true), Category("Momo"), Description("焦点边框颜色"), DefaultValue(typeof(Color), "#27ae60")]
        public Color BorderHoverColor { get; set; }

        [Browsable(true), Category("Momo"), Description("边框按下颜色"), DefaultValue(typeof(Color), "#27ae60")]
        public Color BorderPressColor { get; set; }

        private ContentAlignment alignment = ContentAlignment.MiddleCenter;
        [Browsable(true), Category("Momo"), Description("图片对齐方式"), DefaultValue(typeof(ContentAlignment), "32")]
        public ContentAlignment Alignment { get { return alignment; } set { this.alignment = value; this.Invalidate(); } }

        #region 按钮绘制

        private void Calc(Size textSize, out Point imgPos, out Point txtPos, out Size contentSize)
        {
            var space = 3;
            var imgWidth = imageSize.Width;
            var imgHeight = imageSize.Height;

            var txtWidth = textSize.Width;
            var txtHeight = textSize.Height;

            contentSize = Size.Empty;
            imgPos = Point.Empty;
            txtPos = Point.Empty;

            #region 图片在文本上面
            if (this.textImageRelation == ImageTextRelation.ImageAboveText)
            {
                var height = imgHeight + space + txtHeight;
                var imgCenterX = (this.Width - imgWidth) / 2;
                var imgRightX = this.Width - imgWidth;
                var txtCenterX = (this.Width - txtWidth) / 2;
                var txtRightX = this.Width - txtWidth;
                contentSize = new Size(txtWidth > imgWidth ? txtWidth : imgWidth, height);

                if (this.alignment == ContentAlignment.BottomCenter)
                {
                    imgPos = new Point(imgCenterX, this.Height - height);
                    txtPos = new Point(txtCenterX, imgPos.Y + imgHeight + space);
                }
                else if (this.alignment == ContentAlignment.BottomLeft)
                {
                    imgPos = new Point(0, this.Height - height);
                    txtPos = new Point(0, imgPos.Y + imgHeight + space);
                }
                else if (this.alignment == ContentAlignment.BottomRight)
                {
                    imgPos = new Point(imgRightX, this.Height - height);
                    txtPos = new Point(txtRightX, imgPos.Y + imgHeight + space);
                }
                else if (this.alignment == ContentAlignment.MiddleCenter)
                {
                    imgPos = new Point(imgCenterX, (this.Height - height) / 2);
                    txtPos = new Point(txtCenterX, imgPos.Y + imgHeight + space);
                }
                else if (this.alignment == ContentAlignment.MiddleLeft)
                {
                    imgPos = new Point(0, (this.Height - height) / 2);
                    txtPos = new Point(0, imgPos.Y + imgHeight + space);
                }
                else if (this.alignment == ContentAlignment.MiddleRight)
                {
                    imgPos = new Point(imgRightX, (this.Height - height) / 2);
                    txtPos = new Point(txtRightX, imgPos.Y + imgHeight + space);
                }
                else if (this.alignment == ContentAlignment.TopCenter)
                {
                    imgPos = new Point(imgCenterX, 0);
                    txtPos = new Point(txtCenterX, imgPos.Y + imgHeight + space);
                }
                else if (this.alignment == ContentAlignment.TopLeft)
                {
                    imgPos = new Point(0, 0);
                    txtPos = new Point(0, imgPos.Y + imgHeight + space);
                }
                else if (this.alignment == ContentAlignment.TopRight)
                {
                    imgPos = new Point(imgRightX, 0);
                    txtPos = new Point(txtRightX, imgPos.Y + imgHeight + space);
                }
            }
            #endregion
            #region 图片在文本后面
            else if (this.textImageRelation == ImageTextRelation.ImageAfterText)
            {
                var with = txtWidth + imgWidth + space;
                var txtCenterX = (this.Width - with) / 2;
                var txtRightX = this.Width - with;
                var imgCenterX = txtCenterX + txtWidth + space;
                var imgRightX = txtRightX + txtWidth + space;
                contentSize = new Size(with, imgHeight > txtHeight ? imgHeight : txtHeight);

                if (this.alignment == ContentAlignment.BottomCenter)
                {
                    imgPos = new Point(imgCenterX, this.Height - imgHeight);
                    txtPos = new Point(txtCenterX, this.Height - txtHeight);
                }
                else if (this.alignment == ContentAlignment.BottomLeft)
                {
                    imgPos = new Point(txtWidth + space, this.Height - imgHeight);
                    txtPos = new Point(0, this.Height - txtHeight);
                }
                else if (this.alignment == ContentAlignment.BottomRight)
                {
                    imgPos = new Point(imgRightX, this.Height - imgHeight);
                    txtPos = new Point(txtRightX, this.Height - txtHeight);
                }
                else if (this.alignment == ContentAlignment.MiddleCenter)
                {
                    imgPos = new Point(imgCenterX, (this.Height - imgHeight) / 2);
                    txtPos = new Point(txtCenterX, (this.Height - txtHeight) / 2);
                }
                else if (this.alignment == ContentAlignment.MiddleLeft)
                {
                    imgPos = new Point(txtWidth + space, (this.Height - imgHeight) / 2);
                    txtPos = new Point(0, (this.Height - txtHeight) / 2);
                }
                else if (this.alignment == ContentAlignment.MiddleRight)
                {
                    imgPos = new Point(imgRightX, (this.Height - imgHeight) / 2);
                    txtPos = new Point(txtRightX, (this.Height - txtHeight) / 2);
                }
                else if (this.alignment == ContentAlignment.TopCenter)
                {
                    imgPos = new Point(imgCenterX, 0);
                    txtPos = new Point(txtCenterX, 0);
                }
                else if (this.alignment == ContentAlignment.TopLeft)
                {
                    imgPos = new Point(txtWidth + space, 0);
                    txtPos = new Point(0, 0);
                }
                else if (this.alignment == ContentAlignment.TopRight)
                {
                    imgPos = new Point(imgRightX, 0);
                    txtPos = new Point(txtRightX, 0);
                }
            }
            #endregion
            #region 图片在文字前面
            else if (this.textImageRelation == ImageTextRelation.ImageBeforeText)
            {
                var width = txtWidth + imgWidth + space;
                var imgCenterX = (this.Width - width) / 2;
                var imgRightX = this.Width - width;
                var txtCenterX = imgCenterX + imgWidth + space;
                var txtRightX = imgRightX + imgWidth + space;

                contentSize = new Size(width, imgHeight > txtHeight ? imgHeight : txtHeight);
                if (this.alignment == ContentAlignment.BottomCenter)
                {
                    imgPos = new Point(imgCenterX, this.Height - imgHeight);
                    txtPos = new Point(txtCenterX, this.Height - txtHeight);
                }
                else if (this.alignment == ContentAlignment.BottomLeft)
                {
                    imgPos = new Point(0, this.Height - imgHeight);
                    txtPos = new Point(imgWidth + space, this.Height - txtHeight);
                }
                else if (this.alignment == ContentAlignment.BottomRight)
                {
                    imgPos = new Point(imgRightX, this.Height - imgHeight);
                    txtPos = new Point(txtRightX, this.Height - txtHeight);
                }
                else if (this.alignment == ContentAlignment.MiddleCenter)
                {
                    imgPos = new Point(imgCenterX, (this.Height - imgHeight) / 2);
                    txtPos = new Point(txtCenterX, (this.Height - txtHeight) / 2);
                }
                else if (this.alignment == ContentAlignment.MiddleLeft)
                {
                    imgPos = new Point(0, (this.Height - imgHeight) / 2);
                    txtPos = new Point(imgWidth + space, (this.Height - txtHeight) / 2);
                }
                else if (this.alignment == ContentAlignment.MiddleRight)
                {
                    imgPos = new Point(imgRightX, (this.Height - imgHeight) / 2);
                    txtPos = new Point(txtRightX, (this.Height - txtHeight) / 2);
                }
                else if (this.alignment == ContentAlignment.TopCenter)
                {
                    imgPos = new Point(imgCenterX, 0);
                    txtPos = new Point(txtCenterX, 0);
                }
                else if (this.alignment == ContentAlignment.TopLeft)
                {
                    imgPos = new Point(0, 0);
                    txtPos = new Point(imgWidth + space, 0);
                }
                else if (this.alignment == ContentAlignment.TopRight)
                {
                    imgPos = new Point(imgRightX, 0);
                    txtPos = new Point(txtRightX, 0);
                }
            }
            #endregion
            #region 重叠
            else if (this.textImageRelation == ImageTextRelation.Overlay)
            {
                var imgCenterX = (this.Width - imgWidth) / 2;
                var imgRightX = this.Width - imgWidth;
                var txtCenterX = (this.Width - txtWidth) / 2;
                var txtRightX = this.Width - txtWidth;
                contentSize = new Size(imgWidth > txtWidth ? imgWidth : txtWidth, imgHeight > txtHeight ? imgHeight : txtHeight);
                if (this.alignment == ContentAlignment.BottomCenter)
                {
                    imgPos = new Point(imgCenterX, this.Height - imgHeight);
                    txtPos = new Point(txtCenterX, this.Height - txtHeight);
                }
                else if (this.alignment == ContentAlignment.BottomLeft)
                {
                    imgPos = new Point(0, this.Height - imgHeight);
                    txtPos = new Point(0, this.Height - txtHeight);
                }
                else if (this.alignment == ContentAlignment.BottomRight)
                {
                    imgPos = new Point(imgRightX, this.Height - imgHeight);
                    txtPos = new Point(txtRightX, this.Height - txtHeight);
                }
                else if (this.alignment == ContentAlignment.MiddleCenter)
                {
                    imgPos = new Point(imgCenterX, (this.Height - imgHeight) / 2);
                    txtPos = new Point(txtCenterX, (this.Height - txtHeight) / 2);
                }
                else if (this.alignment == ContentAlignment.MiddleLeft)
                {
                    imgPos = new Point(0, (this.Height - imgHeight) / 2);
                    txtPos = new Point(0, (this.Height - txtHeight) / 2);
                }
                else if (this.alignment == ContentAlignment.MiddleRight)
                {
                    imgPos = new Point(imgRightX, (this.Height - imgHeight) / 2);
                    txtPos = new Point(txtRightX, (this.Height - txtHeight) / 2);
                }
                else if (this.alignment == ContentAlignment.TopCenter)
                {
                    imgPos = new Point(imgCenterX, 0);
                    txtPos = new Point(txtCenterX, 0);
                }
                else if (this.alignment == ContentAlignment.TopLeft)
                {
                    imgPos = new Point(0, 0);
                    txtPos = new Point(0, 0);
                }
                else if (this.alignment == ContentAlignment.TopRight)
                {
                    imgPos = new Point(imgRightX, 0);
                    txtPos = new Point(txtRightX, 0);
                }
            }
            #endregion
            #region 图片在文字上面
            else if (this.textImageRelation == ImageTextRelation.TextAboveImage)
            {
                var height = imgHeight + space + txtHeight;
                var imgCenterX = (this.Width - imgWidth) / 2;
                var imgRightX = this.Width - imgWidth;
                var txtCenterX = (this.Width - txtWidth) / 2;
                var txtRightX = this.Width - txtWidth;
                contentSize = new Size(txtWidth > imgWidth ? txtWidth : imgWidth, height);

                if (this.alignment == ContentAlignment.BottomCenter)
                {
                    txtPos = new Point(txtCenterX, this.Height - height);
                    imgPos = new Point(imgCenterX, txtPos.Y + txtHeight + space);
                }
                else if (this.alignment == ContentAlignment.BottomLeft)
                {
                    txtPos = new Point(0, this.Height - height);
                    imgPos = new Point(0, txtPos.Y + txtHeight + space);
                }
                else if (this.alignment == ContentAlignment.BottomRight)
                {
                    txtPos = new Point(txtRightX, this.Height - height);
                    imgPos = new Point(imgRightX, txtPos.Y + txtHeight + space);
                }
                else if (this.alignment == ContentAlignment.MiddleCenter)
                {
                    txtPos = new Point(txtCenterX, (this.Height - height) / 2);
                    imgPos = new Point(imgCenterX, txtPos.Y + txtHeight + space);
                }
                else if (this.alignment == ContentAlignment.MiddleLeft)
                {
                    txtPos = new Point(0, (this.Height - height) / 2);
                    imgPos = new Point(0, txtPos.Y + txtHeight + space);
                }
                else if (this.alignment == ContentAlignment.MiddleRight)
                {
                    txtPos = new Point(txtRightX, (this.Height - height) / 2);
                    imgPos = new Point(imgRightX, txtPos.Y + txtHeight + space);
                }
                else if (this.alignment == ContentAlignment.TopCenter)
                {
                    txtPos = new Point(txtCenterX, 0);
                    imgPos = new Point(imgCenterX, txtPos.Y + txtHeight + space);
                }
                else if (this.alignment == ContentAlignment.TopLeft)
                {
                    txtPos = new Point(0, 0);
                    imgPos = new Point(0, txtPos.Y + txtHeight + space);
                }
                else if (this.alignment == ContentAlignment.TopRight)
                {
                    txtPos = new Point(txtRightX, 0);
                    imgPos = new Point(imgRightX, txtPos.Y + txtHeight + space);
                }
            }
            #endregion
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            var g = pevent.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var textSize = Size.Ceiling(g.MeasureString(this.Text, this.Font));
            var imgPos = Point.Empty;
            var txtPos = Point.Empty;
            var contentSize = Size.Empty;
            Calc(textSize, out imgPos, out txtPos, out contentSize);
            if (this.image != null)
            {
                g.DrawImage(this.image, imgPos.X, imgPos.Y, imageSize.Width, imageSize.Height);
            }

            if (this.AutoSize)
            {
                var width = this.Width;
                var height = this.Height;
                var auto = false;
                // 增大
                if (contentSize.Width > (width - this.borderWidth * 2))
                {
                    width = contentSize.Width + borderWidth * 2 + 6;//两边留出3个像素点
                    auto = true;
                }

                if (contentSize.Height > (height - this.borderWidth * 2))
                {
                    height = contentSize.Height + borderWidth * 2 + 6;//两边留出3个像素点
                    auto = true;
                }

                if (auto)
                {
                    this.Size = new Size(width, height);
                }
            }

            using (var brush = new SolidBrush(this.ForeColor))
            {
                pevent.Graphics.DrawString(this.Text, this.Font, brush, txtPos.X, txtPos.Y);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // 这里调用父类，主要用来处理透明色（圆角之后的问题）
            base.OnPaintBackground(pevent);
            if (circleButton) { this.Height = this.Width; this.radius = this.Height / 2; this.radiusMode = Forms.RadiusMode.All; }

            Color c1 = Color.Empty;

            if (this.status == ButtonStatus.Default)
            {
                c1 = this.BorderColor;
            }
            else if (this.status == ButtonStatus.Hover)
            {
                c1 = this.BorderHoverColor;
            }
            else if (this.status == ButtonStatus.Press)
            {
                c1 = this.BorderPressColor;
            }                       

            if (this.status == ButtonStatus.Default || this.HoverBackColor == null || this.HoverBackColor.FromColor.IsEmpty)
            {
                RadiusDrawable.DrawRadius(pevent.Graphics, this.ClientRectangle, RadiusMode, this.Radius, this.BackgroundColor.FromColor, this.BackgroundColor.ToColor, this.BackgroundColor.GradientMode, c1, this.borderWidth);
            }
            else
            {
                RadiusDrawable.DrawRadius(pevent.Graphics, this.ClientRectangle, RadiusMode, this.Radius, this.HoverBackColor.FromColor, this.HoverBackColor.ToColor, this.HoverBackColor.GradientMode, c1, this.borderWidth);
            }
        }
        #endregion

        #region 鼠标以及状态变更
        protected override void OnMouseEnter(EventArgs e)
        {
            this.SetStatus(ButtonStatus.Hover);
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.SetStatus(ButtonStatus.Default);
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (mevent.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.SetStatus(ButtonStatus.Press);
            }

            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (mevent.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.SetStatus(ButtonStatus.Default);
            }

            base.OnMouseUp(mevent);
        }
        #endregion

        private void SetStatus(ButtonStatus status)
        {
            this.status = status;
            this.Invalidate();
        }

        public void PerformClick()
        {
            if (base.CanSelect)
            {
                this.SetStatus(ButtonStatus.Default);
                this.OnClick(EventArgs.Empty);
                
            }
        }

        public void NotifyDefault(bool value)
        {
            //return value;
        }
    }
}
