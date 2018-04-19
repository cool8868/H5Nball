using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Games.NBall.Entity.Response
{
    [Serializable]
    [DataContract]
    public class ManagerTreeResponse : BaseResponse<ManagerTree>
    {
       
    }

    [Serializable]
    [DataContract]
    [ProtoContract]
    public class ManagerTree
    {
        /// <summary>
        /// 剩余天赋点
        /// </summary>
        [DataMember]
        public int SkillPoint { get; set; }

        /// <summary>
        /// 经理天赋类型
        /// </summary>
        [DataMember]
        public int SkillType { get; set; }

        /// <summary>
        /// 使用中的主动天赋
        /// </summary>
        [DataMember]
        public string[] UseToDoSkill { get; set; }

        /// <summary>
        /// 技能树列表
        /// </summary>
        [DataMember]
        public List<NbManagertreeEntity> TreeList { get; set; }

        /// <summary>
        /// 剩余点卷
        /// </summary>
        [DataMember]
        public int Point { get; set; }

        /// <summary>
        /// KPI
        /// </summary>
        [DataMember]
        public int Kpi = -1;

      
    }
}
