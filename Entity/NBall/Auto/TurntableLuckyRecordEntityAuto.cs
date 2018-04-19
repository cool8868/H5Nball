
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Turntable_LuckyRecord 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class TurntableLuckyrecordEntity
	{
		
		public TurntableLuckyrecordEntity()
		{
		}

		public TurntableLuckyrecordEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Boolean isadd
,				System.Int32 operationnumber
,				System.DateTime rowtime
,				System.String luckdrawstring
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.IsAdd = isadd;
			this.OperationNumber = operationnumber;
			this.RowTime = rowtime;
			this.LuckDrawString = luckdrawstring;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///IsAdd
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Boolean IsAdd {get ; set ;}

		///<summary>
		///OperationNumber
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 OperationNumber {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///LuckDrawString
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String LuckDrawString {get ; set ;}
		#endregion
        
        #region Clone
        public TurntableLuckyrecordEntity Clone()
        {
            TurntableLuckyrecordEntity entity = new TurntableLuckyrecordEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.IsAdd = this.IsAdd;
			entity.OperationNumber = this.OperationNumber;
			entity.RowTime = this.RowTime;
			entity.LuckDrawString = this.LuckDrawString;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Turntable_LuckyRecord 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class TurntableLuckyrecordResponse : BaseResponse<TurntableLuckyrecordEntity>
    {

    }
}
