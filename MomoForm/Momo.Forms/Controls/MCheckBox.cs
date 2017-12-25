using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Momo.Forms
{
    public class MCheckBox : Control, IButtonControl
    {
        public MCheckBox()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.AutoSize = false;
            this.isChecked = false;
            this.Cursor = Cursors.Hand;
            this.Enabled = true;
            this.DefaultColor = Color.FromArgb(244, 130, 8);
            this.ActivedColor = Color.FromArgb(244, 130, 8);
            this.DisableColor = Color.FromArgb(244, 130, 8);
        }
        [Category("Momo"), Description("选中属性变更事件"), DefaultValue(false)]
        public event EventHandler CheckedChanged;
        
        private bool isChecked;
        [Category("Momo"), Description("是否选中"), DefaultValue(false)]
        public bool Checked { get { return this.isChecked; } set { this.isChecked = value; this.Invalidate(); } }

        private int ellipseSize = 22;
        [Category("Momo"), Description("圆圈大小"), DefaultValue(22)]
        public int EllipseSize { get { return this.ellipseSize; } set { this.ellipseSize = value; this.Invalidate(); } }

        [Category("Momo"), Description("默认状态外环颜色"), DefaultValue(typeof(Color), "244, 130, 8")]
        public Color DefaultColor { get; set; }

        [Category("Momo"), Description("激活/选中状态外环颜色"), DefaultValue(typeof(Color), "244, 130, 8")]
        public Color ActivedColor { get; set; }

        [Category("Momo"), Description("不可用状态外环颜色"), DefaultValue(typeof(Color), "244, 130, 8")]
        public Color DisableColor { get; set; }

        private ButtonStatus status;
        private void SetStatus(ButtonStatus status)
        {
            this.status = status;
            this.Invalidate();
        }

        // 这个方法，会在AutoSize时调用，用于计算大小自适应
        public override Size GetPreferredSize(Size proposedSize)
        {
            Size clientSize = TextRenderer.MeasureText(this.Text, this.Font);
            Size size2 = this.SizeFromClientSize(clientSize);
            size2.Width += 28;
            size2.Height += 6;
            if (size2.Height < 20)
            {
                size2.Height = 26;
            }
            return size2;
        }

        protected override Size DefaultSize
        {
            get
            {
                return new Size(100, 26);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this.SetStatus(ButtonStatus.Hover);
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.SetStatus(ButtonStatus.Default);
            base.OnMouseLeave(e);
        }

        protected override void OnClick(EventArgs e)
        {
            this.Checked = !this.Checked;
            this.CheckedChanged?.Invoke(this, e);
            base.OnClick(e);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (var brush = new SolidBrush(this.BackColor))
            {
                pevent.Graphics.FillRectangle(brush, this.ClientRectangle);
            }

            var c1 = this.DefaultColor;

            if (this.status == ButtonStatus.Hover)
            {
                c1 = this.ActivedColor;
            }

            if (this.Checked)
            {
                c1 = this.ActivedColor;
            }

            if (!this.Enabled)
            {
                c1 = this.DisableColor;
            }

            var top = (this.Height - 16) / 2;
            // 外圈
            //using (var ellipsePen = new Pen(c1, 3))
            //{
            //    pevent.Graphics.DrawLine(ellipsePen, 6, top, 22, top);//上
            //    pevent.Graphics.DrawLine(ellipsePen, 6, top + 16, 22, top + 16);//下
            //    pevent.Graphics.DrawLine(ellipsePen, 6, top, 6, top + 16);//左
            //    pevent.Graphics.DrawLine(ellipsePen, 22, top, 22, top + 16);//右
            //}


            if (this.Checked)
            {
                using (var brush = new SolidBrush(c1))
                {
                    pevent.Graphics.FillEllipse(brush, 0, (this.Height - ellipseSize) / 2, ellipseSize, ellipseSize);
                }

                ImageAttributes imgAtt = new ImageAttributes();
                imgAtt.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                var image = Properties.Resources.ok;

                var rec = new Rectangle((ellipseSize - 16) / 2 + 2, top, 16, 16);

                pevent.Graphics.DrawImage(image, rec, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imgAtt);
            }
            else
            {
                using (var pen = new Pen(c1, 2))
                {
                    pevent.Graphics.DrawEllipse(pen, 1, (this.Height - ellipseSize - 2) / 2 + 2, ellipseSize - 2, ellipseSize - 2);
                }
            }

            var size = TextRenderer.MeasureText(this.Text, this.Font);
            using (var brush = new SolidBrush(this.Enabled ? this.ForeColor : this.DisableColor))
            {
                pevent.Graphics.DrawString(this.Text, this.Font, brush, ellipseSize + 4, (this.Height - size.Height) / 2);
            }
        }

        public DialogResult DialogResult { get; set; }

        public void NotifyDefault(bool value)
        {

        }

        public void PerformClick()
        {
            if (base.CanSelect)
            {
                this.SetStatus(ButtonStatus.Default);
                this.OnClick(EventArgs.Empty);
            }
        }
    }
}
