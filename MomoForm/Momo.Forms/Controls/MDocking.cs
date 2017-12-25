using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Momo.Forms.Controls
{
    /// <summary>
    /// 停靠面板
    /// </summary>
    public class MDocking : Control
    {
        public MDocking()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);//调整大小时重绘
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);// 双缓冲
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);// 禁止擦除背景.
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.dockLocation = DockLocation.Left;
            this.IsDocking = false;
            this.dockColor = new GradientColor(GradientMode.None, Color.Transparent, Color.Transparent);
            this.dockHoverColor = new GradientColor(GradientMode.Horizontal, Color.FromArgb(189, 195, 199), Color.FromArgb(189, 195, 199));
            this.dockButtonBorderColor = Color.FromArgb(189, 195, 199);
            this.dockButtonBorderHoverColor = Color.FromArgb(127, 140, 141);
        }

        private DockLocation dockLocation;

        /// <summary>
        /// 当前是否已经处于停靠状态，true处于停靠（隐藏）
        /// </summary>
        public bool IsDocking { get; private set; }

        private ButtonStatus status = ButtonStatus.Default;

        private int borderWidth = 1;
        [Browsable(true), Category("Momo"), Description("边框大小")]
        public int BorderWidth { get { return borderWidth; } set { borderWidth = value; this.Invalidate(); } }

        private Control dockContent;
        [Browsable(true), Category("Momo"), Description("停靠方向")]
        public Control DockContent
        {
            get { return this.dockContent; }
            set
            {
                if (value == this.Parent && value != null)
                {
                    MessageBox.Show("停靠容器不能是当前停靠组件的父容器。");
                    return;
                }

                if (this.DockContent != null)
                {
                    this.DockContent.LocationChanged -= DockContent_LocationChanged;
                    this.DockContent.SizeChanged -= DockContent_SizeChanged;
                    this.DockContent.VisibleChanged -= DockContent_VisibleChanged;
                }

                this.dockContent = value;
                this.IsDocking = false;
                if (this.DockContent.Dock != DockStyle.None)
                {
                    var size = this.DockContent.Size;
                    var location = this.DockContent.Location;
                    this.DockContent.Dock = DockStyle.None;
                    this.DockContent.Location = location;
                    this.DockContent.Size = size;
                }

                this.DockContent.LocationChanged += DockContent_LocationChanged;
                this.DockContent.SizeChanged += DockContent_SizeChanged;
                this.DockContent.VisibleChanged += DockContent_VisibleChanged;
                this.Invalidate();
            }
        }

        private void DockContent_VisibleChanged(object sender, EventArgs e)
        {
            this.Visible = this.DockContent.Visible;
        }

        private void DockContent_SizeChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void DockContent_LocationChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void SetStatus(ButtonStatus status)
        {
            this.status = status;
            this.Invalidate();
        }

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

        /// <summary>
        /// 停靠方向
        /// </summary>
        [Browsable(true), Category("Momo"), Description("停靠方向")]
        public DockLocation DockLocation { get { return this.dockLocation; } set { this.dockLocation = value; this.Invalidate(); } }

        public GradientColor dockColor;
        /// <summary>
        /// 停靠按钮的渐变颜色
        /// </summary>
        [Browsable(true), Category("Momo"), Description("停靠按钮的渐变颜色")]
        public GradientColor DockColor { get { return this.dockColor; } set { this.dockColor = value; this.Invalidate(); } }

        public GradientColor dockHoverColor;
        /// <summary>
        /// 停靠按钮的悬浮渐变颜色
        /// </summary>
        [Browsable(true), Category("Momo"), Description("停靠按钮的悬浮渐变颜色")]
        public GradientColor DockHoverColor { get { return this.dockHoverColor; } set { this.dockHoverColor = value; this.Invalidate(); } }

        public Color dockButtonBorderColor;
        /// <summary>
        /// 停靠按钮的边框颜色
        /// </summary>
        [Browsable(true), Category("Momo"), Description("停靠按钮的边框颜色")]
        public Color DockBorderColor { get { return this.dockButtonBorderColor; } set { this.dockButtonBorderColor = value; this.Invalidate(); } }

        public Color dockButtonBorderHoverColor;
        /// <summary>
        /// 停靠按钮的悬浮边框颜色
        /// </summary>
        [Browsable(true), Category("Momo"), Description("停靠按钮的悬浮边框颜色")]
        public Color DockBorderHoverColor { get { return this.dockButtonBorderHoverColor; } set { this.dockButtonBorderHoverColor = value; this.Invalidate(); } }

        private RadiusMode radiusMode = RadiusMode.None;
        [Browsable(true), Category("Momo"), Description("圆角模式")]
        public RadiusMode RadiusMode { get { return radiusMode; } set { radiusMode = value; this.Invalidate(); } }

        private int radius = 0;
        [Browsable(true), Category("Momo"), Description("圆角大小")]
        public int Radius { get { return radius; } set { radius = value; this.Invalidate(); } }

        private bool isCircle = false;
        [Browsable(true), Category("Momo"), Description("是否圆形"), DefaultValue(typeof(bool), "false")]
        public bool IsCircle { get { return isCircle; } set { isCircle = value; this.Invalidate(); } }

        private bool useDefaultImage = true;
        [Browsable(true), Category("Momo"), Description("是否使用默认图片"), DefaultValue(typeof(bool), "true")]
        public bool UseDefaultImage { get { return useDefaultImage; } set { useDefaultImage = value; this.Invalidate(); } }

        private Image image;
        [Browsable(true), Category("Momo"), Description("停靠前的图片")]
        public Image Image { get { return image; } set { this.image = value; this.Invalidate(); } }

        private Image dockedImage;
        [Browsable(true), Category("Momo"), Description("停靠后的图片")]
        public Image DockedImage { get { return dockedImage; } set { this.dockedImage = value; this.Invalidate(); } }

        private Size imageSize = new Size(16, 16);
        [Browsable(true), Category("Momo"), Description("图片大小")]
        public Size ImageSize { get { return imageSize; } set { this.imageSize = value; this.Invalidate(); } }
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // 这里调用父类，主要用来处理透明色（圆角之后的问题）
            base.OnPaintBackground(pevent);
            if (isCircle) { this.Height = this.Width; this.radius = this.Height / 2; this.radiusMode = Forms.RadiusMode.All; }

            Color c1 = Color.Empty;

            if (this.status == ButtonStatus.Default)
            {
                c1 = this.DockBorderColor;
            }
            else if (this.status == ButtonStatus.Hover)
            {
                c1 = this.DockBorderHoverColor;
            }
            else if (this.status == ButtonStatus.Press)
            {
                c1 = this.DockBorderHoverColor;
            }

            if (this.status == ButtonStatus.Default || this.DockHoverColor == null || this.DockHoverColor.FromColor.IsEmpty)
            {
                RadiusDrawable.DrawRadius(pevent.Graphics, this.ClientRectangle, RadiusMode, this.Radius, this.DockColor.FromColor, this.DockColor.ToColor, this.DockColor.GradientMode, c1, this.borderWidth);
            }
            else
            {
                RadiusDrawable.DrawRadius(pevent.Graphics, this.ClientRectangle, RadiusMode, this.Radius, this.DockHoverColor.FromColor, this.DockHoverColor.ToColor, this.DockHoverColor.GradientMode, c1, this.borderWidth);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.dockContent == null)
            {
                base.OnPaint(e);
                return;
            }

            var loction = Point.Empty;
            Image image = null;
            // 没有隐藏
            if (!this.IsDocking)
            {
                switch (this.dockLocation)
                {
                    case DockLocation.Bottom:
                        loction = new Point((this.DockContent.Width - this.Width) / 2 + this.DockContent.Left, this.DockContent.Top - this.Height / 2);
                        image = Properties.Resources.down_dock;
                        break;
                    case DockLocation.Left:
                        loction = new Point(this.DockContent.Width + this.DockContent.Left - this.Width / 2, this.DockContent.Top + (this.DockContent.Height - this.Height) / 2);
                        image = Properties.Resources.left_dock;
                        break;
                    case DockLocation.Top:
                        loction = new Point((this.DockContent.Width - this.Width) / 2 + this.DockContent.Left, this.DockContent.Top + this.DockContent.Height - this.Height / 2);
                        image = Properties.Resources.up_dock;
                        break;
                    case DockLocation.Right:
                        loction = new Point(this.DockContent.Left - this.Width / 2, this.DockContent.Top + (this.DockContent.Height - this.Height) / 2);
                        image = Properties.Resources.right_dock;
                        break;
                }

                if (!useDefaultImage && Image != null)
                {
                    image = Image;
                }
            }
            else
            {
                switch (this.dockLocation)
                {
                    case DockLocation.Bottom:
                        loction = new Point((this.DockContent.Width - this.Width) / 2 + this.DockContent.Left, this.DockContent.Top - this.Height);
                        image = Properties.Resources.up_dock;
                        break;
                    case DockLocation.Left:
                        loction = new Point(this.DockContent.Width + this.DockContent.Left, this.DockContent.Top + (this.DockContent.Height - this.Height) / 2);
                        image = Properties.Resources.right_dock;
                        break;
                    case DockLocation.Top:
                        loction = new Point((this.DockContent.Width - this.Width) / 2 + this.DockContent.Left, this.DockContent.Top + this.DockContent.Height);
                        image = Properties.Resources.down_dock;
                        break;
                    case DockLocation.Right:
                        loction = new Point(this.DockContent.Left - this.Width, this.DockContent.Top + (this.DockContent.Height - this.Height) / 2);
                        image = Properties.Resources.left_dock;
                        break;
                }

                if (!useDefaultImage && DockedImage != null)
                {
                    image = DockedImage;
                }
            }

            this.Location = loction;
            e.Graphics.DrawImage(image, (this.Width - imageSize.Width) / 2, (this.Height - imageSize.Height) / 2, imageSize.Width, imageSize.Height);

        }

        protected override void OnClick(EventArgs e)
        {
            //base.OnClick(e);
            if (!this.IsDocking)
            {
                this.IsDocking = true;
                switch (this.dockLocation)
                {
                    case DockLocation.Bottom:
                        this.DockContent.Location = new Point(this.DockContent.Left, this.DockContent.Top + this.DockContent.Height);
                        break;
                    case DockLocation.Top:
                        this.DockContent.Location = new Point(this.DockContent.Left, this.DockContent.Top - this.DockContent.Height);
                        break;
                    case DockLocation.Left:
                        this.DockContent.Location = new Point(this.DockContent.Left - this.DockContent.Width, this.DockContent.Top);
                        break;
                    case DockLocation.Right:
                        this.DockContent.Location = new Point(this.DockContent.Left + this.DockContent.Width, this.DockContent.Top);
                        break;
                }
            }
            else
            {
                this.IsDocking = false;
                switch (this.dockLocation)
                {
                    case DockLocation.Bottom:
                        this.DockContent.Location = new Point(this.DockContent.Left, this.DockContent.Top - this.DockContent.Height);
                        break;
                    case DockLocation.Top:
                        this.DockContent.Location = new Point(this.DockContent.Left, this.DockContent.Top + this.DockContent.Height);
                        break;
                    case DockLocation.Left:
                        this.DockContent.Location = new Point(this.DockContent.Left + this.DockContent.Width, this.DockContent.Top);
                        break;
                    case DockLocation.Right:
                        this.DockContent.Location = new Point(this.DockContent.Left - this.DockContent.Width, this.DockContent.Top);
                        break;
                }
            }
        }
    }
}
