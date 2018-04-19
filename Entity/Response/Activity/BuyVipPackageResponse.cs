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
    public class BuyVipPackageResponse : BaseResponse<BuyVipPackage>
    {
        //vip礼包返回值实体类


    }

    [Serializable]
    [DataContract]
    public class BuyVipPackage
    {
        /// <summary>
        /// 剩余点卷数
        /// </summary>
        [DataMember]
        public int Point { get; set; }
        
        /// <summary>
        /// 剩余金币
        /// </summary>
        [DataMember]
        public int Coin { get; set; }

        /// <summary>
        /// 背包数据
        /// </summary>
        [DataMember]
        public ItemPackageData Package { get; set; }

    }
}


