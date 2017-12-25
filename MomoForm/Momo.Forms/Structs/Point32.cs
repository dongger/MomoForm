using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Momo.Forms
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Point32
    {
        public Int32 x;
        public Int32 y;

        public Point32(Int32 x, Int32 y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
