using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response
{
    /// <summary>
    /// 获取经理信息
    /// </summary>
    [DataContract]
    [Serializable]
    public class NBManagerInfoResponse : BaseResponse<NBManagerInfoData>
    {
    }

    /// <summary>
    /// 经理信息
    /// </summary>
    [DataContract]
    [Serializable]
    public class NBManagerInfoData
    {
        [DataMember]
        public NbManagerEntity Manager { get; set; }
        [DataMember]
        public NbManagerextraEntity ManagerExtra { get; set; }
        [DataMember]
        public long Point { get; set; }
        [DataMember]
        public int BindPoint { get; set; }
        [DataMember]
        public long ServerTime { get; set; }
        [DataMember]
        public bool HasMysteryShop { get; set; }

        /// <summary>
        /// 在线时间
        /// </summary>
        [DataMember]
        public int OnlineMinutes { get; set; }

        /// <summary>
        /// Cookie
        /// </summary>
        [DataMember]
        public string Cookie { get; set; }
    }

    /// <summary>
    /// 获取经理所有功能的次数输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class ManagerAllFunctionNumberResponse : BaseResponse<ManagerAllFunctionNumber>
    {
    }

    [Serializable]
    [DataContract]
    public class ManagerAllFunctionNumber
    {
       
        /// <summary>
        /// PK赛总共可获取多少点卷 
        /// </summary>
        [DataMember]
        public int PkMaxPoint { get; set; }


        /// <summary>
        /// PK赛总已经获取多少点卷 
        /// </summary>
        [DataMember]
        public int PkHavePoint { get; set; }

        /// <summary>
        /// 杯赛可竞猜次数
        /// </summary>
        [DataMember]
        public int DailycupMaxNumber { get; set; }

        /// <summary>
        /// 杯赛已经竞猜次数
        /// </summary>
        [DataMember]
        public int DailycupHaveNumber { get; set; }

        /// <summary>
        /// 是否有免费抽卡次数
        /// </summary>
        [DataMember]
        public bool IsFreeScouting { get; set; }
    }
}
