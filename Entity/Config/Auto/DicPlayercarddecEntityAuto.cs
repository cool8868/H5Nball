
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_PlayerCardDec 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicPlayercarddecEntity
	{
		
		public DicPlayercarddecEntity()
		{
		}

		public DicPlayercarddecEntity(
		System.Int32 itemcode
,				System.Int32 reiki
,				System.Int32 soulrange
,				System.Int32 soul
		)
		{
			this.ItemCode = itemcode;
			this.Reiki = reiki;
			this.SoulRange = soulrange;
			this.Soul = soul;
		}
		
		#region Public Properties
		
		///<summary>
		///球员卡ID
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 ItemCode {get ; set ;}

		///<summary>
		///灵气数量
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Reiki {get ; set ;}

		///<summary>
		///获得球魂概率
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 SoulRange {get ; set ;}

		///<summary>
		///球魂数量
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Soul {get ; set ;}
		#endregion
        
        #region Clone
        public DicPlayercarddecEntity Clone()
        {
            DicPlayercarddecEntity entity = new DicPlayercarddecEntity();
			entity.ItemCode = this.ItemCode;
			entity.Reiki = this.Reiki;
			entity.SoulRange = this.SoulRange;
			entity.Soul = this.Soul;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_PlayerCardDec 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicPlayercarddecResponse : BaseResponse<DicPlayercarddecEntity>
    {

    }
}

