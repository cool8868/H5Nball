
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_PlayerLink 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicPlayerlinkEntity
	{
		
		public DicPlayerlinkEntity()
		{
		}

		public DicPlayerlinkEntity(
		System.Int32 idx
,				System.Int32 linkid
		)
		{
			this.Idx = idx;
			this.LinkId = linkid;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///LinkId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 LinkId {get ; set ;}
		#endregion
        
        #region Clone
        public DicPlayerlinkEntity Clone()
        {
            DicPlayerlinkEntity entity = new DicPlayerlinkEntity();
			entity.Idx = this.Idx;
			entity.LinkId = this.LinkId;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_PlayerLink 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicPlayerlinkResponse : BaseResponse<DicPlayerlinkEntity>
    {

    }
}
