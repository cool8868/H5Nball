using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums
{
    public enum EnumLadderStatus
    {
        /// <summary>
        /// 进行中
        /// </summary>
        Running=0,
        /// <summary>
        /// 分组中...
        /// </summary>
        Grouping = 1,

        /// <summary>
        /// 结束
        /// </summary>
        End = 2,

        Finish=3,
    }

    public enum EnumCrossLadderPrizeType
    {
        Season=1,

        Daily=2,
    }
}
