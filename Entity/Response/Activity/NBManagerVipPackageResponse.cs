using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Activity
{
    [DataContract]
    [Serializable]
    public class NBManagerVipPackageResponse : BaseResponse<ManagerVippackageEntity>
    {
        //vip礼包返回值实体类


    }

    [Serializable]
    [DataContract]

    public class ManagerVippackageEntity
    {
        /// <summary>
        /// 经理是否可以能买vip礼包 0不能购买，1可购买 2 已经购买
        /// </summary>
        [DataMember]
        public List<int> IsHave { get; set; }

        /// <summary>
        /// 下次刷新时间
        /// </summary>
        [DataMember]
        public long NextRefreshTick { get; set; }
    }
}


