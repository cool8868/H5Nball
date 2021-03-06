﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response
{
    /// <summary>
    /// 转盘获取经理信息输出类
    /// </summary>
    [DataContract]
    [Serializable]
    public class TurntableLuckDrawResponse : BaseResponse<TurntableLuckDraw>
    {
    }

    [DataContract]
    [Serializable]
    public class TurntableLuckDraw
    {
        /// <summary>
        /// 转盘详情
        /// </summary>
        [DataMember]
        public TurntableInfo TurntableInfo { get; set; }

        /// <summary>
        /// 抽中的ID
        /// </summary>
        [DataMember]
        public int WinAlotteryId { get; set; }

        /// <summary>
        /// 获得的奖励类型
        /// </summary>
        [DataMember]
        public int PrizeType { get; set; }

        /// <summary>
        /// 获得的奖励code
        /// </summary>
        [DataMember]
        public int PrizeCode { get; set; }

        /// <summary>
        /// 获得的奖励数量
        /// </summary>
        [DataMember]
        public int PrizeCount { get; set; }
    
    }
}
