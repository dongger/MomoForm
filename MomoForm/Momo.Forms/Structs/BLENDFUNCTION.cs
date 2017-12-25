using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Momo.Forms
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BLENDFUNCTION
    {
        public byte BlendOp;
        public byte BlendFlags;
        public byte SourceConstantAlpha;
        public byte AlphaFormat;
    }
}
