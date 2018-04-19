using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class ConfigTaskEntity
    {
        /// <summary>
        /// 需求列表
        /// </summary>
        public List<ConfigTaskrequireEntity> RequireList { get; set; }

        /// <summary>
        /// 任务涉及到的功能，以逗号分隔
        /// </summary>
        public string RequireFuncs { get; set; }
        /// <summary>
        /// 任务涉及到的功能字典，方便check
        /// </summary>
        public Dictionary<int, int> RequireFuncDic { get; set; } 

	}
	
	
    public partial class ConfigTaskResponse
    {

    }
}

