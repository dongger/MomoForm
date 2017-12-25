using System;
using System.Drawing;
using System.Windows.Forms;

namespace Momo.Forms
{
    public static class RadiusDrawable
    {
        public static void DrawRadius(Graphics g, System.Drawing.Rectangle rect, RadiusMode radiusMode, int radius, Color c1, Color c2, GradientMode linearGradientMode, Color borderColor, int borderWidth)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using (System.Drawing.Drawing2D.GraphicsPath graphPath = GetPath(rect, radiusMode, radius))
            {
                if (rect.Width == 0)
                {
                    rect.Width += 1;
                }
                if (rect.Height == 0)
                {
                    rect.Height += 1;
                }

                System.Drawing.Drawing2D.LinearGradientBrush brush;
                if (linearGradientMode == GradientMode.None)
                {
                    brush = new System.Drawing.Drawing2D.LinearGradientBrush(rect, c1, c1, System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                }
                else
                {
                    brush = new System.Drawing.Drawing2D.LinearGradientBrush(rect, c1, c2.IsEmpty ? c1 : c2, ((System.Drawing.Drawing2D.LinearGradientMode)linearGradientMode));
                }

                if (radiusMode != RadiusMode.None && radius > 0)
                {
                    g.FillPath(brush, graphPath);

                    using (System.Drawing.Pen borderPen = new System.Drawing.Pen(borderColor, borderWidth))
                    {
                        g.DrawPath(borderPen, graphPath);
                        borderPen.Dispose();
                    }
                }
                else
                {
                    g.FillRectangle(brush, rect);

                    if (borderWidth > 0 && !borderColor.IsEmpty)
                    {
                        ControlPaint.DrawBorder(g, rect, borderColor, ButtonBorderStyle.Solid);
                    }
                }

                brush.Dispose();
            }
        }

        private static int GetAdjustedCurve(System.Drawing.Rectangle rect, RadiusMode radiusMode, int radius)
        {
            int curve = 0;
            if (radiusMode != RadiusMode.None)
            {
                if (radius > (rect.Width / 2))
                {
                    curve = DoubleToInt(rect.Width / 2);
                }
                else
                {
                    curve = radius;
                }

                if (curve > (rect.Height / 2))
                {
                    curve = DoubleToInt(rect.Height / 2);
                }
            }
            return curve;
        }

        private static int DoubleToInt(double value)
        {
            return System.Decimal.ToInt32(System.Decimal.Floor(System.Decimal.Parse((value).ToString())));
        }

        private static System.Drawing.Drawing2D.GraphicsPath GetPath(System.Drawing.Rectangle rect, RadiusMode radiusMode, int radius)
        {
            System.Drawing.Drawing2D.GraphicsPath graphPath = new System.Drawing.Drawing2D.GraphicsPath();

            try
            {
                int curve = 0;
                int offset = 0;
                curve = GetAdjustedCurve(rect, radiusMode, radius);

                if (curve == 0)
                {
                    graphPath.AddRectangle(System.Drawing.Rectangle.Inflate(rect, -offset, -offset));
                }
                else
                {
                    int rectWidth = rect.Width - 1 - offset;
                    int rectHeight = rect.Height - 1 - offset;
                    int curveWidth = 1;
                    if ((radiusMode & RadiusMode.TopRight) != 0)
                    {
                        curveWidth = (curve * 2);
                    }
                    else
                    {
                        curveWidth = 1;
                    }
                    graphPath.AddArc(rectWidth - curveWidth + rect.X, offset + rect.Y, curveWidth, curveWidth, 270, 90);
                    if ((radiusMode & RadiusMode.BottomRight) != 0)
                    {
                        curveWidth = (curve * 2);
                    }
                    else
                    {
                        curveWidth = 1;
                    }
                    graphPath.AddArc(rectWidth - curveWidth + rect.X, rectHeight - curveWidth+rect.Y, curveWidth, curveWidth, 0, 90);
                    if ((radiusMode & RadiusMode.BottomLeft) != 0)
                    {
                        curveWidth = (curve * 2);
                    }
                    else
                    {
                        curveWidth = 1;
                    }
                    graphPath.AddArc(offset + rect.X, rectHeight - curveWidth + rect.Y, curveWidth, curveWidth, 90, 90);
                    if ((radiusMode & RadiusMode.TopLeft) != 0)
                    {
                        curveWidth = (curve * 2);
                    }
                    else
                    {
                        curveWidth = 1;
                    }
                    graphPath.AddArc(offset + rect.X, offset + rect.Y, curveWidth, curveWidth, 180, 90);
                    graphPath.CloseFigure();
                }
            }
            catch (System.Exception)
            {
                graphPath.AddRectangle(rect);
            }

            return graphPath;
        }

        public static void DrawRadius(Graphics graphics, Rectangle rect, RadiusMode radiusMode, int radius, Color backColor, Color backColorGradient, GradientMode gradient, object color, int width)
        {
            throw new NotImplementedException();
        }
    }
}
