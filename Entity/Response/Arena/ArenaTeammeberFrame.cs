using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response
{
    /// <summary>
    /// 获取对阵记录
    /// </summary>
    [DataContract] 
    [Serializable]
    [ProtoContract]
    public class ArenaTeammeberFrame
    {
        /// <summary>
        /// 阵型ID
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        public int SolutionId { get; set; }

        /// <summary>
        /// 球员串
        /// </summary>
        [DataMember]
        [ProtoMember(2)]
        public string PlayerString { get; set; }

        /// <summary>
        /// 球员列表
        /// </summary>
        [DataMember]
        [ProtoMember(3)]
        public Dictionary<Guid, ArenaTeammember> TeammemberList { get; set; }

        /// <summary>
        /// 综合实力
        /// </summary>
        [DataMember]
        [ProtoMember(4)]
        public int Kpi { get; set; }
    }

    /// <summary>
    /// 球员列表
    /// </summary>
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class ArenaTeammember
    {
        /// <summary>
        /// 球员ID
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        public int PlayerId { get; set; }

        /// <summary>
        /// 使用的球员卡
        /// </summary>
        [DataMember]
        [ProtoMember(2)]
        public PlayerCardProperty UsePlayer { get; set; }

        /// <summary>
        /// 背包ID
        /// </summary>
        [DataMember]
        [ProtoMember(3)]
        public Guid ItemId { get; set; }
    }
}
