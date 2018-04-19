using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Ladder;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Mall;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Match;
using Games.NBall.Dal.Xsd;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Match;
using Games.NBall.Entity.Response.Ladder;
using Games.NBall.Entity.Share;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.CrossLadder
{
    public partial class CrossLadderThread
    {
        #region Fields
        private List<ArenaCondition> _conditions;
        //clearFightlist锁
        private object _clearFightlistLock = new object();

        //分组模板
        private Dictionary<int, int[]> _groupTemplates;

        private System.Timers.Timer timer2;
        private Timer _timer = null;
        private const double ClearFightdicTime = 60000;//清空上一轮比赛池时间，毫秒

        private string _botName = "";
        private NBThreadPool _nbThreadPool;

        private int _ladderProctiveScore; //新手保护分数线
        private int _ladderRegisterScore;
        private bool _needClearFightDic = false;

        

        private LadderMatchMarqueeResponse _matchMarqueeResponse = ResponseHelper.CreateSuccess<LadderMatchMarqueeResponse>();


        /// <summary>
        /// 本轮天梯赛开始时间
        /// </summary>
        public DateTime _startTime;
        /// <summary>
        /// 天梯赛服务状态
        /// 控制同一时间只有一场天梯赛在分组状态
        /// 如前一场天梯赛分组未完成，则当前天梯赛不会进入比赛队列
        /// </summary>
        public EnumLadderStatus _status;

        /// <summary>
        /// 本轮天梯赛参数选手,需要加读写锁.
        /// </summary>
        /// <value>The competitor dic.</value>
        public Dictionary<Guid, CrossladderManagerEntity> CompetitorDic { get; private set; }

        /// <summary>
        /// 经理-比赛对应字典
        /// </summary>
        public Dictionary<Guid, CrossLadderHeartEntity> ManagerFightDic { get; set; }
        //competitor锁
        private object _competitorLock = new object();
        //总玩家数量
        private int _playerNum;
        
        private int _domainId;

        /// <summary>
        /// 最近一次平均等待时间
        /// </summary>
        public int RecentlyAvgWaitSecond = 60;
        #endregion

        #region .ctor
        public CrossLadderThread(int domainId)
        {
            _domainId = domainId;
            Initialize();
        }
        #endregion

        #region Initialize

        private void Initialize()
        {
            _botName = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.BotName);
            _ladderProctiveScore = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.LadderProctiveScore);
            _ladderRegisterScore = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.RegisterLadderScore);
            //初始化分组模板
            InitGroupTemplate();
            CreateLadder();

            #region Init Condition dic

            string condition = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.CrossLadderCondition);
            _conditions = new List<ArenaCondition>();
            if (!string.IsNullOrEmpty(condition))
            {
                string[] tempConditions = condition.Split('|');
                foreach (var s in tempConditions)
                {
                    string[] temp = s.Split(',');
                    _conditions.Add(new ArenaCondition(Convert.ToInt32(temp[0]), Convert.ToInt32(temp[1])));
                }
            }

            #endregion

            _nbThreadPool = new NBThreadPool(5);
            _timer = new Timer {Interval = 5000};
            _timer.Elapsed += new ElapsedEventHandler(CheckStatusJob);
            _timer.Start();
            timer2 = new Timer {Interval = ClearFightdicTime};
            timer2.Elapsed += new ElapsedEventHandler(ClearFightDic);
        }

        #endregion

        #region Facade

        /// <summary>
        /// 报名天梯赛.
        /// </summary>
        /// <returns></returns>
        public MessageCodeResponse Attend(string siteId, Guid managerId)
        {
            if (!IsManagerBusy(managerId))
            {
                if (!CompetitorDic.ContainsKey(managerId))
                {
                    var response = CrossLadderCore.Instance.GetManagerInfo(siteId, managerId);
                    if (response.Code != ShareUtil.SuccessCode)
                    {
                        return ResponseHelper.Create<MessageCodeResponse>(response.Code);
                    }
                    if (response.Data == null)
                    {
                        return ResponseHelper.InvalidParameter<MessageCodeResponse>();
                    }
                    var ladderManager = response.Data;
                    //if (ladderManager.Stamina <= 0)
                    //{
                    //    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.LadderStaminaShortage);
                    //}
                    var kpi = ManagerUtil.GetKpi(managerId, siteId);
                    bool needUpdate = false;
                    if (kpi != ladderManager.Kpi)
                    {
                        ladderManager.Kpi = kpi;
                        needUpdate = true;
                    }
                    if (ladderManager.DomainId != _domainId)
                    {
                        ladderManager.DomainId = _domainId;
                        needUpdate = true;
                    }
                    if (needUpdate)
                    {
                        CrossladderManagerMgr.Update(ladderManager);
                    }
                    response.Data.ShowName = ShareUtil.GetCrossManagerNameByZoneId(siteId, response.Data.Name);
                    //锁住
                    lock (_competitorLock)
                    {
                        if (_playerNum == 0)
                            _startTime = DateTime.Now;

                        CompetitorDic.Add(managerId, response.Data);
                        _playerNum++;
                    }
                }
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.Success);
            }
            else
            {
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.LadderBusy);
            }
        }

        public MessageCode HookAttend(CrossladderHookEntity entity)
        {
            if (!IsManagerBusy(entity.ManagerId))
            {
                if (!CompetitorDic.ContainsKey(entity.ManagerId))
                {
                    entity.LadderManager.UpdateTime = DateTime.Now;
                    //锁住
                    lock (_competitorLock)
                    {
                        if (_playerNum == 0)
                            _startTime = DateTime.Now;

                        CompetitorDic.Add(entity.ManagerId, entity.LadderManager);
                        _playerNum++;
                    }
                }
                return MessageCode.Success;
            }
            else
            {
                return MessageCode.LadderBusy;
            }
        }


        /// <summary>
        /// 退出天梯赛.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        public MessageCodeResponse Leave(Guid managerId)
        {
            if (!IsManagerBusy(managerId))
            {
                if (CompetitorDic.ContainsKey(managerId))
                {
                    lock (_competitorLock)
                    {
                        _playerNum--;
                        CompetitorDic.Remove(managerId);
                    }
                    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.Success);
                }
                else
                {
                    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbParameterError);
                }
            }
            else
            {
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.LadderCountdown);
            }
        }

        /// <summary>
        /// 天梯赛状态轮询.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        public CrossLadderHeartResponse Heart(Guid managerId)
        {
            CrossLadderHeartResponse response;
            if (CompetitorDic.ContainsKey(managerId))
            {
                response = ResponseHelper.CreateSuccess<CrossLadderHeartResponse>();
            }
            else if (ManagerFightDic != null && ManagerFightDic.ContainsKey(managerId))
            {
                var heartEntity = ManagerFightDic[managerId];

                if (heartEntity == null)
                {
                    if (_status == EnumLadderStatus.Grouping)
                    {
                        response = ResponseHelper.Create<CrossLadderHeartResponse>(MessageCode.Success);
                    }
                    else
                    {
                        response = ResponseHelper.Create<CrossLadderHeartResponse>(MessageCode.Success);
                    }
                }
                else
                {
                    response = ResponseHelper.Create<CrossLadderHeartResponse>(MessageCode.Success);
                    response.Data = heartEntity;
                }
            }
            else
            {
                var heartEntity = MemcachedFactory.LadderHeartClient.Get<CrossLadderHeartEntity>(managerId);
                if (heartEntity==null)
                {
                    return ResponseHelper.Create<CrossLadderHeartResponse>(MessageCode.Success);
                }
                else
                {
                    response = ResponseHelper.Create<CrossLadderHeartResponse>(MessageCode.Success);
                    response.Data = heartEntity;
                }
            }
            if (response.Data == null)
                response.Data = new CrossLadderHeartEntity();
            if (RecentlyAvgWaitSecond > 60)
                response.Data.AvgWaitTime = 60;
            else
            {
                response.Data.AvgWaitTime = RecentlyAvgWaitSecond;
            }

            return response;
        }

        #endregion

        public LadderMatchMarqueeResponse GetMatchMarqueeResponse()
        {
            return _matchMarqueeResponse;
        }

        #region ClearFightDic
        void ClearFightDic(object sender, ElapsedEventArgs e)
        {
            timer2.Stop();
            if (!_needClearFightDic)
                return;
            try
            {
                //定时将上一轮比赛的经理池清空，以免经理没办法参加下一轮比赛
                ManagerFightDic = new Dictionary<Guid, CrossLadderHeartEntity>();
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CrossLadderThread-ClearFightdic", ex.Message, ex.StackTrace);
            }
        }
        #endregion

        #region CheckStatus
        
        #endregion

        /// <summary>
        /// Creates the ladder.
        /// </summary>
        public void CreateLadder()
        {
            _startTime = DateTime.Now;
            _playerNum = 0;
            CompetitorDic = new Dictionary<Guid, CrossladderManagerEntity>();
        }

        /// <summary>
        /// Determines whether [is manager busy] [the specified manager id].
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns>
        /// 	<c>true</c> if [is manager busy] [the specified manager id]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsManagerBusy(Guid managerId)
        {
            if (ManagerFightDic != null && ManagerFightDic.ContainsKey(managerId))
                return true;
            else
            {
                return false;
            }
        }

        
    }

    /// <summary>
    /// 比较天梯赛经理积分
    /// </summary>
    public class CompareArenaManager : IComparer<CrossladderManagerEntity>
    {
        /// <summary>
        /// 比较两个选手的积分
        /// 判断score，小的排前面
        /// </summary>
        /// <param name="competitorx">The competitorx.</param>
        /// <param name="competitory">The competitory.</param>
        /// <returns></returns>
        public int Compare(CrossladderManagerEntity competitorx, CrossladderManagerEntity competitory)
        {
            if (competitorx == null)
            {
                if (competitory == null)
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater. 
                    return -1;
                }
            }
            else
            {
                // If x is not null...
                //
                if (competitory == null)
                // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    //判断score，小的排前面
                    var scorex = (int)competitorx.Score;
                    var scorey = (int)competitory.Score;
                    if (scorex < scorey)
                    {
                        return -1;
                    }
                    else if (scorex == scorey)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
        }
    }
}
