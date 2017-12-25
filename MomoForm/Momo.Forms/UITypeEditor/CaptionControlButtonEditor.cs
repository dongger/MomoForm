using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

using System.Text;
using System.Windows.Forms.Design;

namespace Momo.Forms
{
    public class CaptionControlButtonEditor<T> : UITypeEditor where T : CaptionControlButton, new()
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            if (edSvc != null)
            {
                var frm = new FrmCaptionControlButtonEditor<T>(value);
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    return frm.Value;
                }
            }

            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
