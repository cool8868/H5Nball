
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_ArenaBagConfig 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicArenabagconfigEntity
	{
		
		public DicArenabagconfigEntity()
		{
		}

		public DicArenabagconfigEntity(
		System.Int32 idx
,				System.String name
,				System.String area
,				System.String allposition
,				System.String position
,				System.String positiondesc
,				System.String cardlevel
,				System.String kpilevel
,				System.String leaguelevel
,				System.String nameen
,				System.String specific
,				System.String kpi
,				System.String capacity
,				System.String speed
,				System.String shoot
,				System.String freekick
,				System.String balance
,				System.String physique
,				System.String power
,				System.String aggression
,				System.String disturb
,				System.String interception
,				System.String dribble
,				System.String pass
,				System.String mentality
,				System.String response
,				System.String positioning
,				System.String handcontrol
,				System.String acceleration
,				System.String bounce
,				System.String club
,				System.String birthday
,				System.String stature
,				System.String weight
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
		public System.String Area {get ; set ;}

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
		public System.String Position {get ; set ;}

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
		public System.String CardLevel {get ; set ;}

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
		public System.String LeagueLevel {get ; set ;}

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
		public System.String Specific {get ; set ;}

		///<summary>
		///关键属性
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.String Kpi {get ; set ;}

		///<summary>
		///能力值
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.String Capacity {get ; set ;}

		///<summary>
		///速度
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.String Speed {get ; set ;}

		///<summary>
		///射门
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.String Shoot {get ; set ;}

		///<summary>
		///任意球
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.String FreeKick {get ; set ;}

		///<summary>
		///控制
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.String Balance {get ; set ;}

		///<summary>
		///体质
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.String Physique {get ; set ;}

		///<summary>
		///力量
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.String Power {get ; set ;}

		///<summary>
		///侵略性
		///</summary>
        [DataMember]
        [ProtoMember(20)]
		public System.String Aggression {get ; set ;}

		///<summary>
		///干扰
		///</summary>
        [DataMember]
        [ProtoMember(21)]
		public System.String Disturb {get ; set ;}

		///<summary>
		///抢断
		///</summary>
        [DataMember]
        [ProtoMember(22)]
		public System.String Interception {get ; set ;}

		///<summary>
		///控球
		///</summary>
        [DataMember]
        [ProtoMember(23)]
		public System.String Dribble {get ; set ;}

		///<summary>
		///传球
		///</summary>
        [DataMember]
        [ProtoMember(24)]
		public System.String Pass {get ; set ;}

		///<summary>
		///意识
		///</summary>
        [DataMember]
        [ProtoMember(25)]
		public System.String Mentality {get ; set ;}

		///<summary>
		///反应
		///</summary>
        [DataMember]
        [ProtoMember(26)]
		public System.String Response {get ; set ;}

		///<summary>
		///位置感
		///</summary>
        [DataMember]
        [ProtoMember(27)]
		public System.String Positioning {get ; set ;}

		///<summary>
		///手控球
		///</summary>
        [DataMember]
        [ProtoMember(28)]
		public System.String HandControl {get ; set ;}

		///<summary>
		///加速度
		///</summary>
        [DataMember]
        [ProtoMember(29)]
		public System.String Acceleration {get ; set ;}

		///<summary>
		///弹跳
		///</summary>
        [DataMember]
        [ProtoMember(30)]
		public System.String Bounce {get ; set ;}

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
		public System.String Stature {get ; set ;}

		///<summary>
		///体重
		///</summary>
        [DataMember]
        [ProtoMember(34)]
		public System.String Weight {get ; set ;}

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
        public DicArenabagconfigEntity Clone()
        {
            DicArenabagconfigEntity entity = new DicArenabagconfigEntity();
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
	/// 对Table dbo.Dic_ArenaBagConfig 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicArenabagconfigResponse : BaseResponse<DicArenabagconfigEntity>
    {

    }
}
