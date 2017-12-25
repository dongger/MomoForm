using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

using System.Text;
using System.Windows.Forms;

namespace Momo.Forms
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class DefaultCaptionPalette : Palette
    {
        public DefaultCaptionPalette(Control container) : base(container)
        {
            this.BackColor = new GradientColor(GradientMode.None, Color.Black, Color.White);
            this.BackImage = null;
            this.MaximizeBox = true;
            this.CenterCaption = false;
            this.CenterTitle = false;
            this.ControlBox = true;
            this.ControlBoxSize = new Size(24, 24);
            this.Font = new Font("微软雅黑", 12);
            this.ForeColor = Color.Black;
            this.HelpButton = false;
            this.Location = new Point(0, 0);
            this.Logo = null;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Padding = new Padding(4, 4, 4, 4);
            this.logoVisible = true;
            this.Rectangle = new Rectangle(new Point(0, 0), new Size(this.Container.Width, 40));
            this.Text = "Momo Form";
            this.backColor = new GradientColor(GradientMode.Vertical, Color.FromArgb(52, 152, 219), Color.FromArgb(41, 128, 185));
            this.ControlActivedColor = new GradientColor() { FromColor = Color.FromArgb(26, 188, 156), GradientMode = GradientMode.None };
            this.ControlBackColor = new GradientColor() { FromColor = Color.Transparent, GradientMode = GradientMode.None };
            this.fullButtonBackColor = new GradientColor() { FromColor = Color.FromArgb(50, 57, 198, 131), GradientMode = GradientMode.None };

            this.MaximizeBoxImage = Properties.Resources.window_max;
            this.NormalBoxImage = Properties.Resources.window_normal;
            this.MinimizeBoxImage = Properties.Resources.window_min;
            this.HelpButtonImage = Properties.Resources.window_help;
            this.CloseButtonImage = Properties.Resources.window_close;
            this.controlBoxies = new CaptionControlButtonCollection<CaptionControlButton>();
            this.fullButtons = new CaptionControlButtonCollection<CaptionFullButton>();
        }

        internal event Action<CaptionControlButton, MouseEventArgs> CaptionControlClick;

        private CaptionControlButtonCollection<CaptionControlButton> controlBoxies;
        [Browsable(true)]
        [Localizable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof(CaptionControlButtonEditor<CaptionControlButton>), typeof(UITypeEditor))]
        public CaptionControlButtonCollection<CaptionControlButton> ControlBoxies
        {
            get { return this.controlBoxies; }
            set
            {
                this.controlBoxies = value;
                this.Invalidate();
            }
        }

        public void SetNotify(string tag, string notify)
        {
            var n = this.controlBoxies.Find((o) => { return o.Tag == tag; });
            if (n != null)
            {
                n.Notify = new NotifyPalette()
                {
                    ParentPalette = this,
                    Text = notify
                };
            }
        }

        internal event Action<CaptionFullButton, MouseEventArgs> CaptionFullButtonClick;

        private CaptionControlButtonCollection<CaptionFullButton> fullButtons;
        [Browsable(true)]
        [Localizable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof(CaptionControlButtonEditor<CaptionFullButton>), typeof(UITypeEditor))]
        public CaptionControlButtonCollection<CaptionFullButton> FullButtons
        {
            get { return this.fullButtons; }
            set
            {
                this.fullButtons = value;
                if (value != null && value.Count > 0)
                {
                    this.centerCaption = false;
                    this.centerTitle = false;
                }

                this.Invalidate();
            }
        }

        private void ControlBoxItem_Click(object sender, EventArgs e)
        {
            CaptionControlClick?.Invoke(sender as CaptionControlButton, e as MouseEventArgs);
        }

        private void FullButtonItem_Click(object sender, EventArgs e)
        {
            CaptionFullButtonClick?.Invoke(sender as CaptionFullButton, e as MouseEventArgs);
        }

        internal event EventHandler HelpButtonClick;

        public override Rectangle Rectangle
        {
            get
            {
                return base.Rectangle;
            }

            set
            {
                base.Rectangle = value;
                this.Container.Padding = new Padding(this.Container.Padding.Left, this.Height, this.Container.Padding.Right, this.Container.Padding.Bottom);
            }
        }

        private GradientColor backColor;

        /// <summary>
        /// 背景色
        /// </summary>
        [Description("背景色与渐变处理")]
        public GradientColor BackColor { get { return this.backColor; } set { this.backColor = value; this.Invalidate(); } }

        private Image backImage;
        /// <summary>
        /// 背景图片
        /// </summary>
        [Description("背景图片，如果设置了此项，则BackColor属性无效")]
        public Image BackImage { get { return this.backImage; } set { this.backImage = value; this.Invalidate(); } }

        private Image maximizeBoxImage;
        /// <summary>
        /// 最大化按钮图片
        /// </summary>
        [Description("最大化按钮图片")]
        public Image MaximizeBoxImage { get { return this.maximizeBoxImage; } set { this.maximizeBoxImage = value; this.Invalidate(); } }

        private Image normalBoxImage;
        /// <summary>
        /// 默认大小按钮图片
        /// </summary>
        [Description("默认大小按钮图片")]
        public Image NormalBoxImage { get { return this.normalBoxImage; } set { this.normalBoxImage = value; this.Invalidate(); } }

        private Image minimizeBoxImage;
        /// <summary>
        /// 最小化按钮图片
        /// </summary>
        [Description("最小化按钮图片")]
        public Image MinimizeBoxImage { get { return this.minimizeBoxImage; } set { this.minimizeBoxImage = value; this.Invalidate(); } }

        private Image helpButtonImage;
        /// <summary>
        /// 帮助按钮图片
        /// </summary>
        [Description("帮助按钮图片")]
        public Image HelpButtonImage { get { return this.helpButtonImage; } set { this.helpButtonImage = value; this.Invalidate(); } }

        private Image closeButtonImage;
        /// <summary>
        /// 关闭按钮图片
        /// </summary>
        [Description("关闭按钮图片")]
        public Image CloseButtonImage { get { return this.closeButtonImage; } set { this.closeButtonImage = value; this.Invalidate(); } }

        private bool maximizeBox;
        /// <summary>
        /// 是否显示最大化按钮
        /// </summary>
        [Description("是否显示最大化/还原按钮")]
        public bool MaximizeBox { get { return this.maximizeBox; } set { this.maximizeBox = value; this.Invalidate(); } }

        private bool minimizeBox;
        /// <summary>
        /// 是否显示最小化按钮
        /// </summary>
        [Description("是否显示最小化按钮")]
        public bool MinimizeBox { get { return this.minimizeBox; } set { this.minimizeBox = value; this.Invalidate(); } }

        private bool controlBox;
        /// <summary>
        /// 是否显示控制按钮
        /// </summary>
        [Description("是否显示控制栏按钮")]
        public bool ControlBox { get { return this.controlBox; } set { this.controlBox = value; this.Invalidate(); } }

        private Size controlBoxSize;
        /// <summary>
        /// 标题栏按钮大小
        /// </summary>
        [Description("控制按钮大小")]
        public Size ControlBoxSize { get { return this.controlBoxSize; } set { this.controlBoxSize = value; this.Invalidate(); } }

        private Font font;
        /// <summary>
        /// 标题栏字体
        /// </summary>
        [Description("标题字体")]
        public Font Font { get { return this.font; } set { this.font = value; this.Invalidate(); } }

        private string text;
        /// <summary>
        /// 标题
        /// </summary>
        [Description("标题")]
        public string Text { get { return this.text; } set { this.text = value; this.Container.Text = value; this.Invalidate(); } }

        private Color foreColor;
        /// <summary>
        /// 文字颜色
        /// </summary>
        [Description("字体颜色")]
        public Color ForeColor { get { return this.foreColor; } set { this.foreColor = value; this.Invalidate(); } }

        private Image logo;
        /// <summary>
        /// Logo
        /// </summary>
        [Description("Logo图片")]
        public Image Logo { get { return this.logo; } set { this.logo = value; this.Invalidate(); } }

        private Size logoSize;
        /// <summary>
        /// Logo图片大小
        /// </summary>
        [Description("Logo图片大小")]
        public Size LogoSize { get { return this.logoSize; } set { this.logoSize = value; this.Invalidate(); } }

        private bool centerTitle;
        /// <summary>
        /// 标题文本居中
        /// </summary>
        [Description("标题文本居中")]
        public bool CenterTitle { get { return this.centerTitle; } set { this.centerTitle = value; this.Invalidate(); } }

        private bool centerCaption;
        /// <summary>
        /// 标题文字和Logo一起居中
        /// </summary>
        [Description("标题文字和Logo一起居中")]
        public bool CenterCaption { get { return this.centerCaption; } set { this.centerCaption = value; this.Invalidate(); } }
        private GradientColor fullButtonBackColor;

        /// <summary>
        /// 背景色
        /// </summary>
        [Description("背景色与渐变处理")]
        public GradientColor FullButtonBackColor { get { return this.fullButtonBackColor; } set { this.fullButtonBackColor = value; this.Invalidate(); } }

        private bool centerControlBox;
        /// <summary>
        /// 控制按钮是否垂直居中
        /// </summary>
        [Description("控制按钮是否垂直居中")]
        public bool CenterControlBox { get { return this.centerControlBox; } set { this.centerControlBox = value; this.Invalidate(); } }

        private bool helpButton;
        /// <summary>
        /// 是否显示帮助按钮
        /// </summary>
        [Description("是否显示帮助按钮")]
        public bool HelpButton { get { return this.helpButton; } set { this.helpButton = value; this.Invalidate(); } }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Rectangle CloseRect { get; private set; }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Rectangle MaxRect { get; private set; }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Rectangle MinRect { get; private set; }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool HelpHover { get; internal set; }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool MinHover { get; internal set; }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool CloseHover { get; internal set; }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool MaxHover { get; internal set; }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Rectangle HelpRect { get; private set; }

        [Description("用于标题栏按钮的图片列表")]
        public ImageList ImageList { get; set; }

        private bool logoVisible;
        /// <summary>
        /// 标题文本居中
        /// </summary>
        [Description("Logo可见性")]
        public bool LogoVisible { get { return this.logoVisible; } set { this.logoVisible = value; this.Invalidate(); } }

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

        private FormWindowState windowState;
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public FormWindowState WindowState { get { return this.windowState; } set { this.windowState = value; this.Invalidate(); } }

        private void DrawTitleAndLogo(Graphics graphics)
        {
            var x = this.Padding.Left;

            if (this.LogoVisible && this.Logo != null)
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                ImageAttributes imgAtt = new ImageAttributes();
                imgAtt.SetWrapMode(WrapMode.TileFlipXY);
                if (CenterCaption)
                {
                    var fontSize = Size.Ceiling(graphics.MeasureString(this.Text, this.Font));
                    x = (this.Width - fontSize.Width - this.Padding.Left - this.Padding.Right - this.logoSize.Width - 6) / 2 + this.Padding.Left;
                }

                var rec = new Rectangle(x, (this.Height - this.LogoSize.Height - this.Padding.Top - this.Padding.Bottom) / 2 + this.Padding.Top, this.logoSize.Width, this.logoSize.Height);
                graphics.DrawImage(this.Logo, rec, 0, 0, this.Logo.Width, this.Logo.Height, GraphicsUnit.Pixel, imgAtt);
                x += logoSize.Width + 6;
            }

            if (!string.IsNullOrEmpty(this.text))
            {
                var fontSize = Size.Ceiling(graphics.MeasureString(this.Text, this.Font));

                if ((!CenterCaption && this.CenterTitle) || !this.LogoVisible)
                {
                    x = (this.Width - fontSize.Width) / 2;
                }

                using (var brush = new SolidBrush(this.ForeColor))
                {
                    graphics.DrawString(this.Text, font, brush, x, (this.Height - fontSize.Height - this.Padding.Top - this.Padding.Bottom) / 2 + this.Padding.Top);
                }

                x += fontSize.Width + 12;
            }

            #region 填充按钮
            if (this.FullButtons != null && this.ImageList != null && this.ImageList.Images.Count > 0)
            {
                foreach (var item in this.FullButtons)
                {
                    item.Click -= FullButtonItem_Click;
                    item.Click += FullButtonItem_Click;
                    item.ControlActivedColor = this.ControlActivedColor;
                    item.ControlBackColor = this.fullButtonBackColor;

                    item.Rectangle = new Rectangle(x, 0, item.Width, this.Height);
                    item.Image = this.ImageList.Images[item.ImageIndex];

                    item.Draw(graphics);
                    x += item.Width;
                }
            }
            #endregion
        }

        private void DrawBackground(Graphics graphics)
        {
            if (this.BackImage != null)
            {
                graphics.DrawImage(this.BackImage, this.Rectangle, new Rectangle(0, 0, this.BackImage.Width, this.BackImage.Height), GraphicsUnit.Point);
                return;
            }

            if (this.BackColor.GradientMode == GradientMode.None || this.BackColor.ToColor.IsEmpty)
            {
                using (var brush = new SolidBrush(this.BackColor.FromColor))
                {
                    graphics.FillRectangle(brush, this.Rectangle);
                }
            }
            else
            {
                using (var b = new LinearGradientBrush(this.Rectangle, this.BackColor.FromColor, this.BackColor.ToColor, (LinearGradientMode)this.BackColor.GradientMode))
                {
                    graphics.FillRectangle(b, this.Rectangle);
                }
            }
        }

        private void DrawControlBox(Graphics graphics)
        {
            var count = 0;
            #region 按钮数量
            if (this.ControlBox)
            {
                count = 1;
                if (this.MaximizeBox)
                {
                    count++;
                }

                if (this.MinimizeBox)
                {
                    count++;
                }

                if (this.HelpButton)
                {
                    count++;
                }
            }
            else
            {
                return;
            }
            #endregion
            if (this.ControlBoxies != null)
            {
                count += this.controlBoxies.Count;
            }

            if (count == 0)
            {
                return;
            }

            var x = this.Width - this.Padding.Right - this.controlBoxSize.Width;
            var y = this.Padding.Top;//(this.Height - this.Padding.Top - this.Padding.Bottom - this.controlBoxSize.Height) / 2 + this.Padding.Top;
            if (this.centerControlBox)
            {
                y = (this.Height - y - this.ControlBoxSize.Height) / 2;
            }
            ImageAttributes ImgAtt = new ImageAttributes();
            ImgAtt.SetWrapMode(System.Drawing.Drawing2D.WrapMode.Clamp);

            if (this.CloseButtonImage != null)
            {
                CloseRect = new Rectangle(x, y, this.controlBoxSize.Width, this.controlBoxSize.Height);
                var color = CloseHover ? this.ControlActivedColor : this.ControlBackColor;
                RadiusDrawable.DrawRadius(graphics, CloseRect, RadiusMode.None, 0, color.FromColor, color.ToColor, color.GradientMode, Color.Empty, 0);

                graphics.DrawImage(this.CloseButtonImage, CloseRect, 0, 0, this.CloseButtonImage.Width, this.CloseButtonImage.Height, GraphicsUnit.Pixel, ImgAtt);
                x -= this.ControlBoxSize.Width;
            }

            if (this.MaximizeBox && this.WindowState == FormWindowState.Maximized && this.NormalBoxImage != null)
            {
                MaxRect = new Rectangle(x, y, this.controlBoxSize.Width, this.controlBoxSize.Height);

                var color = MaxHover ? this.ControlActivedColor : this.ControlBackColor;
                RadiusDrawable.DrawRadius(graphics, MaxRect, RadiusMode.None, 0, color.FromColor, color.ToColor, color.GradientMode, Color.Empty, 0);

                graphics.DrawImage(this.NormalBoxImage, MaxRect, 0, 0, this.NormalBoxImage.Width, this.NormalBoxImage.Height, GraphicsUnit.Pixel, ImgAtt);
                x -= this.ControlBoxSize.Width;
            }
            else if (this.MaximizeBox && this.WindowState != FormWindowState.Maximized && this.MaximizeBoxImage != null)
            {
                MaxRect = new Rectangle(x, y, this.controlBoxSize.Width, this.controlBoxSize.Height);
                var color = MaxHover ? this.ControlActivedColor : this.ControlBackColor;
                RadiusDrawable.DrawRadius(graphics, MaxRect, RadiusMode.None, 0, color.FromColor, color.ToColor, color.GradientMode, Color.Empty, 0);

                graphics.DrawImage(this.MaximizeBoxImage, MaxRect, 0, 0, this.MaximizeBoxImage.Width, this.MaximizeBoxImage.Height, GraphicsUnit.Pixel, ImgAtt);
                x -= this.ControlBoxSize.Width;
            }

            if (this.MinimizeBox && this.MinimizeBoxImage != null)
            {
                MinRect = new Rectangle(x, y, this.controlBoxSize.Width, this.controlBoxSize.Height);
                var color = MinHover ? this.ControlActivedColor : this.ControlBackColor;
                RadiusDrawable.DrawRadius(graphics, MinRect, RadiusMode.None, 0, color.FromColor, color.ToColor, color.GradientMode, Color.Empty, 0);
                graphics.DrawImage(this.MinimizeBoxImage, MinRect, 0, 0, this.MinimizeBoxImage.Width, this.MinimizeBoxImage.Height, GraphicsUnit.Pixel, ImgAtt);
                x -= this.ControlBoxSize.Width;
            }

            #region 帮助按钮
            if (this.HelpButton && this.HelpButtonImage != null)
            {
                HelpRect = new Rectangle(x, y, this.controlBoxSize.Width, this.controlBoxSize.Height);
                var color = HelpHover ? this.ControlActivedColor : this.ControlBackColor;
                RadiusDrawable.DrawRadius(graphics, HelpRect, RadiusMode.None, 0, color.FromColor, color.ToColor, color.GradientMode, Color.Empty, 0);
                graphics.DrawImage(this.HelpButtonImage, HelpRect, 0, 0, this.HelpButtonImage.Width, this.HelpButtonImage.Height, GraphicsUnit.Pixel, ImgAtt);
                x -= this.ControlBoxSize.Width;
            }
            #endregion

            #region 其他按钮
            if (this.ControlBoxies != null && this.ImageList != null && this.ImageList.Images.Count > 0)
            {
                foreach (var item in this.ControlBoxies)
                {
                    item.Click -= ControlBoxItem_Click;
                    item.Click += ControlBoxItem_Click;
                    item.ControlActivedColor = this.ControlActivedColor;
                    item.ControlBackColor = this.ControlBackColor;

                    item.Rectangle = new Rectangle(x, y, this.controlBoxSize.Width, this.controlBoxSize.Height);
                    item.Image = this.ImageList.Images[item.ImageIndex];

                    item.Draw(graphics);
                    x -= this.ControlBoxSize.Width;
                }
            }
            #endregion
        }

        public override void Draw(Graphics graphics)
        {
            if (this.Visible)
            {
                this.DrawBackground(graphics);
                this.DrawTitleAndLogo(graphics);
                this.DrawControlBox(graphics);
            }
        }

        protected override void MouseClick(MouseEventArgs e)
        {
            if (this.CloseRect != Rectangle.Empty && this.CloseRect.Contains(e.Location))
            {
                (this.Container as Form).Close();
                return;
            }

            if (!this.MaxRect.IsEmpty && this.MaxRect.Contains(e.Location))
            {
                this.MaxHover = false;
                var form = this.Container as MForm;
                if (form.WindowState == FormWindowState.Maximized)
                {
                    form.WindowState = FormWindowState.Normal;
                }
                else
                {
                    form.WindowState = FormWindowState.Maximized;
                }

                return;
            }

            if (!this.MinRect.IsEmpty && this.MinRect.Contains(e.Location))
            {
                (this.Container as Form).WindowState = FormWindowState.Minimized;
                return;
            }

            if (!this.HelpRect.IsEmpty && this.HelpRect.Contains(e.Location))
            {
                this.HelpHover = false;
                this.Invalidate();
                this.HelpButtonClick?.Invoke(this, e);
                return;
            }

            if (this.ControlBoxies != null && this.ImageList != null && this.ImageList.Images.Count > 0)
            {
                foreach (var item in this.ControlBoxies)
                {
                    item.OnMouseClick(e);
                }
            }

            #region 填充按钮
            if (this.FullButtons != null && this.ImageList != null && this.ImageList.Images.Count > 0)
            {
                foreach (var item in this.FullButtons)
                {
                    item.OnMouseClick(e);
                }
            }
            #endregion
        }

        protected override void MouseMoveIn(MouseEventArgs e)
        {
            this.CloseHover = this.CloseRect != Rectangle.Empty && this.CloseRect.Contains(e.Location);
            this.MinHover = this.MinRect != Rectangle.Empty && this.MinRect.Contains(e.Location);
            this.MaxHover = this.MaxRect != Rectangle.Empty && this.MaxRect.Contains(e.Location);
            this.HelpHover = this.HelpRect != Rectangle.Empty && this.HelpRect.Contains(e.Location);
            bool temp = false;
            if (this.ControlBoxies != null && this.ImageList != null && this.ImageList.Images.Count > 0)
            {
                foreach (var item in this.ControlBoxies)
                {
                    item.Hover = false;
                    if (item.Rectangle.Contains(e.Location))
                    {
                        item.Hover = true;
                        temp = true;
                    }
                }
            }

            var temp1 = false;
            #region 填充按钮
            if (this.FullButtons != null && this.ImageList != null && this.ImageList.Images.Count > 0)
            {
                foreach (var item in this.FullButtons)
                {
                    item.Hover = false;
                    if (item.Rectangle.Contains(e.Location))
                    {
                        item.Hover = true;
                        temp1 = true;
                    }
                }
            }
            #endregion

            this.Invalidate();

            this.Container.Cursor = (this.CloseHover || this.MinHover || this.MaxHover || this.HelpHover || temp || temp1) ? Cursors.Hand : Cursors.Default;
        }

        protected override void MouseMoveOut(MouseEventArgs e)
        {
            if (this.CloseHover || this.MinHover || this.MaxHover || this.HelpHover)
            {
                this.CloseHover = this.MinHover = this.MaxHover = this.HelpHover = false;
                this.Invalidate();
            }

            if (this.ControlBoxies != null && this.ImageList != null && this.ImageList.Images.Count > 0)
            {
                foreach (var item in this.ControlBoxies)
                {
                    item.Hover = false;
                }
            }

            #region 填充按钮
            if (this.FullButtons != null && this.ImageList != null && this.ImageList.Images.Count > 0)
            {
                foreach (var item in this.FullButtons)
                {
                    item.Hover = false;
                }
            }
            #endregion
            this.Container.Cursor = Cursors.Default;
            this.Invalidate();
        }

        protected override void MouseDown(MouseEventArgs e)
        {
            // 只处理标题栏，可以拖动，其他位置不处理拖动消息
            if (this.WindowState != FormWindowState.Maximized && !this.HelpHover && !this.MinHover && !this.CloseHover && !this.MaxHover && !ControlBoxies.Hover && !FullButtons.Hover)
            {
                Win32.ReleaseCapture();
                //*********************调用移动无窗体控件函数  
                Win32.SendMessage(this.Container.Handle, Win32.WM_SYSCOMMAND, Win32.SC_MOVE + Win32.HTCAPTION, 0);
            }
        }
    }
}
