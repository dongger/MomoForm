using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Momo.Forms
{
    /// <summary>
    /// 基本动画效果
    /// </summary>
    public abstract class Animation : IDisposable
    {
        private Timer backgroundTimer;
        private Action invokeHandler;

        /// <summary>
        /// 开始执行动画之前
        /// </summary>
        public event EventHandler AnimationExecuting;

        /// <summary>
        /// 执行动画之后
        /// </summary>
        public event EventHandler AnimationExecuted;

        /// <summary>
        /// 附加目标，可以为control和palette
        /// </summary>
        public System.Windows.Forms.Control Target { get; private set; }

        /// <summary>
        /// 间隔/频率/速度
        /// </summary>
        public int Speed { get; private set; }

        /// <summary>
        /// 动画时长
        /// </summary>
        public int Duration { get; private set; }

        /// <summary>
        /// 是否正在运行中
        /// </summary>
        public bool Running { get; private set; }

        public Animation(System.Windows.Forms.Control target, int speed,int duration)
        {
            this.Target = target;
            this.Speed = speed;
            this.Duration = duration;
            invokeHandler = new Action(DoAnimation);
        }

        public void Start()
        {
            if (Running)
            {
                return;
            }

            Running = true;
            AnimationExecuting?.Invoke(this, EventArgs.Empty);
            backgroundTimer = new Timer(new TimerCallback(ExecuteInTimer), null, 0, this.Speed);
        }

        public void Stop()
        {
            if (this.backgroundTimer != null)
            {
                this.backgroundTimer.Dispose();
            }

            AnimationExecuted?.Invoke(this, EventArgs.Empty);
        }

        protected abstract void DoAnimation();

        private void ExecuteInTimer(object obj)
        {
            if (Running)
            {
                try
                {
                    this.Target.Invoke(invokeHandler);
                }
                catch { }
            }
        }

        public void Dispose()
        {
            if (this.backgroundTimer != null)
            {
                this.backgroundTimer.Dispose();
            }
        }
    }
}
