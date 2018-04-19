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
    public class GetPlatformAnnouncementResponse : BaseResponse<GetPlatformAnnouncement>
    {
    }

    [Serializable]
    [DataContract]
    public class GetPlatformAnnouncement
    {
        /// <summary>
        /// 公告列表
        /// </summary>
        [DataMember]
        public List<AnnouncementEntity> AnnouncementList { get; set; }
    }

    
}
