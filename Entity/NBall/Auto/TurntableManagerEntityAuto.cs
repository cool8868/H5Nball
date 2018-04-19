
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Turntable_Manager 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class TurntableManagerEntity
	{
		
		public TurntableManagerEntity()
		{
		}

		public TurntableManagerEntity(
		System.Guid managerid
,				System.Int32 luckycoin
,				System.Int32 giveluckycoin
,				System.Int32 dayproduceluckycoin
,				System.Int32 turntabletype
,				System.Byte[] detailsstring
,				System.String resetnumber
,				System.DateTime refreshdate
,				System.DateTime updatetime
,				System.DateTime rowtime
		)
		{
			this.ManagerId = managerid;
			this.LuckyCoin = luckycoin;
			this.GiveLuckyCoin = giveluckycoin;
			this.DayProduceLuckyCoin = dayproduceluckycoin;
			this.TurntableType = turntabletype;
			this.DetailsString = detailsstring;
			this.ResetNumber = resetnumber;
			this.RefreshDate = refreshdate;
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
		///幸运币数量
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 LuckyCoin {get ; set ;}

		///<summary>
		///赠送的幸运币
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 GiveLuckyCoin {get ; set ;}

		///<summary>
		///系统每天产出的幸运币数量
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 DayProduceLuckyCoin {get ; set ;}

		///<summary>
		///转盘类型 1=青铜 2=白银  3=黄金
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 TurntableType {get ; set ;}

		///<summary>
		///转盘详情串
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.Byte[] DetailsString {get ; set ;}

		///<summary>
		///重置次数 1,1|2,1|3,1|（转盘ID,次数）
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String ResetNumber {get ; set ;}

		///<summary>
		///RefreshDate
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.DateTime RefreshDate {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(10)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public TurntableManagerEntity Clone()
        {
            TurntableManagerEntity entity = new TurntableManagerEntity();
			entity.ManagerId = this.ManagerId;
			entity.LuckyCoin = this.LuckyCoin;
			entity.GiveLuckyCoin = this.GiveLuckyCoin;
			entity.DayProduceLuckyCoin = this.DayProduceLuckyCoin;
			entity.TurntableType = this.TurntableType;
			entity.DetailsString = this.DetailsString;
			entity.ResetNumber = this.ResetNumber;
			entity.RefreshDate = this.RefreshDate;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Turntable_Manager 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class TurntableManagerResponse : BaseResponse<TurntableManagerEntity>
    {

    }
}
