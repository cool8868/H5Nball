using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Response.Rank;

namespace Games.NBall.Core.Rank
{
    public class RankThread
    {
        private Dictionary<int,RankHandler> _rankHandlers;
        private DateTime _rankRecordDate;
        #region .ctor

        public RankThread(int b)
        {
            _rankRecordDate = DateTime.Today;
            _rankHandlers = new Dictionary<int, RankHandler>(2);
            _rankHandlers.Add((int)EnumRankType.KpiRank,new RankHandler(EnumRankType.KpiRank));
            _rankHandlers.Add((int)EnumRankType.LevelRank, new RankHandler(EnumRankType.LevelRank));
            //_rankHandlers.Add((int)EnumRankType.ScoreRank, new RankHandler(EnumRankType.ScoreRank));
        }

        #endregion

        #region Facade
        public static RankThread Instance
        {
            get { return SingletonFactory<RankThread>.SInstance; }
        }

        public MessageCode RunJob(DateTime nextTime)
        {
            if (DateTime.Today != _rankRecordDate)
            {
                RankRecord();
            }
            foreach (var rankHandler in _rankHandlers.Values)
            {
                rankHandler.RunJob(nextTime);
            }
            return MessageCode.Success;
        }

        public RankResponse GetRanking(Guid managerId,int rankType, int pageIndex, int pageSize)
        {
            RankHandler handler = null;
            _rankHandlers.TryGetValue(rankType, out handler);
            if (handler != null)
            {
                var response = handler.GetResponse(managerId, pageIndex, pageSize);
                if (response != null && response.Data != null)
                {
                    response.Data.RankType = rankType;
                }
                return response;
            }
            else
            {
                return ResponseHelper.InvalidParameter<RankResponse>();
            }
        }
        #endregion

        void RankRecord()
        {
            _rankRecordDate = DateTime.Today;
            RankingYesterdayMgr.StartRecord();
            foreach (var rankHandler in _rankHandlers.Values)
            {
                rankHandler.SaveRanking();
            }
        }
    }
}
