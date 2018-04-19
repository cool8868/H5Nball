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
    public class GetUserLoginRecordResponse : BaseResponse<GetUserLoginRecord>
    {
    }

    [Serializable]
    [DataContract]
    public class GetUserLoginRecord
    {
        /// <summary>
        /// 登录记录
        /// </summary>
        [DataMember]
        public string LoginRecord { get; set; }

        /// <summary>
        /// 区列表
        /// </summary>
        [DataMember]
        public List<GetAllZone> ZoneList { get; set; }
    }

    [Serializable]
    [DataContract]
    public class GetAllZone
    {
        /// <summary>
        /// 平台名
        /// </summary>
        [DataMember]
        public string PlatForm { get; set; }
        /// <summary>
        /// 区名
        /// </summary>
        [DataMember]
        public string ZoneName { get; set; }

        /// <summary>
        /// 区ID
        /// </summary>
        [DataMember]
        public string ZoneId { get; set; }

        /// <summary>
        /// 区状态
        /// </summary>
        [DataMember]
        public int ZoneStates { get; set; }

        /// <summary>
        /// 维护内容
        /// </summary>
        [DataMember]
        public string Maintain { get; set; }

        /// <summary>
        /// ApiUrl
        /// </summary>
        [DataMember]
        public string Host { get; set; }
    }
}
