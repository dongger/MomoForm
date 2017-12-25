using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Momo.Forms
{
    public enum ERollingBarStyle
    {
        /// <summary>
        /// 若干个矩形切片转动
        /// </summary>
        Default,

        /// <summary>
        /// 弧形片转动
        /// </summary>
        CurveRolling,

        /// <summary>
        /// 圆圈+一个白色小点在转动
        /// </summary>
        PointRolling,

        /// <summary>
        /// 小点到大点在转动
        /// </summary>
        PointsRolling
    }

    public sealed class MRolling : Control
    {
        public MRolling()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.TabStop = false;

            this.Interval = 100;
            this.RollingStyle = ERollingBarStyle.Default;
            this.RadiusIn = 10;
            this.RadiusOut = 20;
            this.SliceNumber = 12;
            this.PenWidth = 3;
            this.SliceColor = Color.Red;
            this.PointColor = Color.White;
            this.BackColor = Color.Transparent;

            this.timer.Enabled = false;
            this.timer.Interval = 100;
            this.timer.Tick += timer_Tick;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private Timer timer = new Timer();

        [Category("转轮"), Description("转动频率，单位毫秒"), DefaultValue(100)]
        public int Interval { get { return this.timer.Interval; } set { this.timer.Interval = value < 1 ? 100 : value; } }

        [Category("转轮"), Description("转轮样式"), DefaultValue(typeof(ERollingBarStyle), "Default")]
        public ERollingBarStyle RollingStyle { get; set; }

        [Category("转轮"), Description("转轮内环大小，单位像素"), DefaultValue(10)]
        public int RadiusIn { get; set; }

        [Category("转轮"), Description("转轮外环大小，单位像素"), DefaultValue(20)]
        public int RadiusOut { get; set; }

        [Category("转轮"), Description("切片数"), DefaultValue(12)]
        public int SliceNumber { get; set; }

        [Category("转轮"), Description("画笔宽度"), DefaultValue(3)]
        public int PenWidth { get; set; }

        [Category("转轮"), Description("切片颜色"), DefaultValue(typeof(Color), "Red")]
        public Color SliceColor { get; set; }

        [Category("转轮"), Description("圆点颜色"), DefaultValue(typeof(Color), "White")]
        public Color PointColor { get; set; }

        private float currentAngle = 0;

        private void IncreaseCurrentAngle()
        {
            if (timer.Enabled)
            {
                currentAngle += 360f / this.SliceNumber;
                if (currentAngle > 360f)
                    currentAngle -= 360f;
            }
        }

        public void Start()
        {
            if (!this.timer.Enabled)
            {
                this.timer.Enabled = true;
            }
        }

        public void Stop()
        {
            this.timer.Enabled = true;
        }

        private void DrawPointsRolling(Graphics g)
        {
            IncreaseCurrentAngle();
            Point centerPoint = new Point(this.ClientRectangle.X + this.ClientRectangle.Width / 2, this.ClientRectangle.Y + this.ClientRectangle.Height / 2);
            var startAngle = (float)(this.currentAngle * 2 * Math.PI / 360f);
            PointF pf = PointF.Empty;
            double sweepAngle = 2 * Math.PI / 10;
            RectangleF rectGuy = RectangleF.Empty;
            float[] widthArray = new float[] { 5f, 4f, 3f, 2f, 2f };
            Color[] colorArray = ColorHelper.GetLighterArrayColors(this.SliceColor, 5, 50f);

            using (SolidBrush sb = new SolidBrush(this.SliceColor))
            {
                for (int i = 0; i < 5; i++)
                {
                    pf.X = centerPoint.X + (float)(Math.Cos(startAngle - i * sweepAngle) * this.RadiusIn / 2);
                    pf.Y = centerPoint.Y + (float)(Math.Sin(startAngle - i * sweepAngle) * this.RadiusIn / 2);
                    rectGuy = new RectangleF(pf.X - widthArray[i] / 2, pf.Y - widthArray[i] / 2, widthArray[i], widthArray[i]);
                    sb.Color = colorArray[4 - i];
                    g.FillEllipse(sb, rectGuy);
                }
            }
        }

        private void DrawPointRolling(Graphics g)
        {
            IncreaseCurrentAngle();
            Point centerPoint = new Point(this.ClientRectangle.X + this.ClientRectangle.Width / 2, this.ClientRectangle.Y + this.ClientRectangle.Height / 2);
            var startAngle = (float)(this.currentAngle * 2 * Math.PI / 360f);
            PointF pf = PointF.Empty;
            pf.X = centerPoint.X + (float)(Math.Cos(startAngle) * this.RadiusIn / 2);
            pf.Y = centerPoint.Y + (float)(Math.Sin(startAngle) * this.RadiusIn / 2);

            Rectangle rectCircle = new Rectangle(this.ClientRectangle.X + (this.ClientRectangle.Width - this.RadiusIn) / 2, this.ClientRectangle.Y + (this.ClientRectangle.Height - this.RadiusIn) / 2, this.RadiusIn, this.RadiusIn);
            float width = this.PenWidth;
            RectangleF rectDiamond = new RectangleF(pf.X - width / 2, pf.Y - width / 2, width, width);

            using (Pen p = new Pen(this.SliceColor, this.PenWidth))
            {
                g.DrawEllipse(p, rectCircle);
            }

            using (SolidBrush sb = new SolidBrush(this.PointColor))
            {
                g.FillEllipse(sb, rectDiamond);
            }
        }

        private void DrawDefault(Graphics g)
        {
            IncreaseCurrentAngle();

            Point centerPoint = new Point(this.ClientRectangle.X + this.ClientRectangle.Width / 2, this.ClientRectangle.Y + this.ClientRectangle.Height / 2);
            PointF p1, p2;
            p1 = new PointF(0f, 0f);
            p2 = p1;
            double sweepAngle = 2 * Math.PI / this.SliceNumber;
            var startAngle = (float)(this.currentAngle * 2 * Math.PI / 360f);
            var colors = ColorHelper.GetLighterArrayColors(this.SliceColor, this.SliceNumber);
            using (Pen p = new Pen(Color.White, this.PenWidth))
            {
                p.StartCap = LineCap.Round;
                p.EndCap = LineCap.Round;
                for (int i = 0; i < this.SliceNumber; i++)
                {
                    double angle = startAngle + sweepAngle * i;
                    p1.X = centerPoint.X + (float)(this.RadiusIn / 2 * Math.Cos(angle));
                    p1.Y = centerPoint.Y + (float)(this.RadiusIn / 2 * Math.Sin(angle));
                    p2.X = centerPoint.X + (float)(this.RadiusOut / 2 * Math.Cos(angle));
                    p2.Y = centerPoint.Y + (float)(this.RadiusOut / 2 * Math.Sin(angle));
                    p.Color = colors[i];
                    g.DrawLine(p, p1, p2);
                }
            }
        }

        private void DrawCurveRolling(Graphics g)
        {
            IncreaseCurrentAngle();
            Rectangle rectInner = new Rectangle(this.ClientRectangle.X + (this.ClientRectangle.Width - this.RadiusIn) / 2, this.ClientRectangle.Y + (this.ClientRectangle.Height - this.RadiusIn) / 2, this.RadiusIn, this.RadiusIn);
            using (Pen p = new Pen(this.SliceColor, this.PenWidth))
            {
                p.StartCap = LineCap.Round;
                p.EndCap = LineCap.Round;

                g.DrawArc(p, rectInner, currentAngle, 120);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // 抗锯齿
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            switch (this.RollingStyle)
            {
                case ERollingBarStyle.Default:
                    DrawDefault(e.Graphics);
                    break;

                case ERollingBarStyle.CurveRolling:
                    DrawCurveRolling(e.Graphics);
                    break;

                case ERollingBarStyle.PointRolling:
                    DrawPointRolling(e.Graphics);
                    break;

                case ERollingBarStyle.PointsRolling:
                    DrawPointsRolling(e.Graphics);
                    break;
            }
        }
    }
}
