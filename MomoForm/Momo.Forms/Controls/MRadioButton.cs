using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Momo.Forms
{
    public class MRadioButton : Control, IButtonControl
    {
        public MRadioButton()
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
            this.DefaultOutLineColor = Color.FromArgb(244, 130, 8);
            this.ActivedOutLineColor = Color.FromArgb(244, 130, 8);
            this.ActivedPointColor = Color.FromArgb(244, 130, 8);
            this.DisableOutLineColor = Color.FromArgb(244, 130, 8);
            this.DisablePointColor = Color.FromArgb(244, 130, 8);
        }
        [Category("Momo"), Description("选中属性变更事件"), DefaultValue(false)]
        public event EventHandler CheckedChanged;

        private void RaiseCheckedChanged()
        {
            if (this.Checked && this.Parent != null && this.Parent.Controls != null)
            {
                foreach (var item in this.Parent.Controls)
                {
                    if (item is MRadioButton && item != this)
                    {
                        var rb = item as MRadioButton;
                        if (rb.RadioGroup == this.RadioGroup)
                        {
                            rb.Checked = false;
                        }
                    }
                }
            }

            if (this.CheckedChanged != null)
            {
                this.CheckedChanged(this, EventArgs.Empty);
            }
        }

        private int ellipseSize = 22;
        [Category("Momo"), Description("圆圈大小"), DefaultValue(22)]
        public int EllipseSize { get { return this.ellipseSize; } set { this.ellipseSize = value; this.Invalidate(); } }

        private bool isChecked;
        [Category("Momo"), Description("是否选中"), DefaultValue(false)]
        public bool Checked { get { return this.isChecked; } set { this.isChecked = value; this.Invalidate(); } }

        [Category("Momo"), Description("默认状态外环颜色"), DefaultValue(typeof(Color), "244, 130, 8")]
        public Color DefaultOutLineColor { get; set; }

        [Category("Momo"), Description("激活/选中状态圆点颜色"), DefaultValue(typeof(Color), "244, 130, 8")]
        public Color ActivedPointColor { get; set; }

        [Category("Momo"), Description("激活/选中状态外环颜色"), DefaultValue(typeof(Color), "244, 130, 8")]
        public Color ActivedOutLineColor { get; set; }

        [Category("Momo"), Description("不可用状态圆点颜色"), DefaultValue(typeof(Color), "244, 130, 8")]
        public Color DisablePointColor { get; set; }

        [Category("Momo"), Description("不可用状态外环颜色"), DefaultValue(typeof(Color), "244, 130, 8")]
        public Color DisableOutLineColor { get; set; }

        [Category("Momo"), Description("不可用状态外环颜色"), DefaultValue(typeof(Color), "244, 130, 8")]
        public string RadioGroup { get; set; }
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
            if (!this.Checked)
            {
                this.Checked = true;
                this.RaiseCheckedChanged();
                this.Invalidate();
            }

            //this.Checked = !this.Checked;
            base.OnClick(e);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (var brush = new SolidBrush(this.BackColor))
            {
                pevent.Graphics.FillRectangle(brush, this.ClientRectangle);
            }

            var c1 = this.DefaultOutLineColor;
            var c2 = Color.Empty;

            if (this.status == ButtonStatus.Hover)
            {
                c1 = this.ActivedOutLineColor;
            }

            if (this.Checked)
            {
                c1 = this.ActivedOutLineColor;
                c2 = this.ActivedPointColor;
            }

            if (!this.Enabled)
            {
                c1 = this.DisableOutLineColor;
                c2 = this.DisablePointColor;
            }

            // 外圈
            using (var pen = new Pen(c1, 2))
            {
                pevent.Graphics.DrawEllipse(pen, 1, (this.Height - ellipseSize - 2) / 2, ellipseSize - 2, ellipseSize - 2);
            }

            if (!c2.IsEmpty)
            {
                // 小点
                using (var brush = new SolidBrush(c2))
                {
                    var pointSize = ellipseSize / 4;
                    pevent.Graphics.FillEllipse(brush, new Rectangle((ellipseSize - pointSize) / 2 - 2, (this.Height - pointSize) / 2 - 4, (ellipseSize - 2) / 2, (ellipseSize - 2) / 2));
                }
            }

            var size = TextRenderer.MeasureText(this.Text, this.Font);
            using (var brush = new SolidBrush(this.Enabled ? this.ForeColor : this.DisableOutLineColor))
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
