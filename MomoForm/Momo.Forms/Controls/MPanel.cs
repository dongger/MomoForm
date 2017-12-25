using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Momo.Forms
{
    /// <summary>
    /// 自定义Panel
    /// </summary>
    public class MPanel : Panel
    {
        public MPanel()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);//调整大小时重绘
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);// 双缓冲
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);// 禁止擦除背景.
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            base.BackColor = Color.Transparent;
            this.BackColor = Color.Transparent;
            this.BackColorGradint = Color.Empty;
            this.RadiusMode = Forms.RadiusMode.None;
            this.Radius = 0;
            this.BorderColor = Color.Empty;
            this.BorderWidth = 0;
        }

        private ButtonBorderStyle borderStyle = ButtonBorderStyle.Solid;
        [Category("Momo"), Description("边框样式"), DefaultValue(typeof(ButtonBorderStyle), "Solid")]
        public new ButtonBorderStyle BorderStyle { get { return this.borderStyle; } set { this.borderStyle = value;this.Invalidate(); } }

        [Category("Momo"), Description("默认背景色"), DefaultValue(typeof(Color), "Transparent")]
        public new Color BackColor { get; set; }

        [Category("Momo"), Description("默认背景渐变色"), DefaultValue(typeof(Color), "Empty")]
        public Color BackColorGradint { get; set; }

        [Browsable(true), Category("Momo"), Description("背景颜色渐变方向"), DefaultValue(typeof(GradientMode), "None")]
        public GradientMode LinearGradientMode { get; set; }

        private RadiusMode radiusMode;
        [Category("Momo"), Description("圆角模式"), DefaultValue(typeof(RadiusMode), "None")]
        public RadiusMode RadiusMode { get { return this.radiusMode; } set { this.radiusMode = value; this.SetPadding(); } }

        private int radius;
        [Category("Momo"), Description("圆角大小，将覆盖L、T、R、B"), DefaultValue(0)]
        public int Radius { get { return this.radius; } set { this.radius = value; this.SetPadding(); } }

        private Color borderColor;
        [Category("Momo"), Description("边框颜色，将覆盖L、T、R、B"), DefaultValue(typeof(Color), "Empty")]
        public Color BorderColor
        {
            get { return this.borderColor; }
            set
            {
                this.borderColor = value;
                if (this.borderBottomColor.IsEmpty)
                {
                    this.borderBottomColor = value;
                }
                if (this.borderTopColor.IsEmpty)
                {
                    this.borderTopColor = value;
                }
                if (this.borderRightColor.IsEmpty)
                {
                    this.borderRightColor = value;
                }
                if (this.borderLeftColor.IsEmpty)
                {
                    this.borderLeftColor = value;
                }

                this.SetPadding();
            }
        }

        private int borderWidth;
        [Category("Momo"), Description("边框大小"), DefaultValue(0)]
        public int BorderWidth
        {
            get { return this.borderWidth; }
            set
            {
                this.borderWidth = value;

                if (this.borderBottomWidth == 0)
                {
                    this.borderBottomWidth = value;
                }
                if (this.borderTopWidth == 0)
                {
                    this.borderTopWidth = value;
                }
                if (this.borderRightWidth == 0)
                {
                    this.borderRightWidth = value;
                }
                if (this.borderLeftWidth == 0)
                {
                    this.borderLeftWidth = value;
                }

                this.SetPadding();
            }
        }

        private Color borderLeftColor;
        [Category("Momo"), Description("左边框颜色，圆角模式无效"), DefaultValue(typeof(Color), "Empty")]
        public Color BorderLeftColor { get { return this.borderLeftColor; } set { this.borderLeftColor = value; this.SetPadding(); } }

        private int borderLeftWidth;
        [Category("Momo"), Description("左边框大小，圆角模式无效"), DefaultValue(0)]
        public int BorderLeftWidth { get { return this.borderLeftWidth; } set { this.borderLeftWidth = value; this.SetPadding(); } }


        private Color borderRightColor;
        [Category("Momo"), Description("右边框颜色，圆角模式无效"), DefaultValue(typeof(Color), "Empty")]
        public Color BorderRightColor { get { return this.borderRightColor; } set { this.borderRightColor = value; this.SetPadding(); } }

        private int borderRightWidth;
        [Category("Momo"), Description("右边框大小，圆角模式无效"), DefaultValue(0)]
        public int BorderRightWidth { get { return this.borderRightWidth; } set { this.borderRightWidth = value; this.SetPadding(); } }

        private Color borderTopColor;
        [Category("Momo"), Description("上边框颜色，圆角模式无效"), DefaultValue(typeof(Color), "Empty")]
        public Color BorderTopColor { get { return this.borderTopColor; } set { this.borderTopColor = value; this.SetPadding(); } }

        private int borderTopWidth;
        [Category("Momo"), Description("上边框大小，圆角模式无效"), DefaultValue(0)]
        public int BorderTopWidth { get { return this.borderTopWidth; } set { this.borderTopWidth = value; this.SetPadding(); } }

        private Color borderBottomColor;
        [Category("Momo"), Description("下边框颜色，圆角模式无效"), DefaultValue(typeof(Color), "Empty")]
        public Color BorderBottomColor { get { return this.borderBottomColor; } set { this.borderBottomColor = value; this.SetPadding(); } }

        private int borderBottomWidth;
        [Category("Momo"), Description("下边框大小，圆角模式无效"), DefaultValue(0)]
        public int BorderBottomWidth { get { return this.borderBottomWidth; } set { this.borderBottomWidth = value; this.SetPadding(); } }

        // 覆盖掉
        [Browsable(false)]
        public new Padding Padding { get; set; }

        protected virtual void SetPadding()
        {
            var borderLeftWidth = this.BorderLeftColor.IsEmpty ? 0 : this.BorderLeftWidth;
            var borderTopWidth = this.BorderTopColor.IsEmpty ? 0 : this.BorderTopWidth;
            var borderRightWidth = this.BorderRightColor.IsEmpty ? 0 : this.BorderRightWidth;
            var borderBottomWidth = this.BorderBottomColor.IsEmpty ? 0 : this.BorderBottomWidth;
            base.Padding = new Padding(borderLeftWidth, borderTopWidth, borderRightWidth, borderBottomWidth);

            this.Invalidate();
        }

        protected void ResetPadding(Padding padding)
        {
            base.Padding = padding;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.RadiusMode == RadiusMode.None)
            {
                var borderLeftWidth = this.BorderLeftColor.IsEmpty ? 0 : this.BorderLeftWidth;
                var borderTopWidth = this.BorderTopColor.IsEmpty ? 0 : this.BorderTopWidth;
                var borderRightWidth = this.BorderRightColor.IsEmpty ? 0 : this.BorderRightWidth;
                var borderBottomWidth = this.BorderBottomColor.IsEmpty ? 0 : this.BorderBottomWidth;

                ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle
                    , this.BorderLeftColor, borderLeftWidth, BorderStyle
                    , this.BorderTopColor, borderTopWidth, BorderStyle
                    , this.BorderRightColor, borderRightWidth, BorderStyle
                    , this.BorderBottomColor, borderBottomWidth, BorderStyle);
            }
        }

        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)
        {
            // 这里调用父类，主要用来处理透明色（圆角之后的问题）
            base.OnPaintBackground(pevent);

            if (this.RadiusMode != RadiusMode.None)
            {
                RadiusDrawable.DrawRadius(pevent.Graphics, this.ClientRectangle, RadiusMode, this.Radius, BackColor, BackColorGradint, this.LinearGradientMode, this.BorderColor, this.BorderWidth);
            }
            else
            {
                System.Drawing.Drawing2D.LinearGradientBrush brush;

                if (this.LinearGradientMode == GradientMode.None)
                {
                    brush = new System.Drawing.Drawing2D.LinearGradientBrush(this.ClientRectangle, this.BackColor, this.BackColor, System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                }
                else
                {
                    brush = new System.Drawing.Drawing2D.LinearGradientBrush(this.ClientRectangle, this.BackColor, this.BackColorGradint, ((System.Drawing.Drawing2D.LinearGradientMode)LinearGradientMode));
                }

                pevent.Graphics.FillRectangle(brush, pevent.ClipRectangle);
                brush.Dispose();
            }
        }
    }
}
