using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums
{
    /// <summary>
    /// 0,显示比分;1,可竞猜;2,已竞猜;3,关闭竞猜
    /// </summary>
    public enum EnumDailycupMatchStatus
    {
        /// <summary>
        /// 正常,显示比分
        /// </summary>
        ShowScore = 0,

        /// <summary>
        /// 可投注
        /// </summary>
        Gamble = 1,

        /// <summary>
        /// 已投注
        /// </summary>
        HasGamble = 2,

        /// <summary>
        /// 不可投注
        /// </summary>
        NoGambel = 3
    }
}
