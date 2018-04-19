
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_CrossLadderExchange 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicCrossladderexchangeEntity
	{
		
		public DicCrossladderexchangeEntity()
		{
		}

		public DicCrossladderexchangeEntity(
		System.Int32 idx
,				System.Int32 itemcode
,				System.Int32 costhonor
		)
		{
			this.Idx = idx;
			this.ItemCode = itemcode;
			this.CostHonor = costhonor;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///ItemCode
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 ItemCode {get ; set ;}

		///<summary>
		///CostHonor
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 CostHonor {get ; set ;}
		#endregion
        
        #region Clone
        public DicCrossladderexchangeEntity Clone()
        {
            DicCrossladderexchangeEntity entity = new DicCrossladderexchangeEntity();
			entity.Idx = this.Idx;
			entity.ItemCode = this.ItemCode;
			entity.CostHonor = this.CostHonor;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_CrossLadderExchange 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicCrossladderexchangeResponse : BaseResponse<DicCrossladderexchangeEntity>
    {

    }
}
