using System;
using System.Drawing;
using System.Windows.Forms;

namespace Momo.Forms
{
    public sealed class ColorAnimation : Animation
    {
        public ColorAnimation(Control target, int speed, int duration) : base(target, speed, duration)
        {
        }

        private int aStepSize = 0;
        private int rStepSize = 0;
        private int gStepSize = 0;
        private int bStepSize = 0;
        private Color targetColor;
        public Color TargetColor
        {
            get { return targetColor; }
            set
            {
                targetColor = value;
                this.aStepSize = (int)Math.Ceiling((decimal)(this.Target.BackColor.A - this.TargetColor.A) / (Duration / Speed));
                this.rStepSize = (int)Math.Ceiling((decimal)(this.Target.BackColor.R - this.TargetColor.R) / (Duration / Speed));
                this.gStepSize = (int)Math.Ceiling((decimal)(this.Target.BackColor.G - this.TargetColor.G) / (Duration / Speed));
                this.bStepSize = (int)Math.Ceiling((decimal)(this.Target.BackColor.B - this.TargetColor.B) / (Duration / Speed));
            }
        }

        protected override void DoAnimation()
        {
            int a, r, g, b = 0;
            a = this.Target.BackColor.A + aStepSize;
            r = this.Target.BackColor.R + rStepSize;
            g = this.Target.BackColor.G + gStepSize;
            b = this.Target.BackColor.B + bStepSize;

            a = a > 255 ? 255 : a;
            r = r > 255 ? 255 : r;
            g = g > 255 ? 255 : g;
            b = b > 255 ? 255 : b;

            this.Target.BackColor = Color.FromArgb(a, r, g, b);
            if (this.Target.BackColor.A >= this.TargetColor.A
                && this.Target.BackColor.R >= this.TargetColor.R
                && this.Target.BackColor.G >= this.TargetColor.G
                && this.Target.BackColor.B >= this.TargetColor.B
                )
            {
                this.Stop();
            }
        }
    }
}
