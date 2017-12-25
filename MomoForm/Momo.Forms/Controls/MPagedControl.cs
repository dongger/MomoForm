using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Momo.Forms
{
    public partial class MPagedControl : UserControl
    {
        public MPagedControl()
        {
            InitializeComponent();
        }

        public int PageIndex { get; private set; }
        public int RecordCount { get; private set; }
        public int PageCount { get; private set; }

        [Browsable(true), Description("可用的上翻页按钮图片"), Category("Momo")]
        public Image PreviousImage { get; set; }

        [Browsable(true), Description("可用的首页按钮图片"), Category("Momo")]
        public Image FirstImage { get; set; }

        [Browsable(true), Description("可用的下翻页按钮图片"), Category("Momo")]
        public Image NextImage { get; set; }

        [Browsable(true), Description("可用的尾页按钮图片"), Category("Momo")]
        public Image LastImage { get; set; }

        [Browsable(true), Description("不可用的上翻页按钮图片"), Category("Momo")]
        public Image PreviousDisableImage { get; set; }

        [Browsable(true), Description("不可用的首页按钮图片"), Category("Momo")]
        public Image FirstDisableImage { get; set; }

        [Browsable(true), Description("不可用的下翻页按钮图片"), Category("Momo")]
        public Image NextDisableImage { get; set; }

        [Browsable(true), Description("不可用的尾页按钮图片"), Category("Momo")]
        public Image LastDisableImage { get; set; }

        [Browsable(true), Description("当前页变更事件"), Category("Momo")]
        public event EventHandler<PagedChangedEventArgs> PageChanged;

        public void Bind(int recordCount, int pageCount, int pageIndex)
        {
            this.PageCount = pageCount;
            this.RecordCount = recordCount;
            this.PageIndex = pageIndex;

            if (this.PageIndex == 1)
            {
                this.pbFirst.Enabled = false;
                this.pbFirst.Cursor = Cursors.No;
                this.pbPrevious.Enabled = false;
                this.pbPrevious.Cursor = Cursors.No;
                if (this.FirstDisableImage != null)
                {
                    pbFirst.Image = this.FirstDisableImage;
                }

                if (this.PreviousDisableImage != null)
                {
                    pbPrevious.Image = this.PreviousDisableImage;
                }
            }
            else
            {
                this.pbFirst.Enabled = true;
                this.pbFirst.Cursor = Cursors.Hand;
                this.pbPrevious.Enabled = true;
                this.pbPrevious.Cursor = Cursors.Hand;

                if (this.FirstImage != null)
                {
                    pbFirst.Image = this.FirstImage;
                }

                if (this.PreviousImage != null)
                {
                    pbPrevious.Image = this.PreviousImage;
                }
            }

            if (this.PageIndex == this.PageCount)
            {
                this.pbNext.Enabled = false;
                this.pbNext.Cursor = Cursors.No;
                this.pbLast.Enabled = false;
                this.pbLast.Cursor = Cursors.No;

                if (this.NextDisableImage != null)
                {
                    pbNext.Image = this.NextDisableImage;
                }

                if (this.LastDisableImage != null)
                {
                    pbLast.Image = this.LastDisableImage;
                }
            }
            else
            {
                this.pbNext.Enabled = true;
                this.pbNext.Cursor = Cursors.Hand;
                this.pbLast.Enabled = true;
                this.pbLast.Cursor = Cursors.Hand;

                if (this.NextImage != null)
                {
                    pbNext.Image = this.NextImage;
                }

                if (this.LastImage != null)
                {
                    pbLast.Image = this.LastImage;
                }
            }

            if (this.PageIndex == 1 && this.PageCount == 0 || this.RecordCount == 0)
            {
                this.lblPageInfo.Text = string.Empty;
            }
            else
            {
                this.lblPageInfo.Text = string.Format("当前第{0}页，总{1}页，共{2}条数据", this.PageIndex, this.PageCount, this.RecordCount);
            }
        }

        private void RaiseEvent(int pageIndex)
        {
            this.PageIndex = pageIndex;
            this.lblPageInfo.Text = string.Format("当前第{0}页，总{1}页，共{2}条数据", this.PageIndex, this.PageCount, this.RecordCount);
            if (this.PageChanged != null)
            {
                try
                {
                    this.PageChanged(this, new PagedChangedEventArgs(pageIndex));
                }
                catch { }
            }
        }

        private void pbFirst_Click(object sender, EventArgs e)
        {
            if (PageIndex > 1)
            {
                this.RaiseEvent(1);
            }
        }

        private void pbPrevious_Click(object sender, EventArgs e)
        {
            if (PageIndex > 1)
            {
                this.RaiseEvent(--this.PageIndex);
            }
        }

        private void pbNext_Click(object sender, EventArgs e)
        {
            if (PageIndex < PageCount)
            {
                this.RaiseEvent(++this.PageIndex);
            }
        }

        private void pbLast_Click(object sender, EventArgs e)
        {
            if (PageIndex != PageCount)
            {
                this.RaiseEvent(this.PageCount);
            }
        }
    }

    public class PagedChangedEventArgs : EventArgs
    {
        public PagedChangedEventArgs(int pageIndex)
        {
            this.PageIndex = pageIndex;
        }
        public int PageIndex { get; private set; }
    }
}
