
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_Share 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigShareEntity
	{
		
		public ConfigShareEntity()
		{
		}

		public ConfigShareEntity(
		System.Int32 idx
,				System.Int32 sharetype
,				System.Boolean isrepetition
,				System.Int32 prizetype
,				System.Int32 prizeitemtype
,				System.Int32 subtype
,				System.Int32 number
,				System.Int32 maxsharenumber
		)
		{
			this.Idx = idx;
			this.ShareType = sharetype;
			this.IsRepetition = isrepetition;
			this.PrizeType = prizetype;
			this.PrizeItemType = prizeitemtype;
			this.SubType = subtype;
			this.Number = number;
			this.MaxShareNumber = maxsharenumber;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///分享类型
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 ShareType {get ; set ;}

		///<summary>
		///是否可重复分享
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Boolean IsRepetition {get ; set ;}

		///<summary>
		///奖励类型 0 = 首次分享 1=非首次分享
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 PrizeType {get ; set ;}

		///<summary>
		///奖励物品类型 1=钻石 2=金币 3=指定物品
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 PrizeItemType {get ; set ;}

		///<summary>
		///奖励物品code
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 SubType {get ; set ;}

		///<summary>
		///奖励物品数量
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Number {get ; set ;}

		///<summary>
		///可重复分享，最多每天能分享多少次
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 MaxShareNumber {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigShareEntity Clone()
        {
            ConfigShareEntity entity = new ConfigShareEntity();
			entity.Idx = this.Idx;
			entity.ShareType = this.ShareType;
			entity.IsRepetition = this.IsRepetition;
			entity.PrizeType = this.PrizeType;
			entity.PrizeItemType = this.PrizeItemType;
			entity.SubType = this.SubType;
			entity.Number = this.Number;
			entity.MaxShareNumber = this.MaxShareNumber;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_Share 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigShareResponse : BaseResponse<ConfigShareEntity>
    {

    }
}

