using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums
{
    /// <summary>
    /// 任务状态枚举
    /// </summary>
    public enum EnumTaskStatus
    {
        /// <summary>
        /// 挂起
        /// </summary>
        Pending=-1,
        /// <summary>
        /// 初始
        /// </summary>
        Init=0,
        /// <summary>
        /// 完成
        /// </summary>
        Done=1,
        /// <summary>
        /// 领取奖励后关闭
        /// </summary>
        Close=2,
    }
}
