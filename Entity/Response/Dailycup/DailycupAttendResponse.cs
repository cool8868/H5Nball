using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom;

namespace Games.NBall.Entity.Response.Dailycup
{
    [Serializable]
    [DataContract]
    public class DailycupAttendResponse : BaseResponse<DailycupAttendEntity>
    {
    }

    [Serializable]
    [DataContract]
    public class DailycupAttendEntity
    {
        /// <summary>
        /// PopMsg
        /// </summary>
        [DataMember]
        public List<PopMessageEntity> PopMsg { get; set; }
    }
}
