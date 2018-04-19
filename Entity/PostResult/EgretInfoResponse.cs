using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.PostResult
{
    [DataContract]
    [Serializable]
    public class EgretInfoResponse : BaseResponse<EgretInfo>
    {
    }
    [DataContract]
    [Serializable]
    public class EgretData
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Pic { get; set; }
    }

    [DataContract]
    [Serializable]
    public class EgretInfo
    {
        [DataMember]
        public int Code { get; set; }
        [DataMember]

        public string Msg { get; set; }
        [DataMember]

        public EgretData Data { get; set; }
    }
}
