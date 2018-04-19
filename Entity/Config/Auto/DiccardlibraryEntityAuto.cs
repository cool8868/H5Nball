
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_CardLibrary 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicCardlibraryEntity
	{
		
		public DicCardlibraryEntity()
		{
		}

		public DicCardlibraryEntity(
		System.Int32 idx
,				System.String name
,				System.Int32 type
,				System.Int32 itemtype
,				System.Int32 subtype
,				System.Int32 thirdtype
,				System.Int32 minpower
,				System.Int32 maxpower
		)
		{
			this.Idx = idx;
			this.Name = name;
			this.Type = type;
			this.ItemType = itemtype;
			this.SubType = subtype;
			this.ThirdType = thirdtype;
			this.MinPower = minpower;
			this.MaxPower = maxpower;
		}
		
		#region Public Properties
		
		///<summary>
		///卡库id
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///卡库名字
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Name {get ; set ;}

		///<summary>
		///卡库类型
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Type {get ; set ;}

		///<summary>
		///物品类型
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ItemType {get ; set ;}

		///<summary>
		///二级分类
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 SubType {get ; set ;}

		///<summary>
		///三级分类
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 ThirdType {get ; set ;}

		///<summary>
		///能力值min
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 MinPower {get ; set ;}

		///<summary>
		///能力值max
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 MaxPower {get ; set ;}
		#endregion
        
        #region Clone
        public DicCardlibraryEntity Clone()
        {
            DicCardlibraryEntity entity = new DicCardlibraryEntity();
			entity.Idx = this.Idx;
			entity.Name = this.Name;
			entity.Type = this.Type;
			entity.ItemType = this.ItemType;
			entity.SubType = this.SubType;
			entity.ThirdType = this.ThirdType;
			entity.MinPower = this.MinPower;
			entity.MaxPower = this.MaxPower;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_CardLibrary 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicCardlibraryResponse : BaseResponse<DicCardlibraryEntity>
    {

    }
}

