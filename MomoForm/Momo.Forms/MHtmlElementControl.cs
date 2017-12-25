using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Momo.Forms
{
    public class MHtmlElementControl : Control
    {
        public MHtmlElementControl()
        {
            // 设置绘制样式
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer, true);
            base.UpdateStyles();
            this.Root = new MHtmlElement();
            this.Root.Items = new List<MHtmlElement>();
        }

        private readonly List<MHtmlElement> Hovers = new List<MHtmlElement>();
        private readonly List<MHtmlElement> Clicks = new List<MHtmlElement>();
        private readonly List<MHtmlElement> Leaves = new List<MHtmlElement>();

        protected override Size DefaultSize
        {
            get
            {
                return new Size(300, 100);
            }
        }

        //[Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(true), Category("Momo"), Description("展示的内容，xml格式")]
        public MHtmlElement Root { get; set; }

        private void InitElement(MHtmlElement parent, MHtmlElement element)
        {
            element.Parent = parent;

            if (element.ForeColor.IsEmpty)
            {
                element.ForeColor = parent.ForeColor;
            }

            if (element.Font == null)
            {
                element.Font = parent.Font;
            }

            if (element.Rectangle.IsEmpty)
            {
                element.Location = new Point(parent.X + parent.Padding.Left, parent.Y + parent.Padding.Top);
                element.Size = new Size(parent.Width - parent.X - parent.Padding.Right, parent.Height - parent.Y - parent.Padding.Bottom);
            }

            if (element.RelativePosition.IsEmpty)
            {
                element.RelativePosition = new Point(parent.RelativePosition.X + parent.Padding.Left + element.X, parent.RelativePosition.Y + parent.Padding.Top + element.Y);
            }


            if (element.Items != null)
            {
                foreach (var item in element.Items)
                {
                    this.InitElement(element, item);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.Root == null)
            {
                base.OnPaint(e);
                return;
            }

            Hovers.Clear();
            Clicks.Clear();
            Leaves.Clear();

            var container = new MHtmlElement();
            container.Align = TextAlignment.MiddleCenter;
            container.BackColor = this.BackColor;
            container.BackColorGradient = Color.Empty;
            container.Items = null;
            container.Border = null;
            container.ForeColor = this.ForeColor;
            container.Gradient = GradientMode.None;
            container.ID = this.Name;
            container.Radius = 0;
            container.RadiusMode = RadiusMode.None;
            container.Location = new Point(0, 0);
            container.Size = new Size(this.Width, this.Height);

            container.Text = this.Text;

            this.InitElement(container, Root);

            this.DrawElement(this.Root, e.Graphics);
        }

        internal void DrawElement(MHtmlElement element, Graphics graphics)
        {
            var rect = new Rectangle(element.RelativePosition, element.Rectangle.Size);

            if (rect.IsEmpty)
            {
                return;
            }

            #region 边框和背景
            // 圆角模式，忽略边框样式
            if (element.RadiusMode != RadiusMode.None && element.Radius > 0)
            {
                var color1 = element.BackColor.IsEmpty ? Color.Transparent : element.BackColor;
                var color2 = element.BackColorGradient;
                RadiusDrawable.DrawRadius(graphics, rect, element.RadiusMode, element.Radius, element.BackColor, element.BackColorGradient, element.Gradient, element.Border.Color, element.Border.Width);
            }
            // 有边框
            else if (element.Border != null && !element.Border.IsEmpty)
            {
                if (!element.BackColor.IsEmpty)
                {
                    using (var brush = new SolidBrush(element.BackColor))
                    {
                        graphics.FillRectangle(brush, element.Rectangle);
                    }
                }

                var bl = element.Border.Left == 0 ? element.Border.Width : element.Border.Left;
                var bt = element.Border.Top == 0 ? element.Border.Width : element.Border.Top;
                var br = element.Border.Right == 0 ? element.Border.Width : element.Border.Right;
                var bb = element.Border.Bottom == 0 ? element.Border.Width : element.Border.Bottom;

                using (var pen = new Pen(element.Border.Color))
                {
                    pen.DashStyle = element.Border.GetDashStyle();
                    if (bl > 0)
                    {
                        pen.Width = bl;
                        graphics.DrawLine(pen, rect.X, rect.Y, rect.X, rect.Y + rect.Height);
                    }

                    if (bt > 0)
                    {
                        pen.Width = bt;
                        graphics.DrawLine(pen, rect.X, rect.Y, rect.X + rect.Width, rect.Y);
                    }

                    if (br > 0)
                    {
                        pen.Width = br;
                        graphics.DrawLine(pen, rect.X + rect.Width, rect.Y, rect.X + rect.Width, rect.Y + rect.Height);
                    }

                    if (bb > 0)
                    {
                        pen.Width = bb;
                        graphics.DrawLine(pen, rect.X, rect.Y + rect.Height, rect.X + rect.Width, rect.Y + rect.Height);
                    }
                }
            }
            else
            {
                // 没有边框，直接处理背景
                if (!element.BackColor.IsEmpty)
                {
                    using (var brush = new SolidBrush(element.BackColor))
                    {
                        graphics.FillRectangle(brush, rect);
                    }
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(element.Text))
            {
                var textRect = new Rectangle(rect.X + element.Padding.Left, rect.Y + element.Padding.Top, rect.Width - element.Padding.Left - element.Padding.Right, rect.Height - element.Padding.Top - element.Padding.Bottom);
                GDIHelper.DrawString(graphics, textRect, element.Font, element.Text, element.ForeColor, element.Align);
            }

            if (element.HasHover)
            {
                Hovers.Add(element);
            }

            if (element.HasClick)
            {
                Clicks.Add(element);
            }

            if (element.HasLeave)
            {
                Leaves.Add(element);
            }

            if (element.Items != null)
            {
                foreach (var item in element.Items)
                {
                    this.DrawElement(item, graphics);
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            foreach (var item in this.Hovers)
            {
                if (item.Hovered)
                {
                    item.Hovered = false;
                    item.RaiseLeave(e);
                }
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            foreach (var item in this.Clicks)
            {
                if (item.Rectangle.Contains(e.Location))
                {
                    item.RaiseClick(e);
                    break;
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            bool hovered = false;
            foreach (var item in this.Hovers)
            {
                if (item.Rectangle.Contains(e.Location))
                {
                    if (!item.Hovered)
                    {
                        item.Hovered = true;
                        item.RaiseHover(e);
                        hovered = true;
                    }
                }
                else if (item.Hovered)
                {
                    item.Hovered = false;
                    item.RaiseLeave(e);
                }
            }

            this.Cursor = hovered ? Cursors.Hand : Cursors.Default;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
        }

        public Size MeasureString(string text,Font font)
        {
            using (var g = this.CreateGraphics())
            {
                var sizeF = g.MeasureString(text, font);
                return new Size((int)sizeF.Width, (int)sizeF.Height);
            }
        }
    }

    #region 类型定义
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MHtmlElement
    {
        public MHtmlElement()
        {
            this.Align = TextAlignment.MiddleCenter;
            this.Border = new MHtmlBorder();
            this.Font = SystemFonts.DefaultFont;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<MHtmlElement> Items { get; set; }

        internal Rectangle Rectangle { get { return new Rectangle(this.Location, this.Size); } }

        internal int X { get { return Rectangle.X; } }
        internal int Y { get { return Rectangle.Y; } }
        internal int Width { get { return Rectangle.Width; } }
        internal int Height { get { return Rectangle.Height; } }

        public Point Location { get; set; }

        public MHtmlElement Parent { get; set; }

        public Size Size { get; set; }

        public Padding Padding { get; set; }

        public Font Font { get; set; }

        public string Text { get; set; }

        public Color ForeColor { get; set; }

        public Color BackColor { get; set; }

        public Color BackColorGradient { get; set; }

        public GradientMode Gradient { get; set; }

        public TextAlignment Align { get; set; }

        public RadiusMode RadiusMode { get; set; }

        public int Radius { get; set; }

        public MHtmlBorder Border { get; set; }

        internal Point RelativePosition { get; set; }

        public string ID { get; set; }

        public event EventHandler Click;

        public event EventHandler Hover;

        public event EventHandler Leave;

        internal bool Hovered { get; set; }

        public bool HasHover
        {
            get { return Hover != null; }
        }
        public bool HasClick
        {
            get { return Click != null; }
        }
        public bool HasLeave
        {
            get { return Leave != null; }
        }

        internal void RaiseClick(EventArgs args)
        {
            Click?.Invoke(this, args);
        }

        internal void RaiseLeave(EventArgs args)
        {
            Leave?.Invoke(this, args);
        }

        internal void RaiseHover(EventArgs args)
        {
            Hover?.Invoke(this, args);
        }
    }

    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MHtmlBorder
    {
        internal bool IsEmpty
        {
            get
            {
                var flag = this.Type != MHtmlBorderType.None;
                if (!flag)
                {
                    return flag;
                }

                if (Color.IsEmpty)
                {
                    return true;
                }

                flag = this.Width != 0 || this.Top != 0 || this.Bottom != 0 || this.Right != 0 || this.Left != 0;
                return !flag;
            }
        }

        public int Width { get; set; }
        public MHtmlBorderType Type { get; set; }

        public int Top { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }
        public Color Color { get; set; }

        internal DashStyle GetDashStyle()
        {
            if (Type == MHtmlBorderType.Dash)
            {
                return DashStyle.Dash;
            }
            else
            {
                return DashStyle.Solid;
            }
        }
    }

    public enum MHtmlRadiusModel
    {
        None = 0,
        LeftTop = 1,
        TopRight = 2,
        RightBottom = 4,
        BottomLeft = 16,
        All = 32
    }

    public enum MHtmlGradientMode
    {
        /// <summary>
        /// 左到右
        /// </summary>
        Horizontal = 0,

        /// <summary>
        /// 上到下
        /// </summary>
        Vertical = 1,

        /// <summary>
        /// 正向对角线
        /// </summary>
        Forward = 2,

        /// <summary>
        /// 反向对角线
        /// </summary>
        Backward = 3
    }

    public enum MHtmlAlignment
    {
        Left = 1,
        Top = 2,
        Right = 4,
        Bottom = 16,
        Center = 32
    }
    #endregion
}
