using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Momo.Forms
{
    public class MForm : Form
    {
        [DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        private const int AW_HIDE = 0x10000;//隐藏窗口
        private const int AW_ACTIVE = 0x20000;//激活窗口，在使用了AW_HIDE标志后不要使用这个标志
        private const int AW_BLEND = 0x80000;//使用淡入淡出效果 

        public MForm()
        {
            // 设置绘制样式
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer, true);
            base.UpdateStyles();

            // 设置没有标题栏
            base.FormBorderStyle = FormBorderStyle.None;
            this.borderWidth = 1;
            this.borderColor = Color.White;

            this.WaitingFont = new Font("微软雅黑", 11, FontStyle.Regular);

            this.Caption = new DefaultCaptionPalette(this);
            this.Caption.HelpButtonClick += Caption_HelpButtonClick;
            this.Caption.CaptionControlClick += Caption_CaptionControlClick;
            this.Caption.CaptionFullButtonClick += Caption_CaptionFullButtonClick;
        }

        private DShadowForm shadow;

        private void Caption_CaptionFullButtonClick(CaptionFullButton arg1, MouseEventArgs arg2)
        {
            CaptionFullButtonClick?.Invoke(arg1, arg2);
        }

        private void showShadowBorder(bool redraw = true)
        {
            if (!DesignMode && this.ShadowBorder)
            {
                if (this.shadow == null || this.shadow.IsDisposed)
                {
                    shadow = new DShadowForm(this)
                    {
                        ShadowBlur = 6,
                        ShadowSpread = 0,
                        ShadowColor = Color.FromArgb(204, 204, 204)
                    };
                }

                shadow.RefreshShadow(redraw);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            showShadowBorder();
            if (this.borderWidth > 0)
            {
                base.Padding = new Padding(this.borderWidth, this.Caption.Height + this.borderWidth, this.borderWidth, this.borderWidth);
                this.Caption.Rectangle = new Rectangle(this.borderWidth, this.borderWidth, this.Width - this.borderWidth * 2, this.Caption.Height);
            }
            //if (this.ShadowBorder)
            //{
            //    //API函数加载，实现窗体边框阴影效果
            //    Win32.SetClassLong(this.Handle, Win32.GCL_STYLE, Win32.GetClassLong(this.Handle, Win32.GCL_STYLE) | Win32.CS_DropSHADOW);
            //}


            if (AlertForm)
            {
                int x = Screen.PrimaryScreen.WorkingArea.Right - this.Width;
                int y = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height;
                this.Location = new Point(x, y);
                AnimateWindow(this.Handle, 1000, AW_ACTIVE);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (AlertForm)
            {
                AnimateWindow(this.Handle, 1000, AW_BLEND | AW_HIDE);
            }
        }

        private void Caption_CaptionControlClick(CaptionControlButton arg1, MouseEventArgs arg2)
        {
            CaptionControlClick?.Invoke(arg1, arg2);
        }

        private void Caption_HelpButtonClick(object sender, EventArgs e)
        {
            base.OnHelpButtonClicked(new CancelEventArgs());
        }

        [Description("是否从右下角弹出"), Category("Momo")]
        public bool AlertForm { get; set; }

        [Browsable(true), Category("Momo"), Description("标题栏扩展按钮点击事件")]
        public event Action<CaptionControlButton, MouseEventArgs> CaptionControlClick;

        [Browsable(true), Category("Momo"), Description("标题栏填充按钮点击事件")]
        public event Action<CaptionFullButton, MouseEventArgs> CaptionFullButtonClick;
        [Localizable(true)]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(true), Category("Momo"), Description("标题栏设置")]
        public DefaultCaptionPalette Caption { get; set; }

        private int borderWidth;
        [Browsable(true), Category("Momo"), Description("边框宽度")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int BorderWidth
        {
            get { return this.borderWidth; }
            set
            {
                this.borderWidth = value;
                base.Padding = new Padding(value, this.Caption.Height + value, value, value);
                this.Caption.Rectangle = new Rectangle(value, value, this.Width - value * 2, this.Caption.Height);
                this.Invalidate();
            }
        }

        private Color borderColor;
        [Browsable(true), Category("Momo"), Description("边框颜色")]
        public Color BorderColor { get { return this.borderColor; } set { this.borderColor = value; this.Invalidate(); } }

        [Browsable(true), Category("Momo"), Description("等待框字体")]
        public Font WaitingFont { get; set; }

        [Browsable(true), Category("Momo"), Description("是否显示窗体阴影效果")]
        public bool ShadowBorder { get; set; }

        [Browsable(false)]
        public new bool HelpButton { get { return this.Caption.HelpButton; } set { this.Caption.HelpButton = value; } }

        [Browsable(false)]
        public new bool ControlBox { get { return this.Caption.ControlBox; } set { this.Caption.ControlBox = value; } }

        [Browsable(false)]
        public new bool MinimizeBox { get { return this.Caption.MinimizeBox; } set { this.Caption.MinimizeBox = value; } }

        [Browsable(false)]
        public new bool MaximizeBox { get { return this.Caption.MaximizeBox; } set { this.Caption.MaximizeBox = value; } }

        private FormWindowState windowState;
        private Size nomalSize;
        private Point nomalPoint;


        private bool isWaiting = false;
        private Panel waitingLayer = null;
        private MPanel waitingInnerLayer = null;
        private MRolling rolling = null;

        public new FormWindowState WindowState
        {
            get { return this.windowState; }
            set
            {
                if (!DesignMode)
                {
                    if (value == FormWindowState.Maximized)
                    {
                        this.nomalSize = this.Size;
                        this.nomalPoint = this.Location;

                        if (!this.IsMdiChild)
                        {
                            if (this.MaxToFullScreen)
                            {
                                this.MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                            }
                            else
                            {
                                this.MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height - (Screen.PrimaryScreen.Bounds.Height - Screen.PrimaryScreen.WorkingArea.Height));
                            }
                        }
                        else
                        {
                            var sizeForm = new Form();
                            sizeForm.MdiParent = this.MdiParent;
                            this.MaximumSize = new Size((sizeForm.Parent as MdiClient).ClientSize.Width, (sizeForm.Parent as MdiClient).ClientSize.Height);
                        }

                        //base.WindowState = value;
                        this.Size = this.MaximumSize;
                        this.Location = new Point(0, 0);
                        this.Invalidate(this.Caption.Rectangle);
                    }
                    else if (value == FormWindowState.Normal)
                    {
                        this.Size = nomalSize;
                        this.Location = nomalPoint;
                        this.Invalidate(this.Caption.Rectangle);
                    }
                    else if (value == FormWindowState.Minimized)
                    {
                        base.WindowState = value;
                    }
                }

                this.Caption.WindowState = value;
                this.windowState = value;
            }
        }

        public new FormBorderStyle FormBorderStyle { get; set; }

        public new Padding Padding
        {
            get { return base.Padding; }
        }

        [Browsable(true), Category("Momo"), Description("最大化到全屏")]
        public bool MaxToFullScreen { get; set; }

        [Browsable(true), Category("Momo"), Description("是否允许用户调整窗口大小")]
        public bool CustomResizeable { get; set; }

        protected virtual void DrawCaption(Graphics graphics)
        {
            this.Caption.Draw(graphics);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.Caption != null)
            {
                this.Caption.Draw(e.Graphics);
            }

            #region draw border
            if (this.borderWidth > 0)
            {
                ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, borderColor, borderWidth, ButtonBorderStyle.Solid, borderColor, borderWidth, ButtonBorderStyle.Solid, borderColor, borderWidth, ButtonBorderStyle.Solid, borderColor, borderWidth, ButtonBorderStyle.Solid);
            }
            #endregion
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            this.Caption.Width = this.Width;
            base.OnSizeChanged(e);
            if (waitingLayer != null && waitingInnerLayer != null)
            {
                waitingInnerLayer.Location = new Point((waitingLayer.Width - waitingInnerLayer.Width) / 2, (waitingLayer.Height - waitingInnerLayer.Height) / 2 - this.Caption.Height);
            }

            this.showShadowBorder();
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);

            //base.Refresh();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (this.shadow != null)
            {
                if (this.Visible && this.shadow.IsDisposed)
                {
                    showShadowBorder();
                }
                else
                {
                    this.shadow.Visible = this.Visible;
                }
            }
        }
        #region 处理自绘按钮点击事件
        protected override void OnMouseClick(MouseEventArgs e)
        {
            this.Caption.OnMouseClick(e);
            //CancelEventArgs ce = new CancelEventArgs();
            //base.OnHelpButtonClicked(ce);
            base.OnMouseClick(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            this.Caption.OnMouseMove(e);
            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Caption.OnMouseDown(e);
            base.OnMouseDown(e);
        }
        #endregion

        #region 调整窗口大小
        const int Guying_HTLEFT = 10;
        const int Guying_HTRIGHT = 11;
        const int Guying_HTTOP = 12;
        const int Guying_HTTOPLEFT = 13;
        const int Guying_HTTOPRIGHT = 14;
        const int Guying_HTBOTTOM = 15;
        const int Guying_HTBOTTOMLEFT = 0x10;
        const int Guying_HTBOTTOMRIGHT = 17;

        protected override void WndProc(ref Message m)
        {
            if (this.Caption.CloseHover || this.Caption.MinHover || this.Caption.MaxHover || this.Caption.HelpHover || this.Caption.ControlBoxies.Hover || this.Caption.FullButtons.Hover)
            {
                base.WndProc(ref m);
                return;
            }

            if (!this.CustomResizeable || this.WindowState == FormWindowState.Maximized)
            {
                base.WndProc(ref m);
                return;
            }
            switch (m.Msg)
            {
                case 0x0084:
                    base.WndProc(ref m);
                    Point vPoint = new Point((int)m.LParam & 0xFFFF,
                        (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)Guying_HTTOPLEFT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)Guying_HTBOTTOMLEFT;
                        else m.Result = (IntPtr)Guying_HTLEFT;
                    else if (vPoint.X >= ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)Guying_HTTOPRIGHT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)Guying_HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)Guying_HTRIGHT;
                    else if (vPoint.Y <= 5)
                        m.Result = (IntPtr)Guying_HTTOP;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)Guying_HTBOTTOM;
                    break;
                case 0x0201:                //鼠标左键按下的消息   
                    m.Msg = 0x00A1;         //更改消息为非客户区按下鼠标   
                    m.LParam = IntPtr.Zero; //默认值   
                    m.WParam = new IntPtr(2);//鼠标放在标题栏内   
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        #endregion

        #region 通知控件

        public Palette GetControlButtonByTag(string tag)
        {
            return this.Caption.ControlBoxies.Find((o) => { return o.Tag == tag; });
        }

        public Palette GetFullButtonByTag(string tag)
        {
            return this.Caption.ControlBoxies.Find((o) => { return o.Tag == tag; });
        }

        public void SetControlButtonNotifyByTag(string tag, string notify)
        {
            this.SetControlButtonNotifyByTag(tag, notify, Color.Red, Color.White, new Font("Calibri", 8));
        }
        public void SetControlButtonNotifyByTag(string tag, string notify, Color backColor, Color foreColor, Font font)
        {
            var palette = new NotifyPalette();
            palette.Font = font;
            palette.BackColor = backColor;
            palette.ForeColor = foreColor;
            palette.Text = notify;
            foreach (var item in this.Caption.ControlBoxies)
            {
                if (item.Tag == tag)
                {
                    palette.ParentPalette = item;
                    item.Notify = palette;
                }
            }

            this.Caption.Invalidate();
        }
        public void SetFullButtonNotifyByTag(string tag, string notify)
        {
            this.SetFullButtonNotifyByTag(tag, notify, Color.Red, Color.White, new Font("Calibri", 8));
        }
        public void SetFullButtonNotifyByTag(string tag, string notify, Color backColor, Color foreColor, Font font)
        {
            var palette = new NotifyPalette();
            palette.Font = font;
            palette.BackColor = backColor;
            palette.ForeColor = foreColor;
            palette.Text = notify;
            foreach (var item in this.Caption.FullButtons)
            {
                if (item.Tag == tag)
                {
                    palette.ParentPalette = item;
                }
            }

            this.Caption.Invalidate();
        }
        #endregion

        #region 加载中，后台处理方法

        /// <summary>
        /// 创建临时背景图片
        /// </summary>
        private Bitmap CreateBacgroundImage()
        {
            Rectangle rect = new Rectangle(this.Padding.Left,
                    this.Caption.Height,
                    this.Width - this.Padding.Left - this.Padding.Right,
                    this.Height - this.Caption.Height - this.Padding.Bottom);
            int w = rect.Width;
            int h = rect.Height;
            Point p1 = new Point(this.Location.X + this.Padding.Left,
                this.Location.Y + this.Caption.Height);
            Point p = this.Parent == null ? p1 : this.PointToScreen(p1);
            Bitmap TempImg = new Bitmap(w, h);
            try
            {
                Bitmap img = new Bitmap(w, h);
                using (Graphics g = Graphics.FromImage(TempImg))
                {
                    g.CopyFromScreen(p, new Point(0, 0), new Size(w, h));
                }

                using (Graphics g = Graphics.FromImage(img))
                {
                    GDIHelper.DrawImage(g, new Rectangle(0, 0, w, h), TempImg, 0.36F);
                }

                return img;
            }
            catch
            {
                return null;
            }
            finally
            {
                TempImg.Dispose();
            }
        }

        /// <summary>
        /// 加载中，后台处理方法
        /// </summary>
        /// <param name="method"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool Waiting(MethodInvoker method, ERollingBarStyle rollingStyle = ERollingBarStyle.Default, string message = "加载中，请稍后")
        {
            if (isWaiting)
            {
                return false;
            }

            this.isWaiting = true;

            var captionHeight = this.Caption.Height;

            Size fontSize = Size.Empty;
            using (Graphics g = this.CreateGraphics())
            {
                fontSize = Size.Ceiling(g.MeasureString(message, this.WaitingFont));
            }

            Rectangle rect = new Rectangle(this.Padding.Left,
                   this.Caption.Height,
                   this.Width - this.Padding.Left - this.Padding.Right,
                   this.Height - this.Caption.Height - this.Padding.Bottom);

            waitingLayer = new Panel();
            waitingLayer.Size = rect.Size;
            waitingLayer.Location = rect.Location;
            waitingLayer.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this.waitingLayer.BackgroundImageLayout = ImageLayout.Stretch;
            this.Controls.Add(waitingLayer);

            rolling = new MRolling();
            rolling.Size = new System.Drawing.Size(60, 60);
            rolling.Location = new Point(15, 10);
            rolling.SliceColor = Color.FromArgb(231, 76, 60);
            rolling.RadiusOut = 38;
            rolling.SliceNumber = 12;
            rolling.RollingStyle = rollingStyle;
            rolling.RadiusIn = rollingStyle == ERollingBarStyle.Default ? 20 : 38;
            rolling.PenWidth = rollingStyle == ERollingBarStyle.Default ? 3 : 5;

            waitingInnerLayer = new MPanel();
            waitingInnerLayer.Radius = 10;
            waitingInnerLayer.BorderColor = Color.DarkGray;
            waitingInnerLayer.BorderWidth = 1;
            waitingInnerLayer.RadiusMode = RadiusMode.All;
            waitingInnerLayer.BackColor = Color.White;
            waitingInnerLayer.Size = new Size(rolling.Width + fontSize.Width + 42, rolling.Height + 22);
            waitingInnerLayer.Location = new Point((waitingLayer.Width - waitingInnerLayer.Width) / 2, (waitingLayer.Height - waitingInnerLayer.Height) / 2 - captionHeight);

            this.waitingInnerLayer.Controls.Add(rolling);

            var msgLabel = new Label();
            msgLabel.Font = this.WaitingFont;
            msgLabel.Text = message;
            msgLabel.BackColor = Color.Transparent;
            msgLabel.AutoSize = true;
            msgLabel.Location = new Point(15 + rolling.Width + 10, (waitingInnerLayer.Height - fontSize.Height) / 2);

            this.waitingInnerLayer.Controls.Add(msgLabel);

            this.waitingLayer.Controls.Add(this.waitingInnerLayer);
            this.waitingLayer.BackgroundImage = CreateBacgroundImage();
            this.Controls.Add(waitingLayer);
            this.waitingLayer.BringToFront();

            this.rolling.Start();

            IAsyncResult ar = method.BeginInvoke(this.WorkComplete, method);
            return true;
        }
        /// <summary>
        /// Works the complete.
        /// </summary>
        /// <param name="results">The results.</param>
        /// User:Ryan  CreateTime:2012-8-5 16:23.
        private void WorkComplete(IAsyncResult results)
        {
            if (this.IsDisposed || !this.waitingLayer.Visible || !this.IsHandleCreated)
            {
                return;
            }

            if (this.waitingLayer.InvokeRequired)
            {
                try
                {
                    this.Invoke(new Action<IAsyncResult>(this.WorkComplete), results);
                }
                catch { }
            }
            else
            {
                try
                {
                    ((MethodInvoker)results.AsyncState).EndInvoke(results);
                }
                finally
                {
                    this.Invoke(new MethodInvoker(() =>
                    {
                        this.isWaiting = false;
                        rolling.Stop();
                        this.Controls.Remove(waitingLayer);

                        rolling = null;
                        waitingInnerLayer = null;
                        waitingLayer.Visible = false;
                        waitingLayer = null;
                    }));
                    //waitingBox.Visible = false; 
                }
            }
        }
        #endregion
    }
}
