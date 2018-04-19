using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Bll.Schedule
{
    /// <summary>
    /// 计划任务状态枚举
    /// </summary>
    public enum EnumScheduleStatus
    {
        /// <summary>
        /// 开始
        /// </summary>
        Start = 1,
        /// <summary>
        /// 手工退出
        /// </summary>
        Abort = 2,
        /// <summary>
        /// 成功
        /// </summary>
        Succeed = 3,
        /// <summary>
        /// 重试
        /// </summary>
        Retry=4,
        /// <summary>
        /// 失败
        /// </summary>
        Fail = 5
    }
}
