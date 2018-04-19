
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_FormationPoint 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicFormationpointEntity
	{
		
		public DicFormationpointEntity()
		{
		}

		public DicFormationpointEntity(
		System.Int32 idx
,				System.String playerpoint
,				System.String ballparkpoint
,				System.Int32 buff
		)
		{
			this.Idx = idx;
			this.PlayerPoint = playerpoint;
			this.BallParkPoint = ballparkpoint;
			this.Buff = buff;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///球员位置
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String PlayerPoint {get ; set ;}

		///<summary>
		///球场位置
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String BallParkPoint {get ; set ;}

		///<summary>
		///buff百分比
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Buff {get ; set ;}
		#endregion
        
        #region Clone
        public DicFormationpointEntity Clone()
        {
            DicFormationpointEntity entity = new DicFormationpointEntity();
			entity.Idx = this.Idx;
			entity.PlayerPoint = this.PlayerPoint;
			entity.BallParkPoint = this.BallParkPoint;
			entity.Buff = this.Buff;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_FormationPoint 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicFormationpointResponse : BaseResponse<DicFormationpointEntity>
    {

    }
}

