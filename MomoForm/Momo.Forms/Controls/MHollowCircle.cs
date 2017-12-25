using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;

namespace Momo.Forms.Controls
{
    /// <summary>
    /// 空心圆
    /// </summary>
    public partial class MHollowCircle : UserControl
    {
        public MHollowCircle()
        {
            InitializeComponent();
        }

        private string text;

        public override string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
                this.Invalidate();
            }
        }

        private int circleWidth;

        [Browsable(true), Category("Momo"), Description("圆环宽度"), DefaultValue(10)]
        public int CircleWidth
        {
            get { return this.circleWidth; }
            set { this.circleWidth = value; this.Invalidate(); }
        }

        private Color circleColor;
        [Browsable(true), Category("Momo"), Description("圆环颜色"), DefaultValue(10)]
        public Color CircleColor
        {
            get { return this.circleColor; }
            set { this.circleColor = value; this.Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
        }
    }
}
