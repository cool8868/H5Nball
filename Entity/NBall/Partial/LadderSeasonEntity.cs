using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{
    public partial class LadderSeasonEntity
    {
        /// <summary>
        /// 开始时间刻度
        /// </summary>
        [DataMember]
        public long StartTick { get; set; }

        /// <summary>
        /// 结束时间刻度
        /// </summary>
        [DataMember]
        public long EndTick { get; set; }

        /// <summary>
        /// 当前时间刻度
        /// </summary>
        [DataMember]
        public long NowTick { get; set; }

    }


    /// <summary>
    /// 对Table dbo.Season 的输出映射.
    /// </summary>
    public partial class SeasonResponse
    {

    }
}

