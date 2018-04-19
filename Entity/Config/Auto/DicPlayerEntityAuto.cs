
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_Player 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicPlayerEntity
	{
		
		public DicPlayerEntity()
		{
		}

		public DicPlayerEntity(
		System.Int32 idx
,				System.String name
,				System.Int32 area
,				System.String allposition
,				System.Int32 position
,				System.String positiondesc
,				System.Int32 cardlevel
,				System.String kpilevel
,				System.Int32 leaguelevel
,				System.String nameen
,				System.Double specific
,				System.Double kpi
,				System.Int32 capacity
,				System.Double speed
,				System.Double shoot
,				System.Double freekick
,				System.Double balance
,				System.Double physique
,				System.Double power
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
,				System.Double bounce
,				System.String club
,				System.String birthday
,				System.Double stature
,				System.Double weight
,				System.String nationality
,				System.String description
		)
		{
			this.Idx = idx;
			this.Name = name;
			this.Area = area;
			this.AllPosition = allposition;
			this.Position = position;
			this.PositionDesc = positiondesc;
			this.CardLevel = cardlevel;
			this.KpiLevel = kpilevel;
			this.LeagueLevel = leaguelevel;
			this.NameEn = nameen;
			this.Specific = specific;
			this.Kpi = kpi;
			this.Capacity = capacity;
			this.Speed = speed;
			this.Shoot = shoot;
			this.FreeKick = freekick;
			this.Balance = balance;
			this.Physique = physique;
			this.Power = power;
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
			this.Bounce = bounce;
			this.Club = club;
			this.Birthday = birthday;
			this.Stature = stature;
			this.Weight = weight;
			this.Nationality = nationality;
			this.Description = description;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///名字
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Name {get ; set ;}

		///<summary>
		///所属赛区
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Area {get ; set ;}

		///<summary>
		///AllPosition
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String AllPosition {get ; set ;}

		///<summary>
		///场上位置
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Position {get ; set ;}

		///<summary>
		///球员实际位置
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String PositionDesc {get ; set ;}

		///<summary>
		///卡牌颜色
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 CardLevel {get ; set ;}

		///<summary>
		///KpiLevel
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String KpiLevel {get ; set ;}

		///<summary>
		///联赛级别
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 LeagueLevel {get ; set ;}

		///<summary>
		///NameEn
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String NameEn {get ; set ;}

		///<summary>
		///具体
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Double Specific {get ; set ;}

		///<summary>
		///关键属性
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Double Kpi {get ; set ;}

		///<summary>
		///能力值
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 Capacity {get ; set ;}

		///<summary>
		///速度
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Double Speed {get ; set ;}

		///<summary>
		///射门
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Double Shoot {get ; set ;}

		///<summary>
		///任意球
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Double FreeKick {get ; set ;}

		///<summary>
		///控制
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.Double Balance {get ; set ;}

		///<summary>
		///体质
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.Double Physique {get ; set ;}

		///<summary>
		///力量
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.Double Power {get ; set ;}

		///<summary>
		///侵略性
		///</summary>
        [DataMember]
        [ProtoMember(20)]
		public System.Double Aggression {get ; set ;}

		///<summary>
		///干扰
		///</summary>
        [DataMember]
        [ProtoMember(21)]
		public System.Double Disturb {get ; set ;}

		///<summary>
		///抢断
		///</summary>
        [DataMember]
        [ProtoMember(22)]
		public System.Double Interception {get ; set ;}

		///<summary>
		///控球
		///</summary>
        [DataMember]
        [ProtoMember(23)]
		public System.Double Dribble {get ; set ;}

		///<summary>
		///传球
		///</summary>
        [DataMember]
        [ProtoMember(24)]
		public System.Double Pass {get ; set ;}

		///<summary>
		///意识
		///</summary>
        [DataMember]
        [ProtoMember(25)]
		public System.Double Mentality {get ; set ;}

		///<summary>
		///反应
		///</summary>
        [DataMember]
        [ProtoMember(26)]
		public System.Double Response {get ; set ;}

		///<summary>
		///位置感
		///</summary>
        [DataMember]
        [ProtoMember(27)]
		public System.Double Positioning {get ; set ;}

		///<summary>
		///手控球
		///</summary>
        [DataMember]
        [ProtoMember(28)]
		public System.Double HandControl {get ; set ;}

		///<summary>
		///加速度
		///</summary>
        [DataMember]
        [ProtoMember(29)]
		public System.Double Acceleration {get ; set ;}

		///<summary>
		///弹跳
		///</summary>
        [DataMember]
        [ProtoMember(30)]
		public System.Double Bounce {get ; set ;}

		///<summary>
		///俱乐部
		///</summary>
        [DataMember]
        [ProtoMember(31)]
		public System.String Club {get ; set ;}

		///<summary>
		///生日
		///</summary>
        [DataMember]
        [ProtoMember(32)]
		public System.String Birthday {get ; set ;}

		///<summary>
		///身高
		///</summary>
        [DataMember]
        [ProtoMember(33)]
		public System.Double Stature {get ; set ;}

		///<summary>
		///体重
		///</summary>
        [DataMember]
        [ProtoMember(34)]
		public System.Double Weight {get ; set ;}

		///<summary>
		///国家
		///</summary>
        [DataMember]
        [ProtoMember(35)]
		public System.String Nationality {get ; set ;}

		///<summary>
		///描述
		///</summary>
        [DataMember]
        [ProtoMember(36)]
		public System.String Description {get ; set ;}
		#endregion
        
        #region Clone
        public DicPlayerEntity Clone()
        {
            DicPlayerEntity entity = new DicPlayerEntity();
			entity.Idx = this.Idx;
			entity.Name = this.Name;
			entity.Area = this.Area;
			entity.AllPosition = this.AllPosition;
			entity.Position = this.Position;
			entity.PositionDesc = this.PositionDesc;
			entity.CardLevel = this.CardLevel;
			entity.KpiLevel = this.KpiLevel;
			entity.LeagueLevel = this.LeagueLevel;
			entity.NameEn = this.NameEn;
			entity.Specific = this.Specific;
			entity.Kpi = this.Kpi;
			entity.Capacity = this.Capacity;
			entity.Speed = this.Speed;
			entity.Shoot = this.Shoot;
			entity.FreeKick = this.FreeKick;
			entity.Balance = this.Balance;
			entity.Physique = this.Physique;
			entity.Power = this.Power;
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
			entity.Bounce = this.Bounce;
			entity.Club = this.Club;
			entity.Birthday = this.Birthday;
			entity.Stature = this.Stature;
			entity.Weight = this.Weight;
			entity.Nationality = this.Nationality;
			entity.Description = this.Description;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_Player 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicPlayerResponse : BaseResponse<DicPlayerEntity>
    {

    }
}

