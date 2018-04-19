using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Bll.Frame
{
    public class FrameConfig
    {
        public static readonly DateTime BaseTime = new DateTime(2012, 1, 1);
        public static readonly int MAXWillNumber = 999;//可启用主动意志上限
        public static readonly bool SWAPBuffDataReadDb = false;
        public static readonly bool SWAPBuffDataWirteDb = true;
        public static readonly bool SWAPBuffDisableCrossCache = false;
    }
}
