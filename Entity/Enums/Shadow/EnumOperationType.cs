using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums.Shadow
{
    /// <summary>
    /// 操作类型
    /// </summary>
    public enum EnumOperationType
    {
        /// <summary>
        /// 新增
        /// </summary>
        New = 1,

        /// <summary>
        /// 更新
        /// </summary>
        Update = 2,

        /// <summary>
        /// 删除
        /// </summary>
        Delete = 3,
        /// <summary>
        /// 使用后返回背包
        /// </summary>
        UsedReturn=4,
    }
}
