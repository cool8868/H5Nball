
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Teammember 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class TeammemberEntity
	{
		
		public TeammemberEntity()
		{
		}

		public TeammemberEntity(
		System.Guid idx
,				System.Guid managerid
,				System.Int32 playerid
,				System.Int32 level
,				System.Int32 usedproperty
,				System.Double speed
,				System.Double shoot
,				System.Double freekick
,				System.Double balance
,				System.Double physique
,				System.Double bounce
,				System.Double aggression
,				System.Double disturb
,				System.Double interception
,				System.Double dribble
,				System.Double pass
,				System.Double mentality
,				System.Double response
,				System.Double positioning
,				System.Double handcontrol
,				System.Double acceleration
,				System.Byte[] usedplayercard
,				System.Byte[] usedequipment
,				System.Int32 status
,				System.DateTime rowtime
,				System.Boolean iscopyed
,				System.Boolean isinherited
,				System.Byte[] usedbadge
,				System.Int32 arousallv
,				System.Byte[] usedclubclothes
,				System.Int32 strengthenlevel
,				System.Double power
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.PlayerId = playerid;
			this.Level = level;
			this.UsedProperty = usedproperty;
			this.Speed = speed;
			this.Shoot = shoot;
			this.FreeKick = freekick;
			this.Balance = balance;
			this.Physique = physique;
			this.Bounce = bounce;
			this.Aggression = aggression;
			this.Disturb = disturb;
			this.Interception = interception;
			this.Dribble = dribble;
			this.Pass = pass;
			this.Mentality = mentality;
			this.Response = response;
			this.Positioning = positioning;
			this.HandControl = handcontrol;
			this.Acceleration = acceleration;
			this.UsedPlayerCard = usedplayercard;
			this.UsedEquipment = usedequipment;
			this.Status = status;
			this.RowTime = rowtime;
			this.IsCopyed = iscopyed;
			this.IsInherited = isinherited;
			this.UsedBadge = usedbadge;
			this.ArousalLv = arousallv;
			this.UsedClubClothes = usedclubclothes;
			this.StrengthenLevel = strengthenlevel;
			this.Power = power;
		}
		
		#region Public Properties
		
		///<summary>
		///球员唯一id
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid Idx {get ; set ;}

		///<summary>
		///经理id
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///pid
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 PlayerId {get ; set ;}

		///<summary>
		///球员训练等级
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Level {get ; set ;}

		///<summary>
		///球员已分配属性点
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 UsedProperty {get ; set ;}

		///<summary>
		///速度
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Double Speed {get ; set ;}

		///<summary>
		///射门
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Double Shoot {get ; set ;}

		///<summary>
		///任意球
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Double FreeKick {get ; set ;}

		///<summary>
		///控制
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Double Balance {get ; set ;}

		///<summary>
		///体质
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Double Physique {get ; set ;}

		///<summary>
		///弹跳
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Double Bounce {get ; set ;}

		///<summary>
		///侵略性
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Double Aggression {get ; set ;}

		///<summary>
		///干扰
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Double Disturb {get ; set ;}

		///<summary>
		///抢断
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Double Interception {get ; set ;}

		///<summary>
		///控球
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Double Dribble {get ; set ;}

		///<summary>
		///传球
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Double Pass {get ; set ;}

		///<summary>
		///意识
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.Double Mentality {get ; set ;}

		///<summary>
		///反应
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.Double Response {get ; set ;}

		///<summary>
		///位置感
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.Double Positioning {get ; set ;}

		///<summary>
		///手控球
		///</summary>
        [DataMember]
        [ProtoMember(20)]
		public System.Double HandControl {get ; set ;}

		///<summary>
		///加速度
		///</summary>
        [DataMember]
        [ProtoMember(21)]
		public System.Double Acceleration {get ; set ;}

		///<summary>
		///使用的球员卡
		///</summary>
        [DataMember]
        [ProtoMember(22)]
        [JsonIgnore]
		public System.Byte[] UsedPlayerCard {get ; set ;}

		///<summary>
		///使用的装备
		///</summary>
        [DataMember]
        [ProtoMember(23)]
        [JsonIgnore]
		public System.Byte[] UsedEquipment {get ; set ;}

		///<summary>
		///状态：0,初始;1,主力;2,替补
		///</summary>
        [DataMember]
        [ProtoMember(24)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(25)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///IsCopyed
		///</summary>
        [DataMember]
        [ProtoMember(26)]
		public System.Boolean IsCopyed {get ; set ;}

		///<summary>
		///IsInherited
		///</summary>
        [DataMember]
        [ProtoMember(27)]
		public System.Boolean IsInherited {get ; set ;}

		///<summary>
		///UsedBadge
		///</summary>
        [DataMember]
        [ProtoMember(28)]
        [JsonIgnore]
		public System.Byte[] UsedBadge {get ; set ;}

		///<summary>
		///ArousalLv
		///</summary>
        [DataMember]
        [ProtoMember(29)]
		public System.Int32 ArousalLv {get ; set ;}

		///<summary>
		///UsedClubClothes
		///</summary>
        [DataMember]
        [ProtoMember(30)]
        [JsonIgnore]
		public System.Byte[] UsedClubClothes {get ; set ;}

		///<summary>
		///强化等级
		///</summary>
        [DataMember]
        [ProtoMember(31)]
		public System.Int32 StrengthenLevel {get ; set ;}

		///<summary>
		///Power
		///</summary>
        [DataMember]
        [ProtoMember(32)]
		public System.Double Power {get ; set ;}
		#endregion
        
        #region Clone
        public TeammemberEntity Clone()
        {
            TeammemberEntity entity = new TeammemberEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.PlayerId = this.PlayerId;
			entity.Level = this.Level;
			entity.UsedProperty = this.UsedProperty;
			entity.Speed = this.Speed;
			entity.Shoot = this.Shoot;
			entity.FreeKick = this.FreeKick;
			entity.Balance = this.Balance;
			entity.Physique = this.Physique;
			entity.Bounce = this.Bounce;
			entity.Aggression = this.Aggression;
			entity.Disturb = this.Disturb;
			entity.Interception = this.Interception;
			entity.Dribble = this.Dribble;
			entity.Pass = this.Pass;
			entity.Mentality = this.Mentality;
			entity.Response = this.Response;
			entity.Positioning = this.Positioning;
			entity.HandControl = this.HandControl;
			entity.Acceleration = this.Acceleration;
			entity.UsedPlayerCard = this.UsedPlayerCard;
			entity.UsedEquipment = this.UsedEquipment;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.IsCopyed = this.IsCopyed;
			entity.IsInherited = this.IsInherited;
			entity.UsedBadge = this.UsedBadge;
			entity.ArousalLv = this.ArousalLv;
			entity.UsedClubClothes = this.UsedClubClothes;
			entity.StrengthenLevel = this.StrengthenLevel;
			entity.Power = this.Power;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Teammember 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class TeammemberResponse : BaseResponse<TeammemberEntity>
    {

    }
}
