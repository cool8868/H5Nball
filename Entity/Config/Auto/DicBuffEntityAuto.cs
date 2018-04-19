
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_Buff 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicBuffEntity
	{
		
		public DicBuffEntity()
		{
		}

		public DicBuffEntity(
		System.Int32 buffid
,				System.String buffname
,				System.Int32 bufftype
,				System.Boolean baseflag
,				System.String basebuffmap
,				System.String icon
,				System.String memo
,				System.DateTime rowtime
		)
		{
			this.BuffId = buffid;
			this.BuffName = buffname;
			this.BuffType = bufftype;
			this.BaseFlag = baseflag;
			this.BaseBuffMap = basebuffmap;
			this.Icon = icon;
			this.Memo = memo;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///BuffId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 BuffId {get ; set ;}

		///<summary>
		///BuffName
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String BuffName {get ; set ;}

		///<summary>
		///BuffType
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 BuffType {get ; set ;}

		///<summary>
		///基础Buff
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Boolean BaseFlag {get ; set ;}

		///<summary>
		///基础Buff列表
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String BaseBuffMap {get ; set ;}

		///<summary>
		///Icon
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String Icon {get ; set ;}

		///<summary>
		///Memo
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String Memo {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public DicBuffEntity Clone()
        {
            DicBuffEntity entity = new DicBuffEntity();
			entity.BuffId = this.BuffId;
			entity.BuffName = this.BuffName;
			entity.BuffType = this.BuffType;
			entity.BaseFlag = this.BaseFlag;
			entity.BaseBuffMap = this.BaseBuffMap;
			entity.Icon = this.Icon;
			entity.Memo = this.Memo;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_Buff 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicBuffResponse : BaseResponse<DicBuffEntity>
    {

    }
}

