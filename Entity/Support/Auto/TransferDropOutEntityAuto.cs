
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Transfer_DropOut 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class TransferDropoutEntity
	{
		
		public TransferDropoutEntity()
		{
		}

		public TransferDropoutEntity(
		System.Int32 domaid
,				System.Int32 dropouttype
,				System.Int32 dropoutnumber
,				System.DateTime refreshtiem
,				System.DateTime rowtime
,				System.DateTime updatetime
		)
		{
			this.DomaId = domaid;
			this.DropOutType = dropouttype;
			this.DropOutNumber = dropoutnumber;
			this.RefreshTiem = refreshtiem;
			this.RowTime = rowtime;
			this.UpdateTime = updatetime;
		}
		
		#region Public Properties
		
		///<summary>
		///域ID
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 DomaId {get ; set ;}

		///<summary>
		///掉落类型
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 DropOutType {get ; set ;}

		///<summary>
		///可掉落数量
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 DropOutNumber {get ; set ;}

		///<summary>
		///刷新时间
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.DateTime RefreshTiem {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}
		#endregion
        
        #region Clone
        public TransferDropoutEntity Clone()
        {
            TransferDropoutEntity entity = new TransferDropoutEntity();
			entity.DomaId = this.DomaId;
			entity.DropOutType = this.DropOutType;
			entity.DropOutNumber = this.DropOutNumber;
			entity.RefreshTiem = this.RefreshTiem;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Transfer_DropOut 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class TransferDropoutResponse : BaseResponse<TransferDropoutEntity>
    {

    }
}
