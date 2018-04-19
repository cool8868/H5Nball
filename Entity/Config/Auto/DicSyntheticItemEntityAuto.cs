
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_SyntheticItem 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicSyntheticitemEntity
	{
		
		public DicSyntheticitemEntity()
		{
		}

		public DicSyntheticitemEntity(
		System.Int32 itemcode
,				System.Int32 taritemcode
		)
		{
			this.ItemCode = itemcode;
			this.TarItemCode = taritemcode;
		}
		
		#region Public Properties
		
		///<summary>
		///ItemCode
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 ItemCode {get ; set ;}

		///<summary>
		///TarItemCode
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 TarItemCode {get ; set ;}
		#endregion
        
        #region Clone
        public DicSyntheticitemEntity Clone()
        {
            DicSyntheticitemEntity entity = new DicSyntheticitemEntity();
			entity.ItemCode = this.ItemCode;
			entity.TarItemCode = this.TarItemCode;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_SyntheticItem 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicSyntheticitemResponse : BaseResponse<DicSyntheticitemEntity>
    {

    }
}

