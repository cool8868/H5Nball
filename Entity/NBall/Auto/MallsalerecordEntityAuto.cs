
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Mall_SaleRecord 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class MallSalerecordEntity
	{
		
		public MallSalerecordEntity()
		{
		}

		public MallSalerecordEntity(
		System.Guid idx
,				System.Guid managerid
,				System.Int32 mallcode
,				System.Int32 qty
,				System.Int32 currencytype
,				System.Int32 rawcurrency
,				System.Int32 paycurrency
,				System.Boolean packageflag
,				System.Int32 status
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.MallCode = mallcode;
			this.Qty = qty;
			this.CurrencyType = currencytype;
			this.RawCurrency = rawcurrency;
			this.PayCurrency = paycurrency;
			this.PackageFlag = packageflag;
			this.Status = status;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid Idx {get ; set ;}

		///<summary>
		///经理id
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///道具code
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 MallCode {get ; set ;}

		///<summary>
		///购买数量
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Qty {get ; set ;}

		///<summary>
		///货币类型
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 CurrencyType {get ; set ;}

		///<summary>
		///应付货币数量
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 RawCurrency {get ; set ;}

		///<summary>
		///实付货币数量
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 PayCurrency {get ; set ;}

		///<summary>
		///道具是否进背包，不进背包的为购买后立即消耗
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Boolean PackageFlag {get ; set ;}

		///<summary>
		///状态
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(10)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public MallSalerecordEntity Clone()
        {
            MallSalerecordEntity entity = new MallSalerecordEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.MallCode = this.MallCode;
			entity.Qty = this.Qty;
			entity.CurrencyType = this.CurrencyType;
			entity.RawCurrency = this.RawCurrency;
			entity.PayCurrency = this.PayCurrency;
			entity.PackageFlag = this.PackageFlag;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Mall_SaleRecord 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class MallSalerecordResponse : BaseResponse<MallSalerecordEntity>
    {

    }
}

