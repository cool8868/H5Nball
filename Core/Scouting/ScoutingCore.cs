using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Core.Activity;
using Games.NBall.Core.Item;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Mall;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Task;
using Games.NBall.Core.Turntable;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Activity;
using Games.NBall.Entity.Enums.Player;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.NBall.Custom.Item;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Scouting;

namespace Games.NBall.Core
{
    /// <summary>
    /// 联赛逻辑处理
    /// </summary>
    public class ScoutingCore
    {
        private readonly int _scoutingLotteryGuildcardlib;
        private int _europeScoutingRate;
        #region 初始化

        public ScoutingCore(int p)
        {
            _scoutingLotteryGuildcardlib = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.GuideLotteryCard);
            _europeScoutingRate = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.EuropeScoutingRate);
        }

        #endregion

        #region 单例

        public static ScoutingCore Instance
        {
            get { return SingletonFactory<ScoutingCore>.SInstance; }
        }

        #endregion

        #region 获取信息

        public ScoutingInfoResponse GetScoutingInfo(Guid managerId)
        {
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null)
                return ResponseHelper.InvalidParameter<ScoutingInfoResponse>();
            var response = ResponseHelper.CreateSuccess<ScoutingInfoResponse>();
            response.Data = new ScoutingInfoEntity();
            response.Data.Coin = manager.Coin;
            response.Data.Point = PayCore.Instance.GetPoint(manager.Account);
            response.Data.FriendShipPoint = manager.FriendShipPoint;
            var list = CacheFactory.ScoutingCache.GetShowList(DateTime.Now);
            if (ActivityExThread.Instance.IsActivity(EnumActivityExEffectType.ScoutingHalfPrice, 0, 0))
            {
                foreach (var item in list)
                {
                    int price = item.CurrencyCount;
                    ActivityExThread.Instance.ScoutingHalfPrice(ref price);
                    item.CurrencyCount = price;
                }
            }
            response.Data.ShowList = list;
            var managerExtra = ManagerCore.Instance.GetManagerExtra(managerId);
            if (managerExtra != null)
            {
                response.Data.CoinFreeTimeTick = ShareUtil.GetTimeTick(managerExtra.CoinScoutingUpdate);
                response.Data.PointFreeTimeTick = ShareUtil.GetTimeTick(managerExtra.ScoutingUpdate);
                response.Data.FriendShipPointFreeTimeTick = ShareUtil.GetTimeTick(managerExtra.FriendScoutingUpdate);
            }
            var scoutingManager = GetById(managerId);
            if (scoutingManager != null)
            {
                response.Data.CoinNeedCount = GetLotteryNeedCount(scoutingManager.CoinLotteryCount);
                response.Data.PointNeedCount = GetLotteryNeedCount(scoutingManager.PointLotteryCount);
                response.Data.FriendShipPointNeedCount = GetLotteryNeedCount(scoutingManager.FriendLotteryCount);
            }
            else
            {
                response.Data.CoinNeedCount = 10;
                response.Data.PointNeedCount = 10;
                response.Data.FriendShipPointNeedCount = 10;
            }
            var scoutingGoldBar = ScoutingGoldbarMgr.GetById(managerId);
            response.Data.GoldBarNeedCount = 10;
            if (scoutingGoldBar != null)
            {
                response.Data.GoldBar = scoutingGoldBar.GoldBarNumber;
                response.Data.GoldBarNeedCount = scoutingGoldBar.ScoutingNumber == 0
                    ? 10
                    : 10 - scoutingGoldBar.ScoutingNumber%10;
            }
            return response;
        }

        private ScoutingManagerEntity GetById(Guid managerId)
        {
            var scoutingManager = ScoutingManagerMgr.GetById(managerId);
            if (scoutingManager.UpdateTime.Date != DateTime.Now.Date)
            {
                scoutingManager.SpecialItemCoin = 0;
                scoutingManager.SpecialItemFriend = 0;
                scoutingManager.SpecialItemPoint = 0;
            }
            return scoutingManager;
        }

        private int GetLotteryNeedCount(int lotteryCount)
        {
            if (lotteryCount == 0)
                return 10;
            else
            {
                if (lotteryCount % 10 == 0)
                    return  1;
                else
                    return 10 - lotteryCount % 10 + 1;
            }
        }

        #endregion

        #region 球探抽卡

        /// <summary>
        /// 球探抽卡
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="scoutingId">1金币抽卡，2点券抽卡，3友情点抽卡</param>
        /// <param name="hasTask"></param>
        /// <param name="count"></param>
        /// <param name="isAutoDec"></param>
        /// <returns></returns>
        public ScoutingLotteryResponse ScoutingLottery(Guid managerId, int scoutingId, bool hasTask, int count,
            bool isAutoDec = false)
        {
            //金条抽卡
            if (scoutingId == 99)
                return ScoutingLotteryGoldBar(managerId, scoutingId, count);
            if (scoutingId > 3)
                return ResponseHelper.InvalidParameter<ScoutingLotteryResponse>();
            bool isTen = count == 10; //是否十连抽
            var configScouting = CacheFactory.ScoutingCache.GetEntity(scoutingId);
            if (configScouting == null)
                return ResponseHelper.InvalidParameter<ScoutingLotteryResponse>();
            var scoutingType = configScouting.Type;

            var scoutingManager = GetById(managerId);
            var limitedOrangeCount = 0;
            ScoutingRecordfordaysMgr.GetCountByTime(managerId, DateTime.Today, DateTime.Today.AddDays(1),scoutingId,
                ref limitedOrangeCount);


            LotteryEntity lottery = null;
            List<int> cardList = null;
            List<int> limitedCardList = new List<int>();

            var activityRate = _europeScoutingRate;
            if (isTen)
            {
                if (!configScouting.HasTen)
                    return ResponseHelper.InvalidParameter<ScoutingLotteryResponse>();
                lottery = CacheFactory.LotteryCache.ScoutingTen(scoutingType, configScouting.OrangeLib,
                    configScouting.LowLib, out cardList, limitedOrangeCount, out limitedCardList);
            }
            else
            {
                lottery = CacheFactory.LotteryCache.LotteryFive(EnumLotteryType.Scouting, scoutingType);
            }
            if (lottery == null)
                return ResponseHelper.Create<ScoutingLotteryResponse>(MessageCode.ScoutingLotteryFail);

            //是否免费
            bool isFree = false;
            DateTime curTime = DateTime.Now;
            ScoutingRecordEntity scoutingRecord = new ScoutingRecordEntity();
            scoutingRecord.ManagerId = managerId;
            bool isAttendActiviyt = true;
            if (!isTen) //新手引导点券抽卡
            {
                var card = ItemsdicCache.Instance.GetItem(lottery.PrizeItemCode);
                //获取经理信息
                var manager = ManagerCore.Instance.GetManagerExtra(managerId);
                if ((scoutingType == 1 && manager.CoinScouting > 0) ||
                    (scoutingType == 2 && manager.Scouting > 0) ||
                    (scoutingType == 3 && manager.FriendScouting > 0))
                {
                    isFree = true;
                    //免费抽卡不能抽到87以上能力值的卡
                    lottery = CacheFactory.LotteryCache.LotteryFive(EnumLotteryType.Scouting, 4); //免费抽卡卡库
                    card = ItemsdicCache.Instance.GetItem(lottery.PrizeItemCode);
                }


                if (configScouting.Type == 2 && scoutingManager.PointLotteryCount == 0)
                {
                    //第一次必得托雷斯
                    lottery.PrizeItemCode = 130153;
                    var itemstring = lottery.ItemString.Split(',');
                    itemstring[1] = lottery.PrizeItemCode.ToString();

                    lottery.ItemString = string.Join(",", itemstring);
                    isAttendActiviyt = false;
                }
                else if (scoutingType == 3 && scoutingManager.FriendLotteryCount%10 == 0) //友情点抽卡十次必得80-84橙卡
                {
                    lottery = CacheFactory.LotteryCache.LotteryFive(EnumLotteryType.Scouting, 5);
                    card = ItemsdicCache.Instance.GetItem(lottery.PrizeItemCode);
                    isAttendActiviyt = false;
                }
                else if (scoutingType == 1 && scoutingManager.CoinLotteryCount%10 == 0) //金币抽卡十次必得80-84橙卡碎片
                {
                    lottery = CacheFactory.LotteryCache.LotteryFive(EnumLotteryType.Scouting, 6);
                    card = ItemsdicCache.Instance.GetItem(lottery.PrizeItemCode);
                    isAttendActiviyt = false;
                }
                else if (scoutingType == 2 && scoutingManager.PointLotteryCount%10 == 0) //钻石抽卡十次必得85-87橙卡
                {
                    lottery = CacheFactory.LotteryCache.LotteryFive(EnumLotteryType.Scouting, 7);
                    card = ItemsdicCache.Instance.GetItem(lottery.PrizeItemCode);
                    isAttendActiviyt = false;
                }
                //C罗、梅西外每天只能出3张89以上的橙卡
                if (limitedOrangeCount >= 3)
                {
                    while (card.PlayerKpi >= 89 || card.LinkId == 30001 || card.LinkId == 30002)
                    {
                        lottery = CacheFactory.LotteryCache.LotteryFive(EnumLotteryType.Scouting, scoutingType);
                        card = ItemsdicCache.Instance.GetItem(lottery.PrizeItemCode);
                    }
                }
                else
                {
                    if (card.PlayerKpi >= 89 && (card.LinkId != 30001 || card.LinkId != 30002))
                    {
                        limitedCardList = new List<int>();
                        limitedCardList.Add(card.ItemCode);
                        isAttendActiviyt = false;
                    }
                }

            }
            if (isTen)
            {
                bool isReplace = true;
                switch (scoutingType)
                {
                    case 1: //金币 
                        for (int i = 0; i < cardList.Count; i++)
                        {
                            var itemcode = ActivityExThread.Instance.SummerGiftBag(1);
                            if (itemcode > 0)
                                cardList[i] = itemcode;
                            else
                            {
                                itemcode = ActivityExThread.Instance.MidAutumnActivity(1, scoutingManager.SpecialItemCoin);
                                if (itemcode > 0)
                                {
                                    scoutingManager.SpecialItemCoin++;
                                    cardList[i] = itemcode;
                                }
                            }
                        }
                        var coinLottery = CacheFactory.LotteryCache.LotteryFive(EnumLotteryType.Scouting, 6);
                        cardList[RandomHelper.GetInt32WithoutMax(0, cardList.Count)] = coinLottery.PrizeItemCode;
                        lottery.ItemString = string.Join(",", cardList);
                        break;
                    case 2: //钻石
                        int number = 0;
                        for (int i = 0; i < cardList.Count; i++)
                        {
                            if (ActivityExThread.Instance.IsActivity(EnumActivityExEffectType.EquipmentDebris, 0, 0))
                            {
                                if (RandomHelper.CheckPercentage(activityRate))
                                {
                                    if (number < 2)
                                    {
                                        cardList[i] = ActivityExThread.Instance.GetRandomDebris();
                                        number++;
                                    }
                                }
                            }
                            var itemcode = ActivityExThread.Instance.SummerGiftBag(2);
                            if (itemcode > 0)
                                cardList[i] = itemcode;
                            else
                            {
                                itemcode = ActivityExThread.Instance.MidAutumnActivity(2, scoutingManager.SpecialItemPoint);
                                if (itemcode > 0)
                                {
                                    scoutingManager.SpecialItemPoint++;
                                    cardList[i] = itemcode;
                                }
                            }
                            var cardLotteryId = ActivityExThread.Instance.ScoutingDebris1(2);
                            if (cardLotteryId > 0)
                            {
                               var code = CacheFactory.LotteryCache.LotteryByLib(cardLotteryId);
                               if (code > 0)
                                   cardList[i] = code;
                            }
                            var card = ItemsdicCache.Instance.GetItem(cardList[i]);
                            if (card.ItemType == (int)EnumItemType.PlayerCard && card.PlayerCardLevel == (int)EnumPlayerCardLevel.Orange && card.PlayerKpi >= 84)
                                isReplace = false;
                        }
                        if (isReplace)
                        {
                            var pointlottery = CacheFactory.LotteryCache.LotteryFive(EnumLotteryType.Scouting, 7);
                            cardList[RandomHelper.GetInt32WithoutMax(0, cardList.Count)] = pointlottery.PrizeItemCode;
                        }
                        lottery.ItemString = string.Join(",", cardList);
                        break;
                    case 3: //友情点
                        for (int i = 0; i < cardList.Count; i++)
                        {
                            var itemcode = ActivityExThread.Instance.SummerGiftBag(3);
                            if (itemcode > 0)
                                cardList[i] = itemcode;
                            else
                            {
                                itemcode = ActivityExThread.Instance.MidAutumnActivity(3, scoutingManager.SpecialItemFriend);
                                if (itemcode > 0)
                                {
                                    scoutingManager.SpecialItemFriend++;
                                    cardList[i] = itemcode;
                                }
                            }
                            var card = ItemsdicCache.Instance.GetItem(cardList[i]);
                            if (card.ItemType == (int) EnumItemType.PlayerCard && card.PlayerCardLevel == (int)EnumPlayerCardLevel.Orange)
                                isReplace = false;
                        }
                        if (isReplace)
                        {
                            var friendlottery = CacheFactory.LotteryCache.LotteryFive(EnumLotteryType.Scouting, 5);
                            cardList[RandomHelper.GetInt32WithoutMax(0, cardList.Count)] = friendlottery.PrizeItemCode;
                        }
                        lottery.ItemString = string.Join(",", cardList);
                        break;
                }
            }
            else
            {
                if (scoutingType == 2 &&
                    ActivityExThread.Instance.IsActivity(EnumActivityExEffectType.EquipmentDebris, 0, 0))
                {
                    if (RandomHelper.CheckPercentage(activityRate))
                    {
                        if (isAttendActiviyt)
                        {
                            lottery.PrizeItemCode = ActivityExThread.Instance.GetRandomDebris();
                            var itemstring = lottery.ItemString.Split(',');
                            itemstring[1] = lottery.PrizeItemCode.ToString();
                            lottery.ItemString = string.Join(",", itemstring);
                        }
                    }
                }
                if (isAttendActiviyt)
                {
                    var itemcode = ActivityExThread.Instance.MidAutumnActivity(scoutingType, scoutingManager.SpecialItemFriend);
                    if (itemcode > 0)
                    {
                        switch (scoutingType)
                        {
                            case 1:
                                scoutingManager.SpecialItemCoin++;
                                break;
                            case 2:
                                scoutingManager.SpecialItemPoint++;
                                break;
                            case 3:
                                scoutingManager.SpecialItemFriend++;
                                break;
                        }
                        lottery.PrizeItemCode = itemcode;
                        var itemstring = lottery.ItemString.Split(',');
                        itemstring[1] = lottery.PrizeItemCode.ToString();
                        lottery.ItemString = string.Join(",", itemstring);
                    }
                }
            }
            scoutingRecord.ItemCode = lottery.PrizeItemCode;
            scoutingRecord.ItemString = lottery.ItemString;
            scoutingRecord.RowTime = curTime;
            scoutingRecord.ScoutingType = configScouting.Type;
            scoutingRecord.Status = 1;
            scoutingRecord.Strength = lottery.Strength;
            //球探抽卡
            var response = MallCore.Instance.Scouting(managerId, configScouting.MallCode, curTime, scoutingRecord, isTen,
                cardList, isAutoDec, isFree);

            if (response.Code == (int) MessageCode.Success)
            {
                if (scoutingManager != null) //记录玩家抽卡信息
                {
                    if (!isTen)
                    {
                        if (scoutingType == 1)
                            scoutingManager.CoinLotteryCount += 1;
                        else if (scoutingType == 2)
                            scoutingManager.PointLotteryCount += 1;
                        else
                            scoutingManager.FriendLotteryCount += 1;
                    }
                    else
                    {
                        if (scoutingType == 1)
                            scoutingManager.CoinTenLotteryCount += 1;
                        else if (scoutingType == 2)
                            scoutingManager.PointTenLotteryCount += 1;
                        else
                            scoutingManager.FriendTenLotteryCount += 1;

                    }
                    scoutingManager.UpdateTime = DateTime.Now;
                    ScoutingManagerMgr.Update(scoutingManager);
                    response.Data.NextPointScouting =GetLotteryNeedCount(scoutingManager.PointLotteryCount);
                    response.Data.NextCoinScouting = GetLotteryNeedCount(scoutingManager.CoinLotteryCount);
                    response.Data.NextFriendScouting = GetLotteryNeedCount(scoutingManager.FriendLotteryCount);
                }

                foreach (var itemcode in limitedCardList)
                {
                    var scoutingRecordDays = new ScoutingRecordfordaysEntity();
                    scoutingRecordDays.ManagerId = managerId;
                    scoutingRecordDays.CardItemCodeThen89 = itemcode;
                    scoutingRecordDays.RowTime = DateTime.Now;
                    scoutingRecordDays.ScoutingType = scoutingType;
                    ScoutingRecordfordaysMgr.Insert(scoutingRecordDays);
                }

                int orangeCount = 0;
                int purpleCount = 0;
                int luckyCoinNumber = 0;
                //点球游戏币数量
                int gameCurrency = 0;
                MailBuilder mail = null;
                if (isTen)
                {
                    foreach (var card in cardList)
                    {
                        HandleOrangeCard(managerId, card, ref orangeCount, ref purpleCount, hasTask, ref luckyCoinNumber);
                        ActivityExThread.Instance.ScoutingDebris(managerId, card, ref mail);
                        if (response.Data.OlympicTheGoldMedalId == 0)
                        {
                            //奥运金牌掉落
                            response.Data.OlympicTheGoldMedalId = OlympicCore.Instance.GetOlympicTheGoldMedal(
                                managerId, (EnumOlympicGeyType) scoutingId);
                        }
                        //点球游戏币 20%概率 最多2个
                        if (RandomHelper.CheckPercentage(20) && gameCurrency < 2)
                            gameCurrency++;
                    }
                }
                else
                {
                    HandleOrangeCard(managerId, response.Data.ItemCode, ref orangeCount, ref purpleCount, hasTask, ref luckyCoinNumber);
                    ActivityExThread.Instance.ScoutingDebris(managerId, response.Data.ItemCode, ref mail);
                    //奥运金牌掉落
                    response.Data.OlympicTheGoldMedalId = OlympicCore.Instance.GetOlympicTheGoldMedal(
                        managerId, (EnumOlympicGeyType)scoutingId);
                    //点球游戏币 20%概率
                    if (RandomHelper.CheckPercentage(20))
                        gameCurrency++;
                }
                if (mail != null)
                    mail.Save();
                //如果有点球活动
                if (PenaltyKickCore.Instance.IsActivity)
                {
                    //成功增加数量
                    int successAddNumber = 0;
                    PenaltykickManagerMgr.AddSystemProduceGameCurrency(managerId, gameCurrency, ref successAddNumber);
                    gameCurrency = successAddNumber;
                }
                else
                    gameCurrency = 0;

                List<PopMessageEntity> popList = TaskHandler.Instance.ScoutingLottery(managerId, 1);
                response.Data.PopMsg = popList;
                response.Data.LuckyCoinNumber = luckyCoinNumber;
                response.Data.GameCurrency = gameCurrency;
                if (response.Data.AddReiki > 0)
                    ManagerCore.Instance.DeleteCache(managerId);
            }
            return response;
        }

        /// <summary>
        /// 金条抽卡
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="scoutingId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private ScoutingLotteryResponse ScoutingLotteryGoldBar(Guid managerId, int scoutingId, int count)
        {
            bool isTen = count == 10; //是否十连抽
            var configScouting = CacheFactory.ScoutingCache.GetEntity(scoutingId);
            if (configScouting == null)
                return ResponseHelper.InvalidParameter<ScoutingLotteryResponse>();
            var scoutingType = configScouting.Type;

            var scoutingManager = ScoutingGoldbarMgr.GetById(managerId);
            int prize = 10;
            ActivityExThread.Instance.ScoutingHalfPrice(ref prize);
            //没有数据直接返回金条数量不足
            if (scoutingManager == null)
                return ResponseHelper.Create<ScoutingLotteryResponse>(MessageCode.ScoutingGoldBarNot);
            var limitedOrangeCount = 0;

            // ScoutingRecordfordaysMgr.GetCountByTime(managerId, DateTime.Today, DateTime.Today.AddDays(1), scoutingId,
            //    ref limitedOrangeCount);

            LotteryEntity lottery = null;
            List<int> cardList = null;
            List<int> limitedCardList = new List<int>();

            if (isTen)
            {
                if (!configScouting.HasTen)
                    return ResponseHelper.InvalidParameter<ScoutingLotteryResponse>();
                lottery = CacheFactory.LotteryCache.ScoutingTen(scoutingType, configScouting.OrangeLib,
                    configScouting.LowLib, out cardList, limitedOrangeCount, out limitedCardList);
                prize = prize*8; //8折  10连
                scoutingManager.TenNumber++;
            }
            else
            {
                lottery = CacheFactory.LotteryCache.LotteryFive(EnumLotteryType.Scouting, scoutingType);
                scoutingManager.ScoutingNumber++;
                if (scoutingManager.ScoutingNumber > 0 && scoutingManager.ScoutingNumber % 10 == 0)
                {
                    lottery = CacheFactory.LotteryCache.LotteryFive(EnumLotteryType.Scouting, 98);
                }
            }
            if (scoutingManager.GoldBarNumber < prize)
                return ResponseHelper.Create<ScoutingLotteryResponse>(MessageCode.ScoutingGoldBarNot);
            scoutingManager.GoldBarNumber = scoutingManager.GoldBarNumber - prize;
            if (lottery == null)
                return ResponseHelper.Create<ScoutingLotteryResponse>(MessageCode.ScoutingLotteryFail);
            DateTime curTime = DateTime.Now;
            ScoutingRecordEntity scoutingRecord = new ScoutingRecordEntity();
            scoutingRecord.ManagerId = managerId;
            if (!isTen)
            {
                var card = ItemsdicCache.Instance.GetItem(lottery.PrizeItemCode);
                if (scoutingManager.ScoutingNumber > 0 && scoutingManager.ScoutingNumber%10 == 0)
                    //抽卡十次必得89及以上的橙卡，元老，红卡，或传奇碎片
                {
                    lottery = CacheFactory.LotteryCache.LotteryFive(EnumLotteryType.Scouting, 98);
                    card = ItemsdicCache.Instance.GetItem(lottery.PrizeItemCode);
                }
                ////C罗、梅西外每天只能出3张89以上的橙卡
                //if (limitedOrangeCount >= 3)
                //{
                //    while (card.PlayerKpi >= 89 || card.LinkId == 30001 || card.LinkId == 30002)
                //    {
                //        lottery = CacheFactory.LotteryCache.LotteryFive(EnumLotteryType.Scouting, scoutingType);
                //        card = ItemsdicCache.Instance.GetItem(lottery.PrizeItemCode);
                //    }
                //}
                //else
                //{
                //    if (card.PlayerKpi >= 89 && (card.LinkId != 30001 || card.LinkId != 30002))
                //    {
                //        limitedCardList = new List<int>();
                //        limitedCardList.Add(card.ItemCode);
                //    }
                //}

            }

            if (isTen)
            {
                bool isReplace = true;
                for (int i = 0; i < cardList.Count; i++)
                {
                    var itemcode = ActivityExThread.Instance.MidAutumnActivity(99,0);
                    if (itemcode > 0)
                    {
                        cardList[i] = itemcode;
                    }
                    var card = ItemsdicCache.Instance.GetItem(cardList[i]);

                    if (card.ItemType == (int) EnumItemType.PlayerCard &&
                        card.PlayerCardLevel == (int) EnumPlayerCardLevel.Orange && card.PlayerKpi >= 89)
                        isReplace = false;
                    else if (card.ItemType == (int) EnumItemType.MallItem)
                    {
                        var player = CacheFactory.PlayersdicCache.GetPlayer(card.ImageId);
                        if (player != null && player.Capacity >= 89)
                            isReplace = false;
                    }
                    lottery.ItemString = string.Join(",", cardList);
                }
                if (isReplace)
                {
                    var pointlottery = CacheFactory.LotteryCache.LotteryFive(EnumLotteryType.Scouting, 98);
                    cardList[RandomHelper.GetInt32WithoutMax(0, cardList.Count)] = pointlottery.PrizeItemCode;
                }
                lottery.ItemString = string.Join(",", cardList);
            }
            scoutingRecord.ItemString = lottery.ItemString;
            scoutingRecord.ItemCode = lottery.PrizeItemCode;
            scoutingRecord.RowTime = curTime;
            scoutingRecord.ScoutingType = configScouting.Type;
            scoutingRecord.Status = 1;
            scoutingRecord.Strength = lottery.Strength;

            scoutingManager.UpdateTiem = DateTime.Now;


            GoldbarRecordEntity goldBarRecord = new GoldbarRecordEntity();
            goldBarRecord.IsAdd = false;
            goldBarRecord.ManagerId = managerId;
            goldBarRecord.Number = prize;
            goldBarRecord.OperationType = (int) EnumTransactionType.ScoutingLottery;
            goldBarRecord.RowTime = DateTime.Now;


            #region 球探抽卡


            var response = MallCore.Instance.Scouting(managerId, DateTime.Now, scoutingRecord, scoutingManager,
                isTen, cardList);

            GoldbarRecordMgr.Insert(goldBarRecord);

            #endregion

            if (response.Code == (int) MessageCode.Success)
            {
                foreach (var itemcode in limitedCardList)
                {
                    var scoutingRecordDays = new ScoutingRecordfordaysEntity();
                    scoutingRecordDays.ManagerId = managerId;
                    scoutingRecordDays.CardItemCodeThen89 = itemcode;
                    scoutingRecordDays.RowTime = DateTime.Now;
                    scoutingRecordDays.ScoutingType = scoutingType;
                    ScoutingRecordfordaysMgr.Insert(scoutingRecordDays);
                }
                MailBuilder mail = null;
                if (isTen)
                {
                    foreach (var itemcode in cardList)
                    {
                        ActivityExThread.Instance.ScoutingDebris(managerId, itemcode, ref mail);
                    }
                }
                else
                {
                    ActivityExThread.Instance.ScoutingDebris(managerId, scoutingRecord.ItemCode, ref mail);
                }
                if (mail != null)
                    mail.Save();
                List<PopMessageEntity> popList = TaskHandler.Instance.ScoutingLottery(managerId, 1);
                response.Data.PopMsg = popList;
                response.Data.LuckyCoinNumber = 0;
                response.Data.GameCurrency = 0;
                response.Data.NextGoldBarScouting = scoutingManager.ScoutingNumber == 0
                    ? 10
                    : 10 - scoutingManager.ScoutingNumber%10;
                if (response.Data.AddReiki > 0)
                    ManagerCore.Instance.DeleteCache(managerId);
            }
            return response;
        }

        public void ReplacePrizeItem(LotteryEntity lottery, int giftCode)
        {
            var old = lottery.PrizeItemCode;
            lottery.PrizeItemCode = giftCode;
            lottery.ItemString = lottery.ItemString.Replace(old.ToString(), giftCode.ToString());
        }

        void HandleOrangeCard(Guid managerId, int itemCode, ref int orangeCount, ref int pupleCount, bool hasTask,ref int luckyCoinNumber)
        {
            var item = CacheFactory.ItemsdicCache.GetItem(itemCode);
            if (item != null && item.ItemType == (int)EnumItemType.PlayerCard)
            {
                if (item.PlayerCardLevel == (int)EnumPlayerCardLevel.Orange)
                {
                    orangeCount++;
                    //抽卡得幸运币
                    if (TurntableCore.Instance.IsActivity)
                    {
                        var truntable = new TurntableFrame(managerId);
                        truntable.Save(true);
                        bool isAddSuccess = false;
                        TurntableManagerMgr.AddSystemProduceLuckyCoin(managerId, ref isAddSuccess);
                        if (isAddSuccess)
                            luckyCoinNumber++;
                    }
                    if (!hasTask)
                    {
                        //ChatHelper.SendBannerScouting(managerId, managerName, item.ItemCode, item.PlayerKpi);
                    }
                }
                else if (item.PlayerCardLevel == (int)EnumPlayerCardLevel.BlackGold)
                {
                    if (!hasTask)
                    {
                        //ChatHelper.SendBannerScouting(managerId, managerName, item.ItemCode, item.PlayerKpi);
                    }
                }
                else if (item.PlayerCardLevel == (int)EnumPlayerCardLevel.Purple)
                {
                    pupleCount++;
                }
                 var player = CacheFactory.PlayersdicCache.GetPlayer(item.LinkId);
                if (player != null)
                {
                    //欧洲之星
                    if (ActivityExThread.Instance.IsActivity(EnumActivityExEffectType.EuropeTheStars, 0, 0))
                    {
                        if (player != null)
                        {
                            if (player.KpiLevel.Trim() == "A")
                            {
                                ActivityExThread.Instance.EuropeTheStars(managerId, EnumActivityExRequire.ScoutingEurope);
                            }
                            else if (player.KpiLevel.Trim() == "A+" || player.KpiLevel.Trim() == "S")
                            {
                                ActivityExThread.Instance.EuropeTheStars(managerId,
                                    EnumActivityExRequire.ScoutingHugeEurope);
                            }
                        }
                    }
                    ActivityExThread.Instance.ScoutingCardLevel(managerId, player.KpiLevel.Trim());
                }
            }
        }

        #endregion
    }
}
