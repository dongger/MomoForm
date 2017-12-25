using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
namespace Momo.Forms
{
    public class ImageTool
    {
        /// <summary>
        /// 将图片转换成黑白色效果
        /// </summary>
        /// <param name="bmp">Bitmap 对象</param>
        /// <param name="picBox">PictureBox 对象</param>
        public static void HeiBaiSeImage(Bitmap bmp, PictureBox picBox)
        {
            //以黑白效果显示图像
            Bitmap oldBitmap;
            Bitmap newBitmap = null;
            try
            {
                int Height = bmp.Height;
                int Width = bmp.Width;
                newBitmap = new Bitmap(Width, Height);
                oldBitmap = bmp;
                Color pixel;
                for (int x = 0; x < Width; x++)
                    for (int y = 0; y < Height; y++)
                    {
                        pixel = oldBitmap.GetPixel(x, y);
                        int r, g, b, Result = 0;
                        r = pixel.R;
                        g = pixel.G;
                        b = pixel.B;
                        //实例程序以加权平均值法产生黑白图像
                        int iType = 2;
                        switch (iType)
                        {
                            case 0://平均值法
                                Result = ((r + g + b) / 3);
                                break;
                            case 1://最大值法
                                Result = r > g ? r : g;
                                Result = Result > b ? Result : b;
                                break;
                            case 2://加权平均值法
                                Result = ((int)(0.7 * r) + (int)(0.2 * g) + (int)(0.1 * b));
                                break;
                        }
                        newBitmap.SetPixel(x, y, Color.FromArgb(Result, Result, Result));
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示");
            }
            picBox.Image = newBitmap;
        }
        /// <summary>
        /// 雾化效果
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="picBox"></param>
        public static void WuHuaImage(Bitmap bmp, PictureBox picBox)
        {
            //雾化效果
            Bitmap oldBitmap;
            Bitmap newBitmap = null;
            try
            {
                int Height = bmp.Height;
                int Width = bmp.Width;
                newBitmap = new Bitmap(Width, Height);
                oldBitmap = bmp;
                Color pixel;
                for (int x = 1; x < Width - 1; x++)
                    for (int y = 1; y < Height - 1; y++)
                    {
                        System.Random MyRandom = new Random();
                        int k = MyRandom.Next(123456);
                        //像素块大小
                        int dx = x + k % 19;
                        int dy = y + k % 19;
                        if (dx >= Width)
                            dx = Width - 1;
                        if (dy >= Height)
                            dy = Height - 1;
                        pixel = oldBitmap.GetPixel(dx, dy);
                        newBitmap.SetPixel(x, y, pixel);
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示");
            }
            picBox.Image = newBitmap;
        }

        /// <summary>
        /// 锐化效果
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="picBox"></param>
        public static void RuiHuaImage(Bitmap bmp, PictureBox picBox)
        {
            Bitmap oldBitmap;
            Bitmap newBitmap = null;
            try
            {
                int Height = bmp.Height;
                int Width = bmp.Width;
                newBitmap = new Bitmap(Width, Height);
                oldBitmap = bmp;
                Color pixel;
                //拉普拉斯模板
                int[] Laplacian = { -1, -1, -1, -1, 9, -1, -1, -1, -1 };
                for (int x = 1; x < Width - 1; x++)
                    for (int y = 1; y < Height - 1; y++)
                    {
                        int r = 0, g = 0, b = 0;
                        int Index = 0;
                        for (int col = -1; col <= 1; col++)
                            for (int row = -1; row <= 1; row++)
                            {
                                pixel = oldBitmap.GetPixel(x + row, y + col); r += pixel.R * Laplacian[Index];
                                g += pixel.G * Laplacian[Index];
                                b += pixel.B * Laplacian[Index];
                                Index++;
                            }
                        //处理颜色值溢出
                        r = r > 255 ? 255 : r;
                        r = r < 0 ? 0 : r;
                        g = g > 255 ? 255 : g;
                        g = g < 0 ? 0 : g;
                        b = b > 255 ? 255 : b;
                        b = b < 0 ? 0 : b;
                        newBitmap.SetPixel(x - 1, y - 1, Color.FromArgb(r, g, b));
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示");
            }
            picBox.Image = newBitmap;
        }
        /// <summary>
        ///底片效果
        /// </summary>
        /// <param name="bmp">Bitmap 对象</param>
        /// <param name="picBox">PictureBox 对象</param>
        public static void DiPianImage(Bitmap bmp, PictureBox picBox)
        {
            Bitmap oldBitmap;
            Bitmap newBitmap = null;
            try
            {
                int Height = bmp.Height;
                int Width = bmp.Width;
                newBitmap = new Bitmap(Width, Height);
                oldBitmap = bmp;
                Color pixel;
                for (int x = 1; x < Width; x++)
                {
                    for (int y = 1; y < Height; y++)
                    {
                        int r, g, b;
                        pixel = oldBitmap.GetPixel(x, y);
                        r = 255 - pixel.R;
                        g = 255 - pixel.G;
                        b = 255 - pixel.B;
                        newBitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            picBox.Image = newBitmap;
        }

        /// <summary>
        ///浮雕效果
        /// </summary>
        /// <param name="bmp">Bitmap 对象</param>
        /// <param name="picBox">PictureBox 对象</param>
        public static void FuDiaoImage(Bitmap bmp, PictureBox picBox)
        {
            Bitmap oldBitmap;
            Bitmap newBitmap = null;
            try
            {
                int Height = bmp.Height;
                int Width = bmp.Width;
                newBitmap = new Bitmap(Width, Height);
                oldBitmap = bmp;
                Color pixel1, pixel2;
                for (int x = 0; x < Width - 1; x++)
                {
                    for (int y = 0; y < Height - 1; y++)
                    {
                        int r = 0, g = 0, b = 0;
                        pixel1 = oldBitmap.GetPixel(x, y);
                        pixel2 = oldBitmap.GetPixel(x + 1, y + 1);
                        r = Math.Abs(pixel1.R - pixel2.R + 128);
                        g = Math.Abs(pixel1.G - pixel2.G + 128);
                        b = Math.Abs(pixel1.B - pixel2.B + 128);
                        if (r > 255)
                            r = 255;
                        if (r < 0)
                            r = 0;
                        if (g > 255)
                            g = 255;
                        if (g < 0)
                            g = 0;
                        if (b > 255)
                            b = 255;
                        if (b < 0)
                            b = 0;
                        newBitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            picBox.Image = newBitmap;
        }

        /// <summary>
        /// 日光照射效果
        /// </summary>
        /// <param name="bmp">Bitmap 对象</param>
        /// <param name="picBox">PictureBox 对象</param>
        public static void RiGuangZhaoSheImage(Bitmap bmp, PictureBox picBox)
        {
            //以光照效果显示图像
            Graphics MyGraphics = picBox.CreateGraphics();
            MyGraphics.Clear(Color.White);
            Bitmap MyBmp = new Bitmap(bmp, bmp.Width, bmp.Height);
            int MyWidth = MyBmp.Width;
            int MyHeight = MyBmp.Height;
            Bitmap MyImage = MyBmp.Clone(new RectangleF(0, 0, MyWidth, MyHeight), System.Drawing.Imaging.PixelFormat.DontCare);
            int A = MyWidth / 2;
            int B = MyHeight / 2;
            //MyCenter图片中心点，发亮此值会让强光中心发生偏移
            Point MyCenter = new Point(MyWidth / 2, MyHeight / 2);
            //R强光照射面的半径，即”光晕”
            int R = Math.Min(MyWidth / 2, MyHeight / 2);
            for (int i = MyWidth - 1; i >= 1; i--)
            {
                for (int j = MyHeight - 1; j >= 1; j--)
                {
                    float MyLength = (float)Math.Sqrt(Math.Pow((i - MyCenter.X), 2) + Math.Pow((j - MyCenter.Y), 2));
                    //如果像素位于”光晕”之内
                    if (MyLength < R)
                    {
                        Color MyColor = MyImage.GetPixel(i, j);
                        int r, g, b;
                        //220亮度增加常量，该值越大，光亮度越强
                        float MyPixel = 220.0f * (1.0f - MyLength / R);
                        r = MyColor.R + (int)MyPixel;
                        r = Math.Max(0, Math.Min(r, 255));
                        g = MyColor.G + (int)MyPixel;
                        g = Math.Max(0, Math.Min(g, 255));
                        b = MyColor.B + (int)MyPixel;
                        b = Math.Max(0, Math.Min(b, 255));
                        //将增亮后的像素值回写到位图
                        Color MyNewColor = Color.FromArgb(255, r, g, b);
                        MyImage.SetPixel(i, j, MyNewColor);
                    }
                }
                //重新绘制图片
                MyGraphics.DrawImage(MyImage, new Rectangle(0, 0, MyWidth, MyHeight));
            }
        }

        /// <summary>
        /// 油画效果
        /// </summary>
        /// <param name="bmp">Bitmap 对象</param>
        /// <param name="picBox">PictureBox 对象</param>
        public static void YouHuaImage(Bitmap bmp, PictureBox picBox)
        {
            //以油画效果显示图像
            Graphics g = picBox.CreateGraphics();
            int width = bmp.Width;
            int height = bmp.Height;
            RectangleF rect = new RectangleF(0, 0, width, height);
            Bitmap MyBitmap = bmp;
            Bitmap img = MyBitmap.Clone(rect, System.Drawing.Imaging.PixelFormat.DontCare);
            //产生随机数序列
            Random rnd = new Random();
            //取不同的值决定油画效果的不同程度
            int iModel = 2;
            int i = width - iModel;
            while (i > 1)
            {
                int j = height - iModel;
                while (j > 1)
                {
                    int iPos = rnd.Next(100000) % iModel;
                    //将该点的RGB值设置成附近iModel点之内的任一点
                    Color color = img.GetPixel(i + iPos, j + iPos);
                    img.SetPixel(i, j, color);
                    j = j - 1;
                }
                i = i - 1;
            }
            //重新绘制图像
            g.Clear(Color.White);
            g.DrawImage(img, new Rectangle(0, 0, width, height));
        }

        /// <summary>
        /// 左右拉伸效果
        /// </summary>
        /// <param name="bmp">Bitmap 对象</param>
        /// <param name="picBox">PictureBox 对象</param>
        public static void LaShen_ZuoDaoYou(Bitmap bmp, PictureBox picBox)
        {
            //以从左向右拉伸方式显示图像
            try
            {
                int width = bmp.Width; //图像宽度
                int height = bmp.Height; //图像高度
                Graphics g = picBox.CreateGraphics();
                g.Clear(Color.Gray); //初始为全灰色
                for (int x = 1; x <= width; x++)
                {
                    Bitmap bitmap = bmp.Clone(new Rectangle(0, 0, x, height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    g.DrawImage(bitmap, 0, 0);
                    System.Threading.Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示");
            }
        }

        /// <summary>
        /// 淡入效果
        /// </summary>
        /// <param name="bmp">Bitmap 对象</param>
        /// <param name="picBox">PictureBox 对象</param>
        public static void DanRu(Bitmap bmp, PictureBox picBox)
        {
            //淡入显示图像
            try
            {
                Graphics g = picBox.CreateGraphics();
                g.Clear(Color.Gray);
                int width = bmp.Width;
                int height = bmp.Height;
                ImageAttributes attributes = new ImageAttributes();
                ColorMatrix matrix = new ColorMatrix();
                //创建淡入颜色矩阵
                matrix.Matrix00 = (float)0.0;
                matrix.Matrix01 = (float)0.0;
                matrix.Matrix02 = (float)0.0;
                matrix.Matrix03 = (float)0.0;
                matrix.Matrix04 = (float)0.0;
                matrix.Matrix10 = (float)0.0;
                matrix.Matrix11 = (float)0.0;
                matrix.Matrix12 = (float)0.0;
                matrix.Matrix13 = (float)0.0;
                matrix.Matrix14 = (float)0.0;
                matrix.Matrix20 = (float)0.0;
                matrix.Matrix21 = (float)0.0;
                matrix.Matrix22 = (float)0.0;
                matrix.Matrix23 = (float)0.0;
                matrix.Matrix24 = (float)0.0;
                matrix.Matrix30 = (float)0.0;
                matrix.Matrix31 = (float)0.0;
                matrix.Matrix32 = (float)0.0;
                matrix.Matrix33 = (float)0.0;
                matrix.Matrix34 = (float)0.0;
                matrix.Matrix40 = (float)0.0;
                matrix.Matrix41 = (float)0.0;
                matrix.Matrix42 = (float)0.0;
                matrix.Matrix43 = (float)0.0;
                matrix.Matrix44 = (float)0.0;
                matrix.Matrix33 = (float)1.0;
                matrix.Matrix44 = (float)1.0;
                //从0到1进行修改色彩变换矩阵主对角线上的数值
                //使三种基准色的饱和度渐增
                Single count = (float)0.0;
                while (count < 1.0)
                {
                    matrix.Matrix00 = count;
                    matrix.Matrix11 = count;
                    matrix.Matrix22 = count;
                    matrix.Matrix33 = count;
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                    g.DrawImage(bmp, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel, attributes);
                    System.Threading.Thread.Sleep(200);
                    count = (float)(count + 0.02);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示");
            }
        }

        /// <summary>
        /// 逆时针旋转
        /// </summary>
        /// <param name="bmp">Bitmap 对象</param>
        /// <param name="picBox">PictureBox 对象</param>
        public static void XuanZhuan90(Bitmap bmp, PictureBox picBox)
        {
            try
            {
                Graphics g = picBox.CreateGraphics();
                bmp.RotateFlip(RotateFlipType.Rotate90FlipXY);
                g.Clear(Color.White);
                g.DrawImage(bmp, 0, 0);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        /// <summary>
        /// 顺时针旋转
        /// </summary>
        /// <param name="bmp">Bitmap 对象</param>
        /// <param name="picBox">PictureBox 对象</param>
        public static void XuanZhuan270(Bitmap bmp, PictureBox picBox)
        {
            try
            {
                Graphics g = picBox.CreateGraphics();
                bmp.RotateFlip(RotateFlipType.Rotate270FlipXY);
                g.Clear(Color.White);
                g.DrawImage(bmp, 0, 0);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        /// <summary>
        /// 分块显示
        /// </summary>
        /// <param name="bmp">Bitmap 对象</param>
        /// <param name="picBox">PictureBox 对象</param>
        public static void FenKuai(Bitmap MyBitmap, PictureBox picBox)
        {
            //以分块效果显示图像
            Graphics g = picBox.CreateGraphics();
            g.Clear(Color.White);
            int width = MyBitmap.Width;
            int height = MyBitmap.Height;
            //定义将图片切分成四个部分的区域
            RectangleF[] block ={
          new RectangleF(0,0,width/2,height/2),
          new RectangleF(width/2,0,width/2,height/2),
          new RectangleF(0,height/2,width/2,height/2),
          new RectangleF(width/2,height/2,width/2,height/2)};
            //分别克隆图片的四个部分
            Bitmap[] MyBitmapBlack ={
        MyBitmap.Clone(block[0],System.Drawing.Imaging.PixelFormat.DontCare),
        MyBitmap.Clone(block[1],System.Drawing.Imaging.PixelFormat.DontCare),
        MyBitmap.Clone(block[2],System.Drawing.Imaging.PixelFormat.DontCare),
        MyBitmap.Clone(block[3],System.Drawing.Imaging.PixelFormat.DontCare)};
            //绘制图片的四个部分，各部分绘制时间间隔为0.5秒
            g.DrawImage(MyBitmapBlack[0], 0, 0);
            System.Threading.Thread.Sleep(500);
            g.DrawImage(MyBitmapBlack[1], width / 2, 0);
            System.Threading.Thread.Sleep(500);
            g.DrawImage(MyBitmapBlack[3], width / 2, height / 2);
            System.Threading.Thread.Sleep(500);
            g.DrawImage(MyBitmapBlack[2], 0, height / 2);
        }

        /// <summary>
        /// 积木特效
        /// </summary>
        /// <param name="bmp">Bitmap 对象</param>
        /// <param name="picBox">PictureBox 对象</param>
        public static void JiMu(Bitmap MyBitmap, PictureBox picBox)
        {
            //以积木效果显示图像
            try
            {
                Graphics myGraphics = picBox.CreateGraphics();
                int myWidth, myHeight, i, j, iAvg, iPixel;
                Color myColor, myNewColor;
                RectangleF myRect;
                myWidth = MyBitmap.Width;
                myHeight = MyBitmap.Height;
                myRect = new RectangleF(0, 0, myWidth, myHeight);
                Bitmap bitmap = MyBitmap.Clone(myRect, System.Drawing.Imaging.PixelFormat.DontCare);
                i = 0;
                while (i < myWidth - 1)
                {
                    j = 0;
                    while (j < myHeight - 1)
                    {
                        myColor = bitmap.GetPixel(i, j);
                        iAvg = (myColor.R + myColor.G + myColor.B) / 3;
                        iPixel = 0;
                        if (iAvg >= 128)
                            iPixel = 255;
                        else
                            iPixel = 0;
                        myNewColor = Color.FromArgb(255, iPixel, iPixel, iPixel);
                        bitmap.SetPixel(i, j, myNewColor);
                        j = j + 1;
                    }
                    i = i + 1;
                }
                myGraphics.Clear(Color.WhiteSmoke);
                myGraphics.DrawImage(bitmap, new Rectangle(0, 0, myWidth, myHeight));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示");
            }
        }

        /// <summary>
        /// 马赛克效果
        /// </summary>
        /// <param name="bmp">Bitmap 对象</param>
        /// <param name="picBox">PictureBox 对象</param>
        public static void MaSaiKe(Bitmap MyBitmap, PictureBox picBox)
        {
            //以马赛克效果显示图像
            try
            {
                int dw = MyBitmap.Width / 50;
                int dh = MyBitmap.Height / 50;
                Graphics g = picBox.CreateGraphics();
                g.Clear(Color.Gray);
                Point[] MyPoint = new Point[2500];
                for (int x = 0; x < 50; x++)
                    for (int y = 0; y < 50; y++)
                    {
                        MyPoint[x * 50 + y].X = x * dw;
                        MyPoint[x * 50 + y].Y = y * dh;
                    }
                Bitmap bitmap = new Bitmap(MyBitmap.Width, MyBitmap.Height);
                for (int i = 0; i < 10000; i++)
                {
                    System.Random MyRandom = new Random();
                    int iPos = MyRandom.Next(2500);
                    for (int m = 0; m < dw; m++)
                        for (int n = 0; n < dh; n++)
                        {
                            bitmap.SetPixel(MyPoint[iPos].X + m, MyPoint[iPos].Y + n, MyBitmap.GetPixel(MyPoint[iPos].X + m, MyPoint[iPos].Y + n));
                        }
                    picBox.Refresh();
                    picBox.Image = bitmap;
                }
                for (int i = 0; i < 2500; i++)
                    for (int m = 0; m < dw; m++)
                        for (int n = 0; n < dh; n++)
                        {
                            bitmap.SetPixel(MyPoint[i].X + m, MyPoint[i].Y + n, MyBitmap.GetPixel(MyPoint[i].X + m, MyPoint[i].Y + n));
                        }
                picBox.Refresh();
                picBox.Image = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示");
            }
        }

        /// <summary>
        /// 自动旋转
        /// </summary>
        /// <param name="bmp">Bitmap 对象</param>
        /// <param name="picBox">PictureBox 对象</param>
        public static void XuanZhuan(Bitmap MyBitmap, PictureBox picBox)
        {
            Graphics g = picBox.CreateGraphics();
            float MyAngle = 0;//旋转的角度
            while (MyAngle < 360)
            {
                TextureBrush MyBrush = new TextureBrush(MyBitmap);
                picBox.Refresh();
                MyBrush.RotateTransform(MyAngle);
                g.FillRectangle(MyBrush, 0, 0, MyBitmap.Width, MyBitmap.Height);
                MyAngle += 0.5f;
                System.Threading.Thread.Sleep(50);
            }
        }

        /// <summary>
        /// 上下对接
        /// </summary>
        /// <param name="bmp">Bitmap 对象</param>
        /// <param name="picBox">PictureBox 对象</param>
        public static void DuiJie_ShangXia(Bitmap MyBitmap, PictureBox picBox)
        {
            //以上下对接方式显示图像
            try
            {
                int width = MyBitmap.Width; //图像宽度
                int height = MyBitmap.Height; //图像高度
                Graphics g = picBox.CreateGraphics();
                g.Clear(Color.Gray); //初始为全灰色
                Bitmap bitmap = new Bitmap(width, height);
                int x = 0;
                while (x <= height / 2)
                {
                    for (int i = 0; i <= width - 1; i++)
                    {
                        bitmap.SetPixel(i, x, MyBitmap.GetPixel(i, x));
                    }
                    for (int i = 0; i <= width - 1; i++)
                    {
                        bitmap.SetPixel(i, height - x - 1, MyBitmap.GetPixel(i, height - x - 1));
                    }
                    x++;
                    g.Clear(Color.Gray);
                    g.DrawImage(bitmap, 0, 0);
                    System.Threading.Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示");
            }
        }

        /// <summary>
        /// 上下翻转
        /// </summary>
        /// <param name="bmp">Bitmap 对象</param>
        /// <param name="picBox">PictureBox 对象</param>
        public static void FanZhuan_ShangXia(Bitmap MyBitmap, PictureBox picBox)
        {
            //以上下反转方式显示图像
            try
            {
                int width = MyBitmap.Width; //图像宽度
                int height = MyBitmap.Height; //图像高度
                Graphics g = picBox.CreateGraphics();
                g.Clear(Color.Gray); //初始为全灰色
                for (int i = -width / 2; i <= width / 2; i++)
                {
                    g.Clear(Color.Gray); //初始为全灰色
                    int j = Convert.ToInt32(i * (Convert.ToSingle(height) / Convert.ToSingle(width)));
                    Rectangle DestRect = new Rectangle(0, height / 2 - j, width, 2 * j);
                    Rectangle SrcRect = new Rectangle(0, 0, MyBitmap.Width, MyBitmap.Height);
                    g.DrawImage(MyBitmap, DestRect, SrcRect, GraphicsUnit.Pixel);
                    System.Threading.Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示");
            }
        }

        /// <summary>
        /// 左右对接
        /// </summary>
        /// <param name="bmp">Bitmap 对象</param>
        /// <param name="picBox">PictureBox 对象</param>
        public static void DuiJie_ZuoYou(Bitmap MyBitmap, PictureBox picBox)
        {
            //以左右对接方式显示图像
            try
            {
                int width = MyBitmap.Width; //图像宽度
                int height = MyBitmap.Height; //图像高度
                Graphics g = picBox.CreateGraphics();
                g.Clear(Color.Gray); //初始为全灰色
                Bitmap bitmap = new Bitmap(width, height);
                int x = 0;
                while (x <= width / 2)
                {
                    for (int i = 0; i <= height - 1; i++)
                    {
                        bitmap.SetPixel(x, i, MyBitmap.GetPixel(x, i));
                    }
                    for (int i = 0; i <= height - 1; i++)
                    {
                        bitmap.SetPixel(width - x - 1, i,
                        MyBitmap.GetPixel(width - x - 1, i));
                    }
                    x++;
                    g.Clear(Color.Gray);
                    g.DrawImage(bitmap, 0, 0);
                    System.Threading.Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示");
            }
        }
        /// <summary>
        /// 左右翻转
        /// </summary>
        /// <param name="bmp">Bitmap 对象</param>
        /// <param name="picBox">PictureBox 对象</param>
        public static void FanZhuan_ZuoYou(Bitmap MyBitmap, PictureBox picBox)
        {
            //以左右反转方式显示图像
            try
            {
                int width = MyBitmap.Width; //图像宽度
                int height = MyBitmap.Height; //图像高度
                Graphics g = picBox.CreateGraphics();
                g.Clear(Color.Gray); //初始为全灰色
                for (int j = -height / 2; j <= height / 2; j++)
                {
                    g.Clear(Color.Gray); //初始为全灰色
                    int i = Convert.ToInt32(j * (Convert.ToSingle(width) / Convert.ToSingle(height)));
                    Rectangle DestRect = new Rectangle(width / 2 - i, 0, 2 * i, height);
                    Rectangle SrcRect = new Rectangle(0, 0, MyBitmap.Width, MyBitmap.Height);
                    g.DrawImage(MyBitmap, DestRect, SrcRect, GraphicsUnit.Pixel);
                    System.Threading.Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示");
            }
        }

        /// <summary>
        /// 四周扩散
        /// </summary>
        /// <param name="bmp">Bitmap 对象</param>
        /// <param name="picBox">PictureBox 对象</param>
        public static void KuoSan(Bitmap MyBitmap, PictureBox picBox)
        {
            try
            {
                int width = MyBitmap.Width; //图像宽度
                int height = MyBitmap.Height; //图像高度
                Graphics g = picBox.CreateGraphics();
                g.Clear(Color.Gray); //初始为全灰色
                for (int i = 0; i <= width / 2; i++)
                {
                    int j = Convert.ToInt32(i * (Convert.ToSingle(height) / Convert.ToSingle(width)));
                    Rectangle DestRect = new Rectangle(width / 2 - i, height / 2 - j, 2 * i, 2 * j);
                    Rectangle SrcRect = new Rectangle(0, 0, MyBitmap.Width, MyBitmap.Height);
                    g.DrawImage(MyBitmap, DestRect, SrcRect, GraphicsUnit.Pixel);
                    System.Threading.Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示");
            }
        }
        /// <summary>
        /// 上下拉伸
        /// </summary>
        /// <param name="bmp">Bitmap 对象</param>
        /// <param name="picBox">PictureBox 对象</param>
        public static void LaShen_ShangDaoXiao(Bitmap MyBitmap, PictureBox picBox)
        {
            //以从上向下拉伸方式显示图像
            try
            {
                int width = MyBitmap.Width; //图像宽度
                int height = MyBitmap.Height; //图像高度
                Graphics g = picBox.CreateGraphics();
                g.Clear(Color.Gray); //初始为全灰色
                for (int y = 1; y <= height; y++)
                {
                    Bitmap bitmap = MyBitmap.Clone(new Rectangle(0, 0, width, y), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    g.DrawImage(bitmap, 0, 0);
                    System.Threading.Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示");
            }
        }
    }
}