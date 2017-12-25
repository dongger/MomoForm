using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Momo.Forms
{
    public sealed class MPictureBox : PictureBox
    {
        public MPictureBox()
        {
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer, true);
            this.timer = new Timer();
            this.timer.Enabled = false;
            this.timer.Interval = 10;
            this.timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            switch (ImageAnimation)
            {
                case ImageAnimation.BlindsHorizontal:
                    BlindsHorizontal();
                    break;
                case ImageAnimation.BlindsVertical:
                    BlindsVertical();
                    break;
                case ImageAnimation.FadeIn:
                    FadeIin();
                    break;
            }
        }

        private void BlindsVertical()
        {
            var dh = 0;
            if (timer.Tag != null)
            {
                dh = Convert.ToInt32(timer.Tag);
            }

            if (points == null)
            {
                points = new Point[30];
                for (int x = 0; x < 30; x++)
                {
                    points[x].Y = 0;
                    points[x].X = x * this.bitmap.Width / 30;
                }
            }

            if (temp == null)
            {
                temp = new Bitmap(this.bitmap.Width, this.bitmap.Height);
            }

            var height = this.bitmap.Height;
            if (dh < this.bitmap.Height / 30)
            {
                for (int j = 0; j < 30; j++)
                {
                    for (int k = 0; k < height; k++)
                    {
                        temp.SetPixel(points[j].X + dh, points[j].Y + k, this.bitmap.GetPixel(points[j].X + dh, points[j].Y + k));
                    }
                }

                base.Image = temp;

                dh++;
                timer.Tag = dh;
            }
            else
            {
                this.timer.Enabled = false;
                base.Image = image;
            }
        }
        private void BlindsHorizontal()
        {
            var dh = 0;
            if (timer.Tag != null)
            {
                dh = Convert.ToInt32(timer.Tag);
            }

            if (points == null)
            {
                points = new Point[20];
                for (int x = 0; x < 20; x++)
                {
                    points[x].Y = x * this.bitmap.Height / 20;
                    points[x].X = 0;
                }
            }

            if (temp == null)
            {
                temp = new Bitmap(this.bitmap.Width, this.bitmap.Height);
            }

            var width = this.bitmap.Width;
            if (dh < this.bitmap.Height / 20)
            {
                for (int j = 0; j < 20; j++)
                {
                    for (int k = 0; k < width; k++)
                    {
                        temp.SetPixel(points[j].X + k, points[j].Y + dh, this.bitmap.GetPixel(points[j].X + k, points[j].Y + dh));
                    }
                }

                base.Image = temp;

                dh++;
                timer.Tag = dh;
            }
            else
            {
                this.timer.Enabled = false;
                base.Image = image;
            }
        }

        public void FadeIin()
        {
            if (temp == null)
            {
                temp = new Bitmap(this.image.Width, this.image.Height);
            }

            var a = 0;
            if (timer.Tag != null)
            {
                a = Convert.ToInt32(timer.Tag);
            }

            if (a < 100)
            {
                for (int h = 0; h < temp.Height; h++)
                {
                    for (int w = 0; w < temp.Width; w++)
                    {
                        Color c = (image as Bitmap).GetPixel(w, h);
                        temp.SetPixel(w, h, Color.FromArgb(a, c.R, c.G, c.B));
                    }
                }

                a += 10;
                if (a > 100)
                {
                    a = 100;
                }

                timer.Tag = a;
                base.Image = temp;
            }
            else
            {
                this.timer.Enabled = false;
                base.Image = image;
            }
        }

        private Timer timer;

        private Image image;

        private Bitmap bitmap;

        private Bitmap temp;

        private Point[] points;

        public new Image Image
        {
            get { return this.image; }
            set
            {
                this.image = value;
                if (value == null)
                {
                    return;
                }
                timer.Tag = null;
                this.bitmap = new Bitmap(value);

                if (!DesignMode)
                {
                    if (this.timer.Enabled)
                    {
                        this.timer.Stop();
                    }

                    this.timer.Tag = null;
                    this.points = null;
                    this.temp = null;
                    if (this.ImageAnimation != ImageAnimation.None)
                    {
                        this.timer.Enabled = true;
                    }
                    else
                    {
                        base.Image = value;
                    }
                }
                else
                {
                    base.Image = value;
                    this.Invalidate();
                }
            }
        }

        [Browsable(true), Category("Momo"), Description("图片动画展示方式")]
        public ImageAnimation ImageAnimation { get; set; }
    }
}
