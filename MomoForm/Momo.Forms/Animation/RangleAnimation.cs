using System;
using System.Drawing;

namespace Momo.Forms
{
    /// <summary>
    /// 矩形大小变换动画
    /// </summary>
    public sealed class RangleAnimation : Animation
    {
        public RangleAnimation(System.Windows.Forms.Control target, int speed, int duration) : base(target, speed, duration)
        {
            oldLocation = this.Target.Location;
            oldSize = this.Target.Size;
        }

        /// <summary>
        /// 矩形变换锚定
        /// </summary>
        public RangeAnimationAnchor Anchor { get; set; }

        private System.Drawing.Size targetSize { get; set; }

        private int heightStepSize = 0;
        private int widthStepSize = 0;
        private bool wGrow = false;
        private bool hGrow = false;
        private Point oldLocation;
        private Size oldSize;

        /// <summary>
        /// 目标大小
        /// </summary>
        public System.Drawing.Size TargetSize
        {
            get { return this.targetSize; }
            set
            {
                this.targetSize = value;

                this.heightStepSize = (int)Math.Ceiling((decimal)(this.TargetSize.Height - this.Target.Height) / (Duration / Speed));
                this.widthStepSize = (int)Math.Ceiling((decimal)(this.TargetSize.Width - this.Target.Width) / (Duration / Speed));
                this.wGrow = this.widthStepSize > 0;
                this.hGrow = this.heightStepSize > 0;
            }
        }

        protected override void DoAnimation()
        {
            var newSize = Size.Empty;
            var w = 0;
            var h = 0;
            if (this.Target.Width < this.TargetSize.Width)
            {
                w = this.Target.Width + widthStepSize;
            }
            else
            {
                w = this.Target.Width;
            }

            if (this.Target.Height < this.TargetSize.Height)
            {
                h = this.Target.Height + widthStepSize;
            }
            else
            {
                h = this.Target.Height;
            }

            newSize = new Size(w, h);

            this.Target.Size = newSize;
            var newLocation = Point.Empty;
            switch (this.Anchor)
            {
                case RangeAnimationAnchor.BottomLeft:
                    newLocation = new Point(oldLocation.X, oldLocation.Y + (oldSize.Height - newSize.Height));
                    break;
                case RangeAnimationAnchor.BottomRight:
                    newLocation = new Point(oldLocation.X + (oldSize.Width - newSize.Width), oldLocation.Y + (oldSize.Height - newSize.Height));
                    break;
                case RangeAnimationAnchor.TopLeft:
                    newLocation = new Point(oldLocation.X, oldLocation.Y);
                    break;
                case RangeAnimationAnchor.TopRight:
                    newLocation = new Point(oldLocation.X + (oldSize.Width - newSize.Width), oldLocation.Y);
                    break;
                case RangeAnimationAnchor.Center:
                    var c = oldSize - newSize;
                    

                    var cx = oldSize.Width / 2 + oldLocation.X;
                    var cy = oldSize.Width / 2 + oldLocation.Y;
                    newLocation = new Point(cx + c.Width / 2, cy + c.Height / 2);
                    break;
            }

            this.Target.Location = newLocation;
            this.Target.Size = newSize;

            if (((heightStepSize > 0 && newSize.Height >= TargetSize.Height) ||
                (heightStepSize < 0 && newSize.Height <= TargetSize.Height)) &&
                ((widthStepSize > 0 && newSize.Width >= TargetSize.Width) ||
                (widthStepSize < 0 && newSize.Width <= TargetSize.Width))
                )
            {
                this.Stop();
            }

            //if (this.Target.Size.Width >= TargetSize.Width || this.TargetSize.Height > TargetSize.Height)
            //{
            //    this.Stop();
            //}
        }
    }
}
