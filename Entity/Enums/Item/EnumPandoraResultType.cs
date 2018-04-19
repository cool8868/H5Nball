using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums.Item
{
    /// <summary>
    /// 潘多拉操作失败枚举
    /// </summary>
    public enum EnumPandoraResultType
    {
        /// <summary>
        /// 潘多拉，处理成功
        /// </summary>
        Success = 0,
        /// <summary>
        /// 失败-卡牌不消失
        /// </summary>
        None = 1,
        /// <summary>
        /// 失败-卡牌消失
        /// </summary>
        Disappear = 2,
        /// <summary>
        /// 失败-卡牌降1级
        /// </summary>
        Downgrade = 3,
        /// <summary>
        /// 失败-卡牌不降级
        /// </summary>
        NoDowngrade = 4,
    }
}
