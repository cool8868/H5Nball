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
    public class ManagerSkillCache
    {
        #region Config
        const char SPLITWillPid = ',';
        const char SPLITWillStrength = '+';
        #endregion

        #region Cache
        static readonly Dictionary<string, DicManagertalentEntity> s_dicTalent = new Dictionary<string, DicManagertalentEntity>();
        static readonly Dictionary<int, List<string>> s_dicTalentStep = new Dictionary<int, List<string>>();
        static readonly SortedDictionary<int, int> s_dicTalentPoint = new SortedDictionary<int, int>();
        static readonly Dictionary<string, DicManagerwillEntity> s_dicWill = new Dictionary<string, DicManagerwillEntity>();
        static readonly Dictionary<int, Dictionary<string, int>> s_dicCombIndex = new Dictionary<int, Dictionary<string, int>>();
        static readonly Dictionary<int, string> s_dicAntiTalents = new Dictionary<int, string>();
        static readonly Dictionary<string, DicClubskillEntity> s_dicClubSkill = new Dictionary<string, DicClubskillEntity>();
      
        /// <summary>
        /// itemcode->1
        /// </summary>
        static readonly Dictionary<int, int> _willItemcodes = new Dictionary<int, int>();

        private static readonly List<int> _willCodeList = new List<int>();

        public readonly int CountHighWill;
        public readonly int CountLowWill;
        #endregion

        #region Singleton
        static readonly object s_lockObj = new object();
        static volatile ManagerSkillCache s_instnce = null;
        public readonly bool InitFlag = false;
        public static ManagerSkillCache Instance()
        {
            if (null == s_instnce || !s_instnce.InitFlag)
            {
                lock (s_lockObj)
                {
                    if (null == s_instnce || !s_instnce.InitFlag)
                    {
                        s_instnce = new ManagerSkillCache();
                    }
                }
            }
            return s_instnce;
        }
        #endregion

        #region .ctor
        private ManagerSkillCache()
        {
            try
            {
                s_dicTalent.Clear();
                s_dicTalentStep.Clear();
                s_dicTalentPoint.Clear();
                s_dicWill.Clear();
                s_dicCombIndex.Clear();
                s_dicClubSkill.Clear();
                s_dicAntiTalents.Clear();
                s_dicAntiTalents[204] = "MT204";
                s_dicAntiTalents[302] = "MT302";
                s_dicAntiTalents[403] = "MT403";
                var talents = DicManagertalentMgr.GetAllForCache();
                var wills = DicManagerwillMgr.GetAllForCache();
                foreach (var item in talents)
                {
                    s_dicTalent[item.SkillCode] = item;
                    if (!s_dicTalentStep.ContainsKey(item.StepNo))
                        s_dicTalentStep[item.StepNo] = new List<string>();
                    s_dicTalentStep[item.StepNo].Add(item.SkillCode);
                    if (!s_dicTalentPoint.ContainsKey(item.ReqManagerLevel))
                        s_dicTalentPoint[item.ReqManagerLevel] = s_dicTalentPoint.Count + 1;
                }
                string[] arr;
                Dictionary<int, int> dic = null;
                int cnt = 0;
                int pid = 0;
                int cntLow = 0;
                int cntHigh = 0;
                foreach (var item in wills)
                {
                    dic = new Dictionary<int, int>();
                    arr = item.PartMap.Trim(SPLITWillPid).Split(SPLITWillPid, SPLITWillStrength);
                    for (int i = 0; i < arr.Length / 2; i++)
                    {
                        dic[Convert.ToInt32(arr[2 * i])] = Convert.ToInt32(arr[2 * i + 1]);
                    }
                    item.DicPid = dic;
                    s_dicWill[item.SkillCode] = item;
                    if (item.WillRank > 1)
                        cntHigh++;
                    else
                        cntLow++;
                    if (string.IsNullOrEmpty(item.CombSkillCode))
                        continue;
                    cnt = dic.Count;
                    foreach (var kvp in dic)
                    {
                        pid = CastPlayerId(kvp.Key);
                        if (!s_dicCombIndex.ContainsKey(pid))
                            s_dicCombIndex[pid] = new Dictionary<string, int>();
                        s_dicCombIndex[pid][item.CombSkillCode] = cnt;
                    }
                }
            
                _willItemcodes.Clear();
                var list = DicManagerwillparttipsMgr.GetWillItemcodes();
                foreach (var data in list)
                {
                    if (!_willItemcodes.ContainsKey(data.ItemCode))
                    {
                        _willItemcodes.Add(data.ItemCode, 1);
                        _willCodeList.Add(data.ItemCode);
                    }
                }
                this.CountHighWill = cntHigh;
                this.CountLowWill = cntLow;

                var clubSkills = DicClubskillMgr.GetAll();
                foreach (var item in clubSkills)
                {
                    s_dicClubSkill[item.SkillKey] = item;
                }
                this.InitFlag = true;
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex, "ManagerSkillCache:Init");
                this.InitFlag = false;
            }
        }
        #endregion

        #region Retrieve
        public IEnumerable<DicManagerwillEntity> GetWillList()
        {
            return s_dicWill.Values;
        }
        /// <summary>
        /// 获取天赋配置
        /// </summary>
        /// <param name="skillCode"></param>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public bool TryGetTalent(string skillCode, out DicManagertalentEntity cfg)
        {
            s_dicTalent.TryGetValue(skillCode, out cfg);
            return null != cfg;
        }
        public bool TryGetWill(string skillCode, out DicManagerwillEntity cfg)
        {
            s_dicWill.TryGetValue(skillCode, out cfg);
            return null != cfg;
        }
        /// <summary>
        /// 获取这阶段的天赋
        /// </summary>
        /// <param name="stepNo"></param>
        /// <param name="skills"></param>
        /// <returns></returns>
        public bool TryGetStepTalents(int stepNo, out List<string> skills)
        {
            s_dicTalentStep.TryGetValue(stepNo, out skills);
            return null != skills;
        }
        public bool CheckTalentPoint(int managerLevel, out int skillPoint, out int nextLevel)
        {
            skillPoint = nextLevel = 0;
            foreach (var kvp in s_dicTalentPoint)
            {
                if (managerLevel < kvp.Key)
                {
                    nextLevel = kvp.Key;
                    return true;
                }
                skillPoint = kvp.Value;
            }
            nextLevel = 999;
            return true;
        }
        public List<string> CheckCombs(ICollection<string> wills, int[] pIds)
        {
            if (null == wills || wills.Count == 0)
                return null;
            var lst = CheckCombs(pIds);
            if (null == lst || lst.Count == 0)
                return null;
            DicManagerwillEntity cfg = null;
            var dic = new Dictionary<string, int>();
            foreach (var code in wills)
            {
                if (!TryGetWill(code, out cfg) || string.IsNullOrEmpty(cfg.CombSkillCode))
                    continue;
                dic[cfg.CombSkillCode] = 0;
            }
            for (int i = lst.Count - 1; i >= 0; i--)
            {
                if (!dic.ContainsKey(lst[i]))
                    lst.RemoveAt(i);
            }
            return lst;
        }
        public int GetCombsNum(int[] pIds)
        {
            var combs = CheckCombs(pIds);
            if (null == combs)
                return 0;
            return combs.Count;
        }
        public List<string> CheckCombs(int[] pIds)
        {
            if (null == pIds || pIds.Length == 0)
                return null;
            var dic = new Dictionary<string, int>(16);
            var lst = new List<string>(16);
            foreach (var p in pIds)
            {
                if (!s_dicCombIndex.ContainsKey(p))
                    continue;
                foreach (var c in s_dicCombIndex[p])
                {
                    if (dic.ContainsKey(c.Key))
                        dic[c.Key]--;
                    else
                        dic[c.Key] = c.Value - 1;
                }
            }
            foreach (var kvp in dic)
            {
                if (kvp.Value <= 0)
                    lst.Add(kvp.Key);
            }
            dic.Clear();
            return lst;
        }
        public List<string> CheckClubSkills(int[] pIds)
        {
            if (null == pIds || pIds.Length == 0)
                return null;
            var dic = new Dictionary<string, int>(16);
            var lst = new List<string>(8);
            DicPlayerEntity cfg = null;
            int skillVal;
            foreach (int pid in pIds)
            {
                cfg = PlayersdicCache.Instance.GetPlayer(pid);
                if (null == cfg)
                    continue;
                if (dic.ContainsKey(cfg.Club))
                    dic[cfg.Club]--;
                else
                {
                    if (TryGetClubSkillValue(out skillVal, cfg.Club))
                        dic[cfg.Club] = skillVal - 1;
                }
                if (dic.ContainsKey(cfg.Nationality))
                    dic[cfg.Nationality]--;
                else
                {
                    if (TryGetClubSkillValue(out skillVal, cfg.Nationality))
                        dic[cfg.Nationality] = skillVal - 1;
                }
            }
            string skillCode;
            foreach (var kvp in dic)
            {
                if (kvp.Value > 0)
                    continue;
                if (!TryGetClubSkill(out skillCode, kvp.Key))
                    continue;
                lst.Add(skillCode);
            }
            dic.Clear();
            return lst;
        }

        public bool CheckIsWillItem(int itemcode)
        {
            return _willItemcodes.ContainsKey(itemcode);
        }

        public int GetWillItem()
        {
            int count = _willCodeList.Count;
            int randomIdx = RandomHelper.GetInt32(0, count - 1);
            var itemCode = _willCodeList[randomIdx];
            return itemCode;
        }
        public bool TryGetAntiTanlent(out string skill, int srcType, int dstType)
        {
            int key = srcType * 100 + dstType;
            return s_dicAntiTalents.TryGetValue(key, out skill);
        }
        public bool TryGetClubSkillValue(out int value, string key)
        {
            value = 0;
            DicClubskillEntity obj;
            if (!s_dicClubSkill.TryGetValue(key, out obj))
                return false;
            value= obj.SkillValue;
            return true;
        }
        public bool TryGetClubSkill(out string skill, string key)
        {
            skill = string.Empty;
            DicClubskillEntity obj;
            if (!s_dicClubSkill.TryGetValue(key, out obj))
                return false;
            skill = obj.SkillCode;
            return true;
        }
        #endregion

        #region Tools
        int CastPlayerId(int itemCode)
        {
            return itemCode % 100000;
        }
        #endregion
    }
}
