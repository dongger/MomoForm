using System.ComponentModel;
using System.Drawing;

namespace Momo.AutoUpgrade
{
    /// <summary>
    /// 渐变颜色对象
    /// </summary>
    [TypeConverter(typeof(GradientColorTypeConverter))]
    public sealed class GradientColor
    {
        public GradientColor() { }

        public GradientColor(GradientMode mode, Color fromColor, Color toColor)
        {
            this.GradientMode = mode;
            this.FromColor = fromColor;
            this.ToColor = toColor;
        }

        /// <summary>
        /// 渐变模式
        /// </summary>
        [Browsable(true)]
        public GradientMode GradientMode { get; set; }

        /// <summary>
        /// 起始渐变色
        /// </summary>
        [Browsable(true)]
        public Color FromColor { get; set; }

        /// <summary>
        /// 终止渐变色
        /// </summary>
        [Browsable(true)]
        public Color ToColor { get; set; }
    }
}
