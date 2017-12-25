// 这个类，来自于乔克斯 CSkin

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Momo.Forms
{
    internal partial class DShadowForm : Form
    {
        private Bitmap _shadowBitmap;
        private Color _shadowColor;
        private int _shadowH;
        private byte _shadowOpacity = 255;
        private int _shadowV;
        public DShadowForm(Form form)
        {
            InitializeComponent();
            Owner = form;
            ShadowColor = Color.Black;

            // default style
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;

            // bind event
            Owner.LocationChanged += UpdateLocation;
            Owner.FormClosing += (sender, eventArgs) => { if (!eventArgs.Cancel) { Close(); } };
            Owner.VisibleChanged += (sender, eventArgs) =>
            {
                if (Owner != null)
                    Visible = Owner.Visible;
            };

            Owner.Activated += (sender, args) =>
            {
                try
                {
                    Owner.BringToFront();
                }
                catch
                { }
            };

        }
        public Color ShadowColor
        {
            get { return _shadowColor; }
            set
            {
                _shadowColor = value;
                _shadowOpacity = _shadowColor.A;
            }
        }

        public Bitmap ShadowBitmap
        {
            get { return _shadowBitmap; }
            set
            {
                _shadowBitmap = value;
                SetBitmap(_shadowBitmap, ShadowOpacity);
            }
        }

        public byte ShadowOpacity
        {
            get { return _shadowOpacity; }
            set
            {
                _shadowOpacity = value;
                SetBitmap(ShadowBitmap, _shadowOpacity);
            }
        }

        public int ShadowH
        {
            get { return _shadowH; }
            set
            {
                _shadowH = value;
                RefreshShadow(false);
            }
        }

        /// <summary>
        ///     Offset X relate to Owner
        /// </summary>
        public int OffsetX
        {
            get { return ShadowH - (ShadowBlur + ShadowSpread); }
        }

        /// <summary>
        ///     Offset Y relate to Owner
        /// </summary>
        public int OffsetY
        {
            get { return ShadowV - (ShadowBlur + ShadowSpread); }
        }

        public new int Width
        {
            get { return Owner.Width + (ShadowSpread + ShadowBlur) * 2; }
        }

        public new int Height
        {
            get { return Owner.Height + (ShadowSpread + ShadowBlur) * 2; }
        }

        public int ShadowV
        {
            get { return _shadowV; }
            set
            {
                _shadowV = value;
                RefreshShadow(false);
            }
        }

        public int ShadowBlur { get; set; }
        public int ShadowSpread { get; set; }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00080000; // This form has to have the WS_EX_LAYERED extended style
                return cp;
            }
        }

        public static Bitmap DrawShadowBitmap(int width, int height, int borderRadius, int blur, int spread, Color color)
        {
            int ex = blur + spread;
            int w = width + ex * 2;
            int h = height + ex * 2;
            int solidW = width + spread * 2;
            int solidH = height + spread * 2;

            var bitmap = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(bitmap);
            // fill background
            g.FillRectangle(new SolidBrush(color)
                , blur, blur, width + spread * 2 + 1, height + spread * 2 + 1);
            // +1 to fill the gap

            if (blur > 0)
            {
                // four dir gradiant
                {
                    // left
                    var brush = new LinearGradientBrush(new Point(0, 0), new Point(blur, 0), Color.Transparent, color);
                    // will thorw ArgumentException
                    // brush.WrapMode = WrapMode.Clamp; 


                    g.FillRectangle(brush, 0, blur, blur, solidH);
                    // up
                    brush.RotateTransform(90);
                    g.FillRectangle(brush, blur, 0, solidW, blur);

                    // right
                    // make sure parttern is currect
                    brush.ResetTransform();
                    brush.TranslateTransform(w % blur, h % blur);

                    brush.RotateTransform(180);
                    g.FillRectangle(brush, w - blur, blur, blur, solidH);
                    // down
                    brush.RotateTransform(90);
                    g.FillRectangle(brush, blur, h - blur, solidW, blur);
                }


                // four corner
                {
                    var gp = new GraphicsPath();
                    //gp.AddPie(0,0,blur*2,blur*2, 180, 90);
                    gp.AddEllipse(0, 0, blur * 2, blur * 2);


                    var pgb = new PathGradientBrush(gp);
                    pgb.CenterColor = color;
                    pgb.SurroundColors = new[] { Color.Transparent };
                    pgb.CenterPoint = new Point(blur, blur);

                    // lt
                    g.FillPie(pgb, 0, 0, blur * 2, blur * 2, 180, 90);
                    // rt
                    var matrix = new Matrix();
                    matrix.Translate(w - blur * 2, 0);

                    pgb.Transform = matrix;
                    //pgb.Transform.Translate(w-blur*2, 0);
                    g.FillPie(pgb, w - blur * 2, 0, blur * 2, blur * 2, 270, 90);
                    // rb
                    matrix.Translate(0, h - blur * 2);
                    pgb.Transform = matrix;
                    g.FillPie(pgb, w - blur * 2, h - blur * 2, blur * 2, blur * 2, 0, 90);
                    // lb
                    matrix.Reset();
                    matrix.Translate(0, h - blur * 2);
                    pgb.Transform = matrix;
                    g.FillPie(pgb, 0, h - blur * 2, blur * 2, blur * 2, 90, 90);
                }
            }

            //
            return bitmap;
        }

        public void UpdateLocation(Object sender = null, EventArgs eventArgs = null)
        {
            try
            {
                Point pos = Owner.Location;

                pos.Offset(OffsetX, OffsetY);
                Location = pos;
            }
            catch { }
        }

        public void RefreshShadow(bool redraw = true)
        {
            if (redraw)
            {
                //ShadowBitmap = DrawShadow();
                ShadowBitmap = DrawShadowBitmap(Owner.Width, Owner.Height, 0, ShadowBlur, ShadowSpread, ShadowColor);
            }

            //SetBitmap(ShadowBitmap, ShadowOpacity);
            UpdateLocation();

            // 设置显示区域
            //Region r = Region.FromHrgn(Win32.CreateRoundRectRgn(0, 0, Width, Height, BorderRadius, BorderRadius));
            var r = new Region(new Rectangle(0, 0, Width, Height));
            Region or;
            if (Owner.Region == null)
                or = new Region(Owner.ClientRectangle);
            else
                or = Owner.Region.Clone();

            or.Translate(-OffsetX, -OffsetY);
            r.Exclude(or);
            Region = r;

            Owner.Refresh();
        }

        public void SetBitmap(Bitmap bitmap, byte opacity = 255)
        {
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ApplicationException("The bitmap must be 32ppp with alpha-channel.");

            IntPtr screenDc = Win32.GetDC(IntPtr.Zero);
            IntPtr memDc = Win32.CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;

            try
            {
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0)); // grab a GDI handle from this GDI+ bitmap
                oldBitmap = Win32.SelectObject(memDc, hBitmap);

                var size = new Size32(bitmap.Width, bitmap.Height);
                var pointSource = new Point32(0, 0);
                var topPos = new Point32(Left, Top);
                var blend = new BLENDFUNCTION();
                blend.BlendOp = Win32.AC_SRC_OVER;
                blend.BlendFlags = 0;
                blend.SourceConstantAlpha = opacity;
                blend.AlphaFormat = Win32.AC_SRC_ALPHA;

                Win32.UpdateLayeredWindow(Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend,
                    Win32.ULW_ALPHA);
            }
            finally
            {
                Win32.ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    Win32.SelectObject(memDc, oldBitmap);
                    Win32.DeleteObject(hBitmap);
                }
                Win32.DeleteDC(memDc);
            }
        }
    }
}