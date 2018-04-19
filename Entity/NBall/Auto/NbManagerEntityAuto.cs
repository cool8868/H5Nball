
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.NB_Manager 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class NbManagerEntity
	{
		
		public NbManagerEntity()
		{
		}

		public NbManagerEntity(
		System.Guid idx
,				System.String account
,				System.String name
,				System.String logo
,				System.Int32 type
,				System.Int32 level
,				System.Int32 exp
,				System.Int32 sophisticate
,				System.Int32 score
,				System.Int32 coin
,				System.Int32 reiki
,				System.Int32 teammembermax
,				System.Int32 trainseatmax
,				System.Int32 viplevel
,				System.Int32 mod
,				System.Int32 status
,				System.DateTime rowtime
,				System.DateTime updatetime
,				System.Byte[] rowversion
,				System.Int32 friendshippoint
		)
		{
			this.Idx = idx;
			this.Account = account;
			this.Name = name;
			this.Logo = logo;
			this.Type = type;
			this.Level = level;
			this.EXP = exp;
			this.Sophisticate = sophisticate;
			this.Score = score;
			this.Coin = coin;
			this.Reiki = reiki;
			this.TeammemberMax = teammembermax;
			this.TrainSeatMax = trainseatmax;
			this.VipLevel = viplevel;
			this.Mod = mod;
			this.Status = status;
			this.RowTime = rowtime;
			this.UpdateTime = updatetime;
			this.RowVersion = rowversion;
			this.FriendShipPoint = friendshippoint;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid Idx {get ; set ;}

		///<summary>
		///Account
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Account {get ; set ;}

		///<summary>
		///Name
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String Name {get ; set ;}

		///<summary>
		///Logo
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String Logo {get ; set ;}

		///<summary>
		///经理类型
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Type {get ; set ;}

		///<summary>
		///等级
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Level {get ; set ;}

		///<summary>
		///经验
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 EXP {get ; set ;}

		///<summary>
		///灵气
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 Sophisticate {get ; set ;}

		///<summary>
		///世界杯积分
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 Score {get ; set ;}

		///<summary>
		///金币
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 Coin {get ; set ;}

		///<summary>
		///灵气
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 Reiki {get ; set ;}

		///<summary>
		///球员数上限
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 TeammemberMax {get ; set ;}

		///<summary>
		///训练位上限
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 TrainSeatMax {get ; set ;}

		///<summary>
		///Vip等级
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Int32 VipLevel {get ; set ;}

		///<summary>
		///Mod
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Int32 Mod {get ; set ;}

		///<summary>
		///经理状态：-1,未注册经理;0,正常经理;1,沙包
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(17)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(18)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowVersion
		///</summary>
        [DataMember]
        [ProtoMember(19)]
        [JsonIgnore]
		public System.Byte[] RowVersion {get ; set ;}

		///<summary>
		///友情点
		///</summary>
        [DataMember]
        [ProtoMember(20)]
		public System.Int32 FriendShipPoint {get ; set ;}
		#endregion
        
        #region Clone
        public NbManagerEntity Clone()
        {
            NbManagerEntity entity = new NbManagerEntity();
			entity.Idx = this.Idx;
			entity.Account = this.Account;
			entity.Name = this.Name;
			entity.Logo = this.Logo;
			entity.Type = this.Type;
			entity.Level = this.Level;
			entity.EXP = this.EXP;
			entity.Sophisticate = this.Sophisticate;
			entity.Score = this.Score;
			entity.Coin = this.Coin;
			entity.Reiki = this.Reiki;
			entity.TeammemberMax = this.TeammemberMax;
			entity.TrainSeatMax = this.TrainSeatMax;
			entity.VipLevel = this.VipLevel;
			entity.Mod = this.Mod;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
			entity.RowVersion = this.RowVersion;
			entity.FriendShipPoint = this.FriendShipPoint;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.NB_Manager 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class NbManagerResponse : BaseResponse<NbManagerEntity>
    {

    }
}
