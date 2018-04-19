
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_LadderExchange 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicLadderexchangeEntity
	{
		
		public DicLadderexchangeEntity()
		{
		}

		public DicLadderexchangeEntity(
		System.Int32 idx
,				System.Int32 itemtype
,				System.Int32 itemcode
,				System.Int32 costhonor
,				System.Int32 type
,				System.Int32 count
,				System.Int32 laddercoin
		)
		{
			this.Idx = idx;
			this.ItemType = itemtype;
			this.ItemCode = itemcode;
			this.CostHonor = costhonor;
			this.Type = type;
			this.Count = count;
			this.LadderCoin = laddercoin;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///物品类型 0指定物品 1 随机物品
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 ItemType {get ; set ;}

		///<summary>
		///ItemCode
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ItemCode {get ; set ;}

		///<summary>
		///CostHonor
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 CostHonor {get ; set ;}

		///<summary>
		///Type
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Type {get ; set ;}

		///<summary>
		///Count
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Count {get ; set ;}

		///<summary>
		///LadderCoin
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 LadderCoin {get ; set ;}
		#endregion
        
        #region Clone
        public DicLadderexchangeEntity Clone()
        {
            DicLadderexchangeEntity entity = new DicLadderexchangeEntity();
			entity.Idx = this.Idx;
			entity.ItemType = this.ItemType;
			entity.ItemCode = this.ItemCode;
			entity.CostHonor = this.CostHonor;
			entity.Type = this.Type;
			entity.Count = this.Count;
			entity.LadderCoin = this.LadderCoin;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_LadderExchange 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicLadderexchangeResponse : BaseResponse<DicLadderexchangeEntity>
    {

    }
}
