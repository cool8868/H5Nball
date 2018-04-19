
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_VipPackage 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigVippackageEntity
	{
		
		public ConfigVippackageEntity()
		{
		}

		public ConfigVippackageEntity(
		System.Int32 idx
,				System.Int32 viplevel
,				System.Int32 price
,				System.Int32 prizetype
,				System.Int32 prizeitemcode
,				System.Int32 counts
,				System.Int32 isbindlny
,				System.Int32 packageid
		)
		{
			this.Idx = idx;
			this.VipLevel = viplevel;
			this.Price = price;
			this.PrizeType = prizetype;
			this.PrizeItemCode = prizeitemcode;
			this.Counts = counts;
			this.IsBindlny = isbindlny;
			this.PackageId = packageid;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///VipLevel
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 VipLevel {get ; set ;}

		///<summary>
		///Price
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Price {get ; set ;}

		///<summary>
		///PrizeType
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 PrizeType {get ; set ;}

		///<summary>
		///PrizeItemCode
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 PrizeItemCode {get ; set ;}

		///<summary>
		///Counts
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Counts {get ; set ;}

		///<summary>
		///IsBindlny
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 IsBindlny {get ; set ;}

		///<summary>
		///PackageId
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 PackageId {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigVippackageEntity Clone()
        {
            ConfigVippackageEntity entity = new ConfigVippackageEntity();
			entity.Idx = this.Idx;
			entity.VipLevel = this.VipLevel;
			entity.Price = this.Price;
			entity.PrizeType = this.PrizeType;
			entity.PrizeItemCode = this.PrizeItemCode;
			entity.Counts = this.Counts;
			entity.IsBindlny = this.IsBindlny;
			entity.PackageId = this.PackageId;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_VipPackage 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigVippackageResponse : BaseResponse<ConfigVippackageEntity>
    {

    }
}

