
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.All_UaFactory 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class AllUafactoryEntity
	{
		
		public AllUafactoryEntity()
		{
		}

		public AllUafactoryEntity(
		System.Int32 idx
,				System.String code
,				System.String description
		)
		{
			this.Idx = idx;
			this.Code = code;
			this.Description = description;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///Code
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Code {get ; set ;}

		///<summary>
		///Description
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String Description {get ; set ;}
		#endregion
        
        #region Clone
        public AllUafactoryEntity Clone()
        {
            AllUafactoryEntity entity = new AllUafactoryEntity();
			entity.Idx = this.Idx;
			entity.Code = this.Code;
			entity.Description = this.Description;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.All_UaFactory 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class AllUafactoryResponse : BaseResponse<AllUafactoryEntity>
    {

    }
}

