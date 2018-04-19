using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Manager
{
    //vip礼包返回值实体类
    [DataContract]
    [Serializable]
    public class NBManagervippackageResponse : BaseResponse<ManagervippackageEntity>
    {

    }
    [Serializable]
    [DataContract]

    public class ManagervippackageEntity
    {
        /// <summary>
        /// 经理是否可以能买vip礼包
        /// </summary>
        [DataMember]
        public List<NbManagervippackageEntity> Entities { get; set; }

        //礼包物品列表
        [DataMember]
        public List<String> List { get; set; }
    }
}
