﻿
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Item_Package 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ItemPackageEntity
	{
		
		public ItemPackageEntity()
		{
		}

		public ItemPackageEntity(
		System.Guid managerid
,				System.Int32 packagesize
,				System.Byte itemversion
,				System.Byte[] itemstring
,				System.Int32 status
,				System.DateTime rowtime
,				System.Byte[] rowversion
		)
		{
			this.ManagerId = managerid;
			this.PackageSize = packagesize;
			this.ItemVersion = itemversion;
			this.ItemString = itemstring;
			this.Status = status;
			this.RowTime = rowtime;
			this.RowVersion = rowversion;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///背包大小
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 PackageSize {get ; set ;}

		///<summary>
		///物品序列化版本
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Byte ItemVersion {get ; set ;}

		///<summary>
		///物品串
		///</summary>
        [DataMember]
        [ProtoMember(4)]
        [JsonIgnore]
		public System.Byte[] ItemString {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///RowVersion
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.Byte[] RowVersion {get ; set ;}
		#endregion
        
        #region Clone
        public ItemPackageEntity Clone()
        {
            ItemPackageEntity entity = new ItemPackageEntity();
			entity.ManagerId = this.ManagerId;
			entity.PackageSize = this.PackageSize;
			entity.ItemVersion = this.ItemVersion;
			entity.ItemString = this.ItemString;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.RowVersion = this.RowVersion;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Item_Package 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ItemPackageResponse : BaseResponse<ItemPackageEntity>
    {

    }
}

