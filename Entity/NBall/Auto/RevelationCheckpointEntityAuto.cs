
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Revelation_Checkpoint 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class RevelationCheckpointEntity
	{
		
		public RevelationCheckpointEntity()
		{
		}

		public RevelationCheckpointEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 todaygeneralnums
,				System.Int32 customspass
,				System.Int32 schedule
,				System.Boolean isgeneral
,				System.DateTime generaltime
,				System.Boolean isgeneralawary
,				System.DateTime generalawarytime
,				System.String awaryitem
,				System.Int32 states
,				System.Byte[] rowversion
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.ToDayGeneralNums = todaygeneralnums;
			this.CustomsPass = customspass;
			this.Schedule = schedule;
			this.IsGeneral = isgeneral;
			this.GeneralTime = generaltime;
			this.IsGeneralAwary = isgeneralawary;
			this.GeneralAwaryTime = generalawarytime;
			this.AwaryItem = awaryitem;
			this.States = states;
			this.RowVersion = rowversion;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///经理ID
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///今天通关次数
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ToDayGeneralNums {get ; set ;}

		///<summary>
		///关卡
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 CustomsPass {get ; set ;}

		///<summary>
		///进度
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Schedule {get ; set ;}

		///<summary>
		///是否通关
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Boolean IsGeneral {get ; set ;}

		///<summary>
		///通关时间
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime GeneralTime {get ; set ;}

		///<summary>
		///是否领取通关奖励
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Boolean IsGeneralAwary {get ; set ;}

		///<summary>
		///通关奖励领取时间
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime GeneralAwaryTime {get ; set ;}

		///<summary>
		///领取奖励物品
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String AwaryItem {get ; set ;}

		///<summary>
		///States
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 States {get ; set ;}

		///<summary>
		///RowVersion
		///</summary>
        [DataMember]
        [ProtoMember(12)]
        [JsonIgnore]
		public System.Byte[] RowVersion {get ; set ;}
		#endregion
        
        #region Clone
        public RevelationCheckpointEntity Clone()
        {
            RevelationCheckpointEntity entity = new RevelationCheckpointEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.ToDayGeneralNums = this.ToDayGeneralNums;
			entity.CustomsPass = this.CustomsPass;
			entity.Schedule = this.Schedule;
			entity.IsGeneral = this.IsGeneral;
			entity.GeneralTime = this.GeneralTime;
			entity.IsGeneralAwary = this.IsGeneralAwary;
			entity.GeneralAwaryTime = this.GeneralAwaryTime;
			entity.AwaryItem = this.AwaryItem;
			entity.States = this.States;
			entity.RowVersion = this.RowVersion;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Revelation_Checkpoint 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class RevelationCheckpointResponse : BaseResponse<RevelationCheckpointEntity>
    {

    }
}

