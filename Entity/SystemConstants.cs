using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity
{
    /// <summary>
    /// 系统常量
    /// </summary>
    public struct SystemConstants
    {
        /// <summary>
        /// 当前物品序列化版本
        /// </summary>
        public const int CurItemVersion = 1;

        /// <summary>
        /// 阵型中球员的数量
        /// </summary>
        public const int TeammemberCount = 11;
        /// <summary>
        /// 挂机偏移时间
        /// </summary>
        public const int HookOffsetTime = 6;
    }
}
