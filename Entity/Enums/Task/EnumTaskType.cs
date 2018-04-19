using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums
{
    /// <summary>
    /// 任务类型
    /// </summary>
    public enum EnumTaskType
    {
        /// <summary>
        /// 主线任务
        /// </summary>
        Main=1,
        /// <summary>
        /// 新手引导任务
        /// </summary>
        Branch=2,
        /// <summary>
        /// 每日固定任务
        /// </summary>
        Daily=3,
        /// <summary>
        /// 每日随机任务 需随机抽取5个
        /// </summary>
        DailyRandom=4,
        /// <summary>
        /// 成就任务
        /// </summary>
        Achievement=5,

    }
}
