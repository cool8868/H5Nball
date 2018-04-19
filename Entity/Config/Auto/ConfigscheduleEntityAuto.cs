
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_Schedule 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigScheduleEntity
	{
		
		public ConfigScheduleEntity()
		{
		}

		public ConfigScheduleEntity(
		System.Int32 idx
,				System.String name
,				System.Int32 category
,				System.Int32 actiontype
,				System.Int32 timetype
,				System.String setting
,				System.Int32 times
,				System.Int32 retrytimes
,				System.String parameters
,				System.Int32 rundelay
,				System.String description
		)
		{
			this.Idx = idx;
			this.Name = name;
			this.Category = category;
			this.ActionType = actiontype;
			this.TimeType = timetype;
			this.Setting = setting;
			this.Times = times;
			this.RetryTimes = retrytimes;
			this.Parameters = parameters;
			this.RunDelay = rundelay;
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
		///计划任务名
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Name {get ; set ;}

		///<summary>
		///Category
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Category {get ; set ;}

		///<summary>
		///ActionType
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ActionType {get ; set ;}

		///<summary>
		///计划类型：1，按时间表；2，按间隔
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 TimeType {get ; set ;}

		///<summary>
		///执行时间配置
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String Setting {get ; set ;}

		///<summary>
		///执行次数：-1，循环执行；0，暂停执行；1，只执行一次
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Times {get ; set ;}

		///<summary>
		///RetryTimes
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 RetryTimes {get ; set ;}

		///<summary>
		///参数，逗号分隔
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.String Parameters {get ; set ;}

		///<summary>
		///RunDelay
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 RunDelay {get ; set ;}

		///<summary>
		///描述
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.String Description {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigScheduleEntity Clone()
        {
            ConfigScheduleEntity entity = new ConfigScheduleEntity();
			entity.Idx = this.Idx;
			entity.Name = this.Name;
			entity.Category = this.Category;
			entity.ActionType = this.ActionType;
			entity.TimeType = this.TimeType;
			entity.Setting = this.Setting;
			entity.Times = this.Times;
			entity.RetryTimes = this.RetryTimes;
			entity.Parameters = this.Parameters;
			entity.RunDelay = this.RunDelay;
			entity.Description = this.Description;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_Schedule 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigScheduleResponse : BaseResponse<ConfigScheduleEntity>
    {

    }
}

