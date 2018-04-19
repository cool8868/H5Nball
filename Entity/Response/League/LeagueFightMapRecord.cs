using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Games.NBall.Entity.Response
{
    /// <summary>
    /// 获取对阵记录
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract]
    public class LeagueFightMapsEntity
    {
        /// <summary>
        /// 对阵记录
        /// </summary>
        [ProtoMember(1)]
        [DataMember]
        public Dictionary<int, List<LeagueFight>> FM { get; set; }

        /// <summary>
        /// 排名列表
        /// </summary>
        [ProtoMember(2)]
        [DataMember]
        public Dictionary<int,LeagueRankRecord> RL { get; set; }
    }

    /// <summary>
    /// 对阵
    /// </summary>
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class LeagueFight
    {
        /// <summary>
        /// 回合数
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        public int R { get; set; }
        /// <summary>
        /// 主队id
        /// </summary>
        [DataMember]
        [ProtoMember(2)]
        public int H { get; set; }
        /// <summary>
        /// 客队ID
        /// </summary>
        [DataMember]
        [ProtoMember(3)]
        public int A { get; set; }
        /// <summary>
        /// 主队进球数
        /// </summary>
        [DataMember]
        [ProtoMember(4)]
        public int HG { get; set; }
        /// <summary>
        /// 客队进球数
        /// </summary>
        [DataMember]
        [ProtoMember(5)]
        public int AG { get; set; }

    }
    
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class LeagueRankRecord
    {
        /// <summary>
        /// 阵型ID
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        public int T{get;set;}
        
        /// <summary>
        /// 排名
        /// </summary>
        [DataMember]
        [ProtoMember(2)]
        public int R{get;set;}
        
        /// <summary>
        /// 积分
        /// </summary>
        [DataMember]
        [ProtoMember(3)]
        public int J{get;set;}

        /// <summary>
        /// 场次
        /// </summary>
        [DataMember]
        [ProtoMember(4)]
        public int C { get; set; }

        /// <summary>
        /// 胜利场次
        /// </summary>
        [DataMember]
        [ProtoMember(5)]
        public int W { get; set; }

        /// <summary>
        /// 失败场次
        /// </summary>
        [DataMember]
        [ProtoMember(6)]
        public int L { get; set; }

        /// <summary>
        /// 总进球数
        /// </summary>
        [DataMember]
        [ProtoMember(7)]
        public int G { get; set; }

        /// <summary>
        /// 总失球数
        /// </summary>
        [DataMember]
        [ProtoMember(8)]
        public int S { get; set; }

        /// <summary>
        /// 净胜球数
        /// </summary>
        [DataMember]
        [ProtoMember(9)]
        public int JS { get; set; }
    }
}
