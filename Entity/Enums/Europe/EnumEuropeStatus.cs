using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums
{
    public enum EnumEuropeStatus
    {
        /// <summary>
        /// 初始
        /// </summary>
        Default=0,
        /// <summary>
        /// 可竞猜
        /// </summary>
        Gamble = 1,

        /// <summary>
        /// 比赛中
        /// </summary>
        MatchIng = 2,
        /// <summary>
        /// 比赛完成
        /// </summary>
        MatchEnd =3,
        /// <summary>
        /// 发奖完成
        /// </summary>
        PrizeEnd=4,
    }

}
