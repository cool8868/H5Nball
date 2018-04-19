using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Response;

namespace Games.NBall.Bll.Cache
{
    public class PlayerKillCache
    {
        private Dictionary<int, List<PlayerKillOpponentEntity>> _opponentDic;
        private List<List<int>> _sortTemplate;
        private int _minKey;
        private int _maxKey;
        private readonly int _opponentCount = 5;
        private readonly int _baseKpi = 300;
        private int _templateCount = 6;

        private Dictionary<EnumWinType, PkMatchPrizeEntity> _pKMatchPrizeDic;
        private Dictionary<int, int> _pKMatchTimesDic;

        private Dictionary<int, int> _pkMatchVipBuyTimes;//Vip等级可购买次数 
        private Dictionary<int, ConfigPlayerkillpointEntity> _pkPointDic;  

        private DateTime _updateDate;
        #region .ctor
        public PlayerKillCache(int p)
        {
            InitCache();
        }
        #endregion

        #region Facade

        #region Instance
        public static PlayerKillCache Instance
        {
            get { return SingletonFactory<PlayerKillCache>.SInstance; }
        }
        #endregion

        public List<PlayerKillOpponentEntity> GetOpponents(Guid managerId, int kpi)
        {
            List<PlayerKillOpponentEntity> opponents = new List<PlayerKillOpponentEntity>(_opponentCount);
            int key = kpi / _baseKpi;
            if (key < _minKey)
                key = _minKey;
            else if (key > _maxKey)
                key = _maxKey;
            if (_opponentDic.ContainsKey(key))
            {
                var list = _opponentDic[key];
                if (list != null && list.Count > 0)
                {
                    //去除自己
                    var removeItem = list.Find(p => p.ManagerId == managerId);
                    if (removeItem != null)
                        list.Remove(removeItem);

                    if (list.Count <= _opponentCount)
                    {
                        opponents = list;
                    }
                    else
                    {
                        List<PlayerKillOpponentEntity> tempList = new List<PlayerKillOpponentEntity>(_opponentCount);
                        int templateIndex = RandomHelper.GetInt32WithoutMax(0, _templateCount);
                        var templates = _sortTemplate[templateIndex];
                        var index = RandomHelper.GetInt32WithoutMax(0, list.Count);
                        for (int i = 0; i < _opponentCount; i++)
                        {
                            var x = index + i;
                            if (x >= list.Count)
                            {
                                x = x - list.Count;
                            }
                            tempList.Add(list[x]);
                        }
                        if (tempList.Count < _opponentCount)
                        {
                            opponents = tempList;
                        }
                        else
                        {
                            foreach (var template in templates)
                            {
                                opponents.Add(tempList[template]);
                            }
                        }

                    }
                }
            }
            return opponents;
        }

        public PkMatchPrizeEntity GetPrize(EnumWinType winType)
        {
            var prize = _pKMatchPrizeDic[winType].Clone();
            prize.Coin = prize.Coin;
            prize.Exp = prize.Exp;
            return prize;
        }

        /// <summary>
        /// 根据等级获取挑战次数
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public int GetChallengeTimes(int level)
        {
            if (_pKMatchTimesDic.ContainsKey(level))
                return _pKMatchTimesDic[level];
            return 0;
        }

        public void UpdateOpponent()
        {
            if (_updateDate != DateTime.Today)
            {
                _updateDate = DateTime.Today;
                //PlayerkillInfoMgr.ResetByTimes(0, 1000, _updateDate);
            }
            List<int> mergeList = new List<int>();
            var opponentDic = new Dictionary<int, List<PlayerKillOpponentEntity>>();
            int minKey = 0;
            int maxKey = 0;
            GetData(opponentDic, mergeList, ref minKey, ref maxKey);
            foreach (var key in mergeList)
            {
                Merge(opponentDic, key, true, minKey, maxKey);
                Merge(opponentDic, key, false, minKey, maxKey);
            }
            _opponentDic = opponentDic;
            if (maxKey == 0)
                maxKey = minKey;
            _minKey = minKey;
            _maxKey = maxKey;
        }

        public ConfigPlayerkillpointEntity GetPointConfig(int vipLevel)
        {
            if (_pkPointDic.ContainsKey(vipLevel))
                return _pkPointDic[vipLevel];
            else
            {
                return new ConfigPlayerkillpointEntity();
            }
        }

        public int GetTotalPoint(int vipLevel)
        {
            if (_pkPointDic.ContainsKey(vipLevel))
                return _pkPointDic[vipLevel].TotalPoint;
            else
            {
                return 0;
            }
        }
        #endregion

        #region encapsulation
        void InitCache()
        {
            //LogHelper.Insert("PlayerKill cache init start", LogType.Info);
            //200,500|1000,2000|500,1000
            var pKMatchPrize = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.PKMatchPrize);
            _pKMatchPrizeDic = new Dictionary<EnumWinType, PkMatchPrizeEntity>();
            var pks = pKMatchPrize.Split('|');
            _pKMatchPrizeDic.Add(EnumWinType.Win, new PkMatchPrizeEntity(pks[0]));
            _pKMatchPrizeDic.Add(EnumWinType.Draw, new PkMatchPrizeEntity(pks[1]));
            _pKMatchPrizeDic.Add(EnumWinType.Lose, new PkMatchPrizeEntity(pks[2]));

            var pkPKChallengeTimes = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.PKChallengeTimes);
            _pKMatchTimesDic = new Dictionary<int, int>();
            var pks1 = pkPKChallengeTimes.Split('|');
            AddPkChallengeDic(pks1[0]);
            AddPkChallengeDic(pks1[1]);
            AddPkChallengeDic(pks1[2]);
            AddPkChallengeDic(pks1[3]);
            AddPkChallengeDic(pks1[4]);
            AddPkChallengeDic(pks1[5]);
            AddPkChallengeDic(pks1[6]);
            AddPkChallengeDic(pks1[7]);


            _pkMatchVipBuyTimes=new Dictionary<int, int>();
            var pkMatchVipBuyTimes = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.PKVipCanBuyTimes);
            var pks2 = pkMatchVipBuyTimes.Split('|');
            foreach (var vipstr in pks2)
            {
                var level = Convert.ToInt32(vipstr.Split(',')[0]);
                var times = Convert.ToInt32(vipstr.Split(',')[1]);
                _pkMatchVipBuyTimes.Add(level,times);
            }
            var listPoint = ConfigPlayerkillpointMgr.GetAll();
            _pkPointDic = listPoint.ToDictionary(d => d.VipLevel, d => d);

            _updateDate = DateTime.Today.AddDays(-1);
            InitTemplates();
            UpdateOpponent();
            // LogHelper.Insert("PlayerKill cache init end", LogType.Info);
        }

        private void AddPkChallengeDic(string str)
        {
            var entity = new PKChallengeTimesEntity(str);
            int maxLevel = entity.ELevel;
            while (maxLevel >= entity.SLevel)
            {
                if (!_pKMatchTimesDic.ContainsKey(maxLevel))
                    _pKMatchTimesDic.Add(maxLevel, entity.Number);
                _pKMatchTimesDic[maxLevel] = entity.Number;
                maxLevel--;
            }
        }

        void InitTemplates()
        {
            if (_opponentCount != 5)
                throw new Exception("对手数量不是5，请调整排列组合模板，method : InitTemplates()");
            _sortTemplate = new List<List<int>>(5);
            _templateCount = 6;
            var templates = new List<int>(5); templates.Add(3); templates.Add(1); templates.Add(2); templates.Add(0); templates.Add(4); _sortTemplate.Add(templates);
            templates = new List<int>(5); templates.Add(2); templates.Add(0); templates.Add(1); templates.Add(4); templates.Add(3); _sortTemplate.Add(templates);
            templates = new List<int>(5); templates.Add(0); templates.Add(1); templates.Add(4); templates.Add(2); templates.Add(3); _sortTemplate.Add(templates);
            templates = new List<int>(5); templates.Add(1); templates.Add(4); templates.Add(2); templates.Add(3); templates.Add(0); _sortTemplate.Add(templates);
            templates = new List<int>(5); templates.Add(4); templates.Add(2); templates.Add(3); templates.Add(0); templates.Add(1); _sortTemplate.Add(templates);
            templates = new List<int>(5); templates.Add(3); templates.Add(0); templates.Add(4); templates.Add(2); templates.Add(1); _sortTemplate.Add(templates);
        }

        void Merge(Dictionary<int, List<PlayerKillOpponentEntity>> opponentDic, int key, bool isUp, int minKey, int maxKey)
        {
            List<PlayerKillOpponentEntity> rawList = null;
            bool notContain = false;
            if (opponentDic.ContainsKey(key))
            {
                rawList = opponentDic[key];
            }
            else
            {
                rawList = new List<PlayerKillOpponentEntity>();
                notContain = true;
            }
            for (int i = 1; i < 10; i++)
            {
                int addkey = i;
                if (!isUp)
                    addkey = addkey * -1;
                int newKey = key + addkey;
                if (newKey > maxKey || newKey < minKey)
                {
                    if (notContain) opponentDic.Add(key, rawList);
                    return;
                }
                if (opponentDic.ContainsKey(newKey))
                {
                    var newList = opponentDic[newKey];
                    if (newList != null && newList.Count > 0)
                    {
                        foreach (var entity in newList)
                        {
                            if (!rawList.Exists(d => d.ManagerId == entity.ManagerId))
                            {
                                rawList.Add(entity);
                            }
                        }
                    }
                    if (rawList.Count > _opponentCount)
                    {
                        if (notContain) opponentDic.Add(key, rawList);
                        return;
                    }
                }
            }
        }

        void GetData(Dictionary<int, List<PlayerKillOpponentEntity>> opponentDic, List<int> mergeList, ref int minKey, ref int maxKey)
        {
            int noData = 0;
            for (int i = 4; i < 20; i++)
            {
                int minKpi = i * _baseKpi;
                int maxKpi = minKpi + _baseKpi;
                minKpi = 0;
                var list = PlayerkillInfoMgr.GetOpponents(minKpi, maxKpi, 0);
                if (list != null && list.Count > 0)
                {
                    if (list.Count < _opponentCount)
                        mergeList.Add(i);
                    if (minKey == 0)
                        minKey = i;
                    else if (maxKey < i)
                        maxKey = i;
                    opponentDic.Add(i, list);
                }
                else
                {
                    if (minKpi > 5000)
                    {
                        noData++;
                        if (noData > 2)
                        {
                            return;
                        }
                    }
                    else
                    {
                        mergeList.Add(i);
                    }
                }
            }
        }

        /// <summary>
        /// 获取Vip等级对应的次数
        /// </summary>
        /// <param name="vipLevel"></param>
        /// <returns></returns>
        public int GetVipLevelBuyTimes(int vipLevel)
        {
            if (_pkMatchVipBuyTimes.ContainsKey(vipLevel))
                return _pkMatchVipBuyTimes[vipLevel];
            return 0;
        }



        #endregion
    }
}
