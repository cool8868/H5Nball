
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Olympic_Manager 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class OlympicManagerEntity
	{
		
		public OlympicManagerEntity()
		{
		}

		public OlympicManagerEntity(
		System.Guid managerid
,				System.Int32 football
,				System.Int32 basketball
,				System.Int32 volleyball
,				System.Int32 swimming
,				System.Int32 gymnastics
,				System.Int32 shooting
,				System.Int32 trackandfield
,				System.Int32 weightlifting
,				System.Int32 tabletennis
,				System.Int32 badminton
,				System.Int32 rowing
,				System.Int32 judo
,				System.DateTime updatetime
,				System.DateTime rowtime
		)
		{
			this.ManagerId = managerid;
			this.Football = football;
			this.Basketball = basketball;
			this.Volleyball = volleyball;
			this.Swimming = swimming;
			this.Gymnastics = gymnastics;
			this.Shooting = shooting;
			this.TrackAndField = trackandfield;
			this.WeightLifting = weightlifting;
			this.TableTennis = tabletennis;
			this.Badminton = badminton;
			this.Rowing = rowing;
			this.Judo = judo;
			this.UpdateTime = updatetime;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///足球金牌数量
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Football {get ; set ;}

		///<summary>
		///篮球金牌数量
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Basketball {get ; set ;}

		///<summary>
		///排球金牌数量
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Volleyball {get ; set ;}

		///<summary>
		///游泳金牌数量
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Swimming {get ; set ;}

		///<summary>
		///体操金牌数量
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Gymnastics {get ; set ;}

		///<summary>
		///射击金牌数量
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Shooting {get ; set ;}

		///<summary>
		///田径金牌数量
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 TrackAndField {get ; set ;}

		///<summary>
		///举重金牌数量
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 WeightLifting {get ; set ;}

		///<summary>
		///乒乓球金牌数量
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 TableTennis {get ; set ;}

		///<summary>
		///羽毛球金牌数量
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 Badminton {get ; set ;}

		///<summary>
		///赛艇金牌数量
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 Rowing {get ; set ;}

		///<summary>
		///柔道金牌数量
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 Judo {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(14)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(15)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public OlympicManagerEntity Clone()
        {
            OlympicManagerEntity entity = new OlympicManagerEntity();
			entity.ManagerId = this.ManagerId;
			entity.Football = this.Football;
			entity.Basketball = this.Basketball;
			entity.Volleyball = this.Volleyball;
			entity.Swimming = this.Swimming;
			entity.Gymnastics = this.Gymnastics;
			entity.Shooting = this.Shooting;
			entity.TrackAndField = this.TrackAndField;
			entity.WeightLifting = this.WeightLifting;
			entity.TableTennis = this.TableTennis;
			entity.Badminton = this.Badminton;
			entity.Rowing = this.Rowing;
			entity.Judo = this.Judo;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Olympic_Manager 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class OlympicManagerResponse : BaseResponse<OlympicManagerEntity>
    {

    }
}
