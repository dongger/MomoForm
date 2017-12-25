using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;

namespace Momo.Forms
{
    public static class GDIHelper
    {
        /// <summary>
        /// 设置绘制模式为高质量模式
        /// </summary>
        /// <param name="g">The Graphics.</param>
        public static void HighQuality(Graphics g)
        {
            if (g != null)
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = CompositingQuality.HighQuality;
            }
        }
        #region EllipseRender

        /// <summary>
        ///绘制椭圆的边框
        /// </summary>
        /// User:Ryan  CreateTime:2011-07-29 17:10.
        public static void DrawEllipseBorder(Graphics g, Rectangle rect, Color color, int borderWidth)
        {
            using (Pen pen = new Pen(color, borderWidth))
            {
                g.DrawEllipse(pen, rect);
            }
        }

        /// <summary>
        /// 渲染一个圆形区域(简单渲染)
        /// </summary>
        /// User:K.Anding  CreateTime:2011-7-30 14:37.
        public static void FillEllipse(Graphics g, Rectangle rect, Color color)
        {
            using (SolidBrush brush = new SolidBrush(color))
            {
                g.FillEllipse(brush, rect);
            }
        }

        /// <summary>
        /// 渲染一个圆形区域（高级渲染）
        /// </summary>
        /// User:K.Anding  CreateTime:2011-7-30 14:37.
        public static void FillEllipse(Graphics g, Rectangle rect, Color color1, Color color2)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(rect);
                using (PathGradientBrush brush = new PathGradientBrush(path))
                {
                    brush.CenterColor = color1;
                    brush.SurroundColors = new Color[] { color2 };
                    Blend blend = new Blend();
                    blend.Factors = new float[] { 0f, 0.8f, 1f };
                    blend.Positions = new float[] { 0f, 0.5f, 1f };
                    brush.Blend = blend;
                    g.FillPath(brush, path);
                }
            }
        }

        #endregion       


        /// <summary>
        /// 绘制水晶圆形按钮
        /// </summary>
        /// <param name="g">The Graphics.</param>
        /// <param name="rect">The Rectangle.</param>
        /// <param name="surroundColor">环绕色.</param>
        /// <param name="centerColor">中心色彩.</param>
        /// <param name="lightColor">渐变色彩.</param>
        /// <param name="blend">混合图例.</param>
        /// User:K.Anding  CreateTime:2011-8-20 19:42.
        public static void DrawCrystalButton(Graphics g, Rectangle rect, Color surroundColor, Color centerColor, Color lightColor, Blend blend)
        {
            int sweep, start;
            Point p1, p2, p3;
            Rectangle rinner = rect;
            rinner.Inflate(-1, -1);
            using (GraphicsPath p = new GraphicsPath())
            {
                p.AddEllipse(rect);

                using (PathGradientBrush gradient = new PathGradientBrush(p))
                {
                    gradient.WrapMode = WrapMode.Clamp;
                    gradient.CenterPoint = new PointF(Convert.ToSingle(rect.Left + rect.Width / 2), Convert.ToSingle(rect.Bottom));
                    gradient.CenterColor = centerColor;
                    gradient.SurroundColors = new Color[] { surroundColor };
                    gradient.Blend = blend;
                    g.FillPath(gradient, p);
                }
            }

            // Bottom round shine
            Rectangle bshine = new Rectangle(0, 0, rect.Width / 2, rect.Height / 2);
            bshine.X = rect.X + (rect.Width - bshine.Width) / 2;
            bshine.Y = rect.Y + rect.Height / 2;

            using (GraphicsPath p = new GraphicsPath())
            {
                p.AddEllipse(bshine);

                using (PathGradientBrush gradient = new PathGradientBrush(p))
                {
                    gradient.WrapMode = WrapMode.Clamp;
                    gradient.CenterPoint = new PointF(Convert.ToSingle(rect.Left + rect.Width / 2), Convert.ToSingle(rect.Bottom));
                    gradient.CenterColor = Color.White;
                    gradient.SurroundColors = new Color[] { Color.Transparent };

                    g.FillPath(gradient, p);
                }
            }

            // Upper Glossy
            using (GraphicsPath p = new GraphicsPath())
            {
                sweep = 160;
                start = 180 + (180 - sweep) / 2;
                p.AddArc(rinner, start, sweep);

                p1 = Point.Round(p.PathData.Points[0]);
                p2 = Point.Round(p.PathData.Points[p.PathData.Points.Length - 1]);
                p3 = new Point(rinner.Left + rinner.Width / 2, p2.Y - 3);
                p.AddCurve(new Point[] { p2, p3, p1 });

                using (PathGradientBrush gradient = new PathGradientBrush(p))
                {
                    gradient.WrapMode = WrapMode.Clamp;
                    gradient.CenterPoint = p3;
                    gradient.CenterColor = Color.Transparent;
                    gradient.SurroundColors = new Color[] { lightColor };

                    blend = new Blend(3);
                    blend.Factors = new float[] { .3f, .8f, 1f };
                    blend.Positions = new float[] { 0, 0.50f, 1f };
                    gradient.Blend = blend;

                    g.FillPath(gradient, p);
                }

                using (LinearGradientBrush b = new LinearGradientBrush(new Point(rect.Left, rect.Top), new Point(rect.Left, p1.Y), Color.White, Color.Transparent))
                {
                    blend = new Blend(4);
                    blend.Factors = new float[] { 0f, .4f, .8f, 1f };
                    blend.Positions = new float[] { 0f, .3f, .4f, 1f };
                    b.Blend = blend;
                    g.FillPath(b, p);
                }
            }

            // Upper shine
            using (GraphicsPath p = new GraphicsPath())
            {
                sweep = 160;
                start = 180 + (180 - sweep) / 2;
                p.AddArc(rinner, start, sweep);

                using (Pen pen = new Pen(Color.White))
                {
                    g.DrawPath(pen, p);
                }
            }

            // Lower Shine
            using (GraphicsPath p = new GraphicsPath())
            {
                sweep = 160;
                start = (180 - sweep) / 2;
                p.AddArc(rinner, start, sweep);
                Point pt = Point.Round(p.PathData.Points[0]);

                Rectangle rrinner = rinner; rrinner.Inflate(-1, -1);
                sweep = 160;
                start = (180 - sweep) / 2;
                p.AddArc(rrinner, start, sweep);

                using (LinearGradientBrush b = new LinearGradientBrush(
                    new Point(rinner.Left, rinner.Bottom),
                    new Point(rinner.Left, pt.Y - 1),
                    lightColor, Color.FromArgb(50, lightColor)))
                {
                    g.FillPath(b, p);
                }
            }
        }

        /// <summary>
        /// 在指定区域绘制图片(可设置图片透明度) (平铺绘制）
        /// Draws the image.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="rect">The rect.</param>
        /// <param name="img">The img.</param>
        /// User:Ryan  CreateTime:2012-8-3 21:12.
        public static void DrawImage(Graphics g, Rectangle rect, Image img, float opacity)
        {
            if (opacity <= 0)
            {
                return;
            }

            using (ImageAttributes imgAttributes = new ImageAttributes())
            {
                GDIHelper.SetImageOpacity(imgAttributes, opacity >= 1 ? 1 : opacity);
                Rectangle imageRect = new Rectangle(rect.X, rect.Y + rect.Height / 2 - img.Size.Height / 2, img.Size.Width, img.Size.Height);
                g.DrawImage(img, rect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttributes);
            }
        }

        /// <summary>
        /// 设置图片透明度.
        /// </summary>
        /// <param name="imgAttributes">The ImageAttributes.</param>
        /// <param name="opacity">透明度，0完全透明，1不透明（The opacity.）</param>
        /// User:Ryan  CreateTime:2011-07-28 15:26.
        public static void SetImageOpacity(ImageAttributes imgAttributes, float opacity)
        {
            float[][] nArray ={ new float[] {1, 0, 0, 0, 0},
                                                new float[] {0, 1, 0, 0, 0},
                                                new float[] {0, 0, 1, 0, 0},
                                                new float[] {0, 0, 0, opacity, 0},
                                                new float[] {0, 0, 0, 0, 1}};
            ColorMatrix matrix = new ColorMatrix(nArray);
            imgAttributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
        }

        /// <summary>
        /// 文本绘制
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="font"></param>
        /// <param name="text"></param>
        /// <param name="foreColor"></param>
        /// <param name="align"></param>
        public static void DrawString(Graphics g, Rectangle rect, Font font, string text, Color foreColor, TextAlignment align)
        {
            var fontSize = g.MeasureString(text, font);
            using (var brush = new SolidBrush(foreColor))
            {
                var fx = 0;
                var fy = 0;
                switch (align)
                {
                    case TextAlignment.BottomCenter:
                        fx = (rect.Width - (int)fontSize.Width) / 2 + rect.X;
                        fy = rect.Height - (int)fontSize.Height + rect.Y;
                        break;
                    case TextAlignment.BottomLeft:
                        fx = rect.X;
                        fy = rect.Height - (int)fontSize.Height + rect.Y;
                        break;
                    case TextAlignment.BottomRight:
                        fx = rect.X + rect.Width - (int)fontSize.Width;
                        fy = rect.Height - (int)fontSize.Height + rect.Y;
                        break;
                    case TextAlignment.MiddleLeft:
                        fx = rect.X;
                        fy = (rect.Height - (int)fontSize.Height) / 2 + rect.Y;
                        break;
                    case TextAlignment.MiddleRight:
                        fx = rect.X + rect.Width - (int)fontSize.Width;
                        fy = (rect.Height - (int)fontSize.Height) / 2 + rect.Y;
                        break;
                    case TextAlignment.TopCenter:
                        fx = (rect.Width - (int)fontSize.Width) / 2 + rect.X;
                        fy = rect.Y;
                        break;
                    case TextAlignment.TopLeft:
                        fx = rect.X;
                        fy = rect.Y;
                        break;
                    case TextAlignment.TopRight:
                        fx = rect.X + rect.Width - (int)fontSize.Width;
                        fy = rect.Y;
                        break;
                    default:
                    case TextAlignment.MiddleCenter:
                        fx = (rect.Width - (int)fontSize.Width) / 2 + rect.X;
                        fy = (rect.Height - (int)fontSize.Height) / 2 + rect.Y;
                        break;
                }

                g.DrawString(text, font, brush, fx, fy);
            }
        }

        /// <summary>
        /// 绘制一个带有圆角背景颜色的文字块
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="font"></param>
        /// <param name="text"></param>
        /// <param name="foreColor"></param>
        /// <param name="backColor"></param>
        /// <param name="raidusMode"></param>
        /// <param name="radius"></param>
        public static void DrawString(Graphics g, Rectangle rect, Font font, string text, Color foreColor, Color backColor, TextAlignment align, RadiusMode raidusMode, int radius)
        {
            // 先绘制圆角
            RadiusDrawable.DrawRadius(g, rect, raidusMode, radius, backColor, Color.Empty, GradientMode.None, Color.Empty, 0);

            DrawString(g, rect, font, text, foreColor, align);
        }
    }
}
