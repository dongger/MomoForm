using System;
using System.Runtime.InteropServices;

namespace Momo.Forms
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Size32
    {
        public Int32 cx;
        public Int32 cy;

        public Size32(Int32 cx, Int32 cy)
        {
            this.cx = cx;
            this.cy = cy;
        }
    }
}
