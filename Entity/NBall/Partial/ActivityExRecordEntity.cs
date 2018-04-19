using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class ActivityexRecordEntity
	{
        /// <summary>
        /// 是否需要同步
        /// </summary>
        public bool NeedSync { get; set; }

        /// <summary>
        /// 是否需要保存历史
        /// </summary>
        public bool NeedSaveHistory { get; set; }

        /// <summary>
        /// 可得到的物品串
        /// </summary>
        [DataMember]
        public string ItemString { get; set; }
	}
	
	
    public partial class ActivityexRecordResponse
    {

    }
}

