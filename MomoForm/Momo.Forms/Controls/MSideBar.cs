using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;

using System.Windows.Forms;

namespace Momo.Forms
{
    [ToolboxBitmap(typeof(TabControl))]
    [Designer(typeof(MSideBarDesigner))]
    public partial class MSideBar : UserControl
    {
        public MSideBar()
        {
            InitializeComponent();
        }
        
        private bool expansion = true;
        private int oldWidth;

        public int Count { get; private set; }

        public MSideBarMenu SelectedMenu { get; set; }

        public int SelectedIndex { get; set; }

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


        [Browsable(true), Category("Momo"), Description("标题")]
        public Color TitleColor { get { return pnTitle.BackColor; } set { pnTitle.BackColor = value; } }

        [Browsable(true), Category("Momo"), Description("标题")]
        public Color TitleForeColor { get { return lblTitle.ForeColor; } set { lblTitle.ForeColor = value; } }

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

        private new Font Font { get; set; }

        internal virtual void ReLocation()
        {
            int y = 40;
            foreach (Control item in this.Controls)
            {
                if (item is MSideBarMenu && item.Visible)
                {
                    item.Location = new System.Drawing.Point(0, y);
                    y += item.Height;
                }
            }
        }
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            if (e.Control is MSideBarMenu)
            {
                var item = e.Control as MSideBarMenu;
                item.OnSelected -= item_OnSelected;
                item.OnSelected += item_OnSelected;
                item.VisibleChanged += item_VisibleChanged;
                ReLocation();
            }
        }

        void item_VisibleChanged(object sender, EventArgs e)
        {
            this.ReLocation();
        }

        void item_OnSelected(MSideBarMenu obj)
        {
            foreach (Control item in this.Controls)
            {
                if (item is MSideBarMenu)
                {
                    if (item == obj)
                    {
                        continue;
                    }

                    (item as MSideBarMenu).Selected = false;
                }
            }
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            if (e.Control is MSideBarMenu)
            {
                ReLocation();
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            foreach (Control item in this.Controls)
            {
                if (item is MSideBarMenu)
                {
                    item.Width = this.Width;
                }
            }
        }

        private void pnTitle_Click(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            if (this.expansion)
            {
                this.oldWidth = this.Width;
                this.Width = 32;
                this.expansion = false;
            }
            else
            {
                this.Width = this.oldWidth;
                this.expansion = true;
            }
        }
    }
}
