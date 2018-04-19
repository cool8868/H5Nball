
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_CrossLadderPrize 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicCrossladderprizeEntity
	{
		
		public DicCrossladderprizeEntity()
		{
		}

		public DicCrossladderprizeEntity(
		System.Int32 idx
,				System.Int32 minrank
,				System.Int32 maxrank
,				System.Int32 subtype
,				System.Int32 prizetype
,				System.String title
		)
		{
			this.Idx = idx;
			this.MinRank = minrank;
			this.MaxRank = maxrank;
			this.SubType = subtype;
			this.PrizeType = prizetype;
			this.Title = title;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///MinRank
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 MinRank {get ; set ;}

		///<summary>
		///MaxRank
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 MaxRank {get ; set ;}

		///<summary>
		///SubType
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 SubType {get ; set ;}

		///<summary>
		///PrizeType
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 PrizeType {get ; set ;}

		///<summary>
		///Title
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String Title {get ; set ;}
		#endregion
        
        #region Clone
        public DicCrossladderprizeEntity Clone()
        {
            DicCrossladderprizeEntity entity = new DicCrossladderprizeEntity();
			entity.Idx = this.Idx;
			entity.MinRank = this.MinRank;
			entity.MaxRank = this.MaxRank;
			entity.SubType = this.SubType;
			entity.PrizeType = this.PrizeType;
			entity.Title = this.Title;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_CrossLadderPrize 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicCrossladderprizeResponse : BaseResponse<DicCrossladderprizeEntity>
    {

    }
}
