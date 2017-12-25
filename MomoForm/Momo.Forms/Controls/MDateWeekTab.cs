using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

namespace Momo.Forms.Controls
{
    /// <summary>
    /// 表示一周的日期Tab条
    /// </summary>
    public sealed class MDateWeekTab : Control
    {
        public MDateWeekTab()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);//调整大小时重绘
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);// 双缓冲
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);// 禁止擦除背景.
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.Cursor = Cursors.Hand;

            this.minDate = DateTime.MinValue;
            this.maxDate = DateTime.MaxValue;

            this.gridLineColor = Color.FromArgb(221, 221, 221);
            this.gridLineSize = 1;

            this.underLineColor = Color.FromArgb(61, 149, 236);
            this.underLineSize = 2;

            this.defaultColor = Color.FromArgb(221, 244, 255);
            this.activeColor = Color.White;

            this.Date = DateTime.Now;
            this.leftButtonRect = new Rectangle(0, 0, 60, 40);
            this.rightButtonRect = new Rectangle(this.Width - 60, 0, 60, 40);
        }

        private Rectangle leftButtonRect;
        private Rectangle rightButtonRect;
        private bool leftHover;
        private bool rightHover;

        private readonly DateRect[] dateRects = new DateRect[7];

        [Browsable(true), Description("日期改变事件"), Category("Momo")]
        public event EventHandler DateChanged;

        private DateTime date;

        [Browsable(true), Category("Momo"), Description("当前日期")]
        public DateTime Date
        {
            get { return this.date; }
            set
            {
                if ((value - this.minDate).TotalDays < 0 || (value - this.maxDate).TotalDays > 0)
                {
                    return;
                }

                this.date = value;
                this.Invalidate();
            }
        }

        private int gridLineSize;
        [Browsable(true), Description("分割线大小"), Category("Momo")]
        public int GridLineSize
        {
            get { return this.gridLineSize; }
            set
            {
                gridLineSize = value;
                this.Invalidate();
            }
        }

        private int underLineSize;
        [Browsable(true), Description("下划线大小"), Category("Momo")]
        public int UnderLineSize
        {
            get { return this.underLineSize; }
            set
            {
                underLineSize = value;
                this.Invalidate();
            }
        }

        private Color gridLineColor;
        [Browsable(true), Description("分割线颜色"), Category("Momo")]
        public Color GridLineColor
        {
            get { return this.gridLineColor; }
            set
            {
                gridLineColor = value;
                this.Invalidate();
            }
        }

        private Color underLineColor;
        [Browsable(true), Description("下划线颜色"), Category("Momo")]
        public Color UnderLineColor
        {
            get { return this.underLineColor; }
            set
            {
                underLineColor = value;
                this.Invalidate();
            }
        }

        private Color defaultColor;
        [Browsable(true), Description("非激活状态颜色"), Category("Momo")]
        public Color DefaultColor
        {
            get { return this.defaultColor; }
            set
            {
                defaultColor = value;
                this.Invalidate();
            }
        }

        private Color activeColor;
        [Browsable(true), Description("激活状态颜色"), Category("Momo")]
        public Color ActiveColor
        {
            get { return this.activeColor; }
            set
            {
                activeColor = value;
                this.Invalidate();
            }
        }

        private DateTime minDate;
        [Browsable(true), Description("最小日期"), Category("Momo")]
        public DateTime MinDate
        {
            get { return this.minDate; }
            set
            {
                if ((this.MaxDate - value).TotalDays < 7) { throw new ArgumentException("日期范围必须间隔7天以上。"); }
                this.minDate = value;
            }
        }

        private DateTime maxDate;
        [Browsable(true), Description("最大日期"), Category("Momo")]
        public DateTime MaxDate
        {
            get { return this.maxDate; }
            set
            {
                if ((value - this.minDate).TotalDays < 7) { throw new ArgumentException("日期范围必须间隔7天以上。"); }
                this.maxDate = value;
            }
        }

        protected override Size DefaultSize
        {
            get
            {
                return new Size(882, 40);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.leftButtonRect = new Rectangle(0, 0, 60, 40);
            this.rightButtonRect = new Rectangle(this.Width - 60, 0, 60, 40);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            var underLinePen = new Pen(underLineColor, underLineSize);
            #region 绘制左、上、右边框
            using (var pen = new Pen(this.gridLineColor, this.gridLineSize))
            {
                g.DrawLine(pen, 0, 0, 0, Height);

                g.DrawLine(pen, 0, 0, Width, 0);

                g.DrawLine(pen, Width - gridLineSize, 0, Width - gridLineSize, Height);
            }
            #endregion

            ImageAttributes imgAtt = new ImageAttributes();
            imgAtt.SetWrapMode(WrapMode.Clamp);
            var gridPen = new Pen(gridLineColor, gridLineSize);
            #region 绘制 前进、后退 按钮
            // 左右按钮，60
            g.DrawImage(Properties.Resources.go_left, new Rectangle((leftButtonRect.Width - 24) / 2 + leftButtonRect.X, (leftButtonRect.Height - 24) / 2 + leftButtonRect.Y, 24, 24), 0, 0, Properties.Resources.left.Width, Properties.Resources.left.Height, GraphicsUnit.Pixel, imgAtt);
            g.DrawImage(Properties.Resources.go_right, new Rectangle((rightButtonRect.Width - 24) / 2 + rightButtonRect.X, (rightButtonRect.Height - 24) / 2 + leftButtonRect.Y, 24, 24), 0, 0, Properties.Resources.left.Width, Properties.Resources.left.Height, GraphicsUnit.Pixel, imgAtt);

            g.DrawLine(underLinePen, leftButtonRect.X, leftButtonRect.Height - underLineSize, leftButtonRect.Width, leftButtonRect.Height - underLineSize);
            g.DrawLine(underLinePen, rightButtonRect.X, rightButtonRect.Height - underLineSize, Width, rightButtonRect.Height - underLineSize);

            g.DrawLine(gridPen, rightButtonRect.X, rightButtonRect.Y, rightButtonRect.X, rightButtonRect.Height);
            #endregion

            #region 绘制日期框

            var leftDay = (this.Date - this.MinDate).TotalDays;
            var rightDay = (this.MaxDate - this.Date).TotalDays;

            if (leftDay > 3)
            {
                leftDay = 3;
            }
            else
            {
                rightDay = 7 - leftDay - 1;
            }

            if (rightDay > 3)
            {
                if (leftDay == 3)
                {
                    rightDay = 3;
                }
            }
            else
            {
                leftDay = 7 - rightDay - 1;
            }

            var fromDate = this.Date.AddDays(-leftDay);
            var endDate = this.Date.AddDays(rightDay);

            var tempDate = DateTime.Now;
            var itemWidth = (this.Width - 120) / 7;
            var x = 0;
            var textBrush = new SolidBrush(this.ForeColor);
            var defaultBrush = new SolidBrush(this.defaultColor);
            var activeBrush = new SolidBrush(this.activeColor);
            var leftIsActive = false;
            for (var i = 0; i < 7; i++)
            {
                tempDate = fromDate.AddDays(i);
                x = leftButtonRect.Width + itemWidth * i;

                var text = GetDateText(tempDate);

                if (!leftIsActive)
                {
                    // 左边线
                    g.DrawLine(gridPen, x, 0, x, Height);
                }
                else
                {
                    leftIsActive = false;
                }

                var rect = new Rectangle(x, 0, (this.Width - 120) / 7, this.Height);

                this.dateRects[i] = new DateRect() { DateTime = tempDate, Rect = rect };

                if ((tempDate - this.Date).TotalDays == 0)
                {
                    g.FillRectangle(activeBrush, rect);

                    g.DrawLine(underLinePen, rect.X, 0, rect.X, Height);
                    g.DrawLine(underLinePen, rect.X + rect.Width, 0, rect.X + rect.Width, Height);

                    underLinePen.Width = this.underLineSize * 3; ;
                    g.DrawLine(underLinePen, rect.X, 0, rect.X + rect.Width, 0);
                    underLinePen.Width = this.underLineSize;

                    leftIsActive = true;
                }
                else
                {
                    g.FillRectangle(defaultBrush, rect);
                    g.DrawLine(underLinePen, rect.X, Height - underLineSize, rect.X + rect.Width, Height - underLineSize);
                }

                var fontSize = g.MeasureString(text, this.Font);
                g.DrawString(text, this.Font, textBrush, (rect.Width - fontSize.Width) / 2 + rect.X, (this.Height - fontSize.Height) / 2);
            }

            underLinePen.Dispose();
            activeBrush.Dispose();
            defaultBrush.Dispose();
            textBrush.Dispose();
            gridPen.Dispose();
            #endregion
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            leftHover = leftButtonRect.Contains(e.Location);
            rightHover = rightButtonRect.Contains(e.Location);
            if (leftHover || rightHover)
            {
                return;
            }

            foreach (var item in this.dateRects)
            {
                if(item == null) { continue; }
                item.Hover = item.Rect.Contains(e.Location);
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (leftHover)
            {
                this.Date = this.date.AddDays(-1);
                return;
            }
            if (rightHover)
            {
                this.Date = this.date.AddDays(1);
                return;
            }


            foreach (var item in this.dateRects)
            {
                if (item != null && item.Hover)
                {
                    this.Date = item.DateTime;
                    DateChanged?.Invoke(this, e);
                    return;
                }
            }
        }

        private string GetDateText(DateTime date)
        {
            var week = string.Empty;
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    week = "周五";
                    break;
                case DayOfWeek.Monday:
                    week = "周一";
                    break;
                case DayOfWeek.Saturday:
                    week = "周六";
                    break;
                case DayOfWeek.Sunday:
                    week = "周日";
                    break;
                case DayOfWeek.Thursday:
                    week = "周四";
                    break;
                case DayOfWeek.Tuesday:
                    week = "周二";
                    break;
                case DayOfWeek.Wednesday:
                    week = "周三";
                    break;
            }

            return string.Format("{0}  {1}", date.ToString("MM-dd"), week);
        }

        private class DateRect
        {
            public Rectangle Rect { get; set; }

            public DateTime DateTime { get; set; }
            public bool Hover { get; internal set; }
        }
    }
}
