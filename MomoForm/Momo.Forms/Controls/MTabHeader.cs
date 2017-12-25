using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Momo.Forms.Controls
{
    /// <summary>
    /// 选项卡页头效果
    /// </summary>
    public sealed class MTabHeader : UserControl
    {
        private class Tab
        {
            public string Title { get; set; }
            public bool IsActived { get; set; }
            public bool IsHover { get; set; }
            public Rectangle Rect { get; set; }
            public Rectangle CloseRect { get; set; }
            public bool CanClose { get; set; }
        }

        private readonly List<Tab> tabs = new List<Tab>();

        private Color underLineColor;
        private int underLineHeight;

        private Color activeTabColor;
        private Color tabColor;
        private int selectedIndex;
        private int tabCount;
        private bool doubleClose;
        private bool alwaysShowClose;
        private Color splitColor;

        [Browsable(true), Category("Momo"), Description("页卡下边线颜色")]
        public Color UnderLineColor
        {
            get
            {
                return underLineColor;
            }

            set
            {
                underLineColor = value;
            }
        }

        [Browsable(true), Category("Momo"), Description("页卡下边线高度")]
        public int UnderLineHeight
        {
            get
            {
                return underLineHeight;
            }

            set
            {
                underLineHeight = value;
            }
        }

        [Browsable(true), Category("Momo"), Description("激活选项卡颜色")]
        public Color ActiveTabColor
        {
            get
            {
                return activeTabColor;
            }

            set
            {
                activeTabColor = value;
            }
        }

        [Browsable(true), Category("Momo"), Description("默认选项卡颜色")]
        public Color TabColor
        {
            get
            {
                return tabColor;
            }

            set
            {
                tabColor = value;
            }
        }

        /// <summary>
        /// 当前选项卡索引
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                selectedIndex = value;
                for (var i = 0; i < tabs.Count; i++)
                {
                    tabs[i].IsActived = value == i;
                }

                try
                {
                    SelectedIndexChanged?.Invoke(value);
                }
                catch { }

                this.Invalidate();
            }
        }

        /// <summary>
        /// 当前选项卡总数
        /// </summary>
        public int TabCount
        {
            get
            {
                return tabCount;
            }

            set
            {
                tabCount = value;
            }
        }

        [Browsable(true), Category("Momo"), Description("是否双击关闭")]
        public bool DoubleClose
        {
            get
            {
                return doubleClose;
            }

            set
            {
                doubleClose = value;
            }
        }

        [Browsable(true), Category("Momo"), Description("是否始终显示关闭按钮，false状态下，仅鼠标悬浮时显示")]
        public bool AlwaysShowClose
        {
            get
            {
                return alwaysShowClose;
            }

            set
            {
                alwaysShowClose = value;
            }
        }

        [Browsable(true), Category("Momo"), Description("分割线颜色")]
        public Color SplitColor
        {
            get
            {
                return splitColor;
            }
            set
            {
                splitColor = value;
            }
        }

        /// <summary>
        /// 选中索引变更事件
        /// </summary>
        [Browsable(true), Category("Momo"), Description("选中索引变更事件")]
        public event Action<int> SelectedIndexChanged;

        [Browsable(true), Category("Momo"), Description("选项卡移除事件")]
        public event Action<int> TabRemoved;

        public MTabHeader()
        {
            this.BackColor = Color.Transparent;
            this.Font = new Font("微软雅黑", 10);
            this.ForeColor = Color.White;
            this.underLineColor = Color.FromArgb(189, 195, 199);
            this.splitColor = Color.FromArgb(189, 195, 199);
            this.underLineHeight = 1;
            this.activeTabColor = Color.FromArgb(155, 89, 182);
            this.tabColor = Color.FromArgb(22, 160, 133);
            this.doubleClose = true;
            this.alwaysShowClose = false;
            // 设置绘制样式
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer, true);
            base.UpdateStyles();
        }

        public int Add(string title, bool canClose = true)
        {
            tabs.Add(new Tab() { Title = title, IsActived = true, CanClose = canClose, IsHover = false });
            this.SelectedIndex = tabs.Count - 1;
            return this.SelectedIndex;
        }

        public void Remove(int index)
        {
            tabs.RemoveAt(index);
            TabRemoved?.Invoke(index);
            if (this.selectedIndex == index)
            {
                this.SelectedIndex = index - 1;
            }
            else
            {
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var x = 0;
            var y = 0;
            ImageAttributes ImgAtt = new ImageAttributes();
            ImgAtt.SetWrapMode(System.Drawing.Drawing2D.WrapMode.Clamp);
            for (var i = 0; i < this.tabs.Count; i++)
            {
                var tab = tabs[i];
                var fontSize = Size.Ceiling(e.Graphics.MeasureString(tab.Title, this.Font));
                tab.Rect = new Rectangle(x, y, fontSize.Width + 30, this.Height - underLineHeight);
                if (tab.CanClose)
                {
                    tab.CloseRect = new Rectangle(x + 30 + fontSize.Width - 18, (tab.Rect.Height - 16) / 2, 16, 16);
                }

                using (var brush = new SolidBrush(tab.IsActived ? this.activeTabColor : tabColor))
                {
                    e.Graphics.FillRectangle(brush, tab.Rect);
                }

                using (var brush = new SolidBrush(this.ForeColor))
                {
                    e.Graphics.DrawString(tab.Title, this.Font, brush, x + 3, (this.Height - this.underLineHeight - fontSize.Height) / 2);
                }

                if (tab.CanClose && (tab.IsHover || tab.IsActived || this.alwaysShowClose))
                {
                    e.Graphics.DrawImage(Properties.Resources.window_close, tab.CloseRect, 0, 0, Properties.Resources.window_close.Width, Properties.Resources.window_close.Height, GraphicsUnit.Pixel, ImgAtt);
                }

                // 分割线
                if (i > 0)
                {
                    using (var pen = new Pen(this.splitColor, 1))
                    {
                        e.Graphics.DrawLine(pen, tab.Rect.X, 5, tab.Rect.X, tab.Rect.Height - 10);
                    }
                }
                x += tab.Rect.Width;
            }

            y = this.Height - underLineHeight;
            using (var pen = new Pen(this.underLineColor, this.underLineHeight))
            {
                e.Graphics.DrawLine(pen, 0, y, this.Width, y);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            var flag = false;
            foreach (var item in tabs)
            {
                if (item.Rect.Contains(e.Location))
                {
                    item.IsHover = true;
                    flag = true;
                }
                else
                {
                    item.IsHover = false;
                }
            }

            this.Cursor = flag ? Cursors.Hand : Cursors.Default;
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            var flag = false;
            foreach (var item in tabs)
            {
                flag = flag || item.IsHover;
                item.IsHover = false;
            }

            if (flag)
            {
                this.Invalidate();
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            for (var i = 0; i < tabs.Count; i++)
            {
                var tab = tabs[i];
                if (tab.Rect.Contains(e.Location))
                {
                    if (tab.CloseRect.Contains(e.Location))
                    {
                        this.Remove(i);
                        break;
                    }

                    this.SelectedIndex = i;
                    break;
                }
            }
        }
    }
}
