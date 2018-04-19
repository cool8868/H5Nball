
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Online_Info 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class OnlineInfoEntity
	{
		
		public OnlineInfoEntity()
		{
		}

		public OnlineInfoEntity(
		System.Guid managerid
,				System.Boolean activeflag
,				System.Boolean resetflag
,				System.DateTime logintime
,				System.DateTime guildintime
,				System.DateTime activetime
,				System.Int64 totalonlineminutes
,				System.Int32 cntonlineminutes
,				System.Int32 curonlineminutes
,				System.String loginip
,				System.Int32 status
,				System.DateTime rowtime
		)
		{
			this.ManagerId = managerid;
			this.ActiveFlag = activeflag;
			this.ResetFlag = resetflag;
			this.LoginTime = logintime;
			this.GuildInTime = guildintime;
			this.ActiveTime = activetime;
			this.TotalOnlineMinutes = totalonlineminutes;
			this.CntOnlineMinutes = cntonlineminutes;
			this.CurOnlineMinutes = curonlineminutes;
			this.LoginIp = loginip;
			this.Status = status;
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
		///在线标记:当前在线的为1
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Boolean ActiveFlag {get ; set ;}

		///<summary>
		///重置标记:当天上过线的为1
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Boolean ResetFlag {get ; set ;}

		///<summary>
		///最近登录时间
		///</summary>
        [DataMember]
        [ProtoMember(4)]
        [JsonIgnore]
		public System.DateTime LoginTime {get ; set ;}

		///<summary>
		///GuildInTime
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime GuildInTime {get ; set ;}

		///<summary>
		///最后活跃时间
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime ActiveTime {get ; set ;}

		///<summary>
		///总计在线
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int64 TotalOnlineMinutes {get ; set ;}

		///<summary>
		///累计在线时间
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 CntOnlineMinutes {get ; set ;}

		///<summary>
		///当前在线时间
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 CurOnlineMinutes {get ; set ;}

		///<summary>
		///LoginIp
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String LoginIp {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(12)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public OnlineInfoEntity Clone()
        {
            OnlineInfoEntity entity = new OnlineInfoEntity();
			entity.ManagerId = this.ManagerId;
			entity.ActiveFlag = this.ActiveFlag;
			entity.ResetFlag = this.ResetFlag;
			entity.LoginTime = this.LoginTime;
			entity.GuildInTime = this.GuildInTime;
			entity.ActiveTime = this.ActiveTime;
			entity.TotalOnlineMinutes = this.TotalOnlineMinutes;
			entity.CntOnlineMinutes = this.CntOnlineMinutes;
			entity.CurOnlineMinutes = this.CurOnlineMinutes;
			entity.LoginIp = this.LoginIp;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Online_Info 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class OnlineInfoResponse : BaseResponse<OnlineInfoEntity>
    {

    }
}

