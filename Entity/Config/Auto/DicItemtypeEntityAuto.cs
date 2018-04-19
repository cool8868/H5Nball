
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_ItemType 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicItemtypeEntity
	{
		
		public DicItemtypeEntity()
		{
		}

		public DicItemtypeEntity(
		System.Int32 idx
,				System.String name
,				System.Int32 lapovercount
		)
		{
			this.Idx = idx;
			this.Name = name;
			this.LapoverCount = lapovercount;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///名字
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Name {get ; set ;}

		///<summary>
		///可堆叠数量
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 LapoverCount {get ; set ;}
		#endregion
        
        #region Clone
        public DicItemtypeEntity Clone()
        {
            DicItemtypeEntity entity = new DicItemtypeEntity();
			entity.Idx = this.Idx;
			entity.Name = this.Name;
			entity.LapoverCount = this.LapoverCount;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_ItemType 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicItemtypeResponse : BaseResponse<DicItemtypeEntity>
    {

    }
}

