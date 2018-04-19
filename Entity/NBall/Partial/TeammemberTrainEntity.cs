using System;
using System.Runtime.Serialization;
using Games.NBall.Entity.NBall.Custom;

namespace Games.NBall.Entity
{    

	public partial class TeammemberTrainEntity
	{
        ///<summary>
        ///已经训练的时间
        /// 这个是绝对时间，不是相对于基准时间的刻度
        ///</summary>
        [DataMember]
        public long TrainTick { get; set; }
        /// <summary>
        /// 能力值
        /// </summary>
        [DataMember]
        public double Kpi { get; set; }

        /// <summary>
        /// 升级所需经验
        /// </summary>
        [DataMember]
        public int LevelupExp { get; set; }

        /// <summary>
        /// 未分配的属性点
        /// </summary>
        [DataMember]
        public int PropertyPoint { get; set; }

        /// <summary>
        /// 是否达到最高经验
        /// </summary>
        [DataMember]
        public bool IsMaxExp { get; set; }

        /// <summary>
        /// 装备
        /// </summary>
        [DataMember]
        public EquipmentProperty Equipment { get; set; }

        /// <summary>
        /// 强化等级
        /// </summary>
        [DataMember]
        public int StrengthenLevel { get; set; }
	}
	
	
    public partial class TeammemberTrainResponse
    {

    }
}

