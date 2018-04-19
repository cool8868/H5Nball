
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_FunctionOpen 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigFunctionopenEntity
	{
		
		public ConfigFunctionopenEntity()
		{
		}

		public ConfigFunctionopenEntity(
		System.Int32 idx
,				System.Int32 managerlevel
,				System.String functionlist
,				System.Int32 functionid
,				System.String name
,				System.String lockmemo
		)
		{
			this.Idx = idx;
			this.ManagerLevel = managerlevel;
			this.FunctionList = functionlist;
			this.FunctionId = functionid;
			this.Name = name;
			this.LockMemo = lockmemo;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///ManagerLevel
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 ManagerLevel {get ; set ;}

		///<summary>
		///FunctionList
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String FunctionList {get ; set ;}

		///<summary>
		///FunctionId
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 FunctionId {get ; set ;}

		///<summary>
		///Name
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String Name {get ; set ;}

		///<summary>
		///LockMemo
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String LockMemo {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigFunctionopenEntity Clone()
        {
            ConfigFunctionopenEntity entity = new ConfigFunctionopenEntity();
			entity.Idx = this.Idx;
			entity.ManagerLevel = this.ManagerLevel;
			entity.FunctionList = this.FunctionList;
			entity.FunctionId = this.FunctionId;
			entity.Name = this.Name;
			entity.LockMemo = this.LockMemo;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_FunctionOpen 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigFunctionopenResponse : BaseResponse<ConfigFunctionopenEntity>
    {

    }
}

