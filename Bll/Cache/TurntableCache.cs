using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Share;
using Games.NBall.Entity;

namespace Games.NBall.Bll.Cache
{
    public class TurntableCache : BaseSingleton
    {
        #region encapsulation

        private Dictionary<int, List<ConfigTurntableprizeEntity>> _TurntablePrizeDic;

        private Dictionary<int, ConfigTurntableprizeEntity> _TurntablePrize;
       /// <summary>
       /// 跨服活动奖励表
       /// </summary>
        private Dictionary<int, ConfigCrossactivityprizeEntity> _CrossActiviryPrize;

        private List<ConfigCrossactivityprizeEntity> _CrossActivityList;
        /// <summary>
        /// 跨服活动抽奖字典 key=概率 value=奖励ID
        /// </summary>
        public Dictionary<int, int> _CrossActivityRateDic;
        private int giveLuckyCoin;
        private int dayProduceLuckyCoin;
        /// <summary>
        /// 转盘重置配置
        /// </summary>
        public Dictionary<int, int> _TurntableResetDic; 

        public TurntableCache(int p)
            : base(p)
        {
            InitCache();
        }

        void InitCache()
        {
            LogHelper.Insert("TurntableCache init start", LogType.Info);
            _TurntablePrizeDic = new Dictionary<int, List<ConfigTurntableprizeEntity>>();
            _TurntablePrize = new Dictionary<int, ConfigTurntableprizeEntity>();
            _TurntableResetDic = new Dictionary<int, int>();
            var allTurntablePrize = ConfigTurntableprizeMgr.GetAll();
            foreach (var item in allTurntablePrize)
            {
                if(!_TurntablePrizeDic.ContainsKey(item.TurntableType))
                    _TurntablePrizeDic.Add(item.TurntableType,new List<ConfigTurntableprizeEntity>());
                _TurntablePrizeDic[item.TurntableType].Add(item);
                var key = GetKey(item.TurntableType,item.TurntableId);
                if (!_TurntablePrize.ContainsKey(key))
                    _TurntablePrize.Add(key, item);
            }
            var allResetConfig = ConfigTurntableresetMgr.GetAll();
            foreach (var item in allResetConfig)
            {
                var key = GetKey(item.TurntableType, item.ResetNumber);
                if (!_TurntableResetDic.ContainsKey(item.TurntableType))
                    _TurntableResetDic.Add(key, item.Point);
            }
            giveLuckyCoin = 2;
            dayProduceLuckyCoin = 5;

            #region 感恩回馈

            _CrossActivityList = ConfigCrossactivityprizeMgr.GetAll();
            _CrossActiviryPrize = _CrossActivityList.ToDictionary(r => r.PrizeId, r => r);
            int rate = 0;
            _CrossActivityRateDic = new Dictionary<int, int>();
            foreach (var item in _CrossActivityList)
            {
                rate += item.Rate;
                if (!_CrossActivityRateDic.ContainsKey(rate))
                    _CrossActivityRateDic.Add(rate, item.PrizeId);
            }
            #endregion

            LogHelper.Insert("TurntableCache init end", LogType.Info);
        }
        #endregion

        #region Facade

        public static TurntableCache Instance
        {
            get { return SingletonFactory<TurntableCache>.SInstance; }

        }
        #region 转盘
        public int GiveLuckyCoin { get { return giveLuckyCoin; } }
        public int DayProduceLuckyCoin { get { return dayProduceLuckyCoin; } }

        /// <summary>
        /// 获取重置转盘消耗点卷数
        /// </summary>
        /// <param name="turntableType"></param>
        /// <param name="resetNumber"></param>
        /// <returns></returns>
        public int GetResetPoint(int turntableType,int resetNumber)
        {
            if (resetNumber >= 10)
                resetNumber = 10;
            var key = GetKey(turntableType, resetNumber);
            if (_TurntableResetDic.ContainsKey(key))
                return _TurntableResetDic[key];
            return 0;
        }

        /// <summary>
        /// 根据转盘类型 获取转盘列表
        /// </summary>
        /// <param name="turntableType"></param>
        /// <returns></returns>
        public List<ConfigTurntableprizeEntity> GetTurntableList(int turntableType) 
        {
            if(_TurntablePrizeDic.ContainsKey(turntableType))
                return _TurntablePrizeDic[turntableType];
            return new List<ConfigTurntableprizeEntity>();
        }

        /// <summary>
        /// 获取转盘奖励
        /// </summary>
        /// <param name="turntableType"></param>
        /// <param name="turntableId"></param>
        /// <returns></returns>
        public ConfigTurntableprizeEntity GetTurntablePrize(int turntableType, int turntableId) 
        {
            var key = GetKey(turntableType,turntableId);
            if (_TurntablePrize.ContainsKey(key))
                return _TurntablePrize[key];
            return null;
        }

        int GetKey(int turntableType, int turntableId)
        {
            return turntableType * 10000 + turntableId;
        }

        #endregion

        #region 感恩回馈

        /// <summary>
        /// 获取所有奖励
        /// </summary>
        /// <returns></returns>
        public List<ConfigCrossactivityprizeEntity> GetAllPrize()
        {
            return _CrossActivityList;
        }

        public ConfigCrossactivityprizeEntity GetPrize(int prizeId)
        {
            if (_CrossActiviryPrize.ContainsKey(prizeId))
                return _CrossActiviryPrize[prizeId];
            return null;
        }

        public int Prize(int rate)
        {
            int result = 0;
            foreach (var item in _CrossActivityRateDic)
            {
                if (item.Key >= rate)
                {
                    result = item.Value;
                    break;
                }
            }
            return result;
        }

        #endregion
        #endregion
    }
}
