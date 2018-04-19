using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums
{
    public enum EnumHookStatus
    {
        /// <summary>
        /// 异常
        /// </summary>
        Exception = -2,
        /// <summary>
        /// 玩家终止
        /// </summary>
        Stop = -1,
        /// <summary>
        /// 运行中
        /// </summary>
        Run = 0,
        /// <summary>
        /// 已完成
        /// </summary>
        Finish = 1,
        /// <summary>
        /// 没有体力结束
        /// </summary>
        NoStamina = 2,
    }
}
