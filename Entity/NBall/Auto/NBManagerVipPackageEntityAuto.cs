
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.NB_ManagerVipPackage 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class NbManagervippackageEntity
	{
		
		public NbManagervippackageEntity()
		{
		}

		public NbManagervippackageEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 packagelevel
,				System.Int32 isget
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.PackageLevel = packagelevel;
			this.IsGet = isget;
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
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///PackageLevel
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 PackageLevel {get ; set ;}

		///<summary>
		///IsGet
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 IsGet {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public NbManagervippackageEntity Clone()
        {
            NbManagervippackageEntity entity = new NbManagervippackageEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.PackageLevel = this.PackageLevel;
			entity.IsGet = this.IsGet;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.NB_ManagerVipPackage 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class NbManagervippackageResponse : BaseResponse<NbManagervippackageEntity>
    {

    }
}
