using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NBall.Common;

namespace Games.NBall.Entity.Enums
{

    public struct EnumPlatCode
    {
        public const string Gov = "gov";
        public const string U17 = "u17";
        public const string HUPU = "hupu";
        public const string K7 = "7k";
        public const string WYX = "wyx";
    }

    public enum EnumSiteState
    {
        /// <summary>
        /// 维护
        /// </summary>
        pend,
        /// <summary>
        /// 正常
        /// </summary>
        use,
        /// <summary>
        /// 火爆
        /// </summary>
        hot,
        /// <summary>
        /// 推荐
        /// </summary>
        top,
    }
}
