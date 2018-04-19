using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Games.NBall.Entity.Response
{
    [Serializable]
    [DataContract]
    public class UpdateIndulgeResponse : BaseResponse<UpdateIndulgeEntity>
    {
    }

    [Serializable]
    [DataContract]
    public class UpdateIndulgeEntity
    {
        /// <summary>
        /// 在线时间
        /// </summary>
        [DataMember]
        public int OnlineMinutes { get; set; }
    }
}
