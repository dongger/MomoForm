using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Momo.Forms
{
    public partial class MGroupBox : Panel
    {
        public MGroupBox()
        {
            InitializeComponent();
            this.titleColor = Color.Black;            
            this.titleFont  = new Font("微软雅黑", 10);
        }

        [Browsable(true), Description("标题"), Category("Momo")]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        [Browsable(true), Description("边框颜色"), Category("Momo")]
        public Color BorderColor { get; set; }

        private Color titleColor;
        [Category("Momo"), Description("标题颜色")]
        public Color TitleColor { get { return this.titleColor; } set { this.titleColor = value; this.Invalidate(); } }

        private Font titleFont;
        [Category("Momo"), Description("标题栏文字字体样式")]
        public Font TitleFont { get { return this.titleFont; } set { this.titleFont = value; this.Invalidate(); } }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Rectangle clientRectangle = base.ClientRectangle;
            Color disabledColor = Color.FromArgb(189, 195, 199);
            Pen pen2 = new Pen(this.BorderColor == Color.Empty ? Color.White : this.BorderColor);
            try
            {

                Size size;
                using (Brush brush = new SolidBrush(this.TitleColor))
                {
                    using (StringFormat format = new StringFormat())
                    {
                        size = Size.Ceiling(graphics.MeasureString(this.Text, this.TitleFont, clientRectangle.Width, format));
                        clientRectangle.X = (this.Width - size.Width - 20) / 2 + 10;
                        clientRectangle.Width = size.Width + 20;
                        clientRectangle.Height = size.Height;
                        clientRectangle.Y = 10;
                        if (base.Enabled)
                        {
                            graphics.DrawString(this.Text, this.TitleFont, brush, clientRectangle, format);
                        }
                        else
                        {
                            ControlPaint.DrawStringDisabled(graphics, this.Text, this.TitleFont, disabledColor, clientRectangle, format);
                        }
                    }
                }

                int top = base.FontHeight / 2 + 10;
                //graphics.DrawLine(pen2, 0, top, base.Width, top);// 上边线
                graphics.DrawLine(pen2, 1, top, clientRectangle.X - 10, top);// 上边线
                graphics.DrawLine(pen2, clientRectangle.X + clientRectangle.Width - 10, top, base.Width - 2, top);// 上边线

                graphics.DrawLine(pen2, 1, base.Height - 2, base.Width - 2, base.Height - 2);//底部
                graphics.DrawLine(pen2, base.Width - 2, top, base.Width - 2, base.Height - 2);//右边
                graphics.DrawLine(pen2, 1, top, 1, base.Height - 2);//左边

            }
            finally
            {
                pen2.Dispose();
            }
        }

    }
}
