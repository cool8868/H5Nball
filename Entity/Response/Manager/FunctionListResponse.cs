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
    public class FunctionListResponse : BaseResponse<FunctionListEntity>
    {
    }

    [Serializable]
    [DataContract]
    public class FunctionListEntity
    {
        /// <summary>
        /// 功能列表
        /// </summary>
        [DataMember]
        public string FunctionList { get; set; }
    }
}
