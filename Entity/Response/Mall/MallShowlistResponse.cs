using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.Config.Custom;

namespace Games.NBall.Entity.Response
{
    /// <summary>
    /// 商城列表显示
    /// </summary>
    [Serializable]
    [DataContract]
    public class MallShowlistResponse : BaseResponse<MallShowlistEntity>
    {
    }

    /// <summary>
    /// 商城列表显示
    /// </summary>
    [Serializable]
    [DataContract]
    public class MallShowlistEntity
    {
        /// <summary>
        /// 显示列表
        /// </summary>
        [DataMember]
        public List<DicMallItemDataEntity> ShowList { get; set; }

        /// <summary>
        /// 玩家点券数量
        /// </summary>
        [DataMember]
        public int Point { get; set; }
    }
}
