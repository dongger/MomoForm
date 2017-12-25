using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Momo.Forms
{
    public partial class MBorderControl : UserControl
    {
        public MBorderControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取或设置左边框颜色
        /// </summary>
        [Browsable(true), Category("Momo"), Description("获取或设置左边框颜色")]
        public Color BorderLeftColor { get; set; }

        /// <summary>
        /// 获取或设置右边框颜色
        /// </summary>
        [Browsable(true), Category("Momo"), Description("获取或设置右边框颜色")]
        public Color BorderRightColor { get; set; }

        /// <summary>
        /// 获取或设置顶部边框颜色
        /// </summary>
        [Browsable(true), Category("Momo"), Description("获取或设置顶部边框颜色")]
        public Color BorderTopColor { get; set; }

        /// <summary>
        /// 获取或设置底部边框颜色
        /// </summary>
        [Browsable(true), Category("Momo"), Description("获取或设置底部边框颜色")]
        public Color BorderBottomColor { get; set; }

        /// <summary>
        /// 获取或设置左边框样式
        /// </summary>
        [Browsable(true), Category("Momo"), Description("获取或设置左边框样式")]
        public ButtonBorderStyle BorderLeftStyle { get; set; }

        /// <summary>
        /// 获取或设置底部边框样式
        /// </summary>
        [Browsable(true), Category("Momo"), Description("获取或设置底部边框样式")]
        public ButtonBorderStyle BorderBottomStyle { get; set; }

        /// <summary>
        /// 获取或设置顶部边框样式
        /// </summary>
        [Browsable(true), Category("Momo"), Description("获取或设置顶部边框样式")]
        public ButtonBorderStyle BorderTopStyle { get; set; }

        /// <summary>
        /// 获取或设置右边框样式
        /// </summary>
        [Browsable(true), Category("Momo"), Description("获取或设置右边框样式")]
        public ButtonBorderStyle BorderRightStyle { get; set; }

        /// <summary>
        /// 获取或设置左边框宽度
        /// </summary>
        [Browsable(true), Category("Momo"), Description("获取或设置左边框宽度")]
        public int BorderLeftWidth { get; set; }

        /// <summary>
        /// 获取或设置底部边框宽度
        /// </summary>
        [Browsable(true), Category("Momo"), Description("获取或设置底部边框宽度")]
        public int BorderBottomWidth { get; set; }

        /// <summary>
        /// 获取或设置顶部边框宽度
        /// </summary>
        [Browsable(true), Category("Momo"), Description("获取或设置顶部边框宽度")]
        public int BorderTopWidth { get; set; }

        /// <summary>
        /// 获取或设置右边框宽度
        /// </summary>
        [Browsable(true), Category("Momo"), Description("获取或设置右边框宽度")]
        public int BorderRightWidth { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics,
                               this.ClientRectangle,
                               this.BorderLeftColor,//7f9db9
                               this.BorderLeftWidth,
                               this.BorderLeftStyle,
                               this.BorderTopColor,
                               this.BorderTopWidth,
                               this.BorderTopStyle,
                               this.BorderRightColor,
                               this.BorderRightWidth,
                               this.BorderRightStyle,
                               this.BorderBottomColor,
                               this.BorderBottomWidth,
                               this.BorderBottomStyle);
        }
    }
}
