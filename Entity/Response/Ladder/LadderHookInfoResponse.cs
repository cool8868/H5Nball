using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProtoBuf;

namespace Games.NBall.Entity.Response.Ladder
{
    public class LadderHookInfoResponse : BaseResponse<LadderHookInfoEntity>
    {
    }

    [Serializable]
    [DataContract]
    public class LadderHookInfoEntity
    {
        /// <summary>
        /// 是否正在挂机
        /// </summary>
        [ProtoMember(1)]
        [DataMember]
        public bool IsHook { get; set; }
        ///<summary>
        ///CurTimes
        ///</summary>
        [DataMember]
        [ProtoMember(2)]
        public System.Int32 CurTimes { get; set; }

        ///<summary>
        ///CurWiningTimes
        ///</summary>
        [DataMember]
        [ProtoMember(3)]
        public System.Int32 CurWiningTimes { get; set; }

        ///<summary>
        ///MaxTimes
        ///</summary>
        [DataMember]
        [ProtoMember(4)]
        public System.Int32 MaxTimes { get; set; }

        ///<summary>
        ///MinScore
        ///</summary>
        [DataMember]
        [ProtoMember(5)]
        public System.Int32 MinScore { get; set; }

        ///<summary>
        ///MaxScore
        ///</summary>
        [DataMember]
        [ProtoMember(6)]
        public System.Int32 MaxScore { get; set; }

        ///<summary>
        ///MaxWiningTimes
        ///</summary>
        [DataMember]
        [ProtoMember(7)]
        public System.Int32 MaxWiningTimes { get; set; }

        ///<summary>
        ///NextMatchTime
        ///</summary>
        [DataMember]
        [ProtoMember(8)]
        public long NextMatchWaitSeconds { get; set; }

        /// <summary>
        /// 天梯挂机列表
        /// </summary>
        [DataMember]
        [ProtoMember(9)]
        public List<LadderHook> LadderHookList { get; set; }
        [DataMember]
        [ProtoMember(10)]
        public long ExpiredTick { get; set; }
    }

    [Serializable]
    [DataContract]
    public class LadderHook
    {
        /// <summary>
        /// 主队名
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        public string HomeName { get; set; }

        /// <summary>
        /// 客队名
        /// </summary>
        [DataMember]
        [ProtoMember(2)]
        public string AwayName { get; set; }

        /// <summary>
        /// 主队比分
        /// </summary>
        [DataMember]
        [ProtoMember(3)]
        public int HomeScore { get; set; }

        /// <summary>
        /// 客队比分
        /// </summary>
        [DataMember]
        [ProtoMember(4)]
        public int AwayScore { get; set; }

        /// <summary>
        /// 我获得的金币
        /// </summary>
        [DataMember]
        [ProtoMember(5)]
        public int MyCoin { get; set; }

        /// <summary>
        /// 我获得的积分
        /// </summary>
        [DataMember]
        [ProtoMember(6)]
        public int MyIntegral { get; set; }
    }

}
