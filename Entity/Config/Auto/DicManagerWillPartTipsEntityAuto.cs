
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_ManagerWillPartTips 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicManagerwillparttipsEntity
	{
		
		public DicManagerwillparttipsEntity()
		{
		}

		public DicManagerwillparttipsEntity(
		System.Int32 id
,				System.Int32 skillid
,				System.String skillcode
,				System.Int32 itemcode
,				System.Int32 pid
,				System.String pname
,				System.String pnickname
,				System.Int32 pcolor
,				System.String pcolormemo
,				System.Int32 reqstrength
,				System.String buffmemo
,				System.Decimal buffarg
,				System.Decimal buffarg2
,				System.String icon
,				System.String memo
,				System.DateTime rowtime
		)
		{
			this.Id = id;
			this.SkillId = skillid;
			this.SkillCode = skillcode;
			this.ItemCode = itemcode;
			this.Pid = pid;
			this.PName = pname;
			this.PNickName = pnickname;
			this.PColor = pcolor;
			this.PColorMemo = pcolormemo;
			this.ReqStrength = reqstrength;
			this.BuffMemo = buffmemo;
			this.BuffArg = buffarg;
			this.BuffArg2 = buffarg2;
			this.Icon = icon;
			this.Memo = memo;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///Id
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Id {get ; set ;}

		///<summary>
		///SkillId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 SkillId {get ; set ;}

		///<summary>
		///SkillCode
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String SkillCode {get ; set ;}

		///<summary>
		///ItemCode
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ItemCode {get ; set ;}

		///<summary>
		///Pid
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Pid {get ; set ;}

		///<summary>
		///PName
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String PName {get ; set ;}

		///<summary>
		///PNickName
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String PNickName {get ; set ;}

		///<summary>
		///PColor
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 PColor {get ; set ;}

		///<summary>
		///PColorMemo
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.String PColorMemo {get ; set ;}

		///<summary>
		///ReqStrength
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 ReqStrength {get ; set ;}

		///<summary>
		///效果描述
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.String BuffMemo {get ; set ;}

		///<summary>
		///BuffArg
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Decimal BuffArg {get ; set ;}

		///<summary>
		///BuffArg2
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Decimal BuffArg2 {get ; set ;}

		///<summary>
		///Icon
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.String Icon {get ; set ;}

		///<summary>
		///Memo
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.String Memo {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(16)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public DicManagerwillparttipsEntity Clone()
        {
            DicManagerwillparttipsEntity entity = new DicManagerwillparttipsEntity();
			entity.Id = this.Id;
			entity.SkillId = this.SkillId;
			entity.SkillCode = this.SkillCode;
			entity.ItemCode = this.ItemCode;
			entity.Pid = this.Pid;
			entity.PName = this.PName;
			entity.PNickName = this.PNickName;
			entity.PColor = this.PColor;
			entity.PColorMemo = this.PColorMemo;
			entity.ReqStrength = this.ReqStrength;
			entity.BuffMemo = this.BuffMemo;
			entity.BuffArg = this.BuffArg;
			entity.BuffArg2 = this.BuffArg2;
			entity.Icon = this.Icon;
			entity.Memo = this.Memo;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_ManagerWillPartTips 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicManagerwillparttipsResponse : BaseResponse<DicManagerwillparttipsEntity>
    {

    }
}

