using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace Momo.Forms
{
    public sealed class MHtmlTable : Control
    {
        public MHtmlTable()
        {
            // 设置绘制样式
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer, true);
            base.UpdateStyles();

            this.columns = new List<MHtmlTableColumn>();
            this.Rows = new MHtmlTableRowCollection();
            this.TableFooter = new MHtmlTableFooter(this);
            this.TableFooter.Visible = true;
            this.TableFooter.BackColor = SystemColors.Control;
            this.TableFooter.Height = 40;

            this.RowHeight = 40;
            this.columnHeadHeight = 40;
            this.columnHeadVisible = true;
            this.columnHeadBackColor = Color.Tomato;
            this.TableHead = new MHtmlTableHeadPalette(this);
            this.TableHead.Height = 40;
            this.TableHead.Text = "表格标题";
            this.TableHead.Visible = true;

            this.borderStyle = MHtmlBorderType.Solid;
            this.borderSize = 1;
            this.borderColor = Color.LightGray;

            this.HorizontalBorderStyle = MHtmlBorderType.Dash;
            this.HorizontalBorderSize = 1;
            this.HorizontalBorderOffset = 10;
            this.HorizontalBorderColor = Color.DimGray;

            this.VerticalBorderColor = Color.DimGray;
            this.VerticalBorderOffset = 5;
            this.VerticalBorderSize = 1;
            this.VerticalBorderStyle = MHtmlBorderType.Solid;
        }

        private List<MHtmlTableColumn> columns;
        [Localizable(true)]
        [Browsable(true), Category("Momo"), Description("表格列")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof(HtmlTableColumnEditor<MHtmlTableColumn>), typeof(UITypeEditor))]
        public List<MHtmlTableColumn> Columns
        {
            get
            {
                if (this.columns == null)
                {
                    this.columns = new List<MHtmlTableColumn>();
                }

                return this.columns;
            }
        }

        [Browsable(true), Category("Momo"), Description("数据行")]
        public MHtmlTableRowCollection Rows { get; private set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(true), Category("Momo"), Description("表格页头，注意，不是列头")]
        public MHtmlTableHeadPalette TableHead { get; private set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(true), Category("Momo"), Description("表格页脚")]
        public MHtmlTableFooter TableFooter { get; private set; }

        private MHtmlBorderType borderStyle;
        [Browsable(true), Category("Momo"), Description("边框样式")]
        public MHtmlBorderType BorderStyle { get { return this.borderStyle; } set { this.borderStyle = value; SetTableHead(); } }

        [Browsable(true), Category("Momo"), Description("行框线样式")]
        public MHtmlBorderType HorizontalBorderStyle { get; set; }

        [Browsable(true), Category("Momo"), Description("单元格框线样式")]
        public MHtmlBorderType VerticalBorderStyle { get; set; }

        [Browsable(true), Category("Momo"), Description("单元格框线颜色")]
        public Color VerticalBorderColor { get; set; }

        [Browsable(true), Category("Momo"), Description("单元格框线大小")]
        public int VerticalBorderSize { get; set; }

        /// <summary>
        /// 垂直框线 偏移量
        /// </summary>
        [Browsable(true), Category("Momo"), Description("单元格框线偏移量，上下缩进多少")]
        public int VerticalBorderOffset { get; set; }

        /// <summary>
        /// 水平框线偏移量
        /// </summary>
        [Browsable(true), Category("Momo"), Description("行框线偏移量，左右缩进多少")]
        public int HorizontalBorderOffset { get; set; }

        [Browsable(true), Category("Momo"), Description("行框线颜色")]
        public Color HorizontalBorderColor { get; set; }

        [Browsable(true), Category("Momo"), Description("行框线大小")]
        public int HorizontalBorderSize { get; set; }

        private Color borderColor;
        [Browsable(true), Category("Momo"), Description("边框颜色")]
        public Color BorderColor { get { return this.borderColor; } set { this.borderColor = value; } }

        private bool equallyColumn;
        [Browsable(true), Category("Momo"), Description("是否等宽各列")]
        public bool EquallyColumn
        {
            get { return this.equallyColumn; }
            set
            {
                this.equallyColumn = value;
                this.Invalidate();
            }
        }

        private int borderSize;
        [Browsable(true), Category("Momo"), Description("边框大小")]
        public int BorderSize { get { return this.borderSize; } set { this.borderSize = value; SetTableHead(); } }

        private int columnHeadHeight;
        [Browsable(true), Category("Momo"), Description("列头高度")]
        public int ColumnHeadHeight { get { return this.columnHeadHeight; } set { this.columnHeadHeight = value; } }

        private Color columnHeadBackColor;
        [Browsable(true), Category("Momo"), Description("列头背景色")]
        public Color ColumnHeadBackColor { get { return this.columnHeadBackColor; } set { this.columnHeadBackColor = value; } }

        [Browsable(true), Category("Momo"), Description("默认行高")]
        public int RowHeight { get; set; }

        private bool columnHeadVisible;
        [Browsable(true), Category("Momo"), Description("是否显示列头")]
        public bool ColumnHeadVisible { get { return this.columnHeadVisible; } set { this.columnHeadVisible = value; } }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);

            e.Graphics.SetClip(this.ClientRectangle);

            var borderSize = BorderStyle == MHtmlBorderType.None ? 0 : BorderSize;
            var columnWidth = this.equallyColumn ? (this.Width - borderSize * 2) / this.columns.Count : 0;
            var y = borderSize;
            var x = borderSize;
            if (this.TableHead.Visible)
            {
                y += this.TableHead.Height;
            }

            if (this.columnHeadVisible)
            {
                y += this.columnHeadHeight;
            }

            if (Rows == null || Rows.Count == 0)
            {
                if (this.TableFooter.Visible)
                {
                    TableFooter.SuspendLayout();
                    TableFooter.Y = y;
                    using (var brush = new SolidBrush(TableFooter.BackColor))
                    {
                        e.Graphics.FillRectangle(brush, TableFooter.Rectangle);
                    }

                    TableFooter.PerformLayout();
                    y += TableFooter.Height;
                }
            }
            else
            {
                #region 行数据
                var head = 0;
                if (this.ColumnHeadVisible)
                {
                    head += this.ColumnHeadHeight;
                }

                if (this.TableHead.Visible)
                {
                    head += this.TableHead.Height;
                }

                //RowHeight = (this.Height - head - borderSize * 2) / Rows.Count;
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                #region 画行
                foreach (var row in Rows)
                {
                    var rowRect = new Rectangle(x, y, this.Width - borderSize * 2, row.Height == 0 ? RowHeight : row.Height);

                    // 画行下划线，最后一行没有线条
                    if ((row.Index < Rows.Count - 1 || this.TableFooter.Visible) && HorizontalBorderStyle != MHtmlBorderType.None)
                    {
                        using (var pen = new Pen(HorizontalBorderColor.IsEmpty ? BorderColor : HorizontalBorderColor, HorizontalBorderSize))
                        {
                            pen.DashStyle = GetDashStyle(HorizontalBorderStyle);
                            g.DrawLine(pen, rowRect.X + HorizontalBorderOffset, rowRect.Y + rowRect.Height, rowRect.X + rowRect.Width - HorizontalBorderOffset, rowRect.Y + rowRect.Height);
                        }
                    }

                    var cx = x;
                    var cy = rowRect.Y;
                    foreach (var cell in row)
                    {
                        cell.SuspendLayout();

                        var cellRect = new Rectangle(cx, cy, columns[cell.Index].Width == 0 ? columnWidth : columns[cell.Index].Width, rowRect.Height);

                        cell.Rectangle = cellRect;
                        #region 单元格背景
                        if (!cell.BackColor.IsEmpty)
                        {
                            using (var brush = new SolidBrush(cell.BackColor))
                            {
                                g.FillRectangle(brush, cellRect);
                            }
                        }
                        #endregion

                        #region 单元格边框
                        if (cell.Index < row.Count - 1 && VerticalBorderStyle != MHtmlBorderType.None)
                        {
                            using (var pen = new Pen(VerticalBorderColor.IsEmpty ? BorderColor : VerticalBorderColor, VerticalBorderSize == 0 ? BorderSize : VerticalBorderSize))
                            {
                                g.DrawLine(pen, cellRect.X + cellRect.Width - pen.Width, cellRect.Y + VerticalBorderOffset, cellRect.X + cellRect.Width - pen.Width, cellRect.Y + cellRect.Height - VerticalBorderOffset);
                            }
                        }
                        #endregion

                        #region
                        if (cell.Align == TextAlignment.None)
                        {
                            cell.Align = columns[cell.Index].Align;
                        }

                        if (cell.Font == null)
                        {
                            cell.Font = this.Font;
                        }

                        if (cell.ForeColor.IsEmpty)
                        {
                            cell.ForeColor = this.ForeColor;
                        }

                        cell.Draw(g);
                        cell.PerformLayout();
                        #endregion
                        cx += cellRect.Width;
                    }

                    y += rowRect.Height;
                }
                #endregion

                if (this.TableFooter.Visible)
                {
                    this.TableFooter.SuspendLayout();
                    TableFooter.Y = y + (HorizontalBorderSize == 0 ? borderSize : HorizontalBorderSize);
                    this.TableFooter.Draw(g);

                    this.TableFooter.PerformLayout();
                    y += TableFooter.Height;
                }
                #endregion
            }

            if (y > this.Height)
            {
                this.Height = y;
            }
        }

        private DashStyle GetDashStyle(MHtmlBorderType border)
        {
            if (border == MHtmlBorderType.Dash)
            {
                return DashStyle.Dash;
            }
            else
            {
                return DashStyle.Solid;
            }
        }

        private void SetTableHead()
        {
            this.TableHead.SuspendLayout();
            this.TableHead.Width = this.Width - (BorderStyle == MHtmlBorderType.None ? 0 : BorderSize * 2);
            this.TableHead.X = BorderStyle == MHtmlBorderType.None ? 0 : BorderSize;
            this.TableHead.Y = BorderStyle == MHtmlBorderType.None ? 0 : BorderSize;
            this.TableHead.PerformLayout();

            this.TableFooter.SuspendLayout();
            this.TableFooter.Width = this.Width - (BorderStyle == MHtmlBorderType.None ? 0 : BorderSize * 2);
            this.TableFooter.X = (BorderStyle == MHtmlBorderType.None ? 0 : BorderSize);
            this.TableFooter.PerformLayout();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetTableHead();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            DrawBackground(e, e.Graphics);
        }

        private void DrawBackground(PaintEventArgs e, Graphics g)
        {
            using (var brush = new SolidBrush(this.BackColor))
            {
                g.FillRectangle(brush, this.ClientRectangle);
            }

            #region 表格边框
            if (BorderStyle != MHtmlBorderType.None && this.BorderSize > 0)
            {
                using (var pen = new Pen(this.BorderColor, this.BorderSize))
                {
                    pen.DashStyle = GetDashStyle(BorderStyle);

                    g.DrawRectangle(pen, new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width - BorderSize, this.ClientRectangle.Height - BorderSize));
                }
            }
            #endregion

            var borderSize = this.BorderStyle == MHtmlBorderType.None ? 0 : this.BorderSize;
            var y = borderSize;
            if (this.TableHead.Visible)
            {
                this.TableHead.Draw(g);
                y += this.TableHead.Height;
            }

            if (this.columnHeadVisible)
            {
                // 表头背景
                using (var brush = new SolidBrush(this.ColumnHeadBackColor))
                {
                    g.FillRectangle(brush, new Rectangle(borderSize, y, this.Width - borderSize * 2, this.ColumnHeadHeight));
                }

                #region 绘制表头

                if (columns != null)
                {
                    var columnWidth = this.equallyColumn ? (this.Width - borderSize * 2) / this.columns.Count : 0;

                    var x = borderSize;
                    var index = 0;
                    foreach (var item in columns)
                    {
                        item.Width = columnWidth == 0 ? item.Width : columnWidth;
                        var rect = new Rectangle(x, y, item.Width, this.ColumnHeadHeight);
                        if (item.BackColor != Color.Empty)
                        {
                            using (var brush = new SolidBrush(item.BackColor))
                            {
                                g.FillRectangle(brush, rect);
                            }
                        }

                        if (index < columns.Count - 1 && this.VerticalBorderStyle != MHtmlBorderType.None)
                        {
                            using (var pen = new Pen(this.VerticalBorderColor, this.VerticalBorderSize))
                            {
                                pen.DashStyle = GetDashStyle(BorderStyle);

                                g.DrawLine(pen, rect.X + rect.Width - this.VerticalBorderSize, rect.Y, rect.X + rect.Width - this.VerticalBorderSize, rect.Y + rect.Height - borderSize - this.VerticalBorderSize);
                                //g.DrawRectangle(pen, new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width - BorderSize, this.ClientRectangle.Height - BorderSize));
                            }
                        }

                        if (!string.IsNullOrEmpty(item.Text))
                        {
                            var font = item.Font == null ? this.Font : item.Font;

                            GDIHelper.DrawString(g, rect, font, item.Text, item.ForeColor.IsEmpty ? this.ForeColor : item.ForeColor, item.Align);
                        }

                        x += item.Width;
                        index++;
                    }
                }
                #endregion
            }
        }
    }

    public enum MHtmlBorderType
    {
        Dash,
        Solid,
        None
    }

    public class MHtmlTableHeadPalette : Palette
    {
        public MHtmlTableHeadPalette() : base() { }
        public MHtmlTableHeadPalette(Control control) : base(control) { }
        public string Text { get; set; }

        public Font Font { get; set; }

        public Color ForeColor { get; set; }

        public TextAlignment Align { get; set; }

        public Color BackColor { get; set; }

        public event TableHeadDrawEventHandler TableHeadDrawing;
        public override void Draw(Graphics graphics)
        {
            #region 背景色
            if (!BackColor.IsEmpty)
            {
                using (var brush = new SolidBrush(this.BackColor))
                {
                    graphics.FillRectangle(brush, this.Rectangle);
                }
            }
            #endregion

            if (TableHeadDrawing != null)
            {
                var args = new TableHeadDrawEventArgs() { Cancel = false, Graphics = graphics, TableHead = this };
                TableHeadDrawing(args);
                if (args.Cancel)
                {
                    return;
                }
            }

            if (!string.IsNullOrEmpty(Text))
            {
                var font = this.Font == null ? SystemFonts.DefaultFont : this.Font;
                var foreColor = this.ForeColor.IsEmpty ? SystemColors.WindowText : this.ForeColor;
                var fontSize = graphics.MeasureString(this.Text, font);

                GDIHelper.DrawString(graphics, this.Rectangle, font, this.Text, foreColor, Align);
            }
        }
    }

    public class MHtmlTableLabelCollection : MHtmlTableCollection<LabelPalette>
    {
        private MHtmlTable Contrainer;
        public MHtmlTableLabelCollection(MHtmlTable contrainer)
        {
            this.Contrainer = contrainer;
        }

        public LabelPalette NewLabel()
        {
            var label = new LabelPalette();
            this.Contrainer.MouseMove += Contrainer_MouseMove;
            this.Add(label);
            return label;
        }

        private void Contrainer_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (var item in this)
            {
                item.OnMouseMove(e);
            }
        }
    }

    public class MHtmlTableFooter : Palette
    {
        public Color BackColor { get; set; }

        public MHtmlTableFooter(Control control) : base(control)
        {
            this.Items = new MHtmlTableLabelCollection(control as MHtmlTable);
        }

        public event TableFooterDrawEventHandler TableFooterDrawing;

        public MHtmlTableLabelCollection Items { get; private set; }

        public override void Draw(Graphics graphics)
        {
            using (var brush = new SolidBrush(BackColor))
            {
                graphics.FillRectangle(brush, Rectangle);
            }

            if (TableFooterDrawing != null)
            {
                var args = new TableFooterDrawEventArgs() { Cancel = false, Footer = this, Graphics = graphics };
                TableFooterDrawing(args);
                if (args.Cancel)
                {
                    return;
                }
            }

            foreach (var item in this.Items)
            {
                item.Draw(graphics);
            }
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MHtmlTableColumn
    {
        public MHtmlTableColumn()
        {

        }

        public string Text { get; set; }

        public Font Font { get; set; }

        public Color ForeColor { get; set; }

        public TextAlignment Align { get; set; }

        public Color BackColor { get; set; }

        public int Width { get; set; }
    }

    public delegate void CellClickEventHandler(MHtmlTableCell cell);
    public delegate void TableHeadDrawEventHandler(TableHeadDrawEventArgs args);
    public sealed class TableHeadDrawEventArgs
    {
        public MHtmlTableHeadPalette TableHead { get; internal set; }
        public Graphics Graphics { get; internal set; }
        public bool Cancel { get; set; }
    }
    internal delegate void RowsChangedEventHandler();

    public delegate void TableCellDrawEventHandler(TableCellDrawEventArgs args);
    public sealed class TableCellDrawEventArgs
    {
        public MHtmlTableCell Cell { get; internal set; }
        public Graphics Graphics { get; internal set; }
        public bool Cancel { get; set; }
    }

    public delegate void TableFooterDrawEventHandler(TableFooterDrawEventArgs args);
    public sealed class TableFooterDrawEventArgs
    {
        public MHtmlTableFooter Footer { get; internal set; }
        public Graphics Graphics { get; internal set; }
        public bool Cancel { get; set; }
    }

    public abstract class MHtmlTableCollection<T> : IEnumerable<T>, IEnumerable
    {
        private readonly List<T> items = new List<T>();

        protected void Add(T item)
        {
            items.Add(item);
        }

        public int Count { get { return this.items.Count; } }

        public virtual void Clear()
        {
            this.items.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }

    public sealed class MHtmlTableRowCollection : MHtmlTableCollection<MHtmlTableRow>
    {
        internal MHtmlTableRowCollection()
        {
        }

        internal event RowsChangedEventHandler RowsChanged;

        public MHtmlTableRow NewRow()
        {
            var row = new MHtmlTableRow(this.Count);
            this.Add(row);
            return row;
        }

        public override void Clear()
        {
            base.Clear();
            RowsChanged?.Invoke();
        }
    }

    public class MHtmlTableRow : MHtmlTableCollection<MHtmlTableCell>
    {
        internal MHtmlTableRow(int index)
        {
            this.Index = index;

        }

        public int Index { get; private set; }

        public int Height { get; set; }

        public MHtmlTableCell NewCell(string text = "")
        {
            var cell = new MHtmlTableCell(this.Index, this.Count);
            cell.Text = text;
            this.Add(cell);
            return cell;
        }
    }

    public class MHtmlTableCell : Palette
    {
        public event TableCellDrawEventHandler CellDrawing;

        public MHtmlTableCell(int rowIndex, int index)
        {
            this.RowIndex = rowIndex;
            this.Index = index;
            this.Align = TextAlignment.None;
        }

        public int RowIndex { get; private set; }

        public int Index { get; private set; }

        public int Colspan { get; set; }

        public int RowSpan { get; set; }

        public string Text { get; set; }

        public ToolTip ToolTip { get; set; }

        public string ToolTipText { get; set; }

        public Font Font { get; set; }

        public Color ForeColor { get; set; }

        public TextAlignment Align { get; set; }

        public Color BackColor { get; set; }

        private bool toolTipShow;

        public event CellClickEventHandler Click;

        protected override void MouseClick(MouseEventArgs e)
        {
            Click?.Invoke(this);
        }

        protected override void MouseMoveIn(MouseEventArgs e)
        {
            if (ToolTip != null)
            {
                toolTipShow = true;
                ToolTip.Show(ToolTipText, this.Container, e.Location);
            }
        }

        protected override void MouseMoveOut(MouseEventArgs e)
        {
            if (ToolTip != null && toolTipShow)
            {
                ToolTip.Hide(this.Container);
                toolTipShow = false;
            }
        }

        public override void Draw(Graphics graphics)
        {
            if (CellDrawing != null)
            {
                var args = new TableCellDrawEventArgs() { Cancel = false, Cell = this, Graphics = graphics };
                CellDrawing(args);
                if (args.Cancel)
                {
                    return;
                }
            }

            if (!string.IsNullOrEmpty(Text))
            {
                GDIHelper.DrawString(graphics, this.Rectangle, this.Font, Text, ForeColor, Align);
            }
        }
    }
}
