using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class TemplateActivityexgroupEntity
	{
        public bool HasSend { get; set; }

        public DateTime RecordDate { get; set; }

        public bool HasSendDaily
        {
            get
            {
                if (HasSend && RecordDate == DateTime.Today)
                    return true;
                else
                {
                    return false;
                }
            }
        }

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

        public int ZoneActivityId { get; set; }

        public List<TemplateActivityexdetailEntity> Details { get; set; }
        /// <summary>
        /// exStep-->prize list
        /// </summary>
        public Dictionary<int, List<TemplateActivityexprizeEntity>> PrizeDic { get; set; } 
	}
	
	
    public partial class TemplateActivityexgroupResponse
    {

    }
}

