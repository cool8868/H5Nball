//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Games.Chatting.Models.Sockets;
//using Games.NBall.ChattingFacade;
//using Games.NBall.Entity;
//using Games.NBall.Entity.Enums;
//using Games.NBall.Entity.Enums;
//using Games.NBall.Entity.NBall.Custom;

//namespace Games.NBall.Bll.Share
//{
//    /// <summary>
//    /// 从聊天推送消息
//    /// </summary>
//    public class ChatHelper
//    {
//        static readonly ChattingProxy _proxy = new ChattingProxy();

//        #region Pop
//        public static void SendNewMailPop(Guid managerId)
//        {
//            SendPop(managerId, EnumPopType.NewMail, "");
//        }



//        public static void SendAuctionSaleSuccessPop(Guid managerId, int itemCode,int currency,EnumCurrencyType currencyType)
//        {
//            string content = string.Format("AI,{0}|C,{1}|AT,{2}", itemCode, currency, (int)currencyType);
//            SendPop(managerId, EnumPopType.AuctionSaleSuccess, content);
//        }

//        public static void SendAuctionOverPop(Guid managerId, int itemCode)
//        {
//            string content = string.Format("AI,{0}", itemCode);
//            SendPop(managerId, EnumPopType.AuctionOver, content);
//        }

//        public static PopMessageEntity SendTaskFinishPop(Guid managerId, int taskId,bool sendByChat)
//        {
//            string content = string.Format("RT,{0}", taskId);
//            if (sendByChat)
//            {
//                SendPop(managerId, EnumPopType.TaskFinish, content);
//                return null;
//            }
//            else
//            {
//                return BuildPop(EnumPopType.TaskFinish, content);
//            }
//        }

//        public static PopMessageEntity SendTaskProgressPop(Guid managerId, int taskId, int curTimes, bool sendByChat)
//        {
//            string content = string.Format("RT,{0}|T,{1}", taskId,curTimes);
//            if (sendByChat)
//            {
//                SendPop(managerId, EnumPopType.TaskProgress, content);
//                return null;
//            }
//            else
//            {
//                return BuildPop(EnumPopType.TaskProgress, content);
//            }
//        }

//        public static PopMessageEntity SendOpenFuncPop(NbManagerEntity manager, bool sendByChat)
//        {
//            if (manager.OpenFuncs != null && manager.OpenFuncs.Count > 0)
//            {
//                foreach (var openFunc in manager.OpenFuncs)
//                {
//                    string content = string.Format("F,{0}", openFunc);
//                    if (sendByChat)
//                    {
//                        SendPop(manager.Idx, EnumPopType.OpenFunction, content);
//                        return null;
//                    }
//                    else
//                    {
//                        return BuildPop(EnumPopType.OpenFunction, content);
//                    }
                    
//                }
//            }
//            return null;
//        }

//        public static void SendOpenLevelGiftPop(NbManagerEntity manager)
//        {
//            if (manager.OpenLevelGift)
//            {
//                SendPop(manager.Idx, EnumPopType.OpenLevelGift, "");
//            }
//        }

//        public static PopMessageEntity SendOpenTaskPop(NbManagerEntity manager, bool sendByChat)
//        {
//            if (manager!=null && manager.HasOpenTask)
//            {
//                if (sendByChat)
//                {
//                    SendPop(manager.Idx, EnumPopType.OpenTask, "");
//                }
//                else
//                {
//                    return BuildPop(EnumPopType.OpenTask, "");
//                }
//            }
//            return null;
//        }

//        public static void SendSomePop(Guid managerId, List<PopMessageEntity> pop)
//        {
//            _threadPool.Add(() => doSendSomePop(managerId, pop));
//        }

//        public static void SendByPkPop(Guid targetManagerId,string homeName, int homeScore,int awayScore,long revengeRecordId)
//        {
//            string content = string.Format("N,{0}|S,{1}:{2}|I,{3}", homeName,homeScore,awayScore,revengeRecordId);
//            SendPop(targetManagerId,EnumPopType.ByPkPop, content);
//        }

//        public static void SendByRevengePop(Guid targetManagerId, string homeName, int homeScore, int awayScore)
//        {
//            string content = string.Format("N,{0}|S,{1}:{2}", homeName, homeScore, awayScore);
//            SendPop(targetManagerId, EnumPopType.ByRevengePop, content);
//        }

//        public static string BuildCrowdMatch(EnumWinType winType,  string awayName, int homeScore, int awayScore)
//        {
//            switch (winType)
//            {
//                case EnumWinType.Win:
//                    return string.Format("{3}@H,{0}|A,{1}|N,{2}", homeScore, awayScore, awayName, (int)EnumPopType.CrowdMatchWin);
//                    break;
//                case EnumWinType.Draw:
//                    return string.Format("{3}@H,{0}|A,{1}|N,{2}", homeScore, awayScore, awayName, (int)EnumPopType.CrowdMatchDraw);
//                    break;
//                case EnumWinType.Lose:
//                    return string.Format("{3}@H,{0}|A,{1}|N,{2}", homeScore, awayScore, awayName, (int)EnumPopType.CrowdMatchLose);
//                    break;
//            }
//            return "";
//        }

//        public static void SendCrowdPop(Guid managerId, string msg)
//        {
//            SendPop(managerId, EnumPopType.CrowdPop, msg);
//        }

//        public static string BuildCrowdKill(string killName)
//        {
//            return string.Format("{1}@N,{0}", killName,(int)EnumPopType.CrowdKill);
//        }

//        public static string BuildCrowdByKill(string killName)
//        {
//            return string.Format("{1}@N,{0}", killName, (int)EnumPopType.CrowdByKill);
//        }

//        public static string BuildCrowdMatchPrize(int crowdScore, int coin, int honor)
//        {
//            return string.Format("{3}@S,{0}|C,{1}|H,{2}", crowdScore, coin, honor, (int)EnumPopType.CrowdMatchPrize);
//        }

//        public static string BuildCrowdMoraleUp( int morale)
//        {
//            return string.Format("{1}@M,{0}", morale, (int)EnumPopType.CrowdMoraleUp);
//        }

//        public static string BuildCrowdMoraleDown(int morale)
//        {
//            return string.Format("{1}@M,{0}", morale, (int)EnumPopType.CrowdMoraleDown);
//        }

//        public static string BuildCrowdKillTogether(string killName)
//        {
//            return string.Format("{1}@N,{0}", killName, (int)EnumPopType.CrowdKillTogether);
//        }

//        #region UpdateManagerInfo
//        /// <summary>
//        /// 巡回赛更新
//        /// </summary>
//        /// <param name="manager"></param>
//        /// <param name="managerextra"></param>
//        public static PopMessageEntity SendUpdateManagerInfoPop(NbManagerEntity manager, NbManagerextraEntity managerextra, bool sendByChat, int point = -1, int tourLeagueId = -1)
//        {
//            if (manager == null)
//                return null;
//            int levelupExp = -1;
//            if (manager.IsLevelup)
//                levelupExp = CacheFactory.ManagerDataCache.GetExp(manager.Level);
//            long rs = -1;
//            int stamina = -1;
//            if (managerextra != null)
//            {
//                rs = ShareUtil.GetTimeTick(managerextra.ResumeStaminaTime);
//                stamina = managerextra.Stamina;
//            }
//            return SendUpdateManagerInfoPop(manager.Idx, sendByChat, manager.EXP, manager.Level, levelupExp, manager.IsLevelup, stamina, rs, manager.Coin, point, -1, manager.Sophisticate, tourLeagueId);
//        }

//        /// <summary>
//        /// 更新经理数据
//        /// </summary>
//        /// <param name="manager"></param>
//        public static PopMessageEntity SendUpdateManagerInfoPop(NbManagerEntity manager, bool sendByChat, int point = 0)
//        {
//            if(manager==null)
//                return null;
//            int levelupExp = -1;
//            if (manager.IsLevelup)
//                levelupExp = CacheFactory.ManagerDataCache.GetExp(manager.Level);
//            return SendUpdateManagerInfoPop(manager.Idx, sendByChat, manager.EXP, manager.Level, levelupExp, manager.IsLevelup, -1, -1, manager.Coin, point, -1, manager.Sophisticate, -1);
//        }

//        /// <summary>
//        /// 充值或异步扣点通知
//        /// </summary>
//        /// <param name="point"></param>
//        /// <param name="vipLevel"></param>
//        public static PopMessageEntity SendUpdateManagerInfoPop(Guid managerId, int point, bool sendByChat, int vipLevel = -1)
//        {
//            return SendUpdateManagerInfoPop(managerId, sendByChat, -1, -1, -1, false, -1, -1, -1, point, vipLevel, -1, -1);
//        }

//        static PopMessageEntity SendUpdateManagerInfoPop(Guid managerId, bool sendByChat, int exp = -1, int level = -1, int levelupExp = -1, bool islevelup = false, int stamina = -1, long resumeStamina = -1,
//            int coin = -1, int point = -1, int vipLevel = -1, int sophisticate=-1,int tourLeagueId=-1)
//        {
//            //E(Exp经验)|L(Level等级)|U(LevelupExp下一级经验)|I(IsLevelup是否升级)|C(Coin金币)|P(Point点券)|V(VipLevel vip等级)|S(Stamina体力)|R(RStamina恢复体力时间刻度)
//            string content = string.Format("E,{0}|L,{1}|U,{2}|I,{3}|C,{4}|P,{5}|V,{6}|S,{7}|R,{8}|Y,{9}|T,{10}",
//                exp, level, levelupExp, islevelup?"true":"false", coin, point, vipLevel, stamina,
//                                           resumeStamina,sophisticate,tourLeagueId);
//            if (sendByChat)
//            {
//                SendPop(managerId, EnumPopType.UpdateManagerInfo, content);
//                return null;
//            }
//            else
//            {
//                return BuildPop(EnumPopType.UpdateManagerInfo, content);
//            }
//        }

//        public static void SendUpdateKpi(Guid managerId, int kpi)
//        {
//            string content = string.Format("K,{0}",kpi);
//            SendPop(managerId, EnumPopType.UpdateKpi, content);
//        }

//        public static void SendBindPoint(Guid managerId, int bindPoint)
//        {
//            string content = string.Format("BP,{0}", bindPoint);
//            SendPop(managerId, EnumPopType.BindPoint, content);
//        }
//        #endregion

//        #region 巅峰之战

//        /// <summary>
//        /// 巅峰之战挑战NPC推送
//        /// </summary>
//        /// <param name="homeGoals"></param>
//        /// <param name="awayGoals"></param>
//        /// <param name="integral"></param>
//        /// <param name="coin"></param>
//        /// <param name="reiki"></param>
//        /// <returns></returns>
//        public static string BuildPeakPve(int homeGoals, int awayGoals, int integral, int coin, int reiki)
//        {
//            return string.Format("{0}@H,{1}|A,{2}|I,{3}|C,{4}|L,{5}", (int)EnumPopType.PeakPve, homeGoals, awayGoals, integral, coin, reiki);
//        }

//        /// <summary>
//        /// 巅峰之战被挑战失败推送
//        /// </summary>
//        /// <param name="managerName"></param>
//        /// <param name="homeGoals"></param>
//        /// <param name="awayGoals"></param>
//        /// <param name="portName"></param>
//        /// <param name="managerId"></param>
//        /// <returns></returns>
//        public static string BuildPeakByChallengeLose(string managerName, int homeGoals, int awayGoals, string portName, Guid managerId)
//        {
//            return string.Format("{0}@M,{1}|H,{2}|A,{3}|K,{4}|C,{5}", (int)EnumPopType.PeakByChallengeLose, managerName, homeGoals, awayGoals, portName, managerId);
//        }

//        /// <summary>
//        /// 巅峰之战被挑战胜利推送
//        /// </summary>
//        /// <param name="managerName"></param>
//        /// <param name="homeGoals"></param>
//        /// <param name="awayGoals"></param>
//        /// <param name="portName"></param>
//        /// <param name="managerId"></param>
//        /// <returns></returns>
//        public static string BuildPeakByChallengeWin(string managerName, int homeGoals, int awayGoals, string portName, Guid managerId)
//        {
//            return string.Format("{0}@M,{1}|H,{2}|A,{3}|K,{4}|C,{5}", (int)EnumPopType.PeakByChallengeWin, managerName, homeGoals, awayGoals, portName, managerId);
//        }

//        /// <summary>
//        /// 巅峰之战主队挑战失败推送
//        /// </summary>
//        /// <param name="managerName"></param>
//        /// <param name="homeGoals"></param>
//        /// <param name="awayGoals"></param>
//        /// <returns></returns>
//        public static string BuildPeakChallengeLose(string managerName, int homeGoals, int awayGoals)
//        {
//            return string.Format("{0}@M,{1}|H,{2}|A,{3}", (int)EnumPopType.PeakChallengeLose, managerName, homeGoals, awayGoals);
//        }

//        /// <summary>
//        /// 巅峰之战主队挑战成功推送
//        /// </summary>
//        /// <param name="managerName"></param>
//        /// <param name="homeGoals"></param>
//        /// <param name="awayGoals"></param>
//        /// <returns></returns>
//        public static string BuildPeakChallengeWin(string managerName, int homeGoals, int awayGoals)
//        {
//            return string.Format("{0}@M,{1}|H,{2}|A,{3}", (int)EnumPopType.PeakChallengeWin, managerName, homeGoals, awayGoals);
//        }

//        /// <summary>
//        /// 巅峰之战击杀推送
//        /// </summary>
//        /// <param name="managerName"></param>
//        /// <returns></returns>
//        public static string BuildPeakKill(string managerName)
//        {
//            return string.Format("{0}@M,{1}", (int)EnumPopType.PeakKill, managerName);
//        }

//        /// <summary>
//        /// 巅峰之战被击杀推送
//        /// </summary>
//        /// <param name="managerName"></param>
//        /// <param name="portName"></param>
//        /// <param name="managerId"></param>
//        /// <returns></returns>
//        public static string BuildPeakByKill(string managerName, string portName, Guid managerId)
//        {
//            return string.Format("{0}@M,{1}|K,{2}|C,{3}", (int)EnumPopType.PeakByKill, managerName, portName, managerId);
//        }

//        /// <summary>
//        /// 巅峰之战玉石俱焚推送
//        /// </summary>
//        /// <param name="managerName"></param>
//        /// <returns></returns>
//        public static string BuildPeakCrashAndBurn(string managerName)
//        {
//            return string.Format("{0}@M,{1}", (int)EnumPopType.PeakCrashAndBurn, managerName);
//        }

//        /// <summary>
//        /// 巅峰之战绝杀推送
//        /// </summary>
//        /// <param name="itemName"></param>
//        /// <returns></returns>
//        public static string BuildPeakFinalHit(string itemName)
//        {
//            return string.Format("{0}@ItemName,{1}", (int)EnumPopType.PeakFinalHit, itemName);
//        }

//        #endregion

//        static void doSendSomePop(Guid managerId, List<PopMessageEntity> pop)
//        {
//            try
//            {
//                foreach (var entity in pop)
//                {
//                    _proxy.SendPopChannelMessage(managerId, entity.PopType, entity.MessageText);
//                }
//            }
//            catch (Exception ex)
//            {
//                SystemlogMgr.Error("doSendSomePop", ex);
//            }
//        }

//        static PopMessageEntity BuildPop(EnumPopType popType, string content)
//        {
//            return new PopMessageEntity(){PopType = (int)popType,MessageText = content};
//        }

//        private static void SendPop(Guid managerId, EnumPopType popType, string content)
//        {
//            _threadPool.Add(() => doSendPop(managerId,popType, content));
//        }

//        public static void doSendPop(Guid managerId, EnumPopType popType, string content)
//        {
//            try
//            {
//                _proxy.SendPopChannelMessage(managerId, (int)popType, content);
//            }
//            catch (Exception ex)
//            {
//                SystemlogMgr.Error("doSendPop", ex);
//            }
//        }

//        #region GuildPop
//        public static void SendGuildInvitePop(Guid managerId, string srcManagerName, string guildName, long reqNo)
//        {
//            string content = string.Format("ID,{0}|M,{1}|G,{2}", reqNo, srcManagerName, guildName);
//            SendPop(managerId, EnumPopType.GuildInviteChatPop, content);
//        }
//        #endregion
//        #endregion

//        #region Banner

//        public static void SendBannerScouting(Guid managerId, string managerName, int itemCode,int kpi)
//        {
//            string content = string.Format("N,{0}|AI,{1}", managerName, itemCode);
//            SendBanner(managerId,EnumBannerType.Scouting, content);

//            if (kpi >= 86)
//            {
//                SendBanner(managerId,EnumBannerType.RemindScouting,content);
//            }
//        }

//        public static void SendBannerSynthesis(Guid managerId, string managerName, DicItemEntity itemEntity)
//        {
//                    string content = string.Format("N,{0}|AI,{1}", managerName, itemEntity.ItemCode);
//                    SendBanner(managerId, EnumBannerType.Synthesis, content);
//        }

//        public static void SendBannerEquipmentSynthesis(Guid managerId, string managerName, DicItemEntity itemEntity)
//        {
//            string content = string.Format("N,{0}|AI,{1}", managerName, itemEntity.ItemCode);
//            SendBanner(managerId, EnumBannerType.RemindSynthesis, content);
//        }

//        public static void SendBannerSkillAsk(Guid managerId, string managerName, string skillCode)
//        {
//            string content = string.Format("N,{0}|SI,{1}", managerName, skillCode);
//            SendBanner(managerId, EnumBannerType.SkillAsk, content);
//        }

//        public static void SendBannerStrengthen(Guid managerId, string managerName, int itemCode, int strength)
//        {
//            string content = string.Format("N,{0}|AI,{1}|S,{2}", managerName, itemCode, strength);
//            SendBanner(managerId, EnumBannerType.Strengthen, content);
//            if (strength == 9)
//            {
//                var itemCache = CacheFactory.ItemsdicCache.GetItem(itemCode);
//                if (itemCache != null)
//                {
//                    if (itemCache.PlayerCardLevel == (int) EnumPlayerCardLevel.Orange ||
//                        itemCache.PlayerCardLevel == (int) EnumPlayerCardLevel.Gold)
//                    {
//                        var content1 = string.Format("N,{0}|AI,{1}", managerName, itemCode);
//                        SendBanner(managerId, EnumBannerType.RemindStrength, content1);
//                    }
//                }
//            }
//        }

//        public static void SendBannerArenaWinning5(Guid managerId, string managerName)
//        {
//            string content = string.Format("N,{0}", managerName);
//            SendBanner(managerId, EnumBannerType.ArenaWinning5, content);
//        }

//        public static void SendBannerArenaWinning10(Guid managerId, string managerName)
//        {
//            string content = string.Format("N,{0}", managerName);
//            SendBanner(managerId, EnumBannerType.ArenaWinning10, content);
//        }

//        public static void SendBannerArenaWinning30(Guid managerId, string managerName)
//        {
//            string content = string.Format("N,{0}", managerName);
//            SendBanner(managerId, EnumBannerType.ArenaWinning30, content);
//        }

//        public static void SendBannerArenaRanking(Guid managerId, string homeName, string awayName, int rank)
//        {
//            string content = string.Format("N,{0}|M,{1}|R,{2}", homeName,awayName,rank);
//            SendBanner(managerId, EnumBannerType.ArenaRanking, content);
//        }

//        public static void SendBannerWCH(Guid managerId,string managerName,int stage,int itemCode)
//        {
//            string content = string.Format("N,{0}|AI,{1}|R,{2}", managerName, itemCode, stage);
//            SendBanner(managerId, EnumBannerType.RemindWCHPrize, content);
//        }

//        public static void SendCrowdBanner(string msg)
//        {
//            SendBanner(Guid.NewGuid(), EnumBannerType.CrowdBanner, msg);
//        }

//        public static void SendBannerCrowdEnd()
//        {
//            SendBanner(Guid.NewGuid(), EnumBannerType.CrowdEnd, "");
//        }

//        public static void SendBannerCrowdOpen()
//        {
//            SendBanner(Guid.NewGuid(), EnumBannerType.CrowdOpen, "");
//        }

//        public static void SendHireAuctionOpen()
//        {
//            SendBanner(Guid.NewGuid(), EnumBannerType.HireAuctionOpen, "");
//        }

//        public static void SendHireAuctionClose()
//        {
//            SendBanner(Guid.NewGuid(), EnumBannerType.HireAuctionClose, "");
//        }

//        public static string BuildBannerCrowdKill(string managerName, string byName)
//        {
//            return string.Format("{2}@N,{0}|M,{1}", managerName, byName, (int)EnumBannerType.CrowdKill);
//        }

//        public static string BuildBannerCrowd3Win(string managerName)
//        {
//            return string.Format("{1}@N,{0}", managerName, (int)EnumBannerType.Crowd3Win);
//        }

//        public static string BuildBannerCrowd5Win(string managerName)
//        {
//            return string.Format("{1}@N,{0}", managerName, (int)EnumBannerType.Crowd5Win);
//        }

//        public static string BuildBannerCrowdWinOver(string managerName, int homeScore, int awayScore, string awayName)
//        {
//            return string.Format("{4}@N,{0}|H,{1}|A,{2}|M,{3}", managerName, homeScore, awayScore, awayName, (int)EnumBannerType.CrowdWinOver);
//        }


//        public static void SendRemindGuidePrize(Guid managerId, string managerName, int day, int itemCode)
//        {
//            string content = string.Format("N,{0}|AI,{1}|D,{2}", managerName, itemCode, day);
//            SendBanner(managerId, EnumBannerType.RemindGuidePrize, content);
//        }

//        public static void SendRemindLevelGift(Guid managerId, string managerName, int itemCode,int strength1,int strength2)
//        {
//            string content = string.Format("N,{0}|AI,{1}|S,{2}|C,{3}", managerName, itemCode,strength1,strength2);
//            SendBanner(managerId, EnumBannerType.RemindLevelGift, content);
//        }

//        public static void SendRemindVipGift(Guid managerId, string managerName, int itemCode,int vipLevel)
//        {
//            string content = string.Format("N,{0}|AI,{1}|R,{2}", managerName, itemCode,vipLevel);
//            SendBanner(managerId, EnumBannerType.RemindVipGift, content);
//        }

//        public static void SendRemindAuctionSale(Guid managerId, string managerName, int itemCode, int point)
//        {
//            var itemCache = CacheFactory.ItemsdicCache.GetItem(itemCode);
//            if (itemCache != null)
//            {
//                if (itemCache.ItemType == (int) EnumItemType.PlayerCard && itemCache.PlayerKpi >= 86)
//                {
//                    doSendRemindAuctionSale(managerId, managerName, itemCode, point);
//                }
//                else if(itemCache.ItemType == (int) EnumItemType.Equipment)
//                {
//                    if (itemCache.EquipmentQuality == (int)EnumEquipmentQuality.Excellent
//                        || itemCache.EquipmentQuality == (int)EnumEquipmentQuality.Epic)
//                    {
//                        doSendRemindAuctionSale(managerId, managerName, itemCode, point);
//                    }
//                }
//            }
            
//        }

//        static void doSendRemindAuctionSale(Guid managerId, string managerName, int itemCode, int point)
//        {
//            string content = string.Format("N,{0}|AI,{1}|P,{2}", managerName, itemCode, point);
//            SendBanner(managerId, EnumBannerType.RemindAuctionSale, content);
//        }

//        public static void SendRemindGrow(Guid managerId, string managerName, int growLevel)
//        {
//            string content = string.Format("N,{0}|GI,{1}", managerName,  growLevel);
//            SendBanner(managerId, EnumBannerType.RemindGrow, content);
//        }

//        public static void SendRemindGoldmall(Guid managerId, string managerName, int itemCode)
//        {
//            string content = string.Format("N,{0}|AI,{1}", managerName, itemCode);
//            SendBanner(managerId, EnumBannerType.RemindGoldmall, content);
//        }

//        public static void SendRemindDaily(Guid managerId, string managerName)
//        {
//            string content = string.Format("N,{0}", managerName);
//            SendBanner(managerId, EnumBannerType.RemindDaily, content);
//        }

//        public static void SendRemindRevelation(Guid managerId, string managerName, string stageName,int itemCode)
//        {
//            string content = string.Format("N,{0}|M,{1}|AI,{2}", managerName,stageName,itemCode);
//            SendBanner(managerId, EnumBannerType.RemindRevelation, content);
//        }


//        #region 巅峰之战

//        /// <summary>
//        /// 巅峰之战消息
//        /// </summary>
//        /// <param name="msg"></param>
//        public static void BuildPeak( string msg)
//        {
//            SendBanner(Guid.NewGuid(), EnumBannerType.Peak, msg);
//        }

//        /// <summary>
//        /// 巅峰之战活动开启-推送
//        /// </summary>
//        /// <returns></returns>
//        public static void BuildPeakStart()
//        {
//            SendBanner(Guid.NewGuid(), EnumBannerType.PeakStart, "");
//        }

//        /// <summary>
//        /// 巅峰之战活动结束 -推送
//        /// </summary>
//        public static void BuildPeakEnd()
//        {
//            SendBanner(Guid.NewGuid(), EnumBannerType.PeakEnd, "");
//        }

//        /// <summary>
//        /// 巅峰之战连续杀人推送
//        /// </summary>
//        /// <param name="managerName"></param>
//        /// <param name="killCount"></param>
//        /// <param name="portName"></param>
//        /// <param name="managerId"></param>
//        /// <returns></returns>
//        public static string BuildBannerPeakJoinKill(string managerName, int killCount, string portName, Guid managerId)
//        {
//            return string.Format("{0}@M,{1}|N,{2}|K,{3}|C,{4}", (int)EnumBannerType.PeakJoinKill, managerName, killCount, portName, managerId);
//        }

//        /// <summary>
//        /// 巅峰之战绝杀推送
//        /// </summary>
//        /// <param name="managerName"></param>
//        /// <param name="itemName"></param>
//        /// <returns></returns>
//        public static string BuildBannerPeakFinalHit(string managerName, string itemName)
//        {
//            return string.Format("{0}@M,{1}|ItemName,{2}", (int)EnumBannerType.PeakFinalHit, managerName, itemName);
//        }

//        /// <summary>
//        /// 巅峰之战世界奖励推送
//        /// </summary>
//        /// <param name="managerName"></param>
//        /// <param name="point"></param>
//        /// <param name="itemName"></param>
//        /// <returns></returns>
//        public static string BuildBannerPeakWoldPrize(string managerName, int point, string itemName)
//        {
//            return string.Format("{0}@M,{1}|P,{2}|ItemName,{3}", (int)EnumBannerType.PeakWoldPrize, managerName, point, itemName);
//        }

//        /// <summary>
//        /// 巅峰之战经理信息推送
//        /// </summary>
//        /// <param name="managerId"></param>
//        /// <param name="bossHp"></param>
//        /// <param name="managerHp"></param>
//        /// <param name="bossIntegral"></param>
//        /// <param name="nameIsRed"></param>
//        /// <param name="states"></param>
//        /// <param name="challengeCd"></param>
//        /// <param name="resurgenceTime"></param>
//        /// <param name="jobTime"></param>
//        /// <returns></returns>
//        public static string BuildBannerPeakManagerInfo(Guid managerId,int bossHp,int managerHp, int bossIntegral, bool nameIsRed,int states ,long challengeCd, long resurgenceTime,long jobTime)
//        {
//            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}", (int)EnumBannerType.PeakManagerInfo, managerId,  bossHp, managerHp, bossIntegral, nameIsRed, states, challengeCd, resurgenceTime, jobTime);
//        }

//        #endregion

//        private static void SendBanner(Guid managerId, EnumBannerType bannerType, string content)
//        {
//            _threadPool.Add(() => doSendBanner(managerId, bannerType, content));
//        }



//        public static void doSendBanner(Guid managerId, EnumBannerType bannerType, string content)
//        {
//            try
//            {
//                _proxy.SendBannerChannelMessage(managerId, (int)bannerType, content);
//            }
//            catch (Exception ex)
//            {
//                SystemlogMgr.Error("doSendBanner", ex);
//            }
//        }
//        #endregion

//        #region encapsulation
//        static readonly NBThreadPool _threadPool = new NBThreadPool(10);
//        #endregion
//    }
//}
