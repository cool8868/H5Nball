
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_NameSuffix 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicNamesuffixEntity
	{
		
		public DicNamesuffixEntity()
		{
		}

		public DicNamesuffixEntity(
		System.Int32 idx
,				System.String name
		)
		{
			this.Idx = idx;
			this.Name = name;
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
		#endregion
        
        #region Clone
        public DicNamesuffixEntity Clone()
        {
            DicNamesuffixEntity entity = new DicNamesuffixEntity();
			entity.Idx = this.Idx;
			entity.Name = this.Name;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_NameSuffix 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicNamesuffixResponse : BaseResponse<DicNamesuffixEntity>
    {

    }
}

