using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Momo.Forms
{
    public partial class MTextBox : UserControl
    {
        public MTextBox()
        {
            InitializeComponent();
            this.ActivtedBorderColor = Color.FromArgb(74, 182, 1);
            this.BorderColor = Color.FromArgb(189, 195, 199);
            this.borderPanel1.BorderWidth = 1;
            this.textModel = TextModel.Text;
            this.digits = 0;
        }

        public new event EventHandler TextChanged;

        private Color borderColor;
        [Category("Momo"), Description("默认状态下的边框颜色"), DefaultValue(typeof(Color), "236, 240, 241")]
        public Color BorderColor { get { return borderColor; } set { this.borderPanel1.BorderColor = borderColor = value; this.Invalidate(); } }

        private TextModel textModel;

        [Category("Momo"), Description("文本格式"), DefaultValue(typeof(TextModel), "Text")]
        public TextModel TextModel { get { return textModel; } set { textModel = value; } }

        private int digits;
        [Category("Momo"), Description("小数位数"), DefaultValue(typeof(int), "0")]
        public int Digits { get { return digits; } set { digits = value; } }

        private bool allowMinus = false;
        [Category("Momo"), Description("是否允许负数"), DefaultValue(typeof(bool), "false")]
        public bool AllowMinus { get { return allowMinus; } set { allowMinus = value; } }

        [Category("Momo"), Description("激活状态下的边框颜色"), DefaultValue(typeof(Color), "127, 140, 141")]
        public Color ActivtedBorderColor { get; set; }

        /// <summary>
        /// 获取或设置水印文字
        /// </summary>
        [Browsable(true), Category("Momo"), Description("获取或设置水印文字")]
        public string WaterText
        {
            get { return this.lblWater.Text; }
            set
            {
                this.lblWater.Text = value;
                this.lblWater.Visible = this.Text.Trim() == string.Empty;
                this.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置文本最大长度
        /// </summary>
        [Browsable(true), Category("Momo"), Description("获取或设置文本最大长度")]
        public int MaxLength
        {
            get { return this.txtText.MaxLength; }
            set { this.txtText.MaxLength = value; }
        }
        /// <summary>
        /// 获取或设置只读
        /// </summary>
        [Browsable(true), Category("Momo"), Description("获取或设置只读")]
        public bool ReadOnly
        {
            get { return this.txtText.ReadOnly; }
            set { this.txtText.ReadOnly = value; }
        }

        /// <summary>
        /// 获取或设置标题
        /// </summary>
        [Browsable(true), Category("Momo"), Description("获取或设置标题")]
        public string Title
        {
            get { return this.lblTitle.Text; }
            set
            {
                this.lblTitle.Text = value;
                lblTitle.Visible = value.Length > 0;
                ChangeSize();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.ChangeSize();
        }

        /// <summary>
        /// 获取或设置文本
        /// </summary>
        [Browsable(true), Category("Momo"), Description("获取或设置文本")]
        public override string Text
        {
            get
            {
                return this.txtText.Text;
            }
            set
            {
                this.txtText.Text = value;
                txtText.Select(txtText.Text.Length, 0);
                txtText.ScrollToCaret();
                this.Invalidate();

                this.OnTextChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// 获取或设置是否使用系统密码
        /// </summary>
        [Browsable(true), Category("Momo"), Description("获取或设置是否使用系统密码")]
        public bool UseSystemPasswordChar
        {
            get { return this.txtText.UseSystemPasswordChar; }
            set { this.txtText.UseSystemPasswordChar = value; }
        }

        /// <summary>
        /// 获取或设置密码字符
        /// </summary>
        [Browsable(true), Category("Momo"), Description("获取或设置密码字符")]
        public char PasswordChar
        {
            get { return this.txtText.PasswordChar; }
            set { this.txtText.PasswordChar = value; }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                return;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            this.lblWater.Visible = false;
            borderPanel1.BorderColor = this.ActivtedBorderColor;
            this.Invalidate();
            this.OnEnter(e);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            this.lblWater.Visible = this.txtText.Text.Trim() == string.Empty;
            borderPanel1.BorderColor = this.BorderColor;
            this.Invalidate();
            this.OnLeave(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ChangeSize();
            this.Invalidate();
        }

        private void ChangeSize()
        {
            if (this.lblTitle.Visible)
            {
                this.lblTitle.Location = new Point(3, (this.Height - this.lblTitle.Height) / 2);
                this.txtText.Location = new Point(this.lblTitle.Width + 6, (this.Height - this.txtText.Height) / 2);
                this.lblWater.Location = new Point(this.lblTitle.Width + 6, (this.Height - this.lblWater.Height) / 2);
                this.txtText.Width = this.Width - this.lblTitle.Width - 9;
            }
            else
            {
                this.txtText.Location = new Point(6, (this.Height - this.txtText.Height) / 2);
                this.lblWater.Location = new Point(6, (this.Height - this.lblWater.Height) / 2);
                this.txtText.Width = this.Width - 9;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.lblWater.Visible = this.txtText.Text.Trim() == string.Empty;
            this.TextChanged?.Invoke(sender, e);
        }

        private void lblWater_Click(object sender, EventArgs e)
        {
            this.lblWater.Visible = false;
            this.txtText.Focus();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 删除和退格，不处理
            if (e.KeyChar == 8 || e.KeyChar == 127)
            {
                return;
            }

            //48代表0，57代表9，8代表退格，46代表小数点,，45代表-号
            if (this.TextModel == TextModel.Integer)
            {
                if (this.AllowMinus)
                {
                    e.Handled = (e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 45;
                }
                else
                {
                    e.Handled = e.KeyChar < 48 || e.KeyChar > 57;
                }
            }
            else if (this.TextModel == TextModel.Decimal)
            {
                if (this.AllowMinus)
                {
                    e.Handled = (e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 45 && e.KeyChar != 46;
                }
                else
                {
                    e.Handled = (e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 46;
                }

                // 小数点只能有1个
                if (!e.Handled && e.KeyChar == 46 && this.Text.Contains("."))
                {
                    e.Handled = true;
                }

                // 处理小数位数
                if (!e.Handled && this.Text.Contains("."))
                {
                    e.Handled = this.Text.Split(new string[] { "." }, StringSplitOptions.None)[1].Length >= digits;
                }
            }
            else if (this.TextModel == TextModel.IpAddress)
            {
                // IP地址，只支持输入数字和小数点
                e.Handled = (e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 46;
            }
            else
            {
                base.OnKeyPress(e);
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }
    }
}
