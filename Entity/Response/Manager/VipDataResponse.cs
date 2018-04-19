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
    public class VipDataResponse : BaseResponse<VipDataEntity>
    {
    }

    [Serializable]
    [DataContract]
    public class VipDataEntity
    {
        [DataMember]
        public int VipLevel { get; set; }
        [DataMember]
        public int Point { get; set; }
        [DataMember]
        public int LevelUpPoint { get; set; }
    }
}
