using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class CrosscrowdManagerEntity
	{
        public string ShowName { get; set; }
        /// <summary>
        /// cd秒数
        /// </summary>
        [DataMember]
        public int CdSeconds { get; set; }
        /// <summary>
        /// 当前点券数量
        /// </summary>
        [DataMember]
        public int CurPoint { get; set; }
        /// <summary>
        /// 清除cd需要点券数量
        /// </summary>
        [DataMember]
        public int ClearCdPoint { get; set; }
        /// <summary>
        /// 复活需要点券数量
        /// </summary>
        [DataMember]
        public int ResurrectionPoint { get; set; }
        /// <summary>
        /// 复活cd时间
        /// </summary>
        [DataMember]
        public int ResurrectionCdSeconds { get; set; }
	}
	
	
    public partial class CrosscrowdManagerResponse
    {

    }
}
