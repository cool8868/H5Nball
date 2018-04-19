using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Share;

namespace Games.NBall.Bll.Cache
{
    public class CrossLadderCache: BaseSingleton
    {
        /// <summary>
        /// date->seasonid
        /// </summary>
        private Dictionary<DateTime, CrossladderSeasonEntity> _dateSeasonDic;

        private Dictionary<int, CrossladderSeasonEntity> _seasonDic;

        private Dictionary<int, DicCrossladderexchangeEntity> _exchangeDic;
        /// <summary>
        /// rank->packId
        /// </summary>
        private Dictionary<int, int> _prizeDic;
        /// <summary>
        /// rank->packId
        /// </summary>
        private Dictionary<int, int> _dailyPrizeDic;
        /// <summary>
        /// rank->packId
        /// </summary>
        private Dictionary<int, int> _prizeNewDic;
        /// <summary>
        /// rank->packId
        /// </summary>
        private Dictionary<int, int> _dailyPrizeNewDic;

        private int _maxPrizeRank;
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="CrossLadderCache"/> class.
        /// </summary>
        public CrossLadderCache(int p)
            : base(p)
        {
            InitCache();
        }
        
        /// <summary>
        /// Inits the cache.
        /// </summary>
        void InitCache()
        {
            try
            {
                var list = DicCrossladderexchangeMgr.GetAll();
                _exchangeDic = list.ToDictionary(d => d.Idx, d => d);
                var list2 = DicCrossladderprizeMgr.GetAll();
                _prizeDic=new Dictionary<int, int>();
                _dailyPrizeDic=new Dictionary<int, int>();
                _prizeNewDic=new Dictionary<int, int>();
                _dailyPrizeNewDic=new Dictionary<int, int>();
                foreach (var entity in list2)
                {
                    switch (entity.PrizeType)
                    {
                        case 1:
                            BuildPrizeDic(_prizeDic, entity.MinRank, entity.MaxRank, entity.SubType);
                            break;
                        case 2:
                            BuildPrizeDic(_dailyPrizeDic, entity.MinRank, entity.MaxRank, entity.SubType);
                            break;
                        case 3:
                            BuildPrizeDic(_prizeNewDic, entity.MinRank, entity.MaxRank, entity.SubType);
                            break;
                        case 4:
                            BuildPrizeDic(_dailyPrizeNewDic, entity.MinRank, entity.MaxRank, entity.SubType);
                            break;
                    }
                }
                var seasonList = CrossladderSeasonMgr.GetAll();
                _seasonDic = seasonList.ToDictionary(d => d.Idx, d => d);
                DateTime curDate = DateTime.Today.AddDays(-31);
                int count = 0;
                _dateSeasonDic = new Dictionary<DateTime, CrossladderSeasonEntity>(600);
                foreach (var entity in seasonList)
                {
                    if (entity.Enddate >= curDate)
                    {
                        var date = entity.Enddate.Subtract(entity.Startdate).TotalDays + 1;
                        for (int i = 0; i < date; i++)
                        {
                            _dateSeasonDic.Add(entity.Startdate.AddDays(i), entity);
                        }
                        count++;
                        if (count > 40)
                            return;
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CrossLadderCache-InitCache",ex);
            }
        }

        void BuildPrizeDic(Dictionary<int,int> prizeDic,int minRank,int maxRank,int subType)
        {
            if (maxRank == 0)
            {
                prizeDic.Add(minRank,subType);
                _maxPrizeRank = minRank;
                return;
            }
            for (int i = minRank; i <= maxRank; i++)
            {
                prizeDic.Add(i, subType);
            }
        }
        #endregion

        #region Facade
        public static CrossLadderCache Instance
        {
            get { return SingletonFactory<CrossLadderCache>.SInstance; }
        }

        public int GetRankPrize(EnumCrossLadderPrizeType crossLadderPrizeType, int rank,int seasonStatus)
        {
            if (seasonStatus == 1)
            {
                return GetRankPrize(_prizeDic, rank);
            }
            else
            {
                return GetRankPrize(_prizeNewDic, rank);
            }
        }

        int GetRankPrize(Dictionary<int, int> prizeDic, int rank)
        {
            if (prizeDic.ContainsKey(rank))
                return prizeDic[rank];
            else if (prizeDic.ContainsKey(_maxPrizeRank))
                return prizeDic[_maxPrizeRank];
            else
            {
                return 0;
            }
        }

        public DicCrossladderexchangeEntity GetExchangeEntity(int idx)
        {
            DicCrossladderexchangeEntity entity = null;
            _exchangeDic.TryGetValue(idx, out entity);
            return entity;
        }

        /// <summary>
        /// Gets the current season.
        /// </summary>
        /// <returns></returns>
        public CrossladderSeasonEntity GetCurrentSeason()
        {
            var curDate = DateTime.Today;
            CrossladderSeasonEntity season = null;
            _dateSeasonDic.TryGetValue(curDate, out season);
            return season;
        }

        /// <summary>
        /// 获取当前赛季id
        /// </summary>
        /// <returns></returns>
        public int GetCurrentSeasonIndex()
        {
            var season = GetCurrentSeason();
            if (season != null)
            {
                return season.Idx;
            }
            return 0;
        }

        public CrossladderSeasonEntity GetEntity(int seasonId)
        {
            CrossladderSeasonEntity season = null;
            _seasonDic.TryGetValue(seasonId, out season);
            return season;
        }
        #endregion
    
    }
}
