
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_EverydayActivityLegend 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigEverydayactivitylegendEntity
	{
		
		public ConfigEverydayactivitylegendEntity()
		{
		}

		public ConfigEverydayactivitylegendEntity(
		System.DateTime refreshdate
,				System.Int32 legendcode
,				System.Int32 legenddebriscode
		)
		{
			this.RefreshDate = refreshdate;
			this.LegendCode = legendcode;
			this.LegendDebrisCode = legenddebriscode;
		}
		
		#region Public Properties
		
		///<summary>
		///RefreshDate
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.DateTime RefreshDate {get ; set ;}

		///<summary>
		///LegendCode
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 LegendCode {get ; set ;}

		///<summary>
		///LegendDebrisCode
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 LegendDebrisCode {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigEverydayactivitylegendEntity Clone()
        {
            ConfigEverydayactivitylegendEntity entity = new ConfigEverydayactivitylegendEntity();
			entity.RefreshDate = this.RefreshDate;
			entity.LegendCode = this.LegendCode;
			entity.LegendDebrisCode = this.LegendDebrisCode;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_EverydayActivityLegend 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigEverydayactivitylegendResponse : BaseResponse<ConfigEverydayactivitylegendEntity>
    {

    }
}
