using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.NBall.Custom.Item
{
    /// <summary>
    /// 抽奖物品
    /// </summary>
    [Serializable]
    [DataContract]
    public class LotteryEntity
    {
        public LotteryEntity()
        {
            
        }

        public LotteryEntity(int prizeItemCode,string itemString, int strength, bool isBinding,int count=1)
        {
            this.PrizeItemCode = prizeItemCode;
            this.Strength = strength;
            this.IsBinding = isBinding;
            this.ItemString = itemString;
            this.Count = count;
        }

        /// <summary>
        /// 获得的物品编码
        /// </summary>
        [DataMember]
        public int PrizeItemCode { get; set; }
        /// <summary>
        /// 物品串
        /// </summary>
        [DataMember]
        public string ItemString { get; set; }
        /// <summary>
        /// 强化等级
        /// </summary>
        [DataMember]
        public int Strength { get; set; }
        /// <summary>
        /// 是否绑定
        /// </summary>
        [DataMember]
        public bool IsBinding { get; set; }

        public int Count { get; set; }
    }
}
