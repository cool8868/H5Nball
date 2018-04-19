
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_Ballsoul 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicBallsoulEntity
	{
		
		public DicBallsoulEntity()
		{
		}

		public DicBallsoulEntity(
		System.Int32 idx
,				System.String name
,				System.Int32 color
,				System.Int32 level
,				System.Int32 type
,				System.String description
		)
		{
			this.Idx = idx;
			this.Name = name;
			this.Color = color;
			this.Level = level;
			this.Type = type;
			this.Description = description;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///Name
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Name {get ; set ;}

		///<summary>
		///球魂颜色
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Color {get ; set ;}

		///<summary>
		///Level
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Level {get ; set ;}

		///<summary>
		///Type
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Type {get ; set ;}

		///<summary>
		///Description
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String Description {get ; set ;}
		#endregion
        
        #region Clone
        public DicBallsoulEntity Clone()
        {
            DicBallsoulEntity entity = new DicBallsoulEntity();
			entity.Idx = this.Idx;
			entity.Name = this.Name;
			entity.Color = this.Color;
			entity.Level = this.Level;
			entity.Type = this.Type;
			entity.Description = this.Description;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_Ballsoul 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicBallsoulResponse : BaseResponse<DicBallsoulEntity>
    {

    }
}

