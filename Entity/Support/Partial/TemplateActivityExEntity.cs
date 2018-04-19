using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class TemplateActivityexEntity
	{
        ///<summary>
        ///StartTime
        ///</summary>
        public System.DateTime StartTime { get; set; }

        ///<summary>
        ///EndTime
        ///</summary>
        public System.DateTime EndTime { get; set; }

        ///<summary>
        ///CloseTime
        ///</summary>
        public System.DateTime CloseTime { get; set; }

        [DataMember]
        public int ZoneActivityId { get; set; }

        public List<TemplateActivityexgroupEntity> Groups { get; set; }

        /// <summary>
        /// groupId->list
        /// </summary>
        public Dictionary<int, List<TemplateActivityexdetailEntity>> DetailDic { get; set; }
        /// <summary>
        /// 开始时间tick
        /// </summary>
        [DataMember]
        public long StartTimeTick { get; set; }
        /// <summary>
        /// 结束时间tick
        /// </summary>
        [DataMember]
        public long EndTimeTick { get; set; }
	}
	
	
    public partial class TemplateActivityexResponse
    {

    }
}

