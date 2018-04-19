using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response
{
    /// <summary>
    /// 创建经理响应,data=ManagerId
    /// </summary>
    [DataContract]
    [Serializable]
    public class NBManagerCreateResponse : BaseResponse<Guid>
    {
    }

    [DataContract]
    [Serializable]
    public class GetRegisterManagerResponse : BaseResponse<RegisterManagerInfo>
    {
    }

    [DataContract]
    [Serializable]
    public class RegisterManagerInfo
    {
        /// <summary>
        /// 平台用户ID
        /// </summary>
        [DataMember]
        public string UId { get; set; }
        /// <summary>
        /// 区ID
        /// </summary>
        [DataMember]
        public string ServerId { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        [DataMember]
        public string RoleId { get; set; }
        /// <summary>
        /// 角色名
        /// </summary>
        [DataMember]
        public string RoleName { get; set; }
        /// <summary>
        /// 未知
        /// </summary>
        [DataMember]
        public string Cookie { get; set; }
    }
}
