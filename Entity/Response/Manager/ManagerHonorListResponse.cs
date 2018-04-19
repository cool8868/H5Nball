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
    public class ManagerHonorListResponse : BaseResponse<ManagerHonorListEntity>
    {
    }

    [Serializable]
    [DataContract]
    public class ManagerHonorListEntity
    {
        /// <summary>
        /// 荣誉列表s
        /// </summary>
        [DataMember]
        public List<NbManagerhonorEntity> Honors { get; set; }
    }
}
