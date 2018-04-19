using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class DicManagerwilltipsEntity
	{
        /// <summary>
        /// 球员列表
        /// </summary>
        [DataMember]
        public List<DicManagerwillparttipsEntity> PartList
        {
            get;
            set;
        }
	}
	
	
    public partial class DicManagerwilltipsResponse
    {

    }
}

