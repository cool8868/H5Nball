using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{
    /// <summary>
    /// 对Table dbo.Ladder_Info 的数据映射.
    /// </summary>	
    public partial class LadderInfoEntity
    {
        public List<LadderManagerEntity> FightList
        { get; set; }
    }


    /// <summary>
    /// 对Table dbo.Ladder_Info 的输出映射.
    /// </summary>
    public partial class LadderInfoResponse
    {

    }
}

