namespace Momo.Forms
{
    [System.ComponentModel.TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
    public class MBorder
    {
        internal bool IsEmpty
        {
            get
            {
                var flag = this.Type != BorderType.None;
                if (!flag)
                {
                    return flag;
                }

                if (Color.IsEmpty)
                {
                    return true;
                }

                flag = this.Width != 0 || this.Top != 0 || this.Bottom != 0 || this.Right != 0 || this.Left != 0;
                return !flag;
            }
        }

        public int Width { get; set; }
        public BorderType Type { get; set; }

        public int Top { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }
        public System.Drawing.Color Color { get; set; }

        internal System.Drawing.Drawing2D.DashStyle GetDashStyle()
        {
            if (Type == BorderType.Dash)
            {
                return System.Drawing.Drawing2D.DashStyle.Dash;
            }
            else
            {
                return System.Drawing.Drawing2D.DashStyle.Solid;
            }
        }
    }
}
