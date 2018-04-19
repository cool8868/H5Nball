
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_NewPlayerPack 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicNewplayerpackEntity
	{
		
		public DicNewplayerpackEntity()
		{
		}

		public DicNewplayerpackEntity(
		System.Int32 idx
,				System.Int32 packid
,				System.Int32 type
,				System.Int32 subtype
,				System.Int32 strength
,				System.Int32 count
,				System.Boolean isbinding
,				System.String description
		)
		{
			this.Idx = idx;
			this.PackId = packid;
			this.Type = type;
			this.SubType = subtype;
			this.Strength = strength;
			this.Count = count;
			this.IsBinding = isbinding;
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
		///PackId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 PackId {get ; set ;}

		///<summary>
		///Type
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Type {get ; set ;}

		///<summary>
		///SubType
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 SubType {get ; set ;}

		///<summary>
		///Strength
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Strength {get ; set ;}

		///<summary>
		///Count
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Count {get ; set ;}

		///<summary>
		///IsBinding
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Boolean IsBinding {get ; set ;}

		///<summary>
		///Description
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String Description {get ; set ;}
		#endregion
        
        #region Clone
        public DicNewplayerpackEntity Clone()
        {
            DicNewplayerpackEntity entity = new DicNewplayerpackEntity();
			entity.Idx = this.Idx;
			entity.PackId = this.PackId;
			entity.Type = this.Type;
			entity.SubType = this.SubType;
			entity.Strength = this.Strength;
			entity.Count = this.Count;
			entity.IsBinding = this.IsBinding;
			entity.Description = this.Description;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_NewPlayerPack 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicNewplayerpackResponse : BaseResponse<DicNewplayerpackEntity>
    {

    }
}

