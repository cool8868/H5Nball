
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_LeagueExchange 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicLeagueexchangeEntity
	{
		
		public DicLeagueexchangeEntity()
		{
		}

		public DicLeagueexchangeEntity(
		System.Int32 idx
,				System.Int32 itemtype
,				System.Int32 itemcode
,				System.Int32 costscore
,				System.Int32 type
,				System.Int32 count
		)
		{
			this.Idx = idx;
			this.ItemType = itemtype;
			this.ItemCode = itemcode;
			this.CostScore = costscore;
			this.Type = type;
			this.Count = count;
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
		///CostScore
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 CostScore {get ; set ;}

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
		#endregion
        
        #region Clone
        public DicLeagueexchangeEntity Clone()
        {
            DicLeagueexchangeEntity entity = new DicLeagueexchangeEntity();
			entity.Idx = this.Idx;
			entity.ItemType = this.ItemType;
			entity.ItemCode = this.ItemCode;
			entity.CostScore = this.CostScore;
			entity.Type = this.Type;
			entity.Count = this.Count;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_LeagueExchange 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicLeagueexchangeResponse : BaseResponse<DicLeagueexchangeEntity>
    {

    }
}

