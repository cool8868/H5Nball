using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response
{
    /// <summary>
    /// 转盘获取经理信息输出类
    /// </summary>
    [DataContract]
    [Serializable]
    public class TurntableResetResponse : BaseResponse<TurntableReset>
    {
    }

    [DataContract]
    [Serializable]
    public class TurntableReset
    {
        /// <summary>
        /// 转盘详情
        /// </summary>
        [DataMember]
        public TurntableInfo TurntableInfo { get; set; }

        /// <summary>
        /// 剩余点卷
        /// </summary>
        [DataMember]
        public int Point { get; set; }
    
    }
}
