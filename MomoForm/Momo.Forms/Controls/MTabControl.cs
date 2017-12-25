using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Design;

namespace Momo.Forms
{
    [ToolboxBitmap(typeof(TabControl))]
    [Designer(typeof(MTabControlDesigner))]
    public class MTabControl : Control
    {
        internal Momo.Forms.MBorderControl pnTitle;
        internal bool addingPage;
        public MTabControl()
        {
            Initial();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [MergableProperty(false)]
        public DTabPageCollection Pages { get; private set; }

        protected override ControlCollection CreateControlsInstance()
        {
            return new Momo.Forms.ControlCollection(this);
        }

        private Color borderColor;

        /// <summary>
        /// 获取或设置底部边框颜色
        /// </summary>
        [Browsable(true), Category("Momo"), Description("获取或设置底部边框颜色")]
        public Color BorderColor
        {
            get { return this.borderColor; }
            set
            {
                borderColor = value;
                foreach (var item in this.Pages)
                {
                    if (BorderColor != Color.Empty)
                    {
                        item.BorderColor = BorderColor;
                        item.BorderWidth = 1;
                    }
                    else
                    {
                        item.BorderColor = Color.Empty;
                        item.BorderWidth = 0;
                    }
                }
            }
        }

        private void Initial()
        {
            this.SuspendLayout();
            this.Pages = new DTabPageCollection(this);
            this.BorderColor = Color.FromArgb(189, 195, 199);
            this.pnTitle = new MBorderControl();
            this.pnTitle.BorderBottomColor = System.Drawing.Color.White;
            this.pnTitle.BorderBottomStyle = ButtonBorderStyle.Solid;
            this.pnTitle.BorderBottomWidth = 1;
            this.pnTitle.Location = new System.Drawing.Point(0, 0);
            this.pnTitle.Name = "borderPanel1";
            this.pnTitle.Size = new System.Drawing.Size(200, 40);
            this.pnTitle.Dock = DockStyle.Top;
            this.pnTitle.ControlAdded += pnTitle_ControlAdded;
            this.Controls.Add(this.pnTitle);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            this.ResumeLayout(false);
        }

        void pnTitle_ControlAdded(object sender, ControlEventArgs e)
        {
            e.Control.Width = 120;
            e.Control.Location = new Point((pnTitle.Controls.Count - 1) * 120, 0);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.pnTitle.Width = this.Width;
            this.pnTitle.Height = 40;

            foreach (var item in this.Pages)
            {
                item.Width = this.Width;
                item.Height = this.Height - 40;
                item.Invalidate();
            }
        }

        [Browsable(true), Description("选项卡标题字体"), Category("Momo")]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                foreach (var item in this.Pages)
                {
                    item.btnTitle.Font = value;
                }
            }
        }

        [Browsable(true), Description("选项卡标题下划线颜色"), Category("Momo")]
        public Color GridLineColor
        {
            get { return this.pnTitle.BorderBottomColor; }
            set
            {
                this.pnTitle.BorderBottomColor = value;
                this.pnTitle.Invalidate();
            }
        }

        [Browsable(true), Description("选项卡标题字体颜色"), Category("Momo")]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                foreach (var item in this.Pages)
                {
                    item.btnTitle.ForeColor = value;
                }
            }
        }

        [Browsable(true), Description("当前选项卡变更事件"), Category("Momo")]
        public event Action SelectedChanged;

        private int selectedIndex;
        public int SelectedIndex
        {
            get
            {
                return this.selectedIndex;
            }
            set
            {
                this.selectedIndex = value;
                var page = this.Pages[value];
                page.Selected = true;
                (this.Controls as Momo.Forms.ControlCollection).ChangeSelected(page, true);
                if (!DesignMode && this.SelectedChanged != null)
                {
                    this.SelectedChanged();
                }
            }
        }
    }

    public class ControlCollection : Control.ControlCollection
    {
        // Fields
        private MTabControl owner;

        // Methods
        public ControlCollection(MTabControl owner)
            : base(owner)
        {
            this.owner = owner;
        }

        public override void Add(Control value)
        {
            if (value is DTabPage)
            {
                base.Add(value);
                var page = value as DTabPage;
                page.SelectedChanged += ChangeSelected;
                owner.pnTitle.Controls.Add(page.btnTitle);
                if (!owner.addingPage)
                {
                    owner.Pages.Add(page);
                }
            }
            else
            {
                base.Add(value);
            }
        }

        internal void ChangeSelected(DTabPage obj, bool isFromOwner = false)
        {
            if (!isFromOwner)
            {
                this.owner.SelectedIndex = obj.Index;
            }

            foreach (var item in owner.Pages)
            {
                if (obj != item)
                {
                    item.Selected = false;
                    item.Visible = false;
                }
            }

            obj.Visible = true;
            obj.BringToFront();
        }

        public override void Remove(Control value)
        {
            base.Remove(value);
            if (value is DTabPage)
            {
                owner.Pages.Remove(value as DTabPage);
                owner.SelectedIndex = owner.Pages.Count - 1;
                owner.pnTitle.Controls.Remove((value as DTabPage).btnTitle);
            }
        }

    }

    [ToolboxItem(false)]
    public class DTabPage : MPanel
    {
        internal event Action<DTabPage, bool> SelectedChanged;
        public DTabPage()
        {
            this.btnTitle = new MButton();
            this.btnTitle.Height = 39;     
            this.btnTitle.BackgroundColor = new GradientColor(GradientMode.Vertical, Color.FromArgb(0, 153, 204), Color.FromArgb(0, 153, 204));
            this.btnTitle.HoverBackColor = new GradientColor(GradientMode.Vertical, Color.FromArgb(46, 204, 113), Color.FromArgb(46, 204, 113));
            this.btnTitle.Click += btnTitle_Click;
        }

        void btnTitle_Click(object sender, EventArgs e)
        {
            this.Selected = true;

            if (this.SelectedChanged != null)
            {
                this.SelectedChanged(this, false);
            }
        }

        private bool selected;
        public bool Selected
        {
            get { return selected; }
            internal set
            {
                this.selected = value;
                if (value)
                {
                    this.btnTitle.BackgroundColor = new GradientColor(GradientMode.Vertical, Color.FromArgb(39, 174, 96), Color.FromArgb(46, 204, 113));
                }
                else
                {
                    this.btnTitle.BackgroundColor = new GradientColor(GradientMode.Vertical, Color.FromArgb(41, 128, 185), Color.FromArgb(0, 153, 204));
                }
                this.btnTitle.Invalidate();
            }
        }

        public DTabPage(string text)
            : this()
        {
            this.Text = text;
        }

        internal MButton btnTitle;

        [Browsable(true)]
        public override string Text
        {
            get { return this.btnTitle.Text; }
            set { this.btnTitle.Text = value; }
        }

        private int index;
        public int Index
        {
            get { return this.index; }
            internal set
            {
                this.index = value;
                this.btnTitle.Tag = value;
            }
        }
    }
    public class DControlCollection<T> : IList<T> where T : Control
    {
        private List<T> items = new List<T>();
        public int IndexOf(T item)
        {
            return items.IndexOf(item);
        }

        public virtual void Insert(int index, T item)
        {
            items.Insert(index, item);
        }

        public virtual void RemoveAt(int index)
        {
            items.RemoveAt(index);
        }

        public T this[int index]
        {
            get
            {
                return items[index];
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual void Add(T item)
        {
            items.Add(item);
        }

        public virtual void Clear()
        {
            items.Clear();
        }

        public bool Contains(T item)
        {
            return items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public virtual bool Remove(T item)
        {
            return items.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
    public class DTabPageCollection : DControlCollection<DTabPage>
    {
        private MTabControl owner;
        public DTabPageCollection(MTabControl owner)
        {
            this.owner = owner;
        }

        public override void Add(DTabPage item)
        {
            owner.addingPage = true;
            item.Index = this.Count;
            item.Width = owner.Width;
            item.Height = owner.Height - 40;
            item.Location = new Point(0, 40);
            if (owner.BorderColor != Color.Empty)
            {
                item.BorderColor = owner.BorderColor;
                item.BorderWidth = 1;
            }

            base.Add(item);
            owner.Controls.Add(item);

            item.btnTitle.ForeColor = owner.ForeColor;
            item.btnTitle.Font = owner.Font;
            item.btnTitle.Width = 120;
            owner.pnTitle.Controls.Add(item.btnTitle);
            owner.addingPage = false;
        }
    }
}
