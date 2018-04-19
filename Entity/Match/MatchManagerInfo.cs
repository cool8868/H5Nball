using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Entity.Match
{
    /// <summary>
    /// 经理报名信息，包含区和连接信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class MatchManagerInfo
    {
        public MatchManagerInfo(Guid managerId, bool isNpc)
            : this(managerId,"", isNpc,false)
        {
        }

        public MatchManagerInfo(Guid managerId, bool isNpc, int arenaType)
            : this(managerId, "", isNpc, false)
        {
            ArenaType = arenaType;
        }

        public MatchManagerInfo(Guid managerId, bool isNpc,bool isBot)
            : this(managerId, "", isNpc, isBot)
        {
        }

        public MatchManagerInfo(Guid managerId)
            : this(managerId,"")
        { }

        public MatchManagerInfo(Guid managerId,string zoneName)
            : this(managerId, zoneName, false,false)
        { }

        public MatchManagerInfo(Guid managerId, string zoneName, int arenaType)
            : this(managerId, zoneName, false, false)
        {
            ArenaType = arenaType;
        }
        public MatchManagerInfo(Guid managerId, string zoneName,bool isNpc ,bool isBot)
        {
            ManagerId = managerId;
            if (string.IsNullOrEmpty(zoneName))
                ZoneName = "";
            else
            {
                ZoneName = zoneName;
            }
            IsNpc = isNpc;
            IsBot = isBot;
            BuffScale = 100;
        }

        public MatchManagerInfo(Guid managerId, string zoneName, bool isNpc,int buff)
        {
            ManagerId = managerId;
            if (string.IsNullOrEmpty(zoneName))
                ZoneName = "";
            else
            {
                ZoneName = zoneName;
            }
            IsNpc = isNpc;
            IsBot = false;
            BuffScale = 100 + buff;
        }

        [DataMember]
        public Guid ManagerId;

        /// <summary>
        /// 竞技场类型
        /// </summary>
        [DataMember]
        public int ArenaType;

        [DataMember]
        public string Name;

        [DataMember]
        public string ZoneName;
        
        [DataMember]
        public bool IsNpc;

        [DataMember]
        public bool IsBot;
        
        [DataMember]
        public int Score;
        /// <summary>
        /// 提升buff，百分比
        /// </summary>
        [DataMember]
        public int BuffScale;
    }
}
