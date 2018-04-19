
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_LadderDayPrize 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigLadderdayprizeEntity
	{
		
		public ConfigLadderdayprizeEntity()
		{
		}

		public ConfigLadderdayprizeEntity(
		System.Int32 idx
,				System.Int32 winnumber
,				System.Int32 prizetype
,				System.Int32 itemcode
,				System.Int32 number
		)
		{
			this.Idx = idx;
			this.WinNumber = winnumber;
			this.PrizeType = prizetype;
			this.ItemCode = itemcode;
			this.Number = number;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///获胜场次
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 WinNumber {get ; set ;}

		///<summary>
		///奖励类型  1钻石  2金币 3 ItemCode 4荣誉
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 PrizeType {get ; set ;}

		///<summary>
		///奖励具体CODE PrizeType=3时有效
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ItemCode {get ; set ;}

		///<summary>
		///数量
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Number {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigLadderdayprizeEntity Clone()
        {
            ConfigLadderdayprizeEntity entity = new ConfigLadderdayprizeEntity();
			entity.Idx = this.Idx;
			entity.WinNumber = this.WinNumber;
			entity.PrizeType = this.PrizeType;
			entity.ItemCode = this.ItemCode;
			entity.Number = this.Number;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_LadderDayPrize 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigLadderdayprizeResponse : BaseResponse<ConfigLadderdayprizeEntity>
    {

    }
}

