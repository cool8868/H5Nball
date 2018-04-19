using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom;

namespace Games.NBall.Entity
{
    [DataContract]
    [Serializable]
    public class MessageCodeResponse : BaseResponse<MessageDataEntity>
    {
    }

    [DataContract]
    [Serializable]
    public class MessageDataEntity
    {
        /// <summary>
        /// PopMsg
        /// </summary>
        [DataMember]
        public List<PopMessageEntity> PopMsg { get; set; }
    }
}
