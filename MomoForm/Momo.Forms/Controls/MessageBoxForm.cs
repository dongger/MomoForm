using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;

using System.Windows.Forms;

namespace Momo.Forms
{
    internal partial class MessageBoxForm : MForm
    {
        public MessageBoxForm()
        {
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer, true);
            base.UpdateStyles();
            InitializeComponent();
            this.btnOK.BackgroundColor = new GradientColor(GradientMode.Vertical, Color.FromArgb(26, 188, 156), Color.FromArgb(22, 160, 133));
            this.btnOK.HoverBackColor = new GradientColor(GradientMode.Vertical, Color.FromArgb(46, 204, 113), Color.FromArgb(39, 174, 96));

            this.btnCancel.BackgroundColor = new GradientColor(GradientMode.Vertical, Color.FromArgb(149, 165, 166), Color.FromArgb(149, 165, 166));
            this.btnCancel.HoverBackColor = new GradientColor(GradientMode.Vertical, Color.FromArgb(46, 204, 113), Color.FromArgb(39, 174, 96));

            this.btnRetry.BackgroundColor = new GradientColor(GradientMode.Vertical, Color.FromArgb(230, 126, 34), Color.FromArgb(211, 84, 0));
            this.btnRetry.HoverBackColor = new GradientColor(GradientMode.Vertical, Color.FromArgb(46, 204, 113), Color.FromArgb(39, 174, 96));
        }

        private MessageBoxArgs message;

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = (System.Windows.Forms.DialogResult)(sender as Control).Tag;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = (System.Windows.Forms.DialogResult)(sender as Control).Tag;
        }

        /// <summary>
        /// 使用 <see cref="MessageBoxArgs"/> 消息对话框参数显示窗体。
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public DialogResult ShowMessageBoxDialog(MessageBoxArgs message)
        {
            this.message = message;
            this.Icon = message.Icon;
            if (message.Icon != null)
            {
                this.Caption.Logo = message.Icon.ToBitmap();
            }

            this.Text = this.Caption.Text = message.Caption;
            if (message.Buttons == MessageBoxButtons.OK)
            {
                btnOK.Visible = true;
                btnOK.Text = "确定";
                btnOK.Tag = DialogResult.OK;
                btnOK.Location = new Point((this.Width - btnOK.Width) / 2, btnOK.Location.Y);

                btnCancel.Visible = false;
                btnRetry.Visible = false;
            }
            else if (message.Buttons == MessageBoxButtons.OKCancel)
            {
                btnOK.Visible = true;
                btnOK.Text = "确定";
                btnOK.Tag = DialogResult.OK;
                btnOK.Location = new Point((this.Width - btnOK.Width - btnCancel.Width - 20) / 2, btnOK.Location.Y);

                btnCancel.Visible = true;
                btnCancel.Text = "取消";
                btnCancel.Tag = DialogResult.Cancel;
                btnCancel.Location = new Point((this.Width - btnOK.Width - btnCancel.Width - 20) / 2 + btnOK.Width + 20, btnCancel.Location.Y);

                btnRetry.Visible = false;
            }
            else if (message.Buttons == MessageBoxButtons.RetryCancel)
            {
                btnOK.Visible = true;
                btnOK.Text = "重试";
                btnOK.Tag = DialogResult.Retry;
                btnOK.Location = new Point((this.Width - btnOK.Width - btnCancel.Width - 20) / 2, btnOK.Location.Y);

                btnCancel.Visible = true;
                btnCancel.Text = "取消";
                btnCancel.Tag = DialogResult.Cancel;
                btnCancel.Location = new Point((this.Width - btnOK.Width - btnCancel.Width - 20) / 2 + btnOK.Width + 20, btnCancel.Location.Y);

                btnRetry.Visible = false;
            }
            else if (message.Buttons == MessageBoxButtons.YesNo)
            {
                btnOK.Visible = true;
                btnOK.Text = "是";
                btnOK.Tag = DialogResult.Yes;
                btnOK.Location = new Point((this.Width - btnOK.Width - btnCancel.Width - 20) / 2, btnOK.Location.Y);

                btnCancel.Visible = true;
                btnCancel.Text = "否";
                btnCancel.Tag = DialogResult.No;
                btnCancel.Location = new Point((this.Width - btnOK.Width - btnCancel.Width - 20) / 2 + btnOK.Width + 20, btnCancel.Location.Y);

                btnRetry.Visible = false;
            }
            else if (message.Buttons == MessageBoxButtons.YesNoCancel)
            {
                btnOK.Visible = true;
                btnOK.Text = "是";
                btnOK.Tag = DialogResult.Yes;
                btnOK.Location = new Point((this.Width - btnOK.Width - btnCancel.Width - btnRetry.Width - 40) / 2, btnOK.Location.Y);

                btnCancel.Visible = true;
                btnCancel.Text = "否";
                btnCancel.Tag = DialogResult.No;
                btnCancel.Location = new Point((this.Width - btnOK.Width - btnCancel.Width - btnRetry.Width - 40) / 2 + btnOK.Width + 20, btnCancel.Location.Y);

                btnRetry.Visible = true;
                btnRetry.Text = "取消";
                btnRetry.Tag = DialogResult.Cancel;
                btnRetry.Location = new Point((this.Width - btnOK.Width - btnCancel.Width - btnRetry.Width - 40) / 2 + btnOK.Width + btnCancel.Width + 40, btnRetry.Location.Y);
            }
            else if (message.Buttons == MessageBoxButtons.AbortRetryIgnore)
            {
                btnOK.Visible = true;
                btnOK.Text = "重试";
                btnOK.Tag = DialogResult.Retry;
                btnOK.Location = new Point((this.Width - btnOK.Width - btnCancel.Width - btnRetry.Width - 40) / 2, btnOK.Location.Y);

                btnCancel.Visible = true;
                btnCancel.Text = "忽略";
                btnCancel.Tag = DialogResult.Ignore;
                btnCancel.Location = new Point((this.Width - btnOK.Width - btnCancel.Width - btnRetry.Width - 40) / 2 + btnOK.Width + 20, btnCancel.Location.Y);

                btnRetry.Visible = true;
                btnRetry.Text = "终止";
                btnRetry.Tag = DialogResult.Abort;
                btnRetry.Location = new Point((this.Width - btnOK.Width - btnCancel.Width - btnRetry.Width - 40) / 2 + btnOK.Width + btnCancel.Width + 40, btnRetry.Location.Y);
            }
            this.Height = btnOK.Top + this.Padding.Top + this.Padding.Bottom + 20;
            return this.ShowDialog(message.Owner);
        }

        private void btnRetry_Click(object sender, EventArgs e)
        {
            this.DialogResult = (System.Windows.Forms.DialogResult)(sender as Control).Tag;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var size = e.Graphics.MeasureString(message.Text, this.Font);
            var row = (int)size.Width / (this.Width - 6);

            if (message.Text.Contains(System.Environment.NewLine))
            {
                row += this.Text.Split(new string[] { System.Environment.NewLine }, System.StringSplitOptions.None).Length;
            }
            else if (message.Text.Contains("\n"))
            {
                row += this.Text.Split(new string[] { "\n" }, System.StringSplitOptions.None).Length;
            }

            var height = size.Width > this.Width ? (row + 1) * this.Font.Height : 30;
            using (var brush = new SolidBrush(Color.Black))
            {
                e.Graphics.DrawString(message.Text, this.Font, brush, new Rectangle(3, this.Caption.Height + 3, this.Width - 6, height));
            }

            btnOK.Top = btnCancel.Top = btnRetry.Top = this.Caption.Height + 3 + height + 10;
            this.Height= this.Caption.Height + 3 + height + 52;
        }
    }

    #region 消息框对话消息参数
    /// <summary>
    /// 消息框对话消息参数。
    /// </summary>
    public class MessageBoxArgs
    {
        IWin32Window _owner;
        string _text;
        string _caption;
        MessageBoxButtons _buttons;
        Icon _icon;

        /// <summary>
        /// 初始化 <see cref="MessageBoxArgs"/> 类的新实例。
        /// </summary>
        public MessageBoxArgs()
        {
        }

        /// <summary>
        ///  初始化 <see cref="MessageBoxArgs"/> 类的新实例。
        /// </summary>
        /// <param name="owner">任何实现 <see cref="IWin32Window"/>（表示将拥有模式对话框的顶级窗口）的对象。</param>
        /// <param name="text">需要显示消息文本。</param>
        /// <param name="caption">需要现实的消息标题。</param>
        /// <param name="buttons"><see cref="MessageBoxButtons"/> 值之一。</param>
        /// <param name="icon">需要显示的消息图标。</param>
        public MessageBoxArgs(IWin32Window owner, string text, string caption,
            MessageBoxButtons buttons, Icon icon)
        {
            _owner = owner;
            _text = text;
            _caption = caption;
            _buttons = buttons;
            _icon = icon;
        }

        public IWin32Window Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public string Caption
        {
            get { return _caption; }
            set { _caption = value; }
        }

        public MessageBoxButtons Buttons
        {
            get { return _buttons; }
            set { _buttons = value; }
        }

        public Icon Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }
    }
    #endregion

}
