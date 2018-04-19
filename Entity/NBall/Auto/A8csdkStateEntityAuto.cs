
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.A8csdk_State 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class A8csdkStateEntity
	{
		
		public A8csdkStateEntity()
		{
		}

		public A8csdkStateEntity(
		System.Int32 idx
,				System.String gameorderid
,				System.String orderstate
		)
		{
			this.Idx = idx;
			this.GameOrderId = gameorderid;
			this.OrderState = orderstate;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///GameOrderId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String GameOrderId {get ; set ;}

		///<summary>
		///OrderState
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String OrderState {get ; set ;}
		#endregion
        
        #region Clone
        public A8csdkStateEntity Clone()
        {
            A8csdkStateEntity entity = new A8csdkStateEntity();
			entity.Idx = this.Idx;
			entity.GameOrderId = this.GameOrderId;
			entity.OrderState = this.OrderState;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.A8csdk_State 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class A8csdkStateResponse : BaseResponse<A8csdkStateEntity>
    {

    }
}
