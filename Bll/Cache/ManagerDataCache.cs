using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Share;

namespace Games.NBall.Bll.Cache
{
    public class ManagerDataCache : BaseSingleton
    {
        private Dictionary<int, ConfigManagerlevelEntity> _managerlevelDic;
        /// <summary>
        /// 技能匹配
        /// </summary>
        Dictionary<string, List<string>> _skillMatchDic;

        /// <summary>
        /// 天赋
        /// </summary>
        Dictionary<string,DicSkillstreeEntity> _skillTreeDic;
        /// <summary>
        /// 经理等级对应的天赋点
        /// </summary>
        private Dictionary<int, ConfigSkilltreepointEntity> _managerSkillPointDic;
        /// <summary>
        /// level->functionId
        /// </summary>
        private Dictionary<int, ConfigFunctionopenEntity> _functionOpenDic;
        /// <summary>
        /// 分享配置
        /// </summary>
        private Dictionary<int, List<ConfigShareEntity>> _shareDic;

        private int _maxFunctionLevel;

        private Dictionary<int, int> _levelGiftDic;
        #region encapsulation

        public ManagerDataCache(int p)
            : base(p)
        {
            InitCache();
        }

        public static ManagerDataCache Instance
        {
            get { return SingletonFactory<ManagerDataCache>.SInstance; }
        }

        void InitCache()
        {
            LogHelper.Insert("Manager Data cache init start", LogType.Info);
            var list = ConfigManagerlevelMgr.GetAll();
            _managerlevelDic = list.ToDictionary(d => d.Level, d => d);
            int totalExp = 0;
            foreach (var entity in list)
            {
                entity.TotalExp = totalExp;
                totalExp += entity.Exp;
            }
            var list3 = DicSkillbuffmatchMgr.GetAllForCache();
            _skillMatchDic = new Dictionary<string, List<string>>();
            foreach (var dicSkillbuffMatch in list3)
            {
                var key = BuildSkillPlusKey(dicSkillbuffMatch.Type, dicSkillbuffMatch.LinkId, dicSkillbuffMatch.LinkType);
                if (!_skillMatchDic.ContainsKey(key))
                    _skillMatchDic.Add(key, new List<string>());
                _skillMatchDic[key].Add(dicSkillbuffMatch.BuffEngineId);
            }

            var list4 = ConfigFunctionopenMgr.GetAllForCache();
            _functionOpenDic = new Dictionary<int, ConfigFunctionopenEntity>();
            foreach (var item in list4)
            {
                if (_functionOpenDic.ContainsKey(item.ManagerLevel))
                    continue;
                _functionOpenDic.Add(item.ManagerLevel, item);
            }
            _maxFunctionLevel = list4.Max(d => d.ManagerLevel);

            _levelGiftDic = new Dictionary<int, int>(3);
            var levelGiftconfig = AppsettingCache.Instance.GetAppSetting(EnumAppsetting.LevelGiftConfig);
            if (!string.IsNullOrEmpty(levelGiftconfig))
            {
                var ss = levelGiftconfig.Split(',');
                int step = 1;
                foreach (var s in ss)
                {
                    _levelGiftDic.Add(Convert.ToInt32(s), step);
                    step = step + 1;
                }
            }

            _skillTreeDic = new Dictionary<string, DicSkillstreeEntity>();
            var allTree = DicSkillstreeMgr.GetAll();
            foreach (var item in allTree)
            {
                if (!_skillTreeDic.ContainsKey(item.SkillCode))
                    _skillTreeDic.Add(item.SkillCode, item);
                else
                    _skillTreeDic[item.SkillCode] = item;
            }

            _managerSkillPointDic = new Dictionary<int, ConfigSkilltreepointEntity>();
            var allSkillPoint = ConfigSkilltreepointMgr.GetAll();
            _managerSkillPointDic = allSkillPoint.ToDictionary(r => r.ManagerLevel, r => r);

            _shareDic = new Dictionary<int, List<ConfigShareEntity>>();
            var allShare = ConfigShareMgr.GetAll();
            foreach (var item in allShare)
            {
                if (!_shareDic.ContainsKey(item.ShareType))
                    _shareDic.Add(item.ShareType, new List<ConfigShareEntity>());
                _shareDic[item.ShareType].Add(item);
            }
            LogHelper.Insert("Manager Data cache init end", LogType.Info);
        }
        string BuildSkillPlusKey(int type, string linkId, string level)
        {
            if (type == (int)EnumSBMType.Suit)
                return string.Format("{0}_{2}@{1}", linkId, (int)type, level);
            else
            {
                return string.Format("{0}@{1}", linkId, (int)type);
            }
        }

        #endregion

        #region Facade
        public int GetLevelgiftStep(int level)
        {
            if (_levelGiftDic.ContainsKey(level))
                return _levelGiftDic[level];
            return 0;
        }
        /// <summary>
        /// 获取升级所需经验
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public int GetExp(int level)
        {
            if (_managerlevelDic.ContainsKey(level))
                return _managerlevelDic[level].Exp;
            return 0;
        }

        /// <summary>
        /// 天赋树配置
        /// </summary>
        /// <param name="skillCode"></param>
        /// <returns></returns>
        public DicSkillstreeEntity GetSkillTree(string skillCode)
        {
            if (_skillTreeDic.ContainsKey(skillCode))
                return _skillTreeDic[skillCode];
            return null;
        }

        public int GetTotalExp(int level)
        {
            if (_managerlevelDic.ContainsKey(level))
                return _managerlevelDic[level].TotalExp;
            return 0;
        }

        /// <summary>
        /// 获取最大体力
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public int GetMaxStamina(int level)
        {
            if (_managerlevelDic.ContainsKey(level))
                return _managerlevelDic[level].MaxStamina;
            return 0;
        }

        /// <summary>
        /// 获取当前等级技能数量
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public int GetSkillCount(int level)
        {
            if (_managerlevelDic.ContainsKey(level))
                return _managerlevelDic[level].SkillCount;
            return 0;
        }

        public List<string> GetSkillbuff(EnumSBMType type, int linkId)
        {
            return GetSkillbuff(type, linkId, "");
        }

        public List<string> GetSkillbuff(EnumSBMType type, int linkId, string linkType)
        {
            return GetSkillbuff(type, linkId.ToString(), linkType);
        }

        public List<string> GetSkillbuff(EnumSBMType type, string linkId, string linkType)
        {
            string key = BuildSkillPlusKey((int)type, linkId, linkType);
            if (_skillMatchDic.ContainsKey(key))
            {
                var list = _skillMatchDic[key];
                if (list == null)
                    return null;
                var rList = new List<string>(list.Count);
                foreach (var dicSkillbuffMatch in list)
                {
                    rList.Add(dicSkillbuffMatch);
                }
                return rList;
            }
            else
            {
                return new List<string>();
            }
        }

        /// <summary>
        /// 根据经理等级获取总技能点
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public int GetSumSkillPointByLevel(int level)
        {
            if (_managerSkillPointDic.ContainsKey(level))
                return _managerSkillPointDic[level].SumSkillPoint;
            return 0;
        }

        /// <summary>
        /// 根据经理等级获取增加的技能点
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public int GetAddSkillPointByLevel(int level)
        {
            if (_managerSkillPointDic.ContainsKey(level))
                return _managerSkillPointDic[level].AddSkillPoint;
            return 0;
        }

        public ConfigFunctionopenEntity GetFunctionList(int managerLevel)
        {
            if (managerLevel > _maxFunctionLevel)
                return _functionOpenDic[_maxFunctionLevel];
            ConfigFunctionopenEntity config = null;
            _functionOpenDic.TryGetValue(managerLevel, out config);
            return config;
        }

        /// <summary>
        /// 获取分享配置
        /// </summary>
        /// <param name="shareType"></param>
        /// <returns></returns>
        public List<ConfigShareEntity> GetShare(int shareType)
        {
            if (_shareDic.ContainsKey(shareType))
                return _shareDic[shareType];
            return new List<ConfigShareEntity>();
        }

        #endregion
    }
}
