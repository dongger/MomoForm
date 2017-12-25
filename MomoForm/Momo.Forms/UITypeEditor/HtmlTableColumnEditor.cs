using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Momo.Forms
{
    public class HtmlTableColumnEditor<T> : UITypeEditor where T : MHtmlTableColumn, new()
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            if (edSvc != null)
            {
                var frm = new FrmHtmlTableColumnEditor<T>(value);
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
