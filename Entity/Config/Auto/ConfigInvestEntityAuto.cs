
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_Invest 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigInvestEntity
	{
		
		public ConfigInvestEntity()
		{
		}

		public ConfigInvestEntity(
		System.Int32 idx
,				System.Int32 step
,				System.Int32 point
,				System.Int32 lv
,				System.Int32 restorepercent
		)
		{
			this.Idx = idx;
			this.Step = step;
			this.Point = point;
			this.Lv = lv;
			this.RestorePercent = restorepercent;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///Step
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Step {get ; set ;}

		///<summary>
		///Point
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Point {get ; set ;}

		///<summary>
		///Lv
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Lv {get ; set ;}

		///<summary>
		///RestorePercent
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 RestorePercent {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigInvestEntity Clone()
        {
            ConfigInvestEntity entity = new ConfigInvestEntity();
			entity.Idx = this.Idx;
			entity.Step = this.Step;
			entity.Point = this.Point;
			entity.Lv = this.Lv;
			entity.RestorePercent = this.RestorePercent;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_Invest 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigInvestResponse : BaseResponse<ConfigInvestEntity>
    {

    }
}

