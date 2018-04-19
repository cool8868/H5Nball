using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums
{
    public enum EnumDbType
    {
        /// <summary>
        /// 配置库
        /// </summary>
        Config = 0,

        /// <summary>
        /// 游戏主库
        /// </summary>
        Main = 1,

        /// <summary>
        /// 比赛过程库
        /// </summary>
        Process = 2,

        /// <summary>
        /// 系统日志库
        /// </summary>
        SystemLog = 3,

        /// <summary>
        /// 物品日志库
        /// </summary>
        Shadow = 4,
        /// <summary>
        /// Support库
        /// </summary>
        Support = 5,
    }
}
