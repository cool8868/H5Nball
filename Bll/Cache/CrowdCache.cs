using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Crowd;

namespace Games.NBall.Bll.Cache
{
    public class CrowdCache
    {
        public CrowdCache(int p)
        {
            InitCache();
        }

        #region encapsulation
        /// <summary>
        /// taskid->entity
        /// </summary>
        private Dictionary<int, int> _morelaDic;
        /// <summary>
        /// goal->score
        /// </summary>
        private Dictionary<int, int> _scoreDic;
        /// <summary>
        /// category,categorySup->list
        /// </summary>
        private Dictionary<int, List<ConfigCrowdrankprizeEntity>> _rankPrizeDic;

        private Dictionary<int, ConfigCrowdprizeEntity> _matchPrizeDic; 

        void InitCache()
        {
            LogHelper.Insert("crowd cache init start", LogType.Info);
            var list = ConfigCrowdmoraleMgr.GetAll();
            _morelaDic = list.ToDictionary(d => d.Idx, d => d.CostMorela);

            var list1 = ConfigCrowdscoreMgr.GetAll();
            _scoreDic = list1.ToDictionary(d => d.Idx, d => d.Score);

            var list2 = ConfigCrowdrankprizeMgr.GetAll();
            _rankPrizeDic = new Dictionary<int, List<ConfigCrowdrankprizeEntity>>();
            foreach (var entity in list2)
            {
                var key = BuildRankPrizeKey(entity.Category, entity.CategorySub);
                if(!_rankPrizeDic.ContainsKey(key))
                    _rankPrizeDic.Add(key,new List<ConfigCrowdrankprizeEntity>());
                _rankPrizeDic[key].Add(entity);
            }

            var list3 = ConfigCrowdprizeMgr.GetAll();
            _matchPrizeDic = list3.ToDictionary(d => d.WinType, d => d);
            LogHelper.Insert("crowd cache init end", LogType.Info);
        }

        int BuildRankPrizeKey(int category, int categorySub)
        {
            return category*1000 + categorySub;
        }
        #endregion

        #region Facade

        public static CrowdCache Instance
        {
            get { return SingletonFactory<CrowdCache>.SInstance; }
        }
        
        public List<ConfigCrowdrankprizeEntity> GetRankPrizes(EnumCrowdPrizeCategory category,int categorySub)
        {
            var key = BuildRankPrizeKey((int) category, categorySub);
            if (_rankPrizeDic.ContainsKey(key))
                return _rankPrizeDic[key];
            return null;
        }

        public ConfigCrowdprizeEntity GetMatchPrize(EnumWinType winType)
        {
            return _matchPrizeDic[(int)winType];
        }

        public int GetCostMorela(int byGoal)
        {
            if (_morelaDic.ContainsKey(byGoal))
            {
                return _morelaDic[byGoal];
            }
            else
            {
                return 0;
            }
        }

        public int GetCrowdScore(int goal)
        {
            if(_scoreDic.ContainsKey(goal))
            {return _scoreDic[goal];}
            else
            {
                return 0;
            }
        }
        #endregion
    }
}
