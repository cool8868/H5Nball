using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response;
using Games.NBall.Bll;
using Games.NBall.Bll.Frame;

namespace Games.NBall.Bll.Cache
{
    public class BuffCache
    {
        #region Config
        public const char SPLITValues = ',';
        public const char SPLITSkillArgs = '.';
        public const char SPLITVarySkillArgs = '_';
        public const string WILDChar4LiveSkill = "!";
        public const string WILDChar4VarySkill = "#";
        const int SPLITPropIndex = 100;
        public static readonly EnumBuffCode[] BUFFCodes4PlayerProp = new EnumBuffCode[]{
            EnumBuffCode.PlayerSpeed, EnumBuffCode.PlayerShoot, EnumBuffCode.PlayerFreeKick,
            EnumBuffCode.PlayerBalance, EnumBuffCode.PlayerStamina, EnumBuffCode.PlayerStrength,
            EnumBuffCode.PlayerAggression, EnumBuffCode.PlayerDisturb, EnumBuffCode.PlayerInterception,
            EnumBuffCode.PlayerDribble, EnumBuffCode.PlayerPass, EnumBuffCode.PlayerMentality,
            EnumBuffCode.PlayerResponse, EnumBuffCode.PlayerPositioning, EnumBuffCode.PlayerHandControl,
            EnumBuffCode.PlayerAcceleration};
        public static readonly EnumBuffCode[] BUFFCodes4ManagerShow = new EnumBuffCode[] {
            EnumBuffCode.TeamCloth1,EnumBuffCode.TeamCloth2,EnumBuffCode.TeamCloth3,EnumBuffCode.TeamCloth4,EnumBuffCode.TeamCloth5,
            EnumBuffCode.TeamCloth6,EnumBuffCode.TeamCloth7,EnumBuffCode.TeamCloth8,EnumBuffCode.TeamCloth9,EnumBuffCode.TeamCloth10,
            EnumBuffCode.TeamCloth11,EnumBuffCode.TeamCloth12,EnumBuffCode.TeamCloth13,EnumBuffCode.TeamCloth14,EnumBuffCode.TeamCloth15,
            EnumBuffCode.TeamCloth16,EnumBuffCode.TeamCloth17,EnumBuffCode.TeamCloth18,EnumBuffCode.TeamCloth19,EnumBuffCode.TeamCloth20,
            EnumBuffCode.TeamCloth21,EnumBuffCode.TeamCloth22,EnumBuffCode.TeamCloth23,EnumBuffCode.TeamCloth24,EnumBuffCode.TeamCloth25,
            EnumBuffCode.TeamCloth26,EnumBuffCode.TeamCloth27,EnumBuffCode.TeamCloth28,EnumBuffCode.TeamClubBuff,
            EnumBuffCode.PkMatchExp,EnumBuffCode.TourCoin,EnumBuffCode.WorldChallengeExp,EnumBuffCode.WorldChallengeCoin,
            EnumBuffCode.DestroyCardCritRate,EnumBuffCode.TrainExpPlusRate,
            EnumBuffCode.GuildBodyProp,EnumBuffCode.GuildDefenceProp,EnumBuffCode.GuildGoalKeepProp,EnumBuffCode.GuildOrganizeProp,EnumBuffCode.GuildAttackProp,
            EnumBuffCode.GuildAnti1,EnumBuffCode.GuildAnti2,EnumBuffCode.GuildAnti3};
        static readonly DateTime DATEInfi = new DateTime(2050, 1, 1);
        #endregion

        #region Cache
        static readonly Dictionary<int, int> s_dicBuffIdx4PlayerProp = new Dictionary<int, int>();
        static readonly Dictionary<int, int> s_dicBuffIdx4ManagerShow = new Dictionary<int, int>();
        static readonly Dictionary<int, DicBuffEntity> s_dicBuff = new Dictionary<int, DicBuffEntity>();
        static readonly Dictionary<int, int[]> s_dicBaseBuff = new Dictionary<int, int[]>();
        static readonly Dictionary<string, DicSkillEntity> s_dicSkill = new Dictionary<string, DicSkillEntity>();
        static readonly Dictionary<string, string> s_dicSkillRef = new Dictionary<string, string>();
        static readonly Dictionary<string, List<ConfigBuffpoolEntity>> s_dicPoolIncBuff = new Dictionary<string, List<ConfigBuffpoolEntity>>();
        static readonly Dictionary<string, List<ExcPoolBuffItem>> s_dicPoolExcBuff = new Dictionary<string, List<ExcPoolBuffItem>>();
        static readonly Dictionary<string, List<ConfigBuffengineEntity>> s_dicFirmBuff = new Dictionary<string, List<ConfigBuffengineEntity>>();
        static readonly Dictionary<string, List<ConfigBuffengineEntity>> s_dicReadyBuff = new Dictionary<string, List<ConfigBuffengineEntity>>();
        #endregion

        #region Singleton
        static readonly object s_lockObj = new object();
        static volatile BuffCache s_instnce = null;
        public readonly bool InitFlag = false;
        public static BuffCache Instance()
        {
            if (null == s_instnce || !s_instnce.InitFlag)
            {
                lock (s_lockObj)
                {
                    if (null == s_instnce || !s_instnce.InitFlag)
                    {
                        s_instnce = new BuffCache();
                    }
                }
            }
            return s_instnce;
        }
        #endregion

        #region .ctor
        private BuffCache()
        {
            try
            {
                s_dicBuffIdx4PlayerProp.Clear();
                s_dicBuffIdx4ManagerShow.Clear();
                s_dicBuff.Clear();
                s_dicBaseBuff.Clear();
                s_dicSkill.Clear();
                s_dicSkillRef.Clear();
                s_dicPoolIncBuff.Clear();
                s_dicPoolExcBuff.Clear();
                s_dicFirmBuff.Clear();
                s_dicReadyBuff.Clear();
                var buffs = DicBuffMgr.GetAllForCache();
                var skills = DicSkillMgr.GetAll();
                var pools = ConfigBuffpoolMgr.GetAll();
                var flows = ConfigBuffengineMgr.GetAll();
                for (int i = 0; i < BUFFCodes4PlayerProp.Length; ++i)
                {
                    s_dicBuffIdx4PlayerProp[(int)BUFFCodes4PlayerProp[i]] = i;
                }
                for (int i = 0; i < BUFFCodes4ManagerShow.Length; ++i)
                {
                    s_dicBuffIdx4ManagerShow[(int)BUFFCodes4ManagerShow[i]] = i;
                }
                foreach (var item in buffs)
                {
                    s_dicBuff[item.BuffId] = item;
                    if (string.IsNullOrEmpty(item.BaseBuffMap))
                        s_dicBaseBuff[item.BuffId] = new int[] { item.BuffId };
                    else
                        s_dicBaseBuff[item.BuffId] = FrameUtil.CastIntArray(item.BaseBuffMap, SPLITValues);
                }
                string skillKey, skillRefKey;
                skillKey = skillRefKey = string.Empty;
                foreach (var item in skills)
                {
                    item.AsLiveFlag = item.LiveFlag > 0 ? EnumSkillLiveFlag.Live : EnumSkillLiveFlag.None;
                    skillKey = CastSkillKey(item.SkillCode, item.SkillLevel);
                    skillRefKey = CastSkillRefKey(item.RefType, item.RefKey, item.RefFlag);
                    s_dicSkill[skillKey] = item;
                    if (!string.IsNullOrEmpty(skillRefKey))
                        s_dicSkillRef[skillRefKey] = skillKey;
                }
                DicSkillEntity aObj = null;
                foreach (var item in pools)
                {
                    skillKey = CastSkillKey(item.SkillCode, item.SkillLevel);
                    if (!s_dicSkill.TryGetValue(skillKey, out aObj))
                    {
                        aObj = new DicSkillEntity
                        {
                            SkillCode = item.SkillCode,
                            SkillLevel = item.SkillLevel,
                            BuffSrcType = item.BuffSrcType,
                        };
                        s_dicSkill[skillKey] = aObj;
                    }
                    aObj.PoolFlag = 1;
                    aObj.AsLiveFlag |= item.LiveFlag == 0 ? EnumSkillLiveFlag.Firm : EnumSkillLiveFlag.Ready;
                    item.BaseBuffList = GetBaseBuffArray(item.BuffMap);
                    item.PropIndexList = GetPropIndexArray(item.BaseBuffList);
                    item.AsBuffUnitType = GetBuffUnitType(item.BaseBuffList);
                    if (!s_dicPoolIncBuff.ContainsKey(skillKey))
                        s_dicPoolIncBuff[skillKey] = new List<ConfigBuffpoolEntity>();
                    s_dicPoolIncBuff[skillKey].Add(item);
                }
                GenPoolExcBuff(pools);
                foreach (var item in flows)
                {
                    skillKey = CastSkillKey(item.SkillCode, item.SkillLevel);
                    if (!s_dicSkill.TryGetValue(skillKey, out aObj))
                    {
                        aObj = new DicSkillEntity
                        {
                            SkillCode = item.SkillCode,
                            SkillLevel = item.SkillLevel,
                            BuffSrcType = item.BuffSrcType,
                        };
                        s_dicSkill[skillKey] = aObj;
                    }
                    aObj.AsLiveFlag |= item.LiveFlag == 0 ? EnumSkillLiveFlag.Firm : EnumSkillLiveFlag.Ready;
                    item.BaseBuffList = GetBaseBuffArray(item.BuffMap);
                    item.PropIndexList = GetPropIndexArray(item.BaseBuffList);
                    item.AsBuffUnitType = GetBuffUnitType(item.BaseBuffList);
                    if (item.LiveFlag == 0)
                    {
                        if (!s_dicFirmBuff.ContainsKey(skillKey))
                            s_dicFirmBuff[skillKey] = new List<ConfigBuffengineEntity>();
                        s_dicFirmBuff[skillKey].Add(item);
                    }
                    else
                    {
                        if (!s_dicReadyBuff.ContainsKey(skillKey))
                            s_dicReadyBuff[skillKey] = new List<ConfigBuffengineEntity>();
                        s_dicReadyBuff[skillKey].Add(item);
                    }
                }
                this.InitFlag = true;
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex, "BuffCache:Init");
                this.InitFlag = false;
            }
        }
        void GenPoolExcBuff(List<ConfigBuffpoolEntity> pools)
        {
            string skillCode, lvStr, skillKey;
            skillCode = lvStr = skillKey = string.Empty;
            int skillLv = 0;
            string[] aVals = null;
            Dictionary<int, int> aDic = null;
            Dictionary<int, string> aDic2 = null;
            List<ExcPoolBuffItem> aList = null;
            Dictionary<string, Dictionary<int, int>> dicLv = new Dictionary<string, Dictionary<int, int>>();
            Dictionary<string, Dictionary<int, string>> dicBuff = new Dictionary<string, Dictionary<int, string>>();
            foreach (var item in pools)
            {
                skillCode = item.SkillCode;
                skillLv = item.SkillLevel;
                if (!dicLv.TryGetValue(skillCode, out aDic))
                {
                    aDic = new Dictionary<int, int>();
                    dicLv[skillCode] = aDic;
                }
                aDic[skillLv] = 0;
                if (!dicBuff.TryGetValue(skillCode, out aDic2))
                {
                    aDic2 = new Dictionary<int, string>();
                    dicBuff[skillCode] = aDic2;
                }
                if (!aDic2.TryGetValue(item.BuffNo, out lvStr))
                    aDic2[item.BuffNo] = CastLevelStr(item.SkillLevel);
                else
                    aDic2[item.BuffNo] = string.Concat(lvStr, CastLevelStr(item.SkillLevel));
                if (string.IsNullOrEmpty(item.CoverSkillCode))
                    continue;
                aVals = item.CoverSkillCode.Split(new char[] { SPLITValues }, StringSplitOptions.RemoveEmptyEntries);
                skillKey = CastSkillKey(skillCode, skillLv);
                if (!s_dicPoolExcBuff.TryGetValue(skillKey, out aList))
                {
                    aList = new List<ExcPoolBuffItem>();
                    s_dicPoolExcBuff[skillKey] = aList;
                }
                foreach (string val in aVals)
                {
                    aList.Add(new ExcPoolBuffItem()
                    {
                        SkillCode = val,
                        BuffNo = item.BuffNo,
                    });
                }
            }
            foreach (var kvp in dicLv)
            {
                skillCode = kvp.Key;
                if (!dicBuff.TryGetValue(skillCode, out aDic2)
                    || aDic2.Count == 0)
                    continue;
                foreach (var kvp2 in kvp.Value)
                {
                    skillLv = kvp2.Key;
                    skillKey = CastSkillKey(skillCode, skillLv);
                    if (!s_dicPoolExcBuff.TryGetValue(skillKey, out aList))
                    {
                        aList = new List<ExcPoolBuffItem>();
                        s_dicPoolExcBuff[skillKey] = aList;
                    }
                    foreach (var kvp3 in aDic2)
                    {
                        if (!CheckLevelStr(kvp3.Value, skillLv))
                        {
                            aList.Add(new ExcPoolBuffItem()
                            {
                                SkillCode = skillCode,
                                BuffNo = kvp3.Key,
                            });
                        }
                    }
                }
            }
            dicLv.Clear();
            dicBuff.Clear();
        }
        #endregion

        #region Retrieve
        public bool TryGetBuff(out DicBuffEntity cfg, int buffId)
        {
            s_dicBuff.TryGetValue(buffId, out cfg);
            return null != cfg;
        }
        public bool TryGetSkill(out DicSkillEntity cfg, string skillCode, int skillLevel = 0)
        {
            s_dicSkill.TryGetValue(CastSkillKey(FixSkillKey(skillCode), skillLevel), out cfg);
            return null != cfg;
        }
        public string GetSkillName(string skillCode, int skillLevel = 0)
        {
            DicSkillEntity cfg;
            if (skillLevel > 0)
            {
                if (TryGetSkill(out cfg, skillCode, skillLevel) && !string.IsNullOrEmpty(cfg.SkillName))
                    return cfg.SkillName;
            }
            if (!TryGetSkill(out cfg, skillCode, 0) || string.IsNullOrEmpty(cfg.SkillName))
                return string.Empty;
            return cfg.SkillName;
        }
        public string GetSkillByRef(EnumSkillRefType refType, string refKey, string refFlag = "")
        {
            string skillCode;
            s_dicSkillRef.TryGetValue(CastSkillRefKey(refType.ToString(), refKey, refFlag), out skillCode);
            return skillCode ?? string.Empty;
        }
        public EnumBuffUnitType GetBuffUnitType(int[] buffIds)
        {
            EnumBuffUnitType val = EnumBuffUnitType.None;
            foreach (int buffId in buffIds)
            {
                if (s_dicBuffIdx4PlayerProp.ContainsKey(buffId))
                    val |= EnumBuffUnitType.PlayerProp;
                if (s_dicBuffIdx4ManagerShow.ContainsKey(buffId))
                    val |= EnumBuffUnitType.ManagerShow;
            }
            return val;
        }
        public int[] GetBaseBuffArray(string buffMap)
        {
            return GetBaseBuffList(buffMap).ToArray();
        }
        public List<int> GetBaseBuffList(string buffMap)
        {
            var list = new List<int>();
            if (string.IsNullOrEmpty(buffMap))
                return list;
            var aVals = FrameUtil.CastIntArray(buffMap, SPLITValues);
            int[] buffs = null;
            foreach (int val in aVals)
            {
                buffs = CastBaseBuffId(val);
                if (null == buffs)
                    continue;
                list.AddRange(buffs);
            }
            return list;
        }
        public int[] CastBaseBuffId(int buffId)
        {
            int[] buffIds = null;
            if (buffId >= 0 && buffId < BUFFCodes4PlayerProp.Length)
                buffId = (int)BUFFCodes4PlayerProp[buffId];
            if (buffId >= SPLITPropIndex)
                s_dicBaseBuff.TryGetValue(buffId, out buffIds);
            return buffIds;
        }
        public int[] GetPropIndexArray(string buffMap)
        {
            int[] buffIds = null;
            if (!string.IsNullOrEmpty(buffMap))
                buffIds = FrameUtil.CastIntArray(buffMap, SPLITValues);
            return GetPropIndexArray(buffIds);
        }
        public int[] GetPropIndexArray(int[] buffIds)
        {
            if (null == buffIds)
                return null;
            var list = new List<int>(buffIds.Length);
            int idx = 0;
            foreach (int buffId in buffIds)
            {
                idx = CastPropIndex(buffId);
                if (idx >= 0)
                    list.Add(idx);
            }
            return list.ToArray();
        }
        public int CastPropIndex(int buffId)
        {
            if (buffId >= 0 && buffId < BUFFCodes4PlayerProp.Length)
                return buffId;
            if (buffId < SPLITPropIndex)
                return -1;
            int val;
            if (s_dicBuffIdx4PlayerProp.TryGetValue(buffId, out val))
                return val;
            return -1;
        }
        public string GetVarySkillCode(int buffId, double point, double percent)
        {
            return string.Format("{0}{1}{2}{3}{4}{5}", WILDChar4VarySkill, buffId, SPLITSkillArgs, percent, SPLITVarySkillArgs, point);
        }
        public List<string>[] GetRankedSkillList(List<string> skills)
        {
            var rankedSkills = new List<string>[3];
            FillRankedSkillList(skills, rankedSkills);
            return rankedSkills;
        }
        public void FillRankedSkillList(List<string> skills, List<string>[] rankedSkills)
        {
            if (null == rankedSkills || rankedSkills.Length < 3)
                return;
            for (int i = 0; i < 3; ++i)
            {
                if (null == rankedSkills[i])
                    rankedSkills[i] = new List<string>();
            }
            DicSkillEntity cfg;
            foreach (string skill in skills)
            {
                if (skill.StartsWith(WILDChar4LiveSkill))
                {
                    rankedSkills[1].Add(skill.TrimStart(WILDChar4LiveSkill.ToCharArray()));
                    continue;
                }
                if (!TryGetSkill(out cfg, skill))
                    continue;
                if ((cfg.AsLiveFlag & EnumSkillLiveFlag.Ready) > 0)
                    rankedSkills[0].Add(skill);
                if ((cfg.AsLiveFlag & EnumSkillLiveFlag.Live) > 0)
                    rankedSkills[1].Add(skill);
                if ((cfg.AsLiveFlag & EnumSkillLiveFlag.Firm) > 0)
                    rankedSkills[2].Add(skill);
            }
        }
        public List<ConfigBuffpoolEntity> GetPoolIncBuffList(string skillCode, int skillLevel = 0)
        {
            string skillKey = CastSkillKey(skillCode, skillLevel);
            List<ConfigBuffpoolEntity> obj;
            s_dicPoolIncBuff.TryGetValue(skillKey, out obj);
            return obj;
        }
        public List<ExcPoolBuffItem> GetPoolExcBuffList(string skillCode, int skillLevel = 0)
        {
            string skillKey = CastSkillKey(skillCode, skillLevel);
            List<ExcPoolBuffItem> obj;
            s_dicPoolExcBuff.TryGetValue(skillKey, out obj);
            return obj;
        }

        public List<NbManagerbuffpoolEntity> GenManagerPoolList(string skillCode, int skillLevel = 0, string srcId = "")
        {
            var cfgs = GetPoolIncBuffList(skillCode, skillLevel);
            if (null == cfgs)
                return null;
            var rst = new List<NbManagerbuffpoolEntity>(cfgs.Count);
            foreach (var item in cfgs)
            {
                rst.Add(CreateManagerPool(item, srcId));
            }
            return rst;
        }
        public List<ConfigBuffengineEntity> GetFirmBuffList(string skillCode, int skillLevel = 0)
        {
            return GetFlowBuffList(false, skillCode, skillLevel);
        }
        public List<ConfigBuffengineEntity> GetReadyBuffList(string skillCode, int skillLevel = 0)
        {
            return GetFlowBuffList(true, skillCode, skillLevel);
        }
        public Dictionary<string, List<ConfigBuffengineEntity>> GetFirmBuffList(List<string> skills)
        {
            return GetFlowBuffList(false, skills);
        }
        public Dictionary<string, List<ConfigBuffengineEntity>> GetReadyBuffList(List<string> skills)
        {
            return GetFlowBuffList(true, skills);
        }
        List<ConfigBuffengineEntity> GetFlowBuffList(bool liveFlag, string skillCode, int skillLevel = 0)
        {
            string skillKey = FixSkillKey(CastSkillKey(skillCode, skillLevel));
            List<ConfigBuffengineEntity> obj;
            var dic = liveFlag ? s_dicReadyBuff : s_dicFirmBuff;
            dic.TryGetValue(skillKey, out obj);
            if (null == obj || !skillCode.StartsWith(WILDChar4VarySkill))
                return obj;
            decimal point = 0;
            decimal percent = 0;
            var splits = skillCode.Substring(skillKey.Length + 1).Split(SPLITVarySkillArgs);
            if (splits.Length >= 1)
                decimal.TryParse(splits[0], out percent);
            if (splits.Length >= 2)
                decimal.TryParse(splits[1], out point);
            var rst = new List<ConfigBuffengineEntity>(obj.Count);
            foreach (var item in obj)
            {
                rst.Add(CreateNewFlow(item, point, percent));
            }
            return rst;
        }
        Dictionary<string, List<ConfigBuffengineEntity>> GetFlowBuffList(bool liveFlag, List<string> skills)
        {
            var dic = new Dictionary<string, List<ConfigBuffengineEntity>>();
            if (null == skills || skills.Count == 0)
                return dic;
            List<ConfigBuffengineEntity> obj = null;
            int nonce = 0;
            foreach (string skill in skills)
            {
                obj = GetFlowBuffList(liveFlag, skill);
                if (null == obj)
                    continue;
                if (!dic.ContainsKey(skill))
                    dic[skill] = obj;
                else
                    dic[string.Concat(skill, "*", ++nonce)] = obj;
            }
            return dic;
        }
        NbManagerbuffpoolEntity CreateManagerPool(ConfigBuffpoolEntity src, string srcId = "")
        {
            var obj = new NbManagerbuffpoolEntity()
            {
                ManagerId = Guid.Empty,
                SkillCode = src.SkillCode,
                SkillLevel = src.SkillLevel,
                BuffSrcType = src.BuffSrcType,
                BuffSrcId = srcId,
                BuffUnitType = src.BuffUnitType,
                LiveFlag = src.LiveFlag,
                BuffNo = src.BuffNo,
                DstDir = src.DstDir,
                DstMode = src.DstMode,
                DstKey = src.DstKey,
                BuffMap = src.BuffMap,
                BuffVal = src.BuffVal,
                BuffPer = src.BuffPer,
            };
            if (src.ExpiryMinutes <= 0)
                obj.ExpiryTime = DATEInfi;
            else
                obj.ExpiryTime = DateTime.Now.AddMinutes(src.ExpiryMinutes);
            return obj;
        }
        ConfigBuffengineEntity CreateNewFlow(ConfigBuffengineEntity src, decimal point, decimal percent)
        {
            return new ConfigBuffengineEntity()
            {
                Id = src.Id,
                SkillCode = src.SkillCode,
                SkillLevel = src.SkillLevel,
                BuffSrcType = src.BuffSrcType,
                BuffUnitType = src.BuffUnitType,
                LiveFlag = src.LiveFlag,
                CheckMode = src.CheckMode,
                CheckKey = src.CheckKey,
                CalcMode = src.CalcMode,
                SrcDir = src.SrcDir,
                SrcMode = src.SrcMode,
                SrcKey = src.SrcKey,
                DstDir = src.DstDir,
                DstMode = src.DstMode,
                DstKey = src.DstKey,
                BuffMap = src.BuffMap,
                BuffVal = point,
                BuffPer = percent,
                BuffArg = src.BuffArg,
                PropIndexList = src.PropIndexList,
                AsBuffUnitType = src.AsBuffUnitType
            };
        }
        #endregion

        #region Native
        string CastLevelStr(int skillLevel)
        {
            return string.Concat("^", skillLevel);
        }
        bool CheckLevelStr(string lvStr, int skillLevel)
        {
            return lvStr.IndexOf(string.Concat("^", skillLevel)) >= 0;
        }
        string FixSkillKey(string skillKey)
        {
            return skillKey.Split(SPLITSkillArgs)[0];
        }
        public string CastSkillKey(string skillCode, int skillLevel)
        {
            if (skillLevel <= 0)
                return skillCode;
            return string.Concat(skillCode, "_", skillLevel);
        }
        string CastSkillRefKey(string refType, string refKey, string refFlag)
        {
            return string.Format("{0}{1}{2}", refType ?? string.Empty, refKey ?? string.Empty, refFlag ?? string.Empty);
        }
        #endregion
    }
}
