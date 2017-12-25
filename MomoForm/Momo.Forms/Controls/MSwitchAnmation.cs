using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Momo.Forms.Controls
{
    /// <summary>
    /// 控件切换动画组件
    /// </summary>
    [ToolboxItem(true)]
    public sealed class MSwitchAnmation : Component
    {
        public MSwitchAnmation()
        {
            this.Speed = 10;
            this.Duration = 500;
        }

        private bool busy = false;
        private int count = 0;
        public int Current { get; private set; }

        public Control CurrentControl { get { return this.AttatchControls[Current]; } }

        public int Count { get { return this.AttatchControls.Count; } }

        public Control FirstControl { get { return this.AttatchControls[0]; } }
        public Control LastControl { get { return this.AttatchControls[this.Count - 1]; } }

        private readonly List<Control> AttatchControls = new List<Control>();

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

        public Control Remove(int index)
        {
            var ctrl = this.AttatchControls[index];
            this.AttatchControls.RemoveAt(index);
            return ctrl;
        }

        public Control RemoveLast()
        {
            return this.Remove(this.Count - 1);
        }

        public void Clear()
        {
            this.AttatchControls.Clear();
            this.Current = 0;
            this.busy = false;
        }

        public void AddControls(params Control[] controls)
        {
            if (controls == null || controls.Length == 0)
            {
                return;
            }

            this.AttatchControls.AddRange(controls);
        }

        /// <summary>
        /// 切换到指定的控件进行显示
        /// </summary>
        /// <param name="control"></param>
        public bool Switch(Control control)
        {
            var i = 0;
            foreach (var item in this.AttatchControls)
            {
                if (item == control)
                {
                    return this.Switch(i);
                }

                i++;
            }

            return false;
        }

        /// <summary>
        /// 切换到指定控件名称的控件进行显示
        /// </summary>
        /// <param name="name"></param>
        public bool Switch(string name)
        {
            var i = 0;
            foreach (var item in this.AttatchControls)
            {
                if (item != null && item.Name == name)
                {
                    return this.Switch(i);
                }

                i++;
            }

            return false;
        }

        /// <summary>
        /// 切换到指定控件索引的控件进行显示
        /// </summary>
        /// <param name="index">控件索引</param>
        public bool Switch(int index)
        {
            if (busy) { return false; }
            if (index == Current) { return false; }
            busy = true;
            count = 2;

            //var c = this.AttatchControls[Current];
            //var img = new Bitmap(c.Width, c.Height);
            //c.DrawToBitmap(img, c.ClientRectangle);

            this.MoveTo(this.AttatchControls[Current], StopLocation);

            this.AttatchControls[index].Location = StartLocation;
            this.MoveTo(this.AttatchControls[index], VisibleLocation);
            Current = index;
            return true;
        }

        private void MoveTo(Control target, Point targetLocation)
        {
            if (target == null) { count -= 1; return; }

            target.Visible = true;
            var animation = new MoveAnimation(target, Speed, Duration);
            animation.TargetLocation = targetLocation;
            animation.Start();
            animation.AnimationExecuted += Animation_AnimationExecuted;
        }

        private void Animation_AnimationExecuted(object sender, EventArgs e)
        {
            count -= 1;
            busy = count > 0;
            if (this.StopLocation == (sender as Animation).Target.Location)
            {
                (sender as Animation).Target.Visible = false;
            }

            this.AnimationExecuted?.Invoke(sender, e);
        }

        public event EventHandler AnimationExecuted;
    }
}
