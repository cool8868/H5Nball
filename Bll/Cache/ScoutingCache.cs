using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response.Scouting;
using Games.NBall.Entity.Share;

namespace Games.NBall.Bll.Cache
{
    public class ScoutingCache:BaseSingleton
    {

        public ScoutingCache(int p)
            :base(p)
        {
            InitCache();
        }

        #region encapsulation

        private Dictionary<int, ConfigScoutingEntity> _scoutingDic;
        private string[] _scoutingLotteryRate;
        private List<int> _rateList;
        void InitCache()
        {
            LogHelper.Insert("Scouting cache init start", LogType.Info);
            var list = ConfigScoutingMgr.GetAll();
            _scoutingDic = list.ToDictionary(d => d.Idx, d => d);
            _scoutingLotteryRate = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.ScoutingLotteryRate).Split(',');
            _rateList = new List<int>();
            foreach (var i in _scoutingLotteryRate)
            {
                int a = ConvertHelper.ConvertToInt(i);
                _rateList.Add(a);
            }
            LogHelper.Insert("Scouting cache init end", LogType.Info);
        }
        #endregion

        #region Facade

        public static ScoutingCache Instance
        {
            get { return SingletonFactory<ScoutingCache>.SInstance; }
        }

        public List<ConfigScoutingDataEntity> GetShowList(DateTime curTime)
        {
            var list = new List<ConfigScoutingDataEntity>(_scoutingDic.Count);
            foreach (var entity in _scoutingDic.Values)
            {
                if (entity.Type == 99)
                {
                    list.Add(new ConfigScoutingDataEntity()
                    {
                        Idx = 99,
                        CurrencyType = (int) EnumCurrencyType.GoldBar,
                        CurrencyCount = 10
                    });
                }
                else
                {
                    var mallEntity = MallCache.Instance.GetMallEntity(entity.MallCode, curTime);
                    list.Add(new ConfigScoutingDataEntity()
                    {
                        Idx = entity.Idx,
                        CurrencyType = mallEntity.CurrencyType,
                        CurrencyCount = mallEntity.CurrencyCount
                    });
                }
            }
            return list;
        }

        public ConfigScoutingEntity GetEntity(int idx)
        {
            if (_scoutingDic.ContainsKey(idx))
                return _scoutingDic[idx];
            return null;
        }

        public int GetKpi(int itemCode)
        {
            int kpi = 0;
            var item = ItemsdicCache.Instance.GetItem(itemCode);
            if (item.ItemType == (int) EnumItemType.PlayerCard)
                kpi = item.FourthType;
            if (item.ItemType == (int) EnumItemType.MallItem && item.FourthType == 56)
            {
                itemCode = ItemsdicCache.Instance.GetTheContractItemCode(itemCode);
                kpi = ItemsdicCache.Instance.GetItem(itemCode).FourthType;
            }
            return kpi;
        }


        public DateTime CalNextRefreshTime(DateTime curTime)
        {
            var newTime = curTime.AddHours(6);
            return newTime;
        }


        #endregion
    }

    class NScoutingEntityCompare : IComparer<NScoutingEntity>
    {
        public int Compare(NScoutingEntity x, NScoutingEntity y)
        {
            if (x.Kpi - y.Kpi > 0)
                return -1;
            else if (x.Kpi - y.Kpi < 0)
                return 1;
            else
                return 0;
        }
    }
}
