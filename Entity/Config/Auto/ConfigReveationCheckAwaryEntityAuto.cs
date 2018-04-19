
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_ReveationCheckAwary 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigReveationcheckawaryEntity
	{
		
		public ConfigReveationcheckawaryEntity()
		{
		}

		public ConfigReveationcheckawaryEntity(
		System.Int32 mark
,				System.Int32 littlelevels
,				System.Guid npcid
,				System.String checkpointplayers
,				System.String describe
,				System.String thestory
,				System.String awarythecourageto
,				System.Int32 exp
,				System.Int32 gold
,				System.String team
		)
		{
			this.Mark = mark;
			this.LittleLevels = littlelevels;
			this.NpcId = npcid;
			this.CheckpointPlayers = checkpointplayers;
			this.Describe = describe;
			this.TheStory = thestory;
			this.AwaryTheCourageTo = awarythecourageto;
			this.Exp = exp;
			this.Gold = gold;
			this.Team = team;
		}
		
		#region Public Properties
		
		///<summary>
		///关卡
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Mark {get ; set ;}

		///<summary>
		///进度
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 LittleLevels {get ; set ;}

		///<summary>
		///NPC
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Guid NpcId {get ; set ;}

		///<summary>
		///球员
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String CheckpointPlayers {get ; set ;}

		///<summary>
		///关卡名
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String Describe {get ; set ;}

		///<summary>
		///故事
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String TheStory {get ; set ;}

		///<summary>
		///奖励勇气值
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String AwaryTheCourageTo {get ; set ;}

		///<summary>
		///奖励经验
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 Exp {get ; set ;}

		///<summary>
		///奖励金币
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 Gold {get ; set ;}

		///<summary>
		///队徽图片路径
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String Team {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigReveationcheckawaryEntity Clone()
        {
            ConfigReveationcheckawaryEntity entity = new ConfigReveationcheckawaryEntity();
			entity.Mark = this.Mark;
			entity.LittleLevels = this.LittleLevels;
			entity.NpcId = this.NpcId;
			entity.CheckpointPlayers = this.CheckpointPlayers;
			entity.Describe = this.Describe;
			entity.TheStory = this.TheStory;
			entity.AwaryTheCourageTo = this.AwaryTheCourageTo;
			entity.Exp = this.Exp;
			entity.Gold = this.Gold;
			entity.Team = this.Team;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_ReveationCheckAwary 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigReveationcheckawaryResponse : BaseResponse<ConfigReveationcheckawaryEntity>
    {

    }
}

