using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom;
using ProtoBuf;

namespace Games.NBall.Entity.Response
{
    public class ExcPoolBuffItem
    {
        public string SkillCode
        {
            get;
            set;
        }
        public int SkillLevel
        {
            get;
            set;
        }
        public int BuffNo
        {
            get;
            set;
        }
    }
    [DataContract]
    public class DTOBuffValue
    {
        public DTOBuffValue()
        { }
        public DTOBuffValue(int buffId)
        {
            this.BuffId = buffId;
        }
        [DataMember]
        public int BuffId
        {
            get;
            set;
        }
        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        public string BuffName
        {
            get;
            set;
        }
        /// <summary>
        /// 绝对值
        /// </summary>
        [DataMember]
        public double Point
        {
            get;
            set;
        }
        /// <summary>
        /// 百分比
        /// </summary>
        [DataMember]
        public double Percent
        {
            get;
            set;
        }
        /// <summary>
        /// Buff图标
        /// </summary>
        [DataMember]
        public string Icon
        {
            get;
            set;
        }
        /// <summary>
        /// 效果描述
        /// </summary>
        [DataMember]
        public string BuffMemo
        {
            get;
            set;
        }
        /// <summary>
        /// 来源列表
        /// </summary>
        [DataMember]
        public List<DTOBuffSource> SrcList
        {
            get;
            set;
        }
    }
    [DataContract]
    public class DTOBuffSource
    {
        /// <summary>
        /// Idx
        /// </summary>
        public long Idx { get; set; }

        /// <summary>
        /// 来源Code
        /// </summary>
        [DataMember]
        public string SkillCode
        {
            get;
            set;
        }
        /// <summary>
        /// 来源等级
        /// </summary>
        [DataMember]
        public int SkillLevel
        {
            get;
            set;
        }
        /// <summary>
        /// 来源名称
        /// </summary>
        [DataMember]
        public string SkillName
        {
            get;
            set;
        }
        /// <summary>
        /// 绝对值
        /// </summary>
        [DataMember]
        public double Point
        {
            get;
            set;
        }
        /// <summary>
        /// 百分比
        /// </summary>
        [DataMember]
        public double Percent
        {
            get;
            set;
        }
        /// <summary>
        /// 过期时间,相对于基准时间的毫秒数
        /// </summary>
        [DataMember]
        public long ExpiryTime
        {
            get;
            set;
        }
    }

    #region BuffPack
    public class DTOBuffProp
    {
        public double Orig = 0;
        public double Percent = 0;
        public double Point = 0;
        public double BuffValue
        {
            get { return Orig * Percent + Point; }
        }
        public double TotalValue
        {
            get { return Orig * (1 + Percent) + Point; }
        }
        public DTOBuffProp Clone()
        {
            return new DTOBuffProp
            {
                Orig = this.Orig,
                Percent = this.Percent,
                Point = this.Point,
            };
        }
    }
    public class DTOBuffPlayer
    {
        public int Pid;
        public int Pos;
        public int PosOn;
        public int Clr;
        public int Strength;
        public int Level;
        public int ShowOrder;
        public int ArousalLv;
        public string Club;
        public string Nationality;
        /// <summary>
        /// 通用技能
        /// </summary>
        public string ActionSkill { get; set; }
        /// <summary>
        /// 球星技能
        /// </summary>
        public string StarSkill { get; set; }

        public string[] AsStarSkill
        {
            get { return StarSkill.Split(','); }
        }
        /// <summary>
        /// 球星封印
        /// </summary>
        public string StarPlusSkill { get; set; }
        public List<string> SBMList { get; set; }

        #region Buff
        public List<MatchPropInput> MatchPropList
        {
            get;
            set;
        }
        public List<MatchBoostInput> MatchBoostList
        {
            get;
            set;
        }
        public void AddMatchBuff(double point, double percent, params int[] buffId)
        {
            if (null == buffId)
                return;
            if (buffId[0] < 9000)
            {
                if (null == MatchPropList)
                    MatchPropList = new List<MatchPropInput>();
                MatchPropList.Add(new MatchPropInput(point, percent, buffId));
            }
            else if (buffId[0] > 9900)
            {
                if (null == MatchBoostList)
                    MatchBoostList = new List<MatchBoostInput>();
                MatchBoostList.Add(new MatchBoostInput(7, point, percent, buffId));
            }
        }
        #endregion

        public DTOBuffProp[] Props
        {
            get;
            set;
        }
        public int AsPid
        {
            get { return Math.Abs(Pid); }
        }
        public bool OnFlag
        {
            get { return Pid > 0; }
        }
        public void ClearBuff()
        {
            if (null == Props)
                return;
            foreach (var item in Props)
            {
                item.Percent = 0;
                item.Point = 0;
            }
        }
        public DTOBuffPlayer Clone()
        {
            var obj = new DTOBuffPlayer();
            obj.Pid = this.Pid;
            obj.Pos = this.Pos;
            obj.PosOn = this.PosOn;
            obj.Clr = this.Clr;
            obj.Club = this.Club;
            obj.Nationality = this.Nationality;
            obj.Strength = this.Strength;
            obj.Level = this.Level;
            obj.ShowOrder = this.ShowOrder;
            obj.ArousalLv = this.ArousalLv;
            obj.ActionSkill = this.ActionSkill;
            obj.StarSkill = this.StarSkill;
            obj.StarPlusSkill = this.StarPlusSkill;
            obj.SBMList = this.SBMList;
            if (null != this.Props)
            {
                obj.Props = new DTOBuffProp[this.Props.Length];
                for (int i = 0; i < this.Props.Length; i++)
                {
                    obj.Props[i] = this.Props[i].Clone();
                }
            }
            return obj;
        }
    }
    public class DTOBuffPack
    {
        #region Cache
        Guid _homeMid;
        Guid _awayMid;
        Dictionary<Guid, TeammemberEntity> _homeRawMembers;
        Dictionary<Guid, TeammemberEntity> _awayRawMembers;
        Dictionary<int, Guid> _homeOnPids;
        Dictionary<int, Guid> _awayOnPids;
        Dictionary<Guid, DTOBuffPlayer> _homePlayers;
        Dictionary<Guid, DTOBuffPlayer> _awayPlayers;
        List<DTOBuffPlayer> _homeOnPlayers;
        List<DTOBuffPlayer> _awayOnPlayers;
        NbSolutionEntity _homeSolution;
        NbSolutionEntity _awaySolution;
        //主队汇总的buff列表，需要放到buff引擎里计算
        List<string> _homeSBMList;
        //客队汇总的buff列表，需要放到buff引擎里计算
        List<string> _awaySBMList;
        #endregion

        #region RawData
        public Guid GetMid(bool homeFlag)
        {
            return homeFlag ? _homeMid : _awayMid;
        }
        public void SetMid(bool homeFlag, Guid mid)
        {
            if (homeFlag)
                _homeMid = mid;
            else
                _awayMid = mid;
        }
        public Dictionary<Guid, TeammemberEntity> GetRawMembers(bool homeFlag)
        {
            return homeFlag ? _homeRawMembers : _awayRawMembers;
        }
        public void SetRawMembers(bool homeFlag, Dictionary<Guid, TeammemberEntity> rawMembers)
        {
            if (homeFlag)
                _homeRawMembers = rawMembers;
            else
                _awayRawMembers = rawMembers;
        }
        public Dictionary<int, Guid> GetOnPids(bool homeFlag)
        {
            return homeFlag ? _homeOnPids : _awayOnPids;
        }
        public void SetOnPids(bool homeFlag, Dictionary<int, Guid> pids)
        {
            if (homeFlag)
                _homeOnPids = pids;
            else
                _awayOnPids = pids;
        }
        public NbSolutionEntity GetSolution(bool homeFlag)
        {
            return homeFlag ? _homeSolution : _awaySolution;
        }

        public void SetSolution(bool homeFlag, NbSolutionEntity solutionEntity)
        {
            if (homeFlag)
                _homeSolution = solutionEntity;
            else
                _awaySolution = solutionEntity;
        }

        public List<string> GetSBM(bool homeFlag)
        {
            return homeFlag ? _homeSBMList : _awaySBMList;
        }

        public void SetSBM(bool homeFlag, List<string> sbmList)
        {
            if (homeFlag)
                _homeSBMList = sbmList;
            else
                _awaySBMList = sbmList;
        }
        #endregion

        #region BuffData
        public Dictionary<Guid, DTOBuffPlayer> GetBuffPlayers(bool homeFlag)
        {
            return homeFlag ? _homePlayers : _awayPlayers;
        }
        public void SetBuffPlayers(bool homeFlag, Dictionary<Guid, DTOBuffPlayer> players)
        {
            if (homeFlag)
                _homePlayers = players;
            else
                _awayPlayers = players;
        }
        public List<DTOBuffPlayer> GetOnBuffPlayers(bool homeFlag)
        {
            return homeFlag ? _homeOnPlayers : _awayOnPlayers;
        }
        public void SetOnBuffPlayers(bool homeFlag, List<DTOBuffPlayer> players)
        {
            if (homeFlag)
                _homeOnPlayers = players;
            else
                _awayOnPlayers = players;
        }
        #endregion
    }
    #endregion

    #region BuffView
    [DataContract]
    [Serializable]
    [ProtoContract]
    public class MatchPropInput
    {
        public MatchPropInput()
        { }
        public MatchPropInput(double point, double percent, params int[] buffId)
        {
            this.Point = point;
            this.Percent = percent;
            this.BuffId = buffId;
        }
        [DataMember]
        [ProtoMember(10)]
        public int[] BuffId
        {
            get;
            set;
        }
        [DataMember]
        [ProtoMember(1)]
        public double Point
        {
            get;
            set;
        }
        [DataMember]
        [ProtoMember(2)]
        public double Percent
        {
            get;
            set;
        }
    }
    [DataContract]
    [Serializable]
    [ProtoContract]
    public class MatchBoostInput
    {
        public MatchBoostInput()
        { }
        public MatchBoostInput(int boostType, double point, double percent, params int[] buffId)
        {
            this.BoostType = boostType;
            this.Point = point;
            this.Percent = percent;
            this.BuffId = buffId;
        }
        [DataMember]
        [ProtoMember(1)]
        public int BoostType
        {
            get;
            set;
        }
        [DataMember]
        [ProtoMember(2)]
        public double Point
        {
            get;
            set;
        }
        [DataMember]
        [ProtoMember(3)]
        public double Percent
        {
            get;
            set;
        }
        [DataMember]
        [ProtoMember(10)]
        public int[] BuffId
        {
            get;
            set;
        }
    }
    [DataContract]
    [Serializable]
    [ProtoContract]
    public class DTOManagerBuffView
    {
        /// <summary>
        /// 经理Buff列表
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        public List<DTOBuffValue> BuffList
        {
            get;
            set;
        }

    }

    [DataContract]
    [Serializable]
    [ProtoContract]
    public class DTOBuffPoolView
    {
        [DataMember]
        [ProtoMember(1)]
        public DateTime CreateTime
        {
            get;
            set;
        }

        [DataMember]
        [ProtoMember(2)]
        public DateTime SyncTime
        {
            get;
            set;
        }

        [DataMember]
        [ProtoMember(3)]
        public ulong SyncVersion
        {
            get;
            set;
        }

        /// <summary>
        /// Buff列表
        /// </summary>
        [DataMember]
        [ProtoMember(11)]
        public List<NbManagerbuffpoolEntity> BuffPools
        {
            get;
            set;
        }

        [DataMember]
        [ProtoMember(12)]
        public List<string> LiveSkills
        {
            get;
            set;
        }
    }

    [DataContract]
    [Serializable]
    [ProtoContract]
    public class DTOBuffMemberView
    {
        [DataMember]
        [ProtoMember(1)]
        public DateTime CreateTime
        {
            get;
            set;
        }

        [DataMember]
        [ProtoMember(2)]
        public bool SyncIdleFlag
        {
            get;
            set;
        }

        [DataMember]
        [ProtoMember(3)]
        public ulong UseSyncVersion
        {
            get;
            set;
        }

        [DataMember]
        [ProtoMember(4)]
        public DateTime PoolSyncTime
        {
            get;
            set;
        }

        [DataMember]
        [ProtoMember(5)]
        public ulong PoolSyncVersion
        {
            get;
            set;
        }
        [DataMember]
        [ProtoMember(6)]
        public int ClothId
        {
            get;
            set;
        }
        /// <summary>
        /// Kpi
        /// </summary>
        [DataMember]
        [ProtoMember(10)]
        public int Kpi { get; set; }
        /// <summary>
        /// 赛前kpi
        /// </summary>
        [DataMember]
        [ProtoMember(9)]
        public int KpiReady { get; set; }
        /// <summary>
        ///  球员列表
        /// </summary>
        [DataMember]
        [ProtoMember(11)]
        public Dictionary<Guid, NbManagerbuffmemberEntity> BuffMembers
        {
            get;
            set;
        }
        /// <summary>
        ///  原始球员列表
        /// </summary>
        [DataMember]
        [ProtoMember(21)]
        public Dictionary<Guid, TeammemberEntity> RawMembers
        {
            get;
            set;
        }
        public Dictionary<Guid, DTOBuffPlayer> BuffPlayers
        {
            get;
            set;
        }
        /// <summary>
        ///  场前处理技能列表
        /// </summary>
        [DataMember]
        [ProtoMember(12)]
        public List<string> ReadySkillList
        {
            get;
            set;
        }
        /// <summary>
        ///  场上处理技能列表
        /// </summary>
        [DataMember]
        [ProtoMember(13)]
        public List<string> LiveSkillList
        {
            get;
            set;
        }
        /// <summary>
        ///  被动技能列表
        /// </summary>
        [DataMember]
        [ProtoMember(14)]
        public List<string> FirmSkillList
        {
            get;
            set;
        }
        /// <summary>
        /// 主动天赋列表
        /// </summary>
        [DataMember]
        [ProtoMember(15)]
        public string[] SubSkills
        {
            get;
            set;
        }
        public List<string>[] ManagerSkills
        {
            get
            {
                //TODO
                //return new List<string>[] { ReadySkillList, LiveSkillList, null };
                return new List<string>[] { ReadySkillList, LiveSkillList, FirmSkillList };
            }
        }

        #region Buff
        [DataMember]
        [ProtoMember(111)]
        public List<MatchPropInput> MatchPropList
        {
            get;
            set;
        }
        [DataMember]
        [ProtoMember(112)]
        public List<MatchBoostInput> MatchBoostList
        {
            get;
            set;
        }
        #endregion


        [DataMember]
        [ProtoMember(201)]
        public int TalentType
        {
            get;
            set;
        }


    }
    #endregion
}

