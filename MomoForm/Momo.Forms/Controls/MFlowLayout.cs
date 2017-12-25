using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;

using System.Windows.Forms;

namespace Momo.Forms
{
    public partial class MFlowLayout : MPanel
    {
        public MFlowLayout()
        {
            InitializeComponent();
            this.ItemWidth = 60;
        }

        /// <summary>
        /// 分割线宽度，间距
        /// </summary>
        [Browsable(true), Category("Momo"), Description("分割线宽度，间距")]
        public int GridWidth { get; set; }

        [Browsable(true), Category("Momo"), Description("每项元素的宽度（垂直时为高度，水平时为宽度）")]
        public int ItemWidth { get; set; }

        private ELayoutDirection layoutDirection;
        [Browsable(true), Category("Momo"), Description("布局方向")]
        public ELayoutDirection LayoutDirection
        {
            get { return this.layoutDirection; }
            set
            {
                this.layoutDirection = value;
                if (value == ELayoutDirection.Horizontal)
                {
                    this.VerticalScroll.Enabled = true;
                }
                else
                {
                    this.HorizontalScroll.Enabled = true;
                }
                DoLayout();
            }
        }

        protected override ControlCollection CreateControlsInstance()
        {
            return new FlowLayoutControlCollection(this);
        }

        private bool layouting;
        internal void DoLayout()
        {
            if (this.layouting)
            {
                return;
            }
            this.SuspendLayout();
            this.layouting = true;
            if (this.layoutDirection == ELayoutDirection.Horizontal)
            {
                // 横向布局，计算列数
                var columnCount = this.Width / (this.ItemWidth + this.GridWidth);

                int row = 0;
                int c = 0;
                int[] columns = new int[columnCount];

                foreach (Control item in this.Controls)
                {
                    item.Location = new Point(c * this.ItemWidth + c * this.GridWidth, columns[c]);
                    item.Width = this.ItemWidth;
                    columns[c] += item.Height + GridWidth;
                    //item.Invalidate();
                    c++;
                    if (c == columnCount)
                    {
                        c = 0;
                        row++;
                    }
                }
            }
            else
            {
                // 纵向布局，计算行
                var rowCount = this.Height / (this.ItemWidth + this.GridWidth);

                int column = 0;
                int row = 0;
                int[] rows = new int[rowCount];

                foreach (Control item in this.Controls)
                {
                    item.Location = new Point(rows[row], row * this.ItemWidth + row * this.GridWidth);
                    item.Height = this.ItemWidth;
                    rows[row] += item.Width + GridWidth;
                    //item.Invalidate();
                    row++;
                    if (row == rowCount)
                    {
                        row = 0;
                        column++;
                    }
                }
            }

            this.Invalidate();
            this.layouting = false;
            this.ResumeLayout(false);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.DoLayout();
        }
    }

    public enum ELayoutDirection
    {
        Horizontal = 0,
        Vertical = 1
    }

    public class FlowLayoutControlCollection : Control.ControlCollection
    {
        private MFlowLayout owner;
        public FlowLayoutControlCollection(MFlowLayout owner)
            : base(owner)
        {
            this.owner = owner;
        }

        public override void Add(Control value)
        {
            //value.ContextMenuStrip = this.Owner.ContextMenuStrip;
            value.SizeChanged += value_SizeChanged;
            base.Add(value);
            this.owner.DoLayout();
        }

        void value_SizeChanged(object sender, EventArgs e)
        {
            this.owner.DoLayout();
        }

        public override void Remove(Control value)
        {
            base.Remove(value);
            this.owner.DoLayout();
        }
    }
}
