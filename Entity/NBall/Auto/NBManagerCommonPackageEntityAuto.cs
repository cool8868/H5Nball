
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.NB_ManagerCommonPackage 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class NbManagercommonpackageEntity
	{
		
		public NbManagercommonpackageEntity()
		{
		}

		public NbManagercommonpackageEntity(
		System.Guid idx
,				System.String common1
,				System.String common2
,				System.String common3
,				System.String common4
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.Common1 = common1;
			this.Common2 = common2;
			this.Common3 = common3;
			this.Common4 = common4;
			this.Rowtime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid Idx {get ; set ;}

		///<summary>
		///Common1
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Common1 {get ; set ;}

		///<summary>
		///Common2
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String Common2 {get ; set ;}

		///<summary>
		///Common3
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String Common3 {get ; set ;}

		///<summary>
		///Common4
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String Common4 {get ; set ;}

		///<summary>
		///Rowtime
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime Rowtime {get ; set ;}
		#endregion
        
        #region Clone
        public NbManagercommonpackageEntity Clone()
        {
            NbManagercommonpackageEntity entity = new NbManagercommonpackageEntity();
			entity.Idx = this.Idx;
			entity.Common1 = this.Common1;
			entity.Common2 = this.Common2;
			entity.Common3 = this.Common3;
			entity.Common4 = this.Common4;
			entity.Rowtime = this.Rowtime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.NB_ManagerCommonPackage 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class NbManagercommonpackageResponse : BaseResponse<NbManagercommonpackageEntity>
    {

    }
}
