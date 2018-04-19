
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_Grow 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicGrowEntity
	{
		
		public DicGrowEntity()
		{
		}

		public DicGrowEntity(
		System.Int32 idx
,				System.String name
,				System.Int32 stage
,				System.Int32 reiki
,				System.Int32 fastreiki
,				System.Int32 grownum
,				System.Int32 breakrate
,				System.Int32 pluspercent
,				System.Int32 propertymax
,				System.String growtip
		)
		{
			this.Idx = idx;
			this.Name = name;
			this.Stage = stage;
			this.Reiki = reiki;
			this.FastReiki = fastreiki;
			this.GrowNum = grownum;
			this.BreakRate = breakrate;
			this.PlusPercent = pluspercent;
			this.PropertyMax = propertymax;
			this.GrowTip = growtip;
		}
		
		#region Public Properties
		
		///<summary>
		///阶段等级
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///阶段名称
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Name {get ; set ;}

		///<summary>
		///Stage
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Stage {get ; set ;}

		///<summary>
		///普通成长一次需要的灵气值
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Reiki {get ; set ;}

		///<summary>
		///快速成长一次需要的灵气值
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 FastReiki {get ; set ;}

		///<summary>
		///成长值(突破到下一阶段的值)
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 GrowNum {get ; set ;}

		///<summary>
		///突破到下一阶段的概率(百分比)
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 BreakRate {get ; set ;}

		///<summary>
		///全属性加成(百分比)
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 PlusPercent {get ; set ;}

		///<summary>
		///球员属性点分配上限提升值
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 PropertyMax {get ; set ;}

		///<summary>
		///GrowTip
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String GrowTip {get ; set ;}
		#endregion
        
        #region Clone
        public DicGrowEntity Clone()
        {
            DicGrowEntity entity = new DicGrowEntity();
			entity.Idx = this.Idx;
			entity.Name = this.Name;
			entity.Stage = this.Stage;
			entity.Reiki = this.Reiki;
			entity.FastReiki = this.FastReiki;
			entity.GrowNum = this.GrowNum;
			entity.BreakRate = this.BreakRate;
			entity.PlusPercent = this.PlusPercent;
			entity.PropertyMax = this.PropertyMax;
			entity.GrowTip = this.GrowTip;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_Grow 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicGrowResponse : BaseResponse<DicGrowEntity>
    {

    }
}

