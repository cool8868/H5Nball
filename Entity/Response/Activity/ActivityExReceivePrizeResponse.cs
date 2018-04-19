using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Activity
{
    [Serializable]
    [DataContract]
    public class ActivityExReceivePrizeResponse : BaseResponse<ActivityExReceivePrizeEntity>
    {
    }
    [Serializable]
    [DataContract]
    public class ActivityExReceivePrizeEntity
    {
        [DataMember]
        public List<ActivityPrizeEntity> Prizes { get; set; }
    }
}
