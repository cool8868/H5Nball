
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Arena_ManagerInfo 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ArenaManagerinfoEntity
	{
		
		public ArenaManagerinfoEntity()
		{
		}

		public ArenaManagerinfoEntity(
		System.Guid managerid
,				System.String managername
,				System.String siteid
,				System.String zonename
,				System.String logo
,				System.Int32 championnumber
,				System.Int32 integral
,				System.Int32 dangrading
,				System.Int32 arenacoin
,				System.Int32 arenatype
,				System.Int32 stamina
,				System.Int32 maxstamina
,				System.Int32 buystaminanumber
,				System.DateTime staminarestoretime
,				System.Int32 rank
,				System.Int32 status
,				System.Boolean teammember1status
,				System.Boolean teammember2status
,				System.Boolean teammember3status
,				System.Boolean teammember4status
,				System.Boolean teammember5status
,				System.DateTime updatetime
,				System.DateTime rowtime
,				System.Byte[] opponent
,				System.Int32 domainid
		)
		{
			this.ManagerId = managerid;
			this.ManagerName = managername;
			this.SiteId = siteid;
			this.ZoneName = zonename;
			this.Logo = logo;
			this.ChampionNumber = championnumber;
			this.Integral = integral;
			this.DanGrading = dangrading;
			this.ArenaCoin = arenacoin;
			this.ArenaType = arenatype;
			this.Stamina = stamina;
			this.MaxStamina = maxstamina;
			this.BuyStaminaNumber = buystaminanumber;
			this.StaminaRestoreTime = staminarestoretime;
			this.Rank = rank;
			this.Status = status;
			this.Teammember1Status = teammember1status;
			this.Teammember2Status = teammember2status;
			this.Teammember3Status = teammember3status;
			this.Teammember4Status = teammember4status;
			this.Teammember5Status = teammember5status;
			this.UpdateTime = updatetime;
			this.RowTime = rowtime;
			this.Opponent = opponent;
			this.DomainId = domainid;
		}
		
		#region Public Properties
		
		///<summary>
		///经理ID
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///经理名
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String ManagerName {get ; set ;}

		///<summary>
		///SiteId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String SiteId {get ; set ;}

		///<summary>
		///区id
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String ZoneName {get ; set ;}

		///<summary>
		///Logo
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String Logo {get ; set ;}

		///<summary>
		///获得冠军次数
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 ChampionNumber {get ; set ;}

		///<summary>
		///积分
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Integral {get ; set ;}

		///<summary>
		///段位
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 DanGrading {get ; set ;}

		///<summary>
		///竞技币数量
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 ArenaCoin {get ; set ;}

		///<summary>
		///竞技场类型
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 ArenaType {get ; set ;}

		///<summary>
		///体力
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 Stamina {get ; set ;}

		///<summary>
		///最大体力
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 MaxStamina {get ; set ;}

		///<summary>
		///购买体力次数
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 BuyStaminaNumber {get ; set ;}

		///<summary>
		///体力恢复记录时间
		///</summary>
        [DataMember]
        [ProtoMember(14)]
        [JsonIgnore]
		public System.DateTime StaminaRestoreTime {get ; set ;}

		///<summary>
		///排名
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Int32 Rank {get ; set ;}

		///<summary>
		///状态
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///球队是否组建完成
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.Boolean Teammember1Status {get ; set ;}

		///<summary>
		///Teammember2Status
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.Boolean Teammember2Status {get ; set ;}

		///<summary>
		///Teammember3Status
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.Boolean Teammember3Status {get ; set ;}

		///<summary>
		///Teammember4Status
		///</summary>
        [DataMember]
        [ProtoMember(20)]
		public System.Boolean Teammember4Status {get ; set ;}

		///<summary>
		///Teammember5Status
		///</summary>
        [DataMember]
        [ProtoMember(21)]
		public System.Boolean Teammember5Status {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(22)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(23)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///Opponent
		///</summary>
        [DataMember]
        [ProtoMember(24)]
        [JsonIgnore]
		public System.Byte[] Opponent {get ; set ;}

		///<summary>
		///DomainId
		///</summary>
        [DataMember]
        [ProtoMember(25)]
		public System.Int32 DomainId {get ; set ;}
		#endregion
        
        #region Clone
        public ArenaManagerinfoEntity Clone()
        {
            ArenaManagerinfoEntity entity = new ArenaManagerinfoEntity();
			entity.ManagerId = this.ManagerId;
			entity.ManagerName = this.ManagerName;
			entity.SiteId = this.SiteId;
			entity.ZoneName = this.ZoneName;
			entity.Logo = this.Logo;
			entity.ChampionNumber = this.ChampionNumber;
			entity.Integral = this.Integral;
			entity.DanGrading = this.DanGrading;
			entity.ArenaCoin = this.ArenaCoin;
			entity.ArenaType = this.ArenaType;
			entity.Stamina = this.Stamina;
			entity.MaxStamina = this.MaxStamina;
			entity.BuyStaminaNumber = this.BuyStaminaNumber;
			entity.StaminaRestoreTime = this.StaminaRestoreTime;
			entity.Rank = this.Rank;
			entity.Status = this.Status;
			entity.Teammember1Status = this.Teammember1Status;
			entity.Teammember2Status = this.Teammember2Status;
			entity.Teammember3Status = this.Teammember3Status;
			entity.Teammember4Status = this.Teammember4Status;
			entity.Teammember5Status = this.Teammember5Status;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
			entity.Opponent = this.Opponent;
			entity.DomainId = this.DomainId;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Arena_ManagerInfo 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ArenaManagerinfoResponse : BaseResponse<ArenaManagerinfoEntity>
    {

    }
}
