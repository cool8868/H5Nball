using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.League;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Manager;
using Games.NBall.Dal;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Response.Auction;
using Games.NBall.Entity.Response.Gamble;
using MsEntLibWrapper.Caching;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.Gamble
{
    public class GambleCore
    {
        #region .ctor
        private static GambleCore _instance = null;
        static readonly object _lockObj = new object();
        private GambleCore()
        {

        }
        #endregion


        #region Instance
        public static GambleCore Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new GambleCore();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        /// <summary>
        /// 坐庄
        /// </summary>
        /// <param name="managerId">庄家ID</param>
        /// <param name="rateStr">赔率字符串（以‘|’分割)</param>
        /// <param name="gambleTitleId">竞猜的主题ID</param>
        /// <param name="hostMoney">奖池的奖金</param>
        public AuctionBuyResponse ToBeHost(Guid managerId, string rateStr, Guid gambleTitleId, int hostMoney, string zoneId = "")
        {
            AuctionBuyResponse mc = new AuctionBuyResponse();
            //如果赔率是空  默认   2|2|2
            if (string.IsNullOrEmpty(rateStr))
            {
                rateStr = "2|2|2";
            }
            try
            {
                #region 各种验证
                //奖池不得低于5000点券
                if (hostMoney < CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.MinGamblePoolMoney))
                {
                    mc.Code = (int)MessageCode.GambleNotEnoughMoney;
                    return mc;
                }
                //是否存在该题目
                GambleTitleEntity gte = GambleTitleMgr.GetById(gambleTitleId, zoneId);
                if (gte == null)
                {
                    mc.Code = (int)MessageCode.GambleTitleNoExist;
                    return mc;
                }
                //该题目是否允许玩家坐庄
                if (managerId != LeagueConst.GambleNpcId)
                {
                    if (gte.IsOfficial == 1)
                    {
                        mc.Code = (int)MessageCode.ThisGambleTitleIsOfficial;
                        return mc;
                    }
                }

                //最多三个庄家，检查有没有超过3家
                if (gte.CurrentCount >= 3)
                {
                    mc.Code = (int)MessageCode.GambleHostsIsFull;
                    return mc;
                }
                //竞猜还没有开始
                if (gte.StartTime > DateTime.Now)
                {
                    mc.Code = (int)MessageCode.GambleNotStart;
                    return mc;
                }

                if (gte.StopTime < DateTime.Now)
                {
                    mc.Code = (int)MessageCode.GambleNotStart;
                    return mc;
                }



                //验证赔率和玩家资金

                string[] rateArr = rateStr.Split('|');
                List<GambleOptionEntity> optionList = GambleOptionMgr.GetByTitleId(gambleTitleId, zoneId);
                if (optionList == null || optionList.Count == 0 || optionList.Count != rateArr.Length)
                {
                    mc.Code = (int)MessageCode.GambleParamError;
                    return mc;
                }
                decimal[] rateArrM = new decimal[rateArr.Length];
                for (int i = 0, count = optionList.Count; i < count; i++)
                {
                    rateArrM[i] = decimal.Round(Convert.ToDecimal(rateArr[i]), 2);
                }

                #endregion

                //支付奖金

                string billingId = "Gamble" + ShareUtil.GenerateComb();
                GambleHostEntity ghe = new GambleHostEntity();
                ghe.ManagerId = managerId;
                if (managerId == LeagueConst.GambleNpcId)
                {
                    ghe.ManagerName = "Official_Manager";
                }
                else
                {
                    ghe.ManagerName = ManagerCore.Instance.GetManager(managerId).Name;
                }
                ghe.TitleId = gambleTitleId;
                ghe.Status = 0;
                ghe.RowTime = DateTime.Now;
                ghe.TotalMoney = hostMoney;
                ghe.HostMoney = hostMoney;
                string conn = ConnectionFactory.Instance.GetConnectionString(zoneId, EnumDbType.Main);
                using (var transactionManager = new TransactionManager(conn))
                {
                    transactionManager.BeginTransaction();
                    if (managerId != LeagueConst.GambleNpcId)
                    {
                        MessageCode code = PayCore.Instance.GambleTrueMatch(managerId, hostMoney, billingId, transactionManager.TransactionObject);
                        if (code != MessageCode.Success)
                        {
                            transactionManager.Rollback();
                            mc.Code = (int)code;
                            return mc;
                        }
                    }
                    bool addResult = GambleTitleMgr.AddCount(gambleTitleId, transactionManager.TransactionObject, zoneId);
                    if (!addResult)
                    {
                        transactionManager.Rollback();
                        mc.Code = (int)MessageCode.GambleHostsIsFull;
                        return mc;
                    }
                    int idxx = 0;
                    if (!GambleHostMgr.InsertOnce(managerId, ghe.ManagerName, ghe.TitleId, ghe.HostMoney, ghe.TotalMoney, ghe.RowTime, ref idxx, transactionManager.TransactionObject))
                    {
                            transactionManager.Rollback();
                            mc.Code = (int) MessageCode.GambleOnlyOnce;
                            return mc;
                    }
                    ghe.Idx = idxx;
                    GambleHostoptionrateEntity ghoe = new GambleHostoptionrateEntity();
                    ghoe.HostId = ghe.Idx;
                    ghoe.Status = 0;
                    ghoe.RowTime = DateTime.Now;
                    for (int i = 0, count = optionList.Count; i < count; i++)
                    {
                        ghoe.OptionId = optionList[i].Idx;
                        ghoe.WinRate = rateArrM[i];
                        if (!GambleHostoptionrateMgr.Insert(ghoe, transactionManager.TransactionObject, zoneId))
                        {
                            transactionManager.Rollback();
                            mc.Code = (int)MessageCode.GambleHostRateInsertError;
                            return mc;
                        }
                    }
                    transactionManager.Commit();
                }

            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Gamble.ToBeHost", ex);
                mc.Code = (int)MessageCode.Exception;
                return mc;
            }
            mc.Data = new AuctionBuyEntity();
            if (managerId != LeagueConst.GambleNpcId)
            {
                mc.Data.ManagerPoint = PayCore.Instance.GetPoint(managerId);
            }
            mc.Code = (int)MessageCode.Success;
            return mc;
        }
        /// <summary>
        /// 增加奖池奖金
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <param name="gambleTitleId">竞猜主题ID</param>
        /// <param name="addMoney">需要增加的金额</param>
        /// <returns>是否增加奖金成功</returns>
        public AuctionBuyResponse AddMoney(Guid managerId, Guid gambleTitleId, int addMoney)
        {
            #region 各种验证
            AuctionBuyResponse mc = new AuctionBuyResponse();
            GambleHostEntity he = GambleHostMgr.GetByManagerIdAndTitleId(managerId, gambleTitleId);
            if (he == null)
            {
                mc.Code = (int)MessageCode.GambleTitleNoExist;
                return mc;
            }
            //是否存在该题目
            GambleTitleEntity gte = GambleTitleMgr.GetById(he.TitleId);

            if (gte == null)
            {
                mc.Code = (int)MessageCode.GambleTitleNoExist;
                return mc;
            }
            //该题目是否允许玩家坐庄
            if (gte.IsOfficial == 1)
            {
                mc.Code = (int)MessageCode.ThisGambleTitleIsOfficial;
                return mc;
            }
            
            //竞猜还没有开始
            if (gte.StartTime > DateTime.Now)
            {
                mc.Code = (int)MessageCode.GambleNotStart;
                return mc;
            }

            if (gte.StopTime < DateTime.Now)
            {
                mc.Code = (int)MessageCode.GambleNotStart;
                return mc;
            }

            //GambleHostEntity he = GambleHostMgr.GetByManagerIdAndTitleId(managerId, gambleTitleId);
            if (he == null || he.Idx == 0)
            {
                mc.Code = (int)MessageCode.GambleUareNotHost;
                return mc;
            }


            #endregion

            //支付奖金
            string billingId = "Gamble_" + ShareUtil.GenerateComb().ToString();
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    MessageCode code = PayCore.Instance.GambleTrueMatch(managerId, addMoney, billingId, transactionManager.TransactionObject);
                    if (code != MessageCode.Success)
                    {
                        transactionManager.Rollback();
                        mc.Code = (int)MessageCode.GamblePayError;
                        return mc;
                    }
                    he.TotalMoney += addMoney;
                    he.HostMoney += addMoney;
                    bool updateResult = GambleHostMgr.Update(he, transactionManager.TransactionObject);
                    if(!updateResult)
                    {
                        transactionManager.Rollback();
                        mc.Code = (int)MessageCode.GambleAddMoneyError;
                        return mc;
                    }
                    transactionManager.Commit();
                }
            }
            catch(Exception ex)
            {
                SystemlogMgr.Error("Gamble.AddMoney",ex);
                mc.Code = (int)MessageCode.Exception;
            }
            mc.Data = new AuctionBuyEntity();
            mc.Data.ManagerPoint = PayCore.Instance.GetPoint(managerId);
            return mc;
        }
        /// <summary>
        /// 玩家参与竞猜
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <param name="gambleMoney">下注金额</param>
        /// <param name="optionRateId">庄家的哪个选项的竞猜</param>
        /// <returns>是否参与成功</returns>
        public AuctionBuyResponse AttendGamble(Guid managerId, int gambleMoney, int optionRateId)
        {
            #region 各种验证

            NbManagerEntity manager = ManagerCore.Instance.GetManager(managerId);
            
            AuctionBuyResponse mc = new AuctionBuyResponse();
            if (gambleMoney < 1)
            {
                mc.Code = (int)MessageCode.GambleTooPoor;
                return mc;
            }
            if (manager == null)
            {
                 mc.Code = (int)MessageCode.LoginNoUser;
                return mc;
            }
            //是否存在该题目
            GambleTitleEntity gte = GambleTitleMgr.GetByOptionRateId(optionRateId);
            if (gte == null)
            {
                mc.Code = (int)MessageCode.GambleTitleNoExist;
                return mc;
            }

            //竞猜还没有开始
            if (gte.StartTime > DateTime.Now)
            {
                mc.Code = (int)MessageCode.GambleNotStart;
                return mc;
            }

            if (gte.StopTime < DateTime.Now)
            {
                mc.Code = (int)MessageCode.GambleNotStart;
                return mc;
            }
            
            //检查奖金是否够支付
            GambleHostoptionrateEntity ore = GambleHostoptionrateMgr.GetById(optionRateId);
            if (ore == null || ore.Idx == 0)
            {
                mc.Code = (int)MessageCode.Exception;
                return mc;
            }
            GambleHostEntity he = GambleHostMgr.GetById(ore.HostId);
            if (he == null || he.Idx == 0)
            {
                mc.Code = (int)MessageCode.Exception;
                return mc;
            }
            decimal needMoney = (decimal)(ore.GambleMoney + gambleMoney) * ore.WinRate;
            if (needMoney > he.TotalMoney)
            {
                mc.Code = (int)MessageCode.GambleNeedMoreTotalMoney;
                return mc;
            }

            //不能竞猜自己发起的主题
            if (managerId == he.ManagerId)
            {
                mc.Code = (int)MessageCode.GambleCannotGambleSelf;
                return mc;
            }
            //压住金额20钻石：任意等级       100钻石：需VIP3        300钻石：需VIP5
              mc.Code = (int)MessageCode.NbFunctionNotOpen;
            switch (manager.Level)
            {
                case 0:
                case 1:
                case 2:
                    if (gambleMoney != 20)
                        return mc;
                    break;
                case 3:
                case 4:
                    if (gambleMoney != 20 || gambleMoney!=100)
                        return mc;
                    break;
                default:
                    if (gambleMoney != 20 || gambleMoney != 100||gambleMoney!=300)
                        return mc;
                    break;
            }
            //支付金额
            string billingId = "Gamble_" + ShareUtil.GenerateComb().ToString();

            GambleDetailEntity de = new GambleDetailEntity();
            de.ManagerId = managerId;
            de.ManagerName = manager.Name;
            de.GambleMoney = gambleMoney;
            de.HostOptionId = ore.Idx;
            de.ResultMoney = 0;
            de.Status = 0;
            de.RowTime = DateTime.Now;
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    ore.GambleMoney += gambleMoney;
                    ore.AttendPeopleCount += 1;
                    he.TotalMoney += gambleMoney;
                    he.AttendPeopleCount += 1;
                    if (!GambleHostMgr.Update(he, transactionManager.TransactionObject))
                    {
                        transactionManager.Rollback();
                        mc.Code = (int)MessageCode.GambleTooManyPeopleIsGambling;
                        return mc;
                    }
                    if(! GambleHostoptionrateMgr.Update(ore,transactionManager.TransactionObject))
                    {
                        transactionManager.Rollback();
                        mc.Code = (int)MessageCode.GambleTooManyPeopleIsGambling;
                        return mc;
                    }
                    if (!GambleDetailMgr.Insert(de, transactionManager.TransactionObject))
                    {
                        transactionManager.Rollback();
                        mc.Code = (int)MessageCode.GambleTooManyPeopleIsGambling;
                        return mc;
                    }
                    MessageCode code = PayCore.Instance.GambleTrueMatch(managerId, gambleMoney, billingId, transactionManager.TransactionObject);
                    if (code != MessageCode.Success)
                    {
                        transactionManager.Rollback();
                        mc.Code = (int)MessageCode.GamblePayError;
                        return mc;
                    }
                    transactionManager.Commit();
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Gamble.AttendGamble", ex);
                mc.Code = (int)MessageCode.Exception;
            }
            #endregion
            mc.Data = new AuctionBuyEntity();
            mc.Data.ManagerPoint = PayCore.Instance.GetPoint(managerId);
            return mc;
        }

        /// <summary>
        /// 开奖
        /// </summary>
        public void OpenGamble()
        {
            try
            {
                #region deal case
                //获取已经到了开奖时间，还没有开奖的主题
                List<GambleTitleEntity> titleList = GambleTitleMgr.GetNeedOpenGambleTitles();
                if (titleList == null)
                    return;
                if (titleList.Count == 0)
                    return;

                for (int titleIndex = 0, titleCount = titleList.Count; titleIndex < titleCount; titleIndex++)
                {
                    //如果还没有在后台设置最终哪个选项是获胜选项，就报错
                    if (titleList[titleIndex].ResultFlagId == Guid.Empty)
                    {
                        //SystemlogMgr.Error("Gamble.OpenGamble", "titleList[titleIndex].ResultFlagId == 0");
                        continue;
                    }
                    
                    //List<GambleOptionEntity> optionList = GambleOptionMgr.GetByTitleId(titleList[titleIndex].Idx);
                    //if (optionList == null || optionList.Count == 0)
                    //{
                    //    SystemlogMgr.Error("Gamble.OpenGamble", "optionList == null || optionList.Count == 0");
                    //    continue;
                    //}
                    List<GambleHostEntity> hostList = GambleHostMgr.GetByTitleId(titleList[titleIndex].Idx);
                    if (hostList == null || hostList.Count == 0)
                    {
                        //更新状态为已开奖
                        titleList[titleIndex].Status = 2;
                        if (!GambleTitleMgr.Update(titleList[titleIndex]))
                        {
                            string msg = "GambleTitleId:" + titleList[titleIndex].Idx.ToString() + "Update status to 2 error!";
                            SystemlogMgr.Error("Gamble.OpenGamble", msg);
                        }
                        continue;
                    }
                    
                    for (int hostIndex = 0, hostCount = hostList.Count; hostIndex < hostCount; hostIndex++)
                    {
                        List<GambleHostoptionrateEntity> rateList =
                            GambleHostoptionrateMgr.GetByHostId(hostList[hostIndex].Idx);
                        if (rateList == null)
                        {
                            continue;
                        }
                        if (rateList.Count == 0)
                            continue;
                        decimal winRate = 0.00m;
                        int gambleTotalMoney = 0;
                        for (int rateIndex = 0, rateCount = rateList.Count; rateIndex < rateCount; rateIndex++)
                        {
                            List<GambleDetailEntity> detailList = GambleDetailMgr.GetByOptionId(rateList[rateIndex].Idx);
                            if (detailList == null || detailList.Count == 0)
                            {
                                continue;
                            }
                            if (titleList[titleIndex].ResultFlagId == Guid.Empty)
                            {
                                SystemlogMgr.Error("Gamble.OpenGamble", "ResultFlagId is 0");
                                continue;
                            }
                            GambleOptionEntity optionRight = GambleOptionMgr.GetById(titleList[titleIndex].ResultFlagId);
                            #region 结算玩家的竞猜点券
                            //猜中的玩家
                            if (rateList[rateIndex].OptionId == titleList[titleIndex].ResultFlagId)
                            {
                                
                                winRate = rateList[rateIndex].WinRate;
                                gambleTotalMoney = rateList[rateIndex].GambleMoney;
                                //按照赔率进行结算，发送邮件
                                for (int detailIndex = 0, detailCount = detailList.Count; detailIndex < detailCount; detailIndex++)
                                {
                                    //该玩家的竞猜已经开过奖了
                                    if (detailList[detailIndex].Status != 0)
                                    {
                                        continue;
                                    }
                                   
                                    //给玩家结算奖金
                                    int returnMoney = Convert.ToInt32(
                                            (decimal)detailList[detailIndex].GambleMoney *
                                        rateList[rateIndex].WinRate * 0.95m);
                                    //更新状态为猜中
                                    detailList[detailIndex].Status = 1;
                                    detailList[detailIndex].ResultMoney = returnMoney;
                                    if (!GambleDetailMgr.Update(detailList[detailIndex]))
                                        continue;
                                    //string mailContent = "恭喜你，在参与"+titleList[titleIndex].Title +
                                    //    "的竞猜中，成功猜中" + optionRight.OptionContent + "，获得奖励"
                                    //    + returnMoney +"点券";
                                    MailBuilder mailGambler = new MailBuilder(EnumMailType.GambleReturnToGambler,
                                        detailList[detailIndex].ManagerId, titleList[titleIndex].Title, optionRight.OptionContent, EnumCurrencyType.Point, returnMoney);
                                    mailGambler.Save();
                                    //更新竞猜排行榜数据
                                    int subMoney = returnMoney - detailList[detailIndex].GambleMoney;
                                    GambleRankMgr.UpdateData(detailList[detailIndex].ManagerId, detailList[detailIndex].ManagerName, subMoney);
                                }
                            }
                            //没猜中
                            else
                            {
                                for (int detailIndex = 0, detailCount = detailList.Count; detailIndex < detailCount; detailIndex++)
                                {
                                    //更新状态为未猜中
                                    detailList[detailIndex].Status = 2;
                                    GambleDetailMgr.Update(detailList[detailIndex]);
                                    //更新竞猜排行榜数据

                                    GambleRankMgr.UpdateData(detailList[detailIndex].ManagerId, detailList[detailIndex].ManagerName, 0 - detailList[detailIndex].GambleMoney);
                                }
                            }
                            #endregion
                        }

                        #region 结算庄家盈亏
                        if (hostList[hostIndex].ManagerId != LeagueConst.GambleNpcId)
                        {
                            int hostReturnMoney = Convert.ToInt32(winRate * (decimal)gambleTotalMoney);
                            int hostGambleMoney = hostList[hostIndex].TotalMoney - hostReturnMoney;
                            int tax = 0;
                            int subMoney = hostGambleMoney - hostList[hostIndex].HostMoney;
                            if (hostGambleMoney > hostList[hostIndex].HostMoney)
                            {
                                tax = Convert.ToInt32((decimal)subMoney * 0.05m);
                                subMoney -= tax;
                            }
                            int hostMoney = hostGambleMoney - tax;
                            hostList[hostIndex].HostWinMoney = subMoney;
                            //string mailToHost = "你发起的竞猜"+ titleList[titleIndex].Title+"已完成奖励派发，奖池还剩余"+hostMoney
                            //    +"点券，本次竞猜你共盈利"+subMoney+"点券";
                            MailBuilder mailHost = new MailBuilder(EnumMailType.GambleReturnToHost,
                                            hostList[hostIndex].ManagerId, titleList[titleIndex].Title,
                                            EnumCurrencyType.Point, hostMoney,subMoney);
                            mailHost.Save();
                            //更新竞猜排行榜数据
                            
                            string hostName = ManagerCore.Instance.GetManager(hostList[hostIndex].ManagerId).Name;
                            GambleRankMgr.UpdateData(hostList[hostIndex].ManagerId, hostName, subMoney);
                        }
                        #endregion
                        #region 更新Host状态为已开奖
                        hostList[hostIndex].Status = 2;
                        GambleHostMgr.Update(hostList[hostIndex]);
                        #endregion

                    }

                    //更新状态为已开奖
                    titleList[titleIndex].Status = 2;
                    if (!GambleTitleMgr.Update(titleList[titleIndex]))
                    {
                        string msg = "GambleTitleId:" + titleList[titleIndex].Idx.ToString() + "Update status to 2 error!";
                        SystemlogMgr.Error("Gamble.OpenGamble", msg);
                    }


                }
                #endregion

                #region 更新排行榜
                GambleRankMgr.UpdateRank();
                #endregion
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Gamble.OpenGamble", ex);
            }
        }

        /// <summary>
        /// 把已经截至的竞猜，状态标记为1
        /// </summary>
        public void DealStopedGamble()
        {
            //获取已经到了开奖时间，还没有开奖的主题
            try
            {
                List<GambleTitleEntity> titleList = GambleTitleMgr.GetStopedGambleTitles();
                if (titleList == null)
                    return;
                if (titleList.Count == 0)
                    return;
                for (int titleIndex = 0, titleCount = titleList.Count; titleIndex < titleCount; titleIndex++)
                {
                    titleList[titleIndex].Status = 1;
                    GambleTitleMgr.Update(titleList[titleIndex]);
                    List<GambleHostEntity> hostList = GambleHostMgr.GetByTitleId(titleList[titleIndex].Idx);
                    for (int hostIndex = 0, hostCount = hostList.Count; hostIndex < hostCount; hostIndex++)
                    {
                        hostList[hostIndex].Status = 1;
                        GambleHostMgr.Update(hostList[hostIndex]);
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Gamble.DealStopedGamble", ex);
            }
        }

        /// <summary>
        /// top10 奖金排行榜
        /// </summary>
        /// <returns>top10 奖金排行榜</returns>
        public GambleRankListResponse GetTop10Rank(Guid managerId)
        {
            GambleRankListResponse response = new GambleRankListResponse();
            try
            {
                List<GambleRankEntity> list = GambleRankMgr.GetRank(10);
            
                response.Code = (int)MessageCode.Success;
                if (list != null && list.Count != 0)
                {
                    response.Data = list;
                }
                GambleRankEntity rank = GambleRankMgr.GetById(managerId);

                if (rank != null)
                {
                    response.MyRank = rank.RankIndex;
                    response.MyWinPoints = rank.WinTotalMoney;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Gamble.GetTop10Rank", ex);
                response.Code = (int)MessageCode.Exception;
            }
            return response;
        }
        /// <summary>
        /// 获取我的排行
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <returns>我的排行</returns>
        public GambleRankResponse GetMyRank(Guid managerId)
        {
            GambleRankResponse response = new GambleRankResponse();
            try
            {
                GambleRankEntity rank = GambleRankMgr.GetById(managerId);

                if (rank != null)
                    response.Data = rank;
                response.Code = (int)MessageCode.Success;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Gamble.GetMyRank", ex);
                response.Code = (int)MessageCode.Exception;
            }
            return response;
        }

        /// <summary>
        /// 获取我参与的竞猜
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <returns>获取我参与的竞猜</returns>
        public GambleDetailListResponse GetMyGambleList(Guid managerId)
        {
            List<GambleDetailEntity> list = GambleDetailMgr.GetByManagerIdTop10(managerId);
            GambleDetailListResponse response = new GambleDetailListResponse();
            response.Code = (int)MessageCode.Success;
            if (list != null && list.Count != 0)
            {
                response.Data = list;
            }
            return response;
        }

        /// <summary>
        /// 获取我发起的竞猜
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <returns>我发起的竞猜</returns>
        public GambleHostListResponse GetMyHostList(Guid managerId)
        {
            GambleHostListResponse response = new GambleHostListResponse();
            try
            {
                List<GambleHostEntity> list = GambleHostMgr.GetByManagerIdTop10(managerId);
                
                response.Code = (int)MessageCode.Success;
                if (list != null && list.Count != 0)
                {
                    for (int i = 0, count = list.Count; i < count; i++)
                    {
                        List<GambleHostoptionrateEntity> rateList = GambleHostoptionrateMgr.GetByHostId2(list[i].Idx);
                        list[i].RateList = rateList;
                    }
                    response.Data = list;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Gamble.GetMyHostList", ex);
                response.Code = (int)MessageCode.Exception;
            }
            return response;
        }


        /// <summary>
        /// 查看当前可以坐庄的主题
        /// </summary>
        /// <returns>查看当前可以发起的主题</returns>
        public GambleTitleListResponse GetCanHostTitleList()
        {
            GambleTitleListResponse response = new GambleTitleListResponse();
            try
            {
                List<GambleTitleEntity> list = GambleTitleMgr.GetCanHostStartList();
                
                response.Code = (int)MessageCode.Success;
                DateTime BaseTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                
                if (list != null && list.Count != 0)
                {
                    for (int i = 0, count = list.Count; i < count; i++)
                    {
                        list[i].StartedTime = Convert.ToInt64(list[i].StartTime.Subtract(BaseTime).TotalMilliseconds);
                        list[i].StopedTime = Convert.ToInt64(list[i].StopTime.Subtract(BaseTime).TotalMilliseconds);

                        List<GambleOptionEntity> optionList = GambleOptionMgr.GetByTitleId(list[i].Idx);
                        list[i].OptionList = optionList;
                    }
                    response.Data = list;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Gamble.GetCanHostTitleList", ex);
                response.Code = (int)MessageCode.Exception;
            }
            return response;
        }


        /// <summary>
        /// 可以押注的主题
        /// </summary>
        /// <returns>可以押注的主题</returns>
        public GambleHostListResponse GetCanGambleTitleList()
        {
            
            GambleHostListResponse response = new GambleHostListResponse();
            try
            {
                response.RankRewardDate = RankRewardDate();
                List<GambleHostEntity> list = GambleHostMgr.GetStartList();
                if (list != null && list.Count != 0)
                {
                    for (int i = 0, count = list.Count; i < count; i++)
                    {
                        List<GambleHostoptionrateEntity> rateList = GambleHostoptionrateMgr.GetByHostId2(list[i].Idx);
                        AddTeamIcon(rateList);
                       
                        list[i].RateList = rateList;
                    }
                }
                response.Code = (int)MessageCode.Success;
                response.Data = list;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Gamble.GetCanGambleTitleList", ex);
                response.Code = (int)MessageCode.Exception;
            }
            return response;
        }

        private void AddTeamIcon(List<GambleHostoptionrateEntity> rateList)
        {
            for (int j = 0; j < rateList.Count; j++)
            {
                var inners = GambleCache.Instance.GetAllIcon();
                foreach (var inner in inners)
                {
                    if (rateList[j].OptionContent.Contains(inner.Name))
                    {
                        rateList[j].TeamIcon = inner.Idx;
                    }
                }
            }
        }

        /// <summary>
        /// 获取竞猜列表
        /// </summary>
        /// <param name="BeforeDay"></param>
        /// <param name="AfterDay"></param>
        /// <returns></returns>
        public GambleTitleListResponse GetGambleByTime(string beforeDay, string afterDay)
        {
            GambleTitleListResponse response = new GambleTitleListResponse();

            try
            {
                var beforeTime = DateTime.ParseExact(beforeDay, "yyyyMMddHHmmss",null);
                var afterTime = DateTime.ParseExact(afterDay, "yyyyMMddHHmmss", null);

                if ((beforeTime != null && afterTime!=null) && (beforeTime > afterTime))
                {
                    response.Code = (int)MessageCode.NbParameterError;
                    return response;
                }
                response.List = new List<GambleTitleEntity>();
                List<GambleTitleEntity> list = GambleTitleMgr.GetAll();
                if (list != null && list.Count != 0)
                {
                    foreach (var inner in list)
                    {
                        if (inner.StartTime < afterTime || inner.StopTime > beforeTime)
                        {
                            response.List.Add(inner);
                        }
                        
                    }
                    response.Code = (int) MessageCode.Success;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Gamble.GetGambleByTime", ex);
                response.Code = (int)MessageCode.Exception;
            }
            return response;
        }

        /// <summary>
        /// 获取开奖日期
        /// </summary>
        /// <returns></returns>
        private long RankRewardDate()
        {
            try
            {
                DateTime firstTime = GambleTitleMgr.GetFirstTime();
                DateTime now = DateTime.Now;
                for (int i = 1; i < 60; i++)
                {
                    DateTime aimDate = firstTime.AddMonths(i);
                    if (now < aimDate)
                    {
                        DateTime BaseTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                        return Convert.ToInt64(aimDate.Subtract(BaseTime).TotalMilliseconds);
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Gamble.RankRewardDate", ex);
            }
            return 0L;
        }
        /// <summary>
        /// 判断当前时间是否在发奖时间段内
        /// 时间段为到期后的1小时内
        /// </summary>
        /// <returns></returns>
        private bool IsInRewardTime()
        {
            try
            {
                DateTime firstTime = GambleTitleMgr.GetFirstTime();
                DateTime now = DateTime.Now;
                for (int i = 1; i < 60; i++)
                {
                    DateTime aimStartDate = firstTime.AddMonths(i);
                    DateTime aimEndDate = aimStartDate.AddHours(1);
                    if (now >= aimStartDate && now <= aimEndDate)
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Gamble.IsInRewardTime", ex);
            }
            return false;
        }
        
        /// <summary>
        /// 每个月发奖
        /// </summary>
        public void GiveMonthRankReward()
        {
            try
            {
                //如果没到发奖时间
                if (!IsInGiveMonthRewardTime())
                    return;
                //获取最新的发奖操作日志
                GambleRankrewardlogEntity rewardEntity = GambleRankrewardlogMgr.GetLastestOne(string.Empty);
                if (rewardEntity == null)
                {
                    GambleRankrewardlogMgr.AddNewOne();
                    rewardEntity = GambleRankrewardlogMgr.GetLastestOne();
                    return;
                }
                //如果上一次日志记录久远，需要重新创建一条
                if (rewardEntity.UpdateTime.AddMinutes(90) < DateTime.Now)
                {
                    GambleRankrewardlogMgr.AddNewOne();
                    rewardEntity = GambleRankrewardlogMgr.GetLastestOne();
                    return;
                }

                //如果已经发过奖了，直接返回
                if (rewardEntity.Status == 2)
                    return;
                //执行发奖操作
                GiveRankReward(string.Empty);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Gamble.GiveMonthRankReward", ex);
            }
            
        }
        /// <summary>
        /// 判断当前是否在发奖时间内
        /// </summary>
        /// <returns></returns>
        public bool IsInGiveMonthRewardTime()
        {
            DateTime firstTime = GambleTitleMgr.GetFirstTime();
            DateTime now = DateTime.Now;
            for (int i = 1; i < 60; i++)
            {
                DateTime aimDate = firstTime.AddMonths(i);
                if (now < aimDate)
                    return false;
                if (now <= aimDate.AddHours(1))
                {
                    return true;
                }
            }
            return false ;
        }

        /// <summary>
        /// 发排行榜奖励
        /// </summary>
        public bool GiveRankReward(string zoneId)
        {
            try
            {
                //获取最新的发奖操作日志
                GambleRankrewardlogEntity rewardEntity = GambleRankrewardlogMgr.GetLastestOne(zoneId);
                if (rewardEntity == null)
                {
                    GambleRankrewardlogMgr.AddNewOne(null,zoneId);
                    rewardEntity = GambleRankrewardlogMgr.GetLastestOne(zoneId);
                    //return false;
                }
                GiveReward(zoneId);
                if (CheckRewardIsFinished(zoneId))
                {
                    rewardEntity.Status = 2;
                    GambleRankMgr.MoveToHistory(rewardEntity.Idx, zoneId);
                    //GambleRankrewardlogMgr.AddNewOne(null, zoneId);
                }
                rewardEntity.UpdateTime = DateTime.Now;
                GambleRankrewardlogMgr.Update(rewardEntity,null,zoneId);
                //switch ((EnumGambleRankRewardStatus)rewardEntity.Status)
                //{
                //    //判断是否到了发奖时间，到了就改变状态为发奖中，没有就改变updateTime
                //    //发奖时间为过期后的1小时之内
                //    case EnumGambleRankRewardStatus.Init:
                //        if (IsInRewardTime())
                //        {
                //            rewardEntity.Status = 1;
                //        }
                //        break;
                //    //如果是到了发奖中，就执行发奖操作，全部操作完毕了状态改为发奖完成
                //    case EnumGambleRankRewardStatus.Rewarding:
                //        GiveReward();
                //        if(CheckRewardIsFinished())
                //            rewardEntity.Status = 2;
                //        break;
                //    //如果是发奖完成了，需要清数据，然后新增一条操作日志
                //    case EnumGambleRankRewardStatus.Rewarded:
                //        GambleRankrewardlogMgr.AddNewOne();
                //        break;
                //    default: break;
                //}

                rewardEntity.UpdateTime = DateTime.Now;
                GambleRankrewardlogMgr.Update(rewardEntity, null, zoneId);
                return true;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("GambleCore.GiveRankReward", ex);
            }
            return false;
        }
        /// <summary>
        /// 发奖
        /// </summary>
        private void GiveReward(string zoneId)
        {
            try
            {
                List<GambleRankEntity> list = GambleRankMgr.GetRank(10, zoneId);
                for (int i = 0, count = list.Count; i < count; i++)
                {
                    if (list[i].Status == 1)
                        continue;//已经发过奖了

                    //TODO=======邮件发送奖励
                    int cardLib = 0;
                    int rank = list[i].RankIndex;
                    if (rank == 1)
                        cardLib = 9;
                    else if (rank == 2)
                        cardLib = 8;
                    else if (rank >= 3 && rank <= 10)
                        cardLib = 10;
                    if (cardLib > 0)
                    {
                        var itemCode = CacheFactory.LotteryCache.LotteryByLib(cardLib);
                        if (itemCode > 0)
                        {
                            MailInfoEntity mailInfo = new MailInfoEntity();
                            mailInfo.MailType = (int) EnumMailType.AdminSend;
                            mailInfo.ManagerId = list[i].ManagerId;
                            mailInfo.IsRead = false;
                            mailInfo.Status = 0;
                            mailInfo.RowTime = DateTime.Now;
                            mailInfo.ExpiredTime = MailCore.Instance.GetExpiredTime(true, DateTime.Now);
                            mailInfo.ContentString = "竞猜排名奖励|恭喜你竞猜盈利排名第" + rank + "，获得奖励，请查收附件.";
                            mailInfo.MailAttachment=new MailAttachmentEntity();
                            mailInfo.MailAttachment.AddAttachment(1, itemCode, false, 0);
                            mailInfo.Attachment = SerializationHelper.ToByte(mailInfo.MailAttachment);
                            mailInfo.HasAttach = true;
                            MailInfoMgr.Insert(mailInfo, null, zoneId);
                        }
                    }
                    //======已经发奖，改变status标记==============
                    list[i].Status = 1;
                    GambleRankMgr.Update(list[i], null, zoneId);
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("GambleCore.GiveReward", ex);
            }
        }

        /// <summary>
        /// 排名前10的是否都发奖了？
        /// </summary>
        /// <returns>是否都发奖了</returns>
        private bool CheckRewardIsFinished(string zoneId)
        {
            try
            {
                List<GambleRankEntity> list = GambleRankMgr.GetRank(10,zoneId);
                for (int i = 0, count = list.Count; i < count; i++)
                {
                    if (list[i].Status == 0)
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("GambleCore.GiveReward", ex);
            }
            return false;
        }

        

    }
    /// <summary>
    /// 发奖状态枚举
    /// </summary>
    public enum EnumGambleRankRewardStatus
    {
        /// <summary>
        /// 初始状态
        /// </summary>
        Init,
        /// <summary>
        /// 发奖中
        /// </summary>
        Rewarding,
        /// <summary>
        /// 已发奖
        /// </summary>
        Rewarded
    }


    }
