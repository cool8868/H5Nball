
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_Item 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicItemEntity
	{
		
		public DicItemEntity()
		{
		}

		public DicItemEntity(
		System.Int32 itemcode
,				System.String itemname
,				System.Int32 itemtype
,				System.Int32 subtype
,				System.Int32 thirdtype
,				System.Int32 fourthtype
,				System.Int32 imageid
,				System.Int32 linkid
		)
		{
			this.ItemCode = itemcode;
			this.ItemName = itemname;
			this.ItemType = itemtype;
			this.SubType = subtype;
			this.ThirdType = thirdtype;
			this.FourthType = fourthtype;
			this.ImageId = imageid;
			this.LinkId = linkid;
		}
		
		#region Public Properties
		
		///<summary>
		///ItemCode
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 ItemCode {get ; set ;}

		///<summary>
		///物品名称
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String ItemName {get ; set ;}

		///<summary>
		///物品类型
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ItemType {get ; set ;}

		///<summary>
		///二级分类(球员卡>颜色；装备>套装or散装；)
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 SubType {get ; set ;}

		///<summary>
		///三级分类（球员卡>所属联赛；装备>品质）
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 ThirdType {get ; set ;}

		///<summary>
		///四级分类(球员卡>综合能力)
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 FourthType {get ; set ;}

		///<summary>
		///图片id
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 ImageId {get ; set ;}

		///<summary>
		///关联id
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 LinkId {get ; set ;}
		#endregion
        
        #region Clone
        public DicItemEntity Clone()
        {
            DicItemEntity entity = new DicItemEntity();
			entity.ItemCode = this.ItemCode;
			entity.ItemName = this.ItemName;
			entity.ItemType = this.ItemType;
			entity.SubType = this.SubType;
			entity.ThirdType = this.ThirdType;
			entity.FourthType = this.FourthType;
			entity.ImageId = this.ImageId;
			entity.LinkId = this.LinkId;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_Item 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicItemResponse : BaseResponse<DicItemEntity>
    {

    }
}

