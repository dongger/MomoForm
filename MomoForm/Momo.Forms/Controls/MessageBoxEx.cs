using System;
using System.Collections.Generic;
using System.Drawing;

using System.Text;

using System.Windows.Forms;

namespace Momo.Forms
{
    public static class MessageBoxEx
    {
        public static DialogResult Show(
            IWin32Window owner, string text, string caption, MessageBoxButtons buttons,
            Icon icon,  MessageBoxIcon beepType)
        {
            MessageBoxForm form = new MessageBoxForm();
            return form.ShowMessageBoxDialog(new MessageBoxArgs(
                owner, text, caption, buttons, icon));
        }

        public static DialogResult Show(
            IWin32Window owner, string text, string caption, MessageBoxButtons buttons,
            MessageBoxIcon icon)
        {
            return Show(owner, text, caption, buttons, GetIcon(icon),  icon);
        }

        public static DialogResult Show(
            string text, string caption, MessageBoxButtons buttons,
            MessageBoxIcon icon)
        {
            return Show(null, text, caption, buttons, icon);
        }

        public static DialogResult Show(
            IWin32Window owner, string text, string caption, MessageBoxButtons buttons)
        {
            return Show(owner, text, caption, buttons, MessageBoxIcon.None);
        }

        public static DialogResult Show(
            string text, string caption, MessageBoxButtons buttons)
        {
            return Show(null, text, caption, buttons);
        }

        public static DialogResult Show(
            IWin32Window owner, string text, string caption)
        {
            return Show(owner, text, caption, MessageBoxButtons.OK);
        }

        public static DialogResult Show(string text, string caption)
        {
            return Show(null, text, caption, MessageBoxButtons.OK);
        }

        public static DialogResult Show(
           IWin32Window owner, string text)
        {
            return Show(owner, text, "");
        }

        public static DialogResult Show(string text)
        {
            return Show((IWin32Window)null, text);
        }

        private static Icon GetIcon(MessageBoxIcon icon)
        {
            switch (icon)
            {
                default:
                case MessageBoxIcon.Information:
                    return Properties.Resources.info;
                case MessageBoxIcon.Question:
                    return Properties.Resources.help;
                case MessageBoxIcon.Warning:
                    return Properties.Resources.warning;
                case MessageBoxIcon.Error:
                    return Properties.Resources.error;
            }
        }
    }
}
