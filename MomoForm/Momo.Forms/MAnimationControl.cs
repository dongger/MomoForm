using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Momo.Forms
{
    public class MAnimationControl : Component
    {
        public MAnimationControl()
        {
            this.Speed = 10;
            this.Duration = 500;
        }


        [Browsable(true), Category("Momo"), Description("可见位置")]
        public Point VisibleLocation { get; set; }

        [Browsable(true), Category("Momo"), Description("停止位置，即隐藏位置")]
        public Point StopLocation { get; set; }

        [Browsable(true), Category("Momo"), Description("初始位置，即动画开始时处于位置")]
        public Point StartLocation { get; set; }

        /// <summary>
        /// 间隔/频率/速度
        /// </summary>
        [Browsable(true), Category("Momo"), Description("速度，毫秒"), DefaultValue(10)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Speed { get; set; }

        /// <summary>
        /// 动画时长
        /// </summary>
        [Browsable(true), Category("Momo"), Description("动画时长，毫秒"), DefaultValue(500)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Duration { get; set; }

        private MPictureBox hidePic;
        private MPictureBox visiblePic;

        public Control CurrentControl { get; set; }

        [Browsable(true), Category("Momo"), Description("动画执行完成事件"), DefaultValue(500)]
        public event EventHandler AnimationExecuted;

        private readonly List<Control> queue = new List<Control>();

        public void ClearQueue()
        {
            this.queue.Clear();
        }

        public void GoBack()
        {
            var c = queue[0];
            queue.RemoveAt(0);
            this.Switch(this.CurrentControl, c, true);
        }

        public bool IsBusy { get; private set; }

        public int Count { get { return queue.Count; } }

        private int anicount = 0;
        /// <summary>
        /// 将第一个控件隐藏，第二个控件显示
        /// </summary>
        /// <param name="toHide"></param>
        /// <param name="toVisible"></param>
        public void Switch(Control toVisible)
        {
            this.Switch(this.CurrentControl, toVisible);
        }

        /// <summary>
        /// 将第一个控件隐藏，第二个控件显示
        /// </summary>
        /// <param name="toHide"></param>
        /// <param name="toVisible"></param>
        public void Switch(Control toHide, Control toVisible, bool back = false)
        {
            if (toHide.Parent != toVisible.Parent)
            {
                throw new Exception("交换显示状态的控件，必须处于同一个容器中！");
            }

            var hide = new Bitmap(toHide.Width, toHide.Height);
            toHide.DrawToBitmap(hide, toHide.ClientRectangle);
            var visible = new Bitmap(toVisible.Width, toVisible.Height);
            toVisible.DrawToBitmap(visible, toVisible.ClientRectangle);

            hidePic = new MPictureBox();
            hidePic.Size = toHide.Size;
            hidePic.Location = toHide.Location;
            hidePic.ImageAnimation = ImageAnimation.None;
            hidePic.Image = hide;

            visiblePic = new MPictureBox();
            visiblePic.Size = toVisible.Size;
            visiblePic.Location = StartLocation;
            visiblePic.ImageAnimation = ImageAnimation.None;
            visiblePic.Image = visible;

            toHide.Visible = false;
            hidePic.Tag = toHide;
            toHide.Parent.Controls.Add(hidePic);

            toVisible.Visible = false;
            visiblePic.Tag = toVisible;
            toVisible.Parent.Controls.Add(visiblePic);

            CurrentControl = toVisible;

            if (!back)
            {
                this.queue.Insert(0, toHide);
            }

            this.MoveTo(hidePic, StopLocation, "hide");

            this.MoveTo(visiblePic, VisibleLocation, "visible");
        }

        private void MoveTo(Control target, Point targetLocation, string name)
        {
            var animation = new SwitchAnimation(target, Speed, Duration);
            anicount += 1;
            animation.TargetLocation = targetLocation;
            animation.Name = name;
            animation.AnimationExecuted += Ani_AnimationExecuted;
            animation.Start();
        }

        private void Ani_AnimationExecuted(object sender, EventArgs e)
        {
            try
            {
                var animation = sender as SwitchAnimation;
                animation.Target.Visible = false;
                var target = (animation.Target.Tag as Control);
                target.Location = animation.TargetLocation;
                target.Visible = animation.Name == "visible";
                animation.Target.Visible = false;

                animation.Target.Parent.Controls.Remove(animation.Target);
                anicount--;
                if (anicount == 0 && AnimationExecuted != null)
                {
                    AnimationExecuted(this, EventArgs.Empty);
                }
            }
            catch { }
        }
    }

    internal class SwitchAnimation : Animation
    {
        public string Name { get; set; }

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

        public SwitchAnimation(Control target, int speed, int duration) : base(target, speed, duration)
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
