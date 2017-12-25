using System;
using System.Collections.Generic;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace Momo.Forms
{
    /// <summary>
    /// 按钮式下拉菜单绘制
    /// </summary>
    public sealed class MenuPalette : Palette
    {
        public List<Palette> Items { get; set; }

        public Size ImageSize { get; set; }

        public ImageList ImageList { get; set; }

        public Font Font { get; set; }

        public Color BackColor { get; set; }

        public override void Draw(Graphics graphics)
        {
            foreach(var item in this.Items)
            {
                item.Draw(graphics);
            }            
        }
    }

    public sealed class MenuSplitPalette : Palette
    {
        public Color BackColor { get; set; }

        public override void Draw(Graphics graphics)
        {
            
        }
    }

    /// <summary>
    /// 菜单项画板
    /// </summary>
    public sealed class MenuItemPalette : ActivePalette
    {
        public Image Image { get; set; }

        public string Text { get; set; }

        public override void Draw(Graphics graphics)
        {
            
        }
    }
}
