using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base.Model.TranIn;
using Games.NB.Match.Base.Model.TranOut;
using Games.NBall.Bll;
using Games.NBall.Bll.Match;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Match;
using Games.NBall.Entity.Share;
using Games.NBall.MatchFacade;
using Games.NBall.Entity.Response;

namespace Games.NBall.Core.Match
{
    public class MatchThread : BaseSingleton
    {
        public delegate MessageCode MatchCallback(BaseMatchData baseMatchData);
        public delegate MessageCode MatchStateCallback(BaseMatchData baseMatchData, object matchState);


        #region .ctor
        public MatchThread(int p)
            : base(p)
        {
            Initialize();
        }
        #endregion

        #region Parms
        const int MATCHConcurrency = 8;
        const int MATCHRetryTimes = 5;
        #endregion

        #region Fields
        NBThreadPool _poolMatch;
        static readonly Dictionary<EnumMatchType, NBThreadPool> s_dicMatchPool = new Dictionary<EnumMatchType, NBThreadPool>();
        private MatchClient _matchClient;
        readonly object _lockRoot = new object();
        bool _isInit = false;
        static readonly Dictionary<EnumMatchType, byte> s_noCacheMatchTypes = new Dictionary<EnumMatchType, byte>();
        static readonly Dictionary<EnumMatchType, byte> s_DbMatchTypes = new Dictionary<EnumMatchType, byte>();
        #endregion

        #region Facade

        public static MatchThread Instance
        {
            get { return SingletonFactory<MatchThread>.SInstance; }
        }

        void Initialize()
        {
            if (_isInit == false)
            {
                lock (_lockRoot)
                {
                    if (_isInit == false)
                    {
                        _matchClient = new MatchClient();
                        this._poolMatch = new NBThreadPool(MATCHConcurrency);
                        InitMatchPool();
                        _isInit = true;
                    }
                }
            }
        }
        void InitMatchPool()
        {
            s_dicMatchPool.Clear();
            s_noCacheMatchTypes.Clear();
            s_DbMatchTypes.Clear();
            s_dicMatchPool[EnumMatchType.None] = new NBThreadPool(10);
            s_dicMatchPool[EnumMatchType.Tour] = new NBThreadPool(10);
            s_dicMatchPool[EnumMatchType.Ladder] = new NBThreadPool(10);
            s_dicMatchPool[EnumMatchType.Dailycup] = new NBThreadPool(10);
            s_dicMatchPool[EnumMatchType.League] = new NBThreadPool(10);
            s_dicMatchPool[EnumMatchType.Crowd] = new NBThreadPool(15);
            s_dicMatchPool[EnumMatchType.Champion] = new NBThreadPool(15);

            s_noCacheMatchTypes[EnumMatchType.Dailycup] = 0;
            //s_noCacheMatchTypes[EnumMatchType.League] = 0;
            s_noCacheMatchTypes[EnumMatchType.Champion] = 0;

            s_DbMatchTypes[EnumMatchType.Dailycup] = 0;
            //s_DbMatchTypes[EnumMatchType.League] = 0;
            //s_DbMatchTypes[EnumMatchType.Friend] = 0;
            //s_DbMatchTypes[EnumMatchType.Ladder] = 0;
            //s_DbMatchTypes[EnumMatchType.PlayerKill] = 0;
            //s_DbMatchTypes[EnumMatchType.Arena] = 0;
            s_DbMatchTypes[EnumMatchType.CrossLadder] = 0;
            s_DbMatchTypes[EnumMatchType.Champion] = 0;

        }

        public void CreateMatch(BaseMatchData stateObj)
        {
            Handle(stateObj, null);
        }

        public MessageCode CreateMatchAsyn(BaseMatchData stateObj, MatchCallback callback)
        {
            return CreateMatchAsyn(stateObj, (a, b) => callback(a));
        }
        public MessageCode CreateMatchAsyn(BaseMatchData stateObj, MatchStateCallback callback, object matchState = null)
        {
            if (null == stateObj || stateObj.Home == null || stateObj.Away == null)
            {
                return MessageCode.MatchStateObjisNull;
            }
            stateObj.ErrorCode = (int)MessageCode.MatchWait;
            stateObj.RowTime = DateTime.Now;
            MemcachedFactory.MatchClient.Set<BaseMatchData>(stateObj.MatchId, stateObj);
            //_poolMatch.Add(() => Handle(stateObj,callback));
            PushMatch(stateObj, callback, matchState);
            return MessageCode.Success;
        }

        public MessageCode Query(Guid battleId, out BaseMatchData matchData)
        {
            matchData = null;
            matchData = MemcachedFactory.MatchClient.Get<BaseMatchData>(battleId.ToString());
            if (null != matchData)
                return (MessageCode)matchData.ErrorCode;
            matchData = Load(battleId);
            if (null == matchData)
                return MessageCode.MatchMiss;
            matchData.ErrorCode = (int)MessageCode.Success;
            MemcachedFactory.MatchClient.Set<BaseMatchData>(battleId.ToString(), matchData);
            return MessageCode.Success;
        }


        #endregion

        #region Handle
        BaseMatchData Load(Guid matchId)
        {
            return null;
        }

        void PushMatch(BaseMatchData matchData, MatchStateCallback callback, object matchState = null)
        {
            EnumMatchType castMatchType = EnumMatchType.None;
            switch ((EnumMatchType)matchData.MatchType)
            {
                case EnumMatchType.Tour:
                case EnumMatchType.TourElite:
                case EnumMatchType.WorldChallenge:
                    castMatchType = EnumMatchType.Tour;
                    break;
                case EnumMatchType.Ladder:
                case EnumMatchType.Dailycup:
                case EnumMatchType.League:
                case EnumMatchType.Champion:
                    castMatchType = (EnumMatchType)matchData.MatchType;
                    break;
                case EnumMatchType.Crowd:
                case EnumMatchType.CrossCrowd:
                case EnumMatchType.Peak:
                case EnumMatchType.CrossPeak:
                case EnumMatchType.GuildWar:
                    castMatchType = EnumMatchType.Crowd;
                    break;
                default:
                    castMatchType = EnumMatchType.None;
                    break;
            }
            NBThreadPool pool = null;
            if (!s_dicMatchPool.TryGetValue(castMatchType, out pool) || null == pool)
                pool = _poolMatch;
            pool.Add(() => Handle(matchData, callback, matchState));
        }
        void Handle(BaseMatchData matchData, MatchStateCallback callback, object matchState = null)
        {
            if (null == matchData || matchData.Home == null || matchData.Away == null)
            {
                matchData.ErrorCode = (int)MessageCode.NbParameterError;
                return;
            }
            HandleMatch(matchData, callback, matchState);
        }

        void HandleMatch(BaseMatchData matchData, MatchStateCallback callback, object matchState = null)
        {
            MatchInput transferMatchEntity = null;
            try
            {
                transferMatchEntity = MatchTransferUtil.BuildTransferMatch(matchData);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("MatchCore:HandleSingle-TransferMatchDataModel", string.Format("MatchType:{0}", matchData.MatchType), ex);
                matchData.ErrorCode = (int)MessageCode.MatchCreateFail;
                return;
            }

            if (null == transferMatchEntity || null == transferMatchEntity.HomeManager || null == transferMatchEntity.AwayManager)
            {
                matchData.ErrorCode = (int)MessageCode.MatchCreateFail;
            }
            else
            {
                byte[] process = null;
                try
                {
                    if (matchData.Home.ManagerId == new Guid("BC214997-FB0B-41D3-A3AC-A58B00B092D8") || matchData.Away.ManagerId == new Guid("BC214997-FB0B-41D3-A3AC-A58B00B092D8"))
                        LogHelper.Insert("LeagueMatch Id1 " + matchData.MatchId, LogType.Info);

                    process = Exec(matchData, transferMatchEntity);

                   
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("MatchCore:HandleSingle-Exec", ex);
                }

                if (matchData.ErrorCode == (int)MessageCode.Success && process != null)
                {
                    try
                    {
                        MemcachedFactory.MatchClient.Set<BaseMatchData>(matchData.MatchId, matchData);
                        if (!s_noCacheMatchTypes.ContainsKey((EnumMatchType)matchData.MatchType))
                            MemcachedFactory.MatchProcessClient.Set<byte[]>(matchData.MatchId, process);
                        if (s_DbMatchTypes.ContainsKey((EnumMatchType)matchData.MatchType))
                            MatchDataCore.Instance.SaveProcess(matchData.MatchId, matchData.MatchType, process, matchData.RowTime);
                        if (callback != null)
                        {
                            matchData.ErrorCode = (int)callback(matchData, matchState);
                            if (matchData.ErrorCode != 0)
                            {
                                LogHelper.Insert(string.Format("match callback fail,matchId:{0},errorCode:{1},home:{2},away:{3}", matchData.MatchId, matchData.ErrorCode, matchData.Home.Name, matchData.Away.Name), LogType.Info);
                            }
                        }
                        //MatchReward
                        if (matchData.ErrorCode == 0 && ShareUtil.IsAppRXYC && CheckRewardMatchType(matchData.MatchType))
                        {
                            Guid mid = (matchData.Home.IsNpc || matchData.Home.IsBot) ? matchData.Away.ManagerId : matchData.Home.ManagerId;
                            string key = string.Concat(mid, ".", matchData.MatchType).ToLower();
                            var data = new DTOMatchRewardState() { MatchId = matchData.MatchId, Coin = -1, Point = -1 };
                            MemcachedFactory.MatchRewardClient.Set(key, data);
                        }
                        return;
                    }
                    catch (Exception ex)
                    {
                        SystemlogMgr.Error("MatchCore:HandleSingle-Save", ex);
                        matchData.ErrorCode = (int)MessageCode.MatchCreateFail;
                    }
                }
                else
                {
                    matchData.ErrorCode = (int)MessageCode.MatchCreateFail;
                }
            }
            MemcachedFactory.MatchClient.Set<BaseMatchData>(matchData.MatchId, matchData);
        }
        public static bool CheckRewardMatchType(int matchType)
        {
            switch (matchType)
            {
                case (int)EnumMatchType.League:
                case (int)EnumMatchType.PlayerKill:
                    return true;
                default:
                    return false;
            }
        }
        #endregion

        #region Tools
        public byte[] Exec(BaseMatchData stateObj, MatchInput matchInput)
        {
            MatchReport resultEntity = null;
            var home = stateObj.Home.ManagerId;
            var away = stateObj.Away.ManagerId;
            
            int i = 0;
            do
            {
                try
                {
                    byte[] process = null;
                    //固定新手引导首场
                    if (ShareUtil.IsAppRXYC && stateObj.IsGuide && stateObj.MatchType == (int)EnumMatchType.PlayerKill)
                        process = MatchReportCache.Instance().GetReport();
                    if (null == process)
                        process = _matchClient.CreateMatch(matchInput);
                    if (process != null && process.Length > 10)
                    {
                        resultEntity = NB.Match.Base.Util.IOUtil.BinRead<MatchReport>(process, 0);
                    }
                    if (resultEntity == null)
                        throw new Exception("the callback MatchStream is empty");
                    else
                    {
                        if (stateObj.NoDraw && resultEntity.HomeScore == resultEntity.AwayScore)
                        {
                            i++;
                        }
                        else
                        {
                            stateObj.ErrorCode = (int)MessageCode.Success;
                            stateObj.Home.Score = resultEntity.HomeScore;
                            stateObj.Away.Score = resultEntity.AwayScore;
                            return process;
                        }
                    }
                }
                catch (Exception ex)
                {
                    i++;

                    if (i >= MATCHRetryTimes)
                    {
                        SystemlogMgr.Error("MatchThread Exec", string.Format("Home:{0},Away:{1},Message:{2},StackTrace:{3}", home, away, ex.Message, ex.StackTrace));
                        //SystemlogMgr.Error("MatchThread Exec", ex);
                        stateObj.ErrorCode = (int)MessageCode.Exception;
                        return null;
                    }
                }
            }
            while (i < MATCHRetryTimes);
            return null;
        }
        #endregion
    }
}
