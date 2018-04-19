
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.All_App 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class AllAppEntity
	{
		
		public AllAppEntity()
		{
		}

		public AllAppEntity(
		System.Int32 idx
,				System.String name
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.Name = name;
			this.RowTime = rowtime;
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
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(3)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public AllAppEntity Clone()
        {
            AllAppEntity entity = new AllAppEntity();
			entity.Idx = this.Idx;
			entity.Name = this.Name;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.All_App 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class AllAppResponse : BaseResponse<AllAppEntity>
    {

    }
}

