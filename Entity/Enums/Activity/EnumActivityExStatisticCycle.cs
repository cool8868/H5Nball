using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums.Activity
{
    public enum EnumActivityExStatisticCycle
    {
        /// <summary>
        /// 单次
        /// </summary>
        OnceTime=1,
        /// <summary>
        /// 单日
        /// </summary>
        Daily=2,
        /// <summary>
        /// 活动期间
        /// </summary>
        During=3,
        /// <summary>
        /// 账号累计
        /// </summary>
        ByAccount=4,
        /// <summary>
        /// 长期
        /// </summary>
        LongTime=5,
    }
}
