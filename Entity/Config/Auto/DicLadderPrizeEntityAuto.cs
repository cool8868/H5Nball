
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_LadderPrize 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicLadderprizeEntity
	{
		
		public DicLadderprizeEntity()
		{
		}

		public DicLadderprizeEntity(
		System.Int32 idx
,				System.Int32 minrank
,				System.Int32 maxrank
,				System.Int32 subtype
,				System.Int32 itemcode
,				System.String title
,				System.String description
		)
		{
			this.Idx = idx;
			this.MinRank = minrank;
			this.MaxRank = maxrank;
			this.SubType = subtype;
			this.ItemCode = itemcode;
			this.Title = title;
			this.description = description;
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
		///ItemCode
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 ItemCode {get ; set ;}

		///<summary>
		///Title
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String Title {get ; set ;}

		///<summary>
		///description
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String description {get ; set ;}
		#endregion
        
        #region Clone
        public DicLadderprizeEntity Clone()
        {
            DicLadderprizeEntity entity = new DicLadderprizeEntity();
			entity.Idx = this.Idx;
			entity.MinRank = this.MinRank;
			entity.MaxRank = this.MaxRank;
			entity.SubType = this.SubType;
			entity.ItemCode = this.ItemCode;
			entity.Title = this.Title;
			entity.description = this.description;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_LadderPrize 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicLadderprizeResponse : BaseResponse<DicLadderprizeEntity>
    {

    }
}

