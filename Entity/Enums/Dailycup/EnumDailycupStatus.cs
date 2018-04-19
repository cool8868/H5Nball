using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums
{
    /// <summary>
    /// 杯赛状态枚举
    /// </summary>
    public enum EnumDailycupStatus
    {
        /// <summary>
        /// 报名开放中
        /// </summary>
        Opening=0,
        /// <summary>
        /// 报名截止
        /// </summary>
        Close=1,
        /// <summary>
        /// 杯赛结束
        /// </summary>
        End=2,
        /// <summary>
        /// 杯赛开始发奖
        /// </summary>
        StartSend=3,
        /// <summary>
        /// 杯赛发奖完成
        /// </summary>
        Finish=4,
    }

    /// <summary>
    /// 道具押注结果类型枚举
    /// </summary>
    public enum EnumItemGambleType
    {
        /// <summary>
        /// 初始化
        /// </summary>
        Init = 0,

        /// <summary>
        /// 强化
        /// </summary>
        Strengthen = 1,

        /// <summary>
        /// 退化
        /// </summary>
        Weaken = 2,

        /// <summary>
        /// 变白
        /// </summary>
        Whiten = 3,

        /// <summary>
        /// 消失
        /// </summary>
        Disappear = 4
    }
}
