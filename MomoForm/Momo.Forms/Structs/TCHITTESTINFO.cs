using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Momo.Forms
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TCHITTESTINFO
    {

        public TCHITTESTINFO(Point location)
        {
            Point = location;
            Flags = TCHT.TCHT_ONITEM;
        }

        public Point Point;
        public int Flags;
    }
}
