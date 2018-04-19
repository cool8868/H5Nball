using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom;

namespace Games.NBall.Entity.Response
{
    /// <summary>
    /// 阵型信息
    /// </summary>
    [DataContract]
    [Serializable]
    public class NBSolutionInfoResponse : BaseResponse<NBSolutionInfo>
    {
    }

    /// <summary>
    /// 阵型信息
    /// </summary>
    [DataContract]
    [Serializable]
    public class NBSolutionInfo
    {
        /// <summary>
        /// 球员串,11个球员的pid，从守门员开始，以逗号分隔
        /// </summary>
        [DataMember]
        public string PlayerString { get; set; }

        ///<summary>
        ///技能串，11个位置，从守门员开始，没有填空,以逗号分隔
        ///</summary>
        [DataMember]
        public string SkillString { get; set; }

        /// <summary>
        /// 阵型id
        /// </summary>
        [DataMember]
        public int FormationId { get; set; }

        /// <summary>
        /// 最大球员数量
        /// </summary>
        [DataMember]
        public int TeammemberMax { get; set; }

        /// <summary>
        /// 球员列表
        /// </summary>
        [DataMember]
        public List<NBSolutionTeammember> Teammembers { get; set; }

        /// <summary>
        /// 元老数量
        /// </summary>
        [DataMember]
        public int VeteranCount { get; set; }

        /// <summary>
        /// 最大元老数量
        /// </summary>
        [DataMember]
        public int MaxVeteranCount { get; set; }

        /// <summary>
        /// 球衣id
        /// </summary>
        [DataMember]
        public int ClothId { get; set; }

        /// <summary>
        /// PopMsg
        /// </summary>
        [DataMember]
        public List<PopMessageEntity> PopMsg { get; set; }
        /// <summary>
        /// Kpi
        /// </summary>
        [DataMember]
        public int Kpi { get; set; }

        /// <summary>
        /// 下阵球员列表
        /// </summary>
        [DataMember]
        public List<Guid> GoOffStageList { get; set; }
    }

    /// <summary>
    /// 阵型球员信息
    /// </summary>
    [DataContract]
    [Serializable]
    public class NBSolutionTeammember
    {
        /// <summary>
        /// 球员id
        /// </summary>
        [DataMember]
        public Guid Idx { get; set; }

        /// <summary>
        /// pid
        /// </summary>
        [DataMember]
        public int PlayerId { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        [DataMember]
        public int Level { get; set; }

        /// <summary>
        /// 能力值
        /// </summary>
        [DataMember]
        public double Kpi { get; set; }

        /// <summary>
        /// 强化等级
        /// </summary>
        [DataMember]
        public int Strength { get; set; }

        /// <summary>
        /// 阵型中的位置
        /// </summary>
        [DataMember]
        public int Position { get; set; }

        /// <summary>
        /// 是否复制
        /// </summary>
        [DataMember]
        public bool IsCopyed { get; set; }

        /// <summary>
        /// 是否传承
        /// </summary>
        [DataMember]
        public bool IsInherited { get; set; }

        /// <summary>
        /// 觉醒等级
        /// </summary>
        [DataMember]
        public int ArousalLv { get; set; }

        /// <summary>
        /// 是否是主力
        /// </summary>
        [DataMember]
        public bool IsMain { get; set; }

    }

}

