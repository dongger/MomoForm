using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;

using System.Windows.Forms;

namespace Momo.Forms
{
    [DefaultEvent("Click")]
    [Designer(typeof(System.Windows.Forms.Design.ScrollableControlDesigner))]
    public partial class MSideBarMenu : MBorderControl
    {
        public MSideBarMenu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 单击事件
        /// </summary>
        [Browsable(true), Category("Momo"), Description("当前选中事件")]
        internal event Action<MSideBarMenu> OnSelected;

        /// <summary>
        /// 获取或设置标题颜色
        /// </summary>
        [Browsable(true), Category("Momo"), Description("标题")]
        public string Title
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                lblTitle.Text = value;
            }
        }

        private bool selected;

        /// <summary>
        /// 是否当前项
        /// </summary>
        [Browsable(true), Category("Momo"), Description("是否当前项")]
        public bool Selected
        {
            get { return selected; }
            set
            {
                //this.pictureBox2.Visible = value;  这个地方，暂时不需要这个功能
                selected = value;
            }
        }

        private Image image;
        /// <summary>
        /// 获取或设置图片
        /// </summary>
        [Browsable(true), Category("Momo"), Description("图片")]
        public Image Image
        {
            get { return image; }
            set
            {
                image = value;
                this.pictureBox1.Image = value;
            }
        }

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                lblTitle.ForeColor = value;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.pictureBox2.Location = new Point(this.Width - this.pictureBox2.Width - 3, 12);
        }

        private new Font Font { get; set; }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.BackColor = Color.FromArgb(127, 140, 141);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.BackColor = Color.FromArgb(52, 73, 94);
        }

        private void lblTitle_MouseEnter(object sender, EventArgs e)
        {
            this.OnMouseEnter(e);
        }

        private void lblTitle_MouseLeave(object sender, EventArgs e)
        {
            this.OnMouseLeave(e);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            this.RaiseClickEvent(this, e);
        }

        private void RaiseClickEvent(object sender, EventArgs e)
        {
            this.Selected = true;
            this.RaiseSelectedEvent(sender, e);
            if (this.Click != null)
            {
                this.Click(sender, e);
            }
        }

        private void RaiseSelectedEvent(object sender, EventArgs e)
        {
            if (this.OnSelected != null)
            {
                this.OnSelected(this);
            }
        }

        /// <summary>
        /// 单击事件
        /// </summary>
        [Browsable(true), Category("Momo"), Description("单击事件")]
        public new event EventHandler Click;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.RaiseClickEvent(this, e);
        }
    }
}
