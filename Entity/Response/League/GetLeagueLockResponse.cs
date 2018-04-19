using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response
{
    /// <summary>
    /// 获取联赛关卡锁信息输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class GetLeagueLockResponse : BaseResponse<LeagueLockEntity>
    {
    }

    [Serializable]
    [DataContract]
    public class LeagueLockEntity
    {
        /// <summary>
        /// 锁住情况 true=锁住了
        /// </summary>
        [DataMember]
        public List<bool> List { get; set; }
    }
}
