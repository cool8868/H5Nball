using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Share;

namespace Games.NBall.Bll.Cache
{
    /// <summary>
    /// 赛季缓存
    /// </summary>
    public class SeasonCache : BaseSingleton
    {
        /// <summary>
        /// date->seasonid
        /// </summary>
        private Dictionary<DateTime, LadderSeasonEntity> _dateSeasonDic;

        private Dictionary<int, LadderSeasonEntity> _seasonDic; 
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="SeasonCache"/> class.
        /// </summary>
        public SeasonCache(int p)
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
                var seasonList = LadderSeasonMgr.GetAll();
                _seasonDic = seasonList.ToDictionary(d => d.Idx, d => d);
                DateTime curDate = DateTime.Today.AddDays(-31);
                int count = 0;
                _dateSeasonDic = new Dictionary<DateTime, LadderSeasonEntity>(600);
                foreach (var entity in seasonList)
                {
                    if (entity.Enddate >= curDate)
                    {
                        var date = entity.Enddate.Subtract(entity.Startdate).TotalDays+1;
                        for (int i = 0; i < date; i++)
                        {
                            _dateSeasonDic.Add(entity.Startdate.AddDays(i),entity);
                        }
                        count++;
                        if (count > 40)
                            return;
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SeasonCache-InitCache",ex);
            }
        }
        #endregion

        #region Facade
        public static SeasonCache Instance
        {
            get { return SingletonFactory<SeasonCache>.SInstance; }
        }

        /// <summary>
        /// Gets the current season.
        /// </summary>
        /// <returns></returns>
        public LadderSeasonEntity GetCurrentSeason()
        {
            var curDate = DateTime.Today;
            LadderSeasonEntity season = null;
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

        public LadderSeasonEntity GetEntity(int seasonId)
        {
            LadderSeasonEntity season = null;
            _seasonDic.TryGetValue(seasonId, out season);
            return season;
        }
        #endregion
    }
}
