using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.NBall.Custom.Rank;
using Games.NBall.Entity.Response.Rank;

namespace Games.NBall.Core.Rank
{
    public class RankLadderThread
    {
        private Dictionary<int, RankHandler> _rankHandlers;
        private DateTime _rankRecordDate;
        #region .ctor

        public RankLadderThread(int b)
        {
            _rankRecordDate = DateTime.Today;
            _rankHandlers = new Dictionary<int, RankHandler>(1);
            _rankHandlers.Add((int)EnumRankType.LadderRank, new RankHandler(EnumRankType.LadderRank));
            //_rankHandlers.Add((int)EnumRankType.ArenaRank, new RankHandler(EnumRankType.ArenaRank));
            //_rankHandlers.Add((int)EnumRankType.CrowdRank, new RankHandler(EnumRankType.CrowdRank));
        }

        #endregion

        #region Facade
        public static RankLadderThread Instance
        {
            get { return SingletonFactory<RankLadderThread>.SInstance; }
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

        public RankResponse GetRanking(Guid managerId, int rankType, int pageIndex, int pageSize)
        {
            RankHandler handler = null;
            _rankHandlers.TryGetValue(rankType, out handler);
            if (handler != null)
            {
                var response=handler.GetResponse(managerId, pageIndex, pageSize);
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

        public BaseRankEntity GetMyRank(Guid managerId,int rankType)
        {
            RankHandler handler = null;
            _rankHandlers.TryGetValue(rankType, out handler);
            if (handler != null)
            {
                return handler.GetMyRankEntity(managerId);
            }
            else
            {
                return null;
            }
        }
        #endregion

        void RankRecord()
        {
            _rankRecordDate = DateTime.Today;
            RankingLadderyesterdayMgr.StartRecord();
            foreach (var rankHandler in _rankHandlers.Values)
            {
                rankHandler.SaveLadderRanking();
            }
        }
    }
}
