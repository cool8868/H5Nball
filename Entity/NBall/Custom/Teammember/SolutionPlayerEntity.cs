using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Games.NBall.Entity.NBall.Custom.Teammember
{
    [Serializable]
    [ProtoContract]
    public class SolutionPlayerEntity
    {
        /// <summary>
        /// 场上位置
        /// </summary>
        [ProtoMember(1)]
        public int Position { get; set; }

        /// <summary>
        /// 技能
        /// </summary>
        [ProtoMember(2)]
        public string SkillCode { get; set; }
    }
}
