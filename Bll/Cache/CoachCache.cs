using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Player;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Share;

namespace Games.NBall.Bll.Cache
{
    public class CoachCache : BaseSingleton
    {
        public CoachCache(int p)
            : base(p)
        {
            InitCache();
        }

        private Dictionary<int, ConfigCoachinfoEntity> _coachDic;
        private Dictionary<int, ConfigCoachstarEntity> _coachStarDic;
        private Dictionary<int, ConfigCoachupgradeEntity> _coachUpgradeDic;

        private void InitCache()
        {
            LogHelper.Insert("CoachCache cache init start", LogType.Info);

            var allCoach = ConfigCoachinfoMgr.GetAll();
            _coachDic = allCoach.ToDictionary(r => r.Idx, r => r);
            var allcoachStar = ConfigCoachstarMgr.GetAll();
            _coachStarDic = new Dictionary<int, ConfigCoachstarEntity>();
            foreach (var item in allcoachStar)
            {
                var key = GetKey(item.CoachId, item.StarLevel);
                if (!_coachStarDic.ContainsKey(key))
                    _coachStarDic.Add(key, item);
            }

            var allUpgrade = ConfigCoachupgradeMgr.GetAll();
            _coachUpgradeDic = allUpgrade.ToDictionary(r => r.Level, r => r);
            LogHelper.Insert("CoachCache cache init end", LogType.Info);
        }

        #region Facade

        public static CoachCache Instance
        {
            get { return SingletonFactory<CoachCache>.SInstance; }
        }

        private int GetKey(int number1, int number2)
        {
            return number2*100000 + number1;
        }

        /// <summary>
        /// 获取升星配置
        /// </summary>
        /// <param name="coachId"></param>
        /// <param name="starLevel"></param>
        /// <returns></returns>
        public ConfigCoachstarEntity GetCoachStarInfo(int coachId, int starLevel)
        {
            var key = GetKey(coachId, starLevel);
            if (_coachStarDic.ContainsKey(key))
                return _coachStarDic[key];
            return null;
        }

        /// <summary>
        /// 获取升级配置
        /// </summary>
        /// <param name="coachLevel"></param>
        /// <returns></returns>
        public ConfigCoachupgradeEntity GetCoachUpgradeInfo(int coachLevel)
        {
            if (_coachUpgradeDic.ContainsKey(coachLevel))
                return _coachUpgradeDic[coachLevel];
            return null;
        }

        /// <summary>
        /// 获取教练详情
        /// </summary>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public ConfigCoachinfoEntity GetCoachInfo(int coachId)
        {
            if (_coachDic.ContainsKey(coachId))
                return _coachDic[coachId];
            return null;
        }

        public List<ConfigCoachinfoEntity> GetAllCoach()
        {
            return _coachDic.Values.ToList();
        }

        #endregion
    }

}
