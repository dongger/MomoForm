using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Momo.Forms
{
    public sealed class CaptionControlButtonCollection<T> : List<T> where T : CaptionControlButton, new()
    {
        public bool Hover
        {
            get
            {
                foreach (var item in this)
                {
                    if (item.Hover)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// 绘制方法
        /// </summary>
        /// <param name="graphics"></param>
        public void Draw(Graphics graphics)
        {
            foreach (var item in this)
            {
                item.Draw(graphics);
            }
        }

        public void OnMouseMove(MouseEventArgs e)
        {
            foreach (var item in this)
            {
                item.OnMouseMove(e);
            }
        }

        public void OnMouseClick(MouseEventArgs e)
        {
            foreach (var item in this)
            {
                item.OnMouseClick(e);
            }
        }

        public void OnMouseDown(MouseEventArgs e)
        {
            foreach (var item in this)
            {
                item.OnMouseDown(e);
            }
        }
    }

    /// <summary>
    /// 标题栏控制按钮，大小受Caption的ControlBoxSize控制，颜色受Caption的相关控制
    /// </summary>
    public class CaptionControlButton : ActivePalette
    {
        /// <summary>
        /// 按钮图片
        /// </summary>
        [Browsable(false)]
        public Image Image { get; set; }

        /// <summary>
        /// ImageList中的图片索引
        /// </summary>
        public int ImageIndex { get; set; }

        /// <summary>
        /// 按钮事件Tag值
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 名称，用于区分，没有其他作用
        /// </summary>
        public string Name { get; set; }

        private NotifyPalette notify;
        public NotifyPalette Notify
        {
            get
            {
                return this.notify;
            }
            set
            {
                this.notify = value;
                if (value != null)
                {
                    this.notify.ParentPalette = this;
                }
            }
        }

        /// <summary>
        /// 单击事件
        /// </summary>
        public event EventHandler Click;

        public override void Draw(Graphics graphics)
        {
            base.DrawBackground(graphics);
            if (this.Image != null)
            {
                ImageAttributes ImgAtt = new ImageAttributes();
                ImgAtt.SetWrapMode(System.Drawing.Drawing2D.WrapMode.Clamp);
                graphics.DrawImage(this.Image, this.Rectangle, 0, 0, this.Image.Width, this.Image.Height, GraphicsUnit.Pixel, ImgAtt);
            }

            if (this.Notify != null)
            {
                this.Notify.Draw(graphics);
            }
        }

        protected override void MouseClick(MouseEventArgs e)
        {
            if (this.Rectangle.Contains(e.Location))
            {
                this.Click?.Invoke(this, e);
            }
        }
    }
}
