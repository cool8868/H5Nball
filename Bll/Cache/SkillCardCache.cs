using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Bll;
using Games.NBall.Bll.Frame;

namespace Games.NBall.Bll.Cache
{
    public class SkillCardCache
    {
        #region Config
        const char SPLITSect = '_';
        const char SPLITUnit = ',';
        public const int MINAskRank = 1;
        public const int MAXAskRank = 5;
        public const int MINAskNpc = 1;
        public const int MAXAskNPc = 5;
        #endregion

        #region Cache
        static readonly Dictionary<int, Dictionary<int, ConfigSkillcardlevelEntity>> s_dicSkillExp = new Dictionary<int, Dictionary<int, ConfigSkillcardlevelEntity>>();
        static readonly Dictionary<int, ConfigSkillcardaskrankEntity> s_dicAskRank = new Dictionary<int, ConfigSkillcardaskrankEntity>();
        static readonly Dictionary<int, RandomPicker> s_dicAskRand = new Dictionary<int, RandomPicker>();
        static readonly Dictionary<string, DicSkillcardEntity> s_dicSkillCard = new Dictionary<string, DicSkillcardEntity>();
        static readonly Dictionary<int, List<string>> s_dicSkillLib = new Dictionary<int, List<string>>();
        static readonly Dictionary<string, string> s_dicSkillRoot = new Dictionary<string, string>();
        #endregion

        #region Singleton
        static readonly object s_lockObj = new object();
        static volatile SkillCardCache s_instnce = null;
        public readonly bool InitFlag = false;
        public static SkillCardCache Instance()
        {
            if (null == s_instnce || !s_instnce.InitFlag)
            {
                lock (s_lockObj)
                {
                    if (null == s_instnce || !s_instnce.InitFlag)
                    {
                        s_instnce = new SkillCardCache();
                    }
                }
            }
            return s_instnce;
        }
        #endregion

        #region .ctor
        private SkillCardCache()
        {
            try
            {
                s_dicSkillExp.Clear();
                s_dicAskRank.Clear();
                s_dicAskRand.Clear();
                s_dicSkillCard.Clear();
                s_dicSkillLib.Clear();
                s_dicSkillRoot.Clear();
                var levels = ConfigSkillcardlevelMgr.GetAll();
                var asks = ConfigSkillcardaskrankMgr.GetAll();
                var skills = DicSkillcardMgr.GetAllForCache();
                foreach (var g in levels.GroupBy(i => i.SkillClass))
                {
                    s_dicSkillExp[g.Key] = g.ToDictionary(i => i.SkillLevel, i => i);
                }
                foreach (var item in asks)
                {
                    s_dicAskRank[item.NpcId] = item;
                    s_dicAskRand[item.NpcId] = new RandomPicker(2, FrameUtil.CastIntArray(item.SkillRateMap, SPLITSect, SPLITUnit));
                }
                int libKey = 0;
                foreach (var item in skills)
                {
                    s_dicSkillCard[item.SkillCode] = item;
                    s_dicSkillRoot[CastRootKey(item.SkillRoot, item.SkillLevel)] = item.SkillCode;
                    libKey = CastLibKey(item.SkillClass, item.SkillLevel);
                    if (!s_dicSkillLib.ContainsKey(libKey))
                        s_dicSkillLib[libKey] = new List<string>();
                    s_dicSkillLib[libKey].Add(item.SkillCode);
                }
                this.InitFlag = true;
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex, "SkillCardCache:Init");
                this.InitFlag = false;
            }
        }
        #endregion

        #region Retrieve
        public bool TryGetSkillLevel(int skillClass, int skillLevel, out  ConfigSkillcardlevelEntity cfg)
        {
            cfg = null;
            Dictionary<int, ConfigSkillcardlevelEntity> cfgs = null;
            if (!s_dicSkillExp.TryGetValue(skillClass, out cfgs)
                || !cfgs.TryGetValue(skillLevel, out cfg))
                return false;
            return null != cfg;
        }
        public bool TryCheckSkillLevel(int skillClass, int exp, out int skillLevel)
        {
            skillLevel = 0;
            Dictionary<int, ConfigSkillcardlevelEntity> cfgs;
            if (!s_dicSkillExp.TryGetValue(skillClass, out cfgs) || cfgs.Count == 0)
                return false;
            var cfg = cfgs.Values.FirstOrDefault(i => i.MinExp <= exp && exp < i.MaxExp);
            if (null == cfg)
                return false;
            skillLevel = cfg.SkillLevel;
            return true;
        }
        public bool TryGetAskNPC(int npcId, out ConfigSkillcardaskrankEntity cfg)
        {
            s_dicAskRank.TryGetValue(npcId, out cfg);
            return null != cfg;
        }
        public bool TryGetAskPicker(int npcId, out RandomPicker picker)
        {
            s_dicAskRand.TryGetValue(npcId, out picker);
            return null != picker;
        }
        public bool TryGetSkillCard(string skillCode, out DicSkillcardEntity cfg)
        {
            s_dicSkillCard.TryGetValue(skillCode, out cfg);
            return null != cfg;
        }
        public bool TryGetSkillCode(string skillRoot, int skillLevel, out string skillCode)
        {
            s_dicSkillRoot.TryGetValue(CastRootKey(skillRoot, skillLevel), out skillCode);
            return !string.IsNullOrEmpty(skillCode);
        }

        public List<DicSkillcardEntity> GetSkillCardByManagerLevel(int managerLevel)
        {
            var skillList = new List<DicSkillcardEntity>();
            var skillRootList=new List<string>();
            foreach (var entity in s_dicSkillCard.Values)
            {
                if (!skillRootList.Contains(entity.SkillRoot))
                {
                    skillRootList.Add(entity.SkillRoot);
                    if (entity.GetLevel <= managerLevel)
                        skillList.Add(entity);
                }
            }
            return skillList;
        }


        #endregion

        #region Random
        public bool TryRandAskNpc(int npcId, out int newRank, out int newNpcId)
        {
            newRank = MINAskRank;
            newNpcId = MINAskNpc;
            ConfigSkillcardaskrankEntity npc;
            if (!TryGetAskNPC(npcId, out npc))
                return false;
            int askRank = npc.AskRank;
            if (askRank >= MAXAskRank)
                return true;
            int succVal = Convert.ToInt32(npc.SuccRate * 10000);
            if (RandomPicker.RandomInt(1, 10000) > succVal)
                return true;
            newRank = askRank + 1;
            return TryRandAskNpc_Rank(newRank, out newNpcId);
        }
        public bool TryRandAskNpc_Rank(int askRank, out int npcId)
        {
            npcId = askRank;
            ConfigSkillcardaskrankEntity npc;
            return TryGetAskNPC(npcId, out npc);
        }
        public bool TryRandAskSkillCode(int npcId, out string skillCode)
        {
            skillCode = string.Empty;
            RandomPicker picker = null;
            if (!TryGetAskPicker(npcId, out picker))
                return false;
            var pickObj = picker.PickRandom();
            if (null == pickObj)
                return false;
            if (pickObj.Index == 0)
                return true;
            return TryRandSkillCode(pickObj.Index, pickObj.Index2, out skillCode);
        }
        public bool TryRandSkillCode(int skillClass, int skillLevel, out string skillCode)
        {
            skillCode = string.Empty;
            int libKey = CastLibKey(skillClass, skillLevel);
            List<string> lib = null;
            if (!s_dicSkillLib.TryGetValue(libKey, out lib) || lib.Count == 0)
                return false;
            skillCode = lib[RandomPicker.RandomInt(0, lib.Count - 1)];
            return true;
        }
        #endregion

        #region Native
        int CastLibKey(int skillClass, int skillLevel)
        {
            return skillClass * 1000 + skillLevel;
        }
        string CastRootKey(string skillRoot, int skillLevel)
        {
            return skillRoot + "_" + skillLevel;
        }
        #endregion
    }
}
