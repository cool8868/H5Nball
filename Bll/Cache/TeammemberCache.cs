using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Bll.Cache
{
    /// <summary>
    /// 球员成长
    /// </summary>
    public class TeammemberCache
    {

        Dictionary<int, DicGrowEntity> _growDic;
        private Dictionary<int, ConfigPlayerlevelEntity> _playerlevelDic;
        private Dictionary<int, int> _strengthPlusDic; 
        int _growFastPointMax;
        int _growFastPointMin;
        private int _maxExp;
        private int _maxLevel;
        
        public int PropertyBase { get; set; }

        public TeammemberCache(int p)
        {
            InitCache();
        }

        void InitCache()
        {
            LogHelper.Insert("Teammember cache init start", LogType.Info);
            var list = DicGrowMgr.GetAllForCache();
            _growDic = list.ToDictionary(d => d.Idx, d => d);
            string growFastPointRange = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.GrowFastPointRange);
            string[] s = growFastPointRange.Split(',');
            _growFastPointMin =Convert.ToInt32(s[0]);
            _growFastPointMax = Convert.ToInt32(s[1]);

            var list2 = ConfigPlayerlevelMgr.GetAll();
            _playerlevelDic = list2.ToDictionary(d => d.Level, d => d);
            int totalExp = 0;
            _maxExp = 0;
            _maxLevel = 0;
            foreach (var entity in list2)
            {
                var startExp = totalExp;
                totalExp += entity.Exp;
                int key = entity.Level;
                if (_playerlevelDic.ContainsKey(key))
                {
                    _playerlevelDic[key].StartExp = startExp;
                    _playerlevelDic[key].EndExp = totalExp;
                }
                if (_maxLevel < entity.Level)
                {
                    _maxLevel = entity.Level;
                    _maxExp = startExp;
                }
            }

            string strengthPlus = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.PlayerCardStrengthPlus);
            var ss = strengthPlus.Split(',');
            _strengthPlusDic = new Dictionary<int, int>(ss.Length);
            for (int i = 0; i < ss.Length; i++)
            {
                _strengthPlusDic.Add(i,ConvertHelper.ConvertToInt(ss[i]));
            }
            LogHelper.Insert("Teammember cache init end", LogType.Info);
        }

        public static TeammemberCache Instance
        {
            get { return SingletonFactory<TeammemberCache>.SInstance; }
        }

        public int GetFastPoint()
        {
            return RandomHelper.GetInt32(_growFastPointMin, _growFastPointMax);
        }

        public DicGrowEntity GetGrow(int growLevel)
        {
            if (_growDic.ContainsKey(growLevel))
                return _growDic[growLevel];
            return null;
        }

        /// <summary>
        /// 获取升级所需经验
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public int GetExp(int level)
        {
            if (_playerlevelDic.ContainsKey(level))
                return _playerlevelDic[level].Exp;
            return 0;
        }

        /// <summary>
        /// 获取当前等级可分配属性数量
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public int GetPropertyCount(int level)
        {
            if (_playerlevelDic.ContainsKey(level))
                return _playerlevelDic[level].PropertyCount;
            return 0;
        }

        public int GetStrengthPlus(int strength)
        {
            int plus = 0;
            _strengthPlusDic.TryGetValue(strength, out plus);
            return plus;
        }

        /// <summary>
        /// 根据经验获取等级
        /// </summary>
        /// <param name="totalExp"></param>
        /// <returns></returns>
        public int CalPlayerLevel(ref int totalExp)
        {
            int playerlevel = 0;
            if (totalExp >= _maxExp)
            {
                totalExp = 0;
                return _maxLevel;
            }
            foreach (var lv in _playerlevelDic)
            {
                if (lv.Value.StartExp<= totalExp && lv.Value.EndExp>totalExp)
                {
                    playerlevel = lv.Key;
                    break;
                }
            }
            if (playerlevel > 0)
            {
                totalExp =totalExp - _playerlevelDic[playerlevel].StartExp;
            }
            return playerlevel;
        }
    }
}
