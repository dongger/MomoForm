using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;

using System.Text;
using System.Windows.Forms;

namespace Momo.Forms
{
    /// <summary>
    /// 横幅幻灯片
    /// </summary>
    public sealed class MBanner : UserControl
    {
        public MBanner()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            InitializeComponent();
            this.BackColor = Color.Transparent;
            this.TabStop = false;
            this.timer.Enabled = false;
            this.timer.Interval = 2000;
            this.timer.Tick += timer_Tick;
            

            this.Images = new List<Bitmap>();
        }

        /// <summary>
        /// 当前图片索引
        /// </summary>
        private int currentIndex = 0;

        private int animationLoction = 0;

        private List<PointPalette> points = new List<PointPalette>();

        private Timer timer = new Timer();

        /// <summary>
        /// 需要放映的图片列表
        /// </summary>        
        private readonly List<Image> imageList = new List<Image>();

        /// <summary>
        /// 移动方向
        /// </summary>
        [Category("幻灯片"), Description("移动方向")]
        public Direction Direction { get; set; }

        private Rectangle leftRectangle;
        private MPictureBox mPictureBox1;
        private Rectangle rightRectangle;

        protected override Size DefaultSize
        {
            get
            {
                return new Size(600, 300);
            }
        }

        /// <summary>
        /// 切换频率
        /// </summary>
        [Browsable(true), Category("Momo"), Description("切换频率")]
        public int Interval { get { return this.timer.Interval; } set { this.timer.Interval = value; } }

        [Browsable(true), Category("Momo"), Description("图片动画展示方式")]
        public ImageAnimation ImageAnimation { get { return this.mPictureBox1.ImageAnimation; } set { this.mPictureBox1.ImageAnimation = value; } }

        [Category("幻灯片"), Description("图片列表")]
        public List<Bitmap> Images { get; set; }

        void timer_Tick(object sender, EventArgs e)
        {
            var count = this.imageList.Count;
            if (currentIndex == count - 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }

            this.mPictureBox1.Image = imageList[currentIndex];
        }

        public void AddImage(Image image)
        {
            this.imageList.Add(image);
        }

        public void AddImage(Bitmap bitmap)
        {
            this.imageList.Add(bitmap);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            rightRectangle = new Rectangle(this.Width - 80, (this.Height - 40) / 2, 40, 40);
            leftRectangle = new Rectangle(40, (this.Height - 40) / 2, 40, 40);
        }

        public void Start()
        {
            this.timer.Enabled = true;
        }

        public void Stop()
        {
            this.timer.Enabled = false;
        }

        private void InitializeComponent()
        {
            this.mPictureBox1 = new Momo.Forms.MPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.mPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // mPictureBox1
            // 
            this.mPictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mPictureBox1.Image = global::Momo.Forms.Properties.Resources.banner;
            this.mPictureBox1.ImageAnimation = Momo.Forms.ImageAnimation.None;
            this.mPictureBox1.Location = new System.Drawing.Point(0, 0);
            this.mPictureBox1.Name = "mPictureBox1";
            this.mPictureBox1.Size = new System.Drawing.Size(600, 300);
            this.mPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mPictureBox1.TabIndex = 0;
            this.mPictureBox1.TabStop = false;
            this.mPictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.mPictureBox1_Paint);
            this.mPictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mPictureBox1_MouseClick);
            // 
            // MBanner
            // 
            this.Controls.Add(this.mPictureBox1);
            this.Name = "MBanner";
            this.Size = new System.Drawing.Size(600, 300);
            ((System.ComponentModel.ISupportInitialize)(this.mPictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (rightRectangle.Contains(e.Location))
            {
                timer.Stop();
                if (currentIndex == this.imageList.Count - 1)
                {
                    currentIndex = 0;
                }
                else
                {
                    currentIndex++;
                }

                mPictureBox1.Image = this.imageList[currentIndex];
                timer.Start();
            }
            else if (leftRectangle.Contains(e.Location))
            {
                timer.Stop();
                if (currentIndex == 0)
                {
                    currentIndex = this.imageList.Count - 1;
                }
                else
                {
                    currentIndex--;
                }

                mPictureBox1.Image = this.imageList[currentIndex];
                timer.Start();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            PaintImage(e);
        }

        private void PaintImage(PaintEventArgs e)
        {
            if (leftRectangle.IsEmpty)
            {
                rightRectangle = new Rectangle(this.Width - 80, (this.Height - 40) / 2, 40, 40);
                leftRectangle = new Rectangle(40, (this.Height - 40) / 2, 40, 40);
            }

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using (var brush = new SolidBrush(Color.FromArgb(70, 136, 71)))
            {
                e.Graphics.FillEllipse(brush, rightRectangle);
                e.Graphics.FillEllipse(brush, leftRectangle);
            }

            var font = new Font("宋体", 14, FontStyle.Bold);
            GDIHelper.DrawString(e.Graphics, rightRectangle, font, ">", Color.White, TextAlignment.MiddleCenter);
            GDIHelper.DrawString(e.Graphics, leftRectangle, font, "<", Color.White, TextAlignment.MiddleCenter);
        }

        private void mPictureBox1_Paint(object sender, PaintEventArgs e)
        {
            PaintImage(e);
        }

        private void mPictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            this.OnMouseClick(e);
        }
    }
}
