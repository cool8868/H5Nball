
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_Task 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigTaskEntity
	{
		
		public ConfigTaskEntity()
		{
		}

		public ConfigTaskEntity(
		System.Int32 idx
,				System.String name
,				System.Int32 tasktype
,				System.Int32 managerlevel
,				System.Int32 parentid
,				System.Int32 times
,				System.Int32 prizeexp
,				System.Int32 prizecoin
,				System.Int32 prizeitemcode
,				System.Int32 openfunc
,				System.String guidebuff
,				System.Boolean uniqueconstraint
,				System.String description
,				System.String tip
,				System.Int32 npcidx
,				System.Int32 recordperiod
		)
		{
			this.Idx = idx;
			this.Name = name;
			this.TaskType = tasktype;
			this.ManagerLevel = managerlevel;
			this.ParentId = parentid;
			this.Times = times;
			this.PrizeExp = prizeexp;
			this.PrizeCoin = prizecoin;
			this.PrizeItemCode = prizeitemcode;
			this.OpenFunc = openfunc;
			this.GuideBuff = guidebuff;
			this.UniqueConstraint = uniqueconstraint;
			this.Description = description;
			this.Tip = tip;
			this.NpcIdx = npcidx;
			this.RecordPeriod = recordperiod;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///任务名称
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Name {get ; set ;}

		///<summary>
		///任务类型：1，主线任务；2，支线任务；3，每日任务
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 TaskType {get ; set ;}

		///<summary>
		///所需等级
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ManagerLevel {get ; set ;}

		///<summary>
		///前置id
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 ParentId {get ; set ;}

		///<summary>
		///需完成数量
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Times {get ; set ;}

		///<summary>
		///奖励经验
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 PrizeExp {get ; set ;}

		///<summary>
		///奖励游戏币
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 PrizeCoin {get ; set ;}

		///<summary>
		///奖励物品
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 PrizeItemCode {get ; set ;}

		///<summary>
		///开放功能id
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 OpenFunc {get ; set ;}

		///<summary>
		///新手buff
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.String GuideBuff {get ; set ;}

		///<summary>
		///是否唯一，即同一任务下，执行操作的key需唯一
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Boolean UniqueConstraint {get ; set ;}

		///<summary>
		///描述
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.String Description {get ; set ;}

		///<summary>
		///Tip
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.String Tip {get ; set ;}

		///<summary>
		///NPC 序号 针对巡回赛任务有效
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Int32 NpcIdx {get ; set ;}

		///<summary>
		///记录周期  0永久 1单日 2天梯专属 单赛季 3天梯专属，记录天梯积分
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Int32 RecordPeriod {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigTaskEntity Clone()
        {
            ConfigTaskEntity entity = new ConfigTaskEntity();
			entity.Idx = this.Idx;
			entity.Name = this.Name;
			entity.TaskType = this.TaskType;
			entity.ManagerLevel = this.ManagerLevel;
			entity.ParentId = this.ParentId;
			entity.Times = this.Times;
			entity.PrizeExp = this.PrizeExp;
			entity.PrizeCoin = this.PrizeCoin;
			entity.PrizeItemCode = this.PrizeItemCode;
			entity.OpenFunc = this.OpenFunc;
			entity.GuideBuff = this.GuideBuff;
			entity.UniqueConstraint = this.UniqueConstraint;
			entity.Description = this.Description;
			entity.Tip = this.Tip;
			entity.NpcIdx = this.NpcIdx;
			entity.RecordPeriod = this.RecordPeriod;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_Task 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigTaskResponse : BaseResponse<ConfigTaskEntity>
    {

    }
}

