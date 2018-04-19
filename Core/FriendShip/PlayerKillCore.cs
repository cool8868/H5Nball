using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core;
using Games.NBall.Core.Activity;
using Games.NBall.Core.Item;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Match;
using Games.NBall.Core.Task;
using Games.NBall.Core.Teammember;
using Games.NBall.Core.Turntable;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Activity;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Match;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Match;
using MsEntLibWrapper.Data;
using ManagerUtil = Games.NBall.Core.Manager.ManagerUtil;

namespace Games.NBall.Core.FriendShip
{
    public class PlayerKillCore
    {

        private readonly int _pkMatchWaitTime;
        private readonly int _pkMinLevel;
        private const int _pkStamina = 5;
        #region .ctor
        public PlayerKillCore(int p)
        {
            _pkMatchWaitTime = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.PkMatchWaitTime);
            _pkMinLevel = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.PKMinLevel);
        }
        #endregion

        #region Facade
        public static PlayerKillCore Instance
        {
            get { return SingletonFactory<PlayerKillCore>.SInstance; }
        }

        #region 获取信息

        public PlayerkillInfoResponse GetInfo(Guid managerId)
        {
            var info = InnerGetInfo(managerId);
            var manager = ManagerCore.Instance.GetManager(managerId,true);
            if (manager == null)
                return ResponseHelper.Create<PlayerkillInfoResponse>((int)MessageCode.MissManager);
            if (info.LotteryMatchId != Guid.Empty)
            {
                info.HasLottery = true;
            }
            
            if (DateTime.Now > info.OpponentRefreshTime)
            {
                RefreshOpponent(info,manager.Kpi);
            }
            else if (ShareUtil.CheckBytes(info.OpponentInfo))
            {
                info.Opponents = SerializationHelper.FromByte<List<PlayerKillOpponentEntity>>(info.OpponentInfo);
                if (info.Opponents == null)
                {
                    RefreshOpponent(info,manager.Kpi);
                }
                else if (!info.Opponents.Exists(d => d.HasWin == false))
                {
                    RefreshOpponent(info, manager.Kpi);
                }
            }
            else
            {
                RefreshOpponent(info, manager.Kpi);
            }
            var response = ResponseHelper.CreateSuccess<PlayerkillInfoResponse>();
            info.RemainTimes = 6;//CacheFactory.PlayerKillCache.GetChallengeTimes(manager.Level) - info.RemainTimes + info.BuyTimes;
            var managerExtra = ManagerCore.Instance.GetManagerExtra(managerId);
            if(managerExtra!=null)
                info.ResumeStaminaTimeTick = ShareUtil.GetTimeTick(managerExtra.ResumeStaminaTime.AddSeconds(ManagerCore.Instance.GetResumeStaminaTimeConfig(manager.VipLevel)));

            info.OpponentRefreshTimeTick = ShareUtil.GetTimeTick(info.OpponentRefreshTime);
            info.TotalPoint = CacheFactory.PlayerKillCache.GetTotalPoint(manager.VipLevel);
            response.Data = info;
            return response;
        }

        void RefreshOpponent(PlayerkillInfoEntity info,int kpi)
        {
            LogHelper.Insert("refresh opponent:"+info.ManagerId,LogType.Info);
            var opponents = CacheFactory.PlayerKillCache.GetOpponents(info.ManagerId, kpi);
            if (opponents != null)
            {
                info.Opponents=new List<PlayerKillOpponentEntity>(opponents.Count);
                foreach (var entity in opponents)
                {
                    info.Opponents.Add(entity.Clone());
                }
            }
            info.OpponentInfo = SerializationHelper.ToByte(info.Opponents);
            info.OpponentRefreshTime = DateTime.Now.AddHours(2);
            PlayerkillInfoMgr.Update(info);
        }
        #endregion

        #region 获取对手刷新
        public PlayerKillOpponentResponse GetOpponents(Guid managerId, string opponentName)
        {
            var info = InnerGetInfo(managerId);
            if (info == null)
                return ResponseHelper.InvalidParameter<PlayerKillOpponentResponse>("managerId");
            var manager = ManagerCore.Instance.GetManager(managerId, true);
            if (manager == null)
                return ResponseHelper.InvalidParameter<PlayerKillOpponentResponse>("managerId");

            if (manager.Coin < 200)
                return ResponseHelper.Create<PlayerKillOpponentResponse>(MessageCode.LackofCoin);

            var response = ResponseHelper.CreateSuccess<PlayerKillOpponentResponse>();
            if (!string.IsNullOrEmpty(opponentName))
            {
                var opponent = PlayerkillInfoMgr.GetOpponentByName(opponentName, 1);
                if (opponent == null)
                {
                    return ResponseHelper.Create<PlayerKillOpponentResponse>(MessageCode.MissManager);
                }
                else if (opponent.Level < _pkMinLevel)
                {
                    return ResponseHelper.Create<PlayerKillOpponentResponse>(MessageCode.PlayerKillMinLevel);
                }
                PlayerKillOpponentListEntity opponentList = new PlayerKillOpponentListEntity();
                opponentList.Opponents = new List<PlayerKillOpponentEntity>(1);
               
                opponent.FormationId= TeammemberCore.Instance.GetSolution(opponent.ManagerId).FormationId;
                opponentList.Opponents.Add(opponent);
                response.Data = opponentList;
            }
            else
            {
                RefreshOpponent(info,manager.Kpi);
                response.Data = new PlayerKillOpponentListEntity();
                response.Data.Opponents = info.Opponents;
            }
            if (response.Data != null && response.Data.Opponents.Count > 0)
            {
                //扣除金币
                ManagerCore.Instance.CostCoin(manager, 200, EnumCoinConsumeSourceType.PkMatchRefresh,ShareUtil.GenerateComb().ToString());
            }
            response.Data.OpponentRefreshTimeTick = ShareUtil.GetTimeTick(info.OpponentRefreshTime);
            return response;
        }

        #endregion

        #region 打比赛

        public MatchCreateResponse Fight(Guid managerId, Guid awayId, bool hasTask)
        {
            if (managerId == awayId)
                return ResponseHelper.Create<MatchCreateResponse>(MessageCode.PlayerKillNoSelf);

            var code = MatchCdHandler.CheckCd(managerId, EnumMatchType.PlayerKill);
            if (code != MessageCode.Success)
            {
                return ResponseHelper.Create<MatchCreateResponse>(code);
            }
            return doFight(managerId, awayId, 1, hasTask);
        }

        private MatchCreateResponse doFight(Guid managerId, Guid awayId, long revengeRecordId, bool hasTask)
        {
            var info = InnerGetInfo(managerId);
            if (info == null)
                return ResponseHelper.InvalidParameter<MatchCreateResponse>("info");
            info.Opponents = SerializationHelper.FromByte<List<PlayerKillOpponentEntity>>(info.OpponentInfo);
            if (info.Opponents == null)
            {
                return ResponseHelper.InvalidParameter<MatchCreateResponse>("opponents");
            }
            var awayOpp = info.Opponents.Find(d => d.ManagerId == awayId);
            if (awayOpp == null)
            {
                return ResponseHelper.Create<MatchCreateResponse>(MessageCode.PlayerKillNoAway);
            }
            if (awayOpp.HasWin)
            {
                return ResponseHelper.Create<MatchCreateResponse>(MessageCode.PlayerKillWinOver);
            }
            var managerex = ManagerCore.Instance.GetManagerExtra(managerId);
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null || managerex == null)
                return ResponseHelper.Create<MatchCreateResponse>((int)MessageCode.MissManager);
            if(managerex.Stamina<_pkStamina)
                return ResponseHelper.Create<MatchCreateResponse>(MessageCode.LeagueStaminaNotEnough);
            var matchHome = new MatchManagerInfo(manager.Idx, "", false, 20);
            //构建客队
            var matchAway = new MatchManagerInfo(awayId, false, false);
            //创建一场比赛
            Guid matchId = ShareUtil.GenerateComb();
            var matchData = new BaseMatchData((int)EnumMatchType.PlayerKill, matchId, matchHome, matchAway);
            matchData.ErrorCode = (int)MessageCode.MatchWait;
            matchData.HasTask = hasTask;

            var taskListShow = TaskCore.Instance.GetTaskListShow(managerId);
            if (taskListShow.Tasks.Find(t => t.TaskId == 1001) != null)
                matchData.IsGuide = true;
            
            MemcachedFactory.MatchClient.Set(matchData.MatchId, matchData);
            MatchCore.CreateMatch(matchData);

            if (matchData.ErrorCode == (int) MessageCode.Success)
            {
                MatchCallback(matchData, revengeRecordId,awayOpp,info);
                
            }
            else{
                return ResponseHelper.Create<MatchCreateResponse>(matchData.ErrorCode);
            }

            var response = ResponseHelper.MatchCreateResponse(matchId);
            var managerExtra = ManagerCore.Instance.GetManagerExtra(managerId);
            response.Data.Stamina = managerExtra.Stamina;
            return response;
        }

        #endregion

        #region 获取比赛信息

        public PlayerkillMatchResponse GetMatchResult(Guid managerId, Guid matchId)
        {
            var match = PlayerkillMatchMgr.GetById(matchId);
            if (match == null)
                return ResponseHelper.InvalidParameter<PlayerkillMatchResponse>("match is null");
            if (match.HomeId != managerId)
                return ResponseHelper.InvalidParameter<PlayerkillMatchResponse>("HomeId!=managerId");
            if (match.Status == 0)
            {
                match.PrizeItemCode = 0;
            }
            var response = ResponseHelper.CreateSuccess<PlayerkillMatchResponse>();
            response.Data = match;
            var popMsg = MemcachedFactory.MatchPopClient.Get<List<PopMessageEntity>>(matchId);
            if (popMsg != null)
            {
                MemcachedFactory.MatchPopClient.Delete(matchId);
                response.Data.PopMsg = popMsg;
            }
            //是否有vip效果
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager != null)
            {
                var winType = ShareUtil.CalWinType(match.HomeScore, match.AwayScore); 
                var prize = CacheFactory.PlayerKillCache.GetPrize(winType);
                var vipRate = CacheFactory.VipdicCache.GetEffectValue(manager.VipLevel, EnumVipEffect.PkOrLeagueExpPlus);
                response.Data.VipExp = prize.Exp*vipRate/100;
            }
            return response;
        }

        #endregion

        #region 比赛处理

        public MessageCode MatchCallback(BaseMatchData matchData, long revengeRecordId,PlayerKillOpponentEntity awayOpp,PlayerkillInfoEntity info)
        {
            var pkmatchData = matchData;
            if (pkmatchData == null || pkmatchData.ErrorCode != (int)MessageCode.Success)
                return MessageCode.MatchCreateFail;
            bool isrevenge = false;
            Guid lotteryMatchId = Guid.Empty;

            var winType = ShareUtil.CalWinType(pkmatchData.Home.Score, pkmatchData.Away.Score);

            int win = 0;
            int lose = 0;
            int draw = 0;
            int prizeExp = 0;
            int prizeCoin = 0;
            if (!isrevenge)
            {
                if (winType == EnumWinType.Win)
                {
                    win = 1;
                }
                if (winType == EnumWinType.Lose)
                {
                    lose = 1;
                }
                if (winType == EnumWinType.Draw)
                {
                    draw = 1;
                }
                var prize = CacheFactory.PlayerKillCache.GetPrize(winType);
                prizeCoin = prize.Coin;
                prizeExp = prize.Exp;
            }
            int prizeItemCode = 0;
            string prizeItemString = "";
            var manager = ManagerCore.Instance.GetManager(pkmatchData.Home.ManagerId);
            var managerex = ManagerCore.Instance.GetManagerExtra(pkmatchData.Home.ManagerId);

            var subtype = 1;
            if (manager.Level%10 == 0)
                subtype = manager.Level/10;
            else
                subtype = manager.Level / 10 + 1;

            int matchTimes = 0;
            PlayerkillInfoMgr.GetMatchTimes(manager.Idx, ref matchTimes);

            //if (winType == EnumWinType.Win)
            //{
            var lotteryEntity = CacheFactory.LotteryCache.LotteryFive(EnumLotteryType.PlayerKill, subtype);
            int pointCount = 0;
            if (lotteryEntity != null)
            {
                //第一场友谊赛固定获得5钻石
                if (matchTimes == 0)
                {
                    prizeItemCode = 810001;
                    pointCount = 5;
                    lotteryMatchId = pkmatchData.MatchId;
                }
                else
                {
                    if (winType == EnumWinType.Win)
                    {
                        prizeItemCode = lotteryEntity.PrizeItemCode;
                        prizeItemString = lotteryEntity.ItemString;
                        if (prizeItemCode == 810001)
                        {
                            var pointConfig = CacheFactory.PlayerKillCache.GetPointConfig(manager.VipLevel);
                            if (pointConfig != null && (info.DayPoint + pointCount) < pointConfig.TotalPoint)
                            {
                                pointCount = pointConfig.PrizePoint;
                                int point = pointCount;
                                //欧洲杯狂欢
                                ActivityExThread.Instance.EuropeCarnival(5, ref point);
                                info.DayPoint = info.DayPoint + point;
                                info.DayPoint = info.DayPoint > pointConfig.TotalPoint ? pointConfig.TotalPoint : info.DayPoint;
                            }
                            else
                            {
                                prizeItemCode = 910001;
                                pointCount = 5;
                            }
                        }
                        else if (prizeItemCode == 910001)
                        {
                            prizeItemCode = 910001;
                            pointCount = 5;
                        }
                        var itemcode = ActivityExThread.Instance.SummerGiftBag(4);
                        if (itemcode > 0)
                            prizeItemCode = itemcode;
                        else
                        {
                            itemcode = ActivityExThread.Instance.MidAutumnActivity(4, info.SpecialItemNumber);
                            if (itemcode > 0)
                            {
                                info.SpecialItemNumber++;
                                prizeItemCode = itemcode;
                            }
                        }
                        lotteryMatchId = pkmatchData.MatchId;
                    }
                }
            }
            //}
           
            

            //LogHelper.Insert("友谊赛比赛结果处理请求：比赛id"+lotteryMatchId+",ManagerId:" + pkmatchData.Home.ManagerId.ToString() + " ,对手Id:" + pkmatchData.Away.ManagerId.ToString(), LogType.Info);

            double totalPlusRate = 0;
            //是否有vip效果
            var vipRate = CacheFactory.VipdicCache.GetEffectValue(manager.VipLevel, EnumVipEffect.PkOrLeagueExpPlus);
            totalPlusRate += vipRate / 100.00;
            //是否有增加经验的Buff
            var buffExp = BuffPoolCore.Instance().GetBuffValue(manager.Idx, EnumBuffCode.PkMatchExp, true, false);
            NbManagerbuffpoolEntity buffExpEntity = null;
            if (buffExp != null)
            {
                if (buffExp.SrcList != null && buffExp.SrcList.Count > 0)
                {
                    buffExpEntity = NbManagerbuffpoolMgr.GetById(buffExp.SrcList[0].Idx);
                    if (buffExpEntity != null && buffExpEntity.RemainTimes > 0)
                    {
                        totalPlusRate += buffExp.Percent;
                    }
                }
            }
            prizeExp = (int)(prizeExp * (1 + totalPlusRate));
            
            if (matchData.Home.Score > matchData.Away.Score)
            {
                awayOpp.HasWin = true;
                info.OpponentInfo = SerializationHelper.ToByte(info.Opponents);
                //PlayerkillInfoMgr.Update(info);
            }

            //欧洲杯狂欢
            ActivityExThread.Instance.EuropeCarnival(1, ref prizeExp);

            LogHelper.Insert(string.Format("revengeId:{0},score1:{1},score2:{2},haswin:{3}", revengeRecordId, matchData.Home.Score ,matchData.Away.Score,awayOpp.HasWin),LogType.Info);
            OnlineCore.Instance.CalIndulgePrize(manager, ref prizeExp, ref prizeCoin);
            ManagerUtil.AddManagerData(manager, prizeExp, prizeCoin, 0, EnumCoinChargeSourceType.PlayerKillPrize, pkmatchData.MatchId.ToString());
            long outRevengeRecordId = 0;
            //扣除行动力
            var code = ManagerCore.Instance.SubStamina(managerex, _pkStamina, manager.Level,manager.VipLevel);
            if (code != MessageCode.Success)
                return code;
            
            code = SaveMatch(manager, managerex, pkmatchData, lotteryMatchId, win, lose, draw, prizeExp, prizeCoin,
                                 prizeItemCode, prizeItemString, isrevenge, revengeRecordId, ref outRevengeRecordId,info,pointCount);
            //统计使用的行动力
            StatisticKpiMgr.UpdateSame(ShareUtil.ZoneId, DateTime.Now.Date, 0, _pkStamina, 0, 0);
            if (code == MessageCode.Success)
            {
                //更新祝福Buff剩余场次数
                if (buffExpEntity != null && buffExpEntity.RemainTimes > 0)
                {
                    buffExpEntity.RemainTimes--;
                    NbManagerbuffpoolMgr.Update(buffExpEntity);
                }
                //记录比赛数据
                MatchCore.SaveMatchStat(pkmatchData.Home.ManagerId, EnumMatchType.PlayerKill, pkmatchData.Home.Score, pkmatchData.Away.Score, pkmatchData.Home.Score);
                //记录成就相关数据
                AchievementTaskCore.Instance.UpdatePkMatchGoals(manager.Idx, pkmatchData.Home.Score);
                TaskHandler.Instance.PkOrFriendMatchCount(manager.Idx);
                var popList = ManagerUtil.SaveManagerAfter(manager, false);
                var taskPop = TaskHandler.Instance.PlayerKillFight(manager.Idx, (int)winType);
                if (matchData.HasTask)
                {
                    //var taskPop = TaskHandler.Instance.PlayerKillFight(manager.Idx, (int)winType);
                    if (taskPop != null && taskPop.Count > 0)
                    {
                        if (popList == null)
                        {
                            popList = taskPop;
                        }
                        else
                        {
                            popList.AddRange(taskPop);
                        }
                    }
                }

                


                MemcachedFactory.MatchPopClient.Set(matchData.MatchId, popList);
            }
            return code;
        }

       

        #endregion

        #region 比赛抽卡
        /// <summary>
        /// 比赛抽卡
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public TourLotteryResponse Lottery(Guid managerId, Guid matchId)
        {
            var pkMatch = PlayerkillMatchMgr.GetById(matchId);
            if (pkMatch == null || pkMatch.HomeId != managerId)
                return ResponseHelper.InvalidParameter<TourLotteryResponse>();
            var pkInfo = PlayerkillInfoMgr.GetById(managerId);
            if (pkInfo == null || pkInfo.LotteryMatchId != matchId)
                return ResponseHelper.InvalidParameter<TourLotteryResponse>();
            //var itemBinding = ShareUtil.GetItemBinding(EnumItemPrizeType.PlayerKillPrize, ManagerUtil.GetVipLevel(managerId));

            var itemBinding = true;

            //处理抽奖
            if (pkMatch.Status == -1)
            {
                var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.FriendShip);

                var itemDic = CacheFactory.ItemsdicCache.GetItem(pkMatch.PrizeItemCode);
                if (itemDic == null)
                    return ResponseHelper.InvalidParameter<TourLotteryResponse>();
                MailBuilder mail = null;
                EquipmentProperty equipmentProperty = null;
                MessageCode result = MessageCode.Success;
                var itemCount = 1;
                int point = 0;
                if (itemDic.ItemType == (int)EnumItemType.Equipment)
                {
                    equipmentProperty = CacheFactory.EquipmentCache.RandomEquipmentProperty(itemDic.LinkId);
                    result = package.AddEquipment(pkMatch.PrizeItemCode, itemBinding,false, equipmentProperty);
                }
                else if (itemDic.ItemType == (int)EnumItemType.FriendshipPoint)//友情点
                {
                    itemCount = itemDic.FourthType;
                    var manager = ManagerCore.Instance.GetManager(managerId);
                    ManagerCore.Instance.UpdateFriendShipPoint(manager, itemDic.FourthType, EnumActionType.Add);
                }
                else if (itemDic.ItemType == (int)EnumItemType.Point)//钻石
                {
                    itemCount = pkMatch.PrizeItemCount;
                    point = itemCount;
                    //欧洲杯狂欢
                    ActivityExThread.Instance.EuropeCarnival(5, ref point);
                }
                else
                {
                    result = package.AddItem(pkMatch.PrizeItemCode, itemBinding,false);
                }
                if (result != MessageCode.Success)
                {
                    package = null;
                    mail = new MailBuilder(managerId, pkMatch.PrizeItemCode, itemBinding, equipmentProperty, pkMatch.AwayName);
                }

                point += 1;//场次获胜额外获得1钻石

                //var code = SaveLottery(pkMatch.Idx, pkMatch.HomeId, package, mail, (int)MessageCode.TourLotteryRepeat);
                var code = SaveLottery(pkMatch.Idx, pkMatch.HomeId,(int)MessageCode.TourLotteryRepeat,package,mail,point);
                if (code == MessageCode.Success)
                {
                    var response = ResponseHelper.CreateSuccess<TourLotteryResponse>();
                    response.Data = new TourLotteryEntity();
                    response.Data.ItemCode = pkMatch.PrizeItemCode;
                    response.Data.EquipmentProperty = equipmentProperty;
                    response.Data.IsBinding = itemBinding;
                    response.Data.ItemCount = 1;
                    if (itemDic.ItemType == (int) EnumItemType.Point) //钻石 友情点
                        response.Data.ItemCount = point -1;
                    else if (itemDic.ItemType == (int) EnumItemType.FriendshipPoint)
                        response.Data.ItemCount = itemCount;

                    //奥运金牌掉落
                    response.Data.OlympicTheGoldMedalId = OlympicCore.Instance.GetOlympicTheGoldMedal(
                        managerId, EnumOlympicGeyType.FriendMatch);
                    return response;
                }
                else
                {
                    return ResponseHelper.Create<TourLotteryResponse>(code);
                }
            }
            else
            {
                return ResponseHelper.Create<TourLotteryResponse>(MessageCode.TourLotteryRepeat);
            }
        }

        #endregion

        #region 更新对手
        public MessageCode JobUpdateOpponent()
        {
            CacheFactory.PlayerKillCache.UpdateOpponent();
            return MessageCode.Success;
        }

        #endregion
        #endregion

        #region 获取更新经理信息
        public PlayerkillInfoEntity InnerGetInfo(Guid managerId)
        {
            var info = PlayerkillInfoMgr.GetById(managerId);
            if (info == null)
            {
                info = new PlayerkillInfoEntity();
                info.ManagerId = managerId;
                info.RecordDate = DateTime.Now.Date;
                info.RowTime = DateTime.Now;
                info.UpdateTime = DateTime.Now;
                info.RemainByTimes = 1000;
                info.RemainTimes = 0;
                info.LotteryMatchId = Guid.Empty;
                info.OpponentInfo = new byte[0];
                info.OpponentRefreshTime = DateTime.Now;
                info.SpecialItemNumber = 0;
                PlayerkillInfoMgr.Insert(info);
            }
            else if (info.RecordDate != DateTime.Today)
            {
                info.RecordDate = DateTime.Today;
                info.RemainByTimes = 1000;
                info.RemainTimes = 0;
                info.DayWinTimes = 0;
                info.BuyTimes = 0;
                info.UpdateTime = DateTime.Now;
                info.DayPoint = 0;
                info.SpecialItemNumber = 0;
                PlayerkillInfoMgr.Update(info);
            }
            else
            {
                if (info.RemainTimes < 0)
                    info.RemainTimes = 0;
                if (info.RemainByTimes < 0)
                    info.RemainByTimes = 0;
            }
            return info;
        }
        #endregion

        #region SaveMatch
        MessageCode SaveMatch(NbManagerEntity manager, NbManagerextraEntity managerex, BaseMatchData pkmatchData,
            Guid lotteryMatchId, int win, int lose, int draw, int prizeExp, int prizeCoin, int prizeItemCode, string prizeItemString
            , bool isrevenge,long revengeRecordId, ref long outRevengeRecordId,PlayerkillInfoEntity info,int pointCount)
        {
            if (manager == null)
            {
                return MessageCode.NbUpdateFail;
            }
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = Tran_SaveMatch(transactionManager.TransactionObject, manager, managerex, pkmatchData,
                        lotteryMatchId, win, lose, draw, prizeExp, prizeCoin, prizeItemCode, prizeItemString
            , isrevenge,revengeRecordId, ref outRevengeRecordId,info,pointCount);

                    if (messageCode == ShareUtil.SuccessCode)
                    {
                        transactionManager.Commit();
                    }
                    else
                    {
                        transactionManager.Rollback();
                    }
                    return messageCode;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SavePlayerKillMatch", ex);
                return MessageCode.Exception;
            }
        }

        MessageCode Tran_SaveMatch(DbTransaction transaction, NbManagerEntity manager, NbManagerextraEntity managerex, BaseMatchData pkmatchData,
            Guid lotteryMatchId, int win, int lose, int draw, int prizeExp, int prizeCoin, int prizeItemCode, string prizeItemString
            , bool isrevenge, long revengeRecordId, ref long outRevengeRecordId,PlayerkillInfoEntity info, int prizeItemCount)
        {

            if (prizeCoin > 0 || prizeExp > 0)
            {
                if (!ManagerUtil.SaveManagerData(manager, managerex, transaction))
                {
                    return MessageCode.NbUpdateFail;
                }
                else
                {
                    ManagerUtil.SaveManagerAfter(manager, managerex, transaction);
                }
                if (!NbManagerextraMgr.Update(managerex, transaction))
                {
                    return MessageCode.NbUpdateFail;
                }
            }
            PlayerkillInfoMgr.Update(info);
            PlayerkillInfoMgr.SaveFightResult(pkmatchData.Home.ManagerId, manager.Logo, pkmatchData.Away.ManagerId, lotteryMatchId,
                                              win, lose, draw, DateTime.Now,
                                              pkmatchData.MatchId, pkmatchData.Home.Name, pkmatchData.Away.Name,
                                              pkmatchData.Home.Score, pkmatchData.Away.Score, prizeExp, prizeCoin,
                                              prizeItemCode
                                              , prizeItemString, isrevenge, revengeRecordId,prizeItemCount, ref outRevengeRecordId, transaction);

            return MessageCode.Success;
        }
        #endregion

        #region SaveLottery
       // MessageCode SaveLottery(Guid matchId, Guid managerId, ItemPackageFrame package, MailBuilder mail, int lotteryRepeatCode)
        MessageCode SaveLottery(Guid matchId, Guid managerId, int lotteryRepeatCode, ItemPackageFrame package, MailBuilder mail,int point)
        {
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    //var messageCode = Tran_SaveLottery(transactionManager.TransactionObject, matchId, managerId, package, mail, lotteryRepeatCode);
                    var messageCode = Tran_SaveLottery(transactionManager.TransactionObject, matchId, managerId, lotteryRepeatCode,package,mail,point);

                    if (messageCode == ShareUtil.SuccessCode)
                    {
                        transactionManager.Commit();
                    }
                    else
                    {
                        transactionManager.Rollback();
                    }
                    return messageCode;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SavePlayerKillLottery", ex);
                return MessageCode.Exception;
            }
        }
         //MessageCode Tran_SaveLottery(DbTransaction transaction, Guid matchId, Guid managerId, ItemPackageFrame package, MailBuilder mail, int lotteryRepeatCode)
        MessageCode Tran_SaveLottery(DbTransaction transaction, Guid matchId, Guid managerId, int lotteryRepeatCode, ItemPackageFrame package, MailBuilder mail,int point)
        {
            MessageCode msg = MessageCode.Success;
            if (point > 0)
            {
              msg = PayCore.Instance.AddBonus(managerId, point, EnumChargeSourceType.PkMatchPrize,
                    ShareUtil.GenerateComb().ToString(), transaction);
                if (msg != MessageCode.Success)
                    return msg;
            }
            int returnCode = -2;
            PlayerkillInfoMgr.LotterySave(matchId, managerId, lotteryRepeatCode, ref returnCode, transaction);
            if (returnCode != 0)
            {
                return MessageCode.NbUpdateFail;
            }
            if (package != null)
            {
                if (!package.Save(transaction))
                    return MessageCode.NbUpdateFail;
                package.Shadow.Save();
            }
            if (mail != null)
            {
                if (!mail.Save(transaction))
                    return MessageCode.NbUpdateFail;
            }

            return MessageCode.Success;
        }
        #endregion
    }
}
