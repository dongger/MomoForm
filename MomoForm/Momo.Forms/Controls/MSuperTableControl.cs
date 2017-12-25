using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace Momo.Forms
{
    public sealed class MSuperTableControl : TabControl
    {
        public MSuperTableControl()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);//调整大小时重绘
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);// 双缓冲
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);// 禁止擦除背景.
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            this.UpdateStyles();

            this.backgroundColor = Color.White;
            this.borderColor = Color.FromArgb(29, 113, 183);
            this.tabHeadForeColor = Color.FromArgb(80, 149, 217);
            this.tabHeadActiveForeColor = Color.FromArgb(243, 152, 0);
            this.tabHeadBackColor = Color.White;
            this.tabHeadActiveBackColor = Color.White;
            this.tabHeadFont = this.Font;
        }

        private Color tabHeadBackColor;
        [Browsable(true), Description("选项卡标题默认背景颜色"), Category("Momo")]
        public Color TabHeadBackColor
        {
            get { return this.tabHeadBackColor; }
            set
            {
                tabHeadBackColor = value;
                this.Invalidate();
            }
        }

        private Color tabHeadActiveBackColor;
        [Browsable(true), Description("选项卡标题激活状态背景颜色"), Category("Momo")]
        public Color TabHeadActiveBackColor
        {
            get { return this.tabHeadActiveBackColor; }
            set
            {
                tabHeadActiveBackColor = value;
                this.Invalidate();
            }
        }

        private Font tabHeadFont;
        [Browsable(true), Description("选项卡标题激活状态背景颜色"), Category("Momo")]
        public Font TabHeadFont { get { return this.tabHeadFont; } set { this.tabHeadFont = value; this.Invalidate(); } }

        private Color tabHeadForeColor;
        [Browsable(true), Description("选项卡标题文字颜色"), Category("Momo")]
        public Color TabHeadForeColor
        {
            get { return this.tabHeadForeColor; }
            set
            {
                tabHeadForeColor = value;
                this.Invalidate();
            }
        }

        private Color tabHeadActiveForeColor;
        [Browsable(true), Description("选项卡标题激活状态文字颜色"), Category("Momo")]
        public Color TabHeadActiveForeColor
        {
            get { return this.tabHeadActiveForeColor; }
            set
            {
                tabHeadActiveForeColor = value;
                this.Invalidate();
            }
        }

        private Color backgroundColor;
        [Browsable(true), Description("Tab背景色"), Category("Momo")]
        public Color BackgroundColor { get { return this.backgroundColor; } set { this.backgroundColor = value; this.Invalidate(); } }

        private Color borderColor;
        [Browsable(true), Description("边框背景色"), Category("Momo")]
        public Color BorderColor { get { return this.backgroundColor; } set { this.backgroundColor = value; this.Invalidate(); } }

        private bool drawBorder;
        [Browsable(true), Description("是否绘制边框"), Category("Momo")]
        public bool DrawBorder { get { return this.drawBorder; } set { this.drawBorder = value; this.Invalidate(); } }

        private Rectangle HeadRectangle
        {
            get
            {
                int x = 0;
                int y = 0;
                int width = 0;
                int height = 0;

                switch (Alignment)
                {
                    case TabAlignment.Top:
                        x = 0;
                        y = 0;
                        width = ClientRectangle.Width;
                        height = ClientRectangle.Height - DisplayRectangle.Height;
                        break;
                    case TabAlignment.Bottom:
                        x = 0;
                        y = DisplayRectangle.Height;
                        width = ClientRectangle.Width;
                        height = ClientRectangle.Height - DisplayRectangle.Height;
                        break;
                    case TabAlignment.Left:
                        x = 0;
                        y = 0;
                        width = ClientRectangle.Width - DisplayRectangle.Width;
                        height = ClientRectangle.Height;
                        break;
                    case TabAlignment.Right:
                        x = DisplayRectangle.Width;
                        y = 0;
                        width = ClientRectangle.Width - DisplayRectangle.Width;
                        height = ClientRectangle.Height;
                        break;
                }

                return new Rectangle(x, y, width, height);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);

            #region 背景色
            using (var brush = new SolidBrush(backgroundColor))
            {
                e.Graphics.FillRectangle(Brushes.White, this.ClientRectangle);
            }
            #endregion

            switch (this.Alignment)
            {
                case TabAlignment.Top:
                    DrawWithTop(e);
                    break;
                case TabAlignment.Bottom:
                    DrawWithBottom(e);
                    break;
                case TabAlignment.Left:
                    DrawWithLeft(e);
                    break;
                case TabAlignment.Right:
                    DrawWithRight(e);
                    break;
            }
        }
        private void DrawWithBottom(PaintEventArgs e)
        {
            var x = 0;
            for (int i = 0; i < this.TabCount; i++)
            {
                var rect = this.GetTabRect(i);
                #region border
                x = rect.Width + rect.X;
                using (var brush = new SolidBrush(i == this.SelectedIndex ? this.tabHeadActiveBackColor : this.tabHeadBackColor))
                {
                    if (i == 0)
                    {
                        e.Graphics.FillRectangle(brush, rect.X, rect.Y, rect.Width - 1, rect.Height - 2);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(brush, rect.X + 2, rect.Y, rect.Width - 3, rect.Height - 2);
                    }
                }

                if (i == this.SelectedIndex)
                {
                    using (var pen = new Pen(borderColor, 1))
                    {
                        // 激活选项卡下框线
                        if (i == 0)
                        {
                            e.Graphics.DrawLine(pen, 0, rect.Y + rect.Height, rect.X + rect.Width, rect.Y + rect.Height);
                        }
                        else
                        {
                            e.Graphics.DrawLine(pen, rect.X, rect.Y + rect.Height, rect.X + rect.Width, rect.Y + rect.Height);
                        }
                    }

                    using (var pen = new Pen(borderColor, 1))
                    {
                        // 激活选项卡右框线
                        e.Graphics.DrawLine(pen, rect.X + rect.Width, rect.Y, rect.X + rect.Width, rect.Y + rect.Height);

                        // 激活选项卡左边线
                        if (i != 0)
                        {
                            e.Graphics.DrawLine(pen, rect.X, rect.Y, rect.X, rect.Y + rect.Height);
                        }
                        else
                        {
                            e.Graphics.DrawLine(pen, 0, rect.Y, 0, rect.Y + rect.Height);
                        }
                    }
                }
                else
                {
                    // 不是当前的，只有上边线
                    using (var pen = new Pen(borderColor, 1))
                    {
                        if (i != 0)
                        {
                            e.Graphics.DrawLine(pen, rect.X, rect.Y, rect.X + rect.Width, rect.Y);
                        }
                        else
                        {
                            e.Graphics.DrawLine(pen, 0, rect.Y, rect.X + rect.Width, rect.Y);
                        }
                    }
                }
                #endregion

                var tab = this.TabPages[i];
                var fontSize = e.Graphics.MeasureString(tab.Text, tabHeadFont);
                using (var brush = new SolidBrush(i == this.SelectedIndex ? tabHeadActiveForeColor : tabHeadForeColor))
                {
                    e.Graphics.DrawString(tab.Text, this.tabHeadFont, brush, rect.X + (rect.Width - (int)fontSize.Width) / 2, rect.Y + (rect.Height - fontSize.Height) / 2);
                }
            }

            // 选项卡后面，需要画框线
            using (var pen = new Pen(borderColor, 1))
            {
                e.Graphics.DrawLine(pen, x, this.DisplayRectangle.Y + this.DisplayRectangle.Height + 2, this.Width, this.DisplayRectangle.Y + this.DisplayRectangle.Height + 2);
            }

            if (drawBorder)
            {
                using (var pen = new Pen(borderColor, 1))
                {
                    e.Graphics.DrawLine(pen, 0, 0, 0, this.DisplayRectangle.Y + this.DisplayRectangle.Height + 2);

                    e.Graphics.DrawLine(pen, 0, 0, this.Width, 0);

                    e.Graphics.DrawLine(pen, this.Width - 1, 0, this.Width - 1, this.DisplayRectangle.Y + this.DisplayRectangle.Height + 2);
                }
            }
        }

        private void DrawWithTop(PaintEventArgs e)
        {
            var x = 0;
            for (int i = 0; i < this.TabCount; i++)
            {
                var rect = this.GetTabRect(i);
                #region border
                x = rect.Width + rect.X;
                using (var brush = new SolidBrush(i == this.SelectedIndex ? this.tabHeadActiveBackColor : this.tabHeadBackColor))
                {
                    if (i == 0)
                    {
                        e.Graphics.FillRectangle(brush, rect.X, rect.Y, rect.Width - 1, rect.Height);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(brush, rect.X + 2, rect.Y, rect.Width - 3, rect.Height);
                    }
                }

                if (i == this.SelectedIndex)
                {
                    using (var pen = new Pen(borderColor, 2))
                    {
                        if (i == 0)
                        {
                            // 激活选项卡顶部框线
                            e.Graphics.DrawLine(pen, 0, 0, rect.X + rect.Width, 0);
                        }
                        else
                        {
                            e.Graphics.DrawLine(pen, rect.X, 0, rect.X + rect.Width, 0);
                        }
                    }

                    using (var pen = new Pen(borderColor, 1))
                    {
                        // 激活选项卡右框线
                        e.Graphics.DrawLine(pen, rect.X + rect.Width, 0, rect.X + rect.Width, this.DisplayRectangle.Y);

                        // 激活选项卡左边线
                        if (i != 0)
                        {
                            e.Graphics.DrawLine(pen, rect.X, 0, rect.X, this.DisplayRectangle.Y);
                        }
                        else
                        {
                            e.Graphics.DrawLine(pen, 0, 0, 0, this.DisplayRectangle.Y - 1);
                        }
                    }
                }
                else
                {
                    // 不是当前的，只有下边线
                    using (var pen = new Pen(borderColor, 1))
                    {
                        if (i != 0)
                        {
                            e.Graphics.DrawLine(pen, rect.X, this.DisplayRectangle.Y - 1, rect.X + rect.Width, this.DisplayRectangle.Y - 1);
                        }
                        else
                        {
                            e.Graphics.DrawLine(pen, 0, this.DisplayRectangle.Y - 1, rect.X + rect.Width, this.DisplayRectangle.Y - 1);
                        }
                    }
                }
                #endregion

                var tab = this.TabPages[i];
                var fontSize = e.Graphics.MeasureString(tab.Text, tabHeadFont);
                using (var brush = new SolidBrush(i == this.SelectedIndex ? tabHeadActiveForeColor : tabHeadForeColor))
                {
                    e.Graphics.DrawString(tab.Text, this.tabHeadFont, brush, rect.X + (rect.Width - (int)fontSize.Width) / 2, rect.Y + (rect.Height - fontSize.Height) / 2);
                }


            }

            // 选项卡后面，需要画框线
            using (var pen = new Pen(borderColor, 1))
            {
                e.Graphics.DrawLine(pen, x, this.DisplayRectangle.Y - 1, this.Width, this.DisplayRectangle.Y - 1);
            }

            if (drawBorder)
            {
                using (var pen = new Pen(borderColor, 1))
                {
                    e.Graphics.DrawLine(pen, 0, this.DisplayRectangle.Y, 0, this.Height - 1);

                    e.Graphics.DrawLine(pen, 0, this.Height - 1, this.Width, this.Height - 1);

                    e.Graphics.DrawLine(pen, this.Width - 1, this.DisplayRectangle.Y, this.Width - 1, this.Height - 1);
                }
            }
        }

        private void DrawWithLeft(PaintEventArgs e)
        {
            var y = 0;
            for (int i = 0; i < this.TabCount; i++)
            {
                var rect = this.GetTabRect(i);
                #region border
                y = rect.Height + rect.Y;
                using (var brush = new SolidBrush(i == this.SelectedIndex ? this.tabHeadActiveBackColor : this.tabHeadBackColor))
                {
                    if (i == 0)
                    {
                        e.Graphics.FillRectangle(brush, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(brush, rect.X, rect.Y + 2, rect.Width - 1, rect.Height - 3);
                    }
                }

                if (i == this.SelectedIndex)
                {
                    using (var pen = new Pen(borderColor, 2))
                    {
                        if (i == 0)
                        {
                            // 激活选项卡左框线
                            e.Graphics.DrawLine(pen, 0, 0, 0, rect.Y + rect.Height);
                        }
                        else
                        {
                            e.Graphics.DrawLine(pen, 0, rect.Y, 0, rect.Y + rect.Height);
                        }
                    }

                    using (var pen = new Pen(borderColor, 1))
                    {
                        // 激活选项卡下框线
                        e.Graphics.DrawLine(pen, 0, rect.Y + rect.Height, rect.X + rect.Width, rect.Y + rect.Height);

                        // 激活选项卡上边线
                        if (i != 0)
                        {
                            e.Graphics.DrawLine(pen, 0, rect.Y, rect.X + rect.Width, rect.Y);
                        }
                        else
                        {
                            e.Graphics.DrawLine(pen, 0, 0, rect.X + rect.Width, 0);
                        }
                    }
                }
                else
                {
                    // 不是当前的，只有右边线
                    using (var pen = new Pen(borderColor, 1))
                    {
                        if (i != 0)
                        {
                            e.Graphics.DrawLine(pen, rect.X + rect.Width, rect.Y, rect.X + rect.Width, rect.Y + rect.Height);
                        }
                        else
                        {
                            e.Graphics.DrawLine(pen, rect.X + rect.Width, 0, rect.X + rect.Width, rect.Y + rect.Height);
                        }
                    }
                }
                #endregion

                var tab = this.TabPages[i];
                var fontSize = e.Graphics.MeasureString(tab.Text, tabHeadFont);
                using (var brush = new SolidBrush(i == this.SelectedIndex ? tabHeadActiveForeColor : tabHeadForeColor))
                {
                    e.Graphics.DrawString(tab.Text, this.tabHeadFont, brush, rect.X + (rect.Width - fontSize.Height) / 2, rect.Y + (rect.Height - fontSize.Width) / 2, new StringFormat(StringFormatFlags.DirectionVertical));
                }
            }

            // 选项卡后面，需要画框线
            using (var pen = new Pen(borderColor, 1))
            {
                e.Graphics.DrawLine(pen, this.DisplayRectangle.X - 2, y, this.DisplayRectangle.X - 2, this.Height);
            }

            if (drawBorder)
            {
                using (var pen = new Pen(borderColor, 1))
                {
                    e.Graphics.DrawLine(pen, this.DisplayRectangle.X - 2, 0, this.Width, 0);

                    e.Graphics.DrawLine(pen, this.DisplayRectangle.X - 2, this.Height - 1, this.Width, this.Height - 1);

                    e.Graphics.DrawLine(pen, this.Width - 1, 0, this.Width - 1, this.Height - 1);
                }
            }
        }
        private void DrawWithRight(PaintEventArgs e)
        {
            var y = 0;
            for (int i = 0; i < this.TabCount; i++)
            {
                var rect = this.GetTabRect(i);
                #region border
                y = rect.Height + rect.Y;
                using (var brush = new SolidBrush(i == this.SelectedIndex ? this.tabHeadActiveBackColor : this.tabHeadBackColor))
                {
                    if (i == 0)
                    {
                        e.Graphics.FillRectangle(brush, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(brush, rect.X, rect.Y + 2, rect.Width - 1, rect.Height - 3);
                    }
                }

                if (i == this.SelectedIndex)
                {
                    using (var pen = new Pen(borderColor, 2))
                    {
                        if (i == 0)
                        {
                            // 激活选项卡右框线
                            e.Graphics.DrawLine(pen, this.Width, 0, this.Width, rect.Y + rect.Height);
                        }
                        else
                        {
                            e.Graphics.DrawLine(pen, this.Width, rect.Y, this.Width, rect.Y + rect.Height);
                        }
                    }

                    using (var pen = new Pen(borderColor, 1))
                    {
                        // 激活选项卡下框线
                        //e.Graphics.DrawLine(pen, DisplayRectangle.Width + DisplayRectangle.X, rect.Y + rect.Height, DisplayRectangle.Width + DisplayRectangle.X, rect.Y + rect.Height);

                        e.Graphics.DrawLine(pen, DisplayRectangle.Width + DisplayRectangle.X, rect.Y + rect.Height, this.Width, rect.Y + rect.Height);
                        // 激活选项卡上边线
                        if (i != 0)
                        {
                            e.Graphics.DrawLine(pen, 0, rect.Y, rect.X + rect.Width, rect.Y);
                        }
                        else
                        {
                            e.Graphics.DrawLine(pen, DisplayRectangle.Width + DisplayRectangle.X, 0, this.Width, 0);
                        }
                    }
                }
                else
                {
                    // 不是当前的，只有左边线
                    using (var pen = new Pen(borderColor, 1))
                    {
                        if (i != 0)
                        {
                            e.Graphics.DrawLine(pen, DisplayRectangle.Width + DisplayRectangle.X, rect.Y, DisplayRectangle.Width + DisplayRectangle.X, rect.Y + rect.Height);
                        }
                        else
                        {
                            e.Graphics.DrawLine(pen, DisplayRectangle.Width + DisplayRectangle.X, 0, DisplayRectangle.Width + DisplayRectangle.X, rect.Y + rect.Height);
                        }
                    }
                }
                #endregion

                var tab = this.TabPages[i];
                var fontSize = e.Graphics.MeasureString(tab.Text, tabHeadFont);
                using (var brush = new SolidBrush(i == this.SelectedIndex ? tabHeadActiveForeColor : tabHeadForeColor))
                {
                    e.Graphics.DrawString(tab.Text, this.tabHeadFont, brush, rect.X + (rect.Width - fontSize.Height) / 2, rect.Y + (rect.Height - fontSize.Width) / 2, new StringFormat(StringFormatFlags.DirectionVertical));
                }
            }

            // 选项卡后面，需要画框线
            using (var pen = new Pen(borderColor, 1))
            {
                e.Graphics.DrawLine(pen, DisplayRectangle.Width + DisplayRectangle.X, y, DisplayRectangle.Width + DisplayRectangle.X, this.Height);
            }

            if (drawBorder)
            {
                using (var pen = new Pen(borderColor, 1))
                {
                    e.Graphics.DrawLine(pen, 0, 0, DisplayRectangle.Width + DisplayRectangle.X, 0);

                    e.Graphics.DrawLine(pen, 0, 0, 0, this.Height);

                    e.Graphics.DrawLine(pen, 0, this.Height - 1, DisplayRectangle.Width + DisplayRectangle.X, this.Height - 1);
                }
            }
        }
    }
}
