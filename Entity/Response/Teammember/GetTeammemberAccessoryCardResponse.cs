using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;

namespace Games.NBall.Entity.Response
{
    /// <summary>
    /// 获取阵容中附卡
    /// </summary>
    [DataContract]
    [Serializable]
    public class GetTeammemberAccessoryCardResponse : BaseResponse<GetTeammemberAccessoryCard>
    {
    }

    [DataContract]
    [Serializable]
    public class GetTeammemberAccessoryCard
    {
        /// <summary>
        /// 附卡列表
        /// </summary>
        [DataMember]
        public List<TeammemberAccessoryCard> CardList { get; set; }
    }


    [DataContract]
    [Serializable]
    public class TeammemberAccessoryCard
    {
        /// <summary>
        /// 阵容ID
        /// </summary>
        [DataMember]
        public Guid TeammemberId { get; set; }

        /// <summary>
        /// 球员卡物品Code
        /// </summary>
        [DataMember]
        public int ItemCode { get; set; }

        /// <summary>
        /// 物品ID
        /// </summary>
        [DataMember]
        public Guid ItemId { get; set; }

        /// <summary>
        /// 球员卡等级
        /// </summary>
        [DataMember]
        public int CardLevel { get; set; }
    }
}
