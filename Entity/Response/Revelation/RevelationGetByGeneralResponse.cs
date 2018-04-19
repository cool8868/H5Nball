using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Revelation
{
    /// <summary>
    ///球星启示录查询通关的关卡信息输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class RevelationGetByGeneralResponse : BaseResponse<RevelationGetByGeneral>
    {
    }

    [Serializable]
    [DataContract]
    public class RevelationGetByGeneral
    {
        /// <summary>
        /// 是否通关关卡字典
        /// </summary>
        [DataMember]
        public Dictionary<int, int> Dic { get; set; }
    }
}
