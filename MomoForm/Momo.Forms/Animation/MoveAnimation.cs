using System;
using System.Drawing;

namespace Momo.Forms
{
    /// <summary>
    /// 位置移动动画
    /// </summary>
    public sealed class MoveAnimation : Animation
    {
        /// <summary>
        /// 移动步长
        /// </summary>
        private int yStepSize;
        private int xStepSize;
        private bool xGrow;
        private bool yGrow;

        private Point targetLoction;
        /// <summary>
        /// 目标位置
        /// </summary>
        public System.Drawing.Point TargetLocation
        {
            get { return this.targetLoction; }
            set
            {
                this.targetLoction = value;

                this.yStepSize = (int)Math.Ceiling((decimal)(this.TargetLocation.Y - this.Target.Top) / (Duration / Speed));
                this.xStepSize = (int)Math.Ceiling((decimal)(this.TargetLocation.X - this.Target.Left) / (Duration / Speed));
                xGrow = this.xStepSize > 0;
                yGrow = this.yStepSize > 0;
            }
        }

        public MoveAnimation(System.Windows.Forms.Control target, int speed, int duration) : base(target, speed, duration)
        {
        }

        protected override void DoAnimation()
        {
            var stop = true;
            var x = this.Target.Location.X;
            if ((this.xGrow && this.Target.Location.X < this.TargetLocation.X) || (!this.xGrow && this.Target.Location.X > this.TargetLocation.X))
            {
                x += xStepSize;
                stop = false;
            }

            var y = this.Target.Location.Y;
            if ((this.yGrow && this.Target.Location.Y < this.TargetLocation.Y) || (!this.yGrow && this.Target.Location.Y > this.TargetLocation.Y))
            {
                y += yStepSize;
                stop = false;
            }

            this.Target.Location = new Point(x, y);
            if (stop)
            {
                this.Target.Location = this.TargetLocation;
                this.Stop();
            }
        }
    }
}
