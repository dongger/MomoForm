using System.Drawing;
using System.Windows.Forms;

namespace Momo.Forms
{
    /// <summary>
    /// 弹出层工具类
    /// </summary>
    public static class Popup
    {
        /// <summary>
        /// 在指定的容器下面弹出包含指定控件的层
        /// </summary>
        /// <param name="parent">容器</param>
        /// <param name="content">显示的内容</param>
        /// <param name="size">大小，为空则以显示控件大小为准</param>
        /// <returns></returns>
        public static ToolStripDropDown Show(Control parent, Control content, Size size, bool autoClose = false)
        {
            ToolStripControlHost host;
            ToolStripDropDown dropDown;
            MPanel container;

            container = new MPanel();
            content.Location = new System.Drawing.Point(1, 1);
            container.BorderStyle = ButtonBorderStyle.Solid;
            container.BorderWidth = 1;
            container.BorderColor = System.Drawing.Color.LightGray;
            content.SizeChanged += Content_SizeChanged;
            host = new ToolStripControlHost(container);
            container.Controls.Add(content);

            dropDown = new ToolStripDropDown();
            dropDown.Items.Add(host);
            dropDown.AutoClose = autoClose;

            host.Margin = Padding.Empty;
            host.Padding = Padding.Empty;
            host.AutoSize = true;
            dropDown.Padding = Padding.Empty;

            //添加
            dropDown.Items.Add(host);
            if (size.IsEmpty)
            {
                size = new System.Drawing.Size(content.Width + 2, content.Height + 2);
            }

            dropDown.Size = size;
            container.Size = size;
            content.Size = size;
            
            dropDown.Show(parent, 0, parent.Height);

            return dropDown;
        }

        private static void Content_SizeChanged(object sender, System.EventArgs e)
        {
            var content = (sender as Control);
            content.Parent.Size = new System.Drawing.Size(content.Width + 2, content.Height + 2);
        }
    }
}
