using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Momo.Forms.Controls
{
    public sealed class MSplit : Control
    {
        protected override Size DefaultSize
        {
            get
            {
                return new Size(100, 1);
            }
        }

        private SplitDirection direction = SplitDirection.Horizontal;

        [Browsable(true), Category("Momo"), Description("分割方向"), DefaultValue(typeof(ContentAlignment), "32")]
        public SplitDirection Direction
        {
            get { return this.direction; }
            set
            {
                this.direction = value;
                this.Size = new Size(this.Height, this.Width);
                this.Invalidate();
            }
        }
        public MSplit()
        {
        }
    }
}
