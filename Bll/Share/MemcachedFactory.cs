using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.Share;

namespace Games.NBall.Bll.Share
{
    public class MemcachedFactory:BaseSingleton
    {
        private MemCacheClient _ladderMatchClient;
        private MemCacheClient _arenaMatchClient;
        private MemCacheClient _ladderHeartClient;
        private MemCacheClient _matchClient;
        private MemCacheClient _matchProcessClient;
        private MemCacheClient _managerClient;
        private MemCacheClient _teammembersClient;
        private MemCacheClient _arenaTeammembersClient;
        private MemCacheClient _transferClient;
        private MemCacheClient _fightinfoClient;
        private MemCacheClient _wchallengeMutexClient;
        private MemCacheClient _friendMatchMutexClient;
        private MemCacheClient _tourMatchMutexClient;
        private MemCacheClient _matchMutexClient;
        private MemCacheClient _crowdMatchClient;
        private MemCacheClient _crowdHeartClient;
        private MemCacheClient _guildWarMatchClient;
        private MemCacheClient _crowdMessageClient;

        private MemCacheClient _championMatchClient;

        private MemCacheClient _openFunctionClient;
        private MemCacheClient _noticeClient;
        private MemCacheClient _solutionClient;
        private MemCacheClient _arenaSolutionClient;
        private MemCacheClient _tourPassRecordClient;
        private MemCacheClient _onlineSessionClient;
        private MemCacheClient _matchPopClient;
        private MemCacheClient _matchRewardClient;
        
        private readonly int _defaultExpireTime = 600;//10分钟
        private readonly int _2HoursTime = 7200;//2小时
        private readonly int _processExpireTime = 600;

        #region .ctor
        public MemcachedFactory(int p)
            : base(p)
        {
            if (ShareUtil.IsCross)
            {
                _2HoursTime = 30;
                _defaultExpireTime = 30;
            }
            _ladderMatchClient = new MemCacheClient("LMH.", _defaultExpireTime);
            _arenaMatchClient = new MemCacheClient("AMH.", _defaultExpireTime);
            _ladderHeartClient = new MemCacheClient("LRID", 120);
            _crowdMatchClient = new MemCacheClient("CRMH.", _defaultExpireTime);
            _matchClient = new MemCacheClient("MCH.", _defaultExpireTime);
            _matchProcessClient = new MemCacheClient("MPS.", _processExpireTime);
            _managerClient = new MemCacheClient("MGR.", _2HoursTime);
            _teammembersClient = new MemCacheClient("TMB.", _2HoursTime);
            _arenaTeammembersClient = new MemCacheClient("ATMB.", _2HoursTime);
            _transferClient = new MemCacheClient("TSF.", _2HoursTime);
            _fightinfoClient = new MemCacheClient("FIO.", _2HoursTime);
            _wchallengeMutexClient = new MemCacheClient("WCM.", 60);//1分钟
            _friendMatchMutexClient = new MemCacheClient("FMM.", 120);//2分钟
            _tourMatchMutexClient = new MemCacheClient("TMM.", 120);//2分钟
            _matchMutexClient = new MemCacheClient("PMM.", 120);//1分钟
            _crowdHeartClient = new MemCacheClient("CRID", 120);
            _crowdMessageClient = new MemCacheClient("CMC.", _processExpireTime);

            _matchPopClient = new MemCacheClient("MPC", _defaultExpireTime);

            _openFunctionClient = new MemCacheClient("OFN.", _defaultExpireTime);
            _noticeClient = new MemCacheClient("NTC.", 300);//5分钟
            _solutionClient = new MemCacheClient("SLT.", _2HoursTime);
            _arenaSolutionClient = new MemCacheClient("ASLT.", _2HoursTime);
            _tourPassRecordClient = new MemCacheClient("TPR.", _2HoursTime);
            _onlineSessionClient = new MemCacheClient("OSN.", _defaultExpireTime);
            _championMatchClient = new MemCacheClient("Cham.", _2HoursTime * 80);
            _guildWarMatchClient = new MemCacheClient("GWM.", 300);
            _matchRewardClient = new MemCacheClient("MRD.", 480);
        }
        #endregion

        #region Facade
        /// <summary>
        /// 天梯赛比赛缓存
        /// </summary>
        public static MemCacheClient LadderMatchClient
        {
            get { return SingletonFactory<MemcachedFactory>.SInstance._ladderMatchClient; }
        }

        /// <summary>
        /// 竞技场比赛缓存
        /// </summary>
        public static MemCacheClient ArenaMatchClient
        {
            get { return SingletonFactory<MemcachedFactory>.SInstance._arenaMatchClient; }
        }

        public static MemCacheClient LadderHeartClient
        {
            get { return SingletonFactory<MemcachedFactory>.SInstance._ladderHeartClient; }
        }
        /// <summary>
        /// 比赛BaseMatchData缓存
        /// </summary>
        public static MemCacheClient MatchClient
        {
            get { return SingletonFactory<MemcachedFactory>.SInstance._matchClient; }
        }

        /// <summary>
        /// 比赛战报缓存
        /// </summary>
        public static MemCacheClient MatchProcessClient
        {
            get { return SingletonFactory<MemcachedFactory>.SInstance._matchProcessClient; }
        }

        public static MemCacheClient ManagerClient
        {
            get { return SingletonFactory<MemcachedFactory>.SInstance._managerClient; }
        }

        public static MemCacheClient TeammembersClient
        {
            get { return SingletonFactory<MemcachedFactory>.SInstance._teammembersClient; }
        }

        public static MemCacheClient ArenaTeammembersClient
        {
            get { return SingletonFactory<MemcachedFactory>.SInstance._arenaTeammembersClient; }
        }

        public static MemCacheClient SolutionClient
        {
            get { return SingletonFactory<MemcachedFactory>.SInstance._solutionClient; }
        }
        public static MemCacheClient ArenaSolutionClient
        {
            get { return SingletonFactory<MemcachedFactory>.SInstance._arenaSolutionClient; }
        }

        public static MemCacheClient TransferClient
        {
            get { return SingletonFactory<MemcachedFactory>.SInstance._transferClient; }
        }

        public static MemCacheClient FightinfoClient
        {
            get { return SingletonFactory<MemcachedFactory>.SInstance._fightinfoClient; }
        }

        public static MemCacheClient WChallengeMutexClient
        {
            get { return SingletonFactory<MemcachedFactory>.SInstance._wchallengeMutexClient; }
        }

        public static MemCacheClient FriendMutexClient
        {
            get { return SingletonFactory<MemcachedFactory>.SInstance._friendMatchMutexClient; }
        }

        public static MemCacheClient OpenFunctionClient
        {
            get { return SingletonFactory<MemcachedFactory>.SInstance._openFunctionClient; }
        }

        public static MemCacheClient NoticeClient
        {
            get { return SingletonFactory<MemcachedFactory>.SInstance._noticeClient; }
        }

        public static MemCacheClient TourPassRecordClient
        {
            get { return SingletonFactory<MemcachedFactory>.SInstance._tourPassRecordClient; }
        }

        public static MemCacheClient OnlineSessionClient
        {
            get { return SingletonFactory<MemcachedFactory>.SInstance._onlineSessionClient; }
        }

        public static MemCacheClient TourMutexClient
        {
            get { return SingletonFactory<MemcachedFactory>.SInstance._tourMatchMutexClient; }
        }

        public static MemCacheClient MatchMutexClient
        {
            get { return SingletonFactory<MemcachedFactory>.SInstance._matchMutexClient; }
        }

        public static MemCacheClient MatchPopClient
        {
            get { return SingletonFactory<MemcachedFactory>.SInstance._matchPopClient; }
        }

        /// <summary>
        /// 群雄逐鹿比赛缓存
        /// </summary>
        public static MemCacheClient CrowdMatchClient
        {
            get { return SingletonFactory<MemcachedFactory>.SInstance._crowdMatchClient; }
        }

        /// <summary>
        /// 群雄逐鹿比赛缓存
        /// </summary>
        public static MemCacheClient CrowdHeartClient
        {
            get { return SingletonFactory<MemcachedFactory>.SInstance._crowdHeartClient; }
        }

        public static MemCacheClient CrowdMessageClient
        {
            get { return SingletonFactory<MemcachedFactory>.SInstance._crowdMessageClient; }
        }

        public static MemCacheClient ChampionMatchClient {

            get { return SingletonFactory<MemcachedFactory>.SInstance._championMatchClient; }
        }

        public static MemCacheClient GuildWarMatchClient
        {
            get { return SingletonFactory<MemcachedFactory>.SInstance._guildWarMatchClient; }
        }

        public static MemCacheClient MatchRewardClient
        {
            get { return SingletonFactory<MemcachedFactory>.SInstance._matchRewardClient; }
        }
        #endregion
    }
}
