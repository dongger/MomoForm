using System;

namespace Momo.Forms
{
    public static class TCHT
    {
        public const int TCHT_NOWHERE = 1;
        public const int TCHT_ONITEMICON = 2;
        public const int TCHT_ONITEMLABEL = 4;
        public const int TCHT_ONITEM = TCHT_ONITEMICON | TCHT_ONITEMLABEL;
    }
}
