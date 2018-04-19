
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_FormationDetail 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicFormationdetailEntity
	{
		
		public DicFormationdetailEntity()
		{
		}

		public DicFormationdetailEntity(
		System.Int32 idx
,				System.Int32 formationid
,				System.Int32 position
,				System.String coordinate
,				System.Int32 specificpoint
,				System.String specificpointdesc
		)
		{
			this.Idx = idx;
			this.FormationId = formationid;
			this.Position = position;
			this.Coordinate = coordinate;
			this.SpecificPoint = specificpoint;
			this.SpecificPointDesc = specificpointdesc;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///FormationId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 FormationId {get ; set ;}

		///<summary>
		///Position
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Position {get ; set ;}

		///<summary>
		///Coordinate
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String Coordinate {get ; set ;}

		///<summary>
		///SpecificPoint
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 SpecificPoint {get ; set ;}

		///<summary>
		///SpecificPointDesc
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String SpecificPointDesc {get ; set ;}
		#endregion
        
        #region Clone
        public DicFormationdetailEntity Clone()
        {
            DicFormationdetailEntity entity = new DicFormationdetailEntity();
			entity.Idx = this.Idx;
			entity.FormationId = this.FormationId;
			entity.Position = this.Position;
			entity.Coordinate = this.Coordinate;
			entity.SpecificPoint = this.SpecificPoint;
			entity.SpecificPointDesc = this.SpecificPointDesc;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_FormationDetail 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicFormationdetailResponse : BaseResponse<DicFormationdetailEntity>
    {

    }
}

