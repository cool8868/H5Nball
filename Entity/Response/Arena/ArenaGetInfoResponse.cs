using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom;
using ProtoBuf;

namespace Games.NBall.Entity.Response
{
    [Serializable]
    [DataContract]
    public class ArenaGetInfoResponse : BaseResponse<ArenaGetInfo>
    {
    }

    [Serializable]
    [DataContract]
    public class ArenaGetInfo
    {
        /// <summary>
        /// 体力对象
        /// </summary>
        [DataMember]
        public ArenaStamina StaminaEntity { get; set; }

        /// <summary>
        /// 竞技场类型
        /// </summary>
        [DataMember]
        public int ArenaType { get; set; }

        /// <summary>
        /// 竞技场状态  0=准备  1=比赛
        /// </summary>
        [DataMember]
        public int Status { get; set; }

        /// <summary>
        /// 上届冠军名字
        /// </summary>
        [DataMember]
        public string OnChampionName { get; set; }

        /// <summary>
        /// 上届冠军所在区
        /// </summary>
        [DataMember]
        public string OnChampionZoneName { get; set; }

        /// <summary>
        /// 我的冠军次数
        /// </summary>
        [DataMember]
        public int MyChampionNumber { get; set; }

        /// <summary>
        /// 网站之师名字
        /// </summary>
        [DataMember]
        public string TheKingName { get; set; }

        /// <summary>
        /// 王者之师所在区
        /// </summary>
        [DataMember]
        public string TheKingZoneName { get; set; }

        /// <summary>
        /// 王者之师冠军次数
        /// </summary>
        [DataMember]
        public int TheKingChampionNumber { get; set; }

        /// <summary>
        /// 结算时间
        /// </summary>
        [DataMember]
        public long EndTimeTick { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [DataMember]
        public long StartTimeTick { get; set; }

        /// <summary>
        /// 我的积分
        /// </summary>
        [DataMember]
        public int Integral { get; set; }

        /// <summary>
        /// 我的段位
        /// </summary>
        [DataMember]
        public int DanGrading { get; set; }

        /// <summary>
        /// 升级积分
        /// </summary>
        [DataMember]
        public int UpIntegral { get; set; }
        /// <summary>
        /// 是否达到最高段位
        /// </summary>
        [DataMember]
        public bool IsMaxDanGrading { get; set; }

        /// <summary>
        /// 我的排名
        /// </summary>
        [DataMember]
        public int MyRank { get; set; }

        /// <summary>
        /// 是否可进入比赛 
        /// </summary>
        [DataMember]
        public bool IsIntoMatch { get; set; }
    }


}
