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
    public class ActivityExListResponse : BaseResponse<ActivityExListEntity>
    {
    }

    [Serializable]
    [DataContract]
    public class ActivityExListEntity
    {
        [DataMember]
        public List<TemplateActivityexEntity> Activities { get; set; }
    }
}
