using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Momo.Forms
{
    /// <summary>
    /// 基础的画板
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class Palette
    {
        public Palette() { }
        public Palette(Control container)
        {
            this.Container = container;
            this.visible = true;
        }

        private bool suspend;
        public void SuspendLayout()
        {
            suspend = true;
        }

        public void PerformLayout()
        {
            suspend = false;
        }

        public Control Container { get; private set; }

        private Rectangle rectangle;
        /// <summary>
        /// 绘制区域
        /// </summary>        
        public virtual Rectangle Rectangle { get { return rectangle; } set { this.rectangle = value; this.Invalidate(); } }

        /// <summary>
        /// 绘制区域大小
        /// </summary>
        public Size Size
        {
            get
            {
                return this.Rectangle.Size;
            }
            set
            {
                this.Rectangle = new Rectangle(this.Rectangle.Location, value);
                this.Invalidate();
            }
        }

        /// <summary>
        /// 绘制位置
        /// </summary>
        public Point Location
        {
            get
            {
                return this.Rectangle.Location;
            }
            set
            {
                this.Rectangle = new Rectangle(value, this.Rectangle.Size);
                this.Invalidate();
            }
        }

        /// <summary>
        /// 高度
        /// </summary>
        public int Height
        {
            get
            {
                return this.Rectangle.Height;
            }
            set
            {
                this.Rectangle = new Rectangle(this.X, this.Y, this.Width, value);
                this.Invalidate();
            }
        }

        /// <summary>
        /// 宽度
        /// </summary>
        public int Width
        {
            get
            {
                return this.Rectangle.Width;
            }
            set
            {
                this.Rectangle = new Rectangle(this.X, this.Y, value, this.Height);
                this.Invalidate();
            }
        }

        /// <summary>
        /// X坐标
        /// </summary>
        public int X
        {
            get
            {
                return this.Rectangle.X;
            }
            set
            {
                this.Rectangle = new Rectangle(value, this.Y, this.Width, this.Height);
                this.Invalidate();
            }
        }

        /// <summary>
        /// Y坐标
        /// </summary>
        public int Y
        {
            get
            {
                return this.Rectangle.Y;
            }
            set
            {
                this.Rectangle = new Rectangle(this.X, value, this.Width, this.Height);
                this.Invalidate();
            }
        }

        private Padding padding;

        /// <summary>
        /// Padding
        /// </summary>
        public virtual Padding Padding { get { return this.padding; } set { this.padding = value; this.Invalidate(); } }

        private bool visible;
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Visible { get { return this.visible; } set { this.visible = value; this.Invalidate(); } }

        /// <summary>
        /// 重绘当前画板
        /// </summary>
        public void Invalidate()
        {
            if (!suspend && this.Container != null)
            {
                this.Container.Invalidate(this.Rectangle);
            }
        }

        /// <summary>
        /// 绘制方法
        /// </summary>
        /// <param name="graphics"></param>
        public abstract void Draw(Graphics graphics);

        protected virtual void MouseMoveIn(MouseEventArgs e) { }
        protected virtual void MouseMoveOut(MouseEventArgs e) { }
        protected virtual void MouseClick(MouseEventArgs e) { }
        protected virtual void MouseDown(MouseEventArgs e) { }

        public virtual void OnMouseMove(MouseEventArgs e)
        {
            if (this.Rectangle.Contains(e.Location))
            {
                this.MouseMoveIn(e);
            }
            else
            {
                this.MouseMoveOut(e);
            }
        }

        public virtual void OnMouseClick(MouseEventArgs e)
        {
            if (this.Rectangle.Contains(e.Location))
            {
                this.MouseClick(e);
            }
        }

        public virtual void OnMouseDown(MouseEventArgs e)
        {
            if (this.Rectangle.Contains(e.Location))
            {
                this.MouseDown(e);
            }
        }
    }
}
