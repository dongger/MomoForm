using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Design;

namespace Momo.Forms
{
    public partial class MStepControl : UserControl
    {
        public MStepControl()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.Name = "DStepControl";
            this.Size = new System.Drawing.Size(271, 40);
        }

        private Color activeColor;
        [Category("进度"), Description("当前步骤外环颜色"), DefaultValue(typeof(Color), "Gray")]
        public Color ActiveColor { get { return this.activeColor; } set { this.activeColor = value; this.Invalidate(); } }

        private Color defaultColor;
        [Category("进度"), Description("默认步骤外环颜色"), DefaultValue(typeof(Color), "Gray")]
        public Color DefaultColor { get { return this.defaultColor; } set { this.defaultColor = value; this.Invalidate(); } }

        private Color activeInnerColor;
        [Category("进度"), Description("当前步骤内环颜色"), DefaultValue(typeof(Color), "Gray")]
        public Color ActiveInnerColor { get { return this.activeInnerColor; } set { this.activeInnerColor = value; this.Invalidate(); } }

        private Color defaultInnerColor;
        [Category("进度"), Description("默认步骤内环颜色"), DefaultValue(typeof(Color), "Gray")]
        public Color DefaultInnerColor { get { return this.defaultInnerColor; } set { this.defaultInnerColor = value; this.Invalidate(); } }

        private Color lineColor;
        [Category("进度"), Description("线条颜色"), DefaultValue(typeof(Color), "Gray")]
        public Color LineColor { get { return this.lineColor; } set { this.lineColor = value; } }

        [Category("进度"), Description("外圆环大小，单位像素"), DefaultValue(39)]
        public int Radius { get; set; }

        [Category("进度"), Description("内圆环大小，单位像素"), DefaultValue(30)]
        public int RadiusIn { get; set; }

        private List<string> items = new List<string>();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Localizable(true)]
        [MergableProperty(false)]
        [Browsable(true), Description("步骤文本"), Category("进度")]
        public List<string> Items { get { return items; } }

        private int currentStep = 0;
        [Category("进度"), Description("当前步骤"), DefaultValue(10)]
        public int CurrentStep { get { return currentStep; } set { this.currentStep = value; this.Invalidate(); } }

        private int penWidth = 0;
        [Category("进度"), Description("绘笔大小，像素"), DefaultValue(2)]
        public int PenWidth { get { return penWidth; } set { this.penWidth = value; this.Invalidate(); } }

        private int lineHeight = 0;
        [Category("进度"), Description("绘笔大小，像素"), DefaultValue(2)]
        public int LineHeight { get { return lineHeight; } set { this.lineHeight = value; this.Invalidate(); } }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "DStepControl";
            this.Size = new System.Drawing.Size(150, 40);
            this.penWidth = 2;
            this.ResumeLayout(false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var y = PenWidth / 2;
            var x = 0;
            var itemWidth = (this.Width - this.Radius * 2 - PenWidth) / (items.Count - 1);// 减去最后一个
            for (var i = 0; i < this.items.Count; i++)
            {
                x = itemWidth * i + PenWidth / 2 + Radius / 2;

                var fontSize = Size.Ceiling(g.MeasureString(this.items[i], this.Font));

                y = fontSize.Height / 2;

                if (i > CurrentStep)
                {
                    using (var brush = new SolidBrush(this.defaultColor))
                    {
                        g.DrawString(this.items[i], this.Font, brush, x + (Radius - fontSize.Width) / 2, y);
                    }

                    y = PenWidth / 2 + fontSize.Height * 2;

                    DrawEllipse(g, x, y, defaultColor);

                    using (var brush = new SolidBrush(this.defaultInnerColor))
                    {
                        g.FillEllipse(brush, new Rectangle(x + (Radius - RadiusIn) / 2, y + (Radius - RadiusIn) / 2, this.RadiusIn, this.RadiusIn));
                    }
                }
                else
                {
                    using (var brush = new SolidBrush(this.activeColor))
                    {
                        g.DrawString(this.items[i], this.Font, brush, x + (Radius - fontSize.Width) / 2, y);
                    }

                    y = PenWidth / 2 + fontSize.Height * 2;
                    DrawEllipse(g, x, y, activeColor);

                    using (var brush = new SolidBrush(this.activeInnerColor))
                    {
                        g.FillEllipse(brush, new Rectangle(x + (Radius - RadiusIn) / 2, y + (Radius - RadiusIn) / 2, this.RadiusIn, this.RadiusIn));
                    }
                }

                if (i < this.items.Count - 1)
                {
                    DrawLine(g, x + Radius + PenWidth / 2, itemWidth - Radius, this.Radius / 2 + y);
                }
            }
        }

        private void DrawLine(Graphics g, int x, int w, int y)
        {
            using (var brush = new SolidBrush(this.lineColor))
            {
                using (var pen = new Pen(brush, LineHeight))
                {
                    g.DrawLine(pen, x, y, x + w, y);
                }
            }
        }

        private void DrawEllipse(Graphics g, int x, int y, Color color)
        {
            using (var brush = new SolidBrush(color))
            {
                using (var pen = new Pen(brush, penWidth))
                {
                    g.DrawEllipse(pen, new Rectangle(x, y, this.Radius, this.Radius));
                }
            }
        }
    }
}
