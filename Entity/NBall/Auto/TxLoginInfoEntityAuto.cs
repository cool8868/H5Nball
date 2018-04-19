
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Tx_LoginInfo 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class TxLogininfoEntity
	{
		
		public TxLogininfoEntity()
		{
		}

		public TxLogininfoEntity(
		System.String openid
,				System.String openkey
,				System.String pf
,				System.String format
,				System.String ext
,				System.String ext1
,				System.DateTime updatetime
,				System.DateTime rowtime
		)
		{
			this.OpenId = openid;
			this.OpenKey = openkey;
			this.Pf = pf;
			this.Format = format;
			this.Ext = ext;
			this.Ext1 = ext1;
			this.UpdateTime = updatetime;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///OpenId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.String OpenId {get ; set ;}

		///<summary>
		///OpenKey
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String OpenKey {get ; set ;}

		///<summary>
		///Pf
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String Pf {get ; set ;}

		///<summary>
		///Format
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String Format {get ; set ;}

		///<summary>
		///Ext
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String Ext {get ; set ;}

		///<summary>
		///Ext1
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String Ext1 {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public TxLogininfoEntity Clone()
        {
            TxLogininfoEntity entity = new TxLogininfoEntity();
			entity.OpenId = this.OpenId;
			entity.OpenKey = this.OpenKey;
			entity.Pf = this.Pf;
			entity.Format = this.Format;
			entity.Ext = this.Ext;
			entity.Ext1 = this.Ext1;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Tx_LoginInfo 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class TxLogininfoResponse : BaseResponse<TxLogininfoEntity>
    {

    }
}
