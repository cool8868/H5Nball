using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class DicActivityEntity
	{
        /// <summary>
        /// 步骤列表
        /// </summary>
        public Dictionary<int, DicActivitystepEntity> StepDic { get; set; } 
	}
	
	
    public partial class DicActivityResponse
    {

    }
}

