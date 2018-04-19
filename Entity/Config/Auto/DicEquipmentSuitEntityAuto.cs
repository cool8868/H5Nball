
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_EquipmentSuit 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicEquipmentsuitEntity
	{
		
		public DicEquipmentsuitEntity()
		{
		}

		public DicEquipmentsuitEntity(
		System.Int32 idx
,				System.Int32 suittype
,				System.String name
,				System.String memo1
,				System.String memo2
,				System.String memo3
		)
		{
			this.Idx = idx;
			this.SuitType = suittype;
			this.Name = name;
			this.Memo1 = memo1;
			this.Memo2 = memo2;
			this.Memo3 = memo3;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///套装类型
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 SuitType {get ; set ;}

		///<summary>
		///套装名称
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String Name {get ; set ;}

		///<summary>
		///3件套描述
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String Memo1 {get ; set ;}

		///<summary>
		///5件套描述
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String Memo2 {get ; set ;}

		///<summary>
		///7件套描述
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String Memo3 {get ; set ;}
		#endregion
        
        #region Clone
        public DicEquipmentsuitEntity Clone()
        {
            DicEquipmentsuitEntity entity = new DicEquipmentsuitEntity();
			entity.Idx = this.Idx;
			entity.SuitType = this.SuitType;
			entity.Name = this.Name;
			entity.Memo1 = this.Memo1;
			entity.Memo2 = this.Memo2;
			entity.Memo3 = this.Memo3;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_EquipmentSuit 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicEquipmentsuitResponse : BaseResponse<DicEquipmentsuitEntity>
    {

    }
}

